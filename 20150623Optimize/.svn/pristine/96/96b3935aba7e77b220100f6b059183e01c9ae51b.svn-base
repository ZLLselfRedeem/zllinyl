/*//
标题：商户宝 UIController
来源：viewallow UI
日期：2013/10/22
//*/

VA.initPage.currentSellOff = function(){
		YUI().use('login','loginModule','dataTablePack','sellOffCommon-plugin','sellOffBeen-plugin','CatesSelect-plugin','cb-plugin','json-parse',function(Y){
			VA.renderPage = new Y.DataTableClass({pageType:'currentSellOff',contentBox:'#dataTable',shopId:VA.argPage.loginId.get('id'),ioURL:'currentSellOff.aspx/ShopCommonCurrentSellOffInfo'});
			VA.renderPage.plug(Y.Plugin.Cb,{ btnTitleMutil:'批量取消',btnTitleAll:'全部取消' });
			VA.renderPage.plug([Y.Plugin.CatesSelect,Y.Plugin.SellOffCommon,Y.Plugin.SellOffBeen]);
			VA.renderPage.render();
		});
		
	};

YUI.add('sellOffCommon-plugin', function (Y) {
    Y.Plugin.SellOffCommon = Y.Base.create('sellOffCommonPlugin', Y.Plugin.Base, [], {
        initializer: function () {
            if (this.get('rendered')) {
                this.addData();
            } else {
                this.afterHostMethod('dataSuccess', this.addData);
            }
        },
        destructor: function () {
            this.titleNode.remove(true);
        },
        addData: function () {
            this.hostObj = this.get('host');
            var contentBox = this.hostObj.get('contentBox'),
				data = [],
				d = this.hostObj.get('dataTemp'),
				pageType = this.hostObj.get('pageType');

            for (var i = 0; i < d.TableJson.length; i++) {
                data.push({
                    "DishName": d.TableJson[i].DishName,
                    "ScaleName": d.TableJson[i].ScaleName,
                    "DishPrice": d.TableJson[i].DishPrice,
                    "DishI18nID": d.TableJson[i].DishI18nID,
                    "DishPriceI18nID": d.TableJson[i].DishPriceI18nID,
                    "isOrNotSellOff": d.TableJson[i].isOrNotSellOff,
                    "btn": ''
                });
            };
            var sliceHandler = function (o) {
                if (o.value.length > 10) {
                    var trunc = o.value.slice(0, 10) + '...';
                    return trunc;
                }
                return o.value;

            };
            var btnHandler = function (o) {
                if (o.data.isOrNotSellOff == "1"){
                    return '<a class="btn btnBeen" preId="' + o.data.DishI18nID + '"  preId2="' + o.data.DishPriceI18nID + '" href="javascript:;">已沽清</a>';
				}
                else{
                    return '<a class="btn btnSoon" preId="' + o.data.DishI18nID + '"  preId2="' + o.data.DishPriceI18nID + '" href="javascript:;">沽清</a>';
				}
            };
            var table = new Y.DataTable({
                columns: [{
                    key: 'DishName', label: '名称', formatter: sliceHandler
                }, {
                    key: 'ScaleName', label: '规格', formatter: sliceHandler
                }, {
                    key: 'DishPrice', label: '价格', formatter: sliceHandler
                }, {
                    key: 'btn', label: '操作', formatter: btnHandler, allowHTML: true
                }],
                data: data,
                strings: { emptyMessage: '暂无数据显示', loadingMessage: '数据加载中' }
            });
            contentBox.empty(true);
            table.render(contentBox);

        }
    }, {
        NS: 'sellOffCommonPlugin',
        ATTRS: {
            title: { value: '' }
        }
    });

}, '1.0', { requires: ['base-build', 'plugin', 'datatable-base', 'datatable-message'] });



