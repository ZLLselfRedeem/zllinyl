(function(mod){ define([], mod);})(function(){
	var date = {};
	date.format = function( DateObject, type ){
		
		if ( Object.prototype.toString.call(DateObject).split(" ")[1].toLowerCase().replace("]", "") !== "date" ){
			DateObject = new Date(DateObject);
		}
		
		var date = DateObject,
			year = (date.getFullYear()).toString(),
			_month = date.getMonth(),
			month = (_month + 1).toString(),
			day = (date.getDate()).toString(),
			hour = (date.getHours()).toString(),
			miniter = (date.getMinutes()).toString(),
			second = (date.getSeconds()).toString(),
			_day, _year;
			
		var dateArray = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];	
		
		month = month.length === 1 ? "0" + month : month;
		_day = day;
		day = day.length === 1 ? "0" + day : day;
		hour = hour.length === 1 ? "0" + hour : hour;
		miniter = miniter.length === 1 ? "0" + miniter : miniter;
		second = second.length === 1 ? "0" + second : second;
			
		return type.replace(/y/g, year)
				.replace(/m/g, month)
				.replace(/d/g, day)
				.replace(/h/g, hour)
				.replace(/i/g, miniter)
				.replace(/s/g, second)
				.replace(/D/g, _day)
				.replace(/M/g, dateArray[_month]);
				
	};
	
	var meal = new Class(function(section, next){
		this.section = section;
		this.mealList();
		this.close();
		this.doOrder();
		next();
	});
	
	meal.add('mealList', function(){
		var that = this;
		MEAL.ajax('mealList', {
			mobile: MEAL.req.mobile,
			cookie: MEAL.req.cookie,
			mealId: this.section.mealId
		}).then(function(xhr){
			that.insertData(xhr.mealList);
			that.mealSchedule(xhr.mealSchedule);
		})['catch'](function(e){
			window.openview('chklogin.html').then(function(){
				$('#errortit').html('出错啦');
				$('#errormsg').html('服务君跪了');
			});
		});
	});
	
	meal.add('close', function(){
		$(this.section).find('.backBtn').on('click', function(){
			window.closeview(-1);
		});
	});
	
	meal.add('insertData', function(data){
		var isShowOldPrice = true;
		if(data.price === data.originalPrice){ isShowOldPrice = false;}
		
		$('#detail-1').html(data.shopName);
		$('#detail-2').html('电话：' + data.contactPhone);
		$('#detail-3').html('地址：' + data.shopAddress);
		$('#detail-4').attr('src', data.imageURL);
		$('#detail-5').html(data.menu+'<p style="font-size:11px; color:#666; margin-top: 10px; margin-bottom:3px">图片仅供参考，食物以店内为准。</p>');
		$('#detail-6').html('￥' + data.price);
		
		if(isShowOldPrice){
			$('#detail-7').html('￥' + data.originalPrice);
		}
		else{
			$('#detail-7').html("");
		}
		
		$('#detail-8').html(data.suggestion);
	});
	
	meal.add('mealSchedule', function(data){
		this.mealScheduleData = data;
		var that = this;
		// 绑定自定义事件
		$(this.section).on('install', function(){
			$('.theDate').empty();
			var k = {}, h = [];
			that.mealScheduleData.forEach(function(o){
				var strp = new Date(o.DinnerTime.replace(/\-/g, '/'));
				var month = Number(date.format(strp, 'm'));
				var day = Number(date.format(strp, 'd'));
				var s = [month, day].join('-');
				
				if ( h.indexOf(s) === -1 ){
					h.push(s);
				}
				
				if ( !k[s] ){
					k[s] = {};
				};
				k[s][o.DinnerType + ''] = o;
				k[s].lunarCalendar = o.lunarCalendar;
				k[s].isSoldOut = o.isSoldOut;
				//k[s].IsNone = o.IsActive;
				if ( o.IsActive != 0 ){
					k[s].IsNone = true;
				}
			});
			
			h = h.sort(function(a, b){
				var _a = a.split('-');
				var _b = b.split('-');
				
				var c1 = Number(_a[0]) * 30 + Number(_a[1]);
				var c2 = Number(_b[0]) * 30 + Number(_b[1]);
				
				return c1 - c2;
			});
			
			// 输入日期
			h.forEach(function(m){
				if ( k[m].isSoldOut || !k[m].IsNone ){
					$('.theDate').append('<li class="btn btn-default overed" data-mark="' + m + '" >' + (m.split('-').join('月') + '日') + ' ' + k[m].lunarCalendar + '<div></div></li>');
					/*$('.theDate').append('<li class="btn btn-default overed" data-mark="' + m + '" disabled>已抢完</li>');*/
				}else{
					$('.theDate').append('<li class="btn btn-default" data-mark="' + m + '">' + (m.split('-').join('月') + '日') + ' ' + k[m].lunarCalendar + '</li>');
				}
			});
			
			// 绑定日期事件
			$('.theDate li').on('click', function(){
				if ( $(this).hasClass('selected') ){
					return;
				}
				$('.theDate li').removeClass('selected');
				$(this).addClass('selected');
				$('.dinnerTime').empty();
				var mark = $(this).data('mark');
				var n = k[mark];
				delete n['lunarCalendar'];
				delete n['isSoldOut'];
				delete n['IsNone'];
				
				// 输出订餐类型
				for ( var x in n ){
					if ( n[x].isSoldOut || n[x].IsActive === 0 ){
						$('.dinnerTime').append('<li class="btn btn-default overed" data-mark="' + x + '" data-mid="' + n[x].MealScheduleID + '">' + (Number(x) === 1 ? '中餐' : '晚餐') + '<div></div></li>');
					}else{
						$('.dinnerTime').append('<li class="btn btn-default" data-mark="' + x + '" data-mid="' + n[x].MealScheduleID + '">' + (Number(x) === 1 ? '中餐' : '晚餐') + '<div></div></li>');
					}
				}
				
				// 绑定订餐事件
				$('.dinnerTime li').on('click', function(){
					$('.dinnerTime li').removeClass('selected');
					$(this).addClass('selected');
					var mark = $(this).data('mark');
					var d = n[mark];
					if ( d.IsActive === 0 ){
						reduce = 0;
						$(this).addClass('overed').get(0).disabled = true;
						document.getElementById('doOrder').disabled = true;
						document.getElementById('doOrder').innerHTML = '卖光了';
					}else{
						var reduce = d.TotalCount - d.SoldCount;
						if ( reduce === 0 ){
							$(this).addClass('overed').get(0).disabled = true;
							document.getElementById('doOrder').disabled = true;
							document.getElementById('doOrder').innerHTML = '卖光了';
						}else{
							$(this).removeClass('overed').get(0).disabled = false;
							document.getElementById('doOrder').disabled = false;
							document.getElementById('doOrder').innerHTML = '马上下单';
						};	
					}
					$('#reduce').html(reduce === 0 ? '0桌' : '还剩' + reduce + '桌' );
				});
				
				setTimeout(function(){
					var v = $('.dinnerTime li:not(.overed)');
					if ( v.size() === 0 ){
						$('.dinnerTime li:first').trigger('click');
					}else{
						v = v.eq(0);
						if ( v && v.attr('disabled') != 'disabled' ){
							v.trigger('click');
							setTimeout(function(){
								var first = $('.dinnerTime li:not(.overed)').eq(0);
								if ( first && first.size() > 0 && first.attr('disabled') != 'disabled' ){
									first.trigger('click');
								}else{
									$('#reduce').html('0桌');
									document.getElementById('doOrder').disabled = true;
									document.getElementById('doOrder').innerHTML = '卖光了';
								}
							});
						}else{
							$('#reduce').html('0桌');
							document.getElementById('doOrder').disabled = true;
							document.getElementById('doOrder').innerHTML = '卖光了';
						}
					}
				});
			});
		})
		
		// 触发自定义是事件
		.trigger('install');
		
		// 数据初始化
		var cf = $('.theDate li:not(.overed)');
		if ( cf.size() === 0 ){
			$('.theDate li:first').trigger('click');
		}else{
			cf.eq(0).trigger('click');
		}
	});
	
	meal.add('doOrder', function(){
		$('#doOrder').on('click', function(){
			if ( window.doing ) return;
			var id = $('.dinnerTime li.selected').data('mid');
			if ( id && !isNaN(id) ){
				window.doing = true;
				MEAL.ajax('order', {
					mobile: MEAL.req.mobile,
					cookie: MEAL.req.cookie,
					mealScheduleId: id
				}).then(function(xhr){
					window.doing = false;
					if ( xhr.error == 0 ){
						window.openview('orderSuccess.html', 'compiles/error.js', { time: xhr.validPayTime });
					}else{

						//$('.orderFail #myModalLabel').html('出错啦');
						$('.orderFail #orderFailTxt').html('哎呀~晚了一步，桌子被别人抢走了...等一会，再来看看吧~<br /> ');
						$('.orderFail').modal('show');
						
					}
				})['catch'](function(){
					window.doing = false;
					MEAL.error('出错啦', '服务君跪了');
				})
			}
		});
	});
	
	return meal;
});