﻿/*
 * object @栏目导航
 * object @顶部导航
 * 2014-04-14
 * author
 */

// 栏目导航

var menu = {
	init : function () {
		YUI().use('cookie', 'node-base', 'node-style', 'node-event-delegate', 'io-base', 'json-parse', 'node-load', function (Y) {
			menu.container = Y.one('#contentContainer');
			menu.pageHistory(VAcalendar.week.startDay, VAcalendar.today, 1, 10, 2, '', 0, {
				"amountMin" : -2147483648,
				"amountMax" : 2147483647,
				"type" : 0,
				"mobilePhoneNumber" : ""
			});
			Y.io('ajax/doSybSystem.ashx', {
				method : 'post',
				data : 'm=syb_switched_systems&status=0',
				headers : {
					'Content-Type' : 'application/x-www-form-urlencoded; charset=utf-8'
				},
				on : {
					success : function (id, rsp) {
						var res = Y.JSON.parse(rsp.responseText);
						if (res == '-1000') {
							VA.Singleton.popup.timeout();
							return;
						}
						menu.createMenu(res.status);
					},
					failure : function (id, rsp) {
						Y.log(rsp.status);
					}
				}
			});

		});
	},
	pageHistory : function (start, end, pageindex, pagesize, status, inputtextstr, dishtypeid, accountData) {
		this.startDay = start;
		this.endDay = end;
		this.pageIndex = pageindex;
		this.pageSize = pagesize;
		this.statusController = status;

		this.inputTextStr = inputtextstr;
		this.dishTypeId = dishtypeid;

		this.accountData = accountData;
	},
	navIcon : function () {
		var m = document.getElementById('mainNav');
		var secChild = m.getElementsByTagName('li')[1]
			firChild = m.getElementsByTagName('li')[0];
		if (secChild.className.indexOf('cur') > -1) {
			firChild.className = 'icon_corner_hover';
		} else {
			firChild.className = 'icon_corner';
		};
	},
	getType : function (href) {
		var index = href.lastIndexOf('/') + 1,
		aspxIndex = href.lastIndexOf('?');
		aspxIndex = (aspxIndex > -1) ? aspxIndex - 5 : aspxIndex - 4; //.aspx?
		return href.slice(index, aspxIndex);
	},
	createMenu : function (p) {
		YUI().use('cookie', 'node-base', 'node-style', 'node-event-delegate', 'io-base', 'json-parse', 'node-load', function (Y) {
			var m = Y.one('#mainNav'),
			content = Y.one('#mainNav .content');
			var dataSuccess = function (u) {

				if (!u) {
					var okHandler = function () {};
					VA.Singleton.popup.panel.set('headerContent', '提示信息');
					VA.Singleton.popup.panel.set('bodyContent', '您暂时没有相关权限，请联系管理员');
					VA.Singleton.popup.panel.set('buttons', [VA.Singleton.popup.get('okButton')]);
					VA.Singleton.popup.set('ok', okHandler);
					VA.Singleton.popup.showPanel();
					return;
				}
				var str = u + '|login'; //通用功能栏目
				// 存取权限变量
				var userAuthorityArray = str.toLowerCase().split('|'),
				userAuthority = {};

				for (var i = 0, len = userAuthorityArray.length; i < len; i++) {
					userAuthority[userAuthorityArray[i]] = true;
				}
				menu.person.userAuthority = userAuthority;

				var configurations = (userAuthority.issupportshopmanagepage) ? "configurationEmployees" : "configurationPassword";
				var dishIngredientsSelloff = "dishIngredientsSellOff";
				var reg = new RegExp(str, 'i'),
				html = '<li class="icon_corner"><i></i></li>';
				var page = [
					// customer
					{
						name : 'preOrderShopConfirmed',
						text : '入座管理',
						className : 'item1'
					}, {
						name : 'preOrderShopVerified',
						text : '财务对账',
						className : 'item2'
					}, {
						name : 'currentSellOff',
						text : '沽清管理',
						className : 'item3'
					}, {
						name : 'dishList',
						text : '菜品管理',
						className : 'item4'
					}, {
						name : 'couponsActivitiesManage',
						text : '活动管理',
						className : 'item5'
					}, {
						name : 'accountTotal',
						text : '帐户明细',
						className : 'item6'
					},
					// staff
					{
						name : 'companylist',
						text : '公司管理',
						className : 'item9'
					}, {
						name : 'shoplist',
						text : '店铺管理',
						className : 'item10'
					}, {
						name : 'shophandle',
						text : '店铺审核',
						className : 'item11'
					}, {
						name : configurations,
						text : '功能设置',
						className : 'item7'
					}, {
						name : 'increment',
						text : '增值管理',
						className : 'item12'
					}, {
						name : 'login',
						text : '退出系统',
						className : 'item8'
					}
				];
				for (var i = 0; i < page.length; i++) {
					if (reg.test(page[i].name)) {
						html += '<li><a class="' + page[i].className + '" href="' + page[i].name + '.aspx">' + page[i].text + '</a></li>';
					}
					if (page[i].name == "configurationEmployees") {
						html += '<li><a class="' + page[i].className + '" href="' + page[i].name + '.aspx">' + page[i].text + '</a></li>';
					}
				}
				content.empty();
				content.append(html);
				//初始导航及加载内容
				var type = Y.Cookie.get('type'),
				m = document.getElementById('mainNav'),
				itemes = m.getElementsByTagName('a');
				var cur = '',
				url = '';
				if (type == '0' || !type || (type == 'login') || !reg.test(type)) {
					var a = itemes[0];
					a.parentNode.className = 'cur';
					url = a.getAttribute('href');
					cur = menu.getType(a.getAttribute('href'));
					Y.Cookie.set('type', cur); //设置,刷新页面情况

				} else {
					url = type + '.aspx';
					for (var i = 0; i < itemes.length; i++) {
						var temp = menu.getType(itemes[i].getAttribute('href'));
						if (temp == type) {
							cur = temp;
							itemes[i].parentNode.className = 'cur';
						}
					}
				}
				menu.container.load(url, '#page', function () {
					var module = new VA.Class.Page(cur);
					module.init();
					menu.navIcon();
				});

				//切换
				Y.all('#mainNav a').on('click', function (e) {
					e.preventDefault();
					var type = Y.Cookie.get('type'); //读取
					var t = e.target;
					var url = t.getAttribute('href');
					var cur = menu.getType(t.getAttribute('href'));

					if (cur == 'login') {
						var okHandler = function () {
							window.location.href = 'login.aspx';
						};
						var cancelHandler = function () {
							//
						};
						VA.Singleton.popup.panel.set('headerContent', '提示信息');
						VA.Singleton.popup.panel.set('bodyContent', '您确认要退出悠先收银宝系统!');
						VA.Singleton.popup.panel.set('buttons', [VA.Singleton.popup.get('okButton'), VA.Singleton.popup.get('cancelButton')]);
						VA.Singleton.popup.set('ok', okHandler);
						VA.Singleton.popup.set('cancel', cancelHandler);
						VA.Singleton.popup.showPanel();
						return;
					}

					menu.pageHistory(VAcalendar.week.startDay,
						VAcalendar.today,
						1,
						10,
						2,
						'',
						0, {
						"amountMin" : -2147483648,
						"amountMax" : 2147483647,
						"type" : 0,
						"mobilePhoneNumber" : ""
					});

					//if(cur!=type){
					t.ancestor().addClass('cur');
					t.ancestor().siblings().removeClass('cur');

					menu.container.load(url, '#page', function () {
						if (cur == 'login')
							window.location.href = 'login.aspx';
						/*
						 * 单击导航切换时，去掉上一导航项的记录状态（分页、查询）
						 *
						 */
						VA.argPage.qs.pageIndex = 1;
						VA.argPage.qs.statusController = 2;

						var module = new VA.Class.Page(cur);
						module.init();
					});
					//};
					//存储状态，处理1）当前刷新页面情况；2）当导航重复点击加载内容；
					Y.Cookie.set('type', cur);
					menu.navIcon();
				});
			};

			switch (p) {
			case "3":
				p = 0;
				!menu.initQuickNav || (menu.person(true), menu.initQuickNav = false);
			case "1":
				Y.one('#loginInfo').addClass('loginInfoStaff');
				!menu.initQuickNav || menu.person(false);
				break;
			case "2":
				Y.one('#loginInfo').removeClass('loginInfoStaff');
				menu.person(false);
				break;
			default:
				return;
				break;
			}
			Y.io('ajax/doSybSystem.ashx', {
				method : 'post',
				data : 'm=syb_switched_systems&status=' + p,
				headers : {
					'Content-Type' : 'application/x-www-form-urlencoded; charset=utf-8'
				},
				on : {
					success : function (id, rsp) {
						var res = Y.JSON.parse(rsp.responseText);
						if (res == '-1000') {
							VA.Singleton.popup.timeout();
							return;
						}
						dataSuccess(res.userAuthority);
					},
					failure : function (id, rsp) {
						Y.log(rsp.status);
					}
				}
			});
		});
	},
	createTab : function (page) {
		YUI().use('cookie', 'node-base', 'node-style', 'node-event-delegate', 'io-base', 'json-parse', 'node-load', function (Y) {
			Y.io('ajax/doSybWeb.ashx', {
				method : 'POST',
				data : 'm=page_authority_query&pageName=' + page,
				headers : {
					'Content-Type' : 'application/x-www-form-urlencoded; charset=utf-8'
				},
				on : {
					success : function (id, rsp) {
						// 临时解决菜品管理
						var jsonStr = rsp.responseText;

						if (page == "dishList" || page == "dishMix") {
							jsonStr = '{"tab":"dishList,dishMix"}';
						};
						if (page == "accountTotal" || page == "accountDetails") {
							jsonStr = '{"tab":"accountTotal,accountDetails"}';
						};
						if (page == "currentSellOff" || page == "dishIngredientsSellOff") {
							jsonStr = '{"tab":"currentSellOff,dishIngredientsSellOff"}';
						};
						var res = Y.JSON.parse(jsonStr);
						if (res == '-1000') { //登录超时
							VA.Singleton.popup.timeout();
							return;
						}

						var tab = res.tab.split(",");
						var html = "<ul>";
						for (var i = 0, len = tab.length; i < len; i++) {
							if (tab[i] == page) {
								html += '<li class="cur"><a href="' + tab[i] + '.aspx?" class="tab_' + tab[i] + '"></a></li>';
							} else {
								html += '<li><a href="' + tab[i] + '.aspx?" class="tab_' + tab[i] + '"></a></li>';
							}
						}
						html += '</ul>';

						var tabContainer = Y.one('#tab');
						tabContainer.setHTML(html);
						Y.all('#tab a').on('click', menu.tabContent, this);
					},
					failure : function (id, rsp) {
						Y.log(rsp.status);
					}
				}
			});
		});
	},
	tabContent : function (e) {
		e.preventDefault();
		e.stopPropagation();
		menu.pageHistory(VAcalendar.week.startDay,
			VAcalendar.today,
			1,
			10,
			2,
			'',
			0, {
			"amountMin" : -2147483648,
			"amountMax" : 2147483647,
			"type" : 0,
			"mobilePhoneNumber" : ""
		});
		var hrefStr = e.target.getAttribute('href');
		var m = menu.getType(hrefStr),
		aspx = m + ".aspx";
		VA.argPage.qs = UIBase.getNyQueryStringArgs(hrefStr);

		menu.tab(aspx, m);
	},
	tab : function (aspx, m) {
		YUI().use('cookie', 'node-base', 'node-style', 'node-event-delegate', 'io-base', 'json-parse', 'node-load', function (Y) {
			menu.container.load(aspx, '#page', function () {
				var module = new VA.Class.Page(m);
				module.init();
			});

		});
	},
	nextPage : function (aspx, m) { //单击链接跳转 UIModel
		YUI().use('cookie', 'node-base', 'node-style', 'node-event-delegate', 'io-base', 'json-parse', 'node-load', function (Y) {
			menu.pageHistory(VA.renderPage.get('startDay'),
				VA.renderPage.get('endDay'),
				VA.renderPage.get('pageIndex'),
				VA.renderPage.get('pageSize'),
				VA.renderPage.get('statusController'),
				VA.renderPage.get('inputTextStr'),
				VA.renderPage.get('dishTypeId'),
				VA.renderPage.get('account'));
			menu.container.load(aspx, '#page', function () {
				var module = new VA.Class.Page(m);
				module.init();
			});
		});
	},
	prevPage : function (aspx, m) { //单击链接返回跳转 UINy  UIDishmanage companyManage
		YUI().use('cookie', 'node-base', 'node-style', 'node-event-delegate', 'io-base', 'json-parse', 'node-load', function (Y) {
			menu.container.load(aspx, '#page', function () {
				var module = new VA.Class.Page(m);
				module.init();
			});

		});
	}
};

