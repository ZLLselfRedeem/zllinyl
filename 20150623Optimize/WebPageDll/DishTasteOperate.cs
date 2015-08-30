using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 功能描述:菜的口味
    /// 创建标识:罗国华 20131030
    /// </summary>
    public class DishTasteOperate
    {
        public static DishTaste GetModel(int tasteId)
        {
            return DishTasteManager.GetModel(tasteId);
        }


        /// <summary>
        /// 添加口味名称
        /// 修改标识:罗国华20131112
        /// 功能描述:添加口味条件(相同名称时不允许添加)
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="tasteName"></param>
        /// <returns></returns>
        public static string Insert(int menuId, string tasteName)
        {
            DishTaste model = new DishTaste
            {
                menuId = menuId,
                tasteName = tasteName,
                tasteRemark = "",
                tasteSequence = 0,
                tasteStatus = true
            };
            SybMsg sybMsg = new SybMsg();

            //添加条件判断
            var errMsgAdd = "";
            var isAddError = false;
            if (DishTasteManager.Exit(menuId, tasteName))
            {
                errMsgAdd = tasteName;
                isAddError = true;
            }
            if (isAddError) errMsgAdd = errMsgAdd.Substring(0, errMsgAdd.Length - 1) + " 配菜分类中已存在；";

            if (isAddError)
            {
                sybMsg.Insert(-2, errMsgAdd);
                return sybMsg.Value;
            }

            int val = DishTasteManager.Insert(model);
            if (val > 0)
            {
                sybMsg.Insert(1, val.ToString());
            }
            else
            {
                sybMsg.Insert(-1, "添加口味失败");
            }
            return sybMsg.Value;
        }

        /// <summary>
        /// 修改标识:罗国华20131112
        /// 功能描述:删除口味条件(口味被引用时不允许删除)
        /// </summary>
        /// <param name="tasteId"></param>
        /// <returns></returns>
        public static string Del(int tasteId)
        {
            SybMsg sybMsg = new SybMsg();
            //删除条件判断
            var errMsgDel = "";
            var isDelError = false;
            if (DishPriceConnTasteManager.Exit(tasteId))
            {
                errMsgDel = "删除失败，已被菜品引用";
                isDelError = true;
            }

            if (isDelError)
            {
                sybMsg.Insert(-2, errMsgDel);
                return sybMsg.Value;
            }

            if (DishTasteManager.Del(tasteId))
            {
                sybMsg.Insert(1, "删除口味成功");
            }
            else
            {
                sybMsg.Insert(-1, "删除口味失败");
            }
            return sybMsg.Value;
        }

        public static bool Update(DishTaste model)
        {
            return DishTasteManager.Update(model);
        }
        /// <summary>
        /// 修改口味名称
        /// </summary>
        /// <param name="tasteId"></param>
        /// <param name="tasteName"></param>
        /// <returns></returns>
        public static string UpdatetasteName(int menuId, int tasteId, string tasteName)
        {
            SybMsg sybMsg = new SybMsg();

            if (!DishTasteManager.Exist(menuId, tasteName, tasteId))//先判断新口味与剩余口味是否重复
            {
                if (DishTasteManager.UpdatetasteName(tasteId, tasteName))
                {
                    sybMsg.Insert(1, "修改口味成功");
                }
                else
                {
                    sybMsg.Insert(-1, "修改口味失败");
                }
            }
            else
            {
                sybMsg.Insert(-2, "口味名称不能重复");
            }
            return sybMsg.Value;
        }
    }
}