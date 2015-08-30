// JavaScript Document
(function($) {
    /**
    * Get url request vars.
    */
    var exports = new Object();
    window.isShowWeiXinShareButton = true;

    /**
    * cookie 方法简化
    * cookie.set(key,value)
    * cookie.get(key)
    */
    var cookie = {
        set: function(key, value) {
            $.cookie(key, value, {
                expires: new Date('3015/01/01 00:00:00')
			, path: '/'
            });
        },
        get: function(key) {
            return $.cookie(key)
        }
    };

    // location extendtion.
    exports.search = this.location.search.replace(/^\?/, '');

    // url parse.
    exports.req = series.QueryString.parse(exports.search);

    // get common phone number.
    exports.phone = (exports.req.phoneNumber && exports.req.phoneNumber.length > 0 ? exports.req.phoneNumber : null) || cookie.get('mobilePhoneNumber') || '';

    exports.wxmenu = true;

    Object.defineProperty(exports, 'wxmenu', {
        set: function(val) { window.isShowWeiXinShareButton = true; }
	, get: function() { return window.isShowWeiXinShareButton; }
    });

    /**
    * share data saving.
    * share: {
    *	 	data: null,
    *		page: 1
    * }
    */
    exports.share = {
        "1": {
            data: null,
            page: 1,
            more: true
        },
        "2": {
            data: null,
            page: 1,
            more: true
        },
        "3": {
            data: null,
            page: 1,
            more: true
        }
    }

    exports.shareDefine = {
        set: function(which, data) {
            if (series.isNumber(data)) {
                exports.share[which].page = data;
            }
            else if (series.isBoolean(data)) {
                exports.share[which].more = data;
            }
            else {
                exports.share[which].data = data;
            }
        },
        get: function(which) {
            return exports.share[which]
        }
    }

    exports.shareType = 2;
    exports._doing = false;
    exports.qr = false;

    Object.defineProperty(exports, 'doing', {
        set: function(val) {
            exports._doing = val;
            if (!val) {
                $('.doload').addClass('hide');
            } else {
                $('.doload').removeClass('hide');
            }
        }
	, get: function() { return exports._doing; }
    })

    /**
    *
    * Share coupon.
    *
    * @ data: 
    *		preOrder19DianIdEncrypt: exports.req.id
    *		phoneNumber: exports.phone
    *		pageSize: 10
    *		pageIndex: exports.req.page || 1
    *
    * @ return api private
    *
    */
    var share = function(dist) {
        if (exports.doing) return;
        exports.doing = true;
        var urls = '/coupon/CouponMsg.aspx/ShareCoupon';
        if (Number(exports.req.shareType) == 2) {
            urls = '/coupon/CouponMsg.aspx/ShareSystemCoupon';
        };

        var args = {
            "preOrder19DianIdEncrypt": exports.req.id,
            "phoneNumber": exports.phone,
            "pageSize": 10,
            "pageIndex": exports.shareDefine.get(exports.shareType).page,
            "displayType": exports.shareType,
            "longitude": exports.lot || 0,
            "latitude": exports.lat || 0
        };

        if (Number(exports.req.shareType) == 2) {
            args = {
                "activityIdEncrypt": exports.req.id,
                "cityId": exports.req.cityId,
                "pageSize": 10,
                "pageIndex": exports.shareDefine.get(exports.shareType).page,
                "displayType": exports.shareType,
                "longitude": exports.lot || 0,
                "latitude": exports.lat || 0,
                "phoneNumber": exports.phone
            }
        };

        $.ajax({
            contentType: "application/json",
            url: urls,
            type: 'POST',
            dataType: 'json',
            data: JSON.stringify(args),
            success: function(msg) {
                msg = JSON.parse(msg.d);
                var status = msg.shareState;

                // 微信相关
                window.shareTitle = msg.shareTitle;
                window.shareImage = msg.shareImage;
                window.shareContent = msg.shareText;

                if (!exports.bindwx) {
                    exports.bindwx = true;
                    weixinShare();
                }

                if ( dist ){
                    msg.dist = dist;
                }

                exports.shareDefine.set(exports.shareType, msg.isMore);

                if (share[status]) { share[status](msg); }
                else { share['500'](msg); };
                exports.doing = false;
                if (!exports.qr && Number(exports.req.shareType) == 2) {
                    exports.qr = true;
                    exports.shareType = 3;
                    $(".checking li[data-value='3']").trigger('click');
                }
            },
            error: function(e) {
                exports.doing = false;
                share['500']({ message: '网络连接失败，请检查您的网络。' });
            }
        });
    };

    var Got = function(couponId, phone) {
        var args = { "couponId": couponId, "preOrder19DianIdEncrypt": exports.req.id, "phoneNumber": phone };
        var urls = '/coupon/CouponMsg.aspx/GetCoupon';
        if (Number(exports.req.shareType) == 2) {
            args.activityIdEncrypt = exports.req.id;
            delete args.preOrder19DianIdEncrypt;
            urls = '/coupon/CouponMsg.aspx/GetSystemCoupon';
        }
        $.ajax({
            contentType: "application/json",
            url: urls,
            type: 'POST',
            dataType: 'json',
            data: JSON.stringify(args),
            success: function(msg) {
                msg = JSON.parse(msg.d);
                var status = msg.getState;

                if (Got[status]) { Got[status](msg); }
                else { Got['500'](msg); };
            },
            error: function() {
                Got['500']({ message: '网络连接失败，请检查您的网络。' });
            }
        });
    }

    var detail = function() {
        return new Promise(function(resolve, reject) {
            $.ajax({
                contentType: "application/json",
                url: '/coupon/CouponMsg.aspx/GetCouponDetail',
                type: 'POST',
                dataType: 'json',
                data: JSON.stringify({ "preOrder19DianIdEncrypt": exports.req.id, "phoneNumber": exports.phone }),
                success: function(msg) {
                    msg = JSON.parse(msg.d);
                    resolve(msg)
                },
                error: function(e) {
                    reject(e);
                    tip('服务端出错');
                }
            });
        });
    }

    var correct = function(response) {
        $.ajax({
            contentType: "application/json",
            url: '/Coupon/CouponMsg.aspx/CorrectNumber',
            type: 'POST',
            dataType: 'json',
            data: JSON.stringify({ "couponGetDetailId": response.couponGetDetailId, "phoneNumber": response.phone }),
            success: function(msg) {
                msg = JSON.parse(msg.d);
                if (msg.correctState === 0) {
                    tip('修改失败，请重试');
                } else if (msg.correctState === 1) {
                    response.IsCorrected = true;
                    cookie.set('mobilePhoneNumber', response.phone);
                    exports.phone = response.phone;
                    response.shareState = 200;
                    share['200'](response);
                } else if (msg.correctState === 2) {
                    tip('该号码领过了');
                } else {
                    tip('未知错误');
                }
            },
            error: function(e) {
                tip('网络连接失败，请检查您的网络。');
            }
        });
    }

    /**
    *
    * Open GPS.
    * check to use location on phone.
    * return a Promise object.
    *
    *		% success: latitude and longitude join a json object.
    *		% failure: get an error json object.
    *
    * @ api public
    *
    */
    exports.GPS = function() {
        if (Number(exports.req.shareType) == 2) {
            $('.conMain').addClass('hide');
        }
        return new Promise(function(resolve, reject) {
            var _resolve = function(position) {
                resolve({
                    lat: position.coords.latitude
				, lot: position.coords.longitude
                });
            };

            var _reject = function(error) {
                switch (error.code) {
                    case 1: error.message = "位置服务被拒绝。"; break;
                    case 2: error.message = "暂时获取不到位置信息。"; break;
                    case 3: error.message = "获取位置信息超时。"; break;
                    default: error.message = "未知错误。";
                };

                reject(error);
            };

            navigator.geolocation.getCurrentPosition(_resolve, _reject, {
                timeout: 5000
			, enableHighAccuracy: true
            });
        });
    };

    // the main template's id
    exports.id = 'main';

    // ejs options
    exports.options = { delimiter: '%' };

    // local datas.
    exports.locals = {};

    // page order.
    exports.locals.page = 'loading';

    // page data.
    exports.locals.main = {};

    /**
    *
    * Use page init.
    * push [refresh] to node.
    * set [defineProperty] on node.scope
    *
    *		% success: latitude and longitude join a json object.
    *		% failure: get an error json object.
    *
    * @ api public
    *
    */
    exports.init = function() {
        exports.node = document.getElementById(exports.id);
        exports.node.refresh = function(data) {
            if (data) exports.locals = data;
            exports.node.innerHTML =
				series.nodebone.compile(
					series.nodebone.templatecache[exports.id]
				, exports.options
			)(exports.locals);
        };
        Object.defineProperty(exports.node, "scope", {
            get: function() { return exports.locals; },
            set: function(val) { exports.node.refresh(val); }
        });
    }

    /**
    *
    * Dom ready and nodebone init ready.
    * install new template on node.
    * @ api private
    *
    */
    series.nodebone.ready(function() {
        $('#main').removeClass('hide');
        exports.init();
        series.nodebone.load(exports.id, exports.locals, exports.options);
        share();
        onShopEvent();
    });

    share['200'] = function(response) {
        detail().then(function(msg) {
            msg = extend(response, msg);
            if (!msg.phone) msg.phone = exports.phone;
            exports.node.scope = { page: 'got', main: msg };
            $(window).scrollTop(0);

            masonry('.otherMenu');

            $(".downbtn").on("click", function() {
                window.location.href = "http://a.app.qq.com/o/simple.jsp?pkgname=va.dish.sys";
            });
            $('#docrectphone').on('click', function() {
                noScroll();
                $('.mask').css({ "height": +document.body.scrollHeight + "px" });
                $('.mask').removeClass('hide');
                $('.msg').removeClass('hide');
                $('.msg .current').removeClass('hide');
                //$('#currentNumber').val( $('#nowNumber span').html() );
            });
            $('#confirmCur').get(0).cb = function() {
                var value = $('#currentNumber').val();
                if (!isPhoneNumber(value)) {
                    tip('手机号码格式错误');
                    return;
                }
                msg.phone = value;
                $('.mask').addClass('hide');
                $('.msg').addClass('hide');
                correct(msg);
            }

            $('#confirmCur').on('click', function() {
                canScroll();
                this.cb();
            });

            $('#cencelCur').on('click', function() {
                canScroll();
                $('.mask').addClass('hide');
                $('.msg').addClass('hide');
            });
        });
    }

    share['1'] = function(response) {
        exports.wxmenu = false;
        if (response.isGot) {
            response.shareState = 200;
            response.used = response.isGot ? true : false;
            share['200'](response);
            return;
        }

        if (!response.couponList || response.couponList.length === 0) {
            share['2']({});
            return;
        }

        response.shareType = exports.shareType;

        var a = [];
        response.couponList.forEach(function(b, i){
            a[i] = b;
        });

        var data = exports.shareDefine.get(exports.shareType).data;
        if (data) {
            response.couponList = data.couponList.concat(response.couponList);
        }

        exports.shareDefine.set(exports.shareType, response);

        if ( !response.dist ) {
            exports.node.scope = {
                page: 'choose',
                main: exports.shareDefine.get(exports.shareType).data
            }
            onmenu();
            scroller();
        }else{
            var x = [];
            var _masonry = $('.conMain').data('masonry');

            a.forEach(function(c){
                x.push(new Promise(function( resolve, reject ){
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
                    if ( c.isGot ){
                        $(el).addClass('shopEndPic');
                    };


                    var img = new Image();
                    img.onload = img.onerror = function(){
                        $('.conMain').append(el);
                        var dl = $(el).find('dl')[0];
                        _masonry.appended(el);
                        resolve(dl);
                    };
                    img.src = c.shopLogo;
                }));
            });

            Promise.all(x).then(function(z){
                _masonry.reloadItems();
                _masonry.layout();
            });
        }

    };

    share['2'] = share['3'] = function(response) {
        if (response.isGot) {
            share['200']({});
        } else {
            exports.node.scope = { page: 'gameover', main: {} };
            exports.wxmenu = false;
        }
    }

    share['500'] = function() {
        tip('网络连接失败，请检查您的网络。');
    }

    Got['1'] = function(response) {
        exports.phone = response.phoneNumber;
        cookie.set('mobilePhoneNumber', exports.phone);
        share['200']({});
    }
    Got['0'] = function(response) {
        tip('领取失败');
    }
    Got['2'] = function(response) {
        // 已过期
        share['2']();
    }
    Got['3'] = function(response) {
        // 已领过
        response.used = true;
        exports.phone = response.phone = response.phoneNumber;
        share['200'](response);
    }
    Got['500'] = share['500'];

    function onShopEvent() {
        var h = $('.checking').outerHeight();
        $('.conMain').css('margin-top', h + 'px');
        $('body').on('click', '.shop', function() {
            if ( $(this).hasClass('shopEndPic') ) return;
            var shopName = $(this).find('.shopname').html();
            var shopAddress = $(this).find('.shopADD').html();
            var disc = $(this).find('.discountInfo').html();
            var couponId = $(this).data('cid');
            var w = shopName + '(' + shopAddress + ') ' + disc;
            noScroll();
            $('.mask').css({ "height": +document.body.scrollHeight + "px" });
            $('.mask').removeClass('hide');
            $('.iptInfo').removeClass('hide');
            $('.iptInfo').find('.title').html(w);
            $('#phoneNumber').attr('couponId', couponId).val(exports.phone || '').focus();
        });
        $('body').on('click', '#cencel', function() {
            canScroll();
            $('.mask').addClass('hide');
            $('.iptInfo').addClass('hide');
        });
        $('body').on('click', '#confirm', function() {
            canScroll();
            var couponId = $('#phoneNumber').attr('couponId');
            var phone = $('#phoneNumber').val();
            $('.iptInfo').addClass('hide');
            if (!isPhoneNumber(phone)) {
                tip('手机号码格式不正确，应该为11位数字。');
            } else {
                $('.mask').addClass('hide');
                Got(couponId, phone);
            }
        });
    }

    function onmenu() {
        masonry();
        //$('.shop dl').addClass('fadeIn');
        $('.checking li').off('click');
        $('.checking li').on('click', function() {
            $('.checking li').removeClass('chkCur');

            var id = $(this).data('value'), data, self = this;
            if (Number(id) == 3) {
                exports.GPS().then(function(x) {
                    if (Number(exports.req.shareType) == 2) {
                        $('.conMain').removeClass('hide');
                    }
                    $(self).addClass('chkCur');
                    $(window).scrollTop(0);
                    exports.shareType = 3;
                    exports.lot = x.lot;
                    exports.lat = x.lat;
                    data = exports.shareDefine.get(exports.shareType).data;
                    if (!data) {
                        share();
                    } else {
                        exports.node.scope = {
                            page: 'choose',
                            main: data
                        }
                        onmenu();
                    }
                })['catch'](function(e) {
                    tip('抱歉，无法为您定位到附近的餐厅。');
                    setTimeout(function() {
                        if (Number(exports.req.shareType) == 2) {
                            $('.conMain').removeClass('hide');
                        }
                    }, 2000);

                    $(".checking li[data-value='2']").trigger('click');
                });
            } else {
                $(self).addClass('chkCur');
                $(window).scrollTop(0);
                exports.shareType = Number(id);
                data = exports.shareDefine.get(exports.shareType).data;
                if (!data) {
                    share();
                } else {
                    exports.node.scope = {
                        page: 'choose',
                        main: data
                    }
                    onmenu();
                }
            }
        });
    }

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

    function scroller() {
        $(window).off('scroll');
        $(window).on('scroll', function() {
            var page = exports.shareDefine.get(exports.shareType).page;
            if ($(window).height() + $('body').scrollTop() + 10 > $('body').outerHeight() && !exports.doing && exports.shareDefine.get(exports.shareType).more) {
                exports.shareDefine.set(exports.shareType, page + 1);
                share(true);
            }
        });
    }

    function extend(a, b) {
        for (var i in b) {
            a[i] = b[i];
        }
        return a;
    }

    function masonry(x, a) {
        if (!x) x = '.conMain';
        if (!a) a = 'dl';
        var $container = $(x);
        $container.masonry('destroy');
        $container.imagesLoaded(function() {
            $container.masonry({
                itemSelector: a,
                gutterWidth: 30,
                isAnimated: false
            });
            console.log($container.data('masonry'))
        });
    }

    var doScroll = function(e) {
        e.preventDefault();
    }

    function noScroll() {
        document.documentElement.addEventListener('touchmove', doScroll);
    }
    function canScroll() {
        document.documentElement.removeEventListener('touchmove', doScroll);
    }

}).call(this, jQuery);