using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
   public class AwardConfig
    {
       /// <summary>
       /// 配置ID
       /// </summary>
       public int Id { get; set; }
       /// <summary>
       /// 配置名称
       /// </summary>
       public string ConfigName { get; set; }
       /// <summary>
       /// 配置内容
       /// </summary>
       public string ConfigCotent{ get; set; }
       /// <summary>
       /// 配置描述
       /// </summary>
       public string ConfigDescription { get; set; }
    }
}
