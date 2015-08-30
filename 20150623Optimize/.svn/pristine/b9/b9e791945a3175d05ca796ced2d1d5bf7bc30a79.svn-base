//微信相关
var appid = 'wx3f9ff3043cbcb3b7';
var lineLink = location.href;
var imgUrl = 'http://image.u-xian.com/UploadFiles/Images/TreasureChest/weixing_shared.png';
var shareTitle = '';
var descContent = '';

var objTimer; //动画定时器
var qs = getQueryStringArgs(); //获取参数
var screenScale = document.body.clientWidth / 640; // 屏幕宽度比例
if (screenScale > 1)
    screenScale = 1;
if (screenScale < 0.5)
    screenScale = 0.5;
var isShowWeiXinShareButton = false;
var isShowMask = false;

var sharedContents = ['悠先点菜送现金红包啦，分红包，抵饭钱！我已抢到了{0}元，你还不快来抢！', '我已加入美食敢吃队，拿到{0}元，还有千万现金大宝箱'];

jQuery.redenvelopee = {
    load: function () {
        var mobilePhone = "";
        if ($.cookie("mobilePhone") != null)
            mobilePhone = $.cookie("mobilePhone");

        $.ajax({
            contentType: "application/json",
            url: 'Default.aspx/PageLoad',
            type: 'POST',
            dataType: 'json',
            data: '{ "mobilePhone":"' + mobilePhone + '","id":"' + qs.id + '" }',
            async: true,
            success: ajaxSuccess,
            error: function () {
                bang();
            },
            beforeSend: function () {
                $(".loading").show();
                $(".main").hide();
            },
            complete: function () {
                $(".loading").hide();
                $(".main").show();
            }
        });
    },
    gameove: function () {
        $.ajax({
            contentType: "application/json",
            url: 'Default.aspx/ActivityOver',
            type: 'POST',
            dataType: 'json',
            data: '{ id: ' + qs.id + ' }',
            async: true,
            success: ajaxSuccess,
            error: function () {
                bang();
            },
            beforeSend: function () {
                $(".loading").show();
                $(".main").hide();
            },
            complete: function () {
                $(".loading").hide();
                $(".main").show();
            }
        });
    },
};



/*微信*/
function onBridgeReady() {
    if (isShowWeiXinShareButton == true) {
        // 发送给好友
        WeixinJSBridge.on('menu:share:appmessage', function (argv) {
            shareFriend();
        });

        // 分享到朋友圈
        WeixinJSBridge.on('menu:share:timeline', function (argv) {
            shareTimeline();
        });

        // 分享到微博
        WeixinJSBridge.on('menu:share:weibo', function (argv) {
            shareWeibo();
        });
        WeixinJSBridge.call("showOptionMenu");
    } else {
        WeixinJSBridge.call('hideOptionMenu');
    }
}
function shareFriend() {
    $(".mask button").removeClass('button-hui');
    $(".mask button").unbind('click');
    $(".mask button").click(function (event) {
        $(".mask").hide();
    });

    WeixinJSBridge.invoke('sendAppMessage', {
        "appid": appid,
        "img_url": imgUrl,
        "img_width": "640",
        "img_height": "640",
        "link": lineLink,
        "desc": descContent,
        "title": shareTitle
    }, function (res) {
        //_report('send_msg', res.err_msg);
        //alert(JSON.stringify(res));
        if (res.err_msg == ":ok") {
        }
    });
}

function shareTimeline() {
    $(".mask button").removeClass('button-hui');
    $(".mask button").unbind('click');
    $(".mask button").click(function (event) {
        $(".mask").hide();
    });


    WeixinJSBridge.invoke('shareTimeline', {

        "img_url": imgUrl,
        "img_width": "640",
        "img_height": "640",
        "link": lineLink,
        "desc": descContent,
        "title": descContent
    }, function (res) {

    });
}
function shareWeibo() {
    $(".mask button").removeClass('button-hui');
    $(".mask button").unbind('click');
    $(".mask button").click(function (event) {
        $(".mask").hide();
    });

    WeixinJSBridge.invoke('shareWeibo', {
        "content": descContent,
        "url": lineLink,
    }, function (res) {
        _report('weibo', res.err_msg);
    });
}

