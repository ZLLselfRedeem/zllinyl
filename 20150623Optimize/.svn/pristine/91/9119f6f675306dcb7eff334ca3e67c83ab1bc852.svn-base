using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 美食分享客户端webview描述配置项模型层
    /// created by wangc
    /// 20140616
    /// </summary>
    public class FoodDiariesShareConfig
    {
        public int id { get; set; }
        public string foodDiariesShareInfo { get; set; }
        public byte type { get; set; }
        public int status { get; set; }

        public virtual string typaName { get; set; }
    }

    public enum FoodDiariesShareConfigType : byte
    {
        美食日记分享页面顶部描述 = 1,
        美食日记分享页面底部描述_app = 2,
        美食日记分享页面底部描述_pc = 3
    }
}
