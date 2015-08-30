using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using System.Transactions;
using System.Web;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 员工信息操作类
    /// </summary>
    public class EmployeeOperate
    {
        /// <summary>
        /// 新增员工信息
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public int AddEmployee(EmployeeInfo employee)
        {
            int employeeId = 0;
            AuthorityManager authorityMan = new AuthorityManager();
            DataTable dtEmployee = authorityMan.SelectEmployee();
            DataView dvEmployee = dtEmployee.DefaultView;
            dvEmployee.RowFilter = "EmployeeFirstName = '" + employee.EmployeeFirstName
                //+ "' and EmployeeLastName = '" + employee.EmployeeLastName//2014-2-23 取消LastName
                + "' or UserName = '" + employee.UserName + "'";
            if (dvEmployee.Count > 0 || string.IsNullOrEmpty(employee.EmployeeFirstName))//  || string.IsNullOrEmpty(employee.EmployeeLastName)
            {//如果所加员工信息的姓名为空，或者员工信息表中已有该姓名或者用户名的员工，则直接返回false

            }
            else
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    employeeId = authorityMan.InsertEmployeeBySp(employee);
                    if (employeeId > 0)
                    {
                        scope.Complete();
                    }
                }
            }
            return employeeId;
        }
        /// <summary>
        /// 删除员工信息
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public bool RemoveEmployee(int employeeID)
        {
            AuthorityManager authorityMan = new AuthorityManager();
            DataTable dtEmployee = authorityMan.SelectEmployee();
            DataView dvEmployee = dtEmployee.DefaultView;
            dvEmployee.RowFilter = "EmployeeID = '" + employeeID + "'";
            if (1 == dvEmployee.Count)
            {//判断此employeeID是否存在，是则删除
                if (authorityMan.DeleteEmployee(employeeID))
                {//删除成功则返回true
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
        /// <summary>
        /// 修改员工信息
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public bool ModifyEmployee(EmployeeInfo employee)
        {
            AuthorityManager authorityMan = new AuthorityManager();
            DataTable dtEmployee = authorityMan.SelectEmployee();
            DataTable dtEmployeeCopy = dtEmployee.Copy();
            DataView dvEmployeeCopy = dtEmployeeCopy.DefaultView;
            //DataView dvEmployee = dtEmployee.DefaultView;
            dvEmployeeCopy.RowFilter = "EmployeeID = '" + employee.EmployeeID + "'";//判断此ID存在
            //dvEmployee.RowFilter = "EmployeeID <> '" + employee.EmployeeID
            //    + "' and EmployeeFirstName = '" + employee.EmployeeFirstName + "'";//判断修改的姓名不存在
            //+ "' and EmployeeLastName = '" + employee.EmployeeLastName + "'";//2014-2-23 取消LastName
            if (1 == dvEmployeeCopy.Count)              // && 0 == dvEmployee.Count
            {//判断此EmployeeID是否存在，是则修改
                if (authorityMan.UpdateEmployee(employee))
                {//修改成功则返回true
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
        /// <summary>
        /// 修改员工信息(wangcheng)
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <param name="status"></param>
        /// <param name="flagStr">标记修改isSupportEnterSyb，isSupportReceiveMsg字段</param>
        /// <returns></returns>
        public string ModifyEmployee(int EmployeeID, int status, string flagStr)
        {
            SybMsg sysMsg = new SybMsg();
            //int currentEmployeeId = ((VAEmployeeLoginResponse)(HttpContext.Current.Session["MerchantsTreasureUserInfo"])).employeeID;
            //if (currentEmployeeId != EmployeeID)
            //{
            //    int shopID = Common.ToInt32(HttpContext.Current.Session["loginshop"]);
            //    if (shopID > 0)
            //    {
            //        bool isSupportEnterSyb = false;
            //        bool isSupportReceiveMsg = false;
            //        AuthorityManager authorityMan = new AuthorityManager();
            //        DataTable dtEmployee = authorityMan.SelectEmployeeShop(EmployeeID, shopID);
            //        if (1 == dtEmployee.Rows.Count)
            //        {
            //            switch (flagStr)
            //            {
            //                case "enter_syb"://修改可进入收银宝权限
            //                    {
            //                        if (Common.ToBool(dtEmployee.Rows[0]["isSupportEnterSyb"]) == true && status == 0)
            //                        {
            //                            isSupportEnterSyb = false;
            //                            if (authorityMan.UpdateEmployee(EmployeeID, shopID, isSupportEnterSyb))
            //                            {
            //                                sysMsg.Insert(1, "关闭成功");
            //                            }
            //                            else
            //                            {
            //                                sysMsg.Insert(-1, "关闭失败");
            //                            }
            //                        }
            //                        else if (Common.ToBool(dtEmployee.Rows[0]["isSupportEnterSyb"]) == false && status == 1)
            //                        {
            //                            isSupportEnterSyb = true;
            //                            if (authorityMan.UpdateEmployee(EmployeeID, shopID, isSupportEnterSyb))
            //                            {
            //                                sysMsg.Insert(1, "开启成功");
            //                            }
            //                            else
            //                            {
            //                                sysMsg.Insert(-1, "开启失败");
            //                            }
            //                        }
            //                        else
            //                        {
            //                            sysMsg.Insert(1, "操作无效");
            //                        }
            //                    }
            //                    break;
            //                case "receive_msg"://修改服务员是否接受支付点单短信提醒
            //                    {
            //                        if (Common.ToBool(dtEmployee.Rows[0]["isSupportReceiveMsg"]) == true && status == 0)
            //                        {
            //                            isSupportReceiveMsg = false;
            //                            if (authorityMan.UpdateEmployeeReceivePayOrderMsg(EmployeeID, shopID, isSupportReceiveMsg))
            //                            {
            //                                sysMsg.Insert(1, "关闭成功");
            //                            }
            //                            else
            //                            {
            //                                sysMsg.Insert(-1, "关闭失败");
            //                            }
            //                        }
            //                        else if (Common.ToBool(dtEmployee.Rows[0]["isSupportReceiveMsg"]) == false && status == 1)
            //                        {
            //                            isSupportReceiveMsg = true;
            //                            if (authorityMan.UpdateEmployeeReceivePayOrderMsg(EmployeeID, shopID, isSupportReceiveMsg))
            //                            {
            //                                sysMsg.Insert(1, "开启成功");
            //                            }
            //                            else
            //                            {
            //                                sysMsg.Insert(-1, "开启失败");
            //                            }
            //                        }
            //                        else
            //                        {
            //                            sysMsg.Insert(1, "操作无效");
            //                        }
            //                    }
            //                    break;
            //                default:
            //                    break;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        sysMsg.Insert(-2, "当前员工信息不存在");
            //    }
            //}
            //else
            //{
            //    sysMsg.Insert(-3, "无法操作个人信息");
            //}
            return sysMsg.Value;
        }
        /// <summary>
        /// 修改员工登录密码
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ModifyEmployeePwd(int employeeID, string password)
        {
            AuthorityManager authorityMan = new AuthorityManager();
            DataTable dtEmployee = authorityMan.SelectEmployee();
            DataView dvEmployee = dtEmployee.DefaultView;
            dvEmployee.RowFilter = "EmployeeID = '" + employeeID + "'";
            if (1 == dvEmployee.Count)
            {//判断此EmployeeID是否存在，是则修改
                if (authorityMan.UpdateEmployeePwd(employeeID, password))
                {//修改成功则返回true
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
        /// <summary>
        /// 收银宝重置密码（wangcheng）
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string SybModifyEmployeePwd(int employeeID)
        {
            string newPwd = Common.randomStrAndNum(6);
            bool flag = ModifyEmployeePwd(employeeID, MD5Operate.getMd5Hash(newPwd));
            SybMsg message = new SybMsg();
            if (flag)
            {
                Common.RecordEmployeeOperateLogBySYB((int)VAEmployeeOperateLogOperatePageType.PASSWORD_MODIFY, (int)VAEmployeeOperateLogOperateType.UPDATE_OPERATE, "悠先收银重置密码为" + MD5Operate.getMd5Hash(newPwd));
                message.Insert(1, newPwd);
            }
            else
            {
                message.Insert(-1, "重置密码失败");
            }
            return message.Value;
        }
        /// <summary>
        /// 查询所有员工信息
        /// </summary>
        /// <returns></returns>
        public DataTable QueryEmployee()
        {
            AuthorityManager authorityMan = new AuthorityManager();
            DataTable dtEmployee = authorityMan.SelectEmployee();
            DataView dvEmployee = dtEmployee.DefaultView;
            dvEmployee.RowFilter = "EmployeeID <> 1";
            dvEmployee.Sort = "EmployeeSequence ASC,EmployeeFirstName ASC";//,EmployeeLastName ASC
            return dvEmployee.ToTable();
        }
        /// <summary>
        /// 根据员工姓名和用户名查询员工信息（wangcheng）
        /// </summary>
        /// <returns></returns>
        public DataTable QueryEmployeeByName(string name)
        {
            AuthorityManager authorityMan = new AuthorityManager();
            DataTable dtEmployee = authorityMan.SelectEmployeeByName(name);
            DataView dvEmployee = dtEmployee.DefaultView;
            dvEmployee.RowFilter = "EmployeeID <> 1";
            dvEmployee.Sort = "EmployeeSequence ASC,EmployeeFirstName ASC";//,EmployeeLastName ASC
            return dvEmployee.ToTable();
        }
        /// <summary>
        /// 判断用户名是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool IsEmployeeUserNameExit(string userName)
        {
            AuthorityManager authorityMan = new AuthorityManager();
            return authorityMan.IsEmployeeUserNameExit(userName);
        }
        /// <summary>
        /// 根据员工编号查询对应员工基本信息
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public EmployeeInfo QueryEmployee(int employeeID)
        {
            AuthorityManager authorityMan = new AuthorityManager();
            DataTable dtEmployee = authorityMan.SelectEmployee(employeeID);
            DataView dvEmployee = dtEmployee.DefaultView;
            //dvEmployee.RowFilter = "EmployeeID = '" + employeeID + "'";
            EmployeeInfo employee = new EmployeeInfo();
            if (1 == dvEmployee.Count)
            {
                employee.EmployeeID = Common.ToInt32(dvEmployee[0]["EmployeeID"]);
                employee.UserName = Common.ToString(dvEmployee[0]["UserName"]);
                employee.Password = Common.ToString(dvEmployee[0]["Password"]);
                employee.EmployeeFirstName = Common.ToString(dvEmployee[0]["EmployeeFirstName"]);
                //employee.EmployeeLastName = Common.ToString(dvEmployee[0]["EmployeeLastName"]);//2014-2-23 取消LastName
                employee.EmployeeSex = Common.ToInt32(dvEmployee[0]["EmployeeSex"]);
                employee.EmployeeAge = Common.ToInt32(dvEmployee[0]["EmployeeAge"]);
                employee.EmployeePhone = Common.ToString(dvEmployee[0]["EmployeePhone"]);
                employee.EmployeeSequence = Common.ToInt32(dvEmployee[0]["EmployeeSequence"]);
                //2014-2-23 取消 员工抹零金额最大值，快速结账权限，清台权限，称重权限
                //employee.removeChangeMaxValue = Common.ToInt32(dvEmployee[0]["removeChangeMaxValue"]);
                //employee.canQuickCheckout = Common.ToBool(dvEmployee[0]["canQuickCheckout"]);
                //employee.canClearTable = Common.ToBool(dvEmployee[0]["canClearTable"]);
                //employee.canWeigh = Common.ToBool(dvEmployee[0]["canWeigh"]);
                employee.position = Common.ToString(dvEmployee[0]["position"]);
                employee.defaultPage = Common.ToString(dvEmployee[0]["defaultPage"]);
                employee.isViewAllocWorker = Common.ToBool(dvEmployee[0]["isViewAllocWorker"]);
                employee.settlementPoint = Common.ToDouble(dvEmployee[0]["settlementPoint"]);
                employee.notSettlementPoint = Common.ToDouble(dvEmployee[0]["notSettlementPoint"]);
                employee.registerTime = Common.ToDateTime(dvEmployee[0]["registerTime"]);
                employee.birthday = Common.ToDateTime(dvEmployee[0]["birthday"]);
                employee.remark = Common.ToString(dvEmployee[0]["remark"]);
                //add by wangc 20140324
                employee.isSupportLoginBgSYS = Common.ToBool(dvEmployee[0]["isSupportLoginBgSYS"]);
            }
            else
            {
                employee = null;
            }
            return employee;
        }
        /// <summary>
        /// 根据员工编号查询对应角色信息
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public List<VAEmployeeRole> QueryEmployeeRole(int employeeID)
        {
            AuthorityManager authorityMan = new AuthorityManager();
            DataTable dtEmployeeRole = authorityMan.SelectEmployeeRole();
            DataView dvEmployeeRole = dtEmployeeRole.DefaultView;
            dvEmployeeRole.RowFilter = "EmployeeID = '" + employeeID + "'";
            List<VAEmployeeRole> roleList = new List<VAEmployeeRole>();
            if (dvEmployeeRole.Count > 0)
            {
                for (int i = 0; i < dvEmployeeRole.Count; i++)
                {
                    VAEmployeeRole role = new VAEmployeeRole();
                    role.employeeRoleID = Common.ToInt32(dvEmployeeRole[i]["EmployeeRoleID"]);
                    role.roleName = Common.ToString(dvEmployeeRole[i]["RoleName"]);
                    roleList.Add(role);
                }
            }
            else
            {
                roleList = null;
            }
            return roleList;
        }
        /// <summary>
        /// new商户登录
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public VAEmployeeLoginResponse EmployeeLogin(VAEmployeeLogin login)
        {
            VAEmployeeLoginResponse loginResponse = new VAEmployeeLoginResponse();

            loginResponse.type = VAMessageType.EMPLOYEE_LOGIN_RESPONSE;
            AuthorityManager authorityMan = new AuthorityManager();

            DataTable dtEmployee = authorityMan.SelectEmployee();
            DataView dvEmployee = dtEmployee.DefaultView;
            dvEmployee.RowFilter = "UserName = '" + login.userName + "'";
            if (1 == dvEmployee.Count)
            {
                if (login.password == Common.ToString(dvEmployee[0]["Password"]))
                {
                    EmployeeLoginInfo employeeLogin = new EmployeeLoginInfo();
                    employeeLogin.LoginTime = DateTime.Now;
                    employeeLogin.UserName = login.userName;
                    employeeLogin.StatusGUID = Common.ToString(Guid.NewGuid());
                    if (authorityMan.InsertEmployeeLogin(employeeLogin) > 0)
                    {
                        loginResponse.result = VAResult.VA_OK;
                        loginResponse.userName = login.userName;
                        loginResponse.statusGUID = employeeLogin.StatusGUID;
                        loginResponse.employeeID = Common.ToInt32(dvEmployee[0]["EmployeeID"]);
                        loginResponse.isViewAllocWorker = Common.ToBool(dvEmployee[0]["isViewAllocWorker"]);
                        //if (Common.ToBool(dvEmployee[0]["canQuickCheckout"]))
                        //{
                        //    loginResponse.canQuickCheckout = 1;
                        //} 
                        //else
                        //{
                        //    loginResponse.canQuickCheckout = 0;
                        //}
                        //if (Common.ToBool(dvEmployee[0]["canClearTable"]))
                        //{
                        //    loginResponse.canClearTable = 1;
                        //}
                        //else
                        //{
                        //    loginResponse.canClearTable = 0;
                        //}
                        //if (Common.ToBool(dvEmployee[0]["canWeigh"]))
                        //{
                        //    loginResponse.canWeigh = 1;
                        //}
                        //else
                        //{
                        //    loginResponse.canWeigh = 0;
                        //}
                        EmployeeConnShopOperate employeeConnShopOpe = new EmployeeConnShopOperate();
                        loginResponse.employeeShop = employeeConnShopOpe.QueryEmployeeShop(loginResponse.employeeID, true);
                        //string log = login.userName + " " + "员工登录 " + System.DateTime.Now.ToString();
                        //Common.AddOrderLog(log);//向日志表插入记录信息
                        //loginResponse.isCashierAuthority = ZZBPreOrderOperate.IsHaveCheckPreOrder(Common.ToInt32(dvEmployee[0]["EmployeeID"]));
                    }
                    else
                    {
                        loginResponse.result = VAResult.VA_FAILED_DB_ERROR;
                    }
                }
                else
                {
                    loginResponse.result = VAResult.VA_LOGIN_PASSWORD_INCORRECT;
                }
            }
            else
            {
                loginResponse.result = VAResult.VA_LOGIN_USER_NOT_EXIST;
            }
            return loginResponse;
        }
        /// <summary>
        /// 员工注销
        /// </summary>
        /// <param name="logout"></param>
        /// <returns></returns>
        public VAEmployeeLogoutResponse EmployeeLogout(VAEmployeeLogout logout)
        {
            VAEmployeeLogoutResponse logoutResponse = new VAEmployeeLogoutResponse();
            logoutResponse.type = VAMessageType.EMPLOYEE_LOGOUT_RESPONSE;
            if (IsEmployeeLogin(logout.userName, logout.statusGUID))
            {//判断用户是否登录，是则进行注销操作
                AuthorityManager authorityMan = new AuthorityManager();
                if (authorityMan.UpdateLoginStatus(logout.statusGUID))
                {
                    logoutResponse.result = VAResult.VA_OK;
                    //string log = logout.userName + " " + "注销 " + System.DateTime.Now.ToString();
                    //Common.AddOrderLog(log);//向日志表插入记录信息
                }
                else
                {
                    logoutResponse.result = VAResult.VA_FAILED_DB_ERROR;
                }
            }
            else
            {
                logoutResponse.result = VAResult.VA_FAILED_LOGIN_STATUS_ERROR;
            }
            return logoutResponse;
        }
        /// <summary>
        /// 根据员工登录名查询对应权限
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public DataTable QueryEmployeeAuthortiy(string userName)
        {
            AuthorityManager authorityMan = new AuthorityManager();
            DataTable dtEmployeeAuthortiy = authorityMan.SelectAuthority(userName);
            DataView dvEmployeeAuthortiy = dtEmployeeAuthortiy.DefaultView;
            dvEmployeeAuthortiy.Sort = "AuthoritySequence ASC,AuthorityName ASC";
            return dvEmployeeAuthortiy.ToTable();
        }
        /// <summary>
        /// （2013-7-26 wangcheng）根据员工登录名查询员工信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string QueryEmployeeDefaultPage(string userName)
        {
            AuthorityManager authorityMan = new AuthorityManager();
            return authorityMan.SelectDefaultPage(userName);
        }
        /// <summary>
        /// 根据员工登录名查询抹零金额
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public double QueryEmployeeRemoveChangeMaxValue(string userName)
        {
            double removeChangeMaxValue = 0;
            AuthorityManager authorityMan = new AuthorityManager();
            DataTable dtEmployee = authorityMan.SelectEmployee();
            DataRow[] drEmployee = dtEmployee.Select("UserName='" + userName + "'");
            if (drEmployee.Length > 0)
            {
                removeChangeMaxValue = Common.ToDouble(drEmployee[0]["RemoveChangeMaxValue"]);
            }
            return removeChangeMaxValue;
        }
        /// <summary>
        /// 注册
        /// 同时生成默认门店、默认菜谱
        /// </summary>
        /// <param name="dtEmployee"></param>
        /// <param name="employeeInfo"></param>
        /// <param name="companyInfo"></param>
        /// <param name="shopInfo"></param>
        /// <param name="vAMenu"></param>
        /// <returns></returns>
        public FunctionResult Register(DataTable dtEmployee, CompanyInfo companyInfo, ShopInfo shopInfo, VAMenu vAMenu, ref string password)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                FunctionResult functionResult = new FunctionResult();
                int employeeId = Common.ToInt32(dtEmployee.Rows[0]["EmployeeID"]);
                password = Common.randomStrAndNum(6);
                // int employeeId = AddEmployee(employeeInfo);
                bool modifyFlag = ModifyEmployeeStatus(employeeId, MD5Operate.getMd5Hash(password));
                bool allInsertPass = true;
                if (modifyFlag == true)
                {
                    CompanyOperate companyOperate = new CompanyOperate();
                    int newCompanyId = companyOperate.AddCompany(companyInfo);
                    if (newCompanyId > 0)
                    {
                        ShopOperate shopOperate = new ShopOperate();
                        //店铺的经纬度
                        List<ShopCoordinate> shopCoordinateList = new List<ShopCoordinate>();
                        for (int i = 0; i < 2; i++)
                        {//目前服务器支持两种地图坐标（百度和谷歌）
                            ShopCoordinate shopCoordinate = new ShopCoordinate();
                            if (i == 0)
                            {
                                shopCoordinate.latitude = 0;
                                shopCoordinate.longitude = 0;
                                shopCoordinate.mapId = 1;//谷歌地图编号，暂时固定
                            }
                            else
                            {
                                shopCoordinate.latitude = 0;
                                shopCoordinate.longitude = 0;
                                shopCoordinate.mapId = 2;//百度地图编号，暂时固定
                            }
                            shopCoordinateList.Add(shopCoordinate);
                        }
                        shopInfo.companyID = newCompanyId;
                        int shopId = shopOperate.AddShop(shopInfo, shopCoordinateList);
                        if (shopId > 0)
                        {
                            //给这个用户对这个公司的操作权限
                            EmployeeConnShopOperate employeeShopOperate = new EmployeeConnShopOperate();
                            EmployeeConnShop employeeShop = new EmployeeConnShop();
                            employeeShop.shopID = shopId;
                            employeeShop.companyID = newCompanyId;
                            employeeShop.employeeID = employeeId;
                            if (employeeShopOperate.AddEmployeeShop(employeeShop))
                            {
                                List<MenuConnShop> listMenuConnShop = new List<MenuConnShop>();
                                MenuOperate menuOperate = new MenuOperate();
                                MenuConnShop menuConnShop = new MenuConnShop();
                                menuConnShop.shopId = shopId;
                                menuConnShop.companyId = newCompanyId;
                                listMenuConnShop.Add(menuConnShop);
                                FunctionResult functionResultMenu = menuOperate.AddMenuAndMenuShop(vAMenu, listMenuConnShop);
                                if (functionResultMenu.returnResult > 0)
                                {
                                    //给用户赋予一个角色
                                    EmployeeRole employeeRole = new EmployeeRole();
                                    employeeRole.EmployeeID = Common.ToInt32(employeeId);
                                    employeeRole.EmployeeRoleStatus = 1;
                                    employeeRole.RoleID = 11;//分店店长
                                    EmployeeRoleOperate employeeRoleOperate = new EmployeeRoleOperate();
                                    bool chooseRole = employeeRoleOperate.AddEmployeeRole(employeeRole);
                                    if (chooseRole)
                                    {
                                        functionResult.returnResult = 1;
                                        functionResult.message = "";
                                        allInsertPass = true;
                                    }
                                    else
                                    {
                                        functionResult.returnResult = 0;
                                        functionResult.message = "用户角色创建失败";
                                        allInsertPass = false;
                                    }
                                }
                                else
                                {
                                    functionResult.returnResult = 0;
                                    functionResult.message = "默认菜谱创建失败";
                                    allInsertPass = false;
                                }

                            }
                            else
                            {
                                functionResult.returnResult = 0;
                                functionResult.message = "公司用户绑定失败";
                                allInsertPass = false;
                            }
                        }
                        else
                        {
                            functionResult.returnResult = 0;
                            functionResult.message = "默认店铺添加失败";
                            allInsertPass = false;
                        }

                    }
                    else
                    {
                        functionResult.returnResult = 0;
                        functionResult.message = "公司信息添加失败";
                        allInsertPass = false;
                    }
                }
                else
                {
                    functionResult.returnResult = 0;
                    functionResult.message = "用户基本信息添加失败";
                    allInsertPass = false;
                }
                //如果一切通过，则执行
                if (allInsertPass)
                {
                    scope.Complete();
                }
                return functionResult;
            }
        }
        #region SYB 修改密码
        /// <summary>
        /// 收银宝修改密码
        /// </summary>
        /// <param name="currectPassword">当前密码</param>
        /// <param name="newPassword">新密码</param>
        /// <param name="newConfrimPassword">原始密码</param>
        /// <returns>1:修改密码成功;-1:修改密码失败;-2:确认新密码错误;-3:新密码不能为空;-4:原始密码错误</returns>
        /// 
        public static string ResertPassword(VAEmployeeLoginResponse vAEmployeeLoginResponse, string currectPassword, string newPassword, string newConfrimPassword)
        {
            SybMsg sybMsg = new SybMsg();
            try
            {
                string userName = vAEmployeeLoginResponse.userName;
                string password = MD5Operate.getMd5Hash(currectPassword.Trim());
                int employeeID = vAEmployeeLoginResponse.employeeID;
                //根据用户名和用户输入的密码，查询用户的基本信息
                EmployeeOperate employeeOperate = new EmployeeOperate();
                VAEmployeeLogin vAEmployeeLogin = new VAEmployeeLogin();
                vAEmployeeLogin.userName = userName;
                vAEmployeeLogin.password = password;
                VAEmployeeLoginResponse vAEmployeeLoginResponseNew = employeeOperate.EmployeeLogin(vAEmployeeLogin);
                if (vAEmployeeLoginResponseNew.result == VAResult.VA_OK)
                {
                    if (newPassword.Trim() != "")
                    {
                        if (newConfrimPassword.Trim() == newPassword.Trim())
                        {
                            bool i = employeeOperate.ModifyEmployeePwd(employeeID, MD5Operate.getMd5Hash(newConfrimPassword.Trim()));
                            if (i)
                            {
                                Common.RecordEmployeeOperateLogBySYB((int)VAEmployeeOperateLogOperatePageType.PASSWORD_MODIFY, (int)VAEmployeeOperateLogOperateType.UPDATE_OPERATE, "悠先收银修改密码为" + MD5Operate.getMd5Hash(newConfrimPassword));
                                sybMsg.Insert(1, "修改密码成功");
                            }
                            else
                            {
                                sybMsg.Insert(-1, "修改密码失败");
                            }
                        }
                        else
                        {
                            sybMsg.Insert(-2, "确认新密码错误");
                        }
                    }
                    else
                    {
                        sybMsg.Insert(-3, "新密码不能为空");
                    }
                }
                else
                {
                    sybMsg.Insert(-4, "原始密码错误");
                }
            }
            catch
            {
                sybMsg.Insert(-1, "修改密码失败");
            }
            return sybMsg.Value;
        }
        #endregion
        /// <summary>
        /// 收银宝查询服务员角色
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public string QueryWaiterRole(int employeeId)
        {
            int shopId = Common.ToInt32(HttpContext.Current.Session["loginshop"]);
            if (shopId > 0)
            {
                AuthorityManager authorityMan = new AuthorityManager();

                #region 收银宝

                //List<WaiterRoleInfo> list = new List<WaiterRoleInfo>();
                //DataTable dtWaiterDefaultRole = authorityMan.SelectWaiterDefautRole();
                //if (dtWaiterDefaultRole.Rows.Count > 0)
                //{
                //    for (int i = 0; i < dtWaiterDefaultRole.Rows.Count; i++)
                //    {
                //        WaiterRoleInfo waiterRoleInfo = new WaiterRoleInfo()
                //        {
                //            isHave = false,
                //            roleId = Common.ToInt32(dtWaiterDefaultRole.Rows[i]["RoleID"]),
                //            roleName = Common.ToString(dtWaiterDefaultRole.Rows[i]["RoleName"])
                //        };
                //        list.Add(waiterRoleInfo);
                //    }
                //}
                //if (list.Count > 0)
                //{
                //    DataTable dtWaiterRole = authorityMan.SelectWaiterRole(employeeId, shopId);
                //    if (dtWaiterRole.Rows.Count > 0)
                //    {
                //        for (int i = 0; i < dtWaiterRole.Rows.Count; i++)
                //        {
                //            for (int n = 0; n < list.Count; n++)
                //            {
                //                if (Common.ToInt32(dtWaiterRole.Rows[i]["RoleID"]) == list[n].roleId)
                //                {
                //                    list[n].isHave = true;
                //                }
                //                else
                //                {
                //                    continue;
                //                }
                //            }
                //        }
                //    }
                //}
                List<WaiterRoleInfo> list = authorityMan.GetShopRoleListWithEmployee(shopId, employeeId, 1);
                #endregion

                #region 悠先服务

                List<WaiterRoleInfo> vaServiceList = authorityMan.GetShopRoleListWithEmployee(shopId, employeeId, 2);

                #endregion

                return JsonOperate.JsonSerializer(new WaiterRole { sybRoles = list, vaServiceRoles = vaServiceList });
            }
            else
            {
                SybMsg sysMsg = new SybMsg();
                sysMsg.Insert(-1000, "当前信息不存在");
                return sysMsg.Value;
            }
        }
        /// <summary>
        /// 收银宝更新服务员权限
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="roleID"></param>
        /// <returns></returns>
        [Obsolete("使用UpdateEmployeeShopRole")]
        public string UpdateWaiterRole(int employeeID, int roleID)
        {
            SybMsg message = new SybMsg();
            if (roleID == 0 && employeeID > 0)//表示修改的是是否显示店员管理页面
            {
                return "";
            }
            else
            {
                int shopId = Common.ToInt32(HttpContext.Current.Session["loginshop"]);
                if (shopId > 0)
                {
                    EmployeeRole employeeRole = new EmployeeRole();
                    employeeRole.EmployeeID = employeeID;
                    employeeRole.EmployeeRoleStatus = 1;
                    employeeRole.RoleID = roleID;
                    employeeRole.ShopId = shopId;
                    AuthorityManager authorityMan = new AuthorityManager();
                    DataTable dtEmployeeRole = authorityMan.SelectEmployeeRole();
                    DataView dvEmployeeRole = dtEmployeeRole.DefaultView;
                    dvEmployeeRole.RowFilter = "EmployeeID = '" + employeeRole.EmployeeID + "' and RoleID = '" +
                                               employeeRole.RoleID + "' and ShopId='" + employeeRole.ShopId + "'";
                    if (dvEmployeeRole.Count > 0) //记录存在，执行删除操作
                    {
                        if (authorityMan.DeleteEmployeeRole(employeeRole.EmployeeID, employeeRole.RoleID))
                        {
                            message.Insert(1, "删除成功");
                        }
                        else
                        {
                            message.Insert(1, "删除失败");
                        }
                    }
                    else //记录不存在，执行添加操作
                    {
                        if (authorityMan.InsertEmployeeRole(employeeRole) > 0)
                        {
                            message.Insert(1, "添加成功");
                        }
                        else
                        {
                            message.Insert(-1, "添加失败");
                        }
                    }
                }
                else
                {
                    message.Insert(-1000, "信息不存在");
                }
            }
            return message.Value;
        }

        public string UpdateEmployeeShopRole(int employeeID, int roleID)
        {
            SybMsg message = new SybMsg();
            int shopId = Common.ToInt32(HttpContext.Current.Session["loginshop"]);
            if (shopId > 0)
            {
                EmployeeConnShopManager employeeConnShopManager = new EmployeeConnShopManager();
                var config = employeeConnShopManager.GetEmployeeInShopConn(shopId, employeeID);
                if (config != null)
                {
                    EmployeeShopAuthorityManager employeeShopAuthorityManager = new EmployeeShopAuthorityManager();

                    var employeeShopAuthority = employeeShopAuthorityManager.GetEmployeeShopAuthority(config.employeeShopID, roleID);
                    if (employeeShopAuthority == null)
                    {
                        employeeShopAuthorityManager.Insert(new EmployeeShopAuthority()
                        {
                            employeeConnShopId = config.employeeShopID,
                            shopAuthorityId = roleID,
                            employeeShopAuthorityStatus = 1
                        });
                    }
                    else
                    {
                        employeeShopAuthorityManager.Delete(config.employeeShopID, roleID);
                    }
                }
                else
                {
                    message.Insert(-1000, "信息不存在");
                }
            }
            else
            {
                message.Insert(-1000, "信息不存在");
            }
            return message.Value;
        }

        /// <summary>
        /// 根据手机号码查询员工信息 add by wangc
        /// </summary>
        /// <param name="mobilePhoneNumber"></param>
        /// <returns></returns>
        public DataTable QueryEmployeeByMobilephone(string mobilePhoneNumber, bool flag = false)
        {
            AuthorityManager authorityMan = new AuthorityManager();
            DataTable dtEmployee = authorityMan.SelectEmployeeByMobilephone(mobilePhoneNumber, flag);
            return dtEmployee;
        }
        /// <summary>
        /// 用户注册修改员工状态和密码（wangc 20140117 注册模块）
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ModifyEmployeeStatus(int employeeId, string password)
        {
            AuthorityManager authorityMan = new AuthorityManager();
            return authorityMan.UpdateEmployeeStatus(employeeId, password) > 0 ? true : false;
        }
        /// <summary>
        /// 根据登录名和状态码判断用户是否登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="statusGUID"></param>
        /// <returns></returns>
        public static bool IsEmployeeLogin(string userName, string statusGUID)
        {
            AuthorityManager authorityMan = new AuthorityManager();
            DataTable authority = authorityMan.SelectEmployeeLogin(userName, statusGUID);
            if (1 == authority.Rows.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 判断当前用户是否可以登录后台
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool IsSupportLoginBgSYS(string userName)
        {

            AuthorityManager authorityMan = new AuthorityManager();
            DataTable dtEmployee = authorityMan.SelectEmployee();
            DataView dvEmployee = dtEmployee.DefaultView;
            dvEmployee.RowFilter = "UserName = '" + userName.Trim() + "'";
            return Common.ToBool(dvEmployee[0]["isSupportLoginBgSYS"]);
        }
        /// <summary>
        /// 查询员工信息 add by wangc 20140325
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public DataTable QueryEmployeeByEmployeeId(int employeeId)
        {
            AuthorityManager man = new AuthorityManager();
            return man.SelectEmployee(employeeId);
        }
        /// <summary>
        /// 查询客户经理信息 add by wangc 20140328
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public List<PartEmployee> GetPartEmployeeInfo(string str)
        {
            AuthorityManager man = new AuthorityManager();
            return man.GetPartEmployeeInfo(str);
        }
        /// <summary>
        /// 收银宝判断某个用户是否具备某个页面权限 add by wangc 20140515
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="authortiyUrl"></param>
        /// <returns></returns>
        public bool CheckEmployeeAuthortiy(string userName, string authortiyUrl)
        {
            AuthorityManager authorityMan = new AuthorityManager();
            return authorityMan.CheckEmployeeAuthortiy(userName, authortiyUrl);
        }

        /// <summary>
        /// 搜索数个人员信息
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public DataTable SelectEmployeeInfoByemployeeIds(string employeeId)
        {
            AuthorityManager authorityMan = new AuthorityManager();
            return authorityMan.SelectEmployeeInfoByemployeeIds(employeeId);
        }
    }
}
