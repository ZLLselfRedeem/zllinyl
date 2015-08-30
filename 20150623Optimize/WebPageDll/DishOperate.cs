using System;
using System.Collections;
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
    /// Dish操作类
    /// </summary>
    /// <returns></returns>
    public class DishOperate
    {
        private readonly DishManager dishMan = new DishManager();
        /// <summary>
        /// 查询所有菜基本信息
        /// </summary>
        /// <returns></returns>
        public DataTable QueryDishInfo()
        {
            DishManager dishMan = new DishManager();
            DataTable dtDish = dishMan.QueryDish();
            DataView dvDish = dtDish.DefaultView;
            dvDish.Sort = "DishDisplaySequence ASC, DishName ASC";
            return dvDish.ToTable();
        }
        /// <summary>
        /// 根据菜名编号查询菜基本信息
        /// </summary>
        /// <returns></returns>
        public VADish QueryDishInfo(int dishID)
        {
            DishManager dishMan = new DishManager();
            DataTable dtDish = dishMan.QueryDish(dishID);
            VADish dish = new VADish();
            dish.dishName = dtDish.Rows[0]["DishName"].ToString();
            dish.dishDescShort = dtDish.Rows[0]["DishDescShort"].ToString();
            dish.dishDescDetail = dtDish.Rows[0]["DishDescDetail"].ToString();
            dish.dishID = Convert.ToInt32(dtDish.Rows[0]["DishID"]);
            dish.langID = Convert.ToInt32(dtDish.Rows[0]["LangID"]);
            dish.dishI18nID = Convert.ToInt32(dtDish.Rows[0]["DishI18nID"]);
            dish.discountTypeID = Convert.ToInt32(dtDish.Rows[0]["DiscountTypeID"]);
            dish.menuID = Convert.ToInt32(dtDish.Rows[0]["MenuID"]);
            dish.dishDisplaySequence = Convert.ToInt32(dtDish.Rows[0]["DishDisplaySequence"]);
            dish.dishTotalQuantity = Convert.ToInt32(dtDish.Rows[0]["DishTotalQuantity"]);
            dish.dishConnTypeID = Convert.ToInt32(dtDish.Rows[0]["DishConnTypeID"]);
            dish.sendToKitchen = Convert.ToBoolean(dtDish.Rows[0]["SendToKitchen"]);
            dish.isActive = Convert.ToBoolean(dtDish.Rows[0]["IsActive"]);
            dish.dishQuanPin = Common.ToString(dtDish.Rows[0]["dishQuanPin"]);
            dish.dishJianPin = Common.ToString(dtDish.Rows[0]["dishJianPin"]);
            dish.cookPrinterName = Convert.ToString(dtDish.Rows[0]["cookPrinterName"]);
            ArrayList dishTypeID = new ArrayList();
            for (int i = 0; i < dtDish.Rows.Count; i++)
            {
                dishTypeID.Add(Convert.ToInt32(dtDish.Rows[i]["DishTypeID"]));
            }
            dish.DishTypeID = dishTypeID;
            return dish;
        }
        /// <summary>
        /// 根据价格多语言编号查询该规格菜信息
        /// </summary>
        /// <returns></returns>
        public DataTable QueryDishScaleInfo(int dishPriceI18nID)
        {
            DishManager dishMan = new DishManager();
            return dishMan.QueryDishScale(dishPriceI18nID);
        }
        /// <summary>
        /// 增加菜基本信息
        /// </summary>
        /// <param name="dish"></param>
        /// <returns>0：添加失败，非0：所加菜的编号</returns>
        public int AddDishInfo(VADish dish)
        {
            DishManager dishMan = new DishManager();
            //DataTable dtDish = dishMan.QueryDish();
            DataTable dtDish = Common.GetDataTableFieldValue("[DishInfo],[DishI18n],[DishConnType]", "DishI18n.DishID,DishI18n.DishName", "[DishInfo].DishID = [DishI18n].DishID and DishInfo.DishID = DishConnType.DishID and [DishInfo].DishStatus > '0' and " + "DishInfo.MenuID = '" + dish.menuID + "'");

            DataTable dtDishCopy = dtDish.Copy();//2011-09-27,xiaoyu 增加菜基本信息时判断传入的菜名是否存在
            DataView dvDishCopy = dtDishCopy.DefaultView;//2011-10-09，xiaoyu 增加啊菜基本信息时只判断当前菜单中是否存在传入的菜名
            //dvDishCopy.RowFilter = "MenuID = '" + dish.menuID + "'" + "and DishName = '" + dish.dishName + "'";
            dvDishCopy.RowFilter = "DishName = '" + dish.dishName + "'";
            using (TransactionScope scope = new TransactionScope())
            {
                if (string.IsNullOrEmpty(dish.dishName))
                {//如果传入的菜名为空,则直接返回false
                    return 0;
                }
                else if (dvDishCopy.Count > 0)
                {//如果这个菜已经存在，则判断这个分类里面是不是已经有这个菜
                    ArrayList listDishTypeId = dish.DishTypeID;
                    int dishId = Common.ToInt32(dvDishCopy[0]["DishID"]);
                    int ifDishExistInDishType = 0;
                    for (int i = 0; i < listDishTypeId.Count; i++)
                    {
                        int dishTypeId = Common.ToInt32(listDishTypeId[i]);
                        DataTable dtDishType = dishMan.SelectDishConnType(dishTypeId, dishId);
                        if (dtDishType.Rows.Count > 0)
                        {
                            ifDishExistInDishType++;
                        }
                    }
                    if (ifDishExistInDishType > 0)
                    {//如果这个菜的分类下面  已经存在了这个菜
                        dishId = 0;
                    }
                    else
                    {
                        int numberForInsertDishConnType = 0;
                        for (int i = 0; i < listDishTypeId.Count; i++)
                        {
                            DishConnType dishConnType = new DishConnType();
                            dishConnType.DishID = dishId;
                            dishConnType.DishTypeID = Convert.ToInt32(dish.DishTypeID[i]);
                            dishConnType.DishConnTypeStatus = 1;

                            if (dishMan.InsertDishConnType(dishConnType) > 0)
                            {
                                numberForInsertDishConnType += 1;
                            }
                        }
                        if (numberForInsertDishConnType == listDishTypeId.Count)
                        {
                            scope.Complete();
                        }
                        else
                        {
                            dishId = 0;
                        }
                    }
                    return dishId;
                }
                else
                {
                    //DishManager dishMan = new DishManager();
                    //DataTable dtDish = dishMan.QueryDish();
                    DataView dvDish = dtDish.DefaultView;//判断DishInfo表中是否存在该DishID
                    dvDish.RowFilter = "DishID = '" + dish.dishID + "'";
                    if (dvDish.Count > 0)
                    {//如果此DishID存在，则直接返回false
                        return 0;
                    }
                    else
                    {//如果此DishID不存在，则先新增DishInfo表
                        DishInfo dishInfo = new DishInfo();
                        dishInfo.DiscountTypeID = dish.discountTypeID;
                        dishInfo.MenuID = dish.menuID;
                        dishInfo.DishDisplaySequence = dish.dishDisplaySequence;
                        dishInfo.SendToKitchen = dish.sendToKitchen;
                        dishInfo.IsActive = dish.isActive;
                        dishInfo.DishTotalQuantity = dish.dishTotalQuantity;
                        dishInfo.DishStatus = 1;
                        dishInfo.cookPrinterName = dish.cookPrinterName;
                        int dishID = dishMan.InsertDish(dishInfo);
                        if (dishID > 0)
                        {//DishInfo表插入成功
                            DishI18n dishI18n = new DishI18n();
                            dishI18n.DishID = dishID;
                            dishI18n.LangID = dish.langID;
                            dishI18n.DishName = dish.dishName;
                            dishI18n.DishDescShort = dish.dishDescShort;
                            dishI18n.DishDescDetail = dish.dishDescDetail;
                            dishI18n.DishI18nStatus = 1;
                            dishI18n.DishHistory = "";//备用字段
                            dishI18n.dishQuanPin = dish.dishQuanPin;
                            dishI18n.dishJianPin = dish.dishJianPin;
                            DishConnType dishConnType = new DishConnType();
                            int result = 0;
                            for (int i = 0; i < dish.DishTypeID.Count; i++)
                            {//循环插入菜与显示分类关系表DishConnType
                                dishConnType.DishID = dishID;
                                dishConnType.DishTypeID = Convert.ToInt32(dish.DishTypeID[i]);
                                dishConnType.DishConnTypeStatus = 1;
                                dishMan.InsertDishConnType(dishConnType);
                                result += 1;
                            }
                            if (dishMan.InsertDishI18n(dishI18n) > 0 && result == dish.DishTypeID.Count)
                            {//插入菜多语言表成功，且菜与显示分类关系表都插入成功
                                scope.Complete();
                                return dishID;
                            }
                            else
                            {
                                return 0;
                            }
                        }
                        else
                        {//DishInfo表插入失败则直接返回false
                            return 0;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 修改菜基本信息
        /// </summary>
        /// <param name="dish"></param>
        /// <returns></returns>
        public bool ModifyDishInfo(VADish dish)
        {
            DishManager dishMan = new DishManager();
            //DataTable dtDish = dishMan.QueryDish();
            DataTable dtDish = Common.GetDataTableFieldValue("[DishInfo],[DishI18n],[DishConnType]", "DishI18n.DishID,DishI18n.DishName", "[DishInfo].DishID = [DishI18n].DishID and DishInfo.DishID = DishConnType.DishID and [DishInfo].DishStatus > '0' and " + "DishInfo.MenuID = '" + dish.menuID + "'");

            DataTable dtDishCopy = dtDish.Copy();
            DataView dvDishCopy = dtDishCopy.DefaultView;
            //dvDishCopy.RowFilter = "DishID <> '" + dish.dishID + "' and MenuID = '"
            //    + dish.menuID + "' and DishName = '" + dish.dishName + "'";
            dvDishCopy.RowFilter = "DishName = '" + dish.dishName + "' and DishID <>'" + dish.dishID + "'";
            if (dish.dishName == "" || dish.dishName == null || dvDishCopy.Count > 0)
            {//如果传入的菜名为空，或者传入的菜名在当前菜谱已存在（除了修改的项外），则直接返回false
                return false;
            }
            else
            {
                //DishManager dishMan = new DishManager();
                //DataTable dtDish = dishMan.QueryDish();
                DataView dvDish = dtDish.DefaultView;//判断DishInfo表中是否存在该DishID
                dvDish.RowFilter = "DishID = '" + dish.dishID + "'";
                if (dvDish.Count > 0)
                {//如果此DishID存在，则先更新DishInfo表
                    DishInfo dishInfo = new DishInfo();
                    dishInfo.DiscountTypeID = dish.discountTypeID;
                    dishInfo.MenuID = dish.menuID;
                    dishInfo.DishDisplaySequence = dish.dishDisplaySequence;
                    dishInfo.SendToKitchen = dish.sendToKitchen;
                    dishInfo.IsActive = dish.isActive;
                    dishInfo.DishTotalQuantity = dish.dishTotalQuantity;
                    dishInfo.DishStatus = 1;
                    dishInfo.DishID = dish.dishID;
                    dishInfo.cookPrinterName = dish.cookPrinterName;
                    if (dishMan.UpdateDish(dishInfo))
                    {//DishInfo表更新成功
                        DishI18n dishI18n = new DishI18n();
                        dishI18n.DishID = dish.dishID;
                        dishI18n.LangID = dish.langID;
                        dishI18n.DishName = dish.dishName;
                        dishI18n.DishDescShort = dish.dishDescShort;
                        dishI18n.DishDescDetail = dish.dishDescDetail;
                        dishI18n.DishI18nStatus = 1;
                        dishI18n.DishHistory = "";//备用字段
                        dishI18n.DishI18nID = dish.dishI18nID;
                        dishI18n.dishQuanPin = dish.dishQuanPin;
                        dishI18n.dishJianPin = dish.dishJianPin;
                        DishConnType dishConnType = new DishConnType();
                        int result = 0;
                        if (dishMan.DeleteDishConnTypeByDishID(dish.dishID))
                        {
                            for (int i = 0; i < dish.DishTypeID.Count; i++)
                            {//循环更新菜与显示分类关系表DishConnType
                                dishConnType.DishID = dish.dishID;
                                dishConnType.DishTypeID = Convert.ToInt32(dish.DishTypeID[i]);
                                dishConnType.DishConnTypeStatus = 1;
                                //bug:修改菜信息时，不能选择多个dishType，DishConnTypeID主键，自增长。
                                //下面这行代码索引报错，2013-7-19，wangcheng注释掉
                                // dishConnType.DishConnTypeID = Convert.ToInt32(dvDish[i]["DishConnTypeID"]);
                                dishMan.InsertDishConnType(dishConnType);
                                result += 1;
                            }
                        }

                        if (dishMan.UpdateDishI18n(dishI18n) && result == dish.DishTypeID.Count)
                        {//更新菜多语言表成功，且菜与显示分类关系表都更新成功
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {//DishInfo表更新失败则直接返回false
                        return false;
                    }
                }
                else
                {//如果此DishID不存在，则直接返回false
                    return false;
                }
            }
        }
        /// <summary>
        /// 删除菜基本信息
        /// </summary>
        /// <param name="dishID"></param>
        /// <returns></returns>
        public bool RemoveDishInfo(int dishID)
        {
            DishManager dishMan = new DishManager();
            DataTable dtDish = dishMan.QueryDishSimple(dishID);
            DataView dvDish = dtDish.DefaultView;//判断DishInfo表中是否存在该DishID
            dvDish.RowFilter = "DishID = '" + dishID + "'";
            if (dvDish.Count > 0)
            {//如果该DishID存在，则删除
                if (dishMan.DeleteDishByID(dishID))
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
        /// 根据菜编号DishID查询对应图片信息
        /// </summary>
        /// <param name="dishID"></param>
        /// <returns></returns>
        public DataTable QueryDishImage(int dishID)
        {
            DishManager dishMan = new DishManager();
            return dishMan.QueryDishImage(dishID);
        }
        /// <summary>
        /// 根据菜编号DishID,ImageScale查询对应图片信息
        /// </summary>
        /// <param name="dishID"></param>
        /// <returns></returns>
        public DataTable QueryDishImage(int dishID, int ImageScale)
        {
            DishManager dishMan = new DishManager();
            return dishMan.QueryDishImage(dishID, ImageScale);
        }
        /// <summary>
        /// 增加菜图片信息
        /// </summary>
        /// <param name="imageInfo"></param>
        /// <returns></returns>
        public bool AddDishImage(ImageInfo bigimage, ImageInfo smallimage)
        {
            if (bigimage.ImageName == "" || bigimage.ImageName == null || smallimage.ImageName == "" || smallimage.ImageName == null)
            {//如果传入的图片名称为空则直接返回false
                return false;
            }
            else
            {
                DishManager dishMan = new DishManager();
                if (dishMan.InsertDishImage(bigimage, smallimage) > 0)
                {//插入图片表成功则返回true
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 增加菜图片信息
        /// </summary>
        /// <param name="imageInfo"></param>
        /// <returns></returns>
        public bool AddDishImage(ImageInfo imageInfo)
        {
            if (imageInfo.ImageName == "" || imageInfo.ImageName == null)
            {//如果传入的图片名称为空则直接返回false
                return false;
            }
            else
            {
                DishManager dishMan = new DishManager();
                if (dishMan.InsertDishImage(imageInfo) > 0)
                {//插入图片表成功则返回true
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 修改菜图片信息
        /// </summary>
        /// <param name="imageInfo"></param>
        /// <returns></returns>
        public bool ModifyDishImage(ImageInfo imageInfo)
        {
            if (imageInfo.ImageName == "" || imageInfo.ImageName == null)
            {//如果传入的图片名称为空则直接返回false
                return false;
            }
            else
            {
                DishManager dishMan = new DishManager();
                if (dishMan.UpdateDishImage(imageInfo))
                {//如果更新菜图片信息ImageInfo表成功，则返回true
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
        /// <summary>
        /// 删除菜图片信息
        /// </summary>
        /// <param name="imageID"></param>
        /// <returns></returns>
        public bool RemoveDishImage(int imageID)
        {
            DishManager dishMan = new DishManager();
            if (dishMan.DeleteDishImageByID(imageID))
            {//删除成功则返回true
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 删除菜图片信息
        /// </summary>
        /// <param name="DishID"></param>
        /// <returns></returns>
        public bool RemoveDishImagebyDishID(int DishID)
        {
            DishManager dishMan = new DishManager();
            if (dishMan.DeleteDishImageByDishID(DishID))
            {//删除成功则返回true
                return true;
            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// 根据菜编号DishID查询对应视频信息
        /// </summary>
        /// <param name="dishID"></param>
        /// <returns></returns>
        public DataTable QueryVideoImage(int dishID)
        {
            DishManager dishMan = new DishManager();
            return dishMan.QueryDishVideo(dishID);
        }
        /// <summary>
        /// 增加菜视频信息
        /// </summary>
        /// <param name="videoInfo"></param>
        /// <returns></returns>
        public bool AddDishVideo(VideoInfo videoInfo)
        {
            if (videoInfo.VideoName == "" || videoInfo.VideoName == null)
            {//如果传入的视频名称为空则直接返回false
                return false;
            }
            else
            {
                DishManager dishMan = new DishManager();
                if (dishMan.QueryDishVideo(videoInfo.DishID).Rows.Count > 0)
                {//如果该菜编号DishID已有对应的视频信息，则返回false(暂时只支持一个菜一个视频)
                    return false;
                }
                else
                {
                    if (dishMan.InsertDishVideo(videoInfo) > 0)
                    {//插入视频表成功则返回true
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        /// <summary>
        /// 修改菜图片信息
        /// </summary>
        /// <param name="videoInfo"></param>
        /// <returns></returns>
        public bool ModifyDishVideo(VideoInfo videoInfo)
        {
            if (videoInfo.VideoName == "" || videoInfo.VideoName == null)
            {//如果传入的视频名称为空则直接返回false
                return false;
            }
            else
            {
                DishManager dishMan = new DishManager();
                if (dishMan.UpdateDishVideo(videoInfo))
                {//如果更新菜视频信息VideoInfo表成功，则返回true
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
        /// <summary>
        /// 删除菜图片信息
        /// </summary>
        /// <param name="imageID"></param>
        /// <returns></returns>
        public bool RemoveDishVideo(int videoID)
        {
            DishManager dishMan = new DishManager();
            if (dishMan.DeleteDishVideoByID(videoID))
            {//删除成功则返回true
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 查询菜价格信息
        /// </summary>
        /// <param name="dishID"></param>
        /// <returns></returns>
        public DataTable QueryDishPrice(int dishID)
        {
            DishManager dishMan = new DishManager();
            return dishMan.QueryDishPrice(dishID);
        }
        /// <summary>
        /// 增加菜价格信息
        /// </summary>
        /// <param name="dishPrice"></param>
        /// <returns></returns>
        public bool AddDishPrice(VADishPrice dishPrice)
        {
            DishManager dishMan = new DishManager();
            DataTable dtDishPrice = dishMan.QueryDishPrice(dishPrice.dishID);
            DataView dvDishPrice = dtDishPrice.DefaultView;//判断传入的DishPriceID是否存在
            dvDishPrice.RowFilter = "DishPriceID = '" + dishPrice.dishPriceID + "'";
            int dishPriceID = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                // if (dvDishPrice.Count == 0)
                // {//如果传入的DishPriceID不存在，则新增，否则直接写入多语言表
                DishPriceInfo dishPricInfo = new DishPriceInfo();
                dishPricInfo.DishID = dishPrice.dishID;
                dishPricInfo.DishPrice = dishPrice.dishPrice;
                dishPricInfo.DishSoldout = dishPrice.DishSoldout;
                dishPricInfo.DishNeedWeigh = dishPrice.dishNeedWeigh;
                dishPricInfo.vipDiscountable = dishPrice.vipDiscountable;
                dishPricInfo.backDiscountable = dishPrice.backDiscountable;
                dishPricInfo.DishPriceStatus = 1;
                dishPriceID = dishMan.InsertDishPrice(dishPricInfo);
                // }
                if (dishPriceID > 0)
                {//DishPriceInfo表插入成功或者该DishPriceID已存在，则直接向DishPriceI18n表中插入多语言详细信息
                    DishPriceI18n dishPriceI18n = new DishPriceI18n();
                    if (dishPriceID > 0)
                    {//如果是新增规格，则价格编号等于前面插入的价格编号
                        dishPriceI18n.DishPriceID = dishPriceID;
                    }
                    //if (dvDishPrice.Count > 0)
                    //{//如果是修改规格，则价格编号等于函数传入的价格编号
                    //    dishPriceI18n.DishPriceID = dishPrice.dishPriceID;
                    //}
                    dishPriceI18n.LangID = dishPrice.langID;
                    dishPriceI18n.ScaleName = dishPrice.scaleName;
                    dishPriceI18n.DishPriceI18nStatus = 1;

                    dishPriceI18n.markName = dishPrice.markName;

                    if (dishMan.InsertDishPriceI18n(dishPriceI18n) > 0)
                    {//插入DishPriceI18n表成功，则返回true
                        scope.Complete();
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

                //return true;
            }
        }
        /// <summary>
        /// 修改菜价格信息
        /// </summary>
        /// <param name="dishPrice"></param>
        /// <returns></returns>
        public bool ModifyDishPrice(VADishPrice dishPrice)
        {
            DishManager dishMan = new DishManager();
            DataTable dtDishPrice = dishMan.QueryDishPrice(dishPrice.dishID);
            DataView dvDishPrice = dtDishPrice.DefaultView;//判断传入的DishPriceID是否存在
            dvDishPrice.RowFilter = "DishPriceID = '" + dishPrice.dishPriceID + "'";
            if (dvDishPrice.Count > 0)
            {//判断此DishPriceID存在，则修改
                DishPriceInfo dishPricInfo = new DishPriceInfo();
                dishPricInfo.DishID = dishPrice.dishID;
                dishPricInfo.DishPrice = dishPrice.dishPrice;
                dishPricInfo.DishSoldout = dishPrice.DishSoldout;
                dishPricInfo.DishNeedWeigh = dishPrice.dishNeedWeigh;
                dishPricInfo.vipDiscountable = dishPrice.vipDiscountable;
                dishPricInfo.DishPriceStatus = 1;
                dishPricInfo.DishPriceID = dishPrice.dishPriceID;
                dishPricInfo.backDiscountable = dishPrice.backDiscountable;
                if (dishMan.UpdateDishPrice(dishPricInfo))
                {//如果更新DishPriceInfo基本信息成功，则更新价格的多语言表
                    DishPriceI18n dishPriceI18n = new DishPriceI18n();
                    dishPriceI18n.DishPriceID = dishPrice.dishPriceID;
                    dishPriceI18n.LangID = dishPrice.langID;
                    dishPriceI18n.ScaleName = dishPrice.scaleName;
                    dishPriceI18n.DishPriceI18nStatus = 1;
                    dishPriceI18n.DishPriceI18nID = dishPrice.dishPriceI18nID;

                    dishPriceI18n.markName = dishPrice.markName;

                    if (dishMan.UpdateDishPriceI18n(dishPriceI18n))
                    {//更新菜价格多语言表成功，则返回true
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
        /// <summary>
        /// 增加菜价格信息
        /// </summary>
        /// <param name="dishPrice"></param>
        /// <returns></returns>
        public bool AddDishPricesh(VADishPrice dishPrice)
        {
            DishManager dishMan = new DishManager();
            DataTable dtDishPrice = dishMan.QueryDishPrice(dishPrice.dishID);
            DataView dvDishPrice = dtDishPrice.DefaultView;//判断传入的DishPriceID是否存在
            dvDishPrice.RowFilter = "DishPriceID = '" + dishPrice.dishPriceID + "'";
            int dishPriceID = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                // if (dvDishPrice.Count == 0)
                // {//如果传入的DishPriceID不存在，则新增，否则直接写入多语言表
                DishPriceInfo dishPricInfo = new DishPriceInfo();
                dishPricInfo.DishID = dishPrice.dishID;
                dishPricInfo.DishPrice = dishPrice.dishPrice;
                dishPricInfo.DishSoldout = false;
                dishPricInfo.DishNeedWeigh = false;
                dishPricInfo.vipDiscountable = dishPrice.vipDiscountable;
                dishPricInfo.backDiscountable = dishPrice.backDiscountable;
                dishPricInfo.DishPriceStatus = 1;
                dishPriceID = dishMan.InsertDishPrice(dishPricInfo);
                // }
                if (dishPriceID > 0)
                {//DishPriceInfo表插入成功或者该DishPriceID已存在，则直接向DishPriceI18n表中插入多语言详细信息
                    DishPriceI18n dishPriceI18n = new DishPriceI18n();
                    if (dishPriceID > 0)
                    {//如果是新增规格，则价格编号等于前面插入的价格编号
                        dishPriceI18n.DishPriceID = dishPriceID;
                    }
                    //if (dvDishPrice.Count > 0)
                    //{//如果是修改规格，则价格编号等于函数传入的价格编号
                    //    dishPriceI18n.DishPriceID = dishPrice.dishPriceID;
                    //}
                    dishPriceI18n.LangID = dishPrice.langID;
                    dishPriceI18n.ScaleName = dishPrice.scaleName;
                    dishPriceI18n.DishPriceI18nStatus = 1;

                    dishPriceI18n.markName = dishPrice.markName;

                    if (dishMan.InsertDishPriceI18n(dishPriceI18n) > 0)
                    {//插入DishPriceI18n表成功，则返回true
                        scope.Complete();
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

                //return true;
            }
        }
        /// <summary>
        /// 修改菜价格信息
        /// </summary>
        /// <param name="dishPrice"></param>
        /// <returns></returns>
        public bool ModifyDishPricesh(VADishPrice dishPrice)
        {
            DishManager dishMan = new DishManager();
            DataTable dtDishPrice = dishMan.QueryDishPrice(dishPrice.dishID);
            DataView dvDishPrice = dtDishPrice.DefaultView;//判断传入的DishPriceID是否存在
            dvDishPrice.RowFilter = "DishPriceID = '" + dishPrice.dishPriceID + "'";
            if (dvDishPrice.Count > 0)
            {//判断此DishPriceID存在，则修改
                DishPriceInfo dishPricInfo = new DishPriceInfo();
                dishPricInfo.DishID = dishPrice.dishID;
                dishPricInfo.DishPrice = dishPrice.dishPrice;
                dishPricInfo.DishSoldout = dishPrice.DishSoldout;
                dishPricInfo.DishNeedWeigh = dishPrice.dishNeedWeigh;
                dishPricInfo.vipDiscountable = dishPrice.vipDiscountable;
                dishPricInfo.DishPriceStatus = 1;
                dishPricInfo.DishPriceID = dishPrice.dishPriceID;
                dishPricInfo.backDiscountable = dishPrice.backDiscountable;
                if (dishMan.UpdateDishPricesh(dishPricInfo))
                {//如果更新DishPriceInfo基本信息成功，则更新价格的多语言表
                    DishPriceI18n dishPriceI18n = new DishPriceI18n();
                    dishPriceI18n.DishPriceID = dishPrice.dishPriceID;
                    dishPriceI18n.LangID = dishPrice.langID;
                    dishPriceI18n.ScaleName = dishPrice.scaleName;
                    dishPriceI18n.DishPriceI18nStatus = 1;
                    dishPriceI18n.DishPriceI18nID = dishPrice.dishPriceI18nID;

                    dishPriceI18n.markName = dishPrice.markName;

                    if (dishMan.UpdateDishPriceI18n(dishPriceI18n))
                    {//更新菜价格多语言表成功，则返回true
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
        /// <summary>
        /// 删除价格多语言信息
        /// </summary>
        /// <param name="dishPriceI18nID"></param>
        /// <returns></returns>
        public bool RemoveDishPriceI18n(int dishPriceI18nID)
        {
            DishManager dishMan = new DishManager();
            if (dishMan.DeleteDishPriceI18nByID(dishPriceI18nID))
            {//删除成功则返回true
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 删除价格信息（包括多语言信息）
        /// </summary>
        /// <param name="dishPriceID"></param>
        /// <returns></returns>
        public bool RemoveDishPrice(int DishPriceI18nID)
        {
            DishManager dishMan = new DishManager();
            if (dishMan.DeleteDishPriceByID(DishPriceI18nID))
            {//删除成功则返回true
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 根据DishID删除菜价信息
        /// </summary>
        /// <param name="DishId"></param>
        /// <returns></returns>
        public bool DeleteDishPriceByDishID(int DishId)
        {
            DishManager dishMan = new DishManager();
            DataTable dtDishPriceId = Common.GetDataTableFieldValue("DishPriceInfo", "DishPriceID", "DishID ='" + DishId + "'");
            string alist = CommonPageOperate.SplicingAlphabeticStr(dtDishPriceId, "DishPriceID");
            if (dishMan.DeleteDishPriceByDishID(alist))
            {//删除成功则返回true
                return true;
            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// 查询所有菜名基本信息
        /// </summary>
        /// <param name="menuId">菜谱ID</param>
        /// <param name="langId">语言ID</param>
        /// <returns></returns>
        public DataTable QueryDishAndDishPriceByMenuAndLang(int menuId, int langId)
        {
            DishManager dishMan = new DishManager();
            return dishMan.SelectDishAndDishPriceByMenuAndLang(menuId, langId);
        }
        /// <summary>
        /// 根据菜的多语言编号查询对应的大图的路径
        /// 如果没有图片或者查询出错返回空字符串
        /// </summary>
        /// <param name="dishPriceI18nId"></param>
        /// <returns></returns>
        public string QueryDishImagePath(int dishPriceI18nId, int imageScale)
        {
            string dishImagePath = string.Empty;
            dishImagePath += WebConfig.CdnDomain + WebConfig.ImagePath;
            if (!string.IsNullOrEmpty(dishImagePath))
            {
                DataTable dtDishImagePath = dishMan.SelectDishImagePath(dishPriceI18nId, imageScale);
                if (dtDishImagePath.Rows.Count == 1)
                {
                    string imageName = Common.ToString(dtDishImagePath.Rows[0]["ImageName"]);
                    string menuImagePath = Common.ToString(dtDishImagePath.Rows[0]["menuImagePath"]);
                    if (!string.IsNullOrEmpty(imageName) && !string.IsNullOrEmpty(menuImagePath))
                    {
                        dishImagePath += menuImagePath + imageName;
                    }
                    else
                    {
                        dishImagePath = string.Empty;
                    }
                }
                else
                {
                    dishImagePath = string.Empty;
                }
            }
            return dishImagePath;
        }
        /// <summary>
        /// 根据菜谱编号查询菜信息
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public DataTable QueryDishAndScaleName(int menuId)
        {
            return dishMan.SelectDishInfo(menuId);
        }

        public string QueryDishIsAddorUpdate(string DishPriceID)
        {
            DishManager dishMan = new DishManager();
            return dishMan.QueryDishIsAddorUpdate(DishPriceID);
        }

        public bool IsUpdateImg(int imageID)
        {
            DishManager dishMan = new DishManager();
            return dishMan.IsUpdateImg(imageID);
        }

        #region （wangcheng）沽清模块
        /// <summary>
        /// 查询常用沽清菜信息
        /// </summary>
        public DataTable QueryCommonCurrentSellOffInfo(int menuId, int DishPriceI18nID)
        {
            DishManager dishMan = new DishManager();
            return dishMan.SelectCommonCurrentSellOffInfo(menuId, DishPriceI18nID);
        }
        /// <summary>
        /// 新建沽清信息
        /// </summary>
        public long InsertCurrentSellOffInfo(CurrentSellOffInfo currentSellOffInfo)
        {
            DishManager dishMan = new DishManager();
            return dishMan.InsertCurrentSellOffInfoTable(currentSellOffInfo);
        }
        /// <summary>
        /// 新建常用沽清信息
        /// </summary>
        public long InsertCommonCurrentSellOffInfo(CommonCurrentSellOffInfo commonCurrentSellOffInfo)
        {
            DishManager dishMan = new DishManager();
            return dishMan.InsertCommonCurrentSellOffInfoTable(commonCurrentSellOffInfo);
        }
        /// <summary>
        /// 查询沽清信息
        /// </summary>
        public DataTable QueryCurrentSellOffInfo(int menuId, int shopId)
        {
            DishManager dishMan = new DishManager();
            return dishMan.SelectCurrentSellOffInfo(menuId, shopId);
        }
        /// <summary>
        /// 查询菜信息
        /// </summary>
        public DataTable QueryDishInformation(string dishFilter, int menuId)
        {
            DishManager dishMan = new DishManager();
            return dishMan.SelectDishInformation(dishFilter, menuId);
        }
        /// <summary>
        /// 修改沽清次数
        /// </summary>
        public bool UpdateCommonCurrentSellOffCount(int DishI18nID, int DishPriceI18nID)
        {
            DishManager dishMan = new DishManager();
            return dishMan.UpdateCurrentSellOffCount(DishI18nID, DishPriceI18nID);
        }
        /// <summary>
        /// 根据DishPriceI18nID删除沽清信息
        /// </summary>
        public bool DeleteCurrentSellOff(int DishPriceI18nID, int shopId, int operateEmployeeId)
        {
            DishManager dishMan = new DishManager();
            return dishMan.DeleteCurrentSellOffInfo(DishPriceI18nID, shopId, operateEmployeeId);
        }
        /// <summary>
        /// 删除本菜谱本门店全部沽清信息
        /// </summary>
        public bool DeleteAllCurrentSellOffInfo(int shopId, int menuId, int operateEmployeeId)
        {
            DishManager dishMan = new DishManager();
            return dishMan.DeleteAllCurrentSellOffInfo(shopId, menuId, operateEmployeeId);
        }
        #endregion

        /// <summary>
        /// 判断菜名是否相同
        /// </summary>
        /// <param name="menuID"></param>
        /// <param name="DishName"></param>
        /// <returns></returns>
        public bool IsCanQueryThisDish(int menuID, string DishName)
        {
            DishManager DM = new DishManager();
            return DM.IsCanQueryThisDish(menuID, DishName);

        }


        //由于以前方法看不懂所以新开辟方法,坑爹的一堆需求界面没需求数据，现取默认值赋值
        #region  菜品信息新增修改
        /// <summary>
        /// 增加菜基本信息
        /// </summary>
        /// <param name="dish"></param>
        /// <returns>0：添加失败，非0：所加菜的编号</returns>
        public int AddDishInfojson(VADish dish)
        {
            DishManager dishMan = new DishManager();
            DataTable dtDish = dishMan.QueryDish();
            DataTable dtDishCopy = dtDish.Copy();//2011-09-27,xiaoyu 增加菜基本信息时判断传入的菜名是否存在
            DataView dvDishCopy = dtDishCopy.DefaultView;//2011-10-09，xiaoyu 增加啊菜基本信息时只判断当前菜单中是否存在传入的菜名
            dvDishCopy.RowFilter = "MenuID = '" + dish.menuID + "'" + "and DishName = '" + dish.dishName + "'";
            using (TransactionScope scope = new TransactionScope())
            {
                if (string.IsNullOrEmpty(dish.dishName))
                {//如果传入的菜名为空,则直接返回false
                    return 0;
                }
                else if (dvDishCopy.Count > 0)
                {//如果这个菜已经存在，则判断这个分类里面是不是已经有这个菜
                    ArrayList listDishTypeId = dish.DishTypeID;
                    int dishId = Common.ToInt32(dvDishCopy[0]["DishID"]);
                    int ifDishExistInDishType = 0;
                    for (int i = 0; i < listDishTypeId.Count; i++)
                    {
                        int dishTypeId = Common.ToInt32(listDishTypeId[i]);
                        DataTable dtDishType = dishMan.SelectDishConnType(dishTypeId, dishId);
                        if (dtDishType.Rows.Count > 0)
                        {
                            ifDishExistInDishType++;
                        }
                    }
                    if (ifDishExistInDishType > 0)
                    {//如果这个菜的分类下面  已经存在了这个菜
                        dishId = 0;
                    }
                    else
                    {
                        int numberForInsertDishConnType = 0;
                        for (int i = 0; i < listDishTypeId.Count; i++)
                        {
                            DishConnType dishConnType = new DishConnType();
                            dishConnType.DishID = dishId;
                            dishConnType.DishTypeID = Convert.ToInt32(dish.DishTypeID[i]);
                            dishConnType.DishConnTypeStatus = 1;

                            if (dishMan.InsertDishConnType(dishConnType) > 0)
                            {
                                numberForInsertDishConnType += 1;
                            }
                        }
                        if (numberForInsertDishConnType == listDishTypeId.Count)
                        {
                            scope.Complete();
                        }
                        else
                        {
                            dishId = 0;
                        }
                    }
                    return dishId;
                }
                else
                {
                    //DishManager dishMan = new DishManager();
                    //DataTable dtDish = dishMan.QueryDish();
                    DataView dvDish = dtDish.DefaultView;//判断DishInfo表中是否存在该DishID
                    dvDish.RowFilter = "DishID = '" + dish.dishID + "'";
                    if (dvDish.Count > 0)
                    {//如果此DishID存在，则直接返回false
                        return 0;
                    }
                    else
                    {//如果此DishID不存在，则先新增DishInfo表
                        DishInfo dishInfo = new DishInfo();
                        dishInfo.DiscountTypeID = 0;
                        dishInfo.MenuID = dish.menuID;
                        dishInfo.DishDisplaySequence = dish.dishDisplaySequence;
                        dishInfo.SendToKitchen = true;
                        dishInfo.IsActive = true;
                        dishInfo.DishTotalQuantity = 0;
                        dishInfo.DishStatus = 1;
                        dishInfo.cookPrinterName = "";
                        int dishID = dishMan.InsertDish(dishInfo);
                        if (dishID > 0)
                        {//DishInfo表插入成功
                            DishI18n dishI18n = new DishI18n();
                            dishI18n.DishID = dishID;
                            dishI18n.LangID = dish.langID;
                            dishI18n.DishName = dish.dishName;
                            dishI18n.DishDescShort = dish.dishDescShort;
                            dishI18n.DishDescDetail = dish.dishDescDetail;
                            dishI18n.DishI18nStatus = 1;
                            dishI18n.DishHistory = "";//备用字段
                            dishI18n.dishQuanPin = dish.dishQuanPin;
                            dishI18n.dishJianPin = dish.dishJianPin;
                            DishConnType dishConnType = new DishConnType();
                            int result = 0;
                            for (int i = 0; i < dish.DishTypeID.Count; i++)
                            {//循环插入菜与显示分类关系表DishConnType
                                dishConnType.DishID = dishID;
                                dishConnType.DishTypeID = Convert.ToInt32(dish.DishTypeID[i]);
                                dishConnType.DishConnTypeStatus = 1;
                                dishMan.InsertDishConnType(dishConnType);
                                result += 1;
                            }
                            if (dishMan.InsertDishI18n(dishI18n) > 0 && result == dish.DishTypeID.Count)
                            {//插入菜多语言表成功，且菜与显示分类关系表都插入成功
                                scope.Complete();
                                return dishID;
                            }
                            else
                            {
                                return 0;
                            }
                        }
                        else
                        {//DishInfo表插入失败则直接返回false
                            return 0;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 修改菜基本信息
        /// </summary>
        /// <param name="dish"></param>
        /// <returns></returns>
        public bool ModifyDishInfojson(VADish dish)
        {
            DishManager dishMan = new DishManager();
            DataTable dtDish = dishMan.QueryDish();
            DataTable dtDishCopy = dtDish.Copy();
            DataView dvDishCopy = dtDishCopy.DefaultView;
            dvDishCopy.RowFilter = "DishID <> '" + dish.dishID + "' and MenuID = '"
                + dish.menuID + "' and DishName = '" + dish.dishName + "'";
            if (dish.dishName == "" || dish.dishName == null || dvDishCopy.Count > 0)
            {//如果传入的菜名为空，或者传入的菜名在当前菜谱已存在（除了修改的项外），则直接返回false
                return false;
            }
            else
            {
                //DishManager dishMan = new DishManager();
                //DataTable dtDish = dishMan.QueryDish();
                DataView dvDish = dtDish.DefaultView;//判断DishInfo表中是否存在该DishID
                dvDish.RowFilter = "DishID = '" + dish.dishID + "'";
                if (dvDish.Count > 0)
                {//如果此DishID存在，则先更新DishInfo表
                    DishInfo dishInfo = new DishInfo();
                    dishInfo.DiscountTypeID = 0;
                    dishInfo.MenuID = dish.menuID;
                    dishInfo.DishDisplaySequence = dish.dishDisplaySequence;
                    dishInfo.SendToKitchen = true;
                    dishInfo.IsActive = true;
                    dishInfo.DishTotalQuantity = 0;
                    dishInfo.DishStatus = 1;
                    dishInfo.DishID = dish.dishID;
                    dishInfo.cookPrinterName = "";
                    if (dishMan.UpdateDish(dishInfo))
                    {//DishInfo表更新成功
                        DishI18n dishI18n = new DishI18n();
                        dishI18n.DishID = dish.dishID;
                        dishI18n.LangID = dish.langID;
                        dishI18n.DishName = dish.dishName;
                        dishI18n.DishDescShort = dish.dishDescShort;
                        dishI18n.DishDescDetail = dish.dishDescDetail;
                        dishI18n.DishI18nStatus = 1;
                        dishI18n.DishHistory = "";//备用字段
                        dishI18n.DishI18nID = dish.dishI18nID;
                        dishI18n.dishQuanPin = dish.dishQuanPin;
                        dishI18n.dishJianPin = dish.dishJianPin;
                        DishConnType dishConnType = new DishConnType();
                        int result = 0;
                        if (dishMan.DeleteDishConnTypeByDishID(dish.dishID))
                        {
                            for (int i = 0; i < dish.DishTypeID.Count; i++)
                            {//循环更新菜与显示分类关系表DishConnType
                                dishConnType.DishID = dish.dishID;
                                dishConnType.DishTypeID = Convert.ToInt32(dish.DishTypeID[i]);
                                dishConnType.DishConnTypeStatus = 1;
                                //bug:修改菜信息时，不能选择多个dishType，DishConnTypeID主键，自增长。
                                //下面这行代码索引报错，2013-7-19，wangcheng注释掉
                                // dishConnType.DishConnTypeID = Convert.ToInt32(dvDish[i]["DishConnTypeID"]);
                                dishMan.InsertDishConnType(dishConnType);
                                result += 1;
                            }
                        }

                        if (dishMan.UpdateDishI18n(dishI18n) && result == dish.DishTypeID.Count)
                        {//更新菜多语言表成功，且菜与显示分类关系表都更新成功
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {//DishInfo表更新失败则直接返回false
                        return false;
                    }
                }
                else
                {//如果此DishID不存在，则直接返回false
                    return false;
                }
            }
        }
        #endregion

        public static int GetMenuIdByShopId(int shopId)
        {
            return DishManager.GetMenuIdByShopId(shopId);
        }

        //批量上传 有图
        public bool InsertDishInfo(DishInfo dishInfo, string bigImageName, string smallImageName)
        {
            bool ret = false;
            DishManager dishMan = new DishManager();
            using (TransactionScope scope = new TransactionScope())
            {
                int DishID = dishMan.InsertDish(dishInfo);
                if (DishID != 0)
                {
                    DishI18n dish18n = new DishI18n();
                    dish18n.DishID = DishID;
                    dish18n.LangID = 1;
                    dish18n.DishI18nStatus = -1;

                    int iRet = DishOperate.InsertDishI18n(dish18n);
                    if (iRet != 0)
                    {
                        ImageInfo bigimageInfo = new ImageInfo();
                        bigimageInfo.DishID = DishID;
                        bigimageInfo.ImageName = bigImageName;
                        bigimageInfo.ImageScale = 0;
                        bigimageInfo.ImageSequence = 1;
                        bigimageInfo.ImageStatus = -7; //标识一下是批量传图新增的数据
                        ImageInfo smallimageInfo = new ImageInfo();
                        smallimageInfo.DishID = DishID;
                        smallimageInfo.ImageName = smallImageName;
                        smallimageInfo.ImageScale = ImageScale.缩略图;
                        smallimageInfo.ImageSequence = 1;
                        smallimageInfo.ImageStatus = -8;//标识一下是批量传图新增的数据

                        this.AddDishImage(bigimageInfo, smallimageInfo);

                        scope.Complete();
                        ret = true;
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// 批量上传 无图 返回 DishID
        /// </summary>
        public int InsertDishInfo(DishInfo dishInfo)
        {
            DishManager dishMan = new DishManager();
            return dishMan.InsertDish(dishInfo);
        }

        public static int InsertDishI18n(DishI18n dishI18n)
        {
            DishManager dishMan = new DishManager();
            return dishMan.InsertDishI18n(dishI18n);
        }

        //判断菜品名称是否可用(是否重复) 批量传图用
        public static bool IsDishNameUsed(string dishName)
        {
            return DishManager.IsDishNameUsed(dishName);
        }

        //更新菜品信息 有图
        public static bool UpdateDishI18nInfo(int DishID, string dishName, string dishQuanPin, string dishJianPin, string[] prices, string[] units)
        {
            //1.更新 DishI18n 信息，添加dishName, 全拼，简拼，更新 状态
            //2.更新 DishInfo 信息，修改状态
            //3.在表 DishPriceInfo，DishPriceI18n 中新增 价格，单位 数据
            //4.更新 ImageInfo 表状态
            bool ret = false;
            DishManager dishManager = new DishManager();
            using (TransactionScope scope = new TransactionScope())
            {
                if (DishManager.UpdateDishI18nInfo(DishID, dishName, dishQuanPin, dishJianPin))
                {
                    if (DishManager.UpdateDishInfoStatus(DishID, 1))
                    {
                        for (int i = 0; i < prices.Length; i++)
                        {
                            DishPriceInfo dishPriceInfo = new DishPriceInfo();
                            dishPriceInfo.DishID = DishID;
                            dishPriceInfo.DishSoldout = false;
                            dishPriceInfo.DishPriceStatus = 1;
                            dishPriceInfo.DishNeedWeigh = false;
                            dishPriceInfo.vipDiscountable = true;

                            dishPriceInfo.DishPrice = Common.ToInt32(prices[i]);
                            int DishPriceID = dishManager.InsertDishPrice(dishPriceInfo);
                            if (DishPriceID > 0)
                            {
                                DishPriceI18n dishPrice18n = new DishPriceI18n();
                                dishPrice18n.DishPriceID = DishPriceID;
                                dishPrice18n.LangID = 1;
                                dishPrice18n.ScaleName = units[i];
                                dishPrice18n.DishPriceI18nStatus = 1;

                                dishManager.InsertDishPriceI18n(dishPrice18n);
                            }
                        }

                        scope.Complete();
                        ret = true;
                    }
                }
            }
            return ret;
        }

        //更新菜品信息 无图
        public static bool UpdateDishI18nInfoWithNoImg(int DishID, string dishName, string dishQuanPin, string dishJianPin, string[] prices, string[] units)
        {

            return false;
        }

        //更新DishInfo 状态
        public static bool UpdateDishInfoStatus(int DishID, int DishStatus)
        {
            return DishManager.UpdateDishInfoStatus(DishID, DishStatus);
        }

        /// <summary>
        /// 根据DishId查找大图及小图的图片信息
        /// </summary>
        /// <param name="dishId"></param>
        /// <returns></returns>
        public DataTable SelectDishImageInfo(int dishId)
        {
            return dishMan.SelectDishImageInfo(dishId);
        }

        /// <summary>
        /// 全部取消沽清
        /// </summary>
        /// <returns></returns>
        public bool DeleteAllCurrentSellOffInfo()
        {
            return dishMan.DeleteAllCurrentSellOffInfo();
        }

        /// <summary>
        ///  抵扣券分享页面菜谱信息
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="price"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public DataTable GetDishTableByShopIdAndPrice(int shopId, double price, int pageSize)
        {
            return dishMan.GetDishTableByShopIdAndPrice(shopId, price, pageSize);
        }

        public DataTable GetDishTableByShopId(int shopId, int pageSize)
        {
            return dishMan.GetDishTableByShopId(shopId, pageSize);
        }
    }
}
