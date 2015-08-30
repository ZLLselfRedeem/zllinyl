$(function() {
    var bgHeader = (52 / 64), _width, _height;
    doParentDraw(bgHeader, ".headerTop");
    function doParentDraw(ratio, node) {
	var bgList = $(node);
	_width = $(bgList).width();
	for (var i = 0, len = bgList.length; i < len; i++) {
	    scaleChildrenImage(bgList[i], _width, _height, ratio);
	}
    }
    function scaleChildrenImage(ImgD, _width, _height, ratio) {
	var h = Math.round(_width * ratio);
	ImgD.style.height = h + "px";
    }
    window.onresize = function() {
	doParentDraw(bgHeader, ".headerTop");
    };

    function createMaskLayer() {
	var maskLayer = document.createElement("div");
	maskLayer.id = "maskLayer";
        document.body.appendChild(maskLayer);
    }
    createMaskLayer();

    var getDevices = function() {
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
    };
    var checkBrowser = function() {
	var device = getDevices();
	if (device.iOS) {
	    ToiOS();
	} else if (device.Android) {
	    if (!is_weixin()) {
	        ToAndroid();
	    }
	    else {
	        ToiOS();
	    }

	} else {
	    ToAndroid();
	}
    };
    function is_weixin() {
	var ua = navigator.userAgent.toLowerCase();
	if (ua.match(/MicroMessenger/i) == "micromessenger") {
	    return true;
	} else {
	    return false;
	}
    };
    function ToAndroid() {
	$("#panelShow img").attr("src","AppPages/img/pagedownload/t_android.png");
	window.location.href = "http://uxian.oss-cn-hangzhou.aliyuncs.com/UploadFiles/uxian.apk";
    };
    function ToiOS() {
	$("#panelShow img").attr("src","AppPages/img/pagedownload/t_s.png");
	window.location.href = "http://a.app.qq.com/o/simple.jsp?pkgname=va.dish.sys";
    };
    function getApp(){
	checkBrowser();
    };
    
    getApp();
    
    var popup = $(".popup-layer");
    popup.on("click", function(event) {
	$("#maskLayer").show();
	$("#panelShow").slideDown();
	setTimeout(function() {
	    $("#maskLayer").fadeOut();
	    $("#panelShow").slideUp();
	}, 5000);
	getApp();
    });
});