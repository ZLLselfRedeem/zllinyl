var OJS = new ojs({ cache: true });
var req = window.location.href.split('?');
OJS.render('msg', {msg: ''});
if ( req.length === 2 ){
	req = _.fromQuery(req[1]);
}else{
	req = {};
};

var cookie = {
	set: function(key, value){
		$.cookie(key, value, {
			expires: new Date('3015/01/01 00:00:00'),
			path : '/'
        });
	},
	get: function(key){
		return $.cookie(key)
	}
};

var app = {
    init: function() {
        has = true, p = 1;

        ojs.__cache('get');
        ojs.__cache('conlist');
        ojs.__cache('gettedContent');
        ojs.__cache('foodDiscountList');
        ojs.__cache('tc');

        getList(1, function() {
            $(window).on('scroll', function() {
                if (has) {
                    if ($(window).height() + $('body').scrollTop() + 50 > $('body').outerHeight() && doing === false) {
                        console.log('scroll')
                        getList(p + 1);
                    }
                }
            });
        });
    },

    getted: function(f, n) {
        checkgeted(n);
    },

    zget: function(msg) {
       
        msg.mobile = req.phoneNumber;
        document.getElementById('gettedContent').innerHTML = OJS.render('gettedContent', msg);
        document.getElementById('foodDiscountList').innerHTML = OJS.render('foodDiscountList', msg);
        if (msg.IsCorrected) {
            $('#correct').css({ "display": "none" });
        } else {
            $('#correct').css({ "display": "inline-block" });
        }
        $('#get').addClass('hide');
        $('#getted').removeClass('hide');
        $('#over').addClass('hide');

        $("#gettedContent span").removeClass("hide");
        $("#foodDiscountList").removeClass("hide");

        $("#downloadBtn").on("click", function() {
            window.location = "http://a.app.qq.com/o/simple.jsp?pkgname=va.dish.sys";
        });

        $("#correct").on("click", function() {
            var temp = OJS.render('tc', { mobile: req.phoneNumber });
            var msg = OJS.render('msg', { msg: temp });
            document.getElementById('msg').innerHTML = msg;
            $(document.getElementById('msg')).removeClass('hide');
            $('#correctBg').fadeIn({ "display": "block" });
            $('#correctDiv').fadeIn({ "display": "block" });
        });

        req.couponGetDetailId = msg.couponGetDetailId;
        //$("#confirm").trigger('click', couponGetDetailId);
        $('body').scrollTop(0);
    },

    open: function(fromajax, n) {
        this.getted(fromajax, n);
    }
};

var doing = false, has, p;
var mp = cookie.get('mobilePhoneNumber');
req.phoneNumber = req.phoneNumber ? req.phoneNumber : (mp ? mp : '');

app.init();

function getList(page, callback){
	$("#tip").html("正在加载...");
	doing = true;

	$.ajax({
		contentType: "application/json",
		url: '/coupon/CouponMsg.aspx/ShareCoupon',
		type: 'POST',
		dataType: 'json',
		data: JSON.stringify({"preOrder19DianIdEncrypt": req.id, "phoneNumber": req.phoneNumber, "pageSize": 10, "pageIndex": page || 1}),
		success: function(msg) {
			msg = JSON.parse(msg.d);
			console.log(msg)
			msg.mobile = req.phoneNumber || '';
			$("#tip").html("上拉加载");
			if(msg.shareState == 1){//正常
			    //是否已经领过抵扣券
          
				if(msg.isGot){
					app.open(true, 3);
				}else{
				  
          /*修改couponList为空跳转已抢完页面       //*/
          if(msg.couponList == ''){ //
            $('#get').addClass('hide'); //
            $('#getted').addClass('hide');  //
            $('#over').removeClass('hide'); //
          }else{  //
            
  					if ( !document.getElementById('get').setup ){
  						document.getElementById('get').innerHTML = OJS.render('get', msg);
  						document.getElementById('get').setup = true;
  					};
  					document.getElementById('discountList').innerHTML += OJS.render('conlist', msg);
  					$('#get').removeClass("hide");
  					p = page;
  					
          } //
          
				}
				has = msg.isMore;
				doing = false;
				
				if(!has){
					$("#tip").html("");
				}
				
			}else if(msg.shareState == 2){//已抢完
				if ( msg.isGot ){
					app.open(true, 3);
				}else{
					$('#get').addClass('hide');
					$('#getted').addClass('hide');
					$('#over').removeClass('hide');
				}
			}else if(msg.shareState == 3){//已过期
				$('#get').addClass('hide');
				$('#getted').addClass('hide');
				$('#over').removeClass('hide');
			}
			$("#cancel").on("click", function(){
				$("#msg").addClass("hide");
				$('#correctBg').fadeOut({"display":"none"});
				$('#correctDiv').fadeOut({"display":"none"});
			});
			_.isFunction(callback) && callback();
		},
		error: function(e) {
			doing = false;
			console.log('服务端出错');
		}
	});

};