// 顶部导航
menu.person = function (staff) {
	YUI().use('login', 'loginModule', 'cookie', 'io-base', 'node-event-delegate', 'event-hover', 'transition', function (Y) {
		VA.argPage.loginId = new Y.Login();
		VA.argPage.loginModule = new Y.LoginModule();
		VA.argPage.qs = new Object(); //初始化 confirmed 时传入 queryString的空对象；

		var changeTop = {
			com : function (e) {
				e.stopPropagation();
				var comID = e.target.get('id'); ;
				var self = this;
				var dataObj = new Object();
				if (comID.indexOf('yui') == -1) {
					VA.argPage.loginModule.set('companyId', comID); //初始及来回切换导航
					if ("set" in VA.renderPage) {
						VA.renderPage.set('companyId', comID); //syncUI
					}

					Y.io('masterFunction.aspx/ChangeCompanyId', {
						method : 'POST',
						data : '{"companyId":' + comID + '}',
						headers : {
							'Content-Type' : 'application/json; charset=utf-8'
						},
						on : {
							success : function (id, rsp) {},
							failure : function (id, rsp) {
								Y.log(rsp.status);
							}
						}
					});

					//GetShopList id\name
					var shopID = '';
					Y.io('masterFunction.aspx/GetShopJson', {
						method : 'POST',
						data : '{"companyId":"' + comID + '"}',
						headers : {
							'Content-Type' : 'application/json; charset=utf-8'
						},
						on : {
							success : function (id, rsp) {
								var result = Y.JSON.parse(rsp.responseText),
								d = Y.JSON.parse(result.d);
								dataObj.shopList = d.shop;
								shopID = dataObj.shopList[0].shopID;
								//
								var options = '<select name="shop" id="shopSelect" style="display:none;">';
								for (var i = 0; i < dataObj.shopList.length; i++) {
									if (dataObj.shopList[i].shopID != shopID) {
										options += '<option value="' + dataObj.shopList[i].shopID + '" title="' + dataObj.shopList[i].shopName + '">' + dataObj.shopList[i].shopName + '</option>';
									} else {
										options += '<option value="' + dataObj.shopList[i].shopID + '" title="' + dataObj.shopList[i].shopName + '" selected="selected">' + dataObj.shopList[i].shopName + '</option>';
									}
								}
								options += '</select>';

								Y.one('#shop').empty(true);
								Y.one('#shop').setHTML(options);
								var selectModule = new Y.selectClass({
										targetDom : '#shop'
									});

								VA.argPage.loginId.set('id', shopID); //处理当前刷新页面情况,初始
								if ("set" in VA.renderPage) {
									VA.renderPage.set('shopId', shopID); //syncUI
									if (VA.renderPage.hasOwnProperty('CatesSelectPlugin')) {
										VA.renderPage.CatesSelectPlugin.addCates();
									}
								}
							},
							failure : function (id, rsp) {
								Y.log(rsp.status);
							}
						},
						sync : true
					});

					Y.io('masterFunction.aspx/GetShopLogo', {
						method : 'POST',
						data : '{"shopId":' + shopID + '}',
						headers : {
							'Content-Type' : 'application/json; charset=utf-8'
						},
						on : {
							success : function (id, rsp) {
								var result = Y.JSON.parse(rsp.responseText);
								dataObj.logourl = result.d;

								var logourl = Y.one('#logo');
								logourl.set('src', dataObj.logourl);
							},
							failure : function (id, rsp) {
								Y.log(rsp.status);
							}
						}
					});

					Y.io('masterFunction.aspx/ChangeShopId', {
						method : 'POST',
						data : '{"shopId":' + shopID + '}',
						headers : {
							'Content-Type' : 'application/json; charset=utf-8'
						},
						on : {
							success : function (id, rsp) {
								//
							},
							failure : function (id, rsp) {
								Y.log(rsp.status);
							}
						}
					});

				};
			},
			shop : function (e) {
				e.stopPropagation();
				var shopID = e.target.get('id');
				if (shopID.indexOf('yui') == -1) {
					//
					if ("set" in VA.renderPage) {
						VA.argPage.loginId.set('id', shopID); //处理当前刷新页面情况
						VA.renderPage.set('shopId', shopID);
						if (VA.renderPage.hasOwnProperty('CatesSelectPlugin')) {
							VA.renderPage.CatesSelectPlugin.addCates();
						}
					}

					Y.io('masterFunction.aspx/ChangeShopId', {
						method : 'POST',
						data : '{"shopId":' + shopID + '}',
						headers : {
							'Content-Type' : 'application/json; charset=utf-8'
						},
						on : {
							success : function (id, rsp) {
								//
							},
							failure : function (id, rsp) {
								Y.log(rsp.status);
							}
						}
					});

					Y.io('masterFunction.aspx/GetShopLogo', {
						method : 'POST',
						data : '{"shopId":' + shopID + '}',
						headers : {
							'Content-Type' : 'application/json; charset=utf-8'
						},
						on : {
							success : function (id, rsp) {
								var result = Y.JSON.parse(rsp.responseText);
								Y.one('#logo').set('src', result.d);
							},
							failure : function (id, rsp) {
								Y.log(rsp.status);
							}
						}
					});

				}
			}
		};
		Y.one('#companyName').delegate('click', changeTop.com, 'li', this);
		Y.one('#shop').delegate('click', changeTop.shop, 'li', this);

		// 快速导航
		function quickNav() {
			// var w3c = (document.getElementById) ? true : false;
			var agt = navigator.userAgent.toLowerCase();
			var ie = ((agt.indexOf("msie") != -1) && (agt.indexOf("opera") == -1) && (agt.indexOf("omniweb") == -1));
			var mymovey = new Number();
			function IeTrueBody() {
				return (document.compatMode && document.compatMode != "BackCompat") ? document.documentElement : document.body;
			}
			function GetScrollTop() {
				return ie ? IeTrueBody().scrollTop : window.pageYOffset;
			}
			function heartBeats() {
				diffY = GetScrollTop();
				mymovey += Math.floor((diffY - document.getElementById('quick_nav').style.top.replace("px", "") + 210) * 0.1);
				document.getElementById('quick_nav').style.top = mymovey + "px";
			}
			// window.setInterval("heartBeats()", 1);
			window.setInterval(heartBeats, 1);

			var quickNavSprite = Y.one("#quick_nav ul");
			Y.one("#quick_nav").setStyle('display', 'block');
			Y.one("#quick_nav").on('hover', function (e) {
				quickNavSprite.transition({
					right : 0
				});
				quickNavSprite.setStyle('backgroundPosition', "left -34px");
			}, function (e) {
				quickNavSprite.transition({
					right : '-125px'
				});
				quickNavSprite.setStyle('backgroundPosition', "left 0px");
			});
			Y.all('#quick_nav a').on('click', function (e) {
				menu.createMenu(e.currentTarget.getAttribute('rel'));
			});
		}
		if (staff) {
			quickNav();
		}
	});
};
menu.initQuickNav = true;

// history-hash
menu.history = function () {};
