/*//
标题：商户宝 UIController
来源：viewallow UI
日期：2013/10/22
//*/

VA.initPage.accountTotal = function(){//帐户列表页
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
		}).use('login','loginModule', 'dataTablePack', 'accountTotal-plugin','calendarCquery-plugin','datatype',function (Y) {
			menu.createTab('accountTotal');
			var minDate = '1972-10-01';
			var startDateHandler = {
				method: 'POST',	
				//data:'',
				headers: { 'Content-Type': 'application/json; charset=utf-8' },
				on: {
					success: function (id, rsp) {
						var res = Y.JSON.parse(rsp.responseText);
						var d = res.d;
						if(!!d){
							minDate = Y.DataType.Date.format(d.slice(0,10), { format: '%Y-%m-%d' });
						}
					},
					failure: function (id, rsp) {
						Y.log(rsp.status);
					}
				},
				sync:true
			}
			Y.io('accountDetails.aspx/GetTime',startDateHandler);
            VA.renderPage = new Y.DataTableClass({ pageType:'accountTotal',contentBox: '#dataTable',queryString:VA.argPage.qs, shopId:VA.argPage.loginId.get('id'), companyId:VA.argPage.loginModule.get('companyId'),ioURL: 'accountDetails.aspx/GetAccountTotal' });
            VA.renderPage.plug(Y.Plugin.CalendarCquery,{minDate:minDate});
			VA.renderPage.plug(Y.Plugin.AccountTotal,{statusController:1});
            VA.renderPage.render();
			
			//初始化周
        });	
		
	};
	

YUI.add('accountTotal-plugin', function (Y) {
    Y.Plugin.AccountTotal = Y.Base.create('accountTotalPluginNS', Y.Plugin.Base, [], {
        initializer: function () {
            this.initAccount();
            if (this.get('rendered')) {
                this.addData();
            } else {
                this.afterHostMethod('dataSuccess', this.addData);
            }
        },
        destructor: function () {
            this.titleNode.remove(true);
        },
		initAccount:function(){
			this.initAccountDate();
			this.downLoadFile();
		},
		downLoadFile:function(){
			Y.all('#downLoad a').on('click',function(e){
				var t = e.currentTarget,
					host = this.get('host');
				var className = t.getAttribute('class'),
					outType = 0;
				if(className.indexOf('xls')>-1){ outType = 1; }
				var inputsDate = Y.all('#date .inputText')._nodes;
				var shopId = host.get('shopId');
					
				var nowDay = Y.DataType.Date.format(new Date(), { format: '%Y-%m-%d' }),
					startDay = inputsDate[0].value || host.CalendarCqueryNS.get('minDate'),
					endDay = inputsDate[1].value || nowDay;
				var bd = document.getElementsByTagName('body')[0];

				var srcStr = 'AccountTotalOutFile.aspx?outType='+outType+'&datetimestart='+startDay+'&datetimeend='+endDay+'&shopId='+shopId;
				
				if(document.getElementById('AccountTotalOutFile')){
					document.getElementById('AccountTotalOutFile').setAttribute('src',srcStr);
				}else{
					var ifr = document.createElement('iframe');
					ifr.setAttribute('src',srcStr);
					ifr.id="AccountTotalOutFile";
					ifr.style.width="0";
					ifr.style.height="0";
					ifr.style.name="AccountTotalOutFile";
					ifr.style.frameborder="0";
					bd.appendChild(ifr);
				}
			},this);
		},
		initAccountDate: function () {
			var hostObj = this.get('host'),
				accountData = hostObj.get('account');
            var nowDay = Y.DataType.Date.format(new Date(), { format: '%Y-%m-%d' });
			var week = VAcalendar.week;
			accountData.startDay = week.startDay;
			accountData.endDay = nowDay;
			Y.one('#dateStr').set('value',accountData.startDay);
			Y.one('#dateEnd').set('value',accountData.endDay);
        },
        addData: function () {
            var self = this,
				host = this.get('host');
            //累计、可提取
            var str = '{"shopId":' + host.get("shopId") + ',"companyId":' + host.get("companyId") + '}';
            var accountHandler = {
                method: 'POST',
                data: str,
                headers: { 'Content-Type': 'application/json; charset=utf-8' },
                on: {
                    success: function (id, rsp) {
                        var res = Y.JSON.parse(rsp.responseText);
                        var d = Y.JSON.parse(res.d);
                        Y.all('#account i').item(0).set('text', d.income[0].income);
                        Y.all('#account i').item(1).set('text', d.bhave[0].bhave);
                    },
                    failure: function (id, rsp) {
                        Y.log(rsp.status);
                    }
                }
            }
            Y.io('accountDetails.aspx/GetAllcount', accountHandler);
            //
            var contentBox = host.get('contentBox'),
				data = [],
				d = host.get('dataTemp');
            for (var i = 0; i < d.list.length; i++) {
                data.push({
                    "date": d.list[i].date.slice(0,10),
                    "count":d.list[i].count,
                    "total": d.list[i].total,
                    "DeductibleAmount": d.list[i].RealDeductibleAmount,
                    "btn": ''
                });
            };

            var numberHandler = function (o) {
				return Math.round(o.value*100)/100;
            };
            var btnHandler = function (o) {//
				var startDay = Y.one('#dateStr').get('value'),
					endDay = Y.one('#dateEnd').get('value');
                var url = 'accountDetails.aspx';
				url = UIBase.addURIParam(url, 'detail', o.data.date);
				url = UIBase.addURIParam(url, 'totalStartDay', startDay);
				url = UIBase.addURIParam(url, 'totalEndDay', endDay);
				return '<a class="btn detail" href="' + url + '">查看明细</a>';
            };
            var table = new Y.DataTable({
                columns: [{
                    key: 'date', label: '日期'
                }, {
                    key: 'count', label: '已入座订单数'
                }, {
                    key: 'DeductibleAmount', label: '抵扣金额'
                }, {
                    key: 'total', label: '已入座总金额',formatter:numberHandler
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
        NS: 'accountTotalPluginNS',
        ATTRS: {
            statusController: { value: '' }
        }
    });

}, '1.0', { requires: ['base-build', 'plugin', 'datatable-base', 'datatable-message'] });


