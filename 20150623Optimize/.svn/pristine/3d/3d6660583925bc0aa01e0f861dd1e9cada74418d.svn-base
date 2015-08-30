using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.Model;
using System.Data;
using System.Transactions;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 服务员积分日志业务逻辑层
    /// created by wangcheng 
    /// 20140222
    /// </summary>
    public class EmployeePointLogOperate
    {
        private readonly EmployeePointLogManager manager = new EmployeePointLogManager();
        /// <summary>
        /// 对账增加一条服务员积分
        /// </summary>
        public bool ApproveAddPoint(DataTable dtpreOrder19dian)
        {
            double monetary = 0;
            double pointsForMultiple = Common.ToDouble(SystemConfigManager.GetPointsForMultiple());//兑换积分倍数
            double pointsForSpecifications = Common.ToDouble(SystemConfigManager.GetPointsForSpecifications());//兑换积分规则，一元等于多少钱
            double prePaidSum = Common.ToDouble(dtpreOrder19dian.Rows[0]["prePaidSum"]);
            double refundMoneySum = Common.ToDouble(dtpreOrder19dian.Rows[0]["refundMoneySum"]);//refundMoneySum
            double currectOperatePoint = (prePaidSum - refundMoneySum) * pointsForMultiple * pointsForSpecifications;//当前应该更新员工的积分数目
            if (currectOperatePoint < 0.009)//如果当前积分倍数或者当前积分价值为0，则直接视为不参与积分计算，即不应该存在积分变动为0的记录
            {
                return true;
            }
            long preOrder19dianId = Common.ToInt64(dtpreOrder19dian.Rows[0]["preOrder19dianId"]);
            long customerId = Common.ToInt32(dtpreOrder19dian.Rows[0]["customerId"]);//
            int evaluationValue = Common.ToInt32(dtpreOrder19dian.Rows[0]["evaluationValue"]);//当前点单的评分
            PreOrder19dianManager orderMan = new PreOrder19dianManager();
            DataTable dtNewPreorderShopConfirmedInfo = orderMan.SelectNewPreorderShopConfirmedInfo(preOrder19dianId);
            if (dtNewPreorderShopConfirmedInfo.Rows.Count == 1)//审核信息
            {
                int oldOperateEmployeeId = Common.ToInt32(dtNewPreorderShopConfirmedInfo.Rows[0]["employeeId"]);//最后审核点单的人
                DataTable dtPreorderShopCancleConfirmedInfo = orderMan.SelectPreorderShopCancleConfirmedInfo(preOrder19dianId);//取消审核信息
                if (dtPreorderShopCancleConfirmedInfo.Rows.Count <= 0)
                {
                    if (evaluationValue > 3 && dtPreorderShopCancleConfirmedInfo.Rows.Count == 0)//表示当前点单有评分加积分操作//表示当前点单没有取消审核记录
                    {
                        double gridePointsForMultiple = Common.ToDouble(SystemConfigManager.GradeGetPointsForMultiple());//评价点单兑换积分倍数
                        double gridePointsForSpecifications = Common.ToDouble(SystemConfigManager.GradeGetPointsForSpecifications());//评价点单兑换积分规则，一元等于多少钱
                        monetary = currectOperatePoint + (prePaidSum - refundMoneySum) * gridePointsForMultiple * gridePointsForSpecifications;
                    }
                    else
                    {
                        monetary = currectOperatePoint;
                    }
                }
                else
                {
                    //存在取消审核操作，客户端评价获得未结算积分已在取消审核减掉
                    //只有没有取消审核记录点单，并且点单评分大于3的才会将评分积分转化为已结算积分
                    monetary = currectOperatePoint;//审核获得的未结算积分
                }
                EmployeePointOperate employeeOper = new EmployeePointOperate();
                using (TransactionScope scope = new TransactionScope())
                {
                    bool flag = employeeOper.UpdateEmployeeSettlementPoint(preOrder19dianId, monetary, oldOperateEmployeeId);
                    if (flag)
                    {
                        scope.Complete();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 增加一条服务员积分
        /// </summary>
        public bool Add(EmployeePointLog model)
        {
            return manager.Add(model) > 0 ? true : false;
        }
        /// <summary>
        /// 查询服务员兑换记录条数
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public int QueryEmployeeExchangeCount(int employeeID)
        {
            return manager.SelectEmployeeExchangeCount(employeeID);
        }
        /// <summary>
        ///  查询服务员在某个时间段内积分变动日志
        /// </summary>
        /// <param name="starTime"></param>
        /// <param name="endTime"></param>
        /// <param name="employeeId"></param>
        /// <param name="showAdd"></param>
        /// <param name="showReduce"></param>
        /// <returns></returns>
        public DataTable QueryWaiterPointLog(string starTime, string endTime, int employeeId, bool showAdd, bool showReduce)
        {
            return manager.SelectWaiterPointLog(starTime, endTime, employeeId, showAdd, showReduce);
        }

        /// <summary>
        /// 积分商城，服务员兑换商品
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool Exchange(EmployeePointLog point, PointManageLog log)
        {
            EmployeePointManager _PointManage = new EmployeePointManager();//DAL
            PointManageLogManager _LogManager = new PointManageLogManager();//DAL
            GoodsManage _GoodManager = new GoodsManage();
            using (TransactionScope ts = new TransactionScope())
            {
                bool UpdatePoint = _PointManage.UpdateEmployeeSettlementPoint(point.employeeId, point.pointVariation);
                int InsertExchange = manager.ExchangeAdd(point);

                log.pointLogId = InsertExchange;
                int InsertLog = _LogManager.InsertPointManageLog(log);

                if (UpdatePoint && InsertExchange > 0 && InsertLog > 0 && _GoodManager.UpdateGoodsResidueQuantity(point.goodsId))
                {
                    ts.Complete();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 积分商城：后台查询兑换记录
        /// </summary>
        /// <returns></returns>
        public DataTable QueryExchangeLog(string poriodValue, string txbStart, string txbEnd, string shipStatusValue, string phoneNumber, int id)
        {
            string startTime = "", endTime = "";

            //周期选择：兑换时间
            switch (poriodValue)
            {
                case "week":
                    int dayOfWeek = Convert.ToInt32(DateTime.Now.DayOfWeek);
                    int daydiff = (-1) * dayOfWeek + 1;
                    //int dayadd = 5 - dayOfWeek;  
                    startTime = DateTime.Now.AddDays(daydiff).ToString("yyyy-MM-dd");
                    endTime = DateTime.Now.ToString("yyyy-MM-dd");
                    //DateTime weekEndDate = DateTime.Now.AddDays(dayadd);  
                    break;
                case "month":
                    startTime = DateTime.Now.AddDays(-(DateTime.Now.Day) + 1).ToString("yyyy-MM-dd");
                    endTime = DateTime.Now.ToString("yyyy-MM-dd");
                    break;
                default:
                    startTime = txbStart;
                    endTime = txbEnd;
                    break;
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                endTime = endTime + " 23:59:59";
            }
            return manager.QueryExchangeLog(startTime, endTime, shipStatusValue, phoneNumber, id);
        }

        /// <summary>
        /// 积分商城：根据兑换记录ID查询部分信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EmployeePointLog QueryExchangeLog(int id)
        {
            EmployeePointLogManager _Manage = new EmployeePointLogManager();
            return _Manage.QueryExchangeLog(id);
        }

        /// <summary>
        /// 积分商城：客户端查询服务员的积分兑换记录
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public string QueryExchangeLogForClient(int employeeId)
        {
            EmployeePointLogManager _Manage = new EmployeePointLogManager();
            DataTable dtPoint = _Manage.QueryExchangeLogForClient(employeeId);

            return Common.ConvertDateTableToJson(dtPoint);
        }

        /// <summary>
        /// 更改兑换单的兑换状态
        /// </summary>
        /// <param name="exchangeStatus"></param>
        /// <param name="pointLogId"></param>
        /// <returns></returns>
        public bool UpdateExchangeStatus(int exchangeStatus, int confirmStatus, long pointLogId, PointManageLog log)
        {
            EmployeePointLogManager _Point = new EmployeePointLogManager();
            PointManageLogManager _log = new PointManageLogManager();
            using (TransactionScope st = new TransactionScope())
            {
                bool update = _Point.UpdateExchangeStatus(exchangeStatus, confirmStatus, pointLogId);
                int insert = _log.InsertPointManageLog(log);
                if (update && insert > 0)
                {
                    st.Complete();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 后台查询员工的兑换记录
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public DataTable QueryEmployeeExchangeLog(int employeeId)
        {
            EmployeePointLogManager _Manager = new EmployeePointLogManager();
            return _Manager.QueryEmployeeExchangeLog(employeeId);
        }

        /// <summary>
        /// 客户端查询某个服务员的所有积分变动记录
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public string ClientQueryEmployeePoint(int employeeId)
        {
            EmployeePointLogManager _Manager = new EmployeePointLogManager();
            DataTable dt = _Manager.QueryEmployeePoint(employeeId);

            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    switch (Common.ToInt32(dt.Rows[i]["pointVariationMethods"]))
                    {
                        case (int)PointVariationMethods.CLIENT_VALIDATION:
                            dt.Rows[i]["remark"] = Common.GetEnumDescription(PointVariationMethods.CLIENT_VALIDATION);
                            break;
                        case (int)PointVariationMethods.CUSTOMER_CANCEL_REDUCE:
                            dt.Rows[i]["remark"] = Common.GetEnumDescription(PointVariationMethods.CUSTOMER_CANCEL_REDUCE);
                            break;
                        case (int)PointVariationMethods.CUSTOMER_EXPENSE_GET:
                            dt.Rows[i]["remark"] = Common.GetEnumDescription(PointVariationMethods.CUSTOMER_EXPENSE_GET);
                            break;
                        case (int)PointVariationMethods.GOODS_EXCHANGE:
                            dt.Rows[i]["remark"] = Common.GetEnumDescription(PointVariationMethods.GOODS_EXCHANGE);
                            break;
                        case (int)PointVariationMethods.REFUND_VALIDATION:
                            dt.Rows[i]["remark"] = Common.GetEnumDescription(PointVariationMethods.REFUND_VALIDATION);
                            break;
                        case (int)PointVariationMethods.VIEWALLOC_REWARDS:
                            dt.Rows[i]["remark"] = Common.GetEnumDescription(PointVariationMethods.VIEWALLOC_REWARDS);
                            break;
                        default:
                            break;
                    }
                }
                return Common.ConvertDateTableToJson(dt);
            }
            else
            {
                return "";
            }
        }



        /// <summary>
        /// 客户端查询某个服务员最近一次积分变动
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public string ClientQueryEmployeeLastPointLog(int employeeId)
        {
            EmployeePointLogManager _Manager = new EmployeePointLogManager();
            DataTable dt = _Manager.QueryEmployeeLastPointLog(employeeId);
            if (dt != null && dt.Rows.Count > 0)
            {
                return Common.ConvertDateTableToJson(dt);
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 查询确认状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int QueryConfirmStatus(int id)
        {
            EmployeePointLogManager _manager = new EmployeePointLogManager();
            return _manager.QueryConfirmStatus(id);
        }

        /// <summary>
        /// 根据兑换单号查询员工ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int QueryEmployeeID(int id)
        {
            EmployeePointLogManager _manager = new EmployeePointLogManager();
            return _manager.QueryEmployeeID(id);
        }
    }
}
