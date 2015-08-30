var qs = getQueryStringArgs(); //获取参数
/* File Created: 九月 24, 2014 */
var loginappid = 'wxcc9c7eed4384a147';
var loginpixer = 'test.';
var fs = window.location.href;
var logincallback = encodeURIComponent('http://' + loginpixer + 'u-xian.com:82/AppPages/weChat/callbackAddress.aspx?from=' + encodeURIComponent(fs));

function getLoginURI(){
	return 'https://open.weixin.qq.com/connect/oauth2/authorize?appid=' + loginappid + '&redirect_uri=' + logincallback + '&response_type=code&scope=snsapi_userinfo&state=' + window.state + '#wechat_redirect';
}

//微信相关
var appid = 'wx3f9ff3043cbcb3b7';
//var lineLink = window.location.protocol + '//' + window.location.host + '/AppPages/RedEnvelope/TreasureChest.aspx?activityId=' + qs.activityId;
var lineLink = window.location.protocol + '//' + window.location.host + '/AppPages/RedEnvelope/TreasureChest.aspx?activityId=' + encodeURIComponent(qs.activityId);
var imgUrl = 'http://image.u-xian.com/UploadFiles/Images/TreasureChest/weixing_shared.png';
var shareTitle = '';
var descContent = '';

var objTimer; //动画定时器
var imageServer = "http://image.u-xian.com/UploadFiles/Images/TreasureChest/"; //图片服务器

var screenScale = document.body.clientWidth / 640; // 屏幕宽度比例
if (screenScale > 1)
    screenScale = 1;
if (screenScale < 0.5)
    screenScale = 0.5;
var isShowWeiXinShareButton = false;
var isShowMask = false;

var sharedContents = ['今天又领到悠先红包{0}元，每天累积，吃霸王餐不是梦，哈哈思密哒…', '今天又领到悠先红包{0}元，每天累积，吃霸王餐不是梦，哈哈思密哒…'];
var isRetry = true;

var weChatUnionId = $.cookie('weChatUnionId');
weChatUnionId = $.cookie('weChatUnionId');

jQuery.redenvelopee = {
 
    load: function () {
        var mobilePhone = "";
        var cookie = "";
        if (!navigator.userAgent.match(/MicroMessenger/i)) {

            if (qs.mobilephone != "null" && qs.mobilephone != null && qs.mobilephone != undefined && qs.mobilephone != "") {
                mobilePhone = qs.mobilephone;
            } else if ($.cookie("mobilePhoneNumber") != null) {
                mobilePhone = $.cookie("mobilePhoneNumber");
            }

            cookie = qs.cookie;
        } else {
            if ( !qs.mobilePhone ){
                if ($.cookie("mobilePhoneNumber") != null) {
                    mobilePhone = $.cookie("mobilePhoneNumber");
                }
            }else{
                mobilePhone = qs.mobilePhone;
            }
            alert('qs.mobile:' + qs.mobilephone);
            window.modile = mobilePhone;
        }


        $.ajax({
            url: 'TreasureChestHandler.ashx',
            type: 'POST',
            dataType: 'json',
            data: { "m": "pageload", "activityId": qs.activityId, "mobilePhoneNumber": mobilePhone, "cookie": cookie, "source": qs.source, "weChatUnionId": weChatUnionId/*, "wx": navigator.userAgent.match(/MicroMessenger/i)*/ },
            async: true,
            success: ajaxSuccess,
            error: function () { 
                if (isRetry == true) {
                    //console.log("我来了");
                    window.setTimeout($.redenvelopee.load, 3000);
                    isRetry = false;
                } else {
                    $(".loading").hide();
                    $(".main").show();
                    bang();
                }

            },
            beforeSend: function () {
                //console.log("dddd");
                $(".loading").show();
                $(".main").hide();
            },
            complete: function () {
                //$(".loading").hide();
                //$(".main").show();
            }
        });
    },
    gameove: function () {
        $.ajax({
            contentType: "application/json",
            url: 'Default.aspx/ActivityOver',
            type: 'POST',
            dataType: 'json',
            data: '{ id: ' + qs.id + ' }',
            async: true,
            success: ajaxSuccess,
            error: function () {
                bang();
            },
            beforeSend: function () {
                $(".loading").show();
                $(".main").hide();
            },
            complete: function () {
                $(".loading").hide();
                $(".main").show();
            }
        });
    }
};


