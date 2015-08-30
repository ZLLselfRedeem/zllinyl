YUI.add('catesItem-plugin', function(Y) {
    Y.Plugin.CatesItem = Y.Base.create('CatesItem', Y.Plugin.Base, [], {
	initializer: function() {
	    this.initBind();
	    if (this.get('rendered')) {
		this.addData();
	    } else {
		this.afterHostMethod('dataSuccess', this.addData);
	    }
	},
	destructor: function() {
	    Y.one('#cates').empty();
	},
	initBind: function() {
	},
	updateItem: function(e) {
	    e.stopPropagation();
	    this.updateLayout = e.currentTarget;
	    var parent = this.updateLayout.ancestor('li');
	    this.updateInput = parent.one('input');
	    this.updateText = parent.one('label');
	    this.catesLayoutUpdate();
	},
	deleteItem: function(e) {
	    e.stopPropagation();
	    var t = e.currentTarget;
	    var input = t.ancestor('li').one('input');
	    var dishTypeID = input.get('value');

	    var ioHandler = {
		method: 'POST',
		data: 'm=dish_typeinfo_delete&dishTypeID=' + dishTypeID,
		headers: {'Content-Type': 'application/x-www-form-urlencoded; charset=utf-8'},
		on: {
		    success: function(id, rsp) {
			if (rsp.responseText == '-1000') {//登录超时
			    VA.Singleton.popup.timeout();
			    return;
			}
			var res = Y.JSON.parse(rsp.responseText);
			var status = res.list[0].status;
			if (status == '-2') {
			    var okHandler = function() {
				//
			    };
			    VA.Singleton.popup.panel.set('headerContent', '提示信息');
			    VA.Singleton.popup.panel.set('bodyContent', '该分类已被菜品引用了，无法删除');
			    VA.Singleton.popup.panel.set('buttons', [VA.Singleton.popup.get('okButton')]);
			    VA.Singleton.popup.set('ok', okHandler);
			    VA.Singleton.popup.showPanel();
			} else if (status == '-1') {
			    VA.Singleton.popup.panel.set('headerContent', '提示信息');
			    VA.Singleton.popup.panel.set('bodyContent', '删除操作失败');
			    VA.Singleton.popup.panel.set('buttons', [VA.Singleton.popup.get('okButton')]);
			    VA.Singleton.popup.set('ok', okHandler);
			    VA.Singleton.popup.showPanel();
			} else {
			    t.ancestor('li').remove(true);
			}
		    },
		    failure: function(id, rsp) {
			Y.log(rsp.status);
		    }
		}
	    };
	    Y.io('ajax/doSybWeb.ashx', ioHandler);
	},
	submitItem: function(e) {
	    e.preventDefault();
	    e.stopPropagation();
	    var host = this.get('host');

	    var container = Y.one('#cates');
	    var t = e.currentTarget;
	    var className = t.getAttribute('class'),
		    isComfirm = className.indexOf('comfirm') > -1;
	    var inputs = Y.all('.catesLayout .inputText');
	    var n = inputs._nodes;

	    if (isComfirm) {
		// 校验
		var hasOnlyWS = new RegExp("^\\s{0,}$", "g");// white space
		var hasNumber = new RegExp("^[-\\d]+$", "g");
		var htmlEscape = function(text) {
		    return text.replace(/[<>"&\\]/g, function(match, pos, originalText) {
			switch (match) {
			    case "<":
				return "&lt;";
				break;
			    case ">":
				return "&gt;";
				break;
			    case "&":
				return "&amp;";
				break;
			    case "\"":
				return "&quot;";
				break;
			    case "\\":
				return "\\\\";
				break;
			    default:
				break;
			}
		    });
		}

		var dataObj = {};
		dataObj.shopid = host.get('shopId');
		dataObj.dishTypeName = n[0].value;
		dataObj.dishTypeSequence = n[1].value;
		dataObj.dishTypeNameURI = encodeURIComponent(htmlEscape(dataObj.dishTypeName));

		var okHandler = function() {
		    // 
		};
		if (dataObj.dishTypeName === '输入分类名称' || hasOnlyWS.test(dataObj.dishTypeName)) {
		    VA.Singleton.popup.panel.set('headerContent', '提示信息');
		    VA.Singleton.popup.panel.set('bodyContent', '请输入可用的分类名称');
		    VA.Singleton.popup.panel.set('buttons', [VA.Singleton.popup.get('okButton')]);
		    VA.Singleton.popup.set('ok', okHandler);
		    VA.Singleton.popup.showPanel();
		    return;
		}
		if (!hasNumber.test(dataObj.dishTypeSequence)) {// 数字
		    VA.Singleton.popup.panel.set('headerContent', '提示信息');
		    VA.Singleton.popup.panel.set('bodyContent', '请输入可用的排序号( 数字格式 )');
		    VA.Singleton.popup.panel.set('buttons', [VA.Singleton.popup.get('okButton')]);
		    VA.Singleton.popup.showPanel();
		    return;

		}
		//
		var editStatus = this.get('editStatus');

		var msg = {};
		msg.unique = '的分类名称重复';
		msg.error = '分类失败了';
		if (editStatus === 0) { // 添加分类
		    dataObj.dataStr = 'm=dish_typeinfo_insert&shopid=' + dataObj.shopid + '&dishTypeSequence=' + dataObj.dishTypeSequence + '&dishTypeName=' + dataObj.dishTypeNameURI;
		    dataObj.handler = function(info) {
			var dishTypeID = info;
			container.append('<li><input class="inputCheck" type="checkbox" value="' + dishTypeID + '" name="cates" rel="' + dataObj.dishTypeSequence + '" /><label for="">' + dataObj.dishTypeName + '</label><span class="layout">' + dataObj.dishTypeName + '</span></li>');
		    };

		    msg.unique = '添加' + msg.unique;
		    msg.error = '添加' + msg.error;
		} else if (editStatus === 1) {// 修改分类
		    var that = this;
		    var dishTypeID = this.updateInput.get('value');
		    dataObj.dataStr = 'm=dish_typeinfo_update&shopid=' + dataObj.shopid + '&dishTypeID=' + dishTypeID + '&dishTypeSequence=' + dataObj.dishTypeSequence + '&dishTypeName=' + dataObj.dishTypeNameURI;
		    dataObj.handler = function() {
			that.updateLayout.set('text', dataObj.dishTypeName);
			that.updateText.set('text', dataObj.dishTypeName);
			that.updateInput.setAttribute('rel', dataObj.dishTypeSequence);
		    };
		    msg.unique = '修改' + msg.unique;
		    msg.error = '修改' + msg.error;
		};
		var ioHandler = {
		    method: 'POST',
		    data: dataObj.dataStr,
		    headers: {'Content-Type': 'application/x-www-form-urlencoded; charset=utf-8'},
		    on: {
			success: function(id, rsp) {
			    if (rsp.responseText == '-1000') {
				VA.Singleton.popup.timeout();
				return;
			    }
			    var res = Y.JSON.parse(rsp.responseText);
			    var status = res.list[0].status;
			    var info = res.list[0].info;
			    if (status == 1) {
				dataObj.handler(info);
			    } else {
				if (status == -2) {
				    VA.Singleton.popup.panel.set('bodyContent', msg.unique);
				} else if (status == -1) {
				    VA.Singleton.popup.panel.set('bodyContent', msg.error);
				} else if (status == -3) {
				    VA.Singleton.popup.panel.set('bodyContent', info);
				}

				var okHandler = function() {
				    //
				};
				VA.Singleton.popup.panel.set('headerContent', '提示信息');
				VA.Singleton.popup.panel.set('buttons', [VA.Singleton.popup.get('okButton')]);
				VA.Singleton.popup.set('ok', okHandler);
				VA.Singleton.popup.showPanel();
			    }


			},
			failure: function(id, rsp) {
			    Y.log(rsp.status);
			}
		    }
		};
		Y.io('ajax/doSybWeb.ashx', ioHandler);
		this.get('popupLayout').hide();
	    } else {
		this.get('popupLayout').hide();
	    }
	},
	editData: function(e) {
	    var t = e.currentTarget,
		    parent = t.ancestor('.btnSprite');
	    var className = t.getAttribute('class'),
		    isAdd = className.indexOf('add') > -1,
		    isDelete = className.indexOf('edit') > -1,
		    isUpdate = className.indexOf('update') > -1,
		    isEditStatus = className.indexOf('abled') > -1;
	    var layoutIcon = Y.all('.disCates .cates .layout');
	    if (isAdd) {
		this.set('editStatus', 0);

		var updateBtn = parent.one('.update');
		updateBtn.removeClass('abled');
		updateBtn.set('text', '修改分类');
		var deleteBtn = parent.one('.edit');
		deleteBtn.removeClass('abled');
		deleteBtn.set('text', '删除分类');
		layoutIcon.hide();

		this.catesLayoutAdd();

	    } else if (isUpdate) {
		this.get('popupLayout').hide();

		this.set('editStatus', 1);
		if (!isEditStatus) {
		    t.addClass('abled');
		    t.set('text', '完成修改');
		    layoutIcon.setAttribute("class", "layout update");
		    layoutIcon.setStyles({"display": "block"});

		    // 复位兄弟按扭
		    var siblingsBtn = parent.one('.edit');
		    siblingsBtn.removeClass('abled');
		    siblingsBtn.set('text', '删除分类');
		} else {
		    t.removeClass('abled');
		    t.set('text', '修改分类');
		    layoutIcon.setAttribute("class", "layout");
		    layoutIcon.setStyles({"display": "none"});
		}
	    } else if (isDelete) {
		this.get('popupLayout').hide();
		if (!isEditStatus) {
		    t.addClass('abled');
		    t.set('text', '完成删除');
		    layoutIcon.setAttribute("class", "layout delete");
		    layoutIcon.setStyle("display", "block");

		    var siblingsBtn = parent.one('.update');
		    siblingsBtn.removeClass('abled');
		    siblingsBtn.set('text', '修改分类');
		} else {
		    t.removeClass('abled');
		    t.set('text', '删除分类');
		    layoutIcon.removeClass('delete');
		    layoutIcon.setAttribute("class", "layout");
		    layoutIcon.setStyle("display", "none");
		}
	    }
	},
	addData: function() {
	    var self = this,
		    hostObj = this.get('host');
	    var d = hostObj.get('dataConfig').DishTypeInfoList,
		    DishTypeList = hostObj.get('dataTemp').DishTypeList;

	    //分类栏
	    var domStr = '';
	    if (hostObj.queryString.type == 'edit') {
		var checkedStr = '';
		for (var i = 0; i < DishTypeList.length; i++) {
		    checkedStr += '(^' + DishTypeList[i] + '$)|';
		}
		if (checkedStr.indexOf('|') > -1) {
		    checkedStr = checkedStr.slice(0, -1);
		}
		checkedStr = checkedStr ? checkedStr : 'null';
		var checkedReg = new RegExp(checkedStr, 'i');
		for (var j = 0; j < d.length; j++) {
		    if (checkedReg.test(d[j].Id)) {
			domStr += '<li><input class="inputCheck" type="checkbox" value="' + d[j].Id + '" checked="checked" name="cates" rel="' + d[j].Sequence + '" /><label for="">' + d[j].Name + '</label><span class="layout">' + d[j].Name + '</span></li>';
		    } else {
			domStr += '<li><input class="inputCheck" type="checkbox" value="' + d[j].Id + '" name="cates" rel="' + d[j].Sequence + '" /><label for="">' + d[j].Name + '</label><span class="layout">' + d[j].Name + '</span></li>';
		    }
		}

	    } else {
		for (var i = 0; i < d.length; i++) {
		    domStr += '<li><input class="inputCheck" type="checkbox" value="' + d[i].Id + '" name="cates" rel="' + d[i].Sequence + '" /><label for="">' + d[i].Name + '</label><span class="layout">' + d[i].Name + '</span></li>';
		}
	    }
	    Y.one('#cates').setHTML(domStr);
	},
	catesLayoutAdd: function() {
	    var inputs = Y.all('.catesLayout .inputText');
	    var n = inputs._nodes;
	    inputs.on('focus', function(ev) {
		var t = ev.currentTarget;
		var v = t.get('value');				//t._node.attributes[1].value;获取
		if (v == '输入分类名称' || v == '输入排序号') {
		    t.set('value', '');				//yui事件中设置；
		}
	    });
	    Y.one(n[0]).on('blur', function(ev) {
		var t = ev.currentTarget;
		var v = t.get('value');
		if (v == '') {
		    t.set('value', '输入分类名称');
		}
	    }, this);
	    Y.one(n[1]).on('blur', function(ev) {
		var t = ev.currentTarget;
		var v = t.get('value');
		if (v == '') {
		    t.set('value', '输入排序号');
		}
	    }, this);

	    n[0].value = '输入分类名称';
	    n[1].value = '输入排序号';
	    Y.one('#catesTitle').set('text', '添加分类');
	    this.get('popupLayout').show();
	},
	catesLayoutUpdate: function() {
	    var inputs = Y.all('.catesLayout .inputText');
	    var n = inputs._nodes;
	    n[0].value = this.updateLayout.get('text');
	    n[1].value = this.updateInput.getAttribute('rel');
	    Y.one('#catesTitle').set('text', '修改分类');
	    this.get('popupLayout').show();
	}
    }, {
	NS: 'CatesItemNS',
	ATTRS: {
	    editStatus: {
		value: 0 //0 添加 1修改
	    },
	    updateInput: {
		value: ''
	    },
	    popupLayout: {
		valueFn: function() {
		    if (Y.one('.catesLayout'))
			Y.one('.catesLayout').ancestor().remove(true);
		    var bd = document.getElementById('page'),
			    layout = document.createElement('div');
		    layout.className = 'catesLayout popupLayout';
		    layout.innerHTML = '<div class="text">'
			    + '<span id="catesTitle">添加分类</span>'
			    + '	<input class="inputText" id="catesName" type="text" maxlength="50" value="输入分类名称" />'
			    + '	<input class="inputText" id="catesIndex" type="text" maxlength="8" value="输入排序号" />'
			    + '	<div class="btnSprite"><a href="javascript:;" class="btn comfirm">确定</a><a href="javascript:;" class="btn cancel">取消</a></div>'
			    + '</div>';
		    bd.appendChild(layout);
		    var overlay = new Y.Overlay({
			srcNode: '.catesLayout',
			width: "369px",
			height: 278,
			visible: false,
			shim: false,
			centered: true,
			plugins: [{fn: Y.AnimPlugin, cfg: {duration: 0.5}}]
		    });
		    overlay.render();
		    return overlay;
		}
	    }
	}
    });

}, '1.0', {requires: ['base-build', 'plugin', 'overlay', 'popup-plugin']});

