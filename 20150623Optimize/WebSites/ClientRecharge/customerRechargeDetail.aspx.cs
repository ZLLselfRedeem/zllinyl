using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using VAGastronomistMobileApp.WebPageDll;

public partial class ClientRecharge_customerRechargeDetail : System.Web.UI.Page
{
    ClientRechargeOperate recharge = new ClientRechargeOperate();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string date = "";
            int rechargeId = 0;
            if (Request.QueryString["d"] != null && Request.QueryString["d"].ToString().Length > 0)
            {
                date = Request.QueryString["d"].ToString();
            }
            if (Request.QueryString["r"] != null && Request.QueryString["r"].ToString().Length > 0)
            {
                rechargeId = Common.ToInt32(Request.QueryString["r"]);
            }
            if (date != null && rechargeId > 0)
            {
                BindDetail(date, rechargeId);
            }
            else
            {
                Initail();
            }
        }
    }
    private void BindDetail(string date, int rechargeId)
    {
        this.txbBeginTime.Text = date;
        this.txbEndTime.Text = date;
        DropDownListBind();
        ddlRecharge.SelectedValue = rechargeId.ToString();
        BindList(0, 10);
    }
    private void Initail()
    {
        this.txbBeginTime.Text = DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd");
        this.txbEndTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
        DropDownListBind();
        BindList(0, 10);
    }
    private void BindList(int str, int end)
    {
        DateTime beginTime = Common.ToDateTime(Common.ToDateTime(txbBeginTime.Text.Trim()).ToString("yyyy-MM-dd") + " 00:00:00");//Common.ToDateTime(txbBeginTime.Text.Trim() + " 00:00:00");
        DateTime endTime = Common.ToDateTime(Common.ToDateTime(txbEndTime.Text.Trim()).ToString("yyyy-MM-dd") + " 23:59:59");// Common.ToDateTime(txbEndTime.Text.Trim() + " 23:59:59");
        int rechargeId = Common.ToInt32(ddlRecharge.SelectedValue);

        DataTable dt = recharge.QueryCustomerRechargeStatistics(beginTime, endTime, rechargeId);
        if (dt.Rows.Count > 0)
        {
            int cnt = dt.Rows.Count;
            this.AspNetPager1.RecordCount = cnt;
            DataTable dtPage = Common.GetPageDataTable(dt, str, end);
            this.gdvRechageList.DataSource = dtPage;
            this.gdvRechageList.DataBind();

            this.lbCustomerCount.Text = dt.Compute("count(mobilePhoneNumber)", "1=1").ToString();
            this.lbRechargeCount.Text = dt.Compute("sum(rechargeCount)", "1=1").ToString();
            this.lbRechargeAmount.Text = dt.Compute("sum(rechargeAmount)", "1=1").ToString();
        }
        else
        {
            this.gdvRechageList.DataSource = null;
            this.gdvRechageList.DataBind();
            this.lbCustomerCount.Text = "0";
            this.lbRechargeCount.Text = "0";
            this.lbRechargeAmount.Text = "0";
        }
    }
    protected void ddlRecharge_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindList(0, 10);
    }
    protected void txbBeginTime_TextChanged(object sender, EventArgs e)
    {
        BindList(0, 10);
    }
    protected void txbEndTime_TextChanged(object sender, EventArgs e)
    {
        BindList(0, 10);
    }
    private void DropDownListBind()
    {
        DataTable dt = recharge.QueryRecharge("");
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                ddlRecharge.Items.Add(new ListItem(dr["name"].ToString(), dr["id"].ToString()));
            }
        }
    }
    /// <summary>
    /// 分页操作
    /// </summary>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindList(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("customerRechargeStatistic.aspx");
    }
    protected void lnkbtnEdit_OnCommand(object sender, CommandEventArgs e)
    {
        int customerId = Common.ToInt32(e.CommandArgument);
        ViewState["customerId"] = customerId;
        switch (e.CommandName)
        {
            case "detail":
                this.divList.Attributes.Add("style", "display:none");
                this.divDetail.Attributes.Add("style", "display:block");
                BindDetail(customerId, 0, 10);
                break;
            default:
                break;
        }
    }
    #region 详情
    private void BindDetail(int customerId, int str, int end)
    {
        DataTable dt = null;
        if (Request.QueryString["d"] != null && Request.QueryString["d"].ToString().Length > 0 && Request.QueryString["r"] != null && Request.QueryString["r"].ToString().Length > 0)
        {
            DateTime beginTime = Common.ToDateTime(Common.ToDateTime(txbBeginTime.Text.Trim()).ToString("yyyy-MM-dd") + " 00:00:00");
            DateTime endTime = Common.ToDateTime(Common.ToDateTime(txbEndTime.Text.Trim()).ToString("yyyy-MM-dd") + " 23:59:59");
            int rechargeId = Common.ToInt32(ddlRecharge.SelectedValue);
            dt = recharge.QueryCustomerConsumeAndRechargeDetail(customerId, rechargeId, beginTime, endTime);
        }
        else
        {
            dt = recharge.QueryCustomerConsumeAndRechargeDetail(customerId);
        }
        if (dt.Rows.Count > 0)
        {
            int cnt = dt.Rows.Count;
            this.AspNetPager2.RecordCount = cnt;
            DataTable dtPage = Common.GetPageDataTable(dt, str, end);
            this.gdvRechageDetail.DataSource = dtPage;
        }
        else
        {
            this.gdvRechageDetail.DataSource = null;
        }
        this.gdvRechageDetail.DataBind();
        DataTable dtOrder = null;
        PreOrder19dianOperate operate = new PreOrder19dianOperate();
        for (int i = 0; i < gdvRechageDetail.Rows.Count; i++)
        {
            int orderId = Common.ToInt32(gdvRechageDetail.DataKeys[i].Values["preOrder19dianId"].ToString());
            if (orderId > 0)
            {
                dtOrder = operate.QueryPreOrderInfoByPreOrder19dianId(orderId);
                if (dtOrder.Rows.Count == 1)
                {
                    if (Common.ToString(dtOrder.Rows[0]["orderInJson"]) == "" && (Common.ToString(dtOrder.Rows[0]["sundryJson"]) == "" || Common.ToString(dtOrder.Rows[0]["sundryJson"]) == "[]"))
                    {
                        LinkButton lnkbtnDishDetail = gdvRechageDetail.Rows[i].FindControl("lnkbtnDishDetail") as LinkButton;
                        lnkbtnDishDetail.Text = "直接支付订单，无详情";
                        lnkbtnDishDetail.CommandArgument = "";
                        lnkbtnDishDetail.Enabled = false;
                    }
                }
            }
            else
            {
                LinkButton lnkbtnDishDetail = gdvRechageDetail.Rows[i].FindControl("lnkbtnDishDetail") as LinkButton;
                lnkbtnDishDetail.Text = "充值订单，无详情";
                lnkbtnDishDetail.CommandArgument = "";
                lnkbtnDishDetail.Enabled = false;
            }
        }
    }
    /// <summary>
    /// 分页操作
    /// </summary>
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        if (ViewState["customerId"] != null)
        {
            BindDetail(Common.ToInt32(ViewState["customerId"]), AspNetPager2.StartRecordIndex - 1, AspNetPager2.EndRecordIndex);
        }
    }
    #endregion

    private DataTable BindDishList(long orderId)
    {
        Gr_DishDetail.DataSource = null;
        Gr_DishDetail.DataBind();
        PreOrder19dianOperate operate = new PreOrder19dianOperate();
        DataTable dtOrder = operate.QueryPreorder(orderId);
        if (dtOrder.Rows.Count > 0)
        {
            double discount = Common.ToDouble(dtOrder.Rows[0]["discount"]);//当前点单支付时折扣
            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add(new DataColumn("dishDesc", typeof(String)));//菜品名称
            dtTemp.Columns.Add(new DataColumn("dishFraction", typeof(Int32)));//份数
            dtTemp.Columns.Add(new DataColumn("dishPrice", typeof(Double)));//价格
            List<PreOrderIn19dian> listOrderInfo = JsonOperate.JsonDeserialize<List<PreOrderIn19dian>>(Common.ToString(dtOrder.Rows[0]["orderInJson"]));
            if (listOrderInfo != null && listOrderInfo.Count > 0)
            {
                DataRow newDr;
                for (int i = 0; i < listOrderInfo.Count; i++)
                {
                    newDr = dtTemp.NewRow();//创建一行
                    newDr[0] = listOrderInfo[i].dishName;//菜名称
                    //如果有 规格 或 口味 或 掌中宝编号，则家在菜名后面
                    if (!string.IsNullOrEmpty(listOrderInfo[i].dishPriceName) || (listOrderInfo[i].dishTaste != null && !string.IsNullOrEmpty(listOrderInfo[i].dishTaste.tasteName)) || !string.IsNullOrEmpty(listOrderInfo[i].markName))
                    {
                        newDr[0] += "(";
                        if (!string.IsNullOrEmpty(listOrderInfo[i].dishPriceName)) //规格
                        {
                            newDr[0] += listOrderInfo[i].dishPriceName;
                        }
                        if (listOrderInfo[i].dishTaste != null && !string.IsNullOrEmpty(listOrderInfo[i].dishTaste.tasteName)) //口味
                        {
                            newDr[0] += " " + listOrderInfo[i].dishTaste.tasteName;
                        }
                        if (!string.IsNullOrEmpty(listOrderInfo[i].markName)) //掌中宝编号
                        {
                            newDr[0] += " " + listOrderInfo[i].markName;
                        }
                        newDr[0] += ")";
                    }
                    newDr[1] = listOrderInfo[i].quantity.ToString().Replace("0", "");
                    if (listOrderInfo[i].quantity != 0)
                    {
                        if (listOrderInfo[i].vipDiscountable)
                        {
                            newDr[2] = listOrderInfo[i].unitPrice * listOrderInfo[i].quantity * discount;
                        }
                        else
                        {
                            newDr[2] = listOrderInfo[i].unitPrice * listOrderInfo[i].quantity;
                        }
                    }
                    else
                    {
                        if (listOrderInfo[i].vipDiscountable)
                        {
                            newDr[2] = listOrderInfo[i].unitPrice * discount;
                        }
                        else
                        {
                            newDr[2] = listOrderInfo[i].unitPrice;
                        }
                    }
                    dtTemp.Rows.Add(newDr);
                    //如果有配菜则新增行显示
                    if (listOrderInfo[i].dishIngredients != null && listOrderInfo[i].dishIngredients.Count > 0)
                    {
                        List<VADishIngredients> ingredients = listOrderInfo[i].dishIngredients;
                        for (int j = 0; j < ingredients.Count; j++)
                        {
                            newDr = dtTemp.NewRow();//创建一行
                            newDr[0] = "    --" + ingredients[j].ingredientsName;//配菜名称
                            int trueQty = listOrderInfo[i].quantity * ingredients[j].quantity;
                            newDr[1] = trueQty;//数量
                            if (ingredients[j].quantity != 0)
                            {
                                if (ingredients[j].vipDiscountable == "true")
                                {
                                    newDr[2] = ingredients[j].ingredientsPrice * trueQty * discount;//小计
                                }
                                else
                                {
                                    newDr[2] = ingredients[j].ingredientsPrice * trueQty;//小计
                                }
                            }
                            else
                            {
                                if (ingredients[j].vipDiscountable == "true")
                                {
                                    newDr[2] = ingredients[j].ingredientsPrice * discount;//小计 
                                }
                                else
                                {
                                    newDr[2] = ingredients[j].ingredientsPrice;//小计 
                                }
                            }
                            dtTemp.Rows.Add(newDr);
                        }
                    }
                }
                //List<SundryInfoResponse> sundryList = JsonOperate.JsonDeserialize<List<SundryInfoResponse>>(Common.ToString(dtOrder.Rows[0]["sundryJson"]));
                ////如果有杂项，继续添加
                //if (sundryList != null && sundryList.Count > 0)
                //{
                //    for (int i = 0; i < sundryList.Count; i++)
                //    {
                //        newDr = dtTemp.NewRow();//创建一行
                //        newDr[0] = sundryList[i].sundryName;
                //        newDr[1] = sundryList[i].quantity.ToString().Replace("0", "");
                //        newDr[2] = sundryList[i].sundryPrice;
                //        dtTemp.Rows.Add(newDr);
                //    }
                //}
                //重新操作orderjson，重新计算价格
                PreOrder19dianOperate preOrder19dianOperate = new PreOrder19dianOperate();
                double originalPrice = preOrder19dianOperate.CalcPreorederOriginalPrice(Common.ToString(dtOrder.Rows[0]["orderInJson"]));//计算菜品的原价
                List<VASundryInfo> sundryList = JsonOperate.JsonDeserialize<List<VASundryInfo>>(Common.ToString(dtOrder.Rows[0]["sundryJson"]));
                if (sundryList != null && sundryList.Count > 0)
                {
                    double sundryCount = 0;
                    for (int i = 0; i < sundryList.Count; i++)
                    {
                        switch (Common.ToInt32(sundryList[i].sundryChargeMode))
                        {
                            case 1://固定金额
                                sundryCount += Common.ToDouble(sundryList[i].price);
                                break;
                            case 3://按人次
                                sundryCount += Common.ToDouble(sundryList[i].price) * sundryList[i].quantity;
                                break;
                            default://按比例
                                break;
                        }
                    }
                    for (int i = 0; i < sundryList.Count; i++)
                    {
                        newDr = dtTemp.NewRow();//创建一行
                        newDr[0] = sundryList[i].sundryName;
                        switch (sundryList[i].sundryChargeMode)
                        {
                            case 1:
                                {//固定金额
                                    newDr[1] = 1;
                                    newDr[2] = Common.ToDouble(sundryList[i].price);
                                }
                                break;
                            case 2:
                                {//按比例
                                    newDr[1] = 1;
                                    newDr[2] = Common.ToDouble(Math.Round(sundryList[i].price * (sundryCount + originalPrice), 2));
                                }
                                break;
                            case 3:
                                {//按人次
                                    newDr[1] = sundryList[i].quantity;
                                    newDr[2] = Common.ToDouble(sundryList[i].price * sundryList[i].quantity);
                                }
                                break;
                        }
                        dtTemp.Rows.Add(newDr);
                    }
                }
            }
            return dtTemp;
        }
        else
        {
            //当前点单未找到
            return null;
        }
    }
    protected void lnkbtnDishDetail_OnCommand(object sender, CommandEventArgs e)
    {
        int preOrder19dianId = Common.ToInt32(e.CommandArgument);
        if (preOrder19dianId > 0)
        {
            switch (e.CommandName)
            {
                case "detail":
                    DataTable dt = BindDishList(preOrder19dianId);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        Gr_DishDetail.DataSource = dt;
                        Gr_DishDetail.DataBind();
                        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>ConfirmWindow('Panel_QueryDishOrder');</script>");
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script>alert('直接支付点单，无详情内容');</script>");
                    }
                    break;
                default:
                    break;
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script>alert('充值');</script>");
        }
    }
    protected void Button_cancel_Click(object sender, EventArgs e)
    {
        Panel_QueryDishOrder.Visible = false;
    }
    protected void btnBackDetail_Click(object sender, EventArgs e)
    {
        this.divDetail.Attributes.Add("style", "display:none");
        this.divList.Attributes.Add("style", "display:''");
        this.gdvRechageDetail.DataSource = null;
        this.gdvRechageDetail.DataBind();
    }
}