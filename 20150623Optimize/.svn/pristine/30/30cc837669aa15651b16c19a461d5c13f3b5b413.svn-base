var windowHeight = 0; //屏幕高度

var shopID = '';
var employeeID = '';
var index = 0;
var oldAwardJson = '';
var SuppliersName = '';

var shop = [2, 3];
var uXian = [4, 5];

$(document).ready(function() {

	shopID = $.getUrlVar('s');
	employeeID = $.getUrlVar('e');
	windowHeight = parseInt($(window).height());

	//清除默认事件
	addUnDefault();

	//初始化
	pageInit();

	//添加滚动
	pageContentScroll('.page');
});

function pageInit() {
	$('.main').css({
		"height": windowHeight + "px"
	});
	$('.page').css({
		"height": windowHeight + "px"
	});

	//获取数据
	getData();
}

//获取数据
function getData() {
	$.ajax({
		contentType: "application/json",
		url: '/Award/AwardMsg.aspx/SearchAwardList',
		type: 'POST',
		dataType: 'json',
		data: JSON.stringify({
			shopID: shopID
		}),
		success: function(d) {
			d = JSON.parse(d.d);
			var hasQueue = false;
			if (d.ErrorState == 1) {
				oldAwardJson = d;
				var shopOffer = '';
				var uXianOffer = '';
				var dishList = '';
				var uList = '';
				
				for (var i = 0; i < d.AwardDetailList.length; i++) {

					//店铺提供
					if (shop.indexOf(d.AwardDetailList[i].Type) > -1) {
						if (d.AwardDetailList[i].Type == 2) {
							$('#queueNum').val(d.AwardDetailList[i].Count);
							$('#queueNum').attr({
								"data-awardid": d.AwardDetailList[i].AwardID
							});
							hasQueue = true;
						} else {
							dishList += '<tr>';
							dishList += '<td class="dishbtn close"><img src="img/del.png" /></td>';
							dishList += '<td class="dishname">';
							dishList += '<div id="dishname' + index + '">';
							dishList += '<input type="text" value="' + d.AwardDetailList[i].AwardName + '" data-awardid="' + d.AwardDetailList[i].AwardID + '" data-dishid="' + d.AwardDetailList[i].DishID + '" data-dishpriceid="' + d.AwardDetailList[i].DishPriceID + '" placeholder="请输入菜名搜索" />';
							dishList += '<ul></ul></div>';
							dishList += '</td>';
							dishList += '<td class="dishnum"><input type="number" id="dishnum' + index + '" value="' + d.AwardDetailList[i].Count + '" placeholder="0" />份</td>';
							dishList += '</tr>';
							index++;
						}
						
						if( hasQueue ){
							$('.unqueue').addClass('hide');
							$('.queue').addClass('queueTurn0');
							$('.queue').removeClass('queueTurn');
						}
						shopOffer = SuppliersName = d.AwardDetailList[i].SuppliersName;
					}

					//悠先提供
					if (uXian.indexOf(d.AwardDetailList[i].Type) > -1) {
						uList += '<tr><td data-award="' + d.AwardDetailList[i].AwardID + '" data-dishid="' + d.AwardDetailList[i].DishID + '">' + d.AwardDetailList[i].AwardName + '</td></tr>';
						uXianOffer = d.AwardDetailList[i].SuppliersName;
						$('.redEnvelope').removeClass('hide');
					}
				}
				if( !(shopOffer == '') ){
					$('.shopOffer').html('由【' + shopOffer + '】提供');
				}
				if( !(uXianOffer == '') ){
					$('.uXianOffer').html('由【' + uXianOffer + '】提供');
				}
				
				
				$('#add').before(dishList);
				$('.uXianOffer').parent().before(uList);
				setTimeout(function() {
					$('#loading').addClass('hide');
					$('.page').removeClass('hide');
					pageScroll.refresh();
					index = $('.dishlist').find('.dishname').length;
					btnBindEvent();
				}, 0);
			}
		},
		error: function(msg) {
			showtips('系统异常');
		}
	});
}

//提交数据
function postData() {
	var toPost = chkIpt();
	if (toPost) {

		var aa = {
			AwardDetailList: getDishJson(),
			ErrorState: 1,
			IsAvoidQueue: true
		};
		var bb = oldAwardJson;
		var cc = JSON.stringify({
			shopID: shopID,
			employeeID: employeeID,
			awardJson: aa,
			oldAwardJson: bb
		});
		console.log(cc)
		$('#loading').removeClass('hide');
		$.ajax({
			contentType: "application/json",
			url: '/Award/AwardMsg.aspx/SetAwards',
			type: 'POST',
			dataType: 'json',
			data: JSON.stringify({
				shopID: shopID,
				employeeID: employeeID,
				awardJson: JSON.stringify({
					AwardDetailList: getDishJson(),
					ErrorState: 1,
					IsAvoidQueue: true
				}),
				oldAwardJson: JSON.stringify(oldAwardJson)
			}),
			success: function(d) {
				d = JSON.parse(d.d);
				if (d.ErrorState == 1) {
					showtips(d.Message);
					location.replace(location.href);
				} else {
					showtips(d.Message);
					$('#loading').addClass('hide');
				}
			},
			error: function(msg) {
				showtips('系统异常');
				$('#loading').addClass('hide');
			}
		});
	}
}

