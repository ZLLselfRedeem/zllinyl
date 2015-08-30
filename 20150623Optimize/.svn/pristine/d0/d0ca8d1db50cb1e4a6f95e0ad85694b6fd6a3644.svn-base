<!--#include file="tron.min.asp" -->
<!--#include file="config.asp" -->
<%
;(function(iPressModule){
	iPress = new iPressModule();
	iPress.iControler.set(contrast('router.json'));
	iPress.iEvent.set('maker', function(){
		if ( !Session('maker') ){
			iPress.errors['503'] = '抱歉，您没有权限进入 <a href="' + iPress.setURL('page', 'home') + '">返回登录</a>';
			iPress.error = 503;
			return false;
		}
	});

	iPress.render();

	if ( iPress.error > 0 ){
		Response.clear();
		console.log(iPress.errors[iPress.error + '']);
	}	
})(require('iPress'));
%>