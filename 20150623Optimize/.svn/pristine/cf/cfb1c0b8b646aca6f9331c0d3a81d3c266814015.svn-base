using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Collections;

namespace VAGastronomistMobileApp.DBUtility
{
    /// <summary> 
    /// FileName: NoSortHashtable.cs 
    /// CLRVersion: 4.0.30319.269 
    /// Author: TDQ 
    /// Corporation:杭州友络科技有限公司
    /// Description: 
    /// DateTime: 2012-07-23 15:21:24 
    /// </summary>
    public class NoSortHashtable : Hashtable
    {
        private ArrayList keys = new ArrayList();
        public NoSortHashtable()
        {

        }

        public override void Add(object key, object value)
        {
            base.Add(key, value);
            keys.Add(key);
        }
        public override ICollection Keys
        {
            get
            {
                return keys;
            }
        }
        public override void Clear()
        {
            base.Clear();
            keys.Clear();
        }
        public override void Remove(object key)
        {
            base.Remove(key);
            keys.Remove(key);
        }
        public override IDictionaryEnumerator GetEnumerator()
        {
            return base.GetEnumerator();
        }
    }
}
