/*//
 标题：商户宝 UIController
 来源：viewallow UI
 日期：2013/10/22
 //*/

	VA.initPage.dishManageMutil = function() {
	    YUI({
		groups: {
		    'jquery11': {
			base: 'scripts/jquery.upload/',
			async: false,
			modules: {
			    'DisManage': {
				fullpath: 'scripts/modules/UIDisManage.js',
				requires: ['base-build', 'node-base', 'widget', 'io-base', 'json-parse']
			    },
			    'jquery': {
				fullpath: 'scripts/public/jquery.min.js'
			    },
			    'jquery-imgareaselect-css': {
				path: 'css/imgareaselect-default.css',
				type: 'css'
			    },
			    'jquery-imgareaselect': {//jquery plugin
				path: 'js/jquery.imgareaselect.js',
				requires: ['jquery', 'jquery-imgareaselect-css']
			    },
			    'handlers': {
				path: 'js/handlers.js'
			    },
			    'swfupload': {
				path: 'js/swfupload.js',
				requires: ['handlers']
			    }
			}
		    }
		}
	    }).use('DisManage', 'catesItem-plugin', 'disInfoItem-plugin', 'disConfigItem-plugin', 'disUploadItem-plugin', 'jquery-imgareaselect', 'swfupload', 'plugin', 'overlay', 'popup-plugin', function(Y) {
		var sessionValue = VA.Util.session;//获取session 放页面
		var btnSwfUpload = new Object();
		btnSwfUpload.id = "mutilUpload";
		btnSwfUpload.w = 203;
		btnSwfUpload.h = 51;
		btnSwfUpload.imageUrl = "scripts/jquery.upload/images/btn_upload_mutil.png";
		btnSwfUpload.uploadUrl = "ajax/doSybWeb.ashx";
		btnSwfUpload.postParams = {"m": "dish_image_upload", "shopId": VA.argPage.loginId.get('id'), "ASPSESSID": sessionValue};

		VA.renderPage = new Y.DisManage({contentBox: '#addContainer', shopId: VA.argPage.loginId.get('id'), queryString: VA.argPage.qs, ioURL: 'ajax/doSybWeb.ashx'});

		var bd = document.getElementById('page'),
			layout = document.createElement('div');
		layout.className = 'uploadLayout';
		layout.innerHTML = '<ul class="imgs" id="uploadContent">'
			+ '	<li class="source" id="source"><img alt="菜品图片" title="菜品图片" src="images/nonImg.png" width="320" height="240" /></li>'
			+ '	<li class="big" id="preview"><img alt="菜品图片" src="images/nonImg.png" width="240" /></li>'
			// + '	<li class="small" id="previewSec"><img alt="菜品图片" src="images/nonImg.png" width="108" /></li>'
			+ '</ul>'
			+ '<div class="btnSprite">'
			+ '	<a href="javascript:;" class="btn">确定</a>'
			+ '	<a href="javascript:;" class="btn cancel">取消</a>'
			+ '</div>';
		bd.appendChild(layout);
		var overlay = new Y.Overlay({
		    srcNode: '.uploadLayout',
		    visible: false,
		    shim: false,
		    centered: true,
		    zIndex: 4,
		    plugins: [{fn: Y.AnimPlugin, cfg: {duration: 0.5}}]
		});
		overlay.render();


		//var overlay = dishManageMutilMethod.createLayout();
		VA.renderPage.overlay = overlay;

		VA.renderPage.plug([Y.Plugin.CatesItem, Y.Plugin.DisInfoItem, Y.Plugin.DisConfigItem]);
		VA.renderPage.plug(Y.Plugin.DisUploadItem, {btn: btnSwfUpload});
		VA.renderPage.render();


	    });

	};

