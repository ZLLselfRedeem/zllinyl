using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
using VAGastronomistMobileApp.Model;
using System.Transactions;
using Web.Control.DDL;
using System.Net;
using System.Net.Sockets;

public partial class FinanceManage_batchMoneyApplyDetailDS : System.Web.UI.Page
{
    private static DataTable dt = new DataTable();
    private static double Max = 0;
    private BatchMoneyOperate bmo = new BatchMoneyOperate();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          if(!string.IsNullOrEmpty(Common.ToString(Request.QueryString["batchMoneyApplyDetailId"])))
          {
              int batchMoneyApplyDetailId=Common.ToInt32((Request.QueryString["batchMoneyApplyDetailId"]));
              dt=bmo.SelectBatchMoneyApplyDetailByBatchMoneyApplyDetailIdNew(batchMoneyApplyDetailId);
              if (dt.Rows.Count == 0 || !dt.Rows[0]["status"].ToString().Equals(((int)BatchMoneyStatus.wait_for_check).ToString()))
              {
                  btnUpdate.Enabled = false;
              }
              else
              {
                  btnUpdate.Enabled = true;

                  txt_batchMoneyApplyDetailCode.Text = dt.Rows[0]["batchMoneyApplyDetailCode"].ToString();
                  txt_companyName.Text = dt.Rows[0]["companyName"].ToString();
                  txt_shopName.Text = dt.Rows[0]["shopName"].ToString();
                  txt_bankName.Text = dt.Rows[0]["bankName"].ToString();
                  txt_PayeeBankName.Text = dt.Rows[0]["PayeeBankName"].ToString();
                  txt_accountName.Text = dt.Rows[0]["accountName"].ToString();
                  txt_bankAccount.Text = dt.Rows[0]["accountNum"].ToString();
                  txt_createdTime.Text = dt.Rows[0]["createdTime"].ToString();
                  txt_financePlayMoneyTime.Text = dt.Rows[0]["financePlayMoneyTime"].ToString();
                  txt_applyAmount.Value = Common.ToDouble(dt.Rows[0]["applyAmount"].ToString()).ToString();
                  //txt_redEnvelopeAmount.Value = Common.ToDouble(dt.Rows[0]["redEnvelopeAmount"].ToString()).ToString();
                  //txt_foodCouponAmount.Value = Common.ToDouble(dt.Rows[0]["foodCouponAmount"].ToString()).ToString();
                  //txt_wechatPayAmount.Value = Common.ToDouble(dt.Rows[0]["wechatPayAmount"].ToString()).ToString();
                  //txt_alipayAmount.Value = Common.ToDouble(dt.Rows[0]["alipayAmount"].ToString()).ToString();
                  txt_viewallocCommissionValue.Text = dt.Rows[0]["viewallocCommissionValue"].ToString();
                  //txt_commissionAmount.Value = Common.ToDouble(dt.Rows[0]["commissionAmount"].ToString()).ToString();
                  //txt_volume.Value = Common.ToDouble(dt.Rows[0]["volume"].ToString()).ToString();

                  tb_applyAmount.Value = dt.Rows[0]["applyAmount"].ToString();
                  tb_redEnvelopeAmount.Value = dt.Rows[0]["remainRedEnvelopeAmount"].ToString();
                  tb_foodCouponAmount.Value = dt.Rows[0]["remainFoodCouponAmount"].ToString();
                  tb_wechatPayAmount.Value = dt.Rows[0]["remainWechatPayAmount"].ToString();
                  tb_alipayAmount.Value = dt.Rows[0]["remainAlipayAmount"].ToString();
                  tb_commissionAmount.Value = dt.Rows[0]["remainCommissionAmount"].ToString();
                  tb_remainMoney.Value = dt.Rows[0]["remainMoney"].ToString();
                  tb_amountFrozen.Value = dt.Rows[0]["amountFrozen"].ToString();
                  tb_viewallocCommissionType.Value = dt.Rows[0]["viewallocCommissionType"].ToString();
                  double persent = Math.Round(Common.ToDouble(dt.Rows[0]["commissionAmount"].ToString()) / (Common.ToDouble(dt.Rows[0]["commissionAmount"].ToString()) + Common.ToDouble(dt.Rows[0]["applyAmount"].ToString())), 2);
                  //txt_realCommissionValue.Value = persent.ToString();

                  ddlStatus.SelectedValue = dt.Rows[0]["status"].ToString();
                  txt_serialNumberOrRemark.Text = dt.Rows[0]["serialNumberOrRemark"].ToString();
                  if (dt.Rows[0]["isFirst"].Equals(true))
                  {
                      txt_isFirst.Text = "首次打款";
                  }
                  else
                  {
                      txt_isFirst.Text = "非首次打款";
                  }

                  Max = Common.ToDouble(dt.Rows[0]["remainMoney"]) - Common.ToDouble(dt.Rows[0]["amountFrozen"]);
                  lb_maxMin.Text = "（最大可结款金额："
                      + Common.ToDouble(Max.ToString()).ToString()
                      + "元，冻结" + Common.ToDouble(dt.Rows[0]["amountFrozen"].ToString()).ToString() + "元）";//例（最大可结款金额：9392元，冻结2000元）
              }
          }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("batchMoneyApplyDS.aspx?id=" + Request.QueryString["id"].ToString() + "&name=" + Request.QueryString["name"].ToString() + "&Pid=" + Request.QueryString["Pid"].ToString());
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (txt_applyAmount.Value.Trim().Equals(string.Empty))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('结款金额不能为空')</script>");
            return;
        }
        if (Common.ToDouble(txt_applyAmount.Value.Trim()) > Common.ToDouble(Max.ToString()))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('结款金额" + txt_applyAmount.Value + "大于最大可结款金额" + Convert.ToDouble(Max.ToString()).ToString() + "')</script>");
            return;
        }

        if (Common.ToDouble(txt_applyAmount.Value.Trim()) <= 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('结款金额应大于零小于最大可结款金额')</script>");
            return;
        }

        int batchMoneyApplyDetailId = Common.ToInt32((Request.QueryString["batchMoneyApplyDetailId"]));
        dt = bmo.SelectBatchMoneyApplyDetailByBatchMoneyApplyDetailIdNew(batchMoneyApplyDetailId);
        if (dt.Rows.Count == 0 || !dt.Rows[0]["status"].ToString().Equals(((int)BatchMoneyStatus.wait_for_check).ToString()))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('非申请未提交状态不可修改')</script>");
            return;
        }

        BatchMoneyApplyDetail model = new BatchMoneyApplyDetail();
        model.applyAmount = Common.ToDouble(txt_applyAmount.Value.Trim());
        //model.redEnvelopeAmount = Common.ToDouble(txt_redEnvelopeAmount.Value);
        //model.foodCouponAmount = Common.ToDouble(txt_foodCouponAmount.Value);
        //model.wechatPayAmount = Common.ToDouble(txt_wechatPayAmount.Value);
        //model.alipayAmount = Common.ToDouble(txt_alipayAmount.Value);
        //model.commissionAmount = Common.ToDouble(txt_commissionAmount.Value);
        model.batchMoneyApplyDetailId = Common.ToInt32((Request.QueryString["batchMoneyApplyDetailId"]));

        bool result=bmo.ModifySaveBatchMoneyApplyDetailFinance(model);
        if (result == false)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('修改失败')</script>");
            return;
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('修改成功')</script>");
            Response.Redirect("batchMoneyApplyDS.aspx?id=" + Request.QueryString["id"].ToString() + "&name=" + Request.QueryString["name"].ToString() + "&Pid=" + Request.QueryString["Pid"].ToString());
        }
    }
    //public void txt_applyAmount_TextChanged(object sender, EventArgs e)
    //{
    //    if (!Common.ToDouble(txt_applyAmount.Text.Trim()).Equals(0))
    //    {
    //        if (Common.ToDouble(txt_applyAmount.Text.Trim()) > Max)
    //        {
    //            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('结款金额" + txt_applyAmount.Text + "大于最大可结款金额" + Max.ToString() + "')</script>");
    //            return;
    //        }
    //        double[] amount = new double[7];
    //        amount[0] = Common.ToDouble(dt.Rows[0]["redEnvelopeAmount"].ToString());
    //        amount[1] = Common.ToDouble(dt.Rows[0]["foodCouponAmount"].ToString());
    //        amount[2] = Common.ToDouble(dt.Rows[0]["alipayAmount"].ToString());
    //        amount[3] = Common.ToDouble(dt.Rows[0]["wechatPayAmount"].ToString());
    //        amount[4] = Common.ToDouble(dt.Rows[0]["commissionAmount"].ToString());
    //        amount[5] = Common.ToDouble(dt.Rows[0]["applyAmount"].ToString());
    //        amount[6] = Common.ToDouble(txt_applyAmount);
    //        double[] newAmount = mathAmount(amount);
    //        txt_redEnvelopeAmount.Text = newAmount[0].ToString();
    //        txt_foodCouponAmount.Text = newAmount[1].ToString();
    //        txt_alipayAmount.Text = newAmount[2].ToString();
    //        txt_wechatPayAmount.Text = newAmount[3].ToString();
    //        txt_commissionAmount.Text = newAmount[4].ToString();
    //        txt_realCommissionValue.Text = Math.Round(newAmount[4] / amount[5], 2).ToString();
    //    }
    //    else
    //    {
    //        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('结款金额应大于零小于最大可结款金额')</script>");
    //        return;
    //    }
    //}

    /// <summary>
    /// 红包，粮票，支付宝，微信按比例提取
    /// 传入值顺序为 红包、粮票、支付宝、微信、佣金、余额、冻结金额
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    private double[] mathAmount(double[] amount)
    {
        double[] newAmount = new double[6];
        double redP = Math.Round(amount[0] / amount[5], 2);
        //double foodP = Math.Round(amount[1] / amount[5], 2);
        double aliP = Math.Round(amount[2] / amount[5], 2);
        double weP = Math.Round(amount[3] / amount[5], 2);
        //double realamount = amount[5] - amount[6];余额有可能为用户输入 so这里不固定死作为[7]传入
        double realamount = amount[6];

        newAmount[0] = Math.Round(redP * realamount, 2);
        newAmount[2] = Math.Round(aliP * realamount, 2);
        newAmount[3] = Math.Round(weP * realamount, 2);
        newAmount[1] = Math.Round(realamount - newAmount[0] - newAmount[1] - newAmount[2], 2);
        newAmount[4] = Math.Round(realamount / amount[5] * amount[4], 2);
        newAmount[5] = realamount;

        return newAmount;
    }
}