/*/
 /
标题：商户宝 UIController
来源：viewallow UI
日期：2013 / 10 / 22
//*/

VA.initPage.incrementEdit = function () {
	YUI().use('login', 'loginModule', 'dataTablePack', 'incrementEdit-plugin', 'cb-plugin', 'json-parse', function (Y) {
		VA.renderPage = new Y.DataTableClass({
				pageType : 'incrementEdit',
				contentBox : '#dataTable',
				shopId : VA.argPage.loginId.get('id'),
				queryString: VA.argPage.qs,
				ioURL : 'ajax/doSybChannel.ashx'
			});
		VA.renderPage.plug(Y.Plugin.Cb, {
			btnTitleMutil : '批量删除',
			btnTitleAll : '全部删除'
		});
		VA.renderPage.plug([Y.Plugin.incrementEdit]);
		VA.renderPage.render();
	});

};

//增值列表处理
YUI.add('incrementEdit-plugin', function (Y) {
	Y.Plugin.incrementEdit = Y.Base.create('incrementEditPlugin', Y.Plugin.Base, [], {
			initializer : function () {
				this.initBind();
				if (this.get('rendered')) {
					this.addData();
				} else {
					this.afterHostMethod('dataSuccess', this.addData);
				}
			},
			destructor : function () {
				//
			},
			initBind : function () {
				var host = this.get('host');
				this.cbListener = Y.one('#cbAll').delegate('click', host.checkSubmit, '.btn', host);
				
				this.disSubmitListener = Y.one('document').delegate('click', this.saveAndBackSubmit, '.btn-save-back-submmit a', this);
				Y.one("#saveSort").on("click",function(){
					this.incrementEditSortSaveHandler(function(){
						function okHandler(){}
						VA.Singleton.popup.panel.set('headerContent', '增值管理');
						VA.Singleton.popup.panel.set('bodyContent', '设置排序保存成功！');
						VA.Singleton.popup.panel.set('buttons', [VA.Singleton.popup.get('okButton')]);
						VA.Singleton.popup.set('ok', okHandler);
						VA.Singleton.popup.showPanel();
						
					});
				},this);
				Y.one('#addIncrementDish').on('click', function(e){
					e.preventDefault();
					e.stopPropagation();
					var r = this.getRouteParams(e.currentTarget.getAttribute('href'));
					// isNoSave
					menu.prevPage(r.aspx, r.m);
				}, this);
			},
			unBind : function () {
				this.cbListener.detach();
			},
			
			addData : function () {
				var self = this;
				self.isNoSave = true;
				var host = this.get('host');
				var contentBox = host.get('contentBox'),
					d = host.get('dataTemp'),
					data = [];
				var dishIndexNext = 1;
				if (d=='') {
					Y.all('#cbAll .btn').addClass('disabled');
					Y.one("#tipsTotal").setHTML("共计: 0 个菜品");
					self.unBind();
				} else {
					for (var i = 0; i < d.TableJson.length; i++) {
						var itemTemp = d.TableJson[i];
						data.push({
							"DishName": itemTemp.dishName,
							"DishPrice": itemTemp.dishPrice,
							"DishI18nID": itemTemp.id,
							"btn": ''
						});
						if(dishIndexNext<itemTemp.dishIndex){
							dishIndexNext = itemTemp.dishIndex;
						}
					};
					
					Y.all('#cbAll .btn').removeClass('disabled');
					Y.one(".save-sort").setStyle("display","inline-block");
					
					var host = self.get('host');
					if (self.cbListener) {
						self.unBind();
					}
					self.cbListener = Y.one('#cbAll').delegate('click', host.checkSubmit, '.btn', host);
					
					Y.one("#tipsTotal").setHTML("共计: "+d.TableJson.length+" 个菜品");
				};
				var cbHandler = function (o) {
					if (o.value.length > 16) {
						var trunc = o.value.slice(0, 16) + '...';
					} else {
						var trunc = o.value;
					}
					return '<span class="inputSprite"><input class="inputCheck" type="checkbox" name="cb" preid="' + o.data.DishI18nID + '" />' + trunc + '</span>';
				};
				var sliceHandler = function (o) {
					if (o.value.length > 16) {
						var trunc = o.value.slice(0, 16) + '...';
						return trunc;
					}
					return o.value;

				};
				var btnHandler = function(o) {
					var url = UIBase.addURIParam('incrementManage.aspx', 'type', 'incrementEdit');
					url = UIBase.addURIParam(url, 'm', 'edit');
					url = UIBase.addURIParam(url, 'a', host.get('queryString').a);// 频道id
					url = UIBase.addURIParam(url, 'sid', host.get('shopId'));
					url = UIBase.addURIParam(url, 'vid', o.data.DishI18nID);
					url = UIBase.addURIParam(url, 'name', o.data.DishName);
					url = UIBase.addURIParam(url, 'price', o.data.DishPrice);
					url = UIBase.addURIParam(url, 'ind', dishIndexNext);

					return '<div class="btn-increment-sprite">'
						+ '	<a class="btn editor" href="'+url+'">修改</a>'
						+ '	<a class="btn delete" preid="' + o.data.DishI18nID + '" href="javascript:;">删除</a>'
						+ '</div>';
				};
				
				var table = new Y.DataTable({
					columns: [{
								key : 'DishName',
								label : '名称',
								formatter : cbHandler,
								allowHTML : true
							}, {
						key: 'DishPrice', label: '现价/￥', formatter: sliceHandler
						},{
						key: 'btn', label: '操作', formatter: btnHandler, allowHTML: true
						}],
					data: data,
					strings: {emptyMessage: '暂无数据显示', loadingMessage: '数据加载中'}
				});
				contentBox.empty(true);
				table.render(contentBox);
				
				new Y.SelectAllCheckboxGroup('#checkAll', '.inputCheck');
				var sortable = new Y.Sortable({
					container: '#dataTable .yui3-datatable-data',
					nodes: 'tr',
					opacity: '.1'
				});
				
				Y.one("#addIncrementDish").set("href","incrementManage.aspx?type=incrementEdit&m=add&a="+host.get('queryString').a+"&sid="+host.get('shopId')+"&ind="+dishIndexNext);
			},
			dataSubmit : function (arg) {
				var hostObj = this.get("host");
				var config = {};
				config.ioURL = 'ajax/doSybChannel.ashx';
				config.dataStr = 'm=ShopChannelDishRelease&shopChannelID=' + hostObj.get('queryString').a;
				config.headers = {
					'Content-Type' : 'application/x-www-form-urlencoded; charset=utf-8'
				};
				config.sync = false;
				var confirmHandler = function () {};
				hostObj.ioRequest(config, function (data) {
					if (data.list[0].status === 1) {
						VA.Singleton.popup.panel.set('headerContent', '增值管理');
						VA.Singleton.popup.panel.set('bodyContent', '已成功保存并发布了！');
						VA.Singleton.popup.panel.set('buttons', [VA.Singleton.popup.get('okButton')]);
						VA.Singleton.popup.set('ok', confirmHandler);
						VA.Singleton.popup.showPanel();
					} else {
						VA.Singleton.popup.panel.set('headerContent', '提示信息');
						VA.Singleton.popup.panel.set('bodyContent', data.list[0].info);
						VA.Singleton.popup.panel.set('buttons', [VA.Singleton.popup.get('okButton')]);
						VA.Singleton.popup.set('ok', confirmHandler);
						VA.Singleton.popup.showPanel();
					}
				});
				
			},
			saveAndBackSubmit : function (e) {
				e.preventDefault();
				e.stopPropagation();

				var self = this;
				var hostObj = this.get("host");
				var targetItem = e.currentTarget;
				var eClass = targetItem.getAttribute('class'),
				// isCancel = eClass.indexOf('cancel')>-1,
				isSubmit = eClass.indexOf('submit') > -1,
				isBack = eClass.indexOf('back') > -1,
				isSaveBck = eClass.indexOf('save') > -1,
				isMutilNext = eClass.indexOf('mutil-next') > -1;
				var hrefStr = targetItem.getAttribute('href');

				Y.all('.required .tip').each(function (o) {
					o.setStyle('display', 'none');
				});
				
				if (isSubmit) {
					self.dataSubmit("isSubmit");
					self.isNoSave = false;
				} else if (isBack) {
					var cancelHandler = confirmHandler = function () {};
					var okHandler = function () {
						self.disSubmitListener.detach();
						
						var config = {};
						config.ioURL = 'ajax/doSybChannel.ashx';
						config.dataStr = 'm=noPublicDelete&shopChannelID=' + hostObj.get('queryString').a;
						config.headers = {
							'Content-Type' : 'application/x-www-form-urlencoded; charset=utf-8'
						};
						config.sync = false;
						hostObj.ioRequest(config, function (data) {
							if (data.list[0].status === 1) {
								
							}
						});
						
						var r = self.getRouteParams(hrefStr);
						menu.prevPage(r.aspx, r.m);
					}
					VA.Singleton.popup.panel.set('headerContent', '增值管理');
					VA.Singleton.popup.panel.set('bodyContent', '当前未保存，您确认放弃保存吗？');
					VA.Singleton.popup.set('ok', okHandler);
					VA.Singleton.popup.set('cancel', cancelHandler);
					VA.Singleton.popup.panel.set('buttons', [VA.Singleton.popup.get('okButton'), VA.Singleton.popup.get('cancelButton')]);
					if(self.isNoSave){
						VA.Singleton.popup.showPanel();
					}else{
						okHandler();
					}
					return;
				} else if (isSaveBck) { //菜品编辑
					self.dataSubmit("isSaveBck");
				};
			},
			incrementEditSortSaveHandler:function(callback){
				var hostObj = this.get("host");
				var sortStr = '';
				Y.all("#dataTable .inputCheck").each(function(elem, ind){
					sortStr += elem.getAttribute('preid')+','+(ind+1)+';';
				});
				sortStr = sortStr.substr(0, sortStr.length-1);
				
				var cancelHandler = confirmHandler = function () {};
				var okHandler = function () {
					var config = {};
					config.ioURL = 'ajax/doSybChannel.ashx';
					config.dataStr = 'm=dishSort&dishSort=' + sortStr;
					config.headers = {
						'Content-Type' : 'application/x-www-form-urlencoded; charset=utf-8'
					};
					config.sync = false;
					hostObj.ioRequest(config, function (data) {
						if (data.list[0].status === 1) {
							callback();
						} else {
							VA.Singleton.popup.panel.set('headerContent', '提示信息');
							VA.Singleton.popup.panel.set('bodyContent', data.list[0].info);
							VA.Singleton.popup.panel.set('buttons', [VA.Singleton.popup.get('okButton')]);
							VA.Singleton.popup.set('ok', confirmHandler);
							VA.Singleton.popup.showPanel();
						}
					});
				};
				okHandler();
			},
			
			getRouteParams : function (str) {
				var m = menu.getType(str),
				aspx = m + ".aspx";
				VA.argPage.qs = UIBase.getNyQueryStringArgs(str);
				return {
					"aspx" : aspx,
					"m" : m
				};
			}
		}, {
			NS : 'incrementEditPlugin',
			ATTRS : {
				pageIndex : {
					value : 1
				}
			}
		});

}, '1.0', {
	requires : ['base-build', 'plugin', 'datatable-base', 'datatable-message', 'io-base', 'paginator-multi', 'gallery-checkboxgroups', 'event-tap','sortable']
});
