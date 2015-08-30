using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;
//
//  Copyright 2011 View Alloc inc. All rights reserved.
//  Created by Jason Xiao on 2012-04-10.
//
namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class SystemConfigManager
    {
        /// <summary>
        /// 新增系统设置
        /// </summary>
        /// <param name="systemConfig"></param>
        public int InsertSystemConfig(SystemConfigInfo systemConfig)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                SqlTransaction tran = null;
                Object obj = null;
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();

                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into VASystemConfig(");
                    strSql.Append("configName,configDescription,configContent)");
                    strSql.Append(" values (");
                    strSql.Append("@configName,@configDescription,@configContent)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
					    new SqlParameter("@configName", SqlDbType.NVarChar,500),
                        new SqlParameter("@configDescription", SqlDbType.NVarChar,500),
                        new SqlParameter("@configContent", SqlDbType.NVarChar,500)
                    };
                    parameters[0].Value = systemConfig.configName;
                    parameters[1].Value = systemConfig.configDescription;
                    parameters[2].Value = systemConfig.configContent;
                    //1、插入系统设置表信息
                    obj = SqlHelper.ExecuteScalar(tran, CommandType.Text, strSql.ToString(), parameters);
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }

                    throw ex;
                }
                if (obj == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(obj);
                }
            }
        }
        /// <summary>
        /// 修改系统设置
        /// </summary>
        /// <param name="systemConfig"></param>
        public bool UpdateSystemConfig(SystemConfigInfo systemConfig)
        {
            StringBuilder strSql = new StringBuilder();
            SqlParameter[] parameters = null;
            strSql.Append("update VASystemConfig set ");
            strSql.Append("configName=@configName,");
            strSql.Append("configDescription=@configDescription,");
            strSql.Append("configContent=@configContent");
            strSql.Append(" where Id=@Id ");
            parameters = new SqlParameter[]{
					    new SqlParameter("@configName", SqlDbType.NVarChar,500),
                        new SqlParameter("@configDescription", SqlDbType.NVarChar,500),
                        new SqlParameter("@configContent", SqlDbType.NVarChar,500),
                        new SqlParameter("@Id", SqlDbType.Int,4)
                    };
            parameters[0].Value = systemConfig.configName;
            parameters[1].Value = systemConfig.configDescription;
            parameters[2].Value = systemConfig.configContent;
            parameters[3].Value = systemConfig.Id;
            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (result >= 1) //
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 查询所有配置信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectSystemConfig(string configName, string configDescription)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select [Id],[configName],[configDescription],[configContent] FROM VASystemConfig where id>18 ");
            if (!string.IsNullOrEmpty(configName))
            {
                strSql.AppendFormat(" and configName like '%{0}%'", configName);
            }
            if (!string.IsNullOrEmpty(configDescription))
            {
                strSql.AppendFormat(" and configDescription like '%{0}%'", configDescription);
            }
            strSql.Append(" order by id desc");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 查询所有激励配置信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectEncourageConfig()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select [id],[configName],[configDescription],[configContent],[configMessage] FROM VAEncourageConfig");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }


        /// <summary>
        /// 获取使用新账户体系时间
        /// </summary>
        /// <returns></returns>
        public static DateTime GetUseNewAccountTime()
        {
            DateTime val = DateTime.Now;
            string strsql = "select configContent from VASystemConfig where configName='useNewAccountTime'";
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, null))
            {
                if (dr.Read())
                {
                    val = Convert.ToDateTime(dr["configContent"]);
                }
            }
            return val;
        }
        /// <summary>
        /// 获取客户端201401版本背景图片
        /// </summary>
        /// <returns></returns>
        public static string GetClientBgImage()
        {
            string val = string.Empty;
            string strsql = "select configContent from VASystemConfig where configName='clientBgImage'";
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, null))
            {
                if (dr.Read())
                {
                    val = Convert.ToString(dr["configContent"]);
                }
            }
            return val;
        }
        /// <summary>
        /// 查询客服热线
        /// </summary>
        /// <returns></returns>
        public static string GetServicePhone()
        {
            string val = string.Empty;
            string strsql = "select configContent from VASystemConfig where configName='servicePhone'";
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, null))
            {
                if (dr.Read())
                {
                    val = Convert.ToString(dr["configContent"]);
                }
            }
            return val;
        }
        /// <summary>
        /// 查询兑换积分倍数（）
        /// </summary>
        /// <returns></returns>
        public static string GetPointsForMultiple()
        {
            string val = string.Empty;
            string strsql = "select configContent from VASystemConfig where configName='pointsForMultiple'";
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, null))
            {
                if (dr.Read())
                {
                    val = Convert.ToString(dr["configContent"]);
                }
            }
            return val;
        }
        /// <summary>
        /// 查询兑换积分规则
        /// </summary>
        /// <returns></returns>
        public static string GetPointsForSpecifications()
        {
            string val = string.Empty;
            string strsql = "select configContent from VASystemConfig where configName='pointsForSpecifications'";
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, null))
            {
                if (dr.Read())
                {
                    val = Convert.ToString(dr["configContent"]);
                }
            }
            return val;
        }
        /// <summary>
        /// 用户评分查询兑换积分倍数（）
        /// </summary>
        /// <returns></returns>
        public static string GradeGetPointsForMultiple()
        {
            string val = string.Empty;
            string strsql = "select configContent from VASystemConfig where configName='gradePointsForMultiple'";
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, null))
            {
                if (dr.Read())
                {
                    val = Convert.ToString(dr["configContent"]);
                }
            }
            return val;
        }
        /// <summary>
        /// 用户评分查询兑换积分规则
        /// </summary>
        /// <returns></returns>
        public static string GradeGetPointsForSpecifications()
        {
            string val = string.Empty;
            string strsql = "select configContent from VASystemConfig where configName='gradePointsForSpecifications'";
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, null))
            {
                if (dr.Read())
                {
                    val = Convert.ToString(dr["configContent"]);
                }
            }
            return val;
        }
        /// <summary>
        /// 获取悠先点菜支付方式
        /// </summary>
        /// <returns></returns>
        public DataTable SelectPayMode()
        {
            const string strSql = "select [id],[payModeName],[payModeValue] FROM PayModeInfo where status = 1";
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 获取有效支付方式
        /// </summary>
        /// <returns></returns>
        public List<VAPayMode> SelectPayModeList()
        {
            const string strSql = "select payModeValue as payModeId,[payModeName] FROM PayModeInfo where status = 1";
            List<VAPayMode> payModeList = new List<VAPayMode>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql))
            {
                while (sdr.Read())
                {
                    payModeList.Add(sdr.GetEntity<VAPayMode>());
                }
            }
            return payModeList;
        }
        /// <summary>
        /// 查询注册奖励是否需要判断设备编号
        /// </summary>
        /// <returns></returns>
        public static bool IsDeviceCheck()
        {
            try
            {
                string val = string.Empty;
                string strsql = "select configContent from VASystemConfig where configName='registerEncourageDeviceCheck'";
                using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, null))
                {
                    if (dr.Read())
                    {
                        val = Convert.ToString(dr["configContent"]);
                    }
                }
                if (val == "1")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 客户端充值功能开启 add by wangc 20140512
        /// </summary>
        /// <returns></returns>
        public static bool ClientRechargeFeatureIsOpen()
        {
            string val = string.Empty;
            string strsql = "select configContent from VASystemConfig where configName='rechargeFeatureOpen'";
            try
            {
                using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, null))
                {
                    if (dr.Read())
                    {
                        val = Convert.ToString(dr["configContent"]);
                    }
                }
                return val == "1";
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 查询系统配置手续费比例
        /// </summary>
        /// <returns></returns>
        public static double GetVATransactionProportion()
        {
            string strsql = "select configContent from VASystemConfig where configName='transactionProportion'";
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql))
            {
                if (dr.Read())
                {
                    return Convert.ToDouble(dr["configContent"]);
                }
            }
            return 0;
        }
        /// <summary>
        /// 年夜饭套餐有效周期(单位:分钟)
        /// </summary>
        /// <returns></returns>
        public static double GetVAMealValidPeriod()
        {
            string strsql = "select configContent from VASystemConfig where configName='mealValidPeriod'";
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql))
            {
                if (dr.Read())
                {
                    return Convert.ToDouble(dr["configContent"]);
                }
            }
            return 0;
        }
        /// <summary>
        /// 年夜饭套餐活动规则
        /// </summary>
        /// <returns></returns>
        public static string GetVAMealActivityRule()
        {
            string strsql = "select configContent from VASystemConfig where configName='mealRule'";
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql))
            {
                if (dr.Read())
                {
                    return dr["configContent"].ToString();
                }
                else
                {
                    return "";
                }
            }
        }
        /// <summary>
        /// 年夜饭套餐活动规则简版
        /// </summary>
        /// <returns></returns>
        public static string GetVAMealActivityRuleMini()
        {
            const string strsql = "select configContent from VASystemConfig where configName='mealRuleMini'";
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql))
            {
                if (dr.Read())
                {
                    return dr["configContent"].ToString();
                }
                else
                {
                    return "";
                }
            }
        }
        /// <summary>
        ///点菜客户端根据点赞数和销量计算排序基数（点赞基数,销量基数）
        /// </summary>
        /// <returns></returns>
        public static string GetDishSortAlgorithmBase()
        {
            const string strsql = "select configContent from VASystemConfig where configName='dishSortAlgorithmBase'";
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql))
            {
                if (dr.Read())
                {
                    return dr["configContent"].ToString();
                }
                else
                {
                    return "";
                }
            }
        }
        /// <summary>
        /// 查询客户端可用红包版本号
        /// </summary>
        /// <returns></returns>
        public static string GetAvailableRedEnvelopeBuild()
        {
            const string strsql = "select configContent from VASystemConfig where configName='availableRedEnvelopeBuild'";
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql))
            {
                if (dr.Read())
                {
                    return dr["configContent"].ToString();
                }
                else
                {
                    return "6.4.0|2014.08.25";
                }
            }
        }
        /// <summary>
        /// 抵扣券链接对应券数量
        /// </summary>
        /// <returns></returns>
        public static int GetCouponCount()
        {
            const string strsql = "select configContent from VASystemConfig where configName='couponCount'";
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql))
            {
                if (dr.Read())
                {
                    return Convert.ToInt32(dr["configContent"]);
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 抵扣券链接有效期
        /// </summary>
        /// <returns></returns>
        public static double GetCouponValidDate()
        {
            const string strsql = "select configContent from VASystemConfig where configName='couponValidDate'";
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql))
            {
                if (dr.Read())
                {
                    return Convert.ToDouble(dr["configContent"]);
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 查询系统配置表中的内容
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        public static string GetSystemConfigContent(string configName)
        {
            const string strsql = "select configContent from VASystemConfig where configName=@configName";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@configName", SqlDbType.NVarChar, 500) { Value = configName }
            };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, para))
            {
                if (dr.Read())
                {
                    return dr["configContent"].ToString();
                }
                else
                {
                    return "";
                }
            }
        }
    }
}
