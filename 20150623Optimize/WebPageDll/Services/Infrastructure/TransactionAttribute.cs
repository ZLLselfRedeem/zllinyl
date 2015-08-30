using System;
using System.Data;

namespace VAGastronomistMobileApp.WebPageDll.Services.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class TransactionAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public TransactionAttribute()
        {
            this.IsolationLevel = IsolationLevel.Unspecified;
        }

        /// <summary>
        /// 
        /// </summary>
        public IsolationLevel IsolationLevel { get; set; }
    }
}