function getDiscount(couponId){
	
	var phoneNum = $('#phoneNumber').val();
	
	if( phoneNum == "请输入手机号码"){
		//alert("请输入手机号码");
		$("#phoneNumber").focus();
	}else{
	    if (isPhoneNumber(phoneNum)) {
	        $("#cancel").trigger('click', function() {
	            $.ajax({
	                contentType: "application/json",
	                url: '/coupon/CouponMsg.aspx/GetCoupon',
	                type: 'POST',
	                dataType: 'json',
	                data: JSON.stringify({ "couponId": couponId, "preOrder19DianIdEncrypt": req.id, "phoneNumber": phoneNum }),
	                success: function(msg) {
	                    console.log(msg)
	                    msg = JSON.parse(msg.d);

	                    if (msg.getState == 1) {

	                        req.phoneNumber = phoneNum;
	                        cookie.set('mobilePhoneNumber', req.phoneNumber);

	                        app.open(false, 1);
	                    } else if (msg.getState == 0) {
	                        $("#msg").removeClass("hide");
	                        $('#correctBg').show().animate({ opacity: 1, "margin-top": "50px" }, 'slow');
	                        $('#correctDiv').show().animate({ opacity: 1, "margin-top": "50px" }, 'slow');
	                        $('#correctDiv').html("<p class='currTitle'>领取失败</p>");
	                        setTimeout(function() {
	                            $('#correctBg').animate({ opacity: 0 }, 'slow', function() {
	                                $(this).hide();
	                            });
	                            $('#correctDiv').animate({ opacity: 0 }, 'slow', function() {
	                                $(this).hide();
	                            });
	                        }, 2000);

	                    } else if (msg.getState == 2) {
	                        $("#msg").removeClass("hide");
	                        $('#correctBg').show().animate({ opacity: 1, "margin-top": "50px" }, 'slow');
	                        $('#correctDiv').show().animate({ opacity: 1, "margin-top": "50px" }, 'slow');
	                        $('#correctDiv').html("<p class='currTitle'>抵扣券已抢完</p>");
	                        $("#discountli" + couponId).hide();
	                        setTimeout(function() {
	                            $('#correctBg').animate({ opacity: 0 }, 'slow', function() {
	                                $(this).hide();
	                            });
	                            $('#correctDiv').animate({ opacity: 0 }, 'slow', function() {
	                                $(this).hide();
	                            });
	                        }, 2000);

	                    } else if (msg.getState == 3) {
							 req.phoneNumber = phoneNum;
							 cookie.set('mobilePhoneNumber', req.phoneNumber);
							 app.open(false, 3);
	                        //window.location.href = "getted.html?couponId=" + couponId + "&id=" + req.id + "&phoneNumber=" + phoneNum + "&status=3";
	                    }

	                    setTimeout(function() {
	                        $('#correctBg').fadeOut();
	                        $('#correctDiv').fadeOut(function() {
	                            $("#msg").addClass("hide");
	                        });
	                    }, 2000);
	                },
	                error: function(e) {
	                    window.doing = false;
	                    console.log('服务端出错');
	                }
	            });
	        });
		}else{
			//alert('您输入的手机号码不正确！');
			$("#phoneNumber").focus();
		}
	}
}

