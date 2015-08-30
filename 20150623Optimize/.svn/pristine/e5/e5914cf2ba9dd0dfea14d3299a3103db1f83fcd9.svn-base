<%@ WebHandler Language="C#" Class="doSybWeb" %>

using System;
using System.Web;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;
using System.Collections.Generic;
using System.Web.SessionState;
/// <summary>
/// 功能描述:收银包前台和后台交互页面 
///         例如 doSybWeb.aspx?m=dish_info_editinfo&dishid=1234 
///         说明：m：菜品修改模块（模块参数） dishid代表菜品的Id（功能参数,根据模块不同而不一样，参数个数也会不同）
/// 创建标识:罗国华20131101
/// </summary>
public class doSybWeb : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        string val = "";//返回值，json字符串
        if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
        {
            val = "-1000";
            context.Response.Write(val);
        }
        else
        {
            string module = context.Request["m"] == null ? "" : Common.ToString(context.Request["m"].Trim());

            switch (module)
            {
                /*菜品信息修改页面*************************************/
                case "dish_info_editinfo"://菜品修改信息
                    int dishId = Common.ToInt32(context.Request["dishid"].Trim());
                    if (dishId > 0)
                        val = SybDishInfoOperate.GetDishEditInfo(dishId);
                    break;
                case "dish_info_config"://菜品信息配置项(分类列表，口味列表，配料列表)
                    int shopid = Common.ToInt32(context.Request["shopid"].Trim());
                    int menuId = DishOperate.GetMenuIdByShopId(shopid);
                    if (menuId > 0)
                        val = SybDishInfoOperate.GetDishEditConfig(menuId);
                    break;
                case "dish_info_save"://菜品修改信息保存
                    shopid = Common.ToInt32(context.Request["shopid"].Trim());
                    string oper = Common.ToString(context.Request["oper"].Trim());
                    string strJson = Common.ToString(context.Request["json"].Trim());//菜品json数据 SybDishInfo结构的接送串
                    menuId = DishOperate.GetMenuIdByShopId(shopid);
                    if (menuId > 0)
                    {
                        if (oper == "add")
                            val = SybDishInfoOperate.SaveDishInfo(strJson, OperStatus.Insert, menuId);  //return:  1:保存成功;-1:保存失败;
                        else if (oper == "edit")
                            val = SybDishInfoOperate.SaveDishInfo(strJson, OperStatus.Edit, menuId);
                        else if (oper == "mutil")
                            val = SybDishInfoOperate.SaveDishInfo(strJson, OperStatus.MutInsert, menuId);
                    }
                    break;
                case "modify_dish_get_dishs"://查询菜品的上一道下一道菜品信息 add by wangc 20140416
                    int dishTypeId = Common.ToInt32(context.Request["dishTypeId"]);
                    string searchKey = Common.ToString(context.Request["searchKey"]);
                    dishId = Common.ToInt32(context.Request["dishId"]);
                    val = SybDishInfoOperate.GetDishId(dishTypeId, searchKey, dishId);//return 1成功;-1参数传递有误;-2未找到任何菜品信息;-3当前菜品Id无效
                    break;
                /*口味管理页面*************************************/
                case "dish_info_taste_insert": //添加口味
                    shopid = Common.ToInt32(context.Request["shopid"].Trim());
                    menuId = DishOperate.GetMenuIdByShopId(shopid);
                    string tasteName = Common.ToString(context.Request["tastename"].Trim());//口味名称  
                    if (menuId > 0 && !String.IsNullOrEmpty(tasteName))
                        val = DishTasteOperate.Insert(menuId, tasteName); //return:  1:添加口味成功;-1:添加口味失败 -2 添加口味失败,口味配在菜分类中已存在;
                    break;
                case "dish_info_taste_edit": //修改口味 Modify at 20131227 by jinyanni
                    shopid = Common.ToInt32(context.Request["shopid"].Trim());
                    menuId = DishOperate.GetMenuIdByShopId(shopid);
                    int tasteId = Common.ToInt32(context.Request["tasteid"].Trim()); //口味Id
                    tasteName = Common.ToString(context.Request["tastename"].Trim());//口味名称
                    if (tasteId > 0 && !String.IsNullOrEmpty(tasteName))
                    {
                        val = DishTasteOperate.UpdatetasteName(menuId, tasteId, tasteName); //return:  1:修改口味成功;-1:修改口味失败; -2修改失败(口味重复);
                    }
                    break;
                case "dish_info_taste_delete": //删除口味
                    tasteId = Common.ToInt32(context.Request["tasteid"].Trim()); //口味Id
                    if (tasteId > 0)
                        val = DishTasteOperate.Del(tasteId);//return: 1删除口味成功 -1删除口味失败 -1删除口味失败，已被菜品引用
                    break;

                /*配菜管理页面*************************************/
                case "dish_ingredients_save"://保存配料数据     
                    shopid = Common.ToInt32(context.Request["shopid"].Trim());
                    menuId = DishOperate.GetMenuIdByShopId(shopid);
                    strJson = Common.ToString(context.Request["json"].Trim());//配料的json数据
                    if (menuId > 0 && !String.IsNullOrEmpty(strJson))
                        val = SybDishInfoOperate.SaveDishIngredients(strJson, menuId); //return:  1:保存成功;-1:保存失败;
                    break;
                case "dish_ingredients_query"://查询配料数据
                    shopid = Common.ToInt32(context.Request["shopid"].Trim());
                    menuId = DishOperate.GetMenuIdByShopId(shopid);
                    string ingredientsName = Common.ToString(context.Request["ingredientsname"].Trim());
                    int PageIndex = Common.ToInt32(context.Request["PageIndex"].Trim());
                    int PageSize = Common.ToInt32(context.Request["PageSize"].Trim());
                    if (menuId > 0 && PageIndex > 0 && PageSize > 0)
                        val = SybDishInfoOperate.QueryDishIngredientsInfo(menuId, ingredientsName, PageSize, PageIndex);
                    break;

                /*菜谱分类管理页面*************************************/
                case "dish_typeinfo_insert"://新增菜谱分类信息
                    shopid = Common.ToInt32(context.Request["shopid"].Trim());
                    menuId = DishOperate.GetMenuIdByShopId(shopid);
                    int dishTypeSequence = CommonPageOperate.IsNumber(context.Request["dishTypeSequence"].Trim()) == true ? Common.ToInt32(context.Request["dishTypeSequence"].Trim()) : -1;//菜谱分类排序
                    string dishTypeName = Common.ToString(context.Request["dishTypeName"].Trim());//菜谱分类名称                        
                    val = DishTypeOperate.InsertDishType(menuId, dishTypeSequence, dishTypeName); //return:  1:添加分类名称成功;-1:添加分类名称失败;-2:添加名称重复;
                    break;
                case "dish_typeinfo_delete"://删除菜谱分类信息
                    int dishTypeID = Common.ToInt32(context.Request["dishTypeID"].Trim());//菜谱分类ID
                    if (dishTypeID > 0)
                        val = DishTypeOperate.DeleteDishType(dishTypeID); //return:  1:删除成功;-1:删除失败;
                    break;

                case "dish_typeinfo_update"://更新菜谱分类信息（菜谱名称及排序序号） Add at 2013-12-26 by jinyanni
                    dishTypeID = Common.ToInt32(context.Request["dishTypeID"].Trim());//菜谱分类ID
                    shopid = Common.ToInt32(context.Request["shopid"].Trim());
                    menuId = DishOperate.GetMenuIdByShopId(shopid);
                    dishTypeSequence = CommonPageOperate.IsNumber(context.Request["dishTypeSequence"].Trim()) == true ? Common.ToInt32(context.Request["dishTypeSequence"].Trim()) : -1;//菜谱分类排序序号
                    dishTypeName = Common.ToString(context.Request["dishTypeName"].Trim());//菜谱分类名称
                    if (dishTypeID > 0)
                    {
                        val = DishTypeOperate.UpdateDishType(menuId, dishTypeID, dishTypeSequence, dishTypeName);
                    }
                    break;

                case "dish_typeinfo_querySequence"://根据菜谱分类ID查找其排序号 Add at 2013-12-27 by jinyanni
                    dishTypeID = Common.ToInt32(context.Request["dishTypeID"].Trim());//菜谱分类ID
                    if (dishTypeID > 0)
                    {
                        val = DishTypeOperate.QueryDishSequence(dishTypeID);
                    }
                    break;

                /*密码管理页面*************************************/
                case "resert_password"://修改密码
                    VAEmployeeLoginResponse vAEmployeeLoginResponse = (VAEmployeeLoginResponse)HttpContext.Current.Session["MerchantsTreasureUserInfo"];
                    string currectPassword = Common.ToString(context.Request["currectPassword"].Trim());//原密码
                    string newPassword = Common.ToString(context.Request["newPassword"].Trim());//新密码
                    string newConfrimPassword = Common.ToString(context.Request["newConfrimPassword"].Trim());//确认新密码
                    if (vAEmployeeLoginResponse != null && !String.IsNullOrEmpty(currectPassword) && !String.IsNullOrEmpty(newPassword) && !String.IsNullOrEmpty(newConfrimPassword))
                        val = EmployeeOperate.ResertPassword(vAEmployeeLoginResponse, currectPassword, newPassword, newConfrimPassword); //return:  1:修改密码成功;-1:修改密码失败;-2:确认新密码错误;-3:新密码不能为空;-4:原始密码错误;
                    break;

                /*添加新菜品判断该门店是否存在合法菜谱信息*************************************/
                case "isexists_memu"://保存菜品信息和添加菜品信息时，调用该方法
                    SybMsg sybMsg = new SybMsg();
                    shopid = Common.ToInt32(context.Request["shopid"]);
                    menuId = DishOperate.GetMenuIdByShopId(shopid);
                    if (menuId > 0) sybMsg.Insert(1, "菜谱存在");//前端跳转页面
                    else sybMsg.Insert(-1, "当前门店菜谱不存在，请联系管理员");//前端不跳转页面，提示该错误信息
                    val = sybMsg.Value;
                    break;

                /*批量上传图片*/
                case "dish_image_upload":
                    VAEmployeeLoginResponse session = (VAEmployeeLoginResponse)HttpContext.Current.Session["MerchantsTreasureUserInfo"];
                    if (session != null)
                    {
                        try
                        {
                            shopid = Common.ToInt32(context.Request["shopid"].Trim());
                            menuId = DishOperate.GetMenuIdByShopId(shopid);
                            HttpPostedFile jpeg_image_upload = context.Request.Files["Filedata"];
                            if (jpeg_image_upload != null && jpeg_image_upload.ContentLength > 0)
                            {
                                val = VAGastronomistMobileApp.WebPageDll.Syb.SybMultiImageUploadOperate.Upload(session.employeeID, shopid, menuId, jpeg_image_upload.FileName, jpeg_image_upload.InputStream);
                            }
                        }
                        catch (Exception exc)
                        {
                            val = exc.ToString();
                        }
                    }
                    //if (string.IsNullOrEmpty(val))
                    //{
                    //    val = JsonOperate.JsonSerializer<VAGastronomistMobileApp.WebPageDll.Syb.SybMultiImageUploadOperate.SybMultiImageUploadResponse>(new VAGastronomistMobileApp.WebPageDll.Syb.SybMultiImageUploadOperate.SybMultiImageUploadResponse());
                    //}
                    break;
                case "dish_info_save2"://菜品修改信息保存
                    //int x = Common.ToInt32(context.Request["x"].Trim());
                    //int y = Common.ToInt32(context.Request["y"].Trim());
                    //int w = Common.ToInt32(context.Request["w"].Trim());
                    //int h = Common.ToInt32(context.Request["h"].Trim());
                    //int cw = Common.ToInt32(context.Request["cw"].Trim());
                    //int id = Common.ToInt32(context.Request["id"].Trim());
                    shopid = Common.ToInt32(context.Request["shopid"].Trim());
                    strJson = Common.ToString(context.Request["json"].Trim());//菜品json数据 SybDishInfo结构的接送串
                    menuId = DishOperate.GetMenuIdByShopId(shopid);
                    if (menuId > 0)
                    {
                        val = SybDishInfoOperate.SaveDishInfo(strJson, OperStatus.MutInsert, menuId);  //return:  1:保存成功;-1:保存失败;
                    }
                    //VAGastronomistMobileApp.WebPageDll.Syb.SybMultiImageUploadOperate.Make(1, id, x, y, w, h, cw);
                    break;
                case "untreated_imagetask_count_get"://untreated 上次未处理图片
                    VAEmployeeLoginResponse session1 = (VAEmployeeLoginResponse)HttpContext.Current.Session["MerchantsTreasureUserInfo"];
                    if (session1 != null)
                    {
                        shopid = Common.ToInt32(context.Request["shopid"].Trim());
                        int count = VAGastronomistMobileApp.WebPageDll.Syb.SybMultiImageUploadOperate.GetUntreatedImagetaskCount(session1.employeeID, shopid);
                        val = count.ToString();
                    }
                    break;
                case "untreated_imagetask_get":
                    VAEmployeeLoginResponse session2 = (VAEmployeeLoginResponse)HttpContext.Current.Session["MerchantsTreasureUserInfo"];
                    if (session2 != null)
                    {
                        shopid = Common.ToInt32(context.Request["shopid"].Trim());
                        val = VAGastronomistMobileApp.WebPageDll.Syb.SybMultiImageUploadOperate.GetUntreatedImageTaskList(session2.employeeID, shopid);
                    }
                    break;
                case "untreated_imagetask_delete":
                    VAEmployeeLoginResponse session3 = (VAEmployeeLoginResponse)HttpContext.Current.Session["MerchantsTreasureUserInfo"];
                    if (session3 != null)
                    {
                        shopid = Common.ToInt32(context.Request["shopid"].Trim());
                        val = VAGastronomistMobileApp.WebPageDll.Syb.SybMultiImageUploadOperate.DeleteUntreatedImageTask(session3.employeeID, shopid).ToString();
                    }
                    break;
                case "imagetask_delete"://单张删除
                    int id = Common.ToInt32(context.Request["id"].Trim());
                    val = VAGastronomistMobileApp.WebPageDll.Syb.SybMultiImageUploadOperate.DeleteImageTask(id).ToString();
                    break;
                #region 商家为员工管理分配门店(wangcheng)
                case "business_employees_authority_query"://查询
                    VAEmployeeLoginResponse session12 = (VAEmployeeLoginResponse)HttpContext.Current.Session["MerchantsTreasureUserInfo"];
                    if (session12 != null)
                    {

                        shopid = Common.ToInt32(context.Request["shopId"].Trim());
                        PageIndex = Common.ToInt32(context.Request["PageIndex"].Trim());
                        PageSize = Common.ToInt32(context.Request["PageSize"].Trim());
                        AuthorityOperate operate = new AuthorityOperate();
                        val = operate.SYBQueryEmployeeShop(PageIndex, PageSize, shopid, session12.isViewAllocWorker);//json
                    }

                    break;
                case "business_employees_authority_add"://添加
                    shopid = Common.ToInt32(context.Request["shopId"].Trim());
                    string phoneNum = Common.ToString(context.Request["phoneNum"].Trim());
                    AuthorityOperate add_Operate = new AuthorityOperate();
                    val = add_Operate.SYBAddEmployeeShop(shopid, phoneNum);//return:  1:添加成功;-1:手机号码不能为空;-2:手机号码不存在;-3:门店信息未找到;-4:添加失败;
                    break;
                case "business_employees_authority_delete"://删除
                    AuthorityOperate delete_Operate = new AuthorityOperate();
                    int employeeId = Common.ToInt32(context.Request["employeeId"].Trim());
                    val = delete_Operate.SYBDeleteEmployeeShop(employeeId);//return:  1:删除成功;-1:删除失败;-2:选中员工信息有误;
                    break;
                case "business_employees_authority_enter_syb"://添加可进入收银宝的权限
                    EmployeeOperate update_Operate = new EmployeeOperate();
                    employeeId = Common.ToInt32(context.Request["employeeId"].Trim());
                    int status = Common.ToInt32(context.Request["status"].Trim());
                    val = update_Operate.ModifyEmployee(employeeId, status, "enter_syb");//return:  1:操作成功;-1:操作失败;-2:当前员工信息不存在;
                    break;
                case "business_employees_authority_resert_password"://重置密码
                    employeeId = Common.ToInt32(context.Request["employeeId"].Trim());
                    EmployeeOperate resertPwd_Operate = new EmployeeOperate();
                    val = resertPwd_Operate.SybModifyEmployeePwd(employeeId);//return:1:......;-1:重置密码失败;
                    break;
                #endregion
                case "page_authority_query"://收银宝页面权限控制（店员管理）
                    string currentPageName = Common.ToString(context.Request["pageName"].Trim());
                    AuthorityOperate pageOper = new AuthorityOperate();
                    val = pageOper.SybGetPageNameStr(currentPageName);
                    break;
                case "waiter_role_query"://收银宝/悠先服务 服务员角色权限查询 修改于2014/4/23 @bruke 新增shopid,新增悠先服务权限管理
                    employeeId = Common.ToInt32(context.Request["employeeId"].Trim());
                    //shopid = Common.ToInt32(context.Request["shopid"].Trim());

                    EmployeeOperate waiterRoleQuery = new EmployeeOperate();
                    val = waiterRoleQuery.QueryWaiterRole(employeeId);
                    break;
                case "waiter_role_update":
                    employeeId = Common.ToInt32(context.Request["employeeId"].Trim());
                    int roleId = Common.ToInt32(context.Request["roleId"].Trim());
                    EmployeeOperate waiterRoleUpdate = new EmployeeOperate();
                    val = waiterRoleUpdate.UpdateEmployeeShopRole(employeeId, roleId);
                    break;
                /*
                 20140402 
                 add by wangc
                */
                case "waiter_receive_payorder_msg"://当前门店当前服务员接受短信提醒
                    EmployeeOperate update_Oper = new EmployeeOperate();
                    employeeId = Common.ToInt32(context.Request["employeeId"].Trim());
                    status = Common.ToInt32(context.Request["status"].Trim());
                    val = update_Oper.ModifyEmployee(employeeId, status, "receive_msg");//return:  1:操作成功;-1:操作失败;-2:当前员工信息不存在;
                    break;

                /*
                  配菜操作
                 */
                case "ingredients_selloff":
                    int ingredientsId = Common.ToInt32(context.Request["ingredientId"]);
                    status = Common.ToInt32(context.Request["status"]);
                    val = new CurrectIngredientsSellOffInfoOperate().IngredientsSellOffOperate(ingredientsId, status);
                    break;
                default:
                    break;
            }
            context.Response.Write(val);
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}