$("#myModal #mobilePhone").focus(function () {
    $(".bs-example-modal-sm").css({
        'margin-top': '-20px',
        'padding-top': '-20px',
        'top': '-20px',
        'position': 'fixed',
        'z-index': 9999,
        'height': height
    });
});

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
	
	setTimeout(function(){
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
window.mobile = null;
window.redEnvelopeId = null;
function dgo(){
    window.location.href = getLoginURI();
};

function ajaxSuccess(response) {
    alert('status:' + response.status)
    alert('mobile:' + (window.mobile || response.mobilePhoneNumber))
    response.status = Number(response.status);
    if ( navigator.userAgent.match(/MicroMessenger/i) ){
        window.mobile = window.mobile ? window.mobile : response.mobilePhoneNumber;
    }else{
    window.mobile = response.mobilePhoneNumber ? response.mobilePhoneNumber : window.mobile;
    }
    window.redEnvelopeId = response.redEnvelopeId ? response.redEnvelopeId : window.redEnvelopeId;
    var a = response.status;
	var iswx = /MicroMessenger/i.test(String(navigator.userAgent));
	var ispod = /(iPhone|iPod|iPad);?/i.test(String(navigator.userAgent));
	var isand = /android/i.test(String(navigator.userAgent)) || /AppServerUXian/i.test(String(navigator.userAgent));

//alert(!iswx && !ispod && !isand)
//	if ( !iswx && !ispod && !isand  ){
//		dgo(); 
//		return;
//	}
    if ( a == 0 ){
    //navigator.userAgent.match(/MicroMessenger/i) || navigator.userAgent.match(/(iPhone|iPod|iPad);?/i) || navigator.userAgent.match(/android/i)
    alert(!iswx && (navigator.userAgent.match(/(iPhone|iPod|iPad);?/i) || navigator.userAgent.match(/android/i)))
        if (!iswx && (ispod || isand)){  
         treasureChestInit(response);
            $(".main").show();
        
        }else{ 
            dgo(); 
        return ;
        }
    }
    else if ( a == 2 ){
       gameover(response);
       $(".main").show(); 
    }
    else if ( a == 4 ){
        unlockRedEnvelope(response); 
            if (!navigator.userAgent.match(/MicroMessenger/i)) {
                $(".main").show();
            }
    }
    else if ( a == -6 ){
        noActivity();
            $(".main").show();
    }
    else if ( a === 3 ){
        activityNotBegin(response);
    }
    else if ( a == 100 ){
        getRedEnveLope(response);
            $(".main").show();
    }
    else if ( a == 101 ){
        treasureChestInit(response);
            $(".main").show();
    }
    else if ( a == 102 ){
        alert('绑定失败');
    }
    else if ( a == 1004 ){
        getRedEnveLope(response);
            $(".main").show();
    }
    else if ( a == 1007 ){
        $('.main').show();
	        $("#alertMsg").html("<p class='currTitle'>"+response.context+"</p>");
        	$("#alertMsgBg").show().animate({"opacity":"1"});
        	$("#alertMsg").show().animate({"opacity":"1", "top":"18%"});
            
            setTimeout( function() {
	        	$("#alertMsgBg").animate({"opacity":"0"}, 'slow');
	        	$("#alertMsg").animate({"opacity":"0", "top":"15%"}, 'slow', function(){
	        		window.location.reload();
	        		$("#alertMsgBg").hide();
	        		$("#alertMsg").hide();
	        	});
            }, 2000);
    }else {
         bang();
            $(".main").show();
    }

    /*switch (response.status) {
        case 0:
            dgo();
            //treasureChestInit(response);
            //$(".main").show();
            break;
            //case 1:
            //    treasureChestOver(response);
            //    $(".main").show();
            //    break;
        case 2:
            gameover(response);
            $(".main").show();
            break;
        case 4:
            unlockRedEnvelope(response);
            if (!navigator.userAgent.match(/MicroMessenger/i)) {
                $(".main").show();
            }
            //$(".main").show();
            break;
        case -6:
            noActivity();
            $(".main").show();
            break;
        case 3:
            activityNotBegin(response);
            break;
						
		case 100:
			getRedEnveLope(response);
            $(".main").show();
			break;
		case 101:
		    treasureChestInit(response);
            $(".main").show();
            break;
        case 102:
		    alert('绑定失败');
            break;
        case 1004:
            getRedEnveLope(response);
            $(".main").show();
            break;
            //            case 1000:
            //                treasureChestOpen(response);
            //                break;
            //            case 1001:
            //                moneyOver(response);
            //                break;
            //            case 1002:
            //                strengthover();
            //                break;
            //            case 1003:
            //                redEnvelopeOver(response);
            //                break;
            //            case 1005:
            //                treasureChestOwner(response);
            //                break;
        case 1007:
        	$('.main').show();
	        $("#alertMsg").html("<p class='currTitle'>"+response.context+"</p>");
        	$("#alertMsgBg").show().animate({"opacity":"1"});
        	$("#alertMsg").show().animate({"opacity":"1", "top":"18%"});
            
            setTimeout( function() {
	        	$("#alertMsgBg").animate({"opacity":"0"}, 'slow');
	        	$("#alertMsg").animate({"opacity":"0", "top":"15%"}, 'slow', function(){
	        		window.location.reload();
	        		$("#alertMsgBg").hide();
	        		$("#alertMsg").hide();
	        	});
            }, 2000);
            break;
        default:
            bang();
            $(".main").show();
            break;
    }*/
    $(".loading").hide();
    weixinShare();
}


$('body').on('click', '#currNumber', function(){
	
	var t = '<p class="currTitle">号码纠错</p>';
	t += '<p class="currDesc">手别抖，只能修改一次哦~</p>';
	t += '<input type="tel" id="newPhoneNumber" value="" />';
	t += '<div class="correctBtn"><a href="javascript:;" id="cancel">取消</a></div>';
	t += '<div class="correctBtn"><a href="javascript:;" style="border:none;" id="confirm">确定</a></div>';
	$("#alertMsg").html(t);
	$("#newPhoneNumber").val(window.mobile)
	$("#alertMsgBg").show().animate({"opacity":"1"});
	$("#alertMsg").show().animate({"opacity":"1"});
});

$('body').on('click', '#confirm', function(response){
	var mobile = $("#newPhoneNumber").val();
	$("#cancel").trigger("click", function(){
	    var patrn = /^1[34578][0-9]{9}$/;
	    if (!mobile.match(patrn)) {
	        $("#alertMsg").html("<p class='currTitle'>无效的手机号码</p>");
	    	$("#alertMsgBg").show().animate({"opacity":"1"});
	    	$("#alertMsg").show().animate({"opacity":"1", "top":"18%"});
	        
	        setTimeout( function() {
	        	$("#alertMsgBg").animate({"opacity":"0"}, 'slow');
	        	$("#alertMsg").animate({"opacity":"0", "top":"15%"}, 'slow', function(){
	        		window.location.reload();
	        		$("#alertMsgBg").hide();
	        		$("#alertMsg").hide();
	        	});
	        }, 2000);
	    } else {
	        $.ajax({
	            url: 'TreasureChestHandler.ashx',
	            type: 'POST',
	            dataType: 'json',
	            data: { "m": "modify", "redEnvelopeId": window.redEnvelopeId, "mobilePhoneNumber": mobile },
	            success: function (msg) {
	                if (msg.status == 0) {
	                    $.cookie("mobilePhoneNumber", mobile,{
							expires: new Date('3015/01/01 00:00:00'),
			                path : '/'
						});
	                    window.mobile = mobile;
	                    $(".boxowner-money .number-recovery").html(mobile);
	                    
	                    /*$("#alertMsg").html("<p class='currTitle'>修改成功</p>");
				    	$("#alertMsgBg").show().animate({"opacity":"1"});
				    	$("#alertMsg").show().animate({"opacity":"1", "top":"18%"});
				        
				        setTimeout( function() {
				        	$("#alertMsgBg").animate({"opacity":"0"}, 'slow');
				        	$("#alertMsg").animate({"opacity":"0", "top":"15%"}, 'slow', function(){
				        		$("#alertMsgBg").hide();
				        		$("#alertMsg").hide();
				        	});
				        }, 2000);*/
	                } else {
	                    $("#alertMsg").html("<p class='currTitle'>修改失败</p>");
				    	$("#alertMsgBg").show().animate({"opacity":"1"}, 'slow');
				    	$("#alertMsg").show().animate({"opacity":"1", "top":"18%"}, 'slow');
				        
				        setTimeout( function() {
				        	$("#alertMsgBg").animate({"opacity":"0"}, 'slow');
				        	$("#alertMsg").animate({"opacity":"0", "top":"15%"}, 'slow', function(){
				        		$("#alertMsgBg").hide();
				        		$("#alertMsg").hide();
				        	});
				        }, 2000);
	                }
	
	            },
	        });
	    }
    });
});

$('body').on('click', '#cancel', function(ev, callback){
	$("#alertMsgBg").animate({"opacity":"0"}, 'slow');
	$("#alertMsg").animate({"opacity":"0"}, 'slow', function(){
		$("#alertMsgBg").hide();
		$("#alertMsg").hide();
		$.isFunction(callback) && callback();
	});
});


//没有活动
function noActivity() {

    $("body").eq(0).addClass('server-bang');
    var box = $(".box");
    box.empty(true);
    loadImg(box, 'http://image.u-xian.com/UploadFiles/Images/TreasureChest/Activity404.png');

    $(".lock-info").hide();

    var input_info = $(".input-info");
    input_info.empty(true);
    input_info.hide();


    $('#redEnvelope').hide();
    hideOther();
}

//活动没有开始
function activityNotBegin(response) {
    $("body").eq(0).addClass('server-activity');
    $(".main").hide();
    var main = $('<div class="main main-activity" />');
    main.append('<img src="http://image.u-xian.com/UploadFiles/Images/TreasureChest/activity_time.png" />');
    main.append('<div>距离活动开始还有</div>');
    var time = $('<div class="time"></div>');
    main.append(time);
    var begin_time = $('<div class="begin-time"></div>');
    main.append(begin_time);
    $("body").eq(0).append(main);



    var beginDate = new Date(Number(response.totalSeconds));
    var t;
    var fun = function startTime() {
        now = new Date();
        if (beginDate > now) {
            var s = (beginDate.getTime() - now.getTime()) / 1000;
            var m = Math.floor(s / 60 % 60);
            var h = Math.floor(s / 60 / 60);
            s = Math.floor(s % 60);
            if (s < 10) {
                s = "0" + s;
            }
            if (m < 10) {
                m = "0" + m;
            }
            if (h < 10) {
                h = "0" + h;
            }
            time.html(h + ":" + m + ":" + s);
            t = setTimeout(fun, 500);
        } else {
            //location.href = "FromApp.aspx?activityId=" + qs.id;
            $("body").eq(0).removeClass('server-activity');
            main.remove();
            $.redenvelopee.load();
        }
    }
    var now = new Date();
    if (beginDate > now) {
        begin_time.html("活动将在" + beginDate.getFullYear() + "年" + (beginDate.getMonth() + 1) + "月" + beginDate.getDate() + "日 " + (beginDate.getHours() < 10 ? "0" : "") + beginDate.getHours() + ':' + (beginDate.getMinutes() < 10 ? "0" : "") + beginDate.getMinutes() + "开始");


        fun();
    } else {
        //location.href = "FromApp.aspx?activityId=" + qs.id;
        $("body").eq(0).removeClass('server-activity');
        main.remove();
        $.redenvelopee.load();
    }

    //main.empty();

    /*
     * <img src="http://image.u-xian.com/UploadFiles/Images/TreasureChest/activity_time.png" />
        <div>距离活动开始还有</div>
        <div class="time"></div>
        <div class="begin-time"></div>
     * 
     */
		$('#flose').css('zIndex', '99999');
}

//服务器崩
function bang() {
    $("body").eq(0).addClass('server-bang');

    var box = $(".box");
    box.empty(true);
    loadImg(box, 'http://image.u-xian.com/UploadFiles/Images/TreasureChest/server_bang.png');

    $(".lock-info").hide();

    var input_info = $(".input-info");
    input_info.empty(true);

    addButton(input_info, 'btn btn-default', '稍后再试', function () { });

    $('#redEnvelope').hide();
    hideOther();
}

//宝箱页面初始状态
function treasureChestInit(response) {
    // if (objTimer)
    //     window.clearInterval(objTimer);
    
    var box = $(".box");
    box.empty(true);
    loadImg(box, imageServer + 'box_a.jpg');
    //objTimer = window.setInterval(animation, 200);

    //得到的钱
    var block1 = $("<span />");
    block1.addClass('box-money');
    if ( response.activityType === 2 ){block1.html(response.amount + '<span>元</span>');}
    $(".box").append(block1);
    block1.css("font-size", Number(block1.css("font-size").replace("px", "")) * screenScale);

    var input_info = $('<div class="input-info"></div>');
    input_info.empty(true);

    var block3 = $("<div />");
    block3.addClass('left');
    var input1 = $('<input id="mobilePhone" type="tel" placeholder="请输入手机号码"/>');
    input1.addClass('form-control');
    if (navigator.userAgent.match(/MicroMessenger/i)){
        input1.val(window.modile || response.mobilePhoneNumber || $.cookie('mobilePhoneNumber')); 
    }else{
        input1.val($.cookie('mobilePhoneNumber') || window.modile || response.mobilePhoneNumber);
    }
    //input1.val($.cookie('mobilePhoneNumber'));
    //input1.val(response.mobilePhoneNumber);
    block3.append(input1);

    var block4 = $("<div />");
    block4.addClass('right');
    var button1 = $('<button type="button"></button>');
    button1.addClass('btn btn-default');
    button1.html('马上拿钱');
    block4.append(button1);


    button1.unbind('click');
    button1.click(function (event) {
        var mobilePhone = $("#mobilePhone").val();

        var patrn = /^1[34578][0-9]{9}$/;
        if (!mobilePhone.match(patrn)) {
            alert('无效的手机号码');
        } else {
            $.cookie("mobilePhoneNumber", $("#mobilePhone").val(),{
				expires: new Date('3015/01/01 00:00:00'),
			    path : '/'
			});
            isShowMask = true;
            $.ajax({
                url: 'TreasureChestHandler.ashx',
                type: 'POST',
                dataType: 'json',
                data: { "m": "shared", "redEnvelopeId": response.redEnvelopeId, "mobilePhoneNumber": $("#mobilePhone").val(), "source": qs.source, "wx": navigator.userAgent.match(/MicroMessenger/i) ? "1" : "0", "weChatUnionId": weChatUnionId },
                success: ajaxSuccess,
                beforeSend: function () {
                    //console.log("dddd");
                    $(".loading").show();
                    $(".main").hide();
                },
            });
            //$("#myModal #mobilePhone").html(mobilePhone);
            //$("#myModal").modal({ target: ".bs-example-modal-sm" }).css({
            //    'margin-top': function () {
            //        return ($(this).height() / 2);
            //    }
            //});
            //var issmash = false;
            //$("#button_smash").unbind('click');
            //$("#button_smash").click(function (event) {
            //    issmash = true;
            //    $("#myModal").modal('hide');
            //});
            //$("#button_cancel").unbind('click');
            //$("#button_cancel").click(function (event) {
            //    issmash = false;
            //    $("#myModal").modal('hide');
            //});
            //$('#myModal').on('hidden.bs.modal', function () { });
            //$('#myModal').on('hidden.bs.modal', function (e) {

            //    if (issmash == true) {
            //        $.cookie("mobilePhoneNumber", $("#mobilePhone").val());
            //        isShowMask = true;
            //        $.ajax({
            //            url: 'TreasureChestHandler.ashx',
            //            type: 'POST',
            //            dataType: 'json',
            //            data: { "m": "shared", "redEnvelopeId": response.redEnvelopeId, "mobilePhoneNumber": $("#mobilePhone").val() },
            //            success: ajaxSuccess,
            //        });
            //    }
            //    issmash = false;
            //});
        }
    });

    input_info.append(block3);
    input_info.append(block4);
    input_info.append('<div class="title">红包将打入您的悠先账户（通过手机号码登录）</div>');
    input_info.addClass('main-input');
    $(".box").append(input_info);

    if (response.ranklist && response.ranklist.length > 0) {
        $("#redEnvelope").show();
        var ul = $("#redEnvelope > ul");
        loadRanklist(ul, response.ranklist);
    } else {
        $("#redEnvelope").hide();
    }

    $("#activityRule > ul").empty();
    $("#activityRule > ul").append(response.activityRule);

    if (!navigator.userAgent.match(/MicroMessenger/i)) {
        $(".suitable-shop").hide();
        $("#whatisux").hide();
    }
  
}

// 得到钱
function unlockRedEnvelope(response) {

    //if (objTimer)
    //    window.clearInterval(objTimer);
    var box = $(".box");
    box.empty(true);
    loadImg(box, imageServer + 'unlock_over_a.jpg');
    //objTimer = window.setInterval(animation, 200);

    isShowWeiXinShareButton = true;
    //descContent = sharedContents[GetRandomNum(0, 1)].replace('{0}', response.amount);
    descContent = response.shareText.replace('{0}', response.amount).replace('{1}', response.totalamount);
    imgUrl = response.shareImage;

    //得到的钱
    var block1 = $("<span />");
    block1.addClass('boxowner-money');
    block1.html(response.amount + '<span>元</span><div class="number-recovery">' + response.mobilePhoneNumber + '<span id="currNumber">号码纠错</span></div>');

    $(".box").append(block1);
    block1.css("font-size", Number(block1.css("font-size").replace("px", "")) * screenScale);


    $(".number-recovery span").click(function () {
        $("#myModal #mobilePhone").val(response.mobilePhoneNumber);

        var height = '100%';
        if (navigator.userAgent.match(/iPhone/i)) {
            height = '480px';
        }

        $("#myModal #mobilePhone").val(response.mobilePhoneNumber);
        $("#myModal").modal({ target: ".bs-example-modal-sm" }).css({
            'margin-top': '0',
            'padding-top': '0',
            'top': '0',
            'position': 'fixed',
            'z-index': 9999,
            'height': height
        });

        var issmash = false;
        $("#button_smash").unbind('click');
        $("#button_smash").click(function (event) {
            issmash = true;
            $("#myModal").modal('hide');
        });
        $("#button_cancel").unbind('click');
        $("#button_cancel").click(function (event) {
            issmash = false;
            $("#myModal").modal('hide');
        });
        $('#myModal').on('hidden.bs.modal', function () { });
        $('#myModal').on('hidden.bs.modal', function (e) {

            if (issmash == true) {
                //$.cookie("mobilePhoneNumber", $("#mobilePhone").val());
                isShowMask = true;
                var mobile = $("#mobilePhone").val();
                var patrn = /^1[34578][0-9]{9}$/;
                if (!mobile.match(patrn)) {
                    alert('无效的手机号码');
                } else {
                    $.ajax({
                        url: 'TreasureChestHandler.ashx',
                        type: 'POST',
                        dataType: 'json',
                        data: { "m": "modify", "redEnvelopeId": response.redEnvelopeId, "mobilePhoneNumber": mobile, "source": qs.source },
                        success: function (msg) {
                            if (msg.status == 0) {
                                $.cookie("mobilePhoneNumber", mobile,{
									expires: new Date('3015/01/01 00:00:00'),
			                        path : '/'
								});
                                response.mobilePhoneNumber = mobile;
                                $(".boxowner-money .number-recovery").html(mobile);
                                alert("修改成功");
                            } else {
                                alert("修改失败");
                            }

                        },
                    });
                }
            }
            issmash = false;
        });

    });



    var input_info = $('<div class="input-info"></div>');
    input_info.empty(true);

    if (navigator.userAgent.match(/MicroMessenger/i)) {
        addButton(input_info, 'btn btn-default', '下载悠先', open_or_download_app);
        input_info.append('<div class="title"><img src=""' + imageServer + 'text.png"" class="img-responsive" /></div>');
    } else {
        addButton(input_info, 'btn btn-default', '分享拿钱', mask);
		if ( response.activityType === 2 ){
        	//input_info.append('<div class="title">别忘了明天还有哦</div>');
		}
    }



    input_info.addClass('main-input');
    $(".box").append(input_info);

    $("#redEnvelope").show();
    var ul = $("#redEnvelope > ul");
    loadRanklist(ul, response.ranklist);
    //loadMyRanking(response.rankState, response.ranking);

    $("#activityRule > ul").empty();
    $("#activityRule > ul").append(response.activityRule);

    if (!navigator.userAgent.match(/MicroMessenger/i)) {
        $(".suitable-shop").hide();
        $("#whatisux").hide();
    }

    if (navigator.userAgent.match(/MicroMessenger/i)) {
        $(".addone").empty();
        addButton($(".addone"), 'btn btn-default', '下载悠先', open_or_download_app);
    }
    if (isShowMask) {
        mask();
    }


}

function getRedEnveLope(response) {
    //if (objTimer)
    //    window.clearInterval(objTimer);
    var box = $(".box");
    box.empty(true);
    if (response.activityType == 1)
        loadImg(box, imageServer + 'get_redenvelope_a.jpg');
    else
        loadImg(box, imageServer + 'get_redenvelope_b.jpg');
    //objTimer = window.setInterval(animation, 200);

    isShowWeiXinShareButton = true;
    //descContent = sharedContents[GetRandomNum(0, 1)].replace('{0}', response.amount);
    descContent = response.shareText.replace('{0}', response.amount).replace('{1}', response.totalamount);
    imgUrl = response.shareImage;

    //得到的钱
    var block1 = $("<span />");
    block1.addClass('boxowner-money');

    if (response.isChange == false) {
        block1.html(response.amount + '<span>元</span><div class="number-recovery">' + response.mobilePhoneNumber + '<span id="currNumber">号码纠错</span></div>');
    } else {
        block1.html(response.amount + '<span>元</span><div class="number-recovery">' + response.mobilePhoneNumber + '</div>');
    }

    $(".box").append(block1);
    block1.css("font-size", Number(block1.css("font-size").replace("px", "")) * screenScale);

    var input_info = $('<div class="input-info"></div>');
    input_info.empty(true);

    if (navigator.userAgent.match(/MicroMessenger/i)) {
        addButton(input_info, 'btn btn-default', '下载悠先', open_or_download_app);
        //input_info.append('<div class="title">下载APP，还有专属天天红包可领</div>');
		input_info.append('<div class="title"><img src=""' + imageServer + 'text.png"" class="img-responsive" /></div>');
    } else {
        addButton(input_info, 'btn btn-default', '分享拿钱', mask);
        //input_info.append('<div class="title">别忘了明天还有哦</div>');
    }

    $(".number-recovery span").click(function () {
        //iPhone OS 8
        var height = '100%';
        if (navigator.userAgent.match(/iPhone/i)) {
            height = '480px';
        }

        $("#myModal #mobilePhone").val(response.mobilePhoneNumber);
        $("#myModal").modal({ target: ".bs-example-modal-sm" }).css({
            'margin-top': '0',
            'padding-top': '0',
            'top': '0',
            'position': 'fixed',
            'z-index': 9999,
            'height': height
        });

        var issmash = false;
        $("#button_smash").unbind('click');
        $("#button_smash").click(function (event) {
            issmash = true;
            $("#myModal").modal('hide');
        });
        $("#button_cancel").unbind('click');
        $("#button_cancel").click(function (event) {
            issmash = false;
            $("#myModal").modal('hide');
        });
        $('#myModal').on('hidden.bs.modal', function () { });
        $('#myModal').on('hidden.bs.modal', function (e) {

            if (issmash == true) {
                //$.cookie("mobilePhoneNumber", $("#mobilePhone").val());
                isShowMask = true;
                var mobile = $("#mobilePhone").val();
                var patrn = /^1[34578][0-9]{9}$/;
                if (!mobile.match(patrn)) {
                    alert('无效的手机号码');
                } else {
                    $.ajax({
                        url: 'TreasureChestHandler.ashx',
                        type: 'POST',
                        dataType: 'json',
                        data: { "m": "modify", "redEnvelopeId": response.redEnvelopeId, "mobilePhoneNumber": mobile, "source": qs.source },
                        success: function (msg) {
                            if (msg.status == 0) {
                                $.cookie("mobilePhoneNumber", mobile,{
									expires: new Date('3015/01/01 00:00:00'),
			                        path : '/'
								});
                                response.mobilePhoneNumber = mobile;
                                $(".boxowner-money .number-recovery").html(mobile);
                                alert("修改成功");
                            } else {
                                alert("修改失败");
                            }

                        },
                    });
                }
            }
            issmash = false;
        });

    });



    input_info.addClass('main-input');
    $(".box").append(input_info);

    $("#redEnvelope").show();
    var ul = $("#redEnvelope > ul");
    loadRanklist(ul, response.ranklist);
    //loadMyRanking(response.rankState, response.ranking);



    $("#activityRule > ul").empty();
    $("#activityRule > ul").append(response.activityRule);

    if (!navigator.userAgent.match(/MicroMessenger/i)) {
        $(".suitable-shop").hide();
        $("#whatisux").hide();
    }

    if (navigator.userAgent.match(/MicroMessenger/i)) {
        $(".addone").empty();
        addButton($(".addone"), 'btn btn-default', '下载悠先', open_or_download_app);
    }
    if (isShowMask) {
        mask();
    }
}

//活动结束
function gameover(response) {

    //console.log(response);
    //if (objTimer)
    //    window.clearInterval(objTimer);
    var box = $(".box");
    box.empty(true);
    loadImg(box, 'http://image.u-xian.com/UploadFiles/Images/TreasureChest/game_over_a.png');
    //objTimer = window.setInterval(animation, 200);

    $(".lock-info").hide();

    //var input_info = $('<div class="input-info"></div>');
    //input_info.empty(true);

    //if (navigator.userAgent.match(/MicroMessenger/i)) {
    //    addButton(input_info, 'btn btn-default', '下载悠先', open_or_download_app);
    //    input_info.append('<div class="title">下载后用手机号码登录,即可使用</div>');
    //}

    //loadRedEnvelopes(response.redEnvelopes);
    $("#redEnvelope").show();
    var ul = $("#redEnvelope > ul");
    loadRanklist(ul, response.ranklist);

    $(".suitable-shop").show();
    $("#whatisux").show();
    $("#activityRule").hide();

    //friendshipSponsor();

    var input_info = $('.addone');
    input_info.empty(true);
    if (navigator.userAgent.match(/MicroMessenger/i)) {
        addButton(input_info, 'btn btn-default', '下载悠先', open_or_download_app);
        input_info.append('<div class="remark" style="padding-bottom:10px;"></div>');
    }
    //addButton(input_info, 'btn btn-default', '加入我们', function () {
    //    location.href = "http://u-xian.com/";
    //});

    //$('.main').append(input_info);
	if ( navigator.userAgent.match(/MicroMessenger/i) ){
		var box = $('.box');
		addButton(box, 'btn btn-warning', '下载悠先', open_or_download_app);
		box.find('button').css({
			position: 'absolute',
			bottom: '13%',
			left: '50%',
			'margin-left': '-35%',
			height: '44px',
			display: 'block',
			width: '70%',
			'font-size': '16px',
			'max-height': '44px!important',
			'background': '#ffc800',
			color: '#b41f30'
		});
		box.append('<div class="title" style="position:absolute; width:100%; left:0; bottom:5%;"><img src=""' + imageServer + 'text.png"" class="img-responsive" /></div>');
	}
}


/*页面元素*/
function loadImg(parent, imgPath) {
    var img = $("<img />");
    img.attr('src', imgPath);
    parent.append(img);
}

function loadRanklist(ul, ranklist) {
    ul.empty(true);
    var lastIndex = 0;
    var lastAmount = 0;
    $.each(ranklist, function (i, n) {
        var li = $("<li />");
        //手机号            
        var block1 = $('<div />');
        block1.addClass('class1');
        block1.html(n.mobilePhoneNumber);
        li.append(block1);

        //金额            
        var block2 = $('<div />');
        block2.addClass('class2');
        block2.html("累积已领&yen " + n.amount);
        li.append(block2);
        if (i > 0 && lastAmount == n.amount) {
            li.append('<img src="' + imageServer + (lastIndex + 1) + '.png" class="img" />');
        } else {
            li.append('<img src="' + imageServer + (i + 1) + '.png" class="img" />');
            lastIndex = i;
            lastAmount = n.amount;
        }
        //内容            
        var block3 = $('<div />');
        block3.addClass('class3');
        block3.html('以后点菜就用悠先了.');
        li.append(block3);

        //console.log(i);


        ul.append(li);
    });
}

function loadMyRanking(state, ranking) {
    $("#redEnvelope").addClass("redEnvelopeRanking");
    if (state == 1) {
        var my_ranking1 = $('<div class="myRanking"><img src="' + imageServer + 'up.png" />我的排名已上升到<span class="up">第' + ranking + '名</span></div>');
        $("#redEnvelope").append(my_ranking1);
    } else if (state == 0) {
        var my_ranking0 = $('<div class="myRanking"><img src="' + imageServer + 'keep.png" />我的排名保持在<span class="keep">第' + ranking + '名</span></div>');
        $("#redEnvelope").append(my_ranking0);
    } else {
        var my_ranking_1 = $('<div class="myRanking"><img src="' + imageServer + 'down.png" />我的排名已下降到<span class="down">第' + ranking + '名</span></div>');
        $("#redEnvelope").append(my_ranking_1);
    }
}

// 活动剩余的钱并抢箱子
function loadActivityFindTreasureChest(parent, remainAmount, activityId, hadTreasueChest) {
    var block1 = $("<div />");
    block1.addClass('activity-remain-amount');

    var block2 = $('<div>本活动还剩 <span class="emphasize">&yen' + remainAmount + '</span></div>')
    block2.css({ 'line-height': '26px', 'max-width': '60%' });
    block2.addClass('left');
    if (hadTreasueChest == undefined || hadTreasueChest == 0)
        block2.css({ 'line-height': '44px' });
    block1.append(block2);



    var block4 = $("<div />");
    block4.addClass('right');
    block4.css({ 'width': '40%' });
    var button1 = $('<button type="button">');
    button1.addClass('btn btn-default ');

    button1.html("我要组队");
    button1.unbind('click');
    if (hadTreasueChest == undefined || hadTreasueChest == 0) {
        button1.click(function (event) {
            location.href = "FromApp.aspx?activityId=" + activityId;
        });
    }
    else {
        button1.addClass('button-hui');
    }

    block4.append(button1);
    block1.append(block4);

    if (hadTreasueChest == undefined || hadTreasueChest == 0) {

    } else {
        var block3 = $('<div style="font-size:14px;float:left;max-width:60%;clear:left;text-align:left;">(您已领过宝箱,不能再领取)</div>');
        block3.css({ 'line-height': '18px' });
        block1.append(block3);
    }


    parent.append(block1);
    block1.css("font-size", Number(block1.css("font-size").replace("px", "")) * screenScale);
}

function addButton(parent, clazz, text, fun) {

    var button = $('<button type="button">');
    button.addClass(clazz);
    button.html(text);
    button.unbind("click");
    button.click(fun);
    parent.append(button);
}

//特别鸣谢
function friendshipSponsor() {
    $(".main").append('<div id="thanks"><div class="title">特别鸣谢</div><ul><li><img src="http://image.u-xian.com/UploadFiles/Images/TreasureChest/kz.png" />筷子兄弟</li><li><img src="http://image.u-xian.com/UploadFiles/Images/TreasureChest/HomeBoy.png" />王力宏</li><li><img src="http://image.u-xian.com/UploadFiles/Images/TreasureChest/server.png" />服务器君</li></ul></div></div>');
}

// 手快的坏蛋们
function loadRedEnvelopes(redEnvelopes) {
    if (redEnvelopes == null || redEnvelopes.length == 0) {
        $("#redEnvelope").hide();
    } else {
        $("#redEnvelope").show();
        var ul = $("#redEnvelope > ul");
        ul.empty(true);
        $.each(redEnvelopes, function (i, n) {
            var li = $("<li />");
            //手机号            
            var block1 = $('<div />');
            block1.addClass('class1');
            block1.html(n.mobilePhone);
            li.append(block1);

            //时间            
            var block4 = $('<span />');
            block4.addClass('class4');
            block4.html(n.datetime);
            block1.append(block4);

            //金额            
            var block2 = $('<div />');
            block2.addClass('class2');
            block2.html("&yen " + n.amount);
            li.append(block2);

            //内容            
            var block3 = $('<div />');
            block3.addClass('class3');
            block3.html('谢谢箱主,以后点菜就用悠先了。');
            li.append(block3);



            ul.append(li);
        });
    }
}

/*公有方法*/
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

//头部动画
function animation() {
    var boximgSrc = $(".box > img").attr('src');
    if (boximgSrc.indexOf("_a.png") != -1) {
        $(".box > img").attr('src', boximgSrc.replace('_a.png', '_b.png'));
    } else {
        $(".box > img").attr('src', boximgSrc.replace('_b.png', '_a.png'));
    }
}

function hideOther() {

    $(".suitable-shop").hide();
    $("#whatisux").hide();
    $("#activityRule").hide();

}

function mask() {
    if (navigator.userAgent.match(/MicroMessenger/i)) {
		$(".mask").hide();
		$(".main").show();
		return;
        $(".mask").show();

        var playCount = 0;
        var playTime;
        $('.mask button').click(function (event) {

            playTime = window.setInterval(function () {
                var boximgSrc = $(".mask > img").attr('src');
                if (boximgSrc.indexOf("_a.png") != -1) {
                    $(".mask > img").attr('src', boximgSrc.replace('_a.png', '_b.png'));
                } else {
                    $(".mask > img").attr('src', boximgSrc.replace('_b.png', '_a.png'));
                }
                playCount++;
                if (Math.floor(playCount % 3) == 0) {
                    window.clearInterval(playTime);
                }
            }, 200);

        });


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
function mask2() {
    if (navigator.userAgent.match(/MicroMessenger/i)) {

        $(".mask").show();

        var playCount = 0;
        var playTime;
        $('.mask button').click(function (event) {

            playTime = window.setInterval(function () {
                var boximgSrc = $(".mask > img").attr('src');
                if (boximgSrc.indexOf("_a.png") != -1) {
                    $(".mask > img").attr('src', boximgSrc.replace('_a.png', '_b.png'));
                } else {
                    $(".mask > img").attr('src', boximgSrc.replace('_b.png', '_a.png'));
                }
                playCount++;
                if (Math.floor(playCount % 3) == 0) {
                    window.clearInterval(playTime);
                }
            }, 200);

        });


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
function GetRandomNum(Min, Max) {
    var Range = Max - Min;
    var Rand = Math.random();
    return (Min + Math.round(Rand * Range));
}

//navigator.userAgent.match(/(iPhone|iPod|iPad);?/i) || navigator.userAgent.match(/android/i)
//alert(navigator.userAgent);
function open_or_download_app() {
    if (navigator.userAgent.match(/(iPhone|iPod|iPad);?/i)) {
        // 判断useragent，当前设备为ios设备            
        window.location = "http://a.app.qq.com/o/simple.jsp?pkgname=va.dish.sys";　　// iPhone端URL Schema
    } else if (navigator.userAgent.match(/android/i)) {
        // 判断useragent，当前设备为android设备
        window.location = "http://a.app.qq.com/o/simple.jsp?pkgname=va.dish.sys";　　// Android端URL Schema
    }
}

function lookRedEnvelope() {

    window.location = 'list.aspx?mobilePhone=' + $.cookie("mobilePhoneNumber");
}