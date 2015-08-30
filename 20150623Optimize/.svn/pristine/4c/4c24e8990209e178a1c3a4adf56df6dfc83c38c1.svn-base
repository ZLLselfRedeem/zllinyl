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
public partial class PreOrder19dianManage_PreOrderDetail : System.Web.UI.Page
{
    protected int shopId = 0;
    protected string eCardNumber;
    protected string verificationCode;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["preOrder19dianId"] != null)//查看的是对账信息
        {
            QueryButtonStatus();
            GetPreOrder19dianDetail(Common.ToInt32(Request.QueryString["preOrder19dianId"]));
        }
    }
    protected void GetPreOrder19dianDetail(int preOrder19dianId)
    {
        PreOrder19dianOperate preOrder19dianOperate = new PreOrder19dianOperate();
        DataTable dtpreOrder19dian = preOrder19dianOperate.QueryPreOrderById(preOrder19dianId);
        if (dtpreOrder19dian.Rows.Count > 0)
        {
            //显示菜
            string orderInJson = dtpreOrder19dian.Rows[0]["orderInJson"].ToString();
            List<OrderInfo> listOrderInfo = JsonOperate.JsonDeserialize<List<OrderInfo>>(orderInJson);
            GridView_Dish.DataSource = listOrderInfo;
            GridView_Dish.DataBind();
            //显示基本信息
            Label_preOrder19dianId.Text = dtpreOrder19dian.Rows[0]["preOrder19dianId"].ToString();
            Label_preOrderTime.Text = Common.ToDateTime(dtpreOrder19dian.Rows[0]["preOrderTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss").Replace("0001-01-01 00:00:00", "");
            Label_preOrderServerSum.Text = Math.Round(Common.ToDecimal(dtpreOrder19dian.Rows[0]["preOrderServerSum"].ToString()), 2).ToString();
            Label_prePaidSum.Text = dtpreOrder19dian.Rows[0]["prePaidSum"].ToString();
            Label_prePayTime.Text = Common.ToDateTime(dtpreOrder19dian.Rows[0]["prePayTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss").Replace("0001-01-01 00:00:00", "");
            Label_preOrderTime.Text = Common.ToDateTime(dtpreOrder19dian.Rows[0]["prePayTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss").Replace("0001-01-01 00:00:00", "");
            Label_PhoneNum.Text = Common.ToString(dtpreOrder19dian.Rows[0]["mobilePhoneNumber"]);
            Label_UserName.Text = Common.ToString(dtpreOrder19dian.Rows[0]["UserName"]);
            Label_eCardNumber.Text = dtpreOrder19dian.Rows[0]["eCardNumber"].ToString();
            Label_isPaid.Text = Common.GetEnumDescription((VAPreorderIsPaid)Common.ToInt32(dtpreOrder19dian.Rows[0]["isPaid"].ToString()));
            Label_refundDeadline.Text = Common.ToDateTime(dtpreOrder19dian.Rows[0]["refundDeadline"].ToString()).ToString("yyyy-MM-dd HH:mm:ss").Replace("0001-01-01 00:00:00", "");
            Label_invoiceTitle.Text = dtpreOrder19dian.Rows[0]["invoiceTitle"].ToString();
            if (Common.ToInt32(dtpreOrder19dian.Rows[0]["isApproved"]) != 1 && Common.ToInt32(dtpreOrder19dian.Rows[0]["isShopConfirmed"]) != 1
                && Common.ToInt32(dtpreOrder19dian.Rows[0]["isShopVerified"]) != 1 && Common.ToInt32(dtpreOrder19dian.Rows[0]["isPaid"]) == 1
                && Common.ToInt32(dtpreOrder19dian.Rows[0]["status"]) == 105)
            {
                Button_CheckPreOrderAmount.Enabled = false;
                Button_ConfirmPreOrder.Enabled = false;
            }
        }
        #region GridView显示退款信息
        List<MoneyRefundDetail> list = Money19dianDetailOperate.GetMoney19dianDetail(Common.ToInt32(Request.QueryString["preOrder19dianId"]));
        if (list.Count > 0)
        {
            GridView_Refund.DataSource = list;
        }
        GridView_Refund.DataBind();
        #endregion
        #region GridView显示对账日志信息
        DataTable dtPreOrderCheckInfo = preOrder19dianOperate.QueryPreOrderCheckInfo(preOrder19dianId);
        GridView_PreOrderCheckInfo.DataSource = dtPreOrderCheckInfo;
        GridView_PreOrderCheckInfo.DataBind();
        if (GridView_PreOrderCheckInfo.Rows.Count > 0)
        {
            Label_PreOrderCheckInfo.Visible = false;
            for (int i = 0; i < GridView_PreOrderCheckInfo.Rows.Count; i++)
            {
                Label Label_status = GridView_PreOrderCheckInfo.Rows[i].FindControl("Label_status") as Label;
                int status = Common.ToInt32(GridView_PreOrderCheckInfo.DataKeys[i].Values["status"].ToString());
                if (status == 0)
                {
                    Label_status.Text = "取消对账";
                }
                else
                {
                    Label_status.Text = "对账";
                }
            }
        }
        else
        {
            Label_PreOrderCheckInfo.Visible = true;
        }
        #endregion
        #region GridView显示审核日志信息
        DataTable dtPreorderShopConfirmedInfo = preOrder19dianOperate.QueryPreorderShopConfirmedInfo(preOrder19dianId);
        GridView_PreorderShopConfirmedInfo.DataSource = dtPreorderShopConfirmedInfo;
        GridView_PreorderShopConfirmedInfo.DataBind();
        if (GridView_PreorderShopConfirmedInfo.Rows.Count > 0)
        {
            Label_PreorderShopConfirmedInfo.Visible = false;
            for (int i = 0; i < GridView_PreorderShopConfirmedInfo.Rows.Count; i++)
            {
                Label Label_status = GridView_PreorderShopConfirmedInfo.Rows[i].FindControl("Label_PreorderShopConfirmedInfo_status") as Label;
                int status = Common.ToInt32(GridView_PreorderShopConfirmedInfo.DataKeys[i].Values["status"].ToString());
                if (status == (int)VAPreOrderShopConfirmed.NOT_SHOPCONFIRMED)
                {
                    Label_status.Text = "取消审核";
                }
                else
                {
                    Label_status.Text = "审核";
                }
            }
        }
        else
        {
            Label_PreorderShopConfirmedInfo.Visible = true;
        }
        #endregion
    }
    #region 具备特殊权限客服操作，对账，审核，验证，撤单
    protected void QueryButtonStatus()
    {
        if (Request.QueryString["status"] != null)//表示是点单查看页面进入查看该模块的
        {
            using (TransactionScope scope = new TransactionScope())
            {
                PreOrder19dianOperate preOrder19dianOperate = new PreOrder19dianOperate();
                DataTable preOrder19dianInfo = preOrder19dianOperate.QueryPreOrderInfoByPreOrder19dianId(Common.ToInt32(Request.QueryString["preOrder19dianId"]));//查询该点单的信息

                if (preOrder19dianInfo.Rows.Count == 1)//事实上是一定的
                {
                    QuerySpecialAuthority();
                    shopId = Common.ToInt32(preOrder19dianInfo.Rows[0]["shopId"]);
                    eCardNumber = preOrder19dianInfo.Rows[0]["eCardNumber"].ToString();
                    verificationCode = preOrder19dianInfo.Rows[0]["verificationCode"].ToString();
                    if (Common.ToInt32(preOrder19dianInfo.Rows[0]["isPaid"]) == (int)VAPreorderIsPaid.PAID)
                    {
                        Button_CheckPreOrderAmount.Enabled = true;
                        Button_CheckPreOrderAmount.CssClass = "button";
                        Button_ConfirmPreOrder.Enabled = true;
                        Button_ConfirmPreOrder.CssClass = "button";
                        //已验证才可以审核或者对账
                        if (Common.ToInt32(preOrder19dianInfo.Rows[0]["isShopConfirmed"]) == (int)VAPreOrderShopConfirmed.SHOPCONFIRMED)//表示当前单子已审核
                        {
                            Button_ConfirmPreOrder.Text = "取消审核";
                        }
                        else
                        {
                            Button_ConfirmPreOrder.Text = "审 核";
                        }
                        if (Common.ToInt32(preOrder19dianInfo.Rows[0]["isApproved"]) == (int)VAPreorderIsApproved.APPROVED)//表示当前单子已对账
                        {
                            Button_CheckPreOrderAmount.Text = "已对账";
                            Button_CheckPreOrderAmount.Enabled = false;
                            Button_CheckPreOrderAmount.CssClass = "buttonEnable";
                            Button_ConfirmPreOrder.Enabled = false;
                            Button_ConfirmPreOrder.CssClass = "buttonEnable";
                        }
                        else
                        {
                            Button_CheckPreOrderAmount.Text = "对 账";
                        }
                    }
                    else//没有支付的，不可以对账，不可以审核
                    {
                        if (Common.ToInt32(preOrder19dianInfo.Rows[0]["status"]) == 105)
                        {
                            Button_CheckPreOrderAmount.Enabled = false;
                            Button_ConfirmPreOrder.Enabled = false;
                        }
                        else
                        {
                            Button_CheckPreOrderAmount.Enabled = false;
                            Button_ConfirmPreOrder.Enabled = false;
                            Button_CheckPreOrderAmount.CssClass = "buttonEnable";
                            Button_ConfirmPreOrder.CssClass = "buttonEnable";
                        }
                    }
                    scope.Complete();
                }
                else//未支付的，只能验证点单
                {
                    Panel_divOperate.Visible = false;
                }
            }
        }
        else
        {
            Panel_divOperate.Visible = false;
        }
    }
    /// <summary>
    /// 查询当前登录用户的权限信息
    /// </summary>
    protected void QuerySpecialAuthority()
    {
        tr_CheckPreOrderAmount.Visible = false;
        tr_ConfirmPreOrder.Visible = false;
        Button_CheckPreOrderAmount.Visible = false;
        Button_ConfirmPreOrder.Visible = false;
        RoleOperate roleOperate = new RoleOperate();
        DataTable dt = roleOperate.QuerySpecialAuthorityInfoByEmployeeID(Common.ToInt32(Request.QueryString["employeeID"]), 0);//返回当前employeeID下所有的特殊权限信息
        if (dt.Rows.Count > 0)
        {
            Panel_divOperate.Visible = true;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                switch (Common.ToInt32(dt.Rows[i]["specialAuthorityId"]))
                {
                    case (int)VASpecialAuthority.CHECK_PREORDER_AMOUNT://对账
                        Button_CheckPreOrderAmount.Visible = true;
                        tr_CheckPreOrderAmount.Visible = true;
                        break;
                    case (int)VASpecialAuthority.CHECK_PREORDER_CONFIRM://审核
                        Button_ConfirmPreOrder.Visible = true;
                        tr_ConfirmPreOrder.Visible = true;
                        break;
                    default:
                        break;
                }
            }
        }
        else
        {
            Panel_divOperate.Visible = false;//没有任何操作权限    
        }
    }
    /// <summary>
    /// 客服对账和取消对账操作
    /// </summary>
    protected void Button_CheckPreOrderAmount_Click(object sender, EventArgs e)
    {
        int preOrder19dianId = Common.ToInt32(Request.QueryString["preOrder19dianId"]);
        VAEmployeeLoginResponse vAEmployeeLoginResponse = (VAEmployeeLoginResponse)Session["UserInfo"];
        int employeeID = vAEmployeeLoginResponse.employeeID;
        SybMoneyMerchantOperate sybMerchantOperate = new SybMoneyMerchantOperate();
        int result = Common.ToInt32(sybMerchantOperate.ApproveMoneyMerchant(preOrder19dianId, employeeID));
        if (result > 0)
        {
            GetPreOrder19dianDetail(Common.ToInt32(Request.QueryString["preOrder19dianId"]));//重新显示绑定信息
            QueryButtonStatus();
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('操作成功！');</script>");
        }
        else
        {
            switch (result)
            {
                case -2:
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('当前点单未审核，无法对账！');</script>");
                    break;
                default:
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('操作失败！');</script>");
                    break;
            }
        }
    }
    /// <summary>
    /// 客服审核和取消审核操作
    /// </summary>
    protected void Button_ConfirmPreOrder_Click(object sender, EventArgs e)
    {
        int shopConfirmenStatus = 0;
        if (Button_ConfirmPreOrder.Text == "审 核")
        {
            shopConfirmenStatus = (int)VAPreOrderShopConfirmed.SHOPCONFIRMED;//审核
        }
        else
        {
            shopConfirmenStatus = (int)VAPreOrderShopConfirmed.NOT_SHOPCONFIRMED;//取消审核  
        }
        SybMoneyMerchantOperate syb = new SybMoneyMerchantOperate();
        int result = syb.ConfrimPreOrder(Common.ToInt32(Request.QueryString["preOrder19dianId"]), shopConfirmenStatus, PreOrderConfirmOperater.Cash, ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID);
        switch (result)
        {
            case 1:
                //审核成功
                QueryButtonStatus();
                GetPreOrder19dianDetail(Common.ToInt32(Request.QueryString["preOrder19dianId"]));//重新显示绑定信息
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('操作成功！');</script>");
                break;
            case -1:
                //前端提示：当前点单已对账，无法取消审核
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('当前点单已对账，无法取消审核！');</script>");
                break;
            case -2:
                //前端提示：当前单子是未审核状态，无法取消审核
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('当前单子是未审核状态，无法取消审核！');</script>");
                break;
            case -3:
                //前端提示：当前单子是已审核状态，无法审核
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('当前单子是已审核状态，无法审核！');</script>");
                break;
            case -7:
                //前端提示：当前单子已部分退款或者全部退款，无法取消审核  
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('当前单子已部分退款或者全部退款，无法取消审核！');</script>");
                break;
        }
    }
    #endregion
}
public class OrderInfo
{
    public string dishName { get; set; }
    public double unitPrice { get; set; }
    public int quantity { get; set; }
    public string dishPriceName { get; set; }
    public int dishPriceI18nId { get; set; }
}