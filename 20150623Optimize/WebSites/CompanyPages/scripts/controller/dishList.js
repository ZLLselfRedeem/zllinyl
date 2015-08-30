/*//
 标题：商户宝 UIController
 来源：viewallow UI
 日期：2013/10/22
 //*/

	VA.initPage.dishList = function() {
	    YUI().use('login', 'loginModule', 'dataTablePack', 'dishList-plugin', 'CatesSelect-plugin', function(Y) {
		menu.createTab('dishList');

		VA.renderPage = null;
		VA.renderPage = new Y.DataTableClass({pageType: 'dishList', contentBox: '#dataTable', shopId: VA.argPage.loginId.get('id'), queryString: VA.argPage.qs, ioURL: 'dishList.aspx/GetDishListJson'});
		VA.renderPage.plug(Y.Plugin.CatesSelect, {shopId: VA.argPage.loginId.get('id')});
		VA.renderPage.plug(Y.Plugin.DishList);
		VA.renderPage.render();

	    });
	};

var DishList = function() {
    this.lavePicList = 0;
};

DishList.prototype.getLavePics = function() {
    YUI().use('io-base', 'json-parse', function(Y) {
	var picHandler = {
	    method: 'POST',
	    data: 'm=untreated_imagetask_get&shopid=' + VA.argPage.loginId.get('id'),
	    headers: {'Content-Type': 'application/x-www-form-urlencoded; charset=utf-8'},
	    on: {
		success: function(id, rsp) {
		    var res = Y.JSON.parse(rsp.responseText);
		    if (res.length >= 1) {
			/*
			 var okHandler = function(){
			 DishList.lavePicList = rsp.responseText;
			 uploadSuccess('',DishList.lavePicList);
			 }
			 var cancelHandler = function(){
			 alert('已成功删除，上次批量操作未完成图片列表');//停顿一下，删除有效
			 //setTimeout(function(){
			 DishList.lavePicList = 0;
			 dishListMethod.ignoreLavePics();
			 //}, 1000);
			 }
			 VA.Singleton.popup.panel.set('headerContent','批量添加');
			 VA.Singleton.popup.panel.set('bodyContent','上次批量操作还有未完成菜品图片，你需要继续吗？');
			 VA.Singleton.popup.panel.set('buttons',[VA.Singleton.popup.get('okButton'),VA.Singleton.popup.get('cancelButton')]);
			 VA.Singleton.popup.set('cancel',cancelHandler);
			 VA.Singleton.popup.set('ok',okHandler);
			 
			 VA.Singleton.popup.showPanel();
			 */

			if (confirm('上次批量操作还有未完成菜品图片，你需要继续吗？')) {
			    DishList.lavePicList = rsp.responseText;
			} else {
			    DishList.lavePicList = 0;
			    dishListMethod.ignoreLavePics();
			}
			;

		    } else {
			DishList.lavePicList = 0;

		    }
		},
		failure: function(id, rsp) {
		    Y.log(rsp.status);
		}
	    },
	    sync: true
	};
	Y.io('ajax/doSybWeb.ashx', picHandler);
    });
};


DishList.prototype.ignoreLavePics = function() {
    YUI().use('dishList-plugin', 'io-base', 'json-parse', function(Y) {
	var picHandler = {
	    method: 'POST',
	    data: 'm=untreated_imagetask_delete&shopid=' + VA.argPage.loginId.get('id'),
	    headers: {'Content-Type': 'application/x-www-form-urlencoded; charset=utf-8'},
	    on: {
		success: function(id, rsp) {
		    var res = rsp.responseText;
		    if (res == 'True') {
			VA.Singleton.popup.panel.set('headerContent', '提示信息');
			VA.Singleton.popup.panel.set('bodyContent', '已成功删除，上次批量操作未完成图片列表');
			VA.Singleton.popup.panel.set('buttons', [VA.Singleton.popup.get('okButton')]);
			VA.Singleton.popup.showPanel();
		    }
		},
		failure: function(id, rsp) {
		    Y.log(rsp.status);
		}
	    },
	    sync: true
	};
	Y.io('ajax/doSybWeb.ashx', picHandler);
    });
}
var dishListMethod = new DishList();