function weixinShare() {

    if (typeof WeixinJSBridge == "undefined") {
        if (document.addEventListener) {

            document.addEventListener('WeixinJSBridgeReady', onBridgeReady, false);
        } else if (document.attachEvent) {
            document.attachEvent('WeixinJSBridgeReady', onBridgeReady);
            document.attachEvent('onWeixinJSBridgeReady', onBridgeReady);
        }
    } else {
        onBridgeReady();
    }
}

function getJSONForAndroid() {
    window.redEnvelopeShare.getRedenvelopeJson(getJSON());
}
function getJSON() {
    var va = JSON.stringify({ 'content': descContent, 'imgUrl': imgUrl, 'shareUrl': location.href });
    return va;
}

function ajaxSuccess(msg) {
    //console.log(msg);
    if (msg.d == "") {
        bang();
    } else {
        var response = $.parseJSON(msg.d);
        switch (response.status) {
            case 0:
                treasureChestInit(response);
                break;
            case 1:
                treasureChestOver(response);
                break;
            case 2:
                gameover(response);
                break;
            case 1000:
                treasureChestOpen(response);
                break;
            case 1001:
                moneyOver(response);
                break;
            case 1002:
                strengthover();
                break;
            case 1003:
                redEnvelopeOver(response);
                break;
            case 1005:
                treasureChestOwner(response);
                break;
            case 1006:
                alert(response.context);
                break;
            default:
                bang();
                break;
        }
        weixinShare();
    }
}
//体力用完
function strengthover() {
    $("body").eq(0).addClass('server-bang');

    var box = $(".box");
    box.empty(true);
    loadImg(box, 'http://image.u-xian.com/UploadFiles/Images/TreasureChest/strength_over.png');

    $(".lock-info").hide();

    var input_info = $(".input-info");
    input_info.empty(true);

    addButton(input_info, 'btn btn-default', '你太猛了,明天再来', function () { });

    $('#redEnvelope').hide();
    hideOther();
}
//服务器崩
function bang() {
    $("body").eq(0).addClass('server-bang');

    var box = $(".box");
    box.empty(true);
    loadImg(box, 'http://image.u-xian.com/UploadFiles/Images/TreasureChest/server_bang.png');

    $(".lock-info").hide();

    var input_info = $(".input-info");
    input_info.empty(true);

    addButton(input_info, 'btn btn-default', '稍后再试', function () { });

    $('#redEnvelope').hide();
    hideOther();
}

