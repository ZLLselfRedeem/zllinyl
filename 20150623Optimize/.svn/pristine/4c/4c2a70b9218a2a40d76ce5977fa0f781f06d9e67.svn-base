using System.Web.Script.Serialization;

namespace DishMenuAsynUpdate.Core
{
    public class JsonHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string Serialize<T>(T obj)
        {
            var serializer = new JavaScriptSerializer();
            //serializer.RegisterConverters(new JavaScriptConverter[] { new DateTimeJavaScriptConverter() });
            string json = serializer.Serialize(obj);
            return json;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string json)
        {
            var serializer = new JavaScriptSerializer();
            var entity = serializer.Deserialize<T>(json);
            return entity;
        }
    }
}
