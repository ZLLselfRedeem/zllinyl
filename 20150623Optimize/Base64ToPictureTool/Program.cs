using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.Web;
using CloudStorage;
using PagedList;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;
using Task = System.Threading.Tasks.Task;

namespace Base64ToPictureTool
{
    class Program
    {
        static void Main(string[] args)
        {
            //string base64 =
            //    "/9j/4AAQSkZJRgABAQAAAQABAAD/4QBYRXhpZgAATU0AKgAAAAgAAgESAAMAAAABAAEAAIdpAAQAAAABAAAAJgAAAAAAA6ABAAMAAAABAAEAAKACAAQAAAABAAAAZKADAAQAAAABAAAAZAAAAAD/2wBDAB8VFxsXEx8bGRsjIR8lL04yLysrL19ESDhOcGN2dG5jbWt8jLKXfISphmttm9Odqbi+yMrIeJXb6tnC6bLEyMD/2wBDASEjIy8pL1syMlvAgG2AwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMD/wAARCABkAGQDASIAAhEBAxEB/8QAHwAAAQUBAQEBAQEAAAAAAAAAAAECAwQFBgcICQoL/8QAtRAAAgEDAwIEAwUFBAQAAAF9AQIDAAQRBRIhMUEGE1FhByJxFDKBkaEII0KxwRVS0fAkM2JyggkKFhcYGRolJicoKSo0NTY3ODk6Q0RFRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqDhIWGh4iJipKTlJWWl5iZmqKjpKWmp6ipqrKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uHi4+Tl5ufo6erx8vP09fb3+Pn6/8QAHwEAAwEBAQEBAQEBAQAAAAAAAAECAwQFBgcICQoL/8QAtREAAgECBAQDBAcFBAQAAQJ3AAECAxEEBSExBhJBUQdhcRMiMoEIFEKRobHBCSMzUvAVYnLRChYkNOEl8RcYGRomJygpKjU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6goOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4uPk5ebn6Onq8vP09fb3+Pn6/9oADAMBAAIRAxEAPwDFooooAKKKKACmlwKR37Co6AHh/Wn1DShiKAJqKQHIpaACiiigAooooAKKKKACmucLTqjk7UAMoroLfw4k1pHIZ2V3UN0yBmmv4ZkA+S5Un3XFAGDRW1F4buWJ8ySNB7c5qwnhgZ+e6OPZP/r0Ac+hw1S1Pq1gNPuVjVyysu4E9ar0ALRRRQAUUUUAFFFFABTHBZlA6ngU+rr2yRaXBdqF3FxzyST3oA6aFktrWGOaRVKoF5OM4FTJIj/cdW+hzWbqFrvYSJaxy7h8zMCxHoMelV7KzmsnF01vEp2Euqk8D06+lAG3UZuIQcGaMH03CoL5nlhaCJA7SRk/McDH+TWWumywTFBZQSLgbSQefXJzxQBF4pjb7RDL1Urt/HOf61kDpW/q8KB7C1CqqM5yuSQOn6c1k6hAttevEoAAAyAcgGgCvRRRQAUUUUAFFFFABVmAtLZXFvkEKPNXJ6EdcVWpCKAO3tnEttFIP4kB/Sku/wDj0m/65t/Kquhvv0qHnoCPyNOvpkeJol81mPGIlJ/OgCzHGvySfxbNv4VLWbFdXCtH9ohlRNo+7HkZ9+pq/HIkq7kbIoAx9XbdqcHQiCJpDk4/z0rAZ2kdpHOWY5Jq5rj79WlGeFAX9KpUAFFFFABRRRQAUUUUAFFFFAGv4f1BYGNpMcK5yjH19K3ZoFfLjzN2OiyFc/ka4ojIrc0nWlRPIvXxj7sh7/WgDVigLg7zcR46Bpf8DS3VzFp9qXkY4AwoJyWNVv7dsgkjGTlSQAActXO3l3LfTmWU8fwr2UUARyyNNM8r/edixptFFABRRRQAUUUUAFFW7fTp7iHzk2hCcAk9akOkzj+OPH1P+FAFCitAaNcn+KP86gurCa0RXk2lWOAVOeaAK1FFFACYHpS0UUAFFFFABRRRQAUUUUATwXtzAmyKUquc4wD/ADp/9pXn/Pb/AMdX/CiigBf7Uvf+e5/75H+FRT3c9yFE0hYDoMAYoooGyGiiigQUUUUAFFFFABRRRQAUUUUAf//Z";

            //byte[] bytes = Convert.FromBase64String(base64);
            //if (bytes != null && bytes.Length > 0)
            //{
            //    using (var stream = new System.IO.MemoryStream(bytes))
            //    {
            //        using (var image = Image.FromStream(stream))
            //        {
            //            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Guid.NewGuid().ToString("N") + ".jpg");
            //            image.Save(imagePath);
            //        }
            //    }
            //}
            //const int pageSize = 1;

            //using (var scope = Bootstrapper.Container.BeginLifetimeScope(WebLifetime.Request))
            //{
            //    ICustomerInfoRepository customerInfoRepository = scope.Resolve<ICustomerInfoRepository>();
            //    int pageIndex = 1;
            //    IPagedList<CustomerInfo> customers;
            //    do
            //    {
            //        customers = customerInfoRepository.GetPage(new Page(pageIndex, pageSize));
            //        pageIndex++;
            //    } while (customers != null && customers.HasNextPage);
            //}

           

            var task = Task.Factory.StartNew<IPagedList<CustomerInfo>>(Go, 1);
            task.ContinueWith(GoGo1);

            task.Wait();

            Console.WriteLine("处理完成");
            Console.Read();
        }

