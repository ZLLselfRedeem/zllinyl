using Autofac;
using Autofac.Integration.Web;
using System;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure
{
    public class EntityFrameworkModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException("builder");

            builder.RegisterAssemblyTypes(typeof(SqlServerPreOrder19DianInfoRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository") && t.Name.StartsWith("SqlServer"))
                .AsImplementedInterfaces()
                .InstancePerHttpRequest();


            builder.RegisterType<SqlConnectionFactory>().As<ISqlConnectionFactory>().InstancePerHttpRequest();
            builder.RegisterType<SqlServerUnitOfWork>().As<IUnitOfWork>().InstancePerHttpRequest();

        }
    }
}
