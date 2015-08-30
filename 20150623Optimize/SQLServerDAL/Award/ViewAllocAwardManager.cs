﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using System.Data.SqlClient;
using System.Data;
using VAGastronomistMobileApp.DBUtility;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class ViewAllocAwardManager
    {
        /// <summary>
        /// 获取全平台奖品列表
        /// </summary>
        /// <param name="shopID"></param>
        /// <returns></returns>
        public List<ViewAllocAward> SelectVAAwardList()
        {
            const string strSql = "select * from ViewAllocAward where  Enable=1 and Status=1";

            List<ViewAllocAward> shopAwardList = new List<ViewAllocAward>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), null))
            {
                while (sdr.Read())
                {
                    shopAwardList.Add(SqlHelper.GetEntity<ViewAllocAward>(sdr));
                }
            }
            return shopAwardList;
        }

        /// <summary>
        /// 根据奖品ID获取奖品列表 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<ViewAllocAward> SelectVAAwardList(string ids)
        {
            string strSql = "select * from ViewAllocAward where Enable=1 and Status=1 and Id in ({0})";
            strSql = string.Format(strSql, ids);

            List<ViewAllocAward> shopAwardList = new List<ViewAllocAward>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), null))
            {
                while (sdr.Read())
                {
                    shopAwardList.Add(SqlHelper.GetEntity<ViewAllocAward>(sdr));
                }
            }
            return shopAwardList;
        }

        /// <summary>
        /// 查询平台某个类型奖品
        /// </summary>
        /// <param name="awardType"></param>
        /// <returns></returns>
        public List<ViewAllocAward> SelectVAAwardList(AwardType awardType)
        {
            const string strSql = "select * from ViewAllocAward where awardType=@awardType and Enable=1 and Status=1";

            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@awardType", SqlDbType.Int) { Value = (int)awardType }
            };

            List<ViewAllocAward> shopAwards = new List<ViewAllocAward>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para))
            {
                while (sdr.Read())
                {
                    shopAwards.Add(SqlHelper.GetEntity<ViewAllocAward>(sdr));
                }
            }
            return shopAwards;
        }

        /// <summary>
        /// 根据Id获取具体奖品信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ViewAllocAward SelectVAAward(Guid Id)
        {
            const string strSql = "select * from ViewAllocAward where Id=@Id";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = Id }
            };
            ViewAllocAward vaAward = new ViewAllocAward();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                if (sdr.Read())
                {
                    vaAward = SqlHelper.GetEntity<ViewAllocAward>(sdr);
                }
            }
            return vaAward;
        }
    }
}
