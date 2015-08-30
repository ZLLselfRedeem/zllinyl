/*//
标题：商户宝 UIController
来源：viewallow UI
日期：2013/10/22
//*/

VA.initPage.dishManage = function(){
		YUI({
			groups: {
				'jquery11': {
					base: 'scripts/jquery.upload/',
					async: false,
					modules:{
						'DisManage':{ 
							fullpath:'scripts/modules/UIDisManage.js',
							requires: ['base-build','node-base','widget','io-base','json-parse'] 
						},
						'jquery': {
							fullpath: 'scripts/public/jquery.min.js'
						},
						'jquery-imgareaselect-css': {
							path: 'css/imgareaselect-default.css',
							type: 'css'
						},
						'jquery-imgareaselect': {//jquery plugin
							path: 'js/jquery.imgareaselect.js',
							requires: ['jquery','jquery-imgareaselect-css']
						},
						'handlers': {
							path: 'js/handlers.js'
						},
						'swfupload': {
							path: 'js/swfupload.js',
							requires:['handlers']
						}
					}
				}
			}
		}).use('DisManage','catesItem-plugin','disInfoItem-plugin','disConfigItem-plugin','disUploadItem-plugin','jquery-imgareaselect','swfupload',function(Y){
			var sessionValue =  VA.Util.session;//获取session 放页面
			var btnSwfUpload = new Object();
			btnSwfUpload.id = "btnUpload";
			btnSwfUpload.w = 125;
			btnSwfUpload.h = 35;
			btnSwfUpload.imageUrl = "scripts/jquery.upload/images/btn_upload.png";
			btnSwfUpload.uploadUrl = "../Handlers/upload.ashx";
			btnSwfUpload.postParams = { "ASPSESSID": sessionValue };
			
			VA.renderPage = new Y.DisManage({contentBox:'#addContainer',shopId:VA.argPage.loginId.get('id'),queryString:VA.argPage.qs,ioURL:'ajax/doSybWeb.ashx'});	
			VA.renderPage.plug([Y.Plugin.CatesItem,Y.Plugin.DisInfoItem,Y.Plugin.DisConfigItem] );
			VA.renderPage.plug(Y.Plugin.DisUploadItem,{btn:btnSwfUpload});
			VA.renderPage.render();
		});
	};

