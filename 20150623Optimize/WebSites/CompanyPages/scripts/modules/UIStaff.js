YUI.add('companyManage-plugin', function (Y) {
    Y.Plugin.CompanyManagePlugin = Y.Base.create('CompanyManagePlugin', Y.Plugin.Base, [], {
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
                case 'companylist':
                    Y.all('#backMode .inputRadio').on('click', function (e) {
                        var val = e.currentTarget.getAttribute('value');
                        if (val == 2) {
                            Y.one("#backPriceNumber").addClass("numberOneLess");
                            Y.one('#backPriceUint').set('text', ' 请输入0-1之间的小数值');
                        } else {
                            Y.one("#backPriceNumber").removeClass("numberOneLess");
                            Y.one('#backPriceUint').set('text', ' 元');
                        }
                    }, this);
                    break;
                case 'shopimagerevelation':
                    Y.one('.companySubmit').addClass('hiddenClass');
                    that.shopimagerevelationHandler();
                    break;
                case "shoplist":
                    that.shoplistHandler(m);
                    break;
                case "shophandle":
                    var map = new BMap.Map("shopMap");
                    var point = new BMap.Point(that.shopLongitude, that.shopLatitude);
                    map.centerAndZoom(point, 15);
                    map.addControl(new BMap.NavigationControl());
                    map.addControl(new BMap.ScaleControl());
                    map.addControl(new BMap.OverviewMapControl());
                    map.addOverlay(new BMap.Marker(point));
                    break;
                case "financehandle":
                    break;
                default:
                    break;
            }
        },
        areaSelectHandle: function (evt) {
            var optionSelectIndex = evt.currentTarget.get('selectedIndex');
            var t = Y.one(evt.currentTarget._node[optionSelectIndex]), optionId = t.get("value");
            var className = t.ancestor('select').getAttribute('class');
            this.set(className + 'ID', optionId);
            if (className === 'province') {
                var cityId = Y.one("select.city option").get('value');
                this.set('cityID', cityId);
            };
        },
        businessSelectHandle: function (evt) {
            var optionSelectIndex = evt.currentTarget.get('selectedIndex');
            var t = Y.one(evt.currentTarget._node[optionSelectIndex]), optionId = t.get("value");
            var className = t.ancestor('select').getAttribute('class');
            this.set(className + 'ID', optionId);
            if (className === 'firstBusinessDistrict') {
                var that = this;
                var hostObj = this.get('host');
                var argSecondBusinessDistrict = {};
                var obj = document.getElementById("firstBusinessDistrict");
                var index = obj.selectedIndex; // 选中索引
                var tagId = obj.options[index].value; // 选中值
                argSecondBusinessDistrict.dataStr = 'm=get_second_businessdistrict&tagId=' + tagId;
                var secondBusinessDistrictStr = "";
                hostObj.ioRequest(argSecondBusinessDistrict, function (d) {
                    if (d.list[0].status == 1) {
                        var businessDistrict = Y.JSON.parse(d.list[0].info);
                        for (var i = 0, len = businessDistrict.length; i < len; i++) {
                            var tagId = businessDistrict[i].TagId;
                            var name = businessDistrict[i].Name;
                            secondBusinessDistrictStr += '<option value=' + tagId + '>' + name + '</option>';
                        }
                    };
                });
                var _businessDistrict = Y.one("#secondBusinessDistrict");
                _businessDistrict.empty(true);
                _businessDistrict.append(secondBusinessDistrictStr.slice(0, -7));
            };
        },
        updateMap: function (lng, lat, cen) {
            this.shopLongitude = lng;
            this.shopLatitude = lat;	// 经纬数据更新
            var newPoint = new BMap.Point(this.shopLongitude, this.shopLatitude);
            this.mapObj.clearOverlays();
            this.mapObj.addOverlay(new BMap.Marker(newPoint));
            if (cen) {
                this.mapObj.setCenter(new BMap.Point(this.shopLongitude, this.shopLatitude));
            }
        },
        /*
         * [ 功能描述 ]	    门店图片展示
         * [ 方法名 ]	    shopimagerevelationHandler
         * [ 返回 ]
         */
        shopimagerevelationHandler: function () {
            var that = this, hostObj = that.get('host'), uploaderDone = Y.one("#uploaderDone");
            var uploaderDoneListener = false;

            function attachDeleteHandler() {
                var arg = {}, id = arguments[0], shopId = arguments[1], imgSprite = arguments[2];
                arg.dataStr = 'm=syb_delete_shoprevelationimg&id=' + id + '&shopId=' + shopId;
                hostObj.ioRequest(arg, function (data) {
                    if (data.list[0].status == 1) {
                        imgSprite.remove();
                    } else {
                        var okHandler = function () {
                            // 
                        };
                        VA.Singleton.popup.panel.set('headerContent', '提示信息');
                        VA.Singleton.popup.panel.set('bodyContent', data.list[0].info);
                        VA.Singleton.popup.panel.set('buttons', [VA.Singleton.popup.get('okButton')]);
                        VA.Singleton.popup.set('ok', okHandler);
                        VA.Singleton.popup.showPanel();
                    }
                });
            };
            function uploadshowHandler(data, autoAdd) {
                if (autoAdd && data.count < 5) {
                    uploaderDone.append('<a href="javascript:;"><i name="' + data.id + '_' + that.queryString.a + '" class="delete"></i><img height="130" width="172" src="' + data.imgUrl + '" /></a>');
                }
                if (uploaderDoneListener) {
                    uploaderDoneListener.detach();
                }
                uploaderDoneListener = uploaderDone.delegate('click', function (event) {
                    var t = event.currentTarget, dataStr = t.getAttribute('name'), imgSprite = t.ancestor('a');
                    var dataArray = dataStr.split("_");
                    attachDeleteHandler(dataArray[0], dataArray[1], imgSprite);
                }, '.delete', this);
                uploaderDoneListener = uploaderDone.delegate('click', function (event) {
                    var t = event.currentTarget,
                        imgSprite = t.ancestor('a'),
                        dataStr = t.getAttribute('name');
                    var dataArray = dataStr.split("_");
                    attachDeleteHandler(dataArray[0], dataArray[1], imgSprite);
                }, '.delete', this);
            }
            ;
            var arg = {};
            arg.dataStr = 'm=syb_get_shoprevelationimg&shopId=' + hostObj.queryString.a;
            hostObj.ioRequest(arg, function (data) {
                if (data.list[0].status === 1) {
                    var picList = Y.JSON.parse(data.list[0].info);
                    for (var i = 0, len = picList.length; i < len; i++) {
                        uploaderDone.append('<a href="javascript:;"><i name="' + picList[i].id + '_' + that.queryString.a + '" class="delete"></i><img height="130" src="' + picList[i].imgUrl + '" /></a>');
                    }
                    var a = {};
                    uploadshowHandler(a, false);
                    var uploaderObj = that.uploadHandler("#inputFileSprite", "#uploadBoundingBoxShop", "#conID", uploadshowHandler);
                    var submitHandler = function () {
                        var fileList = uploaderObj.get("fileList"), fileListLen = fileList.length;
                        if (fileListLen > 0) {
                            var lastFile = fileList[fileListLen - 1];
                            uploaderObj.upload(lastFile, "ajax/doSybUploadImg.ashx", { type: "shoprevelationimg", shopId: that.queryString.a });
                        }
                    };
                    Y.one("#submitUploaderBtn").on("click", submitHandler);
                }
            });
        },
        shoplistHandler: function (m) {
            var that = this, hostObj = this.get('host');
            var inputNode = Y.one('#acInput');
            inputNode.plug(Y.Plugin.AutoComplete, {
                activateFirstItem: true,
                resultHighlighter: 'phraseMatch',
                resultTextLocator: 'text',
                resultFilters: 'phraseMatch'
            });
            inputNode.on("keyup", function (evt) {
                var arg = {}, k = "";
                k = evt.currentTarget.get("value");
                arg.dataStr = 'm=get_viewalloc_employee&searchKeyWords=' + k;
                hostObj.ioRequest(arg, function (data) {
                    if (data.list[0].status == 1) {
                        var statesData = [];
                        var data = Y.JSON.parse(data.list[0].info);
                        for (var i = 0, len = data.length; i < len; i++) {
                            statesData.push({ "text": data[i].employeeName, "employeeId": data[i].employeeId });
                        }
                        inputNode.ac.set('source', statesData);
                    }
                });
            });
            inputNode.ac.after('select', function (e) {
                that.employeeId = e.result.raw.employeeId;
            });
						
						// dc
						var _inputNode = Y.one('#dcinput');
            _inputNode.plug(Y.Plugin.AutoComplete, {
                activateFirstItem: true,
                resultHighlighter: 'phraseMatch',
                resultTextLocator: 'text',
                resultFilters: 'phraseMatch'
            });
            _inputNode.on("keyup", function (evt) {
                var arg = {}, k = "";
                k = evt.currentTarget.get("value");
                arg.dataStr = 'm=get_viewalloc_employee&searchKeyWords=' + k;
                hostObj.ioRequest(arg, function (data) {
                    if (data.list[0].status == 1) {
                        var statesData = [];
                        var data = Y.JSON.parse(data.list[0].info);
                        for (var i = 0, len = data.length; i < len; i++) {
                            statesData.push({ "text": data[i].employeeName, "employeeId": data[i].employeeId });
                        }
                        _inputNode.ac.set('source', statesData);
                    }
                });
            });
            _inputNode.ac.after('select', function (e) {
                that.demployeeId = e.result.raw.employeeId;
            });

            var setAreaSelect = function () {
                var areaList = [{ area: "province", id: "0" }, { area: "city", id: that.get("provinceID") }, { area: "country", id: that.get("cityID") }];
                that.getAreaSelect("#widgetAreaShop", areaList);
                Y.all("#widgetAreaShop select").on('change', that.areaSelectHandle, that);
                that.getBusinessDistrict("#firstBusinessDistrict", "firstBusinessDistrict", "#secondBusinessDistrict", that.get("cityID"));
                Y.all("#BusinessDistrict select").on('change', this.businessSelectHandle, that);
            };
            setAreaSelect();
            this.after('provinceIDChange', function () {
                setAreaSelect();
            }, this);
            this.after('cityIDChange', function () {
                setAreaSelect();
            }, this);
            Y.all("#widgetAreaShop select").on('change', this.areaSelectHandle, this);
            Y.all("#BusinessDistrict select").on('change', this.businessSelectHandle, this);

            var akCode = "2vVE8OvjQAPIbMR6naON0Ici";
            var cfn = function (evt) {
                var addressStr = evt.currentTarget.get("value"), cityElem = Y.one(".city");
                var cityStr = cityElem._node[cityElem.get('selectedIndex')].firstChild.nodeValue;
                var address = encodeURIComponent(addressStr), city = encodeURIComponent("杭州");
                var url = 'http://api.map.baidu.com/geocoder/v2/?ak=' + akCode + '&output=json&address=' + address + '&city=' + cityStr;
                Y.jsonp(url, function (res) {
                    if (res.result) {
                        that.updateMap(res.result.location.lng, res.result.location.lat, true);
                    }
                });
            };

            Y.one("#shopAdress").on("blur", cfn, this);

            var keyups = null;

            Y.one("#shopAdress").on('keydown', function () {
                try {
                    clearTimeout(keyups);
                } catch (e) { };
            }, this);

            Y.one("#shopAdress").on("keyup", function (evt) {
                var that = this;
                keyups = setTimeout(function () {
                    cfn.call(that, evt);
                }, 1000);
            }, this);

            Y.one('#addShopOnBaiduMaps').on('click', function (evt) {
                Y.one("#shopAdress").trigger('blur');
            });

            var map = new BMap.Map("shopMap");
            var point = new BMap.Point(that.shopLongitude, that.shopLatitude);
            map.centerAndZoom(point, 15);
            map.addControl(new BMap.NavigationControl());
            map.addControl(new BMap.ScaleControl());
            map.addControl(new BMap.OverviewMapControl());
            map.addOverlay(new BMap.Marker(point));		// 初始化地图位置
            map.addEventListener("click", function (e) {
                that.updateMap(e.point.lng, e.point.lat, false);
            });
            that.mapObj = map;

            if (m === "add") {
                var companyNode = Y.one('#companyInput');
                companyNode.plug(Y.Plugin.AutoComplete, {
                    activateFirstItem: true,
                    resultHighlighter: 'phraseMatch',
                    resultTextLocator: 'text',
                    resultFilters: 'phraseMatch'
                });
                companyNode.on("keyup", function (evt) {
                    var arg = {}, k = "";
                    k = evt.currentTarget.get("value");
                    arg.dataStr = 'm=get_search_company&searchKeyWords=' + k;
                    hostObj.ioRequest(arg, function (data) {
                        if (data.list[0].status == 1) {
                            var statesData = [];
                            var data = Y.JSON.parse(data.list[0].info);
                            for (var i = 0, len = data.length; i < len; i++) {
                                statesData.push({ "text": data[i].companyName, "employeeId": data[i].companyID });
                            }
                            companyNode.ac.set('source', statesData);
                        }
                    });
                });
                companyNode.ac.after('select', function (e) {
                    that.companyID = e.result.raw.employeeId;
                });
                return;
            };
            function uploadshowHandler(data, autoAdd) {
                Y.one("#picLogo").set("src", data.imgUrl);
            }
            var uploaderObj = that.uploadHandler("#inputFileSprite", "#uploadBoundingBoxLogo", "#conID", uploadshowHandler);
            var submitHandler = function () {
                var fileList = uploaderObj.get("fileList"), fileListLen = fileList.length;
                if (fileListLen > 0) {
                    var lastFile = fileList[fileListLen - 1];
                    uploaderObj.upload(lastFile, "ajax/doSybUploadImg.ashx", { type: "shoplogo", shopId: that.queryString.a });
                }
            };
            Y.one("#submitUploaderBtn").on("click", submitHandler);
            // 门脸图上传
            function uploadshowBannerHandler(data, autoAdd) {
                Y.one("#picBannerShop").set("src", data.imgUrl);
            }
            var uploaderBannerObj = that.uploadHandler("#inputFileSpriteBanner", "#uploadBoundingBoxBanner", "#conIDBanner", uploadshowBannerHandler);
            var submitBannerHandler = function () {
                var fileList = uploaderBannerObj.get("fileList"), fileListLen = fileList.length;
                if (fileListLen > 0) {
                    var lastFile = fileList[fileListLen - 1];
                    uploaderBannerObj.upload(lastFile, "ajax/doSybUploadImg.ashx", { type: "shoppublicityphoto", shopId: that.queryString.a });
                }
            };
            Y.one("#submitBannerBtn").on("click", submitBannerHandler);

            var qrcode = {};
            qrcode.dataStr = 'm=syb_before_qrcode&shopId=' + hostObj.queryString.a;
            hostObj.ioRequest(qrcode, function (data) {
                if (data.list[0].status === 1) {
                    var info = Y.JSON.parse(data.list[0].info);
                    var bao = info[0], ka = info[1];
                    bao.imgUrl = bao.imgUrl ? bao.imgUrl : "images/nonImg3.png";
                    ka.imgUrl = ka.imgUrl ? ka.imgUrl : "images/nonImg3.png";
                    Y.one("#picBao").set("src", bao.imgUrl);
                    Y.one("#picKa").set("src", ka.imgUrl);
                    var baoNode = Y.one("#bao");
                    baoNode.one(".item-name").set("text", bao.typeName);
                    baoNode.one(".create-qrcode").on("click", function () {
                        var baocode = {};
                        baocode.dataStr = 'm=syb_qrcode_operate&shopId=' + hostObj.queryString.a + "&typeId=" + bao.typeId;
                        hostObj.ioRequest(baocode, function (d) {
                            if (d.list[0].status === 1) {
                                Y.one("#picBao").set("src", d.list[0].info);
                            }
                        });
                    }, this);
                    var kaNode = Y.one("#ka");
                    kaNode.one(".item-name").set("text", ka.typeName);
                    kaNode.one(".create-qrcode").on("click", function () {
                        var kacode = {};
                        kacode.dataStr = 'm=syb_qrcode_operate&shopId=' + hostObj.queryString.a + "&typeId=" + ka.typeId;
                        hostObj.ioRequest(kacode, function (d) {
                            if (d.list[0].status === 1) {
                                Y.one("#picKa").set("src", d.list[0].info);
                            }
                        });
                    }, this);
                    Y.all(".download-qrcode").on("click", function (evt) {
                        var name = (evt.currentTarget.get("name").slice(6));
                        var srcStr = Y.one("#pic" + name).get("src");
                        window.open(srcStr, "_blank", "width=500, height=500,status=0");
                    });
                }
            });
        },
        companylistedit: function () {
            var obj = {}, hostObj = this.get('host'), arg = {};
            arg.dataStr = 'm=syb_get_company_detail&companyId=' + hostObj.queryString.a;
            hostObj.ioRequest(arg, function (data) {
                if (data.list[0].status === 1) {
                    obj.d = Y.JSON.parse(data.list[0].info);
                    obj.template = '<ul>'
                        + '	<li class="gray"><label for="">品牌：</label><span class="txt"><input type="text" class="inputText" value="{ownedCompany}" /> <em class="txtColor">*</em></span></li>'
                        + '	<li><label for="">所属公司：</label><span class="txt"><input type="text" class="inputText" value="{companyName}" /> <em class="txtColor">*</em></span></li>'
                        + '	<li class="gray"><label for="">公司电话：</label><span class="txt"><input type="text" class="inputText" value="{companyTelePhone}" /></span></li>'
                        + '	<li><label for="">联系人：</label><span class="txt"><input type="text" class="inputText" value="{contactPerson}" /></span></li>'
                        + '	<li class="gray"><label for="">联系人电话：</label><span class="txt"><input type="text" class="inputText" value="{contactPhone}" /></span></li>'
                        + '	<li><label for="">新浪微博：</label><span class="txt"><input type="text" class="inputText" value="{sinaWeiboName}" /></span></li>'
                        + '	<li class="gray"><label for="">腾讯微博：</label><span class="txt"><input type="text" class="inputText" value="{qqWeiboName}" /></span></li>'
                        + '	<li><label for="">微信公共帐号：</label><span class="txt"><input type="text" class="inputText" value="{wechatPublicName}" /></span></li>'
                        + '	<li class="gray"><label for="">人均消费：</label><span class="txt"><input type="text" class="inputText " value="{acpp}" /></span></li>'
                        + '	<li><label for="">公司地址：</label><span class="txt"><input type="text" class="inputText address" value="{companyAddress}" /> </span></li>'
                        + '	<li class="gray companyManageComment"><label for="">公司描述：</label><span class="txt"><textarea class="area">{companyDescription}</textarea></span></li>'
                        + '</ul>';
                }
            });
            return obj;
        },
        companylistadd: function () {
            Y.one('.companyManage').addClass('companyManageAdd');
            var obj = {};
            obj.d = { "companyName": "", "ownedCompany": "", "companyTelePhone": "", "contactPerson": "", "contactPhone": "", "sinaWeiboName": "", "qqWeiboName": "", "wechatPublicName": "", "acpp": 0, "companyAddress": "", "companyDescription": "" };
            obj.template = '<ul>'
                + '	<li class="gray"><label for="">品牌：</label><span class="txt"><input type="text" class="inputText" value="{ownedCompany}" /> <em class="txtColor">*</em></span></li>'
                + '	<li><label for="">所属公司：</label><span class="txt"><input type="text" class="inputText" value="{companyName}" /> <em class="txtColor">*</em></span></li>'
                + '	<li class="gray"><label for="">公司电话：</label><span class="txt"><input type="text" class="inputText" value="{companyTelePhone}" /></span></li>'
                + '	<li><label for="">联系人：</label><span class="txt"><input type="text" class="inputText" value="{contactPerson}" /></span></li>'
                + '	<li class="gray"><label for="">联系人电话：</label><span class="txt"><input type="text" class="inputText" value="{contactPhone}" /></span></li>'
                + '	<li><label for="">新浪微博：</label><span class="txt"><input type="text" class="inputText" value="{sinaWeiboName}" /></span></li>'
                + '	<li class="gray"><label for="">腾讯微博：</label><span class="txt"><input type="text" class="inputText" value="{qqWeiboName}" /></span></li>'
                + '	<li><label for="">微信公共帐号：</label><span class="txt"><input type="text" class="inputText" value="{wechatPublicName}" /></span></li>'
                + '	<li class="gray"><label for="">人均消费：</label><span class="txt"><input type="text" class="inputText" value="{acpp}" /></span></li>'
                + '	<li><label for="">公司地址：</label><span class="txt"><input type="text" class="inputText address" value="{companyAddress}" /> </span></li>'
                + '	<li class="gray companyManageComment"><label for="">公司描述：</label><span class="txt"><textarea class="area">{companyDescription}</textarea></span></li>'
                + '</ul>';
            return obj;
        },
        companylistcommission: function () {
            Y.one('.companyManage').addClass('companyCommission');
            var obj = {}, hostObj = this.get('host'), arg = {};
            arg.dataStr = 'm=syb_get_commissioninfo&companyId=' + hostObj.queryString.a;
            hostObj.ioRequest(arg, function (data) {
                if (data.list[0].status === 1) {
                    obj.d = Y.JSON.parse(data.list[0].info);
                    var commissionType = obj.d.viewallocCommissionType, backPriceUintStr = '', backPriceNumberClass = "";
                    var commissionNumber, commissionRate;
                    if (commissionType == 1) {
                        commissionNumber = 'checked="checked"';
                        commissionRate = '';
                        backPriceUintStr = ' 元';
                    } else if (commissionType == 2) {
                        commissionNumber = '';
                        commissionRate = 'checked="checked"';
                        backPriceNumberClass = "numberOneLess";
                        backPriceUintStr = ' 请输入0-1(不包含1)间的小数值(仅支持两位)';
                    }
                    obj.template = '<ul>'
                        + '	<li class="gray"><label for="">所属公司：</label><span class="txt">{companyName}</span></li>'
                        + '	<li><label for="">佣金形式：</label><span class="txt" id="backMode"><input type="radio" name="commission" ' + commissionNumber + ' class="inputRadio" value="1" /> 数值  <input type="radio" name="commission" ' + commissionRate + ' class="inputRadio" value="2" /> 比例  </span></li>'
                        + '	<li class="gray"><label for="">佣金值：</label><span class="txt"><input type="text" id="backPriceNumber" class="inputText verify numberRequired ' + backPriceNumberClass + '" value="{viewallocCommissionValue}" /> <em id="backPriceUint">' + backPriceUintStr + '</em></span></li>'
                        + '	<li><label for="">无忧退款时间：</label><span class="txt"><input type="text" class="inputText" value="{freeRefundHour}" /> ( 单位：小时 )</span></li>'
                        + '</ul>';
                }
            });
            return obj;
        },
        companyaccountlistedit: function () {
            Y.one('.companyManage').addClass('companyAccount');
            var obj = {}, hostObj = this.get('host'), arg = {};
            arg.dataStr = 'm=syb_get_bankaccount_detail&accountId=' + hostObj.queryString.b;
            hostObj.ioRequest(arg, function (data) {
                if (data.list[0].status === 1) {
                    obj.d = Y.JSON.parse(data.list[0].info);
                    obj.template = '<ul>'
                        + '	<li class="gray"><label for="">账户号码：</label><span class="txt"><input type="text" class="inputText" value="{accountNum}" /> </span></li>'
                        + '	<li><label for="">开户名称：</label><span class="txt"><input type="text" class="inputText" value="{accountName}" /> </li>'
                        + '	<li class="gray"><label for="">开户银行：</label><span class="txt"><input type="text" class="inputText" value="{bankName}" /> </span></li>'
												+ '	<li class="gray"><label for="">支行名称：</label><span class="txt"><input type="text" class="inputText" value="{payeeBankName}" /> </span></li>'
                        + '	<li class="companyManageComment"><label for="">备注信息：</label><span class="txt"><textarea class="area">{remark}</textarea></span></li>'
                        + '</ul>';
                }
            });
            return obj;
        },
        companyaccountlistadd: function () {
            Y.one('.companyManage').addClass('companyAccount');
            var obj = {};
            obj.d = { "accountNum": "", "accountName": "", "bankName": "", "remark": "", "payeeBankName": "" };
            obj.template = '<ul>'
                + '	<li class="gray"><label for="">账户号码：</label><span class="txt"><input type="text" class="inputText" value="{accountNum}" /> </span></li>'
                + '	<li><label for="">开户名称：</label><span class="txt"><input type="text" class="inputText" value="{accountName}" /> </li>'
                + '	<li class="gray"><label for="">开户银行：</label><span class="txt"><input type="text" class="inputText" value="{bankName}" /> </span></li>'
								+ '	<li class="gray"><label for="">支行名称：</label><span class="txt"><input type="text" class="inputText" value="{payeeBankName}" /> </span></li>'
                + '	<li class="companyManageComment"><label for="">备注信息：</label><span class="txt"><textarea class="area">{remark}</textarea></span></li>'
                + '</ul>';
            return obj;
        },
        companymenulistedit: function () {
            var obj = {}, hostObj = this.get('host'), arg = {};
            Y.one('.companyManage').addClass('companyMenuAdd');
            arg.dataStr = 'm=syb_before_add_update_menu&menuCompanyId=' + hostObj.queryString.mcid + '&companyId=' + hostObj.queryString.a;
            hostObj.ioRequest(arg, function (data) {
                if (data.list[0].status === 1) {
                    var tempData = Y.JSON.parse(data.list[0].info);
                    obj.d = { "companyName": tempData.companyName, "menuName": tempData.menuInfo.menuName, "remark": tempData.menuInfo.menuDesc };
                    obj.template = '<ul>'
                        + '	<li class="gray"><label for="">所属公司：</label><span class="txt">{companyName} </span></li>'
                        + '	<li><label for="">菜谱名称：</label><span class="txt"><input type="text" class="inputText" value="{menuName}" /> </li>'
                        + '	<li class="gray companyManageComment"><label for="">菜谱描述：</label><span class="txt"><textarea class="area">{remark}</textarea></span></li>'
                        + '</ul>';
                }
            });
            return obj;
        },
        companymenulistadd: function () {
            var obj = {}, hostObj = this.get('host');
            var companyName = decodeURIComponent(hostObj.queryString.cname);
            Y.one('.companyManage').addClass('companyMenuAdd');
            obj.d = { "companyName": companyName, "menuName": "", "remark": "" };
            obj.template = '<ul>'
                + '	<li class="gray"><label for="">所属公司：</label><span class="txt">{companyName} </span></li>'
                + '	<li><label for="">菜谱名称：</label><span class="txt"><input type="text" class="inputText" value="{menuName}" /> </li>'
                + '	<li class="gray companyManageComment"><label for="">菜谱描述：</label><span class="txt"><textarea class="area">{remark}</textarea></span></li>'
                + '</ul>';
            return obj;
        },
        shopimagerevelation: function () {
            Y.one('.companyManage').addClass('shopimagerevelation');
            var obj = {};
            obj.d = { "accountNum": "", "accountName": "", "bankName": "", "remark": "" };
            obj.template = '<ul>'
                + '	<li class="gray uploadSprite" id="conID"><label for="">环境图片：</label>'
                + '    <span class="txt">'
                + '        <input type="text" id="picUrl" class="inputText" value="{accountNum}" />'
                + '        <div class="uploadBoundingBox" id="uploadBoundingBoxShop">'
                + '             <input type="button" class="inputBtn" id="inputFileSprite" value="浏览..." /> '
                + '        </div>'
                + '        <input type="submit" value="上传" class="inputBtn submitUploaderBtn" id="submitUploaderBtn" name="submit" />( 最小尺寸652*489,大小不超过3M，格式 jpg | png )'
                + '    </span>'
                + '  </li>'
                + '	<li class="companyManageComment"><label for="">缩略图：</label><div class="txt thumbnail" id="uploaderDone"></div></li>'
                + '  <li class="gray"></li>'
                + '</ul>';
            return obj;
        },
        shopsundrylistedit: function () {
            var obj = {}, hostObj = this.get('host'), arg = {};
            Y.one('.companyManage').addClass('shopsundrymanage');
            arg.dataStr = 'm=syb_before_update_sundry&sundryId=' + hostObj.queryString.syid;
            hostObj.ioRequest(arg, function (data) {
                if (data.list[0].status === 1) {
                    var tempData = Y.JSON.parse(data.list[0].info);
                    var sundryMode1 = (tempData.sundryChargeMode == 1) ? ' checked="checked"' : ' ',
                        sundryMode2 = (tempData.sundryChargeMode == 2) ? ' checked="checked"' : ' ',
                        sundryMode3 = (tempData.sundryChargeMode == 3) ? ' checked="checked"' : ' ',
                        sundryRequiredYes = (tempData.required == true) ? ' checked="checked"' : ' ',
                        sundryRequiredNo = (tempData.required == false) ? ' checked="checked"' : ' ';
                    var price, priceUint = '', sundryMode = '';
                    if (tempData.sundryChargeMode == 2) {
                        price = Math.round((tempData.price * 10000)) / 100;
                        priceUint = ' %';
                        sundryMode = '<input type="radio" id="sundryMode2" name="sundryMode" ' + sundryMode2 + ' class="inputRadio" value="2" /> 按比例  ';
                    } else {
                        sundryMode = '<input type="radio" name="sundryMode" ' + sundryMode1 + ' class="inputRadio" value="1" /> 按固定金额  <input type="radio" name="sundryMode" ' + sundryMode3 + ' class="inputRadio" value="3" /> 按人次  ';
                        price = Math.round((tempData.price * 100)) / 100;
                        priceUint = ' 元';
                    }
                    obj.d = { "sundryName": tempData.sundryName, "sundryStandard": tempData.sundryStandard, "description": tempData.description, "price": tempData.price, "shopName": decodeURIComponent(hostObj.queryString.sname) };
                    obj.template = '<ul>'
                        + '	<li class="gray"><label for="">所属门店：</label><span class="txt">{shopName} </span></li>'
                        + '	<li><label for="">杂项名称：</label><span class="txt"><input type="text" class="inputText" value="{sundryName}" /> </span></li>'
                        + '	<li class="gray"><label for="">规格名称：</label><span class="txt"><input type="text" class="inputText" value="{sundryStandard}" /> </span></li>'
                        + '	<li><label for="">收费模式：</label><span class="txt" id="sundryMode">' + sundryMode + '</span></li>'
                        + '	<li class="gray"><label for="">额　　度：</label><span class="txt"><input type="text" class="inputText" value="' + price + '" /> <em id="sundryPriceUint">' + priceUint + '</em></span></li>'
                        + '	<li><label for="">必　　选：</label><span class="txt"><input type="radio" name="commission" ' + sundryRequiredYes + ' class="inputRadio" value="true" /> 是  　　<input type="radio" name="commission" ' + sundryRequiredNo + ' class="inputRadio" value="false" /> 否 </span> </li>'
                        + '	<li class="gray companyManageComment"><label for="">描　　述：</label><span class="txt"><textarea class="area">{description}</textarea></span></li>'
                        + '</ul>';
                }
            });
            return obj;
        },
        shopsundrylistadd: function () {
            var obj = {}, hostObj = this.get('host');
            obj.d = { "sundryName": "", "sundryStandard": "", "description": "", "price": "", "shopName": decodeURIComponent(hostObj.queryString.sname) };
            obj.template = '<ul>'
                + '	<li class="gray"><label for="">所属门店：</label><span class="txt">{shopName} </span></li>'
                + '	<li><label for="">杂项名称：</label><span class="txt"><input type="text" class="inputText" value="{sundryName}" /> </span></li>'
                + '	<li class="gray"><label for="">规格名称：</label><span class="txt"><input type="text" class="inputText" value="{sundryStandard}" /> </span></li>'
                + '	<li><label for="">收费模式：</label><span class="txt" id="sundryMode"><input type="radio" name="sundryMode" checked="checked" class="inputRadio" value="1" /> 按固定金额  <input type="radio" name="sundryMode" class="inputRadio" value="3" /> 按人次 </span></li>'
                + '	<li class="gray"><label for="">额　　度：</label><span class="txt"><input type="text" class="inputText" value="0" /> <em id="sundryPriceUint"> 元</em></span></li>'
                + '	<li><label for="">必　　选：</label><span class="txt"><input type="radio" name="commission" class="inputRadio" value="true" /> 是  　　<input type="radio" name="commission" checked="checked" class="inputRadio" value="false" /> 否 </span> </li>'
                + '	<li class="gray companyManageComment"><label for="">描　　述：</label><span class="txt"><textarea class="area">{description}</textarea></span></li>'
                + '</ul>';
            return obj;
        },
        shopdiscountlistedit: function () {
            var obj = {}, hostObj = this.get('host'), arg = {};
            Y.one('.companyManage').addClass('shopdiscounttxt');
            arg.dataStr = 'm=syb_before_add_update_shopvip&shopId=' + hostObj.queryString.a + '&shopVipId=' + hostObj.queryString.vid;
            hostObj.ioRequest(arg, function (data) {
                if (data.list[0].status == 1) {
                    var tempData = Y.JSON.parse(data.list[0].info);
                    var vipStr = '', vaVip = tempData.vaVip, platformVipId = tempData.shopVip.platformVipId;
                    for (var i = 0, len = vaVip.length; i < len; i++) {
                        var checkStr = (platformVipId == vaVip[i].id) ? ' checked="checked"' : '';
                        vipStr += '<input type="radio" name="vip" ' + checkStr + ' class="inputRadio" value="' + vaVip[i].id + '" /> ' + vaVip[i].name + ' — ';
                    }
                    ;
                    vipStr = vipStr.slice(0, -2);
                    obj.d = { "discountName": tempData.shopVip.name, "discount": Math.round(tempData.shopVip.discount * 10000) / 100, "shopName": decodeURIComponent(hostObj.queryString.sname) };
                    obj.template = '<ul>'
                        + '	<li class="gray"><label for="">所属门店：</label><span class="txt">{shopName} </span></li>'
                        + '	<li><label for="">折扣名称：</label><span class="txt"><input type="text" class="inputText" value="{discountName}" /> </span></li>'
                        + '	<li class="gray"><label for="">折扣率：</label><span class="txt"><input type="text" class="inputText" value="{discount}" /> %</span></li>'
                        + '	<li><label for="">等级：</label><span class="txt">' + vipStr + '</span></li>'
                        + '</ul>';
                }
            });
            return obj;
        },
        shopdiscountlistadd: function () {
            var obj = {}, hostObj = this.get('host'), arg = {};
            Y.one('.companyManage').addClass('shopdiscounttxt');
            arg.dataStr = 'm=syb_before_add_update_shopvip&shopId=' + hostObj.queryString.a + '&shopVipId=' + hostObj.queryString.vid;
            hostObj.ioRequest(arg, function (data) {
                if (data.list[0].status == 1) {
                    var tempData = Y.JSON.parse(data.list[0].info);
                    var vipStr = '', vaVip = tempData.vaVip, platformVipId = tempData.shopVip.platformVipId;
                    for (var i = 0, len = vaVip.length; i < len; i++) {
                        var checkStr = (i === 0) ? ' checked="checked"' : '';
                        vipStr += '<input type="radio" name="vip" ' + checkStr + ' class="inputRadio" value="' + vaVip[i].id + '" /> ' + vaVip[i].name + ' — ';
                    }
                    ;
                    vipStr = vipStr.slice(0, -2);
                    obj.d = { "discountName": "", "discount": "100", "shopName": decodeURIComponent(hostObj.queryString.sname) };
                    obj.template = '<ul>'
                        + '	<li class="gray"><label for="">所属门店：</label><span class="txt">{shopName} </span></li>'
                        + '	<li><label for="">折扣名称：</label><span class="txt"><input type="text" class="inputText" value="{discountName}" /> </span></li>'
                        + '	<li class="gray"><label for="">折扣率：</label><span class="txt"><input type="text" class="inputText" value="{discount}" /> %</span></li>'
                        + '	<li><label for="">等级：</label><span class="txt">' + vipStr + '</span></li>'
                        + '</ul>';
                }
            });
            return obj;
        },
        shophandle: function () {
            var obj = {}, hostObj = this.get('host'), arg = {}, that = this;
            Y.one('.companyManage').addClass('shophandle');
            arg.dataStr = 'm=syb_shop_ishandle_before&shopId=' + hostObj.queryString.a;
            hostObj.ioRequest(arg, function (data) {
                if (data.list[0].status == 1) {
                    var tempData = Y.JSON.parse(data.list[0].info);
                    var supportPaymentStr = tempData.isSupportPayment ? "支持" : "暂不支持（ 若为”暂不支持“选项，则原因说明为必填 ）";
                    var isHandle1 = tempData.isHandle == 1 ? checked = "checked" : '';
                    var isHandle2 = tempData.isHandle == -1 ? checked = "checked" : '';
                    var isHandle3 = tempData.isHandle == -2 ? checked = "checked" : '';
                    that.shopLongitude = tempData.longitude,
                    that.shopLatitude = tempData.latitude;
                    var publicityPhotoPath = tempData.publicityPhotoPath;
                    if (!publicityPhotoPath) {
                        publicityPhotoPath = "images/nonImg4.png";
                    }
                    obj.d = tempData;
                    obj.template = '<ul>'
                        + '	<li class="gray"><label for="">门店名称：</label><span class="txt">{shopName} </span></li>'
                        + '	<li><label for="">所属公司：</label><span class="txt">{companyName} </span></li>'
                        + '	<li class="gray companyManageComment"><label for="">门店 logo：</label><span class="txt"><img class="logo" width="136px" height="136px" src="{shopLogo}" /></span></li>'
                        + '	<li><label for="">电　　话：</label><span class="txt">{shopTelephone} </span></li>'
                        + '	<li class="gray"><label for="">联系人员：</label><span class="txt">{contactPerson} </span></li>'
                        + '	<li><label for="">联系人电话：</label><span class="txt">{contactPhone} </span></li>'
                        + '	<li class="gray"><label for="">客户经理：</label><span class="txt">{accountManagerName} </span></li>'
                        + '	<li><label for="">新浪微博：</label><span class="txt">{sinaWeiboName} </span></li>'
                        + '	<li class="gray"><label for="">腾讯微博：</label><span class="txt">{qqWeiboName} </span></li>'
                        + '	<li><label for="">微信公众帐号：</label><span class="txt">{wechatPublicName} </span></li>'
                        + '	<li class="gray"><label for="">营业执照：</label><span class="txt">{shopBusinessLicense} </span></li>'
                        + '	<li><label for="">卫生许可证：</label><span class="txt">{shopHygieneLicense} </span></li>'
                        + '	<li class="gray"><label for="">营业时间：</label><span class="txt">{openTimes} </span></li>'
                        + '	<li><label for="">门店地区：</label><span class="txt">{provinceName}{countyName} </span></li>'
                        + '	<li class="gray"><label for="">详细地址：</label><span class="txt">{shopAddress} </span></li>'
                            + '	<li class="companyManageComment shopMapContainer"><label for="">门店位置：</label><span class="txt shopMapSprite"> <div id="shopMap"></div> </span></li>'
                        + '	<li class="gray"><label for="">店铺描述：</label><span class="txt">{shopDescription} </span></li>'
                        + '	<li><label for="">店铺评分：</label><span class="txt">{shopRating} </span></li>'
                        + '	<li class="gray"><label for="">人均消费：</label><span class="txt">{acpp} 元/人</span></li>'
                        + '	<li><label for="">支付功能：</label><span class="txt">' + supportPaymentStr + ' </span></li>'
                        + '	<li class="gray"><label for="">原因说明：</label><span class="txt">{notPaymentReason} </span></li>'
                        + '	<li><label for="">公告描述：</label><span class="txt">{orderDishDesc} </span>(最多可以输入200个字符)</li>'
                        + '	<li class="gray companyManageComment"><label for="">门脸图片：</label><span class="txt"><img class="logo" width="410px" height="136px" src="' + publicityPhotoPath + '" /></span></li>'
                        + '	<li><label for="">审核状态：</label><span class="txt"><input type="radio" name="shopStatus" ' + isHandle1 + ' class="inputRadio" value="1" /> 已通过审核 <input type="radio" name="shopStatus" class="inputRadio" ' + isHandle2 + ' value="-1" /> 未通过审核 <input type="radio" name="shopStatus" class="inputRadio" ' + isHandle3 + ' value="-2" /> 待审核</span></li>'
                        + '</ul>';
                }
            });
            return obj;
        },
        financehandle: function () {
            var obj = {}, hostObj = this.get('host'), arg = {}, that = this;
            Y.one('.companyManage').addClass('financehandle');
            arg.dataStr = 'm=syb_shop_ishandle_before&shopId=' + hostObj.queryString.a;
            hostObj.ioRequest(arg, function (data) {
                if (data.list[0].status == 1) {
                    var tempData = Y.JSON.parse(data.list[0].info);
                    var supportPaymentStr = tempData.isSupportPayment ? "支持" : "暂不支持（ 若为”暂不支持“选项，则原因说明为必填 ）";
                    var isHandle1 = tempData.isHandle == 1 ? checked = "checked" : '';
                    var isHandle2 = tempData.isHandle == -1 ? checked = "checked" : '';
                    var isHandle3 = tempData.isHandle == -2 ? checked = "checked" : '';
                    that.shopLongitude = tempData.longitude,
                    that.shopLatitude = tempData.latitude;
                    var publicityPhotoPath = tempData.publicityPhotoPath;
                    if (!publicityPhotoPath) {
                        publicityPhotoPath = "images/nonImg4.png";
                    }
                    obj.d = tempData;
                       obj.template = '<table cellpadding="0" cellspacing="0" class="financeTable">'
                        + '<tr><td>门店名称：</td><td>{shopName}</td></tr>'
                        + '<tr><td>所属公司：</td><td>{companyName}</td></tr>'
                        + '<tr class="gray">'
                        + '<td>提款方式：</td>'
                        +   '<td>'
                        +     '<input type="checkbox" name="getMoneyway" checked />星期一'
                        +     '<input type="checkbox" name="getMoneyway" />星期二'
                        +     '<input type="checkbox" name="getMoneyway" checked />星期三'
                        +     '<input type="checkbox" name="getMoneyway" />星期四'
                        +     '<input type="checkbox" name="getMoneyway" />星期五'
                        +   '</td>'
                        + '</tr>'
                        + '<tr>'
                        +   '<td>折扣：</td>'
                        +   '<td>'
                        +     '<p>折扣比例：<input type="text" class="inputText" /> %　　(请输入0~100的数字)</p>'
                        +   '</td>'
                        + '</tr>'
                        + '<tr class="gray">'
                        +   '<td>佣金：</td>'
                        +   '<td>'
                        +     '<p>佣金比例：<input type="text" class="inputText" /> %　　(请输入0~100的数字)</p>'
                        +   '</td>'
                        + '</tr>'
                        + '<tr>'
                        +   '<td>银行账户：</td>'
                        +   '<td>'
                        +     '<table cellpadding="0" cellspacing="0" class="financeTable">'
                        +       '<tr><td><span class="txtColorRed noBold">*</span>账户号码：</td><td><input type="text" placeholder="请输入账户号码" class="inputText" /></td></tr>'
                        +       '<tr><td><span class="txtColorRed noBold">*</span>开户名：</td><td><input type="text" placeholder="王某某" class="inputText" /></td></tr>'
                        +       '<tr><td><span class="txtColorRed noBold">*</span>开户银行：</td><td>'
                        +         '<select>'
                        +         '<option>中国银行</option>'
                        +         '<option>上海浦东发展银行</option>'
                        +         '<option>中国建设银行</option>'
                        +         '<option>中国农业银行</option>'
                        +         '</select>'
                        +       '</td></tr>'
                        +       '<tr><td><span class="txtColorRed noBold">*</span>支行名称：</td><td><input type="text" placeholder="中国银行滨江支行" class="inputText" /></td></tr>'
                        +     '</table>'
                        +   '</td>'
                        + '</tr>'
                        /*+ '<tr class="gray">'
                        +   '<td>服务年费：</td>'
                        +   '<td>'
                        +     '<table cellpadding="0" cellspacing="0" class="financeTable">'
                        +       '<tr><td>开启状态：</td><td><input type="radio" name="annualFeeStatus" checked />开启<input type="radio" name="annualFeeStatus" />关闭</td></tr>'
                        +       '<tr><td>开始时间：</td><td><input type="text" placeholder="请输入账户号码" class="inputText" /></td></tr>'
                        +       '<tr><td>缴费时间：</td><td>'
                        +         '<select>'
                        +         '<option>1年</option>'
                        +         '<option>2年</option>'
                        +         '<option>3年</option>'
                        +         '<option>4年</option>'
                        +         '</select>'
                        +       '</td></tr>'
                        +       '<tr><td>结束时间：</td><td><input type="text" placeholder="" class="inputText" /></td></tr>'
                        +       '<tr><td>服务年费：</td><td><input type="text" placeholder="请输入年费金额" class="inputText" /></td></tr>'
                        +       '<tr><td>收取方式：</td><td><input type="radio" name="collectway" />预付<input type="radio" name="collectway" />流水扣除</td></tr>'
                        +       '<tr><td></td><td>已收款项：<input type="text" class="inputText" /></td></tr>'
                        +     '</table>'
                        +   '</td>'
                        + '</tr>'*/
                        + '<tr class="gray">'
                        + '<td>审核状态：</td>'
                        + '<td>'
                        +   '<input type="radio" name="handleStatus" />待审核'
                        +   '<input type="radio" name="handleStatus" />已通过审核'
                        +   '<input type="radio" name="handleStatus" />未通过审核'
                        +   '<input type="radio" name="handleStatus" checked />待账务审核'
                        + '</td>'
                        + '</tr>'
                        + '</table>'
                }
            });
            return obj;
        },
        withdrawtype: function () {
            var obj = {}, hostObj = this.get('host'), arg = {}, that = this;
            Y.one('.companyManage').addClass('withdrawtype');
            arg.dataStr = 'm=syb_get_withdrawtype&shopId=' + hostObj.queryString.a;
            hostObj.ioRequest(arg, function (data) {
                if (data.list[0].status == 1) {
                  var s0 = '', s1 = '', s2 = '', s3 = '', s4 = '';
                  var str = data.list[0].info.split(',');
                  
                  for( var i = 0; i < str.length; i++ ){
                    if( str[i] === '1' ){ s0 = 'checked'; }
                    else if( str[i] === '2' ){ s1 = 'checked'; }
                    else if( str[i] === '3' ){ s2 = 'checked'; }
                    else if( str[i] === '4' ){ s3 = 'checked'; }
                    else if( str[i] === '5' ){ s4 = 'checked'; }
                  }
                  
                  obj.template = '<ul class="withdrawTypeUL">'
                               +  '<li class="title">提款方式设置：</li>'
                               +  '<li class="txt"><input type="checkbox" name="widthdrawtype" id="withdrawType1" value="1" '+ s0 +' />星期一</li>'
                               +  '<li class="txt"><input type="checkbox" name="widthdrawtype" id="withdrawType2" value="2" '+ s1 +' />星期二</li>'
                               +  '<li class="txt"><input type="checkbox" name="widthdrawtype" id="withdrawType3" value="3" '+ s2 +' />星期三</li>'
                               +  '<li class="txt"><input type="checkbox" name="widthdrawtype" id="withdrawType4" value="4" '+ s3 +' />星期四</li>'
                               +  '<li class="txt"><input type="checkbox" name="widthdrawtype" id="withdrawType5" value="5" '+ s4 +' />星期五</li>'
                               +'</ul>'
                }
            });
            return obj;
        },
        commision: function () {
            var obj = {}, hostObj = this.get('host'), arg = {}, that = this;
            Y.one('.companyManage').addClass('commision');
            arg.dataStr = 'm=syb_get_viewalloccommissionvalue&shopId=' + hostObj.queryString.a;
            hostObj.ioRequest(arg, function (data) {
                if (data.list[0].status == 1) {
                       obj.template = '<table cellpadding="0" cellspacing="0" class="commisionTable">'
                                     +  '<tr>'
                                     +    '<td>佣金：</td>'
                                     +    '<td><label for="" class="hide">佣金：</label><span class="txt"><input type="text" class="inputText verify numberOneToHundred" value="'+ data.list[0].info +'"  /> %    （请输入0-100的数字）</span></td>'
                                     +  '</tr>'
                                     /*+  '<tr>'
                                     +    '<td></td>'
                                     +    '<td><span class="txtColorRed">佣金比例默认值为：0</span></td>'
                                     +  '</tr>'*/
                                     +'</table>'
                }
            });
            return obj;
        },
        shopvipdiscount: function () {
            var obj = {}, hostObj = this.get('host'), arg = {}, that = this;
            Y.one('.companyManage').addClass('shopvipdiscount');
            arg.dataStr = 'm=syb_get_discount&shopId=' + hostObj.queryString.a;
            hostObj.ioRequest(arg, function (data) {
                if (data.list[0].status == 1) {
                       obj.template = '<table cellpadding="0" cellspacing="0" class="shopvipdiscountTable">'
                                     +  '<tr>'
                                     +    '<td>折扣：</td>'
                                     +    '<td><label for="" class="hide">折扣：</label><span class="txt"><input type="text" class="inputText verify inputRequired intNumberRequired" value="' + data.list[0].info + '" /> %    （请输入0-100的数字）</span></td>'
                                     +  '</tr>'
                                     /*+  '<tr>'
                                     +    '<td></td>'
                                     +    '<td><span class="txtColorRed">折扣比例默认值为：100</span></td>'
                                     +  '</tr>'*/
                                     +'</table>'
                }
            });
            return obj;
        },
        companyaccount: function () {
            var obj = {}, hostObj = this.get('host'), arg = {}, that = this;
            Y.one('.companyManage').addClass('companyAccount');
            arg.dataStr = 'm=syb_get_account&shopId=' + hostObj.queryString.a;
            hostObj.ioRequest(arg, function (data) {
                if (data.list[0].status == 1) {
                  var tempData = Y.JSON.parse(data.list[0].info);
                  obj.d = tempData;
                  obj.template = '<table cellpadding="0" cellspacing="0" class="companyaccountTable">'
                               +  '<tr class="gray">'
                               +    '<td><span class="txtColorRed">*</span>账户号码：</td>'
                               +    '<td><label for="" class="hide">账户号码：</label><span class="txt"><input type="text" class="inputText verify inputRequired mustBeNumber" value="{accountNum}" /></span></td>'
                               +  '</tr>'
                               +  '<tr>'
                               +    '<td><span class="txtColorRed">*</span>开户名：</td>'
                               +    '<td><label for="" class="hide">开户名：</label><span class="txt"><input type="text" class="inputText verify inputRequired wordChk" value="{accountName}" /></span></td>'
                               +  '</tr>'
                               +  '<tr class="gray">'
                               +    '<td><span class="txtColorRed">*</span>开户银行：</td>'
                               +    '<td><label for="" class="hide">开户银行：</label><span class="txt"><input type="text" class="inputText verify inputRequired wordChk" value="{bankName}" /></span></td>'
                               +  '</tr>'
                               +  '<tr>'
                               +    '<td><span class="txtColorRed">*</span>支行名称：</td>'
                               +    '<td><label for="" class="hide">支行名称：</label><span class="txt"><input type="text" class="inputText verify inputRequired wordChk" value="{payeeBankName}" /></span></td>'
                               +  '</tr>'
                               +  '<tr class="gray">'
                               +    '<td>备注信息：</td>'
                               +    '<td class="txt"><textarea cols="60" rows="7">{remark}</textarea></td>'
                               +  '</tr>'
                               +'</table>'
                               + '<span class="txt"><input type="hidden" value="{companyId}" /></span>'
                               + '<span class="txt"><input type="hidden" value="{identity_Id}" /></span>'
                }
            });
            return obj;
        },
        shoplistedit: function () {
            var that = this;
            var obj = {}, hostObj = this.get('host'), arg = {};
            arg.dataStr = 'm=syb_before_add_update_shop&shopId=' + hostObj.queryString.a;
            hostObj.ioRequest(arg, function (data) {
                if (data.list[0].status == 1) {
                    var tempData = Y.JSON.parse(data.list[0].info);
                    var supportPaymentYes = tempData.isSupportPayment ? 'checked = "checked"' : '';
                    var supportPaymentNo = !tempData.isSupportPayment ? 'checked = "checked"' : '';
                    var isTrueRedenvelopePaymentYes = tempData.isTrueRedenvelopePayment ? 'checked = "checked"' : '';
                    var isTrueRedenvelopePaymentNo = !tempData.isTrueRedenvelopePayment ? 'checked = "checked"' : '';
                    var isHandle1 = tempData.isHandle == 1 ? 'checked = "checked"' : '';
                    var isHandle2 = tempData.isHandle == -1 ? 'checked = "checked"' : '';
                    var isHandle3 = tempData.isHandle == -2 ? 'checked = "checked"' : '';
                    that.set("provinceID", tempData.provinceID);
                    that.set("cityID", tempData.cityID);
                    that.set("countyID", tempData.countyID);
                    that.shopLatitude = tempData.latitude;
                    that.shopLongitude = tempData.longitude;
                    that.companyID = tempData.companyID;
                    that.isHandle = tempData.isHandle;
                    that.shopStatus = tempData.shopStatus;
                    that.openTimes = tempData.openTimes;
                    that.shopRegisterTime = tempData.shopRegisterTime;
                    that.isSupportAccountsRound = tempData.isSupportAccountsRound;
                    //that.accountManager = tempData.accountManager;
                    that.employeeId = tempData.accountManager;
										that.demployeeId = tempData.areaManager;
                    var argMenu = {};
                    argMenu.dataStr = 'm=syb_shopmenu_list&companyId=' + that.companyID;
                    var menuStr = "", accountStr = "";
                    hostObj.ioRequest(argMenu, function (d) {
                        if (d.list[0].status == 1) {
                            var menuData = Y.JSON.parse(d.list[0].info);
                            for (var i = 0, len = menuData.length; i < len; i++) {
                                var mId = menuData[i].menuCompanyId;
                                if (tempData.menuCompanyId == 0 && i == 0) {
                                    menuStr += '<input type="radio" name="menuCompany" class="inputRadio" checked="checked" value="' + mId + '" /> ' + menuData[i].menuName + ' ';
                                    continue;
                                }
                                if (mId == tempData.menuCompanyId) {
                                    var menuCheckedStr = 'checked = "checked"';
                                } else {
                                    var menuCheckedStr = '';
                                }
                                menuStr += '<input type="radio" name="menuCompany" class="inputRadio" ' + menuCheckedStr + ' value="' + mId + '" /> ' + menuData[i].menuName + ' ';
                            }
                            if (menuStr.length === 0) {
                                menuStr += '<input type="radio" name="menuCompany" class="inputRadio" checked = "checked" value="0" /> 暂无菜谱';
                            }
                        };
                    });

                    /*var argAccount = {};
                    argAccount.dataStr = 'm=get_shop_bankaccount_list&companyId=' + that.companyID;
                    hostObj.ioRequest(argAccount, function (d) {
                        if (d.list[0].status == 1) {
                            var accountData = Y.JSON.parse(d.list[0].info);
                            for (var i = 0, len = accountData.length; i < len; i++) {
                                var mId = accountData[i].bankAccount, accountCheckedStr;
                                if (tempData.bankAccount == 0 && i == 0) {
                                    accountStr += '<input type="radio" name="accountCompany" class="inputRadio" checked="checked" value="' + mId + '" /> ' + accountData[0].bankAccountDesc + ' ';
                                    continue;
                                }
                                if (mId == tempData.bankAccount) {
                                    accountCheckedStr = 'checked = "checked"';
                                } else {
                                    accountCheckedStr = '';
                                }
                                accountStr += '<input type="radio" name="accountCompany" class="inputRadio" ' + accountCheckedStr + ' value="' + mId + '" /> ' + accountData[i].bankAccountDesc + ' ';
                            }
                            if (accountStr.length === 0) {
                                accountStr += '<input type="radio" name="accountCompany" class="inputRadio" checked = "checked" value="0" /> 暂无银行账号';
                            }
                        };
                    });*/
                    var shopLogo = !tempData.shopLogo ? "images/nonImg3.png" : tempData.shopLogo;
                    var publicityPhotoPath = !tempData.publicityPhotoPath ? "images/nonImg4.png" : tempData.publicityPhotoPath;
                    getHadBusinessDistrict(hostObj.queryString.a);
                    obj.d = tempData;
                    obj.template = '<ul>'
                        + '	<li class="gray"><label for="">门店名称：</label><span class="txt"><input type="text" class="inputText verify inputRequired" value="{shopName}" /> <em class="txtColor">*</em></span></li>'
                        + '	<li><label for="">所属公司：</label><span class="txt">{companyName} </span></li>'
                        + '	<li class="gray uploadSprite" id="conID"><label for="">门店 logo：</label>'
                        + '    <span class="txt"><input type="text" id="picUrl" class="inputText" value="{shopLogo}" />'
                        + '        <div class="uploadBoundingBox" id="uploadBoundingBoxLogo">'
                        + '             <input type="button" class="inputBtn" id="inputFileSprite" value="浏览..." /> '
                        + '        </div>'
                        + '        <input type="submit" value="上传" class="inputBtn submitUploaderBtn" id="submitUploaderBtn" name="submit" />( 最小尺寸300*300,大小不超过300k，格式 jpg | png )'
                        + '    </span>'
                        + '  </li>'
                        + '	<li class="gray companyManageComment"><label for=""> </label><span class="txt"><img id="picLogo" class="logo" width="136px" height="136px" src="' + shopLogo + '" /></span></li>'
                        + '	<li><label for="">电　　话：</label><span class="txt"><input type="text" class="inputText" value="{shopTelephone}" /> </span></li>'
                        + '	<li class="gray"><label for="">联系人员：</label><span class="txt"><input type="text" class="inputText" value="{contactPerson}" /> </span></li>'
                        + '	<li><label for="">联系人电话：</label><span class="txt"><input type="text" class="inputText" value="{contactPhone}" /> </span></li>'
                        + '	<li class="gray"><label for="">客户经理：</label><span class="txt auto-complete-sprite"><input type="text" id="acInput" class="inputText verify inputRequired" value="{accountManagerName}" /><em class="txtColor">*</em>(输入关键字搜索选择) </span></li>'
												+ '	<li><label for="">区域经理：</label><span class="txt auto-complete-sprite"><input type="text" id="dcinput" class="inputText verify inputRequired" value="{areaManagerName}" /> <em class="txtColor">*</em>(输入关键字搜索选择) </span></li>'
                        + '	<li><label for="">新浪微博：</label><span class="txt"><input type="text" class="inputText" value="{sinaWeiboName}" /> </span></li>'
                        + '	<li class="gray"><label for="">腾讯微博：</label><span class="txt"><input type="text" class="inputText" value="{qqWeiboName}" /> </span></li>'
                        + '	<li><label for="">微信公众帐号：</label><span class="txt"><input type="text" class="inputText" value="{wechatPublicName}" /> </span></li>'
                        + '	<li class="gray"><label for="">营业执照：</label><span class="txt"><input type="text" class="inputText" value="{shopBusinessLicense}" /> </span></li>'
                        + '	<li><label for="">卫生许可证：</label><span class="txt"><input type="text" class="inputText" value="{shopHygieneLicense}" /> </span></li>'
                        + '	<li class="gray"><label for="">营业时间：</label><span class="txt"><input type="text" class="inputText" value="{openTimes}" /> </span></li>'
                        + '	<li><label for="">门店地区：</label><span class="txt widget-area-shop" id="widgetAreaShop"></span> </li>'
                        + '	<li class="gray"><label for="">详细地址：</label><span class="txt"><input type="text" id="shopAdress" class="inputText verify inputRequired address" value="{shopAddress}" /> <em class="txtColor">*</em></span><input type="button" id="addShopOnBaiduMaps" value="点击获取" /></li>'
                        + '	<li><label for="">商圈配置：</label><span class="txt widget-area-shop" id="BusinessDistrict"><select id="firstBusinessDistrict" class="firstBusinessDistrict"></select>-<select id="secondBusinessDistrict" class="secondBusinessDistrict"></select> <input id="addBusinessDistrict" name="' + hostObj.queryString.a + '" type="button" onclick=addHadBusinessDistrict(this)  class="inputBtn create-qrcode" value="添加" /></span> </li>'
                        + '	<li><label for="">已有商圈：</label><span class="txt widget-area-shop" id="haveBusinessDistrict"></span> </li>'
                        + '	<li class="gray companyManageComment shopMapContainer"><label for="">门店位置：</label><span class="txt shopMapSprite"> <div id="shopMap"></div> </span></li>'
                        + '	<li class="companyManageComment"><label for="">店铺描述：</label><span class="txt"><textarea class="area">{shopDescription}</textarea> </span></li>'
                        + '	<li class="gray"><label for="">店铺评分：</label><span class="txt"><input type="text" class="inputText" value="{shopRating}" /> </span></li>'
                        + '	<li><label for="">人均消费：</label><span class="txt"><input type="text" class="inputText" value="{acpp}" /> 元/人<em class="txtColor">*</em></span></li>'
                        + '	<li class="gray"><label for="">支持红包支付：</label><span class="txt"><input type="radio" id="isTrueRedenvelopePayment" name="redenvelopePayment" class="inputRadio" ' + isTrueRedenvelopePaymentYes + ' value="true" /> 支持  　　<input type="radio" name="redenvelopePayment" class="inputRadio" ' + isTrueRedenvelopePaymentNo + ' value="false" /> 不支持</span></li>'
                        + '	<li><label for="">支付功能：</label><span class="txt"><input type="radio" name="payment" class="inputRadio" ' + supportPaymentYes + ' value="true" /> 支持  　　<input type="radio" name="payment" class="inputRadio" ' + supportPaymentNo + ' value="false" /> 暂不支持（ 若为”暂不支持“选项，则原因说明为必填 ）</span></li>'
                        + '	<li class="gray companyManageComment"><label for="">原因说明：</label><span class="txt"><textarea class="area">{notPaymentReason}</textarea></span></li>'
                        + '	<li class="companyManageComment"><label for="">公告描述：</label><span class="txt"><textarea class="area">{orderDishDesc}</textarea> </span>(最多可以输入200个字符)</li>'
                        + '	<li class="gray uploadSprite" id="conIDBanner"><label for="">门脸图片：</label>'
                        + '    <span class="txt"><input type="text" class="inputText picBannerUrl" value="{publicityPhotoPath}" />'
                        + '        <div class="uploadBoundingBox" id="uploadBoundingBoxBanner">'
                        + '             <input type="button" class="inputBtn" id="inputFileSpriteBanner" value="浏览..." /> '
                        + '        </div>'
                        + '        <input type="submit" value="上传" class="inputBtn submitUploaderBtn" id="submitBannerBtn" name="submit" />( 最小尺寸1440*677,大小不超过500k，格式 jpg | png )'
                        + '    </span>'
                        + '  </li>'
                        + '	<li class="gray companyManageComment"><label for=""> </label><span class="txt"><img id="picBannerShop" class="logo" width="400px" height="188px" src="' + publicityPhotoPath + '" /></span></li>'
                            + '	<li style="height: auto;"><label for="">门店菜谱：</label><span class="txt">' + menuStr + '</span></li>'
                            //+ '	<li class="gray"><label for="">银行账号：</label><span class="txt">' + accountStr + '</span></li>'
                            + '	<li class="qrcodeSprite" id="bao"><label for="">二维码：</label>'
                        + '    <span class="txt">'
                            + '        <em class="item-name">易拉宝</em> '
                        + '        <input type="submit" class="inputBtn create-qrcode" value="生成" /> '
                        + '        <input type="button" value="下载" class="inputBtn download-qrcode" name="buttonBao" />( 尺寸1036*1036,小于100k，格式 png )'
                        + '    </span>'
                        + '  <iframe id="downloadIframe" name="downloadIframe" style="display:none"></iframe></li>'
                        + '	<li class="companyManageComment"><label for=""> </label><span class="txt"><img id="picBao" class="logo" width="136px" height="136px" /></span></li>'
                            + '	<li class="qrcodeSprite" id="ka"><label for=""> </label>'
                        + '    <span class="txt">'
                            + '        <em class="item-name">卡台</em> '
                        + '        <input type="submit" class="inputBtn create-qrcode" value="生成" /> '
                        + '        <input type="button" value="下载" class="inputBtn download-qrcode" name="buttonKa" />( 尺寸1036*1036,小于100k，格式 png )'
                        + '    </span>'
                        + '  </li>'
                        + '	<li class="companyManageComment"><label for=""> </label><span class="txt"><img id="picKa" class="logo" width="136px" height="136px" /></span></li>'
                        + '</ul>';
                }
            });
            return obj;
        },
        shoplistadd: function () {
            var that = this;
            var obj = {};
            Y.one('.companyManage').addClass('shoplistadd');
            that.set("provinceID", 11);
            that.set("cityID", 87);
            that.set("countyID", 844);
            that.shopLatitude = "30.185440082235";
            that.shopLongitude = "120.16201007501";
            that.companyID = 0;
            that.employeeId = 0;
            obj.template = '<ul>'
                + '	<li class="gray"><label for="">门店名称：</label><span class="txt"><input type="text" class="inputText verify inputRequired" value="" /> <em class="txtColor">*</em></span></li>'
                + '	<li><label for="">所属公司：</label><span class="txt auto-complete-sprite"><input type="text" id="companyInput" class="inputText verify inputRequired" value="" /> <em class="txtColor">*</em></span></li>'
                + '	<li class="gray"><label for="">电　　话：</label><span class="txt"><input type="text" class="inputText" value="" /> </span></li>'
                + '	<li><label for="">联系人员：</label><span class="txt"><input type="text" class="inputText" value="" /> </span></li>'
                + '	<li class="gray"><label for="">联系人电话：</label><span class="txt"><input type="text" class="inputText" value="" /> </span></li>'
                + '	<li><label for="">客户经理：</label><span class="txt auto-complete-sprite"><input type="text" id="acInput" class="inputText verify inputRequired" value="" /> <em class="txtColor">*</em>(输入关键字搜索选择)</span></li>'
								+ '	<li><label for="">区域经理：</label><span class="txt auto-complete-sprite"><input type="text" id="dcinput" class="inputText verify inputRequired" value="" /> <em class="txtColor">*</em>(输入关键字搜索选择)</span></li>'
                + '	<li class="gray"><label for="">新浪微博：</label><span class="txt"><input type="text" class="inputText" value="" /> </span></li>'
                + '	<li><label for="">腾讯微博：</label><span class="txt"><input type="text" class="inputText" value="" /> </span></li>'
                + '	<li class="gray"><label for="">微信公众帐号：</label><span class="txt"><input type="text" class="inputText" value="" /> </span></li>'
                + '	<li><label for="">营业执照：</label><span class="txt"><input type="text" class="inputText" value="" /> </span></li>'
                + '	<li class="gray"><label for="">卫生许可证：</label><span class="txt"><input type="text" class="inputText" value="" /> </span></li>'
                + '	<li><label for="">营业时间：</label><span class="txt"><input type="text" class="inputText" value="" /> </span></li>'
                + '	<li class="gray"><label for="">门店地区：</label><span class="txt widget-area-shop" id="widgetAreaShop"></span> </li>'
                + '	<li><label for="">详细地址：</label><span class="txt"><input type="text" id="shopAdress" class="inputText verify inputRequired address" value="" /> <em class="txtColor">*</em></span><input type="button" id="addShopOnBaiduMaps" value="点击获取" /></li>'
                + '	<li class="gray"><label for="">商圈配置：</label><span class="txt widget-area-shop" id="BusinessDistrict"><select class="firstBusinessDistrict" id="firstBusinessDistrict"></select>-<select class="secondBusinessDistrict" id="secondBusinessDistrict"></select></span></li>'
                //+ '	<li><label for="">已有商圈：</label><span class="txt widget-area-shop" id="haveBusinessDistrict"></span> </li>'
                + '	<li class="companyManageComment shopMapContainer"><label for="">门店位置：</label><span class="txt shopMapSprite"> <div id="shopMap"></div> </span></li>'
                + '	<li class="gray companyManageComment"><label for="">店铺描述：</label><span class="txt"><textarea class="area"></textarea> </span></li>'
                + '	<li><label for="">店铺评分：</label><span class="txt"><input type="text" class="inputText" value="" /> </span></li>'
                + '	<li class="gray"><label for="">人均消费：</label><span class="txt"><input type="text" class="inputText verify inputRequired" value="" /> 元/人<em class="txtColor">*</em></span></li>'
                + '	<li ><label for="">支持红包支付：</label><span class="txt"><input type="radio" id="isTrueRedenvelopePayment" name="redenvelopePayment" class="inputRadio" checked="checked" value="true" /> 支持  　　<input type="radio" name="redenvelopePayment" class="inputRadio" value="false" /> 不支持</span></li>'
                + '	<li class="gray"><label for="">支付功能：</label><span class="txt"><input type="radio" name="payment" class="inputRadio" checked="checked" value="true" /> 支持  　　<input type="radio" name="payment" class="inputRadio" value="false" /> 暂不支持（ 若为”暂不支持“选项，则原因说明为必填 ）</span></li>'
                + '	<li class="companyManageComment"><label for="">原因说明：</label><span class="txt"><textarea class="area"></textarea> </span></li>'
                + '	<li class="gray companyManageComment"><label for="">公告描述：</label><span class="txt"><textarea class="area"></textarea> </span>(最多可以输入200个字符)</li>'
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
			
			console.log(initMethod, data.d);
			
            hostObj.get('contentBox').set('innerHTML', Y.Lang.sub(data.template, data.d));
            this.initBind();
        },
        getAreaSelect: function (contentBox, obj) {
            var that = this;
            var hostObj = this.get('host');
            // 省市区（县）列表
            var getAreaSelectHtml = function (obj) {
                var arg = {}, dataStr = '';
                var area = obj.area, id = obj.id;
                arg.dataStr = 'm=get_provinces_cities_counties&type=' + area + '&id=' + id;//
                hostObj.ioRequest(arg, function (data) {
                    if (data.list[0].status == 1) {
                        var areaData = Y.JSON.parse(data.list[0].info), selectedStr = '';
                        var provinceID = that.get("provinceID"), cityID = that.get("cityID"); // countyID = that.get("countyID");
                        dataStr += '<select class=' + area + '>';
                        for (var j = 0, len = areaData.length; j < len; j++) {
                            var itemId = areaData[j][area + "Id"];
                            switch (area) {
                                case 'province':
                                    if (itemId == provinceID) {
                                        selectedStr = 'selected="selected"';
                                    } else {
                                        selectedStr = '';
                                    }
                                    break;
                                case 'city':
                                    if (itemId == cityID) {
                                        selectedStr = 'selected="selected"';
                                    } else {
                                        selectedStr = '';
                                    }
                                    break;
                                default:// 无 countyID ，因为没有区（县）的级联
                                    selectedStr = '';
                                    break;
                            }

                            dataStr += '<option ' + selectedStr + ' class="opt" value="' + itemId + '">' + areaData[j][area + "Name"] + '</option>';
                        }
                        dataStr += "</select> -&nbsp;";// -7
                    }
                });
                return dataStr;
            };

            var areaShop = Y.one(contentBox), dataStr = '';
            switch (obj.length) {
                case 1:
                    dataStr += getAreaSelectHtml(obj[0]);
                    break;
                case 2:
                    dataStr += getAreaSelectHtml(obj[0]);
                    dataStr += getAreaSelectHtml(obj[1]);
                    break;
                case 3:
                    dataStr += getAreaSelectHtml(obj[0]);
                    dataStr += getAreaSelectHtml(obj[1]);
                    dataStr += getAreaSelectHtml(obj[2]);
                    break;
                default:
                    break;
            }
            areaShop.empty(true);
            areaShop.append(dataStr.slice(0, -7));
        },
        //加载商圈信息
        getBusinessDistrict: function (contentBox, contentBoxId, _contentBox, cityId) {
            var that = this;
            var hostObj = this.get('host');
            var argFirstBusinessDistrict = {};
            argFirstBusinessDistrict.dataStr = 'm=get_first_businessdistrict&cityId=' + cityId;
            var firstBusinessDistrictStr = "";
            hostObj.ioRequest(argFirstBusinessDistrict, function (d) {
                if (d.list[0].status == 1) {
                    var businessDistrict = Y.JSON.parse(d.list[0].info);
                    for (var i = 0, len = businessDistrict.length; i < len; i++) {
                        var tagId = businessDistrict[i].TagId;
                        var name = businessDistrict[i].Name;
                        firstBusinessDistrictStr += '<option value=' + tagId + '>' + name + '</option>';
                    }
                };
            });
            var businessDistrict = Y.one(contentBox);
            businessDistrict.empty(true);
            businessDistrict.append(firstBusinessDistrictStr.slice(0, -7));

            var argSecondBusinessDistrict = {};
            var obj = document.getElementById(contentBoxId);
            var index = obj.selectedIndex; // 选中索引
            var tagId = obj.options[index].value; // 选中值
            argSecondBusinessDistrict.dataStr = 'm=get_second_businessdistrict&tagId=' + tagId;
            var secondBusinessDistrictStr = "";
            hostObj.ioRequest(argSecondBusinessDistrict, function (d) {
                if (d.list[0].status == 1) {
                    var businessDistrict = Y.JSON.parse(d.list[0].info);
                    for (var i = 0, len = businessDistrict.length; i < len; i++) {
                        var tagId = businessDistrict[i].TagId;
                        var name = businessDistrict[i].Name;
                        secondBusinessDistrictStr += '<option value=' + tagId + '>' + name + '</option>';
                    }
                };
            });
            var _businessDistrict = Y.one(_contentBox);
            _businessDistrict.empty(true);
            _businessDistrict.append(secondBusinessDistrictStr.slice(0, -7));
        }
    },
    {
        NS: 'CompanyManagePlugin',
        ATTRS: {
            btn: {
                value: ''
            },
            provinceID: {
                value: ''
            },
            cityID: {
                value: ''
            },
            countyID: {
                value: ''
            }
        }
    });
}, '1.0', { requires: ['base-build', 'plugin', 'uploader', 'autocomplete', 'autocomplete-filters', 'autocomplete-highlighters', 'jsonp', 'jsonp-url'] });

