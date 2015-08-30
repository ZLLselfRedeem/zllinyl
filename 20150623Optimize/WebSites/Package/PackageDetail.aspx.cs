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
using VAGastronomistMobileApp.SQLServerDAL;

public partial class Package_PackageDetail : System.Web.UI.Page
{
    private static DataTable dt = new DataTable();
    private static int ID = 0;
    private BatchMoneyOperate bmo = new BatchMoneyOperate();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            new CityDropDownList().BindCity(ddlCity);
            ID = Common.ToInt32(Request.QueryString["id"]);
            if (ID != 0)
            {
                //加载数据
                PackageOperate po = new PackageOperate();
                DataTable dt = po.PackageDetail(ID);
                if (dt.Rows.Count > 0)
                {
                    tbName.Text = dt.Rows[0]["Name"].ToString();
                    tbDescription.Text = dt.Rows[0]["Description"].ToString();
                    if (Common.ToInt32(dt.Rows[0]["PositionType"]) == (int)PositionType.ThisShop)
                    {
                        rbThisShop.Checked = true;
                    }
                    else
                    {
                        rbNearby.Checked = true;
                    }
                    tbDistance.Text = killZero(Convert.ToDecimal(dt.Rows[0]["Distance"]).ToString());
                    tbTimeRange.Text = dt.Rows[0]["TimeRange"].ToString();
                    if (Common.ToInt32(dt.Rows[0]["ISGuestUnitPrice"]) == 1)
                    {
                        cbISGuestUnitPrice.Checked = true;
                        tbMinGuestUnitPrice.ReadOnly = false;
                        tbMaxGuestUnitPrice.ReadOnly = false;
                        tbMinGuestUnitPrice.Text = killZero(Convert.ToDecimal(dt.Rows[0]["MinGuestUnitPrice"].ToString()).ToString());
                        tbMaxGuestUnitPrice.Text = killZero(Convert.ToDecimal(dt.Rows[0]["MaxGuestUnitPrice"].ToString()).ToString());
                    }
                    else
                    {
                        cbISGuestUnitPrice.Checked = false;
                    }
                    if (Common.ToInt32(dt.Rows[0]["ValuationType"]) == (int)ValuationType.Started)
                    {
                        rbStarted.Checked = true;
                        tbStarted.Text = killZero(Convert.ToDecimal(dt.Rows[0]["Cost"].ToString()).ToString());
                    }
                    else
                    {
                        rbByPerson.Checked = true;
                        tbByPerson.Text = killZero(Convert.ToDecimal(dt.Rows[0]["Cost"].ToString()).ToString());
                    }
                    if (Common.ToBool(dt.Rows[0]["EnableFilter"]))
                    {
                        rbYes.Checked = true;
                    }
                    else
                    {
                        rbNo.Checked = true;
                    }
                    ddlLevelRequirements.SelectedValue = dt.Rows[0]["LevelRequirements"].ToString();
                    tbSendLnterval.Text = killZero(Convert.ToDecimal(dt.Rows[0]["SendLnterval"]).ToString());
                    ddlStatus.SelectedValue = Convert.ToInt32(dt.Rows[0]["Status"]).ToString();
                    ddlCity.SelectedValue = dt.Rows[0]["ApplicableCity"].ToString();
                }
            }
            else
            {
                btnUpdate.Text = "添 加";
            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PackageManager.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        //验证
        if(tbDescription.Text.Trim().Equals(string.Empty))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('套餐描述不能为空')</script>");
            return;
        }

