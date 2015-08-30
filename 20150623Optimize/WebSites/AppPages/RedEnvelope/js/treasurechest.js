var appid = 'wx3f9ff3043cbcb3b7';
var lineLink = window.location.protocol + '//' + window.location.host + '/AppPages/RedEnvelope/TreasureChest.aspx?activityId=';
var imgUrl = 'http://image.u-xian.com/UploadFiles/Images/TreasureChest/weixing_shared.png';
var shareTitle = '';
var descContent = '';
var sharedContents = ['今天又领到悠先红包{0}元，每天累积，吃霸王餐不是梦，哈哈思密哒…', '今天又领到悠先红包{0}元，每天累积，吃霸王餐不是梦，哈哈思密哒…'];
var isShowWeiXinShareButton = false;
var isShowMask = false;
// 红包主要对象函数
function redenvelopee(dev) {
    this.dev = !!dev;
    this.qs = getQueryStringArgs();
    this.wx = new wxContext(this, !!dev);
    this.mobile = null;
    this.retry = true;
    this.timer = null;
    this.init(this.device());
}

// 红包初始化
redenvelopee.prototype.init = function (device) {
    var iswx = window.navigator.userAgent.match(/MicroMessenger/i);
    var isad = device.versions.android;
    var isio = device.versions.ios;

    var mobilePhoneNumber = $.cookie('mobilePhoneNumber')
		, cookie;

    this.iswx = iswx; // 判断是否为微信
    this.isad = isad; // 判断是否为安卓
    this.isio = isio; // 判断是否为苹果

    if (this.qs.mobilephone) {
        mobilePhoneNumber = this.qs.mobilephone;
    };

    if (iswx) {
        cookie = this.qs.cookie;
    };

    this.mobilePhoneNumber = mobilePhoneNumber;
    this.cookie = cookie;

    this.load();
}

// 绑定微信分享
redenvelopee.prototype.bindShare = function (mobile) {
    var m = mobile || this.mobilePhoneNumber;
    $.cookie('mobilePhoneNumber', m, { expires: new Date('3015/01/01 00:00:00'), path: '/' });
    this.mobilePhoneNumber = m;
    //alert(JSON.stringify({ 
    //	"m": "shared", 
    //	"redEnvelopeId": this.redEnvelopeId, 
    //	"mobilePhoneNumber": this.mobilePhoneNumber, 
    //	"source": this.qs.source, 
    //	"wx": this.iswx ? 1 : 0
    //}));
    this.post({
        "m": "shared",
        "redEnvelopeId": this.redEnvelopeId,
        "mobilePhoneNumber": this.mobilePhoneNumber,
        "source": this.qs.source,
        "wx": this.iswx ? 1 : 0
    });
}

// 绑定修改号码纠错
redenvelopee.prototype.bindChange = function (m) {
    var that = this;
    //alert(JSON.stringify({ "m": "modify", "redEnvelopeId": this.redEnvelopeId, "mobilePhoneNumber": m, "wx": this.iswx ? 1 : 0 }));
    $.ajax({
        url: 'TreasureChestHandler.ashx',
        type: 'POST',
        dataType: 'json',
        data: { "m": "modify", "redEnvelopeId": this.redEnvelopeId, "mobilePhoneNumber": m, "wx": this.iswx ? 1 : 0 },
        success: function (msg) {
            if (msg.status == 0) {
                $.cookie("mobilePhoneNumber", m, { expires: new Date('3015/01/01 00:00:00'), path: '/' });
                that.mobilePhoneNumber = m;
                $("#changebox span").html(m);
                $('#change').addClass('hide');
                closetip();
            } else {
                opentip('修改失败');
            }
        }
    });
}

// 页面加载完毕后执行的load方法
redenvelopee.prototype.load = function () {
    //alert(JSON.stringify({ 
    //	"m": "pageload", 
    //	"activityId": this.qs.activityId, 
    //	"mobilePhoneNumber": this.mobilePhoneNumber, 
    //	"cookie": this.cookie, 
    //	"source": this.qs.source,
    //	"wx": this.iswx ? 1 : 0
    //}))
    this.post({
        "m": "pageload",
        "activityId": this.qs.activityId,
        "mobilePhoneNumber": this.mobilePhoneNumber,
        "cookie": this.cookie,
        "source": this.qs.source,
        "wx": this.iswx ? 1 : 0
    });
    this.wx.share();
    weixinShare();
}

