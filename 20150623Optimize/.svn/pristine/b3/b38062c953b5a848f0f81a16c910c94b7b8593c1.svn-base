using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;

public partial class Award_EditMenuInfo : System.Web.UI.Page
{
    Guid awardID = new Guid();
    int shopID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["awardID"] != null)
            {
                awardID = new Guid(Request.QueryString["awardID"]);
                shopID = Common.ToInt32(Request.QueryString["shopID"]);
                hidShopID.Value = shopID.ToString();
                GetDishInfo();
            }
        }
    }

    /// <summary>
    /// 获取赠菜信息
    /// </summary>
    /// <param name="awardID"></param>
    private void GetDishInfo()
    {
        ShopAwardOperate operateShopAward = new ShopAwardOperate();
        ShopAward objShopAward = new ShopAward();
        if (awardID != Guid.Empty)
        {
            objShopAward = operateShopAward.QueryShopAward(awardID);
            txtDishName.Value = operateShopAward.GetDishNameI18nID(objShopAward.DishId);
            txtDishCount.Text = Convert.ToString(objShopAward.Count);
            hidDishID.Value = objShopAward.DishId.ToString();
            hidDishPriceId.Value = objShopAward.DishPriceId.ToString();
            hidOldDishID.Value = objShopAward.DishId.ToString();
            hidOldDishPriceId.Value = objShopAward.DishPriceId.ToString();
        }
    }

    /// <summary>
    /// 查询赠菜在当前店铺下是否存在
    /// </summary>
    /// <param name="shopID"></param>
    /// <param name="dishID"></param>
    /// <param name="dishPriceID"></param>
    protected bool CheckHasDish(int shopID,int dishID,int dishPriceID)
    {
        ShopAwardOperate operateShopAward = new ShopAwardOperate();
        var listShopAward=operateShopAward.SelectShopAwardList(shopID);
        listShopAward = listShopAward.FindAll(s => s.DishId == dishID && s.DishPriceId == dishPriceID);
        if(listShopAward.Count>0)
        {
            return true;
        }
        return false;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        ShopAwardOperate operateShopAward = new ShopAwardOperate();
        ShopAward objShopAward = new ShopAward();
        awardID = new Guid(Request.QueryString["awardID"]);
        shopID = Common.ToInt32(Request.QueryString["shopID"]);
        if (awardID == Guid.Empty)
        {
            // 添加
            objShopAward.Id = Guid.NewGuid();
            try
            {
                objShopAward.Count = Common.ToInt32(txtDishCount.Text);
            }
            catch
            {
                objShopAward.Count = 0;
            }
            objShopAward.Name = "赠菜";
            objShopAward.Enable = true;
            objShopAward.Status = true;
            objShopAward.AwardType = AwardType.PresentDish;
            objShopAward.ShopId = shopID;
            objShopAward.DishId = Common.ToInt32(hidDishID.Value); // 这里获取的DishID实际上为DishI18nID
            objShopAward.DishPriceId = Common.ToInt32(hidDishPriceId.Value);
            objShopAward.CreateTime = DateTime.Now;
            objShopAward.CreatedBy = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID.ToString();
            objShopAward.LastUpdateTime = DateTime.Now;
            // 把DishI18nID转换成DishId
            //objShopAward.DishId = operateShopAward.GetDishID(objShopAward.DishId);

            if(CheckHasDish(shopID,objShopAward.DishId,objShopAward.DishPriceId))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('不能添加重复的菜品！');</script>");
                return;
            }

            bool isSuccess = operateShopAward.InsertShopAward(objShopAward);

            // 添加商家奖品版本变更记录
            if (isSuccess)
            {
                var dishName = operateShopAward.GetDishNameI18nID(objShopAward.DishId);
                ShopAwardVersionOperate operateVersion = new ShopAwardVersionOperate();
                operateVersion.InsertShopAwardVersionAndLog(Common.ToInt32(objShopAward.CreatedBy), shopID, "新赠赠菜【" + dishName + "】X" + objShopAward.Count, "老后台", objShopAward.Id);
            }
        }
        else
        {
            objShopAward = operateShopAward.QueryShopAward(awardID);
            try
            {
                objShopAward.Count = Common.ToInt32(txtDishCount.Text);
            }
            catch
            {
                objShopAward.Count = 0;
            }
            objShopAward.DishId = Common.ToInt32(hidDishID.Value);
            objShopAward.DishPriceId = Common.ToInt32(hidDishPriceId.Value);
            objShopAward.LastUpdateTime = DateTime.Now;
            objShopAward.LastUpdatedBy = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID.ToString();

            bool isSuccess = false;

            if (hidOldDishID.Value != hidDishID.Value && hidOldDishPriceId.Value != hidOldDishPriceId.Value)
            {
                if (CheckHasDish(shopID, objShopAward.DishId, objShopAward.DishPriceId))
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('不能添加重复的菜品！');</script>");
                    return;
                }
                // 更换菜品后， 先删除之前的再添加新的赠菜
                isSuccess=operateShopAward.DeleteShopAward(awardID);
                // 添加
                objShopAward.Id = Guid.NewGuid();
                objShopAward.Name = "赠菜";
                objShopAward.Enable = true;
                objShopAward.Status = true;
                objShopAward.AwardType = AwardType.PresentDish;
                objShopAward.ShopId = shopID;
                objShopAward.CreateTime = DateTime.Now;
                objShopAward.CreatedBy = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID.ToString();
                isSuccess = operateShopAward.InsertShopAward(objShopAward);
            }
            else
            {
                isSuccess = operateShopAward.UpdateShopAwardOfDish(objShopAward);
            }

            // 添加商家奖品版本变更记录
            if (isSuccess)
            {
                var dishName = operateShopAward.GetDishNameI18nID(objShopAward.DishId);
                ShopAwardVersionOperate operateVersion = new ShopAwardVersionOperate();
                operateVersion.InsertShopAwardVersionAndLog(Common.ToInt32(objShopAward.CreatedBy), shopID, "修改赠菜【" + dishName + "】X" + objShopAward.Count, "老后台", objShopAward.Id);
            }
        }
        Response.Redirect("EditMerchantActivity.aspx?shopID=" + objShopAward.ShopId);
    }
}