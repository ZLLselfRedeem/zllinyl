/*//
 标题：商户宝 UIController
 来源：viewallow UI
 日期：2014/06/04
 //*/

	VA.initPage.companylist = function() {
	    YUI().use('login', 'loginModule', 'dataTablePack', 'companylist-plugin', function(Y) {
		VA.renderPage = null;
		VA.renderPage = new Y.DataTableClass({pageType: 'companylist', contentBox: '#dataTable', shopId: VA.argPage.loginId.get('id'), queryString: VA.argPage.qs, ioURL: 'ajax/doSybSystem.ashx?m=syb_get_company_list'});
		VA.renderPage.plug(Y.Plugin.CompanyList);
		VA.renderPage.render();
	    });
	};
YUI.add('companylist-plugin', function(Y) {
    Y.Plugin.CompanyList = Y.Base.create('companyListPlugin', Y.Plugin.Base, [], {
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
	addCompany:function(e){
	    e.preventDefault();
            e.stopPropagation();
	    var hrefStr = e.target.getAttribute('href');
	    VA.argPage.qs = UIBase.getNyQueryStringArgs(hrefStr);
	    var m = menu.getType(hrefStr),
		aspx = m + ".aspx";
	    menu.nextPage(aspx, m);
	},
	initBind: function () {
	    // 添加公司
	    if(menu.person.userAuthority.companyadd){
		var addCompany = Y.one('#addCompany');
		addCompany.setStyle('display','block');
		addCompany.one('.btn').on('click', this.addCompany, this);
	    }
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
	    var detailHandler = function(o) {
		var key = o.column.key, url = '', companyName = encodeURIComponent( o.data.companyName );
		if( key==='companyMenu' ){
		    url = UIBase.addURIParam('companymenulist.aspx', 'type', key);
		    url = UIBase.addURIParam(url, 'cname', companyName);
		}/*else if( key==='companyAccount' ){
		    url = UIBase.addURIParam('companyaccountlist.aspx', 'type', key);
		}else if( key==='companyCommission' ){
		    url = UIBase.addURIParam('companyManage.aspx', 'type', 'companylist');
		    url = UIBase.addURIParam(url, 'm', 'commission');// 直接进入编辑页面
		}*/
                url = UIBase.addURIParam(url, 'a', o.data.companyID);
		return '<div class="companyBtn">'
			+ '	<a class="btn companyDetail to-pagecontainer" href="'+url+'">查看详情</a>'
			+ '</div>';
	    };
	    var btnHandler = function(o) {
		var url = UIBase.addURIParam('companyManage.aspx', 'type', 'companylist');
                url = UIBase.addURIParam(url, 'a', o.data.companyID);
		url = UIBase.addURIParam(url, 'm', 'edit');
		return '<div class="companyBtn" style="width: 130px;">'
			+ '	<a class="btn editor" href="'+url+'">修改</a>'
			+ '	<a class="btn delete" preid="' + o.data.companyID + '" href="javascript:;">删除</a>'
			+ '</div>';
	    };
	    var table = new Y.DataTable({
		columns: [{
			key: 'companyName', label: '公司名称', formatter: sliceHandler, allowHTML: true
		    }/*, {
			key: 'companyCommission', label: '设置佣金', formatter: detailHandler, allowHTML: true
		    }, {
			key: 'companyAccount', label: '银行账号', formatter: detailHandler, allowHTML: true
		    }*/, {
			key: 'companyMenu', label: '菜谱', formatter: detailHandler, allowHTML: true
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
	NS: 'companyListPlugin',
	ATTRS: {
	    title: {value: ''}
	}
    });
}, '1.0', {requires: ['base-build', 'plugin', 'datatable-base', 'datatable-message', 'overlay', 'popup-plugin']});