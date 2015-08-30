using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class ShopChannelOperate
    {
        /// <summary>
        /// 收银宝页面根据shopID来获取增值页列表
        /// </summary>
        /// <param name="shopID"></param>
        /// <returns></returns>
        public static string SelectShopChannel(int shopID)
        {
            SybMsg resultMessage = new SybMsg();
            DataTable dt = new ShopChannelManager().SelectShopChannel(shopID);
            string resultJson = Common.ConvertDateTableToJson(dt);
            resultMessage.Insert(1, resultJson);
            return resultMessage.Value;
        }

        /// <summary>
        /// 后台管理网站根据shopID获取增值页列表
        /// </summary>
        /// <param name="shopID"></param>
        /// <param name="pageType"></param>
        /// <returns></returns>
        public DataTable SelectShopChannel(int shopID, string pageType)
        {
            ShopChannelManager manager = new ShopChannelManager();
            DataTable dt = manager.SelectShopChannel(shopID, pageType);
            return dt;
        }

        public static string SwichRequest(int shopChannelID, int status)
        {
            SybMsg resultMessage = new SybMsg();
            if (status == 0)
            {
                bool result = new ShopChannelManager().UpdateStatus(shopChannelID, 0);
                if (result)
                {
                    resultMessage.Insert(1, "关闭成功");
                }
                else
                {
                    resultMessage.Insert(-1, "关闭失败");
                }
            }
            else
            {
                ShopChannel channel = SelectChannelByID(shopChannelID);
                if (channel.IsAuthorization)
                {
                    bool result = new ShopChannelManager().UpdateStatus(shopChannelID, 1);
                    if (result)
                    {
                        resultMessage.Insert(1, "开启成功");
                    }
                    else
                    {
                        resultMessage.Insert(-1, "开启失败");
                    }

                }
                else
                {
                    bool recordFlag = new ShopChannelManager().InsertRecord(channel.ChannelID, channel.ShopID);
                    resultMessage.Insert(-1, "您还未购买此增值服务，请联系我们的销售进行购买使用");
                }
            }
            return resultMessage.Value;
        }

        /// <summary>
        /// 根据shopChannelID获取ShopChannel对象
        /// </summary>
        /// <param name="shopChannelID"></param>
        /// <returns></returns>
        public static ShopChannel SelectChannelByID(int shopChannelID)
        {
            DataTable dtChannel = new ShopChannelManager().SelectChannel(shopChannelID);
            ShopChannel channel = new ShopChannel()
            {
                ID = shopChannelID,
                ChannelID = Common.ToInt32(dtChannel.Rows[0]["channelID"]),
                ShopID = Common.ToInt32(dtChannel.Rows[0]["shopID"]),
                ChanelIndex = Common.ToInt32(dtChannel.Rows[0]["channelIndex"]),
                IsAuthorization = Common.ToBool(dtChannel.Rows[0]["isAuthorization"]),
                Status = Common.ToBool(dtChannel.Rows[0]["status"]),
                CreateTime = Common.ToDateTime(dtChannel.Rows[0]["createTime"]),
                isDelete = Common.ToBool(dtChannel.Rows[0]["isDelete"])
            };
            return channel;
        }

        public static string Sort(List<Tuple<int, int>> sortedList)
        {
            SybMsg resultMessage = new SybMsg();
            foreach (var tuple in sortedList)
            {
                bool result = new ShopChannelManager().UpdateIndex(tuple.Item1, tuple.Item2);
                if (!result)
                {
                    resultMessage.Insert(-1, "排序失败");
                    return resultMessage.Value;
                }
            }
            resultMessage.Insert(1, "排序成功");
            return resultMessage.Value;
        }

        public static bool ChannelIndexIsClash(int shopID, int channelIndex)
        {
            ShopChannelManager manager = new ShopChannelManager();
            bool result = manager.IndexIsClash(shopID, channelIndex);
            return result;
        }

        /// <summary>
        /// 根据商铺id来查询所在的城市ID
        /// </summary>
        /// <param name="shopID"></param>
        /// <returns></returns>
        public static int SearchCityID(int shopID)
        {
            ShopChannelManager manager = new ShopChannelManager();
            DataTable dt = manager.SearchCityID(shopID);
            return Common.ToInt32(dt.Rows[0]["cityID"]);
        }

        public static List<Tuple<int, bool, int>> SelectAllShopChannel()
        {
            List<Tuple<int, bool, int>> shopChannels = new List<Tuple<int, bool, int>>();
            ShopChannelManager manager = new ShopChannelManager();
            DataTable dt = manager.SelectAllShopChanne();
            foreach (DataRow row in dt.Rows)
            {
                int channleID = Common.ToInt32(row["channelID"]);
                bool isAuth = Common.ToBool(row["isAuthorization"]);
                int shopID = Common.ToInt32(row["shopID"]);
                shopChannels.Add(new Tuple<int, bool, int>(channleID, isAuth, shopID));
            }
            return shopChannels;
        }

        public static bool UpdateIndex(int channelID, int channelIndex)
        {
            ShopChannelManager manager = new ShopChannelManager();
            return manager.UpdateIndex(channelID, channelIndex);
        }
    }
}
