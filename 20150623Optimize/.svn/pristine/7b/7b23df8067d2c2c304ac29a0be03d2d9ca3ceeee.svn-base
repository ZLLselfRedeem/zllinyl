using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace VAGastronomistMobileApp.WebPageDll.Services.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
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
    }

    /// <summary>
    /// 
    /// </summary>
    public class DateTimeJavaScriptConverter : JavaScriptConverter
    {
        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            return new JavaScriptSerializer().ConvertToType(dictionary, type);
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            if (!(obj is DateTime)) return null;
            return new CustomString(((DateTime)obj).ToUniversalTime().ToString("O"));
        }

        public override IEnumerable<Type> SupportedTypes
        {
            get { return new[] { typeof(DateTime) }; }
        }

        private class CustomString : Uri, IDictionary<string, object>
        {
            public CustomString(string str)
                : base(str, UriKind.Relative)
            {
            }

            public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
            {
                throw new NotImplementedException();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public void Add(KeyValuePair<string, object> item)
            {
                throw new NotImplementedException();
            }

            public void Clear()
            {
                throw new NotImplementedException();
            }

            public bool Contains(KeyValuePair<string, object> item)
            {
                throw new NotImplementedException();
            }

            public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
            {
                throw new NotImplementedException();
            }

            public bool Remove(KeyValuePair<string, object> item)
            {
                throw new NotImplementedException();
            }

            public int Count { get; private set; }
            public bool IsReadOnly { get; private set; }
            public bool ContainsKey(string key)
            {
                throw new NotImplementedException();
            }

            public void Add(string key, object value)
            {
                throw new NotImplementedException();
            }

            public bool Remove(string key)
            {
                throw new NotImplementedException();
            }

            public bool TryGetValue(string key, out object value)
            {
                throw new NotImplementedException();
            }

            public object this[string key]
            {
                get { throw new NotImplementedException(); }
                set { throw new NotImplementedException(); }
            }

            public ICollection<string> Keys { get; private set; }
            public ICollection<object> Values { get; private set; }
        }
    }
}
