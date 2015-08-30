/*//
标题：商户宝 UIController
来源：viewallow UI
日期：2013/10/22
//*/

VA.initPage.configurationEmployees = function(){
	// 页面数据
	YUI().use('dataTablePack','configurationEmployees-plugin',function(Y){
		menu.createTab("configurationEmployees");
	
		VA.renderPage = new Y.DataTableClass({pageType:'configurationEmployees',contentBox:'#dataTable',shopId:VA.argPage.loginId.get('id'),queryString:VA.argPage.qs,ioURL:'ajax/doSybWeb.ashx?m=business_employees_authority_query'});
		VA.renderPage.plug(Y.Plugin.configurationEmployees);
		VA.renderPage.render();
	});
};


YUI.add('configurationEmployees-plugin', function (Y) {
    Y.Plugin.configurationEmployees = Y.Base.create('configurationEmployeesPlugin', Y.Plugin.Base, [], {
        initializer: function () {
            
            if (this.get('rendered')) {
                this.addData();
            } else {
                this.afterHostMethod('dataSuccess', this.addData);
            }
			this.initBind();
        },
        destructor: function () {
            this.titleNode.remove(true);
        },
		checkHandler:function(e){
			var t = e.currentTarget,
				roleId = t.getAttribute('name');
			var host = this.get('host');
			var dataStr = 'm=waiter_role_update&employeeId='+this.employeeId+'&roleId='+roleId;
			var dataHandler = {
				method: 'POST',
				data: dataStr,
				headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=utf-8' },
				on: {
					success: function (id, rsp) {
						var res = Y.JSON.parse(rsp.responseText);
						if(res.list[0].status!=1){
							var okHandler = function(){
								//host.syncUI();
							}
							VA.Singleton.popup.panel.set('headerContent','店员管理');
							VA.Singleton.popup.panel.set('bodyContent',res.list[0].info);
							VA.Singleton.popup.panel.set('buttons',[VA.Singleton.popup.get('okButton')]);
							VA.Singleton.popup.set('ok',okHandler);
							VA.Singleton.popup.showPanel();
						}
					},
					failure: function (id, rsp) {
						Y.log(rsp.status);
					}
				}
			};
			Y.io('ajax/doSybWeb.ashx',dataHandler);
		},
		sybHandler:function(e){
			var t = e.currentTarget,
				val = parseInt(t.get('value'));
			var isSyb = t.getAttribute("name").indexOf("isSyb")>-1,
				m = isSyb?"business_employees_authority_enter_syb":"waiter_receive_payorder_msg";
			var host = this.get('host');
			var dataStr = 'm='+m+'&employeeId='+this.employeeId+'&status='+val;
			var dataHandler = {
				method: 'POST',
				data: dataStr,
				headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=utf-8' },
				on: {
					success: function (id, rsp) {
						var res = Y.JSON.parse(rsp.responseText);
						if(res.list[0].status!=1){
							var okHandler = function(){
								//host.syncUI();
							}
							VA.Singleton.popup.panel.set('headerContent','店员管理');
							VA.Singleton.popup.panel.set('bodyContent',res.list[0].info);
							VA.Singleton.popup.panel.set('buttons',[VA.Singleton.popup.get('okButton')]);
							VA.Singleton.popup.set('ok',okHandler);
							VA.Singleton.popup.showPanel();
							// 权限管理失败
							return;
						}
						
						if(isSyb){
							// 判定关闭 登录收银宝系统权限 操作
							if(val==0){
								Y.all('#panelContainer .t .inputBox').set('disabled','disabled');
								// Y.all('#panelContainer .t .inputBox').set('checked',false);
							}else{
								Y.all('#panelContainer .t .inputBox').set('disabled','');
							}
						}
					},
					failure: function (id, rsp) {
						Y.log(rsp.status);
					}
				}
			};
			Y.io('ajax/doSybWeb.ashx',dataHandler);
		},
		btnHandler:function(e){ 
			e.preventDefault();
			e.stopPropagation();
			var that = this;
			var t = e.currentTarget;
			var host = this.get('host');
			var str = t.getAttribute('href');
			var param = UIBase.getNyQueryStringArgs(str),
				className = t.getAttribute('class');
			that.employeeId = param.employeeID;
			var isDelete = className.indexOf('delete')>-1,
				isReset = className.indexOf('reset')>-1,
				isEdit = className.indexOf('edit')>-1,
				m = '';
			function ioSubmit(){
				var dataStr = 'm='+m+'&employeeId='+that.employeeId;
				var dataHandler = {
					method: 'POST',
					data: dataStr,
					headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=utf-8' },
					on: {
					    success: function (id, rsp) {
							var res = Y.JSON.parse(rsp.responseText);
							if(isDelete){
								host.syncUI();
							}else if(isReset){
								var okHandler = function(){
									// host.syncUI();
								}
								VA.Singleton.popup.panel.set('headerContent','店员管理');
								VA.Singleton.popup.panel.set('bodyContent','当前新密码为：'+res.list[0].info);
								VA.Singleton.popup.panel.set('buttons',[VA.Singleton.popup.get('okButton')]);
								VA.Singleton.popup.set('ok',okHandler);
								VA.Singleton.popup.showPanel();
							}else if(isEdit){
								var sybYes = param.isSupportEnterSyb=='true'?'checked="checked"':'',
									sybNo = param.isSupportEnterSyb=='false'?'checked="checked"':'',
									isMsgYes = param.isSupportReceiveMsg=='true'?'checked="checked"':'',
									isMsgNo = param.isSupportReceiveMsg=='false'?'checked="checked"':'';
								var checkManage = param.isSupportShopManagePage=='true'?'checked="checked"':'';
								
								var html = '<div style="overflow-y:auto;width:100%;">'
											+'<table id="sybModule" class="t" border="0" cellspacing="0" cellpadding="0"><tbody>'
											+'<tr><td colspan="2"><div id="loginSyb">* 【收银宝】权限</div></td></tr>';
								for (var i = 0, len = res.sybRoles.length; i < len; i++) {
								    var check = res.sybRoles[i].isHave ? 'checked="checked"' : '';
								    html += '<tr><td>' + res.sybRoles[i].roleName + '</td><td><input type="checkbox" name="' + res.sybRoles[i].roleId + '" class="inputBox" ' + check + ' /></td></tr>';
								}
								//html += '<tr><td>店员管理</td><td><input type="checkbox" class="inputBox" name="0" ' + checkManage + ' /></td></tr>';
								html += '</tbody></table><br />';

								html += '<table id="uxianModule" class="t" border="0" cellspacing="0" cellpadding="0"><tbody>';
							    html += '<tr><td colspan="2"><div style="text-align:left;">*【悠先服务】权限</div></td></tr>';
								for (var i = 0, len = res.vaServiceRoles.length; i < len; i++) {
								    var check = res.vaServiceRoles[i].isHave ? 'checked="checked"' : '';
								    html += '<tr><td>' + res.vaServiceRoles[i].roleName + '</td><td><input type="checkbox" name="' + res.vaServiceRoles[i].roleId + '" class="uxianInputBox" ' + check + ' /></td></tr>';
								}

								html += '</tbody></table></div>';
								
								var okHandler = function(){
									host.syncUI();
								}
								VA.Singleton.popup.panel.set('headerContent','编辑【 '+param.EmployeeFirstName+' 】权限');
								VA.Singleton.popup.panel.set('bodyContent',html);
								VA.Singleton.popup.panel.set('buttons',[VA.Singleton.popup.get('closeButton')]);
								VA.Singleton.popup.set('ok',okHandler);
								VA.Singleton.popup.showPanel();
								
								// #panelContainer
								if(param.isSupportEnterSyb=='false'){
									Y.all('#sybModule .inputBox').set('disabled','disabled');
								}else{
									Y.all('#sybModule .inputBox').set('disabled','');
								}
								that.layoutBind();
							}
						},
						failure: function (id, rsp) {
							Y.log(rsp.status);
						}
					}
				};
				Y.io('ajax/doSybWeb.ashx',dataHandler);
			}
			
			// 禁用当前用户(即门店超级用户，才拥有“店员管理”功能块)的操作:删除、重置密码、编辑权限
			if(className.indexOf('disabled')>-1){
				return;
			};
			if(isDelete){
				var okHandler = function(){
					m = 'business_employees_authority_delete';
					ioSubmit();
				}
				var cancelHandler = function(){
					//
				}
				VA.Singleton.popup.panel.set('headerContent','店员管理');
				VA.Singleton.popup.panel.set('bodyContent','您确定要删除该店员信息吗?');
				VA.Singleton.popup.panel.set('buttons',[VA.Singleton.popup.get('okButton'),VA.Singleton.popup.get('cancelButton')]);
				VA.Singleton.popup.set('ok',okHandler);
				VA.Singleton.popup.set('cancel',cancelHandler);
				VA.Singleton.popup.showPanel();
				return;
			}else if(isReset){
				m = 'business_employees_authority_resert_password';
			}else if(isEdit){
				m = 'waiter_role_query';
			}
			
			ioSubmit();
		},
        initBind: function () {
			var fm = Y.one("#dataFm");
			//fm["status"]
			Y.one('.configurationEmployees #dataTable').delegate('click',this.btnHandler,'.btn',this);
        },
		layoutBind:function(){
		    Y.one('#sybModule').delegate('click', this.checkHandler, '.inputBox', this);
		    Y.one('#uxianModule').delegate('click', this.checkHandler, '.uxianInputBox', this);
			Y.one('#sybModule').delegate('click',this.sybHandler,'.inputRadio',this);
			Y.one('#hasSybSMS').delegate('click',this.sybHandler,'.inputRadio',this);
			//Y.one('#hasSybSMS').delegate('click',this.smsHandler,'.inputRadio',this);
		},
        addData: function () {
            var self = this;
			var host = this.get('host');

            var contentBox = host.get('contentBox'),
				data = [],
				d = host.get('dataTemp');
			data = d;
			var user = Y.Cookie.get('u');
            var btnHandler = function (o) {
				var disabled = '';
				if( o.data.UserName==user ){
					disabled = 'disabled';
				};
                return '<div class="btnSprite">'
					   + '	<a class="btn delete '+disabled+'" name="" href="configurationEmployees.aspx?employeeID='+o.data.employeeID+'">删除</a>'
					   + '	<a class="btn reset '+disabled+'" name="" href="configurationEmployees.aspx?employeeID='+o.data.employeeID+'">重置密码</a>'
					   + '	<a class="btn edit '+disabled+'" name="" href="configurationEmployees.aspx?employeeID='+o.data.employeeID+'&isSupportEnterSyb='+o.data.isSupportEnterSyb+'&isSupportShopManagePage='+o.data.isSupportShopManagePage+'&EmployeeFirstName='+o.data.EmployeeFirstName+'&isSupportReceiveMsg='+o.data.isSupportReceiveMsg+'">编辑权限</a>'
					   + '</div>';
            };
		
            var table = new Y.DataTable({
                columns: [{
                    key: 'UserName', label: '用户名'
                },{
                    key: 'EmployeeFirstName', label: '姓名'
                },{
                    key: 'btn', label: '操作', formatter: btnHandler, allowHTML: true
                }],
                data: data,
                strings: { emptyMessage: '暂无数据显示', loadingMessage: '数据加载中' }
            });
            contentBox.empty(true);
            table.render(contentBox);

        }
    }, {
        NS: 'configurationEmployeesPlugin',
        ATTRS: {
            employeeId: { value: 0 }
        }
    });

}, '1.0', { requires: ['base-build', 'plugin', 'datatable-base', 'datatable-message','io-base','json-stringify'] });
