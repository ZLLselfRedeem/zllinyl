﻿/*//
标题：商户宝 UIController
来源：viewallow UI
日期：2013/10/22
//*/

VA.initPage.accountPreOrderDetail = function(){
	YUI({
		modules:{
			"Ny":{
				fullpath:'scripts/modules/UINy.js?v=20150729',
				requires: ['base-build', 'widget','node','transition','io-base','json-parse']
			}
		}
	}).use('loginModule','Ny','io-base','json-parse','widget',function(Y){
			//var loginModule = new Y.LoginModule();
			VA.renderPage = new Y.NyClass({srcNode:'#comment',queryString:VA.argPage.qs,ioURL:'accountPreOrderDetail.aspx/CommonPageInfoShow',pageType:'accountPreOrderDetail'});
			VA.renderPage.render();
		});
	}