        if (rbThisShop.Checked == false && rbNearby.Checked == false)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请选择位置')</script>");
            return;
        }

        if (rbNearby.Checked && Common.ToDecimal(tbDistance.Text) <= 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请填写附近距离')</script>");
            return;
        }

        decimal distance = Common.ToDecimal(SystemConfigManager.GetSystemConfigContent("PackageMaxDistance"));
        if (rbNearby.Checked && Common.ToDecimal(tbDistance.Text) > distance)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('搜索距离不能超过" + distance + "KM')</script>");
            return;
        }

        if (Common.ToInt32(tbTimeRange.Text) <= 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('时间范围必须大于0')</script>");
            return;
        }

        decimal timeRange = Common.ToDecimal(SystemConfigManager.GetSystemConfigContent("PackageMaxTimeRange"));

        if (Common.ToInt32(tbTimeRange.Text) > timeRange)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('时间范围不得大于" + timeRange + "')</script>");
            return;
        }

        if (cbISGuestUnitPrice.Checked && Common.ToDecimal(tbMinGuestUnitPrice.Text) == 0 && Common.ToDecimal(tbMaxGuestUnitPrice.Text) == 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('至少填写一个客单价')</script>");
            return;
        }

        if (!tbMinGuestUnitPrice.Text.Equals(string.Empty) && Common.ToDecimal(tbMinGuestUnitPrice.Text) < 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('客单价最小值不能小于零')</script>");
            return;
        }

        if (!tbMaxGuestUnitPrice.Text.Equals(string.Empty) && Common.ToDecimal(tbMaxGuestUnitPrice.Text) <= 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('客单价最大值必须大于零')</script>");
            return;
        }

        if (!tbMinGuestUnitPrice.Text.Equals(string.Empty) && !tbMaxGuestUnitPrice.Text.Equals(string.Empty)
               && Common.ToDecimal(tbMaxGuestUnitPrice.Text) < Common.ToDecimal(tbMinGuestUnitPrice.Text))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('客单价只能从小到大')</script>");
            return;
        }

        if (rbStarted.Checked == false && rbByPerson.Checked == false)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请选择套餐计价类型')</script>");
            return;
        }

        if (rbYes.Checked == false && rbNo.Checked == false)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请选择是否允许商户筛选点过的菜')</script>");
            return;
        }

        Package model = new Package();
        model.Name = tbTimeRange.Text+"天内";
        if (rbThisShop.Checked)
        {
            model.Name += "在本店消费过";
            model.PositionType = (int)PositionType.ThisShop;
        }
        else
        {
            model.Name += "在附近" + tbDistance.Text + "KM消费过";
            model.PositionType = (int)PositionType.Nearby;
        }
        if (cbISGuestUnitPrice.Checked)
        {
            if (!tbMinGuestUnitPrice.Text.Equals(string.Empty) && !tbMaxGuestUnitPrice.Text.Equals(string.Empty) 
                && Common.ToDecimal(tbMinGuestUnitPrice.Text)!=0 && Common.ToDecimal(tbMaxGuestUnitPrice.Text)!=0)
            {
                model.Name += "，客单价在" + killZero(Common.ToDecimal(tbMinGuestUnitPrice.Text).ToString()) + "元到" + killZero(Common.ToDecimal(tbMaxGuestUnitPrice.Text).ToString()) + "元";
            }
            else if(!tbMinGuestUnitPrice.Text.Equals(string.Empty) && tbMinGuestUnitPrice.Text!="0")
            {
                model.Name += "，客单价在" + killZero(Common.ToDecimal(tbMinGuestUnitPrice.Text).ToString()) + "元以上";
            }
            else if (!tbMaxGuestUnitPrice.Text.Equals(string.Empty) && tbMaxGuestUnitPrice.Text != "0")
            {
                model.Name += "，客单价在" + killZero(Common.ToDecimal(tbMaxGuestUnitPrice.Text).ToString()) + "元以下";
            }
        }

        model.Description = tbDescription.Text;
        model.TimeRange = Common.ToInt32(tbTimeRange.Text);
        model.Distance = Common.ToInt32(tbDistance.Text);
        model.ISGuestUnitPrice = cbISGuestUnitPrice.Checked;
        model.MinGuestUnitPrice = Common.ToDecimal(tbMinGuestUnitPrice.Text);
        model.MaxGuestUnitPrice = Common.ToDecimal(tbMaxGuestUnitPrice.Text);
        if (rbStarted.Checked)
        {
            model.ValuationType = (int)ValuationType.Started;
            model.Cost = Common.ToDecimal(tbStarted.Text);
        }
        else
        {
            model.ValuationType = (int)ValuationType.ByPerson;
            model.Cost = Common.ToDecimal(tbByPerson.Text);
        }
        model.LevelRequirements = Common.ToInt32(ddlLevelRequirements.SelectedValue);
        if (rbYes.Checked)
        {
            model.EnableFilter = true;
        }
        else
        {
            model.EnableFilter = false;
        }
        model.SendLnterval = Common.ToDecimal(tbSendLnterval.Text);
        if (Common.ToInt32(ddlStatus.SelectedValue) == (int)PackageStatus.Enable)
        {
            model.Status = true;
        }
        else
        {
            model.Status = false;
        }
        model.ApplicableCity = Common.ToInt32(ddlCity.SelectedValue);
        model.CreateUser = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID;
        model.CreateDate = DateTime.Now;
        model.UpdateUser = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID;
        model.UpdateDate = model.CreateDate;

        PackageOperate po = new PackageOperate();
        if (ID == 0)
        {
            //insert
            if (po.Insert(model) == 1)
            {
                Response.Redirect("PackageManager.aspx");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('添加失败')</script>");
                return;
            }
        }
        else
        {
            //update
            model.ID = ID;
            if (po.Update(model) == 1)
            {
                Response.Redirect("PackageManager.aspx");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('修改失败')</script>");
                return;
            }
        }
    }

    protected void cbISGuestUnitPrice_CheckedChanged(object sender, EventArgs e)
    {
        if (cbISGuestUnitPrice.Checked)
        {
            tbMinGuestUnitPrice.ReadOnly = false;
            tbMaxGuestUnitPrice.ReadOnly = false;
        }
        else
        {
            tbMinGuestUnitPrice.ReadOnly = true;
            tbMaxGuestUnitPrice.ReadOnly = true;
            tbMinGuestUnitPrice.Text = string.Empty;
            tbMaxGuestUnitPrice.Text = string.Empty;
        }
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