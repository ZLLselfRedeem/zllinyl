﻿/*//
标题：收银宝 UIModule
来源：viewallow UI
日期：2013 / 9 / 19 - 2014 / 2 / 12

//*/

// import uibase.js
// UIBase.shopID

/*
 * 导航信息
 * 模块 [selectPack]
 */
YUI.add('selectPack', function(Y) {
	Y.selectClass = Y.Base.create('selectNS', Y.Base, [], {
		initializer: function() {
			var self = this;
			var shopCount = 0,
				contentDom = this.get('targetDom'),
				node = Y.one(contentDom);
			node.append('<div class="tag_select"></div><ul class="tag_options"></ul>');

			this.set('tagSelect', node.one('.tag_select'));
			this.set('tagOptions', node.one('.tag_options'));
			var tagSelect = this.get('tagSelect'),
				tagOptions = this.get('tagOptions');
			var op = node.all('option')._nodes,
				len = op.length,
				li = '';
			for (var i = 0; i < len; i++) {
				if (!op[i].getAttribute('selected')) {
					li += '<li id="' + op[i].getAttribute('value') + '">' + op[i].getAttribute('title') + '</li>';
				} else {
					li += '<li id="' + op[i].getAttribute('value') + '" class="open_selected">' + op[i].getAttribute('title') + '</li>';
					tagSelect.set('text', op[i].getAttribute('title'));
				}
			};

			tagOptions.setHTML(li);
			tagOptions.on('click', function(e) {
				var t = e.target;
				t.addClass('open_selected');
				t.siblings().removeClass('open_selected');
				tagSelect.set('text', t.get('text'));
				self.hideOptions();
			}, 'li', this);
			//tagOptions.delegate('hover',);

			this.animHide = new Y.Anim({
				node: contentDom + ' .tag_options',
				duration: 0.5,
				to: {
					height: 0,
					opacity: 0
				},
				easing: 'backIn'
			});
			this.animShow = new Y.Anim({
				// to:{height: 37*len,opacity:1  },
				to: {
					height: 38 * len,
					opacity: 1
				},
				duration: 0.5,
				easing: 'backOut'
			});
			this.animShow.set('node', contentDom + ' .tag_options');

			tagSelect.on('click', this.tagHandler, this);
		},
		tagHandler: function(e) {
			var t = e.currentTarget;
			var className = t.getAttribute('class');
			if (className.indexOf("tag_select_open") == -1) {
				this.showOptions();
			} else {
				this.hideOptions();
			}
		},
		hideOptions: function() {
			this.get('tagSelect').removeClass('tag_select_open');
			this.animHide.run();
		},
		showOptions: function() {
			this.get('tagSelect').addClass('tag_select_open');
			this.animShow.run();
		}
	}, { //
		ATTRS: {
			targetDom: {
				value: ''
			},
			tagSelect: {
				value: ''
			},
			tagOptions: {
				value: ''
			}
		}
	});
}, '', {
	requires: ['base-build', 'anim-base', 'anim-easing', 'event-outside', 'node-event-delegate']
});

/*
 * 用户信息
 * 模块 [loginModule]
 */

YUI.add('loginModule', function(Y) {
	Y.LoginModule = Y.Base.create('loginNS', Y.Model, [], {
		initializer: function() {
			var self = this,
				dataObj = new Object(),
				//shopID = VA.argPage.loginId.get('id'),//直接退出 ，emplyeID 错
				shopID = Y.Cookie.get('shopID'), //from "login" module set
				comID = '',
				user = Y.Cookie.get('u'); //from login.aspx

			//GetCompanyId
			Y.io('masterFunction.aspx/GetCompanyId', {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json; charset=utf-8'
				},
				on: {
					success: function(id, rsp) {
						var result = Y.JSON.parse(rsp.responseText),
							d = result.d;
						//if(!comID){
						comID = d;
						//
						//}

					},
					failure: function(id, rsp) {
						Y.log(rsp.status);
					}
				},
				sync: true
			});
			//GetCompanyListJson id\name
			var ioHandler = {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json; charset=utf-8'
				},
				on: {
					success: function(id, rsp) {
						var result = Y.JSON.parse(rsp.responseText),
							d = Y.JSON.parse(result.d);
						dataObj.companyList = d.company;
						comID = (!comID) ? dataObj.companyList[0].ID : comID;
						var options = '<select name="shop" id="comSelect" style="display:none;">';
						for (var i = 0; i < dataObj.companyList.length; i++) {
							if (dataObj.companyList[i].ID != comID) {
								options += '<option value="' + dataObj.companyList[i].ID + '" title="' + dataObj.companyList[i].CN + '">' + dataObj.companyList[i].CN + '</option>';
							} else {
								options += '<option value="' + dataObj.companyList[i].ID + '" title="' + dataObj.companyList[i].CN + '" selected="selected">' + dataObj.companyList[i].CN + '</option>';
							}
						}
						options += '</select>';
						Y.one('#companyName').setHTML(options);
						/*			require('jquery-plugin-chosen').then(function(){
						$('#comSelect').chosen({
						allow_single_deselect:true,
						no_results_text:'未找到数据'
						});
						});*/
						var selectModule = new Y.selectClass({
							targetDom: '#companyName'
						});
					},
					failure: function(id, rsp) {
						Y.log(rsp.status);
					}
				},
				sync: true
			};
			Y.io('masterFunction.aspx/GetCompanyListJson', ioHandler);

			//
			Y.io('masterFunction.aspx/GetShopLogo', {
				method: 'POST',
				data: '{"shopId":' + shopID + '}',
				headers: {
					'Content-Type': 'application/json; charset=utf-8'
				},
				on: {
					success: function(id, rsp) {
						var result = Y.JSON.parse(rsp.responseText);
						dataObj.logourl = result.d;

						var logourl = Y.one('#logo');
						logourl.set('src', dataObj.logourl);
					},
					failure: function(id, rsp) {
						Y.log(rsp.status);
					}
				}
			});

			//GetShopList id\name
			Y.io('masterFunction.aspx/GetShopJson', {
				method: 'POST',
				data: '{"companyId":"' + comID + '"}',
				headers: {
					'Content-Type': 'application/json; charset=utf-8'
				},
				on: {
					success: function(id, rsp) {
						var result = Y.JSON.parse(rsp.responseText),
							d = Y.JSON.parse(result.d);
						dataObj.shopList = d.shop;
						shopID = (!shopID) ? dataObj.shopList[0].shopID : shopID;
						var options = '<select name="shop" id="shopSelect" style="display:none;">';
						for (var i = 0; i < dataObj.shopList.length; i++) {
							if (dataObj.shopList[i].shopID != shopID) {
								options += '<option value="' + dataObj.shopList[i].shopID + '" title="' + dataObj.shopList[i].shopName + '">' + dataObj.shopList[i].shopName + '</option>';
							} else {
								options += '<option value="' + dataObj.shopList[i].shopID + '" title="' + dataObj.shopList[i].shopName + '" selected="selected">' + dataObj.shopList[i].shopName + '</option>';
							}
						}
						options += '</select>';
						Y.one('#shop').setHTML(options);
						var selectModule = new Y.selectClass({
							targetDom: '#shop'
						});
					},
					failure: function(id, rsp) {
						Y.log(rsp.status);
					}
				}
			});

			var username = Y.one('#username');
			username.set('text', user);

			this.set('companyId', comID);
		}
	}, {
		ATTRS: {
			companyId: {
				value: '0'
			}
		}
	});
}, '1.0', {
	requires: ['cookie', 'base-build', 'io-base', 'model', 'json-parse', 'selectPack']
});

YUI.add('login', function(Y) {
	Y.Login = Y.Base.create('loginNAME', Y.Base, [], {
		initializer: function() {
			var self = this;
			var ioHandler = {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json; charset=utf-8'
				},
				on: {
					success: function(id, rsp) {
						var result = Y.JSON.parse(rsp.responseText);
						var d = result.d;
						if (!d)
							window.location.href = 'login.aspx';
						self.set('id', d);
						Y.Cookie.set('shopID', d);
					},
					failure: function(id, rsp) {
						Y.log(rsp.status);
					}
				},
				sync: true
			};
			Y.io('masterFunction.aspx/GetShopId', ioHandler);
		}
	}, {
		ATTRS: {
			id: {
				value: 0
			}
		}
	})
}, '1.0', {
	requires: ['node-base', 'base-build', 'io-base']
});

/*
 * 分页
 * 模块 [paginator-mutilbtn]
 */

