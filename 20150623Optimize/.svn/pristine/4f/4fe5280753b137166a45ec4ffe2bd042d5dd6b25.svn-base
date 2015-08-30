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

public partial class CompanyPages_preOrderVerifiedDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    /// <summary>
    ///  显示固定页面信息
    /// </summary>
    /// <param name="preOrder19dianId">点单流水号</param>
    /// <returns></returns>
    [WebMethod]
    public static string CommonPageInfoShow(Guid preOrder19dianId)
    {
        return SybPreOrder.PreOrderDetail(preOrder19dianId, 2);
    }
    /// <summary>
    ///  财务对账日志信息输出
    /// </summary>
    /// <param name="preOrder19dianId">点单流水号</param>
    /// <returns></returns>
    public static string ShopVerifiedInfo(long preOrder19dianId)
    {
        if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
        {
            return "-1000";
        }
        PreOrder19dianOperate preOrder19dianOperate = new PreOrder19dianOperate();
        DataTable dtPreOrderCheckInfo = preOrder19dianOperate.QueryPreOrderCheckInfo(preOrder19dianId);
        string tableJson = "{" + "\"preOrderVerifiedInfo\":" + Common.ConvertDateTableToJson(dtPreOrderCheckInfo) + ",\"isApproved\":" + SybPreOrder.GetPreOrderApproveStatus(preOrder19dianId) + "}";
        return tableJson;
    }
    /// <summary>
    /// 财务对账和取消对账
    /// </summary>
    /// <param name="statusFlag">statusFlag：对账和取消对账，调用传递参数：对账→1；取消对账→2</param>
    /// <param name="preOrder19dianId">点单流水号</param>
    /// <returns></returns>
    [WebMethod]
    public static string ShopVerifiedOperate(int statusFlag, long preOrder19dianId)
    {
        if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
        {
            return "-1000";
        }
        #region old code
        //using (TransactionScope tScope = new TransactionScope())
        //{
        //    int employeeID = ((VAEmployeeLoginResponse)HttpContext.Current.Session["MerchantsTreasureUserInfo"]).employeeID;
        //    EmployeeOperate employeeOperate = new EmployeeOperate();
        //    EmployeeInfo employeeInfo = employeeOperate.QueryEmployee(employeeID);
        //    string employeeName = employeeInfo.EmployeeFirstName + employeeInfo.EmployeeLastName;
        //    string employeePosition = employeeInfo.position;

        //    PreOrder19dianOperate preOrder19dianOperate = new PreOrder19dianOperate();
        //    DataTable dtpreOrder19dian = preOrder19dianOperate.QueryPreOrderById(preOrder19dianId);
        //    string eCardNumber = dtpreOrder19dian.Rows[0]["eCardNumber"].ToString();
        //    string verificationCode = dtpreOrder19dian.Rows[0]["verificationCode"].ToString();
        //    int approveTag = 0;
        //    int result = 0;
        //    string approveOperateDes = "";
        //    if (statusFlag == 1)
        //    {
        //        approveTag = (int)VAPreorderIsApproved.APPROVED;
        //        approveOperateDes = "对账";
        //    }
        //    else//取消对账
        //    {
        //        approveTag = (int)VAPreorderIsApproved.NOT_APPROVED;
        //        approveOperateDes = "取消对账";
        //    }
        //    if (preOrder19dianOperate.ApprovePreOrder(preOrder19dianId, approveTag, employeeID, employeeName, employeePosition))
        //    {
        //        string employeeOperateStr = "eVIP卡号：" + eCardNumber + "，点单流水号：" + preOrder19dianId
        //           + "，验证码：" + verificationCode + "，操作类型：" + approveOperateDes;
        //        CustomerServiceOperateLogInfo customerServiceOperateLogInfo = new CustomerServiceOperateLogInfo();
        //        CustomerServiceOperateLogOperate customerServiceOperateLogOperate = new CustomerServiceOperateLogOperate();
        //        customerServiceOperateLogInfo.employeeId = employeeID;
        //        customerServiceOperateLogInfo.employeeName = employeeName;
        //        customerServiceOperateLogInfo.employeeOperateTime = DateTime.Now;
        //        customerServiceOperateLogInfo.employeeOperate = employeeOperateStr;
        //        result = customerServiceOperateLogOperate.InsertCustomerServiceOperateLog(customerServiceOperateLogInfo);
        //        double prePaidSum = Common.ToDouble(dtpreOrder19dian.Rows[0]["prePaidSum"]);//该点单支付金额
        //        double refundMoneySum = Common.ToDouble(dtpreOrder19dian.Rows[0]["refundMoneySum"]);//该点单支付金额
        //        EmployeePointOperate employeeOper = new EmployeePointOperate();
        //        if (result > 0 && employeeOper.UpdateEmployeeSettlementPoint(preOrder19dianId, prePaidSum - refundMoneySum))
        //        {
        //            string returnResult = ShopVerifiedInfo(preOrder19dianId);
        //            tScope.Complete();
        //            return returnResult;
        //        }
        //        else
        //        {
        //            return "";
        //        }
        //    }
        //    else
        //    {
        //        return "";
        //    }
        //} 
        #endregion
        SybMoneyMerchantOperate operate = new SybMoneyMerchantOperate();
        int employeeID = ((VAEmployeeLoginResponse)HttpContext.Current.Session["MerchantsTreasureUserInfo"]).employeeID;
        if (operate.ApproveMoneyMerchant(preOrder19dianId, employeeID) == "1")
        {
            return ShopVerifiedInfo(preOrder19dianId);
        }
        else
        {
            return "";
        }
    }
}