// 浏览器环境判断
redenvelopee.prototype.device = function () {
    var browser = {
        versions: function () {
            var u = navigator.userAgent, app = navigator.appVersion;
            return {//移动终端浏览器版本信息 
                trident: u.indexOf('Trident') > -1, //IE内核
                presto: u.indexOf('Presto') > -1, //opera内核
                webKit: u.indexOf('AppleWebKit') > -1, //苹果、谷歌内核
                gecko: u.indexOf('Gecko') > -1 && u.indexOf('KHTML') == -1, //火狐内核
                mobile: !!u.match(/AppleWebKit.*Mobile.*/) || !!u.match(/AppleWebKit/), //是否为移动终端
                ios: !!u.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/), //ios终端
                android: u.indexOf('Android') > -1 || u.indexOf('Linux') > -1, //android终端或者uc浏览器
                iPhone: u.indexOf('iPhone') > -1 || u.indexOf('Mac') > -1, //是否为iPhone或者QQHD浏览器
                iPad: u.indexOf('iPad') > -1, //是否iPad
                webApp: u.indexOf('Safari') == -1 //是否web应该程序，没有头部与底部
            };
        }(),
        language: (navigator.browserLanguage || navigator.language).toLowerCase()
    }
    return browser;
}

// 服务端出错
redenvelopee.prototype.serverError = function () {
    $('body').attr('class', 'status500');
}

// 发送数据前的提示框
redenvelopee.prototype.beforeSend = function () {
    opentip('正在发送数据，请稍后...');
}

// ajax 回调方法 统一优先执行的方法
redenvelopee.prototype.ajaxCallback = function (response) {
    closetip();
    //alert(JSON.stringify(response));
    var code = response.status;
    //alert(code)
    var ajax = new status(this);
    if (response.amount) this.amount = response.amount;
    if (response.amount && response.activityType === 2) $('.money').html('<span>' + response.amount + '</span>元');
    if (response.redEnvelopeId) this.redEnvelopeId = response.redEnvelopeId;
    if (response.mobilePhoneNumber) this.mobilePhoneNumber = response.mobilePhoneNumber;
    if (response.shareText) this.wx.shareText = response.shareText;
    if (response.shareImage) imgUrl = this.wx.shareImg = response.shareImage;
    if (response.activityRule) $('#rule').html(response.activityRule);

    this.wx.getShareText(response.amount);
    this.wx.getShareURL(this.qs.activityId, this.mobilePhoneNumber);

    if (response.ranklist && response.ranklist.length) {
        var randlist = $('#randlist').empty();
        for (var i = 0; i < response.ranklist.length; i++) {
            var h = '<li>';
            h += '<div class="level level' + (i + 1) + ' float_right">' + (i + 1) + '</div>';
            h += '<span>' + response.ranklist[i].mobilePhoneNumber + '</span>';
            h += '<span class="allGet">累积已领￥' + response.ranklist[i].amount + '</span>';
            h += '<span class="display_block des">' + response.ranklist[i].context + '</span>';
            h += '</li>';
            randlist.append(h);
        }
    }

    if (ajax[code]) {
        $('body').attr('class', 'status' + code);
        ajax[code].call(ajax, response);
    } else {
        this.serverError();
    }
}

// 发送请求方法
redenvelopee.prototype.post = function (data) {
    var isRetry = this.retry;
    var that = this;

    $.ajax({
        url: 'TreasureChestHandler.ashx',
        type: 'post',
        dataType: 'json',
        data: data,
        success: function (response) { that.ajaxCallback(response); },
        error: function (response) { that.serverError(response); },
        beforeSend: function () { that.beforeSend(); }
    });

}

// 倒计时方法
redenvelopee.prototype.times = function (intDiff, callback) {
    var that = this;
    this.timer = window.setInterval(function () {
        var day = 0,
				hour = 0,
				minute = 0,
				second = 0;//时间默认值	

        if (intDiff > 0) {
            day = Math.floor(intDiff / (60 * 60 * 24));
            hour = Math.floor(intDiff / (60 * 60)) - (day * 24);
            minute = Math.floor(intDiff / 60) - (day * 24 * 60) - (hour * 60);
            second = Math.floor(intDiff) - (day * 24 * 60 * 60) - (hour * 60 * 60) - (minute * 60);
        } else if (intDiff < 0) {
            clearInterval(that.timer);
            callback.call(that);
            return;
        }

        if (minute <= 9) minute = '0' + minute;
        if (second <= 9) second = '0' + second;
        $('#day_show').html(day + "天");
        $('#hour_show').html(hour + '时');
        $('#minute_show').html(minute + '分');
        $('#second_show').html(second + '秒');

        if (day < 1) {
            $('#day_show').hide();
        } else {
            $('#day_show').show();
        }

        if (hour < 1) {
            $('#hour_show').hide();
        } else {
            $('#hour_show').show();
        }

        if (minute < 1) {
            $('#minute_show').hide();
        } else {
            $('#minute_show').show();
        }

        intDiff--;
    }, 1000);
};

