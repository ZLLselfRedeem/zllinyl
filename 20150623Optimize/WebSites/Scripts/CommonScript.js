//获取get的值
function GetRequestParam(argname) {
    var url = document.location.href;
    var arrStr = url.substring(url.indexOf("?") + 1).split("&");
    for (var i = 0; i < arrStr.length; i++) {
        var loc = arrStr[i].indexOf(argname + "=");
        if (loc != -1) {
            return arrStr[i].replace(argname + "=", "").replace("?", "");
            break;
        }
    }
    return "";
}

jQuery.extend({
    /** 
    * @see 将javascript数据类型转换为json字符串   
    * @param 待转换对象,支持  object,array,string,function,number,boolean,regexp   
    * @return 返回json字符串 
    */
    toJSON: function (object) {
        var type = typeof object;
        if ('object' == type) {
            if (Array == object.constructor)
                type = 'array';
            else if (RegExp == object.constructor)
                type = 'regexp';
            else
                type = 'object';
        }
        switch (type) {
            case 'undefined':
            case 'unknown':
                return;
            case 'function':
            case 'boolean':
            case 'regexp':
                return object.toString();
            case 'number':
                return isFinite(object) ? object.toString() : 'null';
            case 'string':
                return '"' + object.replace(/(|")/g, "$1").replace(/n|r|t/g, function () {
                    var a = arguments[0];
                    return (a == 'n') ? 'n' : (a == 'r') ? 'r' : (a == 't') ? 't' : ""
                }) + '"';
            case 'object':
                if (object === null)
                    return 'null';
                var results = [];
                for (var property in object) {
                    var value = jQuery.toJSON(object[property]);
                    if (value !== undefined) results.push(jQuery.toJSON(property) + ':' + value);
                }
                return '{' + results.join(',') + '}';
            case 'array':
                var results = [];
                for (var i = 0; i < object.length; i++) {
                    var value = jQuery.toJSON(object[i]);
                    if (value !== undefined) results.push(value);
                }
                return '[' + results.join(',') + ']';
        }
    }
});
//判断浏览器所安装的插件
function getPlugins() {
    //取得插件的个数
    num = navigator.plugins.length;
    document.write("你浏览器上安装的插件有：");
    for (i = 0; i < num; i++) {
        //取得插件的名称
        name = navigator.plugins[i].name;
        //取得插件的文件名称
        filename = navigator.plugins[i].filename;
        document.write(name + "---->" + filename + "<br>");
    }
}
function getFlashPluginsActiveXObject() {
    var isInstalled = false;
    var version = null;
    if (window.ActiveXObject) {
        var control = null;
        try {
            control = new ActiveXObject('ShockwaveFlash.ShockwaveFlash');
        } catch (e) {
            return;
        }
        if (control) {
            isInstalled = true;
            version = control.GetVariable('$version').substring(4);
            version = version.split(',');
            version = parseFloat(version[0] + '.' + version[1]);
        }
    } else {
        if (getFlashPluginsNavigator()) {
            isInstalled = true;
        }
        // Check navigator.plugins for "Shockwave Flash"
    }
    return isInstalled;
}

//检查浏览器是否安装了FLASH插件
function getFlashPluginsNavigator() {
    //若检查是否安装有QuickTime插件
    //则:mime = "video/quicktime";
    var message = false;
    mime = "application/x-shockwave-flash";
    if (navigator.mimeTypes && navigator.mimeTypes[mime] && navigator.mimeTypes[mime].enabledPlugin) {
        message = true;
    } else {
        message = false;
    }
    return message;
}
///<summary>
///判断请求来源是手机还是pc
///</summary>
function checkuserAgent() {
    var userAgentInfo = navigator.userAgent;
    var userAgentKeywords = new Array("iPad", "iPhone", "Android", "MIDP", "Opera Mobi",
          "Opera Mini", "BlackBerry", "HP iPAQ", "IEMobile",
          "MSIEMobile", "Windows Phone", "HTC", "LG",
          "MOT", "Nokia", "Symbian", "Fennec",
          "Maemo", "Tear", "Midori", "armv",
          "Windows CE", "WindowsCE", "Smartphone", "240x320",
          "176x220", "320x320", "160x160", "webOS",
          "Palm", "Sagem", "Samsung", "SGH",
          "SIE", "SonyEricsson", "MMP", "UCWEB");
    var isMobilePhone = false;
    for (var i = 0; i < userAgentKeywords.length; i++) {
        var keyWord = userAgentKeywords[i];
        if (userAgentInfo.indexOf(keyWord) != -1 || userAgentInfo.toLowerCase().indexOf(keyWord.toLowerCase()) != -1) {
            isMobilePhone = true;
        }
    }
    return isMobilePhone;
}

