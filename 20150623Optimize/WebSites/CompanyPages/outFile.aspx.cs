using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;

public partial class CompanyPages_outFile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //string datetimestart, string datetimeend, int shopId, int companyId, int type, double paystart, double payend, string mainkey
        string datetimestart = Request["datetimestart"];
        string datetimeend = Request["datetimeend"];
        int shopId = Common.ToInt32(Request["shopId"]);
        int companyId = Common.ToInt32(Request["companyId"]);
        int type = Common.ToInt32(Request["type"]);
        double paystart = Common.ToDouble(Request["paystart"]);
        double payend = Common.ToDouble(Request["payend"]);
        string mainkey = Common.ToString(Request["mainkey"]);
        int outType = Common.ToInt32(Request["outType"]);
        string mobilePhoneNumber = Common.ToString(Request["mobilePhoneNumber"]);

        HttpResponse response = System.Web.HttpContext.Current.Response;
        response.Buffer = true;
        response.ClearContent();
        response.ClearHeaders();
        response.Charset = "GB2312";
        response.ContentEncoding = Encoding.GetEncoding("GB2312");

        DataTable dt = GetAllData(datetimestart, datetimeend, shopId, companyId, type, paystart, payend, mainkey, mobilePhoneNumber);
        if (outType == 1)
        {
            string excelName = "AccountDetail_" + DateTime.Now.ToString("yyyy/mm/dd_hh:mm:ss");
            response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。 
            response.AppendHeader("Content-Disposition", "attachment;filename=" + excelName + ".xls");

        }
        else
        {
            string textName = "AccountDetail_" + DateTime.Now.ToString("yyyy/mm/dd_hh:mm:ss");
            response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlPathEncode(textName) + ".txt");
        }
        StringBuilder strContextBuilder = new StringBuilder();
        strContextBuilder.AppendLine("流水号\t手机号码\t时间\t类型\t金额\t余额");
        DataRow[] myRow = dt.Select();
        int cl = dt.Columns.Count;
        foreach (DataRow row in myRow)
        {
            for (int i = 0; i < cl; i++)
            {
                if (i == (cl - 1))//最后一列，加n
                {
                    //ls_item += row[i].ToString().Trim() + "\n";
                    strContextBuilder.AppendFormat("{0}", row[i].ToString().Trim());
                    strContextBuilder.AppendLine();
                }
                else
                {
                    //ls_item += row[i].ToString().Trim() + "\t";

                    if (outType != 1 && i == 1 && string.IsNullOrWhiteSpace(row[i].ToString()))
                    {
                        strContextBuilder.Append("\t\t");
                    }
                    else
                    {
                        strContextBuilder.AppendFormat("{0}\t", row[i].ToString().Trim());
                    }
                }
            }
        }

        response.Write(strContextBuilder.ToString());
        response.End();
        //if (outType == 1)
        //{
        //    string excelName = "accountDetail_" + DateTime.Now.ToString("yyyy/mm/dd_hh:mm:ss");
        //    HttpResponse resp;
        //    resp = System.Web.HttpContext.Current.Response;
        //    resp.Buffer = true;
        //    resp.ClearContent();
        //    resp.ClearHeaders();
        //    resp.Charset = "GB2312";
        //    resp.AppendHeader("Content-Disposition", "attachment;filename=" + excelName + ".xls");
        //    resp.ContentEncoding = System.Text.Encoding.Default;//设置输出流为简体中文   
        //    resp.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。 
        //    string colHeaders = "", ls_item = "";
        //    DataRow[] myRow = dt.Select();
        //    int i = 0;
        //    int cl = dt.Columns.Count;
        //    colHeaders = "预点单号" + "\t" + "手机号\t" + "时间" + "\t" + "金额" + "\t" + "余额" + "\t" + "操作类型" + "\n";
        //    resp.Write(colHeaders);
        //    foreach (DataRow row in myRow)
        //    {
        //        for (i = 0; i < cl; i++)
        //        {
        //            if (i == (cl - 1))//最后一列，加n
        //            {
        //                ls_item += row[i].ToString().Trim() + "\n";
        //            }
        //            //else if (i == cl - 2) { }
        //            //else if (i == 2) { }
        //            else
        //            {
        //                ls_item += row[i].ToString().Trim() + "\t";
        //            }
        //        }
        //        resp.Write(ls_item);
        //        ls_item = "";
        //    }
        //    resp.End();
        //}
        //else
        //{
        //    string textName = "accountDetail_" + DateTime.Now.ToString("yyyy/mm/dd_hh:mm:ss");
        //    HttpResponse resp = System.Web.HttpContext.Current.Response;
        //    resp.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        //    resp.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlPathEncode(textName) + ".txt");
        //    string colHeaders = "", ls_item = "";
        //    DataRow[] myRow = dt.Select();
        //    int i = 0;
        //    int cl = dt.Columns.Count;

        //    colHeaders = "预点单号" + "\t" + "手机号\t" + "时间" + """\t" + "金额" + "\t" + "余额" + "\t" + "操作类型" + "\r\n";
        //    resp.Write(colHeaders);
        //    foreach (DataRow row in myRow)
        //    {
        //        for (i = 0; i < cl; i++)
        //        {
        //            if (i == (cl - 1))//最后一列，加n
        //            {
        //                ls_item += row[i].ToString().Trim() + "\r\n";
        //            }
        //            else
        //            {
        //                if (i == 1 && string.IsNullOrWhiteSpace(row[i].ToString()))
        //                {
        //                    ls_item += "\t\t";
        //                }
        //                else
        //                {
        //                    ls_item += row[i].ToString().Trim() + "\t";
        //                }
        //            }
        //        }
        //        resp.Write(ls_item);
        //        ls_item = "";
        //    }
        //    resp.End();
        //}
    }

    public static DataTable GetAllData(string datetimestart, string datetimeend, int shopId, int companyId, int type, double paystart, double payend, string mainkey, string mobilePhoneNumber)
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
        //流水号\t手机号码\t时间\t类型\t金额\t余额\t详情
        //strSqlField.Append("SELECT [accountTypeConnId] as preOrderId,mobilePhoneNumber,[operTime],[accountType],'' as [type],[accountMoney],[remainMoney] FROM [View_MoneyMerchantAccountDetail] ");
        strSqlField.Append("SELECT accountTypeConnId preOrderId,mobilePhoneNumber,operTime,[accountType],");
        strSqlField.Append(" '' as [type],accountMoney , remainMoney ");
        strSqlField.Append(" FROM [View_MoneyMerchantAccountDetail13] ");
        strSqlField.AppendFormat("where shopId={0}", shopId);

        //string strWhere = "";
        if (datetimestart != "" && datetimeend != "")
        {
            try
            {
                //strWhere += " (operTime >='" + datetimestart + "'and operTime<='" + datetimeend + "') ";
                strSqlField.AppendFormat(" and (operTime >='{0}'and operTime<='{1}') ", datetimestart, datetimeend);
            }
            catch
            {
                //strWhere += " (operTime >='" + "1970-01-01 00:00:00" + "'and operTime<='" + DateTime.Now + "') ";
            }
        }

        //string selectString = "1=1 ";
        if (paystart != -1)
        {
            //selectString += " and  accountMoney >=" + paystart;
            strSqlField.AppendFormat("  and  accountMoney >={0}", paystart);
        }
        if (payend != -1)
        {
            //selectString += " and accountMoney <=" + payend;
            strSqlField.AppendFormat("  and  accountMoney <={0}", payend);
        }


        DataRow[] dtRow;
        switch (type)
        {
            case 13:
            //selectString += " and (accountType = 13)"; 
            //strSqlField.Append(" and accountType = 13");
            //break;
            case 4:
            //selectString += " and (accountType = 4)"; 
            //break;
            case 5:
            //selectString += " and (accountType = 5)"; 
            //break;
            case 6:
                //selectString += " and (accountType = 6)";
                strSqlField.AppendFormat(" and accountType ={0}", type);
                break;
        }
        if (mainkey != "")
        {
            //selectString += " and Convert(preOrderId, System.String) = '" + mainkey + "'";
            strSqlField.AppendFormat(" and accountTypeConnId={0}", mainkey);
        }

        if (mobilePhoneNumber != "")
        {
            //selectString += " and mobilePhoneNumber = '" + mobilePhoneNumber + "'";
            strSqlField.AppendFormat("  and mobilePhoneNumber = '{0}'", mobilePhoneNumber);
        }
        //strSqlField.Append(" order by [accountId] desc");
        strSqlField.Append("order by operTime desc,case accountType when 5 then 1 when 13 then 2 when 4 then 3 end desc");

        DataTable dt = CommonManager.GetDataTableFieldValue(strSqlField.ToString()); //Common.GetDataTableFieldValueOrderby(strSqlField.ToString(), "*", strWhere, "dt,preOrderId,accountType%10 desc");

        dtRow = dt.Select();
        DataTable dtNew = dt.Clone();

        string Refund = string.Empty;

        for (int n = 0; n < dtRow.Length; n++)
        {
            dtNew.ImportRow(dtRow[n]);
        }
        //dtNew.Columns.Add("type");

        for (int i = 0; i < dtNew.Rows.Count; i++)
        {
            if (dtNew.Rows[i]["accountType"].ToString() == "13")
            {
                dtNew.Rows[i]["type"] = "友络佣金";//友络佣金
            }
            else if (dtNew.Rows[i]["accountType"].ToString() == "4")
            {
                dtNew.Rows[i]["type"] = "点单退款";//点单退款
            }
            else if (dtNew.Rows[i]["accountType"].ToString() == "5")
            {
                dtNew.Rows[i]["type"] = "点单收入";//点单收入
            }
            else if (dtNew.Rows[i]["accountType"].ToString() == "6")
            {
                dtNew.Rows[i]["type"] = "结账扣款";//结账扣款
            }
            if (dtNew.Rows[i]["accountMoney"] == DBNull.Value || dtNew.Rows[i]["accountMoney"] == null || dtNew.Rows[i]["accountMoney"].ToString() == "")
            {
                dtNew.Rows[i]["accountMoney"] = 0;
            }
            if (dtNew.Rows[i]["remainMoney"] == DBNull.Value || dtNew.Rows[i]["remainMoney"] == null || dtNew.Rows[i]["remainMoney"].ToString() == "")
            {
                dtNew.Rows[i]["remainMoney"] = 0;
            }

            //if (Common.ToInt32(dtNew.Rows[i]["accountType"]) == (int)AccountType.ORDER_OUTCOME)
            //{
            //    Refund += dtNew.Rows[i]["preOrderId"].ToString() + ",";
            //}
        }

        //if (Refund.Length > 0)
        //{
        //    Refund = Refund.Substring(0, Refund.Length - 1);
        //    SybMoneyMerchantOperate smo = new SybMoneyMerchantOperate();
        //    DataTable dtOrder = smo.getAccountDetailByAccountType(Refund);
        //    for (int i = 0; i < dtNew.Rows.Count; i++)
        //    {
        //        for (int j = 0; j < dtOrder.Rows.Count; j++)
        //        {
        //            if (Common.ToInt32(dtNew.Rows[i]["accountType"]) == (int)AccountType.ORDER_OUTCOME && dtNew.Rows[i]["OrderId"].ToString() == dtOrder.Rows[j]["OrderId"].ToString())
        //            {
        //                 dtNew.Rows[i]["preOrderId"] = dtOrder.Rows[j]["preOrder19dianId"].ToString();
        //            }
        //        }
        //    }
        //}
        //移除accountType列
        //dtNew.Columns.RemoveAt(0);
        dtNew.Columns.RemoveAt(3);
        return dtNew;
    }


}