        private static IPagedList<CustomerInfo> Go(object i)
        {
            const int pageSize = 20;
            int pageIndex = (int)i;
            using (var scope = Bootstrapper.Container.BeginLifetimeScope(WebLifetime.Request))
            {
                ICustomerInfoRepository customerInfoRepository = scope.Resolve<ICustomerInfoRepository>();
                var customers = customerInfoRepository.GetPage(new Page(pageIndex, pageSize));
                return customers;
            }
        }

        private static void GoGo1(Task<IPagedList<CustomerInfo>> task)
        {
            IPagedList<CustomerInfo> result = task.Result;
            Console.WriteLine("{0},{1}", result.Count, result.HasNextPage);
            if (result.HasNextPage)
            {
                Task.Factory.StartNew<IPagedList<CustomerInfo>>(Go, result.PageNumber + 1).ContinueWith(GoGo1);
            }

            foreach (var customerInfo in result)
            {
                CustomerInfo c = customerInfo;
                Task.Factory.StartNew(Update, c);
            }

        }


        private static void Update(object obj)
        {
            var customer = (CustomerInfo)obj;
            if (customer != null && !string.IsNullOrWhiteSpace(customer.personalImgInfo))
            {
                byte[] bytes = Convert.FromBase64String(customer.personalImgInfo);
                if (bytes.Length > 0)
                {
                    using (var scope = Bootstrapper.Container.BeginLifetimeScope(WebLifetime.Request))
                    {
                        ICustomerInfoRepository customerInfoRepository = scope.Resolve<ICustomerInfoRepository>();
                        try
                        {
                            string imageName = Guid.NewGuid().ToString("N") + ".jpg";


                            AL2(bytes, "customer/" + (customer.RegisterDate.HasValue ? customer.RegisterDate.Value.ToString("yyyyMM") : "201408") + "/", imageName);

                            customerInfoRepository.UpdateCustomerPicture(customer.CustomerID, imageName);
                        }
                        catch (Exception)
                        {

                            //throw;
                        }
                    }
                }
            }
        }

        private static void AL(byte[] bytes, string imageName)
        {
            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images",
                                       imageName);
            using (var stream = new MemoryStream(bytes))
            {
                using (var image = Image.FromStream(stream))
                {
                    image.Save(imagePath);
                }
            }
        }

        private static void AL2(byte[] bytes, string path, string imageName)
        {
            string imagePath = WebConfig.ImagePath + path + imageName;
            using (var stream = new MemoryStream(bytes))
            {
                CloudStorageOperate.PutObject(imagePath, stream);
            }
        }
    }
}