//已沽清
YUI.add('sellOffBeen-plugin', function (Y) {
    Y.Plugin.SellOffBeen = Y.Base.create('sellOffBeenPlugin', Y.Plugin.Base, [], {
        initializer: function () {
			this.initBind();
            if (this.get('rendered')) {
                this.addData();
            } else {
                this.afterHostMethod('dataSuccess', this.addData);
            }
            this.after('pageIndexChange', this.addData);
            Y.all('#pageShow li a').on('click', this.pageSizeSet, this);
        },
        destructor: function () {
            //
        },
		initBind:function(){
			var host = this.get('host');
			this.cbListener = Y.one('#cbAll').delegate('click', host.checkSubmit,'.btn', host);
			Y.one('#sellOffBeen .dataSprite').delegate('click', host.checkSubmit, '.btn', host);
		},	
		unBind:function(){
			this.cbListener.detach();
		},
        pageSizeSet: function (e) {
            this.set('pageIndex', 1);
        },
        pageSearch: function (e) {
            var index = parseInt(e.currentTarget.getAttribute('pageindex'), 10);
            this.set('pageIndex', index);
        },
        switchTo: function (e) {
            e.preventDefault();
            e.stopPropagation();
            var index = parseInt(Y.one('#sellOffBeen #switchTo .inputText').get('value'), 10);
			if(index<=this.pageCount){
				Y.one('#pageSellOffBeen .cur').setAttribute('pageindex', index);
				this.set('pageIndex', index);
			}else if (index) {
				VA.Singleton.popup.panel.set('headerContent','提示信息');
				VA.Singleton.popup.panel.set('bodyContent','跳转页不能大于当前最大页 ' + this.pageCount+' 页');
				VA.Singleton.popup.panel.set('buttons',[VA.Singleton.popup.get('okButton')]);
				VA.Singleton.popup.showPanel();
                return;
            }
        },
        pageController: function (pageCount, recordCount) {
            var pageContentTemplate = ' <div class="page">'
            //分页数字按扭
									+ '</div>'
									+ '<ul class="pageTotal" id="switchTo">'
									+ '	<li class="txt page-size">'
									+ '		共<em>{pageCount}</em>页'
									+ '	</li>'
									+ '	<li class="txt page-index">'
									+ '		<input type="text" class="inputText" />'
									+ '	</li>'
									+ '	<li class="txt">'
									+ '		<a class="btn" href="#">跳至</a>'
									+ '	</li>'
									+ '</ul>';

            var pageItemes = { pageCount: pageCount };
			if(!Y.one('#pageSellOffBeen')){
				return;
			}
            Y.one('#pageSellOffBeen').set('innerHTML',
				Y.Lang.sub(pageContentTemplate, pageItemes)
			);
            var paginatorController = new Y.PaginatorController({ contentBox: '#pageSellOffBeen .page', pageIndex: this.pageIndex, recordCount: recordCount, pageSize: this.pageSize ,numericButtonCount:1});
            paginatorController.render();
            Y.one('#pageSellOffBeen #switchTo li .btn').on('click', this.switchTo, this);
            //bind
            Y.one('#pageSellOffBeen .page').delegate('click', this.pageSearch, 'a', this);
        },
        getData: function (d) {
            var d = d,
				data = [];
			var contentBox = Y.one('#sellOffBeen .dataTable');
			if(d){ 
				for (var i = 0; i < d.TableJson.length; i++) {
					data.push({
						"DishName": d.TableJson[i].DishName,
						"ScaleName": d.TableJson[i].ScaleName,
						"DishPrice": d.TableJson[i].DishPrice,
						"DishI18nID": d.TableJson[i].DishI18nID,
						"DishPriceI18nID": d.TableJson[i].DishPriceI18nID,
						"btn": ''
					});
				};
				var recordCount = d.total[1].ocount;
			    this.pageCount = Math.ceil(d.total[1].ocount / this.pageSize);
				this.pageController(this.pageCount, recordCount);
			}else{
				var pageIndex = this.get('pageIndex');
				if(pageIndex>1){
					this.set('pageIndex',(pageIndex-1));
					return;
				}else{
					this.pageController(0, 0);
				}
				
			}	
			
            var cbHandler = function (o) {
                if (o.value.length > 5) {
                    var trunc = o.value.slice(0, 5) + '...';
                } else {
                    var trunc = o.value;
                }
                return '<span class="inputSprite"><input class="inputCheck" type="checkbox" name="cb" preid="' + o.data.DishI18nID + '"  preid2="' + o.data.DishPriceI18nID + '" />' + trunc + '</span>';
            };
            var sliceHandler = function (o) {
                if (o.value.length > 10) {
                    var trunc = o.value.slice(0, 10) + '...';
                    return trunc;
                }
                return o.value;

            };
            var btnHandler = function (o) {//
                return '<a class="btn cancel" preid="' + o.data.DishI18nID + '"  preid2="' + o.data.DishPriceI18nID + '" href="javascript:;">取消沽清</a>';
            };
            var table = new Y.DataTable({
                columns: [{
                    key: 'DishName', label: '名称', formatter: cbHandler, allowHTML: true
                }, {
                    key: 'ScaleName', label: '规格', formatter: sliceHandler
                }, {
                    key: 'DishPrice', label: '价格', formatter: sliceHandler
                }, {
                    key: 'btn', label: '操作', formatter: btnHandler, allowHTML: true
                }],
                data: data,
                strings: { emptyMessage: '暂无数据显示', loadingMessage: '数据加载中' }
            });
            
            contentBox.empty(true);
            table.render(contentBox);
            new Y.SelectAllCheckboxGroup('#checkAll', '.inputCheck');
        },
        addData: function () {
            var self = this;
            this.hostObj = this.get('host');
            this.pageSize = this.hostObj.get('pageSize'),
			this.shopId = this.hostObj.get('shopId'),
			this.pageIndex = this.get('pageIndex');

            var dataHandler = {
                method: 'POST',
                data: '{"PageSize":"' + this.pageSize + '","PageIndex":"' + this.pageIndex + '","shopId":"' + this.shopId + '"}',
                headers: { 'Content-Type': 'application/json; charset=utf-8' },
                on: {
                    success: function (id, rsp) {
                        var result = Y.JSON.parse(rsp.responseText),
							d = (result.d)?Y.JSON.parse(result.d):0;
						if(result.d==''){
							Y.all('#cbAll .btn').addClass('disabled');
							self.unBind();
						}else{
							Y.all('#cbAll .btn').removeClass('disabled');
							var host = self.get('host');
							if(self.cbListener){
								self.unBind();
							}
							self.cbListener = Y.one('#cbAll').delegate('click', host.checkSubmit,'.btn', host);
						};
                        self.getData(d);
                    },
                    failure: function (id, rsp) {
                        Y.log(rsp.status);
                    }
                },
                sync: true
            };
            Y.io('currentSellOff.aspx/ShopAllCurrentSellOffInfo', dataHandler);
        }
    }, {
        NS: 'sellOffBeenPlugin',
        ATTRS: {
            pageIndex: {
                value: 1
            }
        }
    });

}, '1.0', { requires: ['base-build', 'plugin', 'datatable-base', 'datatable-message', 'io-base', 'paginator-multi', 'gallery-checkboxgroups','event-tap'] });

