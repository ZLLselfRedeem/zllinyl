using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.ServiceModel.Web;

namespace JdSoft.Apple.Apns.Notifications
{
	public class Notification
	{
		public string[] DeviceToken { get; set; }
		public NotificationPayload Payload { get; set; }

		public const int DEVICE_TOKEN_BINARY_SIZE = 32;
		public const int DEVICE_TOKEN_STRING_SIZE = 64;
		public const int MAX_PAYLOAD_SIZE = 256;

        string logPath = AppDomain.CurrentDomain.BaseDirectory + @"Logs\";

		public Notification()
		{
		    //DeviceToken = string.Empty;
            DeviceToken = null;
		    Payload = new NotificationPayload();
		}

		public Notification(string[] deviceToken)
		{
            //if (!string.IsNullOrEmpty(deviceToken) && deviceToken.Length != DEVICE_TOKEN_STRING_SIZE)
            //{
            //    throw new BadDeviceTokenException(deviceToken);
            //}
            foreach (string token in deviceToken)
            {
                if (string.IsNullOrEmpty(token) || token.Length != DEVICE_TOKEN_STRING_SIZE)
                {
                    System.IO.StreamWriter sw = System.IO.File.AppendText(logPath + "Trace.Log");
                    sw.WriteLine(DateTime.Now + ":" + token + ",Length:" + deviceToken.Length);
                    sw.Close();
                    throw new BadDeviceTokenException(token);
                }
                break;
            }
			DeviceToken = deviceToken;
			Payload = new NotificationPayload();
		}

		public Notification(string[] deviceToken, NotificationPayload payload)
		{
            //if (!string.IsNullOrEmpty(deviceToken) && deviceToken.Length != DEVICE_TOKEN_STRING_SIZE)
            //    throw new BadDeviceTokenException(deviceToken);
            foreach (string token in deviceToken)
            {
                if (string.IsNullOrEmpty(token) || token.Length != DEVICE_TOKEN_STRING_SIZE)
                {
                    throw new BadDeviceTokenException(token);
                }
                break;
            }
			DeviceToken = deviceToken;
			Payload = payload;
		}

		/// <summary>
		/// Object for storing state.  This does not affect the actual notification!
		/// </summary>
		public object Tag
		{
			get;
			set;
		}

		public override string ToString()
		{
			return Payload.ToJson();
		}
		

		public byte[] ToBytes(int m)
		{
			byte[] deviceToken = new byte[DeviceToken[m].Length / 2];
            for (int i = 0; i < deviceToken.Length; i++)
            {
                deviceToken[i] = byte.Parse(DeviceToken[m].Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);
            }
            if (deviceToken.Length != DEVICE_TOKEN_BINARY_SIZE)
            {
                throw new BadDeviceTokenException(DeviceToken[m]);
            }		

			byte[] deviceTokenSize = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(Convert.ToInt16(deviceToken.Length)));

			byte[] payload = Encoding.UTF8.GetBytes(Payload.ToJson());
			if (payload.Length > MAX_PAYLOAD_SIZE)
			{
				int newSize = Payload.Alert.Body.Length - (payload.Length - MAX_PAYLOAD_SIZE);
				if (newSize > 0)
				{
					Payload.Alert.Body = Payload.Alert.Body.Substring(0, newSize);
					payload = Encoding.UTF8.GetBytes(Payload.ToString());
				}
				else
				{
					do
					{
						Payload.Alert.Body = Payload.Alert.Body.Remove(Payload.Alert.Body.Length - 1);
						payload = Encoding.UTF8.GetBytes(Payload.ToString());
					}
					while (payload.Length > MAX_PAYLOAD_SIZE && !string.IsNullOrEmpty(Payload.Alert.Body));
				}

				if (payload.Length > MAX_PAYLOAD_SIZE)
					throw new NotificationLengthException(this);
			}
			byte[] payloadSize = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(Convert.ToInt16(payload.Length)));

			int bufferSize = sizeof(Byte) + deviceTokenSize.Length + deviceToken.Length + payloadSize.Length + payload.Length;
			byte[] buffer = new byte[bufferSize];

			buffer[0] = 0x00;
			Buffer.BlockCopy(deviceTokenSize, 0, buffer, sizeof(Byte), deviceTokenSize.Length);
			Buffer.BlockCopy(deviceToken, 0, buffer, sizeof(Byte) + deviceTokenSize.Length, deviceToken.Length);
			Buffer.BlockCopy(payloadSize, 0, buffer, sizeof(Byte) + deviceTokenSize.Length + deviceToken.Length, payloadSize.Length);
			Buffer.BlockCopy(payload, 0, buffer, sizeof(Byte) + deviceTokenSize.Length + deviceToken.Length + payloadSize.Length, payload.Length);
			return buffer;
		}
	}
}
