using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Control
{
    /// <summary>
    /// 导入导出excel辅助类
    /// </summary>
    public class ExcelHelper
    {
        /// <summary>
        /// 数据DataTable导出excel
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="page">当前页面对象</param>
        /// <param name="fileName">导出excel文件名称</param>
        public static void ExportExcel(DataTable dt, Page page, string fileName)
        {
            HttpResponse resp;
            resp = page.Response;
            resp.Buffer = true;
            resp.ClearContent();
            resp.ClearHeaders();
            resp.Charset = "GB2312";
            resp.AppendHeader("Content-Disposition", "attachment;filename=" + fileName + ".xls");
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
                    try
                    {
                        if (i == (cl - 1))//最后一列，加n
                        {
                            ls_item += row[i] == null ? string.Empty : row[i].ToString().Trim() + "\n";
                        }
                        else
                        {
                            ls_item += row[i] == null ? string.Empty : row[i].ToString().Trim() + "\t";
                        }
                    }
                    catch(Exception e)
                    {
                        ls_item += e.Message + "\t";
                    }
                }
                resp.Write(ls_item);
                ls_item = "";
            }
            resp.End();
        }

        /// <summary>
        /// 数据excel导入
        /// </summary>
        /// <param name="fileUpload"></param>
        /// <returns></returns>
        public static ImportResult ImportExcel(FileUpload fileUpload)
        {
            ImportResult result = new ImportResult();
            result.dtPhone = null;
            try
            {
                HttpPostedFile hpf = fileUpload.PostedFile;
                string extension = System.IO.Path.GetExtension(hpf.FileName);
                if (hpf.ContentLength > 0 && (extension == ".xls" || extension == ".xlsx"))
                {
                    string strFileName = "";
                    string strConn = "";
                    if (extension == ".xls")
                    {
                        strFileName = HttpContext.Current.Server.MapPath("../UploadFiles/Temp/" + "CustomerPhone" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
                        hpf.SaveAs(strFileName);
                        strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source = " + strFileName + ";Extended Properties = 'Excel 8.0;HDR=NO;IMEX=1'";
                    }
                    else
                    {
                        strFileName = HttpContext.Current.Server.MapPath("../UploadFiles/Temp/" + "CustomerPhone" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx");
                        hpf.SaveAs(strFileName);
                        strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strFileName + ";Extended Properties=\"Excel 12.0;HDR=YES\"";
                    }
                    if (!string.IsNullOrEmpty(strConn))
                    {
                        using (System.Data.OleDb.OleDbDataAdapter oda = new System.Data.OleDb.OleDbDataAdapter("SELECT * FROM [Sheet1$]", strConn))
                        {
                            DataSet ds = new DataSet();
                            oda.Fill(ds, "dt");
                            if (ds != null && ds.Tables[0].Rows.Count > 0)
                            {
                                result.dtPhone = ds.Tables[0];
                                result.message = "读取Excel成功";
                            }
                        }
                    }
                    else
                    {
                        result.message = "读取Excel失败，请联系系统管理员！";
                    }
                }
                else
                {
                    result.message = "无法获取文件，请检查文件类型！";
                }
            }
            catch (Exception ex)
            {
                result.message = "导入异常：" + ex.Message;
            }
            return result;
        }
    }

    public class ImportResult
    {
        public DataTable dtPhone { get; set; }
        public string message { get; set; }
    }
}
