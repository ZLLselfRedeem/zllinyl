using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Web.Services;

public partial class AppPages_manual_feedback : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    /// <summary>
    /// 保存数据发送到服务器
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_save_Click(object sender, EventArgs e)
    {

    }
    [WebMethod]
    public static int Save(string cookie, string feedbackMsg)
    {
        try
        {
            if (!string.IsNullOrEmpty(cookie))
            {
                if (!string.IsNullOrEmpty(feedbackMsg))
                {
                    CustomerOperate customerOper = new CustomerOperate();
                    long customerId = customerOper.QueryCustomerByCookie(cookie);
                    if (customerId > 0)
                    {
                        long resultId = customerOper.AddCustomerFeedback(customerId, feedbackMsg);
                        if (resultId > 0)
                        {
                            return 1;//发送成功
                        }
                        else
                        {
                            return -1;//发送失败，请重试
                        }
                    }
                    else
                    {
                        return -2;//用户信息有误
                    }
                }
                else
                {
                    return -3;//反馈信息不能为空
                }
            }
            else
            {
                return -2;//用户信息有误
            }
        }
        catch
        {
            return -1;//发送失败，请重试
        }
    }
}