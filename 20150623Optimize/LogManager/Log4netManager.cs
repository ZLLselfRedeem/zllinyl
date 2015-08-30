using log4net;
using log4net.Layout;
using log4net.Layout.Pattern;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LogDll
{
    /// <summary>
    /// Log4net日志记录 add by zhujinlei 2015/05/30
    /// </summary>
    public static class Log4netManager
    {
        private static string basePath = AppDomain.CurrentDomain.BaseDirectory;
        //private static string path = basePath.Substring(0, basePath.Substring(0, basePath.LastIndexOf('\\')).LastIndexOf('\\')) + "\\LogManager\\";
        private static string fileFullPath = Path.Combine(basePath, "logConfig.xml");
        private static FileInfo fileInfo = new System.IO.FileInfo(fileFullPath);
        
        public static void WriteLog(string msg)
        {
            log4net.Config.XmlConfigurator.Configure(fileInfo);
            //创建日志记录组件实例  
            ILog log =log4net.LogManager.GetLogger("fileLog");
            log.Info(msg);
        }

        public static void WriteLogTimeOut(string msg)
        { 
            log4net.Config.XmlConfigurator.Configure(fileInfo);
            //创建日志记录组件实例  
            ILog log = log4net.LogManager.GetLogger("fileTimeOutLog");
            log.Info(msg);
        }

        public static void WriteLogDB(string requestType,int type,string msg,DateTime requestDateTime,Guid requestGuid)
        {
            if (string.IsNullOrEmpty(msg))
            {
                return;
            }
            try
            {
                log4net.ILog logToSQL = log4net.LogManager.GetLogger("adonetLog");   
                LogContent objLogContent = new LogContent(requestType, type, msg, requestDateTime,requestGuid);
                logToSQL.Info(objLogContent);
            }
            catch
            {
                WriteLog("日志写入数据库出错！"+msg);
            }
            
        }
    }

    public class LogContent
    {
        private string _requestType;
        private int _type;
        private string _uuid="";
        private string _cookie="";
        private string _msg;
        private DateTime _requestDateTime;
        private DateTime _logDateTime;
        private Guid _requestGuid;
        public LogContent(string requestType, int type, string msg, DateTime requestDateTime,Guid requestGuid)
        {
            _uuid = msg.Substring(msg.IndexOf("uuid") + 7, 36);
            _cookie = msg.Substring(msg.IndexOf("cookie") + 9, 54);
            _requestType = requestType;
            _requestDateTime = requestDateTime;
            _type = type;
            _msg = msg;
            _requestGuid = requestGuid;
        }

        /// <summary>
        /// 日志类型(request,response,timeout)
        /// </summary>
        public string RequestType
        {
            get { return _requestType; }
            set { _requestType = value; }
        }

        /// <summary>
        /// 记录请求和返回同一性的guid
        /// </summary>
        public Guid RequestGuid
        {
            get { return _requestGuid; }
            set { _requestGuid = value; }
        }

        /// <summary>
        /// 访问用户对应的设备uuid
        /// </summary>
        public string Uuid
        {
            get { return _uuid; }
            set { _uuid = value; }
        }

        /// <summary>
        /// 访问用户对应的Cookie
        /// </summary>
        public string Cookie
        {
            get { return _cookie; }
            set { _cookie = value; }
        }

        /// <summary>
        /// 接口对应的类型
        /// </summary>
        public int Type
        {
            get { return _type; }
            set { _type = value; }
        }

        /// <summary>
        /// 请求的消息或者返回的消息
        /// </summary>
        public string Msg
        {
            get { return _msg; }
            set { _msg = value; }
        }

        /// <summary>
        /// 请求时间
        /// </summary>
        public DateTime RequestDateTime
        {
            get { return _requestDateTime; }
            set { _requestDateTime = value; }
        }
    }

    public class Log4Layout : PatternLayout
    {
        public Log4Layout()
        {
            this.AddConverter("property", typeof(MyMessagePatternConverter));
        }
    }

    public class MyMessagePatternConverter : PatternLayoutConverter
    {
        protected override void Convert(System.IO.TextWriter writer, log4net.Core.LoggingEvent loggingEvent)
        {
            if (Option != null)
            {
                // Write the value for the specified key
                WriteObject(writer, loggingEvent.Repository, LookupProperty(Option, loggingEvent));
            }
            else
            {
                // Write all the key value pairs
                WriteDictionary(writer, loggingEvent.Repository, loggingEvent.GetProperties());
            }
        }

        /// <summary>
        /// 通过反射获取传入的日志对象的某个属性的值
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        private object LookupProperty(string property, log4net.Core.LoggingEvent loggingEvent)
        {
            object propertyValue = string.Empty;

            PropertyInfo propertyInfo = loggingEvent.MessageObject.GetType().GetProperty(property);
            if (propertyInfo != null)
                propertyValue = propertyInfo.GetValue(loggingEvent.MessageObject, null);

            return propertyValue;
        }
    }
}
