using System.IO;
using System.Runtime.Remoting.Channels;
using Castle.DynamicProxy;
using System;
using System.Linq;

namespace VAGastronomistMobileApp.WebPageDll.Services.Infrastructure
{
    public class TransactionInterceptor : IInterceptor
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="invocation"></param>
        public void Intercept(IInvocation invocation)
        {
            var methodInfo = invocation.GetConcreteMethod();
            var attr = methodInfo.GetCustomAttributes(typeof(TransactionAttribute), false).FirstOrDefault() as TransactionAttribute;
            if (attr != null)
            {
                var service = (IService)invocation.InvocationTarget;
                if (service != null)
                {
                    var context = service.RepositoryContext;
                    try
                    {

                        context.UnitOfWork.BeginTransaction(attr.IsolationLevel);
                        invocation.Proceed();
                        context.UnitOfWork.Commit();
                    }
                    catch (Exception ex)
                    {
                        context.UnitOfWork.Rollback();
                        try
                        {
                            File.AppendAllText(System.Web.HttpContext.Current.Server.MapPath("~/Logs/Error.txt"), ex.ToString());
                        }
                        catch
                        {
                        }
                        
                        throw;
                    }
                    finally
                    {
                        context.UnitOfWork.Close();
                    }

                    return;
                }
            }

            invocation.Proceed();

        }
    }
}
