/*//
 标题：商户宝 UIController
 来源：viewallow UI
 日期：2014/10/22
 //*/

	VA.initPage.companyManage = function() {
	    YUI({
		groups: {
		    'staff': {
			async: false,
			modules: {
			    'UIStaff': {
				fullpath: 'scripts/modules/UIStaff.js',
				requires: ['base-build', 'node-base', 'widget', 'io-base', 'json-parse']
			    }
			}
		    }
		}
	    }).use('UIStaff','companyManage-plugin', function(Y) {
		VA.renderPage = new Y.CompanyManage({contentBox: '#companyManageText', queryString: VA.argPage.qs, ioURL: 'ajax/doSybSystem.ashx'});
		VA.renderPage.plug(Y.Plugin.CompanyManagePlugin);
		VA.renderPage.render();
	    });
	};