function isPhoneNumber(phoneNum){
	var partten = /^1[3|4|5|7|8]\d{9}$/;
	if(partten.test(phoneNum)){
		return true;
	}else{
		return false;
	}
}

function checkgeted(n){
    $.ajax({
        contentType: "application/json",
        url: '/coupon/CouponMsg.aspx/GetCouponDetail',
        type: 'POST',
        dataType: 'json',
        data: JSON.stringify({ "couponId": req.couponId, "preOrder19DianIdEncrypt": req.id, "phoneNumber": req.phoneNumber }),
        success: function(msg) {
            msg = JSON.parse(msg.d);
            console.log(msg);
            req.status = n;
            msg.status = req.status ? Number(req.status) : 0;

            //是否已领抵扣券
            //if(msg.isGot){
            app.zget(msg);

            //}else{
            //window.location.href = "index.html?couponId=2&preOrder19DianId=70&phoneNumber=";
            //}

        },
        error: function(e) {
            window.doing = false;
            console.log('服务端出错');
        }
    });
}

$("body").on("click", '#confirm', function(ev, couponGetDetailId) {
    var phoneNum = $('#newPhoneNumber').val();
    if (phoneNum == "请输入手机号码") {
        alert("请输入手机号码");
    } else {
        if (isPhoneNumber(phoneNum)) {

            $("#cancel").trigger('click', function() {
                $.ajax({
                    contentType: "application/json",
                    url: '/Coupon/CouponMsg.aspx/CorrectNumber',
                    type: 'POST',
                    dataType: 'json',
                    data: JSON.stringify({ "couponGetDetailId": req.couponGetDetailId, "phoneNumber": phoneNum }),
                    success: function(msg) {
                        msg = JSON.parse(msg.d);
                        if (msg.correctState === 0) {
                            $('#correctBg').show().animate({ opacity: 1, "margin-top": "50px" }, 'slow');
                            $('#correctDiv').show().animate({ opacity: 1, "margin-top": "50px" }, 'slow');
                            $('#correctDiv').html("<p class='currTitle'>修改失败，请重试</p>");
                            setTimeout(function() {
                                $('#correctBg').animate({ opacity: 0 }, 'slow', function() {
                                    $(this).hide();
                                });
                                $('#correctDiv').animate({ opacity: 0 }, 'slow', function() {
                                    $(this).hide();
                                });
                            }, 2000)

                        } else if (msg.correctState === 1) {
                            $("#confirmPhoneNumber").html(phoneNum);
                            $('#correct').css({ "display": "none" });
                            cookie.set('mobilePhoneNumber', phoneNum);
                        } else if (msg.correctState === 2) {
                            $('#correctBg').show().animate({ opacity: 1, "margin-top":"50px" }, 'slow');
                            $('#correctDiv').show().animate({ opacity: 1, "margin-top": "50px" }, 'slow');
                            $('#correctDiv').html("<p class='currTitle'>该号码领过了</p>");
                            setTimeout(function() {
                                $('#correctBg').animate({ opacity: 0 }, 'slow', function() {
                                    $(this).hide();
                                });
                                $('#correctDiv').animate({ opacity: 0 }, 'slow', function() {
                                    $(this).hide();
                                });
                            }, 2000);
                        }
                    },
                    error: function(e) {
                        window.doing = false;
                        console.log('服务端出错');
                    }
                });
            });
        } else {
            alert('您输入的手机号码不正确！');
        }
    }
});

$("body").on("click", "#cancel", function(ev, callback) {
    $('#correctBg').animate({ opacity: 0 }, 'slow', function() {
        $(this).hide();
    });
    $('#correctDiv').animate({ opacity: 0 }, 'slow', function() {
        $(this).hide();
        callback();
    });
});