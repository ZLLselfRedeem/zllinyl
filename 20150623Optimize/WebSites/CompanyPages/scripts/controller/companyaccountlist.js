/*//
 标题：商户宝 UIController
 来源：viewallow UI
 日期：2014/06/09
 //*/

	VA.initPage.companyaccountlist = function() {
	    YUI().use('login', 'loginModule', 'dataTablePack', 'companyaccountlist-plugin', function(Y) {
		VA.renderPage = null;
		VA.renderPage = new Y.DataTableClass({pageType: 'companyaccountlist', contentBox: '#dataTable', shopId: VA.argPage.loginId.get('id'), queryString: VA.argPage.qs, ioURL: 'ajax/doSybSystem.ashx?m=syb_get_bankaccount_list'});
		VA.renderPage.plug(Y.Plugin.CompanyAccountList);
		VA.renderPage.render();
	    });
	};
YUI.add('companyaccountlist-plugin', function(Y) {
    Y.Plugin.CompanyAccountList = Y.Base.create('companyAccountListPlugin', Y.Plugin.Base, [], {
	initializer: function() {
	    // 访问权限 menu.person.userAuthority
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
	accountTitleHandler:function(e){
	    e.preventDefault();
            e.stopPropagation();
	    var isBack = e.target.getAttribute('class').indexOf('back')>-1;
	    var hrefStr = e.target.getAttribute('href');
	    VA.argPage.qs = UIBase.getNyQueryStringArgs(hrefStr);
	    var m = menu.getType(hrefStr),
		aspx = m + ".aspx";
	    isBack ? menu.prevPage(aspx, m) : menu.nextPage(aspx, m);
	},
	initBind: function () {
	    // 添加账号
	    var accountTitle = Y.one('#accountTitle');
	    accountTitle.all('.btn').on('click', this.accountTitleHandler, this);
	},
	addData: function() {
	    var self = this;
	    var host = this.get('host');
	    var acccountAddLink = Y.one('#accountTitle .add'), url = acccountAddLink.get('href');
	    url = UIBase.addURIParam(url, 'a', host.get('queryString').a);
	    acccountAddLink.set('href', url);
	    
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
	    var btnHandler = function(o) {
		var url = UIBase.addURIParam('companyManage.aspx', 'type', 'companyaccountlist');
		url = UIBase.addURIParam(url, 'a', o.data.companyId);
		url = UIBase.addURIParam(url, 'm', 'edit');
                url = UIBase.addURIParam(url, 'b', o.data.identity_Id);
		return '<div class="companyBtn">'
			+ '	<a class="btn editor" href="'+url+'">修改</a>'
			+ '	<a class="btn delete" preid="' + o.data.identity_Id + '" href="javascript:;">删除</a>'
			+ '</div>';
	    };
	    var table = new Y.DataTable({
		columns: [{
			key: 'accountNum', label: '账户号码'
		    }, {
			key: 'accountName', label: '开户名称'
		    }, {
			key: 'bankName', label: '开户银行'
		    }, {
      key: 'payeeBankName', label: '支行名称'
        }, {
			key: 'remark', label: '备注信息', formatter: sliceHandler, allowHTML: true
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
	NS: 'companyAccountListPlugin',
	ATTRS: {
	    title: {value: ''}
	}
    });
}, '1.0', {requires: ['base-build', 'plugin', 'datatable-base', 'datatable-message', 'overlay', 'popup-plugin']});