var windowHeight = 0; //屏幕高度
var pageNum = 0, //page页面个数
	nowPage = 0; //当前页面

var btnHeight = 0;
var titleHeight = 0;

//跳过
var isSkip = false;

var shopID = '';
var employeeID = '';

var isDataSuccess = false;
var isAjaxing = false;

var dishI = 0;

$(document).ready(function() {

	shopID = $.getUrlVar('s');
	employeeID = $.getUrlVar('e');

	windowHeight = parseInt($(window).height());
	pageNum = parseInt($('.main').children().length);

	//清除默认事件
	addUnDefault();

	//初始化
	pageInit();

	//下一页
	pageturn();
});

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

function pageInit() {
	btnHeight = $(".btn").outerHeight();
	titleHeight = $(".title").outerHeight(true);
	$('.main').css({
		"height": (windowHeight * pageNum) + "px"
	});
	$('.page').css({
		"height": windowHeight + "px"
	});

	$('body').on('touchend', '#skip', function() {
		isSkip = true;
		$('.next').trigger('touchend');
	});
}

//翻页
function pageturn() {
	$('.next').on('touchend', function() {
		nowPage++;
		var str = ['抽奖活动设置', '免排队名额设置', '赠送菜品设置'];
		for (var i = 0; i < pageNum; i++) {
			if (i == nowPage) {

				//点击“跳过”
				if (isSkip) {
					$(document).attr('title', str[i]);
					$('.main').css({
						"transform": "translate(0, -" + windowHeight * nowPage + "px)"
					});
					isSkip = false;
				} else {
					var isTurn = chkIpt(i);
					if (isTurn) {
						$(document).attr('title', str[i]);
						$('.main').css({
							"transform": "translate(0, -" + windowHeight * nowPage + "px)"
						});
					} else {
						nowPage--;
					}
				}
			}
		}
		
		if (nowPage == 2) {
			$('#pageContent').css({
				"height": (windowHeight - btnHeight - titleHeight) + "px"
			});

			pageContentScroll('#pageContent');
			$('#addDish').on('touchend', function() {
				addDish();
				pageScroll.refresh();
			});

			//点击删除新增菜品
			$('body').on('touchend', '.close', function() {
				$(this).parent().parent().parent().parent().remove();
				pageScroll.refresh($(this).parent().parent().parent().parent());
			});
			
			//赠送菜input输入后搜索
			$('body').on('input', '.dishname', function(){
				var key = $(this).val();
				var dishwrapheight = $(this).parent().outerHeight(true);
				var wrapIptId = $(this).parent().attr('id');
				$('#'+wrapIptId+' ul').css({"top":dishwrapheight+"px"});
				
				$.ajax({
					contentType: "application/json",
					url: '/Award/AwardMsg.aspx/SearchDishMeau',
					type: 'POST',
					dataType: 'json',
					//async: false,
					data: JSON.stringify({pageIndex: 1, pageSize: 10, key: key, shopID: shopID}),
					success: function(d) {
						d = JSON.parse(d.d);
						var s = '';
						
						for( var i = 0; i < d.dishInfoDetailList.length; i++ ){
							s += '<li data-dishID=' + d.dishInfoDetailList[i].dishID + ' data-dishPriceID=' + d.dishInfoDetailList[i].dishPriceID + '>' + d.dishInfoDetailList[i].dishName + '</li>';
						}
						$('#'+wrapIptId+' ul').html(s);
						
						$('#'+wrapIptId+' li').on('touchend', function(){
							$('#'+wrapIptId+' input').val( $(this).text() );
							$('#'+wrapIptId+' input').attr({"data-dishid": $(this).attr('data-dishID') });
							$('#'+wrapIptId+' input').attr({"data-dishPriceid": $(this).attr('data-dishPriceID') });
							$('#'+wrapIptId+' ul').empty();
						});
					},
					error: function(msg) {
						showtips('系统异常');
					}
				});
			});
			
			
			//保存
			$('body').on('touchend', '#save', function() {
				var isSave = chkIpt(3);
				if (isSave) {
					console.log("isSave")
					//location.href = '/Award/awardMobile/awardSet/awardSee.html';
				} else {
					nowPage--;
				}
			});
		}

		//桌数文本框失去焦点
		$('body').on('blur', '#deskNum', function() {
			var percent = 0.1;
			var val = parseInt($(this).val());

			if (!val == '') {
				var num = Math.round(val * percent);
				$('#freeQueueNum').val(num);
			}
		});

	});
}

