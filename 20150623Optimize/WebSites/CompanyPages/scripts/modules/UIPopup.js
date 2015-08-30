/*//
标题：收银宝 UIModule
来源：viewallow UI
日期：2013/9/19

* @class Popup
* @fileOverview 弹窗信息提示及功能选择
* @version
* @date 2013-12-03

//*/

YUI.add('popup-plugin', function (Y) {
    Y.AnimPlugin = Y.Base.create('animPlugin', Y.Base, [Y.Plugin.Base], {
        initializer : function(config) {
            this._bindAnimVisible();
            this._bindAnimHidden();

            this.after("animVisibleChange", this._bindAnimVisible);
            this.after("animHiddenChange", this._bindAnimHidden);
            this.doBefore("_uiSetVisible", this._uiAnimSetVisible);
        },
        destructor : function() {
            this.get("animVisible").stop().destroy();
            this.get("animHidden").stop().destroy();
        },
        _uiAnimSetVisible : function(val) {
            if (this.get("host").get("rendered")) {
                if (val) {
                    this.get("animHidden").stop();
                    this.get("animVisible").run();
                } else {
                    this.get("animVisible").stop();
                    this.get("animHidden").run();
                }
                return new Y.Do.Prevent("AnimPlugin prevented default show/hide");
            }
        },
        _uiSetVisible : function(val) {
            var host = this.get("host");
            var hiddenClass = host.getClassName("hidden");
            if (!val) {
                host.get("boundingBox").addClass(hiddenClass);
            } else {
                host.get("boundingBox").removeClass(hiddenClass);
            }
        },
        _bindAnimVisible : function() {
            var animVisible = this.get("animVisible");
            animVisible.on("start", Y.bind(function() {
                this._uiSetVisible(true);
            }, this));
        },
        _bindAnimHidden : function() {
            var animHidden = this.get("animHidden");
            animHidden.after("end", Y.bind(function() {
                this._uiSetVisible(false);
            }, this));
        }
    }, {
		NS:'fx',
        ATTRS: {
            duration : {
				value: 0.2//0.2
			},
			animVisible : {
				valueFn : function() {
					var host = this.get("host"),
						boundingBox = host.get("boundingBox");
					var anim = new Y.Anim({
						node: boundingBox,
						from:{
							top:-100
						},
						to: { 
							opacity: 1
						},
						easing: 'backIn',
						duration: this.get("duration")
					});
					if (!host.get("visible")) {
						boundingBox.setStyle("opacity", 0);
					}
					
					anim.on("destroy", function() {
						if (Y.UA.ie) {
							this.get("node").setStyle("opacity", 1);
						} else {
							this.get("node").setStyle("opacity", "");
						}
					});
					
					return anim;
				}
			},
			animHidden : {
				valueFn : function() {
					return new Y.Anim({
						node: this.get("host").get("boundingBox"),
						to: { 
							opacity:0,
							top:-100
						},
						easing: 'backIn',
						duration: this.get("duration")
					});
				}
			}
        }
    });

}, '1.0', { requires: ['base-build','anim','overlay','transition','plugin'] });


YUI.add('transition-panel', function (Y) {
	Y.TransitionPanel = Y.Base.create('TransitionPanel', Y.Base, [], {
		initializer: function () {
		},
		showPanel:function() {
			this.panel.show();
			var node = this.get('bb');
			node.transition({
				duration: 0.5,
				opacity:1
			});
		},
		hidePanel:function() {
			var that = this;
			this.get('bb').transition({
				duration: 0.5,
				opacity:0
			}, function () {
				that.panel.hide();
			});
		}
	}, {
		NS:'TransitionPanelNS',
        ATTRS: {
			ok:{
				valueFn:function(){
					return function(){};
				}
			},
			cancel:{
				valueFn:function(){
					return function(){};
				}
			},
			okButton:{
				valueFn:function(node){
					var that = this;
					var okCfg = {
						value: '确定',
						section: Y.WidgetStdMod.FOOTER,
						action: function (ev) {
							ev.preventDefault();
							that.hidePanel();
							that.get('ok')();
						}
					}
					return okCfg;
				}
			},
			closeButton:{
				valueFn:function(node){
					var that = this;
					var okCfg = {
						value: '完成',
						section: Y.WidgetStdMod.FOOTER,
						action: function (ev) {
							ev.preventDefault();
							that.hidePanel();
							that.get('ok')();
						}
					}
					return okCfg;
				}
			},
			cancelButton:{
				valueFn:function(){
					var that = this;
					var cancelCfg = {
						value: '取消',
						section: Y.WidgetStdMod.FOOTER,
						action: function (ev) {
							ev.preventDefault();
							that.hidePanel();
							that.get('cancel')();
						}
					}
					return cancelCfg;
				}
			},
            bb: {
                valueFn:function(){
					this.panel = new Y.Panel({
						srcNode: '#panelContainer',
						width: 364,
						centered: true,
						zIndex : 1000,
						modal: true,
						visible: false,
						render : true,
						headerContent: '批量对账',
						bodyContent: '您确定要进行批量对账？',
						buttons: [ this.get('okButton'),this.get('cancelButton') ]
					});
					var bb = this.panel.get('boundingBox');
					return bb;
				}
            }
        }
    });
}, '1.0', { requires: ['base-build','transition','panel','plugin'] });
var popup = {
	tips:function(element){
		var that = this;
		clearTimeout(that.timeoutID);
		YUI().use('base-build','anim-base',function(Y){
			that.tipsShowElem = new Y.Anim({
				node:element,
				to:{opacity:1,top:-56},
				duration:0.2
			});
			that.tipsHiddenElem = new Y.Anim({
				node:element,
				to:{opacity:0,top:-35},
				duration:0.3
			});
			that.tipsShowElem.stop();
			that.tipsHiddenElem.stop();
			that.tipsShowElem.on('end', function() {
				that.timeoutID = setTimeout(function(){
					that.tipsHiddenElem.run();
				},1500);
			});
			that.tipsHiddenElem.on('end',function(){
				Y.one(element).setStyle('display','none');
			});
		});
		return that.tipsShowElem;
	},
	panel:function(){
		YUI().use('transition-panel',function(Y){
			var bd = document.getElementsByTagName('body')[0];
			var panelContainer = document.createElement('div');
			panelContainer.id = 'panelContainer';
			bd.appendChild(panelContainer);
			VA.Singleton.popup = new Y.TransitionPanel();
			var panel = Y.one('#panelContainer').ancestor()._node;
			panel.hideFocus = true;
			VA.Singleton.popup.timeout = function(){
				var okHandler = function(){
					window.location.href = 'login.aspx';
				}
				VA.Singleton.popup.panel.set('headerContent','提示信息');
				VA.Singleton.popup.panel.set('bodyContent','登录超时，请重新登录');
				VA.Singleton.popup.panel.set('buttons',[VA.Singleton.popup.get('okButton')]);
				VA.Singleton.popup.set('ok',okHandler);
				VA.Singleton.popup.showPanel();
			};
		});
	}
};


