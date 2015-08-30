using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using System.Configuration;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;
using LogDll;

namespace VAMenuUpdateService
{
    public partial class VAMenuUpdate : ServiceBase
    {
        private bool stopping = false;
        DateTime menuUpdateStartTime = new DateTime();
        DateTime lastUpdateTime = new DateTime();
        string menuPath = string.Empty;
        private Hashtable updatedMenuHashTable = new Hashtable(); 

        public VAMenuUpdate()
        {
            InitializeComponent();
            lastUpdateTime = Common.ToDateTime(null);
        }
        /// <summary>
        /// 设置默认的配置
        /// </summary>
        private void SetDefaultConfig()
        {
            menuUpdateStartTime = Common.ToDateTime("00:31");
            menuPath = "";
        }
        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            #region 从配置文件读取配置信息
            try
            {
                menuUpdateStartTime = Common.ToDateTime(ConfigurationManager.AppSettings["menuUpdateStartTime"].Trim());
                menuPath = Common.ToString(ConfigurationManager.AppSettings["menuPath"].Trim());
                updatedMenuHashTable.Clear();
            }
            catch (System.Exception)
            {//取配置文件出错时设置默认值
                SetDefaultConfig();
            }
            if (menuUpdateStartTime == DateTime.MinValue)
            {//取配置文件出错时设置默认值
                SetDefaultConfig();
            }
            #endregion
            Thread menuUpdateThread = new Thread(new ThreadStart(CallInMenuUpdateThread));
            menuUpdateThread.Start();
        }
        /// <summary>
        /// MenuUpdateThread
        /// </summary>
        private void CallInMenuUpdateThread()
        {
            while (true)
            {
                DateTime nowTime = System.DateTime.Now;
                if (nowTime > menuUpdateStartTime && nowTime.Date > lastUpdateTime.Date)
                {
                    if(UpdateAllMenu())
                    {
                        lastUpdateTime = nowTime;
                        updatedMenuHashTable.Clear();
                    }
                }
                if (stopping)
                {//收到停止服务命令
                    break;
                }
                Thread.Sleep(1800000);
            }
        }
        /// <summary>
        /// 更新所有菜谱
        /// </summary>
        /// <returns></returns>
        public bool UpdateAllMenu()
        {
            try
            {
                DateTime nowDate = System.DateTime.Now.Date;
                MenuOperate menuOpe = new MenuOperate();
                DataTable dtMenu = menuOpe.QueryMenuForShopHandled();
                DataView dvMenu = dtMenu.DefaultView;
                int count = 0;
                string rowFilter = string.Empty;
                if (updatedMenuHashTable.ContainsKey(nowDate))
                {
                    rowFilter = (string)updatedMenuHashTable[nowDate];
                    dvMenu.RowFilter = rowFilter;
                }
                else
                {
                    rowFilter = "1 = 1";
                }
                for (int i = 0; i < dvMenu.Count; i++)
                {
                    int menuId = Common.ToInt32(dvMenu[i]["MenuID"]);
                    if (menuOpe.UpdateMenu(menuId, menuPath) > 0)
                    {
                        LogManager.WriteLog(LogFile.Trace, menuId.ToString());
                        count++;
                        rowFilter += " and MenuID <> " + menuId;
                        if (updatedMenuHashTable.ContainsKey(nowDate))
                        {
                            updatedMenuHashTable.Remove(nowDate);
                            updatedMenuHashTable.Add(nowDate, rowFilter);
                        }
                        else
                        {
                            updatedMenuHashTable.Add(nowDate, rowFilter);
                        }
                    }
                }
                if (count > 0 && count == dvMenu.Count)
                {
                    LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString()+" OK");
                    return true;
                }
                else
                {
                    LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString()+" not all OK");
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                LogManager.WriteLog(LogFile.Trace,ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// 停止服务
        /// </summary>
        protected override void OnStop()
        {
            stopping = true;
        }
    }
}
