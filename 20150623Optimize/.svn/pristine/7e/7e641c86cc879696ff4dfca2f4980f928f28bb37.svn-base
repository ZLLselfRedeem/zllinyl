<%@ WebHandler Language="C#" Class="clintPoint" %>

using System;
using System.Web;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;
using System.Collections.Generic;
using System.Web.SessionState;
/// <summary>
/// 客户端服务员积分相关
/// created by wangcheng
/// 20140226
/// </summary>
public class clintPoint : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        string val = "";//返回值，json字符串
        string module = context.Request["m"] == null ? "" : Common.ToString(context.Request["m"].Trim());
        HtmlOperate htmlOper = new HtmlOperate();
        GoodsOperate goodsOper = new GoodsOperate();
        EmployeePointOperate employeeOperate = new EmployeePointOperate();
        EmployeePointLogOperate pointOperate = new EmployeePointLogOperate();
        switch (module)
        {
            case "point_html"://html配置
                int cityId = 0;
                if (context.Request["cityId"] != null)
                {
                    cityId = Common.ToInt32(context.Request["cityId"].Trim());
                    val = htmlOper.QueryHtml(cityId);
                }
                break;
            case "point_goods"://所有有效商品
                val = goodsOper.ClientQueryGoods();
                break;
            case "point_exchangeLog"://某个服务员的兑换记录
                int employeeId =0;
                if (context.Request["employeeId"] != null)
                {
                    employeeId = Common.ToInt32(context.Request["employeeId"].Trim());
                    val = pointOperate.QueryExchangeLogForClient(employeeId);
                }
                break;
            case "point_exchangeRequest"://兑换请求
                string phoneNumber = "";
                if (context.Request["phoneNumber"] != null)
                {
                    phoneNumber = context.Request["phoneNumber"].Trim();//用户手机号码                
                    val = employeeOperate.ClientExchangeRequest(phoneNumber);//给用户手机发送验证码并返回给前端结果
                }
                break;
            case "point_exchangeConfirm"://兑换确认
                string verificationCode = "";
                int goodsId = 0;
                phoneNumber = "";
                if (context.Request["phoneNumber"] != null && context.Request["verificationCode"] != null && context.Request["goodsId"] != null)
                {
                    phoneNumber = context.Request["phoneNumber"].Trim();//用户手机号码
                    verificationCode = context.Request["verificationCode"].Trim();//用户输入的验证码
                    goodsId = Common.ToInt32(context.Request["goodsId"].Trim());

                    val = employeeOperate.ClientExchangeConfirm(phoneNumber, verificationCode, goodsId);
                }
                break;
            case "point_log"://服务员所有积分变动记录
                if (context.Request["employeeId"] != null)
                {
                    employeeId = Common.ToInt32(context.Request["employeeId"].Trim());
                    val = pointOperate.ClientQueryEmployeePoint(employeeId);
                }
                break;
            case "point_lastLog"://服务员最近一次积分变动记录
                if (context.Request["employeeId"] != null)
                {
                    employeeId = Common.ToInt32(context.Request["employeeId"].Trim());
                    val = pointOperate.ClientQueryEmployeeLastPointLog(employeeId);
                }
                break;
        }
        context.Response.Write(val);
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}