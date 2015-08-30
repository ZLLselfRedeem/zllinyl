﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model.HomeNew;

namespace VAGastronomistMobileApp.SQLServerDAL.HomeNew
{
    public class SubTitleManager
    {
        /// <summary>
        /// 二级栏目更新
        /// </summary>
        /// <param name="subtitleID"></param>
        /// <param name="subtitleName"></param>
        /// <param name="titleIndex"></param>
        /// <param name="type"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static int Update(int subtitleID, string subtitleName, int titleIndex)
        {
            int result = 0;
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                try
                {

                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("Update HomeSecondTitle");
                    strSql.Append(" set titleName=@titleName,titleIndex=@titleIndex");
                    strSql.Append(" where id=@id");

                    SqlParameter[] param = new SqlParameter[] { 
                            new SqlParameter("@id",SqlDbType.Int,4),
                            new SqlParameter("@titleIndex",SqlDbType.Int, 4),
                            new SqlParameter("@titleName", SqlDbType.NVarChar)};

                    param[0].Value = subtitleID;
                    param[1].Value = titleIndex;
                    param[2].Value = subtitleName;

                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), param);
                }
                catch (Exception)
                {
                }
            }
            return result;
        }

        public static SubTitle SelectSubTitleByID(int id)
        {
            string strSql = string.Format(" Select * from HomeSecondTitle Where id={0}", id);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql);
            DataTable table = ds.Tables[0];
            SubTitle sub = new SubTitle()
            {
                ID = id,
                FirstTitleID = Convert.ToInt32(table.Rows[0]["firstTitleID"]),
                TitleName = Convert.ToString(table.Rows[0]["titleName"]),
                TitleIndex = Convert.ToInt32(table.Rows[0]["titleIndex"]),
                RuleType = Convert.ToInt32(table.Rows[0]["type"]),
                Status = Convert.ToInt32(table.Rows[0]["status"]),
                CreateTime = Convert.ToDateTime(table.Rows[0]["createTime"]),
                CreateBy = Convert.ToInt32(table.Rows[0]["createBy"]),
                IsDelete = Convert.ToBoolean(table.Rows[0]["isDelete"])
            };
            return sub;
        }
        /// <summary>
        /// 二级栏目新增
        /// </summary>
        /// <param name="subTitle"></param>
        /// <returns></returns>
        public static int Insert(SubTitle subTitle)
        {
            int result = 0;
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                try
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("Insert into HomeSecondTitle ");
                    strSql.Append("values(@firstTitleID,@titleName,@titleIndex,@type,@status,@createTime,@createBy,@isDelete)");

                    SqlParameter[] param = new SqlParameter[] { 
                            new SqlParameter("@firstTitleID",SqlDbType.Int,4),
                            new SqlParameter("@titleName", SqlDbType.NVarChar),
                            new SqlParameter("@titleIndex",SqlDbType.Int, 4),
                            new SqlParameter("@type",SqlDbType.Int, 4),
                            new SqlParameter("@status", SqlDbType.Int,4),
                            new SqlParameter("@createTime",SqlDbType.DateTime),
                            new SqlParameter("@createBy",SqlDbType.Int, 4),
                            new SqlParameter("@isDelete",SqlDbType.Bit)
                        };

                    param[0].Value = subTitle.FirstTitleID;
                    param[1].Value = subTitle.TitleName;
                    param[2].Value = subTitle.TitleIndex;
                    param[3].Value = subTitle.RuleType;
                    param[4].Value = 1;
                    param[5].Value = subTitle.CreateTime;
                    param[6].Value = subTitle.CreateBy;
                    param[7].Value = subTitle.IsDelete;

                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), param);
                }
                catch (Exception)
                {
                }
            }
            return result;
        }

        /// <summary>
        /// 查询二级目录
        /// </summary>
        /// <param name="secondTtielID"></param>
        /// <returns></returns>
        public DataTable SelectSubTitle(int secondTtielID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(string.Format(" from HomeSecondTitle where isDelete=0 and id={0} ", secondTtielID));
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
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
                    strSql.AppendFormat(" update HomeSecondTitle set status={0} where id={1}", status, id);
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString());
                }
                catch (Exception)
                {
                }
            }
            return result;
        }

        public static bool NumLessThanTwo(int firstTitleID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select * from HomeSecondTitle where firstTitleID={0} and isDelete=0 and status=1", firstTitleID);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            if (ds.Tables[0].Rows.Count <= 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
