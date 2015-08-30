/*//
标题：商户宝 UIController
来源：viewallow UI
日期：2013/10/22
//*/

VA.initPage.preOrderShopVerified = function(){
		YUI({ 
			groups: {
				'UICalendar': {
					base: 'scripts/calendar/',
					async: false,
					modules:{
						'cQuery':{ 
							fullpath:'scripts/public/cQuery.js'
						},
						'calendar-3.0': {//
							path: 'calendar-3.0.js',
							requires: ['cQuery']
						},
						'calendarCquery-plugin': {
							path: 'UICalendar.js',
							requires: ['base-build','plugin','cQuery','calendar-3.0']
						}
					}
				}
			}
		}).use('login','loginModule','dataTablePack','VerifiedTable-plugin','cb-plugin','transition','calendarCquery-plugin','datatype',function(Y){
			VA.renderPage = new Y.DataTableClass({pageType:'Verified',srcNode:'#dataTable',shopId:VA.argPage.loginId.get('id'),queryString:VA.argPage.qs,ioURL:'preOrderShopVerified.aspx/ShowPageVerifiedInfo'});
			VA.renderPage.plug([Y.Plugin.VerifiedTable,Y.Plugin.CalendarCquery]);
			VA.renderPage.render();
		});	
	};

YUI.add('VerifiedTable-plugin', function (Y) {
    Y.Plugin.VerifiedTable = Y.Base.create('verfiedTablePlugin', Y.Plugin.Base, [], {
        initializer: function () {
            if (this.get('rendered')) {
                this.addData();
            } else {
                this.afterHostMethod('dataSuccess', this.addData);
            }
        },
        destructor: function () {
            Y.one('#dataTable').empty();
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
              var RealDeductibleAmount = "";
              if( d.TableJson[i].RealDeductibleAmount === "" ){
                RealDeductibleAmount = "无";
              }else{
                RealDeductibleAmount = d.TableJson[i].RealDeductibleAmount;
              }
                data.push({
                    "preOrder19dianId": d.TableJson[i].preOrder19dianId,
					"UserName": d.TableJson[i].UserName,
					"mobilePhoneNumber": d.TableJson[i].mobilePhoneNumber,
                    "prePayTime": d.TableJson[i].prePayTime, 						//支付时间

                    "prePaidSum": d.TableJson[i].prePaidSum + getRefundMoneySum(d.TableJson[i].refundMoneySum), 					//预付金额
                    "invoiceTitle": d.TableJson[i].invoiceTitle, 				//发票抬头
                    "isStatus": d.TableJson[i].isApproved, 		//状态 对账 null0 未|1已
                    "orderId": d.TableJson[i].OrderId,
                    "discount": discount,
                    "RealDeductibleAmount": RealDeductibleAmount,
                    "btn": ''
                });
            };
            function getRefundMoneySum(moneyRefund) {
                if (moneyRefund == 0 || moneyRefund == '') {
                    return '';
                } else {
                    return "<span style='color: red' ><br/>(已退" + moneyRefund + ")</span>";
                }
            }
            var cbHandler = function (o) {
                if (o.data.isStatus != '1' && host.get('statusController') == '2') {//流水号状态不为1 且 状态控制为2(未对账)
                    return '<span class="inputSprite"><input class="inputCheck" type="checkbox" name="cb" value="' + o.data.preOrder19dianId + '" />' + o.data.preOrder19dianId + '</span>';
                } else {
                    return o.data.preOrder19dianId;
                }
            };
            var statusHandler = function (o) {
                if (o.value != '0' && !!o.value) { //状态 审核 null0|1
                    return '<span class="status been">已对账</span>';

                } else {
                    return '<span class="status">未对账</span>';
                }
            };
            var btnHandler = function (o) {//
                var url = UIBase.addURIParam('preOrderVerifiedDetail.aspx', 'a', o.data.orderId);
                //url = UIBase.addURIParam(url, 'nyStatusController', host.get('statusController'));
                //url = UIBase.addURIParam(url, 'nyPageIndex', host.get('pageIndex'));
				
                if (o.data.isStatus == '1') {//indexOf('cancel')
					return '<div class="nav"><span class="status been beenVerified">已对账</span><a class="btn detail" href="' + url + '">查看详情</a><a itemid="' + o.data.orderId + '" class="btn printer" href="javascript:;">打印</a></div>';
                } else {
                    return '<div class="nav"><a class="btn verified" href="javascript:void(0);" itemid="' + o.data.orderId + '">对账</a><a class="btn detail" href="' + url + '">查看详情</a><a itemid="' + o.data.orderId + '"  class="btn printer" href="javascript:;">打印</a></div>';
                }
                //
            };
            var table = new Y.DataTable({
                columns: [{
                    //key: 'preOrder19dianId', label: '流水号', formatter: cbHandler, allowHTML: true
					key: 'preOrder19dianId', label: '流水号'
                }, {
                    key: 'UserName', label: '用户昵称'
                }, {
                    key: 'mobilePhoneNumber', label: '手机号码'
                }, {
                    key: 'prePayTime', label: '支付时间'
                }, {
                    key: 'discount', label: '抵扣券'
                }, {
                    key: 'RealDeductibleAmount', label: '实际抵扣'
                }, {
                    key: 'prePaidSum', label: '支付金额', allowHTML: true
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
        NS: 'VerifiedTable',
        ATTRS: {
            title: {
                value: ''
            }
        }
    });

}, '1.0', { requires: ['base-build', 'plugin', 'datatable-base', 'datatable-message', 'gallery-checkboxgroups'] });