// 微信对象
function wxContext(context, dev) {
    this.appid = 'wxcc9c7eed4384a147';//正式
    //this.appid = 'wx3f9ff3043cbcb3b7';//测试
    this.context = context;

    // 缓存本实例地址
    this.callbackURL = window.location.href;
    context.localuri = this.callbackURL;

    // 回调地址
    if (dev) {
        this.oauthCallbackURL = encodeURIComponent('http://test.u-xian.com:81/AppPages/weChat/callbackAddress.aspx?from=' + encodeURIComponent(this.callbackURL));
    } else {
        this.oauthCallbackURL = encodeURIComponent('http://u-xian.com/AppPages/weChat/callbackAddress.aspx?from=' + encodeURIComponent(this.callbackURL));
    }

    // 授权地址
    this.jumpURL = 'https://open.weixin.qq.com/connect/oauth2/authorize?appid=' + this.appid + '&redirect_uri=' + this.oauthCallbackURL + '&response_type=code&scope=snsapi_userinfo&state=' + window.state + '#wechat_redirect';

    this.shareText = '今天又领到悠先红包{0}元，每天累积，吃霸王餐不是梦，哈哈思密哒…';
    this.shareImg = 'http://image.u-xian.com/UploadFiles/Images/TreasureChest/weixing_shared.png';

    if (dev) {
        this.shareURL = 'http://test.u-xian.com:81/AppPages/RedEnvelope/TreasureChest.aspx';
    } else {
        this.shareURL = 'http://u-xian.com/AppPages/RedEnvelope/TreasureChest.aspx';
    }

    lineLink = this.shareURL;
    imgUrl = this.shareImg;
    shareTitle = '悠先点菜';
    descContent = this.shareText;
}

// 获取分享URL
wxContext.prototype.getShareURL = function (activityId, mobile) {
    lineLink = this.shareURL = this.shareURL + '?activityId=' + encodeURIComponent(activityId) + '&mobile=' + mobile;
}

// 获取分享内容
wxContext.prototype.getShareText = function (amount) {
    descContent = this.shareText = this.shareText.replace(/\{0\}/g, amount);
}

// 微信绑定方法
wxContext.prototype.share = function () {
    weixinShare(this.context.localuri);
    this.ready();
}

// 微信ready方法
wxContext.prototype.ready = function () {
    weixinReady(
		'悠先点菜',
		this.shareText,
		this.shareImg,
		this.shareURL
	);
    this.isReady = true;
}

// 状态对象
function status(context) {
    this.context = context;
}

// 成功
status.prototype["0"] = function (response) {
    if (response.isGet) {
        response.status = 1004;
        this.context.ajaxCallback(response);
        return;
    }
    if (response.mobilePhoneNumber) {
        this.context.bindShare();
    } else {
        response.status = 101;
        this.context.ajaxCallback(response)
    }
}

// 活动结束
status.prototype["2"] = function (response) {
    if (!this.context.iswx) {
        $('.dio').addClass('hide');
    }
}

// 分享成功
status.prototype["4"] = function (response) {
    response.status = 1004;
    this.context.ajaxCallback(response);
}

// 未找到活动
status.prototype["-6"] = function (response) {
    // done.
}

// 活动未开始
status.prototype["3"] = function (response) {
    this.context.times(Number(response.totalSeconds), function () {
        $('#second_show').html('loading...');
        this.load();
    });
}

// 微信用户存在
status.prototype["100"] = function (response) {
    window.location.href = this.context.wx.jumpURL;
}

// 微信用户未绑定
status.prototype["101"] = function (response) {
    $('.toGet101').find("input[name='mobilePhone']").val(this.context.mobilePhoneNumber);
    var $btn = $('#getRedEnvelopeBtn');
    var btn = $btn.get(0);
    var that = this;
    if (!btn.installed) {
        $btn.on('click', function () {
            var val = $('#mobilePhone').val();
            if (val.length !== 11) {
                opentip('手机号码格式不正确');
                return;
            }
            that.context.bindShare(val);
        });
        btn.installed = true;
    }
}

