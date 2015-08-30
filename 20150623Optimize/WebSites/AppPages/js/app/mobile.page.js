(function () {
    var companyAdd = window['companyAdd'] = function () {
        var self = this;
        var companyList;
        var companyBannerList;
        //            var latitude = $.cookie("latitude"); //纬度
        //            var longitude = $.cookie("longitude"); //经度
        var clientCookie = $.cookie("clientCookie");
        var clientUuid = $.cookie("clientUuid");
        var cityId = 87;
        // var cityId = $.cookie("selectval");

        var switch_to; //翻转到屏幕序号
        var active; //当前屏幕序号
        var t;

        this.getActive = function () {
            $('.promptumenu_nav a').each(function (i) {
                if ($(this).hasClass('active')) {
                    active = i;
                }
            });
        }
        this.switchWindow = function () {
            var bodyWidth = document.body.clientWidth;
            var total_num = $('.example_1 ul').width() / bodyWidth;
            self.getActive();
            if (active < (total_num - 1)) {
                switch_to = (active + 1);
            } else {
                switch_to = 0;
            }
            $(".promptumenu_nav a:eq(" + switch_to + ")").click();
        }
        this.CompanyAdScrolling = function () {
            $('.example_1 ul').promptumenu({
                'width': "auto",
                'height': 115,
                'columns': 1,
                'rows': 1,
                'pages': true,
                'duration': 500
            });
            t = setInterval(self.switchWindow, 3000);
            $('.example_1 ul').mousedown(function () { clearInterval(t); getActive(); });
            $('.example_1 ul').mouseup(function () { t = setInterval(self.switchWindow, 3000); });
            $(".promptumenu_nav a").bind('click', function () { getActive(); });
        }

        this.ShowRestaurant = function (companyid) {
            $("#" + companyid.id).attr("href", "RestaurantInfo.aspx?companyId=" + companyid.id + "");
        }

        var initialize = function () {
            if (clientCookie == "undefind" || clientCookie == undefined || clientUuid == "undefind" || clientUuid == undefined) {//客户端没有cookie
                //alert("登录失败,请重试！");
            }
            else {
                $.ajax({
                    type: "Post",
                    url: "CompanyInfo.aspx/ClientQueryCompanyByCityId",
                    data: "{cityId:'" + cityId + "',cookie:'" + clientCookie + "',uuid:'" + clientUuid + "',companyId:'" + 0 + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data.d != "" && data.d != null) {
                            var screenWidth = screen.width;
                            companyBannerList = eval("(" + data.d + ")").companyBannerList;
                            var companyBannerListAd = "";
                            for (var i = 0; i < companyBannerList.length; i++) {
                                companyBannerListAd += "<li style='background-color:White;'>";
                                companyBannerListAd += "<a href=\"RestaurantInfo.aspx?companyId=" + companyBannerList[i].companyId + "\" target='_self'>";
                                companyBannerListAd += "<img src=\"" + companyBannerList[i].bannerImageUrlString + "\" style='height:115px; width: 275px' alt=''>";
                                companyBannerListAd += "</a>";
                                companyBannerListAd += "</li>";
                            }
                            $("#companyBannerListImg").append(companyBannerListAd);
                            //$("#companyBannerListImg").lazyload({ threshold: 200 });
                            //$("#companyBannerListImg").listview('refresh');
                            self.CompanyAdScrolling();
                            //公司列表
                            companyList = eval("(" + data.d + ")").companyList;

                            //                            //将距离值存储在属性值中，排序
                            //                            //重新组合数组，没有写到数据库
                            //                            //2013-7-17 wangcheng
                            //                            if (latitude != "undefind" && latitude != undefined && longitude != "undefind" && longitude != undefined) {//定位成功，显示下列信息
                            //                                for (var h = 0; h < companyList.length; h++) {
                            //                                    var minKm = 0;
                            //                                    var status = 1;
                            //                                    for (var n = 0; n < companyList[h].restaurantList.length; n++) {
                            //                                        var restaurantlatitude = companyList[h].restaurantList[n].latitude; //纬度
                            //                                        var restaurantlongitude = companyList[h].restaurantList[n].longitude; //经度
                            //                                        var s = GetDistance(restaurantlatitude, restaurantlongitude, latitude, longitude); //获得的是当前公司的当前门店距离当前位置的距离
                            //                                        if (status == 1) {
                            //                                            if (s > minKm) {
                            //                                                minKm = s;
                            //                                                status = 0;
                            //                                            }
                            //                                        }
                            //                                        if (minKm > s) {
                            //                                            minKm = s;
                            //                                        }

                            //                                    }
                            //                                    companyList[h].description = minKm;
                            //                                }
                            //                                companyList.sort(compare); //返回的是一个数组 
                            //                            }


                            var str_company = "";
                            for (var j = 0; j < companyList.length; j++) {
                                if (screenWidth == "undefind" || screenWidth == undefined) {
                                    str_company += "<li  style=\"background-image:url('uploads/ckmendianCELL300.png');width:100%;background-position:center center;background-repeat:no-repeat;border:0px;\">";
                                } else if (screenWidth <= 480) {
                                    str_company += "<li style=\"background-image:url('uploads/ckmendianCELL300.png');width:100%;background-position:center center;background-repeat:no-repeat;border:0px;\">";
                                }
                                else {
                                    str_company += "<li style=\"background-image:url('uploads/ckmendianCELL340.png');width:100%;background-position:center center;background-repeat:no-repeat;border:0px;\">";
                                }
                                //str_company += "<a href=\"RestaurantInfo.aspx?companyId=" + companyList[j].companyId + "\" target='_self'>";//会有底色变化
                                str_company += "<a id='" + companyList[j].companyId + "' target='_self'>"; //去掉后代码

                                str_company += "<img src=\"" + companyList[j].logoUrlString + "\" style='width:68px;height:68px; margin-top:3px; margin-left:2%;'>";
                                str_company += "<h2 style='color:red;width:140px;overflow: hidden;text-overflow: ellipsis;white-space: nowrap;margin-left:0.5%;margin-top:0;'>" + companyList[j].name + "</h2>";
                                str_company += "<p style='margin-left:0.5%;'>" + companyList[j].restaurantList.length + "家店" + "&nbsp;&nbsp;&nbsp;人均" + companyList[j].acpp + "元</p>";
                                //                                //2013-7-17 wangcheng
                                //                                str_company += "<p style='margin-left:0.5%;'>" + "最近门店" + "&nbsp;&nbsp;&nbsp;" + companyList[j].description.toString() + "公里" + "</p>";
                                str_company += "</a>";
                                str_company += "</li>";
                            }
                            $("#listview_company").append(str_company);
                            $("#listview_company img").lazyload({ threshold: 200 });
                            $("#listview_company").listview('refresh');
                            $("#listview_company").find("a").bind('click', function () {
                                self.ShowRestaurant(this);
                            });
                        }
                        else {
                            alert("获取公司信息失败");
                        }
                    },
                    error: function (XmlHttpRequest, textStatus, errorThrown) {
                        alert(XmlHttpRequest.responseText);
                    }
                });
            }
        }
        initialize();
    };

    var Page = function () {
        this.waiterName = null;
        this.waiterPhone = null;
        this.qs;
        this.msg = "";
    };
    Page.prototype.tousu = function () {
        var qs = this.getQueryStringArgs();
        var preOrderId = qs.p;// 10122
        var that = this;
        that.init = function () {
            var configObj = {};
            configObj.urlMethod = 'complaint.aspx/PageInfo';
            configObj.data = '{ "preOrderId":"' + preOrderId + '" }';

            that.webMethodHandler(configObj, function (data) {
                var d = data;
                if (d != "error") {
                    // 基本信息
                    if (d.waiterName != '') {
                        $("#waiter").text(d.waiterName);
                    }
                    if (d.waiterPhone != '') {
                        var tel = d.waiterPhone,
                       telStr = tel.slice(0, 3);
                        telStr += '****';
                        telStr += tel.slice(8);
                        $("#tel").text('(' + telStr + ')');
                    }

                    // 投诉条款
                    //var dishStr = "";
                    //for (var i = 0, len = d.dishName.length; i < len; i++) {
                    //    dishStr += '<p>' + d.dishName[i] + '</p>';
                    //}
                    //$("#itemCookbook").append(dishStr);
                }

                $(".item p").on("tap", function () {
                    if ($(this).hasClass("selected")) {
                        $(this).removeClass("selected");
                    } else {
                        $(this).addClass("selected");
                    }
                });
            });
        };
        that.save = function () {

            var saveHandler = function () {
                var msg = [],
					msgStr = "";
                var items = $("#itemWaiter p");
                var index = 1;
                for (var i = 0, len = items.length; i < len; i++) {
                    var item = $(items[i]);
                    if (item.hasClass("selected")) {
                        if (index == 1) {
                            msg.push("投诉服务员：" + index + "." + item.text());
                        } else {
                            msg.push(index + "." + item.text());
                        }
                        index++;
                    }
                };

                items = $("#itemCookbook p");
                index = 1;
                for (var i = 0, len = items.length; i < len; i++) {
                    var item = $(items[i]);
                    if (item.hasClass("selected")) {
                        if (index == 1) {
                            msg.push("  投诉菜品质量：" + index + "." + item.text());
                        } else {
                            msg.push(index + "." + item.text());
                        }
                        index++;
                    }
                };

                var areaVal = $("#itemComment .area").val();
                if (areaVal) {
                    msg.push("  其他意见：" + areaVal);
                }

                msgStr = msg.join(",");
                // 
                if (msgStr == "") {
                    $("#msg").text("投诉信息不能为空!");
                    //setTimeout(function () {
                    //    $("#layout").popup("close");
                    //}, 1500);
                    $("#closeMsg").on('click', function () {
                        $("#layout").popup("close");
                    });
                    return;
                }

                var configObj = {};
                configObj.urlMethod = 'complaint.aspx/Save';
                configObj.data = '{ "preOrderId":"' + preOrderId + '","msg":"' + msgStr + '" }';
                that.webMethodHandler(configObj, function (data) {
                    switch (data) {
                        case 1:
                            $("#msg").text("投诉成功!");
                            break;
                        case -1:
                            $("#msg").text("投诉失败!");
                            break;
                        case -2:
                            $("#msg").text("投诉信息不能为空!");
                            break;
                        case -3:
                            $("#msg").text("亲，您已投诉过啦~");
                            break;
                        case -4:
                            $("#msg").text("亲，入座后才能投诉的哦~");
                            break;
                        case -5:
                            $("#msg").text("未找到当前点单!");
                            break;
                        default:
                            $("#msg").text("投诉失败!");
                            break;
                    }
                    //setTimeout(function(){
                    //	$("#layout").popup( "close" );
                    //	$(".item p").removeClass("selected");
                    //	$(".comment .area").val("");
                    //},1500);
                    $("#closeMsg").on('click', function () {
                        $("#layout").popup("close");
                        $(".item p").removeClass("selected");
                        $(".comment .area").val("");
                    });
                });
            };
            $("#btnSubmit").on("tap", saveHandler);

        };

        that.init();
        that.save();
    };

    Page.prototype.cookBook = function () {
        var qs = this.getQueryStringArgs();
        var cid = qs.cid,// qs.cid
		    shopCookie = qs.sc;// qs.sc
        var that = this;
        $.cookie('sc', shopCookie);
        $.cookie('cid', cid);
        var userCookie = $.cookie('userCookie');

        var userUuid = $.cookie('userUuid');
        if (!userUuid) {
            var uu = new UUID();
            userUuid = uu.id;
            $.cookie('userUuid', userUuid);
        }

        var configObj = {};
        configObj.data = { "m": "wechat_login", "cityId": cid, "shopCookie": shopCookie, "userUuid": userUuid, "userCookie": userCookie };
        that.ajaxHandler(configObj, function (data) {
            var status = data.list[0].status;
            if (status == 3) {// 
                var info = $.parseJSON(data.list[0].info);
                $.cookie('userCookie', info.cookie);
                window.location.href = "addidentity.aspx";
            } else if (status == 2) {// 绑手机号验证页面
                window.location.href = "addidentity.aspx";
            } else if (status == 1) {
                var d = $.parseJSON(data.list[0].info);

                // wechat_order_sell_out
                var configObj = {};
                configObj.data = { "m": "wechat_order_sell_out", "shopCookie": shopCookie };
                that.ajaxHandler(configObj, function (dataSellOut) {
                    var shopData = {};
                    shopData.priceRate = (d.indexList[0].userVipInfo.discount),
				    shopData.sundryInfo = d.indexList[0].sundryInfo;
                    shopData.sellOffList = $.parseJSON(dataSellOut.list[0].info);
                    // shopData.sellOffList = [92844,92823];
                    var dataURLStr = d.indexList[0].menuList[0].menuUrl;
                    var dataURL = dataURLStr.slice(0, -4);
                    dataURL += "_wechat.txt";// 
                    // var dataURL = "90_20.txt";
                    $.ajax({
                        type: 'GET',
                        url: 'http://query.yahooapis.com/v1/public/yql', // 可访问公开的数据
                        data: {
                            q: "select * from json where url='" + dataURL + "'",
                            format: 'json'
                        },
                        dataType: 'json',
                        cache: false,
                        error: function (xml) {
                            //
                        },
                        success: function (data) {
                            if (data.query.results) {
                                cookbook.init($.parseJSON(data.query.results.d), shopData);
                            }

                        }
                    });
                });



            }
        });
    };


    Page.prototype.addIdentity = function () {
        var that = this;
        var cid = $.cookie("cid"),
			shopCookie = $.cookie("sc"),
			userUuid = $.cookie('userUuid'),
			userCookie = $.cookie('userCookie');


        var codeInit = $("#validationNumber").val();
        if (codeInit.length === 5) {
            $("#btnSubmit").addClass("abled");
        }
        // phoneNumber
        $("#phoneNumber").on("keyup", function (event) {
            var mobilePhoneNumber = $("#phoneNumber").val();
            if (mobilePhoneNumber.length == 11) {
                if (!VA.mobile.validate.isMobile(mobilePhoneNumber)) {
                    $("#layout").popup("open");
                    $("#msg").text("手机号码不正确!");
                    setTimeout(function () {
                        $("#layout").popup("close");
                    }, 1500);
                    return;
                };
                $("#getValidate").addClass("abled");
            } else {
                $("#getValidate").removeClass("abled");
            }
        });
        $("#validationNumber").on("keyup", function (event) {
            var code = $("#validationNumber").val();
            if (code.length === 5) {
                if (!VA.mobile.validate.isValidationCode(code)) {
                    $("#msg-2").text("验证码不正确!");
                    setTimeout(function () {
                        $("#layout-2").popup("close");
                    }, 1500);
                    return;
                };
                $("#getValidate").removeClass("abled");
                $("#btnSubmit").addClass("abled");
            } else {
                $("#btnSubmit").removeClass("abled");
            }
        });

        var getValidateHandler = function (evt) {
            var t = null,
				c = 30,
				e = evt.currentTarget;
            var isSuccessCode = true;

            var mobilePhoneNumber = $("#phoneNumber").val();
            if (!VA.mobile.validate.isMobile(mobilePhoneNumber)) {
                $("#msg").text("手机号码不正确!");
                setTimeout(function () {
                    $("#layout").popup("close");
                }, 1500);
                return;
            };

            $(e).off();
            $("#layout").popup("disable");
            function setDelay() {
                $(e).text("获取验证码（ " + c + " 秒）");
                if (c == 0) {
                    clearTimeout(t);
                    $(e).text("重新获取验证码");
                    $("#getValidate").on("tap", getValidateHandler);
                    $("#layout").popup("enable");
                    if (!isSuccessCode) {
                        $("#layout").popup("option", "transition", "pop");
                        $("#layout").popup("open");
                        setTimeout(function () {
                            $("#layout").popup("close");
                        }, 1500);
                    }
                    return;
                }
                c -= 1;
                t = setTimeout(arguments.callee, 1000);
            };
            setDelay();

            var verificationCode = "";// 传验证码为空，则获取验证码；
            var configObj = {};
            configObj.data = { "m": "wechat_register", "cityId": cid, "shopCookie": shopCookie, "userUuid": userUuid, "userCookie": userCookie, "mobilePhoneNumber": mobilePhoneNumber, "verificationCode": "" };
            that.ajaxHandler(configObj, function (data) {
                if (data.list[0].status != 2) {
                    $("#msg").text(data.list[0].info);
                    isSuccessCode = false;
                }
            });
        };
        var validateHandler = function (evt) {
            var code = $("#validationNumber").val();
            if (!VA.mobile.validate.isValidationCode(code)) {
                $("#msg-2").text("验证码不正确!");
                setTimeout(function () {
                    $("#layout-2").popup("close");
                }, 1500);
                return;
            };
            var mobilePhoneNumber = $("#phoneNumber").val();
            if (!VA.mobile.validate.isMobile(mobilePhoneNumber)) {
                $("#msg-2").text("手机号码不正确!");
                setTimeout(function () {
                    $("#layout-2").popup("close");
                }, 1500);
                return;
            };
            var configObj = {};
            configObj.data = { "m": "wechat_register", "cityId": cid, "shopCookie": shopCookie, "userUuid": userUuid, "userCookie": userCookie, "mobilePhoneNumber": mobilePhoneNumber, "verificationCode": code };
            that.ajaxHandler(configObj, function (data) {
                if (data.list[0].status != 1) {
                    $("#msg-2").text(data.list[0].info);
                    setTimeout(function () {
                        $("#layout-2").popup("close");
                    }, 1500);
                    return;
                }

                var d = $.parseJSON(data.list[0].info);
                $.cookie('userCookie', d.cookie);
                $("#msg-2").text("验证手机号成功!");
                setTimeout(function () {
                    $("#layout-2").popup("close");
                    window.location.href = "menu.aspx?sc=" + shopCookie + "&cid=" + cid;
                }, 1500);
            });
        };
        $("#getValidate").on("tap", getValidateHandler);
        $("#btnSubmit").on("tap", validateHandler);
    };
    Page.prototype.dishList = function () {
        var qs = app.getQueryStringArgs();
        var configObj = {};

        var preOrderId = qs.pid;
        configObj.data = { "m": "wechat_order_detail", "preOrderId": preOrderId };
        app.ajaxHandler(configObj, function (data) {
            if (data.list[0].status == 1) {
                var d = $.parseJSON(data.list[0].info);
                var time = d.commonInfo[0].TableJson[0].preOrderTime;
                $("#time").text(time);
                // 点单列表
                var dishListStr = "";
                var item = d.orderInfo;
                for (var i = 0, len = item.length; i < len; i++) {
                    dishListStr += '<li>'
								+ '	<p>' + item[i].dishName + '<span class="numberSprite"><em class="number">' + item[i].unitPrice + '</em><em class="count">' + item[i].quantity + '(' + item[i].dishPriceName + ')</em> X </span></p>';
                    // if(item[i].isPrototypeOf("dishTaste")&&item[i].dishTaste){
                    if (item[i].dishTaste) {
                        if (item[i].dishTaste.tasteId != 0) {
                            dishListStr += '	<p class="txt">口味：' + item[i].dishTaste.tasteName + '</p>';
                        }
                    };
                    dishListStr += '</li>';

                };
                $("#dishList-content").html(dishListStr);
                if (qs.p3 > 0) {// app.p.priceBack;
                    $("#priceBack").text(qs.p3);
                } else {
                    $("#priceBackSprite").css("display", "none");
                }
                var priceStr = '<p class="total">到店支付：<span>' + qs.p1 + '</span></p>'
							+ '<p class="total txtColor">手机支付：<span>' + qs.p2 + '</span></p>';
                $(priceStr).insertAfter("#dishList-content");
            }
        });

        var ratioBgHeader = 200 / 640; // 0.6
        doDraw(ratioBgHeader, ".bgScale");
        function doDraw(ratio, node) {
            var bgList = $(node);
            _width = window.innerWidth;
            _height = window.innerHeight - 20;
            for (var i = 0, len = bgList.length; i < len; i++) {
                scaleSpriteImage(bgList[i], _width, _height, ratio);
            }

        }
        function scaleSpriteImage(ImgD, _width, _height, ratio) {
            var h = Math.round(_width * ratio);
            ImgD.style.height = h + "px";
        }
        window.onresize = function () {
            doDraw(ratioBgHeader, ".bgScale");
        };


    };


    Page.prototype.webMethodHandler = function (param, callback) {
        var ajaxOption = new Object();
        ajaxOption.type = 'POST';
        ajaxOption.url = param.urlMethod; 	//
        ajaxOption.data = param.data;		//
        ajaxOption.contentType = "application/json; charset=utf-8";
        ajaxOption.dataType = 'json';
        ajaxOption.success = function (data) {
            var d = $.parseJSON(data.d);
            callback(d);
        };
        ajaxOption.error = function (XmlHttpRequest, textStatus, errorThrown) {
            //
        };
        $.ajax(ajaxOption);
    };

    Page.prototype.ajaxHandler = function (param, callback) {
        var ajaxOption = new Object();
        ajaxOption.type = 'POST';
        ajaxOption.url = '../ajax/wechatOrder.ashx';
        ajaxOption.data = param.data;
        // ajaxOption.contentType = "application/json; charset=utf-8";  // XML
        // ajaxOption.dataType = 'json';                                // parse 解析出错
        ajaxOption.success = function (data) {
            var d = $.parseJSON(data);
            callback(d);
        };
        ajaxOption.error = function (XmlHttpRequest, textStatus, errorThrown) {
            //
        };
        $.ajax(ajaxOption);
    };
    Page.prototype.addURIParam = function (url, name, value) {
        url += (url.indexOf("?") == -1 ? "?" : "&");
        url += encodeURIComponent(name) + "=" + encodeURIComponent(value);
        return url;
    };
    Page.prototype.getQueryStringArgs = function () {
        var qs = (location.search.length > 0 ? location.search.substring(1) : "");
        var args = {};
        //处理查询字符串
        var items = qs.split("&"),
			item = null,
			name = null,
			value = null;
        for (var i = 0; i < items.length; i++) {
            item = items[i].split("=");
            name = decodeURIComponent(item[0]);
            value = decodeURIComponent(item[1]);
            args[name] = value;
        }
        return args;
    };

    window['app'] = new Page();
})()
