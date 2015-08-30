using System;

namespace VAGastronomistMobileApp.Model.Adjunction
{
    /// <summary>
    /// 用户积分参数
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class UserIntegralParamsAttribute : Attribute
    {
        private string _paramsName;

        public UserIntegralParamsAttribute() { }

        public UserIntegralParamsAttribute(string paramsName)
        {
            this._paramsName = paramsName;
        }

        public string ParamsName { get { return _paramsName; } }
    }

    public class OrderSeatRequest
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long userId { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public long preOrder19dianId { get; set; }
    }

    //public class ShareShopRequest
    //{
    //    /// <summary>
    //    /// 用户ID
    //    /// </summary>
    //    public long userId { get; set; }
    //    /// <summary>
    //    /// 店铺ID
    //    /// </summary>
    //    public int shopId { get; set; }
    //}
}
