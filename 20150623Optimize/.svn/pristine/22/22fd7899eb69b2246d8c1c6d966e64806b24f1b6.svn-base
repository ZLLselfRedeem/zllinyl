using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
using VAGastronomistMobileApp.Model;
using System.Transactions;
using VAGastronomistMobileApp.WebPageDll.Syb;

public partial class CompanyPages_preOrderConfirmedDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    /// <summary>
    /// 显示固定页面信息
    /// </summary>
    /// <param name="preOrder19dianId">点单流水号</param>
    /// <returns></returns>
    [WebMethod]
    public static string CommonPageInfoShow(Guid preOrder19dianId)
    {
        return SybPreOrder.PreOrderDetail(preOrder19dianId, 1);
    }
    /// <summary>
    /// 收银审核日志信息输出
    /// </summary>
    /// <param name="preOrder19dianId">点单流水号</param>
    /// <returns></returns>
    public static string ShopConfirmedInfo(int preOrder19dianId)
    {
        if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
        {
            return "-1000";
        }
        PreOrder19dianOperate preOrder19dianOperate = new PreOrder19dianOperate();
        DataTable dtPreorderShopConfirmedInfo = preOrder19dianOperate.QueryPreorderShopConfirmedInfo(preOrder19dianId);
        string tableJson = "{" + "\"preOrderConfirmedInfo\":" + Common.ConvertDateTableToJson(dtPreorderShopConfirmedInfo) + ",\"isShopConfirmed\":" + SybPreOrder.GetPreOrderConfrimStatus(preOrder19dianId) + "}";
        return tableJson;
    }
    /// <summary>
    /// 点单详情审核
    /// </summary>
    /// <param name="preOrder19dianId"></param>
    /// <param name="statusFlag"></param>
    /// <returns></returns>
    [WebMethod]
    public static string ShopConfirmedOperate(Guid preOrder19dianId, int statusFlag)
    {
        if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
        {
            return "-1000";
        }
        else
        {
            string returnResult = string.Empty;

            PreOrder19dianOperate preOrder19dianOperate = new PreOrder19dianOperate();
            if (preOrder19dianOperate.getPreOrder19dianCount(preOrder19dianId) > 1 && statusFlag != 1)
            {
                return "-9";//前端提示：当前单子已补差价，无法取消入座，请选择退款。  
            }

            long preOrder19dianId1 = 0;
            List<PreOrder19dianInfo> list = preOrder19dianOperate.GetPreOrder19dianByOrderId(preOrder19dianId);
            foreach (PreOrder19dianInfo p in list)
            {
                if (p.OrderType.Equals(OrderTypeEnum.Normal))
                {
                    preOrder19dianId1 = p.preOrder19dianId;
                }
            }

            SybMoneyMerchantOperate syb = new SybMoneyMerchantOperate();
            int result = syb.ConfrimPreOrder(preOrder19dianId1, statusFlag, PreOrderConfirmOperater.Cash);
            switch (result)
            {
                case 1:
                    returnResult = "1";//审核成功
                    break;
                case -1:
                    returnResult = "-1";//前端提示：当前点单已对账，无法取消审核
                    break;
                case -2:
                    returnResult = "-2";//前端提示：当前单子是未审核状态，无法取消审核
                    break;
                case -3:
                    returnResult = "-3";//前端提示：当前单子是已审核状态，无法审核
                    break;
                case -7:
                    returnResult = "-7";//前端提示：当前单子已部分退款或者全部退款，无法取消审核
                    break;
                case -8:
                    returnResult = "-8";//前端提示：当前点单已全部退款，无法入座
                    break;
                case 0:
                    returnResult = "0";
                    break;
            }
            return returnResult;
        }
    }
    //[WebMethod]
    //public static string ShopConfirmedOperate(int statusFlag, int preOrder19dianId)
    //{
    //    if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
    //    {
    //        return "-1000";
    //    }
    //    string result = "";
    //    using (TransactionScope scope = new TransactionScope())
    //    {
    //        int employeeID = ((VAEmployeeLoginResponse)HttpContext.Current.Session["MerchantsTreasureUserInfo"]).employeeID;
    //        EmployeeOperate employeeOperate = new EmployeeOperate();
    //        EmployeeInfo employeeInfo = employeeOperate.QueryEmployee(employeeID);
    //        //string employeeName = employeeInfo.EmployeeFirstName + employeeInfo.EmployeeLastName;//2014-2-23 取消LastName
    //        string employeeName = employeeInfo.EmployeeFirstName;
    //        string employeePosition = employeeInfo.position;

    //        PreOrder19dianOperate preOrder19dianOperate = new PreOrder19dianOperate();
    //        DataTable dtpreOrder19dian = preOrder19dianOperate.QueryPreOrderById(preOrder19dianId);
    //        int shopId = Common.ToInt32(dtpreOrder19dian.Rows[0]["shopId"]);
    //        string eCardNumber = dtpreOrder19dian.Rows[0]["eCardNumber"].ToString();
    //        string verificationCode = dtpreOrder19dian.Rows[0]["verificationCode"].ToString();
    //        double refundMoneySum = Common.ToDouble(dtpreOrder19dian.Rows[0]["refundMoneySum"]);//表示当前点单已退款金额
    //        int status = Common.ToInt32(dtpreOrder19dian.Rows[0]["status"]);//表示当前点单状态
    //        double prePaidSum = Common.ToDouble(dtpreOrder19dian.Rows[0]["prePaidSum"]);//当前点单支付的金额
    //        int shopConfirmenStatus = 0;
    //        string approveOperateDes = "";
    //        if (statusFlag == 1)
    //        {
    //            shopConfirmenStatus = (int)VAPreorderIsApproved.APPROVED;
    //            approveOperateDes = "审核";
    //        }
    //        else//取消审核
    //        {
    //            shopConfirmenStatus = (int)VAPreorderIsApproved.NOT_APPROVED;
    //            approveOperateDes = "取消审核";
    //        }
    //        EmployeePointOperate pointOperate = new EmployeePointOperate();
    //        if (statusFlag != 1)//取消审核
    //        {
    //            if (status != (int)VAPreorderStatus.Refund && status != (int)VAPreorderStatus.OriginalRefunding && refundMoneySum == 0)
    //            {
    //                int isApproved = Common.ToInt32(Common.GetFieldValue(" PreOrder19dian", "isnull(isApproved,0)", "preOrder19dianId='" + preOrder19dianId + "'"));//查看是否对账
    //                if (isApproved == 0)//未对账
    //                {
    //                    if (preOrder19dianOperate.ShopConfrimedPreOrder(preOrder19dianId, shopConfirmenStatus, employeeID, employeeName, employeePosition))
    //                    {
    //                        string employeeOperateStr = "eVIP卡号：" + eCardNumber + "，点单流水号：" + preOrder19dianId + "，验证码：" + verificationCode + "，操作类型：" + approveOperateDes;
    //                        CustomerServiceOperateLogInfo customerServiceOperateLogInfo = new CustomerServiceOperateLogInfo();
    //                        CustomerServiceOperateLogOperate customerServiceOperateLogOperate = new CustomerServiceOperateLogOperate();
    //                        customerServiceOperateLogInfo.employeeId = employeeID;
    //                        customerServiceOperateLogInfo.employeeName = employeeName;
    //                        customerServiceOperateLogInfo.employeeOperateTime = DateTime.Now;
    //                        customerServiceOperateLogInfo.employeeOperate = employeeOperateStr;
    //                        if (customerServiceOperateLogOperate.InsertCustomerServiceOperateLog(customerServiceOperateLogInfo) > 0 && pointOperate.UpdateEmployeeNotSettlementPoint(dtpreOrder19dian, employeeID))
    //                        {
    //                            result = ShopConfirmedInfo(preOrder19dianId);
    //                            scope.Complete();
    //                        }
    //                        else
    //                        {
    //                            result = "0";
    //                        }
    //                    }
    //                    else
    //                    {
    //                        result = "0";
    //                    }
    //                }
    //                else//已对账
    //                {
    //                    result = "-1";//前端提示：当前点单已对账，无法取消审核
    //                }
    //            }
    //            else
    //            {
    //                return "-7";//前端提示：当前单子已部分退款或者全部退款，无法取消审核  
    //            }
    //        }
    //        else//审核
    //        {

    //            if (preOrder19dianOperate.ShopConfrimedPreOrder(preOrder19dianId, shopConfirmenStatus, employeeID, employeeName, employeePosition))
    //            {
    //                string employeeOperateStr = "eVIP卡号：" + eCardNumber + "，点单流水号：" + preOrder19dianId + "，验证码：" + verificationCode + "，操作类型：" + approveOperateDes;
    //                CustomerServiceOperateLogInfo customerServiceOperateLogInfo = new CustomerServiceOperateLogInfo();
    //                CustomerServiceOperateLogOperate customerServiceOperateLogOperate = new CustomerServiceOperateLogOperate();
    //                customerServiceOperateLogInfo.employeeId = employeeID;
    //                customerServiceOperateLogInfo.employeeName = employeeName;
    //                customerServiceOperateLogInfo.employeeOperateTime = DateTime.Now;
    //                customerServiceOperateLogInfo.employeeOperate = employeeOperateStr;
    //                if (customerServiceOperateLogOperate.InsertCustomerServiceOperateLog(customerServiceOperateLogInfo) > 0 && pointOperate.UpdateEmployeeNotSettlementPoint(dtpreOrder19dian, employeeID))
    //                {
    //                    result = ShopConfirmedInfo(preOrder19dianId);
    //                    scope.Complete();
    //                }
    //                else
    //                {
    //                    result = "0";
    //                }
    //            }
    //            else
    //            {
    //                result = "0";
    //            }
    //        }
    //    }
    //    return result;//
    //    #region old code
    //    //if (preOrder19dianOperate.ShopConfrimedPreOrder(preOrder19dianId, shopConfirmenStatus, employeeID, employeeName, employeePosition))
    //    //{
    //    //    string employeeOperateStr = "eVIP卡号：" + eCardNumber + "，点单流水号：" + preOrder19dianId + "，验证码：" + verificationCode + "，操作类型：" + approveOperateDes;
    //    //    CustomerServiceOperateLogInfo customerServiceOperateLogInfo = new CustomerServiceOperateLogInfo();
    //    //    CustomerServiceOperateLogOperate customerServiceOperateLogOperate = new CustomerServiceOperateLogOperate();
    //    //    customerServiceOperateLogInfo.employeeId = employeeID;
    //    //    customerServiceOperateLogInfo.employeeName = employeeName;
    //    //    customerServiceOperateLogInfo.employeeOperateTime = DateTime.Now;
    //    //    customerServiceOperateLogInfo.employeeOperate = employeeOperateStr;
    //    //    result = customerServiceOperateLogOperate.InsertCustomerServiceOperateLog(customerServiceOperateLogInfo);
    //    //    if (result > 0)
    //    //    {
    //    //        return ShopConfirmedInfo(preOrder19dianId);
    //    //    }
    //    //    else
    //    //    {
    //    //        return "";
    //    //    }
    //    //}
    //    //else
    //    //{
    //    //    return "";
    //    //} 
    //    #endregion
    //}
}
