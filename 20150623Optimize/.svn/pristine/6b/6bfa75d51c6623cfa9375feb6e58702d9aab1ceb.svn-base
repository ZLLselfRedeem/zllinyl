using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;
namespace VAGastronomistMobileApp.SQLServerDAL
{
    /// <summary>
    /// 功能描述:菜的口味Id
    /// 创建标识:罗国华 20131030
    /// </summary>
    public class DishTasteManager
    {
        #region 通用模块
        public static DishTaste GetModel(int tasteId)
        {
            string strsql = @"select tasteId,menuId,tasteName,tasteRemark,tasteSequence,tasteStatus from DishTaste where tasteId=@tasteId";
            var parm = new[] { new SqlParameter("@tasteId", tasteId) };
            DishTaste model = null;
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, parm))
            {
                if (dr.Read())
                {
                    model = new DishTaste();
                    model.tasteId = !dr.IsDBNull(0) ? dr.GetInt32(0) : 0;
                    model.menuId = !dr.IsDBNull(1) ? dr.GetInt32(1) : 0;
                    model.tasteName = !dr.IsDBNull(2) ? dr.GetString(2) : string.Empty;
                    model.tasteRemark = !dr.IsDBNull(3) ? dr.GetString(3) : string.Empty;
                    model.tasteSequence = !dr.IsDBNull(4) ? dr.GetInt32(4) : 0;
                    model.tasteStatus = !dr.IsDBNull(5) ? dr.GetBoolean(5) : false;

                }
            }
            return model;
        }

        public static int Insert(DishTaste model, SqlTransaction trans)
        {
            string strsql = @"insert into DishTaste (menuId,tasteName,tasteRemark,tasteSequence,tasteStatus)
                        values (@menuId,@tasteName,@tasteRemark,@tasteSequence,@tasteStatus)
select @@identity";
            SqlParameter[] parm = {
new SqlParameter("@menuId", SqlDbType.Int),
new SqlParameter("@tasteName", SqlDbType.NVarChar,50),
new SqlParameter("@tasteRemark", SqlDbType.NVarChar,200),
new SqlParameter("@tasteSequence", SqlDbType.Int),
new SqlParameter("@tasteStatus", SqlDbType.Bit)
                        };
            parm[0].Value = model.menuId;
            parm[1].Value = model.tasteName;
            parm[2].Value = model.tasteRemark;
            parm[3].Value = model.tasteSequence;
            parm[4].Value = model.tasteStatus;
            return Convert.ToInt32(SqlHelper.ExecuteScalar(trans, CommandType.Text, strsql, parm));
        }


        public static bool Update(DishTaste model, SqlTransaction trans)
        {
            string strsql = @"update DishTaste set menuId=@menuId,tasteName=@tasteName,tasteRemark=@tasteRemark,tasteSequence=@tasteSequence,tasteStatus=@tasteStatus where tasteId=@tasteId";
            SqlParameter[] parm = {
                        new SqlParameter("@tasteId", SqlDbType.Int),
new SqlParameter("@menuId", SqlDbType.Int),
new SqlParameter("@tasteName", SqlDbType.NVarChar,50),
new SqlParameter("@tasteRemark", SqlDbType.NVarChar,200),
new SqlParameter("@tasteSequence", SqlDbType.Int),
new SqlParameter("@tasteStatus", SqlDbType.Bit)
                        };
            parm[0].Value = model.tasteId;
            parm[1].Value = model.menuId;
            parm[2].Value = model.tasteName;
            parm[3].Value = model.tasteRemark;
            parm[4].Value = model.tasteSequence;
            parm[5].Value = model.tasteStatus;
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, parm) > 0;
        }

        public static List<SybTypeModel> List(int menuId)
        {
            List<SybTypeModel> list = new List<SybTypeModel>();
            string strsql = @"select tasteId,tasteName from DishTaste
                where menuId=@menuId and tasteStatus=1";
            var parm = new[] { new SqlParameter("@menuId", menuId) };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, parm))
            {
                while (dr.Read())
                {
                    list.Add(new SybTypeModel() { Id = dr["tasteId"].ToString(), Name = dr["tasteName"].ToString() });
                }
            }
            return list;
        }
        #endregion

        public static int Insert(DishTaste model)
        {
            int val = 0;
            using (var conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                   val = Insert(model, trans);

                    trans.Commit();
                
                }
                catch (Exception)
                {
                    trans.Rollback();                    
                }
                finally
                {
                    conn.Close();
                }
            }
            return val;
        }

        public static bool Del(int tasteId)
        {
            bool val = false;
            using (var conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {

                    var strsql = "update DishTaste set tasteStatus=0 where tasteId=@tasteId";
                    var parm = new[] { new SqlParameter("@tasteId", tasteId) };
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, parm);

                    DishPriceConnTasteManager.DelTaste(tasteId, trans);

                    trans.Commit();
                    val = true;
                }
                catch (Exception)
                {
                    trans.Rollback();
                    val = false;
                }
            }
            return val;
        }

        public static bool Update(DishTaste model)
        {
            bool val = false;
            using (var conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    Update(model, trans);

                    trans.Commit();
                    val = true;
                }
                catch (Exception)
                {
                    trans.Rollback();
                    val = false;
                }
            }
            return val;
        }

        public static bool UpdatetasteName(int tasteId, string tasteName)
        {
            string strsql = @"update DishTaste set tasteName=@tasteName where tasteId=@tasteId";
            SqlParameter[] parm = {
                   new SqlParameter("@tasteId", SqlDbType.Int),
new SqlParameter("@tasteName", SqlDbType.NVarChar,50)
                    };
            parm[0].Value = tasteId;
            parm[1].Value = tasteName;
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, parm) == 1;
        }


        public static bool Exit(int menuId, string tasteName)
        {
            var val = false;
            string strsql = @"select 1 from DishTaste where menuId=@menuId and tasteName=@tasteName and tasteStatus=1";
            SqlParameter[] parm = new[] {
                    new SqlParameter("@menuId", menuId),
                     new SqlParameter("@tasteName", tasteName)
                        };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, parm))
            {
                if (dr.Read()) val = true;
            }
            return val;
        }

        /// <summary>
        /// 修改口味时，判断是否和数据库中剩余口味重复
        /// Add at 20121227 by jinyanni
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="tasteName"></param>
        /// <param name="tastId"></param>
        /// <returns>true表示重复</returns>
        public static bool Exist(int menuId, string tasteName, int tasteId)
        {
            bool result = false;
            string strSql = @"select 1 from DishTaste where menuId=@menuId and tasteName=@tasteName and tasteId <> @tasteId and tasteStatus=1 ";
            SqlParameter[] para = new[] { 
            new SqlParameter("@menuId",menuId),
            new SqlParameter("@tasteName",tasteName),
            new SqlParameter("@tasteId",tasteId)
            };

            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                if (dr.Read())
                {
                    result = true;
                }
            }
            return result;
        }
    }
}