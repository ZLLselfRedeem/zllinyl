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

public partial class Package_PackageStatisticsView : System.Web.UI.Page
{
    private static DataTable dt = new DataTable();
    private static int ID = 0;
    private BatchMoneyOperate bmo = new BatchMoneyOperate();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ID = Common.ToInt32(Request.QueryString["ID"]);
            if (ID != 0)
            {
                //加载数据
                PackageOperate po = new PackageOperate();
                DataTable dtMain = po.PackageStatisticsView(ID);
                if (dtMain.Rows.Count > 0)
                {
                    tbShopName.Text = dtMain.Rows[0]["ShopName"].ToString();
                    tbSendTime.Text = dtMain.Rows[0]["SendTime"].ToString();
                    tbName.Text = dtMain.Rows[0]["Name"].ToString();
                    if(tbName.Text.Trim().Equals(string.Empty))
                    {
                        tbName.Text = "由平台发送";
                    }
                    tbCouponName.Text = dtMain.Rows[0]["CouponName"].ToString();
                    tbMJ.Text = "满" + dtMain.Rows[0]["RequirementMoney"].ToString() + "减" + dtMain.Rows[0]["DeductibleAmount"].ToString() + ",最多减" + dtMain.Rows[0]["MaxAmount"].ToString() + "(" + dtMain.Rows[0]["SheetNumber"].ToString() + ")";
                    tbAmount.Text = killZero(dtMain.Rows[0]["Amount"].ToString());
                    if (tbAmount.Text.Trim().Equals(string.Empty))
                    {
                        tbAmount.Text = "0";
                    }
                    else
                    {
                        if ((int)ValuationType.ByPerson == Common.ToInt32(dtMain.Rows[0]["ValuationType"].ToString()))
                        {
                            tbAmount.Text += "(" + killZero(dtMain.Rows[0]["Cost"].ToString()) + "/人次)";
                        }
                        else
                        {
                            tbAmount.Text += "(" + killZero(dtMain.Rows[0]["Cost"].ToString()) + "/次)";
                        }
                    }
                    tbSendUsers.Text = dtMain.Rows[0]["SendUsers"].ToString();
                    if (tbName.Text == "由平台发送")
                    {
                        tbSendUsers.Text = "-";
                    }
                    tbClickCount.Text = dtMain.Rows[0]["ClickCount"].ToString();


                    CouponGetDetailOperate cgo = new CouponGetDetailOperate();
                    long ReceiveCount = cgo.GetCountByCouponId(ID, false);
                    tbReceiveCount.Text = ReceiveCount.ToString();

                    long UsedCount = cgo.GetCountByCouponId(ID, true);
                    tbUsedCount.Text = UsedCount.ToString();

                    //tbDiscountAmount.Text = (UsedCount * Common.ToDecimal(dtMain.Rows[0]["DeductibleAmount"])).ToString();

                    PreOrder19dianOperate poo = new PreOrder19dianOperate();
                    DataTable dtPrePaidSum = poo.GetPrePaidSumByCouponId(ID);
                    if (dtPrePaidSum.Rows.Count > 0)
                    {
                        tbPrePaidSum.Text = dtPrePaidSum.Rows[0]["prePaidSum"].ToString();
                        tbDiscountAmount.Text = dtPrePaidSum.Rows[0]["RealDeductibleAmount"].ToString();
                    }

                    if (Common.ToDecimal(tbDiscountAmount.Text) == 0)
                    {
                        tbROI.Text = string.Empty;
                    }
                    else
                    {
                        tbROI.Text = (Math.Round(Common.ToDecimal(tbPrePaidSum.Text) / Common.ToDecimal(tbDiscountAmount.Text), 2)).ToString();
                    }
                    
                    if (tbClickCount.Text.Trim().Equals(string.Empty))
                    {
                        tbClickCount.Text = "0"; 
                    }
                    if (tbPrePaidSum.Text.Trim().Equals(string.Empty))
                    {
                        tbPrePaidSum.Text = "0";
                    }
                    if (tbDiscountAmount.Text.Trim().Equals(string.Empty))
                    {
                        tbDiscountAmount.Text = "0";
                    }
                }
            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PackageStatistics.aspx");
    }

    private string killZero(string str)
    {
        if (str.IndexOf('.') != -1)
        {
            return str.TrimEnd('0').TrimEnd('.');
        }
        else
        {
            return str;
        }

    }
}