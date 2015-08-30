using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using System.Data.SqlClient;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    /// <summary>
    /// 报表统计操作
    /// </summary>
    public class StatisticalStatementManager
    {
        //订单统计
        /// <summary>
        /// 获得统计订单量的基本信息（订单数）
        /// 订单
        /// </summary>
        /// <returns></returns>
        public DataTable GetOrderStatistics(DateTime strTime, DateTime endTime, int company, int shopId, int cityId, string strHour, string endHour, int orderStatus, bool flag)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT convert(varchar(10),preOrderTime,120) as orderTime,");
            strSql.Append("ISNULL( COUNT(preOrder19dianId),0) as orderNumber,");

            //strSql.Append("ISNULL( Convert(decimal(10,2),sum(preOrderServerSum)),0) as orderAmount,");
            //preOrderServerSum 字段是在订单支付的时候更新数据库，所有如果没有支付，该字段为null
            strSql.Append("ISNULL( Convert(decimal(20,2),sum(preOrderSum)),0) as orderAmount,");
            // strSql.Append("ISNULL( Convert(decimal(10,2),sum(preOrderServerSum)/COUNT(preOrder19dianId)),0) as perCustomerTransaction,");
            strSql.Append("ISNULL( Convert(decimal(20,2),sum(preOrderSum)/COUNT(preOrder19dianId)),0) as perCustomerTransaction,");

            strSql.Append(" '0' as orderNumberIncrement,'0'as orderAmountIncrement,'0' as perCustomerTransactionDiurnalVariation,");
            strSql.Append(" ISNULL( COUNT(case when PreOrder19dian.isPaid='1' then preOrder19dianId end ),0) as payOrderCount,ISNULL( sum(case when PreOrder19dian.isPaid='1' then prePaidSum end ),0) as payOrderAmount,'0' as payOrderAmountIncrement");
            strSql.Append("   ,count(case when ISNULL(PreOrder19dian.refundMoneySum,0)>0 then 1 end) refundOrderCount,ROUND( SUM(ISNULL(PreOrder19dian.refundMoneySum,0)),2) refundOrderAmount");
            strSql.Append(" FROM PreOrder19dian inner join shopInfo on PreOrder19dian.shopId=shopInfo.shopId where");
            if (company != 0)//表示默认选中的是所有门店
            {
                strSql.AppendFormat(" PreOrder19dian.companyId='{0}' and", company);
                if (shopId != 0)
                {
                    strSql.AppendFormat("  PreOrder19dian.shopId='{0}' and", shopId);
                }
            }
            if (cityId > 0)
            {
                strSql.AppendFormat("  shopInfo.cityId='{0}' and ", cityId);
            }
            // strSql.Append("  preOrderTime between @strTime and @endTime ");
            if (flag)
            {
                strSql.AppendFormat("  (preOrderTime between @strTime and @endTime  and CONVERT(varchar(100),preOrderTime, 108) between '{0}' and '23:59:59')", strHour);
                strSql.AppendFormat(" or ( preOrderTime between @strTime and @endTime and  CONVERT(varchar(100),preOrderTime, 108) between '00:00:00' and '{0}')", endHour);
                strHour = " " + strHour;
                endHour = " " + endHour;
                strSql.AppendFormat(" and ( preOrderTime not between '" + strTime + "' and convert(varchar(10),preOrderTime,120)+'" + strHour + "'");
                strSql.AppendFormat(" and  preOrderTime not between '" + endTime + "' and convert(varchar(10),preOrderTime,120)+'" + endHour + "')");
                //strSql.AppendFormat(" and ( preOrderTime not between convert(varchar(12),'" + strTime + "',120)+' 00:00:00' and convert(varchar(10),preOrderTime,120)+'" + strHour + "'");
                // strSql.AppendFormat(" and  preOrderTime not between convert(varchar(12),'" + endTime + "',120)+' 00:00:00' and convert(varchar(10),preOrderTime,120)+'" + endHour + "')");
            }
            else
            {
                strSql.AppendFormat("  preOrderTime between @strTime and @endTime  and CONVERT(varchar(100),preOrderTime, 108) between '{0}' and '{1}'", strHour, endHour);
            }
            // strSql.AppendFormat(" and CONVERT(varchar(100),preOrderTime, 108) between '{0}' and '{1}'", strHour, endHour);
            if (orderStatus == 2)//orderStatus==2,表示已验证订单(审核，入座)
            {
                //筛选出来的是门店验证(审核，入座)过的订单
                strSql.AppendFormat(" and isShopConfirmed='{0}'", (int)VAPreOrderShopConfirmed.SHOPCONFIRMED);//衡量是否为订单的标准
            }
            //else,不加任何筛选订单条件
            strSql.Append(" group by convert(varchar(10),preOrderTime,120)");
            strSql.Append(" order by convert(varchar(10),preOrderTime,120)");
            SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@strTime", strTime),
                    new SqlParameter("@endTime", endTime)};//
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            return ds.Tables[0];
        }
        //支付订单统计
        /// <summary>
        /// 获得统计支付订单量的基本信息（支付订单数）
        /// 支付订单
        /// </summary>
        /// <returns></returns>
        public DataTable GetPayOrderStatistics(DateTime strTime, DateTime endTime, int company, int shopId, int cityId, string strHour, string endHour, bool flag)
        {
            StringBuilder strSql = new StringBuilder();
            //strSql.Append("SELECT ISNULL( COUNT(case when isShopVerified='" + (int)VAPreorderIsShopVerified.SHOPVERIFIED + "' then '" + (int)VAPreorderIsShopVerified.SHOPVERIFIED + "' end),0) as 'orderNumber',");
            strSql.Append("SELECT ISNULL( COUNT(preOrder19dianId),0) as 'orderNumber',");
            strSql.Append(" ISNULL( COUNT(case when isPaid='" + (int)VAPreorderIsPaid.PAID + "' then 1 end),0) as 'payOrderNumber',");//过滤支付过的订单
            strSql.Append(" ISNULL( convert(varchar(10),preOrderTime,120),0 )as 'payOrderTime' ,");
            strSql.Append(" ISNULL( Convert(decimal(20,2),SUM(case when isPaid ='1' then prePaidSum else 0 end)),0) as 'payOrderAmount',");
            strSql.Append(" (case when  COUNT(case when isPaid='" + (int)VAPreorderIsPaid.PAID + "' then 1 end)=0 then 0 else  ISNULL( Convert(decimal(20,2),SUM(case when isPaid ='1' then prePaidSum else 0 end)/ COUNT(case when isPaid='" + (int)VAPreorderIsPaid.PAID + "' then 1 end)) ,0) end) as 'payPerCustomerTransaction', ");
            strSql.Append(" '0' as 'payCompleteProportion',");
            strSql.Append("'0' as 'payOrderNumberFloat','0' as 'payOrderAmountIncrement','0' as 'payPerCustomerTransactionDiurnalVariation'");
            strSql.Append(" ,count(case when ISNULL(PreOrder19dian.refundMoneySum,0)>0 then 1 end) refundOrderCount,ROUND( SUM(ISNULL(PreOrder19dian.refundMoneySum,0)),2) refundOrderAmount");
            strSql.Append(" FROM PreOrder19dian inner join shopInfo on PreOrder19dian.shopId=shopInfo.shopId where");
            if (cityId > 0)
            {
                strSql.AppendFormat(" shopInfo.cityId='{0}' and", cityId);
            }
            if (company != 0)//表示默认选中的是所有门店
            {
                strSql.AppendFormat(" PreOrder19dian.companyId='{0}'", company);
                strSql.Append(" and");
                if (shopId != 0)
                {
                    strSql.AppendFormat(" PreOrder19dian.shopId='{0}'", shopId);
                    strSql.Append(" and");
                }
            }
            // strSql.Append("  preOrderTime between @strTime and @endTime ");
            if (flag)
            {
                strSql.AppendFormat(" ( preOrderTime between @strTime and @endTime and CONVERT(varchar(100),preOrderTime, 108) between '{0}' and '23:59:59')", strHour);
                strSql.AppendFormat(" or ( preOrderTime between @strTime and @endTime and CONVERT(varchar(100),preOrderTime, 108) between '00:00:00' and '{0}')", endHour);
                strHour = " " + strHour;
                endHour = " " + endHour;
                //flag==true执行下列代码，此处UI层strTime，endTime已赋值为xxxx/xx/xx 00:00:00，无需再做转换重新拼接，直接调用即可
                //strSql.AppendFormat(" and ( preOrderTime not between convert(varchar(12),'" + strTime + "',120)+' 00:00:00' and convert(varchar(10),preOrderTime,120)+'" + strHour + "'");
                //strSql.AppendFormat(" and  preOrderTime not between convert(varchar(12),'" + endTime + "',120)+' 00:00:00' and convert(varchar(10),preOrderTime,120)+'" + endHour + "')");
                strSql.AppendFormat(" and ( preOrderTime not between '" + strTime + "' and convert(varchar(10),preOrderTime,120)+'" + strHour + "'");
                strSql.AppendFormat(" and  preOrderTime not between  '" + endTime + "' and convert(varchar(10),preOrderTime,120)+'" + endHour + "')");
            }
            else
            {
                strSql.AppendFormat("  preOrderTime between @strTime and @endTime and  CONVERT(varchar(100),preOrderTime, 108) between '{0}' and '{1}'", strHour, endHour);
            }
            // strSql.AppendFormat(" and CONVERT(varchar(100),preOrderTime, 108) between '{0}' and '{1}'", strHour, endHour);
            strSql.Append(" group by convert(varchar(10),preOrderTime,120)");
            strSql.Append(" order by convert(varchar(10),preOrderTime,120)");
            SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@strTime", strTime),
                    new SqlParameter("@endTime", endTime)};
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            return ds.Tables[0];
        }
        /// <summary>
        /// 查询城市订单量时段明细
        /// condition: 查询条件
        /// </summary>
        public DataTable GetOrderDetailStatisticsInfo(DateTime strTime, DateTime endTime, int company, int shopId, string strHour, string endHour, string condition, int orderStatus, bool flag)
        {
            StringBuilder strSql = new StringBuilder();
            if (condition == "PayOrder")
            {
                strSql.Append("select SUBSTRING(CONVERT(varchar(100),prePayTime, 108),0,3) as hourTime,");
            }
            else
            {
                strSql.Append("select SUBSTRING(CONVERT(varchar(100),preOrderTime, 108),0,3) as hourTime,");
            }
            strSql.Append(" ISNULL( COUNT(case when City.cityName='杭州市' then '杭州市' end),0) as cityHangzhouOrdersCount,");
            strSql.Append(" ISNULL( COUNT(case when City.cityName='北京市' then '北京市' end),0) as cityBeijingOrdersCount,");
            strSql.Append(" ISNULL( COUNT(case when City.cityName='上海市' then '上海市' end),0) as cityShanghaiOrdersCount,");
            strSql.Append(" ISNULL(COUNT(case when City.cityName='杭州市' then '杭州市' end)");
            strSql.Append(" +COUNT(case when City.cityName='北京市' then '北京市' end)+");
            strSql.Append(" COUNT(case when City.cityName='上海市' then '上海市' end),0) as nationwideOrdersCount");
            strSql.Append(" from PreOrder19dian inner join ShopInfo ");
            strSql.Append(" on PreOrder19dian.shopId=ShopInfo.shopID inner join City ");
            strSql.Append(" on City.cityID=ShopInfo.cityID  where ");
            if (company != 0)
            {
                strSql.AppendFormat(" PreOrder19dian.companyId='{0}'", company);
                strSql.Append(" and");
                if (shopId != 0)
                {
                    strSql.AppendFormat(" PreOrder19dian.shopId='{0}'", shopId);
                    strSql.Append(" and");
                }
                else
                {
                    //所有门店
                }
            }
            #region 支付订单明细
            if (condition == "PayOrder")//表示是支付的订单
            {
                strSql.AppendFormat(" PreOrder19dian.isPaid='{0}' ", (int)VAPreorderIsPaid.PAID);//过滤剩下的是支付过的订单
                // strSql.Append(" and PreOrder19dian.prePayTime between @strTime and @endTime ");
                if (flag)
                {
                    strSql.AppendFormat(" and (PreOrder19dian.prePayTime between @strTime and @endTime and CONVERT(varchar(100),prePayTime, 108) between '{0}' and '23:59:59')", strHour);
                    strSql.AppendFormat(" or ( PreOrder19dian.prePayTime between @strTime and @endTime and CONVERT(varchar(100),prePayTime, 108) between '00:00:00' and '{0}')", endHour);
                    strHour = " " + strHour;
                    endHour = " " + endHour;
                    strSql.AppendFormat(" and ( PreOrder19dian.prePayTime not between convert(varchar(10),'" + strTime + "',120)+' 00:00:00' and convert(varchar(10),PreOrder19dian.prePayTime,120)+'" + strHour + "'");
                    strSql.AppendFormat(" and  PreOrder19dian.prePayTime not between convert(varchar(10),'" + endTime + "',120)+' 00:00:00' and convert(varchar(10),PreOrder19dian.prePayTime,120)+'" + endHour + "')");
                }
                else
                {
                    strSql.AppendFormat(" and PreOrder19dian.prePayTime between @strTime and @endTime  and CONVERT(varchar(100),prePayTime, 108) between '{0}' and '{1}'", strHour, endHour);
                }
                strSql.Append(" group by SUBSTRING(CONVERT(varchar(100),prePayTime, 108),0,3) ");//根据小时分组
                strSql.Append(" order by SUBSTRING(CONVERT(varchar(100),prePayTime, 108),0,3) ");//根据小时排序
            }
            #endregion

            #region 普通订单明细（支付和未支付）
            else//订单
            {
                if (flag)
                {
                    strSql.AppendFormat(" ( PreOrder19dian.preOrderTime between @strTime and @endTime  and CONVERT(varchar(100),preOrderTime, 108) between '{0}' and '23:59:59')", strHour);
                    strSql.AppendFormat(" or ( PreOrder19dian.preOrderTime between @strTime and @endTime and CONVERT(varchar(100),preOrderTime, 108) between '00:00:00' and '{0}')", endHour);
                    strHour = " " + strHour;
                    endHour = " " + endHour;
                    strSql.AppendFormat(" and ( preOrderTime not between convert(varchar(10),'" + strTime + "',120)+' 00:00:00' and convert(varchar(10),preOrderTime,120)+'" + strHour + "'");
                    strSql.AppendFormat(" and  preOrderTime not between convert(varchar(10),'" + endTime + "',120)+' 00:00:00' and convert(varchar(10),preOrderTime,120)+'" + endHour + "')");
                }
                else
                {
                    strSql.AppendFormat(" PreOrder19dian.preOrderTime between @strTime and @endTime  and CONVERT(varchar(100),preOrderTime, 108) between '{0}' and '{1}'", strHour, endHour);
                }
                //strSql.Append("  PreOrder19dian.preOrderTime between @strTime and @endTime ");
                if (orderStatus == 2)//表示是查询的是已验证(审核入座)的订单
                {
                    strSql.AppendFormat(" and PreOrder19dian.isShopConfirmed='{0}'", (int)VAPreOrderShopConfirmed.SHOPCONFIRMED);
                }
                strSql.Append(" group by SUBSTRING(CONVERT(varchar(100),preOrderTime, 108),0,3) ");//根据小时分组
                strSql.Append(" order by SUBSTRING(CONVERT(varchar(100),preOrderTime, 108),0,3) ");//根据小时排序
            }
            #endregion

            SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@strTime", strTime),
                    new SqlParameter("@endTime", endTime)};
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            return ds.Tables[0];
        }
        //用户统计
        /// <summary>
        /// 查询杭州，上海，北京三个城市的用户数量信息
        /// </summary>
        /// <returns></returns>
        public DataTable QueryUsersAmountByDifferentCity(DateTime strTime, DateTime endTime, string strHour, string endHour)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select convert(varchar(10),RegisterDate,120) as 'registerDate',");
            strSql.Append(" sum(0) as 'dayAddAmout',");
            strSql.Append(" ISNULL( COUNT(case when City.cityName='杭州市' then '杭州市' end),0) as 'cityHangzhouUsersAddAmount',");
            strSql.Append(" ISNULL( COUNT(case when City.cityName='北京市' then '北京市' end),0) as 'cityBeijingUsersAddAmount',");
            strSql.Append(" ISNULL( COUNT(case when City.cityName='上海市' then '上海市' end),0) as 'cityShanghaiUsersAddAmount',");
            strSql.Append(" ISNULL(COUNT(case when City.cityName='杭州市' then '杭州市' end)");
            strSql.Append(" +COUNT(case when City.cityName='北京市' then '北京市' end)+");
            strSql.Append(" COUNT(case when City.cityName='上海市' then '上海市' end),0) as 'usersAddAmount'");
            strSql.Append(" ,(SELECT COUNT(1) FROM CustomerInfo B WHERE B.RegisterDate< convert(varchar(10),CustomerInfo.RegisterDate,120)+' 23:59:59') userCount");
            strSql.Append(" from CustomerInfo inner join City on City.cityID=CustomerInfo.registerCityId ");
            strSql.Append(" where CustomerInfo.RegisterDate between @strTime and @endTime");
            strSql.Append(" and CONVERT(varchar(100),RegisterDate, 108) between @strHour and @endHour");
            strSql.Append(" group by CONVERT(varchar(10),RegisterDate, 120)");
            strSql.Append(" order by CONVERT(varchar(10),RegisterDate, 120) desc");
            SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@strTime", strTime),
                    new SqlParameter("@endTime", endTime),
            new SqlParameter("@strHour", strHour),
            new SqlParameter("@endHour", endHour)};
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            return ds.Tables[0];
        }
        /// <summary>
        /// 查询杭州，上海，北京三个城市不同时间段的用户数量信息
        /// </summary>
        /// <returns></returns>
        public DataTable QueryUserAmountByDifferentHour(DateTime strTime, DateTime endTime, string strHour, string endHour)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SUBSTRING(CONVERT(varchar(100),RegisterDate, 108),0,3) as registerHourTime,");
            strSql.Append(" ISNULL( COUNT(case when City.cityName='杭州市' then '杭州市' end),0)");
            strSql.Append(" as cityHangzhouUsersCount, ISNULL( COUNT(case when City.cityName='北京市' then '北京市' end),0)");
            strSql.Append(" as cityBeijingUsersCount, ISNULL( COUNT(case when City.cityName='上海市' then '上海市' end),0)");
            strSql.Append(" as cityShanghaiUsersCount, ISNULL(COUNT(case when City.cityName='杭州市' then '杭州市' end) +COUNT(case ");
            strSql.Append(" when City.cityName='北京市' then '北京市' end)+ COUNT(case when City.cityName='上海市' then '上海市' end),0)");
            strSql.Append(" as nationwideUsersCount from CustomerInfo inner join City  on  City.cityID=CustomerInfo.registerCityId  where CustomerInfo.RegisterDate");
            strSql.Append(" between @strTime and @endTime  and CONVERT(varchar(100),RegisterDate, 108) between @strHour and @endHour");
            strSql.Append(" group by SUBSTRING(CONVERT(varchar(100),RegisterDate, 108),0,3)");
            strSql.Append(" order by SUBSTRING(CONVERT(varchar(100),RegisterDate, 108),0,3)");
            SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@strTime", strTime),
                    new SqlParameter("@endTime", endTime),
            new SqlParameter("@strHour", strHour),
            new SqlParameter("@endHour", endHour)};
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            return ds.Tables[0];
        }
        /// <summary>
        /// 门店统计
        /// </summary>
        /// <param name="strTime"></param>
        /// <param name="endTime"></param>
        /// <param name="handleStusts"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public DataTable QueryShopAmountByDifferentCity(string strTime, string endTime, int handleStusts, int cityId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"select CONVERT(varchar(10),A.shopRegisterTime, 120) as dayTime,COUNT(A.shopID) as shopCount,
