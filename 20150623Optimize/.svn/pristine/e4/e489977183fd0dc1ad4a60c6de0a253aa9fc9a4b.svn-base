using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;

public partial class Award_EditMerchantActivity : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["shopID"] != null)
            {
                int shopID = Common.ToInt32(Request.QueryString["shopID"]);
                GetShopInfo(shopID);
                BindGridViewDish(shopID);
            }
        }
    }
    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        ModifyCompany();
    }

    /// <summary>
    /// 获取商家奖品信息
    /// </summary>
    /// <param name="shopID"></param>
    protected void GetShopInfo(int shopID)
    {
        ShopAwardOperate operateShopAward = new ShopAwardOperate();
        DataTable dt = operateShopAward.SelectBussinessActivity(null, null, null, System.DateTime.MinValue, System.DateTime.MinValue, Convert.ToString(shopID));
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            // 抽奖功能
            if (Common.ToInt32(dr["awardCount"]) > 0)
            {
                dr["awardCount"] = "1";
            }
            else
            {
                dr["awardCount"] = "0";
            }

            // 免排队
            if (Common.ToInt32(dr["avoidQueueCount"]) == 0)
            {
                dr["avoidQueueCount"] = "0";
                radAvoidQueue.SelectedValue = Common.ToString(dr["avoidQueueCount"]);
            }
            else
            {
                // 查询免排队数量
                var listShopAward = operateShopAward.SelectShopAwardList(shopID);
                txtSetAvoidQueueCount.Text = Convert.ToString(listShopAward.Find(s => s.AwardType == AwardType.AvoidQueue).Count);
                radAvoidQueue.SelectedValue = "1";
            }

            // 赠菜
            if (Common.ToInt32(dr["presentDishCount"]) == 0 || Convert.ToString(dr["presentDishCount"]) == "")
            {
                dr["presentDishCount"] = "0";
                radDish.SelectedValue = Common.ToString(dr["presentDishCount"]);
                hidShowDish.Value = "0";
            }
            else
            {
                radDish.SelectedValue = "1";
                hidShowDish.Value = "1";
            }

            // 返现限量
            if (Common.ToInt32(dr["shopRedCount"]) == 0)
            {
                dr["shopRedCount"] = "0";
                radRed.SelectedValue = Common.ToString(dr["shopRedCount"]);
            }
            else
            {
                ShopConnRedEnvelopeOperate operate = new ShopConnRedEnvelopeOperate();
                var objShopConnRedEnvelope = operate.SelectShopConnRedEnvelope(shopID);
                if (objShopConnRedEnvelope.Id > 0)
                {
                    txtSetRedCount.Text = Convert.ToString(objShopConnRedEnvelope.RedEnvelopeConsumeCount);
                    txtSetRedAmount.Text = objShopConnRedEnvelope.RedEnvelopeConsumeAmount.ToString();
                }
                radRed.SelectedValue = "1";
            }

            txtCompanyName.Text = Convert.ToString(dr["companyName"]);
            txtShopName.Text = Convert.ToString(dr["shopName"]);
            radAward.SelectedValue = Common.ToString(dr["awardCount"]);
        }
    }
    /// <summary>
    /// 修改公司奖品信息
    /// </summary>
    protected void ModifyCompany()
    {
        using (TransactionScope scope = new TransactionScope())
        {
            bool isOpen = false;
            bool isAvoidQueue = true;
            bool isShopConnRedEnvelope = true;
            bool isDishInfo = true;
            ShopAwardOperate operateShopAward = new ShopAwardOperate();
            // 打开关闭抽奖功能
            if (radAward.SelectedValue == "0")
            {
                isOpen = operateShopAward.UpdateShopAwardEnable(Common.ToInt32(Request.QueryString["shopID"]), 0);
            }
            else
            {
                isOpen = true;// operateShopAward.UpdateShopAwardEnable(Common.ToInt32(Request.QueryString["shopID"]));


                // 修改免排队

                if (radAvoidQueue.SelectedValue == "0")
                {
                    // 关闭,删除免排队这条数据
                    var objShopAward = operateShopAward.SelectShopAwardList(Common.ToInt32(Request.QueryString["shopID"]), AwardType.AvoidQueue).FirstOrDefault();
                    if (objShopAward != null)
                    {
                        objShopAward.Enable = false;
                        objShopAward.Status = true;
                        isAvoidQueue = operateShopAward.UpdateShopAward(objShopAward);

                        // 添加商家奖品版本变更记录
                        if (isAvoidQueue)
                        {
                            ShopAwardVersionOperate operateVersion = new ShopAwardVersionOperate();
                            operateVersion.InsertShopAwardVersionAndLog(Common.ToInt32(((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID), Common.ToInt32(Request.QueryString["shopID"]), "关闭免排队", "老后台", objShopAward.Id);
                        }
                    }
                }
                else
                {
                    // 开通免排队
                    var listShopAward = operateShopAward.SelectShopAwardList(Common.ToInt32(Request.QueryString["shopID"]), AwardType.AvoidQueue);
                    ShopAward objShopAward = new ShopAward();
                    if (listShopAward.Count > 0)
                    {
                        objShopAward = listShopAward.FirstOrDefault();
                        objShopAward.Enable = true;
                        objShopAward.Status = true;
                        objShopAward.Count = Common.ToInt32(txtSetAvoidQueueCount.Text);
                        objShopAward.LastUpdateTime = DateTime.Now;
                        objShopAward.LastUpdatedBy = Convert.ToString(((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID);
                        isAvoidQueue = operateShopAward.UpdateShopAward(objShopAward);

                        if (isAvoidQueue)
                        {
                            ShopAwardVersionOperate operateVersion = new ShopAwardVersionOperate();
                            operateVersion.InsertShopAwardVersionAndLog(Common.ToInt32(((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID), Common.ToInt32(Request.QueryString["shopID"]), "修改免排队" + objShopAward.Count + "份", "老后台", objShopAward.Id);
                        }
                    }
                    else
                    {
                        objShopAward = new ShopAward()
                        {
                            Id = Guid.NewGuid(),
                            ShopId = Common.ToInt32(Request.QueryString["shopID"]),
                            AwardType = AwardType.AvoidQueue,
                            DishId = 0,
                            Name = "免排队",
                            Count = Common.ToInt32(txtSetAvoidQueueCount.Text),
                            SubsidyAmount = 0,
                            Probability = 0,
                            Enable = true,
                            Status = true,
                            CreateTime = DateTime.Now,
                            CreatedBy = Convert.ToString(((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID),
                            LastUpdateTime = DateTime.Now,
                            LastUpdatedBy = Convert.ToString(((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID)
                        };
                        isAvoidQueue = operateShopAward.InsertShopAward(objShopAward);

                        if (isAvoidQueue)
                        {
                            ShopAwardVersionOperate operateVersion = new ShopAwardVersionOperate();
                            operateVersion.InsertShopAwardVersionAndLog(Common.ToInt32(((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID), Common.ToInt32(Request.QueryString["shopID"]), "开启免排队" + objShopAward.Count + "份", "老后台", objShopAward.Id);
                        }
                    }
                }

                // 修改红包限额
                ShopConnRedEnvelopeOperate operateShopRed = new ShopConnRedEnvelopeOperate();
                if (radRed.SelectedValue == "0")
                {
                    var objShopRed = operateShopRed.SelectShopConnRedEnvelope(Common.ToInt32(Request.QueryString["shopID"]));
                    if (objShopRed.Id > 0)
                    {
                        // 关闭
                        isShopConnRedEnvelope = operateShopRed.UpdateShopConnRedEnvelope(Common.ToInt32(Request.QueryString["shopID"]), 0, Common.ToInt32(txtSetRedCount.Text),
                            Common.ToDouble(txtSetRedAmount.Text));

                        if (isAvoidQueue)
                        {
                            ShopAwardVersionOperate operateVersion = new ShopAwardVersionOperate();
                            operateVersion.InsertShopAwardVersionAndLog(Common.ToInt32(((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID), Common.ToInt32(Request.QueryString["shopID"]), "关闭红包", "老后台", Guid.Empty);
                        }
                    }
                }
                else
                {
                    var objShopRed = operateShopRed.SelectShopConnRedEnvelope(Common.ToInt32(Request.QueryString["shopID"]));
                    if (objShopRed.Id > 0)
                    {
                        isShopConnRedEnvelope = operateShopRed.UpdateShopConnRedEnvelope(Common.ToInt32(Request.QueryString["shopID"]), 1, Common.ToInt32(txtSetRedCount.Text),
                             Common.ToDouble(txtSetRedAmount.Text));

                        if (isShopConnRedEnvelope)
                        {
                            ShopAwardVersionOperate operateVersion = new ShopAwardVersionOperate();
                            operateVersion.InsertShopAwardVersionAndLog(Common.ToInt32(((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID), Common.ToInt32(Request.QueryString["shopID"]), "修改红包限额" + txtSetRedCount.Text + "份,金额" + txtSetRedAmount.Text + "元", "老后台", Guid.Empty);
                        }
                    }
                    else
                    {
                        objShopRed.status = true;
                        objShopRed.ShopId = Common.ToInt32(Request.QueryString["shopID"]);
                        objShopRed.RedEnvelopeConsumeCount = Common.ToInt32(txtSetRedCount.Text);
                        objShopRed.RedEnvelopeConsumeAmount = Common.ToDouble(txtSetRedAmount.Text);

                        isShopConnRedEnvelope = operateShopRed.InsertShopConnRedEnvelope(objShopRed);

                        if (isShopConnRedEnvelope)
                        {
                            ShopAwardVersionOperate operateVersion = new ShopAwardVersionOperate();
                            operateVersion.InsertShopAwardVersionAndLog(Common.ToInt32(((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID), Common.ToInt32(Request.QueryString["shopID"]), "开通红包,限额" + txtSetRedCount.Text + "份,金额" + txtSetRedAmount.Text + "元", "老后台", Guid.Empty);
                        }
                    }
                }

                // 赠菜
                if (radDish.SelectedValue == "0")
                {
                    // 关闭赠菜功能修改所有赠菜的状态为不可用
                    List<ShopAward> shopAwards = operateShopAward.SelectShopAwardList(Common.ToInt32(Request.QueryString["shopID"]), AwardType.PresentDish);

                    if (shopAwards != null && shopAwards.Any())
                    {
                        isDishInfo = operateShopAward.UpdateShopAwardEnable(Common.ToInt32(Request.QueryString["shopID"]), 0, 3);

                        if (isDishInfo)
                        {
                            ShopAwardVersionOperate operateVersion = new ShopAwardVersionOperate();
                            operateVersion.InsertShopAwardVersionAndLog(Common.ToInt32(((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID), Common.ToInt32(Request.QueryString["shopID"]), "关闭所有赠菜", "老后台", Guid.Empty);
                        }
                    }
                }
                else
                {
                    // 所有赠菜的状态置为可用
                    var shopAwards = operateShopAward.SelectShopAwardList(Common.ToInt32(Request.QueryString["shopID"]), 2);
                    shopAwards = shopAwards.FindAll(s => s.AwardType == AwardType.PresentDish);

                    if (shopAwards != null && shopAwards.Any())
                    {
                        isDishInfo = operateShopAward.UpdateShopAwardEnable(Common.ToInt32(Request.QueryString["shopID"]));
                    }
                }
            }

            if (isOpen && isAvoidQueue && isDishInfo && isShopConnRedEnvelope)
            {
                scope.Complete();
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('修改成功！');</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('修改失败！');</script>");
            }
        }
    }

    protected void BindGridViewDish(int shopID)
    {
        ShopAwardOperate operate = new ShopAwardOperate();
        var listShopAward = operate.SelectShopAwardList(shopID,2);
        listShopAward = listShopAward.FindAll(s => s.AwardType == AwardType.PresentDish);
        if (hidShowDish.Value== "1")
        {
            listShopAward = listShopAward.FindAll(s =>s.Enable==true);
        }

        foreach (var item in listShopAward)
        {
            string dishName = operate.GetDishNameI18nID(item.DishId);
            item.Name = dishName;
        }

        GridViewDish.DataSource = listShopAward;
        GridViewDish.DataBind();
    }

    protected void GridViewDish_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;//转换为按钮类型，获取其所在的行的索引
        string awardID = GridViewDish.DataKeys[index].Values["Id"].ToString();
        string shopID = GridViewDish.DataKeys[index].Values["ShopId"].ToString();
        switch (e.CommandName.ToString())
        {
            case "edit":
                Response.Redirect("EditMenuInfo.aspx?awardID=" + awardID + "&shopID=" + shopID);
                break;
            case "delete":
                ShopAwardOperate operateShopAward = new ShopAwardOperate();
                bool i = operateShopAward.DeleteShopAward(new Guid(awardID));
                if (i == true)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('删除成功！');</script>");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('删除失败！');</script>");
                }
                // 重新查询
                BindGridViewDish(Convert.ToInt32(shopID));
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 添加赠菜
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddDish_Click(object sender, EventArgs e)
    {
        Response.Redirect("EditMenuInfo.aspx?awardID=" + Guid.Empty + "&shopID=" + Request.QueryString["shopID"]);
    }
    protected void GridViewDish_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void btnCancle_Click(object sender, EventArgs e)
    {
        Response.Redirect("MerchantActivityQuery.aspx");
    }
}