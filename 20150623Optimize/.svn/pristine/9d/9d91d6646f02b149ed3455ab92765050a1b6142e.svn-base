var ww = 0, //屏幕宽度
	wh = 0; //屏幕高度

var shopID = 0;
//获取柱状颜色
var getBarStyle = function(color) {
	var obj = {};
	obj['color'] = color;
	obj['barBorderWidth'] = 0;
	obj['label'] = {
		show: true,
		textStyle: {
			fontSize: '10'
		}
	}
	return obj;
};

//页面Load
$(document).ready(function() {
	ww = $(document).width();
	wh = $(document).height();

	shopID = $.getUrlVar('s');

	//初始化
	pageInit();

});

//初始化
function pageInit() {
	$('#pieChart').css({
		"width": ww + "px",
		"height": wh * 0.3 + "px"
	});
	$('#barChart').css({
		"width": ww*0.8 + "px",
		"height": wh * 0.45 + "px"
	});

	//获取数据
	getData();

}

function getData() {
	$.ajax({
		contentType: "application/json",
		url: '/Award/AwardMsg.aspx/GetAwardPicTable',
		type: 'POST',
		dataType: 'json',
		data: JSON.stringify({
			shopID: shopID
		}),
		success: function(d) {
			d = JSON.parse(d.d);
			
			if (d.ErrorState == 1) {
				$('#month').html(d.Month);

				var barChart = echarts.init(document.getElementById('barChart')),
					pieChart = echarts.init(document.getElementById('pieChart'));
				var pieData = [],
					barData = [];

				//饼图数据
				for (var i = 0; i < d.listAwardPicTotalMonthDetail.length; i++) {
					var p = {};
					p.value = parseInt(d.listAwardPicTotalMonthDetail[i].MonthAwardCount);
					p.name = d.listAwardPicTotalMonthDetail[i].MonthAwardName + ": " + d.listAwardPicTotalMonthDetail[i].MonthAwardCount;
					pieData.push(p);
				}

				//饼图
				var pieOption = {
					animation: false,
					series: [{
						type: 'pie',
						radius: '55%',
						center: ['50%', '60%'],
						itemStyle: {
							normal: {
								label: {
									show: true,
									rotate: true,
									//position: 'inner',
									//distance: 0.1,
									textStyle: {
										fontSize: '10',
										color: '#666'
									}
								},
								labelLine: {
									show: true,
									length: 1
								}
							}
						},
						data: pieData
					}]
				};
				pieChart.setOption(pieOption);
				
				//--
				var legendData = [d.OrderMoneyTotalName, d.OrderCountName],
					dateData = [],
					seriesData0 = [],
					seriesData1 = [];
				//柱状图数据
				for (var i = 0; i < d.listAwardPicTotalDayDetail.length; i++) {
					dateData[i] = d.listAwardPicTotalDayDetail[i].Day;
					seriesData0[i] = d.listAwardPicTotalDayDetail[i].OrderMoneyTotal;
					seriesData1[i] = d.listAwardPicTotalDayDetail[i].OrderCount;
				}

				//柱状图
				var barOption = {
					animation: false,
					//表格位置和大小
					grid: {
						borderWidth: 0,
						x: '10%',
						y: '10%',
						width: '80%'
					},
					//图例
					legend: {
						selectedMode: false,
						x: 'center',
						y: 'bottom',
						data: legendData
					},
					xAxis: [{
						type: 'category',
						data: dateData,
						splitLine: {
							show: false,
							//						//坐标系背景线条样式
							//						lineStyle: {
							//							color: ['#f00'],
							//							width: 1,
							//							type: 'solid'
							//						}
						}
					}],
					yAxis: [{
						type: 'value',
						splitLine: {
							show: false
						},
						//					坐标系背景色
						//					splitArea : {show : true}
					}],
					series: [{
						name: legendData[0],
						type: 'bar',
						barWidth: 13,
						barCategoryGap: '60%', //柱间距离
						itemStyle: {
							normal: getBarStyle('#4e87b4')
						},
						data: seriesData0
					}, {
						name: legendData[1],
						type: 'bar',
						barWidth: 13,
						itemStyle: {
							normal: getBarStyle('#ffcc57')
						},
						data: seriesData1
					}]
				}
				barChart.setOption(barOption);
				setTimeout(function(){
					$('#loading').addClass('hide');
					$('.chart').removeClass('hide');
					
					$('#viewDetail').on('touchend', function(){
						window.location.href = 'statistics.html?s='+shopID;
					});
					
				},0);
			} else {
				showtips('系统异常');
				$('#loading').addClass('hide');
			}
		},
		error: function(msg) {
			showtips('系统异常');
			$('#loading').addClass('hide');
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