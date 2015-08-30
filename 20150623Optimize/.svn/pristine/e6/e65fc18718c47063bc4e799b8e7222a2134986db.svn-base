using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmengPush
{    
    public class AndroidPushInfo
    {
        /// <summary>
        /// 必填  应用唯一标识
        /// </summary>
        public string appkey { get; set; }
        /// <summary>
        /// 必填 时间戳，10位或者13位均可，时间戳有效期为10分钟
        /// </summary>
        public int timestamp { get; set; }
        /// <summary>
        /// 必须传此值
        /// </summary>
        public string validation_token { get; set; }
        /// <summary>
        /// 必填 消息发送类型
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 可选 设备唯一表示
        /// 当type=unicast时,必填, 表示指定的单个设备
        /// 当type=listcast时,必填,要求不超过50个,多个device_token以英文逗号间隔
        /// </summary>
        public string device_tokens { get; set; }
        /// <summary>
        /// 可选 当type=customizedcast时，必填 (Android UXIAN)
        /// </summary>
        public string alias_type { get; set; }
        /// <summary>
        /// 可选 当type=customizedcast时, 开发者填写自己的alias
        /// </summary>
        public string alias { get; set; }
        /// <summary>
        /// 可选 当type=filecast时，file内容为多条device_token, device_token以回车符分隔
        /// 当type=customizedcast时，file内容为多条alias，alias以回车符分隔
        /// </summary>
        public string file_id { get; set; }
        /// <summary>
        /// 可选 终端用户筛选条件,如用户标签、地域、应用版本以及渠道等
        /// </summary>
        public string filter { get; set; }
        /// <summary>
        /// 必填 消息内容(Android最大为824B)
        /// </summary>
        public AndroidPayload payload { get; set; }
        /// <summary>
        /// 可选 正式/测试模式。测试模式下，只会将消息发给测试设备
        /// "true/false"
        /// </summary>
        public string production_mode { get; set; }
        /// <summary>
        /// 可选 发送消息描述，建议填写
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 可选 开发者自定义消息标识ID, 开发者可以为同一批发送的多条消息提供同一个
        /// </summary>
        public string thirdparty_id { get; set; }
    }

    /// <summary>
    /// 发送策略
    /// </summary>
    public class policy
    {
        /// <summary>
        /// 定时发送时间，若不填写表示立即发送。定时发送时间不能小于当前时间
        /// 格式: "YYYY-MM-DD hh:mm:ss"。 注意, start_time只对任务生效
        /// </summary>
        public string start_time { get; set; }
        public string expire_time { get; set; }

        public string max_send_num { get; set; }
        public string out_biz_no { get; set; }
    }

    public class AndroidPayload
    {
        /// <summary>
        /// 必填 消息类型，值可以为:
        /// notification-通知，message-消息
        /// </summary>
        public string display_type { get; set; }
        public body body { get; set; }
        /// <summary>
        /// 可选 用户自定义key-value。只对"通知(display_type=notification)"生效
        /// 可以配合通知到达后, 打开App, 打开URL, 打开Activity使用
        /// </summary>
        public extra extra { get; set; }
    }
    public class body
    {
        /// <summary>
        /// 必填 通知栏提示文字
        /// </summary>
        public string ticker { get; set; }
        /// <summary>
        /// 必填 通知标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 必填 通知文字描述
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// 必填 点击"通知"的后续行为
        /// </summary>
        public string after_open { get; set; }
        /// <summary>
        /// 可选 当"after_open"为"go_url"时，必填
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 可选 当"after_open"为"go_activity"时，必填
        /// </summary>
        public string activity { get; set; }
        /// <summary>
        /// 可选 display_type=message, 或者
        /// display_type=notification且"after_open"为"go_custom"时，该字段必填
        /// 用户自定义内容, 可以为字符串或者JSON格式
        /// </summary>
        public string custom { get; set; }

    }
    public class extra
    {
        public int type { get; set; }
        public string value { get; set; }
    }       
   
    /// <summary>
    /// 点击"通知"的后续行为，默认为打开app
    /// </summary>
    public enum afterOpen
    {
        [DescriptionAttribute("打开应用")]
        go_app = 1,
        [DescriptionAttribute("跳转到URL")]
        go_url = 2,
        [DescriptionAttribute("打开特定的activity")]
        go_activity = 3,
        [DescriptionAttribute("用户自定义内容")]
        go_custom = 4
    }   
}
