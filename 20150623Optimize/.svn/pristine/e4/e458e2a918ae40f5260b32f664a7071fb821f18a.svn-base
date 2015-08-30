/*//
 标题：商户宝 UIController
 来源：viewallow UI
 日期：2014/06/10
 //*/

	VA.initPage.shopsundrylist = function() {
	    YUI().use('login', 'loginModule', 'dataTablePack', 'shopsundrylist-plugin', function(Y) {
		VA.renderPage = null;
		VA.renderPage = new Y.DataTableClass({pageType: 'shopsundrylist', contentBox: '#dataTable', shopId: VA.argPage.loginId.get('id'), queryString: VA.argPage.qs, ioURL: 'ajax/doSybSystem.ashx?m=syb_get_sundry_list'});
		VA.renderPage.plug(Y.Plugin.ShopsundryList);
		VA.renderPage.render();
	    });
	};
YUI.add('shopsundrylist-plugin', function(Y) {
    Y.Plugin.ShopsundryList = Y.Base.create('shopsundrylistPlugin', Y.Plugin.Base, [], {
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
	    if(!menu.person.userAuthority.shopsundrymanage){
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
	    var statusHandler = function(o){
		var sundryopen = ( o.data.status==1 )?' checked="checked"':' ',
		    sundryclose = ( o.data.status==2 )?' checked="checked"':' ';
		return '<span class="txt sundrystatus"><input type="radio" name="sundrystatus_'+o.data.sundryId+'" '+sundryopen+' class="inputRadio" value="1" /> 开启  <input type="radio" name="sundrystatus_'+o.data.sundryId+'" '+sundryclose+' class="inputRadio" value="2" /> 关闭 </span>';
	    }
	    var btnHandler = function(o) {
		var url = UIBase.addURIParam('companyManage.aspx', 'type', host.get('queryString').type);
		url = UIBase.addURIParam(url, 'm', 'edit');
		url = UIBase.addURIParam(url, 'a', host.get('queryString').a);
		url = UIBase.addURIParam(url, 'sname', host.get('queryString').sname);
                url = UIBase.addURIParam(url, 'syid', o.data.sundryId);
		url = UIBase.addURIParam(url, 'status', o.data.status);
		return '<a class="btn editor singleLink" href="'+url+'">修改</a>';
		/*
		return '<div class="companyBtn">'
			+ '	<a class="btn editor" href="'+url+'">修改</a>'
			// + '	<a class="btn delete" preid="' + o.data.sundryId + '" href="javascript:;">删除</a>'
			+ '</div>';
		*/
	    };
	    var table = new Y.DataTable({
		columns: [{
			key: 'sundryName', label: '杂项名称'
		    }, {
			key: 'status', label: '状态',formatter:statusHandler,allowHTML:true
		    }, {
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
	NS: 'shopsundrylistPlugin',
	ATTRS: {
	    title: {value: ''}
	}
    });
}, '1.0', {requires: ['base-build', 'plugin', 'datatable-base', 'datatable-message', 'overlay', 'popup-plugin']});