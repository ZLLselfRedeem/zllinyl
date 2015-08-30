using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogDll;
using VAGastronomistMobileApp.SQLServerDAL.Persistence;
using VAGastronomistMobileApp.TheThirdPartyPaymentDll;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using System.Transactions;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;
using VAGastronomistMobileApp.WebPageDll.ThreadCallBacks;
using System.Threading;

public partial class CustomerServiceProcessing_originalRoadRefund : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            rb_d.Checked = true;
            BindGridView(0, 10);
        }
    }
    /// <summary>
    /// 完成打款操作
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;//转换为按钮类型，获取其所在的行的索引
        ViewState["gdvSelectedIndex"] = index;
        switch (e.CommandName)
        {
            case "finished"://修改不需要具备特殊权限
                HiddenField_Id.Value = GridView_List.DataKeys[index].Values["id"].ToString();
                HiddenField_ConnId.Value = GridView_List.DataKeys[index].Values["connId"].ToString();
                TextBox_HistoryNote.Text = Common.ToString(GridView_List.DataKeys[index].Values["note"]);//显示历史备注信息
                ViewState["historynote"] = TextBox_HistoryNote.Text;
                TextBox_Note.Text = "";
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>ConfirmWindow('Panel_Operate');</script>");
                break;
            case "select":
                string msg = "";
                long id = 0;
                if (long.TryParse(GridView_List.DataKeys[index].Values["id"].ToString(), out id) && id > 0)
                {
                    var originalRoadRefundInfoRepository = ServiceFactory.Resolve<IOriginalRoadRefundInfoRepository>();
                    var originalRoadRefundInfo = originalRoadRefundInfoRepository.GetOriginalRoadRefundInfoById(id);

                    if (originalRoadRefundInfo != null && originalRoadRefundInfo.status == 3)
                    {
                        switch (originalRoadRefundInfo.RefundPayType)
                        {
                            case RefundPayType.微信:
                                #region
                                ITenpayRefundOrderRepository tenpayRefundOrderRepository = ServiceFactory.Resolve<ITenpayRefundOrderRepository>();
                                var tenpayRefundOrder = tenpayRefundOrderRepository.GeTenpayRefundOrderByOriginalRoadRefundInfo(originalRoadRefundInfo.id);

                                if (tenpayRefundOrder != null)
                                {
                                    RequestHandler reqHandler = new RequestHandler(Context);

                                    //通信对象
                                    TenpayHttpClient httpClient = new TenpayHttpClient();

                                    //应答对象
                                    ClientResponseHandler resHandler = new ClientResponseHandler();

                                    //-----------------------------
                                    //设置请求参数
                                    //-----------------------------
                                    reqHandler.init();
                                    reqHandler.setKey(WechatPayConfig.partnerKey);
                                    reqHandler.setGateUrl("https://gw.tenpay.com/gateway/normalrefundquery.xml");


                                    reqHandler.setParameter("partner", WechatPayConfig.partnerId);
                                    //out_trade_no和transaction_id、out_refund_no、refund_id至少一个必填，同时存在时以悠先级高为准，
                                    //悠先级为：refund_id>out_refund_no>transaction_id>out_trade_no
                                    //reqHandler.setParameter("refund_id", "1111218239601201406047837166");
                                    reqHandler.setParameter("out_refund_no", tenpayRefundOrder.OutRefundId);
                                    //reqHandler.setParameter("transaction_id", "1900000109201103020030626316");
                                    //reqHandler.setParameter("out_trade_no", "1144357708");

                                    string requestUrl = reqHandler.getRequestURL();
                                    httpClient.SetCertInfo(WechatPayConfig.CertInfo, WechatPayConfig.CertPasswd);
                                    //设置请求内容
                                    httpClient.SetReqContent(requestUrl);
                                    //设置超时
                                    httpClient.SetTimeOut(10);

                                    string rescontent = "";
                                    //后台调用

                                    if (httpClient.Call())
                                    {
                                        //获取结果
                                        rescontent = httpClient.GetResContent();

                                        resHandler.setKey(WechatPayConfig.partnerKey);
                                        //设置结果参数
                                        resHandler.setContent(rescontent);

                                        //判断签名及结果
                                        if (resHandler.isTenpaySign() && resHandler.getParameter("retcode") == "0")
                                        {
                                            //商户订单号
                                            string out_trade_no = resHandler.getParameter("out_trade_no");
                                            //财付通订单号
                                            string transaction_id = resHandler.getParameter("transaction_id");
                                            //金额,以分为单位
                                            string total_fee = resHandler.getParameter("total_fee");
                                            //如果有使用折扣券，discount有值，total_fee+discount=原请求的total_fee
                                            string discount = resHandler.getParameter("discount");

                                            int refund_count = int.Parse(resHandler.getParameter("refund_count"));

                                            for (int i = 0; i < refund_count; i++)
                                            {
                                                int refund_state = Convert.ToInt32(resHandler.getParameter("refund_state_" + i));
                                                string refund_id = resHandler.getParameter("refund_id_" + i);
                                                //TenpayRefundStatus status;
                                                originalRoadRefundInfo.remittanceTime = new DateTime(1970, 1, 1);
                                                originalRoadRefundInfo.note = "";
                                                switch (refund_state)
                                                {
                                                    case 4:
                                                    case 10:
                                                        tenpayRefundOrder.Status = TenpayRefundStatus.退款成功;
                                                        tenpayRefundOrder.ChangeStatusTime = DateTime.Now;
                                                        originalRoadRefundInfo.status = 2;
                                                        originalRoadRefundInfo.remittanceTime = DateTime.Now;
                                                        originalRoadRefundInfo.note = "微信自动打款";
                                                        break;
                                                    case 3:
                                                    case 5:
                                                    case 6:
                                                        tenpayRefundOrder.Status = TenpayRefundStatus.退款失败;
                                                        tenpayRefundOrder.ChangeStatusTime = DateTime.Now;
                                                        originalRoadRefundInfo.status = 4;
                                                        break;
                                                    case 8:
                                                    case 9:
                                                        //case 11:
                                                        tenpayRefundOrder.Status = TenpayRefundStatus.退款处理中;
                                                        tenpayRefundOrder.ChangeStatusTime = DateTime.Now;
                                                        //修改原路退款状态
                                                        break;
                                                    case 1:
                                                    case 2:
                                                        tenpayRefundOrder.Status = TenpayRefundStatus.未确定;
                                                        tenpayRefundOrder.ChangeStatusTime = DateTime.Now;
                                                        originalRoadRefundInfo.status = 4;
                                                        break;
                                                    case 7:
                                                        tenpayRefundOrder.Status = TenpayRefundStatus.转入代发;
                                                        tenpayRefundOrder.ChangeStatusTime = DateTime.Now;
                                                        originalRoadRefundInfo.status = 4;
                                                        break;
                                                        ;
                                                    default:
                                                        tenpayRefundOrder.Status = TenpayRefundStatus.退款失败;
                                                        tenpayRefundOrder.ChangeStatusTime = DateTime.Now;
                                                        originalRoadRefundInfo.status = 4;
                                                        break;
                                                }
                                                if (tenpayRefundOrder.Status != TenpayRefundStatus.退款处理中)
                                                {

                                                    using (var scope = new TransactionScope())
                                                    {
                                                        try
                                                        {
                                                            tenpayRefundOrderRepository.Update(tenpayRefundOrder);
                                                            originalRoadRefundInfoRepository.Update(originalRoadRefundInfo);

                                                            if (tenpayRefundOrder.Status == TenpayRefundStatus.退款成功)
                                                            {
                                                                if (originalRoadRefundInfo.type == VAOriginalRefundType.PREORDER)
                                                                {
                                                                    IPreOrder19DianInfoRepository preOrder19DianInfoRepository = ServiceFactory.Resolve<IPreOrder19DianInfoRepository>();
                                                                    var orderinfo = preOrder19DianInfoRepository.GetById(tenpayRefundOrder.PreOrder19dianId);

                                                                    if (orderinfo != null)
                                                                    {
                                                                        double prepaidSum = Common.ToDouble(orderinfo.prePaidSum);
                                                                        double refundAmount = Common.ToDouble(tenpayRefundOrder.RefundFee);
                                                                        double refundMoneyClosedSum = Common.ToDouble(orderinfo.refundMoneyClosedSum);

                                                                        if ((prepaidSum - Common.ToDouble(refundAmount + refundMoneyClosedSum)) < 0.001)
                                                                        {
                                                                            preOrder19DianInfoRepository.UpdateOrderRefundStatusAndMoney(tenpayRefundOrder.PreOrder19dianId, tenpayRefundOrder.RefundFee, (int)VAPreorderStatus.Refund);
                                                                        }
                                                                        else
                                                                        {
                                                                            preOrder19DianInfoRepository.UpdateOrderRefundMoney(tenpayRefundOrder.PreOrder19dianId, tenpayRefundOrder.RefundFee);
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    WechatPayOperate wechatPayOperate = new WechatPayOperate();
                                                                    originalRoadRefundInfo.tradeNo = tenpayRefundOrder.OutTradeNo.ToString();//我方产生的交易号
                                                                    wechatPayOperate.UpdateOrderStatus(originalRoadRefundInfo.tradeNo, VAAlipayOrderStatus.REFUNDED);
                                                                }
                                                            }
                                                            scope.Complete();
                                                        }
                                                        catch (Exception exc)
                                                        {
                                                            LogManager.WriteLog(LogFile.Error, DateTime.Now.ToString() + ":" + exc.ToString());
                                                        }
                                                    }
                                                }

                                                msg += tenpayRefundOrder.Status.ToString();
                                                //Label_status
                                                //e.CommandSource.
                                                //l = (Label)e.I.FindControl("Label1");
                                                //Label Label_remitEmployee = e.Row.FindControl("Label_status") as Label;//打款人
                                            }


                                        }
                                        else
                                        {
                                            //错误时，返回结果未签名。
                                            //如包格式错误或未确认结果的，请使用原来订单号重新发起，确认结果，避免多次操作
                                            //Response.Write("业务错误信息或签名错误:" + resHandler.getParameter("retcode") + "," + resHandler.getParameter("retmsg") + "<br>");
                                            msg = "业务错误信息或签名错误:" + resHandler.getParameter("retcode") + "," +
                                                  resHandler.getParameter("retmsg");
                                        }


                                    }
                                    else
                                    {
                                        //后台调用通信失败
                                        Response.Write("call err:" + httpClient.GetErrInfo() + "<br>" + httpClient.GetResponseCode() + "<br>");
                                        msg = "call err:" + httpClient.GetErrInfo();
                                        //有可能因为网络原因，请求已经处理，但未收到应答。
                                    }
                                }
                                #endregion
                                break;
                            case RefundPayType.支付宝:
                                AliSingleTradeQuery aliSingleTradeQuery = new AliSingleTradeQuery();
                                AliRefundOperate aliRefundOperate = new AliRefundOperate();
                                var repositoryContext = ServiceFactory.Resolve<IRepositoryContext>();
                                originalRoadRefundInfo.tradeNo = aliRefundOperate.QueryTradeNo(originalRoadRefundInfo.id);
                                msg = aliSingleTradeQuery.SingleTradeQueryResquest(originalRoadRefundInfo, repositoryContext);
                                break;
                        }
                    }
                    else
                    {
                        msg = "订单已成功";
                    }
                }
                BindGridView(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('" + msg + "');</script>");
                break;
            case "refund":
                long preOrder19dianId = Common.ToInt64(GridView_List.DataKeys[index].Values["connId"]);
                PreOrder19dianManager preOrder19dianManager = new PreOrder19dianManager();
                ThirdPartyPaymentInfo thirdPartyPaymentInfo = preOrder19dianManager.SelectPreorderPayAmount(preOrder19dianId);//该点单使用第三方支付的金额
                var originalRoadRefundInfoRefund = ServiceFactory.Resolve<IOriginalRoadRefundInfoRepository>();
                long originalId = 0;
                if (long.TryParse(GridView_List.DataKeys[index].Values["id"].ToString(), out originalId) && originalId > 0)
                {
                    var originalRoadRefund = originalRoadRefundInfoRefund.GetOriginalRoadRefundInfoById(originalId);
                    originalRoadRefund.tradeNo = thirdPartyPaymentInfo.tradeNo;
                    //这里加入退款流程
                    ThreadPool.QueueUserWorkItem(
                        new RefundCallBack(preOrder19dianId, thirdPartyPaymentInfo,
                            (float)originalRoadRefund.refundAmount, originalRoadRefund).Refund);
                    Thread.Sleep(1500);
                    BindGridView(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('退款申请已提交，请切换到【打款中】查看退款进度');</script>");
                }
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 填写备注，提交表单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_confirm_Click(object sender, EventArgs e)
    {
        string note = TextBox_Note.Text.Trim();
        if (!string.IsNullOrEmpty(note))
        {
            long id = Common.ToInt64(HiddenField_Id.Value);
            double refundAmount = Common.ToDouble(GridView_List.DataKeys[Convert.ToInt32(ViewState["gdvSelectedIndex"])].Values["refundAmount"]);
            VAEmployeeLoginResponse vAEmployeeLoginResponse = (VAEmployeeLoginResponse)Session["UserInfo"];
            int remitEmployee = vAEmployeeLoginResponse.employeeID;

            CustomerServiceOperateLogOperate operate = new CustomerServiceOperateLogOperate();
            long connId = Common.ToInt64(HiddenField_ConnId.Value);
            PreOrder19dianOperate operateOrder = new PreOrder19dianOperate();
            DataTable dtOrder = operateOrder.QueryPreOrderInfoByPreOrder19dianId(connId);
            if (dtOrder.Rows.Count == 1)
            {
                double refundMoneyClosedSum = Common.ToDouble(dtOrder.Rows[0]["refundMoneyClosedSum"]);
                double prepaidSum = Common.ToDouble(dtOrder.Rows[0]["prePaidSum"]);
                PreOrder19dianManager manager = new PreOrder19dianManager();
                bool isOrginalRoadRefund = manager.SelectOriginalRoadRefundLog(id);
                using (TransactionScope scope = new TransactionScope())
                {
                    if (String.IsNullOrWhiteSpace(ViewState["historynote"].ToString()))
                    {
                        note = DateTime.Now.ToString() + "新增备注：" + note;
                    }
                    else
                    {
                        note = ViewState["historynote"] + "。" + DateTime.Now.ToString() + "修改备注：" + note;
                    }
                    bool falgUpdateOriginalRoadRefundApply = operate.UpdateOriginalRoadRefundApply(remitEmployee, note, id, (int)VAOriginalRefundStatus.REMITTED);
                    bool flagUpdatePreOrderRefundInfo = true;
                    bool flagUpdatePreOrderRefundMoneyClosedSum = false;
                    if (!isOrginalRoadRefund)//表示当前点单打款，封闭打款金额只能更新一次
                    {
                        flagUpdatePreOrderRefundMoneyClosedSum = manager.UpdatePreOrderRefundMoneyClosedSum(connId, refundAmount);
                    }
                    else
                    {
                        flagUpdatePreOrderRefundMoneyClosedSum = true;
                    }
                    //if ((prepaidSum - Common.ToDouble(refundAmount + refundMoneyClosedSum)) > -0.01)
                    if ((prepaidSum - Common.ToDouble(refundAmount + refundMoneyClosedSum)) < 0.001)
                    {
                        flagUpdatePreOrderRefundInfo = manager.UpdatePreOrderRefundInfo(connId, VAPreorderStatus.Refund, 0);
                    }
                    if (falgUpdateOriginalRoadRefundApply && flagUpdatePreOrderRefundInfo && flagUpdatePreOrderRefundMoneyClosedSum)//修改点单状态信息
                    {
                        BindGridView(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);//刷新列表信息
                        scope.Complete();
                        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('操作成功');</script>");
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('操作失败');</script>");
                    }
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('当前点单不存在');</script>");
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请填写备注信息');</script>");
        }
    }
    /// <summary>
    /// 绑定列表信息
    /// </summary>
    protected void BindGridView(int str, int end)
    {
        int connId = Common.ToInt32(TextBox_preOrderId.Text.Trim());
        string customerMobilephone = Common.ToString(TextBox_PhoneNum.Text.Trim());
        string strTime = Common.ToString(TextBox_startTime.Text.Trim());
        string endTime = Common.ToString(TextBox_endTime.Text.Trim());
        int status = 0;
        //if (rb_not.Checked)
        //{
        //    status = 1;
        //}
        if (rb_yes.Checked)
        {
            status = 2;
        }
        if (rb_all.Checked)
        {
            status = 3;
        }
        if (rb_d.Checked)
        {
            status = 4;
        }
        if (rb_fail.Checked)
        {
            status = 5;
        }
        int type = 0;
        if (rblRefundType.SelectedValue == "1")
        {
            type = 1;
        }
        else
        {
            type = 2;
        }
        CustomerServiceOperateLogOperate operate = new CustomerServiceOperateLogOperate();
        DataTable dt = operate.QueryOriginalRoadRefundApply(connId, customerMobilephone, strTime, endTime, status, type);
        if (dt.Rows.Count > 0)
        {
            int tableCount = dt.Rows.Count;
            AspNetPager1.RecordCount = tableCount;
            DataTable dt_page = Common.GetPageDataTable(dt, str, end);//分页的DataTable
            GridView_List.DataSource = dt_page; 
            AspNetPager1.Visible = true;
        }
        else
        {
            GridView_List.DataSource = null;
            AspNetPager1.Visible = false;
        }
        GridView_List.DataBind();
    }
    /// <summary>
    /// 分页显示数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindGridView(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        EmployeeOperate operate = new EmployeeOperate();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label Label_remoteOrderStatus = e.Row.FindControl("Label_status") as Label;//状态
            Label Label_remitEmployee = e.Row.FindControl("Label_remitEmployee") as Label;//打款人
            int status = Common.ToInt32(GridView_List.DataKeys[e.Row.RowIndex].Values["status"]);
            int refundPayType = Common.ToInt32(GridView_List.DataKeys[e.Row.RowIndex].Values["RefundPayType"]);

            LinkButton linkbtn = e.Row.FindControl("LinkButton1") as LinkButton;
            if (status == 1)
            {
                Label_remoteOrderStatus.Text = "未打款";
                if (refundPayType == (int)RefundPayType.微信)
                {
                    linkbtn.Text = "退款";
                    linkbtn.CommandName = "refund";
                }
                else
                {
                    linkbtn.Text = "完成打款";
                }
            }
            else if (status == 3)
            {
                Label_remoteOrderStatus.Text = "打款中";
                linkbtn.Text = "查询状态";
                linkbtn.CommandName = "select";
            }
            else if (status == 4)
            {
                Label_remoteOrderStatus.Text = "打款失败";
                linkbtn.Text = "完成打款";
                linkbtn.CommandName = "finished";
            }
            else
            {
                Label_remoteOrderStatus.Text = "已打款";
                linkbtn.Text = "查看备注";
            }
            int remitEmployee = Common.ToInt32(GridView_List.DataKeys[e.Row.RowIndex].Values["remitEmployee"]);
            if (remitEmployee > 0)
            {
                DataTable dt = operate.QueryEmployeeByEmployeeId(remitEmployee);//可以查询那些删除（状态为-1）了的员工
                if (dt.Rows.Count > 0)
                {
                    Label_remitEmployee.Text = Common.ToString(dt.Rows[0]["UserName"]);
                }
                else
                {
                    Label_remitEmployee.Text = "";
                }
            }
            else
            {
                Label_remitEmployee.Text = "";
            }
        }
    }
    /// <summary>
    /// 查询操作
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        BindGridView(0, 10);
    }
    /// <summary>
    /// 切换打款条件 add by wangc 20140324
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rb_CheckedChanged(object sender, EventArgs e)
    {
        BindGridView(0, 10);
    }

    protected string repRemittanceTime(string time)
    {
        DateTime dt = DateTime.MinValue;

        if (DateTime.TryParse(time, out dt) && dt > new DateTime(1970, 1, 1))
        {
            return time;
        }
        return "";

    }
    protected void rblRefundType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGridView(0, 10);
    }
}