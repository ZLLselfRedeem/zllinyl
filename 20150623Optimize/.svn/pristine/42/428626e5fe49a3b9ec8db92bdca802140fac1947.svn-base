<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TreasureChest.aspx.cs" Inherits="AppPages_RedEnvelope_TreasureChest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
    <meta name="format-detection" content="telephone=no" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="renderer" content="webkit" />
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="Cache-Control" content="no-cache, must-revalidate" />
    <meta http-equiv="expires" content="Wed, 26 Feb 1997 08:21:57 GMT" />
    <title>悠先点菜</title>
    <link href="../../Css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../tron/bootstrap/css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/TreasureChest.css?v=10" rel="stylesheet" />
</head>
<body>
  <!--
    1、未领过 .toGet101
    2、已领过 .gotten1004
    3、未开始 .notBegin3
    4、已结束 .over2
    5、未找到 .noActivity-6
  -->
  
  <!--未领取-->
  <div class="box toGet101 hide">
    <img src="http://image220.u-xian.com/UploadFiles/Images/TreasureChest/box_a.jpg" class="img-responsive" />
    <div class="info">
      <p class="money"></p>
      <div class="con">
        <input type="tel" name="mobilePhone" id="mobilePhone" placeholder="请输入手机号码" />
        <input type="button" id="getRedEnvelopeBtn" value="马上拿钱" />
      </div>
      <p class="desc">红包将打入您的悠先账户（通过手机号码登录）</p>
    </div>
  </div>
  
  <!--活动未开始-->
  <div class="notBegin notBegin3 hide">
    <i class="fa fa-clock-o"></i>
    <div class="text-center">距离活动开始还有</div>
    <div class="time">
      <span id="day_show"></span>
      <span id="hour_show"></span>
      <span id="minute_show"></span>
      <span id="second_show"></span>
    </div>
  </div>
  
  <!--已领取-->
  <div class="gotten gotten1004 hide">
    <img src="http://image220.u-xian.com/UploadFiles/Images/TreasureChest/unlock_over_a.jpg" class="img-responsive" />
    <div class="info">
      <p class="money"></p>
      <div class="con" id="changebox">
        <span></span>
        <a href="javascript:;" id="change">号码纠错</a>
      </div>
      <input class="downLoadUX cbtn" type="button" value="下载悠先" />
    </div>
  </div>
  
  <!--活动结束-->
  <div class="activityOver over2 hide">
    <img src="http://image220.u-xian.com/UploadFiles/Images/TreasureChest/game_over_a.png" class="img-responsive" />
    <div class="info">
      <input class="downLoadUX cbtn" type="button" value="下载悠先" />
    </div>
  </div>
  
  <!--找不到活动页-->
  <div class="unfind noActivity-6 hide"><img src="http://image220.u-xian.com/UploadFiles/Images/TreasureChest/Activity404.png" class="img-responsive" /></div>
  
  <!--404-->
  <div class="bang bang500 hide"><img src="http://image220.u-xian.com/UploadFiles/Images/TreasureChest/server_bang.png" class="img-responsive" /></div>
  
  
  <!--悠先排行榜-->
  <div class="redEnvelope toGet101 gotten1004 over2 hide">
    <div class="title"><span>悠先排行榜</span></div>
    <ul id="randlist">
      <li>
        <div class="level level1 float_right">1</div>
        <span>159XXXX3591</span>
        <span class="allGet">累积已领￥844.6</span>
        <span class="display_block des">以后点菜就用悠先了</span>
      </li>
      <li>
        <div class="level level2 float_right">2</div>
        <span>159XXXX1524</span>
        <span class="allGet">累积已领￥829.4</span>
        <span class="display_block des">以后点菜就用悠先了</span>
      </li>
      <li>
        <div class="level level3 float_right">3</div>
        <span>130XXXX0518</span>
        <span class="allGet">累积已领￥784.5</span>
        <span class="display_block des">以后点菜就用悠先了</span>
      </li>
      <li>
        <div class="level level4 float_right">4</div>
        <span>134XXXX6512</span>
        <span class="allGet">累积已领￥757.7</span>
        <span class="display_block des">以后点菜就用悠先了</span>
      </li>
      <li>
        <div class="level level5 float_right">5</div>
        <span>151XXXX1695</span>
        <span class="allGet">累积已领￥754.4</span>
        <span class="display_block des">以后点菜就用悠先了</span>
      </li>
    </ul>
  </div>
  
  <!--适用门店·杭州-->
  <div class="suitable-shop toGet101 gotten1004 over2 hide">
    <div class="title"><span>适用门店·杭州</span></div>
    <div class="content">
      Thank U Mom；外婆家（华浙店、计量大厦店、杭报店、古墩印象城店）；炉鱼（利星店、城西银泰店、西溪印象城店）；老头儿油爆虾（星光店、武林店、文三路店、下沙店、邻水•隔壁、古水店、萧山银隆
