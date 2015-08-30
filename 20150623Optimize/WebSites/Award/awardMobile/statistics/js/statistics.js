var windowHeight = 0; //屏幕高度
var shopID = '';
var page = 0;
var pageSize = 5;
var isMore = false;

$(document).ready(function() {
	shopID = $.getUrlVar('s');

	windowHeight = parseInt($(window).height());

	//清除默认事件
	addUnDefault();

	//初始化
	pageInit();

	//获取数据
	getData();
	
	//滚动
	pageContentScroll('.page');
});

function pageInit() {
	$('.main').css({
		"height": windowHeight + "px"
	});
	$('.page').css({
		"height": windowHeight + "px"
	});
}

//获取数据
function getData() {
	$('#tip').html('正在加载...');
	page++;
	$.ajax({
		contentType: "application/json",
		url: '/Award/AwardMsg.aspx/SearchAwardTotal',
		type: 'POST',
		dataType: 'json',
		data: JSON.stringify({
			shopID: shopID,
			pageIndex: page,
			pageSize: pageSize
		}),
		success: function(d) {
			d = JSON.parse(d.d);
			isMore = d.HasNext;
			console.log("a: "+isMore)
			if (d.ErrorState == 1) {
				
				if (!d.AwardTotalDetailList == '' || !d.AwardTotalDetailList == null) {
					var s = '';
					for( var i = 0; i < d.AwardTotalDetailList.length; i++ ){
						var a = '', b = '';
						for( var j = 0; j < d.AwardTotalDetailList[i].AwardCountList.length; j++ ){
							b += '<li>【'+ d.AwardTotalDetailList[i].AwardCountList[j].AwardName +'】：'+ d.AwardTotalDetailList[i].AwardCountList[j].Count +'</li>'
						}
						a = '<table cellpadding="0" cellspacing="0">'
						a += 	'<tr>'
						a +=		'<td class="day" valign="top">' + formatDate(d.AwardTotalDetailList[i].AwardDate) + '</td>'
						a +=		'<td class="td1" valign="top">'
						a +=			'<ul><li>中奖人数统计：</li>' + b + '</ul>'
						a +=		'</td>'
						a +=		'<td class="td2" valign="top">'
						a +=			'<ul>'
						a +=				'<li>活动效果统计：</li>'
						a +=				'<li>[订单金额]：'+d.AwardTotalDetailList[i].OrderMoneyTotal+'</li>'
						a +=				'<li>[订单量]：'+d.AwardTotalDetailList[i].OrderTotalCount+'</li>'
						a +=			'</ul>'
						a +=		'</td>'
						a +=	'</tr>'
						a += '</table>'
						
						s += a;
					}
					$('#tip').before(s);
					
					setTimeout(function(){
						pageScroll.refresh()
					}, 0);
				} else {
					$('#tip').before('<div class="noData">无数据</div>');
				}
				$('#tip').html('上拉加载更多');
			} else {
				//showtips(d.Message);
				showtips('系统异常');
			}
			if (!isMore) {
				$('#tip').html('');
			}
		},
		error: function(msg) {
			showtips('系统异常');
			//$('#loading').addClass('hide');
		}
	});
}


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

//显示提示
function showtips(t) {
	var tip = $(t).parent().prev().text() + "，不能为空";
	if ($(t).parent().prev().text() == '') {
		tip = t;
	}
	$('#tips').html(t);
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

//pageContent滚动IScroll
var pageScroll;

function pageContentScroll(id) {
	pageScroll = new IScroll(id, {
		mouseWheel: false
	});
	pageScroll.on('scrollEnd', function(){
		//如果滑动到底部，则加载更多数据（距离最底部大于-1高度）
		if( isMore ){
			console.log("b: "+(this.y - this.maxScrollY))
			if ((this.y - this.maxScrollY) > -1) {
				getData();
			}
		}
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
  var re = /-?\d+/;
  var m = re.exec(val);
  var d = new Date(parseInt(m[0]));
  
  var d0 = new Date(0);
	if( d0.getFullYear() < 1970 ){
		d.setHours(d.getHours () + 18);
	}
  
  // 按【2012-02-13 09:09:09】的格式返回日期
  return d.format("MM月dd日");
}