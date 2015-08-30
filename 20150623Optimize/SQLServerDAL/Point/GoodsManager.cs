using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;
using System.Data.SqlClient;
using System.Data;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    /// <summary>
    /// 积分商城兑换商品数据处理层
    /// 2014-2-21 jinyanni
    /// </summary>
    public class GoodsManage
    {
        /// <summary>
        /// 新增商品信息
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        public object[] InsertGoods(Goods goods)
        {
            object[] objResult = new object[] { false, "" };
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" insert into Goods(name,pictureName,exchangePrice,residueQuantity,haveExchangeQuantity,remark,boolVisible,status)");
                strSql.Append(" values(@name,@pictureName,@exchangePrice,@residueQuantity,@haveExchangeQuantity,@remark,@boolVisible,@status)");
                strSql.Append(" select @@identity");

                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@name", SqlDbType.NVarChar, 50),
                    new SqlParameter("@pictureName", SqlDbType.NVarChar, 50),
                    new SqlParameter("@exchangePrice", SqlDbType.Float),
                    new SqlParameter("@residueQuantity", SqlDbType.Int),
                    new SqlParameter("@haveExchangeQuantity", SqlDbType.Int),
                    new SqlParameter("@remark", SqlDbType.NVarChar, 500),
                    new SqlParameter("@boolVisible",SqlDbType.Bit,1),
                    new SqlParameter("@status",SqlDbType.Int)
                };
                para[0].Value = goods.name;
                para[1].Value = goods.pictureName;
                para[2].Value = goods.exchangePrice;
                para[3].Value = goods.residueQuantity;
                para[4].Value = goods.haveExchangeQuantity;
                para[5].Value = goods.remark;
                para[6].Value = goods.visible;
                para[7].Value = goods.status;

                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();
                    int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), para);
                    if (i > 0)
                    {
                        objResult[0] = true;
                        objResult[1] = "insert successful";
                    }
                    else
                    {
                        objResult[1] = "insert failed";
                    }
                }
            }
            catch (Exception ex)
            {
                objResult[1] = ex.Message;
            }
            return objResult;
        }

        /// <summary>
        /// 修改商品信息
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        public object[] UpdateGoods(Goods goods)
        {
            object[] objResult = new object[] { false, "" };
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update Goods set");
                strSql.Append(" name = @name,");
                strSql.Append(" pictureName = @pictureName,");
                strSql.Append(" exchangePrice = @exchangePrice,");
                strSql.Append(" residueQuantity = @residueQuantity,");
                strSql.Append(" haveExchangeQuantity = @haveExchangeQuantity,");
                strSql.Append(" remark = @remark,");
                strSql.Append(" boolVisible = @boolVisible,");
                strSql.Append(" status = @status");
                strSql.Append(" where id = @id");

                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@name", SqlDbType.NVarChar, 50),
                    new SqlParameter("@pictureName", SqlDbType.NVarChar, 50),
                    new SqlParameter("@exchangePrice", SqlDbType.Float),
                    new SqlParameter("@residueQuantity", SqlDbType.Int),
                    new SqlParameter("@haveExchangeQuantity", SqlDbType.Int),
                    new SqlParameter("@remark", SqlDbType.NVarChar, 500),
                    new SqlParameter("@boolVisible",SqlDbType.Bit,1),
                    new SqlParameter("@status",SqlDbType.Int),
                    new SqlParameter("@id", SqlDbType.Int)
                };
                para[0].Value = goods.name;
                para[1].Value = goods.pictureName;
                para[2].Value = goods.exchangePrice;
                para[3].Value = goods.residueQuantity;
                para[4].Value = goods.haveExchangeQuantity;
                para[5].Value = goods.remark;
                para[6].Value = goods.visible;
                para[7].Value = goods.status;
                para[8].Value = goods.id;

                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();
                    int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), para);
                    if (i > 0)
                    {
                        objResult[0] = true;
                        objResult[1] = "update successful";
                    }
                    else
                    {
                        objResult[1] = "update failed";
                    }
                }
            }
            catch (Exception ex)
            {
                objResult[1] = ex.Message;
            }
            return objResult;
        }

        /// <summary>
        /// 删除商品信息
        /// </summary>
        /// <param name="goodId">商品ID</param>
        /// <returns></returns>
        public object[] DeleteGoods(int goodId)
        {
            object[] objResult = new object[] { false, "" };
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update Goods set");
                strSql.Append(" status = -1");
                strSql.Append(" where id = @id");

                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@id", SqlDbType.Int)
                };
                para[0].Value = goodId;

                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();
                    int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), para);
                    if (i > 0)
                    {
                        objResult[0] = true;
                        objResult[1] = "delete successful";
                    }
                    else
                    {
                        objResult[1] = "delete failed";
                    }
                }
            }
            catch (Exception ex)
            {
                objResult[1] = ex.Message;
            }
            return objResult;
        }
        /// <summary>
        /// 修改当前商品库存，修改兑换数量（add by wangc）
        /// </summary>
        /// <param name="goodId"></param>
        /// <returns></returns>
        public bool UpdateGoodsResidueQuantity(int goodId)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update Goods set");
                strSql.Append(" residueQuantity = residueQuantity -1,haveExchangeQuantity=isnull( haveExchangeQuantity,0) +1");
                strSql.Append(" where id = @id");
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@id", SqlDbType.Int)
                };
                para[0].Value = goodId;
                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();
                    int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), para);
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
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 根据GoodId查询相应的商品信息
        /// </summary>
        /// <param name="goodId"></param>
        /// <returns></returns>
        public Goods QueryGoods(int goodId)
        {
            Goods goods = new Goods();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,name,pictureName,exchangePrice,residueQuantity,haveExchangeQuantity,remark,boolVisible,status");
            strSql.Append(" from Goods");
            strSql.Append(" where id = @id and status = 1");

            SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@id", SqlDbType.Int)
                };
            para[0].Value = goodId;

            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para))
            {
                if (sdr.Read())
                {
                    goods.id = sdr.GetInt32(0);
                    goods.name = sdr.GetString(1);
                    goods.pictureName = sdr.GetString(2);
                    goods.exchangePrice = sdr.GetDouble(3);
                    goods.residueQuantity = sdr.GetInt32(4);
                    goods.haveExchangeQuantity = sdr.GetInt32(5);
                    goods.remark = sdr.GetString(6);
                    goods.visible = sdr.GetBoolean(7);
                    goods.status = sdr.GetInt32(8);
                }
            }
            return goods;
        }

        /// <summary>
        /// 查询所有有效的商品数据
        /// </summary>
        /// <returns></returns>
        public IList<Goods> QueryGoods()
        {
            IList<Goods> Goods = new List<Goods>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,name,pictureName,exchangePrice,residueQuantity,haveExchangeQuantity,remark,boolVisible,status");
            strSql.Append(" from Goods");
            strSql.Append(" where status = 1 and boolVisible = 1");

            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString()))
            {
                while (sdr.Read())
                {
                    Goods good = new Goods();
                    good.id = sdr.GetInt32(0);
                    good.name = sdr.GetString(1);
                    good.pictureName = sdr.GetString(2);
                    good.exchangePrice = sdr.GetDouble(3);
                    good.residueQuantity = sdr.GetInt32(4);
                    good.haveExchangeQuantity = sdr.GetInt32(5);
                    good.remark = sdr.GetString(6);
                    good.visible = sdr.GetBoolean(7);
                    good.status = sdr.GetInt32(8);
                    Goods.Add(good);
                }
            }
            return Goods;
        }

        /// <summary>
        /// 根据商品名称查询符合的数据
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IList<Goods> QueryGoods(string name)
        {
            IList<Goods> Goods = new List<Goods>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,name,pictureName,exchangePrice,residueQuantity,haveExchangeQuantity,remark,boolVisible,status");
            strSql.Append(" from Goods");
            strSql.Append(" where status = 1 and name = @name");

            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("name", SqlDbType.NVarChar, 50)
            };
            para[0].Value = name;

            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para))
            {
                while (sdr.Read())
                {
                    Goods good = new Goods();
                    good.id = sdr.GetInt32(0);
                    good.name = sdr.GetString(1);
                    good.pictureName = sdr.GetString(2);
                    good.exchangePrice = sdr.GetDouble(3);
                    good.residueQuantity = sdr.GetInt32(4);
                    good.haveExchangeQuantity = sdr.GetInt32(5);
                    good.remark = sdr.GetString(6);
                    good.visible = sdr.GetBoolean(7);
                    good.status = sdr.GetInt32(8);
                    Goods.Add(good);
                }
            }
            return Goods;
        }

        /// <summary>
        /// 根据商品名称查询符合的数据
        /// </summary>
        /// <param name="name">商品名称</param>
        /// <returns></returns>
        public DataTable QueryGoodsDataTable(string name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,name,pictureName,exchangePrice,residueQuantity,haveExchangeQuantity,remark,boolVisible,status");
            strSql.Append(" from Goods");
            strSql.Append(" where status = 1");
            if (!string.IsNullOrEmpty(name))
            {
                strSql.AppendFormat(" and name like '%{0}%'", name);
            }

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
    }
}
