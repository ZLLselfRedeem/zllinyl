/*//
标题：商户宝 UIController
来源：viewallow UI
日期：2015/08/21
//*/

VA.initPage.increment = function(){
	YUI().use('login','loginModule','dataTablePack','increment-list-plugin','cb-plugin','transition','datatype','sortable',function(Y){
		VA.renderPage = new Y.DataTableClass({pageType:'increment',srcNode:'#dataTable',shopId:VA.argPage.loginId.get('id'),queryString:VA.argPage.qs,ioURL:'ajax/doSybChannel.ashx'});
		VA.renderPage.plug([Y.Plugin.IncrementList]);
		VA.renderPage.render();
	});	
};
																																		
YUI.add('increment-list-plugin', function (Y) {
    Y.Plugin.IncrementList = Y.Base.create('incrementListPlugin', Y.Plugin.Base, [], {
        initializer: function () {
			this.initBind();
            if (this.get('rendered')) {
                this.addData();
            } else {
                this.afterHostMethod('dataSuccess', this.addData);
            }
        },
        destructor: function () {
            Y.one('#dataTable').empty();
        },
		initBind: function () {
			Y.one("#incrementTabList").delegate('click', this.tabListHandler, 'li', this);
			var sortable = new Y.Sortable({
				container: '#incrementTabList',
				nodes: 'li',
				opacity: '.1'
			});
			//Y.one("#incrementSortSave").on("click", this.sortSaveHandler, this);
		},
        addData: function () {
            var host = this.get('host');
			var active = this.get('activeInd');
			
            var contentBox = host.get('contentBox'),
				data = [],
				d = host.get('dataTemp');
			var itemData, htmlStr = '', curListActive = false;
            for (var i = 0; i < d.TableJson.length; i++) {
				itemData = d.TableJson[i];
				var activeStr = i==active?" active":"";
				var statusStr = itemData.status=="True"?"ok":"no";
				if(i==active){
					curListActive = itemData.status;
					this.setTipsShow(statusStr, itemData);
				}
				
				var signId = itemData.sign, singStr = '';
				if(signId=='1'){
					singStr = '<i class="ico-status ico-'+signId+'">新品</i>';
				}else if(signId=='2'){
					singStr = '<i class="ico-status ico-'+signId+'">限时</i>';
				}else if(signId=='3'){
					singStr = '<i class="ico-status ico-'+signId+'">特价</i>';
				};
				htmlStr += '<li class="'+statusStr+activeStr+'" data-id="'+itemData.id+'" data-name="'+itemData.name+'" data-status="'+statusStr+'" data-content="'+itemData.content+'"><a href="javascript:;">'+itemData.name+singStr+'</a></li>'
            };
			Y.one("#incrementTabList").setHTML(htmlStr);
			
		},
		setTipsShow:function(status, d){
			var tipsHtmlStr = '';
			if(status=='ok'){
				tipsHtmlStr += '<div class="content increment-tips-content ">'
							+'	<div class="title ok"><h4>您所在门店已开启"'+d.name+'"增值功能！</h4><p class="txt">'+d.content+'</p></div>'
							+'	<p class="btn-sprite"><a class="btn close" href="javascript:;" data-id="">关闭</a><a class="btn to-pagecontainer" href="incrementEdit.aspx?type=editor&a='+d.id+'">设置</a></p>'
							+'</div>';
			}else{
				tipsHtmlStr += '<div class="content increment-tips-content">'
							+'	<div class="title"><h4>您所在门店暂未开启"'+d.name+'"增值功能！</h4><p class="txt">'+d.content+'</p></div>'
							+'	<p class="btn-sprite"><a class="btn apply" href="javascript:;">开启</a></p>'
							'</div>';
			}
			this.get('incrementTipsElem').setHTML(tipsHtmlStr);
		},
		tabListHandler:function(e,a,b){
			var t = e.currentTarget;
			var that = this;
			var hostObj = this.get("host");
			t.siblings("li").removeClass("active");
			t.addClass("active");
			Y.all("#incrementTabList li").each(function(elem, ind){
				if(elem.hasClass("active")){
					that.set('activeInd', ind);
				};
			});
			var itemData = {"name":t.getAttribute('data-name'), "id":t.getAttribute('data-id'), "content":t.getAttribute('data-content')};
			this.setTipsShow(t.getAttribute('data-status'), itemData);
			
			var status = t.getAttribute('data-status');
			if(status=='ok'){
				// 删除未保存数据
				var config = {};
				config.ioURL = 'ajax/doSybChannel.ashx';
				config.dataStr = 'm=noPublicDelete&shopChannelID=' + t.getAttribute('data-id');
				config.headers = {
					'Content-Type' : 'application/x-www-form-urlencoded; charset=utf-8'
				};
				config.sync = false;
				hostObj.ioRequest(config, function (data) {
					if (data.list[0].status === 1) {
						//
					}
				});
			}
			
		}

    }, {
        NS: 'incrementList',
        ATTRS: {
            title: {
                value: ''
            },
			incrementTipsElem:{
				value:Y.one("#incrementTips")
			},
			activeInd:{
				value:0
			}
        }
    });

}, '1.0', { requires: ['base-build', 'plugin', 'datatable-base', 'datatable-message', 'gallery-checkboxgroups','transition'] });