//宝箱页面初始状态
function treasureChestInit(response) {
    if (objTimer)
        window.clearInterval(objTimer);
    var box = $(".box");
    box.empty(true);
    loadImg(box, 'http://image.u-xian.com/UploadFiles/Images/TreasureChest/box_a.png');
    objTimer = window.setInterval(animation, 200);

    //得到的钱
    var block1 = $("<span />");
    block1.addClass('box-money');
    block1.html(response.amount);
    $(".box").append(block1);
    block1.css("font-size", Number(block1.css("font-size").replace("px", "")) * screenScale);


    loadLockInfo(response.count - response.lockCount, response.lockCount);

    var input_info = $(".input-info");
    input_info.empty(true);

    input_info.append('<div class="title">悠先请你吃饭，红包直接抵扣，杭州千家餐馆可用！</div>');

    var block3 = $("<div />");
    block3.addClass('left');
    var input1 = $('<input id="mobilePhone" type="number" placeholder="请输入手机号码"/>');
    input1.addClass('form-control');
    input1.val(response.mobilePhone);
    block3.append(input1);

    var block4 = $("<div />");
    block4.addClass('right');
    var button1 = $('<button type="button"></button>');
    button1.addClass('btn btn-default');
    button1.html('分享拿钱');
    block4.append(button1);

    button1.unbind('click');
    button1.click(function (event) {
        var mobilePhone = $("#mobilePhone").val();

        var patrn = /^1[34578][0-9]{9}$/;
        if (!mobilePhone.match(patrn)) {
            alert('无效的手机号码');
        } else {
            $("#myModal #mobilePhone").html(mobilePhone);
            $("#myModal").modal({ target: ".bs-example-modal-sm" }).css({
                'margin-top': function () {
                    return ($(this).height() / 2);
                }
            });
            var issmash = false;
            $("#button_smash").unbind('click');
            $("#button_smash").click(function (event) {
                issmash = true;
                $("#myModal").modal('hide');
            });
            $("#button_cancel").unbind('click');
            $("#button_cancel").click(function (event) {
                issmash = false;
                $("#myModal").modal('hide');
            });
            $('#myModal').on('hidden.bs.modal', function () { });
            $('#myModal').on('hidden.bs.modal', function (e) {

                if (issmash == true) {
                    $.cookie("mobilePhone", $("#mobilePhone").val());
                    isShowMask = true;
                    $.ajax({
                        url: 'Default.aspx/Smash',
                        type: 'POST',
                        dataType: 'json',
                        data: '{ "mobilePhone":"' + $("#myModal #mobilePhone").html() + '","treasureChestId":"' + qs.id + '" }',
                        contentType: "application/json",
                        success: ajaxSuccess,
                    });

                }
                issmash = false;
            });


        }
    });


    input_info.append(block3);
    input_info.append(block4);




    loadRedEnvelopes(response.redEnvelopes);



    $("#suitable-shop").show();
    $("#whatisux").show();
    $("#activityRule").show();

}
var chineseNumbers = ['零', '一', '二', '三', '四', '五', '六', '七', '八', '九', '十'];
//宝箱解锁完
function treasureChestOver(response) {

    if (objTimer)
        window.clearInterval(objTimer);
    var box = $(".box");
    box.empty(true);
    loadImg(box, 'http://image.u-xian.com/UploadFiles/Images/TreasureChest/unlock_over_a.png');
    objTimer = window.setInterval(animation, 200);
    isShowWeiXinShareButton = true;

    descContent = sharedContents[GetRandomNum(0, 1)].replace('{0}', response.amount)

    //得到的钱
    var block1 = $("<span />");
    block1.addClass('unlock-money');
    block1.html(response.amount);
    $(".box").append(block1);
    block1.css("font-size", Number(block1.css("font-size").replace("px", "")) * screenScale);


    var lockInfo = $(".lock-info");
    lockInfo.empty(true);
    lockInfo.css('text-align', 'center');
    lockInfo.append(chineseNumbers[response.count] + '人战队合体成功，红包到手!');
    lockInfo.show();


    /**/
    var input_info = $(".input-info");
    input_info.empty(true);

    loadActivityFindTreasureChest(input_info, response.remainAmount, response.activityId, response.hadTreasueChest);
    addButton(input_info, 'btn btn-default button-wr', '我的红包', lookRedEnvelope);
    if (navigator.userAgent.match(/MicroMessenger/i)) {
        input_info.append('<div style="padding-bottom:10px;height:0px;float:left;width:100%;"/>');
        if (response.hadTreasueChest == 0) {
            addButton(input_info, 'btn btn-default button-wr', '下载悠先', open_or_download_app);
        }
        else {
            addButton(input_info, 'btn btn-default', '下载悠先', open_or_download_app);
        }
    }

    loadRedEnvelopes(response.redEnvelopes);
    hideOther();
    //if (isShowMask) {
    //    mask();
    //}
}

//红包抢完
function redEnvelopeOver(response) {
    if (objTimer)
        window.clearInterval(objTimer);
    var box = $(".box");
    box.empty(true);
    loadImg(box, 'http://image.u-xian.com/UploadFiles/Images/TreasureChest/box_over_a.png');
    objTimer = window.setInterval(animation, 200);


    $(".lock-info").hide();

    var input_info = $(".input-info");
    input_info.empty(true);


    var block2 = $('<div>本活动还剩 <span class="emphasize">&yen' + response.remainAmount + '</span></div>');
    block2.addClass('remark');
    block2.css({ 'padding-top': '0px', 'padding-bottom': '10px', 'font-size': '100%' });
    input_info.append(block2);

    if (response.hadTreasueChest == 0) {
        addButton(input_info, 'btn btn-default', '组队抢宝箱', function () { location.href = "FromApp.aspx?activityId=" + response.activityId; });

    }
    else {
        addButton(input_info, 'btn btn-default button-hui', '组队抢宝箱', function () { });
        input_info.append('<div class="remark"">(你已经领过宝箱,不能再领取)</div>');
    }



    loadRedEnvelopes(response.redEnvelopes);

    $("#suitable-shop").show();
    $("#whatisux").show();
    $("#activityRule").hide();
}

