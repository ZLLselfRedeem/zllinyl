using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;

public partial class PointsManage_exchangeQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //检查是否从服务员查询页面点击【兑换记录】跳转而来
            if (Page.Request.QueryString["employeeId"] != null && Page.Request.QueryString["employeeId"].Length > 0)
            {
                Panel_QueryCondition.Visible = false;//隐藏页面查询条件
                BindEmployeeExchangeLog(Common.ToInt32(Page.Request.QueryString["employeeId"]));
            }
            else
            {
                BindExchangeLog(0, 10);
            }
        }
    }

    private void BindEmployeeExchangeLog(int employeeId)
    {
        EmployeePointLogOperate _Operate = new EmployeePointLogOperate();
        DataTable dt = _Operate.QueryEmployeeExchangeLog(employeeId);
        if (dt != null && dt.Rows.Count > 0)
        {
            this.gdvExchangeList.DataSource = dt;
            this.gdvExchangeList.DataBind();
        }
    }

    /// <summary>
    /// 根据查询条件查询兑换记录列表
    /// </summary>
    /// <param name="str"></param>
    /// <param name="end"></param>
    private void BindExchangeLog(int str, int end)
    {
        Panel_List.Visible = true;
        Panel_Detail.Visible = false;
        EmployeePointLogOperate _Operate = new EmployeePointLogOperate();
        DataTable dtExchange = _Operate.QueryExchangeLog(rblPeriod.SelectedValue, TextBox_TimeStr.Text.Trim(), TextBox_TimeEnd.Text.Trim(), rblShipStatus.SelectedValue, txbPhoneNumber.Text.Trim(), 0);

        if (dtExchange != null && dtExchange.Rows.Count > 0)
        {
            int cnt = dtExchange.Rows.Count;//总数
            AspNetPager1.RecordCount = cnt;
            DataTable dt_page = Common.GetPageDataTable(dtExchange, str, end);
            this.gdvExchangeList.DataSource = dt_page;
            this.gdvExchangeList.DataBind();
        }
        else
        {
            this.gdvExchangeList.DataSource = null;
            this.gdvExchangeList.DataBind();
        }
    }

    /// <summary>
    /// 自定义选择时间日期
    /// </summary>
    protected void TextBox_TimeStr_TextChanged(object sender, EventArgs e)
    {
        this.rblPeriod.ClearSelection();
        if (AspNetPager1.EndRecordIndex == 0)
        {
            BindExchangeLog(0, 10);
        }
        else
        {
            BindExchangeLog(0, AspNetPager1.EndRecordIndex);
        }
    }

    //操作
    protected void lnkbtnEdit_OnCommand(object sender, CommandEventArgs e)
    {
        int id = Common.ToInt32(e.CommandArgument);
        ViewState["id"] = id;
        Panel_Detail.Visible = true;
        Panel_List.Visible = false;
        switch (e.CommandName)
        {
            case "confirm":
                Confirm(id);
                break;
            case "modify":
                this.divList.Attributes.Add("style", "display:none");
                this.divEdit.Attributes.Add("style", "display:''");
                this.divDetail.Attributes.Add("style", "display:none");

                BindEditData(id, "modify");
                BindPointManageLog(id, "modify");
                break;
            case "detail":
                this.divList.Attributes.Add("style", "display:none");
                this.divEdit.Attributes.Add("style", "display:none");
                this.divDetail.Attributes.Add("style", "display:''");

                BindEditData(id, "detail");
                BindPointManageLog(id, "detail");
                break;
            default:
                break;
        }
    }

    private void Confirm(int id)
    {
        EmployeePointLogOperate _PointOperate = new EmployeePointLogOperate(); //BLL
        EmployeePointOperate _employeeOperate = new EmployeePointOperate();//BLL
        int loginID = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID;//获取当前的登录信息
        int employeeID = _PointOperate.QueryEmployeeID(id);

        string msg = "";
        bool confirmResult = false;
        int confirmStatus = _PointOperate.QueryConfirmStatus(id);
        if (confirmStatus == 1)
        {
            Panel_List.Visible = true;
            Panel_Detail.Visible = false;
            Page.ClientScript.RegisterStartupScript(GetType(), "confirm", "<script>alert('此单据已经确认，无需再次确认')</script>");
            return;
        }
        else//对于没确认的单据
        {
            PointManageLog log = new PointManageLog();//Model
            log.createdBy = loginID;
            log.createTime = DateTime.Now;
            log.pointLogId = id;
            log.status = 1;

            //先检查积分是否正常
            bool pointValid = _employeeOperate.EmployeePointIsValid(employeeID);
            if (pointValid)//积分正常
            {
                //更改兑换状态为“已兑换”，确认状态为“已确认”
                log.remark = "更改确认状态为【已确认】";
                confirmResult = _PointOperate.UpdateExchangeStatus(2, 1, Common.ToInt64(id), log);
                if (confirmResult)
                {
                    msg = "单据确认成功！";
                }
                else
                {
                    msg = "单据确认失败！";
                }
            }
            else//积分异常
            {
                //兑换状态改为“积分异常”，确认状态为“未确认”，且disable部分栏位
                log.remark = "更改确认状态为【未确认】";
                confirmResult = _PointOperate.UpdateExchangeStatus(-1, -1, Common.ToInt64(id), log);

                if (confirmResult)
                {
                    msg = "此单据积分异常，无法确认！";
                }
                else
                {
                    msg = "操作失败！";
                }
            }
            Page.ClientScript.RegisterStartupScript(GetType(), "confirm", "<script>alert('" + msg + "')</script>");
        }
        BindExchangeLog(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }
    /// <summary>
    /// 根据记录ID绑定积分兑换记录
    /// </summary>
    /// <param name="id"></param>
    private void BindEditData(int id, string type)
    {
        EmployeePointLogOperate _Operate = new EmployeePointLogOperate();//BLL
        DataTable dtExchangeLog = _Operate.QueryExchangeLog("", "", "", "", "", id);
        if (dtExchangeLog != null && dtExchangeLog.Rows.Count > 0)
        {
            #region
            switch (type)
            {
                case "modify":
                    this.lbIDEdit.Text = dtExchangeLog.Rows[0]["id"].ToString();
                    switch (dtExchangeLog.Rows[0]["exchangeStatus"].ToString())
                    {
                        case "-1":
                            this.lbExchangeStatusEdit.Text = "积分异常";
                            this.lbExchangeStatusEdit.ForeColor = System.Drawing.Color.Red;

                            this.txbAddressEdit.Enabled = false;
                            this.rblShipStatusEdit.Enabled = false;
                            this.txbPlatformEdit.Enabled = false;
                            this.txbSerialNumberEdit.Enabled = false;

                            break;
                        case "1":
                            this.lbExchangeStatusEdit.Text = "处理中";
                            break;
                        case "2":
                            this.lbExchangeStatusEdit.Text = "已兑换";
                            break;
                        default:
                            break;
                    }
                    this.lbGoodsNameEdit.Text = dtExchangeLog.Rows[0]["GoodsName"].ToString();
                    this.lbEmployeeNameEdit.Text = dtExchangeLog.Rows[0]["EmployeeFirstName"].ToString();
                    this.lbPhoneNumberEdit.Text = dtExchangeLog.Rows[0]["UserName"].ToString();
                    this.txbAddressEdit.Text = dtExchangeLog.Rows[0]["address"].ToString();
                    this.lbExchangeTimeEdit.Text = dtExchangeLog.Rows[0]["operateTime"].ToString();
                    this.lbPointVariationEdit.Text = dtExchangeLog.Rows[0]["pointVariation"].ToString();
                    this.lbConfirmStatusEdit.Text = dtExchangeLog.Rows[0]["confirmStatus"].ToString();
                    this.lbConfirmTimeEdit.Text = dtExchangeLog.Rows[0]["confirmTime"].ToString();
                    this.lbConfirmByEdit.Text = dtExchangeLog.Rows[0]["confirmBy"].ToString();
                    this.txbRemarkEdit.Text = dtExchangeLog.Rows[0]["exchangeRemark"].ToString();
                    switch (dtExchangeLog.Rows[0]["shipStatus"].ToString())
                    {
                        case "未发货/未充值":
                            this.rblShipStatusEdit.SelectedValue = "-1";
                            break;
                        case "已发货/已充值":
                            this.rblShipStatusEdit.SelectedValue = "1";
                            break;
                        default:
                            break;
                    }
                    if (lbConfirmStatusEdit.Text == "未确认")
                    {
                        this.rblShipStatusEdit.Enabled = false;
                        this.lbMsg.Visible = true;
                    }
                    else
                    {
                        this.rblShipStatusEdit.Enabled = true;
                        this.lbMsg.Visible = false;
                    }
                    this.txbPlatformEdit.Text = dtExchangeLog.Rows[0]["platform"].ToString();
                    this.txbSerialNumberEdit.Text = dtExchangeLog.Rows[0]["serialNumber"].ToString();
                    this.lbShipByEdit.Text = dtExchangeLog.Rows[0]["shipBy"].ToString();
                    break;
                case "detail":
                    this.lbIDDetail.Text = dtExchangeLog.Rows[0]["id"].ToString();
                    switch (dtExchangeLog.Rows[0]["exchangeStatus"].ToString())
                    {
                        case "-1":
                            this.lbExchangeStatusDetail.Text = "积分异常";
                            this.lbExchangeStatusDetail.ForeColor = System.Drawing.Color.Red;
                            break;
                        case "1":
                            this.lbExchangeStatusDetail.Text = "处理中";
                            break;
                        case "2":
                            this.lbExchangeStatusDetail.Text = "已兑换";
                            break;
                        default:
                            break;
                    }
                    this.lbGoodsNameDetail.Text = dtExchangeLog.Rows[0]["GoodsName"].ToString();
                    this.lbEmployeeNameDetail.Text = dtExchangeLog.Rows[0]["EmployeeFirstName"].ToString();
                    this.lbPhoneNumberDetail.Text = dtExchangeLog.Rows[0]["UserName"].ToString();
                    this.lbAddressDetail.Text = dtExchangeLog.Rows[0]["address"].ToString();
                    this.lbExchangeTimeDetail.Text = dtExchangeLog.Rows[0]["operateTime"].ToString();
                    this.lbPointVariationDetail.Text = dtExchangeLog.Rows[0]["pointVariation"].ToString();
                    this.lbConfirmStatusDetail.Text = dtExchangeLog.Rows[0]["confirmStatus"].ToString();
                    this.lbConfirmTimeDetail.Text = dtExchangeLog.Rows[0]["confirmTime"].ToString();
                    this.lbConfirmByDetail.Text = dtExchangeLog.Rows[0]["confirmBy"].ToString();
                    this.lbRemarkDetail.Text = dtExchangeLog.Rows[0]["exchangeRemark"].ToString();
                    this.lbShipStatusDetail.Text = dtExchangeLog.Rows[0]["shipStatus"].ToString().Replace("1", "已发货/已充值").Replace("-1", "未发货/未充值");
                    this.lbPlatformDetail.Text = dtExchangeLog.Rows[0]["platform"].ToString();
                    this.lbSerialNumberDetail.Text = dtExchangeLog.Rows[0]["serialNumber"].ToString();
                    this.lbShipByDetail.Text = dtExchangeLog.Rows[0]["shipBy"].ToString();
                    break;
                default:
                    break;
            }
            #endregion
        }
    }

    /// <summary>
    /// 绑定兑换记录操作日志
    /// </summary>
    /// <param name="pointLogId">兑换记录ID</param>
    /// <param name="type">操作类别</param>
    private void BindPointManageLog(int pointLogId, string type)
    {
        PointManageLogOperate _Operate = new PointManageLogOperate();
        DataTable dtPointMangeLog = _Operate.QueryPointManageLog(pointLogId);
        switch (type)
        {
            case "modify":
                if (dtPointMangeLog != null && dtPointMangeLog.Rows.Count > 0)
                {
                    this.gdvLogEdit.DataSource = dtPointMangeLog;
                    this.gdvLogEdit.DataBind();
                }
                else
                {
                    this.gdvLogEdit.DataSource = null;
                    this.gdvLogEdit.DataBind();
                }
                break;
            case "detail":
                if (dtPointMangeLog != null && dtPointMangeLog.Rows.Count > 0)
                {
                    this.gdvLogDetail.DataSource = dtPointMangeLog;
                    this.gdvLogDetail.DataBind();
                }
                else
                {
                    this.gdvLogDetail.DataSource = null;
                    this.gdvLogDetail.DataBind();
                }
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 分页操作
    /// </summary>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindExchangeLog(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }

    protected void rblPeriod_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindExchangeLog(0, 10);
    }
    protected void rblShipStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindExchangeLog(0, 10);
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindExchangeLog(0, 10);
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        this.rblShipStatus.SelectedValue = "-1";
        this.rblPeriod.SelectedValue = "week";
        this.txbPhoneNumber.Text = "";


        int dayOfWeek = Convert.ToInt32(DateTime.Now.DayOfWeek);
        int daydiff = (-1) * dayOfWeek + 1;
        this.TextBox_TimeStr.Text = DateTime.Now.AddDays(daydiff).ToString("yyyy-MM-dd");
        this.TextBox_TimeEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");

        BindExchangeLog(0, 10);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int employeeID = 0;
        if (Session["UserInfo"] != null)
        {
            employeeID = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID;//获取当前的登录信息
        }
        //服务员积分兑换表
        EmployeePointLog Point = new EmployeePointLog();//Model
        Point.address = this.txbAddressEdit.Text.Trim();
        Point.confirmTime = DateTime.Now;
        Point.confirmBy = employeeID;
        switch (this.lbExchangeStatusEdit.Text)
        {
            case "积分异常":
                Point.exchangeStatus = -1;
                break;
            case "处理中":
                Point.exchangeStatus = 1;
                break;
            case "已兑换":
                Point.exchangeStatus = 2;
                break;
        }
        Point.exchangeRemark = this.txbRemarkEdit.Text.Trim();
        Point.shipStatus = Convert.ToInt32(this.rblShipStatusEdit.SelectedValue);
        if (this.rblShipStatus.SelectedValue == "1")
        {
            Point.shipBy = employeeID;
        }
        Point.platform = this.txbPlatformEdit.Text.Trim();
        Point.serialNumber = this.txbSerialNumberEdit.Text.Trim();
        Point.id = Common.ToInt32(ViewState["id"]);
        Point.employeeId = employeeID;

        //友络工作人员操作日志表
        PointManageLog Log = new PointManageLog();//Model
        EmployeePointLogOperate _Operate = new EmployeePointLogOperate();
        EmployeePointLog oldPoint = _Operate.QueryExchangeLog(Common.ToInt32(ViewState["id"]));

        Log.pointLogId = Common.ToInt32(ViewState["id"]);
        //if (oldPoint.address != Point.address)
        //{
        //    Log.remark += "修改收货门店和地址；";
        //}      
        if (oldPoint.shipStatus != Point.shipStatus)
        {
            Log.remark += "修改发货状态为【" + this.rblShipStatusEdit.SelectedItem.Text + "】";
        }
        if (string.IsNullOrEmpty(Log.remark))
        {
            Log.remark = "其他";
        }
        Log.createTime = DateTime.Now;
        Log.createdBy = employeeID;
        Log.status = 1;

        PointManageLogOperate _logOperate = new PointManageLogOperate();

        bool result = _logOperate.ModifyPointLog(Point, Log);
        if (result)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('保存成功！')</script>");
            BindExchangeLog(0, 10);
            this.divList.Attributes.Add("style", "display:''");
            this.divEdit.Attributes.Add("style", "display:none");
            this.divDetail.Attributes.Add("style", "display:none");
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('保存失败！')</script>");
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Panel_List.Visible = true;
        Panel_Detail.Visible = false;
        this.divList.Attributes.Add("style", "display:block");
        this.divEdit.Attributes.Add("style", "display:none");
        this.divDetail.Attributes.Add("style", "display:none");
        BindExchangeLog(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }
}