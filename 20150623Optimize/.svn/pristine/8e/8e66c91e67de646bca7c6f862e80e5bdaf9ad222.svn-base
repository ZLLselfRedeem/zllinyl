using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using System.Data;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 门店会员页面配置的文章
    /// 2014-7-11
    /// </summary>
    public class ShopVipArticleOperate
    {
        private ShopVipArticleManager shopVipArticleManager = new ShopVipArticleManager();

        /// <summary>
        /// 如果该城市已经配置，则update，反之insert
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public int InsertArticle(ShopVipArticle article)
        {
            DataTable dt = shopVipArticleManager.GetArticle(article.City);
            if (dt != null && dt.Rows.Count > 0)
            {
                bool result = shopVipArticleManager.UpdateArticle(article);
                if (result)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return shopVipArticleManager.InsertArticle(article);
            }
        }

        /// <summary>
        /// 保存文章中的视频
        /// </summary>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public int InsertVideo(string name, string path)
        {
            return shopVipArticleManager.InsertVideo(name, path);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public bool DeleteArtile(int cityId)
        {
            return shopVipArticleManager.DeleteArtile(cityId);
        }

         /// <summary>
        /// 删除文章中视频信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteVideo(int id)
        {
            return shopVipArticleManager.DeleteVideo(id);
        }

        /// <summary>
        /// 客户端根据店铺Id获取相应配置
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public string ClientGetArticle(int shopId)
        {
            DataTable dt = shopVipArticleManager.ClientGetArticle(shopId);
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0]["Content"].ToString();//先匹配店铺所在城市的文章
            }
            else
            {
                DataTable dtAll = shopVipArticleManager.GetArticle(0);//若没有则取全国的数据
                if (dtAll != null && dtAll.Rows.Count > 0)
                {
                    return dtAll.Rows[0]["Content"].ToString();
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// 获取指定城市配置
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public DataTable GetArticle(int cityId)
        {
            return shopVipArticleManager.GetArticle(cityId);
        }

         /// <summary>
        /// 获取所有有效视频
        /// </summary>
        /// <returns></returns>
        public DataTable GetVideo()
        {
            return shopVipArticleManager.GetVideo();
        }

         /// <summary>
        /// 根据id获取视频objectKey
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetVideoPath(int id)
        {
            return shopVipArticleManager.GetVideoPath(id);
        }
    }
}
