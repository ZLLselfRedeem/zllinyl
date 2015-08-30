﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model.Interface;
using VAGastronomistMobileApp.Model.QueryObject;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public partial class CouponManager
    {
        /// <summary>
        /// 查询当前城市有有效优惠券的门店编号
        /// </summary>
        /// <param name="cityId">城市编号</param>
        /// <returns></returns>
        public List<int> SelectHadCouponShopId(int cityId)
        {
            const string strSql = @"select A.ShopId from Coupon A 
inner join ShopInfo B on A.ShopId=B.shopID
where B.cityID=@cityId and A.EndDate>GETDATE() and A.State=1
and B.shopStatus=1 and B.isHandle=1  and A.IsGot=0  and A.StartDate<GETDATE() and SendCount<SheetNumber";
            SqlParameter parameter = new SqlParameter("@cityId", SqlDbType.Int, 4) { Value = cityId };
            var list = new List<int>();
            using (SqlDataReader drReader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameter))
            {
                while (drReader.Read())
                {
                    list.Add(Convert.ToInt32(drReader[0]));
                }
            }
            return list;
        }

        /// <summary>
        /// 查询当前手机号码用户拥有的可用有效优惠券数量（实际为列表count，但考虑接口效率，单独编写此查询方法（少关联表））
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <returns></returns>
        public int SelectHadCouponCount(string phone)
        {
            const string strSql = @"select COUNT(0) from CouponGetDetail d inner join Coupon c on d.CouponId=c.CouponId
inner join ShopInfo s on c.ShopId=s.shopID and s.isHandle=1
where MobilePhoneNumber=@MobilePhoneNumber and d.State in (1,3) and ValidityEnd>GETDATE()";
            SqlParameter parameter = new SqlParameter("@MobilePhoneNumber", SqlDbType.NVarChar, 50) { Value = phone };
            using (SqlDataReader drReader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameter))
            {
                if (drReader.Read())
                {
                    return Convert.ToInt32(drReader[0]);
                }
            }
            return 0;
        }
        /// <summary>
        /// 检查用户是否有未查看的有效红包
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public bool HaveUnCheckCoupon(string phone)
        {
            const string strSql = @"select count(1) from CouponGetDetail d inner join Coupon c on d.CouponId=c.CouponId
inner join ShopInfo s on c.ShopId=s.shopID and s.isHandle=1
where MobilePhoneNumber=@MobilePhoneNumber and d.State in (1,3) and ValidityEnd>GETDATE() and CheckTime is null";
            SqlParameter parameter = new SqlParameter("@MobilePhoneNumber", SqlDbType.NVarChar, 50) { Value = phone };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql, parameter);
                if (obj == null || Convert.ToInt32(obj) == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// 查询当前点单是否已分享过优惠券
        /// </summary>
        /// <param name="orderId">点单编号</param>
        /// <returns></returns>
        public bool SelectIsHaveShareCoupon(long orderId)
        {
            const string strSql = @"select COUNT(0) from CouponSendDetail
where PreOrder19DianId=@PreOrder19DianId";
            SqlParameter parameter = new SqlParameter("@PreOrder19DianId", SqlDbType.BigInt, 8) { Value = orderId };
            using (SqlDataReader drReader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameter))
            {
                if (drReader.Read())
                {
                    return Convert.ToInt32(drReader[0]) > 0;
                }
            }
            return false;
        }

        /// <summary>
        /// 查询当前用户当前门店可用抵价券列表（支付接口）
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public List<OrderPaymentCouponDetail> SelectShopCouponDetails(string phone, int shopId)
        {
            const string strSql = @"select distinct B.CouponGetDetailID couponId,
                                                    B.RequirementMoney requirementMoney,
                                                    B.DeductibleAmount deductibleAmount,
                                                    B.ValidityEnd couponValidityEnd,
                                                    A.MaxAmount maxAmount
                                        from Coupon A inner join CouponGetDetail B 
                                        on A.couponID=B.CouponId
                                        where B.MobilePhoneNumber=@MobilePhoneNumber and B.ValidityEnd>GETDATE() and B.State in (1,3) and A.ShopId=@shopId";
            var list = new List<OrderPaymentCouponDetail>();
            SqlParameter[] parameter =
            {
                new SqlParameter("@MobilePhoneNumber", SqlDbType.NVarChar, 50) { Value = phone },
                 new SqlParameter("@shopId", SqlDbType.Int, 4) { Value = shopId }
            };
            using (SqlDataReader drReader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameter))
            {
                while (drReader.Read())
                {
                    list.Add(new OrderPaymentCouponDetail()
                    {
                        couponId = Convert.ToInt32(drReader["couponId"]),
                        couponName = String.Format("满{0}减{1}", Convert.ToDouble(drReader["requirementMoney"]), Convert.ToDouble(drReader["deductibleAmount"])),
                        requirementMoney = Convert.ToDouble(drReader["requirementMoney"]),
                        deductibleAmount = Convert.ToDouble(drReader["deductibleAmount"]),
                        couponValidityEnd = ToSecondFrom1970(Convert.ToDateTime(drReader["couponValidityEnd"])),
                        maxAmount = Convert.ToDouble(drReader["maxAmount"]),
                        //beginTime = (TimeSpan)drReader["beginTime"],
                        //endTime = (TimeSpan)drReader["endTime"],
                        isGeneralHolidays = Convert.ToBoolean(drReader["isGeneralHolidays"])
                        //h5UseUrl = "" // 抵扣券规则H5链接,需要和前端交互
                    });
                }
            }
            return list;
        }

        /// <summary>
        /// 查询当前点单抵扣金额
        /// </summary>
        /// <param name="preOrder19dianID"></param>
        /// <returns></returns>
        public double[] SelectCouponDeductibleAmount(long preOrder19dianID)
        {
            //2015-5-27修改DeductibleAmount为RealDeductibleAmount
            const string strSql = @"select A.DeductibleAmount,A.RequirementMoney,A.RealDeductibleAmount,B.MaxAmount
            from CouponGetDetail A 
            inner join Coupon B on A.CouponId=B.CouponId
            where A.PreOrder19DianId=@PreOrder19DianId and A.State=2";
            SqlParameter parameter = new SqlParameter("@PreOrder19DianId", SqlDbType.BigInt, 8) { Value = preOrder19dianID };
            double[] data = new double[4] { 0, 0, 0, 0 };
            using (SqlDataReader drReader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameter))
            {
                if (drReader.Read())
                {
                    data[0] = Convert.ToDouble(drReader[0]);
                    data[1] = Convert.ToDouble(drReader[1]);
                    data[2] = Convert.ToDouble(drReader[2]);
                    data[3] = Convert.ToDouble(drReader[3]);
                }
            }
            return data;
        }

        /// <summary>
        /// 查询当前支付中点单对应使用的优惠券编号
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public int SelecCouponGetDetailIdByOrderId(long orderId)
        {
            const string strSql = @"select A.CouponGetDetailID from CouponGetDetail A 
 where A.PreOrder19DianId=@PreOrder19DianId and A.State<>2";
            SqlParameter parameter = new SqlParameter("@PreOrder19DianId", SqlDbType.BigInt, 8) { Value = orderId };
            using (SqlDataReader drReader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameter))
            {
                if (drReader.Read())
                {
                    return Convert.ToInt32(drReader[0]);
                }
            }
            return 0;
        }

        /// <summary>
        /// 查询当前手机号码用户拥有可用优惠券列表
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="isHaveMore"></param>
        /// <returns></returns>
        public List<ClientCouponPacketDetail> SelectHadCouponDetail(string phone, int pageIndex, int pageSize, int cityId, out bool isHaveMore)
        {
            const string fields = @" A.CouponGetDetailID,C.shopID shopId,C.shopName,C.shopImagePath+C.shopLogo shopLogoUrl,A.CouponGetDetailID couponId, CheckTime,
A.DeductibleAmount couponAmount,A.ValidityEnd couponValidityEnd,
A.RequirementMoney couponRequirementMoney,A.DeductibleAmount couponDeductibleAmount,C.cityId,
case when A.State=2 then 2
when A.State in (1,3) and A.ValidityEnd<GETDATE() then -1
when A.State in (1,3) and (ISNULL(C.isHandle,0)!=1 or C.shopStatus!=1) then -2 
when A.State in (1,3) then 1 end couponSatatus ";
            const string tableName = @"  CouponGetDetail A 
inner join Coupon B on A.CouponId=B.CouponId
inner join ShopInfo C on C.shopID=B.ShopId ";
            string strWhere = String.Format(@" MobilePhoneNumber='{0}' and A.State!=4", phone);

            PageQuery pageQuery = new PageQuery()
            {
                tableName = tableName,
                fields = fields,
                orderField = "A.ValidityEnd desc",
                sqlWhere = strWhere
            };
            Paging paging = new Paging()
            {
                pageIndex = pageIndex,
                pageSize = pageSize,
                recordCount = 0,
                pageCount = 0
            };
            var list = new List<ClientCouponPacketDetail>();
            SqlConnection connection = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction);
            if (connection.State != ConnectionState.Open) connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = connection;
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "CommonPageProc";
            comm.Parameters.Add("@TableName", SqlDbType.VarChar, 5000).Value = pageQuery.tableName;
            comm.Parameters.Add("@Fields", SqlDbType.VarChar, 5000).Value = pageQuery.fields;
            comm.Parameters.Add("@OrderField", SqlDbType.VarChar, 5000).Value = pageQuery.orderField;
            comm.Parameters.Add("@sqlWhere", SqlDbType.VarChar, 5000).Value = pageQuery.sqlWhere;
            comm.Parameters.Add("@pageSize", SqlDbType.Int, 16).Value = paging.pageSize;
            comm.Parameters.Add("@pageIndex", SqlDbType.Int, 16).Value = paging.pageIndex;
            SqlParameter parameter = new SqlParameter("@TotalPage", SqlDbType.Int);
            parameter.Direction = ParameterDirection.Output;
            parameter.Value = paging.pageCount;
            comm.Parameters.Add(parameter);
            SqlParameter param = new SqlParameter("@TotalRecord", SqlDbType.Int);
            param.Direction = ParameterDirection.ReturnValue;
            comm.Parameters.Add(param);
            using (IDataReader drReader = comm.ExecuteReader(CommandBehavior.CloseConnection))
            {
                while (drReader.Read())
                {
                    list.Add(new ClientCouponPacketDetail()
                    {
                        couponGetDetailId = Convert.ToInt32(drReader["CouponGetDetailID"]),
                        shopId = Convert.ToInt32(drReader["shopId"]),
                        shopName = Convert.ToString(drReader["shopName"]),
                        shopLogoUrl = Convert.ToString(drReader["shopLogoUrl"]),
                        couponRequirementMoney = Convert.ToDouble(drReader["couponRequirementMoney"]),
                        couponId = Convert.ToInt64(drReader["couponId"]),
                        couponDeductibleAmount = Convert.ToDouble(drReader["couponDeductibleAmount"]),
                        couponValidityEnd = ToSecondFrom1970(Convert.ToDateTime(drReader["couponValidityEnd"])),
                        couponSatatus = Convert.ToInt32(drReader["couponSatatus"]),
                        CheckTime = Convert.IsDBNull(drReader["CheckTime"]) ? null : Convert.ToDateTime(drReader["CheckTime"]) as DateTime?,
                        cityId = Convert.ToInt32(drReader["cityId"])
                    });
                }
            }
            paging.recordCount = param.Value == DBNull.Value ? 0 : Convert.ToInt32(param.Value);
            isHaveMore = paging.recordCount > (pageSize * pageIndex);
            comm.Parameters.Clear();
            return list;
        }

        /// <summary>
        /// 返回当前抵扣券
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">一页行数</param>
        /// <returns></returns>
        public Tuple<int, IList<ClientDeductionVolumeDBEntity>> GetCurrentRebates(string mobile, int pageIndex, int pageSize)
        {
            string fields = "c.ShopId,c.CityId,c.couponId,c.RequirementMoney,c.DeductibleAmount,cd.ValidityEnd,cd.State,c.ShopName,c.Image,cd.CheckTime,cd.CouponGetDetailID,c.MaxAmount,cd.UseTime";
            string order = "cd.CouponGetDetailID";
            string table = "Coupon c INNER JOIN CouponGetDetail cd ON c.CouponId=cd.CouponId";
            string where = "MobilePhoneNumber=@MobilePhoneNumber AND (cd.State=1 OR cd.State=3) and cd.ValidityEnd>GETDATE()";

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(" * ");
            strSql.Append(" FROM (");
            strSql.Append(" SELECT ");
            strSql.AppendFormat(" {0} ,ROW_NUMBER() OVER(ORDER BY {1}) AS ROW_NUMBER", fields, order);
            strSql.AppendFormat(" FROM {0}", table);
            if (!string.IsNullOrEmpty(where))
                strSql.AppendFormat(" WHERE {0}", where);
            strSql.Append(" ) AS AA");
            strSql.Append(" WHERE ");
            strSql.AppendFormat(" AA.ROW_NUMBER BETWEEN {0} AND {1}", (pageIndex - 1) * pageSize + 1, pageSize * pageIndex);

            SqlParameter[] parameters = { new SqlParameter("@MobilePhoneNumber", mobile) };


            string countSql = "SELECT COUNT(0) FROM CouponGetDetail cd inner join Coupon c ON c.CouponId=cd.CouponId WHERE " + where;
            int count = 0;
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                count = (int)SqlHelper.ExecuteScalar(conn, CommandType.Text, countSql, parameters);


            IList<ClientDeductionVolumeDBEntity> currentList = new List<ClientDeductionVolumeDBEntity>();
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            using (SqlDataReader drea = SqlHelper.ExecuteReader(conn, CommandType.Text, strSql.ToString(), parameters))
            {
                while (drea.Read())
                    currentList.Add(SqlHelper.GetEntity<ClientDeductionVolumeDBEntity>(drea));
            }

            return new Tuple<int, IList<ClientDeductionVolumeDBEntity>>(count, currentList);
        }

        /// <summary>
        /// 返回历史抵扣券
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">一页行数</param>
        /// <returns></returns>
        public Tuple<int, IList<ClientDeductionVolumeDBEntity>> GetHistoryRebates(string mobile, int pageIndex, int pageSize)
        {
            string fields = "c.ShopId,c.CityId,c.couponId,c.RequirementMoney,c.DeductibleAmount,cd.ValidityEnd,cd.State,c.ShopName,c.Image,cd.CheckTime,cd.CouponGetDetailID,c.MaxAmount,cd.UseTime";
            string order = "(CASE WHEN cd.State=2 THEN cd.UseTime ELSE cd.ValidityEnd END) DESC";
            string table = "Coupon c INNER JOIN CouponGetDetail cd ON c.CouponId=cd.CouponId";
            string where = "MobilePhoneNumber=@MobilePhoneNumber AND (cd.State=2 OR (cd.State=1 OR cd.State=3) and cd.ValidityEnd<GETDATE()";

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(" * ");
            strSql.Append(" FROM (");
            strSql.Append(" SELECT ");
            strSql.AppendFormat(" {0} ,ROW_NUMBER() OVER(ORDER BY {1}) AS ROW_NUMBER", fields, order);
            strSql.AppendFormat(" FROM {0}", table);
            if (!string.IsNullOrEmpty(where))
                strSql.AppendFormat(" WHERE {0}", where);
            strSql.Append(" ) AS AA");
            strSql.Append(" WHERE ");
            strSql.AppendFormat(" AA.ROW_NUMBER BETWEEN {0} AND {1}", (pageIndex - 1) * pageSize + 1, pageSize * pageIndex);

            SqlParameter[] parameters = { new SqlParameter("@MobilePhoneNumber", mobile) };


            string countSql = "SELECT COUNT(0) FROM CouponGetDetail cd WHERE " + where;
            int count = 0;
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                count = (int)SqlHelper.ExecuteScalar(conn, CommandType.Text, countSql, parameters);


            IList<ClientDeductionVolumeDBEntity> currentList = new List<ClientDeductionVolumeDBEntity>();
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            using (SqlDataReader drea = SqlHelper.ExecuteReader(conn, CommandType.Text, strSql.ToString(), parameters))
            {
                while (drea.Read())
                    currentList.Add(SqlHelper.GetEntity<ClientDeductionVolumeDBEntity>(drea));
            }

            return new Tuple<int, IList<ClientDeductionVolumeDBEntity>>(count, currentList);
        }

        private double ToSecondFrom1970(DateTime inputDateTime)
        {
            DateTime beginTime = new DateTime(1970, 1, 1).ToLocalTime();
            long elapsedTicks = inputDateTime.Ticks - beginTime.Ticks;
            TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);
            return Math.Round(elapsedSpan.TotalSeconds, 0);
        }

        public bool UpdateCouponCheckTime(string mobilePhoneNumber)
        {
            string strSql = @"update CouponGetDetail set CheckTime=GETDATE() where CheckTime is null and State in (1,3) and ValidityEnd>GETDATE() and MobilePhoneNumber=@MobilePhoneNumber";
            //var xml = new StringBuilder();
            //foreach (var t in strCouponGetDetailId.Split(','))
            //{
            //    xml.AppendFormat("<row><id>{0}</id></row>", Convert.ToInt16(t));
            //}

            //SqlParameter[] para = new SqlParameter[]{
            //    new SqlParameter("@xml", SqlDbType.Xml) { Value = xml.ToString() }
            //};
            SqlParameter[] para = new SqlParameter[]{
            new SqlParameter("@MobilePhoneNumber", SqlDbType.NVarChar, 50) { Value = mobilePhoneNumber }
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
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

        /// <summary>
        /// 根据couponID修改抵扣券发送数量
        /// </summary>
        /// <param name="couponID"></param>
        /// <returns></returns>
        public bool UpdateCouponSendCount(int couponID)
        {
            var result = false;
            string strSql = @"begin tran 
                            declare @sheetNumber int 
                            declare @sendCount int 
                            select @sheetNumber=SheetNumber,@sendCount=SendCount from Coupon WITH (TABLOCKX) where CouponId=@CouponId 
                        if (@sheetNumber>@sendCount) 
                        begin 
                            update Coupon WITH(ROWLOCK) set SendCount=@sendCount+1 where CouponId=@CouponId 
                            select 1 as result 
                        end 
                        else 
                        begin 
                            select 0 as result 
                        end 
                        commit tran ";

            SqlParameter[] para = new SqlParameter[]{
            new SqlParameter("@CouponId", SqlDbType.Int, 6) { Value = couponID }
            };
            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                if (reader.Read())
                {
                    result=Convert.ToBoolean(reader["result"]);
                }
            }
            return result;
        }

        public List<CustomerCouponDetail> SelectCustomerCouponDetail(string phone, int cityId, out bool isHaveMore)
        {
            //Modfied by LinDY 只显示未使用的券
            string strSql = @"select top 10 s.shopID,s.shopName,d.RequirementMoney,d.DeductibleAmount,d.ValidityEnd,c.MaxAmount
 from CouponGetDetail d
inner join Coupon c on d.CouponId=c.CouponId
inner join ShopInfo s on c.ShopId=s.shopID and s.isHandle = 1
and s.cityID=@cityID and d.MobilePhoneNumber=@MobilePhoneNumber and d.ValidityEnd>GETDATE() AND d.State in (1,3) order by d.ValidityEnd";
            //End Modfied
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@MobilePhoneNumber", SqlDbType.NVarChar, 50) { Value = phone },
            new SqlParameter("@cityID", SqlDbType.Int) { Value = cityId },
            };
            List<CustomerCouponDetail> detailList = new List<CustomerCouponDetail>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                while (sdr.Read())
                {
                    CustomerCouponDetail detail = new CustomerCouponDetail();
                    detail.shopId = Convert.ToInt32(sdr["shopID"]);
                    detail.shopName = sdr["shopName"].ToString();
                    detail.couponName = "每满" + sdr["RequirementMoney"].ToString() + "元减" + sdr["DeductibleAmount"].ToString() + "元，最多减" + sdr["MaxAmount"].ToString();
                    detail.couponExpireDay = Convert.ToInt32(Math.Floor(Convert.ToDateTime(sdr["ValidityEnd"]).Subtract(DateTime.Now).TotalDays));
                    detailList.Add(detail);
                }
            }
            int count = 0;
            string strCnt = @"select COUNT(1)  from CouponGetDetail d