(select COUNT(B.shopID) from ShopInfo B 
where B.shopStatus=1 and B.shopRegisterTime<=CONVERT(varchar(10),A.shopRegisterTime, 120)+' 00:00:00'");
            if (cityId > 0)
            {
                strSql.AppendFormat(" and B.cityID={0}", cityId);
            }
            switch (handleStusts)
            {
                case 2:
                    break;
                default:
                    strSql.AppendFormat(@" and B.isHandle={0}", handleStusts);
                    break;
            }
            strSql.AppendFormat(@" ) as totalShopCount from ShopInfo A
where A.shopStatus=1 and CONVERT(varchar(10),A.shopRegisterTime, 120) between '{1}' and '{2}'", cityId, strTime, endTime);
            if (cityId > 0)
            {
                strSql.AppendFormat(" and A.cityID={0}", cityId);
            }
            switch (handleStusts)
            {
                case 2:
                    break;
                default:
                    strSql.AppendFormat(@" and A.isHandle={0}", handleStusts);
                    break;
            }
            strSql.AppendFormat(@" group by CONVERT(varchar(10),A.shopRegisterTime, 120)");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        ///(wangchunwen) 按城市获取会员信息
        /// </summary>
        /// <returns></returns>
        public DataTable QueryMemberByDifferentCity(DateTime strTime, DateTime endTime, int provinceID, int cityID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select b.eCardNumber,b.RegisterDate,d.provinceID,d.cityID,d.cityName,");
            strSql.Append(" (case c.appType when 1 then 'iPhone' when 2 then 'iPad' when 3 then 'android' when 4 then '微信' end) as appType from dbo.CustomerConnDevice a ");
            strSql.Append(" inner join dbo.CustomerInfo b on a.customerId = b.CustomerID");
            strSql.Append(" inner join dbo.DeviceInfo c on a.deviceId = c.deviceId inner join dbo.City d on b.registerCityId = d.cityID ");
            strSql.Append("where b.RegisterDate >= @strTime and b.RegisterDate <= @endTime");
            SqlParameter[] parameters;
            if (provinceID > 0)
            {
                if (cityID > 0)
                {
                    strSql.Append(" and d.provinceID = @provinceID and d.cityID= @cityID");
                }
                else
                {
                    strSql.Append(" and d.provinceID = @provinceID");
                }
            }
            parameters = new SqlParameter[] { 
                                                new SqlParameter("@strTime", strTime),
                                                new SqlParameter("@endTime", endTime),
                                                new SqlParameter("@provinceID",provinceID),
                                                new SqlParameter("@cityID",cityID)};
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            return ds.Tables[0];
        }
        /// <summary>
        /// (wangchunwen)按城市统计会员数量
        /// </summary>
        /// <returns></returns>
        public DataTable QueryMemberAmountByDifferentCity(DateTime strTime, DateTime endTime, int provinceID, int cityID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select COUNT(*) as memberAmount,tba.cityName,tba.appType from ");
            strSql.Append(" (select b.eCardNumber,b.RegisterDate,d.provinceID,d.cityID,d.cityName,");
            strSql.Append(" (case c.appType when 1 then 'iPhone' when 2 then 'iPad' when 3 then 'android' when 4 then '微信' end) as appType");
            strSql.Append(" from dbo.CustomerConnDevice a inner join dbo.CustomerInfo b on a.customerId = b.CustomerID");
            strSql.Append(" inner join dbo.DeviceInfo c on a.deviceId = c.deviceId inner join dbo.City d on b.registerCityId = d.cityID");
            strSql.Append(" where b.RegisterDate >= @strTime and b.RegisterDate <= @endTime");
            SqlParameter[] parameters;
            if (provinceID > 0)
            {
                if (cityID > 0)
                {
                    strSql.Append(" and d.provinceID = @provinceID and d.cityID= @cityID");
                }
                else
                {
                    strSql.Append(" and d.provinceID = @provinceID");
                }
            }
            strSql.Append(" ) as tba group by tba.cityName,tba.appType");
            parameters = new SqlParameter[] { 
                                                new SqlParameter("@strTime", strTime),
                                                new SqlParameter("@endTime", endTime),
                                                new SqlParameter("@provinceID",provinceID),
                                                new SqlParameter("@cityID",cityID)};
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            return ds.Tables[0];
        }
        //访问接口（增加和查询）
        /// <summary>
        /// 插入访问API接口日志记录
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public long InsertInvokedAPILogInfo(int invokedAPIType)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into [InvokedAPILog](");
                    strSql.Append("invokedAPIType,invokedAPITime)");
                    strSql.Append(" values (");
                    strSql.Append("@invokedAPIType,@invokedAPITime)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
					new SqlParameter("@invokedAPIType", SqlDbType.Int,4),
					new SqlParameter("@invokedAPITime", SqlDbType.DateTime)};

                    parameters[0].Value = invokedAPIType;
                    parameters[1].Value = DateTime.Now;

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
        /// 查询访问ApI的次数
        /// </summary>
        /// <returns></returns>
        public DataTable QueryInvokedAPILogInfo(DateTime strTime, DateTime endTime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select convert(varchar(10),invokedAPITime,120) as 'invokedAPITime',");
            strSql.Append(" ISNULL( COUNT(case when InvokedAPILog.invokedAPIType='" + (int)VAInvokedAPIType.API_REFRESH_COMPANY_LIST + "' then '1' end),0) as 'invokedAPIAmount_companyList',");
            strSql.Append(" ISNULL( COUNT(case when InvokedAPILog.invokedAPIType='" + (int)VAInvokedAPIType.API_SELECT_PREORDER_LIST + "' then '4' end),0) as 'invokedAPIAmount_preorderList',");
            strSql.Append(" ISNULL( COUNT(case when InvokedAPILog.invokedAPIType='" + (int)VAInvokedAPIType.API_USERS_LOGIN + "' then '2' end),0) as 'invokedAPIAmount_userLogin',");
            strSql.Append(" ISNULL( COUNT(case when InvokedAPILog.invokedAPIType='" + (int)VAInvokedAPIType.API_USERS_REGISTER + "' then '3' end),0) as 'invokedAPIAmount_userRegister'");
            strSql.Append(" from InvokedAPILog ");
            strSql.Append(" where InvokedAPILog.invokedAPITime between @strTime and @endTime");
            strSql.Append(" group by CONVERT(varchar(10),invokedAPITime, 120)");
            strSql.Append(" order by CONVERT(varchar(10),invokedAPITime, 120)");
            SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@strTime", strTime),
                    new SqlParameter("@endTime", endTime)};
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            return ds.Tables[0];
        }
        //查询预点单信息导出excel表格
        /// <summary>
        /// 查询需要导出excel表格的预点单信息
        /// </summary>
        /// <returns></returns>
        public DataTable QueryPreOrderInfo(int cityId)
        {
            string strSql = String.Empty;
            if (cityId > 0)
            {
                strSql = @"select Distinct(A.preOrder19dianId) as '订单编号' ,
isnull(H.cityname,'') as '城市名称',
C.companyName as '品牌名',
B.shopName as '店铺名',
D.mobilePhoneNumber as '手机号码',
A.preOrderTime as '下单时间',
isnull(A.prePayTime,'') as '支付时间',
isnull(Max(E.shopConfirmedTime),'') as '入座时间',
round(A.preOrderSum,2) as '点单金额',
round(isnull(A.prePaidSum,0),2) as '支付金额',

case when isnull(A.isPaid,0)=1 then round(ISNULL(ROUND(A.prePaidSum,2),0)-isnull((select SUM(temp.totalFee) from (select ali.totalFee 
from  AlipayOrderInfo ali where ali.connId =A.preOrder19dianId and orderStatus = 2) temp),0)-
isnull((select SUM(temp.totalFee) from (select wx.totalFee from  WechatPayOrderInfo wx where wx.connId =A.preOrder19dianId and orderStatus = 2) temp),0)-
isnull((select sum(currectUsedAmount) FROM RedEnvelopeConnPreOrder r where r.preOrder19dianId =A.preOrder19dianId),0),2) else 0 end '(支付)粮票',
             
case when isnull(A.isPaid,0)=1 and ISNULL(A.prePaidSum,0)>0 then isnull((select SUM(temp.totalFee) from (select ali.totalFee 
from  AlipayOrderInfo ali where ali.connId =A.preOrder19dianId and orderStatus = 2) temp),0) else 0 end '(支付)支付宝',

case when isnull(A.isPaid,0)=1 and ISNULL(A.prePaidSum,0)>0 then isnull((select SUM(temp.totalFee) from (select wx.totalFee 
from  WechatPayOrderInfo wx where wx.connId =A.preOrder19dianId and orderStatus = 2) temp),0) else 0 end '(支付)微信',

case when isnull(A.isPaid,0)=1 and ISNULL(A.prePaidSum,0)>0 then isnull((select sum(currectUsedAmount)
FROM RedEnvelopeConnPreOrder r where r.preOrder19dianId =A.preOrder19dianId),0) else 0 end '(支付)其他',

round(isnull(A.refundMoneySum,0),2) as '退款金额'
from PreOrder19dian A 
inner join CompanyInfo C on C.companyID=A.companyId 
inner join ShopInfo B on B.shopID=A.shopId 
inner join CustomerInfo D on D.CustomerID=A.customerId
inner join city H on H.cityid=B.cityId
left join PreorderShopConfirmedInfo E on E.preorder19dianId = A.preOrder19dianId
where B.cityId='" + cityId + "' group by H.cityname,A.preOrder19dianId,C.companyName,B.shopName,A.preOrderTime,A.prePayTime,A.prePaidSum,A.preOrderSum,D.mobilePhoneNumber,refundMoneySum,B.shopID,A.isPaid";
            }
            else
            {
                strSql = @"select Distinct(A.preOrder19dianId) as '订单编号' ,
isnull(H.cityname,'') as '城市名称',
C.companyName as '品牌名',
B.shopName as '店铺名',
D.mobilePhoneNumber as '手机号码',
A.preOrderTime as '下单时间',
isnull(A.prePayTime,'') as '支付时间',
isnull(Max(E.shopConfirmedTime),'') as '入座时间',
round(A.preOrderSum,2) as '点单金额',
round(isnull(A.prePaidSum,0),2) as '支付金额',

case when isnull(A.isPaid,0)=1 then round(ISNULL(ROUND(A.prePaidSum,2),0)-isnull((select SUM(temp.totalFee) from (select ali.totalFee 
from  AlipayOrderInfo ali where ali.connId =A.preOrder19dianId and orderStatus = 2) temp),0)-
isnull((select SUM(temp.totalFee) from (select wx.totalFee from  WechatPayOrderInfo wx where wx.connId =A.preOrder19dianId and orderStatus = 2) temp),0)-
isnull((select sum(currectUsedAmount) FROM RedEnvelopeConnPreOrder r where r.preOrder19dianId =A.preOrder19dianId),0),2) else 0 end '(支付)粮票',
             
case when isnull(A.isPaid,0)=1 and ISNULL(A.prePaidSum,0)>0 then isnull((select SUM(temp.totalFee) from (select ali.totalFee 
from  AlipayOrderInfo ali where ali.connId =A.preOrder19dianId and orderStatus = 2) temp),0) else 0 end '(支付)支付宝',

case when isnull(A.isPaid,0)=1 and ISNULL(A.prePaidSum,0)>0 then isnull((select SUM(temp.totalFee) from (select wx.totalFee 
from  WechatPayOrderInfo wx where wx.connId =A.preOrder19dianId and orderStatus = 2) temp),0) else 0 end '(支付)微信',

case when isnull(A.isPaid,0)=1 and ISNULL(A.prePaidSum,0)>0 then isnull((select sum(currectUsedAmount)
FROM RedEnvelopeConnPreOrder r where r.preOrder19dianId =A.preOrder19dianId),0) else 0 end '(支付)其他',

round(isnull(A.refundMoneySum,0),2) as '退款金额'
from PreOrder19dian A 
inner join CompanyInfo C on C.companyID=A.companyId 
inner join ShopInfo B on B.shopID=A.shopId 
inner join CustomerInfo D on D.CustomerID=A.customerId
inner join city H on H.cityid=B.cityId
left join PreorderShopConfirmedInfo E on E.preorder19dianId = A.preOrder19dianId
group by H.cityname,A.preOrder19dianId,C.companyName,B.shopName,A.preOrderTime,A.prePayTime,A.prePaidSum,A.preOrderSum,D.mobilePhoneNumber,refundMoneySum,B.shopID,A.isPaid";
            }
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql);
            return ds.Tables[0];
        }
        /// <summary> 
        /// 统计预点单连续四周的数据
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="shopId"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public DataTable QueryConsumptionWeekStatisticsInfo(int companyId, int shopId, DateTime dateTime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select datename(week,PreOrder19dian.preOrderTime) as 'week', ");//周
            strSql.Append("CONVERT(nvarchar(10), ISNULL( COUNT( PreOrder19dian.preOrder19dianId),0)) as 'newPreOrderCount',");//新增单数
            strSql.Append("CONVERT(nvarchar(20), ISNULL( sum(PreOrder19dian.preOrderSum ),0)) as 'newPreOrderAmount',");//新增金额
            strSql.Append("CONVERT(nvarchar(10), ISNULL( COUNT(case when PreOrder19dian.isShopConfirmed='" + (int)VAPreOrderShopConfirmed.SHOPCONFIRMED + "' then '1' end),0)) as 'newPreOrderCount_Shop',");//新增到店
            strSql.Append("'0' as 'newPreOrderCount_Shop_Proportion',");//到店比例
            strSql.Append("CONVERT(nvarchar(20), ISNULL(SUM (case when PreOrder19dian.isShopConfirmed='" + (int)VAPreOrderShopConfirmed.SHOPCONFIRMED + "' then PreOrder19dian.preOrderSum end),0)) as 'newPreOrderAmount_Shop',");//新增到店金额
            strSql.Append("CONVERT(nvarchar(10),ISNULL( COUNT(case when PreOrder19dian.isPaid='" + (int)VACustomerCouponOrderStatus.PAID + "' then '1' end),0)) as 'newPreOrderCount_isPaid',");//新增手机支付（笔）
            strSql.Append("CONVERT(nvarchar(20),ISNULL(SUM (case when PreOrder19dian.isPaid='" + (int)VACustomerCouponOrderStatus.PAID + "' then PreOrder19dian.prePaidSum end),0)) as 'newPreOrderAmount_isPaid',");//新增手机支付金额
            strSql.Append("'0' as 'newPreOrderAmount_isPaid_Proportion'");//支付比例
            strSql.Append("from PreOrder19dian ");
            strSql.AppendFormat(" where  datename(week,PreOrder19dian.preOrderTime) >= datename(week,'{0}')-3", dateTime);
            strSql.AppendFormat(" and  datename(week,PreOrder19dian.preOrderTime) <= datename(week,'{0}')", dateTime);
            if (companyId != 0)//通过公司查询
            {
                if (shopId != 0)//通过门店从查询
                {
                    strSql.AppendFormat(" and PreOrder19dian.companyId='{0}' and  PreOrder19dian.shopId='{1}'", companyId, shopId);
                }
                else//查询的是所有门店
                {
                    strSql.AppendFormat(" and PreOrder19dian.companyId='{0}'", companyId);
                }
            }
            else//查询的是所有公司
            {

            }
            strSql.Append(" group by  datename(week,PreOrder19dian.preOrderTime)");
            strSql.Append(" order by datename(week,PreOrder19dian.preOrderTime)");//按周排序
            //SqlParameter[] parameters = new SqlParameter[]{
            //        new SqlParameter("@dateTime", dateTime)};
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 统计预点单总数据
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="shopId"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public DataTable QueryAllConsumptionWeekStatisticsInfo(int companyId, int shopId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select ISNULL( COUNT( PreOrder19dian.preOrder19dianId),0) as 'newPreOrderCount',");//新增单数
            strSql.Append(" ISNULL( sum(PreOrder19dian.preOrderSum ),0) as 'newPreOrderAmount',");//新增金额
            strSql.Append(" ISNULL( COUNT(case when PreOrder19dian.isShopConfirmed='" + (int)VAPreOrderShopConfirmed.SHOPCONFIRMED + "' then '1' end),0) as 'newPreOrderCount_Shop',");//新增到店
            strSql.Append(" '0' as 'newPreOrderCount_Shop_Proportion',");//到店比例
            strSql.Append(" ISNULL(SUM (case when PreOrder19dian.isShopConfirmed='" + (int)VAPreOrderShopConfirmed.SHOPCONFIRMED + "' then PreOrder19dian.preOrderSum end),0) as 'newPreOrderAmount_Shop',");//新增到店金额
            strSql.Append(" ISNULL( COUNT(case when PreOrder19dian.isPaid='" + (int)VACustomerCouponOrderStatus.PAID + "' then '1' end),0) as 'newPreOrderCount_isPaid',");//新增手机支付（笔）
            strSql.Append(" ISNULL(SUM (case when PreOrder19dian.isPaid='" + (int)VACustomerCouponOrderStatus.PAID + "' then PreOrder19dian.prePaidSum end),0) as 'newPreOrderAmount_isPaid',");//新增手机支付金额
            strSql.Append(" '0' as 'newPreOrderAmount_isPaid_Proportion'");//支付比例
            strSql.Append(" from PreOrder19dian ");
            if (companyId != 0)//通过公司查询
            {
                if (shopId != 0)//通过门店从查询
                {
                    strSql.AppendFormat(" where PreOrder19dian.companyId='{0}' and  PreOrder19dian.shopId='{1}'", companyId, shopId);
                }
                else//查询的是所有门店
                {
                    strSql.AppendFormat(" where  PreOrder19dian.companyId='{0}'", companyId);
                }
            }
            else//查询的是所有公司
            {

            }
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        //综合查询
        /// <summary>
        /// 综合查询 modify by wangc 20140324
        /// </summary>
        /// <param name="strTime"></param>
        /// <param name="endTime"></param>
        /// <param name="company"></param>
        /// <param name="shopId"></param>
        /// <param name="cityId"></param>
        /// <param name="strHour"></param>
        /// <param name="endHour"></param>
        /// <param name="flag"></param>
        /// <param name="flagStatus">为true表示需要减掉点单退款金额</param>
        /// <returns></returns>
        public DataTable GetComprehensiveStatisticsInfo(DateTime strTime, DateTime endTime, int company, int shopId, int cityId, string strHour, string endHour, bool flag, bool flagStatus = false)
        {
            DataTable dt = new DataTable();
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT convert(varchar(10),preOrderTime,120) as orderTime,");
            strSql.Append(" COUNT (A.preOrder19dianId) as 'orderCount',");
            if (flagStatus == true)
            {
                strSql.Append(" Convert(decimal(20,2),ISNULL(SUM(A.preOrderSum-isnull(A.refundMoneySum,0)),0)) as 'orderSumAmount',");
                strSql.Append(" Convert(decimal(20,2),(case when COUNT (A.preOrder19dianId)=0 then 0 else (ISNULL(SUM(A.preOrderSum-isnull(A.refundMoneySum,0)),0))/COUNT (A.preOrder19dianId) end)) as 'orderTransaction',");
            }
            else
            {
                strSql.Append(" ROUND(ISNULL(SUM(A.preOrderSum),0),2) as 'orderSumAmount',");
                strSql.Append(" ROUND((case when COUNT (A.preOrder19dianId)=0 then 0 else (ISNULL(SUM(A.preOrderSum),0))/COUNT (A.preOrder19dianId) end),2) as 'orderTransaction',");
            }
            strSql.Append(" ISNULL( COUNT(case when A.isPaid=1 then 1 end),0) as 'isPaidOrderCount',");
            if (flagStatus == true)
            {
                strSql.Append(" Convert(decimal(20,2),ISNULL(sum(case when A.isPaid=1 then (A.preOrderServerSum-isnull(A.refundMoneySum,0)) end),0)) as 'isPaidOrderAmount',");
            }
            else
            {
                strSql.Append(" Convert(decimal(20,2),ISNULL(sum(case when A.isPaid=1 then A.preOrderServerSum end),0)) as 'isPaidOrderAmount',");
            }
            strSql.Append(" ISNULL( COUNT(case when A.isShopConfirmed=1 then 1 end),0) as 'isConfrimOrderCount',");
            if (flagStatus == true)
            {
                strSql.Append(" Convert(decimal(20,2),ISNULL(sum(case when A.isShopConfirmed=1 then (A.preOrderServerSum-isnull(A.refundMoneySum,0)) end),0)) as 'isConfrimOrderAmount',");
                strSql.Append(" Convert(decimal(20,2),(case when COUNT(case when A.isPaid=1 then 1 end)=0 then 0 else ISNULL(sum(case when A.isPaid='"
              + (int)VAPreorderIsPaid.PAID + "' then (A.preOrderServerSum-isnull(A.refundMoneySum,0)) end),0)/COUNT(case when A.isPaid=1 then 1 end) end)) as 'isPaidOrderTransaction'");
            }
            else
            {
                strSql.Append(" Convert(decimal(20,2),ISNULL(sum(case when A.isShopConfirmed=1 then A.preOrderServerSum end),0)) as 'isConfrimOrderAmount',");
                strSql.Append(" Convert(decimal(20,2),(case when COUNT(case when A.isPaid=1 then 1 end)=0 then 0 else ISNULL(sum(case when A.isPaid='"
              + (int)VAPreorderIsPaid.PAID + "' then A.preOrderServerSum end),0)/COUNT(case when A.isPaid=1 then 1 end) end)) as 'isPaidOrderTransaction'");
            }
            strSql.Append(" ,CONVERT(nvarchar(10),convert(decimal(20,2),ISNULL( COUNT(case when A.isPaid=1 then 1 end),0)*100/COUNT (A.preOrder19dianId)))+'%' as payRate");
            strSql.Append(" ,count(case when ISNULL(A.refundMoneySum,0)>0 then 1 end) refundOrderCount,ROUND( SUM(ISNULL(A.refundMoneySum,0)),2) refundOrderAmount");//退款笔数，退款金额
            strSql.Append(" from PreOrder19dian A inner join ShopInfo B on A.shopId=B.shopID ");
            strSql.Append(" where ");
            if (flag)
            {
                strSql.AppendFormat(" (A.preOrderTime between @strTime and @endTime  and CONVERT(varchar(100),preOrderTime, 108) between '{0}' and '23:59:59')", strHour);
                strSql.AppendFormat(" or ( preOrderTime between @strTime and @endTime and  CONVERT(varchar(100),preOrderTime, 108) between '00:00:00' and '{0}')", endHour);
                strHour = " " + strHour;
                endHour = " " + endHour;
                strSql.AppendFormat(" and (A.preOrderTime not between '" + strTime + "' and convert(varchar(10),preOrderTime,120)+'" + strHour + "'");
                strSql.AppendFormat(" and A.preOrderTime not between '" + endTime + "' and convert(varchar(10),preOrderTime,120)+'" + endHour + "')");
            }
            else
            {
                strSql.AppendFormat("  preOrderTime between @strTime and @endTime  and CONVERT(varchar(100),preOrderTime, 108) between '{0}' and '{1}'", strHour, endHour);
            }
            if (cityId == 0)//
            {
                if (company != 0)//表示默认选中的是所有门店
                {
                    strSql.AppendFormat(" and A.companyId='{0}'", company);
                    if (shopId != 0)
                    {
                        strSql.AppendFormat(" and A.shopId='{0}'", shopId);
                    }
                }
            }
            else
            {
                strSql.AppendFormat(" and B.cityID='{0}'", cityId);
            }

            strSql.Append(" group by convert(varchar(10),A.preOrderTime,120)");
            strSql.Append(" order by convert(varchar(10),A.preOrderTime,120)");
            SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@strTime", strTime),
                    new SqlParameter("@endTime", endTime)};//
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            return ds.Tables[0];
        }
        /// <summary>
        /// 门店列表 add by wangc 20140321
        /// </summary>
        /// <param name="strTime"></param>
        /// <param name="endTime"></param>
        /// <param name="companyId"></param>
        /// <param name="shopId"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public DataTable GetShopOrderList(string strTime, string endTime, int companyId, int shopId, int cityId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select B.shopName,count(preOrder19dianId) OrderCount,B.shopID,");
            strSql.Append(" ROUND( SUM(preOrderSum),2) OrderAmount,count(case when A.isPaid=1 then 1 end) PayCount,");
            strSql.Append(" ISNULL( ROUND (SUM(case when A.isPaid=1 then prePaidSum end),2),0) PayAmount,");
            strSql.Append(" CONVERT(nvarchar(10),convert(decimal(20,0),(count(case when A.isPaid=1 then 1 end))*100/(count(preOrder19dianId))))+'%' PayRate");
            strSql.Append(" ,count(case when ISNULL(A.refundMoneySum,0)>0 then 1 end) refundOrderCount,ROUND( SUM(ISNULL(A.refundMoneySum,0)),2) refundOrderAmount");
            strSql.Append(" ,isnull(E.EmployeeFirstName,'') EmployeeFirstName,isnull(F.EmployeeFirstName,'') AreaManagerName");
            strSql.AppendFormat(@",ISNULL( ROUND (SUM(case when A.isPaid=1 then prePaidSum end),2),0)-
isnull((select SUM(temp.totalFee) from (select ali.totalFee 
from  AlipayOrderInfo ali 
where ali.connId in (select C.preOrder19dianId
from PreOrder19dian C 
where C.preOrderTime between '{0}' and '{1}' and C.isPaid=1
and C.shopId=B.shopID)  and orderStatus = 2) temp),0)-
isnull((select SUM(temp.totalFee) from (select wx.totalFee 
from  WechatPayOrderInfo wx 
where wx.connId in (select C.preOrder19dianId
from PreOrder19dian C 
where C.preOrderTime between '{0}' and '{1}' and C.isPaid=1
and C.shopId=B.shopID)  and orderStatus = 2) temp),0)-
isnull((select sum(currectUsedAmount)
FROM RedEnvelopeConnPreOrder r where r.preOrder19dianId in (select C.preOrder19dianId
from PreOrder19dian C 
where C.preOrderTime between '{0}' and '{1}' and C.isPaid=1
and C.shopId=B.shopID)),0) yuePay
             
,isnull((select SUM(temp.totalFee) from (select ali.totalFee 
from  AlipayOrderInfo ali 
where ali.connId in (select C.preOrder19dianId
from PreOrder19dian C 
where C.preOrderTime between '{0}' and '{1}' and C.isPaid=1
and C.shopId=B.shopID)  and orderStatus = 2) temp),0) aliPay

,isnull((select SUM(temp.totalFee) from (select wx.totalFee 
from  WechatPayOrderInfo wx 
where wx.connId in (select C.preOrder19dianId
from PreOrder19dian C 
where C.preOrderTime between '{0}' and '{1}' and C.isPaid=1
and C.shopId=B.shopID)  and orderStatus = 2) temp),0) wechatPay

,isnull((select sum(currectUsedAmount)
FROM RedEnvelopeConnPreOrder r where r.preOrder19dianId in (select C.preOrder19dianId
from PreOrder19dian C 
where C.preOrderTime between '{0}' and '{1}' and C.isPaid=1
and C.shopId=B.shopID)),0) redEnvelopePay", strTime, endTime);

            strSql.Append(" from PreOrder19dian A");
            strSql.Append(" inner join ShopInfo B on A.shopId=B.shopID left join employeeinfo E on B.accountManager=E.employeeid");
            strSql.Append(" left join employeeinfo F on B.AreaManager=F.employeeid");
            strSql.AppendFormat(" where A.preOrderTime between '{0}' and '{1}'", strTime, endTime);
            if (cityId > 0)
            {
                strSql.AppendFormat(" and B.cityId={0}", cityId);
            }
            if (companyId > 0)
            {
                strSql.AppendFormat(" and B.companyId={0}", companyId);
                if (shopId > 0)
                {
                    strSql.AppendFormat(" and B.shopID={0}", shopId);
                }
            }
            strSql.Append(" group by B.shopName,B.shopID,E.EmployeeFirstName,F.EmployeeFirstName order by OrderCount desc");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), null);
            return ds.Tables[0];
        }
        /// <summary>
        /// 单店单日订单列表  add by wangc 20140321
        /// </summary>
        /// <param name="strTime"></param>
        /// <param name="endTime"></param>
        /// <param name="shopId"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public DataTable GetSingleShopDayOrderList(string strTime, string endTime, int shopId, int companyId)
        {
            StringBuilder strSql = new StringBuilder();
            //strSql.Append(" select B.UserName,B.mobilePhoneNumber,A.preOrderTime,round( A.preOrderSum,2) preOrderSum,round(case when A.isPaid=1 then  isnull(A.prePaidSum,0) else 0 end ,2) prePaidSum,A.prePayTime");
            //strSql.Append("  ,case when ISNULL(A.refundMoneySum,0)>0 then 1 else 0 end refundOrderCount");
            //strSql.Append("  ,ROUND(ISNULL(A.refundMoneySum,0),2) refundOrderAmount,A.preOrder19dianId,isnull(A.isPaid,0) ispaid");
            //strSql.Append(" from PreOrder19dian A inner join CustomerInfo B on A.customerId=B.CustomerID");
            //strSql.AppendFormat(" where  A.preOrderTime between '{0}' and '{1}'", strTime, endTime);
            strSql.AppendFormat(@"select B.UserName,B.mobilePhoneNumber,A.preOrderTime,round( A.preOrderSum,2) preOrderSum,
round(case when A.isPaid=1 then  isnull(A.prePaidSum,0) else 0 end ,2) prePaidSum,A.prePayTime
,case when ISNULL(A.refundMoneySum,0)>0 then 1 else 0 end refundOrderCount
,ROUND(ISNULL(A.refundMoneySum,0),2) refundOrderAmount,A.preOrder19dianId,isnull(A.isPaid,0) ispaid,C.shopID,C.shopName, 

(round(case when A.isPaid=1 then  isnull(A.prePaidSum,0) else 0 end ,2) -
isnull((select ali.totalFee 
from  AlipayOrderInfo ali 
where ali.connId = A.preOrder19dianId  and orderStatus = 2),0)-
isnull((select wx.totalFee
from  WechatPayOrderInfo wx 
where wx.connId = A.preOrder19dianId  and orderStatus = 2),0)-
isnull((select sum(currectUsedAmount) consumeRedEnvelopeAmount
FROM RedEnvelopeConnPreOrder r where r.preOrder19dianId=A.preOrder19dianId),0)) yuePay,
             
             isnull((select ali.totalFee 
             from  AlipayOrderInfo ali 
             where ali.connId = A.preOrder19dianId  and orderStatus = 2),0) aliPay,
             isnull((select wx.totalFee
             from  WechatPayOrderInfo wx 
             where wx.connId = A.preOrder19dianId  and orderStatus = 2),0) wechatPay, 
             isnull((select sum(currectUsedAmount) consumeRedEnvelopeAmount
             FROM RedEnvelopeConnPreOrder r where r.preOrder19dianId=A.preOrder19dianId),0) redEnvelopePay
          
from PreOrder19dian A inner join CustomerInfo B on A.customerId=B.CustomerID
inner join ShopInfo C on C.shopID=A.shopId
where  A.preOrderTime between '{0}' and '{1}'", strTime, endTime);
            if (companyId > 0)
            {
                strSql.AppendFormat(" and A.companyId={0}", companyId);
                if (shopId > 0)
                {
                    strSql.AppendFormat(" and A.shopId={0}", shopId);
                }
            }
            strSql.Append(" order by A.preOrderTime desc");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), null);
            return ds.Tables[0];
        }
        /// <summary>
        /// 时间段点单用户数量统计 add by wangc 20140322
        /// </summary>
        /// <param name="strTime">开始时间</param>
        /// <param name="endTime">截止时间</param>
        /// <returns></returns>
        public DataTable GetOrderCustomer(string strTime, string endTime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select COUNT(case when  temp.orderCount>=2 then 1 end) as topCount,");
            strSql.Append(" COUNT (case when temp.orderCount>=1 and temp.orderCount<2 then 1 end)  as middleCount,");
            strSql.Append(" COUNT (case when temp.orderCount<=0 then 1 end) as bottomCount,");
            strSql.Append(" (select COUNT (EmployeeInfo.EmployeeID) from EmployeeInfo");
            strSql.AppendFormat(" where EmployeeInfo.EmployeeStatus=1 and  registerTime between '{0}' and '{1}' and (EmployeeInfo.isViewAllocWorker is null or EmployeeInfo.isViewAllocWorker=0)) employeeCount", strTime, endTime);
            strSql.Append(" from (select COUNT (PreOrder19dian.preOrder19dianId) orderCount");
            strSql.Append(" from CustomerInfo left join PreOrder19dian on CustomerInfo.CustomerID=PreOrder19dian.customerId");
            strSql.Append(" where PreOrder19dian.isPaid=1");
            strSql.AppendFormat(" and PreOrder19dian.preOrderTime between '{0}' and '{1}'", strTime, endTime);
            strSql.Append(" group by CustomerInfo.CustomerID)temp");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), null);
            return ds.Tables[0];
        }

        /// <summary>
        /// 查询悠先服务活跃用户 add by wangc 20140324
        /// </summary>
        /// <param name="strTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public DataTable GetActiveEmployee(string strTime, string endTime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("  select COUNT(A.EmployeeID) employeeCount, CONVERT(varchar(100), B.shopConfirmedTime, 111) statisticsTime");
            strSql.Append(" from EmployeeInfo A");
            strSql.Append(" inner join PreorderShopConfirmedInfo B on A.EmployeeID=B.employeeId");
            strSql.Append(" where B.status=1 and (A.isViewAllocWorker=0 or A.isViewAllocWorker is null)");
            strSql.AppendFormat(" and B.shopConfirmedTime between '{0}' and '{1}'", strTime, endTime);
            strSql.Append(" group by CONVERT(varchar(100), B.shopConfirmedTime, 111)");
            strSql.Append(" having COUNT(B.id) >=2");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), null);
            return ds.Tables[0];
        }
        /// <summary>
        /// 用户积分统计  add by wangc 20140324
        /// </summary>
        /// <param name="strTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public DataTable GetPointEmployee(string strTime, string endTime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("  select CONVERT(varchar(100), A.operateTime, 111) 'statisticsTime',COUNT(A.id) 'pointConsumptionCount',");
            strSql.Append(" CONVERT(nvarchar(10), COUNT(A.id)*100/(select COUNT (EmployeeInfo.EmployeeID) from EmployeeInfo ");
            strSql.AppendFormat("  where EmployeeInfo.EmployeeStatus=1 and  registerTime between '{0}' and '{1}' and ", strTime, endTime);
            strSql.Append("  (EmployeeInfo.isViewAllocWorker is null or EmployeeInfo.isViewAllocWorker=0)))+'%' proportion,");
            strSql.Append("  SUM(A.pointVariation)*(-1) 'pointConsumptionAmount'");
            strSql.Append(" from EmployeePointLog A left join EmployeeInfo B on A.employeeId=B.EmployeeID");
            strSql.AppendFormat(" where A.pointVariationMethods=1 and A.operateTime between '{0}' and '{1}'", strTime, endTime);
            strSql.Append(" group by CONVERT(varchar(100), A.operateTime, 111),A.employeeId");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), null);
            return ds.Tables[0];
        }
        /// <summary>
        /// 查询门店点单数量 add by wangc 20140325
        /// </summary>
        /// <param name="minAmount"></param>
        /// <param name="maxAmount"></param>
        /// <param name="strTime"></param>
        /// <param name="endTime"></param>
        /// <param name="flag"></param>
        /// <param name="flag1"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public DataTable SelectShopOrderByCount(double minAmount, double maxAmount, string strTime, string endTime, int flag, int flag1, int cityId,int? shopState = null)
        {
            StringBuilder strSql = new StringBuilder();
            switch (flag1)
            {
                case 1:
                    strSql.Append(" select H.cityName,B.shopID,case when B.isHandle=1 then '是' else '否' end shopStatus,B.shopName,C.EmployeeFirstName, CONVERT(varchar(100),A.prePayTime,111) time,");
                    if (flag == 2)
                    {
                        strSql.Append(" COUNT(A.preOrder19dianId) total ");
                    }
                    else
                    {
                        strSql.Append(" ROUND(sum( A.prePaidSum-ISNULL(A.refundMoneySum,0)),2) as total ");
                    }
                    strSql.Append(" from PreOrder19dian A inner join ShopInfo B on A.shopId=B.shopID ");
                    strSql.Append(" inner join City H on H.cityId=B.cityID");
                    strSql.Append(" left join EmployeeInfo C on B.accountManager=C.EmployeeID ");
                    strSql.Append(" left join PreorderShopConfirmedInfo E on E.preOrder19dianId=A.preOrder19dianId ");
                    strSql.AppendFormat(" where (A.prePaidSum-ISNULL(A.refundMoneySum,0))>={0}", minAmount);
                    strSql.AppendFormat(" and (A.prePaidSum-ISNULL(A.refundMoneySum,0))<{0}", maxAmount);
                    strSql.Append(" and A.isPaid=1 ");
                    strSql.AppendFormat(" and  A.prePayTime between '{0}' and '{1}'", strTime, endTime);
                    if (cityId > 0)
                    {
                        strSql.AppendFormat(" and  B.cityId={0} ", cityId);
                    }
                    strSql.Append(" group by H.cityName, B.isHandle,B.shopID,B.shopName,CONVERT(varchar(100),A.prePayTime, 111),C.EmployeeFirstName");
                    strSql.Append(" order by H.cityName, B.isHandle,B.shopID,C.EmployeeFirstName ,CONVERT(varchar(100), A.prePayTime, 111)");
                    break;
                case 2:
                    strSql.Append(" select H.cityName,B.shopID,case when B.isHandle=1 then '是' else '否' end shopStatus,B.shopName,C.EmployeeFirstName, CONVERT(varchar(100),E.shopConfirmedTime,111) time,");
                    if (flag == 2)
                    {
                        strSql.Append(" COUNT(A.preOrder19dianId) total ");
                    }
                    else
                    {
                        strSql.Append(" ROUND(sum( A.prePaidSum-ISNULL(A.refundMoneySum,0)),2) as total ");
                    }
                    strSql.Append(" from PreOrder19dian A inner join ShopInfo B on A.shopId=B.shopID ");
                    strSql.Append(" inner join City H on H.cityId=B.cityID");
                    strSql.Append(" left join EmployeeInfo C on B.accountManager=C.EmployeeID ");
                    strSql.Append(" left join PreorderShopConfirmedInfo E on E.preOrder19dianId=A.preOrder19dianId ");
                    strSql.AppendFormat(" where (A.prePaidSum-ISNULL(A.refundMoneySum,0))>={0}", minAmount);
                    strSql.AppendFormat(" and (A.prePaidSum-ISNULL(A.refundMoneySum,0))<{0}", maxAmount);
                    strSql.Append(" and A.isPaid=1 and A.isApproved=1");
                    strSql.Append(" and E.shopConfirmedTime = (select MAX(shopConfirmedTime) from PreorderShopConfirmedInfo where E.preOrder19dianId=preOrder19dianId)");
                    strSql.AppendFormat(" and  E.shopConfirmedTime between '{0}' and '{1}'", strTime, endTime);
                    if (cityId > 0)
                    {
                        strSql.AppendFormat(" and  B.cityId={0} ", cityId);
                    }
                    strSql.Append(" group by H.cityName, B.isHandle,B.shopID,B.shopName,CONVERT(varchar(100), E.shopConfirmedTime, 111),C.EmployeeFirstName");
                    strSql.Append(" order by H.cityName, B.isHandle,B.shopID,C.EmployeeFirstName ,CONVERT(varchar(100), E.shopConfirmedTime, 111)");
                    break;
                case 3:
                default:
                    strSql.Append(" select H.cityName,B.shopID,case when B.isHandle=1 then '是' else '否' end shopStatus,B.shopName,C.EmployeeFirstName, CONVERT(varchar(100),A.prePayTime,111) time,");
                    if (flag == 2)
                    {
                        strSql.Append(" COUNT(A.preOrder19dianId) total ");
                    }
                    else
                    {
                        strSql.Append(" ROUND(sum( A.prePaidSum-ISNULL(A.refundMoneySum,0)),2) as total ");
                    }
                    strSql.Append(" from PreOrder19dian A inner join ShopInfo B on A.shopId=B.shopID ");
                    strSql.Append(" inner join City H on H.cityId=B.cityID");
                    strSql.Append(" left join EmployeeInfo C on B.accountManager=C.EmployeeID ");
                    strSql.AppendFormat(" where (A.prePaidSum-ISNULL(A.refundMoneySum,0))>={0}", minAmount);
                    strSql.AppendFormat(" and (A.prePaidSum-ISNULL(A.refundMoneySum,0))<{0}", maxAmount);
                    strSql.Append(" and A.isPaid=1 and isnull(A.isShopConfirmed,0)=0");
                    strSql.AppendFormat(" and  A.prePayTime between '{0}' and '{1}'", strTime, endTime);
                    if (cityId > 0)
                    {
                        strSql.AppendFormat(" and  B.cityId={0} ", cityId);
                    }
                    strSql.Append(" group by H.cityName, B.isHandle,B.shopID,B.shopName,CONVERT(varchar(100), A.prePayTime, 111),C.EmployeeFirstName");
                    strSql.Append(" order by H.cityName, B.isHandle,B.shopID,C.EmployeeFirstName ,CONVERT(varchar(100), A.prePayTime, 111)");
                    break;
            }
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), null);
            return ds.Tables[0];
        }
        /// <summary>
        /// 查询所有上线公司编号
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public DataTable GetShopId(int cityId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(" select shopID,isnull(isHandle,0) isHandle from ShopInfo where shopStatus>0");
            if (cityId > 0)
            {
                strSql.AppendFormat("  and cityId={0} ", cityId);
            }
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), null);
            return ds.Tables[0];
        }

        public DataTable GetUserStatistics(string date)
        {
            string strTime = date + " 00:00:00";
            string endTime = date + " 23:59:59";
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select COUNT (case when A.RegisterDate< '" + endTime + "' then 1 end ) userCount,COUNT (case when A.RegisterDate between '" + strTime + "' and '" + endTime + "' then 1 end) dayAddUserCount from CustomerInfo A");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), null);
            return ds.Tables[0];
        }
        public int GetActiveUserStatistics(string date)
        {
            int count = 0;
            string strTime = date + " 00:00:00";
            string endTime = date + " 23:59:59";
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select COUNT (case when B.loginTime  between '" + strTime + "' and '" + endTime + "' then 1 end) dayActiveUserCount from  CustomerLoginInfo B");
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), null))
            {
                if (dr.Read())
                {
                    count = dr[0] == DBNull.Value ? 0 : Convert.ToInt32(dr[0]);
                }
            }
            return count;
        }
        public int GetTimesOrderQuantity(string date, int companyId, int shopId)
        {
            string strTime = date + " 00:00:00";
            string endTime = date + " 23:59:59";
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select  COUNT( distinct customerId) timesOrderQuantity  from PreOrder19dian");
            strSql.Append(" where customerId in ");
            strSql.Append(" (select customerId from PreOrder19dian");
            strSql.Append(" where preOrderTime between '" + strTime + "' and '" + endTime + "' ");
            if (companyId > 0)
            {
                strSql.AppendFormat("  and companyId={0}", companyId);
                if (shopId > 0)
                {
                    strSql.AppendFormat("  and shopId={0}", shopId);
                }
            }
            strSql.Append(" group by customerId)");
            strSql.Append(" and preOrderTime <'" + strTime + "' ");
            if (companyId > 0)
            {
                strSql.AppendFormat("  and companyId={0}", companyId);
                if (shopId > 0)
                {
                    strSql.AppendFormat("  and shopId={0}", shopId);
                }
            }
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), null);
            if (ds.Tables[0] != null)
            {
                return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }
            else
            {
                return 0;
            }
        }
        public int GetTimesPayCount(string date, int companyId, int shopId)
        {
            string strTime = date + " 00:00:00";
            string endTime = date + " 23:59:59";
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select  COUNT( distinct customerId) timersPayCount  from PreOrder19dian");
            strSql.Append(" where customerId in ");
            strSql.Append(" (select customerId from PreOrder19dian");
            strSql.Append(" where preOrderTime between '" + strTime + "' and '" + endTime + "' and isPaid=1 ");
            if (companyId > 0)
            {
                strSql.AppendFormat("  and companyId={0}", companyId);
                if (shopId > 0)
                {
                    strSql.AppendFormat("  and shopId={0}", shopId);
                }
            }
            strSql.Append(" group by customerId)");
            strSql.Append(" and preOrderTime <'" + strTime + "' and isPaid =1");
            if (companyId > 0)
            {
                strSql.AppendFormat("  and companyId={0}", companyId);
                if (shopId > 0)
                {
                    strSql.AppendFormat("  and shopId={0}", shopId);
                }
            }
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), null);
            if (ds.Tables[0] != null)
            {
                return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }
            else
            {
                return 0;
            }
        }
        public DataTable ComprehensiveStatisticalQuery(DateTime strTime, DateTime endTime, int company, int shopId, int cityId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("  SELECT convert(varchar(10),preOrderTime,120) as orderTime,");
            strSql.Append("  COUNT (A.preOrder19dianId) as 'orderCount', ");
            strSql.Append(" ROUND(ISNULL(SUM(A.preOrderSum),0),2) as 'orderSumAmount', ");
            strSql.Append("  ISNULL(COUNT(case when A.isPaid=1 then 1 end),0) as 'isPaidOrderCount', ");
            strSql.Append("  Convert(decimal(20,2),ISNULL(sum(case when A.isPaid=1 then A.prePaidSum end),0)) as 'isPaidOrderAmount',");
            strSql.Append("  CONVERT(nvarchar(10),convert(decimal(20,2),ISNULL(COUNT(case when A.isPaid=1 then 1 end),0)*100/COUNT (A.preOrder19dianId))) as payRate ,");
            strSql.Append("  count(case when ISNULL(A.refundMoneySum,0)>0 then 1 end) refundOrderCount,");
            strSql.Append("  ROUND(SUM(ISNULL(A.refundMoneySum,0)),2) refundOrderAmount ");
            strSql.Append("  from PreOrder19dian A inner join ShopInfo B on A.shopId=B.shopID  ");
            strSql.AppendFormat("  where preOrderTime between '{0}' and '{1}'  ", strTime, endTime);
            if (cityId > 0)
            {
                strSql.AppendFormat(" and B.cityId='{0}'", cityId);
            }
            if (company != 0)//表示默认选中的是所有门店
            {
                strSql.AppendFormat(" and A.companyId='{0}'", company);
                if (shopId != 0)
                {
                    strSql.AppendFormat(" and A.shopId='{0}'", shopId);
                }
            }
            strSql.Append("  group by convert(varchar(10),A.preOrderTime,120) ");
            strSql.Append("  order by convert(varchar(10),A.preOrderTime,120)");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), null);
            return ds.Tables[0];
        }
        public DataTable ComprehensiveStatisticalQueryDetail(string strTime, string endTime, int company, int shopId, int cityId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("  SELECT convert(varchar(10),preOrderTime,120) as orderTime,");
            strSql.Append("  COUNT (A.preOrder19dianId) as 'orderCount', ");
            strSql.Append(" ROUND(ISNULL(SUM(A.preOrderSum),0),2) as 'orderSumAmount', ");
            strSql.Append("  ISNULL(COUNT(case when A.isPaid=1 then 1 end),0) as 'isPaidOrderCount', ");
            strSql.Append("  Convert(decimal(20,2),ISNULL(sum(case when A.isPaid=1 then A.prePaidSum end),0)) as 'isPaidOrderAmount',");
            strSql.Append("  CONVERT(nvarchar(10),convert(decimal(20,2),ISNULL(COUNT(case when A.isPaid=1 then 1 end),0)*100/COUNT (A.preOrder19dianId))) as payRate ,");
            strSql.Append(@" (select count( C.preOrder19dianId) from PreOrder19dian C inner join ShopInfo S1 on C.shopId=S1.shopID where C.customerId in
(SELECT DISTINCT B.customerId from PreOrder19dian B inner join ShopInfo S2 on B.shopId=S2.shopID  where B.preOrderTime < convert(varchar(10),A.preOrderTime,120)+' 00:00:00'");
            if (cityId > 0)
            {
                strSql.AppendFormat(" and S2.cityId={0}", cityId);
            }
            strSql.Append(@")
AND C.preOrderTime between convert(varchar(10),A.preOrderTime,120 )+' 00:00:00' 
 and convert(varchar(10),A.preOrderTime,120)+' 23:59:59' ");
            if (cityId > 0)
            {
                strSql.AppendFormat(" and S1.cityId={0}", cityId);
            }
            if (company != 0)//表示默认选中的是所有门店
            {
                strSql.AppendFormat(" and C.companyId='{0}'", company);
                if (shopId != 0)
                {
                    strSql.AppendFormat(" and C.shopId='{0}'", shopId);
                }
            }
            strSql.Append(" )  as timesOrderQuantity,");
            strSql.Append(@" '0' as  timersOrderRate,");
            strSql.Append(@" (select count( C.preOrder19dianId) from PreOrder19dian C inner join ShopInfo S1 on C.shopId=S1.shopID where C.customerId in
(SELECT DISTINCT B.customerId from PreOrder19dian B inner join ShopInfo S2 on B.shopId=S2.shopID  where B.preOrderTime < convert(varchar(10),A.preOrderTime,120)+' 00:00:00' and B.isPaid=1");
            if (cityId > 0)
            {
                strSql.AppendFormat(" and S2.cityId={0}", cityId);
            }
            strSql.Append(@")
AND C.preOrderTime between convert(varchar(10),A.preOrderTime,120)+' 00:00:00' 
 and convert(varchar(10),A.preOrderTime,120)+' 23:59:59' and C.isPaid=1 ");
            if (cityId > 0)
            {
                strSql.AppendFormat(" and S1.cityId={0}", cityId);
            }
            if (company != 0)//表示默认选中的是所有门店
            {
                strSql.AppendFormat(" and C.companyId='{0}'", company);
                if (shopId != 0)
                {
                    strSql.AppendFormat(" and C.shopId='{0}'", shopId);
                }
            }
            strSql.Append(" )  as timersPayCount, ");
            strSql.Append(@" '0' as timersPayRate ");
            strSql.Append("  from PreOrder19dian A inner join ShopInfo B on A.shopId=B.shopID  ");
            strSql.AppendFormat("  where preOrderTime between '{0}' and '{1}'  ", strTime, endTime);
            if (cityId > 0)
            {
                strSql.AppendFormat(" and B.cityId={0}", cityId);
            }
            if (company != 0)//表示默认选中的是所有门店
            {
                strSql.AppendFormat(" and A.companyId='{0}'", company);
                if (shopId != 0)
                {
                    strSql.AppendFormat(" and A.shopId='{0}'", shopId);
                }
            }
            strSql.Append("  group by convert(varchar(10),A.preOrderTime,120) ");
            strSql.Append("  order by convert(varchar(10),A.preOrderTime,120)");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), null);
            return ds.Tables[0];
        }
        public DataTable GetActityUser(DateTime strTime, DateTime endTime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@" select COUNT (case when B.loginTime between '{0}' and '{1}' then 1 end) 
 dayActiveUserCount ,convert(varchar(10),B.loginTime,120) as loginTime, COUNT(distinct customerId) as dayActiveCustomerCount from  CustomerLoginInfo B
 where  B.loginTime between '{0}' and '{1}' 
 group by convert(varchar(10),B.loginTime,120)
 order by convert(varchar(10),B.loginTime,120) desc", strTime, endTime);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), null);
            return ds.Tables[0];
        }
        /// <summary>
        /// 查询当前时间段累计活跃用户数量
        /// </summary>
        /// <param name="strTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public int GetActityUserCount(DateTime strTime, DateTime endTime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"select COUNT(a) as b from (SELECT COUNT(customerId) as a
FROM CustomerLoginInfo where loginTime  between '{0}' and '{1}' group by customerId) temp", strTime, endTime);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString()))
            {
                if (dr.Read())
                {
                    return Convert.ToInt32(SqlHelper.ConvertDbNullValue(dr[0]));
                }
            }
            return 0;
        }
        #region 后台综合统计注释代码保留
        //        public List<ComprehensiveStatistics> GetData_One()
        //        {
        //            string strSql = @" SELECT convert(varchar(10),preOrderTime,120) as orderTime,
        // COUNT (A.preOrder19dianId) as 'orderCount', 
        // ROUND(ISNULL(SUM(A.preOrderSum),0),2) as 'orderSumAmount', 
        // ISNULL(COUNT(case when A.isPaid=1 then 1 end),0) as 'isPaidOrderCount', 
        // Convert(decimal(20,2),ISNULL(sum(case when A.isPaid=1 then A.preOrderServerSum end),0)) as 'isPaidOrderAmount',
        // CONVERT(nvarchar(10),convert(decimal(20,2),ISNULL(COUNT(case when A.isPaid=1 then 1 end),0)*100/COUNT (A.preOrder19dianId))) as payRate ,
        // count(case when ISNULL(A.refundMoneySum,0)>0 then 1 end) refundOrderCount,
        // ROUND(SUM(ISNULL(A.refundMoneySum,0)),2) refundOrderAmount ,
        // 
        // (select COUNT(distinct B.customerId) timesOrderQuantity from PreOrder19dian B where B.customerId in
        // (select C.customerId from PreOrder19dian C where C.preOrderTime  
        // between convert(varchar(10),A.preOrderTime,120)+' 00:00:00' and convert(varchar(10),A.preOrderTime,120)+' 23:59:59')
        // and B.preOrderTime < convert(varchar(10),A.preOrderTime,120)+' 00:00:00')  as timesOrderQuantity,
        //  
        // round((convert(decimal(10,2),(select COUNT(distinct B.customerId) timesOrderQuantity from PreOrder19dian B where B.customerId in
        // (select C.customerId from PreOrder19dian C where C.preOrderTime  
        // between convert(varchar(10),A.preOrderTime,120)+' 00:00:00' and convert(varchar(10),A.preOrderTime,120)+' 23:59:59')
        // and B.preOrderTime < convert(varchar(10),A.preOrderTime,120)+' 00:00:00'))/COUNT (A.preOrder19dianId))*100,2) as  timersOrderRate,
        // 
        // (select COUNT( distinct customerId) timersPayCount from PreOrder19dian B
        // where B.customerId in (select C.customerId from PreOrder19dian C
        // where C.preOrderTime between convert(varchar(10),A.preOrderTime,120)+' 00:00:00' and convert(varchar(10),A.preOrderTime,120)+' 23:59:59' and C.isPaid=1) and B.isPaid=1 
        // and  B.preOrderTime < convert(varchar(10),A.preOrderTime,120)+' 00:00:00' and B.isPaid =1) as timersPayCount,
        // 
        // round((case when ISNULL(COUNT(case when A.isPaid=1 then 1 end),0)=0 then 0 else
        // ((convert(decimal(10,2),(select COUNT( distinct customerId) timersPayCount from PreOrder19dian B
        // where B.customerId in (select C.customerId from PreOrder19dian C
        // where C.preOrderTime between convert(varchar(10),A.preOrderTime,120)+' 00:00:00' and convert(varchar(10),A.preOrderTime,120)+' 23:59:59' and C.isPaid=1) and B.isPaid=1 
        // and  B.preOrderTime < convert(varchar(10),A.preOrderTime,120)+' 00:00:00' and B.isPaid =1))
        // /convert(decimal(10,2),ISNULL(COUNT(case when A.isPaid=1 then 1 end),0)))*100) end),2) as timersPayRate,
        // 
        // 0 as dayAddUserCount,0 as userCount,0 as dayActiveUserCount
        // 
        // from PreOrder19dian A inner join ShopInfo B on A.shopId=B.shopID  
        // 
        // where preOrderTime between '2014/5/1' and '2014/6/9'  
        // and CONVERT(varchar(100),preOrderTime, 108) between '00:00:00' and '23:59:59'
        //  
        // group by convert(varchar(10),A.preOrderTime,120) 
        // order by convert(varchar(10),A.preOrderTime,120)";

        //            List<ComprehensiveStatistics> list = new List<ComprehensiveStatistics>();
        //            using (IDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql))
        //            {
        //                while (dr.Read())
        //                {
        //                    ComprehensiveStatistics model = new ComprehensiveStatistics()
        //                    {
        //                        dayActiveUserCount = Convert.ToInt32(dr["dayActiveUserCount"]),
        //                        dayAddUserCount = Convert.ToInt32(dr["dayAddUserCount"]),
        //                        isPaidOrderAmount = Convert.ToDouble(dr["isPaidOrderAmount"]),
        //                        isPaidOrderCount = Convert.ToInt32(dr["isPaidOrderCount"]),
        //                        orderCount = Convert.ToInt32(dr["orderCount"]),
        //                        orderSumAmount = Convert.ToDouble(dr["orderSumAmount"]),
        //                        orderTime = Convert.ToDateTime(dr["orderTime"]),
        //                        payRate = Convert.ToString(dr["payRate"]),
        //                        refundOrderAmount = Convert.ToDouble(dr["refundOrderAmount"]),
        //                        refundOrderCount = Convert.ToInt32(dr["refundOrderCount"]),
        //                        timersOrderRate = Convert.ToString(dr["timersOrderRate"]),
        //                        timersPayCount = Convert.ToInt32(dr["timersPayCount"]),
        //                        timersPayRate = Convert.ToString(dr["timersPayRate"]),
        //                        timesOrderQuantity = Convert.ToInt32(dr["timesOrderQuantity"]),
        //                        userCount = Convert.ToInt32(dr["userCount"])
        //                    };
        //                    list.Add(model);
        //                }
        //            }
        //            return list;
        //        }
        //        public List<ComprehensiveStatistics> GetData_Two(DateTime strTime, DateTime endTime, string strHour, string endHour)
        //        {
        //            string strSql = @"  select convert(varchar(10),A.RegisterDate,120)  as  orderTime,
        // (select COUNT (case when A.RegisterDate < convert(varchar(10),A.RegisterDate,120)+' 23:59:59' then 1 end )) dayAddUserCount,
        // (SELECT COUNT(1) FROM CustomerInfo B WHERE B.RegisterDate< convert(varchar(10),A.RegisterDate,120)+' 23:59:59') userCount
        // ,0 as orderCount,0 as  orderSumAmount,0 as  isPaidOrderCount,0 as  isPaidOrderAmount,0 as  payRate ,
        // 0 as  refundOrderCount,0 as refundOrderAmount ,0 as  timesOrderQuantity,0 as  timersOrderRate,
        // 0 as timersPayCount,0 as timersPayRate,0 as dayActiveUserCount
        // from CustomerInfo A
        // where A.RegisterDate between '2014/5/1' and '2014/6/9'  
        // and CONVERT(varchar(100),A.RegisterDate, 108) between "+strHour+" and "+endHour+"
        // group by convert(varchar(10),A.RegisterDate,120)
        // order by convert(varchar(10),A.RegisterDate,120)";

        //            List<ComprehensiveStatistics> list = new List<ComprehensiveStatistics>();
        //            using (IDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql))
        //            {
        //                while (dr.Read())
        //                {
        //                    ComprehensiveStatistics model = new ComprehensiveStatistics()
        //                    {
        //                        dayActiveUserCount = Convert.ToInt32(dr["dayActiveUserCount"]),
        //                        dayAddUserCount = Convert.ToInt32(dr["dayAddUserCount"]),
        //                        isPaidOrderAmount = Convert.ToDouble(dr["isPaidOrderAmount"]),
        //                        isPaidOrderCount = Convert.ToInt32(dr["isPaidOrderCount"]),
        //                        orderCount = Convert.ToInt32(dr["orderCount"]),
        //                        orderSumAmount = Convert.ToDouble(dr["orderSumAmount"]),
        //                        orderTime = Convert.ToDateTime(dr["orderTime"]),
        //                        payRate = Convert.ToString(dr["payRate"]),
        //                        refundOrderAmount = Convert.ToDouble(dr["refundOrderAmount"]),
        //                        refundOrderCount = Convert.ToInt32(dr["refundOrderCount"]),
        //                        timersOrderRate = Convert.ToString(dr["timersOrderRate"]),
        //                        timersPayCount = Convert.ToInt32(dr["timersPayCount"]),
        //                        timersPayRate = Convert.ToString(dr["timersPayRate"]),
        //                        timesOrderQuantity = Convert.ToInt32(dr["timesOrderQuantity"]),
        //                        userCount = Convert.ToInt32(dr["userCount"])
        //                    };
        //                    list.Add(model);
        //                }
        //            }
        //            return list;
        //        }
        //        public List<ComprehensiveStatistics> GetData_Three()
        //        {
        //            string strSql = @"  select COUNT (case when B.loginTime between '2014/5/1 00:00:00' and '2014/6/9 23:59:59' then 1 end) 
        // dayActiveUserCount ,convert(varchar(10),B.loginTime,120) as orderTime ,
        // 0 as orderCount,0 as  orderSumAmount,0 as  isPaidOrderCount,0 as  isPaidOrderAmount,0 as  payRate ,
        // 0 as  refundOrderCount,0 as refundOrderAmount ,0 as  timesOrderQuantity,0 as  timersOrderRate,
        // 0 as timersPayCount,0 as timersPayRate,0 as dayAddUserCount,0 as userCount
        // from  CustomerLoginInfo B
        // where B.loginTime between '2014/5/1' and '2014/6/9'  
        // and CONVERT(varchar(100),B.loginTime, 108) between '00:00:00' and '23:59:59'
        // group by convert(varchar(10),B.loginTime,120)
        // order by convert(varchar(10),B.loginTime,120)";

        //            List<ComprehensiveStatistics> list = new List<ComprehensiveStatistics>();
        //            using (IDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql))
        //            {
        //                while (dr.Read())
        //                {
        //                    ComprehensiveStatistics model = new ComprehensiveStatistics()
        //                    {
        //                        dayActiveUserCount = Convert.ToInt32(dr["dayActiveUserCount"]),
        //                        dayAddUserCount = Convert.ToInt32(dr["dayAddUserCount"]),
        //                        isPaidOrderAmount = Convert.ToDouble(dr["isPaidOrderAmount"]),
        //                        isPaidOrderCount = Convert.ToInt32(dr["isPaidOrderCount"]),
        //                        orderCount = Convert.ToInt32(dr["orderCount"]),
        //                        orderSumAmount = Convert.ToDouble(dr["orderSumAmount"]),
        //                        orderTime = Convert.ToDateTime(dr["orderTime"]),
        //                        payRate = Convert.ToString(dr["payRate"]),
        //                        refundOrderAmount = Convert.ToDouble(dr["refundOrderAmount"]),
        //                        refundOrderCount = Convert.ToInt32(dr["refundOrderCount"]),
        //                        timersOrderRate = Convert.ToString(dr["timersOrderRate"]),
        //                        timersPayCount = Convert.ToInt32(dr["timersPayCount"]),
        //                        timersPayRate = Convert.ToString(dr["timersPayRate"]),
        //                        timesOrderQuantity = Convert.ToInt32(dr["timesOrderQuantity"]),
        //                        userCount = Convert.ToInt32(dr["userCount"])
        //                    };
        //                    list.Add(model);
        //                }
        //            }
        //            return list;
        //        }
        //public class ComprehensiveStatistics
        //{
        //    public DateTime orderTime { get; set; }
        //    public int dayAddUserCount { get; set; }
        //    public int userCount { get; set; }
        //    public int dayActiveUserCount { get; set; }
        //    public int orderCount { get; set; }
        //    public double orderSumAmount { get; set; }
        //    public int timesOrderQuantity { get; set; }
        //    public string timersOrderRate { get; set; }
        //    public int isPaidOrderCount { get; set; }
        //    public int timersPayCount { get; set; }
        //    public string timersPayRate { get; set; }
        //    public string payRate { get; set; }
        //    public double isPaidOrderAmount { get; set; }
        //    public int refundOrderCount { get; set; }
        //    public double refundOrderAmount { get; set; }


        //    public override bool Equals(object obj)
        //    {
        //        if (ReferenceEquals(this, obj))
        //            return true;
        //        if (obj == null)
        //            return false;
        //        ComprehensiveStatistics other = obj as ComprehensiveStatistics;
        //        if ((object)other == null)
        //            return false;
        //        return this.orderTime.Equals(other.orderTime);
        //    }

        //    public override int GetHashCode()
        //    {
        //        return this.orderTime.GetHashCode();
        //    }
        //} 
        #endregion

        public DataTable GetCustomerExpenseRecord(string phone, string userName)
        {
            string strSql = @"SELECT C.shopName,A.changeValue,A.changeTime,cast(A.remainMoney as numeric(38, 2)) remainMoney,
(case when A.accountType=1 then '支付宝支付充值' 
 when A.accountType=2 then '银联支付充值' 
 when A.accountType=4 or A.accountType=3 then C.shopName+'退款' 
 when A.accountType=7 then C.shopName+'消费' 
 when A.accountType=9 then '邀请用户奖励'
 when A.accountType=10 then '每日登录奖励'
 when A.accountType=11 then '用户注册'
 when A.accountType=12 then '绑定手机奖励'
 when A.accountType=14 then '微信支付充值'
 when A.accountType=15 then '帐户充值' else
'平台充值赠送' end ) as decription
FROM Money19dianDetail A
inner join CustomerInfo B on A.customerId=B.CustomerID
left  join ShopInfo C on C.shopID=A.shopId where 1=1 ";
            string appendStrSql = string.Empty;
            if (!String.IsNullOrWhiteSpace(phone))
            {
                appendStrSql = String.Format(@" and B.mobilePhoneNumber='{0}'", phone);
            }
            if (!String.IsNullOrWhiteSpace(userName))
            {
                appendStrSql = String.Format(@" and B.UserName='{0}'", userName);
            }
            strSql = strSql + appendStrSql + " order by A.changeTime desc";
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), null);
            return ds.Tables[0];
        }

        public double[] GetGetCustomerMoneyRecord(double initMoney, string initDate, string strTime, string endTime)
        {
            string initDateTemp = Convert.ToDateTime(initDate) < Convert.ToDateTime(strTime) ? strTime : initDate;
            double[] doubleResult = new double[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
            //            string strSql1 = String.Format(@"select round(SUM(case when accountType in (11,16)  then changeValue else 0 end),2) '本期活动赠送金额'
            //, round(SUM(case when accountType=7  then changeValue else 0 end),2) '本期账户余额支付金额'
            //, round(SUM(case when accountType in (1,2,3,4,14,15)  then changeValue else 0 end),2) '本期账户收款金额'
            //from Money19dianDetail 
            //where changeTime between '{0}' and '{1}' and customerId not in (select customerId from CustomerInfo
            //where isnull(mobilePhoneNumber,'')='');", initDateTemp, endTime);
            string strSql1 = String.Format(@"select round(SUM(case when accountType in (11,16)  then changeValue else 0 end),2) '本期活动赠送金额'
, round(SUM(case when accountType=7  then changeValue else 0 end),2) '本期账户余额支付金额'
, round(SUM(case when accountType in (15)  then changeValue else 0 end),2) '本期账户充值收款金额'
, round(SUM(case when accountType in (1)  then changeValue else 0 end),2) '本期账户支付宝收款金额'
, round(SUM(case when accountType in (14)  then changeValue else 0 end),2) '本期账户微信收款金额'
, round(SUM(case when accountType in (3,4)  then changeValue else 0 end),2) '本期退款金额'
from Money19dianDetail 
where changeTime between '{0}' and '{1}' and customerId not in (select customerId from CustomerInfo
where isnull(mobilePhoneNumber,'')='');", initDateTemp, endTime);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql1))
            {
                if (dr.Read())
                {
                    doubleResult[0] = !String.IsNullOrWhiteSpace(SqlHelper.ConvertDbNullValue(dr[0])) ? Convert.ToDouble(SqlHelper.ConvertDbNullValue(dr[0])) : 0;//本期活动赠送金额
                    doubleResult[1] = !String.IsNullOrWhiteSpace(SqlHelper.ConvertDbNullValue(dr[1])) ? Convert.ToDouble(SqlHelper.ConvertDbNullValue(dr[1])) : 0;//本期账户余额支付金额
                    doubleResult[2] = !String.IsNullOrWhiteSpace(SqlHelper.ConvertDbNullValue(dr[2])) ? Convert.ToDouble(SqlHelper.ConvertDbNullValue(dr[2])) : 0;//本期账户充值收款金额
                    doubleResult[3] = !String.IsNullOrWhiteSpace(SqlHelper.ConvertDbNullValue(dr[3])) ? Convert.ToDouble(SqlHelper.ConvertDbNullValue(dr[3])) : 0;//本期账户支付宝收款金额
                    doubleResult[4] = !String.IsNullOrWhiteSpace(SqlHelper.ConvertDbNullValue(dr[4])) ? Convert.ToDouble(SqlHelper.ConvertDbNullValue(dr[4])) : 0;//本期账户财富通收款金额
                    doubleResult[5] = !String.IsNullOrWhiteSpace(SqlHelper.ConvertDbNullValue(dr[5])) ? Convert.ToDouble(SqlHelper.ConvertDbNullValue(dr[5])) : 0;//本期退款金额
                }
            }

            string strSql2 = String.Format(@"select  round(isnull(SUM(changeValue),0) + {0},2)
from Money19dianDetail
where changeTime between '{1}' and  '{2}' and customerId not in (select customerId from CustomerInfo
where isnull(mobilePhoneNumber,'')='');", initMoney, initDate, strTime);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql2))
            {
                if (dr.Read())
                {
                    doubleResult[6] = String.IsNullOrEmpty(SqlHelper.ConvertDbNullValue(dr[0])) ? 0 : Convert.ToDouble(SqlHelper.ConvertDbNullValue(dr[0]));//期初余额
                }
            }

            string strSql3 = String.Format(@"select round(isnull(SUM(changeValue),0) + {0},2)
from Money19dianDetail
where changeTime between '{1}' and  '{2}' and customerId not in (select customerId from CustomerInfo
where isnull(mobilePhoneNumber,'')='');", initMoney, initDate, endTime);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql3))
            {
                if (dr.Read())
                {
                    doubleResult[7] = String.IsNullOrEmpty(SqlHelper.ConvertDbNullValue(dr[0])) ? 0 : Convert.ToDouble(SqlHelper.ConvertDbNullValue(dr[0]));//期末余额
                }
            }
            return doubleResult;
        }

        public DataTable GetWeekTempTableBigData(int year)
        {
            string strSql = String.Format(@"
      SELECT [dataYear]
      ,[dataWeek] as monthweek
      ,[orderCount]
      ,CAST([orderAmount] as decimal(38,2)) as orderAmount
      ,[payOrderCount]
      ,CAST([payOrderAmount] as decimal(38,2)) as payOrderAmount
      ,[refundCount]
      ,CAST([refundAmount] as decimal(38,2)) as refundAmount
      ,[activeUserCount]
      ,[addUserCount]
      ,[totalUserCount]
      ,[twiceUserCount]
      ,[twiceOrderCount]
  FROM [VAGastromistStatisticsStatements].[dbo].[BigDataWeekStatistics] where dataYear = {0}", year);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.StatisticsStatementsConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        public DataTable GetWeekCurrectTableBigData(int year, DateTime strTime, DateTime endTime)
        {
            //查询当前时间周数据信息
            string strSql = String.Format(@"
select DATEPART(year,P.preOrderTime) as dataYear,
DATEPART(week,P.preOrderTime) as monthweek,
COUNT(P.preOrder19dianId) as orderCount,
CAST(SUM(P.preOrderSum) as decimal(38,2)) as orderAmount,
sum(case when P.isPaid=1 then 1 else 0 end) as payOrderCount,
CAST(SUM(case when P.isPaid=1 then P.prePaidSum else 0 end) as decimal(38,2)) as payOrderAmount,
sum(case when ISNULL(P.refundMoneySum,0)>0 then 1 else 0 end) as refundCount,
CAST(SUM(ISNULL(P.refundMoneySum,0)) as decimal(38,2)) as refundAmount,

(select COUNT(distinct B.customerId) from CustomerLoginInfo B where DATEPART(week,B.loginTime)=DATEPART(week,P.preOrderTime) and DATEPART(year,B.loginTime)={0}) as activeUserCount, 
(select COUNT(B.CustomerID) from CustomerInfo B where DATEPART(week,B.RegisterDate)=DATEPART(week,P.preOrderTime) and DATEPART(year,B.RegisterDate)={0}) as addUserCount,
(select COUNT(distinct B.customerId) from CustomerInfo B where ((DATEPART(year,B.RegisterDate)<{0}) or (DATEPART(year,B.RegisterDate)={0} and DATEPART(week,B.RegisterDate)<=DATEPART(week,P.preOrderTime)))) as totalUserCount,

(select (select count(distinct C.customerId) from PreOrder19dian C where C.customerId in
(SELECT DISTINCT B.customerId from PreOrder19dian B where B.preOrderTime < '{1}' and B.isPaid=1)
AND C.preOrderTime between '{1}' and '{2}' and C.isPaid=1 )
+
COUNT(distinct customerId) from (select Count(C.preOrder19dianId) a,C.customerId from PreOrder19dian C where C.customerId not in
(SELECT DISTINCT B.customerId from PreOrder19dian B where B.preOrderTime < '{1}' and B.isPaid=1)
AND C.preOrderTime between '{1}' and '{2}' and C.isPaid=1 group by C.customerId
having Count(C.preOrder19dianId)>1) as temp) twiceUserCount,

(select (select count(C.preOrder19dianId) from PreOrder19dian C where C.customerId in
(SELECT DISTINCT B.customerId from PreOrder19dian B where B.preOrderTime < '{1}' and B.isPaid=1)
AND C.preOrderTime between '{1}' and '{2}' and C.isPaid=1 )
+
isnull(Sum(a),0)-isnull(COUNT(distinct customerId),0) from (select Count(C.preOrder19dianId) a,C.customerId from PreOrder19dian C where C.customerId not in
(SELECT DISTINCT B.customerId from PreOrder19dian B where B.preOrderTime < '{1}' and B.isPaid=1)
AND C.preOrderTime between '{1}' and '{2}' and C.isPaid=1 group by C.customerId
having Count(C.preOrder19dianId)>1) as temp) twiceOrderCount

from PreOrder19dian as P
where DATEPART (week,P.preOrderTime)=DATEPART (week,GETDATE()) and DATEPART (year,P.preOrderTime)={0}
group by DATEPART (week,P.preOrderTime),DATEPART(year,P.preOrderTime)
order by DATEPART (week,P.preOrderTime) asc", year, strTime, endTime);
            //(select COUNT(distinct C.customerId) from PreOrder19dian C where C.customerId in
            //(SELECT DISTINCT B.customerId from PreOrder19dian B where ((DATEPART(year,B.preOrderTime)<{0}) or(DATEPART(year,B.preOrderTime)={0} and DATEPART(week,B.preOrderTime) < DATEPART(week,P.preOrderTime)) and B.isPaid=1)
            //AND DATEPART(year,C.preOrderTime)={0} and DATEPART(week,C.preOrderTime)=DATEPART(week,P.preOrderTime) and C.isPaid=1)) as twiceUserCount,

            //(select COUNT(C.preOrder19dianId) from PreOrder19dian C where C.customerId in
            //(SELECT DISTINCT B.customerId from PreOrder19dian B where (DATEPART(year,B.preOrderTime)<{0} or(DATEPART(year,B.preOrderTime)={0} and DATEPART(week,B.preOrderTime) < DATEPART(week,P.preOrderTime)) and B.isPaid=1)
            //AND DATEPART(year,C.preOrderTime)={0} and DATEPART(week,C.preOrderTime)=DATEPART(week,P.preOrderTime)) and C.isPaid=1) as twiceOrderCount
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        public bool InsertWeekTempTableBigData(int year, int week, DateTime strTime, DateTime endTime)
        {
            //批量插入上周数据，多数据库操作
            string strSql = String.Format(@"insert into [VAGastromistStatisticsStatements].[dbo].[BigDataWeekStatistics](
       [dataYear]
      ,[dataWeek]
      ,[orderCount]
      ,[orderAmount]
      ,[payOrderCount]
      ,[payOrderAmount]
      ,[refundCount]
      ,[refundAmount]
      ,[activeUserCount]
      ,[addUserCount]
      ,[totalUserCount]
      ,[twiceUserCount]
      ,[twiceOrderCount]) 
select DATEPART(year,P.preOrderTime) as dataYear,
DATEPART(week,P.preOrderTime) as monthweek,
COUNT(P.preOrder19dianId) as orderCount,
CAST(SUM(P.preOrderSum) as decimal(38,2)) as orderAmount,
sum(case when P.isPaid=1 then 1 else 0 end) as payOrderCount,
CAST(SUM(case when P.isPaid=1 then P.prePaidSum else 0 end) as decimal(38,2)) as payOrderAmount,
sum(case when ISNULL(P.refundMoneySum,0)>0 then 1 else 0 end) as refundCount,
CAST(SUM(ISNULL(P.refundMoneySum,0)) as decimal(38,2)) as refundAmount,

(select COUNT(distinct B.customerId) from [VAGastronomistMobileApp].[dbo].CustomerLoginInfo B where DATEPART(week,B.loginTime)=DATEPART(week,P.preOrderTime) and DATEPART(year,B.loginTime)={0}) as activeUserCount, 
(select COUNT(B.CustomerID) from [VAGastronomistMobileApp].[dbo].CustomerInfo B where DATEPART(week,B.RegisterDate)=DATEPART(week,P.preOrderTime) and DATEPART(year,B.RegisterDate)={0}) as addUserCount,
(select COUNT(distinct B.customerId) from [VAGastronomistMobileApp].[dbo].CustomerInfo B where ((DATEPART(year,B.RegisterDate)<{0}) or (DATEPART(year,B.RegisterDate)={0} and DATEPART(week,B.RegisterDate)<=DATEPART(week,P.preOrderTime)))) as totalUserCount,

(select (select count(distinct C.customerId) from [VAGastronomistMobileApp].[dbo].PreOrder19dian C where C.customerId in
(SELECT DISTINCT B.customerId from [VAGastronomistMobileApp].[dbo].PreOrder19dian B where B.preOrderTime < '{1}' and B.isPaid=1)
AND C.preOrderTime between '{1}' and '{2}' and C.isPaid=1 )
+
COUNT(distinct customerId) from (select Count(C.preOrder19dianId) a,C.customerId from [VAGastronomistMobileApp].[dbo].PreOrder19dian C where C.customerId not in
(SELECT DISTINCT B.customerId from [VAGastronomistMobileApp].[dbo].PreOrder19dian B where B.preOrderTime < '{1}' and B.isPaid=1)
AND C.preOrderTime between '{1}' and '{2}' and C.isPaid=1 group by C.customerId
having Count(C.preOrder19dianId)>1) as temp) twiceUserCount,

(select (select count(C.preOrder19dianId) from [VAGastronomistMobileApp].[dbo].PreOrder19dian C where C.customerId in
(SELECT DISTINCT B.customerId from [VAGastronomistMobileApp].[dbo].PreOrder19dian B where B.preOrderTime < '{1}' and B.isPaid=1)
AND C.preOrderTime between '{1}' and '{2}' and C.isPaid=1 )
+
isnull(Sum(a),0)-isnull(COUNT(distinct customerId),0) from (select Count(C.preOrder19dianId) a,C.customerId from [VAGastronomistMobileApp].[dbo].PreOrder19dian C where C.customerId not in
(SELECT DISTINCT B.customerId from [VAGastronomistMobileApp].[dbo].PreOrder19dian B where B.preOrderTime < '{1}' and B.isPaid=1)
AND C.preOrderTime between '{1}' and '{2}' and C.isPaid=1 group by C.customerId
having Count(C.preOrder19dianId)>1) as temp) twiceOrderCount

from [VAGastronomistMobileApp].[dbo].PreOrder19dian as P
where DATEPART (year,P.preOrderTime)={0} and DATEPART (week,P.preOrderTime)={3}
group by DATEPART (week,P.preOrderTime),DATEPART(year,P.preOrderTime)
order by DATEPART (week,P.preOrderTime) asc", year, strTime, endTime, week);
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.StatisticsStatementsConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return rows > 0;
        }
        /// <summary>
        /// 月数据统计
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public DataTable GetMonthTempTableBigData(int year)
        {
            //查询报表统计数据表
            string strSql = String.Format(@"SELECT 
       [dataYear]
      ,[dataMonth] as monthweek
      ,[orderCount]
      ,CAST([orderAmount] as decimal(38,2)) as orderAmount
      ,[payOrderCount]
      ,CAST([payOrderAmount] as decimal(38,2)) as payOrderAmount
      ,[refundCount]
      ,CAST([refundAmount] as decimal(38,2)) as refundAmount
      ,[activeUserCount]
      ,[addUserCount]
      ,[totalUserCount]
      ,[twiceUserCount]
      ,[twiceOrderCount]
  FROM [VAGastromistStatisticsStatements].[dbo].[BigDataMonthStatistics]  where dataYear = {0}", year);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.StatisticsStatementsConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        public DataTable GetMonthCurrectTableBigData(int year, DateTime strTime, DateTime endTime)
        {
            //查询当前时间月数据信息 
            string strSql = String.Format(@"
select DATEPART(year,P.preOrderTime) as dataYear,
DATEPART(MONTH,P.preOrderTime) as monthweek,
COUNT(P.preOrder19dianId) as orderCount,
CAST(SUM(P.preOrderSum) as decimal(38,2)) as orderAmount,
sum(case when P.isPaid=1 then 1 else 0 end) as payOrderCount,
CAST(SUM(case when P.isPaid=1 then P.prePaidSum else 0 end) as decimal(38,2)) as payOrderAmount,
sum(case when ISNULL(P.refundMoneySum,0)>0 then 1 else 0 end) as refundCount,
CAST(SUM(ISNULL(P.refundMoneySum,0)) as decimal(38,2)) as refundAmount,

(select COUNT(distinct B.customerId) from CustomerLoginInfo B where DATEPART(MONTH,B.loginTime)=DATEPART(MONTH,P.preOrderTime) and DATEPART(year,B.loginTime)={0}) as activeUserCount, 
(select COUNT(B.CustomerID) from CustomerInfo B where DATEPART(MONTH,B.RegisterDate)=DATEPART(MONTH,P.preOrderTime) and DATEPART(year,B.RegisterDate)={0}) as addUserCount,
(select COUNT(distinct B.customerId) from CustomerInfo B where ((DATEPART(year,B.RegisterDate)<{0}) or (DATEPART(year,B.RegisterDate)={0} and DATEPART(MONTH,B.RegisterDate)<=DATEPART(MONTH,P.preOrderTime)))) as totalUserCount,

(select (select count(distinct C.customerId) from PreOrder19dian C where C.customerId in
(SELECT DISTINCT B.customerId from PreOrder19dian B where B.preOrderTime < '{1}' and B.isPaid=1)
AND C.preOrderTime between '{1}' and '{2}' and C.isPaid=1 )
+
COUNT(distinct customerId) from (select Count(C.preOrder19dianId) a,C.customerId from PreOrder19dian C where C.customerId not in
(SELECT DISTINCT B.customerId from PreOrder19dian B where B.preOrderTime < '{1}' and B.isPaid=1)
AND C.preOrderTime between '{1}' and '{2}' and C.isPaid=1 group by C.customerId
having Count(C.preOrder19dianId)>1) as temp) twiceUserCount,

(select (select count(C.preOrder19dianId) from PreOrder19dian C where C.customerId in
(SELECT DISTINCT B.customerId from PreOrder19dian B where B.preOrderTime < '{1}' and B.isPaid=1)
AND C.preOrderTime between '{1}' and '{2}' and C.isPaid=1 )
+
isnull(Sum(a),0)-isnull(COUNT(distinct customerId),0) from (select Count(C.preOrder19dianId) a,C.customerId from PreOrder19dian C where C.customerId not in
(SELECT DISTINCT B.customerId from PreOrder19dian B where B.preOrderTime < '{1}' and B.isPaid=1)
AND C.preOrderTime between '{1}' and '{2}' and C.isPaid=1 group by C.customerId
having Count(C.preOrder19dianId)>1) as temp) twiceOrderCount

from PreOrder19dian as P
where DATEPART (MONTH,P.preOrderTime)=DATEPART (MONTH,GETDATE()) and DATEPART (year,P.preOrderTime)={0}
group by DATEPART (MONTH,P.preOrderTime),DATEPART(year,P.preOrderTime)
order by DATEPART (MONTH,P.preOrderTime) asc", year, strTime, endTime);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        public bool InsertMonthTempTableBigData(int year, int month, DateTime strTime, DateTime endTime)
        {
            //批量插入上周数据，多数据库操作
            string strSql = String.Format(@"insert into [VAGastromistStatisticsStatements].[dbo].[BigDataMONTHStatistics](
       [dataYear]
      ,[dataMonth]
      ,[orderCount]
      ,[orderAmount]
      ,[payOrderCount]
      ,[payOrderAmount]
      ,[refundCount]
      ,[refundAmount]
      ,[activeUserCount]
      ,[addUserCount]
      ,[totalUserCount]
      ,[twiceUserCount]
      ,[twiceOrderCount]) 
select DATEPART(year,P.preOrderTime) as dataYear,
DATEPART(MONTH,P.preOrderTime) as monthweek,
COUNT(P.preOrder19dianId) as orderCount,
CAST(SUM(P.preOrderSum) as decimal(38,2)) as orderAmount,
sum(case when P.isPaid=1 then 1 else 0 end) as payOrderCount,
CAST(SUM(case when P.isPaid=1 then P.prePaidSum else 0 end) as decimal(38,2)) as payOrderAmount,
sum(case when ISNULL(P.refundMoneySum,0)>0 then 1 else 0 end) as refundCount,
CAST(SUM(ISNULL(P.refundMoneySum,0)) as decimal(38,2)) as refundAmount,

(select COUNT(distinct B.customerId) from [VAGastronomistMobileApp].[dbo].CustomerLoginInfo B where DATEPART(MONTH,B.loginTime)=DATEPART(MONTH,P.preOrderTime) and DATEPART(year,B.loginTime)={0}) as activeUserCount, 
(select COUNT(B.CustomerID) from [VAGastronomistMobileApp].[dbo].CustomerInfo B where DATEPART(MONTH,B.RegisterDate)=DATEPART(MONTH,P.preOrderTime) and DATEPART(year,B.RegisterDate)={0}) as addUserCount,
(select COUNT(distinct B.customerId) from [VAGastronomistMobileApp].[dbo].CustomerInfo B where ((DATEPART(year,B.RegisterDate)<{0}) or (DATEPART(year,B.RegisterDate)={0} and DATEPART(MONTH,B.RegisterDate)<=DATEPART(MONTH,P.preOrderTime)))) as totalUserCount,

(select (select count(distinct C.customerId) from [VAGastronomistMobileApp].[dbo].PreOrder19dian C where C.customerId in
(SELECT DISTINCT B.customerId from [VAGastronomistMobileApp].[dbo].PreOrder19dian B where B.preOrderTime < '{1}' and B.isPaid=1)
AND C.preOrderTime between '{1}' and '{2}' and C.isPaid=1 )
+
COUNT(distinct customerId) from (select Count(C.preOrder19dianId) a,C.customerId from [VAGastronomistMobileApp].[dbo].PreOrder19dian C where C.customerId not in
(SELECT DISTINCT B.customerId from [VAGastronomistMobileApp].[dbo].PreOrder19dian B where B.preOrderTime < '{1}' and B.isPaid=1)
AND C.preOrderTime between '{1}' and '{2}' and C.isPaid=1 group by C.customerId
having Count(C.preOrder19dianId)>1) as temp) twiceUserCount,

(select (select count(C.preOrder19dianId) from [VAGastronomistMobileApp].[dbo].PreOrder19dian C where C.customerId in
(SELECT DISTINCT B.customerId from [VAGastronomistMobileApp].[dbo].PreOrder19dian B where B.preOrderTime < '{1}' and B.isPaid=1)
AND C.preOrderTime between '{1}' and '{2}' and C.isPaid=1 )
+
Sum(a)-COUNT(distinct customerId) from (select Count(C.preOrder19dianId) a,C.customerId from [VAGastronomistMobileApp].[dbo].PreOrder19dian C where C.customerId not in
(SELECT DISTINCT B.customerId from [VAGastronomistMobileApp].[dbo].PreOrder19dian B where B.preOrderTime < '{1}' and B.isPaid=1)
AND C.preOrderTime between '{1}' and '{2}' and C.isPaid=1 group by C.customerId
having Count(C.preOrder19dianId)>1) as temp) twiceOrderCount

from [VAGastronomistMobileApp].[dbo].PreOrder19dian as P
where DATEPART (MONTH,P.preOrderTime)={3} and DATEPART (year,P.preOrderTime)={0}
group by DATEPART (MONTH,P.preOrderTime),DATEPART(year,P.preOrderTime)
order by DATEPART (MONTH,P.preOrderTime) asc", year, strTime, endTime, month);
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.StatisticsStatementsConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return rows > 0;
        }

        public DataTable SelectOrderStatusDetail(string startTime, string endTime, int cityId, int companyId, int shopId)
        {
//            string strSql = String.Format(@"select B.mobilePhoneNumber as OrderPhone,A.preOrder19dianId as OrderNumber,A.prePayTime as OrderTime,C.shopName as OrderShop, 
//round(case when A.isPaid=1 then  isnull(A.prePaidSum,0) else 0 end ,2) as OrderAmount,
//(round(case when A.isPaid=1 then  isnull(A.prePaidSum,0) else 0 end ,2) -
//isnull((select ali.totalFee 
//from  AlipayOrderInfo ali 
//where ali.connId = A.preOrder19dianId  and orderStatus = 2),0)-
//isnull((select top 1 wx.totalFee
//from  WechatPayOrderInfo wx 
//where wx.connId = A.preOrder19dianId  and orderStatus = 2 order by orderPayTime),0)-
//isnull((select sum(currectUsedAmount) consumeRedEnvelopeAmount
//FROM RedEnvelopeConnPreOrder r where r.preOrder19dianId=A.preOrder19dianId),0)) as OrderBalance,
//             
//             isnull((select ali.totalFee 
//             from  AlipayOrderInfo ali 
//             where ali.connId = A.preOrder19dianId  and orderStatus = 2),0) as OrderAli,
//             isnull((select top 1 wx.totalFee
//             from  WechatPayOrderInfo wx 
//             where wx.connId = A.preOrder19dianId  and orderStatus = 2 order by orderPayTime),0) as OrderWechat, 
//             isnull((select sum(currectUsedAmount) consumeRedEnvelopeAmount
//             FROM RedEnvelopeConnPreOrder r where r.preOrder19dianId=A.preOrder19dianId),0) as OrderOther,
//case when A.isShopConfirmed=1 then '已入座' else '未入座' end as ConfrimStatus,
//(select MAX(E.shopConfirmedTime) from PreorderShopConfirmedInfo E where E.preOrder19dianId=A.preOrder19dianId) as ConfrimTime,
//case when A.isShopConfirmed=1 then A.prePaidSum-ISNULL(A.refundMoneySum,0) else 0 end as ConfrimAmount, 
//'0' as ConfrimBalance,'0' as ConfrimWechat,'0' as ConfrimAli,'0' as ConfrimAther, 
//case when A.isApproved=1 then '已结算' else '未结算' end as ApproveStatus,D.checkTime as ApproveTime,
//case when A.isApproved=1 then A.prePaidSum-ISNULL(A.refundMoneySum,0) else 0 end as ApproveAmount,
//ISNULL(A.refundMoneySum,0) RefundAmount                     
//          
//from PreOrder19dian A inner join CustomerInfo B on A.customerId=B.CustomerID
//inner join ShopInfo C on C.shopID=A.shopId
//left join PreorderCheckInfo D on A.preOrder19dianId=D.preOrder19dianId
//where A.isPaid=1 and A.prePayTime between '{0}' and '{1}'", startTime, endTime);
            
            // add by zhujinlei 2015/06/08 优化sql
            string strSql = @"SELECT 
	                            B.mobilePhoneNumber AS OrderPhone, 
	                            A.preOrder19dianId AS OrderNumber, 
	                            A.prePayTime AS OrderTime, 
	                            C.shopName AS OrderShop, 
	                            round(CASE WHEN A.isPaid = 1 THEN isnull(A.prePaidSum, 0) ELSE 0 END, 2) AS OrderAmount
	                            ,isnull((SELECT Amount FROM Preorder19DianLine line1 WHERE line1.preOrder19dianId = A.preOrder19dianId AND PayType=1), 0) AS OrderBalance
	                            ,isnull((SELECT Amount FROM Preorder19DianLine line1 WHERE line1.preOrder19dianId = A.preOrder19dianId AND PayType=2), 0) AS OrderAli
	                            ,isnull((SELECT Amount FROM Preorder19DianLine line1 WHERE line1.preOrder19dianId = A.preOrder19dianId AND PayType=3), 0) AS OrderWechat
	                            ,isnull((SELECT Amount FROM Preorder19DianLine line1 WHERE line1.preOrder19dianId = A.preOrder19dianId AND PayType=6), 0) AS OrderOther
	                            , CASE WHEN A.isShopConfirmed = 1 THEN '已入座' ELSE '未入座' END AS ConfrimStatus
	                            , (SELECT MAX(E.shopConfirmedTime) FROM PreorderShopConfirmedInfo E WHERE E.preOrder19dianId = A.preOrder19dianId) AS ConfrimTime
	                            , CASE WHEN A.isShopConfirmed = 1 THEN A.prePaidSum - ISNULL(A.refundMoneySum, 0) ELSE 0 END AS ConfrimAmount
	                            , '0' AS ConfrimBalance, '0' AS ConfrimWechat, '0' AS ConfrimAli
	                            , '0' AS ConfrimAther
	                            , CASE WHEN A.isApproved = 1 THEN '已结算' ELSE '未结算' END AS ApproveStatus
	                            , D.checkTime AS ApproveTime
	                            , CASE WHEN A.isApproved = 1 THEN A.prePaidSum - ISNULL(A.refundMoneySum, 0) ELSE 0 END AS ApproveAmount
	                            , ISNULL(A.refundMoneySum, 0) AS RefundAmount
                            FROM PreOrder19dian A 
                            INNER JOIN CustomerInfo B ON A.customerId = B.CustomerID 
                            INNER JOIN ShopInfo C ON C.shopID = A.shopId 
                            LEFT JOIN PreorderCheckInfo D ON A.preOrder19dianId = D.preOrder19dianId
                            WHERE A.isPaid = 1
	                              AND A.prePayTime BETWEEN '{0}' and '{1}'";
            strSql = string.Format(strSql, startTime, endTime);
            if (cityId > 0)
            {
                strSql += String.Format(" and C.cityId={0}", cityId);
            }
            if (companyId > 0)
            {
                strSql += String.Format(" and C.companyId={0}", companyId);
            }
            if (shopId > 0)
            {
                strSql += String.Format(" and C.shopId={0}", shopId);
            }
            strSql += " order by A.preOrder19dianId desc";
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
    }
}
