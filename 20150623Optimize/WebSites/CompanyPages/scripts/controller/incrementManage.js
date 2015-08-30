/*//
 标题：商户宝 UIController
 来源：viewallow UI
 日期：2014/10/22
 //*/

VA.initPage.incrementManage = function() {
	YUI({
		groups: {
			'increment': {
				async: false,
				modules: {
					'UIIncrement': {
						fullpath: 'scripts/modules/UIIncrement.js',
						requires: ['base-build', 'node-base', 'widget', 'io-base', 'json-parse']
					}
				}
			}
		}
	}).use('UIIncrement','incrementManage-plugin', function(Y) {
	VA.renderPage = new Y.IncrementManage({contentBox: '#incrementManageText', queryString: VA.argPage.qs, ioURL: 'ajax/doSybChannel.ashx'});
	VA.renderPage.plug(Y.Plugin.IncrementManagePlugin);
	VA.renderPage.render();
	});
};

