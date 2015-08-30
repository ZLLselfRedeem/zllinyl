using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmengPush
{
    public class IOSPushInfo
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
        /// 此值必须传，账号校验
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
        /// 必填 消息内容(iOS最大为2012B)
        /// </summary>
        public IOSPayload payload { get; set; }
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

    public class IOSPayload
    {
        public aps aps { get; set; }
        public int type { get; set; }
        public string value { get; set; }
    }

    public class aps
    {
        public string alert { get; set; }
        public string category { get; set; }
    }
}
