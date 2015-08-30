/*//
 标题：商户宝 UIController
 来源：viewallow UI
 日期：2013/10/22
 //*/

//VA.Singleton.initPage = { 
VA.initPage.preOrderShopConfirmed = function () {
    YUI({
        groups: {
            'UICalendar': {
                base: 'scripts/calendar/',
                async: false,
                modules: {
                    'cQuery': {
                        fullpath: 'scripts/public/cQuery.js'
                    },
                    'calendar-3.0': {//
                        path: 'calendar-3.0.js',
                        requires: ['cQuery']
                    },
                    'calendarCquery-plugin': {
                        path: 'UICalendar.js',
                        requires: ['base-build', 'plugin', 'cQuery', 'calendar-3.0']
                    }
                }
            },
            'UIPopup': {
                async: true,
                modules: {
                    'popup': {
                    }
                }
            }
        }
    }).use('login', 'loginModule', 'dataTablePack', 'commonTable-plugin', 'calendarCquery-plugin', 'datatype', 'event-move', function (Y) {
        var dataConfig = {
            pageType: 'Confirmed',
            contentBox: '#dataTable',
            shopId: VA.argPage.loginId.get('id'),
            queryString: VA.argPage.qs,
            components: { "a": 0 },
            ioURL: 'preOrderShopConfirmed.aspx/ShowPageConfirmedInfo'
        };
        VA.renderPage = new Y.DataTableClass(dataConfig);
        VA.renderPage.plug([Y.Plugin.CommonTable, Y.Plugin.CalendarCquery]);
        VA.renderPage.render();
    });
}

YUI.add('commonTable-plugin', function (Y) {
    Y.Plugin.CommonTable = Y.Base.create('commonTablePlugin', Y.Plugin.Base, [], {
        initializer: function () {
            if (this.get('rendered')) {
                this.addData();
            } else {
                this.afterHostMethod('dataSuccess', this.addData);
            }
            this.setRefresh();
        },
        destructor: function () {
            this.titleNode.remove(true);
        },
        setRefresh: function () {
            var that = this;
            that.setRefreshRun();

            // 数据元素上移动鼠标，延迟；单出退款，清除，完成退款，重新侦听
            Y.one('.pageComfirmed').on('mouseup', function () {
                //Y.one('#contentContainer').on('gesturemove',function(e){
                window.clearInterval(that.refreshTimer);
                that.setRefreshRun();

            }, this);
            Y.one('#header').on('mouseup', function () {
                window.clearInterval(that.refreshTimer);
            }, this);
            Y.one('#quick_nav').on('mouseup', function () {
                window.clearInterval(that.refreshTimer);
            }, this);
        },
        setRefreshRun: function () {
            var host = this.get('host'),
                that = this;
            var timerMax = 10;
            that.refreshTimer = window.setInterval(function () {
                if (timerMax-- <= 0) {
                    host.pageChange();
                    host.syncUI();
                    window.clearInterval(that.refreshTimer);
                    that.setRefreshRun();
                }
            }, 1000);
        },
        addData: function () {
            var host = this.get('host');
            var contentBox = host.get('contentBox'),
                data = [],
                d = host.get('dataTemp');
            for (var i = 0; i < d.TableJson.length; i++) {
              var discount = "";
              if( d.TableJson[i].RequirementMoney === "" && d.TableJson[i].DeductibleAmount === "" && d.TableJson[i].MaxAmount === "" ){
                discount = "无";
              }else{
                discount = "每满"+d.TableJson[i].RequirementMoney+"减"+d.TableJson[i].DeductibleAmount+"，最多减"+d.TableJson[i].MaxAmount
              }
                data.push({
                    "preOrder19dianId": d.TableJson[i].preOrder19dianId,
                    "UserName": d.TableJson[i].UserName,
                    "mobilePhoneNumber": d.TableJson[i].mobilePhoneNumber,
                    "prePayTime": d.TableJson[i].prePayTime, //支付时间

                    "prePaidSum": d.TableJson[i].prePaidSum + getRefundMoneySum(d.TableJson[i].refundMoneySum),
                    "isStatus": d.TableJson[i].isShopConfirmed, //状态 入座 null0|1
                    "isVerified": d.TableJson[i].isApproved,
                    "orderId": d.TableJson[i].OrderId,
                    "discount": discount,
                    "btn": ''
                });
            }
            ;

            function getRefundMoneySum(moneyRefund) {
                if (moneyRefund == 0 || moneyRefund == '') {
                    return '';
                } else {
                    return "<span style='color: red' ><br/>(已退" + moneyRefund + ")</span>";
                }
            }

            var statusHandler = function (o) {
                if (o.value != '0' && !!o.value) { //状态 入座 null0|1
                    return '<span class="status been">已入座</span>';

                } else {
                    return '<span class="status">未入座</span>';
                }
            };
            var btnHandler = function (o) {//
                var url = UIBase.addURIParam('preOrderConfirmedDetail.aspx', 'a', o.data.orderId);
                //url = UIBase.addURIParam(url, 'nyStatusController', host.get('statusController'));
                //url = UIBase.addURIParam(url, 'nyPageIndex', host.get('pageIndex'));
                if (o.data.isStatus === '1') {
                    var disabled = ' ';
                    if (o.data.isVerified === '1') {
                        disabled = ' disabled';
                    }
                    return '<div class="nav"><a class="btn confirm cancel" href="javascript:void(0);" itemid="' + o.data.orderId + '"><div class="popupSprite"></div>取消入座</a><a class="btn refund' + disabled + '" href="javascritpt:;" itemid="' + o.data.orderId + '">退款</a><a class="btn detail" href="' + url + '">查看详情</a><a itemid="' + o.data.orderId + '" class="btn printer" href="javascript:;">打印</a></div>';
                } else {
                    return '<div class="nav"><a class="btn confirm" href="javascript:void(0);" itemid="' + o.data.orderId + '"><div class="popupSprite"></div>入座</a><a class="btn refund disabled" href="javascritpt:;" itemid="' + o.data.orderId + '">退款</a><a class="btn detail" href="' + url + '">查看详情</a><a itemid="' + o.data.orderId + '" class="btn printer" href="javascript:;">打印</a></div>';
                }
                //
            };
            var table = new Y.DataTable({
                columns: [{
                    key: 'preOrder19dianId', label: '流水号'
                }, {
                    key: 'UserName', label: '用户昵称'
                }, {
                    key: 'mobilePhoneNumber', label: '手机号码'
                }, {
                    key: 'prePayTime', label: '支付时间'
                }, {
                    key: 'prePaidSum', label: '支付金额', allowHTML: true
                }, {
                    key: 'discount', label: '抵扣券'
                }, {
                    key: 'isStatus', label: '状态', formatter: statusHandler, allowHTML: true
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
        NS: 'commonTable',
        ATTRS: {
            title: { value: '' },
            refreshTimer: {}
        }
    });

}, '1.0', { requires: ['base-build', 'event-move', 'plugin', 'datatable-base', 'datatable-message'] });


