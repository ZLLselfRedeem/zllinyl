using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;

public partial class AppPages_manual_complaint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    /// <summary>
    /// 投诉页面信息
    /// </summary>
    /// <param name="preOrderId"></param>
    /// <returns></returns>
    [WebMethod]
    public static string PageInfo(long preOrderId)
    {
        ComplaintOperate oper = new ComplaintOperate();
        //if (oper.IsComplaint(preOrderId) == true)
        //{
        //    return "-1";//表示当前点单已评价
        //}
        //else
        //{
        DataTable dtOrderInfo = oper.QueryComplaintDish(preOrderId);
        if (dtOrderInfo.Rows.Count == 1)
        {
            ComplaintInfo complaintModel = new ComplaintInfo();
            complaintModel.waiterName = Common.ToString(dtOrderInfo.Rows[0]["waiterName"]);
            complaintModel.waiterPhone = Common.ToString(dtOrderInfo.Rows[0]["waiterPhone"]);
            string orderJson = Common.ToString(dtOrderInfo.Rows[0]["orderInJson"]);
            List<string> list = new List<string>();
            if (!string.IsNullOrEmpty(orderJson))
            {
                List<PreOrderIn19dian> listOrderInfo = JsonOperate.JsonDeserialize<List<PreOrderIn19dian>>(orderJson);
                foreach (var item in listOrderInfo)
                {
                    list.Add(Common.ToString(item.dishName));
                }
            }
            complaintModel.dishName = list;
            return JsonOperate.JsonSerializer<ComplaintInfo>(complaintModel);
        }
        else
        {
            //该点单未审核
            return "error";
        }
        //}
    }
    /// <summary>
    /// 提交保存投诉信息
    /// </summary>
    /// <param name="preOrderId"></param>
    /// <param name="msg"></param>
    /// <returns></returns>
    [WebMethod]
    public static string Save(long preOrderId, string msg)
    {
        if (!string.IsNullOrEmpty(msg))
        {
            ComplaintOperate oper = new ComplaintOperate();
            if (oper.IsComplaint(preOrderId) == true)
            {
                return "-3";//表示已经投诉过
            }
            DataTable dtOrder = new PreOrder19dianOperate().QueryPreOrderInfoByPreOrder19dianId(preOrderId);
            if (dtOrder == null || dtOrder.Rows.Count <= 0)
            {
                return "-5";
            }
            if (Common.ToInt32(dtOrder.Rows[0]["isPaid"]) <= 0)
            {
                return "-4";//点单未支付
            }
            if (Common.ToInt32(dtOrder.Rows[0]["isShopConfirmed"]) <= 0)
            {
                return "-4";//点单未入座
            }
            if (Common.ToDouble(dtOrder.Rows[0]["prePaidSum"]) - Common.ToDouble(dtOrder.Rows[0]["refundMoneySum"]) <= 0.001)
            {
                return "-4";//点单已退款
            }
            if (Common.ToInt32(dtOrder.Rows[0]["status"]) == (int)VAPreorderStatus.OriginalRefunding)
            {
                return "-4";//点单退款中
            }
            CustomerComplaint model = new CustomerComplaint()
            {
                complaintMsg = msg.Trim(),
                complaintTime = DateTime.Now,
                preOrder19dianId = preOrderId
            };

            if (oper.AddCustomerComplaint(model))
            {
                return "1";
            }
            else
            {
                return "-1";
            }
        }
        else
        {
            return "-2";//反馈信息不能为空
        }
    }
}