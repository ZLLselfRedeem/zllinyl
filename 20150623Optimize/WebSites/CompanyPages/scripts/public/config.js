// JavaScript Document
YUI_config = {
	comboBase: '../ncombo.axd?',
	combine: true,
	root: '3.17.2/build/',
	groups: {
		yui2: {
			combine: true,
			root: '2in3/2.8.0/build/',
			patterns: {
				 'yui2-': {
					 configFn: function (me) {
						 if (/-skin|reset|fonts|grids|base/.test(me.name)) {
							 me.type = 'css';
							 me.path = me.path.replace(/\.js/, '.css');
							 me.path = me.path.replace(/\/yui2-skin/, '/assets/skins/sam/yui2-skin');
						 } // if css
					 }
				 }
			} // patterns
		}, 
		gallery: {
			combine: true,
			root: 'gallery/build/',
			patterns: { 
				'gallery-': {},
				'gallerycss-': { type: 'css' }
			}
		}
	}
};