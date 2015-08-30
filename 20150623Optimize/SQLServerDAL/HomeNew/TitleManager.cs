using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model.HomeNew;
using VAGastronomistMobileApp.WebPageDll;

namespace VAGastronomistMobileApp.SQLServerDAL.HomeNew
{
    public enum TitleUplineReturn
    {
        Success = 0,
        CityOrSuTitleClash = 1,
    }
    public class TitleManager
    {
        public static string Upline(int cityID, int firstTitleID, int titleIndex)
        {
            StringBuilder result = new StringBuilder();
            //StringBuilder strCity = new StringBuilder();
            StringBuilder strSubTitle = new StringBuilder();
            StringBuilder strTitleClash = new StringBuilder();
            //StringBuilder strSubTitleClash = new StringBuilder();
            //strCity.AppendFormat("select * from city where isClientShow=1 and cityID={0}", cityID);
            strSubTitle.AppendFormat("select * from HomeSecondTitle where firstTitleID={0} and isDelete=0", firstTitleID);
            strTitleClash.AppendFormat("select * from homeFirstTitle where isDelete=0 and status=1 and cityID={0} and titleIndex ={1}",cityID, titleIndex);
            if (firstTitleID != 0)
            {
                strTitleClash.AppendFormat(" and id!={0}", firstTitleID);
            }
            //DataSet dsCity = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strCity.ToString());
            DataSet dsSubTitle = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSubTitle.ToString());
            DataSet dsTitleClash = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strTitleClash.ToString());
            //if (dsCity.Tables[0].Rows.Count < 1)
            //{
            //    result.Append("城市在客户端未上线！");
            //}
            if (dsSubTitle.Tables[0].Rows.Count <=1)
            {
                result.Append("二级栏目数量少于2！");
            }
            if (dsTitleClash.Tables[0].Rows.Count > 0)
            {
                result.Append("一级栏目排序冲突!");
            }
            //if (firstTitleID != 0)
            //{
            //    strSubTitleClash.AppendFormat("select titleIndex,COUNT(*) from HomeSecondTitle where isDelete=0 and firstTitleID={0} group by titleIndex having COUNT(*) >1", firstTitleID);
            //    DataSet dsSubTitleClash = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSubTitleClash.ToString());
            //    if (dsSubTitleClash.Tables[0].Rows.Count > 0)
            //    {
            //        result.Append("二级栏目排序冲突！");
            //    }
            //}
            return result.ToString();
        }

        /// <summary>
        /// 修改一级栏目操作
        /// </summary>
        /// <param name="id"></param>
        /// <param name="titleName"></param>
        /// <param name="titleIndex"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static int TitleUpdate(int id, string titleName, int titleIndex, int status)
        {
            int result = 0;
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                try
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("Update HomeFirstTitle");
                    strSql.Append(" set titleName=@titleName,titleIndex=@titleIndex,status=@status");
                    strSql.Append(" where id=@id");
                    //strSql.AppendFormat("update HomeSecondTitle set status={0} where firstTitleID={1} and isDelete=0", status, id);

                    SqlParameter[] param = new SqlParameter[] { 
                            new SqlParameter("@id",SqlDbType.Int,4),
                            new SqlParameter("@status", SqlDbType.Int,4),
                            new SqlParameter("@titleIndex",SqlDbType.Int, 4),
                            new SqlParameter("@titleName", SqlDbType.NVarChar)};

                    param[0].Value = id;
                    param[1].Value = status;
                    param[2].Value = titleIndex;
                    param[3].Value = titleName;

                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), param);
                }
                catch (Exception)
                {
                }
            }
            return result;
        }

        /// <summary>
        /// 根据城市id获取一级标题
        /// </summary>
        /// <param name="cityID"></param>
        /// <returns></returns>
        public static DataTable SelectTitle(int cityID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ROW_NUMBER()over(Order by title.titleIndex) as [rowID],title.[id],title.[cityID],city.cityName,title.[titleName],title.[titleIndex],title.[status],(case title.type when 2 then '广告' else '基本' end) type");
            strSql.Append(" from HomeFirstTitle title");
            strSql.Append(" INNER JOIN City city On title.cityID=city.cityID");
            //strSql.Append(" INNER JOIN HomeSecondTitle subTitle On subTitle.firstTitleID=title.id");
            strSql.AppendFormat(" where title.isDelete=0 and title.cityID={0}", cityID);
            strSql.Append(" Order by title.titleIndex");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            DataTable table = ds.Tables[0];
            //int titleID = 0;

            //for (int i = table.Rows.Count - 1; i >= 0; i--)
            //{
            //    if (titleID == Convert.ToInt32(table.Rows[i]["id"]))
            //    {
            //        table.Rows[i + 1]["subTitleInfo"] = Convert.ToString(table.Rows[i + 1]["subTitleInfo"]) + "|" + Convert.ToString(table.Rows[i]["subTitleInfo"]);
            //        table.Rows.RemoveAt(i);
            //    }
            //    else
            //    {
            //        titleID = Convert.ToInt32(table.Rows[i]["id"]);
            //    }
            //}
            return table;
        }

        /// <summary>
        /// 创建二级栏目
        /// </summary>
        /// <param name="cityID"></param>
        /// <returns></returns>
        public static DataTable SelectAllSubTitle(int cityID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select title.[id],title.[cityID],city.cityName,title.[titleName],title.[titleIndex],subTitle.[status],subTitle.id as subid,subTitle.titleName as subtitleName");
            strSql.Append(" from HomeFirstTitle title");
            strSql.Append(" INNER JOIN City city On title.cityID=city.cityID");
            strSql.Append(" INNER JOIN HomeSecondTitle subTitle On subTitle.firstTitleID=title.id");
            strSql.AppendFormat(" where title.isDelete=0 and title.cityID={0} and subTitle.isDelete=0", cityID);
            strSql.Append(" Order by title.titleIndex, subTitle.titleIndex");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            DataTable table = ds.Tables[0];
            ////int titleID = 0;

            //for (int i = table.Rows.Count - 1; i >= 0; i--)
            //{
            //    if (titleID == Convert.ToInt32(table.Rows[i]["id"]))
            //    {
            //        table.Rows[i + 1]["subTitleInfo"] = Convert.ToString(table.Rows[i + 1]["subTitleInfo"]) + "|" + Convert.ToString(table.Rows[i]["subTitleInfo"]);
            //        table.Rows.RemoveAt(i);
            //    }
            //    else
            //    {
            //        titleID = Convert.ToInt32(table.Rows[i]["id"]);
            //    }
            //}
            return table;
        }
        ///// <summary>
        ///// 获取下拉框中的
        ///// </summary>
        ///// <returns></returns>
        //public static DataTable SelectTitle()
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select ROW_NUMBER()over(Order by title.cityID) as [rowID],[id],title.[cityID],city.cityName,[titleName],[titleIndex],(CASE title.status when 1 then '是' else '否' END) [status]");
        //    strSql.Append(" from HomeFirstTitle title");
        //    strSql.Append(" INNER JOIN City city");
        //    strSql.AppendFormat(" On title.cityID=city.cityID where city.status=2 and isDelete=0");
        //    DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
        //    return ds.Tables[0];
        //}

        /// <summary>
        /// 根据id获取二级栏目
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectSubTitle(int subid)
        {
            StringBuilder str = new StringBuilder();
            str.AppendFormat("select second.titleName,second.titleIndex,second.type,second.status,first.titleName as firstName, city.cityName from HomeSecondTitle second inner join HomeFirstTitle first on first.id = second.firstTitleID inner join city city on first.cityID = city.cityID where second.id='{0}'", subid);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, str.ToString());
            return ds.Tables[0];
        }



        /// <summary>
        /// 根据id来删除一级栏目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool RemoveTitle(int id)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("Update HomeFirstTitle set");
                    strSql.Append(" isDelete=1");
                    strSql.AppendFormat(" where id={0};", id);
                    strSql.AppendFormat(" Update HomeSecondTitle set isDelete=1 where firstTitleID={0}", id);
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString());
                }
                catch (System.Exception)
                {
                    return false;
                }
                if (result >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool RemoveSubTitle(int id)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("Update HomeSecondTitle set");
                    strSql.Append(" isDelete=1");
                    strSql.AppendFormat(" where id={0};", id);
                    strSql.AppendFormat(" Update AdvertShop set isDelete=1 where secondTitleID={0}", id);
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString());
                }
                catch (System.Exception)
                {
                    return false;
                }
                if (result >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="titleId"></param>
        /// <returns></returns>
        public static Title QueryTitle(int titleId)
        {
            Title title = new Title();
            DataTable dtTitle = SelectTitleByID(titleId);
            if (1 == dtTitle.Rows.Count)
            {
                title.ID = titleId;
                title.CityID = Convert.ToInt32(dtTitle.Rows[0]["cityID"]);
                title.TitleName = Convert.ToString(dtTitle.Rows[0]["titleName"]);
                title.TitleIndex = Convert.ToInt32(dtTitle.Rows[0]["titleIndex"]);
                title.Type = Convert.ToInt32(dtTitle.Rows[0]["type"]);
                title.Status = Convert.ToInt32(dtTitle.Rows[0]["status"]);
                title.CreateTime = Convert.ToDateTime(dtTitle.Rows[0]["createTime"]);
                title.CreateBy = Convert.ToInt32(dtTitle.Rows[0]["createBy"]);
                title.IsDelete = Convert.ToBoolean(dtTitle.Rows[0]["isDelete"]);
            }
            return title;
        }

        public static DataTable SelectTitleByID(int titleId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Select *");
            strSql.Append(" from HomeFirstTitle");
            strSql.AppendFormat(" where id={0}", titleId);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        ///向数据库中插入一条一级栏目记录
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public static int Insert(Title title)
        {
            int result = 0;
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {

                try
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("Insert into HomeFirstTitle ");
                    strSql.Append("values(@cityID,@titleName,@titleIndex,@type,@status,@createTime,@createBy,@isDelete)");

                    SqlParameter[] param = new SqlParameter[] { 
                        new SqlParameter("@cityID",SqlDbType.Int,4),
                        new SqlParameter("@titleName", SqlDbType.NVarChar),
                        new SqlParameter("@titleIndex",SqlDbType.Int, 4),
                        new SqlParameter("@type",SqlDbType.Int, 4),
                        new SqlParameter("@status", SqlDbType.Int,4),
                        new SqlParameter("@createTime",SqlDbType.DateTime),
                        new SqlParameter("@createBy",SqlDbType.Int, 4),
                        new SqlParameter("@isDelete",SqlDbType.Bit)
                        };

                    param[0].Value = title.CityID;
                    param[1].Value = title.TitleName;
                    param[2].Value = title.TitleIndex;
                    param[3].Value = title.Type;
                    param[4].Value = title.Status;
                    param[5].Value = title.CreateTime;
                    param[6].Value = title.CreateBy;
                    param[7].Value = title.IsDelete;

                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), param);
                }
                catch (Exception)
                {
                }
            }
            return result;
        }


        public static bool IndexClash(int titleIndex, int cityID, int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select id from HomeFirstTitle where titleIndex={0} and isDelete=0 and cityID={1} and status=1", titleIndex, cityID);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            DataTable dTable = ds.Tables[0];
            if (dTable.Rows.Count > 0 && Convert.ToInt32(dTable.Rows[0]["id"]) != id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IndexClash(int titleIndex, int cityID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select id from HomeFirstTitle where titleIndex={0} and isDelete=0 and cityID={1} and status=1", titleIndex, cityID);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            DataTable dTable = ds.Tables[0];
            if (dTable.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool SubIndexClash(int titleIndex, int titleID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select id from HomeSecondTitle where titleIndex='{0}' and isDelete=0 and firstTitleID='{1}' and status=1", titleIndex, titleID);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            DataTable dTable = ds.Tables[0];
            if (dTable.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool SubIndexClash(int titleIndex, int firstTitleID, int titleID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select id from HomeSecondTitle where titleIndex='{0}' and isDelete=0 and firstTitleID='{1}' and status=1", titleIndex, firstTitleID);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            DataTable dTable = ds.Tables[0];
            if (dTable.Rows.Count > 0 && Convert.ToUInt32(dTable.Rows[0]["id"]) != titleID)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 查询一级目录 
        /// </summary>
        /// <returns></returns>
        public static List<TitleViewModel> SelectHandleTitle(int cityID, out int NonAdTitleID)
        {
            NonAdTitleID = 0;
            StringBuilder str = new StringBuilder();
            str.Append("select id,titleName,type from HomeFirstTitle where isdelete=0");
            //if (cityID != 0)
            //{
            str.AppendFormat(" and cityID='{0}'", cityID);
            //}
            //else
            //{
            //    str.AppendFormat(" and 1=2 ");
            //}

            var list = new List<TitleViewModel>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, str.ToString()))
            {
                while (dr.Read())
                {
                    if (Convert.ToInt32(SqlHelper.ConvertDbNullValue(dr["type"])) == 1)
                    {
                        NonAdTitleID = Convert.ToInt32(SqlHelper.ConvertDbNullValue(dr["id"]));
                    }

                    list.Add(new TitleViewModel()
                    {
                        titleID = Convert.ToInt32(SqlHelper.ConvertDbNullValue(dr["id"])),
                        titleName = SqlHelper.ConvertDbNullValue(dr["titleName"])
                    });
                }
            }
            return list;
        }

        public static List<TitleViewModel> SelectHandleSubTitle(int firstTitleID)
        {
            StringBuilder str = new StringBuilder();
            str.Append("select id,titleName from HomeSecondTitle where isdelete=0 ");
            if (firstTitleID != 0)
            {
                str.AppendFormat(" and firstTitleID='{0}'", firstTitleID);
            }
            else
            {
                str.AppendFormat(" and 1=2 ");
            }

            var list = new List<TitleViewModel>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, str.ToString()))
            {
                while (dr.Read())
                {
                    list.Add(new TitleViewModel()
                    {
                        titleID = Convert.ToInt32(SqlHelper.ConvertDbNullValue(dr["id"])),
                        titleName = SqlHelper.ConvertDbNullValue(dr["titleName"])
                    });
                }
            }
            return list;
        }

        public static List<SubTitleModel> SelectHandleSubTitle(int firstTitleID, int type)
        {
            StringBuilder str = new StringBuilder();
            str.Append("select Convert(varchar(5),id)+','+Convert(varchar(5),[TYPE]) as titleId,titleName from HomeSecondTitle where isdelete=0 ");
            if (firstTitleID != 0)
            {
                str.AppendFormat(" and firstTitleID='{0}'", firstTitleID);
            }
            else
            {
                str.AppendFormat(" and 1=2 ");
            }

            var list = new List<SubTitleModel>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, str.ToString()))
            {
                while (dr.Read())
                {
                    list.Add(new SubTitleModel()
                    {
                        titleID = Convert.ToString(SqlHelper.ConvertDbNullValue(dr["titleId"])),
                        titleName = SqlHelper.ConvertDbNullValue(dr["titleName"])
                    });
                }
            }
            return list;
        }

        public static DataTable SelectSubTitle(int cityID, int titleID)
        {
            StringBuilder str = new StringBuilder();
            str.Append("select ROW_NUMBER()over(Order by second.titleIndex) rowID, second.id,first.titleName as firstTitleName,second.firstTitleID,second.titleName,second.titleIndex,(case when first.type=2 and second.type =1 then '手动排序' when first.type=1 and second.type=1 then '智能排序' else '由近到远' end) type,second.firstTitleID,second.status");
            str.Append(" from HomeSecondTitle second INNER JOIN HomeFirstTitle first ON second.firstTitleID = first.id");
            str.Append(" INNER JOIN City city ON first.cityID = city.cityID where second.isDelete=0");
            if (cityID != 0)
            {
                str.AppendFormat(" and city.cityID='{0}'", cityID);
            }
            if (titleID != 0)
            {
                str.AppendFormat(" and second.firstTitleID='{0}'", titleID);
            }
            str.Append(" order by second.titleIndex");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, str.ToString());
            return ds.Tables[0];
        }

        public static int ClientUpdate(int id, int status)
        {
            int result = 0;
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                try
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.AppendFormat("update HomeFirstTitle set status={0} where id={1};", status, id);
                    //strSql.AppendFormat("update HomeSecondTitle set status={0} where firstTitleID={1} and isDelete=0", status, id);
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString());
                }
                catch (Exception)
                {
                }
            }
            return result;
        }
    }
}
