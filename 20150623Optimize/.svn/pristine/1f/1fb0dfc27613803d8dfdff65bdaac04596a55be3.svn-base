(function () {
    if ($ && $.fn.autosize) {
        $('#area').autosize({ append: "\n" });
    };
    /*
     * 
     * @param {type} param
     * @param {type} callback
     * @returns {undefined}
     */
    var pageSize = VA.mobile.widgets.getPageSize();
    function webMethodHandler(param, callback) {
        var ajaxOption = new Object();
        ajaxOption.type = 'POST';
        ajaxOption.url = param.urlMethod; 	//
        ajaxOption.data = param.data;		//
        ajaxOption.contentType = "application/json; charset=utf-8";
        ajaxOption.dataType = 'json';
        ajaxOption.success = function (data) {
            console.log(data);
            if (data.d.indexOf("}") > -1) {
                var d = $.parseJSON(data.d);
            } else {
                var d = data.d;
            }
            callback(d);
        };
        ajaxOption.error = function (XmlHttpRequest, textStatus, errorThrown) {
            //
        };
        //ajaxOption.beforeSend = function () {
        //    console.log("aaaa");
        //    $(".pageLoading").show();
        //    $(".food-diaries-show").hide();
        //},
        //ajaxOption.complete = function () {
        //    console.log("bbbb");
        //    $(".pageLoading").hide();
        //    $(".food-diaries-show").show();
        //}
        $.ajax(ajaxOption);
    };

    function getQueryStringArgs() {
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

    function bgScaleRatio(n, r) {
        r = r ? r : 1; //

        var scaleSpriteImage = function (ImgD, _width, _height, ratio) {
            var h = Math.round(_width * ratio);
            $(ImgD).css({ "height": h + "px" });
        }

        var doDraw = function (node, ratio) {
            var bgList = $(node);
            _width = window.innerWidth;
            _height = window.innerHeight - 20;
            for (var i = 0, len = bgList.length; i < len; i++) {
                scaleSpriteImage(bgList[i], _width, _height, ratio);
            }
        };

        doDraw(n, r);
        window.onresize = function () {
            doDraw(n, r);
        };
    }

    function picsListHandler(data) {
        console.log(data);
        picsListShow(data);

        var picsSort = $("#picsList").sortable({
            placeholder: "ui-state-highlight",
            appendTo: "body",
            scroll: true,
            scrollSensitivity: 10,
            scrollSpeed: 10,
            opacity: 0.8,
            // revert: true,
            // revertDuration: 25000,
            tolerance: 'hand'
        });
        picsSort.sortable({ items: "> li" });
        // picsSort.draggable( "option", "scroll", true );
        $("#picsList").on("sortactivate", function (event, ui) {
            //
        });
        $("#picsList").disableSelection();
        $("#delete").droppable({
            activeClass: "ui-state-default",
            hoverClass: "ui-state-hover",
            drop: function (event, ui) {
                ui.draggable.addClass(".ui-state-delete");
                var len = $("#picsList>li[class=ui-state-default]").length;
                if (len >= 4) {
                    ui.draggable.fadeOut();
                    var d = ui.draggable.data("key");
                    d.status = false;
                } else {
                    $("#isTips").popup("open");
                    // $("#isTips .msg").text("");
                    setTimeout(function () {
                        $("#isTips").popup("close");
                    }, 1500);
                }
            }
        });
    }

    function picsListShow(data) {
        var picsListStr = "",
            picsListItem = data.foodDiaryDishes;
        $("#picsList").empty(true);
        $.each(picsListItem, function(index, val) {
             /* iterate through array or object */
            picsListStr = '<li class="ui-state-default">'// style="border-radius: 10px;-moz-border-radius: 10px; -webkit-border-radius: 10px; "
                        + '<a href="javascript:;">'
                        + ' <img src="' + val.imagePath + '" />'
                        + '</a>'
                        + '<span class="txt">' + val.dishName + '</span>'
                        + '<div class="masker"><img width="100%" src="img/drag_masker.png" /></div>'
                        + '</li>';
                    $("#picsList").append(picsListStr);
                    $("#picsList li:last").data("key", val);
        });

        var page = $("#diaries"), pageWeb = $("#diariesWebShow");
            if (pageWeb.length === 1) {
                $("#picsList li a img").each(function (elem) {
                    this.style.height = 215 + "px";
                });// 针对ie,图片高度做限定
                return;
            }
            //var picWidth = parseInt(pageSize.pageW * 0.44);
            //var picHeight = parseInt(picWidth * 3 / 4);
            //VA.mobile.widgets.insertStyles('.pics-list .text li.ui-state-highlight {'
            //    + ' width:' + picWidth + 'px;'
            //    + ' height:' + picHeight + 'px;'
            //    + ' line-height:' + picHeight + 'px;'
            //    + ' background-color:#f8e4af;'
            //    + ' border:1px dotted #f1c655;'
            //    + '}');

/*
        var pathsArr = [];

        // for (var i = 0, len = picsListItem.length; i < len; i++) {
        //     pathsArr.push(picsListItem[i].imagePath);
        // }

        $.imgpreloader({ paths: pathsArr }).always(function ($allImages, $properImages, $brokenImages) {

            for (var i = 0, len = picsListItem.length; i < len; i++) {
                var hasBroken = false;
                if ($brokenImages) {
                    for (var j = 0, lenBroken = $brokenImages.length; j < lenBroken; j++) {
                        if (picsListItem[i].imagePath === $brokenImages[j].src) {
                            hasBroken = true;
                        }
                    }
                }

                if (!hasBroken) {
                    picsListStr = '<li class="ui-state-default">'
                        + '<a href="javascript:;">'
                        + ' <img src="' + picsListItem[i].imagePath + '" />'
                        + '</a>'
                        + '<span class="txt">' + picsListItem[i].dishName + '</span>'
                        + '<div class="masker"><img width="100%" src="img/drag_masker.png" /></div>'
                        + '</li>';
                    $("#picsList").append(picsListStr);
                    $("#picsList li:last").data("key", picsListItem[i]);
                }
            }
            //var $allPicsSprite = $("#picsList li a");
            //for(var i=0,len=$allPicsSprite.length;i<len;i++){
            //$allPicsSprite[i].appendChild($allImages[i]);
            //}
            var page = $("#diaries"), pageWeb = $("#diariesWebShow");
            if (pageWeb.length === 1) {
                $("#picsList li a img").each(function (elem) {
                    this.style.height = 215 + "px";
                });// 针对ie,图片高度做限定
                return;
            }
            var picWidth = parseInt(pageSize.pageW * 0.44);
            var picHeight = parseInt(picWidth * 3 / 4);
            VA.mobile.widgets.insertStyles('.pics-list .text li.ui-state-highlight {'
                + '	width:' + picWidth + 'px;'
                + '	height:' + picHeight + 'px;'
                + '	line-height:' + picHeight + 'px;'
                + '	background-color:#f8e4af;'
                + '	border:1px dotted #f1c655;'
                + '}');
            ***
            page = page.length>=1?page:$("#diariesShow");
            $("body").pagecontainer("change", page, {
            show:function(){
                //
            }(),
            changeHash: true,
            loadMsgDelay:0,
            // dataUrl:"/",
            reverse: true
            });
            ***
        });
        */
    }

    function shareHandler(data) {
        console.log(data);
        var shareDate = new Date(Number(data.shoppingDate.slice(6, -7)));
        var y = shareDate.getFullYear(), m = shareDate.getMonth() + 1, d = shareDate.getDate();
        m = m < 10 ? ("0" + m) : m;
        d = d < 10 ? ("0" + d) : d;
        var shareDateStr = y + "." + m + "." + d;
        var weatherStr = "&nbsp;&nbsp;" + data.weather;
        var $areaElem = $("#area");
        $("#shareDate").attr("name", data.id);
        $("#shareDate").append(shareDateStr + weatherStr);
        $("#shareShop").text(data.shopName);
        // $("#area")[0].placeholder = data.content;
        $areaElem.val(data.content).addClass("txtColor");
        $areaElem.on("focus", function (event) {
            $(this).removeClass("txtColor");
        });

        $("#areaTrigger a").on("tap", function (evt) {
            $areaElem.val("");
            var calssName = evt.currentTarget.getAttribute("class");
            if (calssName.indexOf("switch-txt") > -1) {
                var configObj = {};
                configObj.urlMethod = 'FoodDiaries.aspx/GetContent';
                configObj.data = '{ "id":"' + data.id + '" }';
                webMethodHandler(configObj, function (d) {
                    $areaElem.val(d);
                });
            }
        });
        // picsListHandler(data);
        picsListShow(data);
    }

    function shareShowHanlder(data) {
        bgScaleRatio(".food-diaries-show h2", 0.22);
        if (!data) {
            window.location.href = "redirect.aspx";
            return;
        }
        var shareDate = new Date(Number(data.shoppingDate.slice(6, -7)));
        var y = shareDate.getFullYear(), m = shareDate.getMonth() + 1, d = shareDate.getDate();
        m = m < 10 ? ("0" + m) : m;
        d = d < 10 ? ("0" + d) : d;
        var shareDateStr = y + "." + m + "." + d;
        var weatherStr = "&nbsp;&nbsp;&nbsp;&nbsp;" + data.weather;
        $("#shareDate").attr("name", data.id);
        $("#shareDate").append(shareDateStr + weatherStr);
        $("#shareShop").text(data.shopName);

        var diariesTextStr = data.content ? data.content : "";
        var usernameStr = data.name ? data.name : "吃货";
        $("#diariesText").text(diariesTextStr);
        $("#username").text(usernameStr);
        picsListShow(data);
    }

    function Diaries() { };
    Diaries.prototype.setDiaries = function () {
        var queryString = getQueryStringArgs();
        var configObj = {};
        configObj.urlMethod = 'FoodDiaries.aspx/GetJson';
        configObj.data = '{ "orderId":"' + queryString.id + '" }';
        webMethodHandler(configObj, shareHandler);
        /*
        $("#cancel").on("tap", function() {
            webMethodHandler(configObj, picsListHandler);
        });
        */
    };

    Diaries.prototype.getDiaries = function () {
        var queryString = getQueryStringArgs();
        var configObj = {};
        configObj.urlMethod = 'FoodDiariesShow.aspx/GetJson';
        configObj.data = '{ "id":"' + queryString.id + '" }';
        webMethodHandler(configObj, shareShowHanlder);
    };
    window["Diaries"] = new Diaries();
})();

function sendHandler() {
    var foodDiaryDishes = [];
    var picList = $("#picsList li"), index = 0;
    for (var len = picList.length, i = len - 1; i >= 0; i--) {
        var d = $(picList[i]).data("key");
        if (d.status) {
            d.sort = index;
            index++;
        }
        foodDiaryDishes.push(d);
    };
    var shareContent = $("#area").val();
    var sendData = {};

    sendData.foodDiaryDishes = foodDiaryDishes;
    sendData.content = shareContent;
    sendData.id = $("#shareDate").attr("name");

    return JSON.stringify(sendData);
}

function sendDataHandler() {
    window.getFoodDiaryDishes.runOnAndroidJavaScript(sendHandler());
}

function sendDataIosHandler() {
    return sendHandler();
}

function setFooderHtml(pageType, flag) {
    $.ajax({
        type: "Post",
        url: "FoodDiariesShow.aspx/GetFooderHtml",
        data: "{'pageType':'" + pageType + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (flag == "true") {
               // $(".text a").attr("href", "#");//客户端美食广场查看美食日记页面，是下载超链接失效，客户端内下载没有意义
            }
            else {
                $(".diariesComment").append(data.d)
            }
        },
        error: function (err) {
            // alert(err);
        }
    });
}

(function () {
    var devices = VA.mobile.widgets.isDevices();
    if (devices.mobile && devices.Android) {
        $(document).on("scrollstart", function () {
            window.getFoodDiaryDishes.setShareStatus(3);
        });
        $(document).on("scrollstop", function () {
            window.getFoodDiaryDishes.setShareStatus(2);
        });
    }
})();