YUI.add('UIStaff', function (Y) {
    Y.CompanyManage = Y.Base.create('CompanyManage', Y.Widget, [], {
        destructor: function () {
            this.get('containerBox').remove(true);
        },
        dataSuccess: function () { },
        prevPage: function (pType) {
            menu.prevPage(pType + ".aspx", pType);
        },
        companylistedit: function (dataArray) {
            dataArray[9] = dataArray[9].toString().replace(/(^\s*)|(\s*$)/g, "") == "" ? 0 : dataArray[9];
            var addData = '{"companyID":' + dataArray[0] + ',"companyAddress":"' + dataArray[10] + '","companyLogo":" ","companyName":"' + dataArray[2] + '","companyStatus":1,"companyTelePhone":"' + dataArray[3] + '","contactPerson":"' + dataArray[4] + '","contactPhone":"' + dataArray[5] + '","companyDescription":"' + dataArray[11] + '","ownedCompany":"' + dataArray[1] + '","companyImagePath":" ","sinaWeiboName":"' + dataArray[6] + '","qqWeiboName":"' + dataArray[7] + '","wechatPublicName":"' + dataArray[8] + '","acpp":"' + dataArray[9] + '"}';
            var arg = {};
            arg.dataStr = 'm=syb_add_update_company&addcompanyjson=' + addData;
            arg.dataStr = dataEncode(arg.dataStr);
            return arg;
        },
        companylistadd: function (companyDataArray) {
            return this.companylistedit(companyDataArray);
        },
        companylistcommission: function (dataArray) {
					
            var addData = '{"companyId":' + dataArray[0] + ',"companyName":"' + dataArray[1] + '","freeRefundHour":"' + dataArray[4] + '","viewallocCommissionType":"' + dataArray[2] + '","viewallocCommissionValue":"' + dataArray[3] + '"}';
            var arg = {};
            arg.dataStr = 'm=syb_add_update_commissioninfo&commissionJson=' + addData;
            return arg;
        },
        companyaccountlistedit: function (dataArray) {		
            dataArray.push(this.queryString.b);
            var addData = '{"companyId":' + dataArray[0] + ',"identity_Id":' + dataArray[6] + ',"accountNum":"' + dataArray[1] + '","accountName":"' + dataArray[2] + '","bankName":"' + dataArray[3] + '","payeeBankName":"' + dataArray[4] + '","remark":"' + dataArray[5] + '","status":1}';
            var arg = {};
            arg.dataStr = 'm=syb_add_update_bankaccount&accountJson=' + addData;
            return arg;
        },
        companyaccountlistadd: function (accountDataArray) {
            return this.companyaccountlistedit(accountDataArray);
        },
        companymenulistedit: function (dataArray) {
            dataArray.push(this.queryString.mcid);
            var addData = '{"companyId":' + dataArray[0] + ',"companyName":"' + dataArray[1] + '","langListInfo":[{"langId":1,"langName":"简体中文"}],"menuCompanyId":' + dataArray[4] + ',"menuInfo":{"menuDesc":"' + dataArray[3] + '","menuId":0,"menuName":"' + dataArray[2] + '"}}';
            var arg = {};
            arg.dataStr = 'm=syb_add_update_menu&menujson=' + addData;
            return arg;
        },
        companymenulistadd: function (menuDataArray) {
            return this.companymenulistedit(menuDataArray);
        },
        shopsundrylistedit: function (dataArray) {
            dataArray.push(this.queryString.status);
            dataArray.push(this.queryString.syid);
            var price = Number(dataArray[5]), sundryChargeMode = dataArray[4];
            price = price ? price : 0;
            if (sundryChargeMode == 2) {
                price = Math.round(price) / 100;
            }
            var addData = '{"description":"' + dataArray[7] + '","price":"' + price + '","required":' + dataArray[6] + ',"shopId":' + dataArray[0] + ',"status":' + dataArray[8] + ',"sundryChargeMode":' + sundryChargeMode + ',"sundryId":' + dataArray[9] + ',"sundryName":"' + dataArray[2] + '","sundryStandard":"' + dataArray[3] + '"}';
            var arg = {};
            arg.dataStr = 'm=syb_add_update_sundry&sundryjson=' + addData;
            return arg;
        },
        shopsundrylistadd: function (sundryDataArray) {
            return this.shopsundrylistedit(sundryDataArray);
        },
        shopdiscountlistedit: function (dataArray) {
            dataArray.push(this.queryString.vid);
            var discountRate = (dataArray[3] / 100);
            var addData = '{"id":"' + dataArray[5] + '","platformVipId":"' + dataArray[4] + '","name":"' + dataArray[2] + '","shopId":' + dataArray[0] + ',"discount":' + discountRate + ',"status":"1"}';
            var arg = {};
            arg.dataStr = 'm=syb_add_update_shopvip&shopVipJson=' + addData;
            return arg;
        },
        shopdiscountlistadd: function (dataArray) {
            return this.shopdiscountlistedit(dataArray);
        },
        shophandle: function (dataArray) {
            var arg = {};
            arg.dataStr = 'm=syb_shop_ishandle&handleStatus=' + dataArray[dataArray.length - 1] + '&shopId=' + dataArray[0];
            return arg;
        },
        financehandle: function (dataArray) {  /*2015.6.2添加*/
            var arg = {};
            arg.dataStr = 'm=syb_shop_ishandle&handleStatus=' + dataArray[dataArray.length - 1] + '&shopId=' + dataArray[0];
            return arg;
        },
        shoplistedit: function (dataArray) {
            var shopRate = Number(dataArray[25]);
            var acpp = Number(dataArray[26]);
            shopRate = shopRate ? shopRate : 0;
            acpp = acpp ? acpp : 0;
            var plugObj = this.CompanyManagePlugin;
            var isTrueRedenvelopePayment = document.getElementById("isTrueRedenvelopePayment").checked == true;
            dataArray[34] = dataArray[34].toString().replace(/(^\s*)|(\s*$)/g, "") == "" ? 0 : dataArray[34];//银行帐号初始化为0保存
            var addData = '{"shopID":"' + dataArray[0]
                + '","shopName":"' + dataArray[1]
                + '","companyID":"' + plugObj.companyID
                + '","shopLogo":"' + dataArray[3]
                + '","shopTelephone":"' + dataArray[5]
                + '","contactPerson":"' + dataArray[6]
                + '","contactPhone":"' + dataArray[7]
                + '","accountManager":"' + (plugObj.employeeId || 0)
								+	'","areaManager":"' + (plugObj.demployeeId || 0)
                + '","sinaWeiboName":"' + dataArray[10]
                + '","qqWeiboName":"' + dataArray[11]
                + '","wechatPublicName":"' + dataArray[12]
                + '","shopBusinessLicense":"' + dataArray[13]
                + '","shopHygieneLicense":"' + dataArray[14]
                + '","openTimes":"' + dataArray[15]
                + '","provinceID":' + dataArray[16]
                + ',"cityID":' + dataArray[17]
                + ',"countyID":' + dataArray[18]
                + ',"shopAddress":"' + dataArray[19]
                + '","shopRating":"' + shopRate
                + '","shopDescription":"' + dataArray[24]
                + '","publicityPhotoPath":"","acpp":"' + acpp
                + '","isSupportPayment":"' + dataArray[28]
                + '","notPaymentReason":"' + dataArray[29]
                + '","orderDishDesc":"' + dataArray[30]
                + '","shopImagePath":"' + dataArray[33]
                + '","menuCompanyId":"' + dataArray[33]
                + '","bankAccount":"' + dataArray[34]
                + '","shopStatus":' + plugObj.shopStatus
                + ',"isHandle":' + plugObj.isHandle
                + ',"latitude":"' + plugObj.shopLatitude
                + '","longitude":' + plugObj.shopLongitude
                + ',"shopRegisterTime":"' + plugObj.shopRegisterTime
                + '","isSupportAccountsRound":' + plugObj.isSupportAccountsRound
                + ',"tagId":0,"isTrueRedenvelopePayment":' + isTrueRedenvelopePayment + '}';
            var arg = {};
            arg.dataStr = 'm=syb_add_update_shop&shopjson=' + addData;
            arg.dataStr = dataEncode(arg.dataStr);
            return arg;
        },
        shoplistadd: function (dataArray) {
            var obj = document.getElementById("secondBusinessDistrict");
            var index = obj.selectedIndex; // 选中索引
            var tagId = obj.options[index].value; // 选中值
            var isTrueRedenvelopePayment = document.getElementById("isTrueRedenvelopePayment").checked == true;
            var plugObj = this.CompanyManagePlugin;
            var shopRate = Number(dataArray[22]);
            shopRate = shopRate ? shopRate : 0;
            dataArray[32] = 0;
            //dataArray[29] = dataArray[29].toString().replace(/(^\s*)|(\s*$)/g, "") == "" ? 0 : dataArray[29];//银行帐号初始化为0保存
            var addData = '{"shopID":' + dataArray[0]
                + ',"shopName":"' + dataArray[1]
                + '","companyID":"' + plugObj.companyID
                + '","shopTelephone":"' + dataArray[3]
                + '","contactPerson":"' + dataArray[4]
                + '","contactPhone":"' + dataArray[5]
                + '","accountManager":"' + (plugObj.employeeId || 0)
								+	'","areaManager":"' + (plugObj.demployeeId || 0)
                + '","sinaWeiboName":"' + dataArray[8]
                + '","qqWeiboName":"' + dataArray[9]
                + '","wechatPublicName":"' + dataArray[10]
                + '","shopLogo":" ","shopBusinessLicense":"' + dataArray[11]
                + '","shopHygieneLicense":"' + dataArray[12]
                + '","openTimes":"' + dataArray[13]
                + '","provinceID":' + dataArray[14]
                + ',"cityID":' + dataArray[15]
                + ',"countyID":' + dataArray[16]
                + ',"shopAddress":"' + dataArray[17]
                + '","shopStatus":1,"isHandle":"-1","shopImagePath":" ","shopDescription":"' + dataArray[21]
                + '","shopRegisterTime":"/Date(-28800000+0800)/","isSupportAccountsRound":false,"shopRating":"' + shopRate
                + '","publicityPhotoPath":"","acpp":"' + dataArray[23]
                + '","isSupportPayment":"' + dataArray[24]
                + '","notPaymentReason":"' + dataArray[26]
                + '","orderDishDesc":"' + dataArray[27]
                + '","latitude":"' + plugObj.shopLatitude
                + '","longitude":' + plugObj.shopLongitude
                + ',"menuCompanyId":0,"tagId":"' + tagId
                + '","isTrueRedenvelopePayment":' + isTrueRedenvelopePayment
                + '}';
            var arg = {};
            arg.dataStr = 'm=syb_add_update_shop&shopjson=' + addData;
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
        withdrawtype: function(dataArray){  //提款方式
          var withdrawtype = '';
          for(var i = 1; i < dataArray.length; i++){
            if( i == 1 ){
              withdrawtype = dataArray[i];
            }else{
              withdrawtype = withdrawtype + "," + dataArray[i];
            }
          }
          var arg = {};
          arg.dataStr = 'm=syb_modify_withdrawtype&shopID='+dataArray[0]+'&withdrawtype=' + withdrawtype;
          arg.dataStr = dataEncode(arg.dataStr);
          return arg;
        },
        commision: function(dataArray){  //佣金
          var arg = {};
          arg.dataStr = 'm=syb_modify_viewalloccommissionvalue&shopID='+dataArray[0]+'&viewalloccommissionvalue=' + dataArray[1];
          arg.dataStr = dataEncode(arg.dataStr);
          return arg;
        },
        shopvipdiscount: function(dataArray){  //折扣
          var arg = {};
          arg.dataStr = 'm=syb_modify_discount&shopID='+dataArray[0]+'&discount=' + dataArray[1];
          arg.dataStr = dataEncode(arg.dataStr);
          return arg;
        },
        companyaccount: function(dataArray){
          var accountInfo = '';
          accountInfo = '{"accountNum":"' + dataArray[1] + '", "accountName":"' + dataArray[2] + '", "bankName":"' + dataArray[3] + '", "payeeBankName":"' + dataArray[4] + '", "remark":"' + dataArray[5] + '", "companyId":"' + dataArray[6] + '", "identity_Id":"' + dataArray[7] + '"}'
          var arg = {};
          arg.dataStr = 'm=syb_modify_account&shopID='+dataArray[0]+'&accountJson=' + accountInfo;
          arg.dataStr = dataEncode(arg.dataStr);
          return arg;
        },
        staffSubmit: function (e) {
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
                VA.Singleton.popup.panel.set('headerContent', '提示信息');
                VA.Singleton.popup.panel.set('bodyContent', '您确定要放弃修改吗？');
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
            Y.all('.companySubmit .btn').on('click', this.staffSubmit, this);
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
var xmlHttpRequest = null;
function getHadBusinessDistrict(shopId) {
    if (window.ActiveXObject) {
        xmlHttpRequest = new ActiveXObject("Microsoft.XMLHTTP");
    }
    else if (window.XMLHttpRequest) {
        xmlHttpRequest = new XMLHttpRequest();
    }
    if (null != xmlHttpRequest) {
        xmlHttpRequest.open("POST", "ajax/doSybSystem.ashx?m=get_had_businessdistrict&shopid=" + shopId, true);
        xmlHttpRequest.onreadystatechange = function () {
            var str = "";
            if (xmlHttpRequest.readyState == 4) {
                if (xmlHttpRequest.status == 200) {
                    var content = xmlHttpRequest.responseText;
                    var json = eval("(" + content + ")");
                    if (json.list[0].status == 1) {
                        var businessDistrict = eval("(" + json.list[0].info + ")");
                        for (var i = 0, len = businessDistrict.length; i < len; i++) {
                            str += businessDistrict[i].Name
                                + '&nbsp;<input id="' + businessDistrict[i].TagId + '" value="'
                                + shopId + '" onclick=removeHadBusinessDistrict(this) type="checkbox" checked="checked"  />';
                        }
                    };
                }
            }
            var cbStr = document.getElementById("haveBusinessDistrict");
            cbStr.innerHTML = str == "" ? "暂无信息" : str;
        };
        xmlHttpRequest.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        xmlHttpRequest.send("");
    }
}
function removeHadBusinessDistrict(cb) {
    var id = cb.id;
    var shopId = cb.value;
    if (window.ActiveXObject) {
        xmlHttpRequest = new ActiveXObject("Microsoft.XMLHTTP");
    }
    else if (window.XMLHttpRequest) {
        xmlHttpRequest = new XMLHttpRequest();
    }
    if (null != xmlHttpRequest) {
        xmlHttpRequest.open("POST", "ajax/doSybSystem.ashx?m=remove_businessdistrict&shopid=" + shopId + "&tagId=" + id, true);
        xmlHttpRequest.onreadystatechange = function () {
            var str = "";
            if (xmlHttpRequest.readyState == 4) {
                if (xmlHttpRequest.status == 200) {
                    var content = xmlHttpRequest.responseText;
                    var json = eval("(" + content + ")");
                    if (json.list[0].status == 1) {
                        getHadBusinessDistrict(shopId);//删除成功
                        return;
                    };
                }
            }
        };
        xmlHttpRequest.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        xmlHttpRequest.send("");
    }
}
function addHadBusinessDistrict(btn) {
    var shopId = btn.name;
    var obj = document.getElementById("secondBusinessDistrict");
    var index = obj.selectedIndex; // 选中索引
    var tagId = obj.options[index].value; // 选中值
    if (window.ActiveXObject) {
        xmlHttpRequest = new ActiveXObject("Microsoft.XMLHTTP");
    }
    else if (window.XMLHttpRequest) {
        xmlHttpRequest = new XMLHttpRequest();
    }
    if (null != xmlHttpRequest) {
        xmlHttpRequest.open("POST", "ajax/doSybSystem.ashx?m=add_businessdistrict&shopid=" + shopId + "&tagId=" + tagId, true);
        xmlHttpRequest.onreadystatechange = function () {
            var str = "";
            if (xmlHttpRequest.readyState == 4) {
                if (xmlHttpRequest.status == 200) {
                    var content = xmlHttpRequest.responseText;
                    var json = eval("(" + content + ")");
                    if (json.list[0].status == 1) {
                        getHadBusinessDistrict(shopId);
                        return;
                    };
                }
            }
        };
        xmlHttpRequest.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        xmlHttpRequest.send("");
    }
}
