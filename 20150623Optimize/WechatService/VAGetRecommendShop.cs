using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
using System.Configuration;

namespace WechatService
{
    public class VAGetRecommendShop
    {
        //cityName,cityID,shopID,shopName,shopAddress,shopTelephone,c.shopLogo,c.shopImagePath,recommandType,recommandTypeName
        //return List<cityID/shopID> 列表
        public static List<string> GetRecommendShop(string cityName)
        {
            //取得图片路径
            string imgServer = "", imgFolder = "";
            try
            {
                imgServer = ConfigurationManager.AppSettings["Server"].ToString();
                imgFolder = ConfigurationManager.AppSettings["ImagePath"].ToString();
            }
            catch { }

            List<string> listRet = new List<string>();
            WechatRecommandShopOperator wso = new WechatRecommandShopOperator();
            DataTable dt = wso.GetRecommandShopInfo(cityName);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    StringBuilder sb = new StringBuilder(dr["cityName"].ToString());
                    sb.Append(";");
                    sb.Append(dr["cityID"].ToString());
                    sb.Append(";");
                    sb.Append(dr["shopID"].ToString());
                    sb.Append(";");
                    sb.Append(dr["shopName"].ToString());
                    sb.Append(";");
                    //sb.Append(dr["shopAddress"].ToString());
                    //sb.Append(";");
                    //sb.Append(dr["shopTelephone"].ToString());
                    //sb.Append(";");
                    sb.Append(imgServer + "/" + imgFolder + dr["shopImagePath"].ToString() + dr["shopLogo"].ToString());
                    //sb.Append(dr["shopLogo"].ToString());
                    //sb.Append(";");
                    //sb.Append(dr["shopImagePath"].ToString());
                    sb.Append(";");
                    sb.Append(dr["recommandType"].ToString());
                    listRet.Add(sb.ToString());
                }
            }
            return listRet;
        }

    }
}
