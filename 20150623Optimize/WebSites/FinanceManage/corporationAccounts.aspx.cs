using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
using VAGastronomistMobileApp.Model;
using System.Transactions;
using Web.Control.DDL;
using System.Net;
using System.Net.Sockets;
using VA.CacheLogic;
using Web.Control;
using System.Xml.Linq;
using System.Text;
using System.Text.RegularExpressions;
using log4net;
using System.IO;
using System.Configuration;

public partial class FinanceManage_corporationAccounts : System.Web.UI.Page
{
    private static string masterID = string.Empty;
    private static string serverAddress = string.Empty;
    private static int signPort = 0;
    private static int httpPort = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            masterID = ConfigurationManager.AppSettings["masterID"];
            serverAddress = ConfigurationManager.AppSettings["serverAddress"];
            signPort = Common.ToInt32(ConfigurationManager.AppSettings["signPort"]);
            httpPort = Common.ToInt32(ConfigurationManager.AppSettings["httpPort"]);
            
            //初始化分页
            //Button_10.CssClass = "tabButtonBlueClick";
            //Button_50.CssClass = "tabButtonBlueUnClick";
            //Button_100.CssClass = "tabButtonBlueUnClick";
            AspNetPager1.CurrentPageIndex = 1;
            AspNetPager1.PageSize = 20;
            SystemConfigCacheLogic sccl=new SystemConfigCacheLogic();
            lbAccount.Text = "当前账户：" + sccl.GetSystemConfig("payAcctNo", "95230154800001743") + "(浦发银行)";
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(txtOperateBeginTime.Text))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('开始时间不能为空')</script>");
            return;
        }
        if (String.IsNullOrEmpty(txtOperateEndTime.Text))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('结束时间不能为空')</script>");
            return;
        }
        if (Common.ToDateTime(txtOperateBeginTime.Text) > Common.ToDateTime(txtOperateEndTime.Text))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('开始时间不能大于结束时间')</script>");
            return;
        }
        if ((Common.ToDateTime(txtOperateEndTime.Text)-Common.ToDateTime(txtOperateBeginTime.Text)).Days>31)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('查询区间不能大于一个月')</script>");
            return;
        }

        AspNetPager1.CurrentPageIndex = 1;

        loadData(0, AspNetPager1.PageSize);
    }

    //分页数目控制
    //protected void Button_LargePageCount_Click(object sender, EventArgs e)
    //{
    //    Button Button = (Button)sender;
    //    Label_LargePageCount.Text = "";
    //    switch (Button.CommandName)
    //    {
    //        case "button_10":
    //            Button_10.CssClass = "tabButtonBlueClick";
    //            Button_50.CssClass = "tabButtonBlueUnClick";
    //            Button_100.CssClass = "tabButtonBlueUnClick";
    //            Label_LargePageCount.Text = "10";
    //            break;
    //        case "button_50":
    //            Button_10.CssClass = "tabButtonBlueUnClick";
    //            Button_50.CssClass = "tabButtonBlueClick";
    //            Button_100.CssClass = "tabButtonBlueUnClick";
    //            Label_LargePageCount.Text = "50";
    //            break;
    //        case "button_100":
    //            Button_10.CssClass = "tabButtonBlueUnClick";
    //            Button_50.CssClass = "tabButtonBlueUnClick";
    //            Button_100.CssClass = "tabButtonBlueClick";
    //            Label_LargePageCount.Text = "100";
    //            break;
    //        default:
    //            break;
    //    }
    //    loadData(0, Common.ToInt32(Label_LargePageCount.Text));
    //}

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        loadData(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }
   
    //protected void gdList_Sorting(object sender, GridViewSortEventArgs e)
    //{
    //    Dictionary<string, string> sorted = new Dictionary<string, string>();
    //    if (ViewState["SortedField"] == null)
    //    {
    //        sorted.Add(e.SortExpression, "ASC");
    //        ViewState["SortedField"] = sorted;
    //    }
    //    else
    //    {
    //        sorted = (Dictionary<string, string>)ViewState["SortedField"];
    //        if (sorted.ContainsKey(e.SortExpression))
    //        {
    //            if (sorted[e.SortExpression] == "ASC")
    //            {
    //                sorted[e.SortExpression] = "DESC";
    //            }
    //            else
    //            {
    //                sorted[e.SortExpression] = "ASC";
    //            }
    //        }
    //        else
    //        {
    //            sorted.Clear();
    //            sorted.Add(e.SortExpression, "ASC");
    //            ViewState["SortedField"] = sorted;
    //        }
    //    }
    //    loadData(0, Common.ToInt32(Label_LargePageCount.Text));
    //}
   
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        CreateExcel(GetGrData());
    }

    private DataTable GetGrData()
    {
        DataTable dt = new DataTable();
        PreOrder19dianOperate po = new PreOrder19dianOperate();
      
        DataTable dtApply = null;// po.GetFinancial(shopID, txtOperateBeginTime.Text, txtOperateEndTime.Text, Common.ToInt32(ddlStatus.SelectedValue), Common.ToInt32(ddlCity.SelectedValue));

        dt.Columns.Add("流水号", typeof(string));
        dt.Columns.Add("交易时间", typeof(string));
        dt.Columns.Add("对方账号", typeof(string));
        dt.Columns.Add("对方户名", typeof(string));
        dt.Columns.Add("借方金额", typeof(string));
        dt.Columns.Add("贷方金额", typeof(string));
        dt.Columns.Add("余额", typeof(string));
        dt.Columns.Add("摘要", typeof(string));
        dt.Columns.Add("备注", typeof(string));

        for (int i = 0; i < dtApply.Rows.Count; i++)
        {
            DataRow dr = dt.NewRow();
            dr[0] = dtApply.Rows[i]["shopName"].ToString();
            dr[1] = dtApply.Rows[i]["companyName"].ToString();
            dr[2] = dtApply.Rows[i]["preOrderSum"].ToString();
            dr[3] = dtApply.Rows[i]["viewallocCommissionValue"].ToString();
            dr[4] = dtApply.Rows[i]["viewalloc"].ToString();
            dr[5] = dtApply.Rows[i]["viewallocCommission"].ToString();
            dr[6] = dtApply.Rows[i]["redenvelope"].ToString();
            dr[7] = dtApply.Rows[i]["balance"].ToString();
            dr[8] = dtApply.Rows[i]["alipay"].ToString();
            dr[9] = dtApply.Rows[i]["wechat"].ToString();
            dt.Rows.Add(dr);

        }
        return dt;
    }

    private void CreateExcel(DataTable dt)
    {
        if (dt.Rows.Count.Equals(0))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('查无相关明细')</script>");
            return;
        }
        string excelName = HttpUtility.UrlEncode("批量打款_" + DateTime.Now.ToString("yyyy/mm/dd_hh:mm:ss"), System.Text.Encoding.UTF8).ToString();
        HttpResponse resp;
        resp = Page.Response;
        resp.Buffer = true;
        resp.ClearContent();
        resp.ClearHeaders();
        resp.Charset = "GB2312";
        resp.AppendHeader("Content-Disposition", "attachment;filename=" + excelName + ".xls");
        resp.ContentEncoding = System.Text.Encoding.Default;//设置输出流为简体中文   
        resp.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。 
        string colHeaders = "", ls_item = "";
        DataRow[] myRow = dt.Select();
        int i = 0;
        int cl = dt.Columns.Count;
        for (i = 0; i < cl; i++)
        {
            if (i == (cl - 1))//最后一列，加n
            {
                colHeaders += dt.Columns[i].Caption.ToString().Trim() + "\n";
            }
            else
            {
                colHeaders += dt.Columns[i].Caption.ToString().Trim() + "\t";
            }
        }
        resp.Write(colHeaders);
        foreach (DataRow row in myRow)
        {
            for (i = 0; i < cl; i++)
            {
                if (i == (cl - 1))//最后一列，加n
                {
                    ls_item += "'" + row[i].ToString().Trim() + "\n";
                }
                else if (i > 1)
                {
                    ls_item += "'" + row[i].ToString().Trim() + "\t";
                }
                else
                {
                    ls_item += row[i].ToString().Trim() + "\t";
                }
            }
            resp.Write(ls_item);
            ls_item = "";
        }
        resp.End();
    }

    /// <summary>
    /// 加载列表
    /// </summary>
    /// <param name="shopId"></param>
    private void loadData(int str, int end)
    {
        AccountDetailsQueryRequestEntity entity = new AccountDetailsQueryRequestEntity();
        entity.BeginDate = Common.ToDateTime(txtOperateBeginTime.Text);
        entity.EndDate = Common.ToDateTime(txtOperateEndTime.Text);
        entity.SubAccount = txtAccount.Text.Trim();

        ResultSet<AccountDetailsQueryResponseEntity> list = AccountDetailsQuery(Guid.NewGuid().ToString(), entity);

        gdList.DataSource = null;

        if (list.Result != null && list.Result.TotalCount > 0)
        {
            int tableCount = list.Result.TotalCount;
            AspNetPager1.RecordCount = tableCount;
            gdList.DataSource = list.Result.AccountDetailsQueryList;
        }
        else
        {
            AspNetPager1.RecordCount = 0;
        }
        
        gdList.DataBind();
    }

    /// <summary>
    /// 帐户明细查询
    /// </summary>
    /// <param name="packetId"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    public ResultSet<AccountDetailsQueryResponseEntity> AccountDetailsQuery(string packetId, AccountDetailsQueryRequestEntity entity)
    {
        if (string.IsNullOrEmpty(packetId) || entity == null)
            return new ResultSet<AccountDetailsQueryResponseEntity>() { State = 1, Message = "参数错误" };

        DateTime bespeakDate = DateTime.Now;
        StringBuilder signXml = new StringBuilder();
        entity.QueryNumber = AspNetPager1.PageSize;
        entity.BeginNumber = (AspNetPager1.CurrentPageIndex - 1) * AspNetPager1.PageSize + 1;
        SystemConfigCacheLogic sccl = new SystemConfigCacheLogic();
        entity.AcctNo = sccl.GetSystemConfig("payAcctNo", "95230154800001743");
        signXml.Append("<body>");
        signXml.AppendFormat("<acctNo>{0}</acctNo>", entity.AcctNo);
        signXml.AppendFormat("<beginDate>{0}</beginDate>", entity.BeginDate.ToString("yyyyMMdd HHmmss"));
        signXml.AppendFormat("<endDate>{0}</endDate>", entity.EndDate.ToString("yyyyMMdd HHmmss"));
        signXml.AppendFormat("<queryNumber>{0}</queryNumber>", entity.QueryNumber);
        signXml.AppendFormat("<beginNumber>{0}</beginNumber>", entity.BeginNumber);
        signXml.AppendFormat("<subAccount>{0}</subAccount>", entity.SubAccount);
        signXml.AppendFormat("<subAcctName>{0}</subAcctName>", entity.SubAcctName);
        signXml.Append("</body>");

        Tuple<int, string> clearValue = RequestSPDBInterface(packetId, bespeakDate, signXml.ToString(), "8924");
        if (clearValue.Item1 != 0)
            return new ResultSet<AccountDetailsQueryResponseEntity>() { State = clearValue.Item1, Message = clearValue.Item2 };

        //解析返回值
        XElement xBody = XDocument.Parse(clearValue.Item2).Element("body");
        IList<XElement> valXmlList = xBody.Element("lists").Elements("list").ToList();
        AccountDetailsQueryResponseEntity aEntity = new AccountDetailsQueryResponseEntity();
        aEntity.TotalCount = int.Parse(xBody.Element("totalCount").Value);
        aEntity.AcctNo = xBody.Element("acctNo").Value;
        aEntity.AcctName = xBody.Element("acctName").Value;
        aEntity.Currency = xBody.Element("currency").Value;

        aEntity.AccountDetailsQueryList = new List<AccountDetailsQueryItemEntity>();

        foreach (var item in valXmlList)
        {
            aEntity.AccountDetailsQueryList.Add(new AccountDetailsQueryItemEntity()
            {
                VoucherNo = item.Element("voucherNo").Value,
                SeqNo = item.Element("seqNo").Value,
                TxAmount = decimal.Parse(item.Element("txAmount").Value),
                Balance = decimal.Parse(item.Element("balance").Value),
                TranFlag = int.Parse(item.Element("tranFlag").Value),
                TransDate = item.Element("transDate").Value,
                TransTime = item.Element("transTime").Value,
                Note = item.Element("note").Value,
                Remark = item.Element("remark").Value,
                PayeeBankNo = item.Element("payeeBankNo").Value,
                PayeeBankName = item.Element("payeeBankName").Value,
                PayeeAcctNo = item.Element("payeeAcctNo").Value,
                PayeeName = item.Element("payeeName").Value,
                TransCode = item.Element("transCode").Value,
                BranchId = item.Element("branchId").Value,
                CustomerAcctNo = item.Element("customerAcctNo").Value,
                PayeeAcctType = item.Element("payeeAcctType").Value,
                TransCounter = item.Element("transCounter").Value,
                AuthCounter = item.Element("authCounter").Value,
                OtherChar10 = item.Element("otherChar10").Value,
                OtherChar40 = item.Element("otherChar40").Value,
                SeqNum = item.Element("seqNum").Value,
                RevFlag = item.Element("revFlag").Value
            });
        }

        return new ResultSet<AccountDetailsQueryResponseEntity>()
        {
            State = clearValue.Item1,
            Message = clearValue.Item2,
            Result = aEntity
        };
    }

    /// <summary>
    /// 请求浦发接口
    /// </summary>
    /// <param name="packetId">本地系统id</param>
    /// <param name="bespeakDate">请求时间</param>
    /// <param name="value">请求内容</param>
    /// <param name="transCode">交易码</param>
    /// <returns></returns>
    private Tuple<int, string> RequestSPDBInterface(string packetId, DateTime bespeakDate, string value, string transCode)
    {
        //签名
        Tuple<int, string> signValue = Signature(value);
        if (signValue.Item1 != 0)
            return signValue;

        //发送转帐请求
        string httpXml = GetHttpXml(packetId, transCode, signValue.Item2, bespeakDate);
        Tuple<int, string> httpValue = SendOperationRequest(httpXml);
        if (httpValue.Item1 != 0)
            return httpValue;

        //验签
        return VerificationSignature(httpValue.Item2);
    }

    /// <summary>
    /// 验证签名
    /// </summary>
    /// <param name="value">待验内容</param>
    private Tuple<int, string> VerificationSignature(string value)
    {
        string responseVal = GetWebContent(string.Format("http://{0}:{1}", serverAddress, signPort), value, "INFOSEC_VERIFY_SIGN/1.0");

        ILog log = LogManager.GetLogger("RemittanceProgram");
        log.Warn(string.Format("验签结果{0}:", responseVal));

        Match reg = new Regex("<result>(?<result>[^<]+)</result>").Match(responseVal);
        int result = int.Parse(reg.Groups["result"].Value);
        if (result != 0)
        {
            reg = new Regex("<BODY>(?<BODY>[\\s\\S]+)</BODY>").Match(responseVal);
            return new Tuple<int, string>(result, reg.Groups["BODY"].Value);
        }
        reg = new Regex("<sic>(?<sic>.+)</sic>").Match(responseVal);
        return new Tuple<int, string>(result, reg.Groups["sic"].Value);
    }

    /// <summary>
    /// 取数据
    /// </summary>
    /// <param name="url">url地址</param>
    /// <param name="value">内容</param>
    /// <param name="contentType"></param>
    /// <returns></returns>
    private string GetWebContent(string url, string value, string contentType)
    {
        string strResult = null;
        Encoding encoding = Encoding.GetEncoding("GBK");
        byte[] byteArray = encoding.GetBytes(value);
        HttpWebRequest request = null;
        HttpWebResponse response = null;
        try
        {
            request = (HttpWebRequest)WebRequest.Create(url);//声明一个HttpWebRequest请求 
            request.Timeout = 30000;//设置连接超时时间 
            request.Headers.Set("Pragma", "no-cache");
            request.Headers.Set("Accept-Language", "zh-cn");
            request.Accept = "image/gif,image/x-xbitmap,image/jpeg,image/pjpeg,application/vnd.ms-powerpoint,application/vnd.ms-excel,application/msword,*/*";
            request.ContentType = contentType;
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.0)";
            request.Method = "POST";
            request.ContentLength = byteArray.Length;
            Stream stream = request.GetRequestStream();
            stream.Write(byteArray, 0, byteArray.Length);
            stream.Close();
            response = (HttpWebResponse)request.GetResponse();
            Stream streamReceive = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(streamReceive, encoding);
            strResult = streamReader.ReadToEnd();
        }
        finally
        {
            if (response != null)
            {
                response.Close();
            }
        }
        return strResult;
    }

    /// <summary>
    /// 返回Http的xml
    /// </summary>
    /// <param name="packetId">本地数据id</param>
    /// <param name="transCode">交易码</param>
    /// <param name="sign">签名</param>
    /// <param name="date">时间</param>
    /// <returns></returns>
    private string GetHttpXml(string packetId, string transCode, string sign, DateTime date)
    {
        return string.Format("<?xml version=\"1.0\" encoding=\"GB2312\"?><packet>{0}<body><signature>{1}</signature></body></packet>", GetHeadXml(packetId, transCode, date), sign);
    }

    /// <summary>
    /// 取head头
    /// </summary>
    /// <param name="packetId">本地数据id</param>
    /// <param name="transCode">交易码</param>
    /// <param name="date">请求时间</param>
    /// <returns></returns>
    private string GetHeadXml(string packetId, string transCode, DateTime date)
    {
        return string.Format("<head><transCode>{0}</transCode><signFlag>{1}</signFlag><packetID>{2}</packetID><masterID>{3}</masterID><timeStamp>{4}</timeStamp></head>", transCode, 1, packetId, masterID, date.ToString("yyyy-MM-dd HH:mm:ss"));
    }

    /// <summary>
    /// 发送操作请求
    /// </summary>
    /// <param name="value">发送内容</param>
    private Tuple<int, string> SendOperationRequest(string value)
    {
        //加请求长度
        Encoding encoding = Encoding.GetEncoding("GBK");
        byte[] byteArray = encoding.GetBytes(value);

        string length = (byteArray.Length + 6).ToString();
        if (length.Length < 6)
            length = new string('0', (6 - length.Length)) + length;
        value = length + value;

        string responseVal = GetWebContent(string.Format("http://{0}:{1}", serverAddress, httpPort), value, "text/plain");
        if (string.IsNullOrEmpty(responseVal))
            return new Tuple<int, string>(1, null);
        //解析
        responseVal = responseVal.Substring(6);

        ILog log = LogManager.GetLogger("RemittanceProgram");
        log.Warn(string.Format("请求包返回{0}:", responseVal));

        XDocument responseXml = XDocument.Parse(responseVal);
        string returnCode = responseXml.Element("packet").Element("head").Element("returnCode").Value;
        XElement returnBody = responseXml.Element("packet").Element("body");
        if (returnCode != "AAAAAAA")
            return new Tuple<int, string>(1, string.Format("错误码{0},错误值{1}", returnCode, returnBody.Element("returnMsg").Value));
        return new Tuple<int, string>(0, returnBody.Element("signature").Value);
    }

    /// <summary>
    /// 签名
    /// </summary>
    /// <param name="value">签名内容</param>
    private Tuple<int, string> Signature(string value)
    {
        string responseVal = GetWebContent(string.Format("http://{0}:{1}", serverAddress, signPort), value, "INFOSEC_SIGN/1.0");
        if (string.IsNullOrEmpty(responseVal))
            return new Tuple<int, string>(1, null);
        //解析
        Match regResult = new Regex("<result>(?<result>[^<]+)</result>").Match(responseVal);
        int result = int.Parse(regResult.Groups["result"].Value);
        if (result != 0)
            return new Tuple<int, string>(result, null);
        Match regSign = new Regex("<sign>(?<sig>[^<]+)</sign>").Match(responseVal);
        return new Tuple<int, string>(result, regSign.Groups["sig"].Value);
    }
    protected void gdList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int i = e.Row.RowIndex;
        if (i > -1)
        {
            if (gdList.DataKeys[i].Values["TranFlag"].ToString().Equals("0"))
            {
                e.Row.Cells[5].Text = string.Empty;
            }
            else
            {
                e.Row.Cells[4].Text = string.Empty;
            }
            
            string dateStr = gdList.DataKeys[i].Values["TransDate"].ToString() + gdList.DataKeys[i].Values["TransTime"].ToString();

            while (dateStr.Length < 14)
            {
                dateStr = dateStr.Substring(0, 8) + "0" + dateStr.Substring(8, dateStr.Length - 8);
            }

            e.Row.Cells[1].Text = dateStr.Substring(0, 4) + "-" + dateStr.Substring(4, 2) + "-" + dateStr.Substring(6, 2) + " " + dateStr.Substring(8, 2) + ":" + dateStr.Substring(10, 2) + ":" + dateStr.Substring(12, 2);
        }
    }
}