YUI.add('disInfoItem-plugin', function(Y) {
    Y.Plugin.DisInfoItem = Y.Base.create('DisInfoItem', Y.Plugin.Base, [], {
	initializer: function() {
	    if (this.get('rendered')) {
		this.addData();
	    } else {
		this.afterHostMethod('dataSuccess', this.addData);
	    }
	},
	destructor: function() {
	    Y.one('#cates').empty();
	},
	addData: function() {
	    var self = this,
		    hostObj = this.get('host');
	    var d = hostObj.get('dataTemp');
	    if (hostObj.queryString.type == 'edit') {
		Y.one('#disNameTxt').set('value', d.DishName);
		Y.one('#disNameQP').setAttribute('value', d.dishQuanPin);
		Y.one('#disNameJP').setAttribute('value', d.dishJianPin);
		Y.one('#disNameIndex').setAttribute('value', d.DishDisplaySequence);
		Y.one('#disCommentProfile').set('value', d.DishDescShort);
		Y.one('#disCommentDetail').set('value', d.DishDescDetail);
		hostObj.dishID = d.DishID;
	    }

	    validate.setLength('#disNameSprite', 50);
	    if (Y.one('#disNameIndex')) {
		validate.setLength('#disNameIndexSprite', 8);
		validate.checkNumber('#disNameIndexSprite');
	    }

	}
    }, {
	NS: 'DisInfoItem',
	ATTRS: {
	    title: {
		value: ''
	    }
	}
    });

}, '1.0', {requires: ['base-build', 'plugin']});


YUI.add('disConfigDataRow', function(Y) {
    Y.DisConfigDataRow = Y.Base.create('DisConfigDataRow', Y.Model, [], {//
	initializer: function() {
	    this.after('DishIDChange', function(ev) {
		Y.log('上一状态: ' + ev.prevVal + ', 当前: ' + ev.newVal);
	    });
	}
    }, {
	ATTRS: {
	    DishID: {},
	    DishPrice: {},
	    markName: {},
	    vip: {},
	    vipNon: {}
	}
    });
}, '1.0', {requires: ['model']});

