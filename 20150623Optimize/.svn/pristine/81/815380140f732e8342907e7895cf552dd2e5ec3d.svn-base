using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;
using System.Web.Services;
using System.Data;
using VA.AllNotifications;
using System.Transactions;
using System.Text;
using Web.Control;
using UmengPush;

public partial class SystemConfig_CustomPushConfig : System.Web.UI.Page
{
    public const int pageSize = 10;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            rblIsSent.SelectedValue = "false";
            txbBegin.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd") + " 00:00:00";
            txbEnd.Text = DateTime.Now.ToString("yyyy/MM/dd") + " 23:59:59";
            BindPushList(0, 0, false, txbBegin.Text, txbEnd.Text, 1, pageSize);
            divRecommend.Attributes.Add("style", "display:none");
            divOrder.Attributes.Add("style", "display:none");
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindPushList(0, VAGastronomistMobileApp.WebPageDll.Common.ToInt32(ddlTypeQuery.SelectedValue), VAGastronomistMobileApp.WebPageDll.Common.ToBool(rblIsSent.SelectedValue), txbBegin.Text, txbEnd.Text, 1, pageSize);
    }

    /// <summary>
    /// 绑定所有自定义推送列表
    /// </summary>
    private void BindPushList(int activityId, int customType, bool isSent, string beginTime, string endTime, int pageIndex, int pageSize)
    {
        CustomPushRecordOperate operate = new CustomPushRecordOperate();
        int cnt = 0;
        List<CustomPushRecord> records = operate.QueryAllCustomPushRecord(new VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure.Page(pageIndex, pageSize), customType, isSent, beginTime, endTime, out cnt);

        if (records.Count > 0)
        {
            AspNetPager1.PageSize = pageSize;
            AspNetPager1.RecordCount = cnt;//总数
            this.gdvPushList.DataSource = records;
        }
        else
        {
            this.gdvPushList.DataSource = null;
        }
        this.gdvPushList.DataBind();
    }

    protected void lnkbtn_OnCommand(object sender, CommandEventArgs e)
    {
        CustomPushRecordOperate operate = new CustomPushRecordOperate();

        long pushId = Convert.ToInt64(e.CommandArgument);

        switch (e.CommandName)
        {
            case "detail":
                ViewState["source"] = "detail";
                int cnt = 0;
                List<CustomPushRecord> records = operate.QueryAllCustomPushRecord(new VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure.Page(1, pageSize), 0, VAGastronomistMobileApp.WebPageDll.Common.ToBool(rblIsSent.SelectedValue), txbBegin.Text, txbEnd.Text, out cnt, pushId);
                if (records != null && records.Count > 0)
                {
                    txbMobilePhoneNumber.Text = records[0].mobilePhoneNumber;
                    ddlType.SelectedValue = records[0].customType;
                    txbMessage.Text = records[0].message;
                    txbCustomSendTime.Text = records[0].customSendTime.ToString();
                    switch (records[0].customType)
                    {
                        case "13":
                        case "21":
                            divRecommend.Attributes.Add("style", "display:'';width:80%");
                            divOrder.Attributes.Add("style", "display:none");
                            txbRecommendUrl.Text = records[0].customValue;
                            break;
                        case "14":
                            divRecommend.Attributes.Add("style", "display:none");
                            divOrder.Attributes.Add("style", "display:'';width:80%");
                            shopName.Value = records[0].customValue;
                            break;
                        case "15":
                        case "22":
                        case "23":
                            divRecommend.Attributes.Add("style", "display:none");
                            divOrder.Attributes.Add("style", "display:none");
                            break;
                        case "16":
                            divRecommend.Attributes.Add("style", "display:none");
                            divOrder.Attributes.Add("style", "display:none");
                            break;
                    }
                }
                this.divList.Attributes.Add("style", "display:none");
                this.divDetail.Attributes.Add("style", "display:''");
                btnSave.Enabled = false;
                break;
            case "del":
                bool delete = operate.DeleteCustomPushRecord(pushId);
                if (delete)
                {
                    BindPushList(0, VAGastronomistMobileApp.WebPageDll.Common.ToInt32(ddlTypeQuery.SelectedValue), VAGastronomistMobileApp.WebPageDll.Common.ToBool(rblIsSent.SelectedValue), txbBegin.Text, txbEnd.Text, 1, pageSize);
                    CommonPageOperate.AlterMsg(this, "删除成功！");
                    Clear();
                }
                else
                {
                    CommonPageOperate.AlterMsg(this, "删除失败！");
                }
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 分页操作
    /// </summary>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindPushList(0, Convert.ToInt32(ddlTypeQuery.SelectedValue), VAGastronomistMobileApp.WebPageDll.Common.ToBool(rblIsSent.SelectedValue), txbBegin.Text, txbEnd.Text, AspNetPager1.CurrentPageIndex, pageSize);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        this.divList.Attributes.Add("style", "display:none");
        this.divDetail.Attributes.Add("style", "display:''");
        divRecommend.Attributes.Add("style", "display:none");
        divOrder.Attributes.Add("style", "display:none");

        ddlType.Enabled = false;
        txbMessage.Enabled = false;
        txbCustomSendTime.Enabled = false;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            IOSPush IOSPush = new IOSPush();
            AndroidPush AndroidPush = new AndroidPush();
            CustomPushRecordOperate customPushRecordOperate = new CustomPushRecordOperate();
            int cnt = 0;
            List<CustomerDevicePushInfo> List = QueryCustomerDeviceList(new VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure.Page(1, 500), out cnt);

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("isLocked", typeof(Boolean)));
            dt.Columns.Add(new DataColumn("pushToken", typeof(String)));
            dt.Columns.Add(new DataColumn("addTime", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("customSendTime", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("isSent", typeof(bool)));
            dt.Columns.Add(new DataColumn("sendCount", typeof(Int32)));
            dt.Columns.Add(new DataColumn("appType", typeof(Int32)));
            dt.Columns.Add(new DataColumn("message", typeof(String)));
            dt.Columns.Add(new DataColumn("customType", typeof(String)));
            dt.Columns.Add(new DataColumn("customValue", typeof(String)));
            dt.Columns.Add(new DataColumn("mobilePhoneNumber", typeof(String)));
            dt.Columns.Add(new DataColumn("customerId", typeof(String)));

            if (VAGastronomistMobileApp.WebPageDll.Common.ToInt32(ddlType.SelectedValue) == (int)UmengPush.VANotificationsCustomType.NOTIFICATIONS_REDENVELOPE)
            {
                #region 每条推送里要带手机号码，所以单条处理
                bool push1 = false;
                bool push2 = false;
                foreach (CustomerDevicePushInfo item in List)
                {

                    DataRow dr = dt.NewRow();
                    dr["isLocked"] = false;
                    dr["pushToken"] = item.pushToken;
                    dr["addTime"] = DateTime.Now;
                    dr["customSendTime"] = VAGastronomistMobileApp.WebPageDll.Common.ToDateTime(txbCustomSendTime.Text.Trim());
                    dr["isSent"] = false;
                    dr["sendCount"] = 0;
                    dr["appType"] = (int)item.appType;
                    dr["message"] = txbMessage.Text.Trim();
                    dr["customType"] = ddlType.SelectedValue;
                    dr["customValue"] = WebConfig.ServerDomain + "AppPages/RedEnvelope/list.aspx?mobilePhone=" + item.mobilePhoneNumber;
                    dr["mobilePhoneNumber"] = item.mobilePhoneNumber;
                    dr["customerId"] = item.CustomerID;

                    if (item.appType == VAAppType.IPHONE)
                    {
                        push1 = IOSPush.IOSUnicast(item.pushToken, txbMessage.Text.Trim(), WebConfig.ServerDomain + "AppPages/RedEnvelope/list.aspx?mobilePhone=" + item.mobilePhoneNumber, UmengPush.VANotificationsCustomType.NOTIFICATIONS_REDENVELOPE);
                        if (push1)
                        {
                            dr["isSent"] = true;
                            dr["sendCount"] = 1;
                        }
                    }
                    else
                    {
                        //友盟推送码提交
                        if (item.pushToken.Contains("um"))
                        {
                            push2 = AndroidPush.AndroidCustomizedcast(item.pushToken, "悠先点菜", txbMessage.Text.Trim(), (int)UmengPush.VANotificationsCustomType.NOTIFICATIONS_REDENVELOPE, WebConfig.ServerDomain + "AppPages/RedEnvelope/list.aspx?mobilePhone=" + item.mobilePhoneNumber);
                            //push2 = AndroidPush.AndroidCustomizedcast(item.pushToken, txbMessage.Text.Trim(), txbMessage.Text.Trim(), (int)UmengPush.VANotificationsCustomType.NOTIFICATIONS_REDENVELOPE, WebConfig.ServerDomain + "AppPages/RedEnvelope/list.aspx?mobilePhone=" + item.mobilePhoneNumber);
                            dr["isSent"] = true;
                            dr["sendCount"] = 1;
                        }
                    }
                    dt.Rows.Add(dr);
                }
                #endregion
            }
            else
            {
                #region 分成Android和IOS两大块处理，pushToken拼在一起
                List<CustomerDevicePushInfo> androidCustomer = List.FindAll(x => (int)x.appType == (int)VAAppType.ANDROID && !x.pushToken.Contains("um"));
                List<CustomerDevicePushInfo> androidCustomerUmeng = List.FindAll(x => (int)x.appType == (int)VAAppType.ANDROID && x.pushToken.Contains("um"));
                List<CustomerDevicePushInfo> iosCustomer = List.FindAll(x => (int)x.appType == (int)VAAppType.IPHONE);

                if (androidCustomer.Count > 0)
                {
                    string androidPushToken = "", androidCustomerId = "", androidPhone = "";
                    foreach (CustomerDevicePushInfo item in androidCustomer)
                    {
                        androidPushToken += item.pushToken + ",";//android用英文逗号隔开
                        androidCustomerId += item.CustomerID + ",";
                        androidPhone += item.mobilePhoneNumber + ",";
                    }
                    androidPushToken = androidPushToken.Remove(androidPushToken.Length - 1, 1);
                    androidCustomerId = androidCustomerId.Remove(androidCustomerId.Length - 1, 1);
                    androidPhone = androidPhone.Remove(androidPhone.Length - 1, 1);

                    #region
                    DataRow dr = dt.NewRow();
                    dr["isLocked"] = false;
                    dr["pushToken"] = androidPushToken;
                    dr["mobilePhoneNumber"] = androidPhone;
                    dr["customerId"] = androidCustomerId;
                    dr["appType"] = (int)VAAppType.ANDROID;
                    dr["addTime"] = DateTime.Now;
                    dr["customSendTime"] = VAGastronomistMobileApp.WebPageDll.Common.ToDateTime(txbCustomSendTime.Text.Trim());
                    dr["isSent"] = false;
                    dr["sendCount"] = 0;
                    dr["message"] = txbMessage.Text.Trim();
                    dr["customType"] = ddlType.SelectedValue;
                    switch (VAGastronomistMobileApp.WebPageDll.Common.ToInt32(ddlType.SelectedValue))
                    {
                        case (int)UmengPush.VANotificationsCustomType.NOTIFICATIONS_RECOMMEND:
                        case (int)UmengPush.VANotificationsCustomType.NOTIFICATIONS_REDENVELOPE_GET:
                            dr["customValue"] = txbRecommendUrl.Text.Trim();
                            break;
                        case (int)UmengPush.VANotificationsCustomType.NOTIFICATIONS_ORDER:
                            if (!string.IsNullOrEmpty(hiddenShopId.Value))
                            {
                                dr["customValue"] = hiddenShopId.Value;
                            }
                            else
                            {
                                if (shopName.Value.Length > 200)
                                {
                                    shopName.Value.Substring(0, 200);
                                }
                                dr["customValue"] = customPushRecordOperate.QueryShopId(shopName.Value);
                                if (dr["customValue"] != null && dr["customValue"] != "")
                                {
                                    CommonPageOperate.AlterMsg(this, "请选择正确的门店！");
                                    return;
                                }
                            }
                            break;
                        case (int)UmengPush.VANotificationsCustomType.NOTIFICATIONS_ORDERLIST:
                        case (int)UmengPush.VANotificationsCustomType.NOTIFICATIONS_FOODPLAZA:
                        case (int)UmengPush.VANotificationsCustomType.NOTIFICATIONS_INDEX:
                            dr["customValue"] = "-999";
                            break;
                        case (int)UmengPush.VANotificationsCustomType.NOTIFICATIONS_EVALUATION://评价，暂时借用跳到点菜
                            dr["customValue"] = "-999";
                            break;
                        default:
                            break;
                    }
                    dt.Rows.Add(dr);
                    #endregion
                }
                if (androidCustomerUmeng != null && androidCustomerUmeng.Count > 0)
                {
                    List<string> androidMobile = new List<string>();
                    int len = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(androidCustomerUmeng.Count) / 10000));
                    string customValue = "";
                    bool androidPushResult = false;
                    for (int i = 0; i < len; i++)
                    {
                        #region
                        int a = 10000 * i, b = 10000 * (i + 1) > androidCustomerUmeng.Count ? androidCustomerUmeng.Count : 10000 * (i + 1);

                        for (int j = a; j < b; j++)
                        {
                            androidMobile.Add(androidCustomerUmeng[j].pushToken);
                        }
                        switch (VAGastronomistMobileApp.WebPageDll.Common.ToInt32(ddlType.SelectedValue))
                        {
                            case (int)UmengPush.VANotificationsCustomType.NOTIFICATIONS_RECOMMEND:
                            case (int)UmengPush.VANotificationsCustomType.NOTIFICATIONS_REDENVELOPE_GET:
                                customValue = txbRecommendUrl.Text.Trim();
                                break;
                            case (int)UmengPush.VANotificationsCustomType.NOTIFICATIONS_ORDER:
                                if (!string.IsNullOrEmpty(hiddenShopId.Value))
                                {
                                    customValue = hiddenShopId.Value;
                                }
                                else
                                {
                                    if (shopName.Value.Length > 200)
                                    {
                                        shopName.Value.Substring(0, 200);
                                    }
                                    customValue = customPushRecordOperate.QueryShopId(shopName.Value);
                                    if (customValue != null && customValue != "")
                                    {
                                        CommonPageOperate.AlterMsg(this, "请选择正确的门店！");
                                        return;
                                    }
                                }
                                break;
                            case (int)UmengPush.VANotificationsCustomType.NOTIFICATIONS_ORDERLIST:
                            case (int)UmengPush.VANotificationsCustomType.NOTIFICATIONS_FOODPLAZA:
                            case (int)UmengPush.VANotificationsCustomType.NOTIFICATIONS_INDEX:
                                customValue = "-999";
                                break;
                            default:
                                break;
                        }
                        #endregion
                        androidPushResult = AndroidPush.AndroidFilecast((UmengPush.VANotificationsCustomType)Convert.ToInt32(ddlType.SelectedValue), androidMobile, "悠先点菜", txbMessage.Text.Trim(), customValue);
                        //androidPushResult = AndroidPush.AndroidFilecast((UmengPush.VANotificationsCustomType)Convert.ToInt32(ddlType.SelectedValue), androidMobile, txbMessage.Text.Trim(), txbMessage.Text.Trim(), customValue);
                        #region
                        string androidPushToken = "", androidCustomerId = "", androidPhone = "";
                        foreach (CustomerDevicePushInfo item in androidCustomerUmeng)
                        {
                            androidPushToken += item.pushToken + ",";//android用英文逗号隔开
                            androidCustomerId += item.CustomerID + ",";
                            androidPhone += item.mobilePhoneNumber + ",";
                        }
                        androidPushToken = androidPushToken.Remove(androidPushToken.Length - 1, 1);
                        androidCustomerId = androidCustomerId.Remove(androidCustomerId.Length - 1, 1);
                        androidPhone = androidPhone.Remove(androidPhone.Length - 1, 1);
                        DataRow dr = dt.NewRow();
                        dr["isLocked"] = false;
                        dr["pushToken"] = androidPushToken;
                        dr["mobilePhoneNumber"] = androidPhone;
                        dr["customerId"] = androidCustomerId;
                        dr["appType"] = (int)VAAppType.ANDROID;
                        dr["addTime"] = DateTime.Now;
                        dr["customSendTime"] = VAGastronomistMobileApp.WebPageDll.Common.ToDateTime(txbCustomSendTime.Text.Trim());
                        dr["isSent"] = true;
                        dr["sendCount"] = 1;
                        dr["message"] = txbMessage.Text.Trim();
                        dr["customType"] = ddlType.SelectedValue;
                        dr["customValue"] = customValue;
                        dt.Rows.Add(dr);
                        #endregion
                    }
                }
                if (iosCustomer.Count > 0)
                {
                    #region
                    List<string> iosMobile = new List<string>();
                    int len = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(iosCustomer.Count) / 4));
                    string customValue = "";
                    bool iosPushResult = false;
                    for (int i = 0; i < len; i++)
                    {
                        int a = 10000 * i, b = 10000 * (i + 1) > iosCustomer.Count ? iosCustomer.Count : 10000 * (i + 1);

                        for (int j = a; j < b; j++)
                        {
                            iosMobile.Add(iosCustomer[j].pushToken);
                        }
                        #region
                        switch (VAGastronomistMobileApp.WebPageDll.Common.ToInt32(ddlType.SelectedValue))
                        {
                            case (int)UmengPush.VANotificationsCustomType.NOTIFICATIONS_RECOMMEND:
                            case (int)UmengPush.VANotificationsCustomType.NOTIFICATIONS_REDENVELOPE_GET:
                                customValue = txbRecommendUrl.Text.Trim();
                                break;
                            case (int)UmengPush.VANotificationsCustomType.NOTIFICATIONS_ORDER:
                                if (!string.IsNullOrEmpty(hiddenShopId.Value))
                                {
                                    customValue = hiddenShopId.Value;
                                }
                                else
                                {
                                    if (shopName.Value.Length > 200)
                                    {
                                        shopName.Value.Substring(0, 200);
                                    }
                                    customValue = customPushRecordOperate.QueryShopId(shopName.Value);
                                    if (customValue != null && customValue != "")
                                    {
                                        CommonPageOperate.AlterMsg(this, "请选择正确的门店！");
                                        return;
                                    }
                                }
                                break;
                            case (int)UmengPush.VANotificationsCustomType.NOTIFICATIONS_ORDERLIST:
                            case (int)UmengPush.VANotificationsCustomType.NOTIFICATIONS_FOODPLAZA:
                            case (int)UmengPush.VANotificationsCustomType.NOTIFICATIONS_INDEX:
                                customValue = "-999";
                                break;
                            default:
                                break;
                        }
                        #endregion

                        iosPushResult = IOSPush.IOSFilecast((UmengPush.VANotificationsCustomType)Convert.ToInt32(ddlType.SelectedValue), iosMobile, "悠先点菜", txbMessage.Text.Trim(), customValue);
                        //iosPushResult = IOSPush.IOSFilecast((UmengPush.VANotificationsCustomType)Convert.ToInt32(ddlType.SelectedValue), iosMobile, txbMessage.Text.Trim(), txbMessage.Text.Trim(), customValue);

                        #region
                        string iosPushToken = "", iosCustomerId = "", iosPhone = "";
                        foreach (CustomerDevicePushInfo item in iosCustomer)
                        {
                            iosPushToken += item.pushToken + "*";//IOS用*隔开
                            iosCustomerId += item.CustomerID + ",";
                            iosPhone += item.mobilePhoneNumber + ",";
                        }
                        iosPushToken = iosPushToken.Remove(iosPushToken.Length - 1, 1);
                        iosCustomerId = iosCustomerId.Remove(iosCustomerId.Length - 1, 1);
                        iosPhone = iosPhone.Remove(iosPhone.Length - 1, 1);

                        DataRow dr = dt.NewRow();
                        dr["isLocked"] = false;
                        dr["pushToken"] = iosPushToken;
                        dr["mobilePhoneNumber"] = iosPhone;
                        dr["customerId"] = iosCustomerId;
                        dr["appType"] = (int)VAAppType.IPHONE;
                        dr["addTime"] = DateTime.Now;
                        dr["customSendTime"] = VAGastronomistMobileApp.WebPageDll.Common.ToDateTime(txbCustomSendTime.Text.Trim());
                        dr["isSent"] = true;
                        dr["sendCount"] = 1;
                        dr["message"] = txbMessage.Text.Trim();
                        dr["customType"] = ddlType.SelectedValue;
                        dr["customValue"] = customValue;
                        dt.Rows.Add(dr);
                        #endregion
                    }
                    #endregion
                }
                #endregion
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    bool insert = customPushRecordOperate.BatchInsertCustomPushRecord(dt);

                    if (insert)
                    {
                        ts.Complete();
                        this.divDetail.Attributes.Add("style", "display:none");
                        this.divList.Attributes.Add("style", "display:''");
                        CommonPageOperate.AlterMsg(this, "保存成功！");
                    }
                    else
                    {
                        CommonPageOperate.AlterMsg(this, "保存失败！");
                    }
                }
                Clear();
                BindPushList(0, VAGastronomistMobileApp.WebPageDll.Common.ToInt32(ddlTypeQuery.SelectedValue), VAGastronomistMobileApp.WebPageDll.Common.ToBool(rblIsSent.SelectedValue), txbBegin.Text, txbEnd.Text, 1, pageSize);
            }
        }
        catch (Exception ex)
        {
            CommonPageOperate.AlterMsg(this, ex.Message);
        }
    }

    protected void btnCancle_Click(object sender, EventArgs e)
    {
        this.divDetail.Attributes.Add("style", "display:none");
        this.divList.Attributes.Add("style", "display:''");
        Clear();
        ViewState["source"] = "";
    }

    private void Clear()
    {
        ddlType.SelectedValue = "0";
        txbMobilePhoneNumber.Text = "";
        txbCustomSendTime.Text = "";
        txbMessage.Text = "";
        txbRecommendUrl.Text = "";
        gdvCustomerDevice.DataSource = null;
        gdvCustomerDevice.DataBind();
        AspNetPager2.RecordCount = 0;
        btnSave.Enabled = true;
        if (ViewState["source"].ToString() != "detail")
        {
            rblIsSent.SelectedValue = "false";
            txbBegin.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd") + " 00:00:00";
            txbEnd.Text = DateTime.Now.ToString("yyyy/MM/dd") + " 23:59:59";
            BindPushList(0, VAGastronomistMobileApp.WebPageDll.Common.ToInt32(ddlTypeQuery.SelectedValue), VAGastronomistMobileApp.WebPageDll.Common.ToBool(rblIsSent.SelectedValue), txbBegin.Text, txbEnd.Text, 1, pageSize);
        }
    }

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        string value = ddlType.SelectedValue;
        switch (value)
        {
            case "13":
            case "21":
                divRecommend.Attributes.Add("style", "display:'';width:80%");
                divOrder.Attributes.Add("style", "display:none");
                break;
            case "14":
                divRecommend.Attributes.Add("style", "display:none");
                divOrder.Attributes.Add("style", "display:'';width:80%");
                break;
            case "15":
            case "22":
            case "23":
            case "16":
                divRecommend.Attributes.Add("style", "display:none");
                divOrder.Attributes.Add("style", "display:none");
                break;
            default:
                break;
        }
    }

    protected void btnCheck_Click(object sender, EventArgs e)
    {
        BindPhoneList(new VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure.Page(1, pageSize));
    }

    private void BindPhoneList(VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure.Page page)
    {
        try
        {
            int cnt = 0;
            List<CustomerDevicePushInfo> customerDevice = QueryCustomerDeviceList(page, out cnt);
            if (customerDevice != null && customerDevice.Count > 0)
            {
                AspNetPager2.RecordCount = cnt;
                AspNetPager2.PageSize = pageSize;
                gdvCustomerDevice.DataSource = customerDevice;
                ddlType.Enabled = true;
                txbMessage.Enabled = true;
                txbCustomSendTime.Enabled = true;
            }
            else
            {
                gdvCustomerDevice.DataSource = null;
            }
            gdvCustomerDevice.DataBind();
        }
        catch (Exception ex)
        {
            divRecommend.Attributes.Add("style", "display:none");
            divOrder.Attributes.Add("style", "display:none");
            CommonPageOperate.AlterMsg(this, "出错啦" + ex.Message);
        }
    }

    private List<CustomerDevicePushInfo> QueryCustomerDeviceList(VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure.Page page, out int cnt)
    {
        CustomPushRecordOperate pushOperate = new CustomPushRecordOperate();
        string phoneList = txbMobilePhoneNumber.Text.Trim().Replace("，", ",");

        string[] strPhone = phoneList.Split(new string[1] { "," }, StringSplitOptions.RemoveEmptyEntries);
        string sqlPhone = "";
        for (int i = 0; i < strPhone.Length; i++)
        {
            if (i == strPhone.Length - 1)
            {
                sqlPhone += "'" + strPhone[i] + "'";
            }
            else
            {
                sqlPhone += "'" + strPhone[i] + "',";
            }
        }
        List<CustomerDevicePushInfo> customerDevice = pushOperate.QueryCustomerInfoList(page, sqlPhone, out cnt);
        return customerDevice;
    }

    [WebMethod]
    public static string CheckUser(string phone)
    {
        CustomPushRecordOperate pushOperate = new CustomPushRecordOperate();
        string userInfo = "";
        try
        {
            CustomerDevicePushInfo customerDevicePushInfo = pushOperate.QueryCustomerInfo(phone);
            if (customerDevicePushInfo != null)
            {
                userInfo = VAGastronomistMobileApp.WebPageDll.JsonOperate.JsonSerializer<CustomerDevicePushInfo>(customerDevicePushInfo);
                return userInfo;
            }
            else
            {
                return "{\"err\":\"不符合推送要求\"}";
            }
        }
        catch (Exception)
        {
            return "{\"err\":\"出错啦\"}";
        }
    }

    [WebMethod]
    public static string Save(string type, string message, string attachment, string customSendTime)
    {
        if (!string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(message) && !string.IsNullOrEmpty(customSendTime))
        {
            return "{\"result\":\"ok\"}";
        }
        else
        {
            return "{\"result\":\"参数不正确\"}";
        }
    }

    protected void gdvPushList_DataBound(object sender, EventArgs e)
    {
        int customType = 0;
        string isSent = "";
        for (int i = 0; i < gdvPushList.Rows.Count; i++)
        {
            customType = VAGastronomistMobileApp.WebPageDll.Common.ToInt32(gdvPushList.DataKeys[i].Values["customType"]);
            switch (customType)
            {
                case 13:
                    gdvPushList.Rows[i].Cells[1].Text = "推荐专题";
                    break;
                case 14:
                    gdvPushList.Rows[i].Cells[1].Text = "点菜页面";
                    break;
                case 15:
                    gdvPushList.Rows[i].Cells[1].Text = "红包列表";
                    break;
                case 16:
                    gdvPushList.Rows[i].Cells[1].Text = "点单列表";
                    break;
                case 21:
                    gdvPushList.Rows[i].Cells[1].Text = "领红包H5";
                    break;
                case 22:
                    gdvPushList.Rows[i].Cells[1].Text = "美食广场";
                    break;
                case 23:
                    gdvPushList.Rows[i].Cells[1].Text = "首页";
                    break;
                default:
                    break;
            }
            isSent = gdvPushList.DataKeys[i].Values["isSent"].ToString();
            if (isSent == "False")
            {
                gdvPushList.Rows[i].Cells[5].Text = "";
            }
        }
    }

    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        BindPhoneList(new VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure.Page(AspNetPager2.CurrentPageIndex, pageSize));
    }

    protected void rblIsSent_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindPushList(0, VAGastronomistMobileApp.WebPageDll.Common.ToInt32(ddlTypeQuery.SelectedValue), VAGastronomistMobileApp.WebPageDll.Common.ToBool(rblIsSent.SelectedValue), txbBegin.Text, txbEnd.Text, 1, pageSize);
    }


    protected void btnImport_Click(object sender, EventArgs e)
    {
        ImportResult result = ExcelHelper.ImportExcel(fileUploadPhone);
        if (result != null && result.dtPhone != null && result.dtPhone.Rows.Count > 0)
        {
            StringBuilder strPhone = new StringBuilder();
            foreach (DataRow dr in result.dtPhone.Rows)
            {
                strPhone.Append(dr[0]);
                strPhone.Append(',');
            }
            strPhone = strPhone.Remove(strPhone.Length - 1, 1);
            txbMobilePhoneNumber.Text = strPhone.ToString();
        }
        else
        {
            CommonPageOperate.AlterMsg(this, result.message);
        }
    }
}
