using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll.OrderOptimization
{
    public class ChannelOperate
    {
        public static List<Tuple<int, string, int>> SelectAllChannel()
        {
            List<Tuple<int, string, int>> channels = new List<Tuple<int, string, int>>();
            DataTable dt = new ChannelManager().SelectAllChannel();
            foreach (DataRow row in dt.Rows)
            {
                int id = Common.ToInt32(row["id"]);
                string channelName = Common.ToString(row["channelName"]);
                int cityID = Common.ToInt32(row["cityID"]);
                channels.Add(new Tuple<int, string, int>(id, channelName, cityID));
            }
            return channels;
        }
    }
}
