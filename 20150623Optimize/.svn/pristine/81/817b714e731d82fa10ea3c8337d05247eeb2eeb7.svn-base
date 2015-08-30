using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;
using System.Web.Services;

public partial class weiboshare : System.Web.UI.Page
{
    //查询数据库获得顶的次数
    [WebMethod]
    public static int GetDingCount(long preorderId)
    {
        PreOrder19dianOperate poo = new PreOrder19dianOperate();
        int dingcount = 0;// poo.WebUpdatePreorderSupportCount(preorderId);
        return dingcount;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["type"] != null)
        {
            string type = Request.QueryString["type"];
            if (Common.ToInt32(type) == (int)VAWeiboShareType.PREORDER_SHARE)
            {
                if (Request.QueryString["value"] != null)
                {
                    int preorderId = Common.ToInt32(Request.QueryString["value"]);
                    VAWebPreOrderQueryRequest vAWebPreOrderQueryRequest = new VAWebPreOrderQueryRequest();
                    vAWebPreOrderQueryRequest.preorderId = preorderId;
                    PreOrder19dianOperate preOrder19dianOperate = new PreOrder19dianOperate();
                    VAWebPreOrderQueryResponse webPreOrderQueryResponse = null;// preOrder19dianOperate.WebQueryPreOrder(vAWebPreOrderQueryRequest);
                    if (webPreOrderQueryResponse.result == VAResult.VA_OK)
                    {
                        logo_company.Src = webPreOrderQueryResponse.companyLogoURL;
                        logo_company.Attributes.Add("style", "vertical-align:middle;height: 100px;width: 100px;border:none");
                        string listOrder = webPreOrderQueryResponse.orderInJson;
                        yidingcount.Text = webPreOrderQueryResponse.preorderSupportCount.ToString();
                        if (webPreOrderQueryResponse.seqencingForCurrentWeek < 0.0001)
                        {
                            weektr.Attributes.Add("style", "display:none");//隐藏
                        }
                        else
                        {
                            week_label.Text = (webPreOrderQueryResponse.seqencingForCurrentWeek * 100).ToString() + "%";//星期百分比
                        }
                        if (webPreOrderQueryResponse.seqencingForCurrentMonth < 0.0001)
                        {
                            monthtr.Attributes.Add("style", "display:none");//隐藏
                        }
                        else
                        {
                            month_label.Text = (webPreOrderQueryResponse.seqencingForCurrentMonth * 100).ToString() + "%";//星期百分比
                        }
                        List<PreOrderIn19dian> listOrderInfo = JsonOperate.JsonDeserialize<List<PreOrderIn19dian>>(listOrder);
                        if (listOrderInfo != null)
                        {
                            Label_message.Visible = false;
                            List<PreOrderIn19dian> listOrderInfoNew = new List<PreOrderIn19dian>();//将菜名和规格放到一起的新的list
                            double orderSum = 0;//总价
                            int quantitySum = 0;
                            for (int i = 0; i < listOrderInfo.Count; i++)
                            {
                                PreOrderIn19dian orderInfo = new PreOrderIn19dian();
                                orderInfo.dishPriceI18nId = listOrderInfo[i].dishPriceI18nId;
                                orderInfo.quantity = listOrderInfo[i].quantity;
                                orderInfo.unitPrice = listOrderInfo[i].unitPrice;
                                orderInfo.dishId = listOrderInfo[i].dishId;
                                string newDishName = string.Empty;
                                if (listOrderInfo[i].dishPriceName.Trim() != "")
                                {
                                    newDishName = listOrderInfo[i].dishName + "(" + listOrderInfo[i].dishPriceName + ")";
                                }
                                else
                                {
                                    newDishName = listOrderInfo[i].dishName;
                                }
                                orderInfo.dishName = newDishName;
                                listOrderInfoNew.Add(orderInfo);

                                orderSum += listOrderInfo[i].unitPrice * listOrderInfo[i].quantity;//计算总价
                                quantitySum += listOrderInfo[i].quantity;//计算总分量
                            }
                            Repeater_Order.DataSource = listOrderInfoNew;
                            Repeater_Order.DataBind();
                            Label_quantitySum.Text = "份数：" + quantitySum.ToString();
                            Label_orderSum.Text = "总金额：" + Math.Round(orderSum, 2).ToString();
                            Label_orderTime.Text = "下单时间：" + webPreOrderQueryResponse.preOrderTime.ToString();
                        }
                        else
                        {
                            Label_message.Visible = false;
                            if (webPreOrderQueryResponse.prePayAmount > 0)
                            {
                                Label_orderSum.Text = "总金额：" + Math.Round(webPreOrderQueryResponse.prePayAmount, 2).ToString();
                            } 
                            else
                            {
                                Label_orderSum.Text = "总金额：" + Math.Round(webPreOrderQueryResponse.preorderAmount, 2).ToString();
                            }
                            Label_orderTime.Text = "下单时间：" + webPreOrderQueryResponse.preOrderTime.ToString();
                        }
                    }
                    else
                    {
                        Label_message.Visible = true;
                    }
                }
            }
        }
    }
}