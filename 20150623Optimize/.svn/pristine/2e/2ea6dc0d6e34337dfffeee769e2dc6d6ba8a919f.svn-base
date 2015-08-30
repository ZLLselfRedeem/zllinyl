/*//
标题：收银宝 UIModule
来源：viewallow UI
日期：2013/9/19
//*/
YUI().use('cookie', 'node-base', 'node-event-simulate', 'base-build', 'io-base', 'json-parse', function (Y) {
	Y.all('.inputtext')._nodes[0].focus();
	Y.all('.inputtext').on('focus', function (e) {
		var t = e.target,
			val = t.get('value');
		if(val=='请输入用户名'){
			t.set('value', '');
		}
	   
	}, this);

	var btnSubmit = Y.one('#submit');
	var login = function (e) {
		var n = Y.one('#userName').get('value'),
			pw = Y.one('#pw').get('value');
		var loginHandler = {
			method: 'POST',
			data: '{"userName":"' + n + '","password":"' + pw + '"}',
			headers: { 'Content-Type': 'application/json; charset=utf-8' },
			on: {
				success: function (id, rsp) {
					var result = Y.JSON.parse(rsp.responseText);
					var	d = Y.JSON.parse(result.d);
					var	msg = Y.one("#error");
					
					switch (d) {
						case -1:
							msg.set("text", "登录失败，请重试");
							break;
						case -2:
							msg.set("text", "密码错误");
							break;
						case -3:
							msg.set("text", "用户名不存在");
							break;
						case -4:
							msg.set("text", "对不起，您的帐号暂无登录本系统的权限");
							break;
						case -5:
							msg.set("text", "对不起，管理员未为您分配管理门店");
							break;
						default:
							msg.set("text", "");
							Y.Cookie.set('u',d.user[0].userName);
							window.location.href='page.aspx?r='+Math.random();
							break;
					}
				},
				failure: function (id, rsp) {
					Y.log(rsp.status);
				}
			}
		};
		Y.io('login.aspx/Login', loginHandler);
	};
	btnSubmit.on('click', login);
	document.onkeydown = function (e) {
		e = e ? e : (window.event ? window.event : null);
		if (e.keyCode == 13) {
			btnSubmit.simulate('click', { shiftKey: false });
			return false;
		}
	};
});

