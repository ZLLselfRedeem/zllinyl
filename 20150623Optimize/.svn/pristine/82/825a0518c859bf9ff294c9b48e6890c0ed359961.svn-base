/*//
标题：商户宝 UIController
来源：viewallow UI
日期：2013/10/22
//*/

VA.initPage.configurationPassword = function () {

    YUI().use('node-base', 'io-base', 'json-parse', function (Y) {
		menu.createTab("configurationPassword");

		function tabContent(e){
			e.preventDefault();
			e.stopPropagation();
			var str = e.target.getAttribute('href');
			VA.argPage.qs = UIBase.getNyQueryStringArgs(str);
			
			var index = str.indexOf('?'),
				indexHeng = str.lastIndexOf('/')+1,
				indexDot = str.lastIndexOf('.');
			var urlAspx = str.substring(0,index),
				cur = str.substring(indexHeng,indexDot);
			menu.tab(urlAspx,cur);
		}
		
	
	
        //
        var msg = {
            inputNull: {
                current: "密码不能为空",
                now: "新密码不能为空",
                confirm: "确认密码不能为空"
            },
            inputError: {
                current: "密码不正确",
                now: "新密码格式不正确",
                confirm: "确认密码与新密码不一致"
            },
            inputSuccess: {
                text: "密码修改成功"
            }
        };
        var inputs = Y.all('#resetPassword .inputText');
        var confirm = Y.one('#confirm');
        var now = Y.one('#now');
        var btn = Y.one('#btnConfirm');
        inputs.on('blur', function (e) {
            var t = e.target;
            var n = t.get('name');
            if (t.get('value') == '') {
                t.ancestor().one('label').set('text', msg.inputNull[n]);
            } else if (confirm.get('value') != now.get('value')) {
                confirm.ancestor().one('label').set('text', msg.inputError.confirm);
            } else {
                confirm.ancestor().one('label').set('text', '');
            };

        }, this);
	   inputs.on('focus', function (e) {
			var t = e.target;
		   t.ancestor().one('label').set('text', '');
	   }, this);

        btn.on('click', function (e) {
            var confirmValue = confirm.get('value');
            var nowValue = now.get('value');
            var currentValue = Y.one('#current').get('value');

            //字符条件判断
            var isPass = true;
            if (currentValue == "") {
                Y.one('#current').ancestor().one("label").set("text", msg.inputError.current);
                isPass = false;
            }
            if (nowValue == "") {
                now.ancestor().one("label").set("text", msg.inputError.now);
                isPass = false;
            }
            if (nowValue != confirmValue) {
                confirm.ancestor().one("label").set("text", msg.inputError.confirm);
                return;
            }

            if (!isPass) return;//客户端检测退出

            Y.io('ajax/doSybWeb.ashx', {
                method: 'POST',
                data: 'm=resert_password&currectPassword=' + currentValue + '&newPassword=' + nowValue + '&newConfrimPassword=' + confirmValue,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=utf-8' },
                on: {
                    success: function (id, rsp) {
                        var r = Y.JSON.parse(rsp.responseText);
                        if (r.list[0].status == 1) {
                            Y.one("#current").set('value', '');
                            Y.one("#now").set('value', '');
                            Y.one("#confirm").set('value', '');
                            alert("修改密码成功！");
                        }
                        else { 
                            alert(r.list[0].info);
                        }
                    },
                    failure: function (id, rsp) {
                        Y.log(rsp.status);
                    }
                }
            });

        }, this);
    })
}