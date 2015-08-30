using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;

namespace LogDll
{
    /// <summary>
    /// 引用此类的exe程序必须在app.config中添加debugInfo字段（true or false）
    /// </summary>
    public static class LogManager
    {
        private static string logPath = string.Empty;

        private static string _debugFlag;
        private static string debugFlag
        {
            get
            {
                if (string.IsNullOrEmpty(_debugFlag))
                {
                    try
                    {
                        _debugFlag = ConfigurationManager.AppSettings["debugInfo"].Trim();
                    }
                    catch (System.Exception)
                    {
                        _debugFlag = "false";
                    }
                }
                return _debugFlag;
            }
        }
        /// <summary>
        /// 保存日志的文件夹
        /// </summary>
        private static string LogPath
        {
            get
            {
                logPath = AppDomain.CurrentDomain.BaseDirectory + @"Logs\";
                if (logPath == string.Empty || !Directory.Exists(logPath))
                {
                    FolderCreate(logPath);
                }
                return logPath;
            }
            set { logPath = value; }
        }
        private static string logFielPrefix = string.Empty;
        /// <summary>
        /// 日志文件前缀
        /// </summary>
        private static string LogFielPrefix
        {
            get { return logFielPrefix; }
            set { logFielPrefix = value; }
        }
        /// <summary>
        /// 写日志
        /// </summary>
        private static void WriteLog(string logFile, string msg)
        {
            try
            {
                System.IO.StreamWriter sw = System.IO.File.AppendText(LogPath + LogFielPrefix + logFile + ".Log");
                sw.WriteLine(msg);
                sw.Close();
            }
            catch (Exception)
            {

            }
        }
        /// <summary>
        /// 写日志
        /// </summary>
        public static void WriteLog(LogFile logFile, string msg)
        {
            switch (logFile)
            {
                case LogFile.Trace:
                    {
                        if (debugFlag == "true")
                        {
                            WriteLog(logFile.ToString(), msg);
                        }
                    }
                    break;
                case LogFile.Error:
                default:
                    {
                        WriteLog(logFile.ToString(), msg);
                    }
                    break;
            }
        }
        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="Path"></param>
        private static bool FolderCreate(string Path)
        {
            bool result = false;
            // 判断目标目录是否存在如果不存在则新建之
            if (!Directory.Exists(Path))
            {
                try
                {
                    DirectoryInfo directoryInfo = Directory.CreateDirectory(Path);
                    if (directoryInfo != null)
                    {
                        result = true;
                    }
                }
                catch
                {

                }
            }
            return result;
        }
    }
    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LogFile
    {
        Trace,
        Warning,
        Error,
        SQL,
        Login,
        QueryPreOrder,
        //处理用户未在规定时间内支付年夜饭点单服务
        MealOrderService,
    }
}
