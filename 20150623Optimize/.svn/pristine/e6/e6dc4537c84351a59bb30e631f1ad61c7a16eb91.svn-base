using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Transactions;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 菜显示分类操作类
    /// </summary>
    /// <returns></returns>
    public class DishTypeOperate
    {
        /// <summary>
        /// 查询显示分类信息
        /// </summary>
        /// <returns></returns>
        public DataTable QueryDishTypeInfo()
        {
            DishTypeManager dishTypeMan = new DishTypeManager();
            DataTable dtDishType = dishTypeMan.QueryDishType();
            DataView dvDishType = dtDishType.DefaultView;
            dvDishType.Sort = "DishTypeSequence ASC, DishTypeName ASC";
            return dvDishType.ToTable();
        }
        public DataTable QueryDishTypeInfo(int menuid)
        {
            DishTypeManager dishTypeMan = new DishTypeManager();
            DataTable dtDishType = dishTypeMan.QueryDishType(menuid);
            return dtDishType;
        }
        /// <summary>
        /// 增加显示分类
        /// </summary>
        /// <param name="addDishType"></param>
        /// <returns></returns>
        public int AddDishType(VADishType addDishType)
        {
            DishTypeManager dishTypeMan = new DishTypeManager();

            DataTable dtDishType = dishTypeMan.QueryDishType();
            DataTable dtDishTypeCopy = dtDishType.Copy();
            DataView dvDishType = dtDishType.DefaultView;//判断传入的DishTypeID是否存在
            dvDishType.RowFilter = "DishTypeID = '" + addDishType.dishTypeID + "'";
            int dishTypeID = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                if (dvDishType.Count == 0)
                {//如果传入的DishTypeID不存在，则新增，否则直接写入多语言表
                    DishTypeInfo dishType = new DishTypeInfo();
                    dishType.MenuID = addDishType.menuID;
                    dishType.DishTypeSequence = addDishType.dishTypeSequence;
                    dishType.DishTypeStatus = 1;
                    dishTypeID = dishTypeMan.InsertDishType(dishType);//插入DishTypeInfo表基本信息
                }
                if (dishTypeID > 0 || dvDishType.Count > 0)
                {//DishTypeInfo表插入成功或者该DishTypeID已存在，则直接向DishTypeI18n表中插入多语言详细信息
                    DataView dvDishTypeCopy = dtDishTypeCopy.DefaultView;
                    dvDishTypeCopy.RowFilter = "DishTypeName = '" + addDishType.dishTypeName + "' and MenuID = '" + addDishType.menuID + "'";
                    if (dvDishTypeCopy.Count > 0 || addDishType.dishTypeName == "" || addDishType.dishTypeName == null)
                    {//如果所加DishType信息的名称为空，或者该菜单的DishType信息表中已有该名称的DishType，则返回false
                        dishTypeID = 0;
                    }
                    else
                    {
                        DishTypeI18n dishTypeI18n = new DishTypeI18n();
                        dishTypeI18n.DishTypeID = dishTypeID;
                        dishTypeI18n.LangID = addDishType.langID;
                        dishTypeI18n.DishTypeName = addDishType.dishTypeName;
                        dishTypeI18n.DishTypeI18nStatus = 1;
                        if (dishTypeMan.InsertDishTypeI18n(dishTypeI18n) > 0)
                        {//插入DishTypeI18n表成功，则返回true
                            scope.Complete();
                        }
                        else
                        {
                            dishTypeID = 0;
                        }
                    }
                }
                else
                {

                }
            }
            return dishTypeID;
        }
        /// <summary>
        /// 删除显示分类
        /// </summary>
        /// <returns></returns>
        public bool RemoveDishType(int dishTypeID)
        {
            DishTypeManager dishTypeMan = new DishTypeManager();
            DataTable dtDishType = dishTypeMan.QueryDishType();
            DataView dvDishType = dtDishType.DefaultView;
            dvDishType.RowFilter = "DishTypeID = '" + dishTypeID + "'";
            if (dvDishType.Count > 0)
            {//判断此dishTypeID是否存在，是则删除
                if (dishTypeMan.DeleteDishType(dishTypeID))
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
        /// 修改显示分类
        /// </summary>
        /// <returns></returns>
        public bool ModifyDishType(VADishType modifyDishType)
        {
            DishTypeManager dishTypeMan = new DishTypeManager();
            DataTable dtDishType = dishTypeMan.QueryDishType();
            DataTable dtDishTypeCopy = dtDishType.Copy();
            DataView dvDishType = dtDishType.DefaultView;//查询过滤显示分类信息，判断是否存在该分类
            dvDishType.RowFilter = "DishTypeID = '" + modifyDishType.dishTypeID + "'";
            DataView dvDishTypeCopy = dtDishTypeCopy.DefaultView;//过滤当前菜谱中是否存在所修改的显示分类名称
            dvDishTypeCopy.RowFilter = "DishTypeID <> '" + modifyDishType.dishTypeID + "' and DishTypeName = '"
                + modifyDishType.dishTypeName + "' and MenuID = '" + modifyDishType.menuID + "'";
            if (dvDishType.Count > 0 && modifyDishType.dishTypeName != ""
                && modifyDishType.dishTypeName != null && 0 == dvDishTypeCopy.Count)
            {//判断此DishTypeID存在,修改内容的名称不为空且传入的显示分类名称在当前菜谱已存在（除了修改的项外），则修改
                DishTypeInfo dishType = new DishTypeInfo();
                dishType.DishTypeID = modifyDishType.dishTypeID;
                dishType.DishTypeSequence = modifyDishType.dishTypeSequence;
                dishType.MenuID = modifyDishType.menuID;
                dishType.DishTypeStatus = 1;
                if (dishTypeMan.UpdateDishType(dishType))
                {//如果更新DishTypeInfo基本信息成功，则更新显示分类的多语言表
                    DishTypeI18n dishTypeI18n = new DishTypeI18n();
                    dishTypeI18n.DishTypeI18nID = modifyDishType.dishTypeI18nID;
                    dishTypeI18n.DishTypeI18nStatus = 1;
                    dishTypeI18n.DishTypeID = modifyDishType.dishTypeID;
                    dishTypeI18n.DishTypeName = modifyDishType.dishTypeName;
                    dishTypeI18n.LangID = modifyDishType.langID;
                    if (dishTypeMan.UpdateDishTypeI18n(dishTypeI18n))
                    {//更新显示分类多语言表成功，则返回true
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
            else
            {
                return false;
            }
        }
        #region SYB模块
        /// <summary>
        /// 收银宝添加菜谱分类信息
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="dishTypeSequence"></param>
        /// <param name="dishTypeName"></param>
        /// <returns></returns>
        public static string InsertDishType(int menuId, int dishTypeSequence, string dishTypeName)
        {
            SybMsg sysMsg = new SybMsg();
            if (menuId <= 0)
            {
                sysMsg.Insert(-3, "参数传递失败");
                return sysMsg.Value;
            }
            if (dishTypeSequence <= 0)
            {
                sysMsg.Insert(-3, "请输入不小于1的整数排序号");
                return sysMsg.Value;
            }
            if (String.IsNullOrEmpty(dishTypeName.Trim()))
            {
                sysMsg.Insert(-3, "分类名称不能为空");
                return sysMsg.Value;
            }

            using (TransactionScope tScop = new TransactionScope())
            {
                if (DishTypeManager.CheckDishTypeName(menuId, dishTypeName))//判断当前菜谱当前分类名称是否重复
                {
                    DishTypeInfo modelDishTypeInfo = new DishTypeInfo
                    {
                        MenuID = menuId,
                        DishTypeSequence = dishTypeSequence,
                        DishTypeStatus = 1
                    };
                    DishTypeManager dishMan = new DishTypeManager();
                    int dishTypeID = dishMan.InsertDishType(modelDishTypeInfo);
                    #region
                    if (dishTypeID > 0)
                    {
                        //多语言表
                        DishTypeI18n modelDishTypeI18n = new DishTypeI18n
                        {
                            DishTypeI18nStatus = 1,
                            DishTypeID = dishTypeID,
                            DishTypeName = dishTypeName,
                            LangID = 1
                        };
                        if (dishMan.InsertDishTypeI18n(modelDishTypeI18n) > 0)
                        {
                            tScop.Complete();
                            sysMsg.Insert(1, Common.ToString(dishTypeID));
                        }
                        else
                        {
                            sysMsg.Insert(-1, "添加分类名称失败");
                        }
                    }
                    else
                    {
                        sysMsg.Insert(-1, "添加分类名称失败");
                    }
                    #endregion
                }
                else
                {
                    sysMsg.Insert(-2, "分类名称重复");
                }
                return sysMsg.Value;
            }
        }

        /// <summary>
        /// 收银宝修改菜谱分类信息
        /// </summary>
        /// <param name="menuId">菜谱ID</param>
        /// <param name="dishTypeID">菜谱分类ID</param>
        /// <param name="dishTypeSequence">菜谱分类排序序号</param>
        /// <param name="dishTypeName">菜谱分类名称</param>
        /// <returns></returns>
        public static string UpdateDishType(int menuId, int dishTypeID, int dishTypeSequence, string dishTypeName)
        {
            SybMsg sysMsg = new SybMsg();
            if (menuId <= 0 || dishTypeID <= 0)
            {
                sysMsg.Insert(-3, "参数传递失败");
                return sysMsg.Value;
            }
            if (dishTypeSequence <= 0)
            {
                sysMsg.Insert(-3, "请输入不小于1的整数排序号");
                return sysMsg.Value;
            }
            if (string.IsNullOrEmpty(dishTypeName.Trim()))
            {
                sysMsg.Insert(-3, "分类名称不能为空");
                return sysMsg.Value;
            }
            using (TransactionScope tScop = new TransactionScope())
            {
                if (DishTypeManager.CheckUpdateDishTypeName(menuId, dishTypeName, dishTypeID))//判断当前菜谱当前分类名称是否重复
                {
                    //菜谱分类表
                    DishTypeInfo modelDishTypeInfo = new DishTypeInfo
                    {
                        MenuID = menuId,
                        DishTypeSequence = dishTypeSequence,
                        DishTypeStatus = 1,
                        DishTypeID = dishTypeID
                    };
                    //菜谱分类多语言表
                    DishTypeI18n modelDishTypeI18n = new DishTypeI18n
                    {
                        DishTypeID = dishTypeID,
                        DishTypeName = dishTypeName,
                    };
                    DishTypeManager dishTypeMan = new DishTypeManager();

                    //菜谱分类表中更改 排序 ，多语言表中更改 名称
                    if (dishTypeMan.UpdateDishType(modelDishTypeInfo) && dishTypeMan.UpdateDishName(modelDishTypeI18n))
                    {
                        tScop.Complete();
                        sysMsg.Insert(1, "修改分类名称成功");
                    }
                    else
                    {
                        sysMsg.Insert(-1, "修改分类名称失败");
                    }
                }
                else
                {
                    sysMsg.Insert(-2, "分类名称重复");
                }
                return sysMsg.Value;
            }
        }
        /// <summary>
        /// 删除菜谱分类信息
        /// </summary>
        /// <param name="dishTypeID"></param>
        /// <returns></returns>
        public static string DeleteDishType(int dishTypeID)
        {
            using (TransactionScope tScop = new TransactionScope())
            {
                SybMsg sybMsg = new SybMsg();
                DishTypeManager dishTypeMan = new DishTypeManager();
                if (DishTypeManager.CheckDishTypeCanDel(dishTypeID))
                {
                    if (dishTypeMan.DeleteDishType(dishTypeID))
                    {
                        tScop.Complete();
                        sybMsg.Insert(1, "删除成功");
                    }
                    else
                    {
                        sybMsg.Insert(-1, "删除失败");
                    }
                }
                else
                {
                    sybMsg.Insert(-2, "该类型含有菜品，请先删除菜品信息");
                }
                return sybMsg.Value;
            }
        }
        /// <summary>
        /// 根据DishTypeID查找对应的Sequence
        /// </summary>
        /// <param name="dishTypeID"></param>
        /// <returns>DishTypeSequence</returns>
        public static string QueryDishSequence(int dishTypeID)
        {
            DishTypeManager dishTypeMan = new DishTypeManager();
            return SysJson.JsonSerializer(dishTypeMan.QueryDishSequence(dishTypeID).ToString());
        }
        /// <summary>
        /// 删除菜品分类关联表关联信息 add by wangc 20140416
        /// </summary>
        /// <param name="dishId">菜品编号</param>
        /// <returns></returns>
        public bool DeleteDishConnTypeStatus(int dishId)
        {
            DishTypeManager dishTypeMan = new DishTypeManager();
            return dishTypeMan.UpdateDishConnTypeStatus(dishId);
        }
        #endregion
    }
}