inner join Coupon c on d.CouponId=c.CouponId
inner join ShopInfo s on c.ShopId=s.shopID and s.isHandle = 1
and s.cityID=@cityID and d.MobilePhoneNumber=@MobilePhoneNumber and d.ValidityEnd>GETDATE() AND d.State in (1,3) ";
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strCnt, para);
                if (obj == null)
                {
                    count = 0;
                }
                else
                {
                    count = Convert.ToInt32(obj);
                }
            }
            isHaveMore = count > 10;
            return detailList;
        }

        /// <summary>
        /// 查询点单使用抵价券金额
        ///[0]  RequirementMoney [1] DeductibleAmount
        /// </summary>
        /// <param name="preOrder19DianId"></param>
        /// <returns></returns>
        public List<double> GetAmountOfOrder(long preOrder19DianId)
        {
            //后续修改DeductibleAmount为RealDeductibleAmount
            //            const string strSql = @"select ISNULL(c.RequirementMoney,0) RequirementMoney,ISNULL(c.DeductibleAmount,0) DeductibleAmount
            // from CouponUseRecord r inner join Coupon c on r.CouponId=c.CouponId 
            // where preOrder19dianId=@preOrder19DianId and r.StateType=2";
            const string strSql = @"select ISNULL(a.RequirementMoney,0) RequirementMoney,
            ISNULL(a.DeductibleAmount,0) DeductibleAmount ,
            ISNULL(a.RealDeductibleAmount,0) RealDeductibleAmount,ISNULL(MaxAmount,0) MaxAmount
            from CouponGetDetail a
            inner join Coupon b on a.CouponId=b.CouponID
            where a.State =2 and PreOrder19DianId=@preOrder19DianId";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@preOrder19DianId", SqlDbType.BigInt) { Value = preOrder19DianId }
            };
            List<double> coupon = new List<double>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                if (sdr.Read())
                {
                    coupon.Add(Convert.ToDouble(sdr[0]));
                    coupon.Add(Convert.ToDouble(sdr[1]));
                    coupon.Add(Convert.ToDouble(sdr[2]));
                    coupon.Add(Convert.ToDouble(sdr[3]));
                }
            }
            return coupon;
        }

        /// <summary>
        /// 新增抵价券使用记录
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public long InsertCouponUseRecord(CouponUseRecord record)
        {
            const string strSql = @"insert into CouponUseRecord(CouponGetDetailID,CouponId,StateType,ChangeReason,ChangeTime,PreOrder19DianId)
                                        values (@CouponGetDetailID,@CouponId,@StateType,@ChangeReason,@ChangeTime,@PreOrder19DianId)
                                        select @@identity";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@CouponGetDetailID", SqlDbType.BigInt) { Value = record.CouponGetDetailID },
            new SqlParameter("@CouponId", SqlDbType.Int) { Value = record.CouponId },
            new SqlParameter("@StateType", SqlDbType.Int) { Value = record.StateType },
            new SqlParameter("@ChangeReason", SqlDbType.NVarChar, 100) { Value = record.ChangeReason },
            new SqlParameter("@ChangeTime", SqlDbType.DateTime) { Value = record.ChangeTime },
            new SqlParameter("@PreOrder19DianId", SqlDbType.BigInt) { Value = record.PreOrder19DianId }
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
                    return Convert.ToInt64(obj);
                }
            }
        }

        /// <summary>
        /// 查询点单对应的抵扣券是否已退款返还
        /// </summary>
        /// <param name="preOrder19DianId"></param>
        /// <returns></returns>
        public bool IsCouponRefund(long preOrder19DianId)
        {
            string strSql = " select 1 from CouponUseRecord where PreOrder19DianId=@preOrder19DianId and StateType=" + (int)CouponUseStateType.refund + "";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@preOrder19DianId", SqlDbType.BigInt) { Value = preOrder19DianId }
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql, para);
                if (obj == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public List<ICoupon> GetListByDistanceOrder(int pageSize, int pageIndex, CouponQueryObject queryObject)
        {
            if (queryObject.Longitude == null | queryObject.Latitude == null)
            {
                return null;
            }
            double longitude = queryObject.Longitude.Value;
            double latitude = queryObject.Latitude.Value;

            SqlParameter[] sqlParameters = null;
            StringBuilder whereSqlStringBuilder = new StringBuilder(@"SELECT ROW_NUMBER() 
                               OVER( ORDER BY ( 6371 * ACOS( COS( RADIANS(@Latitude) ) * COS( RADIANS( Latitude )) * COS( RADIANS( Longitude ) 
                                    - RADIANS(@Longitude) ) + SIN( RADIANS(@Latitude) ) * SIN( RADIANS( Latitude ) ) ) ) ) AS ROWNUM
                                ,[CouponId]
                                ,[CouponName]
                                ,[ValidityPeriod]
                                ,[StartDate]
                                ,[EndDate]
                                ,[SheetNumber]
                                ,[SendCount]
                                ,[ShopId]
                                ,[CityId]
                                ,[RequirementMoney]
                                ,[SortOrder]
                                ,[DeductibleAmount]
                                ,[State]
                                ,[CreatedBy]
                                ,[CreateTime]
                                ,[LastUpdatedBy]
                                ,[LastUpdatedTime]
                                ,[Remark]
                                ,[RefuseReason]
                                ,[AuditEmployee]
                                ,[AuditTime]
                                ,[ShopName]
                                ,[ShopAddress]
                                ,[IsGot]
                                ,[Longitude]
                                ,[Latitude]
                                ,[DeductibleProportion]
                                ,[Image]
                                ,[CouponType]
                                ,[IsDisplay]
                            FROM  [Coupon] 
                            WHERE 1 =1 "); ;

            GetWhereSqlBuilderAndSqlParameterList(queryObject, ref sqlParameters, whereSqlStringBuilder, pageSize, pageIndex);

            string sql = string.Format(" SELECT TOP (@PageSize) * FROM ({0}) T WHERE T.ROWNUM > @PageIndex ", whereSqlStringBuilder.ToString());

            List<ICoupon> list = null;
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameters))
            {
                list = new List<ICoupon>();
                while (reader.Read())
                {
                    list.Add(reader.GetEntity<Coupon>());
                }
            }
            return list;

        }


        public List<ShopInfo> SearchShopWithCouponByKeyWord(int pageSize, int pageIndex, string keyWord, CouponQueryObject queryObject)
        {
            string sql = @" SELECT TOP (@PageSize) * FROM ( SELECT ROW_NUMBER() OVER( ORDER BY  LEN(shopName)) AS ROWNUM
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
                                ,[CanTakeout]
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
                            FROM  [ShopInfo] S
                            WHERE 1 =1 AND CityId = @CityId AND ShopName LIKE @KeyWord AND  EXISTS(SELECT NULL FROM Coupon C WHERE S.ShopId = C.ShopId {0}) ) T WHERE 
                            T.ROWNUM > @PageIndex ";

            SqlParameter[] sqlParameters = null;
            StringBuilder whereSqlBuilder = new StringBuilder();
            GetWhereSqlBuilderAndSqlParameterList(queryObject, ref sqlParameters, whereSqlBuilder, pageSize, pageIndex);
            sql = string.Format(sql, whereSqlBuilder.ToString());
            var sqlParameterList = sqlParameters.ToList();
            sqlParameterList.Add(new SqlParameter("@KeyWord", SqlDbType.NVarChar, 400) { Value = string.Format("{0}%", keyWord) });
            sqlParameters = sqlParameterList.ToArray();
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

        /// <summary>
        /// 获取某家店指定的抵扣券
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public Coupon GetCouponListOfShop(int shopId)
        {
            string strSql = @"select top 1 [CouponId]
                                ,[CouponName]
                                ,[ValidityPeriod]
                                ,[StartDate]
                                ,[EndDate]
                                ,[SheetNumber]
                                ,[SendCount]
                                ,[ShopId]
                                ,[CityId]
                                ,[RequirementMoney]
                                ,[SortOrder]
                                ,[DeductibleAmount]
                                ,[State]
                                ,[CreatedBy]
                                ,[CreateTime]
                                ,[LastUpdatedBy]
                                ,[LastUpdatedTime]
                                ,[Remark]
                                ,[RefuseReason]
                                ,[AuditEmployee]
                                ,[AuditTime]
                                ,[ShopName]
                                ,[ShopAddress]
                                ,[IsGot]
                                ,[Longitude]
                                ,[Latitude]
                                ,[DeductibleProportion]
                                ,[Image]
                                ,[CouponType]
                                ,[IsDisplay]
                                ,[SubsidyAmount] from Coupon where ShopId=@shopId
and CouponType=1
and State=1
and StartDate<GETDATE()
and EndDate>GETDATE()
and SendCount<SheetNumber
order by RequirementMoney,CreateTime";
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("shopId", SqlDbType.Int) { Value = shopId } };
            Coupon coupon = new Coupon();

            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                if (sdr.Read())
                {
                    coupon = SqlHelper.GetEntity<Coupon>(sdr);
                }
            }
            return coupon;
        }

        /// <summary>
        /// 查询指定订单对应的抵扣金额
        /// </summary>
        /// <param name="preOrder19dianIds"></param>
        /// <returns></returns>
        public List<OrderCoupon> SelectOrderDeductibleAmount(List<long> preOrder19dianIds)
        {
            string strSql = "select PreOrder19DianId,RealDeductibleAmount from CouponGetDetail where PreOrder19DianId in (select d.x.value('./id[1]','int') from @xml.nodes('/*') as d(x)) and state=2";

            var xml = new StringBuilder();
            foreach (var t in preOrder19dianIds)
            {
                xml.AppendFormat("<row><id>{0}</id></row>", (int)t);
            }

            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@xml", SqlDbType.Xml) { Value = xml.ToString() }
            };

            List<OrderCoupon> OrderCoupons = new List<OrderCoupon>();

            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction,CommandType.Text,strSql,para))
            {
                while (sdr.Read())
                {
                    OrderCoupons.Add(SqlHelper.GetEntity<OrderCoupon>(sdr));
                }
            }
            return OrderCoupons;
        }

        /// <summary>
        /// 获取某家店指定的礼券（CouponType=3）
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public List<CouponList> GetCouponListByShopName(string ShopName)
        {
            string strSql = @"select a.CouponName,a.CouponId,b.ShopID from coupon a
            inner join ShopInfo b on a.ShopId=b.shopID
            where a.CouponType=3 and b.ShopName like @ShopName and a.State=1
            and a.StartDate<GETDATE() and a.EndDate>GETDATE()";
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("ShopName", SqlDbType.NVarChar) { Value = "%"+ShopName+"%" } };
            List<CouponList> couponList = new List<CouponList>();

            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                while (sdr.Read())
                {
                    couponList.Add(SqlHelper.GetEntity<CouponList>(sdr));
                }
            }
            return couponList;
        }
    }
}
