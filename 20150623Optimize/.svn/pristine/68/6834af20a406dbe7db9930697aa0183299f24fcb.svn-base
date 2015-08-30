using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using System.Data;
using VA.CacheLogic;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 客户端图片处理参数：数据处理层
    /// 创建日期：2014-5-29
    /// </summary>
    public class ClientImageSizeOperate
    {
        ClientImageSizeManager imageManager = new ClientImageSizeManager();

        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <param name="clientImageSize"></param>
        /// <returns></returns>
        public int InsertClientImageSize(ClientImageSize clientImageSize)
        {
            return imageManager.InsertClientImageSize(clientImageSize);
        }

        /// <summary>
        /// 更新某图片尺寸相关设置
        /// </summary>
        /// <param name="clientImageSize"></param>
        /// <returns></returns>
        public bool UpdateClientImageSize(ClientImageSize clientImageSize)
        {
            return imageManager.UpdateClientImageSize(clientImageSize);
        }

        /// <summary>
        /// 删除某个图片尺寸的设置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteClientImageSize(int id)
        {
            return imageManager.DeleteClientImageSize(id);
        }

        /// <summary>
        /// 根据条件查询对应的图片处理参数
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="appType">设备类别</param>
        /// <param name="imageType">图片类别</param>
        /// <param name="screenWidth">屏幕宽度</param>
        /// <returns></returns>
        public DataTable QueryClientImageSize(int id, int appType, string imageType, string screenWidth)
        {
            return imageManager.QueryClientImageSize(id, appType, imageType, screenWidth);
        }

        /// <summary>
        /// 客户端根据屏幕宽度查询所有图片处理尺寸
        /// </summary>
        /// <param name="screenWidth">屏幕宽度</param>
        /// <param name="appType">设备类别</param>
        /// <returns></returns>
        public List<VAClientImageSize> QueryClientImageSize(string screenWidth, int apptype, string clientBuild)
        {
            SystemConfigCacheLogic systemConfigCacheLogic = new SystemConfigCacheLogic();
            var clientImageSize = systemConfigCacheLogic.GetClientImageSize();
            //从缓存中抓出匹配的appType及屏幕宽度
            clientImageSize = clientImageSize.FindAll(p => p.apptype == apptype && p.screenWidth == screenWidth);

            List<VAClientImageSize> clientImageSizeList = new List<VAClientImageSize>();
            if (clientImageSize != null && clientImageSize.Any())
            {
                foreach (ClientImageSize imageSize in clientImageSize)
                {
                    VAClientImageSize newImageSize = new VAClientImageSize();
                    newImageSize.imageType = (VAImageType)Common.ToInt32(imageSize.imageType);
                    newImageSize.value = imageSize.value;
                    clientImageSizeList.Add(newImageSize);
                }
            }

            bool newVersion = Common.CheckLatestBuild_November((VAAppType)apptype, clientBuild);
            if (!newVersion)
            {
                var bannerOld = clientImageSizeList.Find(p => p.imageType == VAImageType.BANNER_IMAGE);
                var bannerNew = clientImageSizeList.Find(p => p.imageType == VAImageType.BANNER_IMAGE_VERSION2);

                bannerNew.value = bannerOld.value;
            }
            return clientImageSizeList;
        }
    }
}
