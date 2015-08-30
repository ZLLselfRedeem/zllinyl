﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using PagedList;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class DishManager
    {
        /// <summary>
        /// 新增菜名信息
        /// </summary>
        /// <param name="dish"></param>
        public int InsertDish(DishInfo dish)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                //SqlTransaction tran = null;
                Object obj = null;
                try
                {
                    conn.Open();
                    //tran = conn.BeginTransaction();

                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into DishInfo(");
                    strSql.Append("DiscountTypeID,MenuID,DishDisplaySequence,SendToKitchen,IsActive,DishStatus,DishTotalQuantity,cookPrinterName)");
                    strSql.Append(" values (");
                    strSql.Append("@DiscountTypeID,@MenuID,@DishDisplaySequence,@SendToKitchen,@IsActive,@DishStatus,@DishTotalQuantity,@cookPrinterName)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
                    new SqlParameter("@DiscountTypeID", SqlDbType.Int,4),
                    new SqlParameter("@MenuID", SqlDbType.Int,4),
                    new SqlParameter("@DishDisplaySequence",SqlDbType.Int,4),
                    new SqlParameter("@SendToKitchen", SqlDbType.Bit,1),
                    new SqlParameter("@IsActive",SqlDbType.Bit,1),
                    new SqlParameter("@DishStatus",SqlDbType.Int,4),
                    new SqlParameter("@DishTotalQuantity",SqlDbType.Float,8),
                    new SqlParameter("@cookPrinterName",SqlDbType.VarChar,50)};
                    parameters[0].Value = dish.DiscountTypeID;
                    parameters[1].Value = dish.MenuID;
                    parameters[2].Value = dish.DishDisplaySequence;
                    parameters[3].Value = dish.SendToKitchen;
                    parameters[4].Value = dish.IsActive;
                    parameters[5].Value = dish.DishStatus;
                    parameters[6].Value = dish.DishTotalQuantity;
                    parameters[7].Value = dish.cookPrinterName;
                    obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters);

                    //tran.Commit();
                }
                catch (Exception)
                {
                    //if (tran != null)
                    //{
                    //    tran.Rollback();
                    //}
                    return 0;
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
        /// 新增菜名多语言信息
        /// </summary>
        /// <param name="dish"></param>
        public int InsertDishI18n(DishI18n dish)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                //SqlTransaction tran = null;
                Object obj = null;
                try
                {
                    conn.Open();
                    //tran = conn.BeginTransaction();

                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into DishI18n(");
                    strSql.Append("DishID,LangID,DishName,DishDescShort,DishDescDetail,DishHistory,DishI18nStatus,dishQuanPin,dishJianPin)");
                    strSql.Append(" values (");
                    strSql.Append("@DishID,@LangID,@DishName,@DishDescShort,@DishDescDetail,@DishHistory,@DishI18nStatus,@dishQuanPin,@dishJianPin)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
                        //new SqlParameter("@DishI18nID", SqlDbType.Int,4),
                    new SqlParameter("@DishID", SqlDbType.Int,4),
                    new SqlParameter("@LangID", SqlDbType.Int,4),
                    new SqlParameter("@DishName",SqlDbType.NVarChar,500),
                    new SqlParameter("@DishDescShort", SqlDbType.NVarChar,500),
                    new SqlParameter("@DishDescDetail",SqlDbType.NVarChar),
                    new SqlParameter("@DishHistory",SqlDbType.NVarChar),
                    new SqlParameter("@DishI18nStatus",SqlDbType.Int,4),
                    new SqlParameter("@dishQuanPin",SqlDbType.NVarChar,500),
                    new SqlParameter("@dishJianPin",SqlDbType.NVarChar,500)};
                    //parameters[0].Value = dish.DishID;
                    parameters[0].Value = dish.DishID;
                    parameters[1].Value = dish.LangID;
                    parameters[2].Value = dish.DishName;
                    parameters[3].Value = dish.DishDescShort;
                    parameters[4].Value = dish.DishDescDetail;
                    parameters[5].Value = dish.DishHistory;
                    parameters[6].Value = dish.DishI18nStatus;
                    parameters[7].Value = dish.dishQuanPin;
                    parameters[8].Value = dish.dishJianPin;

                    obj = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

                    //tran.Commit();
                }
                catch (Exception)
                {
                    //if (tran != null)
                    //{
                    //    tran.Rollback();
                    //}
                    return 0;
                }
                if (obj == null && obj.ToString() == "")
                {
                    return 1;
                }
                else
                {
                    return Convert.ToInt32(obj);
                }
            }
        }
        /// <summary>
        /// 新增菜和显示分类关系信息
        /// </summary>
        /// <param name="dishConnType"></param>
        public int InsertDishConnType(DishConnType dishConnType)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                //SqlTransaction tran = null;
                Object obj = null;
                try
                {
                    conn.Open();
                    //tran = conn.BeginTransaction();

                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into DishConnType(");
                    strSql.Append("DishID,DishTypeID,DishConnTypeStatus)");
                    strSql.Append(" values (");
                    strSql.Append("@DishID,@DishTypeID,@DishConnTypeStatus)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
                    new SqlParameter("@DishID", SqlDbType.Int,4),
                    new SqlParameter("@DishTypeID", SqlDbType.Int,4),
                    new SqlParameter("@DishConnTypeStatus",SqlDbType.Int,4)};
                    parameters[0].Value = dishConnType.DishID;
                    parameters[1].Value = dishConnType.DishTypeID;
                    parameters[2].Value = dishConnType.DishConnTypeStatus;

                    obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters);

                    //tran.Commit();
                }
                catch (Exception)
                {
                    //if (tran != null)
                    //{
                    //    tran.Rollback();
                    //}
                    return 0;
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
        /// 查询某个分类下面是不是有这个菜
        /// </summary>
        /// <param name="dishConnType"></param>
        public DataTable SelectDishConnType(int dishTypeId, int dishId)
        {
            StringBuilder strSql = new StringBuilder();
            SqlParameter[] parameters = null;
            strSql.Append("select [DishConnTypeID],[DishID],[DishTypeID],[DishConnTypeStatus] from DishConnType where DishID=@DishID and DishTypeID=@DishTypeID");
            parameters = new SqlParameter[]{
                    new SqlParameter("@DishID", SqlDbType.Int,4),
                    new SqlParameter("@DishTypeID", SqlDbType.Int,4)};
            parameters[0].Value = dishId;
            parameters[1].Value = dishTypeId;
            DataSet dt = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            return dt.Tables[0];
        }

        /// <summary>
        /// 新增菜价格信息
        /// </summary>
        /// <param name="dishPrice"></param>
        public int InsertDishPrice(DishPriceInfo dishPrice)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into DishPriceInfo(");
                    strSql.Append("DishPrice,DishID,DishPriceStatus,vipDiscountable,backDiscountable,DishSoldout,DishNeedWeigh)");
                    strSql.Append(" values (");
                    strSql.Append("@DishPrice,@DishID,@DishPriceStatus,@vipDiscountable,@backDiscountable,@DishSoldout,@DishNeedWeigh)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
                        new SqlParameter("@DishPrice", SqlDbType.Float,8),
                        new SqlParameter("@DishID", SqlDbType.Int,4),
                        new SqlParameter("@DishPriceStatus", SqlDbType.Int,4),
                        new SqlParameter("@vipDiscountable", SqlDbType.Bit,1),
                    new SqlParameter("@backDiscountable", SqlDbType.Bit,1),
                    new SqlParameter("@DishSoldout", SqlDbType.Bit,1),
                    new SqlParameter("@DishNeedWeigh", SqlDbType.Bit,1)};
                    parameters[0].Value = dishPrice.DishPrice;
                    parameters[1].Value = dishPrice.DishID;
                    parameters[2].Value = dishPrice.DishPriceStatus;
                    parameters[3].Value = dishPrice.vipDiscountable;
                    parameters[4].Value = dishPrice.backDiscountable;
                    parameters[5].Value = dishPrice.DishSoldout;
                    parameters[6].Value = dishPrice.DishNeedWeigh;
                    //1、插入菜价格表信息
                    obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (Exception)
                {
                    return 0;
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
        /// 新增菜价格多语言信息
        /// </summary>
        /// <param name="dishPrice"></param>
        public int InsertDishPriceI18n(DishPriceI18n dishPrice)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();

                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into DishPriceI18n(");
                    strSql.Append("DishPriceI18nID, DishPriceID,LangID,ScaleName,DishPriceI18nStatus,markName)");
                    strSql.Append(" values (");
                    strSql.Append("@DishPriceI18nID,@DishPriceID,@LangID,@ScaleName,@DishPriceI18nStatus,@markName)");
                    //strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
                        new SqlParameter("@DishPriceI18nID", SqlDbType.Int,4),
                        new SqlParameter("@DishPriceID", SqlDbType.Int,4),
                        new SqlParameter("@LangID", SqlDbType.Int,4),
                        new SqlParameter("@ScaleName", SqlDbType.NVarChar,50),
                        new SqlParameter("@DishPriceI18nStatus", SqlDbType.Int,4),
                     new SqlParameter("@markName", SqlDbType.NVarChar,50)
                    };
                    parameters[0].Value = dishPrice.DishPriceID;
                    parameters[1].Value = dishPrice.DishPriceID;
                    parameters[2].Value = dishPrice.LangID;
                    parameters[3].Value = dishPrice.ScaleName;
                    parameters[4].Value = dishPrice.DishPriceI18nStatus;
                    parameters[5].Value = dishPrice.markName;

                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (Exception)
                {

                }
                return result;
            }
        }

        public int InsertDishImage(ImageInfo bigimage, ImageInfo smallimage)
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
                    strSql.Append("insert into ImageInfo(");
                    strSql.Append("DishID,ImageName,ImageSequence,ImageScale,ImageStatus)");
                    strSql.Append(" values (");
                    strSql.Append("@DishID,@ImageName,@ImageSequence,@ImageScale,@ImageStatus)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
                        new SqlParameter("@DishID", SqlDbType.Int,4),
                        new SqlParameter("@ImageName", SqlDbType.NVarChar,50),
                        new SqlParameter("@ImageSequence", SqlDbType.Int,4),
                        new SqlParameter("@ImageScale", SqlDbType.Int,4),
                        new SqlParameter("@ImageStatus", SqlDbType.Int,4)};
                    parameters[0].Value = bigimage.DishID;
                    parameters[1].Value = bigimage.ImageName;
                    parameters[2].Value = bigimage.ImageSequence;
                    parameters[3].Value = bigimage.ImageScale;
                    parameters[4].Value = bigimage.ImageStatus;


                    StringBuilder strSql2 = new StringBuilder();
                    SqlParameter[] parameters2 = null;
                    strSql2.Append("insert into ImageInfo(");
                    strSql2.Append("DishID,ImageName,ImageSequence,ImageScale,ImageStatus)");
                    strSql2.Append(" values (");
                    strSql2.Append("@DishID,@ImageName,@ImageSequence,@ImageScale,@ImageStatus)");
                    strSql2.Append(" select @@identity");
                    parameters2 = new SqlParameter[]{
                        new SqlParameter("@DishID", SqlDbType.Int,4),
                        new SqlParameter("@ImageName", SqlDbType.NVarChar,50),
                        new SqlParameter("@ImageSequence", SqlDbType.Int,4),
                        new SqlParameter("@ImageScale", SqlDbType.Int,4),
                        new SqlParameter("@ImageStatus", SqlDbType.Int,4)};
                    parameters2[0].Value = smallimage.DishID;
                    parameters2[1].Value = smallimage.ImageName;
                    parameters2[2].Value = smallimage.ImageSequence;
                    parameters2[3].Value = smallimage.ImageScale;
                    parameters2[4].Value = smallimage.ImageStatus;

                    obj = SqlHelper.ExecuteScalar(tran, CommandType.Text, strSql.ToString(), parameters);
                    obj = SqlHelper.ExecuteScalar(tran, CommandType.Text, strSql2.ToString(), parameters2);
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
        /// 新增菜图片信息
        /// </summary>
        /// <param name="image"></param>
        public int InsertDishImage(ImageInfo image)
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
                    strSql.Append("insert into ImageInfo(");
                    strSql.Append("DishID,ImageName,ImageSequence,ImageScale,ImageStatus)");
                    strSql.Append(" values (");
                    strSql.Append("@DishID,@ImageName,@ImageSequence,@ImageScale,@ImageStatus)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
                        new SqlParameter("@DishID", SqlDbType.Int,4),
                        new SqlParameter("@ImageName", SqlDbType.NVarChar,50),
                        new SqlParameter("@ImageSequence", SqlDbType.Int,4),
                        new SqlParameter("@ImageScale", SqlDbType.Int,4),
                        new SqlParameter("@ImageStatus", SqlDbType.Int,4)};
                    parameters[0].Value = image.DishID;
                    parameters[1].Value = image.ImageName;
                    parameters[2].Value = image.ImageSequence;
                    parameters[3].Value = image.ImageScale;
                    parameters[4].Value = image.ImageStatus;

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
        /// 新增菜视频信息
        /// </summary>
        /// <param name="video"></param>
        public int InsertDishVideo(VideoInfo video)
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
                    strSql.Append("insert into VideoInfo(");
                    strSql.Append("DishID,VideoName,VideoSequence,VideoScale,VideoStatus)");
                    strSql.Append(" values (");
                    strSql.Append("@DishID,@VideoName,@VideoSequence,@VideoScale,@VideoStatus)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
                        new SqlParameter("@DishID", SqlDbType.Int,4),
                        new SqlParameter("@VideoName", SqlDbType.NVarChar,50),
                        new SqlParameter("@VideoSequence", SqlDbType.Int,4),
                        new SqlParameter("@VideoScale", SqlDbType.Int,4),
                        new SqlParameter("@VideoStatus", SqlDbType.Int,4)};
                    parameters[0].Value = video.DishID;
                    parameters[1].Value = video.VideoName;
                    parameters[2].Value = video.VideoSequence;
                    parameters[3].Value = video.VideoScale;
                    parameters[4].Value = video.VideoStatus;

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
        /// 删除菜名信息（及对应的多语言信息，价格信息，图片信息，视频信息）
        /// </summary>
        /// <param name="dishID"></param>
        public bool DeleteDishByID(int dishID)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {

                SqlTransaction tran = null;
                int result = 0;
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();
                    StringBuilder strSql = new StringBuilder();

                    strSql.Append("update DishInfo set DishStatus = '-1' where DishID=@dishId;");//菜名称表
                    strSql.Append("update DishI18n set DishI18nStatus = '-1' where DishID=@dishId;");//菜名多语言称表
                    strSql.Append("update DishPriceInfo set DishPriceStatus = '-1' where DishID=@dishId;");//菜价格表
                    strSql.Append("update DishPriceI18n set DishPriceI18nStatus = '-1' where DishPriceID in (SELECT DishPriceID FROM DishPriceInfo where DishID=@dishId);");//菜价格多语言表
                    strSql.Append("update ImageInfo set ImageStatus = '-1' where DishID=@dishId;");//菜图片表
                    strSql.Append("update VideoInfo set VideoStatus = '-1' where DishID=@dishId;");//菜视频表
                    strSql.Append("update DishConnType set DishConnTypeStatus = '-1' where DishID=@dishId;");//菜价格多语言表

                    SqlParameter[] parameters = {					
                    new SqlParameter("@dishID", SqlDbType.Int,4)};
                    parameters[0].Value = dishID;

                    result = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, strSql.ToString(), parameters);

                    tran.Commit();
                }
                catch
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
                }
                if (result > 0)
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
        /// 删除菜名多语言信息
        /// </summary>
        /// <param name="dishI18nID"></param>
        public void DeleteDishI18nByID(int dishI18nID)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {

                SqlTransaction tran = null;
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();
                    StringBuilder strSql = new StringBuilder();

                    strSql.Append("update DishI18n set DishI18nStatus = '0' where DishI18nID=@dishI18nID;");//菜名多语言称表

                    SqlParameter[] parameters = {					
                    new SqlParameter("@dishI18nID", SqlDbType.Int,4)};
                    parameters[0].Value = dishI18nID;

                    SqlHelper.ExecuteNonQuery(tran, CommandType.Text, strSql.ToString(), parameters);

                    tran.Commit();
                }
                catch
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
                }
            }
        }

        /// <summary>
        /// 删除菜和显示分类关系信息
        /// </summary>
        /// <param name="dishI18nID"></param>
        public bool DeleteDishConnTypeByDishID(int dishID)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                SqlTransaction tran = null;
                int result = 0;
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("delete from DishConnType where DishID=@DishID;");
                    SqlParameter[] parameters = {					
                    new SqlParameter("@DishID", SqlDbType.Int,4)};
                    parameters[0].Value = dishID;
                    result = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, strSql.ToString(), parameters);
                    tran.Commit();
                }
                catch
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
                }
                if (result > 0)
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
        /// 删除菜和显示分类关系信息
        /// </summary>
        /// <param name="dishI18nID"></param>
        public bool DeleteDishConnTypeByID(int dishConnTypeID)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {

                SqlTransaction tran = null;
                int result = 0;
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();
                    StringBuilder strSql = new StringBuilder();

                    strSql.Append("update DishConnType set DishConnTypeStatus = '-1' where DishConnTypeID=@DishConnTypeID;");

                    SqlParameter[] parameters = {					
                    new SqlParameter("@DishConnTypeID", SqlDbType.Int,4)};
                    parameters[0].Value = dishConnTypeID;

                    result = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, strSql.ToString(), parameters);

                    tran.Commit();
                }
                catch
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
                }
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public string GetQueryDishPriceID(int dishPrice18nID)
        {
            StringBuilder strSql = new StringBuilder();


            strSql.AppendFormat("select DishPriceID from DishPriceI18n where DishPriceI18nID={0}", dishPrice18nID);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

            return ds.Tables[0].Rows[0]["DishPriceID"].ToString();

        }

        public bool DeleteDishPriceByDishID(string DishPriceIdString)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {

                SqlTransaction tran = null;
                int result = 0;
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();
                    StringBuilder strSql = new StringBuilder();
                    strSql.AppendFormat("update DishPriceInfo set DishPriceStatus = '-1' where DishPriceID in {0};", DishPriceIdString);

                    strSql.AppendFormat("update DishPriceI18n set DishPriceI18nStatus = '-1' where DishPriceI18nID in {0};", DishPriceIdString);

                    result = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, strSql.ToString());

                    tran.Commit();
                }
                catch
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
                }
                if (result > 0)
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
        /// 删除菜价信息
        /// </summary>
        /// <param name="dishPriceID"></param>
        public bool DeleteDishPriceByID(int DishPriceI18nID)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {

                SqlTransaction tran = null;
                int result = 0;
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();
                    StringBuilder strSql = new StringBuilder();
                    string dishPriceId = GetQueryDishPriceID(DishPriceI18nID);
                    strSql.AppendFormat("update DishPriceInfo set DishPriceStatus = '-1' where DishPriceID={0};", dishPriceId);
                    strSql.AppendFormat("update DishPriceI18n set DishPriceI18nStatus = '-1' where DishPriceI18nID={0};", DishPriceI18nID);

                    result = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, strSql.ToString());

                    tran.Commit();
                }
                catch
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
                }
                if (result > 0)
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
        /// 删除菜价多语言信息
        /// </summary>
        /// <param name="dishPriceI18nID"></param>
        public bool DeleteDishPriceI18nByID(int dishPriceI18nID)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {

                SqlTransaction tran = null;
                int result = 0;
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();
                    StringBuilder strSql = new StringBuilder();

                    strSql.Append("update DishPriceI18n set DishPriceI18nStatus = '-1' where DishPriceI18nID=@dishPriceI18nID;");

                    SqlParameter[] parameters = {					
                    new SqlParameter("@dishPriceI18nID", SqlDbType.Int,4)};
                    parameters[0].Value = dishPriceI18nID;

                    result = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, strSql.ToString(), parameters);

                    tran.Commit();
                }
                catch
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
                }
                if (result > 0)
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
        /// 删除菜图像信息
        /// </summary>
        /// <param name="imageID"></param>
        public bool DeleteDishImageByDishID(int DishID)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {

                SqlTransaction tran = null;
                int result = 0;
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();
                    StringBuilder strSql = new StringBuilder();

                    strSql.Append("update ImageInfo set ImageStatus = '-1' where DishID=@DishID;");//菜图片表

                    SqlParameter[] parameters = {					
                    new SqlParameter("@DishID", SqlDbType.Int,4)};
                    parameters[0].Value = DishID;

                    result = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, strSql.ToString(), parameters);

                    tran.Commit();
                }
                catch
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
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
        /// 删除菜图像信息
        /// </summary>
        /// <param name="imageID"></param>
        public bool DeleteDishImageByID(int imageID)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {

                SqlTransaction tran = null;
                int result = 0;
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();
                    StringBuilder strSql = new StringBuilder();

                    strSql.Append("update ImageInfo set ImageStatus = '0' where ImageID=@imageId;");//菜图片表

                    SqlParameter[] parameters = {					
                    new SqlParameter("@imageID", SqlDbType.Int,4)};
                    parameters[0].Value = imageID;

                    result = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, strSql.ToString(), parameters);

                    tran.Commit();
                }
                catch
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
                }
                if (result == 1)
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
        /// 删除菜视频信息
        /// </summary>
        /// <param name="videoID"></param>
        public bool DeleteDishVideoByID(int videoID)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {

                SqlTransaction tran = null;
                int result = 0;
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();
                    StringBuilder strSql = new StringBuilder();

                    strSql.Append("update VideoInfo set VideoStatus = '0' where VideoID=@videoID;");//菜视频表

                    SqlParameter[] parameters = {					
                    new SqlParameter("@videoID", SqlDbType.Int,4)};
                    parameters[0].Value = videoID;

                    result = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, strSql.ToString(), parameters);

                    tran.Commit();
                }
                catch
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
                }
                if (result == 1)
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
        /// 修改菜名称信息
        /// </summary>
        /// <param name="dish"></param>
        public bool UpdateDish(DishInfo dish)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update DishInfo set ");
            strSql.Append("DiscountTypeID=@DiscountTypeID,");
            strSql.Append("MenuID=@MenuID,");
            strSql.Append("DishDisplaySequence=@DishDisplaySequence,");
            strSql.Append("SendToKitchen=@SendToKitchen,");
            strSql.Append("IsActive=@IsActive,");
            strSql.Append("DishStatus=@DishStatus,");
            strSql.Append("cookPrinterName=@cookPrinterName,");
            strSql.Append("DishTotalQuantity=@DishTotalQuantity");
            strSql.Append(" where DishID=@DishID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@DiscountTypeID", SqlDbType.Int,4),
                    new SqlParameter("@MenuID", SqlDbType.Int,4),
                    new SqlParameter("@DishDisplaySequence",SqlDbType.Int,4),
                    new SqlParameter("@SendToKitchen", SqlDbType.Bit,1),
                    new SqlParameter("@IsActive",SqlDbType.Bit,1),
                    new SqlParameter("@DishStatus",SqlDbType.Int,4),
                    new SqlParameter("@DishTotalQuantity",SqlDbType.Float,8),
                    new SqlParameter("@DishID",SqlDbType.Int,4),
                    new SqlParameter("@cookPrinterName",SqlDbType.VarChar,50)};
            parameters[0].Value = dish.DiscountTypeID;
            parameters[1].Value = dish.MenuID;
            parameters[2].Value = dish.DishDisplaySequence;
            parameters[3].Value = dish.SendToKitchen;
            parameters[4].Value = dish.IsActive;
            parameters[5].Value = dish.DishStatus;
            parameters[6].Value = dish.DishTotalQuantity;
            parameters[7].Value = dish.DishID;
            parameters[8].Value = dish.cookPrinterName;
            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 修改菜名多语言信息
        /// </summary>
        /// <param name="dish"></param>
        public bool UpdateDishI18n(DishI18n dish)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update DishI18n set ");
            strSql.Append("DishID=@DishID,");
            strSql.Append("LangID=@LangID,");
            strSql.Append("DishName=@DishName,");
            strSql.Append("DishDescShort=@DishDescShort,");
            strSql.Append("DishDescDetail=@DishDescDetail,");
            strSql.Append("DishHistory=@DishHistory,");
            strSql.Append("dishQuanPin=@dishQuanPin,");
            strSql.Append("dishJianPin=@dishJianPin,");
            strSql.Append("DishI18nStatus=@DishI18nStatus");
            strSql.Append(" where DishI18nID=@DishI18nID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@DishID", SqlDbType.Int,4),
                    new SqlParameter("@LangID", SqlDbType.Int,4),
                    new SqlParameter("@DishName",SqlDbType.NVarChar,500),
                    new SqlParameter("@DishDescShort", SqlDbType.NVarChar,500),
                    new SqlParameter("@DishDescDetail",SqlDbType.NVarChar),
                    new SqlParameter("@DishHistory",SqlDbType.NVarChar),
                    new SqlParameter("@dishQuanPin", SqlDbType.NVarChar,500),
                    new SqlParameter("@dishJianPin", SqlDbType.NVarChar,500),
                    new SqlParameter("@DishI18nStatus",SqlDbType.Int,4),
                    new SqlParameter("@DishI18nID",SqlDbType.Int,4)};
            parameters[0].Value = dish.DishID;
            parameters[1].Value = dish.LangID;
            parameters[2].Value = dish.DishName;
            parameters[3].Value = dish.DishDescShort;
            parameters[4].Value = dish.DishDescDetail;
            parameters[5].Value = dish.DishHistory;
            parameters[6].Value = dish.dishQuanPin;
            parameters[7].Value = dish.dishJianPin;
            parameters[8].Value = dish.DishI18nStatus;
            parameters[9].Value = dish.DishI18nID;

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 修改菜和显示分类关系信息
        /// </summary>
        /// <param name="dishConnType"></param>
        public bool UpdateDishConnType(DishConnType dishConnType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update DishConnType set ");
            strSql.Append("DishID=@DishID,");
            strSql.Append("DishTypeID=@DishTypeID,");
            strSql.Append("DishConnTypeStatus=@DishConnTypeStatus");
            strSql.Append(" where DishConnTypeID=@DishConnTypeID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@DishID", SqlDbType.Int,4),
                    new SqlParameter("@DishTypeID",SqlDbType.Int,4),
                    new SqlParameter("@DishConnTypeStatus", SqlDbType.Int,4),
                    new SqlParameter("@DishConnTypeID",SqlDbType.Int,4)};
            parameters[0].Value = dishConnType.DishID;
            parameters[1].Value = dishConnType.DishTypeID;
            parameters[2].Value = dishConnType.DishConnTypeStatus;
            parameters[3].Value = dishConnType.DishConnTypeID;

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);

            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 修改菜价格信息
        /// </summary>
        /// <param name="dishPrice"></param>
        public bool UpdateDishPricesh(DishPriceInfo dishPrice)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update DishPriceInfo set ");
            strSql.Append("DishPrice=@DishPrice,");
            strSql.Append("DishID=@DishID,");
            //strSql.Append("DishSoldout=@DishSoldout,");
            strSql.Append("DishPriceStatus=@DishPriceStatus,");
            //strSql.Append("DishNeedWeigh=@DishNeedWeigh,");
            strSql.Append("vipDiscountable=@vipDiscountable,");
            strSql.Append("backDiscountable=@backDiscountable");
            strSql.Append(" where DishPriceID=@DishPriceID ");
            SqlParameter[] parameters = {
                        new SqlParameter("@DishPrice", SqlDbType.Float,8),
                        new SqlParameter("@DishID", SqlDbType.Int,4),
                       // new SqlParameter("@DishSoldout", SqlDbType.Bit,1),
                        new SqlParameter("@DishPriceStatus", SqlDbType.Int,4),
                       // new SqlParameter("@DishNeedWeigh", SqlDbType.Bit,1),
                        new SqlParameter("@vipDiscountable", SqlDbType.Bit,1),
                        new SqlParameter("@backDiscountable", SqlDbType.Bit,1),
                        new SqlParameter("@DishPriceID", SqlDbType.Int,4)};
            parameters[0].Value = dishPrice.DishPrice;
            parameters[1].Value = dishPrice.DishID;
            // parameters[2].Value = dishPrice.DishSoldout;
            parameters[2].Value = dishPrice.DishPriceStatus;
            //parameters[4].Value = dishPrice.DishNeedWeigh;
            parameters[3].Value = dishPrice.vipDiscountable;
            parameters[4].Value = dishPrice.backDiscountable;
            parameters[5].Value = dishPrice.DishPriceID;

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 修改菜价格信息
        /// </summary>
        /// <param name="dishPrice"></param>
        public bool UpdateDishPrice(DishPriceInfo dishPrice)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update DishPriceInfo set ");
            strSql.Append("DishPrice=@DishPrice,");
            strSql.Append("DishID=@DishID,");
            strSql.Append("DishSoldout=@DishSoldout,");
            strSql.Append("DishPriceStatus=@DishPriceStatus,");
            strSql.Append("DishNeedWeigh=@DishNeedWeigh,");
            strSql.Append("vipDiscountable=@vipDiscountable,");
            strSql.Append("backDiscountable=@backDiscountable");
            strSql.Append(" where DishPriceID=@DishPriceID ");
            SqlParameter[] parameters = {
                        new SqlParameter("@DishPrice", SqlDbType.Float,8),
                        new SqlParameter("@DishID", SqlDbType.Int,4),
                        new SqlParameter("@DishSoldout", SqlDbType.Bit,1),
                        new SqlParameter("@DishPriceStatus", SqlDbType.Int,4),
                        new SqlParameter("@DishNeedWeigh", SqlDbType.Bit,1),
                        new SqlParameter("@vipDiscountable", SqlDbType.Bit,1),
                        new SqlParameter("@backDiscountable", SqlDbType.Bit,1),
                        new SqlParameter("@DishPriceID", SqlDbType.Int,4)};
            parameters[0].Value = dishPrice.DishPrice;
            parameters[1].Value = dishPrice.DishID;
            parameters[2].Value = dishPrice.DishSoldout;
            parameters[3].Value = dishPrice.DishPriceStatus;
            parameters[4].Value = dishPrice.DishNeedWeigh;
            parameters[5].Value = dishPrice.vipDiscountable;
            parameters[6].Value = dishPrice.backDiscountable;
            parameters[7].Value = dishPrice.DishPriceID;

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 修改菜价多语言信息
        /// </summary>
        /// <param name="dishPrice"></param>
        public bool UpdateDishPriceI18n(DishPriceI18n dishPrice)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update DishPriceI18n set ");
            strSql.Append("DishPriceID=@DishPriceID,");
            strSql.Append("LangID=@LangID,");
            strSql.Append("ScaleName=@ScaleName,");
            strSql.Append("DishPriceI18nStatus=@DishPriceI18nStatus,");
            strSql.Append("markName=@markName");
            strSql.Append(" where DishPriceI18nID=@DishPriceI18nID ");
            SqlParameter[] parameters = {
                        new SqlParameter("@DishPriceID", SqlDbType.Int,4),
                        new SqlParameter("@LangID", SqlDbType.Int,4),
                        new SqlParameter("@ScaleName", SqlDbType.NVarChar,50),
                        new SqlParameter("@DishPriceI18nStatus", SqlDbType.Int,4),
                         new SqlParameter("@markName", SqlDbType.NVarChar,50),
                        new SqlParameter("@DishPriceI18nID", SqlDbType.Int,4)};
            parameters[0].Value = dishPrice.DishPriceID;
            parameters[1].Value = dishPrice.LangID;
            parameters[2].Value = dishPrice.ScaleName;
            parameters[3].Value = dishPrice.DishPriceI18nStatus;
            parameters[4].Value = dishPrice.markName;
            parameters[5].Value = dishPrice.DishPriceI18nID;
            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 修改菜图片信息
        /// </summary>
        /// <param name="image"></param>
        public bool UpdateDishImage(ImageInfo image)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ImageInfo set ");
            strSql.Append("DishID=@DishID,");
            strSql.Append("ImageName=@ImageName,");
            strSql.Append("ImageSequence=@ImageSequence,");
            strSql.Append("ImageScale=@ImageScale,");
            strSql.Append("ImageStatus=@ImageStatus");
            strSql.Append(" where ImageID=@ImageID ");
            SqlParameter[] parameters = {
                        new SqlParameter("@DishID", SqlDbType.Int,4),
                        new SqlParameter("@ImageName", SqlDbType.NVarChar),
                        new SqlParameter("@ImageSequence", SqlDbType.Int,4),
                        new SqlParameter("@ImageScale", SqlDbType.Int,4),
                        new SqlParameter("@ImageStatus", SqlDbType.Int,4),
                        new SqlParameter("@ImageID", SqlDbType.Int,4)};
            parameters[0].Value = image.DishID;
            parameters[1].Value = image.ImageName;
            parameters[2].Value = image.ImageSequence;
            parameters[3].Value = image.ImageScale;
            parameters[4].Value = image.ImageStatus;
            parameters[5].Value = image.ImageID;

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 修改菜视频信息
        /// </summary>
        /// <param name="video"></param>
        public bool UpdateDishVideo(VideoInfo video)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update VideoInfo set ");
            strSql.Append("DishID=@DishID,");
            strSql.Append("VideoName=@VideoName,");
            strSql.Append("VideoSequence=@VideoSequence,");
            strSql.Append("VideoScale=@VideoScale,");
            strSql.Append("VideoStatus=@VideoStatus");
            strSql.Append(" where VideoID=@VideoID ");
            SqlParameter[] parameters = {
                        new SqlParameter("@DishID", SqlDbType.Int,4),
                        new SqlParameter("@VideoName", SqlDbType.NVarChar),
                        new SqlParameter("@VideoSequence", SqlDbType.Int,4),
                        new SqlParameter("@VideoScale", SqlDbType.Int,4),
                        new SqlParameter("@VideoStatus", SqlDbType.Int,4),
                        new SqlParameter("@ImageID", SqlDbType.Int,4)};
            parameters[0].Value = video.DishID;
            parameters[1].Value = video.VideoName;
            parameters[2].Value = video.VideoSequence;
            parameters[3].Value = video.VideoScale;
            parameters[4].Value = video.VideoStatus;
            parameters[5].Value = video.VideoID;

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 根据菜名编号修改该菜的总销量
        /// </summary>
        /// <param name="dishID"></param>
        /// <param name="totalQuantity"></param>
        /// <returns></returns>
        public bool UpdateDishTotalQuantity(int dishID, double totalQuantity)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {

                SqlTransaction tran = null;
                int result = 0;
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();
                    StringBuilder strSql = new StringBuilder();

                    strSql.Append("update DishInfo set DishTotalQuantity =@DishTotalQuantity where DishID=@DishID;");

                    SqlParameter[] parameters = {
                    new SqlParameter("@DishTotalQuantity",SqlDbType.Float,8),
                    new SqlParameter("@DishID", SqlDbType.Int,4)};
                    parameters[0].Value = totalQuantity;
                    parameters[1].Value = dishID;

                    result = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, strSql.ToString(), parameters);

                    tran.Commit();
                }
                catch
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
                }
                if (result == 1)
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
        /// 查询所有菜名基本信息
        /// </summary>
        /// <returns></returns>
        public DataTable QueryDish()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" from [DishInfo],[DishI18n],[DishConnType]");
            strSql.Append(" where [DishInfo].DishID = [DishI18n].DishID and DishInfo.DishID = DishConnType.DishID");
            strSql.Append(" and [DishInfo].DishStatus > '0'");//由于显示分类DishType修改为关系表（一个菜对应多个显示分类）
            //因此，此处增加关系表DishConnType以查询菜的显示分类ID
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

            return ds.Tables[0];
        }
        public DataTable QueryDishSimple(int DishID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DishInfo.DishID");
            strSql.Append(" from [DishInfo],[DishI18n],[DishConnType]");
            strSql.Append(" where [DishInfo].DishID = [DishI18n].DishID and DishInfo.DishID = DishConnType.DishID");
            strSql.Append(" and DishInfo.DishID=@DishID and [DishInfo].DishStatus > 0");//由于显示分类DishType修改为关系表（一个菜对应多个显示分类）
            //因此，此处增加关系表DishConnType以查询菜的显示分类ID
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@DishID", SqlDbType.Int) { Value = DishID }
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para);

            return ds.Tables[0];
        }
        /// <summary>
        /// 查询所有菜名基本信息
        /// </summary>
        /// <returns></returns>
        public DataTable QueryDishByMenu(int menuId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct(DishInfo.DishID),DishTypeID,DishDescDetail,DishDescShort,DishName,dishQuanPin,");//1
            //strSql.Append("select DishTypeID,DishInfo.DishID,DishDescDetail,DishDescShort,DishName,dishQuanPin,");
            strSql.Append(" dishJianPin,DishTypeID,LangID,DishDisplaySequence,DishStatus,dishSalesIn19dian");
            //strSql.Append(" from [DishInfo],[DishI18n],[DishConnType]");
            strSql.Append(" from [DishInfo],[DishI18n],[DishConnType],DishPriceInfo");//2
            strSql.Append(" where [DishInfo].DishID = [DishI18n].DishID and DishInfo.DishID = DishConnType.DishID");
            strSql.Append(" and [DishInfo].DishStatus > '0' and DishConnType.DishConnTypeStatus >'0'");//由于显示分类DishType修改为关系表（一个菜对应多个显示分类）因此，此处增加关系表DishConnType以查询菜的显示分类ID
            strSql.Append(" and [DishInfo].DishID = DishPriceInfo.DishID");//3
            strSql.Append(" and DishPriceInfo.DishSoldout = 0 and DishPriceInfo.DishPriceStatus =1");//4  由于客户端暂时没有售罄功能，因此此处将售罄的菜过滤掉，要恢复时1、2行替换为注释的行，3、4行取消即可
            strSql.AppendFormat(" and [DishInfo].MenuID = {0}", menuId);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

            return ds.Tables[0];
        }

        /// <summary>
        /// 查询所有菜名基本信息
        /// </summary>
        /// <param name="menuId">菜谱ID</param>
        /// <param name="langId">语言ID</param>
        /// <returns></returns>
        public DataTable SelectDishAndDishPriceByMenuAndLang(int menuId, int langId)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@menuId",SqlDbType.Int,4),
                    new SqlParameter("@langId", SqlDbType.Int,4)};
            parameters[0].Value = menuId;
            parameters[1].Value = langId;

            StringBuilder strSql = new StringBuilder();
            //选择某个属于多个分类的菜bug修改
            strSql.Append("select distinct dishName,A.dishId,A.dishSalesIn19dian,dishPriceI18nID,scaleName,dishDescDetail,dishDescShort,dishPrice,dishJianPin,imageName,ImageScale from DishInfo A");
            // strSql.Append("select dishName,A.dishId,A.dishSalesIn19dian,dishPriceI18nID,scaleName,dishDescDetail,dishDescShort,dishPrice,dishJianPin,imageName,ImageScale,DishTypeID from DishInfo A");
            strSql.Append(" inner join DishI18n B on A.DishID=B.DishID");
            strSql.Append(" inner join DishConnType C on A.DishID=C.DishID");
            strSql.Append(" inner join DishPriceInfo D on A.DishID=D.DishID");
            strSql.Append(" inner join DishPriceI18n E on D.DishPriceID=E.DishPriceID");
            strSql.Append(" left join ImageInfo F on A.DishID=F.DishID");
            strSql.Append(" where  (A.DishStatus > '0'");
            strSql.Append(" and D.DishPriceStatus>0 ");
            strSql.Append(" and E.DishPriceI18nStatus>0 ");
            strSql.Append(" and (isnull(F.ImageStatus,1)=1) ");
            strSql.Append(" and  A.MenuID=@menuId");
            strSql.Append(" and  B.LangID=@langId)");
            //过滤掉不享受vip折扣的菜（不展示）//2013/9/7 modefy by wangcheng//过滤掉售罄的菜
            strSql.AppendFormat(" and D.vipDiscountable={0}", 1);
            strSql.AppendFormat(" and D.DishSoldout={0}", 0);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据菜名编号查询菜名基本信息
        /// </summary>
        /// <returns></returns>
        public DataTable QueryDish(int dishID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [DishInfo].[DishID],[DiscountTypeID],[MenuID],[DishDisplaySequence],");
            strSql.Append("[SendToKitchen],[IsActive],[DishTotalQuantity],[DishStatus],[InterfaceID],");
            strSql.Append("[cookPrinterName],[cookOrderCopy],[DishI18nID],[LangID],[DishName],");
            strSql.Append("[DishDescShort],[DishDescDetail],[DishHistory],[DishI18nStatus],[dishQuanPin],");
            strSql.Append("[dishJianPin],[DishConnTypeID],[DishTypeID],[DishConnTypeStatus]");
            strSql.Append(" from [DishInfo],[DishI18n],[DishConnType]");
            strSql.Append(" where [DishInfo].DishID = [DishI18n].DishID and DishInfo.DishID = DishConnType.DishID");
            strSql.AppendFormat(" and DishInfo.DishID = '{0}' and [DishInfo].DishStatus > '0'", dishID);//由于显示分类DishType修改为关系表（一个菜对应多个显示分类）
            //因此，此处增加关系表DishConnType以查询菜的显示分类ID
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

            return ds.Tables[0];
        }
        /// <summary>
        /// 根据菜名编号查询菜价信息
        /// </summary>
        /// <param name="dishID"></param>
        /// <returns></returns>
        public DataTable QueryDishPrice(int dishID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [DishPriceInfo].[DishPriceID],[DishPrice],[DishID],[vipDiscountable],");
            strSql.Append("[DishSoldout],[DishPriceStatus],[DishNeedWeigh],[DishPriceI18nID],[LangID],DishPriceInfo.[ScaleName],[DishPriceI18nStatus],markName,isnull(backDiscountable,1) as backDiscountable,dishIngredientsMinAmount,dishIngredientsMaxAmount");
            strSql.Append(" from DishPriceInfo,DishPriceI18n");//xiaoyu删除默认语言过滤，由调用处自行过滤
            strSql.Append(" where DishPriceInfo.DishPriceID = DishPriceI18n.DishPriceID ");// and DishPriceI18n.LangID ='1'
            strSql.AppendFormat(" and DishPriceInfo.DishID = '{0}' and [DishPriceInfo].DishPriceStatus > '0'", dishID);

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据菜名编号查询菜图片信息
        /// </summary>
        /// <param name="dishID"></param>
        /// <returns></returns>
        public DataTable QueryDishImage(int dishID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DishInfo.[DishID],[DiscountTypeID],[MenuID],[DishDisplaySequence],[SendToKitchen],[IsActive],");
            strSql.Append("[DishTotalQuantity],[DishStatus],[InterfaceID],[cookPrinterName],[cookOrderCopy],[DishI18nID],");
            strSql.Append("[LangID],[DishName],[DishDescShort],[DishDescDetail],[DishHistory],[DishI18nStatus],[dishQuanPin],[dishJianPin],");
            strSql.Append("[ImageID],[ImageName],[ImageSequence],[ImageScale],[ImageStatus]");
            strSql.Append(" from DishInfo,DishI18n,ImageInfo");
            strSql.Append(" where DishI18n.DishID=DishInfo.DishID and ImageInfo.DishID=DishInfo.DishID");
            strSql.AppendFormat(" and DishInfo.DishID = '{0}' and [ImageInfo].ImageStatus > '0' order by ImageInfo.ImageSequence", dishID);

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据菜名编号,规格查询菜图片信息
        /// </summary>
        /// <param name="dishID"></param>
        /// <param name="ImageScale"></param>
        /// <returns></returns>
        public DataTable QueryDishImage(int dishID, int ImageScale)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DishInfo.[DishID],[DiscountTypeID],[MenuID],[DishDisplaySequence],[SendToKitchen],[IsActive],");
            strSql.Append("[DishTotalQuantity],[DishStatus],[InterfaceID],[cookPrinterName],[cookOrderCopy],[DishI18nID],");
            strSql.Append("[LangID],[DishName],[DishDescShort],[DishDescDetail],[DishHistory],[DishI18nStatus],[dishQuanPin],[dishJianPin],");
            strSql.Append("[ImageID],[ImageName],[ImageSequence],[ImageScale],[ImageStatus],ImageXY");
            strSql.Append(" from DishInfo,DishI18n,ImageInfo");
            strSql.Append(" where DishI18n.DishID=DishInfo.DishID and ImageInfo.DishID=DishInfo.DishID");
            strSql.AppendFormat(" and DishInfo.DishID = '{0}' and [ImageInfo].ImageStatus > '0' and ImageInfo.ImageScale ='{1}' order by ImageInfo.ImageSequence", dishID, ImageScale);

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];

        }
        /// <summary>
        /// 判断是否需要更新图片
        /// </summary>
        /// <param name="imageID"></param>
        /// <returns></returns>
        public bool IsUpdateImg(int imageID)
        {
            bool ret = false;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ImageID ").Append("from ImageInfo ").AppendFormat("where ImageID={0}", imageID);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                ret = true;

            }
            return ret;

        }
        /// <summary>
        /// 根据菜名编号查询菜视频信息
        /// </summary>
        /// <param name="dishID"></param>
        /// <returns></returns>
        public DataTable QueryDishVideo(int dishID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DishInfo.[DishID],[DiscountTypeID],[MenuID],[DishDisplaySequence],[SendToKitchen],[IsActive],");
            strSql.Append("[DishTotalQuantity],[DishStatus],[InterfaceID],[cookPrinterName],[cookOrderCopy],[DishI18nID],");
            strSql.Append("[LangID],[DishName],[DishDescShort],[DishDescDetail],[DishHistory],[DishI18nStatus],[dishQuanPin],[dishJianPin]");
            strSql.Append("[VideoID],[VideoName],[VideoSequence],[VideoScale],[VideoStatus]");
            strSql.Append(" from DishInfo,DishI18n,VideoInfo");//暂时只支持每个菜一个视频，xiaoyu
            strSql.Append(" where DishI18n.DishID=DishInfo.DishID and VideoInfo.DishID=DishInfo.DishID");
            strSql.AppendFormat(" and DishInfo.DishID = '{0}' and [VideoInfo].VideoStatus > '0'", dishID);//order by VideoInfo.VideoSequence

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据菜名多语言编号查询菜编号
        /// </summary>
        /// <param name="dishI18nID"></param>
        /// <returns></returns>
        public int QueryDishID(int dishI18nID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DishID");
            strSql.Append(" from DishI18n");
            strSql.AppendFormat(" where DishI18nID = '{0}' and DishI18nStatus > '0'", dishI18nID);

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
        }
        /// <summary>
        /// 根据菜价多语言编号查询菜价编号
        /// </summary>
        /// <param name="dishPriceI18nID"></param>
        /// <returns></returns>
        public int QueryDishPriceID(int dishPriceI18nID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DishPriceID");
            strSql.Append(" from DishPriceI18n");
            strSql.AppendFormat(" where DishPriceI18nID = '{0}' and DishPriceI18nStatus > '0'", dishPriceI18nID);

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
        }
        /// <summary>
        /// 根据菜价多语言编号查询该规格的菜的所有信息
        /// </summary>
        /// <param name="dishPriceI18nID"></param>
        /// <returns></returns>
        public DataTable QueryDishScale(int dishPriceI18nID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DishNeedWeigh,DishPrice,DishName,ScaleName,DishInfo.DishID");
            strSql.Append(" from DishInfo INNER JOIN");
            strSql.Append(" DishI18n ON DishInfo.DishID = DishI18n.DishID INNER JOIN");
            strSql.Append(" DishPriceInfo ON DishInfo.DishID = DishPriceInfo.DishID INNER JOIN");
            strSql.Append(" DishPriceI18n ON DishPriceInfo.DishPriceID = DishPriceI18n.DishPriceID");
            strSql.Append(" where DishI18n.LangID = DishPriceI18n.LangID and DishPriceI18nStatus > '0'");
            strSql.AppendFormat(" and DishPriceI18n.DishPriceI18nID = '{0}'", dishPriceI18nID);

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据菜价多语言编号和用户等级编号查询该菜的折扣信息
        /// </summary>
        /// <param name="disPriceI18nID"></param>
        /// <param name="customerRankID"></param>
        /// <returns></returns>
        public DataTable QueryDishDiscount(int disPriceI18nID, int customerRankID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DiscountValue,RankConnDisStatus");
            strSql.Append(" FROM DishPriceInfo INNER JOIN");
            strSql.Append(" DishPriceI18n ON DishPriceInfo.DishPriceID = DishPriceI18n.DishPriceID INNER JOIN");
            strSql.Append(" DishInfo ON DishPriceInfo.DishID = DishInfo.DishID INNER JOIN");
            strSql.Append(" RankConnDiscount ON DishInfo.DiscountTypeID = RankConnDiscount.DiscountTypeID");
            strSql.AppendFormat(" where DishPriceI18n.DishPriceI18nID = '{0}' and RankConnDiscount.CustomerRankID = '{1}'", disPriceI18nID, customerRankID);

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据菜的多语言编号查询对应的图片的存放路径
        /// </summary>
        /// <param name="dishPriceI18nId"></param>
        /// <param name="imageScale"></param>
        /// <returns></returns>
        public DataTable SelectDishImagePath(int dishPriceI18nId, int imageScale)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT D.menuImagePath,E.ImageName");
            strSql.Append(" FROM DishPriceI18n as A inner join DishPriceInfo as B on A.DishPriceID = B.DishPriceID");
            strSql.Append(" inner join DishInfo as C on B.DishID = C.DishID");
            strSql.Append(" inner join MenuInfo as D on D.MenuID = C.MenuID");
            strSql.Append(" inner join ImageInfo as E on E.DishID = B.DishID");
            if (imageScale == 1)
            {
                strSql.AppendFormat(" where A.DishPriceI18nID = '{0}' and E.ImageScale = '1'", dishPriceI18nId);
            }
            else
            {
                strSql.AppendFormat(" where A.DishPriceI18nID = '{0}' and E.ImageScale = '0'", dishPriceI18nId);
            }
            strSql.Append(" and A.DishPriceI18nStatus > 0 and B.DishPriceStatus > 0 and C.DishStatus > 0");
            strSql.Append(" and D.MenuStatus > 0 and E.ImageStatus > 0");

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据菜谱编号查询菜信息
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public DataTable SelectDishInfo(int menuId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select C.DishPriceI18nID as id, a.DishName,c.ScaleName from DishI18n a,DishInfo b,DishPriceI18n c,DishPriceInfo d");
            strSql.AppendFormat(" where b.DishID=a.DishID and b.MenuID={0} and d.DishID=a.DishID and C.DishPriceID=D.DishPriceID", menuId);
            strSql.Append(" and c.DishPriceI18nStatus > 0 and a.DishI18nStatus>0 and b.DishStatus > 0 and d.DishPriceStatus > 0 order by a.dishName asc,c.ScaleName asc");

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 配料是否支持折扣
        /// </summary>
        /// <param name="ingredientsId"></param>
        /// <returns></returns>
        public bool IsIngredientsVipDiscountable(int ingredientsId)
        {
            const string strSql = @"select isnull(vipDiscountable,1) as vipDiscountable from DishIngredients where ingredientsId=@ingredientsId";
            SqlParameter[] parameter = new SqlParameter[] { new SqlParameter("ingredientsId", ingredientsId) };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameter))
            {
                if (dr.Read())
                {
                    return dr[0] == DBNull.Value ? false : Convert.ToBoolean(dr[0]);
                }
            }
            return false;
        }

        /// <summary>
        /// 该配料是否支持返送
        /// </summary>
        /// <param name="ingredientsId"></param>
        /// <returns></returns>
        public bool IsIngredientsPayBackable(int ingredientsId)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select isnull(backDiscountable,1) as backDiscountable from DishIngredients ");
                strSql.AppendFormat(" where ingredientsId={0}", ingredientsId);

                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
                if (ds.Tables[0].Rows.Count == 1)
                {
                    if (Convert.ToBoolean(ds.Tables[0].Rows[0]["backDiscountable"]))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception)
            {
                return false;
            }

        }

        /// <summary>
        /// 判断该规格是否支持返送
        /// </summary>
        /// <param name="dishPriceI18nId"></param>
        /// <returns></returns>
        public bool IsDishPayBackable(int dishPriceI18nId)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select isnull(B.backDiscountable,1) as backDiscountable from DishPriceI18n as A inner join DishPriceInfo as B on A.DishPriceID = B.DishPriceID");
                strSql.AppendFormat(" where A.DishPriceI18nID={0}", dishPriceI18nId);

                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
                if (ds.Tables[0].Rows.Count == 1)
                {
                    if (Convert.ToBoolean(ds.Tables[0].Rows[0]["backDiscountable"]))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception)
            {
                return false;
            }

        }
        /// <summary>
        /// 判断指定的规格是否享受Vip折扣
        /// </summary>
        /// <param name="dishPriceI18nId"></param>
        /// <returns></returns>
        public bool IsDishScaleVipDiscountable(int dishPriceI18nId)
        {
            const string strSql = @"select isnull(B.vipDiscountable,1) as vipDiscountable from DishPriceI18n as A inner join DishPriceInfo as B on A.DishPriceID = B.DishPriceID where A.DishPriceI18nID=@DishPriceI18nID";
            SqlParameter[] parameter = new SqlParameter[] { new SqlParameter("DishPriceI18nID", dishPriceI18nId) };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameter))
            {
                if (dr.Read())
                {
                    return dr[0] == DBNull.Value ? false : Convert.ToBoolean(dr[0]);
                }
            }
            return false;
        }
        public string QueryDishIsAddorUpdate(string DishPriceID)
        {
            try
            {
                DishPriceID = DishPriceID.Replace("undefined", "''");
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select DishPriceID from DishPriceInfo");
                strSql.AppendFormat(" where DishPriceID={0}", DishPriceID);
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

                if (ds.Tables[0].Rows.Count == 1)
                {
                    return ds.Tables[0].Rows[0]["DishPriceID"].ToString();
                }
                else
                {
                    return "";
                }
            }
            catch (System.Exception)
            {
                return "";
            }
        }
        #region （wangcheng）沽清模块
        /// <summary>
        /// 查询常用沽清菜信息
        /// </summary>
        public DataTable SelectCommonCurrentSellOffInfo(int menuId, int DishPriceI18nID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CommonCurrentSellOffInfo.menuId,CommonCurrentSellOffInfo.DishI18nID,DishI18n.DishName,");
            strSql.Append(" DishPriceI18n.ScaleName,DishPriceInfo.DishPrice,DishPriceI18n.DishPriceI18nID from CommonCurrentSellOffInfo");
            strSql.Append(" inner join DishI18n on CommonCurrentSellOffInfo.DishI18nID=DishI18n.DishI18nID");
            strSql.Append(" inner join DishPriceInfo on DishI18n.DishID=DishPriceInfo.DishID");
            strSql.Append(" inner join DishPriceI18n on DishPriceI18n.DishPriceID=DishPriceInfo.DishPriceID");
            strSql.AppendFormat(" where CommonCurrentSellOffInfo.DishPriceI18nID=DishPriceI18n.DishPriceI18nID and CommonCurrentSellOffInfo.menuId='{0}'", menuId);
            strSql.Append(" and DishPriceInfo.DishSoldout=0 and DishPriceInfo.DishPriceStatus=1");
            if (DishPriceI18nID != 0)
            {
                strSql.AppendFormat(" and DishPriceI18n.DishPriceI18nID='{0}'", DishPriceI18nID);
            }
            strSql.Append("  order by CommonCurrentSellOffInfo.currentSellOffCount desc");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 新建沽清信息
        /// </summary>
        public long InsertCurrentSellOffInfoTable(CurrentSellOffInfo currentSellOffInfo)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into [CurrentSellOffInfo](");
                    strSql.Append(" [companyId],[shopId],[menuId],[DishI18nID],[status],[DishPriceI18nID],[expirationTime],operateEmployeeId)");
                    strSql.Append(" values (");
                    strSql.Append(" @companyId,@shopId,@menuId,@DishI18nID,@status,@DishPriceI18nID,@expirationTime,@operateEmployeeId)");
                    strSql.Append(" select @@identity");

                    parameters = new SqlParameter[]{
                            new SqlParameter("@companyId", SqlDbType.Int,4),
                            new SqlParameter("@shopId",SqlDbType.Int,4),
                             new SqlParameter("@menuId",SqlDbType.Int,4),
                              new SqlParameter("@DishI18nID",SqlDbType.Int,4),
                            new SqlParameter("@status",SqlDbType.Int,4),
                    new SqlParameter("@DishPriceI18nID",SqlDbType.NVarChar,50),
                     new SqlParameter("@expirationTime",SqlDbType.DateTime),
                     new SqlParameter("@operateEmployeeId",SqlDbType.Int,4)};

                    parameters[0].Value = currentSellOffInfo.companyId;
                    parameters[1].Value = currentSellOffInfo.shopId;
                    parameters[2].Value = currentSellOffInfo.menuId;
                    parameters[3].Value = currentSellOffInfo.DishI18nID;
                    parameters[4].Value = currentSellOffInfo.status;
                    parameters[5].Value = currentSellOffInfo.DishPriceI18nID;
                    parameters[6].Value = currentSellOffInfo.expirationTime;
                    parameters[7].Value = currentSellOffInfo.operateEmployeeId;
                    obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (Exception)
                {
                    return 0;
                }
                if (obj == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt64(obj);
                }
            }
        }
        /// <summary>
        /// 新建常用沽清信息
        /// </summary>
        public long InsertCommonCurrentSellOffInfoTable(CommonCurrentSellOffInfo commonCurrentSellOffInfo)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into [CommonCurrentSellOffInfo](");
                    strSql.Append(" [DishI18nID],[menuId],[currentSellOffCount],[status],[DishPriceI18nID])");
                    strSql.Append(" values (");
                    strSql.Append(" @DishI18nID,@menuId,@currentSellOffCount,@status,@DishPriceI18nID)");
                    strSql.Append(" select @@identity");

                    parameters = new SqlParameter[]{
                            new SqlParameter("@DishI18nID",SqlDbType.Int,4),
                             new SqlParameter("@menuId",SqlDbType.Int,4),
                              new SqlParameter("@currentSellOffCount",SqlDbType.Int,4),
                            new SqlParameter("@status",SqlDbType.Int,4),
                    new SqlParameter("@DishPriceI18nID",SqlDbType.NVarChar,50) };

                    parameters[0].Value = commonCurrentSellOffInfo.DishI18nID;
                    parameters[1].Value = commonCurrentSellOffInfo.menuId;
                    parameters[2].Value = commonCurrentSellOffInfo.currentSellOffCount;
                    parameters[3].Value = commonCurrentSellOffInfo.status;
                    parameters[4].Value = commonCurrentSellOffInfo.DishPriceI18nID;
                    obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (Exception)
                {
                    return 0;
                }
                if (obj == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt64(obj);
                }
            }
        }
        /// <summary>
        /// 修改沽清次数
        /// </summary>
        public bool UpdateCurrentSellOffCount(int DishI18nID, int DishPriceI18nID)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update CommonCurrentSellOffInfo set currentSellOffCount =currentSellOffCount+1");
                    strSql.Append("  where DishPriceI18nID=@DishPriceI18nID and DishI18nID=@DishI18nID;");
                    SqlParameter[] parameters = {
                    new SqlParameter("@DishPriceI18nID",SqlDbType.Int,4),
                    new SqlParameter("@DishI18nID", SqlDbType.Int,4)};
                    parameters[0].Value = DishPriceI18nID;
                    parameters[1].Value = DishI18nID;
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch
                {
                    result = 0;
                }
                if (result == 1)
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
        /// 根据DishPriceI18nID和shopId删除沽清信息
        /// </summary>
        public bool DeleteCurrentSellOffInfo(int DishPriceI18nID, int shopId, int operateEmployeeId)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update CurrentSellOffInfo set status=-1, operateTime=getdate(), operateEmployeeId=@operateEmployeeId where DishPriceI18nID=@DishPriceI18nID and shopId=@shopId;");
                    SqlParameter[] parameters = {
                    new SqlParameter("@DishPriceI18nID",SqlDbType.Int,4),
                    new SqlParameter("@shopId",SqlDbType.Int,4),
                                                 new SqlParameter("@operateEmployeeId",SqlDbType.Int,4)};
                    parameters[0].Value = DishPriceI18nID;
                    parameters[1].Value = shopId;
                    parameters[2].Value = operateEmployeeId;
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch
                {
                    result = 0;
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
        /// 删除本菜谱本门店全部沽清信息
        /// </summary>
        public bool DeleteAllCurrentSellOffInfo(int shopId, int menuId, int operateEmployeeId)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update CurrentSellOffInfo set status=-1, operateTime=getdate(), operateEmployeeId=@operateEmployeeId where shopId=@shopId and menuId=@menuId;");
                    SqlParameter[] parameters = {
                    new SqlParameter("@shopId",SqlDbType.Int,4),
                    new SqlParameter("@menuId", SqlDbType.Int,4), new SqlParameter("@operateEmployeeId", SqlDbType.Int,4)};
                    parameters[0].Value = shopId;
                    parameters[1].Value = menuId;
                    parameters[2].Value = operateEmployeeId;
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch
                {
                    result = 0;
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
        /// 全部取消沽清
        /// </summary>
        /// <returns></returns>
        public bool DeleteAllCurrentSellOffInfo()
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                const string strSql = "delete FROM  CurrentSellOffInfo;";
                SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql);
                return true;
            }
        }

        /// <summary>
        /// 查询沽清菜信息
        /// </summary>
        public DataTable SelectCurrentSellOffInfo(int menuId, int shopId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct CurrentSellOffInfo.menuId,CurrentSellOffInfo.DishI18nID,CurrentSellOffInfo.status,CurrentSellOffInfo.companyId,");
            strSql.Append(" CurrentSellOffInfo.shopId,DishI18n.DishName,DishPriceI18n.ScaleName,DishPriceInfo.DishPrice,DishPriceI18n.DishPriceI18nID from CurrentSellOffInfo");
            strSql.Append(" inner join DishI18n on CurrentSellOffInfo.DishI18nID=DishI18n.DishI18nID");
            strSql.Append(" inner join DishPriceInfo on DishI18n.DishID=DishPriceInfo.DishID");
            strSql.Append(" inner join DishPriceI18n on DishPriceI18n.DishPriceID=DishPriceInfo.DishPriceID");
            strSql.Append(" inner join DishInfo on DishInfo.DishID=DishI18n.DishI18nID");

            strSql.AppendFormat(" where CurrentSellOffInfo.DishPriceI18nID=DishPriceI18n.DishPriceI18nID and CurrentSellOffInfo.menuId='{0}' and CurrentSellOffInfo.shopId='{1}'", menuId, shopId);
            strSql.Append(" and DishPriceInfo.DishSoldout=0 and DishPriceInfo.DishPriceStatus=1");
            strSql.Append("  and CurrentSellOffInfo.status=1 and CurrentSellOffInfo.expirationTime>GETDATE()");//增长状态和过期时间过滤
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 查询菜信息
        /// </summary>
        public DataTable SelectDishInformation(string dishFilter, int menuId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct B.DishName,B.DishI18nID,C.ScaleName,D.DishPrice,A.MenuID as menuId,C.DishPriceI18nID from DishInfo A");
            strSql.Append(" inner join DishI18n B on A.DishID=B.DishID");
            strSql.Append(" inner join DishPriceInfo D on A.DishID=D.DishID");
            strSql.Append(" inner join DishPriceI18n C on D.DishPriceID=C.DishPriceID");
            strSql.Append(" where ( B.DishName like '%" + dishFilter + "%' or B.dishJianPin like '%" + dishFilter + "%')");
            strSql.AppendFormat(" and A.MenuID='{0}'", menuId);
            strSql.Append(" and D.DishSoldout=0 and D.DishPriceStatus=1");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        #region 沽清接口
        /// <summary>
        /// 查询当前门店所有被沽清菜品编号
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public List<int> SelectCurrentSellOffInfoByShopId(int shopId)
        {
            const string strSql = @"select DishPriceI18nID  from CurrentSellOffInfo where shopId=@shopId and  CurrentSellOffInfo.status=1 and CurrentSellOffInfo.expirationTime>GETDATE()";
            SqlParameter[] parameter = new SqlParameter[] { new SqlParameter("shopId", shopId) };
            var list = new List<int>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameter))
            {
                while (dr.Read())
                {
                    list.Add(dr["DishPriceI18nID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["DishPriceI18nID"]));
                }
            }
            return list;
        }
        #endregion
        #endregion

        /// <summary>
        /// 判断菜名是否相同
        /// </summary>
        /// <param name="menuID"></param>
        /// <param name="DishName"></param>
        /// <returns></returns>
        public bool IsCanQueryThisDish(int menuID, string DishName)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select DishI18n.DishName from DishInfo,DishI18n where DishInfo.DishID =DishI18n.DishID ");
                strSql.AppendFormat(" and DishInfo.MenuID= {0} and DishI18n.DishName ='{1}' and  DishInfo.DishStatus ='1'", menuID, DishName);

                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (System.Exception)
            {
                return true;
            }

        }

        #region 菜品页面操作
        /// <summary>
        /// 获取菜品数据（联合字典表）
        /// </summary>
        /// <param name="dishID"></param>
        public static SybDishInfo GetDishInfo(int dishID)
        {
            SybDishInfo data = null;
            string strsql = string.Format(@"SELECT DishInfo.DishID,DishInfo.MenuId, DishI18n.DishName, DishI18n.dishQuanPin, DishI18n.dishJianPin
                      , DishI18n.DishDescShort, DishI18n.DishDescDetail, DishInfo.DishDisplaySequence
                      FROM DishInfo LEFT JOIN DishI18n ON DishInfo.DishID = DishI18n.DishID where DishInfo.DishID=@dishID and DishInfo.DishStatus=1");
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@dishID", SqlDbType.Int) { Value = dishID }
            };

            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, para))
            {
                if (dr.Read())
                {
                    data = new SybDishInfo();
                    data.menuId = dr["MenuId"].ToString() == "" ? 0 : Convert.ToInt32(dr["MenuId"]);
                    data.DishID = dr["DishID"].ToString() == "" ? 0 : Convert.ToInt32(dr["DishID"]);
                    data.DishName = dr["DishName"].ToString();
                    data.dishQuanPin = dr["dishQuanPin"].ToString();
                    data.dishJianPin = dr["dishJianPin"].ToString();
                    data.DishDescShort = dr["DishDescShort"].ToString();
                    data.DishDescDetail = dr["DishDescDetail"].ToString();
                    data.DishDisplaySequence = dr["DishDisplaySequence"].ToString() == "" ? 0 : Convert.ToInt32(dr["DishDisplaySequence"]);
                    data.PicStatus = 1;
                    //TODO 图片信息
                    string imageBigSQL = "select ImageName from ImageInfo where DishID =" + dishID + " and ImageStatus =1 and ImageScale =0";
                    data.DishImageUrlBig = (CommonManager.GetFieldValue(imageBigSQL) ?? "").ToString();
                }
            }

            if (data != null)
            {
                data.DishPriceList = GetDishPriceInfoList(dishID);
                data.DishTypeList = DishConnTypeManager.ListType(dishID);
            }
            return data;
        }




        /// <summary>
        /// 获取菜品的规格信息
        /// </summary>
        /// <param name="dishID"></param>
        /// <returns></returns>
        public static List<SybDishPriceInfo> GetDishPriceInfoList(int dishID)
        {
            List<SybDishPriceInfo> list = new List<SybDishPriceInfo>();
            string strsql = string.Format(@"SELECT DishPriceInfo.DishPriceID,ROUND( DishPriceInfo.DishPrice,2) DishPrice, DishPriceInfo.DishID
, DishPriceInfo.DishSoldout, DishPriceInfo.DishNeedWeigh,DishPriceInfo.vipDiscountable, DishPriceInfo.backDiscountable
, DishPriceInfo.dishIngredientsMinAmount, DishPriceInfo.dishIngredientsMaxAmount,DishPriceI18n.ScaleName, DishPriceI18n.markName
FROM DishPriceInfo LEFT JOIN DishPriceI18n ON DishPriceInfo.DishPriceID = DishPriceI18n.DishPriceID
where DishPriceInfo.DishID=@dishID and DishPriceInfo.DishPriceStatus=1");
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@dishID", SqlDbType.Int) { Value = dishID }
            };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, para))
            {
                while (dr.Read())
                {
                    SybDishPriceInfo data = new SybDishPriceInfo();
                    data.DishPriceID = dr["DishPriceID"].ToString() == "" ? 0 : Convert.ToInt32(dr["DishPriceID"]);
                    data.DishPrice = dr["DishPrice"].ToString() == "" ? 0 : Convert.ToDouble(dr["DishPrice"]);
                    data.DishID = dr["DishID"].ToString() == "" ? 0 : Convert.ToInt32(dr["DishID"]);
                    data.DishSoldout = dr["DishSoldout"] == DBNull.Value ? false : Convert.ToBoolean(dr["DishSoldout"]);
                    data.DishNeedWeigh = dr["DishNeedWeigh"] == DBNull.Value ? false : Convert.ToBoolean(dr["DishNeedWeigh"]);
                    data.vipDiscountable = dr["vipDiscountable"] == DBNull.Value ? false : Convert.ToBoolean(dr["vipDiscountable"]);
                    data.backDiscountable = dr["backDiscountable"] == DBNull.Value ? false : Convert.ToBoolean(dr["backDiscountable"]);
                    data.dishIngredientsMinAmount = dr["dishIngredientsMinAmount"].ToString() == "" ? 0 : Convert.ToInt32(dr["dishIngredientsMinAmount"]);
                    data.dishIngredientsMaxAmount = dr["dishIngredientsMaxAmount"].ToString() == "" ? 0 : Convert.ToInt32(dr["dishIngredientsMaxAmount"]);
                    data.ScaleName = dr["ScaleName"].ToString();
                    data.markName = dr["markName"].ToString();
                    data.operStatus = OperStatus.Edit;
                    list.Add(data);
                }
            }

            //获取口味和配料
            if (list.Count > 0)
            {
                foreach (SybDishPriceInfo data in list)
                {
                    data.DishTasteList = DishPriceConnTasteManager.ListTaste(data.DishPriceID);
                    data.DishIngredientsList = DishPriceConnIngredientsManager.ListIngredients(data.DishPriceID);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取规格的口味
        /// </summary>
        /// <param name="dishPriceId"></param>
        /// <returns></returns>
        public static List<DishTaste> GetDishTasteList(int dishPriceId)
        {
            List<DishTaste> list = new List<DishTaste>();
            string strsql = @"select DishPriceConnTaste.tasteId,menuId,tasteName,tasteRemark,tasteSequence,tasteStatus 
                from DishPriceConnTaste LEFT JOIN DishTaste ON DishPriceConnTaste.tasteId = DishTaste.tasteId 
                where DishPriceConnTaste.dishPriceId=@dishPriceId and DishTaste.tasteStatus=1";
            var parm = new[] { new SqlParameter("@dishPriceId", dishPriceId) };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, parm))
            {
                while (dr.Read())
                {
                    DishTaste model = new DishTaste();
                    model.tasteId = !dr.IsDBNull(0) ? dr.GetInt32(0) : 0;
                    model.menuId = !dr.IsDBNull(1) ? dr.GetInt32(1) : 0;
                    model.tasteName = !dr.IsDBNull(2) ? dr.GetString(2) : string.Empty;
                    model.tasteRemark = !dr.IsDBNull(3) ? dr.GetString(3) : string.Empty;
                    model.tasteSequence = !dr.IsDBNull(4) ? dr.GetInt32(4) : 0;
                    model.tasteStatus = !dr.IsDBNull(5) ? dr.GetBoolean(5) : false;
                    list.Add(model);
                }
            }
            return list;
        }
        /// <summary>
        /// 查询配菜列表数据
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public static SybDishIngredientsList ListDishIngredients(int menuId, string ingredientsName, int pageSize, int pageIndex)
        {
            string sqlWhere = "menuId=" + menuId + " and ingredientsStatus=1";
            if (!string.IsNullOrEmpty(ingredientsName)) sqlWhere += "and ingredientsName like '%" + ingredientsName + "%'";//待处理 sql注入 
            PageQuery pageQuery = new PageQuery()
            {
                tableName = "DishIngredients",
                fields = "[ingredientsId],[ingredientsName],[ingredientsPrice],[vipDiscountable],[backDiscountable],[ingredientsSequence],2 as operStatus",
                orderField = "ingredientsSequence",
                sqlWhere = sqlWhere
            };
            Paging paging = new Paging()
            {
                pageIndex = pageIndex,
                pageSize = pageSize,
                recordCount = 0,
                pageCount = 0
            };
            SybDishIngredientsList data = new SybDishIngredientsList();
            data.list = CommonManager.GetPageData<SybDishIngredients>(SqlHelper.ConnectionStringLocalTransaction, pageQuery, paging);
            data.page = new PageNav() { pageIndex = pageIndex, pageSize = pageSize, totalCount = paging.recordCount };
            return data;
        }
        /// <summary>
        /// 获取规格的配料
        /// </summary>
        /// <param name="dishPriceId"></param>
        /// <returns></returns>
        public static List<DishIngredients> GetDishIngredientsList(int dishPriceId)
        {
            List<DishIngredients> list = new List<DishIngredients>();
            string strsql = string.Format(@"SELECT  DishIngredients.ingredientsId, DishIngredients.menuId, DishIngredients.ingredientsName, DishIngredients.ingredientsPrice, 
                      DishIngredients.vipDiscountable, DishIngredients.backDiscountable, DishIngredients.ingredientsRemark, DishIngredients.ingredientsSequence, 
                      DishIngredients.ingredientsStatus
                FROM DishPriceConnIngredients LEFT JOIN DishIngredients ON DishPriceConnIngredients.ingredientsId = DishIngredients.ingredientsId
                where DishPriceConnIngredients.dishPriceId=@dishPriceId and DishIngredients.ingredientsStatus=1");
            var parm = new[] { new SqlParameter("@dishPriceId", dishPriceId) };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, parm))
            {
                while (dr.Read())
                {
                    DishIngredients model = new DishIngredients();
                    model.ingredientsId = !dr.IsDBNull(0) ? dr.GetInt32(0) : 0;
                    model.menuId = !dr.IsDBNull(1) ? dr.GetInt32(1) : 0;
                    model.ingredientsName = !dr.IsDBNull(2) ? dr.GetString(2) : string.Empty;
                    model.ingredientsPrice = !dr.IsDBNull(3) ? dr.GetDouble(3) : 0;
                    model.vipDiscountable = !dr.IsDBNull(4) ? dr.GetBoolean(4) : false;
                    model.backDiscountable = !dr.IsDBNull(5) ? dr.GetBoolean(5) : false;
                    model.ingredientsRemark = !dr.IsDBNull(6) ? dr.GetString(6) : string.Empty;
                    model.ingredientsSequence = !dr.IsDBNull(7) ? dr.GetInt32(7) : 0;
                    model.ingredientsStatus = !dr.IsDBNull(8) ? dr.GetBoolean(8) : false;
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 保存菜品修改页面的信息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="operStatus">修改或新加</param>
        /// <returns></returns>
        public static bool SaveDishInfo(SybDishInfo data, OperStatus operStatus)
        {
            if (operStatus == OperStatus.Insert || operStatus == OperStatus.MutInsert)
                return SaveDishInfoForInsert(data);
            else if (operStatus == OperStatus.Edit)
                return SaveDishInfoForEdit(data);
            return false;
        }

        /// <summary>
        /// 保存菜品修改页面的信息(新增)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool SaveDishInfoForInsert(SybDishInfo data)
        {
            bool val = true;
            if (SaveDishInfo_InsertBaseInfo(ref data))
            {
                //多规格分批进行数据操作，无法整体回滚
                foreach (SybDishPriceInfo data_sybDishPrice in data.DishPriceList)
                {
                    DishPriceInfo data_dishPrice = new DishPriceInfo()
                    {
                        DishID = data.DishID,
                        DishPriceID = 0,
                        DishPrice = data_sybDishPrice.DishPrice,
                        DishSoldout = data_sybDishPrice.DishSoldout,
                        DishPriceStatus = 1,
                        DishNeedWeigh = false,
                        vipDiscountable = data_sybDishPrice.vipDiscountable,
                        backDiscountable = data_sybDishPrice.backDiscountable,
                        dishIngredientsMinAmount = data_sybDishPrice.dishIngredientsMinAmount,
                        dishIngredientsMaxAmount = data_sybDishPrice.dishIngredientsMaxAmount

                    };
                    DishPriceI18n data_dishPriceI18n = new DishPriceI18n()
                    {
                        DishPriceI18nID = DishPriceI18nManager.GetDishPriceI18nID(data_sybDishPrice.DishPriceID, 1),
                        DishPriceI18nStatus = 1,
                        DishPriceID = data_sybDishPrice.DishPriceID,
                        LangID = 1,
                        ScaleName = data_sybDishPrice.ScaleName,
                        markName = data_sybDishPrice.markName
                    };

                    if (data_sybDishPrice.operStatus == OperStatus.Insert)
                    {
                        //插入规格
                        int DishPriceId = DishPriceInfoManager.InsertDishPriceInfo(data_dishPrice);
                        if (DishPriceId > 0)
                        {
                            data_dishPrice.DishPriceID = DishPriceId;
                            using (var conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                            {
                                conn.Open();
                                SqlTransaction trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                                try
                                {
                                    data_dishPriceI18n.DishPriceI18nID = DishPriceId;
                                    data_dishPriceI18n.DishPriceID = DishPriceId;
                                    DishPriceI18nManager.Insert(data_dishPriceI18n, trans);

                                    //口味操作
                                    //DishPriceConnTasteManager.Del(data_dishPrice.DishPriceID, trans);
                                    foreach (int id in data_sybDishPrice.DishTasteList)
                                        DishPriceConnTasteManager.Insert(new DishPriceConnTaste { dishPriceId = DishPriceId, tasteId = id }, trans);

                                    //配料操作
                                    //DishPriceConnIngredientsManager.Del(data_dishPrice.DishPriceID, trans);
                                    foreach (int id in data_sybDishPrice.DishIngredientsList)
                                        DishPriceConnIngredientsManager.Insert(new DishPriceConnIngredients { dishPriceId = DishPriceId, ingredientsId = id }, trans);

                                    trans.Commit();
                                    val = true;
                                }
                                catch (Exception)
                                {
                                    trans.Rollback();
                                    val = false;
                                }
                                if (!val) return false;
                            }
                        }
                    }
                }
                #region 图片数据库信息添加
                if (data.PicStatus == 2)
                {
                    ImageInfo bigimageInfo = new ImageInfo();
                    bigimageInfo.DishID = data.DishID;
                    bigimageInfo.ImageName = data.DishImageUrlBig;
                    bigimageInfo.ImageScale = 0;
                    bigimageInfo.ImageSequence = 1;
                    bigimageInfo.ImageStatus = 1;
                    //ImageInfo smallimageInfo = new ImageInfo();
                    //smallimageInfo.DishID = data.DishID;
                    //smallimageInfo.ImageName = data.DishImageUrlSmall;
                    //smallimageInfo.ImageScale = ImageScale.缩略图;
                    //smallimageInfo.ImageSequence = 1;
                    //smallimageInfo.ImageStatus = 1;
                    DishManager dishMan = new DishManager();
                    //dishMan.InsertDishImage(bigimageInfo, smallimageInfo);
                    dishMan.InsertDishImage(bigimageInfo);//2014-6-9 菜图取消小图
                }
                #endregion
            }
            return val;
        }

        /// <summary>
        /// 保存菜品修改页面的信息(修改)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool SaveDishInfoForEdit(SybDishInfo data)
        {
            //修改菜品基础数据
            if (!SaveDishInfo_UpdateBaseInfo(data)) return false;

            //多规格分批进行数据操作，无法整体回滚
            foreach (SybDishPriceInfo data_sybDishPrice in data.DishPriceList)
            {
                DishPriceInfo data_dishPrice = new DishPriceInfo()
                {
                    DishID = data.DishID,
                    DishPriceID = data_sybDishPrice.DishPriceID,
                    DishPrice = data_sybDishPrice.DishPrice,
                    DishSoldout = data_sybDishPrice.DishSoldout,
                    DishNeedWeigh = false,
                    vipDiscountable = data_sybDishPrice.vipDiscountable,
                    backDiscountable = data_sybDishPrice.backDiscountable,
                    dishIngredientsMinAmount = data_sybDishPrice.dishIngredientsMinAmount,
                    dishIngredientsMaxAmount = data_sybDishPrice.dishIngredientsMaxAmount
                };
                DishPriceI18n data_dishPriceI18n = new DishPriceI18n()
                {
                    DishPriceI18nID = DishPriceI18nManager.GetDishPriceI18nID(data_sybDishPrice.DishPriceID, 1),
                    DishPriceI18nStatus = 1,
                    DishPriceID = data_sybDishPrice.DishPriceID,
                    LangID = 1,
                    ScaleName = data_sybDishPrice.ScaleName,
                    markName = data_sybDishPrice.markName
                };

                //插入规格
                if (data_sybDishPrice.operStatus == OperStatus.Insert)
                {
                    data_dishPrice.DishPriceStatus = 1;
                    int DishPriceId = DishPriceInfoManager.InsertDishPriceInfo(data_dishPrice);
                    if (DishPriceId > 0)
                    {
                        data_dishPrice.DishPriceID = DishPriceId;
                        bool val = false;
                        using (var conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                        {
                            conn.Open();
                            SqlTransaction trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                            try
                            {
                                data_dishPriceI18n.DishPriceI18nID = DishPriceId;
                                data_dishPriceI18n.DishPriceID = DishPriceId;
                                DishPriceI18nManager.Insert(data_dishPriceI18n, trans);

                                //口味操作
                                //DishPriceConnTasteManager.Del(data_dishPrice.DishPriceID, trans);
                                foreach (int id in data_sybDishPrice.DishTasteList)
                                    DishPriceConnTasteManager.Insert(new DishPriceConnTaste { dishPriceId = DishPriceId, tasteId = id }, trans);

                                //配料操作
                                //DishPriceConnIngredientsManager.Del(data_dishPrice.DishPriceID, trans);
                                foreach (int id in data_sybDishPrice.DishIngredientsList)
                                    DishPriceConnIngredientsManager.Insert(new DishPriceConnIngredients { dishPriceId = DishPriceId, ingredientsId = id }, trans);

                                trans.Commit();
                                val = true;
                            }
                            catch (Exception)
                            {
                                trans.Rollback();
                                val = false;
                            }
                        }
                        if (!val) return false;
                    }
                }
                //修改规格
                if (data_sybDishPrice.operStatus == OperStatus.Edit)
                {
                    bool val = false;
                    using (var conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                    {
                        conn.Open();
                        SqlTransaction trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                        try
                        {
                            DishPriceInfoManager.Update(data_dishPrice, trans);
                            DishPriceI18nManager.Update(data_dishPriceI18n, trans);

                            //口味操作
                            DishPriceConnTasteManager.Del(data_dishPrice.DishPriceID, trans);
                            foreach (int id in data_sybDishPrice.DishTasteList)
                                DishPriceConnTasteManager.Insert(new DishPriceConnTaste { dishPriceId = data_dishPrice.DishPriceID, tasteId = id }, trans);

                            //配料操作
                            DishPriceConnIngredientsManager.Del(data_dishPrice.DishPriceID, trans);
                            foreach (int id in data_sybDishPrice.DishIngredientsList)
                                DishPriceConnIngredientsManager.Insert(new DishPriceConnIngredients { dishPriceId = data_dishPrice.DishPriceID, ingredientsId = id }, trans);

                            trans.Commit();
                            val = true;
                        }
                        catch (Exception)
                        {
                            trans.Rollback();
                            val = false;
                        }
                    }
                    if (!val) return false;

                }
                //删除规格
                if (data_sybDishPrice.operStatus == OperStatus.Delete)
                {
                    bool val = false;
                    using (var conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                    {
                        conn.Open();
                        SqlTransaction trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                        try
                        {
                            DishPriceInfoManager.Del(data_sybDishPrice.DishPriceID, trans);
                            DishPriceI18nManager.Del(data_sybDishPrice.DishPriceID, trans);

                            DishPriceConnTasteManager.Del(data_dishPrice.DishPriceID, trans);
                            DishPriceConnIngredientsManager.Del(data_dishPrice.DishPriceID, trans);

                            trans.Commit();
                            val = true;
                        }
                        catch (Exception)
                        {
                            trans.Rollback();
                            val = false;
                        }
                    }
                    if (!val) return false;
                }
            }
            #region 图片数据库信息添加
            if (data.PicStatus == 2)
            {
                ImageInfo bigimageInfo = new ImageInfo();
                bigimageInfo.DishID = data.DishID;
                bigimageInfo.ImageName = data.DishImageUrlBig;
                bigimageInfo.ImageScale = 0;
                bigimageInfo.ImageSequence = 1;
                bigimageInfo.ImageStatus = 1;
                //ImageInfo smallimageInfo = new ImageInfo();
                //smallimageInfo.DishID = data.DishID;
                //smallimageInfo.ImageName = data.DishImageUrlSmall;
                //smallimageInfo.ImageScale = ImageScale.缩略图;
                //smallimageInfo.ImageSequence = 1;
                //smallimageInfo.ImageStatus = 1;
                DishManager dishMan = new DishManager();
                //dishMan.InsertDishImage(bigimageInfo, smallimageInfo);
                dishMan.InsertDishImage(bigimageInfo);//2014-6-9 菜图取消小图
            }
            #endregion

            return true;
        }

        /// <summary>
        /// 修改菜品基础数据（新增菜品） 
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool SaveDishInfo_InsertBaseInfo(ref SybDishInfo data)
        {
            DishInfo mode_dish = new DishInfo
            {
                MenuID = data.menuId,
                DishDisplaySequence = data.DishDisplaySequence,
                IsActive = true,
                DishTotalQuantity = 0,
                DishStatus = 1,
            };

            int dishID = InsertDishInfo(mode_dish);
            if (dishID <= 0) return false;

            //基础数据准备
            DishI18n model_DishI18n = new DishI18n
            {
                DishID = dishID,
                DishName = data.DishName,
                DishDescShort = data.DishDescShort,
                DishDescDetail = data.DishDescDetail,
                dishQuanPin = data.dishQuanPin,
                dishJianPin = data.dishJianPin,
                DishI18nStatus = 1,
                LangID = 1,
                DishHistory = "",
            };

            data.DishID = dishID;

            bool val = false;
            using (var conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    //分类
                    foreach (int id in data.DishTypeList)
                        DishConnTypeManager.Insert(new DishConnType() { DishID = data.DishID, DishTypeID = id, DishConnTypeStatus = 1 }, trans);
                    //插入18n
                    InsertDishI18nInfo(model_DishI18n, trans);

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

        /// <summary>
        /// 修改菜品基础数据（保存菜品）
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool SaveDishInfo_UpdateBaseInfo(SybDishInfo data)
        {
            bool val = false;
            //基础数据准备
            DishI18n model_DishI18n = new DishI18n
            {
                DishID = data.DishID,
                DishName = data.DishName,
                DishDescShort = data.DishDescShort,
                DishDescDetail = data.DishDescDetail,
                dishQuanPin = data.dishQuanPin,
                dishJianPin = data.dishJianPin
            };

            using (var conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    UpdateDishDisplaySequence(data.DishID, data.DishDisplaySequence, trans);
                    UpdateDishI18n(model_DishI18n, trans);
                    //分类
                    DishConnTypeManager.Del(data.DishID, trans);
                    foreach (int id in data.DishTypeList)
                        DishConnTypeManager.Insert(new DishConnType() { DishID = data.DishID, DishTypeID = id, DishConnTypeStatus = 1 }, trans);

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

        /// <summary>
        /// 更新菜品的排序
        /// </summary>
        /// <param name="dishID"></param>
        /// <param name="dishDisplaySequence"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public static bool UpdateDishDisplaySequence(int dishID, int dishDisplaySequence, SqlTransaction trans)
        {
            string strsql = @"update DishInfo set DishDisplaySequence=@DishDisplaySequence where DishID=@DishID";
            SqlParameter[] parm = new[] {
                new SqlParameter("@DishID", dishID),
                new SqlParameter("@DishDisplaySequence", dishDisplaySequence)
                };
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, parm) > 0;
        }

        public static int InsertDishInfo(DishInfo model)
        {
            string strsql = @"insert into DishInfo (DiscountTypeID,MenuID,DishDisplaySequence,SendToKitchen,IsActive,DishTotalQuantity,DishStatus)
                        values (@DiscountTypeID,@MenuID,@DishDisplaySequence,@SendToKitchen,@IsActive,@DishTotalQuantity,@DishStatus)
select @@IDENTITY";
            SqlParameter[] parm = {
new SqlParameter("@DiscountTypeID", SqlDbType.Int),
new SqlParameter("@MenuID", SqlDbType.Int),
new SqlParameter("@DishDisplaySequence", SqlDbType.Int),
new SqlParameter("@SendToKitchen", SqlDbType.Bit),
new SqlParameter("@IsActive", SqlDbType.Bit),
new SqlParameter("@DishTotalQuantity", SqlDbType.Float),
new SqlParameter("@DishStatus", SqlDbType.Int),
                        };
            parm[0].Value = model.DiscountTypeID;
            parm[1].Value = model.MenuID;
            parm[2].Value = model.DishDisplaySequence;
            parm[3].Value = model.SendToKitchen;
            parm[4].Value = model.IsActive;
            parm[5].Value = model.DishTotalQuantity;
            parm[6].Value = model.DishStatus;

            object val = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, parm);
            return Convert.ToInt32(val);
        }

        /// <summary>
        /// 更新菜品的数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public static bool UpdateDishI18n(DishI18n model, SqlTransaction trans)
        {
            string strsql = @"update DishI18n set DishName=@DishName,DishDescShort=@DishDescShort,DishDescDetail=@DishDescDetail,dishQuanPin=@dishQuanPin,dishJianPin=@dishJianPin where DishID=@DishID";
            SqlParameter[] parm = {
new SqlParameter("@DishID", SqlDbType.Int),
new SqlParameter("@DishName", SqlDbType.NVarChar,500),
new SqlParameter("@DishDescShort", SqlDbType.NVarChar,500),
new SqlParameter("@DishDescDetail", SqlDbType.NVarChar,-1),
new SqlParameter("@dishQuanPin", SqlDbType.NVarChar,500),
new SqlParameter("@dishJianPin", SqlDbType.NVarChar,500)
                        };
            parm[0].Value = model.DishID;
            parm[1].Value = model.DishName;
            parm[2].Value = model.DishDescShort;
            parm[3].Value = model.DishDescDetail;
            parm[4].Value = model.dishQuanPin;
            parm[5].Value = model.dishJianPin;
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, parm) > 0;
        }

        public static bool InsertDishI18nInfo(DishI18n model, SqlTransaction trans)
        {
            string strsql = @"insert into DishI18n (DishID,LangID,DishName,DishDescShort,DishDescDetail,DishHistory,DishI18nStatus
,dishQuanPin,dishJianPin)
                        values (@DishID,@LangID,@DishName,@DishDescShort,@DishDescDetail,@DishHistory,@DishI18nStatus
,@dishQuanPin,@dishJianPin)";
            SqlParameter[] parm = {
new SqlParameter("@DishID", SqlDbType.Int),
new SqlParameter("@LangID", SqlDbType.Int),
new SqlParameter("@DishName", SqlDbType.NVarChar,500),
new SqlParameter("@DishDescShort", SqlDbType.NVarChar,500),
new SqlParameter("@DishDescDetail", SqlDbType.NVarChar,-1),
new SqlParameter("@DishHistory", SqlDbType.NVarChar,-1),
new SqlParameter("@DishI18nStatus", SqlDbType.Int),
new SqlParameter("@dishQuanPin", SqlDbType.NVarChar,500),
new SqlParameter("@dishJianPin", SqlDbType.NVarChar,500)
                        };
            parm[0].Value = model.DishID;
            parm[1].Value = model.LangID;
            parm[2].Value = model.DishName;
            parm[3].Value = model.DishDescShort;
            parm[4].Value = model.DishDescDetail;
            parm[5].Value = model.DishHistory;
            parm[6].Value = model.DishI18nStatus;
            parm[7].Value = model.dishQuanPin;
            parm[8].Value = model.dishJianPin;
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, parm) == 1;

        }

        public static bool ExitDishName(int menuId, string dishName)
        {
            var val = false;
            string strsql = @"select 1 from DishInfo left join DishI18n on DishInfo.DishID=DishI18n.DishID 
                        where DishI18n.LangID=1 and menuId=@menuId and DishI18n.DishName=@DishName and DishInfo.DishStatus=1";
            SqlParameter[] parm = new[] {
                    new SqlParameter("@menuId", menuId),
                     new SqlParameter("@DishName", dishName)
                        };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, parm))
            {
                if (dr.Read()) val = true;
            }
            return val;
        }

        public static bool ExitDishName(int menuId, string dishName, int dishId)
        {
            var val = false;
            string strsql = @"select 1 from DishInfo left join DishI18n on DishInfo.DishID=DishI18n.DishID 
                        where DishI18n.LangID=1 and menuId=@menuId and DishI18n.DishName=@DishName and DishInfo.DishStatus=1 and DishInfo.DishID<>@dishId";
            SqlParameter[] parm = new[] {
                    new SqlParameter("@menuId", menuId),
                     new SqlParameter("@DishName", dishName),
                      new SqlParameter("@dishId", dishId),
                        };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, parm))
            {
                if (dr.Read()) val = true;
            }
            return val;
        }
        #endregion

        #region 配料页面操作
        /// <summary>
        /// 保存菜品配料的信息        
        /// </summary>
        /// <param name="list"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public static bool SaveDishIngredients(List<SybDishIngredients> list, int menuId)
        {
            bool val = false;
            using (var conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    foreach (SybDishIngredients data in list)
                    {
                        DishIngredients d = new DishIngredients()
                        {
                            ingredientsId = data.ingredientsId,
                            menuId = menuId,
                            ingredientsName = data.ingredientsName,
                            ingredientsPrice = data.ingredientsPrice,
                            vipDiscountable = data.vipDiscountable,
                            backDiscountable = data.backDiscountable,
                            ingredientsRemark = "",
                            ingredientsSequence = data.ingredientsSequence,
                            ingredientsStatus = true
                        };

                        if (data.operStatus == OperStatus.Insert)
                            DishIngredientsManager.Insert(d, trans);
                        else if (data.operStatus == OperStatus.Edit)
                            DishIngredientsManager.Update(d, trans);
                        else if (data.operStatus == OperStatus.Delete)
                        {
                            DishIngredientsManager.UpdateingredientsStatus(data.ingredientsId, false, trans);
                            DishPriceConnIngredientsManager.DelIngredients(data.ingredientsId, trans);
                        }
                    }

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
        #endregion

        public static int GetMenuIdByShopId(int shopId)
        {
            int val = 0;
            string strsql = @"select MenuConnShop.menuId from MenuConnShop,MenuInfo where MenuInfo.MenuID =MenuConnShop.menuId and MenuInfo.MenuStatus=1 and MenuConnShop.shopId=@shopId";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@shopId", SqlDbType.Int) { Value = shopId }
            };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, para))
            {
                if (dr.Read())
                {
                    val = dr["menuId"] != DBNull.Value ? Convert.ToInt32(dr["menuId"]) : 0;
                }
            }
            return val;
        }

        //判断菜品名称是否可用(是否重复) 批量传图用
        public static bool IsDishNameUsed(string dishName)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.AppendFormat("select count(DishI18nID) from DishI18n where DishName = {0}", dishName);

                    obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), null);
                }
                catch
                {
                    return false;
                }
                if (obj == null)
                {
                    return false;
                }
                else
                {
                    int iRet = Convert.ToInt32(obj);
                    if (iRet > 0)
                        return true;
                    else
                        return false;
                }
            }
        }

        //更新菜品名称，状态，全拼，简拼 批量传图
        public static bool UpdateDishI18nInfo(int DishID, string dishName, string dishQuanPin, string dishJianPin)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update DishI18n set ");
            strSql.Append("DishName=@DishName,");
            strSql.Append("dishQuanPin=@dishQuanPin,");
            strSql.Append("dishJianPin=@dishJianPin,");
            strSql.Append("DishI18nStatus=@DishI18nStatus");
            strSql.Append(" where DishI18nID=@DishI18nID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@DishID", SqlDbType.Int,4),
                    new SqlParameter("@DishName",SqlDbType.NVarChar,500),
                    new SqlParameter("@dishQuanPin", SqlDbType.NVarChar,500),
                    new SqlParameter("@dishJianPin", SqlDbType.NVarChar,500),
                    new SqlParameter("@DishI18nStatus",SqlDbType.Int,4)};
            parameters[0].Value = DishID;
            parameters[1].Value = dishName;
            parameters[2].Value = dishQuanPin;
            parameters[3].Value = dishJianPin;
            parameters[4].Value = 1;

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //更新DishInfo状态
        public static bool UpdateDishInfoStatus(int DishID, int DishStatus)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update DishInfo set ");
            strSql.Append("DishStatus=@DishStatus,");
            strSql.Append(" where DishID=@DishID ");
            SqlParameter[] parameters = {
                        new SqlParameter("@DishStatus", SqlDbType.Int,4),
                        new SqlParameter("@DishID", SqlDbType.Int,4)
                        };
            parameters[0].Value = DishStatus;
            parameters[1].Value = DishID;

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //在 DishPriceInfo，DishPriceI18n 中新增 价格，单位 数据

        /// <summary>
        /// 查询某店铺销量最高的店铺（菜价大于5元）
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public DataTable GetCommonDishList(int shopId)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" select top 15 d.DishID");
            strSql.Append(" from MenuConnShop m inner join DishInfo d on d.MenuID = m.menuId and d.DishStatus > 0");
            strSql.Append(" inner join DishPriceInfo p on d.DishID = p.DishID and p.DishPriceStatus > 0 and p.DishPrice > 5");
            strSql.AppendFormat(" and m.shopId = '{0}' order by d.dishSalesIn19dian desc;", shopId);

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

            if (ds.Tables[0] != null)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }

        public DataTable SelectDishId(int menuId, int dishTypeId, string dishName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [DishInfo].DishID ,row_number() over (order by DishInfo.DishID desc ) as rowIndex ");
            strSql.AppendFormat(" from [DishInfo],[DishI18n]");
            if (dishTypeId > 0)
            {
                strSql.Append(" ,[DishConnType],[DishTypeInfo]");//关联菜品分类表查询
            }
            strSql.AppendFormat(" where [DishInfo].DishID = [DishI18n].DishID  and [DishInfo].DishStatus > 0  and DishI18n.DishI18nStatus=1 and DishInfo.MenuID ={0}", menuId);
            strSql.AppendFormat(" and  (DishI18n.DishName like '%{0}%' or DishI18n.dishQuanPin like '%{0}%' or DishI18n.dishJianPin like '%{0}%')", dishName);//文本框模糊查询
            if (dishTypeId > 0)
            {
                strSql.AppendFormat(" and DishInfo.DishID=DishConnType.DishID and DishConnType.DishTypeID=DishTypeInfo.DishTypeID and DishTypeInfo.DishTypeID ={0} and DishConnType.DishConnTypeStatus =1", dishTypeId);
            }
            strSql.Append(" order by DishInfo.DishID desc");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据DishId查找大图及小图的图片信息
        /// </summary>
        /// <param name="dishId"></param>
        /// <returns></returns>
        public DataTable SelectDishImageInfo(int dishId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select menu.menuImagePath,img.ImageName");
            strSql.Append(" from DishInfo dish inner join MenuInfo menu on dish.MenuID = menu.MenuID");
            strSql.Append(" and dish.DishStatus = 1 and menu.MenuStatus = 1");
            strSql.Append(" inner join ImageInfo img on dish.DishID = img.DishID");
            strSql.Append(" and img.ImageStatus = 1 and dish.DishID = @dishId");
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@dishId", SqlDbType.Int) { Value = dishId }
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para);
            return ds.Tables[0];
        }

        /// <summary>
        /// 搜索指定店家的家品(分页)
        /// @bruke 20140422
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="key"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IPagedList<DishDetails> GetDishDetailsesForPage(int shopId, string key, int pageIndex, int pageSize)
        {
            StringBuilder strSqlBuilder = new StringBuilder();
            strSqlBuilder.Append(
                "select t.companyId,t.shopId,t.menuId,t.DishI18nID,t.DishPriceI18nID,t.DishName,t.dishQuanPin,t.dishJianPin,t.DishPrice,t.ScaleName,t.DishDisplaySequence,t.SellOff from (SELECT ROW_NUMBER() Over(order by  b.DishDisplaySequence asc) as rowId,c.companyId,c.shopId,c.menuId,a.DishI18nID,e.DishPriceI18nID, a.DishName,a.dishQuanPin,a.dishJianPin,d.[DishPrice],e.ScaleName,b.DishDisplaySequence,");
            strSqlBuilder.Append(
                "case when exists (SELECT 1 FROM [dbo].[CurrentSellOffInfo] f where f.DishI18nID=a.DishI18nID and f.DishPriceI18nID=e.DishPriceI18nID and f.status=1 and f.shopid=@ShopId) then 1 else 0 end as SellOff ");
            strSqlBuilder.Append("FROM [dbo].DishI18n a ");
            strSqlBuilder.Append("inner join dbo.DishInfo b on a.DishID=b.DishID and b.DishStatus=1 ");
            strSqlBuilder.Append("inner join dbo.MenuConnShop c on b.MenuID=c.menuId ");
            strSqlBuilder.Append("inner join dbo.[DishPriceInfo] d on a.DishID=d.DishID and d.DishPriceStatus=1 ");
            strSqlBuilder.Append(
                "inner join dbo.DishPriceI18n e on e.DishPriceID=d.DishPriceID and e.DishPriceI18nStatus=1 ");
            strSqlBuilder.Append("where a.DishI18nStatus=1 and c.shopId=@ShopId ");
            if (!string.IsNullOrWhiteSpace(key))
            {
                strSqlBuilder.Append("and (a.DishName like @Key or a.dishQuanPin like @Key or a.dishJianPin like @Key) ");
            }
            strSqlBuilder.Append(") as t ");
            strSqlBuilder.Append("where t.rowId between @StartIndex and @EndIndex");

            Console.WriteLine(strSqlBuilder.ToString());

            List<SqlParameter> cmdCountParameters = new List<SqlParameter>();
            cmdCountParameters.AddRange(new SqlParameter[]
            {
                new SqlParameter("@ShopId", shopId),
            });
            if (!string.IsNullOrWhiteSpace(key))
            {
                cmdCountParameters.Add(new SqlParameter("@Key", string.Format("%{0}%", key)));
            }

            List<SqlParameter> cmdParameters = new List<SqlParameter>(cmdCountParameters);
            cmdParameters.AddRange(new SqlParameter[]
            {
                new SqlParameter("@StartIndex", (pageIndex-1)*pageSize),
                new SqlParameter("@EndIndex", pageIndex*pageSize)
            });

            List<DishDetails> list = new List<DishDetails>();
            using (var dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text,
                strSqlBuilder.ToString(), cmdParameters.ToArray()))
            {
                while (dr.Read())
                {
                    list.Add(new DishDetails
                    {
                        CompanyId = Convert.ToInt32(dr["companyId"]),
                        ShopId = Convert.ToInt32(dr["shopId"]),
                        MenuId = Convert.ToInt32(dr["menuId"]),
                        DishId = Convert.ToInt32(dr["DishI18nID"]),
                        DishPriceId = Convert.ToInt32(dr["DishPriceI18nID"]),
                        DishDisplaySequence = dr["DishDisplaySequence"] == DBNull.Value
                            ? int.MaxValue
                            : Convert.ToInt32(dr["DishDisplaySequence"]),
                        DishJianPin = dr["dishJianPin"] == DBNull.Value ? "" : dr["dishJianPin"].ToString(),
                        DishName = dr["DishName"] == DBNull.Value ? "" : dr["DishName"].ToString(),
                        DishQuanPin = dr["dishQuanPin"] == DBNull.Value ? "" : dr["dishQuanPin"].ToString(),
                        ScaleName = dr["ScaleName"] == DBNull.Value ? "" : dr["ScaleName"].ToString(),
                        DishPrice = Convert.ToSingle(dr["DishPrice"]),
                        SellOff = Convert.ToBoolean(dr["SellOff"])
                    });
                }
            }

            StringBuilder strCountSqlBuilder = new StringBuilder();
            strCountSqlBuilder.Append("SELECT COUNT(1) FROM [dbo].DishI18n a ");
            strCountSqlBuilder.Append("inner join dbo.DishInfo b on a.DishID=b.DishID and b.DishStatus=1 ");
            strCountSqlBuilder.Append("inner join dbo.MenuConnShop c on b.MenuID=c.menuId ");
            strCountSqlBuilder.Append("inner join dbo.[DishPriceInfo] d on a.DishID=d.DishID and d.DishPriceStatus=1 ");
            strCountSqlBuilder.Append(
                "inner join dbo.DishPriceI18n e on e.DishPriceID=d.DishPriceID and e.DishPriceI18nStatus=1 ");
            strCountSqlBuilder.Append("where c.shopId=@ShopId ");
            if (!string.IsNullOrWhiteSpace(key))
            {
                strCountSqlBuilder.Append("and (a.DishName like @Key or a.dishQuanPin like @Key or a.dishJianPin like @Key) ");
            }
            object v = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text,
                strCountSqlBuilder.ToString(), cmdCountParameters.ToArray());

            return new StaticPagedList<DishDetails>(list, pageIndex, pageSize, Convert.ToInt32(v));
        }


        /// <summary>
        /// 获取指定店沽清菜品列表(分页)
        /// @bruke 20140422
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IPagedList<DishDetails> GetSellOffDishDetailsesForPage(int shopId, int pageIndex, int pageSize)
        {
            StringBuilder strSqlBuilder = new StringBuilder();
            strSqlBuilder.Append(
                @"select  t.companyId,t.shopId,t.menuId,t.DishI18nID,t.DishPriceI18nID,t.DishName,t.dishQuanPin,t.dishJianPin,t.DishPrice,
t.ScaleName,t.DishDisplaySequence,t.SellOff 
from (SELECT ROW_NUMBER() Over(order by  b.DishDisplaySequence asc) as rowId,
f.companyId,f.shopId,f.menuId,a.DishI18nID,e.DishPriceI18nID, a.DishName,a.dishQuanPin,a.dishJianPin,d.[DishPrice],
e.ScaleName,b.DishDisplaySequence,1 as SellOff
 from CurrentSellOffInfo f
inner join dbo.DishI18n a on f.DishI18nID=a.DishI18nID
inner join dbo.DishInfo b on a.DishID=b.DishID
inner join dbo.DishPriceI18n e on e.DishPriceI18nID=f.DishPriceI18nID
inner join dbo.[DishPriceInfo] d on b.DishID=d.DishID and e.DishPriceID=d.DishPriceID
where f.shopId=@ShopId and f.[status]=1 and a.DishI18nStatus=1 and b.DishStatus=1 and e.DishPriceI18nStatus=1
and d.DishPriceStatus=1 and exists(select 1 from MenuConnShop where shopid=@ShopId and [menuId]=f.menuId)) as t 
where t.rowId between @StartIndex and @EndIndex");

            StringBuilder strCountSqlBuilder = new StringBuilder();
            strCountSqlBuilder.Append(@"SELECT COUNT(1)
from CurrentSellOffInfo f
inner join dbo.DishI18n a on f.DishI18nID=a.DishI18nID
inner join dbo.DishInfo b on a.DishID=b.DishID
inner join dbo.DishPriceI18n e on e.DishPriceI18nID=f.DishPriceI18nID
inner join dbo.[DishPriceInfo] d on b.DishID=d.DishID and e.DishPriceID=d.DishPriceID
where f.shopId=@ShopId and f.[status]=1 and a.DishI18nStatus=1 and b.DishStatus=1 and e.DishPriceI18nStatus=1 
and d.DishPriceStatus=1 and exists(select 1 from MenuConnShop where shopid=@ShopId and [menuId]=f.menuId)");

            SqlParameter[] cmdCountParameters = new[] { new SqlParameter("@ShopId", shopId) };
            List<SqlParameter> cmdParameters = new List<SqlParameter>(cmdCountParameters);
            cmdParameters.AddRange(new SqlParameter[]
            {
                new SqlParameter("@StartIndex", (pageIndex-1)*pageSize),
                new SqlParameter("@EndIndex", pageIndex*pageSize)
            });

            List<DishDetails> list = new List<DishDetails>();
            using (var dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text,
                strSqlBuilder.ToString(), cmdParameters.ToArray()))
            {
                while (dr.Read())
                {
                    list.Add(new DishDetails
                    {
                        CompanyId = Convert.ToInt32(dr["companyId"]),
                        ShopId = Convert.ToInt32(dr["shopId"]),
                        MenuId = Convert.ToInt32(dr["menuId"]),
                        DishId = Convert.ToInt32(dr["DishI18nID"]),
                        DishPriceId = Convert.ToInt32(dr["DishPriceI18nID"]),
                        DishDisplaySequence = dr["DishDisplaySequence"] == DBNull.Value
                            ? int.MaxValue
                            : Convert.ToInt32(dr["DishDisplaySequence"]),
                        DishJianPin = dr["dishJianPin"] == DBNull.Value ? "" : dr["dishJianPin"].ToString(),
                        DishName = dr["DishName"] == DBNull.Value ? "" : dr["DishName"].ToString(),
                        DishQuanPin = dr["dishQuanPin"] == DBNull.Value ? "" : dr["dishQuanPin"].ToString(),
                        ScaleName = dr["ScaleName"] == DBNull.Value ? "" : dr["ScaleName"].ToString(),
                        DishPrice = Convert.ToSingle(dr["DishPrice"]),
                        SellOff = Convert.ToBoolean(dr["SellOff"])
                    });
                }
            }

            object v = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text,
               strCountSqlBuilder.ToString(), cmdCountParameters);

            return new StaticPagedList<DishDetails>(list, pageIndex, pageSize, Convert.ToInt32(v));

        }

        /// <summary>
        /// 获取指定规格的沽清
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="shopId"></param>
        /// <param name="menuId"></param>
        /// <param name="dishI18Id"></param>
        /// <param name="dishPriceI18nId"></param>
        /// <returns></returns>
        public CurrentSellOffInfo GetCurrentSellOffInfoByDishPrice(int companyId, int shopId, int menuId, int dishI18Id,
            int dishPriceI18nId)
        {
            StringBuilder strSqlBuilder = new StringBuilder();
            strSqlBuilder.Append(
                @"SELECT [id],[companyId],[shopId],[menuId],[DishI18nID],[status],[DishPriceI18nID] FROM [dbo].[CurrentSellOffInfo] where [companyId]=@companyId and [shopId]=@shopId and [menuId]=@menuId and [DishI18nID]=@dishId and [DishPriceI18nID]=@dishPriceId
 and CurrentSellOffInfo.status=1 and  CurrentSellOffInfo.expirationTime>GETDATE()");

            SqlParameter[] cmdParameters = new SqlParameter[]
            {
                new SqlParameter("@companyId",companyId), 
                new SqlParameter("@shopId",shopId), 
                new SqlParameter("@menuId",menuId), 
                new SqlParameter("@dishId",dishI18Id), 
                new SqlParameter("@dishPriceId",dishPriceI18nId), 
            };

            using (var dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text,
                strSqlBuilder.ToString(), cmdParameters))
            {
                CurrentSellOffInfo csoi = null;
                if (dr.Read())
                {
                    csoi = new CurrentSellOffInfo()
                    {
                        companyId = Convert.ToInt32(dr["companyId"]),
                        DishI18nID = Convert.ToInt32(dr["DishI18nID"]),
                        shopId = Convert.ToInt32(dr["shopId"]),
                        DishPriceI18nID = Convert.ToInt32(dr["DishPriceI18nID"]),
                        menuId = Convert.ToInt32(dr["menuId"]),
                        status = Convert.ToInt32(dr["status"]),
                        Id = Convert.ToInt32(dr["id"])
                    };
                }
                return csoi;
            }
        }

        /// <summary>
        /// 更新当前沽清状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        public void UpdateCurrentSellOffInfoStatus(int id, int status)
        {
            StringBuilder strSqlBuilder = new StringBuilder();
            strSqlBuilder.Append("UPDATE [dbo].[CurrentSellOffInfo] SET [status]=@status WHERE [id]=@id");

            SqlParameter[] cmdParameters = new SqlParameter[]
            {
                new SqlParameter("@status",status), 
                new SqlParameter("@id",id)
            };

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text,
                strSqlBuilder.ToString(), cmdParameters);
        }


        /// <summary>
        /// 获取指定规格常用沽清
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="dishI18Id"></param>
        /// <param name="dishPriceI18nId"></param>
        /// <returns></returns>
        public CommonCurrentSellOffInfo GetCommonCurrentSellOffInfoByDishPrice(int menuId, int dishI18Id,
            int dishPriceI18nId)
        {
            StringBuilder strSqlBuilder = new StringBuilder();
            strSqlBuilder.Append(
                @"SELECT [id],[menuId],[DishI18nID],[status],[currentSellOffCount],[DishPriceI18nID]FROM [dbo].[CommonCurrentSellOffInfo] where [menuId]=@menuId and [DishI18nID]=@dishId and [DishPriceI18nID]=@dishPriceId");

            SqlParameter[] cmdParameters = new SqlParameter[]
            {
                new SqlParameter("@menuId",menuId), 
                new SqlParameter("@dishId",dishI18Id), 
                new SqlParameter("@dishPriceId",dishPriceI18nId), 
            };

            using (var dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text,
                strSqlBuilder.ToString(), cmdParameters))
            {
                CommonCurrentSellOffInfo csoi = null;
                if (dr.Read())
                {
                    csoi = new CommonCurrentSellOffInfo()
                    {
                        DishI18nID = Convert.ToInt32(dr["DishI18nID"]),
                        DishPriceI18nID = Convert.ToInt32(dr["DishPriceI18nID"]),
                        menuId = Convert.ToInt32(dr["menuId"]),
                        status = Convert.ToInt32(dr["status"]),
                        currentSellOffCount = Convert.ToInt32(dr["currentSellOffCount"]),
                        Id = Convert.ToInt32(dr["id"])
                    };
                }
                return csoi;
            }
        }

        /// <summary>
        /// 抵扣券分享页面菜谱信息
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public DataTable GetDishTableByShopIdAndPrice(int shopId, double price, int pageSize)
        {
            string sql = @"select TOP (@PageSize) I.DishID,I.DishName,p.DishPrice,m.menuImagePath,img.ImageName
                                from DishPriceInfo p
                                inner join DishInfo d on p.DishID=d.DishID and p.DishPrice>=@DishPrice and p.vipDiscountable=1
                                inner join DishI18n I on d.DishID=I.DishID
                                inner join MenuInfo m on d.MenuID = m.MenuID
                                inner join MenuConnShop conn on m.MenuID=conn.menuId and conn.shopId=@shopId
                                inner join ImageInfo img on d.DishID=img.DishID and img.ImageScale=0 and ImageStatus=1
                                order by d.dishPraiseNum DESC";
            SqlParameter[] sqlParameters =  
            {
                new SqlParameter("@shopId", shopId),
                new SqlParameter("@DishPrice", price),
                new SqlParameter("@PageSize", pageSize)
            };
            var dataSet = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameters);
            if (dataSet != null)
            {
                return dataSet.Tables[0];
            }
            return null;
        }

        public DataTable GetDishTableByShopId(int shopId, int pageSize)
        {
            string sql = @"select TOP (@PageSize) I.DishID,I.DishName,p.DishPrice,m.menuImagePath,img.ImageName
                                from DishPriceInfo p
                                inner join DishInfo d on p.DishID=d.DishID and p.vipDiscountable=1
                                inner join DishI18n I on d.DishID=I.DishID
                                inner join MenuInfo m on d.MenuID = m.MenuID
                                inner join MenuConnShop conn on m.MenuID=conn.menuId and conn.shopId=@shopId
                                inner join ImageInfo img on d.DishID=img.DishID and img.ImageScale=0 and ImageStatus=1
                                order by p.DishPrice DESC";
            SqlParameter[] sqlParameters =  
            {
                new SqlParameter("@shopId", shopId),
                new SqlParameter("@PageSize", pageSize)
            };
            var dataSet = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameters);
            if (dataSet != null)
            {
                return dataSet.Tables[0];
            }
            return null;
        }
    }
}