//箱主开箱成功
function treasureChestOwner(response) {
    if (objTimer)
        window.clearInterval(objTimer);
    var box = $(".box");
    box.empty(true);
    loadImg(box, 'http://image.u-xian.com/UploadFiles/Images/TreasureChest/boxowner_unlock_a.png');
    objTimer = window.setInterval(animation, 200);

    isShowWeiXinShareButton = true;

    descContent = sharedContents[GetRandomNum(0, 1)].replace('{0}', response.amount);

    var block1 = $("<span />");
    block1.addClass('boxowner-money');
    block1.html(response.amount);
    box.append(block1);
    block1.css("font-size", Number(block1.css("font-size").replace("px", "")) * screenScale);


    loadLockInfo(response.count - response.lockCount, response.lockCount, true);

    var input_info = $(".input-info");
    input_info.empty(true);
    loadActivityFindTreasureChest(input_info, response.remainAmount, response.activityId, response.hadTreasueChest);

    //if (!navigator.userAgent.match(/MicroMessenger/i)) {
    addButton(input_info, 'btn btn-default', '分享微信好友', function () {
        mask();
    });
    input_info.append('<div style="padding-bottom:10px;"></div>');
    addButton(input_info, 'btn btn-default button-wr', '查看红包', lookRedEnvelope);
    //} else {
    //    addButton(input_info, 'btn btn-default', '查看红包', lookRedEnvelope);
    //}

    //input_info.append('<div class="remark">(组队成功后，红包会自动存入该号码的悠先账户)</div>');



    loadRedEnvelopes(response.redEnvelopes);
    hideOther();

    if (isShowMask) {
        mask();
    }

}

//开箱成功
function treasureChestOpen(response) {

    if (objTimer)
        window.clearInterval(objTimer);
    var box = $(".box");
    box.empty(true);
    loadImg(box, 'http://image.u-xian.com/UploadFiles/Images/TreasureChest/box_unlock_a.png');
    objTimer = window.setInterval(animation, 200);
    isShowWeiXinShareButton = true;

    descContent = sharedContents[GetRandomNum(0, 1)].replace('{0}', response.amount);

    var block1 = $("<span />");
    block1.addClass('user-money');
    block1.html(response.amount);
    box.append(block1);
    block1.css("font-size", Number(block1.css("font-size").replace("px", "")) * screenScale);

    loadLockInfo(response.count - response.lockCount, response.lockCount, true);

    var input_info = $(".input-info");
    input_info.empty(true);


    loadActivityFindTreasureChest(input_info, response.remainAmount, response.activityId, response.hadTreasueChest);

    //if (!navigator.userAgent.match(/MicroMessenger/i)) {

    if (response.hadTreasueChest == 0)
        addButton(input_info, 'btn btn-default button-wr', '分享微信好友', function () {
            mask();
        });
    else
        addButton(input_info, 'btn btn-default', '分享微信好友', function () {
            mask();
        });
    input_info.append('<div style="padding-bottom:10px;"></div>');
    addButton(input_info, 'btn btn-default button-wr', '查看红包', lookRedEnvelope);
    //} else {
    //    if (response.hadTreasueChest == 0)
    //        addButton(input_info, 'btn btn-default button-wr', '查看红包', lookRedEnvelope);
    //    else
    //        addButton(input_info, 'btn btn-default', '查看红包', lookRedEnvelope);
    //}


    loadRedEnvelopes(response.redEnvelopes);
    hideOther();

    //if (isShowMask) {
    //    mask();
    //}
}