YUI.add('disConfigItem-plugin', function(Y) {
    Y.Plugin.DisConfigItem = Y.Base.create('DisConfigItem', Y.Plugin.Base, [], {
	initializer: function() {
	    if (this.get('rendered')) {
		this.addData();
	    } else {
		this.afterHostMethod('dataSuccess', this.addData);
	    }
	},
	destructor: function() {
	    Y.one('#disConfigPrice').empty();
	},
	attachData: function(e) {
	    var t = e.currentTarget,
		    className = t.getAttribute('class'),
		    isUp = className.indexOf('up') > -1,
		    itemSprite = t.ancestor('.itemSprite');
	    var dataAttach = itemSprite.one('.dataAttach');
	    if (isUp) {
		t.removeClass('up');
		dataAttach.removeClass('hide');
	    } else {
		t.addClass('up');
		dataAttach.addClass('hide');
	    }
	},
	deleteItem: function(e) {
	    e.preventDefault();
	    e.stopPropagation();
	    /* 处理单行数据添加\删除*/
	    var t = e.currentTarget;
	    var input = t.siblings('input');
	    var tasteid = input.get('value');

	    var container = Y.all('#disConfigPrice .catesKouWei input');

	    var ioHandler = {
		method: 'POST',
		data: 'm=dish_info_taste_delete&tasteid=' + tasteid,
		headers: {'Content-Type': 'application/x-www-form-urlencoded; charset=utf-8'},
		on: {
		    success: function(id, rsp) {
			if (rsp.responseText == '-1000') {
			    VA.Singleton.popup.timeout();
			    return;
			}
			var res = Y.JSON.parse(rsp.responseText);
			var okHandler = function() {
			    //
			};
			if (res.list[0].status == '1') {
			    container.each(function(o) {
				if (o.getAttribute('value') == tasteid) {
				    o.ancestor('li').hide();
				}
			    });

			} else if (res.list[0].status == '-2') {
			    VA.Singleton.popup.panel.set('headerContent', '提示信息');
			    VA.Singleton.popup.panel.set('bodyContent', '该口味已被菜品引用了，无法删除');
			    VA.Singleton.popup.panel.set('buttons', [VA.Singleton.popup.get('okButton')]);
			    VA.Singleton.popup.set('ok', okHandler);
			    VA.Singleton.popup.showPanel();
			} else if (res.list[0].status == '-1') {
			    VA.Singleton.popup.panel.set('headerContent', '提示信息');
			    VA.Singleton.popup.panel.set('bodyContent', '删除操作失败');
			    VA.Singleton.popup.panel.set('buttons', [VA.Singleton.popup.get('okButton')]);
			    VA.Singleton.popup.showPanel();
			}
		    },
		    failure: function(id, rsp) {
			Y.log(rsp.status);
		    }
		}
	    };
	    Y.io('ajax/doSybWeb.ashx', ioHandler);
	},
	updateItem: function(e) {
	    e.stopPropagation();
	    this.updateLayout = e.currentTarget;
	    var input = this.updateLayout.siblings('input');
	    this.configTasteid = input.get('value');
	    this.configContainer = Y.all('#disConfigPrice .catesKouWei input');

	    this.configLayoutUpdate();
	},
	submitItem: function(e) {
	    e.preventDefault();
	    e.stopPropagation();
	    var t = e.currentTarget;
	    var that = this;
	    var container = this.currentAllCates;

	    var className = t.getAttribute('class'),
		    isComfirm = className.indexOf('comfirm') > -1;
	    var inputs = Y.all('.configLayout .inputText');
	    var n = inputs._nodes;
	    var host = this.get('host');

	    var editStatus = this.get('editStatus');
	    if (isComfirm) {
		var htmlEscape = function(text) {
		    return text.replace(/[<>"&\\]/g, function(match, pos, originalText) {
			switch (match) {
			    case "<":
				return "&lt;";
				break;
			    case ">":
				return "&gt;";
				break;
			    case "&":
				return "&amp;";
				break;
			    case "\"":
				return "&quot;";
				break;
			    case "\\":
				return "\\\\";
				break;
			    default:
				break;
			}
		    });
		};

		var dataObj = {};
		dataObj.shopid = host.get('shopId');
		dataObj.tastename = n[0].value;
		dataObj.tastenameURI = encodeURIComponent(htmlEscape(dataObj.tastename));
		var hasOnlyWS = new RegExp("^\\s{0,}$", "g");
		var okHandler = function() {
		};
		if (dataObj.tastename == '输入口味名称' || hasOnlyWS.test(dataObj.tastename)) {
		    VA.Singleton.popup.panel.set('headerContent', '提示信息');
		    VA.Singleton.popup.panel.set('bodyContent', '请输入可用的口味分类名称');
		    VA.Singleton.popup.panel.set('buttons', [VA.Singleton.popup.get('okButton')]);
		    VA.Singleton.popup.set('ok', okHandler);
		    VA.Singleton.popup.showPanel();
		    return;
		}

		var msg = {};
		msg.unique = '的口味名称重复';
		msg.error = '口味失败了';
		if (editStatus === 0) {
		    dataObj.dataStr = 'm=dish_info_taste_insert&shopid=' + dataObj.shopid + '&tastename=' + dataObj.tastenameURI;
		    dataObj.handler = function(info) {
			var tasteid = info;
			container.append('<li><input class="inputCheck" type="checkbox" value="' + tasteid + '" name="cates" /><label for="">' + dataObj.tastename + '</label><span class="layout">' + dataObj.tastename + '</span></li>');

			that.setSelectAllCheckboxGroup();
		    };
		    msg.unique = '添加' + msg.unique;
		    msg.error = '添加' + msg.error;
		} else if (editStatus === 1) {
		    dataObj.dataStr = 'm=dish_info_taste_edit&shopid=' + dataObj.shopid + '&tasteid=' + that.configTasteid + '&tastename=' + dataObj.tastenameURI;
		    dataObj.handler = function() {
			that.updateLayout.set('text', dataObj.tastename);
			that.configContainer.each(function(o) {
			    if (o.getAttribute('value') == that.configTasteid) {
				o.siblings('label').set('text', dataObj.tastename);
			    }
			});

			that.setSelectAllCheckboxGroup();
		    };

		    msg.unique = '修改' + msg.unique;
		    msg.error = '修改' + msg.error;
		}
		;

		var ioHandler = {
		    method: 'POST',
		    data: dataObj.dataStr,
		    headers: {'Content-Type': 'application/x-www-form-urlencoded; charset=utf-8'},
		    on: {
			success: function(id, rsp) {
			    if (rsp.responseText == '-1000') {//登录超时
				VA.Singleton.popup.timeout();
				return;
			    }
			    var res = Y.JSON.parse(rsp.responseText);
			    var status = res.list[0].status;
			    var info = res.list[0].info;
			    var okHandler = function() {
				//
			    };
			    if (status == 1) {
				dataObj.handler(info);

			    } else if (status == -2) {
				VA.Singleton.popup.panel.set('headerContent', '提示信息');
				VA.Singleton.popup.panel.set('bodyContent', msg.unique);
				VA.Singleton.popup.panel.set('buttons', [VA.Singleton.popup.get('okButton')]);
				VA.Singleton.popup.set('ok', okHandler);
				VA.Singleton.popup.showPanel();
			    } else if (status == -1) {
				VA.Singleton.popup.panel.set('headerContent', '提示信息');
				VA.Singleton.popup.panel.set('bodyContent', msg.error);
				VA.Singleton.popup.panel.set('buttons', [VA.Singleton.popup.get('okButton')]);
				VA.Singleton.popup.set('ok', okHandler);
				VA.Singleton.popup.showPanel();
			    }
			},
			failure: function(id, rsp) {
			    Y.log(rsp.status);
			}
		    }
		};
		Y.io('ajax/doSybWeb.ashx', ioHandler);
		this.get('popupLayout').hide();
	    } else {
		//
		this.get('popupLayout').hide();
	    }
	},
	disConfigEdit: function(e) {
	    e.preventDefault();
	    e.stopPropagation();
	    var self = this,
		    targetItem = e.currentTarget,
		    hostObj = this.get('host'),
		    dataTaste = '',
		    dataIngredient = '';

	    var className = targetItem.getAttribute('class'),
		    isDelete = className.indexOf('delete') > -1,
		    isAddPrice = className.indexOf('addPrice') > -1,
		    dataContainer = Y.one('#disConfigPrice');
	    var isAdd = className.indexOf('add') > -1,
		    isDeleteTaste = className.indexOf('edit') > -1,
		    isUpdate = className.indexOf('update') > -1,
		    isEditStatus = className.indexOf('abled') > -1;
	    if (isDelete) {
		var rel = targetItem.getAttribute('rel');
		if (rel == 1) {
		    targetItem.setAttribute('rel', -1);
		} else if (rel == 2) {
		    targetItem.setAttribute('rel', 3);
		}
		var dataRow = targetItem.ancestors('.itemSprite');
		dataRow.hide();
	    } else if (isAddPrice) {
		hostObj.updateConfigData();
		var count = dataContainer.get('childNodes')._nodes.length;
		var dConfig_Ingredient = hostObj.get('dataConfig').DishIngredientsList,
			dConfig_Taste = hostObj.get('dataConfig').DishTasteList;
		var dataRow = '<tr class="itemSprite">'
			+ '	<td colspan="2" class="item">'
			+ '		<table class="dataPrice" cellpadding="0" cellspacing="0" border="0">'
			+ '			<tr>'
			+ '				<td><span class="title required"><span class="tip">需要输入<i class="txt">规格[名称]</i>!<i class="close">x</i></span>规　　格：<i class="icon">*</i></span><input class="inputText" maxlength="50" type="text" value="份" name="" /> </td>'
			+ '				<td><span class="title required number"><span class="tip">请输入<span class="txt">价格</span>（最多两位小数）!<i class="close">x</i></span>价　　格：<i class="icon">*</i></span><input class="inputText" maxlength="8" type="text" value="" name="" /> </td>'
			+ '				<td><span class="title">掌中宝编号：</span><input class="inputText" maxlength="8" type="text" value="" name="" /> </td>'
			+ '				<td><a class="btn delete" rel="1" href="dishManage.aspx?DishID=0&DishNeedWeigh=false&DishPriceID=0&DishPriceStatus=1">删除</a></td>'
			+ '			</tr>'
			+ '			<tr>'
			+ '				<td><span class="title">VIP 折扣：</span><span class="discount"><input type="radio" value="" checked="checked" name="vipadd' + count + '" />是</span><span class="discount"><input type="radio" value="" name="vipadd' + count + '" />否</span></td>'
			+ '				<td><span class="title">是否停售  ：</span><span class="discount"><input type="radio" value="" name="saleadd' + count + '" />是</span><span class="discount"><input type="radio" value="" checked="checked" name="saleadd' + count + '" /> 否</span></td>'
			+ '				<td><span class="title">配菜可选份数：</span><input id="dishIngredientsMinAmount"  class="inputText" style="width:40px" value="0" type="text" /> - <input id="dishIngredientsMaxAmount"  class="inputText" style="width:40px" value="1000" type="text" /></td>'
			+ '				<td><a class="more up" href="javascript:;">更多设置（口味、配菜）</a></td>'
			+ '			</tr>'
			+ '		</table>'
			+ '		<table class="dataAttach hide" cellpadding="0" cellspacing="0" border="0">'
			+ '			<tr class="bot">'
			+ '				<td width="75"><span class="title">选择口味：</span></td>'
			+ '				<td width="608">'
			+ '					<div class="checkAll"><input class="inputCheck" id="catesKouWei_checkAll_' + count + '" type="checkbox" name="catesKouWei_cb" value="" /> 全选</div>'
			+ '					<ul class="cates catesKouWei catesKouWei_add' + count + '">'
			+ '					</ul>'
			+ '				</td>'
			+ '				<td width="225">'
			+ '					<div class="btnSprite" id="dishTasteBtn">'
			+ '						<a class="btn add" href="javascript:;">添加口味</a>'
			+ '						<a class="btn update" href="javascript:;">修改口味</a>'
			+ '						<a class="btn edit" href="javascript:;">删除口味</a>'
			+ '					</div>'
			+ '				</td>'
			+ '			</tr>'
			+ '			<tr>'
			+ '				<td width="75"><span class="title">选择配菜：</span></td>'
			+ '				<td width="833" colspan="2">'
			+ '					<div class="checkAll"><input class="inputCheck" id="catesPeiCai_checkAll_' + count + '" type="checkbox" name="catesPeiCai_cb" value="" /> 全选</div>'
			+ '					<ul class="cates catesPeiCai catesPeiCai_add' + count + '">'
			+ '					</ul>'
			+ '				</td>'
			+ '			</tr>'
			+ '		</table>'
			+ '	</td>'
			+ '</tr>';
		dataContainer.appendChild(dataRow);
		for (var j = 0; j < dConfig_Taste.length; j++) {
		    dataTaste += '<li><input class="inputCheck" type="checkbox" value="' + dConfig_Taste[j].Id + '" name="cates" /><label for="">' + dConfig_Taste[j].Name + '</label><span class="layout">' + dConfig_Taste[j].Name + '</span></li>';
		}
		for (var j = 0; j < dConfig_Ingredient.length; j++) {
		    dataIngredient += '<li><input class="inputCheck" type="checkbox" value="' + dConfig_Ingredient[j].Id + '" name="cates" />' + dConfig_Ingredient[j].Name + '</li>';
		}

		Y.one('.catesKouWei_add' + count).setHTML(dataTaste);
		Y.one('.catesPeiCai_add' + count).setHTML(dataIngredient);

	    } else {
		var parent = targetItem.ancestor('.btnSprite');
		var currentRow = targetItem.ancestor('tr');
		this.currentCates = currentRow.one('.cates');
		var layoutIcon = this.currentCates.all('.layout');
		this.currentAllCates = dataContainer.all('.catesKouWei');

		if (isAdd) {
		    var updateBtn = parent.one('.update');
		    updateBtn.removeClass('abled');
		    updateBtn.set('text', '修改口味');
		    var deleteBtn = parent.one('.edit');
		    deleteBtn.removeClass('abled');
		    deleteBtn.set('text', '删除口味');

		    layoutIcon.hide();

		    Y.one('#configTitle').set('text', '添加口味');
		    this.get('popupLayout').show();
		    this.set('editStatus', 0);

		    var inputs = Y.all('.configLayout .inputText');
		    var n = inputs._nodes[0];
		    inputs.on('focus', function(ev) {
			var t = ev.currentTarget;
			var v = t.get('value');
			if (v == '输入口味名称') {
			    t.set('value', '');
			}
		    }, this);
		    Y.one(n).on('blur', function(ev) {
			var t = ev.currentTarget;
			var v = t.get('value');
			if (v == '') {
			    t.set('value', '输入口味名称');
			}
		    });
		    n.value = '输入口味名称';


		} else if (isUpdate) {
		    this.get('popupLayout').hide();
		    this.set('editStatus', 1);

		    if (!isEditStatus) {
			targetItem.addClass('abled');
			targetItem.set('text', '完成修改');

			var siblingsBtn = parent.one('.edit');
			siblingsBtn.removeClass('abled');
			siblingsBtn.set('text', '删除口味');

			layoutIcon.setAttribute("class", "layout update");
			layoutIcon.setStyles({"display": "block"});
		    } else {
			targetItem.removeClass('abled');
			targetItem.set('text', '修改口味');
			layoutIcon.setAttribute("class", "layout");
			layoutIcon.setStyles({"display": "none"});
		    }
		} else if (isDeleteTaste) {
		    this.get('popupLayout').hide();

		    if (!isEditStatus) {
			targetItem.addClass('abled');
			targetItem.set('text', '完成删除');

			var siblingsBtn = parent.one('.update');
			siblingsBtn.removeClass('abled');
			siblingsBtn.set('text', '修改口味');

			layoutIcon.setAttribute("class", "layout delete");
			layoutIcon.setStyle("display", "block");
		    } else {
			targetItem.removeClass('abled');
			targetItem.set('text', '删除口味');
			layoutIcon.setAttribute("class", "layout");
			layoutIcon.setStyle("display", "none");
		    }
		}
	    }
	    this.setSelectAllCheckboxGroup();
	},
	setSelectAllCheckboxGroup: function() {
	    Y.all('.checkAll .inputCheck').each(function(instance, index, nodeList) {
		var catesParent = instance.ancestor('.checkAll').next('.cates');
		var inputCheckes = catesParent.all('.inputCheck');
		new Y.SelectAllCheckboxGroup(instance, inputCheckes);
	    });
	},
	configLayoutUpdate: function() {
	    var inputs = Y.all('.configLayout .inputText');
	    var n = inputs._nodes;
	    n[0].value = this.updateLayout.get('text');
	    Y.one('#configTitle').set('text', '修改口味');
	    this.get('popupLayout').show();
	},
	addData: function() {
	    var self = this,
		    hostObj = this.get('host');
	    var d = hostObj.get('dataTemp').DishPriceList,
		    dConfig_Ingredient = hostObj.get('dataConfig').DishIngredientsList,
		    dConfig_Taste = hostObj.get('dataConfig').DishTasteList;
	    var dataRow = '',
		    dataIngredient = '',
		    dataTaste = '',
		    checkedStr = '',
		    checkedReg = null;
	    Y.one('#disConfigPrice').empty(true);
	    if (hostObj.queryString.type == 'edit') {
		var row = {};
		for (var i = 0; i < d.length; i++) {
		    var ScaleName = d[i].ScaleName,
			    DishID = d[i].DishID,
			    DishNeedWeigh = d[i].DishNeedWeigh,
			    DishPriceStatus = d[i].DishPriceStatus,
			    DishPriceID = d[i].DishPriceID,
			    DishPrice = d[i].DishPrice,
			    markName = d[i].markName,
			    vip = (d[i].vipDiscountable) ? 'checked="checked" ' : '',
			    vipNon = (!d[i].vipDiscountable) ? 'checked="checked" ' : '',
			    operStatus = d[i].operStatus,
			    sale = (d[i].DishSoldout) ? 'checked="checked" ' : '',
			    saleNon = (!d[i].DishSoldout) ? 'checked="checked" ' : '';

		    var dList_Ingredient = d[i].DishIngredientsList,
			    dList_Taste = d[i].DishTasteList;

		    var dishIngredientsMaxAmount = d[i].dishIngredientsMaxAmount <= 0 ? 1000 : d[i].dishIngredientsMaxAmount;
		    var dishIngredientsMinAmount = d[i].dishIngredientsMinAmount <= 0 ? 0 : d[i].dishIngredientsMinAmount;
		    dataRow += '<tr class="itemSprite">'
			    + '	<td colspan="2" class="item">'
			    + '		<table class="dataPrice" cellpadding="0" cellspacing="0" border="0">'
			    + '			<tr>'
			    + '				<td><span class="title required"><span class="tip">需要输入<i class="txt">规格[名称]</i>!<i class="close">x</i></span>规　　格：<i class="icon">*</i></span><input class="inputText" maxlength="50" type="text" value="' + ScaleName + '" name="" /> </td>'
			    + '				<td><span class="title required number"><span class="tip">请输入<i class="txt">价格</i>（最多两位小数）!<i class="close">x</i></span>价　　格：<i class="icon">*</i></span><input class="inputText" maxlength="8" type="text" value="' + DishPrice + '" name="" /> </td>'
			    + '				<td><span class="title">掌中宝编号：</span><input class="inputText" maxlength="8" type="text" value="' + markName + '" name="" /> </td>'
			    + '				<td><a class="btn delete" rel="' + operStatus + '" href="dishManage.aspx?DishID=' + DishID + '&DishNeedWeigh=' + DishNeedWeigh + '&DishPriceID=' + DishPriceID + '&DishPriceStatus=' + DishPriceStatus + '">删除</a></td>'
			    + '			</tr>'
			    + '			<tr>'
			    + '				<td><span class="title">VIP 折扣：</span><span class="discount"><input type="radio" ' + vip + ' value="1" checked="checked" name="vip' + i + '" />是</span><span class="discount"><input type="radio" ' + vipNon + ' value="" name="vip' + i + '" />否</span></td>'
			    + '				<td><span class="title">是否停售  ：</span><span class="discount"><input type="radio" ' + sale + ' value="" checked="checked" name="sale' + i + '" />是</span><span class="discount"><input type="radio" ' + saleNon + ' value="" name="sale' + i + '" /> 否</span></td>'
			    + '				<td><span class="title">配菜可选份数：</span><input id="dishIngredientsMinAmount"  class="inputText" style="width:40px" value=' + dishIngredientsMinAmount + ' type="text" /> - <input id="dishIngredientsMaxAmount"  class="inputText" style="width:40px" value=' + dishIngredientsMaxAmount + ' type="text" /></td>'
			    + '				<td><a class="more up" href="javascript:;">更多设置（口味、配菜）</a></td>'
			    + '			</tr>'
			    + '		</table>'
			    + '		<table class="dataAttach hide" cellpadding="0" cellspacing="0" border="0">'
			    + '			<tr class="bot">'
			    + '				<td width="75"><span class="title">选择口味：</span></td>'
			    + '				<td width="608">'
			    + '					<div class="checkAll"><input class="inputCheck" id="catesKouWei_checkAll_' + i + '" type="checkbox" name="catesKouWei_cb" value="" /> 全选</div>'
			    + '					<ul class="cates catesKouWei catesKouWei_' + i + '">'
			    + '					</ul>'
			    + '				</td>'
			    + '				<td width="225">'
			    + '					<div class="btnSprite" id="dishTasteBtn">'
			    + '						<a class="btn add" href="javascript:;">添加口味</a>'
			    + '						<a class="btn update" href="javascript:;">修改口味</a>'
			    + '						<a class="btn edit" href="javascript:;">删除口味</a>'
			    + '					</div>'
			    + '				</td>'
			    + '			</tr>'
			    + '			<tr>'
			    + '				<td width="75"><span class="title">选择配菜：</span></td>'
			    + '				<td width="833" colspan="2">'
			    + '					<div class="checkAll"><input class="inputCheck" id="catesPeiCai_checkAll_' + i + '" type="checkbox" name="catesPeiCai_cb" value="" /> 全选</div>'
			    + '					<ul class="cates catesPeiCai catesPeiCai_' + i + '">'
			    + '					</ul>'
			    + '				</td>'
			    + '			</tr>'
			    + '		</table>'
			    + '	</td>'
			    + '</tr>';

		    checkedStr = '';
		    for (var iSnd = 0; iSnd < dList_Taste.length; iSnd++) {
			checkedStr += '(^' + dList_Taste[iSnd] + '$)|';
		    }
		    if (checkedStr.indexOf('|') > -1) {
			checkedStr = checkedStr.slice(0, -1);
		    }
		    checkedStr = checkedStr ? checkedStr : 'null';
		    checkedReg = new RegExp(checkedStr, 'i');
		    for (var j = 0; j < dConfig_Taste.length; j++) {
			if (checkedReg.test(dConfig_Taste[j].Id)) {
			    dataTaste += '<li><input class="inputCheck" type="checkbox" value="' + dConfig_Taste[j].Id + '" checked="checked" name="cates" /><label for="">' + dConfig_Taste[j].Name + '</label><span class="layout">' + dConfig_Taste[j].Name + '</span></li>';
			} else {
			    dataTaste += '<li><input class="inputCheck" type="checkbox" value="' + dConfig_Taste[j].Id + '" name="cates" /><label for="">' + dConfig_Taste[j].Name + '</label><span class="layout">' + dConfig_Taste[j].Name + '</span></li>';
			}
		    }
		    checkedReg = null;
		    checkedStr = '';
		    for (var iThir = 0; iThir < dList_Ingredient.length; iThir++) {
			checkedStr += '(^' + dList_Ingredient[iThir] + '$)|';
		    }
		    if (checkedStr.indexOf('|') > -1) {
			checkedStr = checkedStr.slice(0, -1);
		    }
		    checkedStr = checkedStr ? checkedStr : 'null';
		    checkedReg = new RegExp(checkedStr, 'i');//
		    for (var j = 0; j < dConfig_Ingredient.length; j++) {
			if (checkedReg.test(dConfig_Ingredient[j].Id)) {
			    dataIngredient += '<li><input class="inputCheck" type="checkbox" value="' + dConfig_Ingredient[j].Id + '" checked="checked" name="cates" />' + dConfig_Ingredient[j].Name + '</li>';
			} else {
			    dataIngredient += '<li><input class="inputCheck" type="checkbox" value="' + dConfig_Ingredient[j].Id + '" name="cates" />' + dConfig_Ingredient[j].Name + '</li>';
			}
		    }
		    checkedReg = null;

		    Y.one('#disConfigPrice').append(dataRow);
		    dataRow = '';
		    Y.one('.catesKouWei_' + i).setHTML(dataTaste);
		    dataTaste = '';
		    Y.one('.catesPeiCai_' + i).setHTML(dataIngredient);
		    dataIngredient = '';
		}

	    } else {
		for (var i = 0; i < 1; i++) {
		    dataRow += '<tr class="itemSprite">'
			    + '	<td colspan="2" class="item">'
			    + '		<table class="dataPrice" cellpadding="0" cellspacing="0" border="0">'
			    + '			<tr>'
			    + '				<td><span class="title required"><span class="tip">需要输入<i class="txt">规格[名称]</i>!<i class="close">x</i></span>规　　格：<i class="icon">*</i></span><input class="inputText" maxlength="50" type="text" value="份" name="" /> </td>'
			    + '				<td><span class="title required number"><span class="tip">请输入<span class="txt">价格</span>（最多两位小数）!<i class="close">x</i></span>价　　格：<i class="icon">*</i></span><input class="inputText" maxlength="8" type="text" value="" name="" /> </td>'
			    + '				<td><span class="title">掌中宝编号：</span><input class="inputText" maxlength="8" type="text" value="" name="" /> </td>'
			    + '				<td><a class="btn delete" rel="1" href="dishManage.aspx?DishID=0&DishNeedWeigh=false&DishPriceID=0&DishPriceStatus=1">删除</a></td>'
			    + '			</tr>'
			    + '			<tr>'
			    + '				<td><span class="title">VIP 折扣：</span><span class="discount"><input type="radio" value="" checked="checked" name="vip" />是</span><span class="discount"><input type="radio" value="" name="vip" />否</span></td>'
			    + '				<td><span class="title">是否停售  ：</span><span class="discount"><input type="radio" value="" name="sale" />是</span><span class="discount"><input type="radio" value="" checked="checked" name="sale" /> 否</span></td>'
			    + '				<td><span class="title">配菜可选份数：</span><input id="dishIngredientsMinAmount"  class="inputText" style="width:40px" value="0" type="text" /> - <input id="dishIngredientsMaxAmount"  class="inputText" style="width:40px" value="1000" type="text" /></td>'
			    + '				<td><a class="more up" href="javascript:;">更多设置（口味、配菜）</a></td>'
			    + '			</tr>'
			    + '		</table>'
			    + '		<table class="dataAttach hide" cellpadding="0" cellspacing="0" border="0">'
			    + '			<tr class="bot">'
			    + '				<td width="75"><span class="title">选择口味：</span></td>'
			    + '				<td width="608">'
			    + '					<div class="checkAll"><input class="inputCheck" id="catesKouWei_checkAll_' + i + '" type="checkbox" name="catesKouWei_cb" value="" /> 全选</div>'
			    + '					<ul class="cates catesKouWei">'
			    + '					</ul>'
			    + '				</td>'
			    + '				<td width="225">'
			    + '					<div class="btnSprite" id="dishTasteBtn">'
			    + '						<a class="btn add" href="javascript:;">添加口味</a>'
			    + '						<a class="btn update" href="javascript:;">修改口味</a>'
			    + '						<a class="btn edit" href="javascript:;">删除口味</a>'
			    + '					</div>'
			    + '				</td>'
			    + '			</tr>'
			    + '			<tr>'
			    + '				<td width="75"><span class="title">选择配菜：</span></td>'
			    + '				<td width="833" colspan="2">'
			    + '					<div class="checkAll"><input class="inputCheck" id="catesPeiCai_checkAll_' + i + '" type="checkbox" name="catesPeiCai_cb" value="0" /> 全选</div>'
			    + '					<ul class="cates catesPeiCai">'
			    + '					</ul>'
			    + '				</td>' 
			    + '			</tr>'
			    + '		</table>'
			    + '	</td>'
			    + '</tr>';


		    for (var j = 0; j < dConfig_Ingredient.length; j++) {
			dataIngredient += '<li><input class="inputCheck" type="checkbox" value="' + dConfig_Ingredient[j].Id + '" name="cates" />' + dConfig_Ingredient[j].Name + '</li>';
		    }

		    for (var j = 0; j < dConfig_Taste.length; j++) {
			dataTaste += '<li><input class="inputCheck" type="checkbox" value="' + dConfig_Taste[j].Id + '" name="cates" /><label for="">' + dConfig_Taste[j].Name + '</label><span class="layout">' + dConfig_Taste[j].Name + '</span></li>';
		    }
		    Y.one('#disConfigPrice').empty(true);
		    Y.one('#disConfigPrice').append(dataRow);
		    dataRow = '';
		    Y.one('.catesKouWei').setHTML(dataTaste);
		    dataTaste = '';
		    Y.one('.catesPeiCai').setHTML(dataIngredient);
		    dataIngredient = '';
		}

	    }

	    this.setSelectAllCheckboxGroup();
	}
    }, {
	NS: 'DisConfigItem',
	ATTRS: {
	    editStatus: {
		value: 0
	    },
	    popupLayout: {
		valueFn: function() {
		    var bd = document.getElementById('page'),
			    layout = document.createElement('div');
		    layout.className = 'configLayout popupLayout';
		    layout.innerHTML = '<div class="text">'
			    + '<span id="configTitle">添加口味</span>'
			    + '	<input class="inputText" maxlength="50" type="text" value="输入口味名称" />'
			    + '	<div class="btnSprite"><a href="javascript:;" class="btn comfirm">确定</a><a href="javascript:;" class="btn cancel">取消</a></div>'
			    + '</div>';
		    bd.appendChild(layout);
		    var overlay = new Y.Overlay({
			srcNode: '.configLayout',
			width: "369px",
			height: "208px",
			visible: false,
			shim: false,
			centered: true,
			plugins: [{fn: Y.AnimPlugin, cfg: {duration: 0.5}}]
		    });
		    overlay.render();
		    return overlay;
		}
	    }
	}
    });

}, '1.0', {requires: ['base-build', 'plugin', 'model', 'disConfigDataRow', 'gallery-checkboxgroups']});

