/*//
标题：商户宝 UIController
来源：viewallow UI
日期：2013/10/22
//*/

VA.initPage.couponUsedDetail = function(){
		YUI({
			modules:{
				"CouponsChart":{
					fullpath:'scripts/modules/UICharts.js',
					requires: ['base-build','node-base','widget','io-base','json-parse','charts']
				}
			}
		}).use('login','loginModule','dataTablePack','couponsDetails-plugin','CouponsChart',function(Y){
			
			VA.renderPage = new Y.DataTableClass({pageType:'couponUsedDetail',contentBox:'#dataTable',companyId:VA.argPage.loginModule.get('companyId'),shopId:VA.argPage.loginId.get('id'),queryString:VA.argPage.qs,ioURL:'couponUsedDetail.aspx/ShopCouponUsedDetail'});
			VA.renderPage.plug(Y.Plugin.couponsDetails, { title: '[couponUsedDetail]' });
			VA.renderPage.render();
			
			var couponsChartModule = new Y.CouponsChartClass({queryString:VA.argPage.qs,ioURL:'couponUsedDetail.aspx/ShowCouponReportData'});
			couponsChartModule.render();
		});
		
	};



YUI.add('couponsDetails-plugin', function (Y) {
    Y.Plugin.couponsDetails = Y.Base.create('couponsDetailsPlugin', Y.Plugin.Base, [], {
        initializer: function () {
            if (this.get('rendered')) {
                this.addData();
            } else {
                this.afterHostMethod('dataSuccess', this.addData);
            }
        },
        destructor: function () {
            //this.titleNode.remove(true);
        },
        showChart: function () {
            //
        },
        addData: function () {
            var self = this;
            host = this.get('host');
            var contentBox = host.get('contentBox'),
				data = [],
				d = host.get('dataTemp'),
				pageType = host.get('pageType');
            var couponName = d.CouponNameAndStatus[0].couponName;
			var headerTitle = Y.one('#couponTitle');
            headerTitle.set('text', couponName);
            var url = UIBase.addURIParam('couponsActivitiesManage.aspx', 'nyStatusController', host.get('statusController'));
            //url = UIBase.addURIParam(url, 'nyPageIndex', host.get('pageIndex'));
			headerTitle.set('href',url);//生成动态上级路径
			
			if(d.TableJson){ 
				for (var i = 0; i < d.TableJson.length; i++) {
					data.push({
						"eCardNumber": d.TableJson[i].eCardNumber,
						"useTime": d.TableJson[i].useTime,
						"preOrder19dianId": d.TableJson[i].preOrder19dianId,
						"preOrderServerSum": d.TableJson[i].preOrderServerSum,
						"btn": ''
					});
				};
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
                return '<a class="btn detail" href="couponPreOrderDetail.aspx?a=' + o.data.preOrder19dianId + '&b=' + host.qs.b + '&pageIndex=' + host.get('pageIndex') + '">查看详情</a>';
            };
            var table = new Y.DataTable({
                columns: [{
                    key: 'eCardNumber', label: '用户帐号', formatter: sliceHandler
                }, {
                    key: 'useTime', label: '使用时间', formatter: sliceDateHandler
                }, {
                    key: 'preOrder19dianId', label: '流水号'
                }, {
                    key: 'preOrderServerSum', label: '订单金额'
                }, {
                    key: 'btn', label: '详情', formatter: btnHandler, allowHTML: true
                }],
                data: data,
                strings: { emptyMessage: '暂无数据显示', loadingMessage: '数据加载中' }
            });
            contentBox.empty(true);
            table.render(contentBox);
        }
    }, {
        NS: 'commonTable',
        ATTRS: {
            title: { value: '' }
        }
    });

}, '1.0', { requires: ['base-build', 'plugin', 'datatable-base', 'datatable-message'] });