/*
* 设备端口判断:
*		移动端
*		桌面端
*/
function checkBrowser(arg) {
    var browser;
    $(function () {
        try {
            browser = {
                versions: function () {
                    var u = navigator.userAgent, app = navigator.appVersion;
                    return {         									//移动终端浏览器版本信息
                        trident: u.indexOf('Trident') > -1, 			//IE内核
                        presto: u.indexOf('Presto') > -1, 				//opera内核
                        webKit: u.indexOf('AppleWebKit') > -1, 			//苹果、谷歌内核
                        gecko: u.indexOf('Gecko') > -1 && u.indexOf('KHTML') == -1, //火狐内核
                        mobile: !!u.match(/AppleWebKit.*Mobile.*/), 	//是否为移动终端
                        iOS: !!u.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/), //ios终端
                        Android: u.indexOf('Android') > -1 || u.indexOf('Linux') > -1, //android终端或uc浏览器
                        iPhone: u.indexOf('iPhone') > -1, 				//是否为iPhone或者QQHD浏览器
                        iPad: u.indexOf('iPad') > -1, 					//是否iPad
                        webApp: u.indexOf('Safari') == -1 				//是否web应该程序，没有头部与底部
                    };
                } (),
                language: (navigator.browserLanguage || navigator.language).toLowerCase()
            }
            if (browser.versions.mobile || browser.versions.Android) {
                window.location.href = arg.mobile;
            } else {
                window.location.href = arg.web;
            }
        } catch (e) {
            // 
        }
    });
}
/*
*Default页面加载滚动条问题
*/
function htCheckBrowser() {
    var browser;
    $(function () {
        try {
            browser = {
                versions: function () {
                    var u = navigator.userAgent, app = navigator.appVersion;
                    return {         									//移动终端浏览器版本信息
                        trident: u.indexOf('Trident') > -1, 			//IE内核
                        presto: u.indexOf('Presto') > -1, 				//opera内核
                        webKit: u.indexOf('AppleWebKit') > -1, 			//苹果、谷歌内核
                        gecko: u.indexOf('Gecko') > -1 && u.indexOf('KHTML') == -1, //火狐内核
                        mobile: !!u.match(/AppleWebKit.*Mobile.*/), 	//是否为移动终端
                        iOS: !!u.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/), //ios终端
                        Android: u.indexOf('Android') > -1 || u.indexOf('Linux') > -1, //android终端或uc浏览器
                        iPhone: u.indexOf('iPhone') > -1, 				//是否为iPhone或者QQHD浏览器
                        iPad: u.indexOf('iPad') > -1, 					//是否iPad
                        webApp: u.indexOf('Safari') == -1 				//是否web应该程序，没有头部与底部
                    };
                } (),
                language: (navigator.browserLanguage || navigator.language).toLowerCase()
            }
            if (browser.versions.mobile || browser.versions.Android) {
                return;
            } else {
                $("body").css("overflow-y", "hidden");
            }
        } catch (e) {
            // 
        }
    });
}

function getQueryStringArgs() {
	var qs = (location.search.length > 0 ? location.search.substring(1) : "");
	var args = {};
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


