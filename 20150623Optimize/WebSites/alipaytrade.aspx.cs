using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
//using Alipay.Class;
using VAGastronomistMobileApp.TheThirdPartyPaymentDll;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;

public partial class alipaytrade : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CreateAlipayTread();
        }
    }

    private void CreateAlipayTread()
    {
        int type = Common.ToInt32(Request.QueryString["type"]);
        long value = Common.ToInt64(Request.QueryString["value"]);
        long customerId = Common.ToInt64(Request.QueryString["cookie"]);
        double totalFee = Common.ToDouble(Request.QueryString["totalFee"]);
        long outTradeNo = Common.ToInt64(Request.QueryString["outTradeNo"]);
        //string msg = Request.Form["msg"];
        DateTime requestTime = System.DateTime.Now;
        if (Common.debugFlag == "true")
        {//写日志文件开启
            string filePath = Server.MapPath("~/Logs/paymentLog.txt");
            using (StreamWriter file = new StreamWriter(@filePath, true))
            {
                file.WriteLine(requestTime.ToString("yyyy/MM/dd HH:mm:ss:fff") + ":" + " type:"
                    + type + " value:" + value + " customerId:" + customerId + " totalFee:" + totalFee);
            }
        }
        //VAAlipayWebRequest alipayWebRequest = JsonOperate.JsonDeserialize<VAAlipayWebRequest>(msg);
        string url = WebConfig.ServerDomain + Config.Merchant_url;
        if (type > 0 && value > 0 && customerId > 0 && totalFee > 0)
        {
            try
            {
                switch (type)
                {
                    case (int)VAPayOrderType.PAY_PREORDER:
                        {
                            PreOrder19dianOperate preorder19dianOpe = new PreOrder19dianOperate();
                            DataTable dtPreorder = preorder19dianOpe.QueryPreorder(value);
                            DataView dvPreorder = dtPreorder.DefaultView;
                            dvPreorder.RowFilter = "customerId =" + customerId ;
                            if (dvPreorder.Count == 1)
                            {
                                string subject = Common.ToString(dvPreorder[0]["companyName"]);
                                url = CreatAlipayOrder(outTradeNo, type, totalFee, requestTime, url, value, customerId, subject);
                            }
                        }
                        break;
                    case (int)VAPayOrderType.PAY_CHARGE:
                        {
                            CustomerOperate customerOpe = new CustomerOperate();
                            DataTable dtChargeOrder = customerOpe.QueryCustomerChargeOrder(value);
                            DataView dvChargeOrder = dtChargeOrder.DefaultView;
                            dvChargeOrder.RowFilter = "customerId =" + customerId;
                            if (dvChargeOrder.Count == 1)
                            {
                                string subject = Common.ToString(dvChargeOrder[0]["subjectName"]);
                                url = CreatAlipayOrder(outTradeNo, type, totalFee, requestTime, url, value, customerId, subject);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (System.Exception)
            {

            }
        }
        if (Common.debugFlag == "true")
        {//写日志文件开启
            string filePath = Server.MapPath("~/Logs/paymentLog.txt");
            using (StreamWriter file = new StreamWriter(@filePath, true))
            {
                file.WriteLine(url);
            }
        }
        //跳转收银台支付页面
        Response.Redirect(url);
    }

    private string CreatAlipayOrder(long outTradeNo, int type, double totalFee, DateTime requestTime, string url, long value, long customerId, string subject = "")
    {
        
        string callBackUrl = WebConfig.ServerDomain + Config.Call_back_url;
        subject = Common.GetEnumDescription((VAPayOrderType)type) + " " + subject;
        if (subject.Length > 128)
        {
            subject = subject.Substring(0, 128);
        }
        if (outTradeNo == 0)
        {
            AlipayOperate alipayOpe = new AlipayOperate();
            AlipayOrderInfo alipayOrder = new AlipayOrderInfo();
            alipayOrder.orderCreatTime = requestTime;
            alipayOrder.orderStatus = VAAlipayOrderStatus.NOT_PAID;
            alipayOrder.totalFee = totalFee;
            alipayOrder.subject = subject;
            alipayOrder.conn19dianOrderType = (VAPayOrderType)type;
            alipayOrder.connId = value;
            alipayOrder.customerId = customerId;
            outTradeNo = alipayOpe.AddAlipayOrder(alipayOrder);
        }
        if (outTradeNo > 0)
        {
            url = AlipayTradeInterface(type.ToString() + outTradeNo.ToString(),
                totalFee.ToString(), url, callBackUrl, subject, outTradeNo.ToString());
        }
        return url;
    }

    private string AlipayTradeInterface(string req_id, string totalFee, string url, string callBackUrl, string subject, string outTradeNo)
    {
        //初始化Service
        Service ali = new Service();
        //创建交易接口,Out_user留空，Merchant_url设为错误返回地址
        string token = ali.alipay_wap_trade_create_direct(Config.Req_url, subject,
            outTradeNo, totalFee, Config.Seller_account_name,WebConfig.ServerDomain + Config.Notify_url,
            "", url, callBackUrl, Config.Service_Create, Config.Sec_id, Config.Partner, req_id,
            Config.Format, Config.V, Config.Req_url, Config.PrivateKey, Config.Input_charset_UTF8);

        //构造，重定向URL
        return ali.alipay_Wap_Auth_AuthAndExecute(Config.Req_url, Config.Sec_id, Config.Partner, callBackUrl, Config.Format,
            Config.V, Config.Service_Auth, token, Config.Req_url, Config.PrivateKey, Config.Input_charset_UTF8);
    }
}