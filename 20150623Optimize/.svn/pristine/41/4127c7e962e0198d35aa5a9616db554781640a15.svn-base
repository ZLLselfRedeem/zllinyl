(function (mod) {

    // CommonJS Rule
    if (typeof exports == "object" && typeof module == "object") {
        require('jquery-plugin-cookie');
        module.exports = mod(require('jquery'));
    }

        // AMD Rule
    else if (typeof define == "function" && define.amd) {
        return define(['jquery'], mod);
    }

        // Brower Env
    else {
        this.CodeMirror = mod($);
    }

})(function ($) {

    var isShowWeiXinShareButton = false;
    var root = '../RedEnvelope/';
    var isRetry = true;
    var descContent = '100万人抢10000个红包，每个50-100元，手要快！';
    var imgUrl = 'http://image.u-xian.com/UploadFiles/Images/TreasureChest/weixing_shared.png';
    var lineLink = '';
    var appid = 'wx3f9ff3043cbcb3b7';
    var shareTitle = '';
    var imageServer = "http://image.u-xian.com/UploadFiles/Images/TreasureChest/";

    var doing = false;

    var cookiexp = 365 * 24 * 60 * 60 * 1000;
    var cookies = function (x) {
        if (typeof x === 'string') {
            return new Date(x);
        } else {
            return new Date(new Date().getTime() + x);
        }
    }

    var screenScale = document.body.clientWidth / 640; // 屏幕宽度比例
    if (screenScale > 1)
        screenScale = 1;

    // 判断环境
    var inWx = navigator.userAgent.match(/MicroMessenger/i); // wei xin
    var inAp = navigator.userAgent.match(/(iPhone|iPod|iPad);?/i); // ios
    var inAn = navigator.userAgent.match(/android/i); // android

    // 初始化类模块
    var envelope = new Class(function (configs) {
        this.configs = $.extend({}, configs || {});
        this.req = this.createServer();
        this.ParseConfigs();
        this.ParseBannerEvents();
        this.getStatus();
        lineLink = window.location.protocol + '//' + window.location.host + '/AppPages/BigRedEnvelope/index.html?activityId=' + encodeURIComponent(this.req.activityId);
        var ms = this.req.mobilephone;
        if (ms && ms.length > 0) {
            $('#PhoneNumber').val();
        };
    });

    // 系统崩溃
    envelope.add('beng', function () {
        $('.envelope-wrap').hide();
        $('.envelope-beng').show();
    });

    // 初始化之参数初始化
    envelope.add('ParseConfigs', function () {
        this.configs.banner = $(this.configs.banner);
        this.configs.bannerfail = this.configs.banner.find(this.configs.bannerfail);
        this.configs.bannersucc = this.configs.banner.find(this.configs.bannersucc);
        this.configs.bannerover = this.configs.banner.find(this.configs.bannerover);
        this.staticElment = $(this.configs.staticElment);
    });

    // 初始化之事件初始化
    envelope.add('ParseBannerEvents', function () {
        var that = this;
        this.configs.banner.on('getEnvelopeSuccess', function () {
            that.configs.bannerfail.hide();
            that.configs.bannersucc.show();
        }).on('getEnvelopeFailure', function () {
            that.configs.bannerfail.show();
            that.configs.bannersucc.hide();
        });
        this.ModifyPhoneNumber();
        $('body').on('click', '.share-btn', function () {
            if (inWx) {
                download();
            } else {
                share();
            }
        });
        $('.envelope-rule-btn').on('click', function () {
            $('body').scrollTo('#rule', 800);
        })
    });

    // 请求主体
    envelope.add('ajaxSuccess', function (response) {
        doing = false;
        $('.submit-btn').removeClass('disabled');
        setTimeout(function () {
            $('.envelope-loading').remove();
        }, 100);
        if (ajaxStatus[response.status + '']) {

            ajaxStatus[response.status + ''].call(this, response);


        } else {
            this.beng();
        };
        weixinShare();
    });

    envelope.add('getRedEnveLope', function (response) {
        var isChange = response.isChange;
        var amount = response.amount;
        var isGet = response.isGet;
        var mobilePhoneNumber = response.mobilePhoneNumber;
        var redEnvelopeId = response.redEnvelopeId;

        isShowWeiXinShareButton = true;

        $.cookie('mobilePhoneNumber', response.mobilePhoneNumber, {
            expires: cookies(cookiexp)
        });

        if (response.shareText && response.shareText.length > 0) {
            descContent = response.shareText.replace('{0}', response.amount).replace('{1}', response.totalamount);
        }
        imgUrl = response.shareImage;

        this.configs.banner.trigger('getEnvelopeSuccess');

        $('#phoneNumberChange').attr('data-phone', mobilePhoneNumber).attr('data-redEnvelopeId', redEnvelopeId);
        $('#share-phone-number').html(mobilePhoneNumber);
        $('#cny span').html(amount);

        if (isChange) {
            $('#phoneNumberChange').hide();
        }

        if (inWx) {
            $('.share-btn').html('下载悠先');
            $('.envelope-ad').removeClass('hide');
        } else {
            $('.share-btn').addClass('small').html('赶紧转发男票女票闺蜜基友干爹干妈...').after('<div style="text-align:center; margin-top:' + (10 * screenScale) + 'px; font-size:14px; color:#fff;">一百万人抢一万个红包</div>');
        }



        $('#rule ol').html(response.activityRule);
        this.staticElment.show();

        if (response.openshare) {
            descContent = response.shareText.replace('{0}', response.amount).replace('{1}', response.totalamount);
            imgUrl = response.shareImage;
        }

        $('body').scrollTo(0, 0);
    });

    envelope.add('openMasker', function () {
        try {
            $("#myModal").modal().css({
                'margin-top': '0',
                'padding-top': '0',
                'top': '0',
                'position': 'fixed',
                'z-index': 9999
            });
        } catch (e) { alert(e.message) }
    });

    envelope.add('ModifyPhoneNumber', function () {
        var that = this;
        $('body').on('click', '#phoneNumberChange', function () {
            var PhoneNumber = $(this).attr('data-phone'),
				redenvelopeid = $(this).attr('data-redenvelopeid');

            $('#dcmobilePhone').val(PhoneNumber);
            that.openMasker();
        });
        $('body').on('click', '#button_cancel', function () {
            $("#myModal").modal('hide');
        });
        $('body').on('click', '#button_smash', function () {
            if (doing) {
                return;
            }
            var val = $('#dcmobilePhone').val();
            var id = $('#phoneNumberChange').attr('data-redenvelopeid');
            if (!/^1[34578][0-9]{9}$/.test(val)) {
                alert('手机号码格式不正确');
                return;
            }
            doing = true;
            $.ajax({
                url: root + 'TreasureChestHandler.ashx',
                type: 'POST',
                dataType: 'json',
                data: { "m": "modify", "redEnvelopeId": id, "mobilePhoneNumber": val },
                success: function (msg) {
                    doing = false;
                    if (msg.status == 0) {
                        $.cookie("mobilePhoneNumber", val, {
                            expires: cookies(cookiexp)
                        });
                        $('#share-phone-number').html(val);
                        $('#phoneNumberChange').attr('data-phone', val).hide();
                    } else if (msg.status === 1007 || msg.status === -10) {
                        alert("该号码已领过红包");
                    } else {
                        alert('修改失败');
                    }
                    $('#button_cancel').trigger('click');
                },
                error: function () {
                    alert('修改失败');
                    doing = false;
                }
            });
        });
    });

    envelope.add('activeStart', function (response) {
        this.configs.banner.trigger('getEnvelopeFailure');
        this.onBindNotGetEnvelope(response);
        $('#rule ol').html(response.activityRule);
        this.staticElment.show();
    });

    envelope.add('onBindNotGetEnvelope', function (response) {
        var that = this;
        if (response.mobilePhoneNumber && response.mobilePhoneNumber.length > 0) {
            $('#PhoneNumber').val(response.mobilePhoneNumber || '');
        }
        $('.submit-btn').on('click', function () {
            if (doing) {
                return;
            }
            $(this).addClass('disabled');
            var PhoneNumber = $('#PhoneNumber').val();
            if (!/^1[34578][0-9]{9}$/.test(PhoneNumber)) {
                $('.submit-btn').removeClass('disabled');
                alert('手机号码格式不正确');
                return;
            }
            $.cookie("mobilePhoneNumber", PhoneNumber, {
                expires: cookies(cookiexp)
            });
            doing = true;
            $.ajax({
                url: root + 'TreasureChestHandler.ashx',
                type: 'POST',
                dataType: 'json',
                data: { "m": "shared", "redEnvelopeId": response.redEnvelopeId, "mobilePhoneNumber": PhoneNumber },
                success: Library.proxy(that.ajaxSuccess, that),
                error: function () {
                    $('.submit-btn').removeClass('disabled');
                    doing = false;
                    var bang = Library.proxy(that.beng, that);
                    bang();
                }
            });
        });
    });

    // 获取页面初始化状态
    envelope.add('getStatus', function () {
        var mobilePhone = "";
        var cookie = "";
        var that = this;
        if (!navigator.userAgent.match(/MicroMessenger/i)) {
            try {
                if (this.req.mobilephone && this.req.mobilephone.length > 0) {
                    mobilePhone = this.req.mobilephone;
                } else if ($.cookie("mobilePhoneNumber") != null) {
                    mobilePhone = $.cookie("mobilePhoneNumber");
                }
            } catch (e) { }
            cookie = this.req.cookie;
        } else {
            if ($.cookie("mobilePhoneNumber") != null) {
                mobilePhone = $.cookie("mobilePhoneNumber");
            }
        };
        _cookie = $.cookie("cookie_" + this.req.activityId);

        $.ajax({
            url: root + 'TreasureChestHandler.ashx',
            type: 'POST',
            dataType: 'json',
            data: { "m": "pageload", "activityId": this.req.activityId, "mobilePhoneNumber": mobilePhone, "cookie": cookie },
            async: true,
            success: Library.proxy(this.ajaxSuccess, this),
            error: function () {
                if (isRetry == true) {
                    window.setTimeout(Library.proxy(that.getStatus, that), 3000);
                    isRetry = false;
                } else {
                    beng();
                }
            }
        });
    });

    envelope.add('activityNotBegin', function (response) {
        $('.envelope-noStart').show();
        $('html, body').addClass('nostart');
        timer((response.totalSeconds - new Date().getTime()) / 1000);
    });

    envelope.add('gameover', function (response) {
        this.configs.bannerover.show();
        if (inWx) {
            $('.envelope-ad').removeClass('hide');
            this.configs.bannerover.html('<img src="' + imageServer + 'wx-26.png" />');
            this.configs.bannerover.append('<div style="position: absolute; bottom: 40px; left: 0px; width:100%;"><a href="javascript:;" class="share-btn" style="">下载悠先</a></div>');
            isShowWeiXinShareButton = true;
        } else {
            this.configs.bannerover.html('<img src="' + imageServer + 'ap-25.png" />');
            this.configs.bannerover.append('<div style="position: absolute; bottom: 40px; left: 0px; width:100%;"><a href="javascript:;" class="share-btn small" style="">赶紧转发男票女票闺蜜基友干爹干妈...</a></div>')
        }
        $('#rule').hide();
        this.staticElment.show();
    });

    envelope.add('createServer', function () {
        var qs = (window.location.search.length > 0 ? location.search.substring(1) : "");
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
    });

    function timer(intDiff) {
        window.setInterval(function () {
            var day = 0,
				hour = 0,
				minute = 0,
				second = 0;//时间默认值		
            if (intDiff > 0) {
                day = Math.floor(intDiff / (60 * 60 * 24));
                hour = Math.floor(intDiff / (60 * 60)) - (day * 24);
                minute = Math.floor(intDiff / 60) - (day * 24 * 60) - (hour * 60);
                second = Math.floor(intDiff) - (day * 24 * 60 * 60) - (hour * 60 * 60) - (minute * 60);
            }

            if (minute <= 9) minute = '0' + minute;
            if (second <= 9) second = '0' + second;
            $('#day_show').html(day + "天");
            if (day < 1) {
                $('#day_show').hide();
            } else {
                $('#day_show').show();
            }
            $('#hour_show').html('<s id="h"></s>' + hour + '时');
            $('#minute_show').html('<s></s>' + minute + '分');
            $('#second_show').html('<s></s>' + second + '秒');
            intDiff--;
        }, 1000);
    };

    function download() {
        if (inAp || inAn) {
            window.location = "http://a.app.qq.com/o/simple.jsp?pkgname=va.dish.sys";
        }
    }

    function share() {
        if (inAp) {
            // 判断useragent，当前设备为ios设备  
            window.location = "message:type:0,value:showShareLayout";
        } else if (inAn) {
            // 判断useragent，当前设备为android设备
            window.redEnvelopeShare.showShareLayout();
        }
    }

    function getJSONForAndroid() {
        var j = getJSON();
        window.redEnvelopeShare.getRedenvelopeJson(j);
    }
    function getJSON() {
        var va = JSON.stringify({ 'content': descContent, 'imgUrl': imgUrl, 'shareUrl': lineLink });
        return va;
    }

    window.getJSONForAndroid = getJSONForAndroid;
    window.getJSON = getJSON;

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
            //$(".main").show();
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
            _report('send_msg', res.err_msg);
        });
    }

    function shareTimeline() {
        $(".mask button").removeClass('button-hui');
        $(".mask button").unbind('click');
        $(".mask button").click(function (event) {
            $(".mask").hide();
            //$(".main").show();
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
            //$(".main").show();
        });

        WeixinJSBridge.invoke('shareWeibo', {
            "content": descContent,
            "url": lineLink,
        }, function (res) {
            _report('weibo', res.err_msg);
        });
    }

    var ajaxStatus = {
        "0": function (response) {
            var urlphonenumber = this.req.mobilephone;
            var _temphone = urlphonenumber;
            var cookiephone = $.cookie("mobilePhoneNumber");

            if (response.isGet) {
                response.openshare = false;
                //if ( !response.mobilePhoneNumber ){
                //	response.mobilePhoneNumber = urlphonenumber;
                //	if ( !response.mobilePhoneNumber ){
                //		response.mobilePhoneNumber = cookiephone;
                //	}
                //}

                this.getRedEnveLope(response);
            } else {
                //response.mobilePhoneNumber = _temphone;
                if (!response.mobilePhoneNumber) {
                    response.mobilePhoneNumber = cookiephone;
                }
                this.activeStart(response);
            }
        },
        "2": function (response) {
            this.gameover(response);
        },
        "4": function (response) {
            response.isChange = false;
            response.isGet = true;
            response.openshare = true;
            this.getRedEnveLope(response);
            if (!inWx) {
                $('.share-btn').trigger('click');
            }
        },
        "-6": function (response) {
            $('.envelope-wrap').hide();
            $('.envelope-none').show();
        },
        "3": function (response) {
            this.activityNotBegin(response);
        },
        "1004": function (response) {
            response.openshare = false;
            this.getRedEnveLope(response);
        },
        "1007": function (response) {
            alert(response.context);
            return;
            response.openshare = false;
            this.getRedEnveLope(response);

        }
    }


    return envelope;
});