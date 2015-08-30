using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VAGastronomistMobileApp.DBUtility;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    /// <summary>
    /// Server上图片应用OSS数据处理类
    /// 创建时间：2014-4-17
    /// </summary>
    public class OSSImageManager
    {
        /// <summary>
        /// 获取指定公司所有菜单的菜图
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        public DataTable GetDishImage(string shopId)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select distinct menu.MenuID, dish.DishID, menu.menuImagePath, img.ImageName");
            strSql.Append(" from ImageInfo img inner join DishInfo dish");
            strSql.Append(" on img.DishID = dish.DishID");
            strSql.Append(" and img.ImageStatus = 1 and dish.DishStatus = 1");
            strSql.Append(" and isnull(img.ImageName, '') != ''");
            strSql.Append(" inner join MenuInfo menu");
            strSql.Append(" on dish.MenuID = menu.MenuID");
            strSql.Append(" and menu.MenuStatus = 1 and isnull(menu.menuImagePath, '') != ''");
            strSql.Append(" inner join MenuConnShop connShop on menu.MenuID = connShop.menuId");
            strSql.Append(" inner join ShopInfo shop on connShop.shopId = shop.shopID");
            strSql.Append(" and shop.isHandle = 1 and shop.shopStatus = 1");
            //strSql.AppendFormat(" and shop.companyID in ({0})", companyId);
            strSql.AppendFormat(" and shop.shopId in ({0})", shopId);

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取所有公司Logo
        /// </summary>
        /// <returns></returns>
        public DataTable GetCompanyLogo()
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select company.companyID,company.companyImagePath, company.companyLogo");
            strSql.Append(" from CompanyInfo company inner join ShopInfo shop");
            strSql.Append(" on company.companyID = shop.companyID and company.companyStatus = 1 and shop.shopStatus = 1 and shop.isHandle = 1");
            strSql.Append(" and isnull(company.companyLogo,'') != '' and isnull(company.companyImagePath,'') != '' ");

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取所有店铺Logo
        /// </summary>
        /// <returns></returns>
        public DataTable GetShopLogo()
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select shopID,shopImagePath,shopLogo");
            strSql.Append(" from ShopInfo");
            strSql.Append(" where shopStatus = 1 and isHandle = 1 and isnull(shopLogo,'') != '' and isnull(shopImagePath,'') != '' ");

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取所有店铺形象展示照，每个店铺一张
        /// </summary>
        /// <returns></returns>
        public DataTable GetShopPublicityPhoto()
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select shopID,publicityPhotoPath ");
            strSql.Append(" from ShopInfo");
            strSql.Append(" where shopStatus = 1 and isHandle = 1 and isnull(publicityPhotoPath,'') != '' ");

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取所有店铺环境，每个店铺N张
        /// </summary>
        /// <returns></returns>
        public DataTable GetShopEnvironmentPhoto()
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select img.shopId,shop.shopImagePath+'ShopImage/' shopImagePath,img.revealImageName");
            strSql.Append(" from ShopRevealImage img inner join ShopInfo shop");
            strSql.Append(" on img.shopId = shop.shopID");
            strSql.Append(" and shop.shopStatus = 1 and shop.isHandle = 1 and isnull(img.revealImageName,'') != '' and isnull(shop.shopImagePath,'') != '' ");

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取平台VIP等级图片
        /// </summary>
        /// <returns></returns>
        public DataTable GetPlatformVipImage()
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select id,'UploadFiles/VipImg/'+vipImg vipImg");
            strSql.Append(" from ViewAllocPlatformVipInfo");
            strSql.Append(" where status = 1 and isnull(vipImg,'') != ''");

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 积分商城商品图片
        /// </summary>
        /// <returns></returns>
        public DataTable GetPointGoodsImage()
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select Id,'Goods/'+pictureName pictureName");
            strSql.Append(" from Goods");
            strSql.Append(" where status = 1 and isnull(pictureName,'') !='' ");

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 菜谱压缩包结构(IOS,Android)
        /// </summary>
        /// <returns></returns>
        public DataTable GetMenuZip()
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select distinct menu.MenuID,menu.MenuVersion, menu.menuImagePath");
            strSql.Append(" from MenuInfo menu inner join MenuConnShop connShop");
            strSql.Append(" on menu.MenuID = connShop.menuId and isnull(menu.menuImagePath, '') != ''");
            strSql.Append(" inner join ShopInfo shop");
            strSql.Append(" on connShop.shopId = shop.shopID");
            strSql.Append(" and shop.isHandle = 1 and shop.shopStatus = 1");
            strSql.Append(" order by menu.MenuID");

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据菜图数量区间计算公司
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public DataTable Compute(string start, string end)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select distinct shop.shopID,count(img.ImageID) ImageCount");
            strSql.Append(" from ImageInfo img inner join DishInfo dish");
            strSql.Append(" on img.DishID = dish.DishID and img.ImageStatus = 1 and dish.DishStatus = 1");
            strSql.Append(" and isnull(img.ImageName,'') != '' ");
            strSql.Append(" inner join MenuInfo menu");
            strSql.Append(" on dish.MenuID = menu.MenuID and menu.MenuStatus = 1");
            strSql.Append(" inner join MenuConnShop connShop on menu.MenuID = connShop.menuId");
            strSql.Append(" inner join ShopInfo shop on connShop.shopId = shop.shopID");
            strSql.Append(" and shop.isHandle = 1 and shop.shopStatus = 1");
            strSql.Append(" group by connShop.menuId,shop.shopID");
            strSql.AppendFormat(" having ( count(img.ImageID)>={0} and count(img.ImageID) <= {1})", start, end);

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 指定区间待上传的菜图总数
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public DataTable UploadDishCount(string start, string end)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select SUM(ImageCount) dishCount from");
            strSql.Append(" (select distinct connShop.companyId,count(img.ImageID) ImageCount");
            strSql.Append(" from ImageInfo img inner join DishInfo dish");
            strSql.Append(" on img.DishID = dish.DishID and img.ImageStatus = 1 and dish.DishStatus = 1");
            strSql.Append(" and isnull(img.ImageName,'') != '' ");
            strSql.Append(" inner join MenuInfo menu");
            strSql.Append(" on dish.MenuID = menu.MenuID and menu.MenuStatus = 1");
            strSql.Append(" inner join MenuConnShop connShop on menu.MenuID = connShop.menuId");
            strSql.Append(" inner join ShopInfo shop on connShop.shopId = shop.shopID");
            strSql.Append(" and shop.isHandle = 1 and shop.shopStatus = 1");
            strSql.Append(" group by connShop.menuId,connShop.companyId,shop.shopID");
            strSql.AppendFormat(" having (count(img.ImageID)>={0} and count(img.ImageID) <= {1})) a", start, end);

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
    }
}
