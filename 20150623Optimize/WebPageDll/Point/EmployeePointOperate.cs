using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.Model;
using System.Data;
using System.Web;
using System.Transactions;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 员工积分操作业务逻辑层
    /// created by wangcheng 
    /// 20140221
    /// </summary>
    public class EmployeePointOperate
    {
        protected readonly EmployeePointManager man = new EmployeePointManager();
        /// <summary>
        /// 更新用户当前已结算积分（当前可用积分）
        /// </summary>
        /// <param name="preOrder19dianId">点单流水号</param>
        /// <param name="currectOperatePoint">金额</param>
        /// <param name="oldOperateEmployeeId">最后审核当前点单的服务员</param>
        /// <returns></returns>
        public bool UpdateEmployeeSettlementPoint(long preOrder19dianId, double currectOperatePoint, int oldOperateEmployeeId)
        {
            EmployeeOperate employeeOper = new EmployeeOperate();
            if (employeeOper.QueryEmployee(oldOperateEmployeeId) != null)
            {
                bool falg1 = false;//更新未结算积分
                bool falg2 = false;//更新已结算积分
                falg1 = man.UpdateEmployeeNotSettlementPoint(oldOperateEmployeeId, (-1) * Common.ToDouble(currectOperatePoint));//更新未结算积分
                falg2 = man.UpdateEmployeeSettlementPoint(oldOperateEmployeeId, Common.ToDouble(currectOperatePoint));//更新已结算积分
                if (falg1 && falg2)
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
                //表示当前用户不存在，不执行更新用户积分操作，直接return true
                return true;
            }
        }
        /// <summary>
        /// 更新用户当前已结算积分（当前可用积分）
        /// </summary>
        /// <param name="employeeId">员工编号</param>
        /// <param name="point">增加积分积分</param>
        /// <returns></returns>
        public bool ModifyEmployeeSettlementPoint(int employeeId, double point)
        {
            return man.UpdateEmployeeSettlementPoint(employeeId, Common.ToDouble(point));
        }
        /// <summary>
        ///  更新用户当前未结算积分（当前不可用积分）
        /// </summary>
        /// <param name="dtpreOrder19dian"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public bool UpdateEmployeeNotSettlementPoint(DataTable dtpreOrder19dian, int employeeId)
        {
            double pointsForMultiple = Common.ToDouble(SystemConfigManager.GetPointsForMultiple());//兑换积分倍数
            double pointsForSpecifications = Common.ToDouble(SystemConfigManager.GetPointsForSpecifications());//兑换积分规则，一元等于多少钱
            double prePaidSum = Common.ToDouble(dtpreOrder19dian.Rows[0]["prePaidSum"]);
            double refundMoneySum = Common.ToDouble(dtpreOrder19dian.Rows[0]["refundMoneySum"]);
            double currectOperatePoint = (prePaidSum - refundMoneySum) * pointsForMultiple * pointsForSpecifications;//当前应该更新员工的积分数目
            if (currectOperatePoint < 0.009)//如果当前积分倍数或者当前积分价值为0，则直接视为不参与积分计算，即不应该存在积分变动为0的记录
            {
                return true;
            }
            int isShopConfrim = Common.ToInt32(dtpreOrder19dian.Rows[0]["isShopConfirmed"]);//表示当前点单审核状态
            long preOrder19dianId = Common.ToInt64(dtpreOrder19dian.Rows[0]["preOrder19dianId"]);
            int evaluationValue = Common.ToInt32(dtpreOrder19dian.Rows[0]["evaluationValue"]);//当前点单的评分
            PreOrder19dianManager orderMan = new PreOrder19dianManager();
            EmployeePointLogOperate pointLogOper = new EmployeePointLogOperate();
            using (TransactionScope scope = new TransactionScope())
            {
                bool falg = false;
                bool logFlag = false;//积分日志变动
                if (isShopConfrim == (int)VAPreOrderShopConfirmed.NOT_SHOPCONFIRMED)//点单处于未审核状态，审核操作
                {
                    falg = man.UpdateEmployeeNotSettlementPoint(employeeId, Common.ToDouble(currectOperatePoint));//加上当前操作服务员审核当前点单获得的积分
                    EmployeePointLog pointLog = new EmployeePointLog()
                    {
                        customerId = Common.ToInt64(dtpreOrder19dian.Rows[0]["customerId"]),
                        employeeId = employeeId,//最后审核当前点单的服务员
                        monetary = prePaidSum - refundMoneySum,
                        pointVariation = currectOperatePoint,
                        pointVariationMethods = (int)PointVariationMethods.CUSTOMER_EXPENSE_GET,
                        preOrder19dianId = preOrder19dianId,
                        remark = "审核消费" + Common.ToString(prePaidSum - refundMoneySum) + "元",
                        shopId = Common.ToInt32(dtpreOrder19dian.Rows[0]["shopId"]),
                        status = 1,
                        viewallocEmployeeId = employeeId,
                        operateTime = DateTime.Now
                    };
                    logFlag = pointLogOper.Add(pointLog);//审核获得积分日志变动
                }
                else//取消审核操作(判断点单是否评分，是，减去评分对应的积分)
                {
                    double currectGradeOperatePoint = 0;
                    DataTable dtPreorderShopCancleConfirmedInfo = orderMan.SelectPreorderShopCancleConfirmedInfo(preOrder19dianId);
                    DataTable dtNewPreorderShopConfirmedInfo = orderMan.SelectNewPreorderShopConfirmedInfo(preOrder19dianId);
                    if (dtNewPreorderShopConfirmedInfo.Rows.Count == 1)//表示当前点单有审核记录
                    {
                        int oldOperateEmployeeId = Common.ToInt32(dtNewPreorderShopConfirmedInfo.Rows[0]["employeeId"]);
                        EmployeePointLog pointLog = new EmployeePointLog()
                        {
                            customerId = Common.ToInt64(dtpreOrder19dian.Rows[0]["customerId"]),
                            employeeId = oldOperateEmployeeId,//最后审核当前点单的服务员
                            monetary = prePaidSum - refundMoneySum,
                            pointVariation = (-1) * currectOperatePoint,
                            pointVariationMethods = (int)PointVariationMethods.CUSTOMER_CANCEL_REDUCE,
                            preOrder19dianId = preOrder19dianId,
                            remark = "取消审核" + Common.ToString(prePaidSum - refundMoneySum) + "元",//获得负分
                            shopId = Common.ToInt32(dtpreOrder19dian.Rows[0]["shopId"]),
                            status = 1,
                            viewallocEmployeeId = employeeId,
                            operateTime = DateTime.Now
                        };
                        logFlag = pointLogOper.Add(pointLog);//取消审核获得积分日志变动
                        bool evaluationPointFlag = false;
                        if (evaluationValue > 3 && dtPreorderShopCancleConfirmedInfo.Rows.Count == 0)//表示当前点单有评分加积分操作//表示当前点单没有取消审核记录
                        {
                            double gridePointsForMultiple = Common.ToDouble(SystemConfigManager.GradeGetPointsForMultiple());//评价点单兑换积分倍数
                            double gridePointsForSpecifications = Common.ToDouble(SystemConfigManager.GradeGetPointsForSpecifications());//评价点单兑换积分规则，一元等于多少钱
                            currectGradeOperatePoint = (prePaidSum - refundMoneySum) * gridePointsForMultiple * gridePointsForSpecifications;
                            EmployeePointLog _pointLog = new EmployeePointLog()
                            {
                                customerId = Common.ToInt64(dtpreOrder19dian.Rows[0]["customerId"]),
                                employeeId = oldOperateEmployeeId,//最后审核当前点单的服务员
                                monetary = 0,//没有消费
                                pointVariation = (-1) * currectOperatePoint,
                                pointVariationMethods = (int)PointVariationMethods.CLIENT_VALIDATION,
                                preOrder19dianId = preOrder19dianId,
                                remark = "取消评分奖励" + Common.ToString(currectOperatePoint) + "分",//系统减分
                                shopId = Common.ToInt32(dtpreOrder19dian.Rows[0]["shopId"]),
                                status = 1,
                                viewallocEmployeeId = employeeId,
                                operateTime = DateTime.Now
                            };
                            evaluationPointFlag = pointLogOper.Add(_pointLog);
                        }
                        else
                        {
                            evaluationPointFlag = true;
                        }
                        falg = man.UpdateEmployeeNotSettlementPoint(oldOperateEmployeeId, (-1) * Common.ToDouble(currectOperatePoint) + (-1) * Common.ToDouble(currectGradeOperatePoint));//减去上一个服务员审核当前点单获得的积分
                    }
                    else
                    {
                        falg = true;
                    }
                }
                if (falg && logFlag)
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
        /// <summary>
        /// 退款积分变动
        /// </summary>
        /// <param name="dtpreOrder19dian"></param>
        /// <param name="employeeId"></param>
        /// <param name="refundMoneySum"></param>
        /// <returns></returns>
        public bool RefundOpdatePoint(DataTable dtpreOrder19dian, int employeeId, double refundMoneySum)
        {
            double pointsForMultiple = Common.ToDouble(SystemConfigManager.GetPointsForMultiple());//兑换积分倍数
            double pointsForSpecifications = Common.ToDouble(SystemConfigManager.GetPointsForSpecifications());//兑换积分规则，一元等于多少钱
            double currectOperatePoint = refundMoneySum * pointsForMultiple * pointsForSpecifications;//当前应该更新员工的积分数目
            if (currectOperatePoint < 0.009)//如果当前积分倍数或者当前积分价值为0，则直接视为不参与积分计算，即不应该存在积分变动为0的记录
            {
                return true;
            }
            int isShopConfrim = Common.ToInt32(dtpreOrder19dian.Rows[0]["isShopConfirmed"]);//表示当前点单审核状态
            long preOrder19dianId = Common.ToInt64(dtpreOrder19dian.Rows[0]["preOrder19dianId"]);
            int evaluationValue = Common.ToInt32(dtpreOrder19dian.Rows[0]["evaluationValue"]);//当前点单的评分
            PreOrder19dianManager orderMan = new PreOrder19dianManager();
            EmployeePointLogOperate pointLogOper = new EmployeePointLogOperate();
            using (TransactionScope scope = new TransactionScope())
            {
                bool falg = false;
                bool logFlag = false;//积分日志变动
                bool evaluationPointFlag = false;
                double currectGradeOperatePoint = 0;
                DataTable dtPreorderShopCancleConfirmedInfo = orderMan.SelectPreorderShopCancleConfirmedInfo(preOrder19dianId);
                DataTable dtNewPreorderShopConfirmedInfo = orderMan.SelectNewPreorderShopConfirmedInfo(preOrder19dianId);
                if (dtNewPreorderShopConfirmedInfo.Rows.Count == 1)//表示当前点单有审核记录
                {
                    int oldOperateEmployeeId = Common.ToInt32(dtNewPreorderShopConfirmedInfo.Rows[0]["employeeId"]);
                    EmployeePointLog pointLog = new EmployeePointLog()
                    {
                        customerId = Common.ToInt64(dtpreOrder19dian.Rows[0]["customerId"]),
                        employeeId = oldOperateEmployeeId,//最后审核当前点单的服务员
                        monetary = refundMoneySum,
                        pointVariation = (-1) * currectOperatePoint,
                        pointVariationMethods = (int)PointVariationMethods.REFUND_VALIDATION,
                        preOrder19dianId = preOrder19dianId,
                        remark = "退款" + Common.ToString(refundMoneySum) + "元",//获得负分
                        shopId = Common.ToInt32(dtpreOrder19dian.Rows[0]["shopId"]),
                        status = 1,
                        viewallocEmployeeId = employeeId,
                        operateTime = DateTime.Now
                    };
                    logFlag = pointLogOper.Add(pointLog);//取消审核获得积分日志变动
                    if (evaluationValue > 3 && dtPreorderShopCancleConfirmedInfo.Rows.Count == 0)//表示当前点单有评分加积分操作//表示当前点单没有取消审核记录
                    {
                        double gridePointsForMultiple = Common.ToDouble(SystemConfigManager.GradeGetPointsForMultiple());//评价点单兑换积分倍数
                        double gridePointsForSpecifications = Common.ToDouble(SystemConfigManager.GradeGetPointsForSpecifications());//评价点单兑换积分规则，一元等于多少钱
                        currectGradeOperatePoint = refundMoneySum * gridePointsForMultiple * gridePointsForSpecifications;
                        EmployeePointLog _pointLog = new EmployeePointLog()
                        {
                            customerId = Common.ToInt64(dtpreOrder19dian.Rows[0]["customerId"]),
                            employeeId = oldOperateEmployeeId,//最后审核当前点单的服务员
                            monetary = 0,//没有消费
                            pointVariation = (-1) * currectGradeOperatePoint,
                            pointVariationMethods = (int)PointVariationMethods.CLIENT_VALIDATION,
                            preOrder19dianId = preOrder19dianId,
                            remark = "退款，取消评分奖励" + Common.ToString(currectGradeOperatePoint) + "分",//系统减分
                            shopId = Common.ToInt32(dtpreOrder19dian.Rows[0]["shopId"]),
                            status = 1,
                            viewallocEmployeeId = employeeId,
                            operateTime = DateTime.Now
                        };
                        evaluationPointFlag = pointLogOper.Add(_pointLog);
                    }
                    else
                    {
                        evaluationPointFlag = true;
                    }
                    falg = man.UpdateEmployeeNotSettlementPoint(oldOperateEmployeeId, (-1) * Common.ToDouble(currectOperatePoint) + (-1) * Common.ToDouble(currectGradeOperatePoint));//减去上一个服务员审核当前点单获得的积分
                }
                else
                {
                    logFlag = true;
                }
                if (logFlag && falg && evaluationPointFlag)
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
        /// <summary>
        /// 查询服务员信息
        /// </summary>
        /// <returns></returns>
        public DataTable QueryWaiter(string phoneNum)
        {
            return man.SelectWaiter(phoneNum);
        }
        /// <summary>
        /// 查询服务员在门店服务信息
        /// </summary>
        /// <param name="starTime"></param>
        /// <param name="endTime"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public DataTable QueryWaiterWorkExperience(string starTime, string endTime, int employeeId)
        {
            return man.SelectWaiterWorkExperience(starTime, endTime, employeeId);
        }
        /// <summary>
        /// 查询服务员积分排名
        /// </summary>
        /// <param name="starTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="cityId">城市编号</param>
        /// <param name="orderRule">排序规则</param>
        /// <param name="phoneNum">手机号码</param>
        /// <param name="amount">金额</param>
        /// <returns></returns>
        public DataTable QueryWaiterPointRanking(string starTime, string endTime, int cityId, int orderRule, string phoneNum, double amount)
        {
            return man.SelectWaiterPointRanking(starTime, endTime, cityId, orderRule, phoneNum, amount);
        }

        /// <summary>
        /// 查询服务员可用积分是否正常
        /// true:正常；false:异常
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public bool EmployeePointIsValid(int employeeId)
        {
            return man.EmployeePointIsValid(employeeId);
        }

        /// <summary>
        /// 根据员工cookie信息查询基本信息
        /// </summary>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public DataTable SelectEmployeeByCookie(string cookie)
        {
            AuthorityManager _Manager = new AuthorityManager();
            return _Manager.SelectEmployeePointByCookie(cookie);
        }

        /// <summary>
        /// 客户端根据员工cookie信息查询基本信息
        /// </summary>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public string ClientQueryEmployeeByCookie(string cookie)
        {
            DataTable dt = SelectEmployeeByCookie(cookie);
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
        /// 客户端用户兑换请求，服务端发送验证码给用户手机
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public string ClientExchangeRequest(string phoneNumber)
        {
            string msgResult = "";
            ZZBPreOrderOperate _Operate = new ZZBPreOrderOperate();
            if (string.IsNullOrEmpty(phoneNumber))
            {
                msgResult = "-1";//手机号码不能为空
            }
            else
            {
                string verificationCode = "";
                AuthorityManager authorityMan = new AuthorityManager();
                DataTable dtEmployee = authorityMan.SelectEmployeeByMobilephone(phoneNumber);//查找用户验证码相关信息
                if (dtEmployee.Rows.Count == 1)
                {
                    DateTime verificationCodeTime = Common.ToDateTime(dtEmployee.Rows[0]["verificationCodeTime"]);
                    if ((System.DateTime.Now - verificationCodeTime) < TimeSpan.FromMinutes(10))
                    {
                        verificationCode = Common.ToString(dtEmployee.Rows[0]["verificationCode"]);
                    }
                    VAResult result = _Operate.SendVerificationCodeBySms(phoneNumber, verificationCode);
                    switch (result)
                    {
                        case VAResult.VA_OK:
                            msgResult = "1";//验证码发送成功
                            break;
                        case VAResult.VA_FAILED_SMS_NOT_SEND:
                            msgResult = "-2";//验证码发送失败
                            break;
                        case VAResult.VA_FAILED_DB_ERROR:
                            msgResult = "-3";//验证码记录失败
                            break;
                        default:
                            break;
                    }
                }
            }
            return msgResult;
        }

        /// <summary>
        /// 积分商城：用户确认用积分兑换商品
        /// </summary>
        /// <param name="phoneNumber">用户手机号码</param>
        /// <param name="verificationCode">验证码</param>
        /// <param name="goodsId">商品ID</param>
        /// <param name="shopId">店铺ID</param>
        /// <returns></returns>
        public string ClientExchangeConfirm(string phoneNumber, string verificationCode, int goodsId)
        {
            string result = "";
            if (string.IsNullOrEmpty(verificationCode) || string.IsNullOrEmpty(phoneNumber))//验证码为空或电话号码为空
            {
                result = "-1";//请求参数不能为空
            }
            else
            {
                #region
                AuthorityManager authorityMan = new AuthorityManager();
                DataTable dtEmployee = authorityMan.SelectEmployeeByMobilephone(phoneNumber);//查找用户验证码相关信息
                if (dtEmployee.Rows.Count == 1)
                {
                    DateTime verificationCodeTime = Common.ToDateTime(dtEmployee.Rows[0]["verificationCodeTime"]);
                    if ((System.DateTime.Now - verificationCodeTime) < TimeSpan.FromMinutes(10))
                    {
                        if (string.Equals(verificationCode, Common.ToString(dtEmployee.Rows[0]["verificationCode"])))//验证码OK
                        {
                            //检查可用积分
                            EmployeePointLogOperate _pointOperate = new EmployeePointLogOperate();//BLL
                            GoodsOperate _goodsOperate = new GoodsOperate();//BLL

                            Goods goods = _goodsOperate.QueryGoods(goodsId);//查询用户要兑换的商品信息
                            if (goods.residueQuantity <= 0)
                            {
                                return "-7";//库存不足 add by wangc
                            }
                            double employeePoint = Common.ToDouble(dtEmployee.Rows[0]["settlementPoint"]);
                            double goodsPoint = goods.exchangePrice;

                            if (employeePoint > goodsPoint)//员工可用积分足够
                            {
                                EmployeePointLog pointLog = new EmployeePointLog();//Model
                                PointManageLog manageLog = new PointManageLog();//Model
                                #region
                                pointLog.operateTime = DateTime.Now;
                                pointLog.employeeId = Common.ToInt32(dtEmployee.Rows[0]["employeeId"]);
                                pointLog.pointVariation = -goods.exchangePrice;
                                pointLog.pointVariationMethods = (int)PointVariationMethods.GOODS_EXCHANGE;
                                pointLog.status = 1;
                                pointLog.goodsId = goodsId;
                                pointLog.exchangeStatus = 1;
                                pointLog.confirmStatus = -1;
                                pointLog.shipStatus = -1;
                                pointLog.remark = "兑换商品：" + goods.name;

                                manageLog.remark = "新建兑换";
                                manageLog.createTime = DateTime.Now;
                                manageLog.createdBy = Common.ToInt32(dtEmployee.Rows[0]["employeeId"]);
                                manageLog.status = 1;
                                #endregion
                                bool exchange = _pointOperate.Exchange(pointLog, manageLog);
                                if (exchange)
                                {
                                    result = "1";//兑换成功
                                }
                                else
                                {
                                    result = "-6";//兑换失败
                                }
                            }
                            else
                            {
                                result = "-5";//可用积分不足，无法兑换当前商品
                            }
                        }
                        else
                        {
                            result = "-4";//验证码错误，重新输入验证码重试，或者重新接收新验证码后重试
                        }
                    }
                    else
                    {
                        result = "-3";//验证码过期，重新接收新验证码后重试
                    }
                }
                else//未找到此电话号码用户
                {
                    result = "-2";
                }
                #endregion
            }
            return result;
        }
    }
}