店）；热意餐厅（城西银泰店、青芝坞店）；灶丰年间（满陇桂雨店、运河天地店、西溪天堂店、定安路店）；翠庄西溪店、名人名家文二店；7080；岽阳餐馆长生路店；Pato土豆堡；喜鹊餐厅；火狐狸
玉古店；胖子鱼头（南环路店、万塘路店）；江南阿二（运河店、龙井店、塘栖店）；北疆饭店；7017中财店；九重天迷踪蟹；半瓶子陶艺咖啡吧；隐竹亚洲美食厨房；神马公社；摩嘉果木牛扒；Baby-jerry……等千家门店
    </div>
  </div>
  
  <!--适用门店·上海-->
  <div class="suitable-shop toGet101 gotten1004 over2 hide">
    <div class="title"><span>适用门店·上海</span></div>
    <div class="content">
      新白鹿（张杨路店）；鸿福记（莲花国际店）；千秋膳房（百脑汇店、正大乐城店、常熟路店、上海日月光店）；西贝莜面村（金谊广场）；97港式料理；LAROSA南京西路店；Awana满金香；Fungo；老长沙跳跳蛙；米神餐厅（陕西南路店）；卢大姐四川简阳羊肉汤（糟溪北路店）；
醉匠烧；蜀锦川；绿米；重口味麻辣料理（南京西路店）；牛肉藏烧&日本料理（上海）；半山小馆818广场；一品鹅；饭小馆；糕福奇；玛格萝妮披萨屋；名根西北菜；巴麦隆烤肉（延长路店、虹口店、政通路店）……等千家门店
    </div>
  </div>
  
  <!--红包怎么抢，怎么用-->
  <div class="activityRule toGet101 gotten1004 hide">
    <div class="title"><span>红包怎么抢，怎么用</span></div>
    <ol id="rule"></ol>
  </div>
  
  <!--悠先点菜-->
  <div class="whatisUX toGet101 gotten1004 over2 hide">
    <div class="title"><span>悠先点菜</span></div>
    <ol>
      <li>省时，不受时间地点限制，查看高清图片菜单，点菜，支付，开发票，一键完成。</li>
      <li>省力，手机尾号就能验证，到店直接吃，吃完直接走，小伙伴都惊呆了，好嘛！</li>
      <li>省钱，每月新菜试吃，返现，抢红包.活动不重样，让你拿奖拿到手发软啊！</li>
    </ol>
  </div>
  
  <!--下载悠先-->
  <div class="downLoadUX gotten1004 over2 hide dio">下载悠先</div>
  
  <!--弹出框-->
  <div id="msgBg" class="hide"></div>
  <div id="showMsg" class="hide">
    <p class="currTitle error hide">出错了</p>
    <div class="numberCurrect hide">
      <p class="currTitle">号码纠错</p>
      <p class="currDesc">手别抖，只能修改一次哦~</p>
      <input type="tel" id="newPhoneNumber" value="" />
      <div class="correctBtn"><a href="javascript:;" id="cancel">取消</a></div>
      <div class="correctBtn"><a href="javascript:;" style="border:none;" id="confirm">确定</a></div>
    </div>
  </div>
  
	<script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
  <script src="../../tron/coms/jquery/jquery-2.1.1/jquery-2.1.1.min.js"></script>
  <script src="js/jquery.cookie.js"></script>
  <script src="../../Scripts/bootstrap.min.js"></script>
  <script>window.state = '<%=state%>';</script>
  <script src="js/treasurechest.js?v=2" type="text/javascript"></script>
  <script> $(function () { new redenvelopee(false); }); </script>
  
</body>
</html>