var DishManageMutil = function() {
    this.successPicList = 0;
};
DishManageMutil.prototype.uploadHandler = function() {
    var imgReset = function(arg) {
	$(function() {
	    var hostObj = VA.renderPage;
	    arg.setOptions({hide: true, disable: true});
	    var selection = selectRect = {x: 0, y: 0, w: 320, h: 240};
	    var ratioW = 4, ratioH = 3;

	    if (hostObj.selectRectObj) {
		selection = hostObj.selectRectObj;
	    }
	    var scaleX = ratioW * 100 / selection.w,
		    scaleY = ratioH * 100 / selection.h;
	    $('#uploadList .cur img').css({
		width: Math.round(scaleX * 39 * ratioW),
		height: Math.round(scaleY * 39 * ratioH),
		marginLeft: -Math.round(scaleX * selection.x * 0.4),
		marginTop: -Math.round(scaleY * selection.y * 0.4)
	    });
	});
    };
    YUI().use('node-base', 'node-style', 'node-event-delegate', 'io-base', 'json-parse', function(Y) {
	/* 图片裁切区 */
	var uploadList = Y.one('#uploadList');
	var curContainer = uploadList.one('li');
	var hostObj = VA.renderPage;
	//设置首张菜品图当前状态框
	curContainer.addClass('cur');
	//更新步骤状态条
	Y.one('.stepBar .edit').addClass('cur').siblings().removeClass('cur');

	curContainer.on('click', function(e) {
	    var t = e.currentTarget;
	    e.preventDefault();
	    e.stopPropagation();

	    var urlImg = t.one('img').getAttribute('src');
	    Y.all('#uploadContent img').each(function(o) {
		o.set('src', urlImg);
	    });
	    var ratioW = 4,
		    ratioH = 3,
		    selectRect = {x: 0, y: 0, w: 320, h: 240};
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
		$('#uploadList .cur img').css({
		    width: Math.round(scaleX * 39 * ratioW),
		    height: Math.round(scaleY * 39 * ratioH),
		    marginLeft: -Math.round(scaleX * selection.x1 * 0.4),
		    marginTop: -Math.round(scaleY * selection.y1 * 0.4)
		});
		/*
		$('#previewSec img').css({
		    width: Math.round(scaleX * 22 * ratioW),
		    height: Math.round(scaleY * 22 * ratioH),
		    marginLeft: -Math.round(scaleX * selection.x1 * 0.28),
		    marginTop: -Math.round(scaleY * selection.y1 * 0.28)
		});
		*/
	    }
	    ;
	    var getRect = function(img, selection) {
		if (!!selection.width || !!selection.height) {
		    selectRect.x = selection.x1;
		    selectRect.y = selection.y1;
		    selectRect.w = selection.width;
		    selectRect.h = selection.height;
		    //VA.Util.picStatus = '2';
		}
		hostObj.set('selectRect', selectRect);
	    };
	    var imgSize = hostObj.get('dataConfig').dishImage.size.split(",");
	    
	    var tempImg = new Image();
            tempImg.onload = function () {
		var minWidth = Math.floor(imgSize[0]*320/tempImg.width);
		minWidth = minWidth>320? 320 :minWidth;
		var minHeight = Math.floor(minWidth*3/4);
		$(function() {
		    var ratioStr = ratioW + ':' + ratioH;
		    hostObj.imgAreaObj = $('#source img').imgAreaSelect({aspectRatio: ratioStr, handles: true, fadeSpeed: 400, disable: false, minWidth: minWidth, minHeight: minHeight, onSelectChange: preview, onSelectEnd: getRect, instance: true});
		});
		// hostObj.set('selectRect', selectRect);
	    };
	    tempImg.src= urlImg;
	    
	    VA.renderPage.overlay.show();
	}, this);

	Y.one('.uploadLayout').delegate('click', function(e) {
	    var t = e.currentTarget,
		    className = t.getAttribute('class'),
		    isCancel = className.indexOf('cancel') > -1;
	    if (isCancel) {
		imgReset(hostObj.imgAreaObj);
	    } else {
		var iconError = Y.one('#uploadList .icon');//去掉error标识
		if (iconError) {
		    iconError.remove(true);
		}
		hostObj.selectRectObj = hostObj.get('selectRect');
		hostObj.imgAreaObj.setOptions({hide: true, disable: true});
	    }
	    VA.renderPage.overlay.hide();
	}, '.btn', this);
    });
};

DishManageMutil.prototype.deletePic = function() {
    var that = this;
    that.uploadHandler();
    YUI().use('node-base', 'io-base', 'node-style', function(Y) {
	Y.one('#mutilEdit .delete').on('click', function(e) {

	    var container = Y.all('#uploadList li'),
		    len = container._nodes.length;

	    var cur = Y.one('#uploadList li');
	    var imgId = cur.one('img').getAttribute('id');
	    var ioHandler = {
		method: 'POST',
		data: 'm=imagetask_delete&id=' + imgId,
		headers: {'Content-Type': 'application/x-www-form-urlencoded; charset=utf-8'},
		on: {
		    success: function(id, rsp) {
			if (len <= 1) {
			    Y.one('#mutilEdit').setStyle('display', 'none');
			    Y.one('.stepBar .success').addClass('cur').siblings().removeClass('cur');
			    Y.one('#mutilSuccess').setStyle('display', 'block');
			    Y.all('#mutilSuccess .btn').on('click', VA.renderPage.mutilSuccess, VA.renderPage);
			    //提示成功处理图片数
			    Y.one('#successPicList').set('text', dishManageMutilMethod.successPicList);
			    dishManageMutilMethod.successPicList = 0;
			    return;
			}
			var deleteElem = Y.one('#uploadList li');
			deleteElem.remove(true);
			var container = Y.all('#uploadList li'),
				lenNew = container._nodes.length;
			if (lenNew > 0) {
			    var curContainer = Y.one('#uploadList li').addClass('cur');
			    that.getPicName();
			}
			that.uploadHandler();

			//删除重置信息表单
			VA.renderPage.renderUI();
			VA.renderPage.bindUI();
			VA.renderPage.syncUI();
		    },
		    failure: function(id, rsp) {
			Y.log(rsp.status);
		    }
		}
	    };
	    Y.io('ajax/doSybWeb.ashx', ioHandler);

	}, this);

    });
};

