using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.Wcf;
using DishMenuAsynUpdate.Core;
using IDishMenuAsynUpdate;
using log4net;

namespace DishMenuAsynUpdate.WindowsService
{
    public partial class DishMenuAsynUpdateService : ServiceBase
    {
        public DishMenuAsynUpdateService()
        {
            InitializeComponent();
        }

        private IContainer _container;
        private ServiceHost _host;
        protected override void OnStart(string[] args)
        {
            _host = new ServiceHost(typeof(MenuService));

            _host.AddDependencyInjectionBehavior<IMenuService>(_container);
            if (_host.Description.Behaviors.Find<ServiceMetadataBehavior>() == null)
            {
                var behavior = new ServiceMetadataBehavior
                {
                    HttpGetEnabled = true,
                    HttpGetUrl = new Uri("http://127.0.0.1:9999/MenuService/metadata")
                };
                _host.Description.Behaviors.Add(behavior);
            }

            _host.Open();
        }

        protected override void OnStop()
        {
            if (_host != null)
            {
                if (_host.State == CommunicationState.Opened)
                {
                    _host.Close();
                }
            }
        }
    }
}
