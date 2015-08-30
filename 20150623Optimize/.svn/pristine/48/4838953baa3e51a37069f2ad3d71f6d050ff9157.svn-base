/*//
 标题：商户宝 UIController
 来源：viewallow UI
 日期：2014/06/04
 //*/

	VA.initPage.shoplist = function() {
	    YUI().use('login', 'loginModule', 'dataTablePack', 'shoplist-plugin', function(Y) {
		VA.renderPage = null;
		VA.renderPage = new Y.DataTableClass({pageType: 'shoplist', contentBox: '#dataTable', shopId: VA.argPage.loginId.get('id'), queryString: VA.argPage.qs, ioURL: 'ajax/doSybSystem.ashx?m=syb_get_shop_list'});
		VA.renderPage.plug(Y.Plugin.ShopList);
		VA.renderPage.render();
	    });
	};
YUI.add('shoplist-plugin', function(Y) {
    Y.Plugin.ShopList = Y.Base.create('shopListPlugin', Y.Plugin.Base, [], {
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
	addShop:function(e){
	    e.preventDefault();
            e.stopPropagation();
	    var hrefStr = e.target.getAttribute('href');
	    VA.argPage.qs = UIBase.getNyQueryStringArgs(hrefStr);
	    var m = menu.getType(hrefStr),
		aspx = m + ".aspx";
	    menu.nextPage(aspx, m);
	},
	initBind: function () {
	    if(menu.person.userAuthority.shopadd){
		var addCompany = Y.one('#addCompany');
		addCompany.setStyle('display','block');
		addCompany.one('.btn').on('click', this.addShop, this);
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
		var key = o.column.key, url = '';
		var userAuthority = menu.person.userAuthority[key]?' to-pagecontainer':' disabled';
		if( key==='shopsundrymanage' ){// shopsundrymanage | shopsundrylist
		    url = UIBase.addURIParam('shopsundrylist.aspx', 'type', 'shopsundrylist'); // 当 key 关键字与服务器默认定义的权限常量名不一致时；
		}else if( key==='shopvipdicount' ){
		    url = UIBase.addURIParam('shopdiscountlist.aspx', 'type', 'shopdiscountlist');// shopvipdicount | shopdi[s]countlist
		}else if( key==='shopimagerevelation' ){
        url = UIBase.addURIParam('companyManage.aspx', 'type', key);
		    url = UIBase.addURIParam(url, 'm', '');
		}else if( key==='withdrawtype' ){
        url = UIBase.addURIParam('companyManage.aspx', 'type', 'withdrawtype');
        url = UIBase.addURIParam(url, 'm', '');
    }else if( key==='commision' ){
        url = UIBase.addURIParam('companyManage.aspx', 'type', 'commision');
        url = UIBase.addURIParam(url, 'm', '');
    }else if( key==='shopvipdiscount' ){
        url = UIBase.addURIParam('companyManage.aspx', 'type', 'shopvipdiscount');
        url = UIBase.addURIParam(url, 'm', '');
    }else if( key==='companyaccount' ){
        url = UIBase.addURIParam('companyManage.aspx', 'type', 'companyaccount');
        url = UIBase.addURIParam(url, 'm', '');
    }
    url = UIBase.addURIParam(url, 'a', o.data.shopId);
		url = UIBase.addURIParam(url, 'sname', encodeURIComponent( o.data.shopName ));
		return '<div class="companyBtn" >'
			+ '	<a class="btn companyDetail '+userAuthority+'" href="'+url+'">查看详情</a>'
			+ '</div>';
	    };
	    var btnHandler = function(o) {
		var url = UIBase.addURIParam('companyManage.aspx', 'type', 'shoplist');
		url = UIBase.addURIParam(url, 'm', 'edit');
                url = UIBase.addURIParam(url, 'a', o.data.shopId);
		return '<div class="companyBtn" style="width: 130px;">'
			+ '	<a class="btn editor" href="'+url+'">修改</a>'
			+ '	<a class="btn delete" preid="' + o.data.shopId + '" href="javascript:;">删除</a>'
			+ '</div>';
	    };
	    var table = new Y.DataTable({
		columns: [{
			key: 'shopName', label: '门店名称', formatter: sliceHandler, allowHTML: true
		    }, {
			key: 'companyName', label: '所属公司', formatter: sliceHandler, allowHTML: true
		    }, {
			key: 'shopimagerevelation', label: '环境展示', formatter: detailHandler, allowHTML: true
		    }, {
      key: 'shopsundrymanage', label: '杂项信息', formatter: detailHandler, allowHTML: true
        }, {
      key: 'withdrawtype', label: '提款方式', formatter: detailHandler, allowHTML: true
        }, /*{
      key: 'shopvipdicount', label: '服务费', formatter: detailHandler, allowHTML: true
        },*/ {
      key: 'commision', label: '佣金', formatter: detailHandler, allowHTML: true
        },  {
      key: 'shopvipdiscount', label: '折扣', formatter: detailHandler, allowHTML: true
        },  {
      key: 'companyaccount', label: '银行账户', formatter: detailHandler, allowHTML: true
        }, /*{
			key: 'shopvipdicount', label: '折扣信息', formatter: detailHandler, allowHTML: true
		    }, */{
			key: 'btn', label: '操作', formatter: btnHandler, allowHTML: true
		    }],
		data: data,
		strings: {emptyMessage: '暂无数据显示', loadingMessage: '数据加载中'}
	    });
	    contentBox.empty(true);
	    table.render(contentBox);

	}
    }, {
	NS: 'shopListPlugin',
	ATTRS: {
	    title: {value: ''}
	}
    });
}, '1.0', {requires: ['base-build', 'plugin', 'datatable-base', 'datatable-message', 'overlay', 'popup-plugin']});