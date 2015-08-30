using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll.Messages
{
    public class MessageFirstTitleOperate
    {
          /// <summary>
        /// 用于填充标签下拉列表
        /// </summary>
        /// <param name="CityID"></param>
        /// <returns></returns>
        public List<MessageFirstTitleViewModel> GetHandleByCity(int CityID)
        {
            MessageFirstTitleManager mftm = new MessageFirstTitleManager();
            return mftm.GetHandleByCity(CityID);
        }

        /// 获取标标签列表
        /// </summary>
        /// <param name="CityID">城市ID</param>
        /// <returns></returns>
        public DataTable MessageFirstTitles(int CityID)
        {
            MessageFirstTitleManager mftm = new MessageFirstTitleManager();
            return mftm.MessageFirstTitles(CityID);
        }

         /// <summary>
        /// 获取某条标签记录
        /// </summary>
        /// <param name="CityID">城市ID</param>
        /// <returns></returns>
        public DataTable MessageFirstTitleDetail(int ID)
        {
            MessageFirstTitleManager mftm = new MessageFirstTitleManager();
            return mftm.MessageFirstTitleDetail(ID);
        }

         /// <summary>
        /// 是否已有主要标签存在
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="CityID"></param>
        /// <returns></returns>
        public long GetCountByCityID(int ID, int CityID)
        {
            MessageFirstTitleManager mftm = new MessageFirstTitleManager();
            return mftm.GetCountByCityID(ID, CityID);
        }

        /// <summary>
        /// 是否是商户
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CityID"></param>
        /// <returns></returns>
        public long GetCountByCityIDIsMerchant(int ID, int CityID)
        {
            MessageFirstTitleManager mftm = new MessageFirstTitleManager();
            return mftm.GetCountByCityIDIsMerchant(ID, CityID);
        }
         /// <summary>
        /// 添加标签记录
        /// </summary>
        /// <param name="model">对象实体</param>
        /// <returns></returns>
        public int InsertMessageFirstTitle(MessageFirstTitle model)
        {
            MessageFirstTitleManager mftm = new MessageFirstTitleManager();
            return mftm.InsertMessageFirstTitle(model);
        }

         /// <summary>
        /// 更新标签信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateMessageFirstTitle(MessageFirstTitle model)
        {
            MessageFirstTitleManager mftm = new MessageFirstTitleManager();
            return mftm.UpdateMessageFirstTitle(model);
        }

          /// <summary>
        /// 更新状态，删除或恢复
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public int UpdateMessageFirstTitleStatus(int ID, bool Status)
        {
            MessageFirstTitleManager mftm = new MessageFirstTitleManager();
            return mftm.UpdateMessageFirstTitleStatus(ID, Status);
        }

         /// <summary>
        /// 更新是否启用
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="Enable">是否启用</param>
        /// <returns></returns>
        public int UpdateMessageFirstTitleEnable(int ID, bool Enable)
        {
            MessageFirstTitleManager mftm = new MessageFirstTitleManager();
            return mftm.UpdateMessageFirstTitleEnable(ID, Enable);
        }
    }
}
