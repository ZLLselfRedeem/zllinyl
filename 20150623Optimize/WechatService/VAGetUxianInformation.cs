using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;

namespace WechatService
{
    public class VAGetUxianInformation
    {
        public static string GetTopPrice()
        {
            WechatTopPriceOperator wop = new WechatTopPriceOperator();
            string sret = wop.GetTopOneOfTopPriceInfo();
            if (string.IsNullOrEmpty(sret))
                return "Coming Soon...";
            else
                return sret;
        }

        public static string GetFreeCase()
        {
            WechatFreeCaseOperator wfo = new WechatFreeCaseOperator();
            string sret = wfo.GetTopOneFreeCaseInfo();
            if (string.IsNullOrEmpty(sret))
                return "Coming Soon...";
            else
                return sret;
        }
        //获取本期热菜，各地区设置的热菜信息 cityName,shopName,shopAddress,DishName,DishPrice,ImageFolder,ImageName
        public static DataTable GetHotMenu()
        {
            WechatHotMenuOperate wmo = new WechatHotMenuOperate();
            
            return wmo.GetHopMenuInfo();
        }

        //亲聆老板娘 返回声音media_id ,语音文件传到微信平台上去后，会返回一个media_id，供微信客户端调用
        public static string GetLandLadysVoice()
        {
            WechatLandladyVoiceOperate wvo = new WechatLandladyVoiceOperate();
            
            return wvo.GetMediaId();
        }
    }
}
