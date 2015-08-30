using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.Model;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Configuration;
using System.Web;
using System.IO;
using VAGastronomistMobileApp.WebPageDll.Syb;
using CloudStorage;
using System.Net;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 功能描述:收银宝菜品业务逻辑类
    /// 创建标识:罗国华 20131112
    /// </summary>
    public class SybDishInfoOperate
    {
        /// <summary>
        /// 获取菜品的修改页面的详细信息
        /// </summary>
        /// <param name="dishID"></param>
        /// <returns></returns>
        public static string GetDishEditInfo(int dishID)
        {
            string val = string.Empty;
            SybDishInfo data = DishManager.GetDishInfo(dishID);
            if (data != null)
            {
                string menuImagePath = Common.GetFieldValue("MenuInfo", "menuImagePath", "MenuStatus =1 and MenuID =" + data.menuId + "");
                string serverpath = WebConfig.ImagePath + menuImagePath;

                CloudStorageObject cloudStorageObject = CloudStorageOperate.GetObject(serverpath + data.DishImageUrlBig);// + "@320w_240h_50Q"
                if (cloudStorageObject == null || cloudStorageObject.Content == null)
                {
                    data.DishImageUrlBig = "";
                }
                else
                {
                    data.DishImageUrlBig = MakeUrl(cloudStorageObject.Content);//给前端的图片新名称NewGuid
                }
                val = SysJson.JsonSerializer(data);
            }
            return val;
        }
        /// <summary>
        /// 从session获取图片
        /// </summary>
        /// <param name="stream">从阿里云获取到菜图后转成Stream</param>
        /// <returns></returns>
        public static string MakeUrl(Stream stream)
        {
            try
            {
                System.Drawing.Image original_image = null;
                System.Drawing.Bitmap final_image = null;
                System.Drawing.Graphics graphic = null;
                MemoryStream ms = null;
                original_image = System.Drawing.Image.FromStream(stream);
                int width = original_image.Width;
                int height = original_image.Height;
                //客户端修改图片显示时不缩小 2014-6-10
                int target_width = original_image.Width;
                int target_height = original_image.Height;
                //int target_width = 600;//600
                //int target_height = 450;//450
                int new_width, new_height;

                float target_ratio = (float)target_width / (float)target_height;
                float image_ratio = (float)width / (float)height;

                if (target_ratio > image_ratio)
                {
                    new_height = target_height;
                    new_width = (int)Math.Floor(image_ratio * (float)target_height);
                }
                else
                {
                    new_height = (int)Math.Floor((float)target_width / image_ratio);
                    new_width = target_width;
                }

                new_width = new_width > target_width ? target_width : new_width;
                new_height = new_height > target_height ? target_height : new_height;


                // Create the thumbnail

                // Old way
                //thumbnail_image = original_image.GetThumbnailImage(new_width, new_height, null, System.IntPtr.Zero);
                // We don't have to create a Thumbnail since the DrawImage method will resize, but the GetThumbnailImage looks better
                // I've read about a problem with GetThumbnailImage. If a jpeg has an embedded thumbnail it will use and resize it which
                //  can result in a tiny 40x40 thumbnail being stretch up to our target size


                final_image = new System.Drawing.Bitmap(target_width, target_height);
                graphic = System.Drawing.Graphics.FromImage(final_image);
                graphic.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Black), new System.Drawing.Rectangle(0, 0, target_width, target_height));
                int paste_x = (target_width - new_width) / 2;
                int paste_y = (target_height - new_height) / 2;
                graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic; /* new way */
                //graphic.DrawImage(thumbnail_image, paste_x, paste_y, new_width, new_height);
                graphic.DrawImage(original_image, paste_x, paste_y, new_width, new_height);

                // Store the thumbnail in the session (Note: this is bad, it will take a lot of memory, but this is just a demo)
                ms = new MemoryStream();
                final_image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                // Store the data in my custom Thumbnail object
                //string thumbnail_id = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string thumbnail_id = System.Guid.NewGuid().ToString();
                Thumbnail thumb = new Thumbnail(thumbnail_id, ms.GetBuffer());

                // Put it all in the Session (initialize the session if necessary)			
                List<Thumbnail> thumbnails = HttpContext.Current.Session["file_info"] as List<Thumbnail>;
                if (thumbnails == null)
                {
                    thumbnails = new List<Thumbnail>();
                    HttpContext.Current.Session["file_info"] = thumbnails;
                }
                thumbnails.Add(thumb);

                return thumbnail_id;

            }
            catch (Exception ex)
            {
                //return "";
                return ex.Message;
            }
        }
        /// <summary>
        /// 获取菜品修改页面的配置信息
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public static string GetDishEditConfig(int menuId)
        {
            string val = string.Empty;
            SybDishConfig data = new SybDishConfig();
            data.DishTypeInfoList = DishTypeManager.List(menuId);
            data.DishTasteList = DishTasteManager.List(menuId);
            data.DishIngredientsList = DishIngredientsManager.List(menuId);
            SybDishImage dishImage = new SybDishImage();
            dishImage.size = WebConfig.DishImageWidth + "," + WebConfig.DishImageHeight;
            dishImage.space = WebConfig.DishImageSpace + "KB";
            data.dishImage = dishImage;
            val = SysJson.JsonSerializer(data);
            return val;
        }
        /// <summary>
        /// 查询配菜信息，add by wangcheng
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="ingredientsName"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public static string QueryDishIngredientsInfo(int menuId, string ingredientsName, int pageSize, int pageIndex)
        {
            SybDishIngredientsList initlist = DishManager.ListDishIngredients(menuId, ingredientsName, pageSize, pageIndex);
            List<int> ids = new CurrectIngredientsSellOffInfoOperate().Select();
            SybDishIngredientsListExtend newList = new SybDishIngredientsListExtend()
            {
                page = initlist.page,
                list = (from q in initlist.list
                        select new SybDishIngredientsExtend()
                        {
                            ingredientsId = q.ingredientsId,
                            ingredientsName = q.ingredientsName,
                            ingredientsPrice = q.ingredientsPrice,
                            vipDiscountable = q.vipDiscountable,
                            backDiscountable = q.backDiscountable,
                            ingredientsSequence = q.ingredientsSequence,
                            operStatus = q.operStatus,
                            isSellOff = ids.Contains(q.ingredientsId)
                        }).ToList()
            };
            return JsonOperate.JsonSerializer<SybDishIngredientsListExtend>(newList);
        }

        /// <summary>
        /// 保存菜品修改页面的信息
        /// 修改标识:罗国华 20131113
        /// 功能描述:空的规格不允许提交 菜品名称不允许重复 
        /// 修改标识:罗国华 20161202
        /// 功能描述:规格不能为空，且不能重复
        /// </summary>
        /// <param name="strJson"></param>
        /// <param name="operStatus"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public static string SaveDishInfo(string strJson, OperStatus operStatus, int menuId)
        {
            SybMsg sybMsg = new SybMsg();
            SybDishInfo data = SysJson.JsonDeserialize<SybDishInfo>(strJson);
            if (data == null)
            {
                sybMsg.Insert(-1000, "保存失败！数据格式存在问题！");
                return sybMsg.Value;
            }
            data.menuId = menuId;

            if (operStatus == OperStatus.Insert || operStatus == OperStatus.MutInsert)
            {
                //菜品名称不允许重复
                if (DishManager.ExitDishName(menuId, data.DishName.Trim()))
                {
                    sybMsg.Insert(-3, "保存失败，菜品名称存在重复");
                    return sybMsg.Value;
                }
            }
            else if (operStatus == OperStatus.Edit)
            {
                //菜品名称不允许重复
                if (DishManager.ExitDishName(menuId, data.DishName.Trim(), data.DishID))
                {
                    sybMsg.Insert(-3, "保存失败，菜品名称存在重复");
                    return sybMsg.Value;
                }
            }

            //查询添加和修改的条数
            var list = from p in data.DishPriceList
                       where (p.operStatus == OperStatus.Insert || p.operStatus == OperStatus.Edit)
                       select p;
            if (list.Count() == 0)
            {
                sybMsg.Insert(-2, "保存失败，菜品必须有一个规格");
                return sybMsg.Value;
            }
            List<SybDishPriceInfo> dishPriceInfo = data.DishPriceList;
            foreach (var item in dishPriceInfo)
            {
                if (item.DishPrice < 0)
                {
                    sybMsg.Insert(-99, "菜品价格不能小于0");
                    return sybMsg.Value;
                }
                else
                {
                    continue;
                }
            }
            //规格不能重复
            var listNullScale = from p in data.DishPriceList
                                where ((p.operStatus == OperStatus.Insert || p.operStatus == OperStatus.Edit) && p.ScaleName == "")
                                select p;
            if (listNullScale.Count() > 0)
            {
                sybMsg.Insert(-11, "保存失败，规格不能为空");
                return sybMsg.Value;
            }

            //规格不能重复
            var listRepeatScale = from p in data.DishPriceList
                                  where (p.operStatus == OperStatus.Insert || p.operStatus == OperStatus.Edit)
                                  group p by p.ScaleName into g
                                  from r in g
                                  where (g.Count() > 1)
                                  select r;
            if (listRepeatScale.Count() > 0)
            {
                sybMsg.Insert(-12, "保存失败，规格不能重复");
                return sybMsg.Value;
            }

            //if (data.SessionId != "" && data.SessionId !=null)
            //{
            //    if (data.PicX == 0 && data.PicY == 0 && data.PicWidth == 600 && data.PicHeight == 450)
            //    {
            //        data.PicWidth = 320;
            //        data.PicHeight = 240;
            //    }
            //    if (!(data.PicX == 0 && data.PicY == 0 && data.PicWidth == 600 && data.PicHeight == 450))
            //    {
            //        SaveDishImage(ref data);//保存并处理图片
            //    }
            //}
            //进行图片上传处理
            if (data.PicStatus == 2)
            {
                if (operStatus == OperStatus.MutInsert)
                {
                    object[] obj = SaveDishImageByDisk(ref data);
                    if (!Common.ToBool(obj[0]))
                    {
                        //sybMsg.Insert(-4, "保存图片失败");
                        sybMsg.Insert(-4, obj[1].ToString());
                        return sybMsg.Value;
                    }
                }
                else
                {
                    #region 检查图最小尺寸 2014-6-11
                    List<Thumbnail> thumbnails = HttpContext.Current.Session["file_info"] as List<Thumbnail>;
                    byte[] imgData = null;
                    foreach (Thumbnail thumb in thumbnails)
                    {
                        if (thumb.ID == data.SessionId)
                        {
                            imgData = thumb.Data;
                        }
                    }
                    System.Drawing.Image originalImage = ReturnPhoto(imgData);
                    int ow = originalImage.Width;//session中取出来的原始图片                  
                    int jiemianw = data.PicClientWidth;//客户端界面实际宽度  

                    int pw = 0;
                    int ph = 0;
                    //前端截图后上传的图片宽高忽略了小数点，可能会导致截图小于最小尺寸，此处要处理一下
                    if (Math.Ceiling(Common.ToDouble(jiemianw) * Common.ToDouble(WebConfig.DishImageWidth) / Common.ToDouble(ow)) == data.PicWidth + 1)
                    {
                        pw = Common.ToInt32(WebConfig.DishImageWidth);
                        ph = Common.ToInt32(WebConfig.DishImageHeight);
                    }
                    else
                    {
                        pw = (data.PicWidth * ow) / jiemianw;//换算后图片实际宽度
                        ph = (data.PicHeight * ow) / jiemianw;//换算后图片实际高度
                    }
                    //检查图片最小尺寸
                    if (pw < Common.ToInt32(WebConfig.DishImageWidth) || ph < Common.ToInt32(WebConfig.DishImageHeight))
                    {
                        sybMsg.Insert(-14, "保存失败，图片最小尺寸应为" + WebConfig.DishImageWidth + "*" + WebConfig.DishImageHeight);
                        return sybMsg.Value;
                    }
                    #endregion

                    SaveDishImage(ref data);//保存并处理图片
                }
            }

            if (DishManager.SaveDishInfo(data, operStatus))
            {
                sybMsg.Insert(1, "保存成功");
                #region 添加和编辑菜品信息日志
                string operStr = "菜品基本信息：（全拼：" + data.dishQuanPin.ToString() + "，菜名称：" + data.DishName + "，简拼：" + data.dishJianPin.ToString()
                              + "，菜品编号：" + data.DishID.ToString() + "，简介：" + data.DishDescShort.ToString() + "，详情：" + data.DishDescDetail + "）,规格信息：";
                foreach (var item in data.DishPriceList)
                {
                    operStr += "(规格名称：" + item.ScaleName + ",悠先服务编号：" + item.markName + ",是否售罄：" + item.DishSoldout + ",价格：" + item.DishPrice + "，";
                    operStr += "口味：(";
                    foreach (var tasteItem in item.DishTasteList)
                    {
                        operStr += tasteItem.ToString() + ",";
                    }
                    operStr += ")，";
                    operStr += "配菜：(";
                    foreach (var ingredientsItem in item.DishIngredientsList)
                    {
                        operStr += ingredientsItem.ToString() + ",";
                    }
                    operStr += ")，";
                }
                operStr += "分类信息：（";
                foreach (var item in data.DishTypeList)
                {
                    operStr += item.ToString() + ",";
                }
                operStr += ")";
                if (operStatus == OperStatus.Edit)
                {
                    Common.RecordEmployeeOperateLogBySYB((int)VAEmployeeOperateLogOperatePageType.DISHINFO, (int)VAEmployeeOperateLogOperateType.UPDATE_OPERATE, operStr);
                }
                else
                {
                    Common.RecordEmployeeOperateLogBySYB((int)VAEmployeeOperateLogOperatePageType.DISHINFO, (int)VAEmployeeOperateLogOperateType.ADD_OPERATE, operStr);
                }
                #endregion
            }
            else
            {
                sybMsg.Insert(-1, "保存失败");
            }
            return sybMsg.Value;
        }

        /// <summary>
        /// byte  转image
        /// </summary>
        /// <param name="streamByte"></param>
        /// <returns></returns>
        public static System.Drawing.Image ReturnPhoto(byte[] streamByte)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream(streamByte);
            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            return img;
        }
        /// <summary>
        /// 图片处理在硬盘
        /// </summary>
        /// <param name="data"></param>
        private static object[] SaveDishImageByDisk(ref SybDishInfo data)
        {
            object[] objResult = new object[] { false, "" };//修改日期：2014-6-11 方法由bool改为object[]
            DishOperate dishimageoperate = new DishOperate();
            MenuOperate menuOperate = new MenuOperate();
            if (data.DishID != 0)
            {
                DataTable dtImage = dishimageoperate.SelectDishImageInfo(data.DishID);
                if (dtImage.Rows.Count > 0)
                {
                    dishimageoperate.RemoveDishImagebyDishID(data.DishID);
                    string imagePath = WebConfig.ImagePath + menuOperate.QueryMenuImagePath(data.menuId);
                    string objectKey = "";
                    for (int i = 0; i < dtImage.Rows.Count; i++)
                    {
                        objectKey = imagePath + dtImage.Rows[i]["ImageName"].ToString();
                        CloudStorageOperate.DeleteObject(objectKey);
                    }
                }
            }
            string bigimageName;
            //string smallimageName;
            object[] obj = SybMultiImageUploadOperate.Make(data.menuId, Common.ToInt32(data.SessionId), data.PicX, data.PicY, data.PicWidth, data.PicHeight, data.PicClientWidth, out bigimageName);//, out smallimageName 2014-6-9 菜图取消小图
            if (Common.ToBool(obj[0]))
            {
                data.DishImageUrlBig = bigimageName;
                //data.DishImageUrlSmall = smallimageName;
                objResult[0] = true;
            }
            else
            {
                objResult[1] = obj[1];
            }
            return objResult;
        }
        /// <summary>
        /// 图片处理在session
        /// </summary>
        /// <param name="data"></param>
        private static void SaveDishImage(ref SybDishInfo data)
        {
            #region 图片处理
            //删除老的图片信息
            DishOperate dishimageoperate = new DishOperate();
            MenuOperate menuOperate = new MenuOperate();
            if (data.DishID != 0)
            {
                DataTable dtImage = dishimageoperate.SelectDishImageInfo(data.DishID);
                if (dtImage.Rows.Count > 0)
                {
                    dishimageoperate.RemoveDishImagebyDishID(data.DishID);
                    string imagePath = WebConfig.ImagePath + menuOperate.QueryMenuImagePath(data.menuId);
                    string objectKey = "";
                    for (int i = 0; i < dtImage.Rows.Count; i++)
                    {
                        objectKey = imagePath + dtImage.Rows[i]["ImageName"].ToString();
                        CloudStorageOperate.DeleteObject(objectKey);//删除阿里云中对应的图片
                    }
                }
            }
            string bigimageName;
            //string smallimageName;

            ImageUploadOperate.ImageMake(Common.ToString(data.menuId), Common.ToString(data.SessionId), data.PicX, data.PicY, data.PicWidth, data.PicHeight, data.PicClientWidth, out bigimageName);//, out smallimageName 2014-6-9 菜图取消小图
            data.DishImageUrlBig = bigimageName;
            //data.DishImageUrlSmall = smallimageName;
            #endregion
        }
        /// <summary>
        /// 批量保存配料数据 （在添加、修改条件允许的情况下保存配料数据） 
        /// 
        /// 修改标识:罗国华20131112
        /// 功能描述:添加删除条件(删除时，如果该分类下有菜品就不允许删除)
        ///         添加条件(相同名称时不允许添加)
        ///         配料名称不能为空
        /// </summary>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static string SaveDishIngredients(string strJson, int menuId)
        {
            SybMsg sybMsg = new SybMsg();

            SybDishIngredientsList data = SysJson.JsonDeserialize<SybDishIngredientsList>(strJson);
            if (data == null)
            {
                sybMsg.Insert(-1000, "保存失败！数据格式存在问题！");
                return sybMsg.Value;
            }

            string errMsgDel = "", errMsgAdd = "", errNullName = "";
            bool isDelError = false, isAddError = false, isNullName = false;

            //判断本身列表存在的重复性
            var t = from p in data.list
                    where (p.operStatus == OperStatus.Insert || p.operStatus == OperStatus.Edit)
                    group p by p.ingredientsName into g
                    where g.Count() > 1
                    select new { g.Key, num = g.Count() };
            foreach (var p in t)
            {
                errMsgAdd += p.Key + ",";
                isAddError = true;
            }

            foreach (SybDishIngredients n in data.list)
            {
                //删除条件判断
                if (n.operStatus == OperStatus.Delete && DishPriceConnIngredientsManager.Exit(n.ingredientsId))
                {
                    errMsgDel += n.ingredientsName + ",";
                    isDelError = true;
                }
                //添加条件判断
                if (n.operStatus == OperStatus.Insert && DishIngredientsManager.Exit(menuId, n.ingredientsName.Trim()))
                {
                    errMsgAdd += n.ingredientsName + ",";
                    isAddError = true;
                }
                //空名称
                if ((n.operStatus == OperStatus.Insert || n.operStatus == OperStatus.Edit) && n.ingredientsName.Trim() == "")
                {
                    isNullName = true;
                }
            }
            if (isDelError) errMsgDel = errMsgDel.Substring(0, errMsgDel.Length - 1) + " 已被菜品引用；";
            if (isAddError) errMsgAdd = errMsgAdd.Substring(0, errMsgAdd.Length - 1) + " 配菜分类中已存在；";
            if (isNullName) errNullName = "存在空的配料名称;";

            if (isDelError || isAddError || isNullName)
            {
                sybMsg.Insert(-2, " 保存失败，" + errMsgDel + errMsgAdd + errNullName);
                return sybMsg.Value;
            }


            if (DishManager.SaveDishIngredients(data.list, menuId))
            {
                sybMsg.Insert(1, "保存成功");
            }
            else
            {
                sybMsg.Insert(-1, "保存失败");
            }
            return sybMsg.Value;
        }
        /// <summary>
        /// 收银宝查询菜品上一页下一页数据
        /// </summary>
        /// <param name="dishTypeId"></param>
        /// <param name="dishName"></param>
        /// <param name="dishId"></param>
        /// <returns></returns>
        public static string GetDishId(int dishTypeId, string dishName, int dishId)
        {
            int shopId = Common.ToInt32(HttpContext.Current.Session["loginshop"]);
            SybMsg resultMessage = new SybMsg();
            if (shopId < 0 || dishId < 0)
            {
                resultMessage.Insert(-1, "参数传递有误");
                return resultMessage.Value;
            }
            int menuId = DishOperate.GetMenuIdByShopId(shopId);
            if (menuId < 0)
            {
                resultMessage.Insert(-1, "参数传递有误");
                return resultMessage.Value;
            }
            // List<int> list = new List<int>();
            string dishIdStr = string.Empty;
            DishManager manager = new DishManager();
            DataTable dtDish = manager.SelectDishId(menuId, dishTypeId, dishName);
            if (dtDish.Rows.Count > 0)
            {
                DataRow[] dr = dtDish.Select("DishID=" + dishId);
                if (dr.Length == 1)
                {
                    int rowIndx = Common.ToInt32(dr[0]["rowIndex"]);//获取当前行的唯一索引
                    //获取上一道菜的dishId
                    if (rowIndx == 1)
                    {
                        dishIdStr += "0,";
                        // list.Add(0);//表示此时没有上一道菜
                    }
                    else
                    {
                        dr = dtDish.Select("rowIndex=" + (rowIndx - 1));
                        if (dr.Length == 1)
                        {
                            dishIdStr += dr[0]["DishID"].ToString() + ",";
                            // list.Add(Common.ToInt32(dr[0]["DishID"]));
                        }
                        else
                        {
                            dishIdStr += "0,";
                            // list.Add(0);
                        }
                    }
                    dishIdStr += dishId.ToString() + ",";
                    //  list.Add(dishId);//当前菜品的dishId
                    //获取下一道菜的dishId
                    if (rowIndx == dtDish.Rows.Count)
                    {
                        dishIdStr += "0";
                        // list.Add(0);//表示此时没有下一道菜
                    }
                    else
                    {
                        dr = dtDish.Select("rowIndex=" + (rowIndx + 1));
                        if (dr.Length == 1)
                        {
                            // list.Add(Common.ToInt32(dr[0]["DishID"]));
                            dishIdStr += dr[0]["DishID"].ToString();
                        }
                        else
                        {
                            // list.Add(0);
                            dishIdStr += "0";
                        }
                    }
                    // string returnJson = JsonOperate.JsonSerializer<List<int>>(list);
                    resultMessage.Insert(1, dishIdStr);
                }
                else
                {
                    resultMessage.Insert(-3, "当前菜品Id无效");
                }
            }
            else
            {
                resultMessage.Insert(-2, "未找到任何菜品信息");
            }
            return resultMessage.Value;
        }
    }
}