YUI.add('dishList-plugin', function(Y) {
    Y.Plugin.DishList = Y.Base.create('dishListPlugin', Y.Plugin.Base, [], {
	initializer: function() {
	    this.initBind();
	    if (this.get('rendered')) {
		this.addData();
	    } else {
		this.afterHostMethod('dataSuccess', this.addData);
	    }
	},
	destructor: function() {
	    this.titleNode.remove(true);
	},
	updateHandler: function(e) {
	    var self = this;
	    self.hostObj = this.get('host');
	    e.preventDefault();

	    var txtTips = Y.one('#update-txt-tips').setStyles({"display": "block", "left": 152, "opacity": 0});
	    var tips = popup.tips(txtTips);

	    var ioURL = 'dishList.aspx/UpdateMenu',
		    shopid = self.hostObj.get('shopId');
	    var dataHandler = {
		method: 'POST',
		data: '{"shopid":' + shopid + '}',
		headers: {'Content-Type': 'application/json; charset=utf-8'},
		on: {
		    start: function() {
			self.overlay.set('bodyContent', '<img src="images/loading.gif" width="32" height="32" />');
			//self.overlay.set('bodyContent', '<img src="data:image/gif;base64,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" />');
			self.overlay.show();
			self.handle.detach();
		    },
		    success: function(id, rsp) {
			var res = Y.JSON.parse(rsp.responseText);
			var status = res.d;
			if (status == '-1') {
			    txtTips.set('text', '当前门店菜谱不存在，请联系管理员!');
			}
			tips.stop().run();
		    },
		    end: function() {
			self.overlay.hide();
			self.handle = Y.one('#disBtn').delegate('click', self.updateHandler, '.update', self);
		    },
		    failure: function(id, rsp) {
			Y.log(rsp.status);
		    }
		}
	    }
	    Y.io(ioURL, dataHandler);
	    //
	},
	addDishUrl: function(e) {
	    e.preventDefault();
	    e.stopPropagation();
	    var txtTips = Y.one('#update-txt-tips').setStyles({"display": "block", "left": 0, "opacity": 0});
	    var tips = popup.tips(txtTips);

	    var host = this.get('host');
	    var status = '';
	    //isexists_memu
	    var ioHandler = {
		method: 'POST',
		data: 'm=isexists_memu&shopid=' + host.get('shopId'),
		headers: {'Content-Type': 'application/x-www-form-urlencoded; charset=utf-8'},
		on: {
		    success: function(id, rsp) {
			var res = Y.JSON.parse(rsp.responseText);
			status = res.list[0].status;
			if (status == '-1') {
			    txtTips.set('text', '当前门店菜谱不存在，请联系管理员!');
			    tips.stop().run();
			}
		    },
		    failure: function(id, rsp) {
			Y.log(rsp.status);
		    }
		},
		sync: true
	    };
	    Y.io('ajax/doSybWeb.ashx', ioHandler);

	    if (status == '-1')
		return;
	    var hrefStr = e.target.getAttribute('href');
	    VA.argPage.qs = UIBase.getNyQueryStringArgs(hrefStr);
	    var m = menu.getType(hrefStr),
		aspx = m + ".aspx";
	    menu.nextPage(aspx, m);
	},
	initBind: function() {
	    var dishBtn = Y.one('#disBtn');
	    dishBtn.append('<div class="txt-tips-sprite"><i class="txt-tips" id="update-txt-tips">更新菜单成功</i></div>');
	    dishBtn.append('<div id="layer"></div>');
	    this.overlay = new Y.Overlay({
		srcNode: "#layer",
		width: "205px",
		height: "56px",
		visible: false,
		shim: false,
		centered: true//,
			//plugins : [{fn:Y.AnimPlugin, cfg:{duration:1}}]
	    });
	    this.overlay.render();
	    this.handle = dishBtn.delegate('click', this.updateHandler, '.update', this);

	    dishBtn.all('.add').on('click', this.addDishUrl, this);
	    //
	},
	addData: function() {
	    var self = this;
	    var host = this.get('host');

	    var contentBox = host.get('contentBox'),
		    data = [],
		    d = host.get('dataTemp');
	    data = d.TableJson;
	    var sliceHandler = function(o) {
		if (o.value.length > 10) {
		    var trunc = o.value.slice(0, 10) + '...';
		    return trunc;
		}
		return o.value;
	    };
	    var btnHandler = function(o) {
		var url = UIBase.addURIParam('dishManage.aspx', 'type', 'edit');
		url = UIBase.addURIParam(url, 'a', o.data.DishID);
		return '<div class="disListBtn">'
			+ '	<a class="btn editor" href="' + url + '">修改</a>'
			+ '	<a class="btn delete" preid="' + o.data.DishI18nID + '" href="javascript:;">删除</a>'
			+ '</div>';
	    };

	    var imgHandler = function(o) {
		if (!!o.value) {
		    return '<img src="' + o.value + '" width="60px" height="45px" alt="' + o.data.DishName + '" />';
		} else {
		    return '<img src="images/nonImg2.png" width="60px" height="45px" alt="' + o.data.DishName + '" />';
		}

	    };

	    var table = new Y.DataTable({
		columns: [{
			key: 'imageurl', label: '图片', formatter: imgHandler, allowHTML: true
		    }, {
			key: 'dishTypeList', label: '所在分类'
		    }, {
			key: 'DishName', label: '菜品名称'
		    }, {
			key: 'dishScaleList', label: '规格'
		    }, {
			key: 'dishPriceList', label: '价格'
		    }, {
			key: 'btn', label: '操作', formatter: btnHandler, allowHTML: true
		    }],
		data: data,
		strings: {emptyMessage: '暂无数据显示', loadingMessage: '数据加载中'}
	    });
	    contentBox.empty(true);
	    table.render(contentBox);

	}
    }, {
	NS: 'dishListPlugin',
	ATTRS: {
	    title: {value: ''}
	}
    });

}, '1.0', {requires: ['base-build', 'plugin', 'datatable-base', 'datatable-message', 'overlay', 'popup-plugin']});