YUI.add('paginator-multi', function(Y) {
	Y.PaginatorController = Y.Base.create('paginator-multi', Y.Widget, [], {
		initializer: function() {
			//
		},
		getPageHtml: function() {
			this.pageIndex = Number(this.get('pageIndex'));
			this.pageSize = this.get('pageSize');
			this.recordCount = this.get('recordCount');
			this.numericButtonCount = this.get('numericButtonCount');
			this.selectedClass = this.get('selectedClass');
			//this.pageCount = 0;

			this.pageCount = Math.ceil(this.recordCount / this.pageSize);
			var a = this.pageIndex == 1 ? " <span class='disable' href='javascript:void(0);'><code>&lt;</code></span>" : " <a href='javascript:void(0);' pageindex='" + (this.pageIndex - 1) + "'><code>&lt;</code></a>";
			var f = this.pageCount <= this.pageIndex ? "<span class='disable' href='javascript:void(0);'><code>&gt;</code></span>" : "<a href='javascript:void(0);' pageindex='" + (this.pageIndex + 1) + "'><code>&gt;</code></a>";

			var b = this.pageIndex == 1 ? "<ul><li><a class='cur' href='javascript:void(0);'>1</a></li>" : "<ul><li><a href='javascript:void(0);' pageindex='1'>1</a></li><li>...</li>";
			var d = this.pageCount <= this.pageIndex ? "<li>...</li><li><a class='cur' href='javascript:void(0);'>" + this.pageCount + "</a></li></ul>" : "<li>...</li><li><a pageindex='" + this.pageCount + "' href='javascript:void(0);'>" + this.pageCount + "</a></li></ul>";

			var pp = ""

			var pageMathIndex = Math.floor(this.numericButtonCount / 2);
			var pageStartIndex;
			var pageEndIndex;

			if (this.pageCount < this.numericButtonCount) {
				pageStartIndex = 1
				pageEndIndex = this.pageCount;
			} else { //21 > 5
				if (this.pageCount - pageMathIndex < this.pageIndex) { //21-2 < *
					pageStartIndex = this.pageCount - this.numericButtonCount + 1;
					pageEndIndex = this.pageCount;
				} else {
					if (this.pageIndex - pageMathIndex < 1) {
						pageStartIndex = 1;
						pageEndIndex = this.numericButtonCount;
					} else {
						pageStartIndex = this.pageIndex - pageMathIndex;
						pageEndIndex = this.pageIndex + pageMathIndex;
					}
				}
			}

			for (var i = pageStartIndex; i <= pageEndIndex; i++) {
				if (this.pageIndex == i)
					pp += " <li><a class='cur' href='javascript:void(0);' pageindex='" + i + "'>" + i + "</a></li>"
				else
					pp += " <li><a href='javascript:void(0);' pageindex='" + i + "'>" + i + "</a></li>";
			}

			if (pageStartIndex == 1)
				b = '<ul>';
			if (pageEndIndex == this.pageCount)
				d = '</ul>';
			pp = a + b + pp + d + f;
			return pp;

		},
		setIndex: function(e) {
			e.preventDefault();
			var selectedClass = this.get('selectedClass');
			var control = e.currentTarget;
			if (control.hasClass(selectedClass)) {
				return;
			} else {
				control.addClass(selectedClass);
				control.siblings().removeClass(selectedClass);
			};

			this.set('pageIndex', parseInt(control.getAttribute("pageindex"), 10));
		},
		renderUI: function() {},
		bindUI: function() {
			var pageUI = Y.one('#pageContent .page');
		},
		syncUI: function() {
			this.get('contentBox').set('innerHTML',
				this.getPageHtml());
		}
	}, {
		ATTRS: {
			recordCount: {
				value: 0
			},
			numericButtonCount: {
				value: 3
			},
			pageSize: {
				value: 10
			},
			pageIndex: {
				value: 1
			},
			selectedClass: {
				value: 'cur'
			},
			contentBox: {
				value: ''
			}
		}
	})
}, '1.0', {
	requires: ['node-base', 'node-event-delegate']
});

YUI.add('CatesSelect-plugin', function(Y) {
	Y.Plugin.CatesSelect = Y.Base.create('CatesSelectPlugin', Y.Plugin.Base, [], {
		initializer: function() {
			if (this.get('rendered')) {
				this.addCates();
			} else {
				this.afterHostMethod('renderUI', this.addCates);
			}
		},
		destructor: function() {},
		bindChange: function() {
			var host = this.get('host');
			Y.one('#catesSelect select').on('change', function(e) {
				var t = e.currentTarget;
				host.set('pageIndex', 1);

				var val = Y.one('#search .inputText').get('value');
				if (val != '按菜品名 简拼 全拼搜索') {
					Y.one('#search .inputText').set('value', '按菜品名 简拼 全拼搜索');
				}
				host.set('inputTextStr', '');
				host.set('dishTypeId', t.get('value'));

				menu.dishTypeId = t.get('value');
			}, host);
		},
		addCates: function() {
			var self = this;
			var host = this.get('host');
			var dataStr = '{"shopId":"' + VA.argPage.loginId.get('id') + '"}';
			var dataHandler = {
				method: 'POST',
				data: dataStr,
				headers: {
					'Content-Type': 'application/json; charset=utf-8'
				},
				on: {
					success: function(id, rsp) {
						var res = Y.JSON.parse(rsp.responseText);
						var d = Y.JSON.parse(res.d);
						var options = '<select name="s" class="s"><option value="0"><strong>全部</strong></option>';
						for (var i = 0; i < d.length; i++) {
							if (d[i].dishTypeID != menu.dishTypeId) {
								options += '<option value="' + d[i].dishTypeID + '">' + d[i].dishTypeName + '</option>';
							} else {
								options += '<option value="' + d[i].dishTypeID + '" selected="selected">' + d[i].dishTypeName + '</option>';
							}
						}
						options += '</select>';
						var catesSelect = Y.one('#catesSelect');
						catesSelect.setHTML(options);

						self.bindChange();
						host.set('pageIndex', menu.pageIndex);
						host.set('dishTypeId', menu.dishTypeId);
					},
					failure: function(id, rsp) {
						Y.log(rsp.status);
					}
				},
				sync: true
			}
			Y.io('currentSellOff.aspx/QueryDishTypeIdAndDiahTypeName', dataHandler);
		}
	}, {
		NS: 'CatesSelectPlugin',
		ATTRS: {
			shopId: {
				value: ''
			}
		}
	});

}, '1.0', {
	requires: ['base-build', 'plugin', 'datatable-base']
});

YUI.add('cb-plugin', function(Y) {
	Y.Plugin.Cb = Y.Base.create('cbPlugin', Y.Plugin.Base, [], {
		initializer: function() {
			if (this.get('rendered')) {
				this.addCheckBox();
			} else {
				this.afterHostMethod('dataSuccess', this.addCheckBox);
			}
		},
		destructor: function() {},
		addCheckBox: function() {
			if (this.get('btnTitleAll')) {
				var a = '<ul class="checkAll">' + '	<li class="inputSprite"><input class="inputCheck" id="checkAll" type="checkbox" name="cb" value="" /> 全选</li>' + '	<li class="btnSprite"><a class="btn mutil" id="mutil" href="javascript:;">' + this.get('btnTitleMutil') + '</a><a class="btn all" href="javascript:;">' + this.get('btnTitleAll') + '</a></li>' + '</ul>';
			} else {
				var a = '<ul class="checkAll">' + '	<li class="inputSprite"><input class="inputCheck" id="checkAll" type="checkbox" name="cb" value="" /> 全选</li>' + '	<li class="btnSprite"><a class="btn mutil" id="mutil" href="javascript:;">' + this.get('btnTitleMutil') + '</a></li>' + '</ul>';
			}
			Y.one('#cbAll').setHTML(a);
		}
	}, {
		NS: 'cbPlugin',
		ATTRS: {
			btnTitleMutil: {
				value: ''
			},
			btnTitleAll: {
				value: ''
			}
		}
	});
}, '1.0', {
	requires: ['node', 'base-build', 'plugin', 'transition', 'gallery-checkboxgroups']
});

/*
 * 表格数据加载
 * 模块
 * ... ... （ 对应控制页面 ）
 */
