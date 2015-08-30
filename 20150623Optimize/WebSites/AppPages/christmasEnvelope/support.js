function getsnow() {
    return require(['snow/js/ThreeCanvas'])
				.then(function () {
				    return require('snow/js/Snow');
				});
};


var isShowWeiXinShareButton = true;
var descContent = 'evio100万人抢10000个红包，每个50-100元，手要快！';
var imgUrl = 'http://image.u-xian.com/UploadFiles/Images/TreasureChest/weixing_shared.png';
var lineLink = '';
var appid = 'wx3f9ff3043cbcb3b7';
var shareTitle = '';
var imageServer = "http://image.u-xian.com/UploadFiles/Images/TreasureChest/";

function getJSONForAndroid() {
    var j = getJSON();
    window.redEnvelopeShare.getRedenvelopeJson(j);
}
function getJSON() {
    var va = JSON.stringify({ 'content': descContent, 'imgUrl': imgUrl, 'shareUrl': lineLink });
    return va;
}

var getJSONForAndroid = getJSONForAndroid;
var getJSON = getJSON;

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
    WeixinJSBridge.invoke('shareWeibo', {
        "content": descContent,
        "url": lineLink,
    }, function (res) {
        _report('weibo', res.err_msg);
    });
}

define(['jquery', 'jquery-plugin-cookie'], true, function ($) {
    var support = new Class(function () {
        var devices = this.device();
        this.enit = {
            wx: window.navigator.userAgent.match(/MicroMessenger/i),   // 微信
            ap: !window.navigator.userAgent.match(/MicroMessenger/i)   // app中
        }
        this.eqit = {
            ios: devices.iOS, 			// ios设备
            aod: devices.Android		// Android设备
        }
        this.req = this.createServer();
        this.params = {};
        this.getParams();
        this.root = '../RedEnvelope/';
        this.isRetry = false;
        this.ww = $(window).width();
        lineLink = window.location.protocol + '//' + window.location.host + '/AppPages/christmasEnvelope/?activityId=' + encodeURIComponent(this.req.activityId);


        //自定义环境状态改变 用于测试
        /*		this.enit.wx = true;
                this.enit.ap = false;
                this.eqit.ios = false;
                this.eqit.aod = true;*/
    });

    support.add('device', function getDevices() {
        var u = navigator.userAgent, app = navigator.appVersion;
        return {
            trident: u.indexOf('Trident') > -1,
            presto: u.indexOf('Presto') > -1,
            webKit: u.indexOf('AppleWebKit') > -1,
            gecko: u.indexOf('Gecko') > -1 && u.indexOf('KHTML') == -1,
            mobile: !!u.match(/AppleWebKit.*Mobile.*/),
            iOS: !!u.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/),
            Android: u.indexOf('Android') > -1 || u.indexOf('Linux') > -1,
            iPhone: u.indexOf('iPhone') > -1,
            iPad: u.indexOf('iPad') > -1,
            webApp: u.indexOf('Safari') == -1
        };
    });

    support.add('createServer', function () {
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

    support.add('shareContent', function (response) {
        try {

            if (response.shareText && response.shareText.length > 0) {
                descContent = response.shareText.replace('{0}', response.amount).replace('{1}', response.totalamount);
                imgUrl = /^http\:\/\//i.test(response.shareImage) ? response.shareImage : 'http://image220.u-xian.com/' + response.shareImage;
                //alert('ok')
                //alert(descContent);
                //alert(imgUrl)
            }
        } catch (e) { }
        weixinShare();
    });

    support.add('getParams', function () {
        var mobilePhone, cookie;
        if (!this.enit.wx) {
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
        this.params.phone = mobilePhone;
        this.params.cookie = cookie;
    });

    support.add('timer', function (intDiff) {
        window.setInterval(function () {
            $('.time-delay').removeClass('hide');
            if (intDiff <= 0) {
                $('.time-delay').trigger('reload');
            }
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
    });

    support.add('snow', function () {
        var that = this;
        getsnow().then(function () {
            that.startSnow();
        });
    });

    support.add('startSnow', function () {
        var SCREEN_WIDTH = $('.snows').outerWidth();
        var SCREEN_HEIGHT = $('.snows').outerHeight();

        var container;

        var particle;

        var camera;
        var scene;
        var renderer;

        var mouseX = 0;
        var mouseY = 0;

        var windowHalfX = window.innerWidth / 2;
        var windowHalfY = window.innerHeight / 2;

        var particles = [];
        var particleImage = new Image();//THREE.ImageUtils.loadTexture( "img/ParticleSmoke.png" );
        particleImage.src = 'snow/images/ParticleSmoke.png';

        function init() {

            container = document.createElement('div');
            $('.snows').append(container);

            camera = new THREE.PerspectiveCamera(75, SCREEN_WIDTH / SCREEN_HEIGHT, 1, 10000);
            camera.position.z = 1000;

            scene = new THREE.Scene();
            scene.add(camera);

            renderer = new THREE.CanvasRenderer();
            renderer.setSize(SCREEN_WIDTH, SCREEN_HEIGHT);
            var material = new THREE.ParticleBasicMaterial({ map: new THREE.Texture(particleImage) });

            for (var i = 0; i < 500; i++) {

                particle = new Particle3D(material);
                particle.position.x = Math.random() * 2000 - 1000;
                particle.position.y = Math.random() * 2000 - 1000;
                particle.position.z = Math.random() * 2000 - 1000;
                particle.scale.x = particle.scale.y = 1;
                scene.add(particle);

                particles.push(particle);
            }

            container.appendChild(renderer.domElement);


            /*document.addEventListener( 'mousemove', onDocumentMouseMove, false );
			document.addEventListener( 'touchstart', onDocumentTouchStart, false );
			document.addEventListener( 'touchmove', onDocumentTouchMove, false );*/

            setInterval(loop, 1000 / 60);

        }

        //

        function loop() {

            for (var i = 0; i < particles.length; i++) {

                var particle = particles[i];
                particle.updatePhysics();

                with (particle.position) {
                    if (y < -1000) y += 2000;
                    if (x > 1000) x -= 2000;
                    else if (x < -1000) x += 2000;
                    if (z > 1000) z -= 2000;
                    else if (z < -1000) z += 2000;
                }
            }

            camera.position.x += (mouseX - camera.position.x) * 0.05;
            camera.position.y += (-mouseY - camera.position.y) * 0.05;
            camera.lookAt(scene.position);

            renderer.render(scene, camera);


        }

        init();
    });


    support.add('button', function (text, callback) {
        var a = document.createElement('a');
        $('.text').append(a);
        $(a).append('<img src="img/btn-1.png" class="img-responsive" />');
        $(a).append('<span class="val">' + text + '</span>');
        $(a).attr('href', 'javascript:;').addClass('button-bg').on('click', callback);
        var w = $(a).find('span').outerWidth(),
			h = $(a).find('span').outerHeight();

        $(a).find('span').css({
            top: '50%',
            left: '50%',
            "margin-top": "-" + (h / 2) + "px",
            "margin-left": "-" + (w / 2) + "px"
        })
    });

    support.add('addRule', function (html) {
        $('#rule').html(html);
    });

    support.add('share', function () {

        if (this.eqit.ios) {
            window.location = "message:type:0,value:showShareLayout";
        }
        else if (this.eqit.aod) {
            window.redEnvelopeShare.showShareLayout();
        }

    });

    support.add('download', function () {
        window.location = "http://a.app.qq.com/o/simple.jsp?pkgname=va.dish.sys";
    });

    support.add('music', function () {

        var b = new Audio("http://uxian.oss-cn-hangzhou.aliyuncs.com/UploadFiles/Images/TreasureChest/christmas/family.mp3");
        b.loop = true;
        b.autoplay = false;
        isAudioFirst = !0,
        b && (b.loop = !0, $("#music_div").on("click",
        function () {
            isAudioFirst && b.play(),
            isAudioFirst = !1
        }), $("#music_div").on('click', function () {
            var c = $("#music_div");
            c.hasClass("stop") ? (b.pause(), c.removeClass("stop")) : (b.play(), c.addClass("stop"))
        }))

        return b;
    });


    return support;
});