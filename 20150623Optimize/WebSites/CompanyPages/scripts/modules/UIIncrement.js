YUI.add('incrementManage-plugin', function (Y) {
    Y.Plugin.IncrementManagePlugin = Y.Base.create('IncrementManagePlugin', Y.Plugin.Base, [], {
        initializer: function () {
            if (this.get('rendered')) {
                this.addData();
            } else {
                this.afterHostMethod('dataSuccess', this.addData);
            }
        },
        initBind: function () {
            var that = this, type = that.queryString.type, m = that.queryString.m;
            switch (type) {
				case "incrementEdit":
                    that.incrementEditHandler(m);
                    break;
                default:
                    break;
            }
        },
		incrementEditHandler: function (m) {
            var that = this, hostObj = this.get('host');
            function uploadshowHandler(data, autoAdd) {
				var picURL = data.imgUrl;
				var pic = picURL.split('-ViewAlloc-');
				that.picSumbitURL = pic[1];
                Y.one("#picLogo").set("src", pic[0]+pic[1]);
            }
            var uploaderObj = that.uploadHandler("#inputFileSprite", "#uploadBoundingBoxLogo", "#conID", uploadshowHandler);
            var submitHandler = function () {
                var fileList = uploaderObj.get("fileList"), fileListLen = fileList.length;
                if (fileListLen > 0) {
                    var lastFile = fileList[fileListLen - 1];
                    uploaderObj.upload(lastFile, "ajax/doSybChannel.ashx", { "m": "uploadDishImage", "shopID": that.queryString.sid });
                }
            };
            Y.one("#submitUploaderBtn").on("click", submitHandler);
			if (m === "add") {
				var acNode = Y.one('#dishNameInput');
				acNode.plug(Y.Plugin.AutoComplete, {
					activateFirstItem: true,
					resultHighlighter: 'phraseMatch',
					resultTextLocator: 'text',
					resultFilters: 'phraseMatch'
				});
				acNode.on("keyup", function (evt) {
					var arg = {}, k = "";
					k = evt.currentTarget.get("value");
					arg.ioURL = 'ajax/doSybChannel.ashx';
					arg.dataStr = 'm=searchShopDish&shopID=' + that.queryString.sid + '&key=' + k + '&pageIndex=1&pageSize=10';
					hostObj.ioRequest(arg, function (data) {
						if (data.list[0].status == 1) {
							var statesData = [];
							var d = Y.JSON.parse(data.list[0].info);
							data = d.dishInfoDetailList;
							for (var i = 0, len = data.length; i < len; i++) {
								statesData.push({ "dishID": data[i].dishID, "dishPriceID": data[i].dishPriceID,"text": data[i].dishName });
							}
							acNode.ac.set('source', statesData);
						}
					});
				});
				acNode.ac.after('select', function (e) {
					that.dishID = e.result.raw.dishID;
					that.dishPriceID = e.result.raw.dishPriceID;
					var arg = {};
					arg.ioURL = 'ajax/doSybChannel.ashx';
					arg.dataStr = 'm=SearchDishPriceAndDiscount&shopID=' + that.queryString.sid + '&dishPriceID=' + that.dishPriceID;
					hostObj.ioRequest(arg, function (data) {
						if (data.list[0].status == 1) {
							var d = Y.JSON.parse(data.list[0].info);
							var discountStr = '';
							if(!d.discount){
								discountStr = '无';
							}else{
								discountStr = d.discount+'折';
							}
							Y.one("#dishDiscountInput").setHTML(discountStr);
							Y.one("#dishPriceInput").setHTML('￥'+d.price);
						}
					});
				});
			}else if(m === 'edit'){
				var arg = {};
				arg.ioURL = 'ajax/doSybChannel.ashx';
				arg.dataStr = 'm=searchShopChannelDish&shopChannelDishID=' + that.queryString.vid;
				hostObj.ioRequest(arg, function (data) {
					if (data.list[0].status == 1) {
						var tempData = Y.JSON.parse(data.list[0].info);
						var d = tempData.TableJson[0];
						var pic = (d.dishImageUrl).split('-ViewAlloc-');
						that.picSumbitURL  = pic[1];
						that.dishID = d.dishID;
						that.dishPriceID = d.dishPriceID;
						Y.one("#dishContentText").set('value', d.dishContent);
						Y.one("#picLogo").set("src", pic[0]+pic[1]);
						
						setTimeout(function(){
							var arg = {};
							arg.ioURL = 'ajax/doSybChannel.ashx';
							arg.dataStr = 'm=SearchDishPriceAndDiscount&shopID=' + that.queryString.sid + '&dishPriceID=' + d.dishPriceID;
							hostObj.ioRequest(arg, function (data) {
								if (data.list[0].status == 1) {
									var d = Y.JSON.parse(data.list[0].info);
									var discountStr = '';
									if(!d.discount){
										discountStr = '无';
									}else{
										discountStr = d.discount+'折';
									}
									Y.one("#dishDiscountInput").setHTML(discountStr);
									Y.one("#dishPriceInput").setHTML('￥'+d.price);
								}
							});
							
						},10);
					}
				});
				
			}
        },
		incrementEditedit: function () {
            var that = this;
            var obj = {}, hostObj = this.get('host'), arg = {};
			Y.one('.incrementManage').addClass('hd-dish-edit');
			
            // arg.dataStr = ' + hostObj.queryString.sid; //获取所编辑菜品的其他相关、必要信息:图片、描述、折扣
            // hostObj.ioRequest(arg, function (data) {
            //    if (data.list[0].status == 1) {
            //        var tempData = Y.JSON.parse(data.list[0].info);
                    
					var tempData = {};
					tempData.dishName = hostObj.queryString.name;
					tempData.price = hostObj.queryString.price;
                    obj.d = tempData;
					
                    obj.template = '<ul>'
						+ '	<li class="gray "><label for="">菜品名称：</label><span class="txt auto-complete-sprite">'+tempData.dishName+'<em class="txtColor">*</em></span></li>'
						+ '	<li><label for="">价　　格：</label><span class="txt" id="dishPriceInput">'+tempData.price+'/￥</span></li>'
						+ '	<li class="gray"><label for="">折　　扣：</label><span class="txt" id="dishDiscountInput">0折 </span></li>'
						
						+ '	<li class="uploadSprite" id="conID"><label for="">招贴海报：</label>'
						+ '    <span class="txt"><input type="text" id="picUrl" class="inputText" value="http://" />'
						+ '        <div class="uploadBoundingBox" id="uploadBoundingBoxLogo">'
						+ '             <input type="button" class="inputBtn" id="inputFileSprite" value="浏览..." /> '
						+ '        </div>'
						+ '        <input type="submit" value="上传" class="inputBtn submitUploaderBtn" id="submitUploaderBtn" name="submit" />( 建议尺寸640*320,大小不超过1M，格式 png )<em class="txtColor">*</em>'
						+ '    </span>'
						+ '  </li>'
						+ '	<li class="upload-thumbnail-sprite"><label for=""> </label><span class="txt"><img id="picLogo" class="logo" width="200" height="150" src="images/upload_bg_default.png" /></span></li>'
						
						+ '	<li class="gray comment-sprite"><label for="">菜品描述：</label><span class="txt"><textarea class="area verify inputRequired" maxLength="26" id="dishContentText"></textarea> </span>(最多可以输入26个字符)<em class="txtColor">*</em></li>'
						+ '</ul>';
            //    }
            //});
            return obj;
        },
        incrementEditadd: function () {
            var that = this;
            var obj = {};
            Y.one('.incrementManage').addClass('hd-dish-add');
			obj.template = '<ul>'
                + '	<li class="gray "><label for="">菜品名称：</label><span class="txt auto-complete-sprite"><input id="dishNameInput" type="text" class="inputText verify inputRequired" value="" maxLength="10" style="width:450px;" /> <em class="txtColor">*</em></span></li>'
                + '	<li><label for="">价　　格：</label><span class="txt" id="dishPriceInput">0.00 /￥</span></li>'
                + '	<li class="gray"><label for="">折　　扣：</label><span class="txt" id="dishDiscountInput">0折 </span></li>'
                
				+ '	<li class="uploadSprite" id="conID"><label for="">招贴海报：</label>'
				+ '    <span class="txt"><input type="text" id="picUrl" class="inputText" value="http://" />'
				+ '        <div class="uploadBoundingBox" id="uploadBoundingBoxLogo">'
				+ '             <input type="button" class="inputBtn" id="inputFileSprite" value="浏览..." /> '
				+ '        </div>'
				+ '        <input type="submit" value="上传" class="inputBtn submitUploaderBtn" id="submitUploaderBtn" name="submit" />( 建议尺寸640*320,大小不超过1M，格式 png )<em class="txtColor">*</em>'
				+ '    </span>'
				+ '  </li>'
				+ '	<li class="upload-thumbnail-sprite"><label for=""> </label><span class="txt"><img id="picLogo" class="logo" width="200" height="150" src="images/upload_bg_default.png" /></span></li>'
				
				+ '	<li class="gray comment-sprite"><label for="">菜品描述：</label><span class="txt"><textarea class="area verify inputRequired" maxLength="26" id="dishContentText"></textarea> </span>(最多可以输入26个字符)<em class="txtColor">*</em></li>'
                + '</ul>';
            return obj;
        },
        uploadHandler: function (fileBtn, boundingBox, srcNode, callback) {
            var inputFileSprite = Y.one(fileBtn), uploadBoundingBox = Y.one(boundingBox);
            if (Y.Uploader.TYPE != "none" && !Y.UA.ios) {
                var uploader = new Y.Uploader({
                    width: "70px",
                    height: "24px",
                    selectFilesButton: inputFileSprite,
                    boundingBox: uploadBoundingBox,
                    multipleFiles: false,
                    swfURL: "scripts/yui3swfs/flashUploader.swf?t=" + Math.random(),
                    withCredentials: false
                });
                uploader.render(srcNode);
                var srcNodeElem = Y.one(srcNode);
                uploader.after("fileselect", function (event) {
                    var perFileVars = {}, picUrl = srcNodeElem.one(".inputText");
                    Y.each(event.fileList, function (fileInstance) {
                        perFileVars[fileInstance.get("id")] = { filename: fileInstance.get("name") };
                        picUrl.set('value', fileInstance.get("name"));
                    });
                    uploader.set("postVarsPerFile", Y.merge(uploader.get("postVarsFile"), perFileVars));
                });
                uploader.on("uploadcomplete", function (event) {
                    var d = Y.JSON.parse(event.data);
                    if (d.list[0].status == 1) {
                        var infoStr = d.list[0].info, infoArr = infoStr.split("|"), data = {};
                        for (var i = 0, len = infoArr.length; i < len; i++) {
                            var itemArr = infoArr[i].split(",");
                            data[itemArr[0]] = itemArr[1];
                        }
                        callback(data, true);
                    } else {
                        var okHandler = function () {
                            // 
                        };
                        var cancelHandler = function () {
                            //
                        };
                        VA.Singleton.popup.panel.set('headerContent', '提示信息');
                        VA.Singleton.popup.panel.set('bodyContent', d.list[0].info);
                        VA.Singleton.popup.panel.set('buttons', [VA.Singleton.popup.get('okButton'), VA.Singleton.popup.get('cancelButton')]);
                        VA.Singleton.popup.set('ok', okHandler);
                        VA.Singleton.popup.set('cancel', cancelHandler);
                        VA.Singleton.popup.showPanel();
                    }
                    uploader.set("enabled", true);
                    uploader.set("fileList", []);
                });

                return uploader;
            } else {
                uploadBoundingBox.set("text", "对不起您的浏览不支持 html5 上传特性且没有安装好 flashplayer 播放器,以至于无法进行上传操作");
            }
        },
        addData: function () {
            var self = this, hostObj = self.get('host');
            this.queryString = hostObj.get('queryString');
            var initMethod = this.queryString.type + this.queryString.m;

            var data = self[initMethod]();
            hostObj.get('contentBox').set('innerHTML', Y.Lang.sub(data.template, data.d));
            this.initBind();
        }
    },
    {
        NS: 'IncrementManagePlugin',
        ATTRS: {
            btn: {
                value: ''
            },
            dishNameList: {
                value: []
            }
        }
    });
}, '1.0', { requires: ['base-build', 'plugin', 'uploader', 'autocomplete', 'autocomplete-filters', 'autocomplete-highlighters', 'jsonp', 'jsonp-url'] });

