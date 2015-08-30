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
    public class CompanyManager
    {
        /// <summary>
        /// 新增公司信息
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public int InsertCompany(CompanyInfo company)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into CompanyInfo(");
                    strSql.Append("companyName,companyAddress,companyTelePhone,companyLogo,contactPerson,contactPhone,companyStatus,companyImagePath,companyDescription,ownedCompany)");
                    strSql.Append(" values (");
                    strSql.Append("@companyName,@companyAddress,@companyTelePhone,@companyLogo,@contactPerson,@contactPhone,@companyStatus,@companyImagePath,@companyDescription,@ownedCompany)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
					    new SqlParameter("@companyName", SqlDbType.VarChar,500),
                        new SqlParameter("@companyAddress", SqlDbType.VarChar,500),
                        new SqlParameter("@companyTelePhone", SqlDbType.VarChar,500),
                        new SqlParameter("@companyLogo",SqlDbType.NVarChar,500),
                        new SqlParameter("@contactPerson", SqlDbType.VarChar,500),
                        new SqlParameter("@contactPhone",SqlDbType.NVarChar,500),
                        new SqlParameter("@companyStatus",SqlDbType.Int,4),
                        new SqlParameter("@companyImagePath",SqlDbType.NVarChar,500),
                        new SqlParameter("@companyDescription",SqlDbType.NVarChar,500),
                         new SqlParameter("@ownedCompany",SqlDbType.NVarChar,500)
                    };
                    parameters[0].Value = company.companyName;
                    parameters[1].Value = company.companyAddress;
                    parameters[2].Value = company.companyTelePhone;
                    parameters[3].Value = company.companyLogo;
                    parameters[4].Value = company.contactPerson;
                    parameters[5].Value = company.contactPhone;
                    parameters[6].Value = 1;
                    parameters[7].Value = company.companyImagePath;
                    parameters[8].Value = company.companyDescription;
                    parameters[9].Value = company.ownedCompany;
                    //1、插入公司信息表信息
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
        /// 收银宝添加详情公司信息
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public int Syb_InsertCompany(CompanyInfo company)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into CompanyInfo(");
                    strSql.Append("companyName,companyAddress,companyTelePhone,companyLogo,contactPerson,contactPhone,companyStatus,companyImagePath,companyDescription,ownedCompany,sinaWeiboName,acpp,qqWeiboName,wechatPublicName)");
                    strSql.Append(" values (");
                    strSql.Append("@companyName,@companyAddress,@companyTelePhone,@companyLogo,@contactPerson,@contactPhone,@companyStatus,@companyImagePath,@companyDescription,@ownedCompany,@sinaWeiboName,@acpp,@qqWeiboName,@wechatPublicName)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
					    new SqlParameter("@companyName", SqlDbType.VarChar,500),
                        new SqlParameter("@companyAddress", SqlDbType.VarChar,500),
                        new SqlParameter("@companyTelePhone", SqlDbType.VarChar,500),
                        new SqlParameter("@companyLogo",SqlDbType.NVarChar,500),
                        new SqlParameter("@contactPerson", SqlDbType.VarChar,500),
                        new SqlParameter("@contactPhone",SqlDbType.NVarChar,500),
                        new SqlParameter("@companyStatus",SqlDbType.Int,4),
                        new SqlParameter("@companyImagePath",SqlDbType.NVarChar,500),
                        new SqlParameter("@companyDescription",SqlDbType.NVarChar,500),
                        new SqlParameter("@ownedCompany",SqlDbType.NVarChar,500),

                        new SqlParameter("@sinaWeiboName",SqlDbType.NVarChar,100),
                        new SqlParameter("@acpp",SqlDbType.Float),
                        new SqlParameter("@qqWeiboName",SqlDbType.NVarChar,100),
                        new SqlParameter("@wechatPublicName",SqlDbType.NVarChar,100)
                    };
                    parameters[0].Value = company.companyName;
                    parameters[1].Value = company.companyAddress;
                    parameters[2].Value = company.companyTelePhone;
                    parameters[3].Value = company.companyLogo;
                    parameters[4].Value = company.contactPerson;
                    parameters[5].Value = company.contactPhone;
                    parameters[6].Value = 1;
                    parameters[7].Value = company.companyImagePath;
                    parameters[8].Value = company.companyDescription;
                    parameters[9].Value = company.ownedCompany;

                    parameters[10].Value = company.sinaWeiboName;
                    parameters[11].Value = company.acpp;
                    parameters[12].Value = company.qqWeiboName;
                    parameters[13].Value = company.wechatPublicName;
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
        /// 删除公司信息
        /// </summary>
        /// <param name="companyID"></param>
        public bool DeleteCompany(int companyID)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update CompanyInfo set companyStatus = '-1' where companyID=@companyID;");
                    strSql.AppendFormat("update ShopInfo set shopStatus = '{0}' where companyID=@companyID;", (int)VAShopStatus.SHOP_DELETED);
                    SqlParameter[] parameters = {					
					new SqlParameter("@companyID", SqlDbType.Int,4)};
                    parameters[0].Value = companyID;
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (Exception)
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
        /// 修改公司信息
        /// </summary>
        /// <param name="company"></param>
        public bool UpdateCompany(CompanyInfo company)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update CompanyInfo set ");
                    strSql.Append("companyName=@companyName,");
                    strSql.Append("companyAddress=@companyAddress,");
                    strSql.Append("companyTelePhone=@companyTelePhone,");
                    strSql.Append("companyLogo=@companyLogo,");
                    strSql.Append("contactPerson=@contactPerson,");
                    strSql.Append("contactPhone=@contactPhone,");
                    strSql.Append("companyDescription=@companyDescription,");
                    strSql.Append("sinaWeiboName=@sinaWeiboName,");
                    strSql.Append("qqWeiboName=@qqWeiboName,");
                    strSql.Append("acpp=@acpp,");

                    strSql.Append("wechatPublicName=@wechatPublicName,");

                    strSql.Append("ownedCompany=@ownedCompany");

                    strSql.Append(" where companyID=@companyID ");
                    SqlParameter[] parameters = {
                        new SqlParameter("@companyName", SqlDbType.VarChar,500),
                        new SqlParameter("@companyAddress", SqlDbType.VarChar,500),
                        new SqlParameter("@companyTelePhone", SqlDbType.VarChar,500),
                        new SqlParameter("@companyLogo",SqlDbType.NVarChar,500),
                        new SqlParameter("@contactPerson", SqlDbType.VarChar,500),
                        new SqlParameter("@contactPhone",SqlDbType.NVarChar,500),
                        new SqlParameter("@companyDescription",SqlDbType.NVarChar,500),
                        new SqlParameter("@sinaWeiboName",SqlDbType.NVarChar,100),
                        new SqlParameter("@qqWeiboName",SqlDbType.NVarChar,100),
                        new SqlParameter("@acpp",SqlDbType.Float),
                        new SqlParameter("@companyID",SqlDbType.Int,4),
                       new SqlParameter("@wechatPublicName",SqlDbType.NVarChar,100),
                                                 new SqlParameter("@ownedCompany",SqlDbType.NVarChar,500)
                                                };
                    parameters[0].Value = company.companyName;
                    parameters[1].Value = company.companyAddress;
                    parameters[2].Value = company.companyTelePhone;
                    parameters[3].Value = company.companyLogo;
                    parameters[4].Value = company.contactPerson;
                    parameters[5].Value = company.contactPhone;
                    parameters[6].Value = company.companyDescription;
                    parameters[7].Value = company.sinaWeiboName;
                    parameters[8].Value = company.qqWeiboName;
                    parameters[9].Value = company.acpp;
                    parameters[10].Value = company.companyID;
                    parameters[11].Value = company.wechatPublicName;

                    parameters[12].Value = company.ownedCompany;

                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
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
        /// 修改自己公司信息
        /// </summary>
        /// <param name="company"></param>
        public bool UpdateCurrentCompany(CompanyInfo company)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update CompanyInfo set ");
                    strSql.Append("companyName=@companyName,");
                    strSql.Append("companyAddress=@companyAddress,");
                    strSql.Append("companyTelePhone=@companyTelePhone,");
                    strSql.Append("companyLogo=@companyLogo,");
                    strSql.Append("contactPerson=@contactPerson,");
                    strSql.Append("contactPhone=@contactPhone,");
                    strSql.Append("companyDescription=@companyDescription,");
                    strSql.Append("qqWeiboName=@qqWeiboName,");
                    strSql.Append("sinaWeiboName=@sinaWeiboName,");

                    strSql.Append("wechatPublicName=@wechatPublicName,");
                    strSql.Append("ownedCompany=@ownedCompany");
                    strSql.Append(" where companyID=@companyID ");
                    SqlParameter[] parameters = {
                        new SqlParameter("@companyName", SqlDbType.VarChar,500),
                        new SqlParameter("@companyAddress", SqlDbType.VarChar,500),
                        new SqlParameter("@companyTelePhone", SqlDbType.VarChar,500),
                        new SqlParameter("@companyLogo",SqlDbType.NVarChar,500),
                        new SqlParameter("@contactPerson", SqlDbType.VarChar,500),
                        new SqlParameter("@contactPhone",SqlDbType.NVarChar,500),
                        new SqlParameter("@companyDescription",SqlDbType.NVarChar,500),
                        new SqlParameter("@qqWeiboName",SqlDbType.NVarChar,100),
                        new SqlParameter("@sinaWeiboName",SqlDbType.NVarChar,100),
                        new SqlParameter("@companyID",SqlDbType.Int,4),

                        new SqlParameter("@wechatPublicName",SqlDbType.NVarChar,100),
                                                   new SqlParameter("@ownedCompany",SqlDbType.NVarChar,500)};

                    parameters[0].Value = company.companyName;
                    parameters[1].Value = company.companyAddress;
                    parameters[2].Value = company.companyTelePhone;
                    parameters[3].Value = company.companyLogo;
                    parameters[4].Value = company.contactPerson;
                    parameters[5].Value = company.contactPhone;
                    parameters[6].Value = company.companyDescription;
                    parameters[7].Value = company.qqWeiboName;
                    parameters[8].Value = company.sinaWeiboName;
                    parameters[9].Value = company.companyID;
                    parameters[10].Value = company.wechatPublicName;
                    parameters[11].Value = company.ownedCompany;

                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
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
        /// 获取公司默认的菜谱
        /// </summary>
        /// <param name="company"></param>
        public bool SetCompanyDefaultMenu(int company, int menuId)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update CompanyInfo set ");
                    strSql.Append("defaultMenuId=@defaultMenuId ");
                    strSql.Append("where companyID=@companyID ");
                    SqlParameter[] parameters = {
                        new SqlParameter("@defaultMenuId", SqlDbType.Int,4),
                        new SqlParameter("@companyID", SqlDbType.Int,4),
                      };
                    parameters[0].Value = menuId;
                    parameters[1].Value = company;
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
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
        /// 更新公司支付给友络的佣金和预支付的免费退款时间
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public bool UpdateCompanyCommissionAndRefundHour(CompanyInfo company)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update CompanyInfo set ");
                    strSql.Append("freeRefundHour=@freeRefundHour,");
                    strSql.Append("viewallocCommissionType=@viewallocCommissionType,");
                    strSql.Append("viewallocCommissionValue=@viewallocCommissionValue");
                    strSql.Append(" where companyID=@companyID ");
                    SqlParameter[] parameters = {
                        new SqlParameter("@freeRefundHour", SqlDbType.Float),
                        new SqlParameter("@viewallocCommissionType", SqlDbType.SmallInt,2),
                        new SqlParameter("@viewallocCommissionValue", SqlDbType.Float),
                        new SqlParameter("@companyID",SqlDbType.Int,4)};
                    parameters[0].Value = company.freeRefundHour;
                    parameters[1].Value = company.viewallocCommissionType;
                    parameters[2].Value = company.viewallocCommissionValue;
                    parameters[3].Value = company.companyID;
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
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
        /// 查询所有公司信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectCompany()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [companyID],[companyName],[companyAddress],[companyTelePhone],[companyLogo],[contactPerson],[contactPhone]");
            strSql.Append(",[companyStatus],[companyImagePath],[companyDescription],[defaultMenuId],[freeRefundHour],[viewallocCommissionType]");
            strSql.Append(",[viewallocCommissionValue],[prepayOrderCount],[prePayCashBackCount],[prePayVIPCount],[prePaySendGiftCount]");
            strSql.Append(",[recommendRating],[preorderCount],[sinaWeiboName],[acpp],[qqWeiboName]");
            strSql.Append(" from CompanyInfo where companyStatus > '0' order by companyID desc");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 查询所有公司信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectCompanyNameAndId()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [companyID],[companyName]");
            strSql.Append(" from CompanyInfo where companyStatus > '0' order by companyID desc");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 查询公司信息（不包含没有门店的公司）
        /// </summary>
        /// <returns></returns>
        public DataTable SelectCompanyNameAndIdRemovenNotHaveShop()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("  select A.[companyID],[companyName]");
            strSql.Append("  from CompanyInfo A");
            strSql.Append("  inner join ShopInfo B on A.companyID=B.companyID where companyStatus > '0' and B.shopStatus>'0'");
            strSql.Append("  group by A.companyID,[companyName] order by A.companyID desc");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据角色查询该角色可以查询的城市区域的公司
        /// </summary>
        /// <returns></returns>
        public DataTable SelectCompanyNameAndIdByAuthority(int employeeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct CompanyInfo.[companyID],CompanyInfo.[companyName]");
            strSql.Append(" from  EmployeeRole ");
            strSql.Append(" inner join RoleAuthority on EmployeeRole.RoleID=RoleAuthority.RoleID");
            strSql.Append(" inner join SpecialAuthority on SpecialAuthority.RoleId=RoleAuthority.RoleID");
            strSql.Append(" inner join SpecialAuthorityConnCity on SpecialAuthorityConnCity.connSpecialAuthorityId=SpecialAuthority.id");
            strSql.Append(" inner join ShopInfo on ShopInfo.cityID=SpecialAuthorityConnCity.cityId");
            strSql.Append(" inner join CompanyInfo on ShopInfo.companyID=CompanyInfo.companyID");
            strSql.Append(" where CompanyInfo.companyStatus > '0' and ShopInfo.shopStatus='1' and SpecialAuthorityConnCity.status='1'");
            strSql.AppendFormat(" and EmployeeRole.employeeID='{0}'", employeeID);
            strSql.Append(" order by CompanyInfo.companyID desc");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据公司编号查询对应公司信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectCompany(int companyID)
        {
            const string strSql = @"select [companyID],[companyName],[companyAddress],[companyTelePhone],[companyLogo],[contactPerson],[contactPhone]
,[companyStatus],[companyImagePath],[companyDescription],[defaultMenuId],[freeRefundHour],[viewallocCommissionType]
,[viewallocCommissionValue],[prepayOrderCount],[prePayCashBackCount],[prePayVIPCount],[prePaySendGiftCount]
,[recommendRating],[preorderCount],[sinaWeiboName],[acpp],[qqWeiboName],[wechatPublicName],[ownedCompany]
from CompanyInfo where companyID =@companyID order by companyID desc";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@companyID", SqlDbType.Int) { Value = companyID }
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para);
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据门店编号查询公司编号（add by wangc 20140321）
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public int GetCompanyId(int shopId)
        {
            const string strSql = @"select CompanyInfo.companyID from CompanyInfo inner join ShopInfo on CompanyInfo.companyID=ShopInfo.companyID
                                            where shopID=@shopId and CompanyInfo.companyStatus=1";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@shopId", SqlDbType.Int) { Value = shopId }
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql, para);
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
        /// 根据城市编号查询对应公司信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectCompanyByCityId(int cityId, int companyId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT DISTINCT(A.companyID) ,companyName,companyLogo,companyImagePath ,companyDescription,recommendRating,");
            strSql.Append(" defaultMenuId, A.prePayCashBackCount,A.prePayVIPCount,A.prePaySendGiftCount,A.acpp,A.preorderCount,A.prepayOrderCount from dbo.CompanyInfo as A");
            strSql.AppendFormat(" inner join ShopInfo as B on A.companyID = B.companyID where B.cityID = {0} and A.companyStatus > '0' and B.isHandle = '" + (int)VAShopHandleStatus.SHOP_Pass + "'", cityId);
            strSql.Append(" and B.shopStatus > 0");//20130808 xiaoyu
            if (companyId > 0)
            {
                strSql.AppendFormat(" and A.companyID ={0}", companyId);
            }
            strSql.Append(" order by recommendRating desc,companyName asc");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据城市编号查询对应公司Banner信息
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="adClassify"></param>
        /// <returns></returns>
        public DataTable SelectCompanyBannerByCityId(int cityId, VAAdvertisementClassify adClassify)
        {
            string strSql = @"SELECT A.advertisementId, B.advertisementAreaId,C.name,C.advertisementDescription,C.imageURL,C.webAdvertisementUrl
                                    from AdvertisementConnAdColumn as A 
                                    inner join AdvertisementColumn as B on B.id = A.advertisementColumnId 
                                    inner join AdvertisementInfo as C on C.id = A.advertisementId 
                                    where A.status > 0 and B.status > 0 and C.status > 0 
                                    and A.cityId =@cityId 
                                    and A.displayStartTime <= @displayStartTime
                                    and A.displayEndTime > @displayEndTime";
            if (adClassify == VAAdvertisementClassify.INDEX_AD)
            {
                strSql += " and C.advertisementClassify = " + (int)VAAdvertisementClassify.INDEX_AD + "";
                strSql += " and B.advertisementAreaId !='" + (int)VAAdvertisementArea.COUPON_BANNER + "'";
            }
            else
            {
                strSql += " and C.advertisementClassify = " + (int)VAAdvertisementClassify.FOODPLAZA_AD + "";
            }
            strSql += " order by B.sequence asc";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@cityId ", SqlDbType.Int ){ Value = cityId },
            new SqlParameter("@displayStartTime ", SqlDbType.DateTime ){ Value = DateTime.Now },
            new SqlParameter("@displayEndTime ", SqlDbType.DateTime ){ Value = DateTime.Now }
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para);
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据店铺编号查询对应的公司编号
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public int SelectCompanyIdByShopId(int shopId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT A.companyID from ShopInfo as A");
            // strSql.AppendFormat(" where A.shopID = {0} and A.shopStatus > '0' and A.isHandle = '" + (int)VAShopHandleStatus.SHOP_Pass + "'", shopId);
            strSql.AppendFormat(" where A.shopID = {0}", shopId);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            if (ds.Tables[0].Rows.Count == 1)
            {
                return Convert.ToInt32(ds.Tables[0].Rows[0]["companyID"]);
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 更新公司各类优惠计数
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="prepayCashBack"></param>
        /// <param name="prepayVIP"></param>
        /// <param name="prePayGift"></param>
        /// <returns></returns>
        public bool UpdateCompanyPrepayPrevilegeCount(int companyId, int prepayCashBack, int prepayVIP, int prePayGift)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update CompanyInfo set ");
                    strSql.Append("prePayCashBackCount=isnull(prePayCashBackCount, 0) + @prePayCashBackCount,");
                    strSql.Append("prePayVIPCount=isnull(prePayVIPCount, 0) + @prePayVIPCount,");
                    strSql.Append("prePaySendGiftCount=isnull(prePaySendGiftCount, 0) + @prePaySendGiftCount");
                    strSql.Append(" where companyID=@companyID ");
                    SqlParameter[] parameters = {
                        new SqlParameter("@prePayCashBackCount",SqlDbType.SmallInt,2),
                        new SqlParameter("@prePayVIPCount",SqlDbType.SmallInt,2),
                        new SqlParameter("@prePaySendGiftCount",SqlDbType.SmallInt,2),
                        new SqlParameter("@companyID",SqlDbType.Int,2)};
                    parameters[0].Value = prepayCashBack;
                    parameters[1].Value = prepayVIP;
                    parameters[2].Value = prePayGift;
                    parameters[3].Value = companyId;

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
        /// 根据广告ID查询店铺ID add by jlf on 20130816
        /// </summary>
        /// <param name="advertisementid"></param>
        /// <returns></returns>
        public List<int> GetShopIdbyCompanyId(long advertisementId)
        {
            List<int> shopidlist = new List<int>();
            StringBuilder strSql = new StringBuilder();

            strSql.AppendFormat("select shopId from AdvertisementConnShop where advertisementId ={0} ", advertisementId);

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                shopidlist.Add(Convert.ToInt32(ds.Tables[0].Rows[i]["shopId"].ToString()));
            }
            return shopidlist;
        }

        public DataTable GetMedal(int CompanyId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MedalConnShopCompany.medalName,MedalConnShopCompany.medalDescription,tmp.medalURL,tmp.smallmedalURL from MedalConnShopCompany,(");
            strSql.Append("select A.medalId,A.medalURL as medalURL,B.medalURL as smallmedalURL from MedalImageInfo as A,");
            strSql.AppendFormat("MedalImageInfo as B where A.medalId =B.medalId and A.medalScale ='{0}' and B.medalScale ='{1}')tmp where ", (int)VAMedalImageType.MEDAL_IMAGE_BIG, (int)VAMedalImageType.MEDAL_IMAGE_SMALL);
            strSql.AppendFormat("MedalConnShopCompany.id =tmp.medalId and MedalConnShopCompany.medalType ='{0}' and MedalConnShopCompany.companyOrShopId ='{1}'", (int)VAMedalType.MEDAL_COMPANY, CompanyId);

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];

        }
        #region 公司官网模块
        /// <summary>
        /// 公司官网查询最近上线公司的信息
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="logoImageCount"></param>
        /// <returns></returns>
        public DataTable SelectCompanyLogoAndName(int cityId, int logoImageCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select distinct top {0} CompanyInfo.companyID,[companyName],companyImagePath+companyLogo", logoImageCount);
            strSql.Append(" as companyLogoFullPath ,ShopInfo.shopRegisterTime from CompanyInfo");
            strSql.Append(" left join ShopInfo on ShopInfo.companyID=CompanyInfo.companyID");
            strSql.Append(" where companyLogo is not null and companyLogo!=''");
            strSql.Append(" and ShopInfo.isHandle=1 and ShopInfo.shopStatus=1 and CompanyInfo.companyStatus=1");
            strSql.AppendFormat(" and ShopInfo.cityID={0} ", cityId);
            strSql.Append(" group by  CompanyInfo.companyID,[companyName],companyImagePath+companyLogo ,ShopInfo.shopRegisterTime");
            strSql.Append(" order by ShopInfo.shopRegisterTime desc");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        #endregion

        /// <summary>
        /// 根据城市查询所有公司列表
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="isHandle"></param>
        /// <returns></returns>
        public List<CompanyViewModel> SelectCompanyByCity(int cityId, bool isHandle = false)
        {
            string strSql = @"select distinct C.[companyID],[companyName]
from CompanyInfo C 
inner join ShopInfo S on C.companyID=S.companyID 
where companyStatus > '0' and S.shopStatus=1 ";
            if (cityId > 0)
            {
                strSql += " and S.cityID=" + cityId + "";
            }
            if (isHandle)
            {
                strSql += " and S.isHandle=1";//过滤查询上线公司
            }
            strSql += " order by companyID desc";
            var list = new List<CompanyViewModel>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql))
            {
                while (dr.Read())
                {
                    list.Add(new CompanyViewModel()
                    {
                        companyID = Convert.ToInt32(SqlHelper.ConvertDbNullValue(dr["companyID"])),
                        companyName = Convert.ToString(SqlHelper.ConvertDbNullValue(dr["companyName"]))
                    });
                }
            }
            return list;
        }
        /// <summary>
        /// 根据城市查询员工权限门店列表
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public List<CompanyViewModel> SelectEmlpoyeeCompanyByCity(int cityId, int employeeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select distinct CompanyInfo.companyID,CompanyInfo.companyName
 FROM EmployeeConnShop INNER JOIN
 EmployeeInfo ON EmployeeConnShop.employeeID = EmployeeInfo.EmPloyeeID INNER JOIN
 CompanyInfo ON EmployeeConnShop.companyID = CompanyInfo.companyID
 inner join ShopInfo on ShopInfo.companyID=CompanyInfo.companyID
 where CompanyInfo.companyStatus =1 and EmployeeInfo.EmployeeStatus =1
 and EmployeeConnShop.status>0 and ShopInfo.shopStatus=1");
            if (cityId > 0)
            {
                strSql.AppendFormat("  and ShopInfo.cityID={0}", cityId);
            }
            strSql.AppendFormat(" and EmployeeInfo.EmployeeID = {0}  order by CompanyInfo.companyID desc", employeeId);
            var list = new List<CompanyViewModel>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString()))
            {
                while (dr.Read())
                {
                    list.Add(new CompanyViewModel()
                    {
                        companyID = Convert.ToInt32(SqlHelper.ConvertDbNullValue(dr["companyID"])),
                        companyName = Convert.ToString(SqlHelper.ConvertDbNullValue(dr["companyName"]))
                    });
                }
            }
            return list;
        }
    }
}
