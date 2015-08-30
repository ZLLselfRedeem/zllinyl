using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace UmengPush
{
    public class AndroidPush
    {
        string uploadUrl = "http://msg.umeng.com/upload";
        string apiSendUrl = "http://msg.umeng.com/api/send";
        string appkey = "526f51f856240bdc3407188a";
        string app_master_secret = "agbh1biijehf8omwkqgr5q2jly5cgiki";
        string production_mode = ConfigurationManager.AppSettings["UmengProductionMode"].Trim();

        /// <summary>
        /// 自定义播（文件上传批量别名）
        /// </summary>
        /// <param name="customType">推送场景</param>
        /// <param name="aliasList">别名</param>
        /// <param name="title"></param>
        /// <param name="text"></param>
        /// <param name="extraValue">自定义value值（订单Id，shopId等等）</param>
        /// <returns></returns>
        public bool AndroidFilecast(VANotificationsCustomType customType, List<string> aliasList, string title, string text, string extraValue)
        {
            int timestamp = Common.ConvertDateTimeInt(DateTime.Now);

            //组装要群发的别名，用换行符分隔
            StringBuilder content = new StringBuilder();
            if (aliasList != null && aliasList.Count > 0)
            {
                foreach (string alias in aliasList)
                {
                    content.Append(alias);
                    content.Append("\\r\\n");
                }
            }

            string validationToken = Common.GetMD5(appkey.ToLower() + app_master_secret.ToLower() + timestamp);

            string uploadJson = "{\"appkey\":\"" + appkey + "\",\"timestamp\":\"" + timestamp + "\",\"validation_token\":\"" + validationToken + "\",\"content\":\"" + content.ToString() + "\"}";

            string mySign = "POST" + apiSendUrl + uploadJson + app_master_secret;
            mySign = Common.GetMD5(mySign);

            //第一步，上传别名得到文件ID
            string fileResult = Common.HttpPost(uploadUrl + "?&" + mySign, uploadJson);
            SendResult uploadResult = Common.JsonDeserialize<SendResult>(fileResult);

            if (uploadResult.ret.ToUpper() == "SUCCESS")
            {
                //上传成功后再提交推送信息
                AndroidPushInfo sendInfo = new AndroidPushInfo();
                sendInfo.appkey = appkey;
                sendInfo.timestamp = timestamp;
                sendInfo.validation_token = validationToken;
                sendInfo.type = type.customizedcast.ToString();
                sendInfo.device_tokens = "";
                sendInfo.alias_type = "UXIAN";
                sendInfo.alias = "";
                sendInfo.file_id = uploadResult.data.file_id;
                sendInfo.filter = "";

                #region payload

                AndroidPayload payload = new AndroidPayload();
                payload.display_type = "notification";

                #region body
                body body = new UmengPush.body();
                body.ticker = title;
                body.title = title;
                body.text = text;
                body.url = "";
                body.custom = "";

                extra extra = new extra();
                switch (customType)
                {
                    case VANotificationsCustomType.NOTIFICATIONS_EVALUATION:
                        body.after_open = afterOpen.go_app.ToString();
                        body.activity = "";
                        extra.type = (int)VANotificationsCustomType.NOTIFICATIONS_EVALUATION;
                        extra.value = extraValue;//orderId
                        break;
                    case VANotificationsCustomType.NOTIFICATIONS_ORDER:
                        body.after_open = afterOpen.go_app.ToString();
                        body.activity = "";
                        extra.type = (int)VANotificationsCustomType.NOTIFICATIONS_ORDER;
                        extra.value = extraValue;//shopId
                        break;
                    case VANotificationsCustomType.NOTIFICATIONS_RECOMMEND:
                        body.after_open = afterOpen.go_app.ToString();
                        body.activity = "";
                        extra.type = (int)VANotificationsCustomType.NOTIFICATIONS_RECOMMEND;
                        extra.value = extraValue;//url
                        break;
                    case VANotificationsCustomType.NOTIFICATIONS_REDENVELOPE:
                        body.after_open = afterOpen.go_app.ToString();
                        body.activity = "";
                        extra.type = (int)VANotificationsCustomType.NOTIFICATIONS_REDENVELOPE;
                        extra.value = extraValue;//url
                        break;
                    case VANotificationsCustomType.NOTIFICATIONS_REDENVELOPE_GET:
                        body.after_open = afterOpen.go_app.ToString();
                        body.activity = "";
                        extra.type = (int)VANotificationsCustomType.NOTIFICATIONS_REDENVELOPE_GET;
                        extra.value = extraValue;//url
                        break;
                    case VANotificationsCustomType.NOTIFICATIONS_INDEX:
                        body.after_open = afterOpen.go_app.ToString();
                        body.activity = "";
                        extra.type = (int)VANotificationsCustomType.NOTIFICATIONS_INDEX;
                        extra.value = "-999";
                        break;
                    case VANotificationsCustomType.NOTIFICATIONS_ORDERLIST:
                        body.after_open = afterOpen.go_activity.ToString();
                        body.activity = "va.order.ui.MainContentTabActivity";
                        extra.type = (int)VANotificationsCustomType.NOTIFICATIONS_ORDERLIST;
                        extra.value = "-999";
                        break;
                    case VANotificationsCustomType.NOTIFICATIONS_ORDERDETAIL:
                        body.after_open = afterOpen.go_app.ToString();
                        body.activity = "";
                        extra.type = (int)VANotificationsCustomType.NOTIFICATIONS_ORDERDETAIL;
                        extra.value = extraValue;
                        break;
                    case VANotificationsCustomType.NOTIFICATIONS_FOODPLAZA:
                        body.after_open = afterOpen.go_activity.ToString();
                        body.activity = "va.order.ui.MainContentTabActivity";
                        extra.type = (int)VANotificationsCustomType.NOTIFICATIONS_FOODPLAZA;
                        extra.value = "-999";
                        break;
                }

                #endregion

                payload.body = body;
                payload.extra = extra;

                #endregion

                sendInfo.payload = payload;
                sendInfo.production_mode = production_mode;
                sendInfo.description = "悠先点菜";
                sendInfo.thirdparty_id = "";

                string json = Common.JsonSerializer<AndroidPushInfo>(sendInfo);
                mySign = "POST" + apiSendUrl + json + app_master_secret;
                mySign = Common.GetMD5(mySign);

                string Result = Common.HttpPost(apiSendUrl + "?&" + mySign, json);
                SendResult sendResult = Common.JsonDeserialize<SendResult>(Result);
                if (sendResult.ret == "SUCCESS")
                {
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
        /// 自定义播    
        /// 支付，入座，退款 专用，客户端点击推送后跳至点单详情页
        /// </summary>
        /// <param name="alias"></param>
        /// <param name="title"></param>
        /// <param name="text"></param>
        /// <param name="extra"></param>
        public bool AndroidCustomizedcast(string alias, string title, string text, int extraType, string extraValue)
        {
            int timestamp = Common.ConvertDateTimeInt(DateTime.Now);
            string validationToken = Common.GetMD5(appkey.ToLower() + app_master_secret.ToLower() + timestamp);

            AndroidPushInfo sendInfo = new AndroidPushInfo();
            sendInfo.appkey = appkey;
            sendInfo.timestamp = timestamp;
            sendInfo.validation_token = validationToken;
            sendInfo.type = type.customizedcast.ToString();
            sendInfo.alias_type = "UXIAN";
            sendInfo.alias = alias;
            sendInfo.device_tokens = "";
            sendInfo.file_id = "";
            sendInfo.filter = "";

            #region payload
            #region body
            body body = new UmengPush.body();
            body.ticker = title;
            body.title = title;
            body.text = text;
            body.after_open = afterOpen.go_app.ToString();
            body.activity = "";// "va.order.ui.PreOrderDetailActivity";
            body.url = "";
            body.custom = "";
            #endregion

            AndroidPayload payload = new AndroidPayload();
            payload.display_type = "notification";
            payload.body = body;

            extra extra = new UmengPush.extra();
            extra.type = extraType;
            extra.value = extraValue;//orderId
            payload.extra = extra;

            #endregion

            sendInfo.payload = payload;
            sendInfo.production_mode = production_mode;
            sendInfo.description = "悠先点菜";
            sendInfo.thirdparty_id = "";

            string json = Common.JsonSerializer<AndroidPushInfo>(sendInfo);
            string mySign = "POST" + apiSendUrl + json + app_master_secret;
            mySign = Common.GetMD5(mySign);
            string Result = Common.HttpPost(apiSendUrl + "?&" + mySign, json);
            SendResult sendResult = Common.JsonDeserialize<SendResult>(Result);
            if (sendResult.ret == "SUCCESS")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
