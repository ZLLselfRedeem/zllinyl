﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using System.Data;
using System.Data.SqlClient;
using VAGastronomistMobileApp.DBUtility;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class ShopConnRedEnvelopeManager
    {
        /// <summary>
        /// 查询店铺有没有单独配置红包
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public ShopConnRedEnvelope SelectShopConnRedEnvelope(int shopId)
        {
            const string strSql = "select * from ShopConnRedEnvelope where shopId=@shopId";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@shopId", SqlDbType.Int) { Value = shopId }
            };
            ShopConnRedEnvelope redEnvelope = new ShopConnRedEnvelope();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                if (sdr.Read())
                {
                    redEnvelope = SqlHelper.GetEntity<ShopConnRedEnvelope>(sdr);
                }
            }
            return redEnvelope;
        }

        public bool UpdateShopConnRedEnvelope(int shopId, int status, int RedEnvelopeConsumeCount, double RedEnvelopeConsumeAmount)
        {
            const string strSql = @"update ShopConnRedEnvelope set status=@status,RedEnvelopeConsumeCount=@RedEnvelopeConsumeCount,
RedEnvelopeConsumeAmount=@RedEnvelopeConsumeAmount where ShopId=@ShopID";

            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@status",SqlDbType.Bit){Value=status},
                new SqlParameter("@RedEnvelopeConsumeCount",SqlDbType.Int){Value=RedEnvelopeConsumeCount},
                new SqlParameter("@RedEnvelopeConsumeAmount",SqlDbType.Float){Value=RedEnvelopeConsumeAmount},
                new SqlParameter("@ShopID", SqlDbType.Int){ Value = shopId }
            };

            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql, para);
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool InsertShopConnRedEnvelope(ShopConnRedEnvelope shopConnRedEnvelope)
        {
            const string strSql = @"Insert into ShopConnRedEnvelope(ShopId,RedEnvelopeConsumeCount,RedEnvelopeConsumeAmount,status)
            Values(@ShopId,@RedEnvelopeConsumeCount,@RedEnvelopeConsumeAmount,@status)";

            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@ShopId", SqlDbType.Int){ Value = shopConnRedEnvelope.ShopId },
            new SqlParameter("@RedEnvelopeConsumeCount", SqlDbType.Int){ Value = shopConnRedEnvelope.RedEnvelopeConsumeCount},
            new SqlParameter("@RedEnvelopeConsumeAmount", SqlDbType.Float){ Value = shopConnRedEnvelope.RedEnvelopeConsumeAmount },
            new SqlParameter("@Status", SqlDbType.Bit){ Value = shopConnRedEnvelope.status }
            };

            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql, para);
                if (i > 0)
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
}
