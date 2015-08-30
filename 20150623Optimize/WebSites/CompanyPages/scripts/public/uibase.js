﻿(function(){ 
	if(!window['UIBase']){ 
		var base = function(){
			EventBase.call( this );
			var self = this;
			this.shopID = null;
			this.companyID = null; 

			this.addURIParam = function(url,name,value){ 
				url += (url.indexOf("?")==-1?"?":"&");
				url += encodeURIComponent(name)+"="+encodeURIComponent(value);
				return url;
			};
			this.getQueryStringArgs = function(){ 
				var qs = (location.search.length>0?location.search.substring(1):"");
				var args ={};
				//处理查询字符串
				var items = qs.split("&"),
					item = null,
					name = null,
					value = null;
				for(var i=0;i<items.length;i++){ 
					item = items[i].split("=");
					name = decodeURIComponent(item[0]);
					value = decodeURIComponent(item[1]);
					args[name]=value;
				}
				return args;
			};
			this.getNyQueryStringArgs = function(arg){ 
				var index = arg.indexOf('?');
				var qs = ((index>-1)?arg.substring(index+1):"");
				var args ={};
				//处理内页传值
				var items = qs.split("&"),
					item = null,
					name = null,
					value = null;
				for(var i=0;i<items.length;i++){ 
					item = items[i].split("=");
					name = decodeURIComponent(item[0]);
					value = decodeURIComponent(item[1]);
					args[name]=value;
				}
				return args;
			};
			
			this.loader = {
				js: function(a, b) {
					
					b = cQuery.extend({
						type: "text/javascript",
						charset: cQuery.config("charset"),
						async: !1,
						group: "",
						onload: cQuery.COMMON_DONOTHING,
						onerror: cQuery.COMMON_DONOTHING
					}, b || {});
					
					var c = document.createElement("script"),
						d = b.onload;
					b.onload = function() {
							d.apply(c, arguments);
							c.onload = c.onreadystatechange = cQuery.COMMON_DONOTHING
						};
					cQuery.extend(c, b);
					c.onreadystatechange = function() {
							switch (c.readyState) {
							case "loaded":
							case "complete":
								b.onload.apply(c, arguments)
							}
						};
					c.src = a;
					document.getElementsByTagName('head')[0].appendChild(c); 
					return c
				}
			}
		};
	};
	window['UIBase'] = new base();
	
	var basePath = 'scripts/controller/';
	var searchFile = { 
	    preOrderShopConfirmed: basePath + "confirmed.js?v=20150729",
		preOrderConfirmedDetail: basePath + "confirmedDetail.js?v=20150729",
		
		preOrderShopVerified: basePath + "verified.js?v=20150729",
		preOrderVerifiedDetail: basePath + "verifiedDetail.js?v=20150729",
		
		currentSellOff: basePath+"currentSellOff.js",
		
		dishList:basePath+"dishList.js",
		dishManage:basePath+'dishManage.js',
		dishMix:basePath+'dishMix.js',
		dishManageMutil:basePath+'dishManageMutil.js',
		
		couponPreOrderDetail:basePath+"couponDetail.js",
		couponUsedDetail:basePath+"couponUsedDetail.js",
		couponsActivitiesManage:basePath+"couponActivities.js",
		
		accountDetails:basePath+"account.js",
		accountPreOrderDetail:basePath+"accountDetail.js?v=20150729",
		accountPayDetail: basePath + "accountPayDetail.js?v=20150729",
		accountTotal: basePath + "accountTotal.js?v=20150729",
		
		configurationPrinter:basePath+"configurationPrinter.js",
		configurationEmployees:basePath+"configurationEmployees.js",
		configurationPassword:basePath+"configurationPassword.js",
		
		companylist:basePath+"companylist.js?v="+Math.random(),
		companyaccountlist:basePath+"companyaccountlist.js",
		companymenulist:basePath+"companymenulist.js",
		companyManage:basePath+"companyManage.js",
		shoplist:basePath+"shoplist.js",
		shopsundrylist:basePath+"shopsundrylist.js",
		shopdiscountlist:basePath+"shopdiscountlist.js",
		shophandle: basePath + "shophandle.js",

        dishIngredientsSellOff:basePath+"dishIngredientsSellOff.js",
		increment:basePath+"increment.js",
		incrementEdit:basePath+"incrementEdit.js",
		incrementManage:basePath+"incrementManage.js"
	};
	
	if(!window['VA']){
		var VA = VA || {};
		VA.Util = {};
		VA.Class = {};
		VA.Singleton = {};
		VA.initPage = {};//
		VA.renderPage = {};
		VA.argPage = {};
		VA.Util.session = 0;
		VA.Util.sessionID = '';
		VA.Util.picStatus = '1';
		VA.Util.loadJS = function(a, b) {
			var res = UIBase.loader.js(a, {
				type: "text/javascript",
				async: !0,
				charset: "utf-8",
				onload: b
			});
		};
		
		VA.Class.Page = function(curPage) {
			this.iniPage = curPage;
			this.intPageJS = searchFile[curPage];
		};
		
		VA.Class.Page.prototype.disInt = function() {
			var that = this;
			if(VA.Singleton[that.iniPage]){ 
				VA.initPage[that.iniPage]();
				return;
			}
			VA.Singleton.intPageJSLoad || VA.Util.loadJS(that.intPageJS, function() {
				VA.Singleton[that.iniPage] = !0;
				VA.initPage[that.iniPage]();
			});
		};
		
		VA.Class.Page.prototype.init = function() {
			var that = this;
			that.disInt();
		};
		window['VA'] = VA;
	}
	
	menu.init();
	popup.panel();
})();

 
function UICalendar(){
	//
	this.month = this.getMonthRange();
	this.week = this.getWeekRange();
	this.today = this.getNowDay();
};
UICalendar.prototype={
	getNowDay:function(){
		var now = new Date(),
			y = now.getFullYear(),
			m = now.getMonth()+1,
			d = now.getDate();
		var today = y+"-"+m+"-"+d;
		return today;
	},
	getDurationDay:function(duration){
		var now = new Date(),
			y = now.getFullYear(),
			m = now.getMonth()+1,
			d = now.getDate()+duration;
		var durationDay = y+"-"+m+"-"+d;
		return durationDay;
	},
	getMonthRange: function () {
		var nowDay = new Date();
		var y = nowDay.getFullYear(),
			m = nowDay.getMonth(),
			d = nowDay.getDate(); //
		var curMonthDays = new Date(y, (m + 1), 0).getDate();
		var month = new Object();
		month.startDay = new Date(y, m, 1);
		month.endDay = new Date(y, m, curMonthDays);
		return month;
	},
	getWeekRange: function () {
		var nowDay = new Date();
		var y = nowDay.getFullYear(),
			m = nowDay.getMonth(),
			d = nowDay.getDate(); //
		var w = nowDay.getDay();
		var soonDays = 6 - w,
			beenDays = w;
		var week = new Object();
		var startDay = new Date(y, m, d - beenDays);
		var y1 = startDay.getFullYear(),
			m1 = startDay.getMonth()+1,
			d1 = startDay.getDate();
		d1 = d1<=9?'0'+d1:d1;
		week.startDay = y1+'-'+m1+'-'+d1;
		var endDay = new Date(y, m, d + soonDays);
		var y2 = endDay.getFullYear(),
			m2 = endDay.getMonth()+1,
			d2 = endDay.getDate();
		d2 = d2<=9?'0'+d2:d2;
		week.endDay = y2+'-'+m2+'-'+d2;
		
		return week;
	}
};
var VAcalendar = new UICalendar();


