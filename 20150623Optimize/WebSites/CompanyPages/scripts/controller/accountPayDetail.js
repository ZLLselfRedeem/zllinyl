/*//
标题：商户宝 UIController
来源：viewallow UI
日期：2013/10/22
//*/

VA.initPage.accountPayDetail = function(){
	YUI({
		modules:{
			"Ny":{
				fullpath:'scripts/modules/UINy.js?v=3',
				requires: ['base-build', 'widget','node','transition','io-base','json-parse']
			}
		}
	}).use('loginModule','Ny','io-base','json-parse','widget',function(Y){
			VA.renderPage = new Y.NyClass({srcNode:'#comment',queryString:VA.argPage.qs,ioURL:'accountPayDetail.aspx/AccountDetail',pageType:'accountPayDetail'});
			VA.renderPage.render();
		});
	}