//检查input
function chkIpt(nowPage) {
	var turnNext = false;
	var inputNum = $('#page' + (nowPage - 1)).find('input').length;
	if (!inputNum == 0) {
		$('#page' + (nowPage - 1) + ' input').each(function() {
			var val = $(this).val();
			if (val == '') {
				showtips(this);
				turnNext = false;
				return false;
			} else {
				turnNext = true;
			}
		});
	} else {
		turnNext = true;
	}

	if (turnNext) {
		return postData(nowPage - 1);
	}
}

//提交数据
function postData(type) {
	var count = $('#freeQueueNum').val();
	var url = ['/Award/AwardMsg.aspx/OpenDraw', '/Award/AwardMsg.aspx/AddQueue', '/Award/AwardMsg.aspx/AddDishMeau'];
	var data = [{shopID: shopID}, {shopID: shopID, employeeID: employeeID, count: count }, {dishJson: JSON.stringify({dishInfoList: getDishJson()})}];
	$('#loading').removeClass('hide');
	
	$.ajax({
		contentType: "application/json",
		url: url[type],
		type: 'POST',
		dataType: 'json',
		async: false,
		data: JSON.stringify(data[type]),
		success: function(d) {
			d = JSON.parse(d.d);
			if (d.ErrorState == 1) {
				//showtips(d.Message);
				$('#loading').addClass('hide');
				isDataSuccess = true;
				
				if( type == 2 ){
					location.href = '/Award/awardMobile/awardSet/awardSee.html?s=' + shopID + '&e=' + employeeID;
				}
				
				setTimeout(function(){
					$('#loading').addClass('hide');
				}, 0);

			} else if (d.ErrorState == '-1') {

				showtips(d.Message);
				$('#loading').addClass('hide');
				isDataSuccess = false;

			}
		},
		error: function(msg) {

			showtips('系统异常');
			$('#loading').addClass('hide');
			isDataSuccess = false;

		}
	});
	return isDataSuccess;
}

//拼菜品与数量
function getDishJson() {
	var ii = 0,
			jj = 0;
	var dishnames = [],
			dishnums = [],
			dishids = [],
			dishPrices = [];
	var s = {};
	var dishInfoList = [];
	
	$('#pageContent .dishname').each(function() {
		dishnames[ii] = $(this).val();
		dishids[ii] = $(this).attr('data-dishid');
		dishPrices[ii] = $(this).attr('data-dishPriceid');
		ii++;
	});
	$('#pageContent .dishnum').each(function() {
		dishnums[jj] = $(this).val();
		jj++;
	});
	for (var i = 0; i < dishnames.length; i++) {
		s.shopID = shopID;
		s.dishID = dishids[i];
		s.dishPriceID = dishPrices[i];
		s.dishName = dishnames[i];
		s.count = dishnums[i];
		s.employeeID = employeeID;
		dishInfoList.push(s);
	}
	return dishInfoList;
}

//显示提示
function showtips(t) {
	var tip = $(t).parent().prev().text() + "，不能为空";
	if ($(t).parent().prev().text() == '') {
		tip = t;
	}
	if( nowPage == 3 ){
	tip = $(t).parent().parent().prev().html() + "，不能为空";
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

//新增赠送菜品
function addDish() {
	dishI++;
	var str = '<table class="dishTable" cellpadding="0" cellspacing="0">';
	str += '<tr class="atr">';
	str += '<td class="td0 borderbottom" rowspan="2"><img src="img/del.png" class="close" /></td>';
	str += '<td class="td1">赠菜名称</td>';
	str += '<td class="atd td2"><div class="iptWrap" id="iptWrap' + dishI + '"><input type="text" class="dishname" placeholder="请输入菜品名称进行查找" /><ul></ul></div></td>';
	str += '<tr>';
	str += '<td class="td1 bordertop borderbottom">每日奖品分数</td>';
	str += '<td class="td2 bordertop borderbottom"><input type="text" class="dishnum" placeholder="请设置每日奖品份数" /></td>';
	str += '</tr>';
	str += '</table>';
	
	$('.dishs .dishTable:last-child').after(str);
}

//pageContent滚动IScroll
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