//获取菜品名称：图片载入、li结构生成
DishManageMutil.prototype.getPicName = function() {
    YUI().use('node-base', 'node-style', 'io-base', function(Y) {
	var curContainer = Y.one('#uploadList .cur');
	var imgName = curContainer.one('img').getAttribute('rel');
	Y.on('available', function() {
	    var disNameTxt = Y.one('#disNameTxt')._node;
	    disNameTxt.value = imgName;
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
	    };
	    Y.io('../Handlers/CN2PY.ashx?Hz=' + encodeURIComponent(imgName) + '&typeID=', dataHandler);

	}, '#disNameTxt');
    });
};

DishManageMutil.prototype.infoHandler = function() {
    var mutilInfoHtml = '<div class="disCates gray">'
	    + '	 <table cellpadding="0" cellspacing="0" border="0">'
	    + '		<tbody>'
	    + '			<tr>'
	    + '				<td width="112"><span class="title required"><span class="tip">需要选择<i class="txt">[分类]</i>!<i class="close">x</i></span>分　　类<i class="icon">*</i></span></td>'
	    + '				<td width="600">'
	    + '					<ul class="cates" id="cates">'
	    + '					</ul>'
	    + '				</td>'
	    + '				<td width="218">'
	    + '					<div class="btnSprite" id="catesBtn">'
	    + '						<a class="btn add" href="javascript:;">添加分类</a>'
	    + '						<a class="btn update" href="javascript:;">修改分类</a>'
	    + '						<a class="btn edit" href="javascript:;">删除分类</a>'
	    + '					</div>'
	    + '				</td>'
	    + '			</tr>'
	    + '		</tbody>'
	    + '	 </table>'
	    + '</div>'
	    + '<div class="disName">'
	    + '	<p class="text" id="disNameSprite"><span class="title required"><label for="disNameTxt" class="tip">需要输入<i class="txt">[菜名]</i>!<i class="close">x</i></label>菜　　名：<i class="icon">*</i></span><input type="text" class="inputText" id="disNameTxt" name="disNameTxt" value="" /></p>'
	    + '	<p class="text"><span class="title">菜名全拼：</span><input type="text" class="inputText" id="disNameQP" maxlength="50" value="" /></p>'
	    + '	<p class="text"><span class="title">菜名简拼：</span><input type="text" class="inputText" id="disNameJP" maxlength="50" value="" /></p>'
	    + '	<p class="last text" id="disNameIndexSprite" style="display:none;"><span class="title">排　　序：</span><input type="text" class="inputText" id="disNameIndex" value="1" /></p>'
	    + '</div>'
	    + '<div class="disConfig">'
	    + '	<table class="dataContent" id="disConfigContent" cellpadding="0" cellspacing="0" border="0">'
	    + '		<thead>'
	    + '			<tr class="configHeader">'
	    + '				<td width="828px"><span class="configTitle">规格</span></td>'
	    + '				<td width="83px"><div class="btnSprite"><a class="btn addPrice" href="javascript:;">继续添加</a></div></td>'
	    + '			</tr>'
	    + '		</thead>'
	    + '		<tbody id="disConfigPrice">'
	    + '		</tbody>'
	    + '	 </table>'
	    + '</div>'
	    + '<div class="disComment gray">'
	    + '	<p class="text"><span class="title">菜品简介：</span><textarea rows="20" cols="20" class="inputTextArea" id="disCommentProfile">还可以输入 153 个字</textarea></p>'
	    + '	<p class="text last"><span class="title">菜品详情：</span><textarea rows="20" cols="20" class="inputTextArea" id="disCommentDetail">还可以输入 153 个字</textarea></p>'
	    + '</div>';
    return mutilInfoHtml;
};

var dishManageMutilMethod = new DishManageMutil();






	