YUI.add('disUploadItem-plugin', function(Y) {
    Y.Plugin.DisUploadItem = Y.Base.create('DisUploadItem', Y.Plugin.Base, [], {
	initializer: function() {
	    if (this.get('rendered')) {
		this.addData();
	    } else {
		this.afterHostMethod('dataSuccess', this.addData);
	    }
	    Y.all('#disUpload .btn').on('click', this.btnSubmit, this);
	    VA.Util.sessionID = '';
	    var hostObj = this.get('host');
	    VA.Util.disManageType = hostObj.get('queryString').type;
	    var btn = this.get('btn');

	    if (VA.Util.disManageType == 'mutil') {
		if (dishManageMutilMethod.successPicList) {
		    dishManageMutilMethod.successPicList = 0;
		}
		dishListMethod.getLavePics();

		if (DishList.lavePicList) {
		    uploadSuccess('', DishList.lavePicList);
		    return;
		}
	    }
	    
	},
	createSWFUpload: function() {
	    var btn = this.get('btn'), hostObj = this.get('host');
	    var imgCapacity = hostObj.get('dataConfig').dishImage.space.slice(0,-2);
	    var imgCapacityStr = Math.round(imgCapacity/1024*100)/100;
	    if(Y.one("#imgCapacity")){
		Y.one("#imgCapacity").set("text",imgCapacityStr);
	    }
	    new SWFUpload({
		upload_url: btn.uploadUrl,
		post_params: btn.postParams,
		file_size_limit: imgCapacityStr+" MB",
		file_types: "*.jpg",
		file_types_description: "JPG Images",
		file_upload_limit: "", //
		file_queue_error_handler: fileQueueError,
		file_dialog_complete_handler: fileDialogComplete,
		upload_start_handler:uploadStart,
		upload_progress_handler: uploadProgress,
		upload_error_handler: uploadError,
		upload_success_handler: uploadSuccess,
		upload_complete_handler: uploadComplete,
		button_image_url: btn.imageUrl,
		button_placeholder_id: btn.id,
		button_width: btn.w,
		button_height: btn.h,
		button_text: '',
		button_text_style: '',
		button_text_top_padding: 0,
		button_text_left_padding: 0,
		button_text_bottom_padding: 0,
		button_text_right_padding: 0,
		flash_url: "scripts/jquery.upload/swfupload.swf",
		debug: false
	    });
	},
	btnSubmit: function(e) {
	    e.preventDefault();
	    e.stopPropagation();
	    var self = this,
		    hostObj = this.get('host');
	    var d = hostObj.get('dataTemp');
	    var t = e.currentTarget,
		    classStr = t.getAttribute('class'),
		    isCancel = classStr.indexOf('cancel');
	    var okHandler = function() {
		//
	    };
	    if (isCancel != -1) {
		this.addData();
		this.imgReset(hostObj.imgAreaObj);
		VA.Util.sessionID = !!d.DishImageUrlBig ? d.DishImageUrlBig : '';
		VA.Util.picStatus = '1';
	    } else {
		VA.Singleton.popup.panel.set('headerContent', '图片裁剪');
		VA.Singleton.popup.panel.set('bodyContent', '当前截取的图片已成功提交!');
		VA.Singleton.popup.panel.set('buttons', [VA.Singleton.popup.get('okButton')]);
		VA.Singleton.popup.set('ok', okHandler);
		VA.Singleton.popup.showPanel();
	    }
	},
	imgReset: function(arg) {
	    $(function() {
		arg.setOptions({show: true, x1: 0, y1: 0, x2: 320, y2: 240});

		var ratioW = 4, ratioH = 3, selectRect = {x: 0, y: 0, w: 320, h: 240};
		var scaleX = ratioW * 100 / 320, scaleY = ratioH * 100 / 240;
		$('#preview img').css({
		    width: Math.round(scaleX * 48 * ratioW),
		    height: Math.round(scaleY * 48 * ratioH),
		    marginLeft: -Math.round(scaleX * 0 * 0.6),
		    marginTop: -Math.round(scaleY * 0 * 0.6)
		});
	    });
	},
	addData: function() {
	    this.createSWFUpload();
	    var self = this,
		    hostObj = this.get('host');
	    var d = hostObj.get('dataTemp').DishImageUrlBig;
	    self.imgSize = hostObj.get('dataConfig').dishImage.size.split(",");
	    var urlImg = '';
	    switch(hostObj.queryString.type){
		case "edit":
		    if (d) {
			urlImg = urlImg = 'thumbnail.aspx?id=' + d;
		    } else {
			urlImg = 'images/nonImg.png';
		    }
		    Y.all('#uploadContent img').each(function(o) {
			o.set('src', urlImg);
		    }); 
		break;
	        case "add":
		    urlImg = 'images/nonImg.png';
		    Y.all('#uploadContent img').each(function(o) {
			o.set('src', urlImg);
		    });
		break;
	        case "mutil":
		    return;
	        break;
	    }
	    var ratioW = 4, ratioH = 3, selectRect = {x: 0, y: 0, w: 320, h: 240};
	    function preview(img, selection) {
		if (!selection.width || !selection.height) {
		    return;
		}
		var scaleX = ratioW * 100 / selection.width,
			scaleY = ratioH * 100 / selection.height;
		$('#preview img').css({
		    width: Math.round(scaleX * 48 * ratioW),
		    height: Math.round(scaleY * 48 * ratioH),
		    marginLeft: -Math.round(scaleX * selection.x1 * 0.6),
		    marginTop: -Math.round(scaleY * selection.y1 * 0.6)
		});
	    };
	    var getRect = function(img, selection) {
		if (!!selection.width || !!selection.height) {
		    selectRect.x = selection.x1;
		    selectRect.y = selection.y1;
		    selectRect.w = selection.width;
		    selectRect.h = selection.height;
		    VA.Util.picStatus = '2';
		}
		hostObj.set('selectRect', selectRect);
	    };
	    
	    var tempImg = new Image();
            tempImg.onload = function () {
		var minWidth = Math.floor(self.imgSize[0]*320/tempImg.width);
		minWidth = minWidth>320? 320 :minWidth;
		var minHeight = Math.floor(minWidth*3/4);
		$(function() {
		    var ratioStr = ratioW + ':' + ratioH;
		    hostObj.imgAreaObj = $('#source img').imgAreaSelect({aspectRatio: ratioStr, handles: true, fadeSpeed: 400, disable: false, minWidth: minWidth, minHeight: minHeight, onSelectChange: preview, onSelectEnd: getRect, instance: true});
		});
		hostObj.set('selectRect', selectRect);
	    };
	    tempImg.src= urlImg;
	    
	},
	setImgAreaObjOptions:function(optW){
	    var hostObj = this.get('host');
	    if(optW){
		var minWidth = Math.floor(this.imgSize[0]*320/optW);
		minWidth = minWidth>320? 320 :minWidth;
		var minHeight = Math.floor(minWidth*3/4);
		hostObj.imgAreaObj.setOptions({"minWidth":minWidth, "minHeight":minHeight, hide:true});
	    }
	}
    }, {
	NS: 'DisUploadItem',
	ATTRS: {
	    btn: {
		value: ''
	    }
	}
    });
}, '1.0', {requires: ['base-build', 'plugin']});

