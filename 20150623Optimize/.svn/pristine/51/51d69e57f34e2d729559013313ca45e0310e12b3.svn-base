var
	windowWidth = 0, //屏幕宽度
	windowHeight = 0; //屏幕高度

var shopID = '',
	cityID = '';

var markHeight = 0;

var
	myScroll0,
	myScroll1;

document.addEventListener('touchmove', function (e) { e.preventDefault(); }, false);

$(document).ready(function() {
	shopID = $.getUrlVar('s');
	cityID = $.getUrlVar('c');

	windowWidth = parseInt($.getUrlVar('ww'));
	windowHeight = parseInt($.getUrlVar('wh'));
	
	//page页面初始化
	pageInit();

});

//初始化
function pageInit() {
	$('body').css({"width":windowWidth + "px"});
	$('.wrap').css({"width":windowWidth + "px", "height":windowHeight + "px"});
	$('.page').css({"width":windowWidth + "px", "padding-bottom":"20px"});
	$('.main').css({"height": (windowHeight - 20) + "px", "padding-bottom":"20px"});	
	$('#mark').css({"width":windowWidth + "px"});
	
	setTimeout(function() {
		getData(shopID);
	}, 0);
}

//左右滑页
function pageSlide(){
	var swiper = new Swiper('.main', {
    pagination: '.swiper-pagination',
    paginationClickable: true
  });
}

//获取数据
function getData(shopID) {
	$.ajax({
		contentType: "application/json",
		url: '/Award/AwardMsg.aspx/GetShopAwardNoticeDetail',
		type: 'POST',
		dataType: 'json',
		data: JSON.stringify({
			shopID: shopID,
			cityID: cityID
		}),
		success: function(data) {
			var d = JSON.parse(data.d);
			if (d.ErrorState === 1) {
				if (d.isLottery) {
					//是否开启抽奖
					$('#page0').removeClass('hide');
					
					//本店奖品
					var listShopAward = '';
					for (var i = 0; i < d.ListShopAwardDetail.length; i++) {
						listShopAward += '<dl>';
						listShopAward += '<dt><img src="img/award' + d.ListShopAwardDetail[i].ShopAwardType + '.png" /></dt>';
						listShopAward += '<dd>' + d.ListShopAwardDetail[i].Name + '</dd>';
						listShopAward += '</dl>';
					}
					$('.awards').html(listShopAward + "<div class='c'></div>");
					var dlNum = parseInt($('.awards').find('dl').length);
					if (dlNum < 5) {
						$('.awards dl').css({
							"margin-left": (28 / (dlNum + 1)) + "%"
						});
					} else {
						$('.awards dl').css({
							"margin-left": "5.6%"
						});
					}
					
					//奖品列表为空
					if( d.ListShopAwardDetail.length == 0 ){
						$('#page0').addClass('hide');
					}

					//最近中奖
					var listAwardUser = '';
					for (var i = 0; i < d.ListAwardUserDetail.length; i++) {
						if( d.ListAwardUserDetail[i].MobilePhone != null && d.ListAwardUserDetail[i].AwardName != null ){
							listAwardUser += '<li>用户' + d.ListAwardUserDetail[i].MobilePhone + '抽中【' + d.ListAwardUserDetail[i].AwardName + '】</li>';
						}
					}
					
					$('.lucyPeople li:last-child').after(listAwardUser);
					if( d.ListAwardUserDetail.length != 0 && listAwardUser != '' ){
						$('.lucyPeople').removeClass('hide');
					}

					//活动规则
					$('.awardRule li:last-child').after(d.AwardRule);
					if( !(d.AwardRule == '') ){
						$('.awardRule').removeClass('hide');
					}
					
				}
				
				//折扣
				if (!(d.CouponDiscount == 0)) {
					$('#CouponDiscount').html(d.CouponDiscount);
					$('#CouponDiscount').parent().parent().parent().parent().parent().removeClass('hide');
				}
				//券列表
				var couponlist = '';
				for (var i = 0; i < d.couponDetails.length; i++) {
					var t = formatDate(d.couponDetails[i].couponValidityEnd);
					couponlist += '<li>';
					couponlist += '<table>';
					couponlist += '<tr>';
					couponlist += '<td rowspan="2" valign="top"><div class="radiusD radiusD0">券</div></td>';
					couponlist += '<td>每满' + d.couponDetails[i].requirementMoney + '减' + d.couponDetails[i].deductibleAmount + '，最多减' + d.couponDetails[i].maxAmount + '</td>';
					couponlist += '</tr>';
					couponlist += '<tr>';
					couponlist += '<td><span>截止日期：' + t + '</span></td>';
					couponlist += '</tr>';
					couponlist += '</table>';
					couponlist += '</li>';
				}
				$('#couponDetails').html(couponlist);
				
				//没有折扣没有券
				if( d.CouponDiscount == 0 && d.couponDetails.length == 0 ){
					$('.discount').addClass('hide');
					$('.line').addClass('hide');
				}

				//公告内容
				if (!(d.ShopNoticeContent == '')) {
					$('#ShopNoticeContent').html(d.ShopNoticeContent);
					$('.shopNotice').removeClass('hide');
				}
				
				//折扣、券、公告都不为空
				if( (d.CouponDiscount != 0) || (d.couponDetails.length != 0) || (d.ShopNoticeContent != '')){
					$('#page1').removeClass('hide');
					//$('.line').addClass('hide');
				}
				
				setTimeout(function() {
					$('#loading').addClass('hide');
					$('#pageContent1').css({"height": (parseInt($('#pageContent1').height()) + 50) + "px"});
					$('#pageContent0').css({"height": (parseInt($('#pageContent0').height()) + 50) + "px"});
					myScroll0 = new IScroll('#page0');
					myScroll1 = new IScroll('#page1');
					
					//page左右滑动
					pageSlide();
					
				}, 0);
				
			} else {
				showtips('系统异常');
			}

			/*setTimeout(function() {
				//pageContentScroll('#page0');
				if ($('#page0').hasClass('hide')) {
					pageNum = pageNum - 1;
				}
				markNum();
			}, 0);*/
		},
		error: function(msg) {
			showtips('系统异常');
		}
	});
}

