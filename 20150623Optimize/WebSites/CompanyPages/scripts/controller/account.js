/*//
标题：商户宝 UIController
来源：viewallow UI
日期：2013/10/22
//*/

VA.initPage.accountDetails = function(){//帐户列表页
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
		}).use('login','loginModule', 'dataTablePack', 'accountDetails-plugin','calendarCquery-plugin','datatype',function (Y) {
			menu.createTab('accountDetails');
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
            VA.renderPage = new Y.DataTableClass({ pageType:'accountDetails',contentBox: '#dataTable',queryString:VA.argPage.qs, shopId:VA.argPage.loginId.get('id'), companyId:VA.argPage.loginModule.get('companyId'),ioURL: 'accountDetails.aspx/GetAccountDetail' });
            VA.renderPage.plug(Y.Plugin.CalendarCquery,{minDate:minDate});
			VA.renderPage.plug(Y.Plugin.AccountDetails,{statusController:1});
            VA.renderPage.render();
			
			//初始化周
        });	
		
	};
	

YUI.add('accountDetails-plugin', function (Y) {
    Y.Plugin.AccountDetails = Y.Base.create('accountDetailsPluginNS', Y.Plugin.Base, [], {
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
			
			Y.one('#accountDetailBack').on('click',function(e){
				e.preventDefault();
				e.stopPropagation();
				var hrefStr = e.target.getAttribute('href');
				var m = menu.getType(hrefStr),
					aspx = m+".aspx";
				VA.argPage.qs = UIBase.getNyQueryStringArgs(hrefStr);
				
				menu.pageHistory(VA.argPage.qs.totalStartDay,
							VA.argPage.qs.totalEndDay,
							1,
							10,
							2,
							'',
							0,
							{"amountMin":-2147483648,"amountMax":2147483647,"type":0,"mobilePhoneNumber":""});
				menu.tab(aspx,m);
			},this);
		},
		downLoadFile:function(){
			Y.all('#downLoad .d').on('click',function(e){
				var t = e.currentTarget,
					host = this.get('host');
				var className = t.getAttribute('class'),
					outType = 0;
				if(className.indexOf('xls')>-1){ outType = 1; }
				var v = Y.one('#search .inputText').get('value');
				var inputsPay = Y.all('#pay .inputText')._nodes,
					a = (inputsPay[0].value)?parseInt(inputsPay[0].value):-2147483648,
					b = (inputsPay[1].value)?parseInt(inputsPay[1].value):2147483647;
				var amountMax = Math.max(a,b),
					amountMin = Math.min(a,b);

				var type = 0;
				Y.all('#accountSelect input').each(function(element){
					if(element.get('checked')){
						type = element.get('value');
					}
				});
				if(Y.one('#mobilePhone')){
					var mobileValue = Y.one('#mobilePhone .inputText').get('value');
					mobileValue = (mobileValue=='请输入手机号码')?'':mobileValue;
				};
				var inputsDate = Y.all('#accountDate .inputText')._nodes,
					inputTextStr = (v=='请输入营业收入流水号')?'':v;//mainkey 请输入营业收入流水号
				var shopId = host.get('shopId'),
					companyId = host.get('companyId');
					
				var nowDay = Y.DataType.Date.format(new Date(), { format: '%Y-%m-%d' }),
					startDay = inputsDate[0].value || host.CalendarCqueryNS.get('minDate'),
					endDay = inputsDate[1].value || nowDay;
				var bd = document.getElementsByTagName('body')[0];
				var srcStr = 'outFile.aspx?outType='+outType+'&datetimestart='+startDay+'&datetimeend='+endDay+'&shopId='+shopId+'&companyId='+companyId+'&type='+type+'&paystart='+amountMin+'&payend='+amountMax+'&mainkey='+inputTextStr+'&mobilePhoneNumber='+mobileValue;//
				if(document.getElementById('outFile')){
					document.getElementById('outFile').setAttribute('src',srcStr);
				}else{
					var ifr = document.createElement('iframe');
					ifr.setAttribute('src',srcStr);
					ifr.id="outFile";
					ifr.style.width="0";
					ifr.style.height="0";
					ifr.style.name="outFile";
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
			// 【流水号】【手机号码】【入座时间】【类型】【金额】【余额】【查看详情】
            for (var i = 0; i < d.list.length; i++) {
                data.push({
					"accountTypeConnId": d.list[i].accountTypeConnId,
                    "mobilePhoneNumber": d.list[i].mobilePhoneNumber,
					"Btime": d.list[i].operTime,
					"accountTypeName":d.list[i].accountTypeName,
					
					"accountId":d.list[i].accountId,
                    "type": d.list[i].accountType,
					
                    "pay": d.list[i].accountMoney,
                    "payd": d.list[i].remainMoney,
                    "orderId": d.list[i].OrderId,
                    "btn": ''
                });
            };

            var numberHandler = function (o) {
				var num = o.value;
				num = Math.round(num*100)/100;
                if (num>=0) { //状态 审核 null0|1
                    return '+' + num;
                } else {
                    return num;
                }
            };
            var btnHandler = function (o) {//
                var url = '';
				var qs = VA.argPage.qs = host.get('queryString');
				if(o.data.type==5){
					url = UIBase.addURIParam('accountPreOrderDetail.aspx', 'a', o.data.orderId);
					
					url = UIBase.addURIParam(url, 'detail', qs.detail);
					url = UIBase.addURIParam(url, 'totalStartDay', qs.totalStartDay);
					url = UIBase.addURIParam(url, 'totalEndDay', qs.totalEndDay);
					
					return '<a class="btn detail" href="' + url + '">查看详情</a>';
				}/*else if(o.data.type==6){
					url = UIBase.addURIParam('accountPayDetail.aspx', 'a', o.data.accountId);
					
					url = UIBase.addURIParam(url, 'detail', qs.detail);
					url = UIBase.addURIParam(url, 'totalStartDay', qs.totalStartDay);
					url = UIBase.addURIParam(url, 'totalEndDay', qs.totalEndDay);
					
					return '<a class="btn detail" href="' + url + '">查看详情</a>';
				}*/else{
					return '暂无详情';
				}
                
				
            };
            var table = new Y.DataTable({
                columns: [{
                    key: 'accountTypeConnId', label: '流水号'
                }, {
                    key: 'mobilePhoneNumber', label: '手机号码'
                }, {
                    key: 'Btime', label: '时间'
                }, {
                    key: 'accountTypeName', label: '类型'
                }, {
                    key: 'pay', label: '金额', formatter: numberHandler
                }, {
                    key: 'payd', label: '余额'
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
        NS: 'accountDetailsPluginNS',
        ATTRS: {
            statusController: { value: '' }
        }
    });

}, '1.0', { requires: ['base-build', 'plugin', 'datatable-base', 'datatable-message'] });


