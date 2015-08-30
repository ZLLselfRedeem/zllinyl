using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 积分商城Html内容逻辑层
    /// 2014-2-21 jinyanni
    /// </summary>
    public class HtmlOperate
    {
        /// <summary>
        /// 保存一条Html内容，若有旧数据则直接update,反之insert
        /// </summary>
        /// <param name="htmlInfo"></param>
        /// <returns></returns>
        public object[] SaveHtml(HtmlInfo htmlInfo)
        {
            HtmlManage _htmlManage = new HtmlManage();

            HtmlInfo htmlInfoExist = _htmlManage.QueryHtmlByCityId(htmlInfo.cityId);

            if (!string.IsNullOrEmpty(htmlInfoExist.html))
            {
                return _htmlManage.UpdateHtml(htmlInfo);
            }
            else
            {
                return _htmlManage.InsertHtml(htmlInfo);
            }
        }

        /// <summary>
        /// 新增一条Html内容
        /// </summary>
        /// <param name="htmlInfo">htmlInfo Model</param>
        /// <returns></returns>
        public object[] InsertHtml(HtmlInfo htmlInfo)
        {
            HtmlManage _htmlManage =new HtmlManage();
            return _htmlManage.InsertHtml(htmlInfo);
        }

        /// <summary>
        /// 更新一条Html内容
        /// </summary>
        /// <param name="htmlInfo">htmlInfo Model</param>
        /// <returns></returns>
        public object[] UpdateHtml(HtmlInfo htmlInfo)
        {
            HtmlManage _htmlManage = new HtmlManage();
            return _htmlManage.UpdateHtml(htmlInfo);
        }

         /// <summary>
        /// 根据城市ID删除对应的Html内容
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public object[] DeleteHtml(int cityId)
        {
            HtmlManage _htmlManage = new HtmlManage();
            return _htmlManage.DeleteHtml(cityId);
        }

         /// <summary>
        /// 根据城市ID查询对应的Html内容
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public HtmlInfo QueryHtmlByCityId(int cityId)
        {
            HtmlManage _htmlManage = new HtmlManage();
            return _htmlManage.QueryHtmlByCityId(cityId);
        }

        /// <summary>
        /// 客户端服务员积分排名html
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public string QueryHtml(int cityId)
        {
            HtmlOperate _Operate = new HtmlOperate();//BLL
            HtmlInfo htmlInfo = _Operate.QueryHtmlByCityId(cityId);
            if (string.IsNullOrEmpty(htmlInfo.html))
            {
                htmlInfo = _Operate.QueryHtmlByCityId(0); 
            }
            string html = JsonOperate.JsonSerializer<HtmlInfo>(htmlInfo);
            return html;
        }

    }
}
