using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmengPush
{

    public class IOSPush
    {
        string uploadUrl = "http://msg.umeng.com/upload";
        string apiSendUrl = "http://msg.umeng.com/api/send";
        //string appkey = "526f494b56240bdc41067d21";
        //string app_master_secret = "hc98mm0k1enwictnzaht9jxj3opk6l2k";
        string appkey = "55b195f667e58e11b4003e2f";
        string app_master_secret = "caldulwribqrxtzqqech6sko1ks6ya9w";
        string production_mode = ConfigurationManager.AppSettings["UmengProductionMode"].Trim();

        public bool IOSFilecast(VANotificationsCustomType customType, List<string> device_tokens, string title, string text, string extraValue)
        {
            int timestamp = Common.ConvertDateTimeInt(DateTime.Now);
            string validationToken = Common.GetMD5(appkey.ToLower() + app_master_secret.ToLower() + timestamp);

            //组装要群发的PushToken，用换行符分隔
            StringBuilder content = new StringBuilder();
            if (device_tokens != null && device_tokens.Count > 0)
            {
                foreach (string token in device_tokens)
                {
                    content.Append(token);
                    content.Append("\\r\\n");
                }
            }

            string uploadJson = "{\"appkey\":\"" + appkey + "\",\"timestamp\":\"" + timestamp + "\",\"validation_token\":\"" + validationToken + "\",\"content\":\"" + content.ToString() + "\"}";

            string mySign = "POST" + apiSendUrl + uploadJson + app_master_secret;

            mySign = Common.GetMD5(mySign);

            //第一步，上传别名得到文件ID
            string fileResult = Common.HttpPost(uploadUrl + "?&" + mySign, uploadJson);
            SendResult uploadResult = Common.JsonDeserialize<SendResult>(fileResult);
            if (uploadResult.ret.ToUpper() == "SUCCESS")
            {
                //上传成功后再提交推送信息
                IOSPushInfo sendInfo = new IOSPushInfo();
                sendInfo.appkey = appkey;
                sendInfo.timestamp = timestamp;
                sendInfo.validation_token = validationToken;
                sendInfo.type = type.filecast.ToString();
                sendInfo.file_id = uploadResult.data.file_id;

                #region payload

                IOSPayload payload = new IOSPayload();

                #region aps
                aps aps = new UmengPush.aps();
                aps.alert = text;
                aps.category = "";

                payload.aps = aps;

                switch (customType)
                {
                    case VANotificationsCustomType.NOTIFICATIONS_EVALUATION:
                        payload.type = (int)VANotificationsCustomType.NOTIFICATIONS_EVALUATION;
                        payload.value = extraValue;//orderId
                        break;
                    case VANotificationsCustomType.NOTIFICATIONS_ORDER:
                        payload.type = (int)VANotificationsCustomType.NOTIFICATIONS_ORDER;
                        payload.value = extraValue;//shopId
                        break;
                    case VANotificationsCustomType.NOTIFICATIONS_RECOMMEND:
                        payload.type = (int)VANotificationsCustomType.NOTIFICATIONS_RECOMMEND;
                        payload.value = extraValue;//url
                        break;
                    case VANotificationsCustomType.NOTIFICATIONS_REDENVELOPE:
                        payload.type = (int)VANotificationsCustomType.NOTIFICATIONS_REDENVELOPE;
                        payload.value = extraValue;//url
                        break;
                    case VANotificationsCustomType.NOTIFICATIONS_REDENVELOPE_GET:
                        payload.type = (int)VANotificationsCustomType.NOTIFICATIONS_REDENVELOPE_GET;
                        payload.value = extraValue;//url
                        break;
                    case VANotificationsCustomType.NOTIFICATIONS_INDEX:
                        payload.type = (int)VANotificationsCustomType.NOTIFICATIONS_INDEX;
                        payload.value = "-999";
                        break;
                    case VANotificationsCustomType.NOTIFICATIONS_ORDERLIST:
                        payload.type = (int)VANotificationsCustomType.NOTIFICATIONS_ORDERLIST;
                        payload.value = "-999";
                        break;
                    case VANotificationsCustomType.NOTIFICATIONS_ORDERDETAIL:
                        payload.type = (int)VANotificationsCustomType.NOTIFICATIONS_ORDERDETAIL;
                        payload.value = extraValue;//orderId
                        break;
                    case VANotificationsCustomType.NOTIFICATIONS_FOODPLAZA:
                        payload.type = (int)VANotificationsCustomType.NOTIFICATIONS_FOODPLAZA;
                        payload.value = "-999";
                        break;
                }

                #endregion


                #endregion

                sendInfo.payload = payload;
                sendInfo.production_mode = production_mode;
                sendInfo.description = "悠先点菜";
                sendInfo.thirdparty_id = "";

                string json = Common.JsonSerializer<IOSPushInfo>(sendInfo);

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

        public bool IOSUnicast(string device_tokens, string text, string extraValue, VANotificationsCustomType customType)
        {
            int timestamp = Common.ConvertDateTimeInt(DateTime.Now);
            string validationToken = Common.GetMD5(appkey.ToLower() + app_master_secret.ToLower() + timestamp);

            IOSPushInfo sendInfo = new IOSPushInfo();
            sendInfo.appkey = appkey;
            sendInfo.timestamp = timestamp;
            sendInfo.validation_token = validationToken;
            sendInfo.type = type.unicast.ToString();
            sendInfo.device_tokens = device_tokens;
            sendInfo.alias = "";
            sendInfo.alias_type = "";
            sendInfo.file_id = "";
            sendInfo.filter = "";

            #region payload

            #region aps
            aps aps = new aps();
            aps.alert = text;
            aps.category = "";

            #endregion

            IOSPayload payload = new IOSPayload();
            payload.aps = aps;
            payload.type = (int)customType;
            payload.value = extraValue;

            #endregion

            sendInfo.payload = payload;
            sendInfo.production_mode = production_mode;
            sendInfo.description = "悠先点菜";
            sendInfo.thirdparty_id = "";

            string json = Common.JsonSerializer<IOSPushInfo>(sendInfo);

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
