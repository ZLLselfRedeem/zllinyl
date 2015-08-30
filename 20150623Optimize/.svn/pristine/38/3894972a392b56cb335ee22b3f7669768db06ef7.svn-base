/*//
 标题：商户宝 UIController
 来源：viewallow UI
 日期：2014/06/20
 //*/

	VA.initPage.shophandle = function() {
	    YUI().use('login', 'loginModule', 'dataTablePack', 'shophandle-plugin', function(Y) {
		VA.renderPage = null;
		VA.renderPage = new Y.DataTableClass({pageType: 'shophandle', contentBox: '#dataTable', shopId: VA.argPage.loginId.get('id'), queryString: VA.argPage.qs, ioURL: 'ajax/doSybSystem.ashx?m=syb_get_shopishandle_list'});
		VA.renderPage.plug(Y.Plugin.ShopHandle);
		VA.renderPage.render();
	    });
	};
YUI.add('shophandle-plugin', function(Y) {
    Y.Plugin.ShopHandle = Y.Base.create('shopHandlePlugin', Y.Plugin.Base, [], {
	initializer: function() {
	    if (this.get('rendered')) {
		this.addData();
	    } else {
		this.afterHostMethod('dataSuccess', this.addData);
	    }
	},
	destructor: function() {
	    this.titleNode.remove(true);
	},
	addData: function() {
	    var self = this;
	    var host = this.get('host');
	    var contentBox = host.get('contentBox'),
		    data = [],
		    d = host.get('dataTemp');
	    data = d;
	    var sliceHandler = function(o) {
		if (o.value.length > 20) {
		    var trunc = o.value.slice(0, 20) + '...';
		    return trunc;
		}
		return o.value;
	    };
	    var statusHanlder = function(o){
		var res = '';
		switch( o.data.handleStatus ){
		    case 1:
			res = '<em class="txtGreen">已通过</em>';
		    break;
		    case -1:
			res = '<em class="txtColor">未通过</em>';
		    break;
		    case -2:
			res = '待审核';
		    break;
		}
		return res;
	    };
	  var btnHandler = function(o) {
  		var url = UIBase.addURIParam('companyManage.aspx', 'type', 'shophandle');
  		url = UIBase.addURIParam(url, 'm', '');
      url = UIBase.addURIParam(url, 'a', o.data.shopId);
      var url1 = UIBase.addURIParam('companyManage.aspx', 'type', 'financehandle');
      url1 = UIBase.addURIParam(url1, 'm', '');
      url1 = UIBase.addURIParam(url1, 'a', o.data.shopId);
      
  		return '<div class="controllerBtn">'
  			+ '	<a class="btn detail" style="display: inline-block;" href="'+url+'">查看详情</a>'
        /*+ ' <a class="btn detail" style="display: inline-block;" href="'+url1+'">财务审核</a>'*/
  			+ '</div>';
	    };
	    var table = new Y.DataTable({
		columns: [{
			key: 'shopName', label: '门店名称', formatter: sliceHandler, allowHTML: true
		    }, {
			key: 'companyName', label: '所属公司', formatter: sliceHandler, allowHTML: true
		    }, {
			key: 'handleStatus', label: '审核状态',formatter: statusHanlder, allowHTML: true
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
	NS: 'shopHandlePlugin',
	ATTRS: {
	    title: {value: ''}
	}
    });
}, '1.0', {requires: ['base-build', 'plugin', 'datatable-base', 'datatable-message', 'overlay', 'popup-plugin']});