using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using VAGastronomistMobileApp.TheThirdPartyPaymentDll;
using System.Collections.Specialized;
using System.Data;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;
using System.Transactions;
using VAGastronomistMobileApp.SQLServerDAL.Persistence;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;

/// <summary>
/// 功能：服务器异步通知页面
/// 创建该页面文件时，请留心该页面文件中无任何HTML代码及空格。
/// 该页面不能在本机电脑测试，请到服务器上做测试。请确保外部可以访问该页面。
/// 如果没有收到该页面返回的 success 信息，支付宝会在24小时内按一定的时间策略重发通知
/// </summary>
public partial class aliRefundNotify : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string currentDate = System.DateTime.Now.Date.ToString("yyyyMMdd");//当前日期
        string filePath = Server.MapPath("~/Logs/refundLog" + currentDate + ".txt");//log路径

        SortedDictionary<string, string> sPara = GetRequestPost();

        if (sPara.Count > 0)//判断是否有带返回参数
        {
            Notify aliNotify = new Notify();
            bool verifyResult = aliNotify.Verify(sPara, Request.Form["notify_id"], Request.Form["sign"]);

            if (verifyResult)//是支付宝发来的信息且验签成功
            {
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //获取支付宝的通知返回参数
                string batch_no = Request.Form["batch_no"];//退款批次号
                string success_num = Request.Form["success_num"];//退款成功总数，必填，0<= success_num<= batch_num
                string result_details = Request.Form["result_details"];//处理结果详情，原付款支付宝交易号^退款总金额^退款状态
                string unfreezed_deta = Request.Form["unfreezed_deta"];//解冻结果明细，格式：解冻结订单号^冻结订单号^解冻结金额^交易号^处理时间^状态^描述码
                string notify_time = Request.Form["notify_time"];//通知时间
                string notify_type = Request.Form["notify_type"];//通知类型
                string notify_id = Request.Form["notify_id"];//通知校验ID 

                using (StreamWriter file = new StreamWriter(@filePath, true))
                {
                    file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " ----------------------success验证成功----------------------");
                    file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " batch_no--" + batch_no);
                    file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " success_num--" + success_num);
                    file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " result_details--" + result_details);
                    file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " unfreezed_deta--" + unfreezed_deta);
                    file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " notify_time--" + notify_time);
                    file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " notify_type--" + notify_type);
                    file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " notify_id--" + notify_id);
                }

                string[] resultDetails = result_details.Split('^');//处理结果详情
                string aliTradeNo = resultDetails[0].ToString();
                string refundSum = resultDetails[1].ToString();
                string refundStatus = resultDetails[2].ToString();

                using (StreamWriter file = new StreamWriter(@filePath, true))
                {
                    file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " 原付款支付宝交易号--" + aliTradeNo);
                    file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " 退款总金额--" + refundSum);
                    file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " 退款状态--" + refundStatus);
                }

                AliRefundOperate aliRefundOperate = new AliRefundOperate();
                AliRefundOrderInfo alirefundOrder = new AliRefundOrderInfo();
                alirefundOrder.notifyTime = notify_time;
                alirefundOrder.notifyType = notify_type;
                alirefundOrder.notifyId = notify_id;
                alirefundOrder.successNum = success_num;
                alirefundOrder.lastUpdateTime = DateTime.Now;
                alirefundOrder.batchNo = batch_no;
                alirefundOrder.notifyStatus = 1;

                //判断是否已经做过了这次通知返回的处理
                DataTable dtRefundOrder = aliRefundOperate.QueryAliRefund(batch_no);
                if (dtRefundOrder != null && dtRefundOrder.Rows.Count > 0)
                {
                    int originalId = Common.ToInt32(dtRefundOrder.Rows[0]["originalId"]);
                    long preOrder19dianId = Common.ToInt64(dtRefundOrder.Rows[0]["connId"]);
                    string refundMoney = dtRefundOrder.Rows[0]["refundSum"].ToString();

                    if (refundStatus == "SUCCESS")//退款成功
                    {
                        using (StreamWriter file = new StreamWriter(@filePath, true))
                        {
                            file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "支付宝返回退款状态为SUCCESS");
                        }

                        bool updateAliRefundOrder = aliRefundOperate.UpdateAliRefundOrder(alirefundOrder);

                        using (StreamWriter file = new StreamWriter(@filePath, true))
                        {
                            file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "支付宝退款记录更新完毕" + updateAliRefundOrder);
                        }
                        Response.Write("success");  //请不要修改或删除

                        using (StreamWriter file = new StreamWriter(@filePath, true))
                        {
                            file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "----------------------回复支付宝success----------------------");
                        }
                    }
                    else
                    {
                        Response.Write("fail");
                        using (StreamWriter file = new StreamWriter(@filePath, true))
                        {
                            file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "支付宝返回退款状态为" + refundStatus);
                            file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "----------------------回复支付宝fail----------------------");
                        }
                    }
                }
                else//如果没有找到相应数据
                {
                    Response.Write("success");//也回复支付宝success
                    using (StreamWriter file = new StreamWriter(@filePath, true))
                    {
                        file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " 没有找到相应数据");
                        file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "----------------------回复支付宝success----------------------");
                    }
                }
            }
            else//验证失败
            {
                Response.Write("fail");

                using (StreamWriter file = new StreamWriter(@filePath, true))
                {
                    file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "fail验证失败");
                    file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "----------------------回复支付宝fail----------------------");
                }
            }
        }
        else
        {
            Response.Write("无通知参数");

            using (StreamWriter file = new StreamWriter(@filePath, true))
            {
                file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "无通知参数");
                file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "----------------------回复支付宝无通知参数----------------------");
            }
        }
    }

    /// <summary>
    /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
    /// </summary>
    /// <returns>request回来的信息组成的数组</returns>
    public SortedDictionary<string, string> GetRequestPost()
    {
        int i = 0;
        SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
        NameValueCollection coll;
        //Load Form variables into NameValueCollection variable.
        coll = Request.Form;

        // Get names of all forms into a string array.
        String[] requestItem = coll.AllKeys;

        for (i = 0; i < requestItem.Length; i++)
        {
            sArray.Add(requestItem[i], Request.Form[requestItem[i]]);
        }

        return sArray;
    }
}