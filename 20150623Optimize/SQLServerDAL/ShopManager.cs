﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;
using System.Data.SqlClient;
using System.Data;
using System.Transactions;
//
//  Copyright 2011 View Alloc inc. All rights reserved.
//  Created by Jason Xiao on 2012-04-10.
//
namespace VAGastronomistMobileApp.SQLServerDAL
{
    /// <summary> 
    /// 门店数据库操作类
    /// </summary>
    public class ShopManager
    {

        /// <summary>
        /// 新增店铺信息
        /// </summary>
        /// <param name="shop"></param>
        /// <returns></returns>
        public int InsertShop(ShopInfo shop)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into ShopInfo(");
                    strSql.Append("companyID,shopName,shopAddress,shopTelephone,shopLogo,");
                    strSql.Append("shopBusinessLicense,shopHygieneLicense,contactPerson,contactPhone,");
                    strSql.Append("canTakeout,canEatInShop,provinceID,cityID,countyID,shopStatus,");
                    strSql.Append("isHandle,shopImagePath,shopDescription,sinaWeiboName,qqWeiboName,wechatPublicName,");
                    strSql.Append("openTimes,shopRegisterTime,shopVerifyTime,isSupportAccountsRound,shopRating,publicityPhotoPath");
                    strSql.Append(" ,acpp,isSupportPayment,orderDishDesc,notPaymentReason,accountManager,isSupportRedEnvelopePayment,shopLevel,areaManager)");
                    strSql.Append(" values (");
                    strSql.Append("@companyID,@shopName,@shopAddress,@shopTelephone,@shopLogo,");
                    strSql.Append("@shopBusinessLicense,@shopHygieneLicense,@contactPerson,@contactPhone,");
                    strSql.Append("@canTakeout,@canEatInShop,@provinceID,@cityID,@countyID,@shopStatus,");
                    strSql.Append("@isHandle,@shopImagePath,@shopDescription,@sinaWeiboName,@qqWeiboName,@wechatPublicName,");
                    strSql.Append("@openTimes,@shopRegisterTime,@shopVerifyTime,@isSupportAccountsRound,@shopRating,@publicityPhotoPath,@acpp,");
                    strSql.Append("@isSupportPayment,@orderDishDesc,@notPaymentReason,@accountManager,@isSupportRedEnvelopePayment,@shopLevel,@areaManager)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
					    new SqlParameter("@companyID", SqlDbType.Int,4),
                        new SqlParameter("@shopName", SqlDbType.NVarChar,500),
                        new SqlParameter("@shopAddress", SqlDbType.NVarChar,500),
                        new SqlParameter("@shopTelephone",SqlDbType.NVarChar,500),
                        new SqlParameter("@shopLogo", SqlDbType.VarChar,500),
                        new SqlParameter("@shopBusinessLicense",SqlDbType.NVarChar,500),
                        new SqlParameter("@shopHygieneLicense",SqlDbType.NVarChar,500),
                        new SqlParameter("@contactPerson",SqlDbType.NVarChar,500),
                        new SqlParameter("@contactPhone",SqlDbType.NVarChar,500),
                        new SqlParameter("@canTakeout",SqlDbType.Bit,1),
                        new SqlParameter("@canEatInShop",SqlDbType.Bit,1),
                        new SqlParameter("@provinceID",SqlDbType.Int,4),
                        new SqlParameter("@cityID",SqlDbType.Int,4),
                        new SqlParameter("@countyID",SqlDbType.Int,4),
                        new SqlParameter("@shopStatus",SqlDbType.Int,4),
                        new SqlParameter("@isHandle",SqlDbType.Int,4),
                        new SqlParameter("@shopImagePath",SqlDbType.NVarChar,500),
                        new SqlParameter("@shopDescription",SqlDbType.NVarChar,500),
                        new SqlParameter("@sinaWeiboName",SqlDbType.NVarChar,100),
                        new SqlParameter("@qqWeiboName",SqlDbType.NVarChar,100),
                        new SqlParameter("@wechatPublicName",SqlDbType.NVarChar,100),
                        new SqlParameter("@openTimes",SqlDbType.NVarChar,50),
                        new SqlParameter("@shopRegisterTime",SqlDbType.DateTime,50),//注册时间
                        new SqlParameter("@shopVerifyTime",SqlDbType.DateTime,50),//审核时间
                        new SqlParameter("@isSupportAccountsRound",SqlDbType.Bit,1),
                        new SqlParameter("@shopRating",SqlDbType.Float),//店铺评分 2014-1-3 jinyanni
                        new SqlParameter("@publicityPhotoPath",SqlDbType.NVarChar,500),
                        new SqlParameter("@acpp",SqlDbType.Float),
                        new SqlParameter("@isSupportPayment",SqlDbType.Bit,1),
                        new SqlParameter("@orderDishDesc",SqlDbType.NVarChar,500),
                        new SqlParameter("@notPaymentReason",SqlDbType .NVarChar,300),
                        new SqlParameter("@accountManager",SqlDbType.Int,4),
                         new SqlParameter("@isSupportRedEnvelopePayment",SqlDbType.Bit,1),
                         new SqlParameter("@shopLevel",SqlDbType.Int,4),
                         new SqlParameter("@areaManager",SqlDbType.Int,4)
                    };
                    parameters[0].Value = shop.companyID;
                    parameters[1].Value = shop.shopName;
                    parameters[2].Value = shop.shopAddress;
                    parameters[3].Value = shop.shopTelephone;
                    parameters[4].Value = shop.shopLogo;
                    parameters[5].Value = shop.shopBusinessLicense;
                    parameters[6].Value = shop.shopHygieneLicense;
                    parameters[7].Value = shop.contactPerson;
                    parameters[8].Value = shop.contactPhone;
                    parameters[9].Value = shop.canTakeout;
                    parameters[10].Value = shop.canEatInShop;
                    parameters[11].Value = shop.provinceID;
                    parameters[12].Value = shop.cityID;
                    parameters[13].Value = shop.countyID;
                    parameters[14].Value = (int)VAShopStatus.SHOP_NORMAL;
                    parameters[15].Value = shop.isHandle;
                    parameters[16].Value = shop.shopImagePath;
                    parameters[17].Value = shop.shopDescription;
                    parameters[18].Value = shop.sinaWeiboName;
                    parameters[19].Value = shop.qqWeiboName;
                    parameters[20].Value = shop.wechatPublicName;
                    parameters[21].Value = shop.openTimes;
                    parameters[22].Value = shop.shopRegisterTime;
                    parameters[23].Value = shop.shopVerifyTime;
                    parameters[24].Value = shop.isSupportAccountsRound;
                    parameters[25].Value = shop.shopRating;
                    parameters[26].Value = shop.publicityPhotoPath;
                    parameters[27].Value = shop.acpp;
                    parameters[28].Value = shop.isSupportPayment;
                    parameters[29].Value = shop.orderDishDesc;
                    parameters[30].Value = shop.notPaymentReason;
                    parameters[31].Value = shop.accountManager;
                    parameters[32].Value = shop.isSupportRedEnvelopePayment;
                    parameters[33].Value = shop.shopLevel;
                    parameters[34].Value = SqlHelper.GetDbNullValue(shop.AreaManager); ;
                    //1、插入店铺信息表信息
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
        /// 删除店铺信息
        /// </summary>
        /// <param name="shopID"></param>
        public bool DeleteShop(int shopID)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.AppendFormat("update ShopInfo set shopStatus = '{0}' where shopID=@shopID;", (int)VAShopStatus.SHOP_DELETED);
                    SqlParameter[] parameters = {					
					new SqlParameter("@shopID", SqlDbType.Int,4)};
                    parameters[0].Value = shopID;
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (Exception)
                {
                    return false;
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
        /// 修改店铺信息
        /// </summary>
        /// <param name="company"></param>
        public bool UpdateShop(ShopInfo shop)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update ShopInfo set ");
                    strSql.Append("companyID=@companyID,shopName=@shopName,shopAddress=@shopAddress,shopTelephone=@shopTelephone,shopLogo=@shopLogo,");
                    strSql.Append("shopBusinessLicense=@shopBusinessLicense,shopHygieneLicense=@shopHygieneLicense,contactPerson=@contactPerson,contactPhone=@contactPhone,");
                    strSql.Append("canTakeout=@canTakeout,canEatInShop=@canEatInShop,provinceID=@provinceID,cityID=@cityID,countyID=@countyID,");
                    strSql.Append("shopStatus=@shopStatus,shopDescription=@shopDescription,sinaWeiboName=@sinaWeiboName,qqWeiboName=@qqWeiboName,");
                    strSql.Append("wechatPublicName=@wechatPublicName,openTimes=@openTimes,isSupportAccountsRound=@isSupportAccountsRound,");
                    strSql.Append("shopRating=@shopRating,publicityPhotoPath=@publicityPhotoPath,acpp=@acpp");
                    strSql.Append(",isSupportPayment=@isSupportPayment,orderDishDesc=@orderDishDesc,notPaymentReason=@notPaymentReason,accountManager=@accountManager");
                    strSql.Append(",isSupportRedEnvelopePayment=@isSupportRedEnvelopePayment,shopLevel = @shopLevel");//bankAccount=@bankAccount,
                    strSql.Append(",areaManager=@areaManager");
                    strSql.Append(" where shopID=@shopID");
                    SqlParameter[] parameters = {
                        new SqlParameter("@companyID", SqlDbType.Int,4),
                        new SqlParameter("@shopName", SqlDbType.VarChar,500),
                        new SqlParameter("@shopAddress", SqlDbType.VarChar,500),
                        new SqlParameter("@shopTelephone",SqlDbType.NVarChar,500),
                        new SqlParameter("@shopLogo", SqlDbType.VarChar,500),
                        new SqlParameter("@shopBusinessLicense",SqlDbType.NVarChar,500),
                        new SqlParameter("@shopHygieneLicense",SqlDbType.NVarChar,500),
                        new SqlParameter("@contactPerson",SqlDbType.NVarChar,500),
                        new SqlParameter("@contactPhone",SqlDbType.NVarChar,500),
                        new SqlParameter("@canTakeout",SqlDbType.Bit,1),
                        new SqlParameter("@canEatInShop",SqlDbType.Bit,1),
                        new SqlParameter("@provinceID",SqlDbType.Int,4),
                        new SqlParameter("@cityID",SqlDbType.Int,4),
                        new SqlParameter("@countyID",SqlDbType.Int,4),
                        new SqlParameter("@shopStatus",SqlDbType.Int,4),
                        new SqlParameter("@shopDescription",SqlDbType.NVarChar,500),
                        new SqlParameter("@sinaWeiboName",SqlDbType.NVarChar,100),
                        new SqlParameter("@qqWeiboName",SqlDbType.NVarChar,100),
                        new SqlParameter("@shopID",SqlDbType.Int,4),
                        new SqlParameter("@wechatPublicName",SqlDbType.NVarChar,100),
                        new SqlParameter("@openTimes",SqlDbType.NVarChar,50),
                        new SqlParameter("@isSupportAccountsRound",SqlDbType.Bit,1),
                        new SqlParameter("@shopRating",SqlDbType.Float),//店铺评分 2014-1-3 jinyanni
                        new SqlParameter("@publicityPhotoPath",SqlDbType.NVarChar,500),//店铺形象展示照
                        new SqlParameter("@acpp",SqlDbType.Float),
                        new SqlParameter("@isSupportPayment",SqlDbType.Bit,1),
                        new SqlParameter("@orderDishDesc",SqlDbType.NVarChar,500),
                        new SqlParameter("@notPaymentReason",SqlDbType.NVarChar,300),
                        new SqlParameter("@accountManager",SqlDbType.Int,4),
                        //new SqlParameter("@bankAccount",SqlDbType.Int,4),
                         new SqlParameter("@isSupportRedEnvelopePayment",SqlDbType.Bit,1),
                           new SqlParameter("@shopLevel",SqlDbType.Int,4),
                           new SqlParameter("@areaManager",SqlDbType.Int,4)
                     };
                    parameters[0].Value = shop.companyID;
                    parameters[1].Value = shop.shopName;
                    parameters[2].Value = shop.shopAddress;
                    parameters[3].Value = shop.shopTelephone;
                    parameters[4].Value = shop.shopLogo;
                    parameters[5].Value = shop.shopBusinessLicense;
                    parameters[6].Value = shop.shopHygieneLicense;
                    parameters[7].Value = shop.contactPerson;
                    parameters[8].Value = shop.contactPhone;
                    parameters[9].Value = shop.canTakeout;
                    parameters[10].Value = shop.canEatInShop;
                    parameters[11].Value = shop.provinceID;
                    parameters[12].Value = shop.cityID;
                    parameters[13].Value = shop.countyID;
                    parameters[14].Value = shop.shopStatus;
                    parameters[15].Value = shop.shopDescription;
                    parameters[16].Value = shop.sinaWeiboName;
                    parameters[17].Value = shop.qqWeiboName;
                    parameters[18].Value = shop.shopID;
                    parameters[19].Value = shop.wechatPublicName;
                    parameters[20].Value = shop.openTimes;
                    parameters[21].Value = shop.isSupportAccountsRound;
                    parameters[22].Value = shop.shopRating;
                    parameters[23].Value = shop.publicityPhotoPath;
                    parameters[24].Value = shop.acpp;
                    parameters[25].Value = shop.isSupportPayment;
                    parameters[26].Value = shop.orderDishDesc;
                    parameters[27].Value = shop.notPaymentReason;
                    parameters[28].Value = shop.accountManager;
                    //parameters[29].Value = shop.bankAccount;
                    parameters[29].Value = shop.isSupportRedEnvelopePayment;
                    parameters[30].Value = shop.shopLevel;
                    parameters[31].Value = SqlHelper.GetDbNullValue(shop.AreaManager);
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

                }
                catch (System.Exception)
                {
                    return false;
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
        /// 异步修改店铺信息
        /// </summary>
        /// <param name="company"></param>
        public void UpdateShopAsync(ShopInfo shop)
        {
            int result = 0;
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update ShopInfo set ");
                strSql.Append("companyID=@companyID,shopName=@shopName,shopAddress=@shopAddress,shopTelephone=@shopTelephone,shopLogo=@shopLogo,");
                strSql.Append("shopBusinessLicense=@shopBusinessLicense,shopHygieneLicense=@shopHygieneLicense,contactPerson=@contactPerson,contactPhone=@contactPhone,");
                strSql.Append("canTakeout=@canTakeout,canEatInShop=@canEatInShop,provinceID=@provinceID,cityID=@cityID,countyID=@countyID,");
                strSql.Append("shopStatus=@shopStatus,shopDescription=@shopDescription,sinaWeiboName=@sinaWeiboName,qqWeiboName=@qqWeiboName,");
                strSql.Append("wechatPublicName=@wechatPublicName,openTimes=@openTimes,isSupportAccountsRound=@isSupportAccountsRound,");
                strSql.Append("shopRating=@shopRating,publicityPhotoPath=@publicityPhotoPath,acpp=@acpp");
                strSql.Append(",isSupportPayment=@isSupportPayment,orderDishDesc=@orderDishDesc,notPaymentReason=@notPaymentReason,accountManager=@accountManager");
                strSql.Append(",bankAccount=@bankAccount,isSupportRedEnvelopePayment=@isSupportRedEnvelopePayment,shopLevel = @shopLevel");
                strSql.Append(" where shopID=@shopID");
                SqlParameter[] parameters = {
                        new SqlParameter("@companyID", SqlDbType.Int,4),
                        new SqlParameter("@shopName", SqlDbType.VarChar,500),
                        new SqlParameter("@shopAddress", SqlDbType.VarChar,500),
                        new SqlParameter("@shopTelephone",SqlDbType.NVarChar,500),
                        new SqlParameter("@shopLogo", SqlDbType.VarChar,500),
                        new SqlParameter("@shopBusinessLicense",SqlDbType.NVarChar,500),
                        new SqlParameter("@shopHygieneLicense",SqlDbType.NVarChar,500),
                        new SqlParameter("@contactPerson",SqlDbType.NVarChar,500),
                        new SqlParameter("@contactPhone",SqlDbType.NVarChar,500),
                        new SqlParameter("@canTakeout",SqlDbType.Bit,1),
                        new SqlParameter("@canEatInShop",SqlDbType.Bit,1),
                        new SqlParameter("@provinceID",SqlDbType.Int,4),
                        new SqlParameter("@cityID",SqlDbType.Int,4),
                        new SqlParameter("@countyID",SqlDbType.Int,4),
                        new SqlParameter("@shopStatus",SqlDbType.Int,4),
                        new SqlParameter("@shopDescription",SqlDbType.NVarChar,500),
                        new SqlParameter("@sinaWeiboName",SqlDbType.NVarChar,100),
                        new SqlParameter("@qqWeiboName",SqlDbType.NVarChar,100),
                        new SqlParameter("@shopID",SqlDbType.Int,4),
                        new SqlParameter("@wechatPublicName",SqlDbType.NVarChar,100),
                        new SqlParameter("@openTimes",SqlDbType.NVarChar,50),
                        new SqlParameter("@isSupportAccountsRound",SqlDbType.Bit,1),
                        new SqlParameter("@shopRating",SqlDbType.Float),//店铺评分 2014-1-3 jinyanni
                        new SqlParameter("@publicityPhotoPath",SqlDbType.NVarChar,500),//店铺形象展示照
                        new SqlParameter("@acpp",SqlDbType.Float),
                        new SqlParameter("@isSupportPayment",SqlDbType.Bit,1),
                        new SqlParameter("@orderDishDesc",SqlDbType.NVarChar,500),
                        new SqlParameter("@notPaymentReason",SqlDbType.NVarChar,300),
                        new SqlParameter("@accountManager",SqlDbType.Int,4),
                        new SqlParameter("@bankAccount",SqlDbType.Int,4),
                        new SqlParameter("@isSupportRedEnvelopePayment",SqlDbType.Bit,1),
                        new SqlParameter("@shopLevel",SqlDbType.Int,4)
                                            };
                parameters[0].Value = shop.companyID;
                parameters[1].Value = shop.shopName;
                parameters[2].Value = shop.shopAddress;
                parameters[3].Value = shop.shopTelephone;
                parameters[4].Value = shop.shopLogo;
                parameters[5].Value = shop.shopBusinessLicense;
                parameters[6].Value = shop.shopHygieneLicense;
                parameters[7].Value = shop.contactPerson;
                parameters[8].Value = shop.contactPhone;
                parameters[9].Value = shop.canTakeout;
                parameters[10].Value = shop.canEatInShop;
                parameters[11].Value = shop.provinceID;
                parameters[12].Value = shop.cityID;
                parameters[13].Value = shop.countyID;
                parameters[14].Value = shop.shopStatus;
                parameters[15].Value = shop.shopDescription;
                parameters[16].Value = shop.sinaWeiboName;
                parameters[17].Value = shop.qqWeiboName;
                parameters[18].Value = shop.shopID;
                parameters[19].Value = shop.wechatPublicName;
                parameters[20].Value = shop.openTimes;
                parameters[21].Value = shop.isSupportAccountsRound;
                parameters[22].Value = shop.shopRating;
                parameters[23].Value = shop.publicityPhotoPath;
                parameters[24].Value = shop.acpp;
                parameters[25].Value = shop.isSupportPayment;
                parameters[26].Value = shop.orderDishDesc;
                parameters[27].Value = shop.notPaymentReason;
                parameters[28].Value = shop.accountManager;
                parameters[29].Value = shop.bankAccount;
                parameters[30].Value = shop.isSupportRedEnvelopePayment;
                parameters[31].Value = shop.shopLevel;
                SqlHelper.ExecuteNonQueryAsync(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);

            }
            catch (System.Exception)
            {
                return;
            }
        }
        /// <summary>
        /// 修改店铺是否审批
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="isHandle"></param>
        /// <returns></returns>
        public bool UpdateShopHandle(int shopId, int isHandle)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update ShopInfo set ");
                    strSql.Append("isHandle=@isHandle");

                    strSql.AppendFormat(" ,shopVerifyTime='{0}'", DateTime.Now);//只要修改状态，这个时间就会变化

                    strSql.Append(" where shopID=@shopID ");
                    SqlParameter[] parameters = {
                        new SqlParameter("@isHandle",SqlDbType.Int,4),
                        new SqlParameter("@shopID",SqlDbType.Int,4)};
                    parameters[0].Value = isHandle;
                    parameters[1].Value = shopId;

                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (System.Exception)
                {
                    return false;
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
        /// 修改店铺赠送信息
        /// </summary>
        /// <param name="shop"></param>
        public bool UpdateShopGift(ShopInfo shop)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update ShopInfo set ");
                    strSql.Append("preorderGiftTitle=@preorderGiftTitle,preorderGiftDesc=@preorderGiftDesc,");
                    strSql.Append("preorderGiftValidTimeType=@preorderGiftValidTimeType,");
                    strSql.Append("preorderGiftValidTime=@preorderGiftValidTime,");
                    strSql.Append("preorderGiftValidDay=@preorderGiftValidDay,");
                    strSql.Append("preorderGiftValid=@preorderGiftValid");
                    strSql.Append(" where shopID=@shopID ");
                    SqlParameter[] parameters = {
                        new SqlParameter("@preorderGiftTitle", SqlDbType.VarChar,500),
                        new SqlParameter("@preorderGiftDesc", SqlDbType.VarChar,500),
                        new SqlParameter("@preorderGiftValidTimeType",SqlDbType.SmallInt),
                        new SqlParameter("@preorderGiftValidTime", SqlDbType.DateTime),
                        new SqlParameter("@preorderGiftValidDay",SqlDbType.Int),
                        new SqlParameter("@preorderGiftValid",SqlDbType.Int),
                        new SqlParameter("@shopID",SqlDbType.Int,4)
                     };
                    parameters[0].Value = shop.preorderGiftTitle;
                    parameters[1].Value = shop.preorderGiftDesc;
                    parameters[2].Value = shop.preorderGiftValidTimeType;
                    parameters[3].Value = shop.preorderGiftValidTime;
                    parameters[4].Value = shop.preorderGiftValidDay;
                    parameters[5].Value = shop.preorderGiftValid;
                    parameters[6].Value = shop.shopID;
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (System.Exception)
                {
                    return false;
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
        /// 根据公司编号查询对应的店铺信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectCompanyShop(int companyID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [shopID],[companyID],[shopName],[shopAddress],");
            strSql.Append("[shopTelephone],[shopLogo],[shopBusinessLicense],");
            strSql.Append("[shopHygieneLicense],[contactPerson],[contactPhone],");
            strSql.Append("[canTakeout],[canEatInShop],[provinceID],[cityID],");
            strSql.Append("[countyID],[shopStatus],[isHandle],[shopImagePath],");
            strSql.Append("[shopDescription],[prePayCashBackCount],[prePayVIPCount],");
            strSql.Append("[prePaySendGiftCount],[sinaWeiboName],[shopRating],[publicityPhotoPath],[acpp]");
            strSql.AppendFormat(" from ShopInfo where companyID = '{0}' and shopStatus > '0' order by shopID desc", companyID);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据店铺编号查询对应店铺信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectShop(int shopID)
        {
            const string strSql = @"select [shopID],[companyID],[shopName],[shopAddress],
[shopTelephone],[shopLogo],[shopBusinessLicense],[shopHygieneLicense],[contactPerson],[contactPhone],
[canTakeout],[canEatInShop],[provinceID],[cityID],[countyID],[shopStatus],[isHandle],[shopImagePath],
[shopDescription],[prePayCashBackCount],[prePayVIPCount],[prePaySendGiftCount],[sinaWeiboName],
[preorderGiftTitle],[preorderGiftDesc],[preorderGiftValidTimeType],[preorderGiftValidTime],
[preorderGiftValidDay],[preorderGiftValid],[qqWeiboName],[wechatPublicName],[openTimes],
isSupportAccountsRound,shopRating,publicityPhotoPath,acpp ,isSupportPayment,orderDishDesc,
notPaymentReason,accountManager,bankAccount,isSupportRedEnvelopePayment,[shopLevel],remainMoney,AreaManager
 from ShopInfo where shopID = @shopID and shopStatus=1";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@shopID", shopID) };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), sqlParameters);
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据店铺编号查询对应店铺信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetShop(int shopID)
        {
            const string strSql = @"select [shopID],[companyID],[shopName],[shopAddress],
[shopTelephone],[shopLogo],[shopBusinessLicense],[shopHygieneLicense],[contactPerson],[contactPhone],
[canTakeout],[canEatInShop],[provinceID],[cityID],[countyID],[shopStatus],[isHandle],[shopImagePath],
[shopDescription],[prePayCashBackCount],[prePayVIPCount],[prePaySendGiftCount],[sinaWeiboName],
[preorderGiftTitle],[preorderGiftDesc],[preorderGiftValidTimeType],[preorderGiftValidTime],
[preorderGiftValidDay],[preorderGiftValid],[qqWeiboName],[wechatPublicName],[openTimes],
isSupportAccountsRound,shopRating,publicityPhotoPath,acpp ,isSupportPayment,orderDishDesc,
notPaymentReason,accountManager,bankAccount,isSupportRedEnvelopePayment,[shopLevel],remainMoney,AreaManager
 from ShopInfo where shopID = @shopID";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@shopID", shopID) };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), sqlParameters);
            return ds.Tables[0];
        }

        public ShopInfo SelectShopInfo(int shopId)
        {
            const string strSql = @"select [shopID],[companyID],[shopName],[shopAddress],[shopLevel],
            [shopTelephone],[shopLogo],[shopBusinessLicense],
            [shopHygieneLicense],[contactPerson],[contactPhone],
            [canTakeout],[canEatInShop],[provinceID],[cityID],
            [countyID],[shopStatus],[isHandle],[shopImagePath],
            [shopDescription],[sinaWeiboName],
            [preorderGiftTitle],[preorderGiftDesc],
            [preorderGiftValidTimeType],[preorderGiftValidTime],
            [preorderGiftValidDay],[preorderGiftValid],[qqWeiboName],[wechatPublicName],
            [openTimes],[shopRegisterTime],[shopVerifyTime],isSupportAccountsRound,shopRating,publicityPhotoPath,acpp
             ,isSupportPayment,orderDishDesc,notPaymentReason,accountManager,bankAccount,isSupportRedEnvelopePayment,prepayOrderCount
            from ShopInfo where shopID = @shopId and shopStatus =1";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("shopId",SqlDbType.Int){ Value = shopId }
            };
            ShopInfo shopInfo = new ShopInfo();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                if (sdr.Read())
                {
                    shopInfo = sdr.GetEntity<ShopInfo>();
                }
            }
            return shopInfo;
        }
        /// <summary>
        /// 查询店铺信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectShop()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [shopID],[companyID],[shopName],[shopAddress],");
            strSql.Append("[shopTelephone],[shopLogo],[shopBusinessLicense],");
            strSql.Append("[shopHygieneLicense],[contactPerson],[contactPhone],");
            strSql.Append("[canTakeout],[canEatInShop],[provinceID],[cityID],");
            strSql.Append("[countyID],[shopStatus],[isHandle],[shopImagePath],");
            strSql.Append("[shopDescription],[prePayCashBackCount],[prePayVIPCount],");
            strSql.Append("[prePaySendGiftCount],[sinaWeiboName],");
            strSql.Append("[preorderGiftTitle],[preorderGiftDesc],");
            strSql.Append("[preorderGiftValidTimeType],[preorderGiftValidTime],");
            strSql.Append("[preorderGiftValidDay],[preorderGiftValid],[qqWeiboName],[wechatPublicName],[shopRating]");
            strSql.AppendFormat(" from ShopInfo where  shopStatus > '0' order by shopId desc");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 查询店铺信息含公司信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectShopAndCompany()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [shopID],[shopName],[companyName],[remainMoney]-isnull([amountFrozen],0) as remainMoney");
            strSql.Append(" from ShopInfo a inner join CompanyInfo b on a.companyID=b.companyID");
            strSql.Append(" where  shopStatus > '0' order by shopId desc");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据店铺名模糊匹配 某城市店铺信息 2013-09-02 微信平台管理 餐厅推荐用
        /// </summary>
        /// <param name="shopName"></param>
        /// <returns></returns>
        public DataTable SelectShopByName(string shopName, string cityName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.[shopID],a.[shopName],a.[shopAddress],");
            strSql.Append("a.[shopTelephone],a.[shopLogo],a.[shopBusinessLicense],");
            strSql.Append("a.[shopHygieneLicense],a.[contactPerson],a.[contactPhone],");
            strSql.Append("a.[canTakeout],a.[canEatInShop],a.[provinceID],a.[cityID],b.[cityName],");
            strSql.Append(" ISNULL(c.recommandType,-1) recommandType ");
            //strSql.Append("[countyID],[shopStatus],[isHandle],[shopImagePath],");
            //strSql.Append("[shopDescription],[prePayCashBackCount],[prePayVIPCount],");
            //strSql.Append("[prePaySendGiftCount],[sinaWeiboName],");
            //strSql.Append("[preorderGiftTitle],[preorderGiftDesc],");
            //strSql.Append("[preorderGiftValidTimeType],[preorderGiftValidTime],");
            //strSql.Append("[preorderGiftValidDay],[preorderGiftValid],[qqWeiboName],[wechatPublicName]");
            strSql.Append(" from ShopInfo a inner join City b on a.cityID = b.cityID ");
            strSql.Append(" left join dbo.WechatRecommandShopInfo c on a.shopID = c.shopID ");
            strSql.AppendFormat(" where  a.shopStatus > '0' and a.[shopName] like '%{0}%' and b.cityName='{1}'", shopName, cityName);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 添加店铺的经纬度
        /// </summary>
        /// <param name="shop"></param>
        /// <returns></returns>
        public int InsertShopCoordinate(ShopCoordinate shopCoordinate)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into ShopCoordinate(");
                    strSql.Append("shopId,mapId,longitude,latitude)");
                    strSql.Append(" values (");
                    strSql.Append("@shopId,@mapId,@longitude,@latitude)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
					    new SqlParameter("@shopId", SqlDbType.Int,4),
                        new SqlParameter("@mapId", SqlDbType.Int,4),
                        new SqlParameter("@longitude", SqlDbType.Float),
                        new SqlParameter("@latitude",SqlDbType.Float),
                    };
                    parameters[0].Value = shopCoordinate.shopId;
                    parameters[1].Value = shopCoordinate.mapId;
                    parameters[2].Value = shopCoordinate.longitude;
                    parameters[3].Value = shopCoordinate.latitude;
                    //
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
        /// 获取某个城市的坐标
        /// </summary>
        /// <param name="mapId"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public ShopCoordinate SelectShopCoordinate(int mapId, int shopId)
        {
            const string strSql = "select [shopCoordinateId],[shopId],[mapId],[longitude],[latitude] from ShopCoordinate where mapId=@mapId and shopId=@shopId";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@mapId",SqlDbType.Int){ Value = mapId },
            new SqlParameter("@shopId",SqlDbType.Int){ Value = shopId }
            };
            ShopCoordinate shopCoordinate = new ShopCoordinate();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                if (sdr.Read())
                {
                    shopCoordinate = sdr.GetEntity<ShopCoordinate>();
                }
            }
            return shopCoordinate;
        }
        /// <summary>
        /// 删除某个城市的坐标，修改操作是先删除，再添加
        /// </summary>
        /// <param name="mapId"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public bool DeleteShopCoordinate(int mapId, int shopId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("delete ShopCoordinate where mapId={0} and shopId={1}", mapId, shopId);
            int deleteResult = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            if (deleteResult >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 根据公司编号和城市编号查询对应的门店信息
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public DataTable SelectShopByCompanyAndCity(int companyId, int cityId, VAAppType appType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select A.[shopID],[companyID],[shopName],[shopAddress],");
            strSql.Append("[shopTelephone],[shopLogo],[shopBusinessLicense],");
            strSql.Append("[shopHygieneLicense],[contactPerson],[contactPhone],");
            strSql.Append("[canTakeout],[canEatInShop],[provinceID],[cityID],");
            strSql.Append("[countyID],[shopStatus],[isHandle],[shopImagePath],");
            strSql.Append("[shopDescription],[prePayCashBackCount],[prePayVIPCount],");
            strSql.Append("[prePaySendGiftCount],[sinaWeiboName],[shopCoordinateId],[openTimes],B.[shopId],B.[mapId],[longitude],[latitude],[mapName],[mapStatus],[shopRating]");
            strSql.Append(",shopRating,publicityPhotoPath,acpp");
            strSql.Append(" from ShopInfo as A inner join ShopCoordinate as B on A.shopID = B.shopId");
            strSql.Append(" inner join dbo.MapInfo as C on C.mapId = B.mapId");
            switch (appType)
            {
                case VAAppType.ANDROID:
                case VAAppType.WAP:
                    {//百度地图
                        strSql.Append(" and C.mapId = 2");
                    }
                    break;
                case VAAppType.IPHONE:
                case VAAppType.IPAD:
                default:
                    {//谷歌地图
                        strSql.Append(" and C.mapId = 1");
                    }
                    break;
            }
            strSql.AppendFormat(" where C.mapStatus = '" + (int)VAMapStatus.IN_USE + "' and A.cityID = {0} and A.companyID = {1}", cityId, companyId);
            strSql.AppendFormat(" and A.shopStatus > '0' and A.isHandle = '" + (int)VAShopHandleStatus.SHOP_Pass + "'");

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据菜单编号查询对应的门店编号（该函数已弃用）
        /// 早期一个店铺对应一个菜单模式时使用
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public int SelectShopIdByMenuId(int menuId)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT A.[shopId] from MenuConnShop as A inner join MenuInfo as B on A.menuId = B.MenuID");
                strSql.Append(" inner join ShopInfo as C on C.shopID = A.shopId");
                strSql.AppendFormat(" where B.MenuStatus > 0 and A.menuId = {0}", menuId);
                strSql.AppendFormat(" and C.shopStatus > '0' and C.isHandle = '" + (int)VAShopHandleStatus.SHOP_Pass + "'");
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

                if (ds.Tables[0].Rows.Count == 1)
                {
                    return Convert.ToInt32(ds.Tables[0].Rows[0]["shopId"]);
                }
                else
                {
                    return 0;
                }

            }
            catch (System.Exception)
            {
                return 0;
            }
        }
        /// <summary>
        /// 更新店铺各类优惠计数
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="prepayCashBack"></param>
        /// <param name="prepayVIP"></param>
        /// <param name="prePayGift"></param>
        /// <returns></returns>
        public bool UpdateShopPrepayPrevilegeCount(int shopId, int prepayCashBack, int prepayVIP, int prePayGift)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update ShopInfo set ");
                    strSql.Append("prePayCashBackCount=isnull(prePayCashBackCount, 0) + @prePayCashBackCount,");
                    strSql.Append("prePayVIPCount=isnull(prePayVIPCount, 0) + @prePayVIPCount,");
                    strSql.Append("prePaySendGiftCount=isnull(prePaySendGiftCount, 0) + @prePaySendGiftCount");
                    strSql.Append(" where shopID=@shopID ");
                    SqlParameter[] parameters = {
                        new SqlParameter("@prePayCashBackCount",SqlDbType.SmallInt,2),
                        new SqlParameter("@prePayVIPCount",SqlDbType.SmallInt,2),
                        new SqlParameter("@prePaySendGiftCount",SqlDbType.SmallInt,2),
                        new SqlParameter("@shopID",SqlDbType.Int,4)};
                    parameters[0].Value = prepayCashBack;
                    parameters[1].Value = prepayVIP;
                    parameters[2].Value = prePayGift;
                    parameters[3].Value = shopId;

                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (System.Exception)
                {
                    return false;
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
        /// 添加店铺的详情展示图片的信息
        /// </summary>
        /// <param name="shop"></param>
        /// <returns></returns>
        public int InsertShopRevealPicture(int shopId, string uploadImageName, int status)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into ShopRevealImage(");
                    strSql.Append("shopId,revealImageName,status)");
                    strSql.Append(" values (");
                    strSql.Append("@shopId,@revealImageName,@status)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
					    new SqlParameter("@shopId", SqlDbType.Int,4),
                        new SqlParameter("@revealImageName", SqlDbType.NVarChar,50),
                        new SqlParameter("@status", SqlDbType.Int)
                    };
                    parameters[0].Value = shopId;
                    parameters[1].Value = uploadImageName;
                    parameters[2].Value = status;
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
        /// 根据公司查询对应的门店名称和id信息
        /// </summary>
        public DataTable SelectShopInfoByCompanyId(int companyId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [shopId],[shopName]");
            strSql.Append(" from [ShopInfo] inner join CompanyInfo on CompanyInfo.companyID=ShopInfo.companyID");
            strSql.AppendFormat(" where CompanyInfo.companyID= {0} AND [ShopInfo].shopStatus = 1", companyId);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 上线门店名称和id信息
        /// </summary>
        public DataTable SelectHandShopInfoByCompanyId(string shopName, int cityId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [shopId],[shopName]");
            strSql.Append(" from [ShopInfo] ");
            strSql.AppendFormat(" where cityId={0} and  shopName like '%{1}%'", cityId, shopName);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据门店Id查询对应的门店详情图片信息
        /// </summary>
        public DataTable SelectShopRevealImageInfo(int shopId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT id, [shopId],[revealImageName],[status]");
            strSql.Append(" from [ShopRevealImage]");
            if (shopId > 0)
            {
                strSql.AppendFormat(" where shopId= {0}", shopId);
            }
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 删除门店环境图片
        /// </summary>
        /// <param name="imageID"></param>
        public bool DeleteShopRevealImageInfo(string revealImageName, int shopId)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("delete from ShopRevealImage where revealImageName=@revealImageName and shopId=@shopId");
                    SqlParameter[] parameters = {					
					new SqlParameter("@revealImageName", SqlDbType.NVarChar,50),new SqlParameter("@shopId", SqlDbType.NVarChar,50)};
                    parameters[0].Value = revealImageName;
                    parameters[1].Value = shopId;
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (Exception)
                {
                    return false;
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
        public bool DeleteShopRevealImageInfo(long id)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("delete from ShopRevealImage where id=@id");
                    SqlParameter[] parameters = {					
					new SqlParameter("@id", SqlDbType.BigInt,8)};
                    parameters[0].Value = id;
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (Exception)
                {
                    return false;
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
        /// 根据店铺ID查询营业时间
        /// </summary>
        /// <param name="shopid"></param>
        /// <returns></returns>
        public string GetOpenTime(int shopid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select openTimes from ShopInfo where shopID ='{0}'", shopid);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0].Rows[0]["openTimes"].ToString();

        }
        /// <summary>
        /// 获取勋章
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public DataTable GetMedal(int shopId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MedalConnShopCompany.medalName,MedalConnShopCompany.medalDescription,tmp.medalURL,tmp.smallmedalURL from MedalConnShopCompany,(");
            strSql.Append("select A.medalId,A.medalURL as medalURL,B.medalURL as smallmedalURL from MedalImageInfo as A,");
            strSql.AppendFormat("MedalImageInfo as B where A.medalId =B.medalId and A.medalScale ='{0}' and B.medalScale ='{1}')tmp where ", (int)VAMedalImageType.MEDAL_IMAGE_BIG, (int)VAMedalImageType.MEDAL_IMAGE_SMALL);
            strSql.AppendFormat("MedalConnShopCompany.id =tmp.medalId and MedalConnShopCompany.medalType ='{0}' and MedalConnShopCompany.companyOrShopId ='{1}'", (int)VAMedalType.MEDAL_SHOP, shopId);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];

        }
        /// <summary>
        /// 获取门店的免返送描述信息
        /// </summary>
        /// <param name="shopid"></param>
        /// <returns></returns>
        public DataTable GetQueryListMFS(int shopid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select description,descriptionType from FreeToReturnToSend where shopId ='{0}'", shopid);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];

        }
        public int QueryCouponCount(long CustomerID, int shopId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select COUNT(*)from CustomerConnCoupon as A inner join CouponConnShop as B  on A.couponID =B.couponID inner join CouponInfo as C on C.couponID =A.couponID where A.status='1' and ");
            strSql.AppendFormat(" B.shopID ='{0}' and A.customerID='{1}' and '{2}' >A.couponValidStartTime and '{3}' <A.couponValidEndTime", shopId, CustomerID, System.DateTime.Now, System.DateTime.Now);

            //strSql.AppendFormat(" and CustomerCouponPreOrder.customerID=CustomerConnCoupon.customerID");

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

            return Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());

        }
        public DataTable QueryImageURL(int shopid)
        {
            List<string> liShopImage = new List<string>();
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select revealImageName from ShopRevealImage where status ='1' and shopId ='{0}'", shopid);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];

        }
        /// <summary>
        /// 根据公司查询门店信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectShopByCompany(int companyId, bool flag = false)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ShopInfo.shopID,ShopInfo.shopName,ShopInfo.shopImagePath,CompanyInfo.companyId");
            strSql.Append(",ShopInfo.[isHandle] FROM");
            strSql.Append(" ShopInfo ");
            strSql.Append(" INNER JOIN CompanyInfo ON ShopInfo.companyID = CompanyInfo.companyID");
            strSql.AppendFormat(" where ShopInfo.shopStatus > '0' and CompanyInfo.companyStatus > '0' and CompanyInfo.companyId='{0}'", companyId);
            if (flag)
            {
                strSql.Append(" and ShopInfo.isHandle='" + (int)VAShopHandleStatus.SHOP_Pass + "'");
            }
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 201401 created by wangcheng
        /// 查询获取收藏门店信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectCustomerFavoriteShop(long customerId, int shopId)
        {
            const string strSql = "SELECT  [id],[customerId],[companyId],[collectTime],[shopId]  FROM [CustomerFavoriteCompany] where customerId=@customerId and shopId=@shopId";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@customerId",SqlDbType.BigInt) { Value = customerId },
            new SqlParameter("@shopId",SqlDbType.Int) { Value = shopId }
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para);
            return ds.Tables[0];
        }
        /// <summary>
        /// 查询门店vip信息20140313
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public DataTable SelectShopVipInfo(int shopId)
        {
            const string strSql = @"SELECT id,platformVipId,name,discount,shopId,status FROM shopVipInfo where shopId=@shopId and status>0 and discount<1";
            SqlParameter[] parameter = new SqlParameter[] { new SqlParameter("@shopId", SqlDbType.Int, 4) };
            parameter[0].Value = shopId;
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter);
            return ds.Tables[0];
        }
        /// <summary>
        /// 收银宝后台查询门店VIP等级和对应的平台等级信息
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public DataTable SelectShopVipInfoAndViewAllocPlatformVipInfo(int shopId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT A.id, [platformVipId],A.[name],B.name platformVipName,discount FROM [shopVipInfo] A");
            strSql.Append(" inner join dbo.ViewAllocPlatformVipInfo B on A.platformVipId=B.id");
            strSql.AppendFormat(" where shopId={0} and A.status>0 and B.status>0", shopId);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 新增店铺VIP折扣信息
        /// </summary>
        /// <param name="platformVipId">平台VIP ID</param>
        /// <param name="name">VIP名称</param>
        /// <param name="shopId">店铺ID</param>
        /// <param name="discount">折扣</param>
        /// <returns></returns>
        public int InsertShopVipInfo(int platformVipId, string name, int shopId, double discount)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("insert into shopVipInfo(");
                    strSql.Append("platformVipId,name,shopId,discount,status)");
                    strSql.Append(" values(@platformVipId,@name,@shopId,@discount,@status)");
                    strSql.Append(" select @@identity");
                    SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@platformVipId",SqlDbType.Int),
                new SqlParameter("@name",SqlDbType.NVarChar,50),
                new SqlParameter("@shopId",SqlDbType.Int),
                new SqlParameter("@discount",SqlDbType.Decimal),
                new SqlParameter("@status",SqlDbType.Int)};
                    para[0].Value = platformVipId;
                    para[1].Value = name;
                    para[2].Value = shopId;
                    para[3].Value = discount;
                    para[4].Value = 1;
                    obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), para);
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
        /// 修改店铺VIP折扣信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="discount"></param>
        /// <returns></returns>
        public object[] UpdateShopVipInfo(int id, string name, int platformVipId, double discount)
        {
            object[] result = new object[] { false, "" };
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update shopVipInfo set");
                strSql.Append(" discount=@discount,");
                strSql.Append(" name=@name,");
                strSql.Append(" platformVipId=@platformVipId");
                strSql.Append(" where id=@id");

                SqlParameter[] para = new SqlParameter[] {
                    new SqlParameter("@discount",SqlDbType.Decimal),
                     new SqlParameter("@name",SqlDbType.NVarChar),
                       new SqlParameter("@platformVipId",SqlDbType.Int),
                    new SqlParameter("@id",SqlDbType.Int)
                };
                para[0].Value = discount;
                para[1].Value = name;
                para[2].Value = platformVipId;
                para[3].Value = id;

                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();
                    int res = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), para);
                    if (res > 0)
                    {
                        result[0] = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result[1] = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 删除店铺VIP折扣信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object[] DeleteShopVipInfo(int id)
        {
            object[] result = new object[] { false, "" };
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update shopVipInfo set");
                strSql.Append(" status = -1");
                strSql.AppendFormat(" where id = '{0}'", id);

                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();
                    result[0] = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString());
                }
            }
            catch (Exception ex)
            {
                result[1] = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 根据店铺ID获取相应的下一级VIP等级ID及名称
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public DataTable GetAssignVipInfoForShop(int shopId)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select v.id,v.name");
                strSql.Append(" from shopVipInfo s inner join ViewAllocPlatformVipInfo v");
                strSql.Append(" on s.platformVipId+1 = v.id and s.status > 0 and v.status > 0");
                strSql.AppendFormat(" and s.shopId = '{0}'", shopId);

                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
                return ds.Tables[0];
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 根据门店查询公司编号和菜单编号
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public DataTable GetPartShopInfo(int shopId)
        {
            const string strSql = @"select ShopInfo.shopID,ShopInfo.companyID,ShopInfo.isSupportPayment,ShopInfo.shopName,MenuConnShop.MenuID,ShopInfo.notPaymentReason
,ShopInfo.isHandle,ShopInfo.isSupportAccountsRound,ShopInfo.isSupportRedEnvelopePayment
  from ShopInfo inner join MenuConnShop on ShopInfo.shopID=MenuConnShop.shopId INNER JOIN MenuInfo on MenuInfo.MenuID=MenuConnShop.menuId 
 where ShopInfo.shopStatus>0 and ShopInfo.shopId =@shopId and MenuInfo.MenuStatus=1";
            SqlParameter parameter = new SqlParameter("@shopId", SqlDbType.Int, 4) { Value = shopId };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter);
            return ds.Tables[0];
        }
        /// <summary>
        /// 客户端查询所有收藏门店信息（wangcheng）
        /// </summary>
        /// <param name="appType">客户端APP类型</param>
        /// <param name="cityId">城市编号</param>
        /// <returns></returns>
        //public IDataReader ClientSelectAllFavoriteShop(VAAppType appType, int cityId, long customerId)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select distinct CompanyInfo.companyID ,CompanyInfo.companyName,ShopInfo.[prepayOrderCount],ShopInfo.[shopID],ShopInfo.[shopName],ShopInfo.[shopAddress],[shopTelephone],[shopLogo],CompanyInfo.defaultMenuId,");
        //    strSql.Append(" [shopImagePath],[shopDescription],ShopInfo.[prePayCashBackCount],ShopInfo.[prePayVIPCount],ShopInfo.[prePaySendGiftCount],[openTimes],[longitude] ");
        //    strSql.Append(",[latitude],[shopRating],publicityPhotoPath,ShopInfo.acpp,ShopInfo.isSupportAccountsRound,MenuConnShop.menuId,ShopInfo.orderDishDesc");
        //    strSql.AppendFormat(" from CompanyInfo inner join ShopInfo  on CompanyInfo.companyID = ShopInfo.companyID");
        //    strSql.Append(" inner join ShopCoordinate on ShopInfo.shopID = ShopCoordinate.shopId inner join MapInfo on ShopCoordinate.mapId = MapInfo.mapId");
        //    strSql.Append(" inner join MenuConnShop on ShopInfo.shopID=MenuConnShop.shopId  ");
        //    strSql.Append(" inner join CustomerFavoriteCompany on   CustomerFavoriteCompany.shopId=ShopInfo.shopID");
        //    strSql.Append(" inner join MenuInfo on MenuInfo.MenuID=MenuConnShop.menuId");
        //    strSql.AppendFormat(" where  CompanyInfo.companyStatus > 0 and ShopInfo.isHandle = 1 and ShopInfo.shopStatus > 0 and ShopInfo.cityID ={0}", cityId);
        //    strSql.AppendFormat(" and CustomerFavoriteCompany.customerId={0}", customerId);
        //    strSql.Append(" and MenuInfo.MenuStatus=1");
        //    switch (appType)
        //    {
        //        case VAAppType.ANDROID:
        //        case VAAppType.WAP:
        //        case VAAppType.IPHONE:
        //        case VAAppType.IPAD:
        //        default:
        //            strSql.AppendFormat(" and ShopCoordinate.mapId = 2"); break;//百度地图
        //    }
        //    SqlParameter[] parm = new SqlParameter[] { };
        //    IDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parm);
        //    return dr;
        //}
        /// <summary>
        /// 积分商城：查询店铺排行榜
        /// </summary>
        /// <param name="startTime">审核开始时间</param>
        /// <param name="endTime">审核结束时间</param>
        /// <param name="cityId">城市ID</param>
        /// <param name="orderBy">排序方式：ASC,DESC</param>
        /// <returns></returns>
        public DataTable QueryShopRanking(string startTime, string endTime, string cityId, string orderBy)
        {
            StringBuilder strSql = new StringBuilder();
            if (orderBy == "asc")
            {
                strSql.Append("select row_number() over(order by a.ApprovedMoney asc) as row_number,* from ");
            }
            else
            {
                strSql.Append("select row_number() over(order by a.ApprovedMoney desc) as row_number,* from ");
            }
            strSql.Append(" (select S.shopID,S.shopName, round(sum(isnull( O.prePaidSum,0)- isnull(O.refundMoneySum,0)), 2) ApprovedMoney");
            strSql.Append(" from ShopInfo S inner join");
            strSql.Append(" PreOrder19dian O on S.shopID = O.shopId");
            strSql.Append(" inner join PreorderCheckInfo C");
            strSql.Append(" on O.preOrder19dianId = C.preOrder19dianId");
            strSql.Append(" and S.shopStatus = 1");
            strSql.Append(" and C.status = 1");
            strSql.Append(" and S.isHandle = 1");//已上线店铺
            strSql.Append(" and O.isApproved = 1");//已对账
            if (cityId != "")
            {
                strSql.AppendFormat(" and S.cityID = '{0}'", cityId);
            }
            if (startTime != "")
            {
                strSql.AppendFormat(" and C.checkTime >= '{0}'", startTime);
            }
            if (endTime != "")
            {
                strSql.AppendFormat(" and C.checkTime < '{0}'", endTime);
            }
            strSql.Append(" group by S.shopID,S.shopName) a;");

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        #region 门店微信公共帐号点菜配置 add by wangc 20140317
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="shopId"></param>
        /// <param name="status"></param>
        /// <param name="wechatOrderUrl"></param>
        /// <returns></returns>
        public long InsertShopWechatOrderConfig(string cookie, int shopId, string wechatOrderUrl)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into ShopWechatOrderConfig(");
                strSql.Append("cookie,shopId,status,createdTime,wechatOrderUrl)");
                strSql.Append(" values (");
                strSql.Append("@cookie,@shopId,@status,@createdTime,@wechatOrderUrl)");
                strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
					new SqlParameter("@cookie", SqlDbType.NVarChar,100),
					new SqlParameter("@shopId", SqlDbType.Int,4),
					new SqlParameter("@status", SqlDbType.Int,4),
					new SqlParameter("@createdTime", SqlDbType.DateTime),
					new SqlParameter("@wechatOrderUrl", SqlDbType.NVarChar,200)};
                parameters[0].Value = cookie;
                parameters[1].Value = shopId;
                parameters[2].Value = 1;
                parameters[3].Value = DateTime.Now;
                parameters[4].Value = wechatOrderUrl;
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters);
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
        /// 获得数据列表
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public DataTable SelectShopWechatOrderConfig(int shopId, int companyId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,cookie,ShopWechatOrderConfig.shopId,status,createdTime,wechatOrderUrl,shopName ");
            strSql.Append(" FROM ShopWechatOrderConfig ");
            strSql.Append(" inner join ShopInfo on ShopInfo.shopID=ShopWechatOrderConfig.shopId");
            if (shopId > 0)
            {
                strSql.AppendFormat(" where ShopWechatOrderConfig.shopId={0}", shopId);
            }
            else
            {
                if (companyId > 0)
                {
                    strSql.AppendFormat(" where ShopInfo.companyId={0}", companyId);
                }
            }
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 更新当前记录状态
        /// </summary>
        /// <param name="id">记录主键ID</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        public bool UpdateShopWechatOrderConfigStatus(long id, int status)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update ShopWechatOrderConfig set ");
                strSql.Append("status=@status");
                strSql.Append(" where id=@id");
                SqlParameter[] parameters = {
					new SqlParameter("@status", SqlDbType.Int,4),
					new SqlParameter("@id", SqlDbType.BigInt,8)};
                parameters[0].Value = status;
                parameters[1].Value = id;
                int rows = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                if (rows > 0)
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
        /// 根据Id查询唯一信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable SelectShopWechatOrderConfigById(long id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,cookie,shopId,status,createdTime,wechatOrderUrl from ShopWechatOrderConfig ");
            strSql.AppendFormat(" where id={0}", id);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        #endregion
        /// <summary>
        /// 查询当前门店所有服务员手机号码  add by wangc 20140324
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public DataTable SelectShopEmployeePhone(int shopId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct A.UserName phone,C.shopName from EmployeeInfo A");
            strSql.Append(" inner join EmployeeConnShop B on A.EmployeeID=B.employeeID");
            strSql.Append(" left join ShopInfo C on C.shopID=B.shopID");
            strSql.Append(" inner join EmployeeShopAuthority D on D.employeeConnShopId=B.employeeShopID");
            strSql.Append(" inner join ShopAuthority E on D.shopAuthorityId = E.shopAuthorityId");
            strSql.Append(" where (A.isViewAllocWorker is null or A.isViewAllocWorker=0)");
            strSql.Append(" and B.status=1 and A.EmployeeStatus=1 ");
            strSql.AppendFormat(" and E.shopAuthorityStatus=1 and D.employeeShopAuthorityStatus=1 and B.shopID={0}", shopId);
            strSql.Append("  and E.authorityCode='5'");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取指定城市指定个数的shopLogo
        /// Add at 2014-4-2
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="logoImageCount"></param>
        /// <returns></returns>
        public DataTable QueryShopLogo(int cityId, int logoImageCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select top {0} s.shopID,s.shopName,s.shopImagePath+s.shopLogo shopLogo,s.shopRegisterTime", logoImageCount);
            strSql.Append(" from ShopInfo s");
            strSql.Append(" where s.isHandle = 1 and s.shopStatus = 1");
            strSql.Append(" and s.shopLogo is not null and s.shopLogo != ''");
            strSql.AppendFormat("and s.cityID = {0}", cityId);
            strSql.Append(" order by s.shopRegisterTime desc");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 查询所有门店的审核状态信息
        /// </summary>
        /// <returns></returns>
        public List<ShopHandleListInfo> SybSelectShopHandleList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT ShopInfo.shopID,shopName,companyName,isHandle");
            strSql.Append(" from ShopInfo INNER JOIN CompanyInfo on ShopInfo.companyID=CompanyInfo.companyID");
            strSql.Append(" where CompanyInfo.companyStatus=1 and ShopInfo.shopStatus=1 order by ShopInfo.shopID desc");
            List<ShopHandleListInfo> list = new List<ShopHandleListInfo>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString()))
            {
                while (dr.Read())
                {
                    list.Add(new ShopHandleListInfo()
                    {
                        companyName = dr["companyName"] == DBNull.Value ? "" : Convert.ToString(dr["companyName"]),
                        handleStatus = dr["isHandle"] == DBNull.Value ? -2 : Convert.ToInt32(dr["isHandle"]),
                        shopId = dr["shopId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["shopId"]),
                        shopName = dr["shopName"] == DBNull.Value ? "" : Convert.ToString(dr["shopName"])
                    });
                }
                dr.Close();
            }
            return list;
        }


        public List<ShopInfo> GetListByQuery(int pageSize, int pageIndex, ShopInfoQueryObject queryObject = null)
        {
            string sql = @"SELECT ROW_NUMBER() OVER( ORDER BY  shopID DESC) AS ROWNUM
                                ,[shopID]
                                ,[companyID]
                                ,[shopName]
                                ,[shopAddress]
                                ,[shopTelephone]
                                ,[shopLogo]
                                ,[shopBusinessLicense]
                                ,[shopHygieneLicense]
                                ,[contactPerson]
                                ,[contactPhone]
                                ,[canTakeout]
                                ,[canEatInShop]
                                ,[provinceID]
                                ,[cityID]
                                ,[countyID]
                                ,[shopStatus]
                                ,[isHandle]
                                ,[shopImagePath]
                                ,[shopDescription]
                                ,[prePayCashBackCount]
                                ,[prePayVIPCount]
                                ,[prePaySendGiftCount]
                                ,[sinaWeiboName]
                                ,[preorderCount]
                                ,[prepayOrderCount]
                                ,[preorderGiftTitle]
                                ,[preorderGiftDesc]
                                ,[preorderGiftValidTimeType]
                                ,[preorderGiftValidTime]
                                ,[preorderGiftValidDay]
                                ,[preorderGiftValid]
                                ,[qqWeiboName]
                                ,[wechatPublicName]
                                ,[openTimes]
                                ,[shopRegisterTime]
                                ,[shopVerifyTime]
                                ,[isSupportAccountsRound]
                                ,[remainMoney]
                                ,[totalMoney]
                                ,[shopRating]
                                ,[publicityPhotoPath]
                                ,[acpp]
                                ,[isSupportPayment]
                                ,[orderDishDesc]
                                ,[notPaymentReason]
                                ,[accountManager]
                                ,[bankAccount]
                                ,[isSupportRedEnvelopePayment]
                                ,[ShopLevel] 
                            FROM  [ShopInfo] 
                            WHERE 1 =1 ";
            SqlParameter[] sqlParameters = null;
            StringBuilder whereSqlBuilder = new StringBuilder();
            GetWhereSqlBuilderAndSqlParameterList(queryObject, ref sqlParameters, whereSqlBuilder, pageSize, pageIndex);
            sql = whereSqlBuilder.Insert(0, sql).ToString();
            sql = string.Format(" SELECT TOP (@PageSize) * FROM ({0}) T WHERE T.ROWNUM > @PageIndex ", sql);
            List<ShopInfo> list = null;
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameters))
            {
                list = new List<ShopInfo>();
                while (reader.Read())
                {
                    list.Add(reader.GetEntity<ShopInfo>());
                }
            }
            return list;
        }

        public List<ShopInfo> GetListByQuery(ShopInfoQueryObject queryObject = null)
        {
            string sql = @"SELECT ROW_NUMBER() OVER( ORDER BY  shopID DESC) AS ROWNUM
                                ,[shopID]
                                ,[companyID]
                                ,[shopName]
                                ,[shopAddress]
                                ,[shopTelephone]
                                ,[shopLogo]
                                ,[shopBusinessLicense]
                                ,[shopHygieneLicense]
                                ,[contactPerson]
                                ,[contactPhone]
                                ,[canTakeout]
                                ,[canEatInShop]
                                ,[provinceID]
                                ,[cityID]
                                ,[countyID]
                                ,[shopStatus]
                                ,[isHandle]
                                ,[shopImagePath]
                                ,[shopDescription]
                                ,[prePayCashBackCount]
                                ,[prePayVIPCount]
                                ,[prePaySendGiftCount]
                                ,[sinaWeiboName]
                                ,[preorderCount]
                                ,[prepayOrderCount]
                                ,[preorderGiftTitle]
                                ,[preorderGiftDesc]
                                ,[preorderGiftValidTimeType]
                                ,[preorderGiftValidTime]
                                ,[preorderGiftValidDay]
                                ,[preorderGiftValid]
                                ,[qqWeiboName]
                                ,[wechatPublicName]
                                ,[openTimes]
                                ,[shopRegisterTime]
                                ,[shopVerifyTime]
                                ,[isSupportAccountsRound]
                                ,[remainMoney]
                                ,[totalMoney]
                                ,[shopRating]
                                ,[publicityPhotoPath]
                                ,[acpp]
                                ,[isSupportPayment]
                                ,[orderDishDesc]
                                ,[notPaymentReason]
                                ,[accountManager]
                                ,[bankAccount]
                                ,[isSupportRedEnvelopePayment]
                                ,[ShopLevel] 
                            FROM  [ShopInfo] 
                            WHERE 1 =1 ";
            SqlParameter[] sqlParameters = null;
            StringBuilder whereSqlBuilder = new StringBuilder();
            GetWhereSqlBuilderAndSqlParameterList(queryObject, ref sqlParameters, whereSqlBuilder);
            sql = whereSqlBuilder.Insert(0, sql).ToString();
            List<ShopInfo> list = null;
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameters))
            {
                list = new List<ShopInfo>();
                while (reader.Read())
                {
                    list.Add(reader.GetEntity<ShopInfo>());
                }
            }
            return list;
        }
        public long GetCountByQuery(ShopInfoQueryObject queryObject = null)
        {
            string sql = @"SELECT COUNT(0)
                            FROM  [ShopInfo] 
                            WHERE 1 =1 ";
            object retutnValue;
            SqlParameter[] sqlParameters = null;
            if (queryObject != null)
            {
                StringBuilder whereSqlBuilder = new StringBuilder();
                GetWhereSqlBuilderAndSqlParameterList(queryObject, ref sqlParameters, whereSqlBuilder);
                sql = whereSqlBuilder.Insert(0, sql).ToString();
            }
            retutnValue = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameters);
            int count;
            if (int.TryParse(retutnValue.ToString(), out count))
            {
                return count;
            }
            return 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryObject"></param>
        /// <param name="sqlParameterList"></param>
        /// <param name="whereSqlBuilder"></param>
        private static void GetWhereSqlBuilderAndSqlParameterList(ShopInfoQueryObject queryObject, ref SqlParameter[] sqlParameters,
            StringBuilder whereSqlBuilder, int? pageSize = null, int? pageIndex = null)
        {


            if (queryObject == null)
            {
                if (pageIndex.HasValue && pageSize.HasValue)
                {
                    sqlParameters = new SqlParameter[]
                    {
                        new SqlParameter("@PageSize", pageSize.Value),
                        new SqlParameter("@PageIndex", pageSize.Value * (pageIndex.Value - 1))
                    };
                }
                return;
            }
            var sqlParameterList = new List<SqlParameter>();
            if (queryObject.CityID.HasValue)
            {
                whereSqlBuilder.Append(" AND CityID = @CityID ");
                sqlParameterList.Add(new SqlParameter("@CityID ", queryObject.CityID.Value));
            }
            if (queryObject.ShopStatus.HasValue)
            {
                whereSqlBuilder.Append(" AND ShopStatus = @ShopStatus ");
                sqlParameterList.Add(new SqlParameter("@ShopStatus ", queryObject.ShopStatus.Value));
            }
            if (queryObject.IsHandle.HasValue)
            {
                whereSqlBuilder.Append(" AND IsHandle = @IsHandle ");
                sqlParameterList.Add(new SqlParameter("@IsHandle ", queryObject.IsHandle.Value));
            }
            if (!string.IsNullOrEmpty(queryObject.ShopNameFuzzy))
            {
                whereSqlBuilder.Append(" AND ShopName LIKE @ShopNameFuzzy ");
                sqlParameterList.Add(new SqlParameter("@ShopNameFuzzy", SqlDbType.NVarChar, 200) { Value = string.Format("%{0}%", queryObject.ShopNameFuzzy) });
            }
            if (!string.IsNullOrEmpty(queryObject.ShopName))
            {
                whereSqlBuilder.Append(" AND ShopName = @ShopName ");
                sqlParameterList.Add(new SqlParameter("@ShopName", SqlDbType.NVarChar, 200) { Value = queryObject.ShopName });
            }
            if (queryObject.RemainMoney.HasValue)
            {
                whereSqlBuilder.Append(" AND RemainMoney = @RemainMoney ");
                sqlParameterList.Add(new SqlParameter("@RemainMoney", SqlDbType.Float) { Value = queryObject.RemainMoney.Value });
            }
            if (queryObject.RemainMoneyFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND RemainMoney >= @RemainMoneyFrom ");
                sqlParameterList.Add(new SqlParameter("@RemainMoneyFrom", SqlDbType.Float) { Value = queryObject.RemainMoneyFrom.Value });
            }
            if (queryObject.RemainMoney.HasValue)
            {
                whereSqlBuilder.Append(" AND RemainMoney <= @RemainMoneyTo ");
                sqlParameterList.Add(new SqlParameter("@RemainMoneyTo", SqlDbType.Float) { Value = queryObject.RemainMoneyTo.Value });
            }
            if (queryObject.ShopRegisterTime.HasValue)
            {
                whereSqlBuilder.Append(" AND ShopRegisterTime = @ShopRegisterTime ");
                sqlParameterList.Add(new SqlParameter("@ShopRegisterTime", SqlDbType.DateTime) { Value = queryObject.ShopRegisterTime.Value });
            }
            if (queryObject.ShopRegisterTimeFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND ShopRegisterTime >= @ShopRegisterTimeFrom ");
                sqlParameterList.Add(new SqlParameter("@ShopRegisterTimeFrom", SqlDbType.DateTime) { Value = queryObject.ShopRegisterTimeFrom.Value });
            }
            if (queryObject.ShopRegisterTimeTo.HasValue)
            {
                whereSqlBuilder.Append(" AND ShopRegisterTime <= @ShopRegisterTimeTo ");
                sqlParameterList.Add(new SqlParameter("@ShopRegisterTimeTo", SqlDbType.DateTime) { Value = queryObject.ShopRegisterTimeTo.Value });
            }
            if (pageIndex.HasValue && pageSize.HasValue)
            {
                sqlParameterList.Add(new SqlParameter("@PageSize", pageSize));
                sqlParameterList.Add(new SqlParameter("@PageIndex", pageSize * (pageIndex - 1)));
            }
            sqlParameters = sqlParameterList.ToArray();
        }

        /// <summary>
        /// 查询某门店对应的收款人
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public string QueryShopAccountName(int shopId)
        {
            const string strSql = @"select a.accountName from ShopInfo s inner join CompanyAccounts a
on s.bankAccount = a.identity_Id and s.shopID=@shopId and a.status=1";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@shopId", SqlDbType.Int) { Value = shopId }
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql, para);
                if (obj == null)
                {
                    return "";
                }
                else
                {
                    return obj.ToString();
                }
            }
        }

        /// <summary>
        /// 修改商铺金额（批量打款提交到出纳）
        /// </summary>
        public bool UpdateShopMoneyApply(ShopInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ShopInfo set ");
            strSql.Append("remainMoney=remainMoney-@remainMoney");
            //strSql.Append("remainRedEnvelopeAmount=remainRedEnvelopeAmount-@remainRedEnvelopeAmount,");
            //strSql.Append("remainFoodCouponAmount=remainFoodCouponAmount-@remainFoodCouponAmount,");
            //strSql.Append("remainAlipayAmount=remainAlipayAmount-@remainAlipayAmount,");
            //strSql.Append("remainWechatPayAmount=remainWechatPayAmount-@remainWechatPayAmount,");
            //strSql.Append("remainCommissionAmount=remainCommissionAmount-@remainCommissionAmount ");
            strSql.Append(" where shopID=@shopID and cast((remainMoney-isnull(amountFrozen,0)-@remainMoney) as numeric(18,2))>=0");
            SqlParameter[] parameters = {
					new SqlParameter("@remainMoney", SqlDbType.Float),
                    //new SqlParameter("@remainRedEnvelopeAmount", SqlDbType.Float),
                    //new SqlParameter("@remainFoodCouponAmount", SqlDbType.Float),
                    //new SqlParameter("@remainAlipayAmount", SqlDbType.Float),
                    //new SqlParameter("@remainWechatPayAmount", SqlDbType.Float),
                    //new SqlParameter("@remainCommissionAmount", SqlDbType.Float),
                    new SqlParameter("@shopID", SqlDbType.Int)};
            parameters[0].Value = model.remainMoney;
            //parameters[1].Value = model.remainRedEnvelopeAmount;
            //parameters[2].Value = model.remainFoodCouponAmount;
            //parameters[3].Value = model.remainAlipayAmount;
            //parameters[4].Value = model.remainWechatPayAmount;
            //parameters[5].Value = model.remainCommissionAmount;
            parameters[1].Value = model.shopID;
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 修改商铺金额（返还到门店，批量打款提交到出纳或确认后的撤回及银行打款失败）
        /// </summary>
        public bool UpdateShopMoneyApplyBack(ShopInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ShopInfo set ");
            strSql.Append("remainMoney=remainMoney+@remainMoney");
            //strSql.Append("remainRedEnvelopeAmount=remainRedEnvelopeAmount+@remainRedEnvelopeAmount,");
            //strSql.Append("remainFoodCouponAmount=remainFoodCouponAmount+@remainFoodCouponAmount,");
            //strSql.Append("remainAlipayAmount=remainAlipayAmount+@remainAlipayAmount,");
            //strSql.Append("remainWechatPayAmount=remainWechatPayAmount+@remainWechatPayAmount,");
            //strSql.Append("remainCommissionAmount=remainCommissionAmount+@remainCommissionAmount ");
            strSql.Append(" where shopID=@shopID");
            SqlParameter[] parameters = {
					new SqlParameter("@remainMoney", SqlDbType.Float),
                    new SqlParameter("@remainRedEnvelopeAmount", SqlDbType.Float),
                    new SqlParameter("@remainFoodCouponAmount", SqlDbType.Float),
                    new SqlParameter("@remainAlipayAmount", SqlDbType.Float),
                    new SqlParameter("@remainWechatPayAmount", SqlDbType.Float),
					new SqlParameter("@remainCommissionAmount", SqlDbType.Float),
                    new SqlParameter("@shopID", SqlDbType.Int)};
            parameters[0].Value = model.remainMoney;
            parameters[1].Value = model.remainRedEnvelopeAmount;
            parameters[2].Value = model.remainFoodCouponAmount;
            parameters[3].Value = model.remainAlipayAmount;
            parameters[4].Value = model.remainWechatPayAmount;
            parameters[5].Value = model.remainCommissionAmount;
            parameters[6].Value = model.shopID;
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 商户余额日志查询
        /// </summary>
        /// <param name="logDate"></param>
        /// <param name="cityID"></param>
        /// <param name="shopID"></param>
        /// <returns></returns>
        public DataTable SelectShopAmountLog(DateTime logDate, int cityID, int shopID)
        {
            StringBuilder strSql = new StringBuilder();
            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@logDate", SqlDbType.DateTime) { Value = logDate });
            strSql.Append("select shopName,cityName,amount from ShopAmountLog a");
            strSql.Append(" inner join ShopInfo b on a.shopID=b.shopID");
            strSql.Append(" inner join City c on b.cityID=c.cityID");
            strSql.Append(" where a.logDate=@logDate");
            if (cityID > 0)
            {
                strSql.Append("  and b.cityID=@cityID");
                para.Add(new SqlParameter("@cityID", SqlDbType.Int) { Value = cityID });
            }
            if (shopID > 0)
            {
                strSql.Append("  and b.shopID=@shopID");
                para.Add(new SqlParameter("@shopID", SqlDbType.Int) { Value = shopID });
            }
            strSql.Append(" order by amount desc");
            SqlParameter[] pa = para.ToArray();
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), pa);
            return ds.Tables[0];
        }

        /// <summary>
        /// 添加商户余额日志
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int InsertShopAmountLog(ShopAmountLog model)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into shopAmountLog(");
                    strSql.Append("shopID,amount,logDate)");
                    strSql.Append(" values (");
                    strSql.Append("@shopID,@amount,@logDate)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
					    new SqlParameter("@shopID", SqlDbType.Int,4),
                        new SqlParameter("@amount", SqlDbType.Money),
                        new SqlParameter("@logDate",SqlDbType.Date),
                    };
                    parameters[0].Value = model.shopID;
                    parameters[1].Value = model.amount;
                    parameters[2].Value = model.logDate;
                    //
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

        /// 查询店铺信息含公司信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectShopAmount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [shopID],[shopName],[remainMoney]");
            strSql.Append(" from ShopInfo ");
            strSql.Append(" where  shopStatus > 0 order by shopId desc");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 判断店铺是否下线
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public bool IsOffline(int shopId)
        {
            string sql = "SELECT COUNT(0) FROM ShopInfo WHERE shopId=@shopId AND (ISNULL(isHandle,0)!=1 OR shopStatus!=1)";
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@shopID", SqlDbType.Int, 4) };
            return (int)SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, parameters) != 0;
        }

        /// <summary>
        /// 查询打款方式
        /// </summary>
        /// <param name="shopid"></param>
        /// <returns></returns>
        public int getWithdrawType(int shopid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select isnull(withdrawtype,0) withdrawtype from shopInfo");
            strSql.Append(" where shopid = @shopID");
            SqlParameter[] parameters = new SqlParameter[]{
					    new SqlParameter("@shopID", SqlDbType.Int,4)
                    };
            parameters[0].Value = shopid;
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(ds.Tables[0].Rows[0]["withdrawtype"].ToString());
            }
            return -1;
        }

        /// <summary>
        /// 修改打款方式
        /// </summary>
        /// <param name="shopid"></param>
        /// <param name="withdrawtype"></param>
        /// <returns></returns>
        public int updateWithdrawType(int shopid, int withdrawtype)
        {
            int result = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                CompanyAccountInfo model = new CompanyAccountInfo();
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select a.accountName,a.accountNum,a.bankName,a.payeeBankName,a.remark,isnull(a.identity_Id,0) identity_Id,b.companyId");
                strSql.Append(" from ShopInfo b");
                strSql.Append(" left join CompanyAccounts a on b.bankAccount=a.identity_Id");
                strSql.Append(" where b.shopID=@shopID");
                SqlParameter[] parameters1 = new SqlParameter[]{
					    new SqlParameter("@shopID", SqlDbType.Int,4)
                    };
                parameters1[0].Value = shopid;
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters1);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    model = new CompanyAccountInfo()
                    {
                        accountName = ds.Tables[0].Rows[0]["accountName"].ToString(),
                        accountNum = ds.Tables[0].Rows[0]["accountNum"].ToString(),
                        bankName = ds.Tables[0].Rows[0]["bankName"].ToString(),
                        payeeBankName = ds.Tables[0].Rows[0]["payeeBankName"].ToString(),
                        remark = ds.Tables[0].Rows[0]["remark"].ToString(),
                        companyId = Convert.ToInt32(ds.Tables[0].Rows[0]["companyId"].ToString()),
                        identity_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["identity_Id"].ToString())
                    };
                }


                strSql.Length = 0;
                strSql.AppendFormat("update shopInfo set withdrawtype=@withdrawtype");
                strSql.AppendFormat(" where shopid = @shopID", shopid);
                SqlParameter[] parameters = new SqlParameter[]{
					    new SqlParameter("@shopID", SqlDbType.Int,4),
                         new SqlParameter("@withdrawtype", SqlDbType.Int,4)
                    };
                parameters[0].Value = shopid;
                parameters[1].Value = withdrawtype;
                result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);

                strSql.Length = 0;
                strSql.Append("update CompanyAccounts set");
                strSql.Append(" isFirst=1");
                strSql.Append(" where identity_Id=@identity_Id");

                SqlParameter[] parameters2 = new SqlParameter[]{
					    new SqlParameter("@identity_Id", SqlDbType.Int,4),
                    };
                parameters2[0].Value = model.identity_Id;
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters2);

                if (result == 1)
                {
                    scope.Complete();
                }

                return result;
            }
        }

        /// <summary>
        /// 查询佣金比例
        /// </summary>
        /// <param name="shopid"></param>
        /// <returns></returns>
        public double getViewallocCommissionValue(int shopid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select isnull(viewallocCommissionValue,0) viewallocCommissionValue from ShopInfo");
            strSql.Append(" where shopID=@shopID");
            SqlParameter[] parameters = new SqlParameter[]{
					    new SqlParameter("@shopID", SqlDbType.Int,4)
                    };
            parameters[0].Value = shopid;
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return Math.Round(Convert.ToDouble(ds.Tables[0].Rows[0]["viewallocCommissionValue"].ToString()), 2);
            }
            return 0;
        }

        /// <summary>
        /// 修改佣金比例
        /// </summary>
        /// <param name="shopid"></param>
        /// <returns></returns>
        public int updateViewallocCommissionValue(int shopid, double viewallocCommissionValue)
        {
             int result = 0;
             using (TransactionScope scope = new TransactionScope())
             {
                 CompanyAccountInfo model = new CompanyAccountInfo();
                 StringBuilder strSql = new StringBuilder();
                 strSql.Append("select a.accountName,a.accountNum,a.bankName,a.payeeBankName,a.remark,isnull(a.identity_Id,0) identity_Id,b.companyId");
                 strSql.Append(" from ShopInfo b");
                 strSql.Append(" left join CompanyAccounts a on b.bankAccount=a.identity_Id");
                 strSql.Append(" where b.shopID=@shopID");
                 SqlParameter[] parameters1 = new SqlParameter[]{
					    new SqlParameter("@shopID", SqlDbType.Int,4)
                    };
                 parameters1[0].Value = shopid;
                 DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters1);
                 if (ds.Tables[0].Rows.Count > 0)
                 {
                     model = new CompanyAccountInfo()
                     {
                         accountName = ds.Tables[0].Rows[0]["accountName"].ToString(),
                         accountNum = ds.Tables[0].Rows[0]["accountNum"].ToString(),
                         bankName = ds.Tables[0].Rows[0]["bankName"].ToString(),
                         payeeBankName = ds.Tables[0].Rows[0]["payeeBankName"].ToString(),
                         remark = ds.Tables[0].Rows[0]["remark"].ToString(),
                         companyId = Convert.ToInt32(ds.Tables[0].Rows[0]["companyId"].ToString()),
                         identity_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["identity_Id"].ToString())
                     };
                 }

                 strSql.Length = 0;
                 strSql.Append("update ShopInfo set viewallocCommissionValue=@viewallocCommissionValue");
                 strSql.Append(" where shopID=@shopID");
                 SqlParameter[] parameters = new SqlParameter[]{
					    new SqlParameter("@shopID", SqlDbType.Int,4),
                        new SqlParameter("@viewallocCommissionValue", SqlDbType.Float)
                    };
                 parameters[0].Value = shopid;
                 parameters[1].Value = viewallocCommissionValue;
                 result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);

                 strSql.Length = 0;
                 strSql.Append("update CompanyAccounts set");
                 strSql.Append(" isFirst=1");
                 strSql.Append(" where identity_Id=@identity_Id");

                 SqlParameter[] parameters2 = new SqlParameter[]{
					    new SqlParameter("@identity_Id", SqlDbType.Int,4),
                    };
                 parameters2[0].Value = model.identity_Id;
                 SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters2);

                 if (result == 1)
                 {
                     scope.Complete();
                 }

                 return result;
             }
        }

        /// <summary>
        /// 查询折扣信息
        /// </summary>
        /// <param name="shopid"></param>
        /// <returns></returns>
        public double getShopVipInfo(int shopid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select discount from ShopVipInfo where ShopId=@shopID and platformVipId=1 and status=1");
            SqlParameter[] parameters = new SqlParameter[]{
					    new SqlParameter("@shopID", SqlDbType.Int,4)
                    };
            parameters[0].Value = shopid;
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return Math.Round(Convert.ToDouble(ds.Tables[0].Rows[0]["discount"].ToString()) * 100, 2);
            }
            return 100;
        }

        /// <summary>
        /// 添加折扣
        /// </summary>
        /// <param name="shopid"></param>
        /// <returns></returns>
        public int insertShopVipInfo(int shopid, double discount)
        {
             int result = 0;
             using (TransactionScope scope = new TransactionScope())
             {
                 CompanyAccountInfo model = new CompanyAccountInfo();
                 StringBuilder strSql = new StringBuilder();
                 strSql.Append("select a.accountName,a.accountNum,a.bankName,a.payeeBankName,a.remark,isnull(a.identity_Id,0) identity_Id,b.companyId");
                 strSql.Append(" from ShopInfo b");
                 strSql.Append(" left join CompanyAccounts a on b.bankAccount=a.identity_Id");
                 strSql.Append(" where b.shopID=@shopID");
                 SqlParameter[] parameters1 = new SqlParameter[]{
					    new SqlParameter("@shopID", SqlDbType.Int,4)
                    };
                 parameters1[0].Value = shopid;
                 DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters1);
                 if (ds.Tables[0].Rows.Count > 0)
                 {
                     model = new CompanyAccountInfo()
                     {
                         accountName = ds.Tables[0].Rows[0]["accountName"].ToString(),
                         accountNum = ds.Tables[0].Rows[0]["accountNum"].ToString(),
                         bankName = ds.Tables[0].Rows[0]["bankName"].ToString(),
                         payeeBankName = ds.Tables[0].Rows[0]["payeeBankName"].ToString(),
                         remark = ds.Tables[0].Rows[0]["remark"].ToString(),
                         companyId = Convert.ToInt32(ds.Tables[0].Rows[0]["companyId"].ToString()),
                         identity_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["identity_Id"].ToString())
                     };
                 }

                 strSql.Length = 0;
                 strSql.Append("insert into ShopVipInfo ");
                 strSql.Append("(platformVipId,shopId,discount,status)");
                 strSql.Append(" values (@platformVipId,@shopId,@discount,@status)");
                 SqlParameter[] parameters = new SqlParameter[]{
					    new SqlParameter("@platformVipId", SqlDbType.Int,4),
                        new SqlParameter("@shopId", SqlDbType.Int),
                        new SqlParameter("@discount", SqlDbType.Float),
                        new SqlParameter("@status", SqlDbType.Int)
                    };
                 parameters[0].Value = 1;
                 parameters[1].Value = shopid;
                 parameters[2].Value = discount;
                 parameters[3].Value = 1;
                 result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);

                 strSql.Length = 0;
                 strSql.Append("update CompanyAccounts set");
                 strSql.Append(" isFirst=1");
                 strSql.Append(" where identity_Id=@identity_Id");

                 SqlParameter[] parameters2 = new SqlParameter[]{
					    new SqlParameter("@identity_Id", SqlDbType.Int,4),
                    };
                 parameters2[0].Value = model.identity_Id;
                 SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters2);

                 if (result == 1)
                 {
                     scope.Complete();
                 }

                 return result;
             }
        }

        /// <summary>
        /// 修改折扣
        /// </summary>
        /// <param name="shopid"></param>
        /// <returns></returns>
        public int updateShopVipInfo(int shopid, double discount)
        {
             int result = 0;
             using (TransactionScope scope = new TransactionScope())
             {
                 CompanyAccountInfo model = new CompanyAccountInfo();
                 StringBuilder strSql = new StringBuilder();
                 strSql.Append("select a.accountName,a.accountNum,a.bankName,a.payeeBankName,a.remark,isnull(a.identity_Id,0) identity_Id,b.companyId");
                 strSql.Append(" from ShopInfo b");
                 strSql.Append(" left join CompanyAccounts a on b.bankAccount=a.identity_Id");
                 strSql.Append(" where b.shopID=@shopID");
                 SqlParameter[] parameters1 = new SqlParameter[]{
					    new SqlParameter("@shopID", SqlDbType.Int,4)
                    };
                 parameters1[0].Value = shopid;
                 DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters1);
                 if (ds.Tables[0].Rows.Count > 0)
                 {
                     model = new CompanyAccountInfo()
                     {
                         accountName = ds.Tables[0].Rows[0]["accountName"].ToString(),
                         accountNum = ds.Tables[0].Rows[0]["accountNum"].ToString(),
                         bankName = ds.Tables[0].Rows[0]["bankName"].ToString(),
                         payeeBankName = ds.Tables[0].Rows[0]["payeeBankName"].ToString(),
                         remark = ds.Tables[0].Rows[0]["remark"].ToString(),
                         companyId = Convert.ToInt32(ds.Tables[0].Rows[0]["companyId"].ToString()),
                         identity_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["identity_Id"].ToString())
                     };
                 }

                 strSql.Length = 0;
                 strSql.Append("update ShopVipInfo set discount=@discount");
                 strSql.Append(" where shopID=@shopID and platformVipId=1 and status=1");
                 SqlParameter[] parameters = new SqlParameter[]{
					    new SqlParameter("@shopID", SqlDbType.Int,4),
                        new SqlParameter("@discount", SqlDbType.Float)
                    };
                 parameters[0].Value = shopid;
                 parameters[1].Value = discount;
                 result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);

                 strSql.Length = 0;
                 strSql.Append("update CompanyAccounts set");
                 strSql.Append(" isFirst=1");
                 strSql.Append(" where identity_Id=@identity_Id");

                 SqlParameter[] parameters2 = new SqlParameter[]{
					    new SqlParameter("@identity_Id", SqlDbType.Int,4),
                    };
                 parameters2[0].Value = model.identity_Id;
                 SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters2);

                 if (result >= 1)
                 {
                     scope.Complete();
                 }

                 return result;
             }
        }

        public int updateShopAccount(int shopid, int bankAccount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ShopInfo set bankAccount=@bankAccount");
            strSql.Append(" where shopID=@shopID");
            SqlParameter[] parameters = new SqlParameter[]{
					    new SqlParameter("@shopID", SqlDbType.Int,4),
                        new SqlParameter("@bankAccount", SqlDbType.Int)
                    };
            parameters[0].Value = shopid;
            parameters[1].Value = bankAccount;
            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            return result;
        }

        /// <summary>
        /// 查询店铺信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectShopNew(int isHandle, string searchKeyWords)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.shopID,a.shopName,b.companyID,b.companyName");
            strSql.Append(" from ShopInfo a");
            strSql.Append(" inner join CompanyInfo b on a.companyID=b.companyID");
            strSql.Append(" where  shopStatus > '0' ");

            if (isHandle > 0)
            {
                strSql.Append(" and isHandle=1 ");
            }
            if (searchKeyWords.Trim().Length > 0)
            {
                strSql.AppendFormat(" and shopName like '%{0}%'", searchKeyWords.Trim());
            }
            strSql.Append(" order by shopId desc");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取门店默认LOG地址
        /// </summary>
        /// <returns></returns>
        public string getDefaultLogPath(int shopID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select shopName,shopImagePath,shopLogo from ShopInfo where shopID=@shopID");
            SqlParameter[] parameter=new SqlParameter[]
            {
                new SqlParameter("shopID",SqlDbType.Int){Value=shopID}
            };
           
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(),parameter);
            string Path = string.Empty;
            if (ds.Tables[0].Rows.Count > 0)
            {
                Path = ds.Tables[0].Rows[0]["shopImagePath"].ToString() + ds.Tables[0].Rows[0]["shopLogo"].ToString();
            }
            return Path;
        }
    }
}
