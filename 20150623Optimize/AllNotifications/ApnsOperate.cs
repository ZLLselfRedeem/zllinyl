using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JdSoft.Apple.Apns.Notifications;

namespace VA.AllNotifications
{
    public class ApnsOperate
    {
        public VASendApnsResponse SendApnsNotification(VASendApnsRequest sendApnsRequest)
        {
            VASendApnsResponse sendApnsResponse = new VASendApnsResponse();


            NotificationService service = new NotificationService(sendApnsRequest.sandbox, sendApnsRequest.p12FileName, sendApnsRequest.p12FilePassword, 1);
            service.SendRetries = 1; //1 retries before generating notificationfailed event
            service.ReconnectDelay = 500; //0.5 seconds

            service.Error += new NotificationService.OnError(service_Error);
            service.NotificationTooLong += new NotificationService.OnNotificationTooLong(service_NotificationTooLong);

            service.BadDeviceToken += new NotificationService.OnBadDeviceToken(service_BadDeviceToken);
            service.NotificationFailed += new NotificationService.OnNotificationFailed(service_NotificationFailed);
            service.NotificationSuccess += new NotificationService.OnNotificationSuccess(service_NotificationSuccess);
            service.Connecting += new NotificationService.OnConnecting(service_Connecting);
            service.Connected += new NotificationService.OnConnected(service_Connected);
            service.Disconnected += new NotificationService.OnDisconnected(service_Disconnected);

            //int count = sendApnsRequest.pushToken.Count;
            //for (int i = 0; i < count; i++)
            //{
            //    //Create a new notification to send
            //    Notification alertNotification = new Notification(sendApnsRequest.pushToken[i]);

            //    alertNotification.Payload.AddCustom("type", (int)sendApnsRequest.notificationsContentType);
            //    alertNotification.Payload.AddCustom("value", sendApnsRequest.notificationsContentValue);
            //    alertNotification.Payload.Alert.Body = sendApnsRequest.message;
            //    alertNotification.Payload.Sound = "default";
            //    //alertNotification.Payload.Badge = i + 1;

            //    //Queue the notification to be sent
            //    if (service.QueueNotification(alertNotification))
            //    {
            //        //sendApnsResponse.result = VANotificationsResult.VA_OK;
            //    }
            //    else
            //    {
            //        Global.result = VANotificationsResult.VA_FAILED_QUEUE;
            //    }

            //    //Sleep in between each message
            //    if (i < count)
            //    {
            //        System.Threading.Thread.Sleep(sendApnsRequest.sleepBetweenNotifications);
            //    }
            //}


            //Create a new notification to send
            string[] newPushToken = new string[sendApnsRequest.pushToken.Count];
            for (int i = 0; i < sendApnsRequest.pushToken.Count; i++)
            {
                newPushToken[i] = sendApnsRequest.pushToken[i];
            }
            Notification alertNotification = new Notification(newPushToken);

            alertNotification.Payload.AddCustom("type", (int)sendApnsRequest.notificationsContentType);
            alertNotification.Payload.AddCustom("value", sendApnsRequest.notificationsContentValue);
            alertNotification.Payload.Alert.Body = sendApnsRequest.message;
            alertNotification.Payload.Sound = "default";
            //alertNotification.Payload.Badge = i + 1;

            //Queue the notification to be sent
            if (service.QueueNotification(alertNotification))
            {
                //sendApnsResponse.result = VANotificationsResult.VA_OK;
            }
            else
            {
                Global.result = VANotificationsResult.VA_FAILED_QUEUE;
            }

            //First, close the service.  
            //This ensures any queued notifications get sent befor the connections are closed
            service.Close();

            //Clean up
            service.Dispose();
            sendApnsResponse.result = Global.result;
            return sendApnsResponse;
        }
        static void service_BadDeviceToken(object sender, BadDeviceTokenException ex)
        {
            Global.result = VANotificationsResult.VA_FAILED_BAD_DEVICE_TOKEN;
        }

        static void service_Disconnected(object sender)
        {
            Global.result = VANotificationsResult.VA_FAILED_DISCONNECTED;
        }

        static void service_Connected(object sender)
        {
            //Connected...
        }

        static void service_Connecting(object sender)
        {
            //Connecting...
        }

        static void service_NotificationTooLong(object sender, NotificationLengthException ex)
        {
            Global.result = VANotificationsResult.VA_FAILED_NOTIFICATION_TOO_LONG;
        }

        static void service_NotificationSuccess(object sender, Notification notification)
        {
            Global.result = VANotificationsResult.VA_OK;
        }

        static void service_NotificationFailed(object sender, Notification notification)
        {
            Global.result = VANotificationsResult.VA_FAILED_OTHER;
        }

        static void service_Error(object sender, Exception ex)
        {
            Global.result = VANotificationsResult.VA_FAILED_OTHER;
        }
    }
}