// 微信用户绑定失败
status.prototype["102"] = function (response) {
    this.context.serverError();
}

// 微信用户绑定失败
status.prototype["103"] = function (response) {
    response.context = '你已经领过啦~同一设备同一号码只能领取1次哟';
    response.status = 101;
    this.context.ajaxCallback(response);
}

// 已领过
status.prototype["1004"] = function (response) {
    var $btn = $('#change');
    var btn = $btn.get(0);
    var that = this;

    $('.money').html(this.context.amount);
    $('#changebox span').html(this.context.mobilePhoneNumber);
    if (response.context && response.show) {
        opentip(response.context);
        delete response.show;
    }

    isShowWeiXinShareButton = true;

    if (response.isChange) {
        $btn.remove();
    } else {
        if (!btn.installed) {
            $btn.on('click', function () {
                var el = opentip();
                if (!el.installed) {
                    $('#cancel').on('click', function () {
                        closetip();
                    });

                    $('#confirm').on('click', function () {
                        var val = $('#newPhoneNumber').val();
                        if (val.length !== 11) {
                            opentip('手机号码格式不正确');
                            return;
                        }
                        that.context.bindChange(val);
                    })

                    el.installed = true;
                }
            });
            btn.installed = true;
        }
    }

    var tb = $('.cbtn');

    if (!this.context.iswx && (this.context.isad || this.context.isio)) {
        tb.val('分享拿钱');
        tb.on('click', mask);
    } else {
        tb.on('click', open_or_download_app);
    }

    if (!this.context.iswx) {
        $('.dio').addClass('hide');
    }
}

// 已经拥有过红包
status.prototype["1007"] = function (response) {
    opentip(response.context);
    response.status = 1004;
    response.show = true;
    this.context.ajaxCallback(response);
    /*opentip(response.context);
	setTimeout(function(){
		window.location.reload();
	}, 1000);*/
}

//辅助方法
function getQueryStringArgs() {
    var qs = (location.search.length > 0 ? location.search.substring(1) : "");
    var args = {};
    //处理查询字符串
    var items = qs.split("&"),
			item = null,
			name = null,
			value = null;
    for (var i = 0; i < items.length; i++) {
        item = items[i].split("=");
        name = decodeURIComponent(item[0]);
        value = decodeURIComponent(item[1]);
        args[name] = value;
    }
    return args;
}

var tiptimer = null;

function opentip(word) {
    try {
        tiptimer && clearTimeout(tiptimer);
    } catch (e) { }
    $('#msgBg,#showMsg').removeClass('hide');
    if (word) {
        $('#showMsg .error').removeClass('hide');
        $('#showMsg .numberCurrect').addClass('hide');
        $('#showMsg .error').html(word);
        tiptimer = setTimeout(closetip, 2000);
    } else {
        $('#showMsg .error').addClass('hide');
        $('#showMsg .numberCurrect').removeClass('hide');
    }

    return $('#showMsg').get(0);
}

function closetip() {
    try {
        tiptimer && clearTimeout(tiptimer);
    } catch (e) { }
    $('#msgBg,#showMsg').addClass('hide');
}

//微信配置config
function weixinShare(thisUrl) {
    $.ajax({
        url: "/AppPages/ajax/wechatGongzhong.ashx",
        type: "post",
        dataType: "json",
        data: { m: 'config', url: thisUrl },
        success: function (data) {
            wx.config({
                debug: false, // 开启调试模式,调用的所有api的返回值会在客户端alert出来，若要查看传入的参数，可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。
                appId: data.appId, // 公众号的唯一标识
                timestamp: data.timestamp, // 生成签名的时间戳
                nonceStr: data.nonceStr, // 生成签名的随机串
                signature: data.signature, // 签名
                jsApiList: ['getNetworkType', 'checkJsApi', 'onMenuShareTimeline', 'onMenuShareAppMessage', 'onMenuShareWeibo', 'onMenuShareQQ'] // 必填，需要使用的JS接口列表，所有JS接口列表见附录2
            });
        },
        error: function () {

        }
    });
    weixinReady();
}

