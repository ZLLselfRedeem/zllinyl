/**
 * Created by evio on 2015/5/12.
 */
(function($, exports){
    var global = this;
    var listtype = { "hot": 2, "conforts": 1, "nearly": 3 };
    Object.defineProperty(exports, 'wxshare', { set: function(val){ window.isShowWeiXinShareButton = val; } });
    Object.defineProperty(exports, 'cookiephone', {
        set: function(value){
            $.cookie('mobilePhoneNumber', value, {
               expires: new Date('3015/01/01 00:00:00'),
               path: '/'
            });
        },
        get: function(){
            return $.cookie('mobilePhoneNumber');
        }
    });
    exports.storage = {};
    exports.list = {};
    exports.doing = false;
    exports.http = function(url, data){
        return new Promise(function(resolve, reject){
            $.ajax({
                contentType: "application/json",
                url: url,
                type: 'POST',
                dataType: 'json',
                data: JSON.stringify(data),
                success: function(msg){
                    resolve(msg);
                },
                error: function(msg){
                    reject(msg);
                }
            });
        });
    };
    exports.lookup = function(){
        exports.storage.__DefineActive__ = null;
        exports.storage.__DefineOrders__ = 0;
        Object.defineProperty(exports.list, 'active', {
            set: function(val){
                if ( exports.storage[val] ){
                    exports.storage.__DefineActive__ = val;
                    exports.list.order = listtype[val];
                }
            },
            get: function(){
                return exports.storage.__DefineActive__;
            }
        });
        Object.defineProperty(exports.list, 'order', {
            set: function(val){
                exports.storage.__DefineOrders__ = val;
            },
            get: function(){
                return exports.storage.__DefineOrders__;
            }
        });
        ['data', 'page', 'more'].forEach(function(sechme){
            Object.defineProperty(exports.list, sechme, {
                set: function(val){
                    if ( exports.storage[exports.list.active] ){
                        exports.storage[exports.list.active][sechme] = val;
                    }
                },
                get: function(){
                    return exports.storage[exports.list.active][sechme];
                }
            });
        });
    };
    exports.http.list = function(isAppend){
        var url = exports.qr ? '/coupon/CouponMsg.aspx/ShareSystemCoupon' : '/coupon/CouponMsg.aspx/ShareCoupon';
        var args = exports.qr ?
        {
            "activityIdEncrypt": exports.req.id,
            "cityId": exports.req.cityId,
            "pageSize": 10,
            "pageIndex": exports.list.page,
            "displayType": exports.list.order,
            "longitude": exports.lot || 0,
            "latitude": exports.lat || 0,
            "phoneNumber": exports.phone || ''
        }
            :
        {
            "preOrder19DianIdEncrypt": exports.req.id,
            "phoneNumber": exports.phone || '',
            "pageSize": 10,
            "pageIndex": exports.list.page,
            "displayType": exports.list.order,
            "longitude": exports.lot || 0,
            "latitude": exports.lat || 0
        };

        var callback = function(msg){
            msg = JSON.parse(msg.d);
            window.shareTitle = msg.shareTitle;
            window.shareImage = msg.shareImage;
            window.shareContent = msg.shareText;
            msg.appended = isAppend;
            if ( !exports.bindwx ) { exports.bindwx = true; weixinShare(); }
            if ( exports.http.list[msg.shareState] ){  exports.http.list[msg.shareState](msg); }
            else{ exports.http.list['500'](); }
        };

        var xhr;

        if ( exports.list.active === 'nearly' ){
            xhr = exports.GPS().then(function(msg){
                exports.lot = msg.lot;
                exports.lat = msg.lat;
                args.longitude = exports.lot;
                args.latitude = exports.lat;
                return exports.http(url, args);
            })['catch'](function(msg){ 
							if (msg && msg.code) {
                exports.doing = false;
							    $("#tabs li[data-value='hot']").trigger('click');
							}
							return Promise.reject();
						});
        }else{
            xhr = exports.http(url, args);
        }
        exports.doing = true;
        xhr.then(function(msg){ exports.doing = false; callback(msg); })['catch'](function(err){ exports.http.list['500'](err); });
    };
    exports.http.got = function(couponId, phone){
        var args = { "couponId": couponId, "preOrder19DianIdEncrypt": exports.req.id, "phoneNumber": phone };
        var urls = '/coupon/CouponMsg.aspx/GetCoupon';
        if ( exports.qr ) {
            args.activityIdEncrypt = exports.req.id;
            delete args.preOrder19DianIdEncrypt;
            urls = '/coupon/CouponMsg.aspx/GetSystemCoupon';
        }
        exports.http(urls, args).then(function(msg){
            msg = JSON.parse(msg.d);
            var status = msg.getState;
            if (exports.http.got[status]) { exports.http.got[status](msg); }
            else { exports.http.got['500'](msg); }
        })['catch'](function(){
            exports.http.got['500']();
        });
    };
    exports.http.info = function(){
        return exports.http(
            '/coupon/CouponMsg.aspx/GetCouponDetail',
            {
                "preOrder19DianIdEncrypt": exports.req.id,
                "phoneNumber": exports.phone
            }
        );
    };
    exports.http.shopName = function(name){
        return exports.http(
            '/coupon/CouponMsg.aspx/SearchShopName',
            {
                "keyWord": name,
                "idEncrypt": exports.req.id,
                "pageSize": 10,
                "pageIndex": 1,
                "cityId": exports.req.cityId || 0
            }
        );
    };
    exports.http.correct = function(response){
        return exports.http('/Coupon/CouponMsg.aspx/CorrectNumber', {
            "couponGetDetailId": response.couponGetDetailId,
            "phoneNumber": response.phone
        });
    };
    exports.init = function(callback){
        exports.wxshare = true;
        exports.search = global.location.search.replace(/^\?/, '');
        exports.req = series.QueryString.parse(exports.search);
        exports.qr = exports.req.shareType == 2;
        if ( exports.req.phoneNumber ){ exports.phone = exports.req.phoneNumber; }
        else{ exports.phone = exports.cookiephone; }
        ['hot', 'conforts', 'nearly'].forEach(function(which){
            exports.storage[which] = { data: [], page: 1, more: true }
        });
        exports.lookup();
        $(function(){ typeof callback === 'function' && callback($, exports); });
    };
    exports.reload = function(couponList){
        var _masonry = $('.conMain').data('masonry'),
            cache = exports.list.data.length === 0,
            dist = [];

        couponList.forEach(function(detail){
            dist.push(exports.makeListDiscount(detail, _masonry));
        });

        Promise.all(dist).then(function(){
            if ( _masonry ) { _masonry.reloadItems(); _masonry.layout(); }
            $('#loading').addClass('hide');
            if ( !_masonry || cache ){ exports.masonry(); }
            $('.doload').addClass('hide');
        });
    };
    exports.GPS = function() {
        return new Promise(function(resolve, reject) {
            var _resolve = function(position) { resolve({ lat: position.coords.latitude, lot: position.coords.longitude }); };
            var _reject = function(error) {
                var err;
                switch (error.code) {
                    case 1: err = "位置服务被拒绝。"; break;
                    case 2: err = "暂时获取不到位置信息。"; break;
                    case 3: err = "获取位置信息超时。"; break;
                    default: err = "未知错误。";
                }
                reject({ code: error.code, message: err });
            };
            navigator.geolocation.getCurrentPosition(_resolve, _reject, { timeout: 5000, enableHighAccuracy: true });
        });
    };
    exports.masonry = function(x, a){
        if (!x) x = '.conMain';
        if (!a) a = 'dl';
        var $container = $(x);
        $container.imagesLoaded(function() {
            $container.masonry({
                itemSelector: a,
                gutterWidth: 30,
                isAnimated: false
            });
        });
    };

    exports.scroller = function(){
        $(window).off('scroll');
        $(window).on('scroll', function() {
            var page = exports.list.page;
            if ($(window).height() + $('body').scrollTop() + 10 > $('body').outerHeight() && !exports.doing && exports.list.more) {
                exports.list.page = page + 1;
                $('.doload').removeClass('hide');
                exports.http.list();
            }
        });
    };

    exports.stopScroll = function(){
        $(document).on('touchmove', function(ev){
            ev.preventDefault();
        });
    };

    exports.useScroll = function(){
        $(document).off('touchmove');
    };

    exports.nodebone = function(id, data){
        var template = exports.nodebone[id] ? exports.nodebone[id] : document.getElementById(id).innerHTML.replace(/\&lt\;/ig, '<').replace(/\&gt\;/ig, '>');
        exports.nodebone[id] = template;
        var html = series.nodebone.compile( template, { delimiter: '%' } )(data);
        $('#' + id).removeClass('hide').html(html);
    };

    exports.buildShop = function(response){
        exports.nodebone('search-result', {list: response});
    };
		
		exports.version = function(){
			var u = navigator.userAgent, app = navigator.appVersion;
			return {         //移动终端浏览器版本信息
					trident: u.indexOf('Trident') > -1, //IE内核
					presto: u.indexOf('Presto') > -1, //opera内核
					webKit: u.indexOf('AppleWebKit') > -1, //苹果、谷歌内核
					gecko: u.indexOf('Gecko') > -1 && u.indexOf('KHTML') == -1, //火狐内核
					mobile: !!u.match(/AppleWebKit.*Mobile.*/), //是否为移动终端
					ios: !!u.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/), //ios终端
					android: u.indexOf('Android') > -1 || u.indexOf('Linux') > -1, //android终端或uc浏览器
					iPhone: u.indexOf('iPhone') > -1 , //是否为iPhone或者QQHD浏览器
					iPad: u.indexOf('iPad') > -1, //是否iPad
					webApp: u.indexOf('Safari') == -1 //是否web应该程序，没有头部与底部
			};
		}();

    return exports;
}).call(this, jQuery, new Object()).init(function($, exports){
    exports.makeListDiscount = function(c, _masonry){
        return new Promise(function(resolve){
            var h = '';
            h +=    '<dl class="animated">';
            h +=        '<dd>';
            h +=            '<img src="' + c.shopLogo + '" />';
            h +=            '<strong><p class="discountInfo">' + c.couponName + '</p></strong>';
            h +=            '<img src="img/end.png" class="endPic" />';
            h +=        '</dd>';
            h +=        '<dt>';
            h +=            '<p class="shopname">' + c.shopName + '</p>';
            h +=            '<p class="shopADD">' + c.shopAddress + '</p>';
            h +=        '</dt>';
            h +=    '</dl>';

            var el = document.createElement('a');
            $(el).attr('href', 'javascript:;').addClass('shop').attr('data-cid', c.couponId).html(h);
            if ( c.isGot ){ $(el).addClass('shopEndPic'); };

            var img = new Image();
            img.onload = img.onerror = function(){
                $('.conMain').append(el);
                var dl = $(el).find('dl')[0];
                _masonry && _masonry.appended(el);
                resolve(dl);
            };
            img.src = c.shopLogo;
        });
    };

    $('#tabs li').on('click', function(){
        if ( exports.doing ) return;
        var _masonry = $('.conMain').data('masonry');
        $('#tabs li').removeClass('chkCur');
        $('#loading').removeClass('hide');
        $(this).addClass('chkCur');
        _masonry && _masonry.destroy();
        $('.conMain').empty();
        exports.list.active = $(this).data('value');
        if ( exports.list.data.length === 0 ){ exports.http.list(); }
        else{ exports.reload(exports.list.data); }
        exports.scroller();
    });

    $('body').on('click', '.shop', function(){
        if ( $(this).hasClass('shopEndPic') ) return;
        var shopName = $(this).find('.shopname').html();
        var shopAddress = $(this).find('.shopADD').html();
        var disc = $(this).find('.discountInfo').html();
        var couponId = $(this).data('cid');
        var w = shopName + '(' + shopAddress + ') ' + disc;
        exports.stopScroll();
        $('.mask').css({ "height": document.body.scrollHeight + "px" });
        $('.mask').removeClass('hide');
        $('.iptInfo').removeClass('hide');
        $('.iptInfo').find('.title').html(w);
        $('#phoneNumber').attr('couponId', couponId).val(exports.phone || '').focus();
    });

    $('body').on('click', '#cencel', function() {
        exports.useScroll();
        $('.mask').addClass('hide');
        $('.iptInfo').addClass('hide');
    });

    $('body').on('click', '#confirm', function() {
        exports.useScroll();
        var couponId = $('#phoneNumber').attr('couponId');
        var phone = $('#phoneNumber').val();
        $('.iptInfo').addClass('hide');
        if (!isPhoneNumber(phone)) {
            tip('手机号码格式不正确，应该为11位数字。');
        } else {
            $('.mask').addClass('hide');
            exports.http.got(couponId, phone);
        }
    });

    $('.f_search_cur').on('click', function(){
        $('.pages').addClass('hide');
        $('#search_content').css({"top": $(document).height() + "px"});
        $('#search_content').removeClass('hide').animate({"top": "0"}, 200, function(){
            //$('#search_content').css({"position":"fixed"});
            $('#search_content .s').css({"position":"fixed"});
            $('#search_ipt').focus();
        });
    });
    
    $('#search-shopname').on('touchstart', function(){
      $('#search_ipt').blur();
    });

    $('#search-result dl').on('touchstart', function(){
      $('#search_ipt').blur();
    });
    
    $('#search_cancel').on('click', function(){
        $('#search_content .s').css({"position":"absolute"});
        $('#search_content').animate({"top": $(document).height() + "px"}, 200, function(){
            $('#search_ipt').val('');
            $('#choose').removeClass('hide');
            $('#search-shopname, #search-result').empty('');
            $('#search_content').addClass('hide');
            //$('#search_content').css({"position":"absolute"});
            $('.conMain').masonry('layout');
            exports.useScroll();
        });

    });

    $('#postsearch').attr('action', $('#postsearch').attr('action').replace('{id}', exports.req.id).replace('{city}', exports.req.cityId || 0));

    var timer = null, local = 0;
    $('#search_ipt').on('input', function(){
        var name = $('#search_ipt').val();
        if ( timer ) clearTimeout(timer);
        timer = setTimeout(function(){
            var t = new Date().getTime();
            local = t;
            exports.http.shopName(name).then(function (msg) {
                if ( t === local ) {
                    var d = msg.d;
                    if (d && d.length) {
                        msg = JSON.parse(d);
                    }
                    else {
                        msg = [];
                    }
                    exports.nodebone('search-shopname', {list: msg});
                    $('#search-shopname').find('li:not(.none)').on('click', function () {
                        var val = $(this).text();
                        $('#search_ipt').val(val);
                        $('#search-shopname').addClass('hide');
                        $('#postsearch').find("input[type='submit']").trigger('click');
                    });
                }
            });
        }, 300);
    });

    $('#postsearch').ajaxForm({
        dataType: 'json',
        success: function(msg){
          $('#search_ipt').blur();
            if ( msg.error === 0 ){
                exports.buildShop(msg.msg);
                $('#search-shopname').addClass('hide');
            }else{
                tip('查询出错');
            }
        }
    });
    
    exports.http.list['1'] = function(response){
        exports.wxshare = false;

        if ( response.isGot ){
            response.shareState = 200;
            response.used = !!response.isGot;
            exports.http.list['200'](response);
            return;
        }

        if (!response.couponList || response.couponList.length === 0) {
            exports.http.list['2']();
            return;
        }

        exports.list.more = response.isMore;
        exports.list.data = exports.list.data.concat(response.couponList);
        exports.reload(response.couponList);
    };

    exports.http.list['500'] = exports.http.got['500'] = function(err){
        // 500 error.
        if ( err ){
            tip(err.message || err.Message || '服务器出错');
        }
    };

    exports.http.list['2'] = exports.http.list['3'] = function(response) {
        if (response.isGot) {
            exports.http.list['200']({});
        } else {
            $('#loading').addClass('hide');
            $('.pages').addClass('hide');
            $('#gameover').removeClass('hide');
            exports.wxshare = false;
        }
    };

    exports.http.list['200'] = function(response){
        if ( !response ) response = {};
        exports.http.info().then(function(msg){
            $(window).off('scroll');
            msg = JSON.parse(msg.d);
            msg = $.extend(response, msg);
            if ( exports.qr ) { msg.delay = true; }

            if (!msg.phone) msg.phone = exports.phone;
            $('.pages').addClass('hide');
            var template = document.getElementById('got').innerHTML.replace(/\&lt\;/ig, '<').replace(/\&gt\;/ig, '>');
            var html = series.nodebone.compile( template, { delimiter: '%' } )({ main: msg });
            $('#got').html(html).removeClass('hide');
            $(window).scrollTop(0);
            exports.masonry('.otherMenu');

            $('body').on("click", ".downbtn", function() {
                window.location.href = "http://a.app.qq.com/o/simple.jsp?pkgname=va.dish.sys";
            });

            $('#docrectphone').on('click', function() {
                exports.stopScroll();
                $('.mask').css({ "height": +document.body.scrollHeight + "px" });
                $('.mask').removeClass('hide');
                $('.msg').removeClass('hide');
                $('.msg .current').removeClass('hide');
                //$('#currentNumber').val( $('#nowNumber span').html() );
            });

            $('#confirmCur').on('click', function() {
                exports.useScroll();
                var value = $('#currentNumber').val();
                var nowPhoneNumber = $('#nowNumber span').html();
                if (!isPhoneNumber(value)) {
                    tip('手机号码格式错误');
                    return;
                }
                if(value === nowPhoneNumber){
                    tip('请勿输入相同的手机号码');
                    return;
                }
                msg.phone = value;
                $('.mask').addClass('hide');
                $('.msg').addClass('hide');
                exports.http.correct(msg).then(function(response){
                    response = JSON.parse(response.d);
                    if (response.correctState === 0) {
                        tip('修改失败，请重试');
                    }
                    else if (response.correctState === 1){
                        msg.IsCorrected = true;
                        exports.cookiephone = msg.phone ;
                        exports.phone = msg.phone;
                        var html = series.nodebone.compile( template, { delimiter: '%' } )({ main: msg });
                        $('#got').html(html).removeClass('hide');
                    }
                    else if (response.correctState === 3) {
                        tip('该号码已经纠错过了');
                    }
                    else if (response.correctState === 2) {
                        tip('该号码领过了');
                    }
                    else{
                        tip('未知错误');
                    }
                })['catch'](function(){
                    tip('网络连接失败，请检查您的网络。');
                });
            });

            $('#cencelCur').on('click', function() {
                exports.useScroll();
                $('.mask').addClass('hide');
                $('.msg').addClass('hide');
            });
        })['catch'](function(e){
            tip('服务器崩溃');
        });
    };

    exports.http.got['0'] = function(response){
        tip('领取失败');
    };

    exports.http.got['1'] = function(response){
        exports.phone = response.phoneNumber;
        exports.cookiephone = exports.phone;
        exports.http.list['200']();
    };

    exports.http.got['2'] = function(response){
        exports.http.list['2']();
    };

    exports.http.got['3'] = function(response){
        response.used = true;
        exports.phone = response.phone = response.phoneNumber;
        exports.http.list['200'](response);
    };

    if ( exports.qr && !exports.version.android ){ $("#tabs li[data-value='nearly']").trigger('click'); }
    else{ $("#tabs li[data-value='hot']").trigger('click'); }

    function isPhoneNumber(phoneNum) {
        var partten = /^1[3|4|5|7|8]\d{9}$/;
        if (partten.test(phoneNum)) {
            return true;
        } else {
            return false;
        }
    }

    function tip(t) {
        $('.mask,.msg').removeClass('hide');
        $('.msg .msgcon').removeClass('hide').html(t);
        $('.msg .current').addClass('hide');
        setTimeout(function() {
            $('.mask,.msg .msgcon,.msg .current').addClass('hide');
        }, 2000);
    }

});