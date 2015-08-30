/*//
 标题：商户宝 UIController
 来源：viewallow UI
 日期：2014/06/12
 //*/

	VA.initPage.shopdiscountlist = function() {
	    YUI().use('login', 'loginModule', 'dataTablePack', 'shopdiscountlist-plugin', function(Y) {
		VA.renderPage = null;
		VA.renderPage = new Y.DataTableClass({pageType: 'shopdiscountlist', contentBox: '#dataTable', shopId: VA.argPage.loginId.get('id'), queryString: VA.argPage.qs, ioURL: 'ajax/doSybSystem.ashx?m=syb_get_shopvip_list'});
		VA.renderPage.plug(Y.Plugin.ShopdiscountList);
		VA.renderPage.render();
	    });
	};
YUI.add('shopdiscountlist-plugin', function(Y) {
    Y.Plugin.ShopdiscountList = Y.Base.create('shopdiscountlistPlugin', Y.Plugin.Base, [], {
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
	addHandler:function(e){
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
	    var that = this, host = this.get('host');
	    var addSprite = Y.one('#addSprite');
	    addSprite.all('.btn').on('click', this.addHandler, this);
	    // 添加
	    var addBtn = addSprite.one('.add');
	    if(!menu.person.userAuthority.shopvipdicount){
		addBtn.setStyle('display','none');
	    }else{
		var url = addBtn.getAttribute('href');
		// url = UIBase.addURIParam(url, 'm', 'add');
		url = UIBase.addURIParam(url, 'sname', host.get('queryString').sname);
		url = UIBase.addURIParam(url, 'a', host.get('queryString').a);
		addBtn.set('href',url);
	    }
	    Y.all('.sundrystatus .inputRadio').on('click',function(e){
		var val = e.currentTarget.getAttribute('value'),  syid = e.currentTarget.getAttribute('name').slice(13), 
		    hostObj = that.get('host'), arg = {};
		arg.ioURL = 'ajax/doSybSystem.ashx';
		arg.dataStr = 'm=syb_open_close_sundry&sundryId=' + syid+'&status=' + val;
		hostObj.ioRequest(arg, function(data) {
		    //
		});
	    },this);
	},
	addData: function() {
	    var self = this;
	    var host = this.get('host');
	    var contentBox = host.get('contentBox'),
		    data = [],
		    d = host.get('dataTemp');
	    data = d;
	    var rateHandler = function(o){
		var rate = Math.round(o.data.discount*10000)/100;
		return rate+'%';
	    }
	    var btnHandler = function(o) {
		var url = UIBase.addURIParam('companyManage.aspx', 'type', host.get('queryString').type);
		url = UIBase.addURIParam(url, 'm', 'edit');
		url = UIBase.addURIParam(url, 'a', host.get('queryString').a);
		url = UIBase.addURIParam(url, 'vid', o.data.id);
		url = UIBase.addURIParam(url, 'sname', host.get('queryString').sname);

		return '<div class="companyBtn">'
			+ '	<a class="btn editor" href="'+url+'">修改</a>'
			+ '	<a class="btn delete" preid="' + o.data.id + '" href="javascript:;">删除</a>'
			+ '</div>';
	    };
	    var table = new Y.DataTable({
		columns: [{
			key: 'name', label: '折扣名称'
		    }, {
			key: 'discount', label: '折扣率',formatter:rateHandler,allowHTML:true
		    }, {
			key: 'platformVipName', label: '平台等级'
		    },{
			key: 'btn', label: '操作', formatter: btnHandler, allowHTML: true
		    }],
		data: data,
		strings: {emptyMessage: '暂无数据显示', loadingMessage: '数据加载中'}
	    });
	    contentBox.empty(true);
	    table.render(contentBox);
	    this.initBind();
	}
    }, {
	NS: 'shopdiscountlistPlugin',
	ATTRS: {
	    title: {value: ''}
	}
    });
}, '1.0', {requires: ['base-build', 'plugin', 'datatable-base', 'datatable-message', 'overlay', 'popup-plugin']});