//红包侧漏
function moneyOver(response) {
    if (objTimer)
        window.clearInterval(objTimer);
    var box = $(".box");
    box.empty(true);
    loadImg(box, 'http://image.u-xian.com/UploadFiles/Images/TreasureChest/redenvelope_over_a.png');
    objTimer = window.setInterval(animation, 200);

    var block1 = $("<span />");
    var anchor1 = $("<a />");
    anchor1.html("活动规则>>");
    anchor1.attr('href', '#rule');
    block1.addClass('rule');
    block1.append(anchor1);
    box.append(block1);

    block1.css("font-size", Number(block1.css("font-size").replace("px", "")) * screenScale);


    $(".lock-info").hide();

    var input_info = $(".input-info");
    input_info.empty(true);

    input_info.append('<div class="remark" style="padding-bottom:10px;">(把红包吃光光再来领)</div>');
    addButton(input_info, 'btn btn-default', '使用红包', open_or_download_app);
    input_info.append('<div style="padding-bottom:10px;"></div>');
    addButton(input_info, 'btn btn-default button-wr', '查看红包', lookRedEnvelope);

    $("#redEnvelope").hide();
    $("#suitable-shop").show();
    $("#whatisux").show();
    var anchor2 = $("<a />");
    anchor2.attr('name', 'rule');

    $("#activityRule").show();
    $(".main").append(anchor2);
    $(".main").append($("#activityRule"));
}

//活动结束
function gameover(response) {
    console.log(response);
    if (objTimer)
        window.clearInterval(objTimer);
    var box = $(".box");
    box.empty(true);
    loadImg(box, 'http://image.u-xian.com/UploadFiles/Images/TreasureChest/game_over_a.png');
    objTimer = window.setInterval(animation, 200);

    $(".lock-info").hide();

    loadRedEnvelopes(response.redEnvelopes);

    $("#suitable-shop").show();
    $("#whatisux").show();
    $("#activityRule").hide();

    friendshipSponsor();

    var input_info = $(".input-info");
    input_info.empty(true);
    if (navigator.userAgent.match(/MicroMessenger/i)) {
        addButton(input_info, 'btn btn-default', '下载悠先', open_or_download_app);
        input_info.append('<div class="remark" style="padding-bottom:10px;"></div>');
    }
    addButton(input_info, 'btn btn-default button-wr', '加入我们', function () {
        location.href = "http://u-xian.com/";
    });

}

/*页面元素*/
function loadImg(parent, imgPath) {
    var img = $("<img />");
    img.attr('src', imgPath);
    parent.append(img);
}
// 锁信息
function loadLockInfo(unlock, lock, p) {
    var lockInfo = $(".lock-info");
    lockInfo.empty(true);
    if (p == undefined || p == false)
        lockInfo.append('<div><img src="http://image.u-xian.com/UploadFiles/Images/TreasureChest/lock.png" />需要' + lock + '个人</div>');
    else {
        lockInfo.append('<div><img src="http://image.u-xian.com/UploadFiles/Images/TreasureChest/lock.png" />还差' + lock + '个人<span>(人齐后红包即生效)</span></div>')
    }

    lockInfo.show();
}
// 活动剩余的钱并抢箱子
function loadActivityFindTreasureChest(parent, remainAmount, activityId, hadTreasueChest) {
    var block1 = $("<div />");
    block1.addClass('activity-remain-amount');

    var block2 = $('<div>本活动还剩 <span class="emphasize">&yen' + remainAmount + '</span></div>')
    block2.css({ 'line-height': '26px','max-width':'60%' });
    block2.addClass('left');
    if (hadTreasueChest == undefined || hadTreasueChest == 0)
        block2.css({ 'line-height': '44px' });
    block1.append(block2);



    var block4 = $("<div />");
    block4.addClass('right');
    block4.css({ 'width': '40%' });
    var button1 = $('<button type="button">');
    button1.addClass('btn btn-default ');
        
    button1.html("我要组队");
    button1.unbind('click');
    if (hadTreasueChest == undefined || hadTreasueChest == 0) {
        button1.click(function (event) {
            location.href = "FromApp.aspx?activityId=" + activityId;
        });
    }
    else {
        button1.addClass('button-hui');
    }

    block4.append(button1);
    block1.append(block4);

    if (hadTreasueChest == undefined || hadTreasueChest == 0) {

    } else {
        var block3 = $('<div style="font-size:14px;float:left;max-width:60%;clear:left;text-align:left;">(您已领过宝箱,不能再领取)</div>');
        block3.css({ 'line-height': '18px' });
        block1.append(block3);
    }


    parent.append(block1);
    block1.css("font-size", Number(block1.css("font-size").replace("px", "")) * screenScale);
}

