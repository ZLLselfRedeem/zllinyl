(function(mod){ define(['/AppPages/BigRedEnvelope/modules/scrollto'], mod);})(function(){	
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
		var that = this;
		this.section = section;
		this.mealActivity();
		setTimeout(function(){
			that.ScrollDown();
		}, 500);
		this.JumpDetailPage();
		$(this.section).find('.toTop').on('click', function(){
			$(that.section).find('.section-box').scrollTo('#orders', 300);
			$('.toTop').hide();
		});
		this.refresh();
		next();
	});
	
	meal.add('refresh', function(){
		var that = this;
		$(this.section).on('refresh', function(){
			window.doing = true;
			MEAL.ajax('mealActivity', {
				cityId: MEAL.req.cityId,
				tagId: that.section.tagId,
				mobile: MEAL.req.mobile,
				pageIndex: that.section.page,
				pageSize: 2,
				cookie: MEAL.req.cookie
			}).then(function(xhr){
				window.doing = false;
				that.orderList(xhr.customerMealOrder);
			})['catch'](function(){
				window.doing = false;
				MEAL.error('出错啦', '服务君跪了');
			});
		});
	});
	
	meal.add('JumpDetailPage', function(){
		$(this.section).on('click', '.mingo', function(){
			var mealId = $(this).data('mealid'),
				shopId = $(this).data('shopid');
			
			window.openview('storeMenu.html', 'compiles/detail.js', { mealId: mealId, shopId: shopId });
		});
		$(this.section).on('click', '.ddddd', function(){
			var p = $(this).parents('.storeMenuInfo:first').find('.mingo');
			var mealId = p.data('mealid'),
				shopId = p.data('shopid');
			
			window.openview('storeMenu.html', 'compiles/detail.js', { mealId: mealId, shopId: shopId });
		});
	});
	
	meal.add('ScrollDown', function(){
		var that = this;
		$(that.section).find('.section-box').on('scroll', function(){
			var box_height = $(this).outerHeight();
			var window_height = $(window).height();
			var scroll_Height = $(this).scrollTop();
			var deep = 50;
			if ( window_height + scroll_Height + deep >= box_height && !window.doing && that.section.isMore ){
				that.mealActivity(that.section.tagId, that.section.page + 1);
			};

			if ( that.offset ){
				var el = $('.activityArea');
				if ( scroll_Height >= that.offset ){
					if ( !document.getElementById('vtag') ){
						var x = $('.activityArea').clone().appendTo(that.section);
						el.css('opacity', '0');
						x.css('position', 'fixed');
						x.css({
							top: '0',
							left: '0',
							width: '100%',
							zIndex: 9999,
							'background-color': 'rgba(255,255,255,.9)',
						}).attr('id', 'vtag');
						x.find('li').on('click', function(){
							var i = $.inArray(this, x.find('li').toArray());
							$('.activityArea').find('li').eq(i).trigger('click');
						});
					}
				}else{
					if ( document.getElementById('vtag') ){
						$('#vtag').remove();
						$('.activityArea').css('opacity', '1');
					}
				}
			}
			if(scroll_Height > window_height){
				$('.toTop').show();
			}
			else{
				$('.toTop').hide();
			}
		});
	});
	
	meal.add('mealActivity', function(tagId, page, callback, empty){
		
		if ( window.doing ) return;
		
		if ( !page ){ page = 1; };
		if ( !tagId ){ tagId = 0; };
		this.section.tagId = tagId;
		this.section.page = page;
		
		var that = this;
		
		window.doing = true;
		$('#stores').append('<p class="loading text-center" style="font-size:12px; display:block; width:100%;">正在加载第' + this.section.page + '页数据...</p>');
		MEAL.ajax('mealActivity', {
			cityId: MEAL.req.cityId,
			tagId: this.section.tagId,
			mobile: MEAL.req.mobile,
			pageIndex: this.section.page,
			pageSize: 2,
			cookie: MEAL.req.cookie
		}).then(function(xhr){
			window.doing = false;
			that.section.isMore = xhr.isMore;
			if ( xhr.activityRule ){ $('#rule').html(xhr.activityRuleMini); };
			if ( xhr.activityRule ){ $('#activityRules').html(xhr.activityRule); };
			if ( !window.installedTags ){
				that.addTags(xhr.shopTag);
			}
			that.orderList(xhr.customerMealOrder);
			if ( empty ){$('#stores').empty();}
			that.mealList(xhr.mealShopList);
			/*if ( that.section.page > 1 ){
				$(that.section).find('.toTop').show();
			}else{
				$(that.section).find('.toTop').hide();
			}*/
			$('#stores').find('p.loading').remove();
			typeof callback === 'function' && callback.call(that);
		})['catch'](function(){
			window.doing = false;
			MEAL.error('出错啦', '服务君跪了');
		});
		
	});
	
	meal.add('addTags', function(data){
		var that = this;
		$('#tags').empty();
		if ( data ){
			$('.activityArea').show();
			if ( data.length > 1 ){
				$('#tags')
				.append('<li data-Flag="0" data-ShopCount="0" data-TagId="0" class="areaActive">全部</li>');
			};
			data.forEach(function(o){
				$('#tags')
				.append('<li data-Flag="' + o.Flag + '" data-ShopCount="' + o.ShopCount + '" data-TagId="' + o.TagId + '">' + o.Name + '</li>');
			});
			$('#tags').find('li').on('click', function(){
				var tid = $(this).data('tagid');
				if ( !isNaN(tid) ) {
					tid = Number(tid);
					that.mealActivity(tid, 1, function(){
						$(that.section).find('.section-box').scrollTo('#dtag', 300);
					}, true);
					$('#tags li, #vtag li').removeClass('areaActive');
					var i = $.inArray(this, $('#tags li').toArray());
					$(this).addClass('areaActive');
					try{
						$('#vtag li').eq(i).addClass('areaActive');
					}catch(e){}
				}
			});
			window.installedTags = true;
		}else{
			$('.activityArea').hide();
		}
	});
	
	meal.add('mealList', function(data){
		if ( data && data.length > 0 ){
			data.forEach(function(o){
				
				var h = '';
				h +=	'<div class="storeInfo">';
				h +=		'<div class="storeName text-center">' + o.shopName + '</div>';
				if ( o.mealList && o.mealList.length > 0 ){
				h +=			'<div class="storeMenus row">';
					
					o.mealList.forEach(function(z){
						var isShowOldPrice = true;
						if(z.price === z.originalPrice){ isShowOldPrice = false;}
					
				h +=				'<div class="storeMenuInfo">';
				h +=					'<div class="storeMenuPic">';
				h +=						'<img src="' + z.imageURL + '" class="ddddd" />';
				if ( z.isSoldOut ){
				h +=						'<img src="img/over.png" class="coverOver ddddd" />';
				}
				h +=					'</div>';
				h +=					'<span class="nowPrice">￥' + z.price + '</span>';
				
						if(isShowOldPrice){
				h +=					'<del><span class="oldPrice">￥' + z.originalPrice + '</span></del>';
						}
				
				h +=					'<span class="peopleNum">' + z.suggestion + '</span>';
				h +=					'<button data-mealId="' + z.mealId + '" data-shopId="' + z.shopId + '" class="btn btn-' + (z.isSoldOut ? 'default mingo' : 'danger mingo godetail') + '" ' + (z.isSoldOut ? 'disabled' : '') + '>去预订</button>';
				h +=				'</div>';
					});
				h +=			'</div>';
				}
				h +=	'</div>';
				$('#stores').append(h);
			});
		}
	});
	
	meal.add('orderList', function(data){
		if ( data && data.length > 0 ){
			var m = '';
			data.forEach(function(o){
				var status = "";
				var txt = "";
				var btntxt = "查看订单";
				var d = date.format(new Date(o.dinnerTime.replace(/\-/g, '/')), 'y-m-d');
				var dinnerType = "";
				
				if(o.DinnerType == 1) { dinnerType = '中餐'; }
				else if(o.DinnerType == 2) { dinnerType = '晚餐';}
				
				if(o.status == 1)		{ status = '未付款'; txt = '请在' + o.validPayTime + '前支付订单，时间过期，订单有可能被别人抢走哦~'; btntxt = "马上付款"; }
				else if(o.status == 2)	{ status = '待确认'; txt = '您预订的' + o.shopName  + ' - ' + d + dinnerType + ' - ' + o.price + '元套餐已成功付款，'+ o.shopName +'将在24小时内与您联系确认订单。';}
				else if(o.status == 3)	{ status = '已确认'; txt = '您预订的' + o.shopName  + ' - ' + d + dinnerType + ' - ' + o.price + '元套餐订单已确认。';}
				else if(o.status == 4)	{ status = '已退款'; txt = '您预订的' + o.shopName + ' - ' + d + dinnerType + ' - ' + o.price + '元套餐订单已退款。';}
				else if(o.status == 5)	{ status = '超时未付款'; }
				else if(o.status == 6)	{ status = '退款中';  txt = '您预订的' + o.shopName + ' - ' + d + dinnerType + ' - ' + o.price + '元套餐退款中，金额原路返回需3-5个工作日。';}
				
				var h = '';
				h = '<div class="order showOrder">';
				h +=	'<div class="orderStatus">' + status + '</div>';
				h +=	'<div class="orderInfo text-center">';
				h +=		'<p class="text-left">' + txt + '</p>';
				h +=		'<button class="btn btn-default btn-xs showOrder">'+btntxt+'</button>';
				h +=	'</div>';
				h += '</div>';
				m += h;
			});
			
			$('#orders').html(m);
			
			$('.showOrder').on('click', function(){
				MEAL.open(16, -999);
			});
		}
		
		var el = $('.activityArea');
		if ( !el.data('installed') ){
			var img = $(this.section).find('img.activityPic').get(0);
			var src = img.src;
			var that = this;
			var Images = new Image();
			Images.onload = Images.onerror = function(){
				that.offset = el.offset().top;
			};
			Images.src = src;
			el.data('installed', true);
		}
	});
	
	return meal;
});