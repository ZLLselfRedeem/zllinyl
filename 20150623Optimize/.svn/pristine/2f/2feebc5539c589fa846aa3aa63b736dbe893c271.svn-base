/*//
标题：商户宝 UIController
来源：viewallow UI
日期：2013/10/22
//*/

VA.initPage.preOrderVerifiedDetail = function(){
	YUI({
		modules:{
			"Ny":{
				fullpath:'scripts/modules/UINy.js?v=3',
				requires: ['base-build', 'widget','node','transition','io-base','json-parse']
			}
		}
	}).use('Ny','loginModule','io-base','json-parse','widget',function(Y){
		VA.renderPage = new Y.NyClass({srcNode:'#comment',queryString:VA.argPage.qs,ioURL:'preOrderVerifiedDetail.aspx/CommonPageInfoShow',pageType:'preOrderVerifiedDetail'});
		VA.renderPage.render();
	});	
};