//检查input
function chkIpt() {
	var isSet = false;
	var inputNum = $('.page').find('input').length;
	if (!inputNum == 0) {
		$('.page' + ' input').each(function() {
			var val = $(this).val();
			if (val == '') {
				showtips('请正确输入信息');
				isSet = false;
				return false;
			} else {
				isSet = true;
				return true;
			}
		});
	} else {
		isSet = true;
	}

	if (isSet) {
		return true;
	}
}

//拼菜品与数量
function getDishJson() {
	var ii = 0,
		jj = 0;
	var dishnames = [],
		dishnums = [],
		dishids = [],
		dishPrices = [],
		dishAwards = [];
	var m = {};

	var dishInfoList = [];
	var b = {};

	//免排队
	m.AwardID = $('#queueNum').attr('data-awardid');
	m.AwardName = '免排队';
	m.Count = $('#queueNum').val();
	m.DishId = 0;
	m.DishPriceID = 0;
	m.SuppliersName = SuppliersName;
	m.Type = 2;
	dishInfoList.push(m);

	$('.dishlist .dishname input').each(function() {
		dishnames[ii] = $(this).val();
		dishids[ii] = $(this).attr('data-dishid');
		dishPrices[ii] = $(this).attr('data-dishPriceid');

		var a = $(this).attr('data-awardid');
		if (!a) {
			dishAwards[ii] = 0;
		} else {
			dishAwards[ii] = $(this).attr('data-awardid');
		}
		ii++;
	});

	$('.dishlist .dishnum input').each(function() {
		dishnums[jj] = $(this).val();
		jj++;
	});
	for (var i = 0; i < dishnames.length; i++) {
		var s = {};
		s.AwardID = dishAwards[i];
		s.AwardName = dishnames[i];
		s.Count = dishnums[i];
		s.DishID = dishids[i];
		s.DishPriceID = dishPrices[i];
		s.SuppliersName = SuppliersName;
		s.Type = 3;
		dishInfoList.push(s);
	}
	return dishInfoList;
}

//按钮事件委托
function btnBindEvent() {
	//点击开启免排队
	$('.unqueue').on('touchend', function() {
		$(this).addClass('turn')
		$('.queue').addClass('queueTurn0')
		$('.queue').removeClass('queueTurn')
		setTimeout(function(){
			$('.unqueue').css({"display":"none"})
		}, 150)
	});

	//删除赠菜
	$('body').on('touchend', '.close', function() {
		$(this).parent().remove();
		pageScroll.refresh();
	});
	
	var ts = 0,
		tm = 0,
		tX = 0;
	$('body').on('touchstart', '#add', function(e) {
		ts = 0;
		tm = 0;
		tX = e.originalEvent.changedTouches[0].pageX;
		ts = 1;
	});
	$('body').on('touchmove', '#add', function(e) {
		var thisX = e.originalEvent.changedTouches[0].pageX;
		var movelen = thisX - tX;
		if (Math.abs(movelen) > 5) {
			tm = 1;
		}
	});
	//新增赠送菜品
	$('body').on('touchend', '#add', function() {
		if (ts === 1 && tm !== 1) {
			var s = '<tr>';
			s += '<td class="dishbtn close"><img src="img/del.png" /></td>';
			s += '<td class="dishname"><div id="dishname' + index + '"><input type="text" value="" placeholder="请输入菜名搜索" /><ul></ul></div></td>';
			s += '<td class="dishnum"><input type="number" id="dishnum' + index + '" value="" placeholder="0" />份</td>';
			s += '</tr>';
			$(this).before(s);
			index++;
			pageScroll.refresh();
		}
	});
	//保存 - 提交数据
	$('body').on('touchend', '#submitData', function() {
		postData();
	});
	//搜索菜品
	$('body').on('input', '.dishname input', function() {
		var dishnameIptWidth = $(this).outerWidth();
		$('.dishname li').css({"width": parseInt(dishnameIptWidth) + "px"});
		
		var key = $(this).val();
		var dishwrapheight = $(this).parent().outerHeight(true);
		var wrapIptId = $(this).parent().attr('id');

		$.ajax({
			contentType: "application/json",
			url: '/Award/AwardMsg.aspx/SearchDishMeau',
			type: 'POST',
			dataType: 'json',
			data: JSON.stringify({
				pageIndex: 1,
				pageSize: 10,
				key: key,
				shopID: shopID
			}),
			success: function(d) {
				d = JSON.parse(d.d);
				var s = '';

				for (var i = 0; i < d.dishInfoDetailList.length; i++) {
					s += '<li data-dishid=' + d.dishInfoDetailList[i].dishID + ' data-dishPriceID=' + d.dishInfoDetailList[i].dishPriceID + '>' + d.dishInfoDetailList[i].dishName + '</li>';
				}
				$('#' + wrapIptId + ' ul').html(s);

				$('#' + wrapIptId + ' li').on('touchend', function() {
					$('#' + wrapIptId + ' input').val($(this).text());
					$('#' + wrapIptId + ' input').attr({
						"data-dishid": $(this).attr('data-dishID')
					});
					$('#' + wrapIptId + ' input').attr({
						"data-dishPriceid": $(this).attr('data-dishPriceID')
					});
					$('#' + wrapIptId + ' ul').empty();
				});
			},
			error: function(msg) {
				showtips('系统异常');
			}
		});
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

//清除默认
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