//显示提示
function showtips(tip) {
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
	}, 2000);
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

/*
function getLocalTime(t) {
	$('#debug').append(t+"===<bt/>");
	var t = new Date(parseInt(t) * 1000).toLocaleString();
	$('#debug').append(t+"===<bt/>");
	t = t.split(' ');
	$('#debug').append(t+"===<bt/>");
	var tt = t[0].split('/');
	$('#debug').append(tt+"===<bt/>");
	tt = tt[0] + '年' + tt[1] + '月' + tt[2] + '日';
	$('#debug').append(tt+"===<bt/>");
	return tt;
	//return t;
}*/


Date.prototype.format = function (format) //author: meizz
{
  var o = {
    "M+": this.getMonth() + 1, //month
    "d+": this.getDate(),    //day
    "h+": this.getHours(),   //hour
    "m+": this.getMinutes(), //minute
    "s+": this.getSeconds(), //second
    "q+": Math.floor((this.getMonth() + 3) / 3),  //quarter
    "S": this.getMilliseconds() //millisecond
  }
  if (/(y+)/.test(format)) format = format.replace(RegExp.$1,
  (this.getFullYear() + "").substr(4 - RegExp.$1.length));
  for (var k in o) if (new RegExp("(" + k + ")").test(format))
      format = format.replace(RegExp.$1,
    RegExp.$1.length == 1 ? o[k] :
      ("00" + o[k]).substr(("" + o[k]).length));
  return format;
}
function formatDate(val) {
  /*var re = /-?\d+/;
  var m = re.exec(val);*/
  var d = new Date(parseInt(val)*1000);
  
  var d0 = new Date(0);
	if( d0.getFullYear() < 1970 ){
		d.setHours(d.getHours () + 18);
	}
  
  // 按【2012-02-13 09:09:09】的格式返回日期
  return d.format("yyyy年MM月dd日");
}