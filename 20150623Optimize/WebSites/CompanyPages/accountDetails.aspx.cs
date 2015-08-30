using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
using System.Collections;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;
using System.Text;
using VAGastronomistMobileApp.SQLServerDAL;
using Web.Control.Enum;

public partial class CompanyPages_accountDetails : System.Web.UI.Page
{
    public static string allcount;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //GetAccountDetail(10, 1, "1970-01-01", "2013-11-01", 89, 67, 1, 0, 300, "");
        }

    }
    /// <summary>
    /// 账户明细
    /// </summary>
    /// <param name="PageSize"></param>
    /// <param name="PageIndex"></param>
    /// <param name="strWhere"></param>
    /// <param name="datetimestart"></param>
    /// <param name="datetimeend"></param>
    /// <param name="mobilePhoneNumber"></param>
    /// <returns></returns>

    #region MyRegion
    //public static string GetAccountDetail(int PageSize, int PageIndex, string datetimestart, string datetimeend, int shopId, int companyId)
    //{
    //    //处理wangyh 脏数据
    //    if (datetimestart == "" || datetimestart == "NaN" || datetimestart == null)
    //    {
    //        datetimestart = "1970-01-01";
    //    }
    //    if (datetimeend == "" || datetimeend == "NaN" || datetimestart == null)
    //    {
    //        datetimeend = "1970-01-01";
    //    }
    //    datetimestart = datetimestart + " 00:00:00";
    //    datetimeend = datetimeend + " 23:59:59";

    //    PaginationPager pg = new PaginationPager();
    //    pg.tblName = "(select * from (select PreOrder19dian.preorder19dianId,tmp.queryTime as Btime,[prePaidSum] as pay from PreOrder19dian,(select MAX(queryTime) as queryTime,preorder19dianId,shopId,isShopVerified  from PreOrderQueryInfo where PreOrderQueryInfo.isShopVerified =1 group by preorder19dianId,shopId,isShopVerified)tmp where  PreOrder19dian.companyId='" + companyId + "' and PreOrder19dian.shopId=" + shopId + " and isPaid='1' and isApproved ='1' and tmp.preorder19dianId=PreOrder19dian.preOrder19dianId)ABC union select type as preorder19dianId,Btime,-pay from BusinessPay where companyId='" + companyId + "' and shopId='" + shopId + "')AB";
    //    pg.PageSize = PageSize;
    //    pg.PageIndex = PageIndex;
    //    pg.strWhere = "1=1";
    //    pg.strGetFields = "preorder19dianId,Btime,pay,(select sum(pay) as payd from(select * from (select PreOrder19dian.preorder19dianId,tmp.queryTime as Btime,[prePaidSum] as pay from PreOrder19dian,(select MAX(queryTime) as queryTime,preorder19dianId,shopId,isShopVerified  from PreOrderQueryInfo where PreOrderQueryInfo.isShopVerified =1 group by preorder19dianId,shopId,isShopVerified)tmp  where  PreOrder19dian.companyId='" + companyId + "' and PreOrder19dian.shopId='" + shopId + "' and isPaid='1' and isApproved ='1' and tmp.preorder19dianId=PreOrder19dian.preOrder19dianId)ABC union select type as preorder19dianId,Btime,-pay from BusinessPay where companyId='" + companyId + "' and shopId='" + shopId + "')as D1 where D1.Btime<=AB.Btime) as payd";
    //    pg.OrderType = 0;
    //    pg.OrderfldName = "Btime";
    //    if (datetimestart != "" && datetimeend != "")
    //    {
    //        try
    //        {
    //            // DateTime timestart = Common.ToDateTime(datetimestart);
    //            // DateTime timeend = Common.ToDateTime(datetimeend);
    //            pg.strWhere += " and " + " (Btime >='" + datetimestart + "'and Btime<='" + datetimeend + "') ";
    //        }
    //        catch
    //        {
    //            pg.strWhere += " and " + " (Btime >='" + "1970-01-01 00:00:00" + "'and Btime<='" + DateTime.Now + "') ";

    //        }
    //    }

    //    DataTable dtaccount = Common.DbPager(pg);

    //    dtaccount.Columns.Add("type");
    //    for (int i = 0; i < dtaccount.Rows.Count; i++)
    //    {
    //        if (dtaccount.Rows[i]["preorder19dianId"].ToString() == "-1")
    //        {
    //            dtaccount.Rows[i]["type"] = "2";//提款

    //        }
    //        else
    //        {
    //            dtaccount.Rows[i]["type"] = "1";//营业收入

    //        }

    //        if (dtaccount.Rows[i]["pay"] == DBNull.Value || dtaccount.Rows[i]["pay"] == null || dtaccount.Rows[i]["pay"].ToString() == "")
    //        {
    //            dtaccount.Rows[i]["pay"] = 0;
    //        }
    //        if (dtaccount.Rows[i]["payd"] == DBNull.Value || dtaccount.Rows[i]["payd"] == null || dtaccount.Rows[i]["payd"].ToString() == "")
    //        {
    //            dtaccount.Rows[i]["payd"] = 0;
    //        }
    //    }
    //    AccountOperate account = new AccountOperate();
    //    double totalAmount = account.GetCount(datetimestart, datetimeend, companyId, shopId);

    //    string json = Common.ConvertDateTableToJson(dtaccount);
    //    int ocount = pg.doCount;
    //    if (json != "" && json != null)
    //    {
    //        json = json.TrimEnd('}');
    //    }
    //    else
    //    {
    //        return "";
    //    }
    //    json += ",\"total\":[{\"totalAmount\":" + totalAmount + "},{\"ocount\":" + ocount + "}]}";
    //    return json;
    //} 
    #endregion
    //测试GetAccountDetail(10,1,"1970-01-01","2013-11-01",89,67,0,0,300,""); 通过!
    /// <summary>
    /// 
    /// </summary>
    /// <param name="PageSize"></param>
    /// <param name="PageIndex"></param>
    /// <param name="datetimestart"></param>
    /// <param name="datetimeend"></param>
    /// <param name="shopId"></param>
    /// <param name="companyId"></param>
    /// <param name="type">交易分类：0:所有交易，1：:营业收入，2：提款，3：佣金</param>
    /// <param name="paystart">金额范围，开始</param>
    /// <param name="payend">金额范围，结束</param>
    /// <param name="mainkey">关键字</param>
    /// <returns></returns>
    [WebMethod]
    public static string GetAccountDetail(int PageSize, int PageIndex, string datetimestart, string datetimeend, int shopId, int companyId, int type, double paystart, double payend, string mainkey, int couponType, string mobilePhoneNumber = "")
    {
        if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
        {
            return "-1000";
        }
        mainkey = mainkey.Trim();
        if (mainkey == "undefined")
        {
            mainkey = string.Empty;
        }
        #region 处理脏数据
        ////处理脏数据
        //if (datetimestart == "" || datetimestart == "NaN" || datetimestart == null)
        //{
        //    datetimestart = "1970-01-01";
        //}
        //if (datetimeend == "" || datetimeend == "NaN" || datetimestart == null)
        //{
        //    datetimeend = "1970-01-01";
        //}
        //datetimestart = datetimestart + " 00:00:00";
        //datetimeend = datetimeend + " 23:59:59";
        //if (double.IsNaN(paystart))
        //{
        //    paystart = 0;
        //}
        //if (double.IsNaN(payend))
        //{
        //    payend = 0;
        //}
        //paystart = Common.ToDouble(paystart);
        //payend = Common.ToDouble(payend);
        //StringBuilder strSqlField = new StringBuilder();
        //strSqlField.Append("(SELECT [accountTypeConnId] as preOrderId,[operTime],[accountType],[accountMoney],[remainMoney],DATEDIFF(SECOND,operTime,GETDATE()) dt FROM [MoneyMerchantAccountDetail] ");
        //strSqlField.AppendFormat("where shopId={0})ttt", shopId);

        //string strWhere = "";
        //if (datetimestart != "" && datetimeend != "")
        //{
        //    try
        //    {
        //        strWhere += " (operTime >='" + datetimestart + "'and operTime<='" + datetimeend + "') ";
        //    }
        //    catch
        //    {
        //        strWhere += " (operTime >='" + "1970-01-01 00:00:00" + "'and operTime<='" + DateTime.Now + "') ";
        //    }
        //}
        //DataTable dt = Common.GetDataTableFieldValueOrderby(strSqlField.ToString(), "*", strWhere, "dt,preOrderId,accountType%10 desc");
        //string selectString = "1=1 ";

        ////金额
        //if (paystart != int.MinValue)
        //    selectString += " and accountMoney>=" + paystart;
        //if (payend != int.MaxValue)
        //    selectString += " and accountMoney<=" + payend;

        //DataRow[] dtRow;
        //switch (type)
        //{
        //    case 13: selectString += " and (accountType = 13)"; break;
        //    case 4: selectString += " and (accountType = 4)"; break;
        //    case 5: selectString += " and (accountType = 5)"; break;
        //    case 6: selectString += " and (accountType = 6)"; break;
        //}
        //if (mainkey != "")
        //{
        //    selectString += " and Convert(preOrderId, System.String) = '" + mainkey + "'";
        //}

        //dtRow = dt.Select(selectString);
        //DataTable dtNew = dt.Clone();
        //for (int n = 0; n < dtRow.Length; n++)
        //{
        //    dtNew.ImportRow(dtRow[n]);
        //}
        //DataTable dt_page = Common.GetPageDataTable(dtNew, (PageIndex - 1) * PageSize, PageIndex * PageSize);

        //dt_page.Columns.Add("type");
        //for (int i = 0; i < dt_page.Rows.Count; i++)
        //{
        //    if (dt_page.Rows[i]["accountType"].ToString() == "13")
        //    {
        //        dt_page.Rows[i]["type"] = "友络佣金";//友络佣金
        //    }
        //    else if (dt_page.Rows[i]["accountType"].ToString() == "4")
        //    {
        //        dt_page.Rows[i]["type"] = "点单退款";//点单退款
        //    }
        //    else if (dt_page.Rows[i]["accountType"].ToString() == "5")
        //    {
        //        dt_page.Rows[i]["type"] = "点单收入";//点单收入
        //    }
        //    else if (dt_page.Rows[i]["accountType"].ToString() == "6")
        //    {
        //        dt_page.Rows[i]["type"] = "结账扣款";//结账扣款
        //    }
        //    if (dt_page.Rows[i]["accountMoney"] == DBNull.Value || dt_page.Rows[i]["accountMoney"] == null || dt_page.Rows[i]["accountMoney"].ToString() == "")
        //    {
        //        dt_page.Rows[i]["accountMoney"] = 0;
        //    }
        //    if (dt_page.Rows[i]["remainMoney"] == DBNull.Value || dt_page.Rows[i]["remainMoney"] == null || dt_page.Rows[i]["remainMoney"].ToString() == "")
        //    {
        //        dt_page.Rows[i]["remainMoney"] = 0;
        //    }
        //}
        //string json = Common.ConvertDateTableToJson(dt_page);
        //int ocount = dtNew.Rows.Count;

        //SybMoneyMerchantOperate sybMoneyMerchantOperate = new SybMoneyMerchantOperate();
        //double totalAmount = sybMoneyMerchantOperate.GetConditionSum(datetimestart, datetimeend, shopId, companyId, type, paystart, payend, mainkey);
        //if (json != "" && json != null)
        //{
        //    json = json.TrimEnd('}');
        //}
        //else
        //{
        //    return "";
        //}
        //json += ",\"total\":[{\"totalAmount\":" + totalAmount + "},{\"ocount\":" + ocount + "}]}";
        //return json;
        #endregion

        #region 条件部分
        string strWhere = "shopId=" + shopId;
        //时间
        if (datetimestart != "" && Common.IsTime(datetimestart))
            strWhere += string.Format(@" and [operTime] >='{0}'", datetimestart);
        if (datetimeend != "" && Common.IsTime(datetimeend))
            strWhere += string.Format(@" and [operTime] <='{0}'", datetimeend + " 23:59:59");
        //类型
        if (type > 0)
            strWhere += " and accountType=" + type;
        //金额
        if (payend != int.MinValue)
            strWhere += " and accountMoney>=" + paystart;
        if (payend != int.MaxValue)
            strWhere += " and accountMoney<=" + payend;
        //流水号
        if (mainkey != "")
            strWhere += " and accountTypeConnId='" + mainkey + "'";

        if (!string.IsNullOrWhiteSpace(mobilePhoneNumber))
            strWhere += " and mobilePhoneNumber='" + mobilePhoneNumber + "'";

        if (couponType != 0)
        {
            strWhere += " and CouponType='" + couponType + "'";
        }
        #endregion

        PageQuery pageQuery = new PageQuery()
        {
            tableName = "[dbo].[View_MoneyMerchantAccountDetail12]",
            fields = "[OrderId],[accountTypeConnId],[operTime],[accountType],[accountMoney],[remainMoney],[mobilePhoneNumber]",
            //fields = "[OrderId],[accountId],[accountTypeConnId],[operTime],[accountType],[accountMoney],[remainMoney],[mobilePhoneNumber]",
            orderField = "[operTime] desc,case accountType when 5 then 1 when 13 then 2 when 4 then 3 end desc",
            sqlWhere = strWhere
        };
        Paging paging = new Paging()
        {
            pageIndex = PageIndex,
            pageSize = PageSize,
            recordCount = 0,
            pageCount = 0
        };
        MoneyMerchantAccountDetailListResponse<MoneyMerchantAccountDetailResponse> data = new MoneyMerchantAccountDetailListResponse<MoneyMerchantAccountDetailResponse>();
        List<MoneyMerchantAccountDetailResponse> list = CommonManager.GetPageData<MoneyMerchantAccountDetailResponse>(SqlHelper.ConnectionStringLocalTransaction, pageQuery, paging);
        
        string preOrder19dianID=string.Empty;
        string Refund = string.Empty;
        foreach (MoneyMerchantAccountDetailResponse d in list)
        {
            d.accountTypeName = EnumHelper.GetEnumName<AccountType>(d.accountType);
            d.accountMoney = Common.ToDouble(d.accountMoney);
            d.remainMoney = Common.ToDouble(d.remainMoney);
            preOrder19dianID += d.accountTypeConnId + ",";
            if (d.accountType == (int)AccountType.ORDER_OUTCOME)
            {
                Refund += d.accountTypeConnId + ",";
            }
        }

        SybMoneyMerchantOperate smo = new SybMoneyMerchantOperate();
        //if (preOrder19dianID.Length > 0)
        //{
        //    preOrder19dianID = preOrder19dianID.Substring(0, preOrder19dianID.Length - 1);
        //    DataTable dtOrder = smo.getAccountDetailByOrderID(preOrder19dianID, shopId);

        //    foreach (MoneyMerchantAccountDetailResponse d in list)
        //    {
        //        for (int i = 0; i < dtOrder.Rows.Count; i++)
        //        {
        //            if (d.accountType == Common.ToInt32(dtOrder.Rows[i]["accountType"]) && d.OrderId.ToString() == dtOrder.Rows[i]["OrderId"].ToString())
        //            {
        //                d.accountMoney = Common.ToDouble(dtOrder.Rows[i]["accountMoney"]);
        //                d.remainMoney = Common.ToDouble(dtOrder.Rows[i]["remainMoney"]);
        //            }
        //        }
        //    }
        //}

           

        if (Refund.Length > 0)
        {
            Refund = Refund.Substring(0, Refund.Length - 1);
            DataTable dtOrder = smo.getAccountDetailByAccountType(Refund);
            foreach (MoneyMerchantAccountDetailResponse d in list)
            {
                for (int i = 0; i < dtOrder.Rows.Count; i++)
                {
                    if (d.accountType != (int)AccountType.MERCHANT_CHECKOUT && d.OrderId == (Guid)dtOrder.Rows[i]["OrderId"])
                    {
                        d.accountTypeConnId = dtOrder.Rows[i]["preOrder19dianId"].ToString();
                    }
                }
            }
        }

        data.list = list;
        data.page = new PageNav() { pageIndex = PageIndex, pageSize = PageSize, totalCount = paging.recordCount };
        //data.totalMoney = MoneyMerchantAccountDetailManager.GetaccountMoneySum(strWhere);

        return SysJson.JsonSerializer(data);
    }

    [WebMethod]
    public static string GetAccountTotal(int PageSize, int PageIndex, string datetimestart, string datetimeend,
        int shopId, int companyId, int couponType)
    {
        string strWhere = "shopId=" + shopId;
        //时间
        if (datetimestart != "" && Common.IsTime(datetimestart))
            strWhere += string.Format(@" and [date] >='{0}'", datetimestart);
        if (datetimeend != "" && Common.IsTime(datetimeend))
            strWhere += string.Format(@" and [date] <='{0}'", datetimeend + " 23:59:59");

        string tabName = string.Empty;
        if (couponType == 0)
        {
            tabName = "[dbo].[View_MoneyMerchantAccountDetail33]";
        }
        else
        {
            tabName = "[dbo].[View_MoneyMerchantAccountDetail34]";
            strWhere += string.Format(@" and CouponType ={0}", couponType);
        }

        PageQuery pageQuery = new PageQuery()
        {
            tableName = tabName,
            fields = "[date],[total],[count]",
            orderField = "[date] desc",
            sqlWhere = strWhere
        };
        Paging paging = new Paging()
        {
            pageIndex = PageIndex,
            pageSize = PageSize,
            recordCount = 0,
            pageCount = 0
        };

        MoneyMerchantAccountDetailListResponse<MoneyMerchantAccountSumResponse> data = new MoneyMerchantAccountDetailListResponse<MoneyMerchantAccountSumResponse>();
        List<MoneyMerchantAccountSumResponse> list = CommonManager.GetPageData<MoneyMerchantAccountSumResponse>(SqlHelper.ConnectionStringLocalTransaction, pageQuery, paging);
        //foreach (MoneyMerchantAccountDetailResponse d in list)
        //{
        //    d.accountTypeName = CommonManager.GetEnumName<AccountType>(d.accountType);
        //}

        data.list = list;
        data.page = new PageNav() { pageIndex = PageIndex, pageSize = PageSize, totalCount = paging.recordCount };
        data.totalMoney = MoneyMerchantAccountDetailManager.GetaccountMoneySum(strWhere);
        data.orderCount = MoneyMerchantAccountDetailManager.GetOrderCount(strWhere, couponType);
        return SysJson.JsonSerializer(data);

    }

    /// <summary>
    /// 计算累计收入和余额
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string GetAllcount(int companyId, int shopId)
    {
        if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
        {
            return "-1000";
        }
        string datetimeend = DateTime.Now.ToString();
        SybMoneyMerchantOperate sybMoneyMerchantOperate = new SybMoneyMerchantOperate();
        string jsonCount = Common.GetJSON<string>(Common.ToString(sybMoneyMerchantOperate.GetSumForTime("1970-01-01 00:00:00", datetimeend, shopId, companyId)));//累计收入
        string jsonBhaved = Common.GetJSON<string>(Common.ToString(sybMoneyMerchantOperate.GetCleanSumForTime("1970-01-01 00:00:00", datetimeend, shopId, companyId)));//余额

        string jsonAll = "{\"income\":[{\"income\":" + jsonCount + "}],\"bhave\":[{\"bhave\":" + jsonBhaved + "}]}";
        return jsonAll;

    }

    /// <summary>
    /// 开始时间
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string GetTime()
    {
        string startTime = Common.GetFieldValue("VASystemConfig", "configContent", "configName='PreOrder19dianTime'");
        return startTime;
    }
    /// <summary>
    /// 返回默认最大金额
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static int GetPrice()
    {
        string endPrice = Common.GetFieldValue("VASystemConfig", "configContent", "configName='accountPriceMax'");
        return Common.ToInt32(endPrice);
    }


    /// <summary>
    /// 需要导出的数据
    /// </summary>
    /// <param name="datetimestart"></param>
    /// <param name="datetimeend"></param>
    /// <param name="shopId"></param>
    /// <param name="companyId"></param>
    /// <param name="type"></param>
    /// <param name="paystart"></param>
    /// <param name="payend"></param>
    /// <param name="mainkey"></param>
    /// <returns></returns>
    public static DataTable GetAllData(string datetimestart, string datetimeend, int shopId, int companyId, int type, double paystart, double payend, string mainkey)
    {
        //处理wangyh 脏数据
        if (datetimestart == "" || datetimestart == "NaN" || datetimestart == null)
        {
            datetimestart = "1970-01-01";
        }
        if (datetimeend == "" || datetimeend == "NaN" || datetimestart == null)
        {
            datetimeend = "1970-01-01";
        }
        datetimestart = datetimestart + " 00:00:00";
        datetimeend = datetimeend + " 23:59:59";
        StringBuilder strSqlField = new StringBuilder();
        strSqlField.Append("(SELECT [flowNumber],[operTime],[accountType],[accountMoney],[remainMoney],[remark] FROM [MoneyMerchantAccountDetail14] ");
        strSqlField.AppendFormat("where shopId={0})ttt", shopId);

        string strWhere = "";
        if (datetimestart != "" && datetimeend != "")
        {
            try
            {
                strWhere += " (operTime >='" + datetimestart + "'and operTime<='" + datetimeend + "') ";
            }
            catch
            {
                strWhere += " (operTime >='" + "1970-01-01 00:00:00" + "'and operTime<='" + DateTime.Now + "') ";
            }
        }
        DataTable dt = Common.GetDataTableFieldValueOrderby(strSqlField.ToString(), "*", strWhere, "operTime");
        string selectString = "1=1 ";
        if (paystart != -1)
        {
            selectString += " and  accountMoney >=" + paystart;
        }
        if (payend != -1)
        {
            selectString += " and accountMoney <=" + payend;
        }

        DataRow[] dtRow;
        switch (type)
        {
            case 1: selectString += " and (accountType = 1)" + " and Convert(flowNumber, System.String) like '%" + mainkey + "%'"; break;
            case 2: selectString += " and (accountType = 2)" + " and Convert(flowNumber, System.String) like '%" + mainkey + "%'"; break;
            case 3: selectString += " and (accountType = 3)" + " and Convert(flowNumber, System.String) like '%" + mainkey + "%'"; break;
            case 4: selectString += " and (accountType = 4)" + " and Convert(flowNumber, System.String) like '%" + mainkey + "%'"; break;
            case 5: selectString += " and (accountType = 5)" + " and Convert(flowNumber, System.String) like '%" + mainkey + "%'"; break;
            case 6: selectString += " and (accountType = 6)" + " and Convert(flowNumber, System.String) like '%" + mainkey + "%'"; break;
            default: selectString += " and Convert(flowNumber, System.String) like '%" + mainkey + "%'"; break;
        }

        dtRow = dt.Select(selectString, "operTime desc,case accountType when 5 then 1 when 13 then 2 when 4 then 3 end desc");
        DataTable dtNew = dt.Clone();
        return dtNew;
    }
    /// <summary>
    ///  excel导出
    /// </summary>
    /// <param name="datetimestart"></param>
    /// <param name="datetimeend"></param>
    /// <param name="shopId"></param>
    /// <param name="companyId"></param>
    /// <param name="type"></param>
    /// <param name="paystart"></param>
    /// <param name="payend"></param>
    /// <param name="mainkey"></param>
    [WebMethod]
    public static void ExcelExport(string datetimestart, string datetimeend, int shopId, int companyId, int type, double paystart, double payend, string mainkey)
    {
        DataTable dtExcel = GetAllData(datetimestart, datetimeend, shopId, companyId, type, paystart, payend, mainkey);
        CreateExcel(dtExcel);
    }
    /// <summary>
    /// Txt导出
    /// </summary>
    /// <param name="datetimestart"></param>
    /// <param name="datetimeend"></param>
    /// <param name="shopId"></param>
    /// <param name="companyId"></param>
    /// <param name="type"></param>
    /// <param name="paystart"></param>
    /// <param name="payend"></param>
    /// <param name="mainkey"></param>
    public static void TextExport(string datetimestart, string datetimeend, int shopId, int companyId, int type, double paystart, double payend, string mainkey)
    {
        DataTable dtText = GetAllData(datetimestart, datetimeend, shopId, companyId, type, paystart, payend, mainkey);
    }

    /// <summary>
    /// dt导出txt
    /// </summary>
    /// <param name="dt"></param>
    private static void CreateText(DataTable dt)
    {
        string textName = "accountDetail_" + DateTime.Now.ToString("yyyy/mm/dd_hh:mm:ss");
        HttpResponse resp = System.Web.HttpContext.Current.Response;
        resp.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        resp.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlPathEncode(textName));
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
                    ls_item += row[i].ToString().Trim() + "\n";
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
    /// 由DataTable导出Excel
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="fileName"></param>
    private static void CreateExcel(DataTable dt)
    {
        string excelName = "accountDetail_" + DateTime.Now.ToString("yyyy/mm/dd_hh:mm:ss");
        HttpResponse resp;
        resp = System.Web.HttpContext.Current.Response;
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
                    ls_item += row[i].ToString().Trim() + "\n";
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
}