function weixinReady(shareTitle, shareContent, shareImage, thisUrl) {
    //微信ready
    wx.ready(function () {
        //微信 - 分享朋友
        wx.onMenuShareAppMessage({
            title: shareTitle,
            desc: shareContent,
            link: thisUrl,
            imgUrl: shareImage,
            trigger: function (res) {

            },
            success: function (res) {

            },
            cancel: function (res) {

            },
            fail: function (res) {
                //alert(JSON.stringify(res));
            }
        });

        //微信 - 分享朋友圈
        wx.onMenuShareTimeline({
            title: shareContent,
            link: thisUrl,
            imgUrl: shareImage,
            trigger: function (res) {
                //alert('用户点击分享到朋友圈');
            },
            success: function (res) {

            },
            cancel: function (res) {

            },
            fail: function (res) {
                //alert(JSON.stringify(res));
            }
        });
    });
}

function open_or_download_app() {
    if (navigator.userAgent.match(/(iPhone|iPod|iPad);?/i)) {
        // 判断useragent，当前设备为ios设备            
        window.location = "http://a.app.qq.com/o/simple.jsp?pkgname=va.dish.sys";　　// iPhone端URL Schema
    } else if (navigator.userAgent.match(/android/i)) {
        // 判断useragent，当前设备为android设备
        window.location = "http://a.app.qq.com/o/simple.jsp?pkgname=va.dish.sys";　　// Android端URL Schema
    }
}

function mask() {
    if (navigator.userAgent.match(/MicroMessenger/i)) {
        // 未完待续..
    } else {
        if (navigator.userAgent.match(/(iPhone|iPod|iPad);?/i)) {
            // 判断useragent，当前设备为ios设备  
            window.location = "message:type:0,value:showShareLayout";
        } else if (navigator.userAgent.match(/android/i)) {
            // 判断useragent，当前设备为android设备
            window.redEnvelopeShare.showShareLayout();
        }
    }
}

/*微信*/
function onBridgeReady() {
    if (isShowWeiXinShareButton == true) {
        // 发送给好友
        WeixinJSBridge.on('menu:share:appmessage', function (argv) {
            shareFriend();
        });

        // 分享到朋友圈
        WeixinJSBridge.on('menu:share:timeline', function (argv) {
            shareTimeline();
        });

        // 分享到微博
        WeixinJSBridge.on('menu:share:weibo', function (argv) {
            shareWeibo();
        });
        WeixinJSBridge.call("showOptionMenu");
    } else {
        WeixinJSBridge.call('hideOptionMenu');
    }

    setTimeout(function () {
        $(".mask button").removeClass('button-hui');
        $(".mask button").unbind('click');
        $(".mask button").click(function (event) {
            $(".mask").hide();
            $(".main").show();
        });
    }, 0);
}
function shareFriend() {
    $(".mask button").removeClass('button-hui');
    $(".mask button").unbind('click');
    $(".mask button").click(function (event) {
        $(".mask").hide();
        $(".main").show();
    });

    WeixinJSBridge.invoke('sendAppMessage', {
        "appid": appid,
        "img_url": imgUrl,
        "img_width": "640",
        "img_height": "640",
        "link": lineLink,
        "desc": descContent,
        "title": shareTitle
    }, function (res) {
        _report('send_msg', res.err_msg);
    });
}

function shareTimeline() {
    $(".mask button").removeClass('button-hui');
    $(".mask button").unbind('click');
    $(".mask button").click(function (event) {
        $(".mask").hide();
        $(".main").show();
    });


    WeixinJSBridge.invoke('shareTimeline', {

        "img_url": imgUrl,
        "img_width": "640",
        "img_height": "640",
        "link": lineLink,
        "desc": descContent,
        "title": descContent
    }, function (res) {

    });
}
function shareWeibo() {
    $(".mask button").removeClass('button-hui');
    $(".mask button").unbind('click');
    $(".mask button").click(function (event) {
        $(".mask").hide();
        $(".main").show();
    });

    WeixinJSBridge.invoke('shareWeibo', {
        "content": descContent,
        "url": lineLink,
    }, function (res) {
        _report('weibo', res.err_msg);
    });
}

function weixinShare() {

    if (typeof WeixinJSBridge == "undefined") {
        if (document.addEventListener) {

            document.addEventListener('WeixinJSBridgeReady', onBridgeReady, false);
        } else if (document.attachEvent) {
            document.attachEvent('WeixinJSBridgeReady', onBridgeReady);
            document.attachEvent('onWeixinJSBridgeReady', onBridgeReady);
        }
    } else {
        onBridgeReady();
    }
}

function getJSONForAndroid() {
    var j = getJSON();
    window.redEnvelopeShare.getRedenvelopeJson(j);
}
function getJSON() {
    var va = JSON.stringify({ 'content': descContent, 'imgUrl': imgUrl, 'shareUrl': lineLink });
    return va;
}