YUI.add('DisManage', function(Y) {
    Y.DisManage = Y.Base.create('DisManage', Y.Widget, [], {
	destructor: function() {
	    this.get('containerBox').remove(true);
	},
	dataSubmit: function(arg) {
	    var isPass = true;
	    var type = this.queryString.type;
	    var self = this;

	    var rsp = new Object(),
		    d = this.get('dataTemp');
	    rsp.menuId = d.menuId;
	    //配置类型
	    if (type == 'edit') {
		rsp.DishID = d.DishID;
		rsp.DishImageUrlSmall = d.DishImageUrlSmall;
	    } else {
		rsp.DishID = 0;
		rsp.DishImageUrlBig = '';
		rsp.DishImageUrlSmall = '';
	    }

	    rsp.DishTypeList = [];
	    Y.all('#cates .inputCheck').each(function(o) {
		if (o.get('checked')) {
		    rsp.DishTypeList.push(o.getAttribute('value'));
		}
		;
	    });
	    var inputsName = Y.all('.disName .inputText')._nodes;
	    rsp.DishName = inputsName[0].value,
		    rsp.dishQuanPin = inputsName[1].value,
		    rsp.dishJianPin = inputsName[2].value,
		    rsp.DishDisplaySequence = inputsName[3].value ? inputsName[3].value : 0;
	    var DishDescShort = Y.one('#disCommentProfile').get('value');
	    var DishDescDetail = Y.one('#disCommentDetail').get('value');
	    rsp.DishDescShort = (DishDescShort != '还可以输入 153 个字') ? DishDescShort : '';
	    rsp.DishDescDetail = (DishDescDetail != '还可以输入 153 个字') ? DishDescDetail : '';
	    var rect = this.get('selectRect');
	    rsp.PicClientWidth = 320;
	    rsp.PicHeight = rect.h;
	    rsp.PicWidth = rect.w;
	    rsp.PicX = rect.x;
	    rsp.PicY = rect.y;
	    rsp.SessionId = !!VA.Util.sessionID ? VA.Util.sessionID : d.DishImageUrlBig;
	    rsp.PicStatus = VA.Util.picStatus;
	    if (!rsp.SessionId) {
		rsp.PicStatus = '1';
	    }
	    ;
	    rsp.DishImageUrlBig = rsp.SessionId;

	    rsp.DishPriceList = [];
	    var contentTable = Y.one('#disConfigPrice');
	    contentTable.all('.itemSprite').each(function(o) {
		var dataPrice = o.one('.dataPrice'),
			dataKW = o.one('.catesKouWei'),
			dataPC = o.one('.catesPeiCai');
		var inputsPrice = dataPrice.all('input')._nodes,
			tipsLayout = dataPrice.all('.tip')._nodes;
		var ScaleName = inputsPrice[0].value,
			DishPrice = inputsPrice[1].value==="" ? inputsPrice[1].value : Number(inputsPrice[1].value),
			markName = inputsPrice[2].value,
			vip = inputsPrice[3].checked ? true : false,
			sale = inputsPrice[5].checked ? true : false;
		var dishIngredientsMinAmount = (inputsPrice[7].value == '' || inputsPrice[7].value < 0) ? 0 : inputsPrice[7].value;
		var dishIngredientsMaxAmount = (inputsPrice[8].value == '' || inputsPrice[8].value < 0) ? 0 : inputsPrice[8].value;
		var inputsKW = dataKW.all('input')._nodes,
			countKW = inputsKW.length,
			inputsPC = dataPC.all('input')._nodes,
			countPC = inputsPC.length;
		var DishIngredientsList = [],
			DishTasteList = [];
		for (var i = 0; i < countPC; i++) {//
		    if (inputsPC[i].checked) {
			DishIngredientsList.push(inputsPC[i].value)
		    }
		    ;
		}
		for (var i = 0; i < countKW; i++) {
		    if (inputsKW[i].checked) {
			DishTasteList.push(inputsKW[i].value)
		    }
		    ;
		}

		var btn = dataPrice.one('.delete'),
			operStatus = btn.getAttribute('rel');

		if (operStatus == '-1')
		    return;
		if (ScaleName == '' && operStatus != '3') {
		    tipsLayout[0].style.display = 'block';
		    isPass = false;
		}
		if (!/^\d+((\.{1})\d{1,2})?$/.test(DishPrice) && operStatus != '3' || DishPrice==='') {
		    tipsLayout[1].style.display = 'block';
		    isPass = false;
		};
		
		var btnData = UIBase.getNyQueryStringArgs(btn.getAttribute('href'));

		rsp.DishPriceList.push({ "DishID": btnData.DishID, "DishIngredientsList": DishIngredientsList, "DishNeedWeigh": btnData.DishNeedWeigh, "DishPrice": DishPrice, "DishPriceID": btnData.DishPriceID, "DishPriceStatus": btnData.DishPriceStatus, "DishSoldout": sale, "DishTasteList": DishTasteList, "ScaleName": ScaleName, "backDiscountable": false, "dishIngredientsMaxAmount": dishIngredientsMaxAmount, "dishIngredientsMinAmount": dishIngredientsMinAmount, "markName": markName, "operStatus": operStatus, "vipDiscountable": vip });
	    });

	    Y.all('.close').on('click', function(e) {
		var t = e.currentTarget;
		t.ancestors('.tip').hide();
	    }, this);
	    if (rsp.DishTypeList.length == 0) {
		Y.one('.disCates .tip').setStyle('display', 'block');
		isPass = false;
	    }
	    if (!rsp.DishName) {
		Y.one('.disName .tip').setStyle('display', 'block');
		isPass = false;
	    }

	    var okHandler = function() {
		//
	    };
	    if (!isPass) {
		VA.Singleton.popup.panel.set('headerContent', '提示信息');
		VA.Singleton.popup.panel.set('bodyContent', '保存不成功，带 * 符号项为必要的信息');
		VA.Singleton.popup.panel.set('buttons', [VA.Singleton.popup.get('okButton')]);
		VA.Singleton.popup.set('ok', okHandler);
		VA.Singleton.popup.showPanel();
		this.bindUI();
		return;
	    }

	    if (type == 'mutil') {
		rsp.PicStatus = '2';
		rsp.SessionId = arg;
	    }
	    ;
	    var rspStr = Y.JSON.stringify(rsp);

	    var dataStr = 'm=dish_info_save&shopid=' + this.get('shopId');
	    dataStr += '&oper=' + type + '&json=' + rspStr;
	    var dataHandler = {
		method: 'POST',
		data: dataStr,
		headers: {'Content-Type': 'application/x-www-form-urlencoded; charset=utf-8'},
		on: {
		    success: function(id, rsp) {
			if (rsp.responseText == '-1000') {//登录超时
			    VA.Singleton.popup.timeout();
			    return;
			}
			var s = Y.JSON.parse(rsp.responseText);
			if (type == 'mutil') {
			    if (s.list[0].status <= 0) {
				var okHandler = function() {
				    //
				};
				VA.Singleton.popup.panel.set('headerContent', '提示信息');
				VA.Singleton.popup.panel.set('bodyContent', s.list[0].info);
				VA.Singleton.popup.panel.set('buttons', [VA.Singleton.popup.get('okButton')]);
				VA.Singleton.popup.set('ok', okHandler);
				VA.Singleton.popup.showPanel();
				self.bindUI();
				return;
			    }
			    ;
			    var continueEdit = true;
			    var len = Y.one('#uploadList').get('childNodes')._nodes.length;
			    if (len <= 1) {
				continueEdit = false;
			    }
			    ;

			    dishManageMutilMethod.successPicList++;

			    if (continueEdit) {
				Y.one('#mutilEdit .disPicture li.cur').remove(true);
				dishManageMutilMethod.uploadHandler();//第二次侦听裁图弹层
				dishManageMutilMethod.getPicName();
				self.renderUI();
				self.bindUI();
				self.syncUI();
			    } else {
				Y.one('#mutilEdit').setStyle('display', 'none');
				Y.one('.stepBar .success').addClass('cur').siblings().removeClass('cur');
				Y.one('#mutilSuccess').setStyle('display', 'block');
				Y.all('#mutilSuccess .btn').on('click', self.mutilSuccess, self);
				//提示成功处理图片数
				Y.one('#successPicList').set('text', dishManageMutilMethod.successPicList);
				dishManageMutilMethod.successPicList = 0;
			    }
			    ;

			} else {
			    var txtTips = Y.one('#save-txt-tips').setStyles({"display": "block", "left": 658, "opacity": 0});
			    var tips = popup.tips(txtTips);
			    if (s.list[0].status == 1) {
				if (arg == "isSaveBck") {
				    VA.renderPage.unplug(Y.Plugin.DisUploadItem);
				    // VA.renderPage.unplug();
				    self.prevPage();
				} else {
				    tips.stop().run();
				}
			    } else {
				var okHandler = function() {
				    //
				};
				VA.Singleton.popup.panel.set('headerContent', '提示信息');
				VA.Singleton.popup.panel.set('bodyContent', s.list[0].info);
				VA.Singleton.popup.panel.set('buttons', [VA.Singleton.popup.get('okButton')]);
				VA.Singleton.popup.set('ok', okHandler);
				VA.Singleton.popup.showPanel();
			    }
			    self.bindUI();
			    VA.Util.picStatus = '1';
			}
		    },
		    failure: function(id, rsp) {
			Y.log(rsp.status);
		    }
		}
	    };
	    Y.io('ajax/doSybWeb.ashx', dataHandler);
	},
	mutilSuccess: function(e) {
	    var t = e.currentTarget;
	    var className = t.getAttribute('class'),
		    isContinue = className.indexOf('continue') > -1,
		    isBack = className.indexOf('back') > -1;
	    if (isContinue) {
		this.prevUrl = 'dishManageMutil.aspx';
	    }
	    ;
	    if (isBack) {
		this.prevUrl = 'dishList.aspx';
	    }
	    ;
	    this.prevPage();
	},
	disSubmit: function(e) {
	    e.preventDefault();
	    e.stopPropagation();
	    var self = this;
	    var targetItem = e.currentTarget;
	    var eClass = targetItem.getAttribute('class'),
		    // isCancel = eClass.indexOf('cancel')>-1,
		    isSubmit = eClass.indexOf('submit') > -1,
		    isBack = eClass.indexOf('back') > -1,
		    isSaveBck = eClass.indexOf('save') > -1,
		    isMutilNext = eClass.indexOf('mutil-next') > -1;

	    Y.all('.required .tip').each(function(o) {
		o.setStyle('display', 'none');
	    });
	    if (isSubmit) {
		this.disSubmitListener.detach();
		this.dataSubmit("isSubmit");
		this.isNoSave = false;
	    } else if (isBack) {
		this.prevPage();
		return;
	    } else if (isSaveBck) {//菜品编辑
		this.disSubmitListener.detach();
		this.dataSubmit("isSaveBck");
	    } else if (isMutilNext) {//批量
		this.disSubmitListener.detach();
		var imgId = Y.one('#mutilEdit .disPicture li.cur img').get('id');
		this.dataSubmit(imgId);
	    } else {// 上一道、下一道
		var okHandler = function() {
		    // self.disSubmitListener.detach();
		    self.dataSubmit("isSubmit");
		    continueHandler();
		};
		function continueHandler() {
		    var hrefStr = targetItem.getAttribute('href');
		    var m = menu.getType(hrefStr),
			    aspx = m + ".aspx";
		    VA.argPage.qs = UIBase.getNyQueryStringArgs(hrefStr);
		    menu.nextPage(aspx, m);
		}
		;

		if (this.isNoSave) {
		    var saveBtn = function() {
			var that = this;
			var cancelCfg = {
			    value: '保存',
			    section: Y.WidgetStdMod.FOOTER,
			    action: function(ev) {
				ev.preventDefault();
				that.hidePanel();
				that.get('ok')();
			    }
			}
			return cancelCfg;
		    }
		    var noSaveBtn = function() {
			var that = this;
			var cancelCfg = {
			    value: '不保存',
			    section: Y.WidgetStdMod.FOOTER,
			    action: function(ev) {
				ev.preventDefault();
				that.hidePanel();
				that.get('cancel')();
			    }
			}
			return cancelCfg;
		    }

		    VA.Singleton.popup.panel.set('headerContent', '提示信息');
		    VA.Singleton.popup.panel.set('bodyContent', '当前菜品未保存！');
		    VA.Singleton.popup.panel.set('buttons', [saveBtn.apply(VA.Singleton.popup), noSaveBtn.apply(VA.Singleton.popup)]);
		    VA.Singleton.popup.set('ok', okHandler);
		    VA.Singleton.popup.set('cancel', continueHandler);
		    VA.Singleton.popup.showPanel();
		} else {
		    continueHandler();
		}
	    }
	    ;
	},
	dataSuccess: function(res) {
	    this.set('dataConfig', res);
	},
	updateConfigData: function() {
	    var self = this;
	    var arg = {};
	    arg.dataStr = 'm=dish_info_config&shopid=' + self.get('shopId');
	    this.ioRequest(arg, function(data) {
		self.set('dataConfig', data);
	    });
	},
	prevPage: function() {
	    var qs = VA.argPage.qs = this.get('queryString');
	    var type = this.get('queryString').type;
	    var url = 'dishList.aspx';
	    if (type == 'mutil') {
		url = this.prevUrl;
	    }
	    var m = menu.getType(url),
		    aspx = m + ".aspx";
	    menu.prevPage(aspx, m);
	},
	ioRequest: function(arg, callback) {
	    arg.ioURL = arg.ioURL || 'ajax/doSybWeb.ashx';
	    arg.sync = arg.sync || true;
	    var ioHandler = {
		method: 'POST',
		data: arg.dataStr,
		headers: {'Content-Type': 'application/x-www-form-urlencoded; charset=utf-8'},
		on: {
		    success: function(id, rsp) {
			if (rsp.responseText == '-1000') {
			    VA.Singleton.popup.timeout();
			    return;
			}
			var res = Y.JSON.parse(rsp.responseText);
			callback(res);
		    },
		    failure: function(id, rsp) {
			Y.log(rsp.status);
		    }
		},
		sync: arg.sync
	    };
	    Y.io(arg.ioURL, ioHandler);
	},
	renderUI: function() {
	    var type = this.get('queryString').type;
	    if (type == 'edit') {
		Y.one('.dishManage').addClass('edit');
		var arg = {};
		arg.dataStr = 'm=modify_dish_get_dishs&dishTypeId=' + menu.dishTypeId + '&searchKey=' + menu.inputTextStr + '&dishId=' + this.get('queryString').a;
		this.ioRequest(arg, function(data) {
		    if (data.list[0].status == 1) {
			var d = data.list[0].info.split(',');
			var prev = (d[0] == '0') ? '' : '	<a class="prev" href="dishManage.aspx?type=edit&a=' + d[0] + '">&lt; 上一道</a>',
				next = (d[2] == '0') ? '' : '	<a class="next" href="dishManage.aspx?type=edit&a=' + d[2] + '">下一道 &gt;</a>';
			var footerNavStr = '<p class="footer-nav">' + prev + next + '</p>';
			Y.one('#disSubmit').append(footerNavStr);
		    }
		});

		this.isNoSave = true;

	    } else if (type == 'mutil') {
		var mutilInfoHtml = dishManageMutilMethod.infoHandler();
		Y.one('#mutilInfo').setHTML(mutilInfoHtml);
	    } else if (type == 'add') {
		Y.one('#disSubmit .back').setStyle('display', 'none');
		Y.one('#disSubmit .submit').setStyle('display', 'none');
	    }
	    var cnInput = Y.one('#disNameTxt');
	    var cn = '';
	    cnInput.on('blur', function() {
		cn = cnInput.get('value');
		var dataHandler = {
		    method: 'GET',
		    headers: {'Content-Type': 'application/json; charset=utf-8'},
		    on: {
			success: function(id, rsp) {
			    var str = rsp.responseText;
			    if (str != '') {
				var arr = str.split(',');
				Y.one('#disNameJP').set('value', arr[0]);
				Y.one('#disNameQP').set('value', arr[1]);
			    }
			},
			failure: function(id, rsp) {
			    Y.log(rsp.status);
			}
		    }
		}
		Y.io('../Handlers/CN2PY.ashx?Hz=' + encodeURIComponent(cn) + '&typeID=', dataHandler);
	    }, this);
	    var disComment = Y.one('.disComment');
	    disComment.delegate('focus', function(e) {
		var t = e.currentTarget;
		if (t.get('text') == '还可以输入 153 个字') {
		    t.set('text', '');
		}
	    }, '.inputTextArea', this);
	},
	bindUI: function() {
	    var self = this;
	    this.disSubmitListener = Y.one('#disSubmit').delegate('click', this.disSubmit, 'a', this);
	    if (this.DisConfigEditListener) {
		this.DisConfigEditListener.detach();
	    }
	    this.DisConfigEditListener = Y.one('#disConfigContent').delegate('click', this.DisConfigItem.disConfigEdit, '.btn', this.DisConfigItem);

	    if (this.DisConfigListener) {
		this.DisConfigListener.detach();
	    }
	    this.DisConfigListener = Y.one('.configLayout').delegate('click', this.DisConfigItem.submitItem, '.btn', this.DisConfigItem);
	    if (this.DisConfigMoreListener) {
		this.DisConfigMoreListener.detach();
	    }
	    this.DisConfigMoreListener = Y.one('#disConfigPrice').delegate('click', this.DisConfigItem.attachData, '.more', this.DisConfigItem);
	    Y.one('#disConfigPrice').delegate('click', this.DisConfigItem.deleteItem, 'span.delete', this.DisConfigItem);
	    Y.one('#disConfigPrice').delegate('click', this.DisConfigItem.updateItem, 'span.update', this.DisConfigItem);

	    if (this.CatesItemEditDataListener) {
		this.CatesItemEditDataListener.detach();
	    }
	    this.CatesItemEditDataListener = Y.one('#catesBtn').delegate('click', this.CatesItemNS.editData, '.btn', this.CatesItemNS);

	    Y.one('#cates').delegate('click', this.CatesItemNS.deleteItem, '.delete', this.CatesItemNS);
	    Y.one('#cates').delegate('click', this.CatesItemNS.updateItem, '.update', this.CatesItemNS);

	    if (this.CatesItemListener) {
		this.CatesItemListener.detach();
	    }
	    this.CatesItemListener = Y.one('.catesLayout').delegate('click', this.CatesItemNS.submitItem, '.btn', this.CatesItemNS);

	    if (this.get('queryString').type == 'add') {
		this.after('shopIdChange', this.syncUI, this); //
	    } else {
		this.after('shopIdChange', this.prevPage, this);
	    }
	},
	syncUI: function() {
	    var self = this;
	    this.queryString = this.get('queryString');

	    if (this.queryString.type == 'edit') {
		var arg = {};
		arg.dataStr = 'm=dish_info_editinfo&dishid=' + self.queryString.a;
		self.ioRequest(arg, function(data) {
		    self.set('dataTemp', data);
		});
	    }
	    ;
	    var arg = {};
	    arg.dataStr = 'm=dish_info_config&shopid=' + self.get('shopId');
	    self.ioRequest(arg, function(data) {
		self.dataSuccess(data);
	    });
	}
    }, {//
	ATTRS: {
	    ioURL: {
		value: ''
	    },
	    dataTemp: {
		value: ''
	    },
	    initEditData: {
		value: ''
	    },
	    selectRect: {
		value: ''
	    },
	    contentBox: {
		value: ''
	    },
	    shopId: {
		value: ''
	    },
	    queryString: {
		value: ''
	    },
	    inputTextStr: {value: menu.inputTextStr},
	    dishTypeId: {value: menu.dishTypeId},
	    pageSize: {value: menu.pageSize},
	    pageIndex: {value: menu.pageIndex},
	    statusController: {value: menu.statusController}
	}
    });
}, '1.0', {requires: ['base-build', 'node-base', 'widget', 'io-base', 'json-parse', 'json-stringify']});