function addButton(parent, clazz, text, fun) {

    var button = $('<button type="button">');
    button.addClass(clazz);
    button.html(text);
    button.unbind("click");
    button.click(fun);
    parent.append(button);
}

//特别鸣谢
function friendshipSponsor() {
    $(".main").append('<div id="thanks"><div class="title">特别鸣谢</div><ul><li><img src="http://image.u-xian.com/UploadFiles/Images/TreasureChest/kz.png" />筷子兄弟</li><li><img src="http://image.u-xian.com/UploadFiles/Images/TreasureChest/HomeBoy.png" />王力宏</li><li><img src="http://image.u-xian.com/UploadFiles/Images/TreasureChest/server.png" />服务器君</li></ul></div></div>');
}

// 手快的坏蛋们
function loadRedEnvelopes(redEnvelopes) {
    if (redEnvelopes == null || redEnvelopes.length == 0) {
        $("#redEnvelope").hide();
    } else {
        $("#redEnvelope").show();
        var ul = $("#redEnvelope > ul");
        ul.empty(true);
        $.each(redEnvelopes, function (i, n) {
            var li = $("<li />");
            //手机号            
            var block1 = $('<div />');
            block1.addClass('class1');
            block1.html(n.mobilePhone);
            li.append(block1);

            //时间            
            var block4 = $('<span />');
            block4.addClass('class4');
            block4.html(n.datetime);
            block1.append(block4);

            //金额            
            var block2 = $('<div />');
            block2.addClass('class2');
            block2.html("&yen " + n.amount);
            li.append(block2);

            //内容            
            var block3 = $('<div />');
            block3.addClass('class3');
            block3.html('谢谢箱主,以后点菜就用悠先了。');
            li.append(block3);



            ul.append(li);
        });
    }
}

/*公有方法*/
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
}

//头部动画
function animation() {
    var boximgSrc = $(".box > img").attr('src');
    if (boximgSrc.indexOf("_a.png") != -1) {
        $(".box > img").attr('src', boximgSrc.replace('_a.png', '_b.png'));
    } else {
        $(".box > img").attr('src', boximgSrc.replace('_b.png', '_a.png'));
    }
}

function hideOther() {

    $("#suitable-shop").hide();
    $("#whatisux").hide();
    $("#activityRule").hide();

}

function mask() {

    if (navigator.userAgent.match(/MicroMessenger/i)) {

        $(".mask").show();

        var playCount = 0;
        var playTime;
        $('.mask button').click(function (event) {

            playTime = window.setInterval(function () {
                var boximgSrc = $(".mask > img").attr('src');
                if (boximgSrc.indexOf("_a.png") != -1) {
                    $(".mask > img").attr('src', boximgSrc.replace('_a.png', '_b.png'));
                } else {
                    $(".mask > img").attr('src', boximgSrc.replace('_b.png', '_a.png'));
                }
                playCount++;
                if (Math.floor(playCount % 3) == 0) {
                    window.clearInterval(playTime);
                }
            }, 200);

        });


    } else {
        if (navigator.userAgent.match(/(iPhone|iPod|iPad);?/i)) {
            // 判断useragent，当前设备为ios设备  
            window.location = "message:type:0,value:showShareLayout";
        } else if (navigator.userAgent.match(/android/i)) {
            // 判断useragent，当前设备为android设备
            window.redEnvelopeShare.showShareLayout();
        }
    }
}
function GetRandomNum(Min, Max) {
    var Range = Max - Min;
    var Rand = Math.random();
    return (Min + Math.round(Rand * Range));
}


//alert(navigator.userAgent);
function open_or_download_app() {
    if (navigator.userAgent.match(/(iPhone|iPod|iPad);?/i)) {
        // 判断useragent，当前设备为ios设备            
        window.location = "http://mp.weixin.qq.com/mp/redirect?url=http%3A%2F%2Fu-xian.com%2Fd.aspx";　　// iPhone端URL Schema
    } else if (navigator.userAgent.match(/android/i)) {
        // 判断useragent，当前设备为android设备
        window.location = "http://a.app.qq.com/o/simple.jsp?pkgname=va.dish.sys";　　// Android端URL Schema
    }
}

function lookRedEnvelope() {

    window.location = 'list.aspx?mobilePhone=' + $.cookie("mobilePhone");
}