YUI.add('UIIncrement', function (Y) {
    Y.IncrementManage = Y.Base.create('IncrementManage', Y.Widget, [], {
        destructor: function () {
            this.get('containerBox').remove(true);
        },
        dataSuccess: function () { },
        prevPage: function (pType) {
            menu.prevPage(pType + ".aspx", pType);
        },
        incrementEditedit: function (dataArray) {
            dataArray.push(this.IncrementManagePlugin.dishID);
			dataArray.push(this.IncrementManagePlugin.dishPriceID);
			dataArray.push(this.IncrementManagePlugin.picSumbitURL);
			dataArray.push(this.queryString.ind);
			dataArray.push(this.queryString.vid);
            var arg = {};
			
			arg.ioURL = 'ajax/doSybChannel.ashx';
            arg.dataStr = 'm=channelDishSave&operate=update&id='+dataArray[11]+'&shopChannelID='+dataArray[0]+'&dishID='+dataArray[7]+'&dishPriceID='+dataArray[8]+'&dishIndex='+dataArray[10]+'&dishName='+dataArray[1].split("-")[0]+'&dishImageUrl='+dataArray[9]+'&dishContent='+dataArray[6];
            arg.dataStr = dataEncode(arg.dataStr);
            return arg;
        },
        incrementEditadd: function (dataArray) {
			dataArray.push(this.IncrementManagePlugin.dishID);
			dataArray.push(this.IncrementManagePlugin.dishPriceID);
			dataArray.push(this.IncrementManagePlugin.picSumbitURL);
			dataArray.push(this.queryString.ind);
			
            var arg = {};
			
			arg.ioURL = 'ajax/doSybChannel.ashx';
            arg.dataStr = 'm=channelDishSave&operate=add&shopChannelID='+dataArray[0]+'&dishID='+dataArray[7]+'&dishPriceID='+dataArray[8]+'&dishIndex='+dataArray[10]+'&dishName='+dataArray[1].split("-")[0]+'&dishImageUrl='+dataArray[9]+'&dishContent='+dataArray[6];
            arg.dataStr = dataEncode(arg.dataStr);
            return arg;
        },
        verifedHandler: function () {
            var msgStr = "", classNameStr = "", that = this;
            var okHandler = function () { };
            that.get('contentBox').all('.txt').each(function (node, index, instance) {
                var childNode = node._node.childNodes[0];
                var classNameNode = childNode.className;
                if (classNameNode && classNameNode.indexOf("verify") > -1) {
                    classNameStr += classNameNode + " ";
                };
            });
            var val = "", labelStr = "";
            if (classNameStr.indexOf("numberRequired") > -1) {
                var node = Y.one(".numberRequired"),
                    label = node.ancestor(".txt").siblings("label");
                var reg = /^[0-9]+\.{0,1}[0-9]{0,2}$/gi;
                val = node.get("value").replace(/^(\s*)|(\s*)$/g, "");
                labelStr = label.get("text") + "";
                if (!reg.test(val)) {
                    msgStr = labelStr.slice(0, -1) + "，请输入数值（小数仅支持两位数）";
                };
            }
            if (classNameStr.indexOf("numberOneLess") > -1) {
                var node = Y.one(".numberOneLess"),
                    label = node.ancestor(".txt").siblings("label");
                var reg = /^(0\.{0,1}[0-9]{0,2})$/gi;// 0.21
                val = node.get("value").replace(/^(\s*)|(\s*)$/g, "");
                labelStr = label.get("text") + "";
                if (!reg.test(val)) {
                    msgStr = labelStr.slice(0, -1) + "，请输入0-1(不包含1)间的小数值(仅支持两位)";
                };
            }
            if (classNameStr.indexOf("intNumberRequired") > -1) {
                var node = Y.one(".intNumberRequired"),
                    label = node.ancestor(".txt").siblings("label");
                var reg = /^\d+$/;
                val = node.get("value").replace(/^(\s*)|(\s*)$/g, "");
                labelStr = label.get("text") + "";
                if (!reg.test(val) || !( parseFloat(val) >= 0 && parseFloat(val) <=100 ) ) {
                  msgStr = labelStr.slice(0, -1) + "，请输入0-100的整数";
                };
            }
            if (classNameStr.indexOf("mustBeNumber") > -1) {
                var node = Y.one(".mustBeNumber"),
                    label = node.ancestor(".txt").siblings("label");
                var reg = /^[0-9]\d*$/;
                val = node.get("value").replace(/^(\s*)|(\s*)$/g, "");
                labelStr = label.get("text") + "";
                if (!reg.test(val)) {
                  msgStr = labelStr.slice(0, -1) + "，只能输入数字";
                };
            }
            if (classNameStr.indexOf("numberOneToHundred") > -1) {
                var node = Y.one(".numberOneToHundred"),
                    label = node.ancestor(".txt").siblings("label");
                var reg = /^-?\d+\.?\d{0,2}$/;
                val = node.get("value").replace(/^(\s*)|(\s*)$/g, "");
                labelStr = label.get("text") + "";
                if (!reg.test(val) || !( parseFloat(val) >= 0 && parseFloat(val) <=100 ) ) {
                    msgStr = labelStr.slice(0, -1) + "，请输入0-100之间的整数或小数值(小数最多仅支持两位)";
                };
            }
            if (classNameStr.indexOf("wordChk") > -1) {
                var nodeList = Y.all(".wordChk");
                nodeList.each(function (element) {
                  label = element.ancestor(".txt").siblings("label");
                  var reg = /^[a-zA-Z0-9\u4e00-\u9fa5\()\_]+$/;
                  val = element.get("value").replace(/^(\s*)|(\s*)$/g, "");
                  labelStr = label.get("text") + "";
                  if (!reg.test(val)) {
                      msgStr = labelStr.slice(0, -1) + "，不能输入特殊符号。如：<>，中文括号（），空格";
                  };
                });
            }
            if (classNameStr.indexOf("inputRequired") > -1) {
                var nodeList = Y.all(".inputRequired");
                nodeList.each(function (element) {
                    val = element.get("value").replace(/^(\s*)|(\s*)$/g, "");
                    label = element.ancestor(".txt").siblings("label");
                    labelStr = label.get("text") + "";
                    if (!val) {
                        msgStr = labelStr.slice(0, -1) + "，不能为空";
                    };
                });
            }
			if(!this.IncrementManagePlugin.picSumbitURL){
				msgStr = "图片上传，不能为空";
			}
			
            if (msgStr) {
                VA.Singleton.popup.panel.set('headerContent', '提示信息');
                VA.Singleton.popup.panel.set('bodyContent', msgStr);
                VA.Singleton.popup.panel.set('buttons', [VA.Singleton.popup.get('okButton')]);
                VA.Singleton.popup.set('ok', okHandler);
                VA.Singleton.popup.showPanel();
                return false;
            } else {
                return true;
            }
        },
		nySubmit: function (e) {
            e.preventDefault();
            e.stopPropagation();
            var that = this,
                t = e.currentTarget,
                isCancel = t.getAttribute('class').indexOf('cancel') > -1;
            if (isCancel) {
                var okHandler = function () {
                  if( that.queryString.type === "withdrawtype" || that.queryString.type === "commision" || that.queryString.type === "shopvipdiscount" || that.queryString.type === "companyaccount"   ){
                    that.prevPage('shoplist');
                  }else{
                    that.prevPage(that.queryString.type);
                  }
                };
                var cancelHandler = function () {
                    //
                };
				var m = that.queryString.m, mTips = '';
				if(m === 'add'){
					mTips = '添加';
				}else if(m === 'edit'){
					mTips = '修改';
				}
                VA.Singleton.popup.panel.set('headerContent', '提示信息');
                VA.Singleton.popup.panel.set('bodyContent', '您确定要放弃'+mTips+'吗？');
                VA.Singleton.popup.panel.set('buttons', [VA.Singleton.popup.get('okButton'), VA.Singleton.popup.get('cancelButton')]);
                VA.Singleton.popup.set('ok', okHandler);
                VA.Singleton.popup.set('cancel', cancelHandler);
                VA.Singleton.popup.showPanel();
                return;
            } else {
                if (that.verifedHandler()) {
                    // 提交数据
                    var addDataArray = [that.queryString.a];
                    that.get('contentBox').all('.txt').each(function (node, index, instance) {
                        var childNode = node._node.childNodes[0];
                        var txt = childNode.nodeName.toLowerCase();
                        if (txt === 'input') {
                            var elmType = childNode.getAttribute('type').toLowerCase();
                            if (elmType === 'radio') {
                                var chdNodes = node._node.childNodes;
                                for (var j = 0, lenRadio = chdNodes.length; j < lenRadio; j++) {
                                    if (chdNodes[j].checked) {
                                        addDataArray.push(chdNodes[j].value);
                                        break;
                                    }
                                }
                            }else if (elmType === 'checkbox') {
                                var chdNodes = node._node.childNodes;
                                for (var j = 0, lenCheckbox = chdNodes.length; j < lenCheckbox; j++) {
                                    if (chdNodes[j].checked) {
                                        addDataArray.push(chdNodes[j].value);
                                        break;
                                    }
                                }
                            } else {
                                addDataArray.push(childNode.value);
                            }
                        } else if (txt === 'textarea') {
                            addDataArray.push(childNode.value);
                        } else if (txt === 'img') {
                            addDataArray.push(childNode.src);
                        } else if (txt === 'select') {
                            var chdNodes = node._node.childNodes;
                            for (var j = 0, len = chdNodes.length; j < len; j++) {
                                var selectNode = chdNodes[j].nodeName.toLowerCase();
                                if (selectNode == 'select') {
                                    addDataArray.push(chdNodes[j][chdNodes[j].selectedIndex].value);
                                }
                            }
                        } else {// #text
                            addDataArray.push(childNode.nodeValue);
                        };
                    });
                    
                    var initMethod = that.queryString.type + that.queryString.m;
                    var arg = this[initMethod](addDataArray);
					
                    that.ioRequest(arg, function (data) {
                        var status = data.list[0].status;
                        var confirmHandler = function () {
                        };
                        if (status == 1) {
                          if( that.queryString.type === "withdrawtype" || that.queryString.type === "commision" || that.queryString.type === "shopvipdiscount" || that.queryString.type === "companyaccount"   ){
                            that.prevPage('shoplist');
                          }else{
                            that.prevPage(that.queryString.type);
                          }
                        } else {
                            VA.Singleton.popup.panel.set('headerContent', '提示信息');
                            VA.Singleton.popup.panel.set('bodyContent', data.list[0].info);
                            VA.Singleton.popup.panel.set('buttons', [VA.Singleton.popup.get('okButton')]);
                            VA.Singleton.popup.set('ok', confirmHandler);
                            VA.Singleton.popup.showPanel();
                            // }
                        }
                    });
                };
            }
        },
        ioRequest: function (arg, callback) {
            arg.ioURL = arg.ioURL || 'ajax/doSybSystem.ashx';
            arg.sync = arg.sync || true;
            var ioHandler = {
                method: 'POST',
                data: arg.dataStr,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=utf-8' },
                on: {
                    success: function (id, rsp) {
                        if (rsp.responseText == '-1000') {//登录超时
                            VA.Singleton.popup.timeout();
                            return;
                        }
                        var res = Y.JSON.parse(rsp.responseText);
                        callback(res);
                    },
                    failure: function (id, rsp) {
                        Y.log(rsp.status);
                    }
                },
                sync: arg.sync
            };
            Y.io(arg.ioURL, ioHandler);
        },
        renderUI: function () {
            this.queryString = this.get('queryString');
        },
        bindUI: function () {
            Y.all('.btn-ny-submit  .btn').on('click', this.nySubmit, this);
        },
        syncUI: function () {
            this.dataSuccess();
        }
    }, {//
        ATTRS: {
            ioURL: {
                value: ''
            },
            dataTemp: {
                value: ''
            },
            contentBox: {
                value: ''//{}
            },
            queryString: {
                value: ''
            },
            checkOutNodeList: {
                value: ''
            }
        }
    });
}, '1.0', { requires: ['base-build', 'node-base', 'widget', 'io-base', 'json-parse', 'json-stringify'] });

//转义+ - .
function dataEncode(parameters) {
    return parameters.replace(/\+/g, "%2B").replace(/\-/g, "%2D").replace(/\./g, "%2E");
}
