using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using System.Configuration;
using ChineseCharacterToPinyin;
using System.Web;
using System.Transactions;
using System.Data;
using VAGastronomistMobileApp.SQLServerDAL;
using System.Runtime.Serialization;
using CloudStorage;
using System.Net;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using System.Reflection;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.SQLServerDAL.Persistence;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;
using PagedList;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class ShopChannelDishOperate
    {
        private readonly ShopChannelDishManager manager = new ShopChannelDishManager();

        /// <summary>
        /// 添加商家频道菜品
        /// </summary>
        /// <param name="shopChannelID">商家频道ID</param>
        /// <param name="dishID">菜品ID</param>
        /// <param name="dishPriceID">菜品规格ID</param>
        /// <param name="dishIndex">菜品顺序</param>
        /// <param name="dishName">菜品名称</param>
        /// <param name="dishImageUrl">菜品URL</param>
        /// <param name="dishContent">菜品摘要</param>
        /// <returns></returns>
        public static string ChannelDishAdd(int shopChannelID, int dishID, int dishPriceID, int dishIndex, string dishName, string dishImageUrl, string dishContent)
        {
            SybMsg resultMessage = new SybMsg();
            // 1、判断是否已添加
            bool hasAdd = ShopChannelDishManager.CheckHasChannelDish(shopChannelID, dishPriceID);
            if (hasAdd)
            {
                resultMessage.Insert(-2, "菜品已添加");
                return resultMessage.Value;
            }
            dishIndex = dishIndex + 1;
            // 2、判断是否添加成功
            string strCheck = ShopChannelDishManager.ChannelDishAdd(shopChannelID, dishID, dishPriceID, dishIndex, dishName, dishImageUrl, dishContent, DateTime.Now, false);
            if (strCheck == "1")
            {
                resultMessage.Insert(1, "保存成功");
                return resultMessage.Value;
            }
            else
            {
                resultMessage.Insert(-3, "保存失败");
                return resultMessage.Value;
            }
        }

        /// <summary>
        /// 修改商家频道菜品 
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <param name="dishID">菜品ID</param>
        /// <param name="dishPriceID">菜品规格ID</param>
        /// <param name="dishIndex">菜品顺序</param>
        /// <param name="dishName">菜品名称</param>
        /// <param name="dishImageUrl">菜品URL</param>
        /// <param name="dishContent">菜品摘要</param>
        /// <returns></returns>
        public static string ChannelDishUpdate(int id,int dishID, int dishPriceID, int dishIndex, string dishName, string dishImageUrl, string dishContent,int shopChannelID)
        {
            SybMsg resultMessage = new SybMsg();

            //修改已发布的频道菜品业务逻辑
            //a)先把老的菜品isDelete=1
            //b)插入一条同样顺序的菜品status=0 and isDelete=0
            //c)取消发布后，点频道菜品设置按钮做如下操作
            //i,删除所有status=0的数据
            //ii,恢复所有isDelete=1 的数据 为 isDelete=0
            //d)发布后，做如下操作
            //i,物理删除所有isDelete=1的数据
            //ii,修改频道下所有的status=1
            DataTable dtShopChannel=ShopChannelDishManager.GetShopChannelDish(id);
            if (dtShopChannel != null && dtShopChannel.Rows.Count > 0)
            {
                DataRow dr = dtShopChannel.Rows[0];
                // 插入一条新的修改过的记录
                ShopChannelDishManager.ChannelDishAddDelete(Common.ToInt32(dr["shopChannelID"]), Common.ToInt32(dr["dishID"]), Common.ToInt32(dr["dishPriceID"]),
                    Common.ToInt32(dr["dishIndex"]), Common.ToString(dr["dishName"]),dishImageUrl , dishContent, false);
                // 老的数据删除掉
                ShopChannelDishManager.ChannelDishDelete(id.ToString());
                resultMessage.Insert(1, "修改成功");
                return resultMessage.Value;
            }

            // 判断新修改的菜品和老菜品是否相同， 相同不需要判断是否修改重复
            bool isDifference = false;
            if(dtShopChannel.Rows.Count>0)
            {
                foreach(DataRow dr in dtShopChannel.Rows)
                {
                    if(Convert.ToString(dr["dishPriceID"])==Common.ToString(dishPriceID))
                    {
                        isDifference = true;
                        break;
                    }
                }
            }

            if (!isDifference)
            {
                bool hasAdd = ShopChannelDishManager.CheckHasChannelDish(shopChannelID, dishPriceID);
                if (hasAdd)
                {
                    resultMessage.Insert(-2, "菜品已添加");
                    return resultMessage.Value;
                }
            }

            string strCheck = ShopChannelDishManager.ChannelDishUpdate(id, dishID, dishPriceID, dishIndex, dishName, dishImageUrl, dishContent);
            if (strCheck == "1")
            {
                resultMessage.Insert(1, "修改成功");
                return resultMessage.Value;
            }
            else
            {
                resultMessage.Insert(-3, "修改失败");
                return resultMessage.Value;
            }
        }

        /// <summary>
        /// 删除指定商家频道菜品
        /// </summary>
        /// <param name="channelDishIDS"></param>
        /// <returns></returns>
        public static string ChannelDishDelete(string channelDishIDS)
        {
            SybMsg resultMessage = new SybMsg();

            //1、删除已发布的频道菜品业务逻辑
            //a)先把老的菜品isDelete=1 
            //b)插入一条同样顺序的菜品status=0 and isDelete=1
            //c)取消发布后，点频道菜品设置按钮做如下操作
            //i,删除所有status=0的数据
            //ii,恢复所有isDelete=1 的数据 为 isDelete=0
            //d)发布后，做如下操作
            //i,物理删除所有isDelete=1的数据
            //ii,修改频道下所有的status=1

            string[] strChannelDishIDS = channelDishIDS.Split(',');
            foreach (string channelDishID in strChannelDishIDS)
            {
                DataTable dtShopChannel = ShopChannelDishManager.GetShopChannelDish(Common.ToInt32(channelDishID));
                if(dtShopChannel!=null && dtShopChannel.Rows.Count>0)
                {
                    DataRow dr=dtShopChannel.Rows[0];
                    // 插入一条相同的删除记录
                    ShopChannelDishManager.ChannelDishAddDelete(Common.ToInt32(dr["shopChannelID"]), Common.ToInt32(dr["dishID"]), Common.ToInt32(dr["dishPriceID"]),
                        Common.ToInt32(dr["dishIndex"]), Common.ToString(dr["dishName"]), Common.ToString(dr["dishImageUrl"]), Common.ToString(dr["dishContent"]),true);
                }
            }

            string strCheck = ShopChannelDishManager.ChannelDishDelete(channelDishIDS);
            if (strCheck == "1")
            {
                resultMessage.Insert(1, "删除成功");
                return resultMessage.Value;
            }
            else
            {
                resultMessage.Insert(-1, "删除失败");
                return resultMessage.Value;
            }
        }

        /// <summary>
        /// 上传菜品 图片
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="fs"></param>
        /// <returns></returns>
        public static string UploadShopDishImage(int shopId, Stream fs)
        {
            SybMsg resultMessage = new SybMsg();
            if (fs == null)
            {
                resultMessage.Insert(-1, "请求参数有误");
                return resultMessage.Value;
            }
            string extension = "";
            using (Image original_image = Image.FromStream(fs))
            {
                //if (original_image.Width != 640 || original_image.Height != 320)
                //{
                //    resultMessage.Insert(-7, "图片的宽度应为640，高度为320");
                //    return resultMessage.Value;
                //}
                
                if (!CheckImageExtension(original_image, ref extension))
                {
                    resultMessage.Insert(-5, "图片格式必须为png");
                    return resultMessage.Value;
                }
            }
            if (fs.Length > 1000*1024)
            {
                resultMessage.Insert(-4, "图片大小不能超过1024KB");
                return resultMessage.Value;
            }
            fs.Seek(0, SeekOrigin.Begin);

            string imgName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "." + extension;//图片名称
            string imgPath = WebConfig.ImagePath + "ShopChannelDish/"+shopId+"/" + imgName;//图片上传地址

            CloudStorage.AliyunOpenStorageService service = new CloudStorage.AliyunOpenStorageService();
            CloudStorage.AliyunOSSResult result = service.PutObject(imgPath, fs);

            imgPath = WebConfig.ImagePath + "-ViewAlloc-ShopChannelDish/" + shopId + "/" + imgName;//图片上传地址
            if (result.code)
            {
                resultMessage.Insert(1,  "imgUrl," +WebConfig.CdnDomain + imgPath);
            }
            else
            {
                resultMessage.Insert(-3, "上传失败");
            }
            return resultMessage.Value;
        }

        /// <summary>
        /// 判断Image对象的后缀
        /// </summary>
        /// <param name="p_Image"></param>
        /// <param name="extension"></param>
        /// <returns></returns>
        private static bool CheckImageExtension(Image p_Image, ref string extension)
        {
            string[] str = new string[] { "png" };
            string currectExtension = GetImageExtension(p_Image).ToLower();
            extension = currectExtension;
            return str.Any(p => p.Contains(currectExtension));
        }

        /// <summary>
        /// 获取Image对象的后缀
        /// </summary>
        /// <param name="p_Image"></param>
        /// <returns></returns>
        private static string GetImageExtension(Image p_Image)
        {
            Type type = typeof(ImageFormat);
            PropertyInfo[] _ImageFormatList = type.GetProperties(BindingFlags.Static | BindingFlags.Public);
            for (int i = 0; i < _ImageFormatList.Length; i++)
            {
                ImageFormat _FormatClass = (ImageFormat)_ImageFormatList[i].GetValue(null, null);
                if (_FormatClass.Guid.Equals(p_Image.RawFormat.Guid))
                {
                    return _ImageFormatList[i].Name;
                }
            }
            return "";
        }


        public static string SearchShopDish(int shopID, string key, int pageIndex, int pageSize)
        {
            SybMsg resultMessage = new SybMsg();
            try
            {
                IDishInfoRepository dishInfoRepository = ServiceFactory.Resolve<IDishInfoRepository>();
                IPagedList<DishDetails> list = null;

                list = dishInfoRepository.GetPageShopAllDishDetailsForAward(
                                            new VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure.Page(pageIndex, pageSize), shopID, key);

                ListDishInfoDetail dishInfoList = new ListDishInfoDetail();
                dishInfoList.dishInfoDetailList = list.Select(p => new DishInfoDetail
                {
                    dishID = p.DishId,
                    dishPriceID = p.DishPriceId,
                    dishName = p.DishName + "-" + p.ScaleName
                }).ToList();
                dishInfoList.hasPageNext = list.HasNextPage;
                string json = JsonOperate.JsonSerializer<ListDishInfoDetail>(dishInfoList);
                resultMessage.Insert(1, json);
                return resultMessage.Value;
            }
            catch
            {
                resultMessage.Insert(-1, "查询失败");
                return resultMessage.Value;
            }
        }

        /// <summary>
        /// 查询商家频道菜品详情
        /// </summary>
        /// <param name="shopChannelDishID"></param>
        /// <returns></returns>
        public static string SearchShopChannelDish(int shopChannelDishID)
        {
            SybMsg resultMessage = new SybMsg();
            try
            {
                DataTable dt = ShopChannelDishManager.SearchShopChannelDish(shopChannelDishID);
                if(dt!=null && dt.Rows.Count>0)
                {
                    foreach(DataRow dr in dt.Rows)
                    {
                        dr["dishImageUrl"] = WebConfig.CdnDomain +WebConfig.ImagePath+ "-ViewAlloc-" + dr["dishImageUrl"];
                    }
                }
                string json = Common.ConvertDateTableToJson(dt);
                resultMessage.Insert(1, json);
                return resultMessage.Value;
            }
            catch
            {
                resultMessage.Insert(-1, "查询失败");
                return resultMessage.Value;
            }
        }

        /// <summary>
        /// 查询菜品价格和折扣
        /// </summary>
        /// <param name="dishPriceID"></param>
        /// <returns></returns>
        public static string SearchDishPriceAndDiscount(int dishPriceID, int shopID)
        {
            SybMsg resultMessage = new SybMsg();
            try
            {
                ShopVIPOperate operateShopVip = new ShopVIPOperate();
                double shopVip = operateShopVip.GetShopVipDiscount(shopID);
                var objDishPriceInfo = DishPriceInfoManager.GetModel(dishPriceID);
                DishChannelPrice responseDishChannelPrice = new DishChannelPrice();
                if (objDishPriceInfo != null)
                {
                    // 菜品可以享受VIP折扣
                    if (Convert.ToBoolean(objDishPriceInfo.vipDiscountable))
                    {
                        responseDishChannelPrice.discount = shopVip;
                    }
                    else
                    {
                        // 该菜品不享受折扣
                        responseDishChannelPrice.discount = 0;
                    }
                    responseDishChannelPrice.price = Common.ToDecimal(objDishPriceInfo.DishPrice);
                }
                string json = JsonOperate.JsonSerializer<DishChannelPrice>(responseDishChannelPrice);
                resultMessage.Insert(1, json);
                return resultMessage.Value;
            }
            catch
            {
                resultMessage.Insert(-1, "查询失败");
                return resultMessage.Value;
            }
        }

        public static DishChannelPrice SearchPriceAndDiscount(int dishPriceID, int shopID)
        {
            ShopVIPOperate operateShopVip = new ShopVIPOperate();
            double shopVip = operateShopVip.GetShopVipDiscount(shopID);
            var objDishPriceInfo = DishPriceInfoManager.GetModel(dishPriceID);
            DishChannelPrice responseDishChannelPrice = new DishChannelPrice();
            if (objDishPriceInfo != null)
            {
                // 菜品可以享受VIP折扣
                if (Convert.ToBoolean(objDishPriceInfo.vipDiscountable))
                {
                    responseDishChannelPrice.discount = shopVip;
                }
                else
                {
                    // 该菜品不享受折扣
                    responseDishChannelPrice.discount = 0;
                }
                responseDishChannelPrice.price = Common.ToDecimal(objDishPriceInfo.DishPrice);
            }
            return responseDishChannelPrice;

        }

        public static string Sort(List<Tuple<int, int>> dishList)
        {
            SybMsg resultMessage = new SybMsg();
            foreach (var tuple in dishList)
            {
                bool result = new ShopChannelDishManager().UpdateIndex(tuple.Item1, tuple.Item2);
                if (!result)
                {
                    resultMessage.Insert(-1, "排序失败");
                    return resultMessage.Value;
                }
            }
            resultMessage.Insert(1, "排序成功");
            return resultMessage.Value;
        }

        public static string Select(int shopChannelID)
        {
            SybMsg resultMessage = new SybMsg();
            DataTable dishes = new ShopChannelDishManager().SelectByShopChannelID(shopChannelID);
            string dishJson = Common.ConvertDateTableToJson(dishes);
            resultMessage.Insert(1, dishJson);
            return resultMessage.Value;
        }

        /// <summary>
        /// 发布指定频道菜品
        /// </summary>
        /// <param name="shopChannelDishID"></param>
        /// <returns></returns>
        public static string ShopChannelDishRelease(int shopChannelID)
        {
            SybMsg resultMessage = new SybMsg();
            string strCheck = ShopChannelDishManager.ShopChannelDishRelease(shopChannelID);
            if (strCheck == "1")
            {
                resultMessage.Insert(1, "发布成功");
                return resultMessage.Value;
            }
            else
            {
                resultMessage.Insert(-1, "发布失败");
                return resultMessage.Value;
            }
        }

        public DataTable SelectBack(int shopChannelID)
        {
            ShopChannelDishManager manager = new ShopChannelDishManager();
            DataTable dishes = manager.SelectByShopChannelIDBack(shopChannelID);
            return dishes;
        }

        public bool Delete(int dishID)
        {
            ShopChannelDishManager manager = new ShopChannelDishManager();
            return manager.Remove(dishID);
        }

        public static string NoPublicDelete(int shopChannelID)
        {
            SybMsg resultMessage = new SybMsg();
            ShopChannelDishManager manager = new ShopChannelDishManager();
            int result = manager.NoPublicDelete(shopChannelID);
            if (result>1)
            {
                resultMessage.Insert(1, "发布成功");
                return resultMessage.Value;
            }
            else
            {
                resultMessage.Insert(-1, "发布失败");
                return resultMessage.Value;
            }
        }

        /// <summary>
        /// 频道菜品发布
        /// </summary>
        /// <param name="channelID"></param>
        /// <returns></returns>
        public static bool Public(int channelID)
        {
            ShopChannelDishManager manager = new ShopChannelDishManager();
            bool result = manager.Public(channelID);
            return result;
        }


        public static ShopChannelDish Search(int dishID)
        {
            DataTable dt = ShopChannelDishManager.SearchShopChannelDish(dishID);
            if(dt==null || dt.Rows.Count==0)
            {
                return new ShopChannelDish();
            }
            DataRow row = dt.Rows[0];
            ShopChannelDish dish = new ShopChannelDish()
            {
                id = Convert.ToInt32(row["id"]),
                shopChannelID = Convert.ToInt32(row["shopChannelID"]),
                dishID = Convert.ToInt32(row["dishID"]),
                dishPriceID = Convert.ToInt32(row["dishPriceID"]),
                dishName = Convert.ToString(row["dishName"]),
                dishIndex = Convert.ToInt32(row["dishIndex"]),
                dishContent = Convert.ToString(row["dishContent"]),
                dishImageUrl = Convert.ToString(row["dishImageUrl"]),
                createTime = Convert.ToDateTime(row["createTime"]),
                isDelete = Convert.ToBoolean(row["isDelete"]),
                status = Convert.ToBoolean(row["status"])
            };
            return dish;
        }

        public bool Insert(ShopChannelDish dish)
        {
            string result = ShopChannelDishManager.ChannelDishAdd(dish.shopChannelID, dish.dishID, dish.dishPriceID, dish.dishIndex, dish.dishName, dish.dishImageUrl, dish.dishContent, dish.createTime, dish.isDelete);
            if (result == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool DishIndexIsClash(int channelID, int index)
        {
            ShopChannelDishManager manager = new ShopChannelDishManager();
            DataTable dt = manager.IndexIsClash(channelID, index);
            if (dt.Rows.Count > 0)
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
    /// 菜品详情LIST
    /// </summary>
    public class ListDishInfoDetail
    {
        public List<DishInfoDetail> dishInfoDetailList { get; set; }

        /// <summary>
        /// 是否还有下一页
        /// </summary>
        public bool hasPageNext { get; set; }
    }

    public class DishInfoDetail
    {
        /// <summary>
        /// 菜品ID
        /// </summary>
        public int dishID { get; set; }

        /// <summary>
        /// 菜品价格ID
        /// </summary>
        public int dishPriceID { get; set; }

        /// <summary>
        /// 菜品名称
        /// </summary>
        public string dishName { get; set; }
    }

    /// <summary>
    /// 菜品折扣价格
    /// </summary>
    public class DishChannelPrice
    {
        /// <summary>
        /// 菜品折扣
        /// </summary>
        public double discount { get; set; }

        /// <summary>
        /// 菜品价格
        /// </summary>
        public decimal price { get; set; }
    }
}