YUI.add('dataTablePack', function(Y) {
	menu.couponType = 0;
	/*var couponTypeSelect = document.getElementById('couponType');
	console.log(couponTypeSelect)*/
	Y.DataTableClass = Y.Base.create('dataTableNS', Y.Widget, [], {
		destructor: function() {
			this.get('contentBox').remove(true);
		},
		switchTo: function(e) {
			e.preventDefault();
			e.stopPropagation();
			var ipt = Y.one('#switchTo .inputText');
			var index = parseInt(ipt.get('value'), 10);

			if (index <= this.pageCount) {
				Y.one('#pageContent .cur').setAttribute('pageindex', index);
				this.set('pageIndex', index);
			} else if (index) {
				VA.Singleton.popup.panel.set('headerContent', '提示信息');
				VA.Singleton.popup.panel.set('bodyContent', '跳转页不能大于当前最大页 ' + this.pageCount + ' 页');
				VA.Singleton.popup.panel.set('buttons', [VA.Singleton.popup.get('okButton')]);
				VA.Singleton.popup.showPanel();
				return;
			}
		},
		statusSearch: function(e) {
			e.preventDefault();
			e.stopPropagation();
			var status = e.currentTarget;
			// this.set('inputTextStr','');
			this.set('pageIndex', 1); //
			this.set('statusController', status.getAttribute('name'));

			menu.statusController = status.getAttribute('name');

			status.ancestor('li').addClass('cur');
			status.ancestor('li').siblings().removeClass('cur');
		},
		dateSearch: function(e) {
			e.stopPropagation();
			if (validate.checkDate('#dateStr')) {
				VA.Singleton.popup.panel.set('headerContent', '提示信息');
				VA.Singleton.popup.panel.set('bodyContent', '开始日期，格式不正确');
				VA.Singleton.popup.panel.set('buttons', [VA.Singleton.popup.get('okButton')]);
				VA.Singleton.popup.showPanel();
				return;
			};
			if (validate.checkDate('#dateEnd')) {
				VA.Singleton.popup.panel.set('headerContent', '提示信息');
				VA.Singleton.popup.panel.set('bodyContent', '截止日期，格式不正确');
				VA.Singleton.popup.panel.set('buttons', [VA.Singleton.popup.get('okButton')]);
				VA.Singleton.popup.showPanel();
				return;
			};
			var nowDay = Y.DataType.Date.format(new Date(), {
					format: '%Y-%m-%d'
				}),
				start = Y.one('#dateStr').get('value') || this.CalendarCqueryNS.get('minDate'),
				end = Y.one('#dateEnd').get('value') || nowDay;
			this.set('startDay', start);
			this.set('endDay', end);
			menu.startDay = start;
			menu.endDay = end;

			if (this.get('pageType') == 'accountTotal') {
				Y.all('#statusDate li').removeClass('cur');
			};
		},
		pageSizeSet: function(e) {
			e.preventDefault();
			var btn = e.currentTarget,
				pageSize = parseInt(btn.get('text'), 10),
				type = this.get('pageType');
			Y.Cookie.set('pageSize' + type, pageSize);
			if (type == 'Confirmed' || type == 'Verified') {
				// this.set('inputTextStr', '');
				menu.pageIndex = 1;
			}
			this.set('pageIndex', 1);
			this.set('pageSize', pageSize);
			menu.pageSize = pageSize;
			//
			btn.ancestor('li').addClass('cur');
			btn.ancestor('li').siblings().removeClass('cur');
		},
		pageSearch: function(e) {
			var index = parseInt(e.currentTarget.getAttribute('pageindex'), 10);
			var type = this.get('pageType');
			this.set('inputTextStr', menu.inputTextStr);
			this.set('pageIndex', index);

			menu.pageIndex = index;
		},
		idSearchHandler: function() {
			this.set('pageIndex', 1);
			var type = this.get('pageType');

			if (type == 'Verified' || type == 'accountTotal' || type == 'accountDetails') {
				var couponType = 0;
				couponType = Y.one('#couponType').get('value');
				menu.couponType = couponType;
			}

			if (type !== 'accountTotal') {
				var v;
				if (type === 'Verified') {
					v = Y.one('#search000 .inputText').get('value');
				} else {
					v = Y.one('#search .inputText').get('value');
				}
				if (Y.one('#mobilePhone')) {
					var mobileValue = Y.one('#mobilePhone .inputText').get('value');
				};
			}

			if (type == 'accountDetails') {
				var inputsPay = Y.all('#pay .inputText')._nodes,
					a = (inputsPay[0].value) ? parseInt(inputsPay[0].value) : -2147483648,
					b = (inputsPay[1].value) ? parseInt(inputsPay[1].value) : 2147483647;
				var amountMax = Math.max(a, b),
					amountMin = Math.min(a, b);

				// accountRadio
				var type = 0;
				Y.all('#accountSelect input').each(function(element) {
					if (element.get('checked')) {
						type = element.get('value');
					}
				});
				var inputsDate = Y.all('#accountDate .inputText')._nodes;
				var nowDay = Y.DataType.Date.format(new Date(), {
						format: '%Y-%m-%d'
					}),
					startDay = inputsDate[0].value || this.CalendarCqueryNS.get('minDate'),
					endDay = inputsDate[1].value || nowDay;
				var inputTextStr = (v == '请输入营业收入流水号') ? '' : v; //mainkey
				mobileValue = (mobileValue == '请输入手机号码') ? '' : mobileValue;
				this.set('inputTextStr', inputTextStr);

				this.accountData.amountMax = amountMax;
				this.accountData.amountMin = amountMin;
				this.accountData.mobilePhoneNumber = mobileValue;
				this.accountData.type = type;
				this.set('startDay', startDay);
				this.set('endDay', endDay);
				menu.startDay = startDay;
				menu.endDay = endDay;
				menu.inputTextStr = v;
				menu.accountData.amountMax = amountMax;
				menu.accountData.amountMin = amountMin;
				menu.accountData.mobilePhoneNumber = mobileValue;
				menu.accountData.amountType = type;
				menu.couponType = couponType;
				//this.syncUI();
			} else {
				if (v == '请输入手机号码或尾号' || v == '按菜品名 简拼 全拼搜索' || v == '按配菜名搜索' || v == '请输入公司名称' || v == '请输入门店名称') {
					this.set('inputTextStr', '');
					menu.inputTextStr = '';
				} else {
					this.set('inputTextStr', v);
					menu.inputTextStr = v;
				}
			};
			this.syncUI();
		},
		idSearch: function(e) {
			e.preventDefault();
			this.idSearchHandler();
		},
		addPhoneHandler: function() {
			var that = this;

			var phoneNum = Y.one('#addPhone .inputText')._node.value;
			phoneNum = phoneNum === "输入手机号码" ? "" : phoneNum;
			var dataStr = 'm=business_employees_authority_add&shopId=' + this.shopId + '&phoneNum=' + phoneNum;
			var dataHandler = {
				method: 'POST',
				data: dataStr,
				headers: {
					'Content-Type': 'application/x-www-form-urlencoded; charset=utf-8'
				},
				on: {
					success: function(id, rsp) {
						var res = Y.JSON.parse(rsp.responseText);
						if (res == '-1000') {
							VA.Singleton.popup.timeout();
							return;
						}

						var okHandler = function() {
							if (res.list[0].status == 1) {
								that.syncUI();
							}
						};
						VA.Singleton.popup.panel.set('headerContent', '店员管理');
						VA.Singleton.popup.panel.set('bodyContent', res.list[0].info);
						VA.Singleton.popup.panel.set('buttons', [VA.Singleton.popup.get('okButton')]);
						VA.Singleton.popup.set('ok', okHandler);
						VA.Singleton.popup.showPanel();
					},
					failure: function(id, rsp) {
						Y.log(rsp.status);
					}
				}
			};
			Y.io('ajax/doSybWeb.ashx', dataHandler);
		},

		checkSuccess: function(res, t) {
			var className = t.getAttribute('class'),
				isCancel = className.indexOf('cancel') > -1;
			switch (this.get('pageType')) {
				case 'Confirmed':
					if (res == -1) {} else if (res === -7 || res === -8 || res === -9) {

						var popupSprite = t.one('.popupSprite');
						popupSprite.empty(true);
						Y.all('#dataTable .popupSprite').empty(true);
						var tipsTxt = '';
						if (res === -7) {
							tipsTxt = '<h3 class="popup"><span class="txt">您已执行<strong>[退款操作]</strong>，无法取消入座</span></h3>';
						} else if (res === -8) {
							tipsTxt = '<h3 class="popup"><span class="txt">顾客已执行<strong>[退款操作]</strong>，无法入座</span></h3>';
						} else if (res === -9) {
							tipsTxt = '<h3 class="popup"><span class="txt">当前单子已补差价，无法取消入座，请选择退款</span></h3>';
						}
						popupSprite.append(tipsTxt);

						var popup = Y.one('.popup');
						popup.transition({
							duration: 0.2,
							easing: 'ease-in',
							top: '-56px',
							height: '56px',
							on: {
								start: function() {
									//
								},
								end: function() {
									popup.transition({
										delay: 0.5,
										easing: 'ease-out',
										height: '0px',
										top: '-25px',
										opacity: {
											duration: 1,
											value: 0
										}
									}, function() {
										popup.remove(true);
									});
								}
							}
						});
					} else {
						this.syncUI();
					}
					break;
				case 'currentSellOff':
					var isSoon = className.indexOf('btnSoon') > -1,
						isBeen = className.indexOf('btnBeen') > -1,
						isAll = className.indexOf('all') > -1; //
					if (isSoon) {
						this.sellOffBeenPlugin.addData(),
							t.set('text', '已沽清'),
							t.removeClass('btnSoon'),
							t.addClass('btnBeen');
					} else if (isBeen) {
						//
					} else if (isAll) {
						this.sellOffBeenPlugin.pageController(0, 0);
						this.syncUI();
					} else {
						this.syncUI();
					};

					break;
				case 'dishList':
					var isOff = className.indexOf('sellOff'),
						isDelete = className.indexOf('delete') > -1;
					if (isDelete) {
						return;
					}
					var txt = (isOff > -1) ? '沽清' : '停售';
					if (isCancel) {
						t.set('text', txt), t.removeClass('cancel');
					} else {
						t.set('text', '取消' + txt), t.addClass('cancel');
					}
					break;
				case 'companylist':
					break;
				default:
					this.set('pageIndex', 1);
					this.syncUI();
					break;
			};

		},
		checkSubmit: function(e) {
			var cb = Y.one('#cbAll');
			if (!cb) {
				e.preventDefault();
			}
			e.stopPropagation();

			var self = this,
				shopId = this.shopId,
				companyId = this.companyId,
				targetItem = e.currentTarget,
				checkStr = '',
				ioURL = '';

			var eClass = targetItem.getAttribute('class'),
				isCancel = eClass.indexOf('cancel') > -1;

			var pType = this.get('pageType'),
				isDetail = eClass.indexOf('detail') > -1,
				isEditor = eClass.indexOf('editor') > -1,
				isBack = eClass.indexOf('back') > -1,
				isRefund = eClass.indexOf('refund') > -1,
				disabled = eClass.indexOf('disabled') > -1,
				isPageChange = eClass.indexOf('to-pagecontainer') > -1;

			var pageSwitch = isDetail || isEditor || isBack || isPageChange;
			if (pageSwitch) {
				if (pType == 'Verified' || pType == 'incrementEdit') {
					e.preventDefault();
				}
				if (pType == 'increment') {
					//
				}
				var hrefStr = targetItem.getAttribute('href');
				var m = menu.getType(hrefStr),
					aspx = m + ".aspx";
				VA.argPage.qs = UIBase.getNyQueryStringArgs(hrefStr);

				return isBack ? menu.prevPage(aspx, m) : menu.nextPage(aspx, m);
			}

			function ioSubmit() {
				Y.io(ioURL, {
					method: 'POST',
					data: checkStr,
					headers: {
						'Content-Type': 'application/json; charset=utf-8'
					},
					on: {
						success: function(id, rsp) {
							var result = Y.JSON.parse(rsp.responseText);
							var res = Number(result.d);
							if (res == -1000) {
								VA.Singleton.popup.timeout();
								return;
							}
							self.checkSuccess(res, targetItem);
						},
						failure: function(id, rsp) {}
					},
					sync: true
				});
			};

			switch (pType) {
				case 'Confirmed':
					//审核退款操作
					if (disabled) {
						return;
					}
					if (isRefund) {
						var itemId = targetItem.getAttribute('itemid');
						var config = {};
						config.dataStr = '{"preOrder19dianId":"' + itemId + '"}';
						config.ioURL = 'preOrderShopConfirmed.aspx/QueryCanRefundAccount';
						config.headers = {
							'Content-Type': 'application/json; charset=utf-8'
						};
						config.sync = true;
						self.ioRequest(config, function(data) {
							var max = data.d ? data.d : 0;
							self.refundHandler(max, itemId);
						});
						return;
					}

					var statusFlag = '1',
						str = '',
						itemId = targetItem.getAttribute('itemid');
					if (isCancel) {
						str = itemId;
						statusFlag = '0';
					} else {
						str = itemId;
					}
					checkStr = '{"preOrder19dianId":"' + str + '","statusFlag":"' + statusFlag + '"}';
					ioURL = 'preOrderShopConfirmed.aspx/ShopConfirmedOperate';
					break;
				case 'Verified':
					var statusFlag = '1',
						str = '',
						isVerified = eClass.indexOf('verified'),
						itemId = targetItem.getAttribute('itemid');
					if (isVerified) {
						var okHandler = function() {
							checkStr = '{"preOrder19dianIdStr":"' + itemId + '","statusFlag":"' + statusFlag + '","shopId":"' + shopId + '"}';
							ioURL = 'preOrderShopVerified.aspx/ShopVerifiedOperate';
							ioSubmit();
						}
						var cancelHandler = function() {
							//
						}
						VA.Singleton.popup.panel.set('headerContent', '财务对账');
						VA.Singleton.popup.panel.set('bodyContent', '您确定要进行对账操作？');
						VA.Singleton.popup.set('ok', okHandler);
						VA.Singleton.popup.set('cancel', cancelHandler);
						VA.Singleton.popup.panel.set('buttons', [VA.Singleton.popup.get('okButton'), VA.Singleton.popup.get('cancelButton')]);
						VA.Singleton.popup.showPanel();
						return;
					}
					break;
				case 'currentSellOff':
					var str = '',
						isMutil = eClass.indexOf('mutil'),
						isAll = eClass.indexOf('all');
					if (isCancel) {
						str = targetItem.getAttribute('preid2');
						checkStr = '{"shopId":"' + this.shopId + '","DishPriceI18nID":"' + str + '"}';
						ioURL = 'currentSellOff.aspx/CancleCurrentSellOff';
					} else if (isMutil > -1) {

						var okHandler = function() {
							Y.all('#sellOffBeen .inputCheck').each(function(o) {
								if (o.get('checked') && o.getAttribute('preid2')) {
									str += o.getAttribute('preid2') + ',';
								};
							});
							str = str.slice(0, -1);
							checkStr = '{"shopId":"' + shopId + '","DishPriceI18nID":"' + str + '"}';
							ioURL = 'currentSellOff.aspx/CancleCurrentSellOff';
							ioSubmit();
						}
						var cancelHandler = function() {
							//
						}
						VA.Singleton.popup.panel.set('headerContent', '沽清管理');
						VA.Singleton.popup.panel.set('bodyContent', '您确定要进行【 批量取消 】操作？');
						VA.Singleton.popup.panel.set('buttons', [VA.Singleton.popup.get('okButton'), VA.Singleton.popup.get('cancelButton')]);
						VA.Singleton.popup.set('ok', okHandler);
						VA.Singleton.popup.set('cancel', cancelHandler);
						VA.Singleton.popup.showPanel();

						return;

					} else if (isAll > -1) {
						var okHandler = function() {
							str = 'cancleAll';
							checkStr = '{"shopId":"' + shopId + '","DishPriceI18nID":"' + str + '"}';
							ioURL = 'currentSellOff.aspx/CancleCurrentSellOff';
							ioSubmit();
						}
						var cancelHandler = function() {
							//
						}
						VA.Singleton.popup.panel.set('headerContent', '沽清管理');
						VA.Singleton.popup.panel.set('bodyContent', '您确定要进行【 全部取消 】操作？');
						VA.Singleton.popup.panel.set('buttons', [VA.Singleton.popup.get('okButton'), VA.Singleton.popup.get('cancelButton')]);
						VA.Singleton.popup.set('ok', okHandler);
						VA.Singleton.popup.set('cancel', cancelHandler);
						VA.Singleton.popup.showPanel();

						return;
					} else { //沽清
						str = targetItem.getAttribute('preid2');
						var DishI18nID = targetItem.getAttribute('preid');
						checkStr = '{"companyId":"' + this.companyId + '","shopId":"' + this.shopId + '","DishI18nID":"' + DishI18nID + '","DishPriceI18nID":"' + str + '"}';
						ioURL = 'currentSellOff.aspx/CurrentSellOff';
					}
					break;
				case 'dishList':
					var str = '',
						isOff = eClass.indexOf('sellOff'),
						isOut = eClass.indexOf('sellOut'),
						isDelete = eClass.indexOf('delete');
					var DishI18nID = targetItem.getAttribute('preid'),
						str = targetItem.getAttribute('preid2');
					if (isOff > -1) {
						if (isCancel) {
							checkStr = '{"DishPriceI18nID":"' + str + '"}';
							ioURL = 'dishList.aspx/CancleCurrentSellOff';
						} else {
							checkStr = '{"companyId":"' + companyId + '","shopId":"' + shopId + '","DishI18nID":"' + DishI18nID + '","DishPriceI18nID":"' + str + '"}';
							ioURL = 'dishList.aspx/CurrentSellOff';
						}
					} else if (isOut > -1) {
						if (isCancel) {
							ioURL = 'dishList.aspx/SetNoSelloutJson';
						} else {
							ioURL = 'dishList.aspx/SetSelloutJson';
						}
						checkStr = '{"dishpriceid":"' + str + '"}';
					} else if (isDelete > -1) {
						var okHandler = function() {
							checkStr = '{"DishI18nID":"' + DishI18nID + '"}';
							ioURL = 'dishList.aspx/RemoveDishInfo';
							ioSubmit();

							self.syncUI();
						}
						var cancelHandler = function() {
							//
						}
						VA.Singleton.popup.panel.set('headerContent', '菜品管理');
						VA.Singleton.popup.panel.set('bodyContent', '您确定要进行【 删除 】操作？');
						VA.Singleton.popup.panel.set('buttons', [VA.Singleton.popup.get('okButton'), VA.Singleton.popup.get('cancelButton')]);
						VA.Singleton.popup.set('ok', okHandler);
						VA.Singleton.popup.set('cancel', cancelHandler);
						VA.Singleton.popup.showPanel();

						return;
					} else {
						return;
					}
					break;
				case 'dishMix':
					var isDelete = eClass.indexOf('delete') > -1;
					if (isDelete) {
						targetItem.setAttribute('rel', 3);
						var dataRow = targetItem.ancestor('tr');

						//var txtTips = Y.one(targetItem).ancestor('.disMixBtn').one('.txt-tips').setStyles({"display": "block", "left": -32, "opacity": 0, "z-index": 1000});
						//var tips = popup.tips(txtTips);
						//tips.stop().run();
						self.dishMixTimeoutID = setTimeout(function() {
							dataRow.hide();
						}, 2000);
					}
					return;
					break;
				case 'configurationEmployees':
					return;
					break;
				case 'companylist':
				case 'companyaccountlist':
				case 'companymenulist':
				case 'shoplist':
				case 'shopdiscountlist':
					var isDelete = eClass.indexOf('delete') > -1,
						identity = targetItem.getAttribute('preid');
					if (isDelete) {
						var dataStr = '';
						if (pType === 'companylist') {
							dataStr = 'm=syb_delete_company&companyId=' + identity;
						} else if (pType === 'companyaccountlist') {
							dataStr = 'm=syb_delete_bankaccount&accountId=' + identity;
						} else if (pType === 'companymenulist') {
							dataStr = 'm=syb_delete_menu&menuCompanyId=' + identity;
						} else if (pType === 'shopdiscountlist') {
							dataStr = 'm=syb_delete_shopvip&shopVipId=' + identity;
						} else if (pType === 'shoplist') {
							dataStr = 'm=syb_delete_shop&shopId=' + identity;
						}

						dataStr = 'm=channelDishDelete&channelDishIDS=' + identityStr;
						self.deleteItemHandler('ajax/doSybSystem.ashx', dataStr);
					}
					return;
					break;
				case 'increment':
					var isClose = eClass.indexOf('close') > -1,
						isApply = eClass.indexOf('apply') > -1,
						isSave = eClass.indexOf('save') > -1;
					var incrementId = Y.one("#incrementTabList .active").getAttribute("data-id"),
						status = '0';
					var cancelHandler = confirmHandler = function() {};
					var okHandler = function() {
						var config = {};
						config.ioURL = 'ajax/doSybChannel.ashx';
						config.dataStr = 'm=shopChannelSwitch&id=' + incrementId + '&status=' + status;;
						config.headers = {
							'Content-Type': 'application/x-www-form-urlencoded; charset=utf-8'
						};
						config.sync = false;
						self.ioRequest(config, function(data) {
							if (data.list[0].status === 1) {
								self.incrementSortSaveHandler(function() {
									/*
									Y.all("#incrementTabList li").each(function(elem, ind){
										if(elem.hasClass("active")){
											that.set('activeInd', ind);
										};
									});
									*/
									self.syncUI();
								});


							} else {
								VA.Singleton.popup.panel.set('headerContent', '提示信息');
								VA.Singleton.popup.panel.set('bodyContent', data.list[0].info);
								VA.Singleton.popup.panel.set('buttons', [VA.Singleton.popup.get('okButton')]);
								VA.Singleton.popup.set('ok', confirmHandler);
								VA.Singleton.popup.showPanel();
							}
						});
					};
					if (isClose) {
						status = '0';
						VA.Singleton.popup.panel.set('headerContent', '增值管理');
						VA.Singleton.popup.panel.set('bodyContent', '您确定要进行【 关闭 】操作？');
						VA.Singleton.popup.panel.set('buttons', [VA.Singleton.popup.get('okButton'), VA.Singleton.popup.get('cancelButton')]);
						VA.Singleton.popup.set('ok', okHandler);
						VA.Singleton.popup.set('cancel', cancelHandler);
						VA.Singleton.popup.showPanel();
					}
					if (isApply) {
						status = '1';
						okHandler();
					}
					if (isSave) {
						self.incrementSortSaveHandler(function() {
							VA.Singleton.popup.panel.set('headerContent', '增值管理');
							VA.Singleton.popup.panel.set('bodyContent', '设置排序保存成功！');
							VA.Singleton.popup.panel.set('buttons', [VA.Singleton.popup.get('okButton')]);
							VA.Singleton.popup.set('ok', okHandler);
							VA.Singleton.popup.showPanel();
						});
					}

					return;
					break;
				case 'incrementEdit':
					// 
					var isDelete = eClass.indexOf('delete') > -1,
						isAll = eClass.indexOf('all') > -1,
						isMutil = eClass.indexOf('mutil') > -1;
					if (disabled) {
						return;
					}
					var identityStr = '',
						dataStr;
					if (isDelete) {
						identityStr = targetItem.getAttribute('preid');
					} else if (isMutil) {
						Y.all('#dataTable .inputCheck').each(function(o) {
							if (o.get('checked') && o.getAttribute('preid')) {
								identityStr += o.getAttribute('preid') + ',';
							};
						});
						identityStr = identityStr.substr(0, identityStr.length - 1);
					} else if (isAll) {
						Y.all('#dataTable .inputCheck').each(function(o) {
							if (o.getAttribute('preid')) {
								identityStr += o.getAttribute('preid') + ',';
							};
						});
						identityStr = identityStr.substr(0, identityStr.length - 1);
					}
					dataStr = 'm=channelDishDelete&channelDishIDS=' + identityStr;
					self.deleteItemHandler('ajax/doSybChannel.ashx', dataStr);
					return;

					return;
					break;
				default:
					return;
					break;

			};
			ioSubmit();
		},
		deleteItemHandler: function(ioURL, dataStr) {
			var self = this;
			var cancelHandler = confirmHandler = function() {};
			var okHandler = function() {
				var config = {};
				config.ioURL = ioURL;
				config.dataStr = dataStr;
				config.headers = {
					'Content-Type': 'application/x-www-form-urlencoded; charset=utf-8'
				};
				config.sync = false;
				self.ioRequest(config, function(data) {
					if (data.list[0].status === 1) {
						self.syncUI();
					} else {
						VA.Singleton.popup.panel.set('headerContent', '提示信息');
						VA.Singleton.popup.panel.set('bodyContent', data.list[0].info);
						VA.Singleton.popup.panel.set('buttons', [VA.Singleton.popup.get('okButton')]);
						VA.Singleton.popup.set('ok', confirmHandler);
						VA.Singleton.popup.showPanel();
					}
				});
			};
			VA.Singleton.popup.panel.set('headerContent', '公司管理');
			VA.Singleton.popup.panel.set('bodyContent', '您确定要进行【 删除 】操作？');
			VA.Singleton.popup.panel.set('buttons', [VA.Singleton.popup.get('okButton'), VA.Singleton.popup.get('cancelButton')]);
			VA.Singleton.popup.set('ok', okHandler);
			VA.Singleton.popup.set('cancel', cancelHandler);
			VA.Singleton.popup.showPanel();
		},
		refundHandler: function(max, id) {
			var refundMax = max,
				itemId = id,
				that = this;
			if (Y.one('.refundLayout'))
				Y.one('.refundLayout').ancestor().remove(true);
			var bd = document.getElementById('page'),
				layout = document.createElement('div');
			layout.className = 'refundLayout';
			layout.innerHTML = '<h4 class="headerItem">退款操作</h4>' + '<div class="text">' + '	<p class="refundMoney"><label for="refundMoney">退款金额：</label><input name="refundMoney" class="inputText" type="text" maxlength="50" /></p>' + '	<p class="comment"><span class="txt">（当前可退最大金额<span class="num">' + refundMax + '</span>）</span></p>' + '	<p class="remark"><label for="remark">退款原因：</label><textarea name="remark" class="area" rows="3" cols="3"></textarea></p>'

			+ '	<div class="btnSprite">' + '		<div class="txt-tips-sprite"><i class="txt-tips" id="refund-txt-tips">退款原因不能为空!</i></div>' + '		<a href="javascript:;" class="btn comfirm">确定</a><a href="javascript:;" class="btn cancel">取消</a>' + '</div>' + '<p class="tips">备注：预计在[ <span class="account">3</span> ]个工作日内到账</p>' + '</div>';
			bd.appendChild(layout);
			var overlay = new Y.Overlay({
				srcNode: '.refundLayout',
				width: "388px",
				height: "304px",
				shim: false,
				centered: true
			});
			overlay.render();
			overlay.show();

			Y.one('.remark .area').on('focus', function(e) {
				var t = e.currentTarget;
				var v = t.get('value');
				if (v == '可选填') {
					t.set('value', '');
				}
			}, this);

			function refundSubmitHandler(e) {
				that.refundListener.detach();
				var t = e.currentTarget;
				var className = t.getAttribute('class'),
					isComfirm = className.indexOf('comfirm') > -1,
					isCancel = className.indexOf('cancel') > -1;
				var txtTips = Y.one('#refund-txt-tips').setStyles({
					"display": "block",
					"opacity": 0
				});
				var tips = popup.tips(txtTips);
				if (isComfirm) {
					var refundAccount = Y.one('.refundMoney .inputText')._node.value;
					var refundDes = Y.one('.remark .area').get('value');
					if (refundAccount == '') {
						txtTips.set('text', '退款金额不能为空!');
						tips.stop().run();
						clearTimeout(that.refundTimeoutID);
						that.refundTimeoutID = setTimeout(function() {
							that.refundListener = Y.all('.refundLayout .btnSprite .btn').on('click', refundSubmitHandler, that);
						}, 2000);
						return;
					}
					if (refundDes == '') {
						txtTips.set('text', '退款原因不能为空!');
						tips.stop().run();
						clearTimeout(that.refundTimeoutID);
						that.refundTimeoutID = setTimeout(function() {
							that.refundListener = Y.all('.refundLayout .btnSprite .btn').on('click', refundSubmitHandler, that);
						}, 2000);
						return;
					}
					var config = {};
					config.dataStr = '{"refundAccount":"' + refundAccount + '","refundDes":"' + refundDes + '","preOrder19dianId":"' + itemId + '"}';
					config.ioURL = 'preOrderShopConfirmed.aspx/OrderRefundOperate';
					config.headers = {
						'Content-Type': 'application/json; charset=utf-8'
					};
					that.ioRequest(config, function(data) {
						var d = Y.JSON.parse(data.d);
						if (d.list[0].status == 1) {
							clearTimeout(that.refundTimeoutID);
							that.refundTimeoutID = setTimeout(function() {
								overlay.hide();
							}, 2000);
							that.syncUI(); //wangc 退款成功刷新当前页面
						}
						txtTips.set('text', d.list[0].info + '!');
						tips.stop().run();
						that.refundListener = Y.all('.refundLayout .btnSprite .btn').on('click', refundSubmitHandler, that);
					});
				} else if (isCancel) {
					overlay.hide();
				}
			};
			this.refundListener = Y.all('.refundLayout .btnSprite .btn').on('click', refundSubmitHandler, this);
		},
		dataSuccess: function(res) {
			var self = this;
			var pageItemes = new Object();
			var UseCouponCount = 0;
			var RealDeductibleAmountTotal = 0;
			//----------------------------------------------------------------------------------
			var getNumber = function(a) {
				var num = Math.round(a * 100) / 100;
				return num;
			}
			if (res.d) {
				if (res.d == '-1000') {
					VA.Singleton.popup.timeout();
					return;
				}

				/*//"取消入座"提示
		if( res.d == '-9' ){
		  VA.Singleton.popup.panel.set('headerContent', '提示信息');
      VA.Singleton.popup.panel.set('bodyContent', data.list[0].info);
      VA.Singleton.popup.panel.set('buttons', [VA.Singleton.popup.get('okButton')]);
      VA.Singleton.popup.set('ok', confirmHandler);
      VA.Singleton.popup.showPanel();
      return;
		}*/

				var d = Y.JSON.parse(res.d);
				if (d.page) {
					var pageCount = Math.ceil(d.page.totalCount / this.pageSize);
					this.pageCount = pageCount;
					if (self.get('pageType') == 'accountTotal') { // if(self.get('pageType')=='accountTotal'){
						var recordCount = d.orderCount;
					} else {
						var recordCount = d.page.totalCount;
					}

					if (d.hasOwnProperty('RealDeductibleAmountTotal')) {
						if (!(d.RealDeductibleAmountTotal === "")) {
							RealDeductibleAmountTotal = d.RealDeductibleAmountTotal;
						}
					}

					var pay = getNumber(d.totalMoney);
					var total = getNumber(d.totalMoney);
					pageItemes = {
						recordCount: recordCount,
						totalAmount: total,
						payAmount: pay,
						pageCount: pageCount,
						RealDeductibleAmountTotal: RealDeductibleAmountTotal
					};
				} else {
					var pageCount = Math.ceil(d.total[1].ocount / this.pageSize);
					this.pageCount = pageCount;
					if (d.total.length > 2) {
						var pay = getNumber(d.total[2].payTotalAmount);
					}
					var total = getNumber(d.total[0].totalAmount);

					for (var i = 0; i < d.total.length; i++) {
						if (d.total[i].hasOwnProperty('UseCouponCount')) {
							if (!(d.total[i].UseCouponCount === "")) {
								UseCouponCount = d.total[i].UseCouponCount;
							}
						} else if (d.total[i].hasOwnProperty('RealDeductibleAmountTotal')) {
							if (!(d.total[i].RealDeductibleAmountTotal === "")) {
								RealDeductibleAmountTotal = d.total[i].RealDeductibleAmountTotal;
							}
						}
					}

					pageItemes = {
						recordCount: d.total[1].ocount,
						totalAmount: total,
						payAmount: pay,
						pageCount: pageCount,
						UseCouponCount: UseCouponCount,
						RealDeductibleAmountTotal: RealDeductibleAmountTotal
					};
				}
			} else if (res.list) {
				if (res.list == '-1000') {
					VA.Singleton.popup.timeout();
					return;
				}
				var listData = res.list;
				var d = res.list;
				if (d.length >= 1 && !!d[0].info) {
					if (d[0].info == 'null') {

					} else {
						listData = Y.JSON.parse(d[0].info);
					}
					if (listData.hasOwnProperty('recordCount')) {
						res.page = {
							"pageIndex": listData.pageIndex,
							"pageSize": listData.pageSize,
							"totalCount": listData.recordCount
						};
						d = Y.JSON.parse(listData.data);
					} else { // listData.TableJson
						d = listData
					}
				} else if (!d[0].info) { // 处理info 为null 时的数据情况；
					d = listData
				}

				var totalCount = res.page ? res.page.totalCount : 0;
				var pageCount = Math.ceil(totalCount / this.pageSize);
				this.pageCount = pageCount;

				pageItemes = {
					recordCount: totalCount,
					totalAmount: total,
					payAmount: pay,
					pageCount: pageCount
				};
				//存储分页信息
				this.set('dataPage', res.page);
			} else {
				var pageIndex = self.get('pageIndex');
				if (pageIndex > 1) {
					self.set('pageIndex', (pageIndex - 1));
					return;
				}
				var d = new Object();
				d.TableJson = '';
				d.total = ['', ''];
				pageItemes = {
					recordCount: 0,
					totalAmount: 0,
					payAmount: 0,
					pageCount: 0,
					UseCouponCount: UseCouponCount,
					RealDeductibleAmountTotal: RealDeductibleAmountTotal
				};

				//----------------------------------------------------------------------------------
			}
			if (typeof res.length != "undefined") {
				var d = res;
			}
			this.set('dataTemp', d);

			var dataTotalTemplate = '<span class="num">共<em>{recordCount}</em><i class="unit1">单</i><i class="unit2">项</i><i class="unit3">道</i><i class="unit4">种</i><i class="unit5">位</i></span>' + '<span class="total3">总金额<em>￥{totalAmount}</em></span>' + '<span class="total2">支付金额<em>￥{payAmount}</em></span>' + '<span class="total2">退款金额<em>￥{totalAmount}</em></span>' + '<span class="total6">使用抵扣券<em>{UseCouponCount}</em>张&nbsp;</span>' + '<span class="total6">实际抵扣金额共计<em>￥{RealDeductibleAmountTotal}</em></span>' + '<span class="total7">实际抵扣金额共计<em>￥{RealDeductibleAmountTotal}</em></span>' + '<span class="total1">总收入<em>￥{totalAmount}</em></span>',
				pageContentTemplate = ' <div class="page">' + '</div>' + '<ul class="pageTotal" id="switchTo">' + '	<li class="txt page-size">' + '		共<em>{pageCount}</em>页' + '	</li>' + '	<li class="txt page-index">' + '		<input type="text" class="inputText" maxlength="8" />' + '	</li>' + '	<li class="txt">' + '		<a class="btn" href="#">跳至</a>' + '	</li>' + '</ul>';

			if (Y.one('#dataTotal')) {
				Y.one('#dataTotal').set('innerHTML',
					Y.Lang.sub(dataTotalTemplate, pageItemes));
			};
			if (!Y.one('#pageContent')) {
				return;
			}
			Y.one('#pageContent').set('innerHTML',
				Y.Lang.sub(pageContentTemplate, pageItemes));

			var numBtn = (this.get('pageType') == 'currentSellOff') ? 1 : 3;
			if (self.get('pageType') == 'accountTotal') {
				var recordCount = d.page.totalCount;
			} else {
				var recordCount = pageItemes.recordCount;
			}
			var paginatorController = new Y.PaginatorController({
				contentBox: '#pageContent .page',
				pageIndex: this.pageIndex,
				recordCount: recordCount,
				pageSize: this.pageSize,
				numericButtonCount: numBtn
			});
			paginatorController.render();
			Y.one('#pageContent .page').delegate('click', this.pageSearch, 'a', this);
			Y.one('#switchTo li .btn').on('click', this.switchTo, this);
		},
		dateRange: function(e) {
			e.preventDefault();
			var t = e.currentTarget,
				n = t.getAttribute('name');
			t.ancestor('li').addClass('cur');
			t.ancestor('li').siblings().removeClass('cur');
			var startDay;
			var endDay;
			if (n == '1') {
				startDay = VAcalendar.month.startDay;
				endDay = VAcalendar.month.endDay;
			} else if (n == '2') {
				startDay = VAcalendar.week.startDay;
				endDay = VAcalendar.week.endDay;
			} else if (n == '3') {
				this.dateSearch(e);
			} else if (n == '4') {
				//today
				startDay = VAcalendar.today;
				endDay = VAcalendar.today;
			} else if (n == '5') {
				var durationDay = VAcalendar.getDurationDay(-1);
				startDay = durationDay;
				endDay = durationDay;
			}
			this.set('startDay', startDay);
			this.set('endDay', endDay);

			Y.one('#dateStr').set('value', startDay);
			Y.one('#dateEnd').set('value', endDay);
		},
		pageChange: function() {
			var type = this.get('pageType');
			var pageSizeItem = Y.Cookie.get('pageSize' + type);
			pageSizeItem = pageSizeItem ? pageSizeItem : 10;
			Y.all("#pageShow a").each(function(element) {
				var pageSizeNumber = parseInt(element.get('text'));
				if (pageSizeNumber == pageSizeItem) {
					element.ancestor('li').addClass('cur');
				} else {
					element.ancestor('li').removeClass('cur');
				}
			});
			this.set('pageSize', pageSizeItem);

			this.set("startDay", menu.startDay);
			this.set("endDay", menu.endDay);
			this.set("pageIndex", menu.pageIndex);
			this.set("statusController", menu.statusController);
			this.set("dishTypeId", menu.dishTypeId);
			this.set("inputTextStr", menu.inputTextStr);

			function refundSubmitHandler(e) {
				that.refundListener.detach();
				var t = e.currentTarget;
				var className = t.getAttribute('class'),
					isComfirm = className.indexOf('comfirm') > -1,
					isCancel = className.indexOf('cancel') > -1;
				var txtTips = Y.one('#refund-txt-tips').setStyles({
					"display": "block",
					"opacity": 0
				});
				var tips = popup.tips(txtTips);
				if (isComfirm) {
					var refundAccount = Y.one('.refundMoney .inputText')._node.value;
					var refundDes = Y.one('.remark .area').get('value');
					if (refundAccount == '') {
						txtTips.set('text', '退款金额不能为空!');
						tips.stop().run();
						clearTimeout(that.refundTimeoutID);
						that.refundTimeoutID = setTimeout(function() {
							that.refundListener = Y.all('.refundLayout .btnSprite .btn').on('click', refundSubmitHandler, that);
						}, 2000);
						return;
					}
					if (refundDes == '') {
						txtTips.set('text', '退款原因不能为空!');
						tips.stop().run();
						clearTimeout(that.refundTimeoutID);
						that.refundTimeoutID = setTimeout(function() {
							that.refundListener = Y.all('.refundLayout .btnSprite .btn').on('click', refundSubmitHandler, that);
						}, 2000);
						return;
					}
					var config = {};
					config.dataStr = '{"refundAccount":"' + refundAccount + '","refundDes":"' + refundDes + '","preOrder19dianId":"' + itemId + '"}';
					config.ioURL = 'preOrderShopConfirmed.aspx/OrderRefundOperate';
					config.headers = {
						'Content-Type': 'application/json; charset=utf-8'
					};
					that.ioRequest(config, function(data) {
						var d = Y.JSON.parse(data.d);
						if (d.list[0].status == 1) {
							clearTimeout(that.refundTimeoutID);
							that.refundTimeoutID = setTimeout(function() {
								overlay.hide();
							}, 2000);
							that.syncUI(); //wangc 退款成功刷新当前页面
						}
						txtTips.set('text', d.list[0].info + '!');
						tips.stop().run();
						that.refundListener = Y.all('.refundLayout .btnSprite .btn').on('click', refundSubmitHandler, that);
					});
				} else if (isCancel) {
					overlay.hide();
				}
			};
			this.refundListener = Y.all('.refundLayout .btnSprite .btn').on('click', refundSubmitHandler, this);
		},
		ioRequest: function(arg, callback) {
			var ioURLStr = arg.ioURL;
			if (ioURLStr.indexOf(".ashx") > -1) {
				arg.headers = {
					'Content-Type': 'application/x-www-form-urlencoded; charset=utf-8'
				};
			} else {
				arg.headers = {
					'Content-Type': 'application/json; charset=utf-8'
				};
			}
			arg.ioURL = arg.ioURL || 'ajax/doSybWeb.ashx';
			arg.sync = arg.sync || false;
			var ioHandler = {
				method: 'POST',
				data: arg.dataStr,
				headers: arg.headers,
				on: {
					success: function(id, rsp) {
						if (rsp.responseText == '-1000') {
							VA.Singleton.popup.timeout();
							return;
						}
						var res = Y.JSON.parse(rsp.responseText);
						callback(res);
					},
					failure: function(id, rsp) {
						Y.log(rsp.status);
					}
				},
				sync: arg.sync
			};
			Y.io(ioURLStr, ioHandler);
		},
		renderUI: function() {
			var type = this.get('pageType');
			var qs = VA.argPage.qs = this.get('queryString');
			if (menu.statusController) {
				this.set('statusController', menu.statusController);
			};
			if (type == 'accountDetails') {
				Y.all('#accountSelect input').each(function(element) {
					if (element.get("value") == menu.accountData.type) {
						element.set("checked", "checked");
					};
				});
				var inputsPay = Y.all('#pay .inputText')._nodes;
				inputsPay[0].value = (menu.accountData.amountMin > -2147483648) ? menu.accountData.amountMin : "";
				inputsPay[1].value = (menu.accountData.amountMax < 2147483647) ? menu.accountData.amountMax : "";
				var searchPhoneNumber = menu.accountData.mobilePhoneNumber ? menu.accountData.mobilePhoneNumber : "";
				Y.one('#mobilePhone .inputText').set('value', searchPhoneNumber);

				var couponType;
				qs.couponType ? couponType = qs.couponType : couponType = 0;
				document.getElementById('couponType').value = couponType;
				menu.couponType = couponType;

				if (qs.detail && qs.detail != 'undefined') {
					var accountDetailBack = Y.one('#accountDetailBack');
					accountDetailBack.setStyle('display', 'block');
					var url = accountDetailBack.get('href');
					url = UIBase.addURIParam(url, 'detail', qs.detail);
					url = UIBase.addURIParam(url, 'totalStartDay', qs.totalStartDay);
					url = UIBase.addURIParam(url, 'totalEndDay', qs.totalEndDay);
					accountDetailBack.set('href', url);
					menu.startDay = menu.endDay = qs.detail;
				};
			}
			if (type == 'Confirmed') {
				this.set('statusController', 3);
				menu.statusController = 3;
			};
			this.pageChange();

			if (this.hasOwnProperty('CalendarCqueryNS')) {
				Y.one('#dateStr').set('value', this.get('startDay'));
				Y.one('#dateEnd').set('value', this.get('endDay'));
			};
		},
		bindUI: function() {
			var type = this.get('pageType');

			var that = this;
			if ("CalendarCqueryNS" in this._plugins) {
				if (type == 'accountDetails' || type == 'Confirmed' || type == 'Verified') {} else {
					Y.one('#statusDate').delegate('click', this.dateRange, 'a', this);
				};
				Y.all('#date input').on('blur', this.dateSearch, this);
			};

			var searchBox = Y.one('#search');
			var searchBox000 = Y.one('#search000');
			var searchBox0 = Y.one('#search0');
			if (searchBox0) {
				searchBox0.one('.btnSearch').on('click', this.idSearch, this);
			}
			if (searchBox || searchBox000) {
				var inputText;
				if (type === 'Verified') {
					inputText = searchBox000.one('.inputText');
				} else {
					inputText = searchBox.one('.inputText');
				}

				if (menu.inputTextStr) {
					inputText.set('value', menu.inputTextStr);
				}

				inputText.on('focus', function(e) {
					var val = e.target.get('value');
					if (val == '请输入手机号码或尾号' || val == '按菜品名 简拼 全拼搜索' || val == '请输入营业收入流水号' || val == '按配菜名搜索' || val == '请输入公司名称' || val == '请输入门店名称')
						e.target.set('value', '');
				});
				inputText.on('blur', function(e) {
					var t = e.currentTarget;
					var v = t.get('value'),
						defaultTxt;
					if (v === '') {
						if (type === 'currentSellOff' || type === 'dishList') {
							defaultTxt = '按菜品名 简拼 全拼搜索';
						} else if (type === 'Verified' || type === 'Confirmed') {
							defaultTxt = '请输入手机号码或尾号';
						} else if (type === 'companylist') {
							defaultTxt = '请输入公司名称';
						} else if (type === 'shoplist' || type === 'shophandle') {
							defaultTxt = '请输入门店名称';
						}
						t.set('value', defaultTxt);
					}
				});
				if (searchBox) {
					searchBox.one('.btnSearch').on('click', this.idSearch, this);
				}

				if (type === 'Verified') {
					validate.setLength('#search000', 11);
				} else if (type === 'Confirmed') {
					validate.setLength('#search', 11);
				} else if (type === 'currentSellOff' || type === 'dishList') {
					validate.setLength('#search', 50);
				}
			};

			var addPhone = Y.one('#addPhone');
			if (addPhone) {
				addPhone.one('.btnSearch').on('click', this.addPhoneHandler, this);
				var inputText = addPhone.one('.inputText');
				inputText.on('focus', function(e) {
					var val = e.target.get('value');
					if (val == '输入手机号码')
						e.target.set('value', '');

					inputText.on('focus', function(e) {
						var val = e.target.get('value');
						if (val == '请输入手机号码或尾号' || val == '按菜品名 简拼 全拼搜索' || val == '请输入营业收入流水号' || val == '按配菜名搜索' || val == '请输入公司名称' || val == '请输入门店名称')
							e.target.set('value', '');
					});
					inputText.on('blur', function(e) {
						var t = e.currentTarget;
						var v = t.get('value'),
							defaultTxt;
						if (v === '') {
							if (type === 'currentSellOff' || type === 'dishList') {
								defaultTxt = '按菜品名 简拼 全拼搜索';
							} else if (type === 'Verified' || type === 'Confirmed') {
								defaultTxt = '请输入手机号码或尾号';
							} else if (type === 'companylist') {
								defaultTxt = '请输入公司名称';
							} else if (type === 'shoplist' || type === 'shophandle') {
								defaultTxt = '请输入门店名称';
							} else if (type === 'accountDetails') {
								defaultTxt = '';
							}
							t.set('value', defaultTxt);
						}
					});
					searchBox.one('.btnSearch').on('click', this.idSearch, this);

					//inputText.on('blur', function (ev) {
					//var t = ev.currentTarget;
					//var v = t.get('value');
					//if (v == '') {
					//t.set('value', '输入手机号码');
					//}
					//})
				});
			}

			var hasOwnNode = function(node) {
				var n = Y.one(node);
				return n ? 0 : n;
			};

			var page = Y.one('#pageShow');
			if (page) {
				page.all('li a').on('click', this.pageSizeSet, this);
			}

			var s = Y.one('#status');
			if (s) {
				s.delegate('click', this.statusSearch, 'a', this);
			}

			var cb = Y.one('#cbAll');

			if (cb && type != 'currentSellOff') {
				cb.delegate('click', this.checkSubmit, '.btn', this);
			}
			var couponBack = Y.one('#couponTitle');
			if (couponBack)
				couponBack.on('click', this.checkSubmit, this);
			this.get('contentBox').delegate('click', this.checkSubmit, '.btn', this);

			if (Y.one(".btn-check-submit")) {
				Y.one('.btn-check-submit').delegate('click', this.checkSubmit, '.btn', this);
			};
			// change
			this.after('statusControllerChange', this.syncUI, this);
			this.after('dishTypeIdChange', this.syncUI, this);
			this.after('pageSizeChange', this.syncUI, this);
			this.after('startDayChange', this.syncUI, this);
			this.after('endDayChange', this.syncUI, this);
			this.after('pageIndexChange', this.syncUI, this);
			this.after('shopIdChange', this.syncUI, this);
		},
		syncUI: function() {
			var self = this,
				type = self.get('pageType');
			this.accountData = this.get('account'); //仅单次查询
			this.qs = this.get('queryString');

			this.pageSize = this.get('pageSize');
			this.pageIndex = this.get('pageIndex');
			this.shopId = this.get('shopId');
			this.companyId = this.get('companyId');
			this.inputTextStr = encodeURIComponent(this.get('inputTextStr'));
			this.statusController = this.get('statusController');
			this.startDay = this.get('startDay');
			this.endDay = this.get('endDay');
			this.dishTypeId = this.get('dishTypeId');
			if (menu.couponType) {
				this.couponType = menu.couponType;
			} else {
				this.couponType = 0;
			}

			var config = {};
			switch (type) {
				case 'accountTotal':
					config.dataStr = '{"PageSize":"' + this.pageSize + '","PageIndex":"' + this.pageIndex + '","shopId":"' + this.shopId + '","companyId":"' + this.companyId + '","datetimestart":"' + this.startDay + '","datetimeend":"' + this.endDay + '","couponType":"' + this.couponType + '"}';
					break;
				case 'accountDetails':
					config.dataStr = '{"PageSize":"' + this.pageSize + '","PageIndex":"' + this.pageIndex + '","shopId":"' + this.shopId + '","companyId":"' + this.companyId + '","datetimestart":"' + this.startDay + '","datetimeend":"' + this.endDay + '","type":"' + this.accountData.type + '","paystart":"' + this.accountData.amountMin + '","payend":"' + this.accountData.amountMax + '","mainkey":"' + this.inputTextStr + '","mobilePhoneNumber":"' + menu.accountData.mobilePhoneNumber + '","couponType":"' + this.couponType + '"}';
					break;

				case 'currentSellOff':
					config.dataStr = '{"PageSize":"' + this.pageSize + '","PageIndex":"' + this.pageIndex + '","shopId":"' + this.shopId + '","dishFilter":"' + this.inputTextStr + '","dishTypeId":"' + this.dishTypeId + '"}';
					break;
				case 'dishList':
					config.dataStr = '{"PageSize":"' + this.pageSize + '","PageIndex":"' + this.pageIndex + '","shopId":"' + this.shopId + '","dishname":"' + this.inputTextStr + '","dishtypeid":"' + this.dishTypeId + '"}'; //dishtypeid与dishTypeId
					break;
				case 'dishMix':
					config.dataStr = 'PageSize=' + this.pageSize + '&PageIndex=' + this.pageIndex + '&shopId=' + this.shopId + '&ingredientsname=' + this.inputTextStr;

					break;
				case 'configurationEmployees':
					config.dataStr = 'PageSize=' + this.pageSize + '&PageIndex=' + this.pageIndex + '&shopId=' + this.shopId;
					break;
				case 'companylist':
				case 'shoplist':
				case 'shophandle':
					config.dataStr = 'pageSize=' + this.pageSize + '&pageIndex=' + this.pageIndex + '&searchKeyWords=' + this.inputTextStr;

					break;
				case 'companyaccountlist':
				case 'companymenulist':
					config.dataStr = 'pageSize=' + this.pageSize + '&pageIndex=1&companyId=' + VA.argPage.qs.a;
					break;
				case 'shopsundrylist':
				case 'shopdiscountlist':
					config.dataStr = 'pageSize=' + this.pageSize + '&pageIndex=1&shopId=' + VA.argPage.qs.a;

					break;
					// 
				case 'increment':
					config.dataStr = 'm=shopChannelList&shopID=' + this.shopId;
					break;
				case 'incrementEdit':
					config.dataStr = 'm=dishList&shopChannelID=' + VA.argPage.qs.a;
					break;
				default:
					config.dataStr = '{"PageSize":"' + this.pageSize + '","PageIndex":"' + this.pageIndex + '","shopId":"' + this.shopId + '", "inputTextStr":"' + this.inputTextStr + '","approvedStatus":"' + this.statusController + '","preOrderTimeStr":"' + this.startDay + '","preOrderTimeEnd":"' + this.endDay + '","couponType":"' + this.couponType + '"}';
					break;
			};

			config.ioURL = this.get('ioURL');
			this.ioRequest(config, function(data) {
				self.dataSuccess(data);
			});
		}
	}, {
		ATTRS: {
			pageSize: {
				value: menu.pageSize
			},
			pageIndex: {
				value: menu.pageIndex
			},
			shopId: {
				value: 21
			},
			companyId: {
				value: 0
			},
			inputTextStr: {
				value: menu.inputTextStr
			},
			statusController: {
				value: menu.statusController
			},
			startDay: {
				value: menu.startDay
			},
			endDay: {
				value: menu.endDay
			},
			dishTypeId: {
				value: menu.dishTypeId
			},
			account: {
				valueFn: function() {
					if (this.get('pageType') == 'accountDetails') { // 规避 tabContent
						var accountData = new Object();
						accountData.type = menu.accountData.type;
						accountData.amountMin = menu.accountData.amountMin;
						accountData.amountMax = menu.accountData.amountMax;
						accountData.mobilePhoneNumber = menu.accountData.mobilePhoneNumber;
						accountData.datetimestart = menu.accountData.datetimestart;
						accountData.datetimeend = menu.accountData.datetimeend;
						return accountData;
					}
					if (this.get('pageType') == 'accountTotal') {
						var accountData = new Object();
						accountData.type = menu.accountData.type;
						accountData.amountMin = menu.accountData.amountMin;
						accountData.amountMax = menu.accountData.amountMax;
						accountData.mobilePhoneNumber = menu.accountData.mobilePhoneNumber;
						accountData.datetimestart = menu.accountData.datetimestart;
						accountData.datetimeend = menu.accountData.datetimeend;
						return accountData;
					}

				}
			},
			subPage: {
				valueFn: function() {
					var subPage = new Object();
					subPage.pageIndex = 1;
					return subPage;
				}
			},
			ioURL: {
				value: ""
			},
			dataTemp: {
				value: ''
			},
			pageType: {
				value: 'Confirmed'
			},
			queryString: {
				value: ''
			}
		}
	});
}, '1.1', {
	requires: ['base-build', 'event-base', 'event-valuechange', 'widget-base', 'datatable-base', 'loginModule', 'paginator-multi', 'json-stringify', 'datatype-date-format', 'popup-plugin', 'transition', 'panel']
});