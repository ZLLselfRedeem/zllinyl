using System;

namespace VAGastronomistMobileApp.Model
{
	/// <summary>
	/// 微信用户表
	/// </summary>
	public class WeChatUser
	{
		/// <summary>
        /// 主键
        /// </summary>
		public Guid Id { set; get; }

        /// <summary>
        /// 用户id
        /// </summary>
        public long CustomerInfo_CustomerID { set; get; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string MobilePhoneNumber { set; get; }
		
		/// <summary>
        /// 用户OpenId
        /// </summary>
		public string OpenId { set; get; }	
		
		/// <summary>
        /// 用户统一标识。针对一个微信开放平台帐号下的应用，同一用户的unionid是唯一的。
        /// </summary>
		public string UnionId { set; get; }	
		
		/// <summary>
        /// 昵称
        /// </summary>
		public string NickName { set; get; }	
		
		/// <summary>
        /// 1为男性，2为女性
        /// </summary>
		public int Sex { set; get; }	
		
		/// <summary>
        /// 省份
        /// </summary>
		public string Province { set; get; }	
		
		/// <summary>
        /// 城市
        /// </summary>
		public string City { set; get; }	
		
		/// <summary>
        /// 国家，如中国为CN
        /// </summary>
		public string Country { set; get; }	
		
		/// <summary>
        /// 用户头像
        /// </summary>
		public string HeadImgUrl { set; get; }	
		
		/// <summary>
        /// 数值代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像）
        /// </summary>
		public int? HeadImgSize { set; get; }	
		
		/// <summary>
        /// 添加时间
        /// </summary>
		public DateTime AddTime { set; get; }	
		
		/// <summary>
        /// 修改时间
        /// </summary>
		public DateTime ModifyTime { set; get; }	
		
		/// <summary>
        /// 修改人
        /// </summary>
		public string ModifyUser { set; get; }	
		
		/// <summary>
        /// 修改IP
        /// </summary>
		public string ModifyIP { set; get; }	
	}
}
