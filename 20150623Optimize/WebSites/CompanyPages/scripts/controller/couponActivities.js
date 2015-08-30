/*//
标题：商户宝 UIController
来源：viewallow UI
日期：2013/10/22
//*/

VA.initPage.couponsActivitiesManage = function(){
		YUI().use('login', 'loginModule', 'dataTablePack', 'transition', 'coupons-plugin', function (Y) {
            VA.renderPage = new Y.DataTableClass({ pageType: 'couponsActivitiesManage', contentBox: '#dataTable',queryString:VA.argPage.qs,companyId:VA.argPage.loginModule.get('companyId'), shopId:VA.argPage.loginId.get('id'), ioURL: 'couponsActivitiesManage.aspx/ShopCouponInfo' });
            VA.renderPage.plug(Y.Plugin.Coupons, { title: '[couponsActivitiesManage]' });
            VA.renderPage.render();
        });	
		
	}


YUI.add('coupons-plugin', function (Y) {
    Y.Plugin.Coupons = Y.Base.create('couponsPlugin', Y.Plugin.Base, [], {
        initializer: function () {
			this.initBind();
            if (this.get('rendered')) {
                this.addData();
            } else {
                this.afterHostMethod('dataSuccess', this.addData);
            }
        },
        destructor: function () {
            this.titleNode.remove(true);
        },
		initBind:function(){
			//
		},
        addData: function () {
            var host = this.get('host');
            var contentBox = host.get('contentBox'),
				data = [],
				d = host.get('dataTemp');
            for (var i = 0; i < d.TableJson.length; i++) {
                data.push({
                    "couponName": d.TableJson[i].couponName,
                    "couponDesc": d.TableJson[i].couponDesc,
                    "GrantingDate": d.TableJson[i].GrantingDate,
                    //	useDate			
                    "provided": d.TableJson[i].provided,
                    "used": d.TableJson[i].used,
                    "recovery": d.TableJson[i].recovery+'%',
                    "sumAmount": d.TableJson[i].sumAmount,
                    "couponId": d.TableJson[i].couponID,
                    //"Encouragetype":d.TableJson[i].Encouragetype,
                    "btnNote": ''
                });
            };
            var sliceHandler = function (o) {
                if (o.value.length > 10) {
                    var trunc = o.value.slice(0, 10) + '...';
                    return trunc;
                }
                return o.value;

            };
            var sliceDateHandler = function (o) {
                if (o.value.length > 22) {
                    var trunc = o.value.slice(0, 22) + '...';
                    return trunc;
                }
                return o.value;
            };
            var btnHandler = function (o) {//
                var url = UIBase.addURIParam('couponUsedDetail.aspx', 'b', o.data.couponId);
				//url = UIBase.addURIParam(url, 'nyStatusController', host.get('statusController'));
                //url = UIBase.addURIParam(url, 'nyPageIndex', host.get('pageIndex'));
                return '<a class="btn detail singleBtn" href="' + url + '">查看详情</a>';
            };
            var table = new Y.DataTable({
                columns: [{
                    key: 'couponName', label: '优惠券标题', formatter: sliceHandler
                }, {
                    key: 'couponDesc', label: '优惠券内容'//,formatter: sliceHandler
                }, {
                    //key:'GrantingDate',label:'发放起止日期',formatter: sliceDateHandler
                    key: 'GrantingDate', label: '发放起止日期'
                }, {
                    key: 'provided', label: '已发放'
                }, {
                    key: 'used', label: '已使用'
                }, {
                    key: 'recovery', label: '回收率'
                }, {
                    key: 'sumAmount', label: '总金额'
                }, {
                    key: 'btnNote', label: '详细记录', formatter: btnHandler, allowHTML: true
                }],
                data: data,
                strings: { emptyMessage: '暂无数据显示', loadingMessage: '数据加载中' }
            });
            table.set('strings.loadingMessage', "数据加载中");
            contentBox.empty(true);
            table.render(contentBox);
        }
    }, {
        NS: 'couponsPlugin',
        ATTRS: {
            title: { value: '' }
        }
    });

}, '1.0', { requires: ['base-build', 'plugin', 'datatable-base', 'datatable-message'] });
