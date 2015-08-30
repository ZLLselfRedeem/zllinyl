var VA = {}; //viewAlloc

VA.mobile = {};
VA.mobile.widgets = {
    resizeStage: function () {
        var $page = VA.mobile.initpage, $pageSprite = $('.pageSprite', $page), $pageLoading = $('.page-loading', $page), $pageFooter = $('footer', $page);
        $pageSprite.css({ 'height': VA.mobile.winHeight });
        $page.css({
            'background-image': 'url(css/images/bg.png)'
        });
        $pageLoading.css('width',VA.mobile.winWidth*0.8).css({
            position: 'absolute',
            left: (VA.mobile.winWidth - $pageLoading.width()) / 2,
            top: (VA.mobile.winHeight - $pageLoading.height()) / 2
        });
        $pageFooter.css({
            position: 'absolute',
            left: (VA.mobile.winWidth - 168) / 2,
            top: (VA.mobile.winHeight - 28)
        });
    },
    pageloading: function () {
        var $page = VA.mobile.initpage, $pageSprite = $('.pageSprite', $page), $pageLoading = $('.page-loading', $page), $pageLoadingValue = $pageLoading.find('.page-loading-progressbar-value');

        if (VA.mobile.loaded) {
            $pageLoading.hide();
            return;
        }
        VA.mobile.loaded = true;
        $pageLoadingValue.animate({
            width: '100%'
        }, 3000, "swing", function () {
            $pageSprite.delay(600).fadeOut();
        })
    },

    setCookieUser: function () {
        var _self = this;
        var wechatInfo = new VAWechatInfo(); //userInfo

        wechatInfo.wechatId = GetRequestParam("wechatopenid"); //CommonScript
        var wechatIdJson = jQuery.toJSON(wechatInfo); //{"wechatId":""}
        var ajaxOption = new Object();
        ajaxOption.type = 'post';
        ajaxOption.url = 'Default.aspx/WechatLogin';
        ajaxOption.data = "{'wechatIdJson':'" + wechatIdJson + "'}"; //
        ajaxOption.contentType = "application/json; charset=utf-8";
        ajaxOption.dataType = 'json';
        ajaxOption.success = function (data) {
            if (data.d != "" && data.d != null) {
                var clientCookie = eval("(" + data.d + ")").cookie;
                var clientUuid = eval("(" + data.d + ")").uuid;
                var clienteVip = eval("(" + data.d + ")").eVip;
                $.cookie("clientCookie", clientCookie);
                $.cookie("clientUuid", clientUuid);
                $.cookie("clienteVip", clienteVip);
                //                //开始预加载CompanyInfo.aspx页面
                //                var strPageDiv = "<a href='CompanyInfo.aspx' rel='external' data-prefetch='ture'>点点</a>";
                //                $("#PrestrainPage").append(strPageDiv);
                //
				new companyAdd();
                _self.geo(_self.setCookieGeo);
            }
            else {
                alert("不好意思，您登录不成功");
            }
        };
        ajaxOption.error = function (XmlHttpRequest, textStatus, errorThrown) {
            //
        };
        $.ajax(ajaxOption);
    },

    setGeoErr: function (msg) {
        alert(msg);
    },
    setCookieGeo: function (arg) {
        //$.cookie("latitude", arg.latitude);
        //$.cookie("longitude", arg.longitude);
        //alert("经度:"+arg.longitude+",纬度:"+arg.latitude);

        VA.mobile.widgets.pageloading();
    },
    geo: function (successCallback, errorCallback) {
        var that = this;
        //this.success = successCallback;
        var url = 'http://api.map.baidu.com/location/ip?ak=87213a0c1f927b4bb083e6f9d5865856&ip=&coor=bd09ll';
        jQuery.getJSON(url + "&callback=?", function (data) {
            var coords = {
                latitude: data.content.point['y'],
                longitude: data.content.point['x']
            };
            //alert(data.content.address_detail['city']);
            return successCallback.call(that, coords);
        });

    },

    getPageSize: function () {
        var xScroll, yScroll;
        if (window.innerHeight && window.scrollMaxY) {
            xScroll = window.innerWidth + window.scrollMaxX;
            yScroll = window.innerHeight + window.scrollMaxY;
        }
        else {
            if (document.body.scrollHeight > document.body.offsetHeight) { // all but Explorer Mac    
                xScroll = document.body.scrollWidth;
                yScroll = document.body.scrollHeight;
            }
            else { // Explorer Mac...would also work in Explorer 6 Strict, Mozilla and Safari    
                xScroll = document.body.offsetWidth;
                yScroll = document.body.offsetHeight;
            }
        }

        var windowWidth, windowHeight;
        if (self.innerHeight) { // all except Explorer    
            if (document.documentElement.clientWidth) {
                windowWidth = document.documentElement.clientWidth;
            }
            else {
                windowWidth = self.innerWidth;
            }

            windowHeight = self.innerHeight;
        }
        else {
            if (document.documentElement && document.documentElement.clientHeight) { // Explorer 6 Strict Mode    
                windowWidth = document.documentElement.clientWidth;
                windowHeight = document.documentElement.clientHeight;
            }
            else {
                if (document.body) { // other Explorers    
                    windowWidth = document.body.clientWidth;
                    windowHeight = document.body.clientHeight;
                }
            }
        }
        // for small pages with total height less then height of the viewport    
        if (yScroll < windowHeight) {
            pageHeight = windowHeight;
        }
        else {
            pageHeight = yScroll;
        }

        // for small pages with total width less then width of the viewport    
        if (xScroll < windowWidth) {
            pageWidth = xScroll;
        }
        else {
            pageWidth = windowWidth;
        }
        arrayPageSize = { "pageW": pageWidth, "pageH": pageHeight, "winW": windowWidth, "winH": windowHeight };

        return arrayPageSize;
    },
	isDevices : function(){
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
			webApp: u.indexOf('Safari') == -1, 				//是否web应该程序，没有头部与底部
			
			language: (navigator.browserLanguage || navigator.language).toLowerCase()
		};
		
		// if (browser.mobile||browser.Android)
	},
	insertStyles:function(){
		var doc,cssCode=[],cssText;
		var len = arguments.length;
		var head,styles,styleElem;
		if(len==1){
			doc = document;
			cssCode.push(arguments[0]);
		}else if(len==2){
			doc = arguments[0];
			cssCode.push(arguments[1]);
		}else{
			alert("函数最多只能接受两个参数；");
		}
		
		head = doc.getElementsByTagName("head")[0];
		styles = head.getElementsByTagName("style");
		if(styles.length==0){// 当前文档没有嵌入式样式属性情况；
			if(doc.createStyleSheet){// ie 能力检测
				doc.createStyleSheet();
			}else{
				var tempStyle = doc.createElement("style");
				tempStyle.setAttribute("type","text/css");
				head.appendChild(tempStyle);
			}
		};
		
		styleElem = styles[0];// first stylesheet
		cssText = cssCode.join("\n");
		if(!+"\v1"){// opacity兼容
			var str = cssText.match(/opacity:(\d?\.\d+);/);
			if(str!=null){
				cssText = cssText.replace(str[0],"filter:alpha(opacity="+pareFloat(str[1])*100+")");
			}
		}
		
		// 检测已有style属性值
		if(styleElem.styleSheet){
			styleElem.styleSheet.cssText += cssText;
		}else if(doc.getBoxObjectFor){
			styleElem.innerHTML += cssText;
		}else{
			styleElem.appendChild(doc.createTextNode(cssText));
		}
	}
};

VA.mobile.validate = {
	trim:function(s) {
		return s.replace(/(^\s*)|(\s*$)/g,"");
	},
	isMobile:function(arg){
		return /(^1[3|5|8][0-9]{9}$)/.test(this.trim(arg));
	},
	isTel:function(){
	//"兼容格式: 国家代码(2到3位)-区号(2到3位)-电话号码(7到8位)-分机号(3位)"
	//return (/^(([0+]d{2,3}-)?(0d{2,3})-)?(d{7,8})(-(d{3,}))?$/.test(this.trim()));
		return (/^(([0+]d{2,3}-)?(0d{2,3})-)(d{7,8})(-(d{3,}))?$/.test(this.trim()));
	},
	isEmail:function isEmail(strEmail){
		if (strEmail.search(/^w+((-w+)|(.w+))*@[A-Za-z0-9]+((.|-)[A-Za-z0-9]+)*.[A-Za-z0-9]+$/) != -1)
			return true;
		else
			return false;
	},
	isValidationCode:function(arg){
		return /^[0-9]{5}$/.test(this.trim(arg));
	}
};


