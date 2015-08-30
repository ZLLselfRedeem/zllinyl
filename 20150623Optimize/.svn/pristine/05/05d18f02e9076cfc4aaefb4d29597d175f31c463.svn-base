using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.WebPageDll.Services;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;

public partial class AuthorizationManagement_ShopAuthorityAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            dropDownListCode.DataSource = ShopRole.菜品沽清.ToListItem();
            dropDownListCode.DataTextField = "Text";
            dropDownListCode.DataValueField = "Value";
            dropDownListCode.DataBind();
            SetSelectedItem(dropDownListCode, ShopRole.菜品沽清.GetString());
        }
    }

    private void SetSelectedItem(System.Web.UI.WebControls.ListControl list, String value)
    {
        foreach (ListItem e in list.Items)
        {
            if (e.Value == value)
            {
                e.Selected = true;
                return;
            }
            else e.Selected = false;
        }
    }

    protected void button1_OnClick(object sender, EventArgs e)
    {
        ShopRole shopRole;
        if (ShopRole.TryParse(dropDownListCode.SelectedValue, out shopRole))
        {
            IShopAuthorityService shopAuthorityService = ServiceFactory.Resolve<IShopAuthorityService>();
            var shopAuthority = shopAuthorityService.GetShopAuthorityByCode(shopRole.GetString());
            if (shopAuthority == null)
            {
                shopAuthority = getShopAuthority(shopRole);
                shopAuthority.ShopAuthorityName = textBoxshopAuthorityName.Text;
                shopAuthority.ShopAuthorityDescription = textBoxshopAuthorityDescription.Text;

                shopAuthorityService.Add(shopAuthority);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('已经存在" + shopRole.ToString() + "')</script>");
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message",
                "<script language='javascript' defer>alert('出错啦')</script>");
        }





        //shopAuthorityService
    }

    private ShopAuthority getShopAuthority(ShopRole shopRole)
    {
        ShopAuthority shopAuthority = new ShopAuthority() { AuthorityCode = shopRole.GetString(), ShopAuthorityStatus = 1 };
        switch (shopRole)
        {
            case ShopRole.配菜沽清:
            case ShopRole.调整价格:
            case ShopRole.菜品沽清:
            case ShopRole.菜谱更新:
                shopAuthority.IsClientShow = true;
                shopAuthority.IsSYBShow = true;
                shopAuthority.IsViewAllocWorkerEnable = true;
                shopAuthority.ShopAuthorityType = ShopAuthorityType.悠先服务;
                break;
            case ShopRole.店员退款:
            case ShopRole.客户信息:
                shopAuthority.IsClientShow = false;
                shopAuthority.IsSYBShow = true;
                shopAuthority.IsViewAllocWorkerEnable = true;
                shopAuthority.ShopAuthorityType = ShopAuthorityType.悠先服务;
                break;
            case ShopRole.短信通知:
            case ShopRole.门店会员:
                shopAuthority.IsClientShow = false;
                shopAuthority.IsSYBShow = true;
                shopAuthority.IsViewAllocWorkerEnable = false;
                shopAuthority.ShopAuthorityType = ShopAuthorityType.悠先服务统计;
                break;
            case ShopRole.客户追溯:
                shopAuthority.IsClientShow = true;
                shopAuthority.IsSYBShow = false;
                shopAuthority.IsViewAllocWorkerEnable = false;
                shopAuthority.ShopAuthorityType = ShopAuthorityType.悠先服务内部;
                break;
            case ShopRole.余额调整:
                shopAuthority.IsClientShow = false;
                shopAuthority.IsSYBShow = false;
                shopAuthority.IsViewAllocWorkerEnable = false;
                shopAuthority.ShopAuthorityType = ShopAuthorityType.悠先服务内部;
                break;
            case ShopRole.用户评价信息:
                shopAuthority.IsClientShow = false;
                shopAuthority.IsSYBShow = true;
                shopAuthority.IsViewAllocWorkerEnable = true;
                shopAuthority.ShopAuthorityType = ShopAuthorityType.悠先服务;
                break;
            case ShopRole.抵扣券发布:
                shopAuthority.IsClientShow = false;
                shopAuthority.IsSYBShow = true;
                shopAuthority.IsViewAllocWorkerEnable = true;
                shopAuthority.ShopAuthorityType = ShopAuthorityType.悠先服务;
                break;
            case ShopRole.抵扣券统计:
                shopAuthority.IsClientShow = false;
                shopAuthority.IsSYBShow = true;
                shopAuthority.IsViewAllocWorkerEnable = true;
                shopAuthority.ShopAuthorityType = ShopAuthorityType.悠先服务;
                break;
            case ShopRole.抽奖活动统计:
                shopAuthority.IsClientShow = false;
                shopAuthority.IsSYBShow = true;
                shopAuthority.IsViewAllocWorkerEnable = true;
                shopAuthority.ShopAuthorityType = ShopAuthorityType.悠先服务统计;
                break;
            case ShopRole.会员营销:
                shopAuthority.IsClientShow = true;
                shopAuthority.IsSYBShow = true;
                shopAuthority.IsViewAllocWorkerEnable = true;
                shopAuthority.ShopAuthorityType = ShopAuthorityType.悠先服务;
                break;
            case ShopRole.会员营销统计:
                shopAuthority.IsClientShow = true;
                shopAuthority.IsSYBShow = true;
                shopAuthority.IsViewAllocWorkerEnable = true;
                shopAuthority.ShopAuthorityType = ShopAuthorityType.悠先服务统计;
                break;
        }

        return shopAuthority;
    }
}