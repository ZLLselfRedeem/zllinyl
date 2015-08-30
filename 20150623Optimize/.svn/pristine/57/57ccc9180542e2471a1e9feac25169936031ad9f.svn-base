using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Transactions;
using System.Web;
using Autofac;
using log4net;
using LogDll;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence;
using VAGastronomistMobileApp.TheThirdPartyPaymentDll;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;
using System.Threading;

namespace TenpayRefundCheck
{
    partial class TenpayRefundCheckService : ServiceBase
    {

        public TenpayRefundCheckService()
        {
            InitializeComponent();
        }

        private Timer _timer = null;
        protected override void OnStart(string[] args)
        {
            var serviceStartTime = ConfigurationManager.AppSettings["ServiceStartTime"];
            TimeSpan ts = TimeSpan.Parse(serviceStartTime);

            var startTime = DateTime.Now.Date.Add(ts);//零晨三点
            var dueTime = startTime - DateTime.Now;
            if (dueTime < TimeSpan.Zero)
            {
                dueTime = TimeSpan.Zero;
            }
            var callback = container.Resolve<ICallback>();
            callback.Container = container;
            _timer = new Timer(callback.Run, null, dueTime, new TimeSpan(1, 0, 0, 0));
        }

        protected override void OnStop()
        {
            if (_timer != null)
            {
                _timer.Dispose();
            }
        }
    }
}
