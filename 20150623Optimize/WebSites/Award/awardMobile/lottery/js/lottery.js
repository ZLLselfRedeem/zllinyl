var windowWidth = 0, //屏幕宽度
	windowHeight = 0, //屏幕高度
	pageNum = 0; //page个数

var shopID = 0;
var preOrder19DianId = 0;

var device = getDevices();
var inWx = navigator.userAgent.match(/MicroMessenger/i); // wei xin
var inAp = device.iOS; // ios
var inAn = device.Android;

$(document).ready(function() {
	windowWidth = parseInt($.getUrlVar('ww'));
	windowHeight = parseInt($.getUrlVar('wh'));

	shopID = $.getUrlVar('s');
	preOrder19DianId = $.getUrlVar('p')

	//清除默认事件
	addUnDefault();

	//page页面初始化
	pageInit();
});

//初始化
function pageInit() {
	var btnHeight = $("#closeLottery").outerHeight(true);
	
	$('.wrap').css({
		"width": windowWidth + "px",
		"height": windowHeight + "px"
	});
	$('.main').css({
		"width": windowWidth * pageNum + "px",
		"transform": "translateX(0)"
	});
	$('.page').css({
		"width": windowWidth + "px",
		"height": windowHeight + "px"
	});

	//page内容上下滑动
	//pageContentScroll('#page0');
	
	//获取数据
	getData();
}

function getData() {
	var imgArr = ['img/redenvelope.png', 'img/freequeue.png', 'img/dishs.png', 'img/others.png'];
	var classArr = ['redEnvelope', 'freeQueue', 'dishs', 'others'];

	var dishImgW = parseInt(windowWidth * 0.6);
	var dishImgH = parseInt(dishImgW * (3 / 4));
	$('.dishImg').css({
		"width": dishImgW + "px",
		"height": dishImgH + "px",
		"margin-left": "-" + (dishImgW / 2) + "px"
	});

	var type = 0;

	$.ajax({
		contentType: "application/json",
		url: '/Award/AwardMsg.aspx/GetAwardDetail',
		type: 'POST',
		dataType: 'json',
		data: JSON.stringify({
			shopID: shopID,
			preOrder19DianId: preOrder19DianId
		}),
		success: function(d) {
			d = JSON.parse(d.d);

			var imgtype = '',
				classname = '';

			if (d.ErrorState == 1) {
				if (d.ShopAwardType == 4) { //红包
					imgtype = classname = 0;
					$('#' + classArr[classname] + " .redEnvelopePrice span").html(d.RedMoneyTotal)
					$('#' + classArr[classname] + " .btn").attr({
						"data-value": d.RedUrl
					});
					$('.awardtip').css({"padding-top": (windowHeight * 0.343) + "px"});
				} else if (d.ShopAwardType == 2) { //免排队
					imgtype = classname = 1;
					$('#' + classArr[classname] + " .shopInf").html(d.ShopName + '<br/><span>' + d.NotLotteryDate + '</span><p>有效</p>');
					$('.awardtip').css({"padding-top": (windowHeight * 0.272) + "px"});
					$('.shopInf').css({"top": (windowHeight * 0.045) + "px"});
				} else if (d.ShopAwardType == 3) { //送菜
					imgtype = classname = 2;
					if( d.ThirdUrl != '' ){
						$('#' + classArr[classname] + " .dishImg img").attr({
							"src": d.DishPicUrl
						});
					}
				} else if (d.ShopAwardType == 5) { //第三方
					imgtype = classname = 3;
					$('#' + classArr[classname] + " .btn").attr({
						"data-value": d.ThirdUrl
					});
					$('.awardtip').css({"padding-top": (windowHeight * 0.24) + "px"});
				}

				$('#' + classArr[classname] + " .btn").on('touchend', function() {
					if (d.IsThirdCompoate) {
						var type = $(this).attr('data-type'),
							value = $(this).attr('data-value');
						if (inAp) {
							window.location = "message:type:" + type + ",value:" + value;
						} else if (inAn) {
							window.commonJSInterface.jsInvokeNativeMethod('{ "type": "' + type + '", "value": "' + value + '" }');
						}
					} else {
						if (imgtype == 3) {
							showtips('订单消费完成后才可以领取哦');
						}
					}
				});
				
				$('.pageContent').addClass(classArr[classname]);
				$('#' + classArr[classname]).css({
					"background": "url(" + imgArr[imgtype] + ")",
					"background-size": "100% 100%"
				});
				$('#' + classArr[classname] + " .awardInf").html(d.AwardName);

				$('#loading').addClass('hide');

			}else{
				$('#loading').addClass('hide');
				showtips('系统异常');
			}
		},
		error: function(msg) {
			$('#loading').addClass('hide');
			showtips('系统异常');
		}
	});
}

//显示提示
function showtips(t) {
	var tip = $(t).parent().prev().text() + "，不能为空";
	if ($(t).parent().prev().text() == '') {
		tip = t;
	}
	$('#tips').html(tip);
	$('#tipsBg').removeClass('hide');
	$('#tips').removeClass('hide');
	$('#tips').animate({
		"top": "130px",
		"opacity": "1"
	});
	setTimeout(function() {
		$('#tips').animate({
			"top": "70px",
			"opacity": "0"
		}, function() {
			$('#tipsBg').addClass('hide');
			$('#tips').addClass('hide');
		});
	}, 1000);
}

//如果没有图片
var imgerror = function(pic) {
	$(pic).attr({
		'src': 'img/dish.png'
	});
}

//默认事件
var unDefalut = function(e) {
	e = e || window.event;
	e.preventDefault();
}

function addUnDefault() {
	//document.documentElement.addEventListener('touchstart', unDefalut);
	document.documentElement.addEventListener('touchmove', unDefalut);
	//document.documentElement.addEventListener('mousedown', unDefalut);
	document.documentElement.addEventListener('mouseover', unDefalut);
}

function removeUnDefault() {
	document.documentElement.removeEventListener('touchstart', unDefalut);
	document.documentElement.removeEventListener('mousedown', unDefalut);
}

//page页内容滚动IScroll
var pageScroll;

function pageContentScroll(id) {
	pageScroll = new IScroll(id, {
		mouseWheel: false
	});
}
$.extend({
	getUrlVars: function() {
		var vars = [],
			hash;
		var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
		for (var i = 0; i < hashes.length; i++) {
			hash = hashes[i].split('=');
			vars.push(hash[0]);
			vars[hash[0]] = hash[1];
		}
		return vars;
	},
	getUrlVar: function(name) {
		return $.getUrlVars()[name];
	}
});


function getDevices() {
	var u = navigator.userAgent,
		app = navigator.appVersion;

	return {
		trident: u.indexOf('Trident') > -1,
		presto: u.indexOf('Presto') > -1,
		webKit: u.indexOf('AppleWebKit') > -1,
		gecko: u.indexOf('Gecko') > -1 && u.indexOf('KHTML') == -1,
		mobile: !!u.match(/AppleWebKit.*Mobile.*/),
		iOS: !!u.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/),
		Android: u.indexOf('Android') > -1 || u.indexOf('Linux') > -1,
		iPhone: u.indexOf('iPhone') > -1,
		iPad: u.indexOf('iPad') > -1,
		webApp: u.indexOf('Safari') == -1,
	};
};