(function(){
	//http://work.cn/meal/?cookie=3212cf07-3f63-4987-8f0b-b8d1c33182e2635194386347964375&mobile=18507385766&cityId=87
	window.openview = function(url, compile, argc){
		return new Promise(function( resolve, reject ){
			$('html').addClass('anmating');
			var section = document.createElement('section');
			var box = document.createElement('div');
			$('.main').append(section);
			$(section).append(box);
			$(box).addClass('section-box');
			if ( argc ){
				for ( var i in argc ){
					section[i] = argc[i];
				}
			}
			$(section).velocity({translateX: '100%'}, 0);
			$(box).load(url, function(){
				if ( compile ){
					require(compile, function(Factorys){
						if ( typeof Factorys === 'function' ){
							new Factorys(section, function(){
								$(section).velocity({translateX: '0'}, 'fast', function(){
									$('html').removeClass('anmating');
									//$('section').hide();
									//$(section).show();
									resolve(section);
								});
							});
						}else{
							//$('section').hide();
							//$(section).show();
							resolve(section);
						}
					})['catch'](function(){ reject(section); });
				}else{
					$(section).velocity({translateX: '0'}, 'fast', function(){
						$('html').removeClass('anmating');
						//$('section').hide();
						//$(section).show();
						resolve(section);
					});
				}
			});
		});
	};
	
	window.closeview = function(deep){
		if ( deep === -1 ){
			$('section').show();
			$('section:last').velocity({translateX: '100%'}, 'fast', function(){
				$(this).remove();
				//$('section').hide();
				//$('section:last').show();
			});
		}else if ( deep === 1 ){
			$('section').show();
			$('section:not(:first)').velocity({translateX: '100%'}, 'fast', function(){
				$(this).remove();
				//$('section').hide();
				//$('section:first').show();
			});
			//$('section:first').trigger('refresh');
		}
	};
	
	var main = new Class(function(){
		
		var that = this;
		this.req = this.createServer();
		this.ev = this.dev();
		this.ios = this.ev.iOS;
		this.aos = this.ev.Android;
		
		var mobile, cookie;
		if (!navigator.userAgent.match(/MicroMessenger/i)) {
            try {
                if (this.req.mobilephone && this.req.mobilephone.length > 0) {
                    mobile = this.req.mobilephone;
                } else if ($.cookie("mobilephone") != null) {
                    mobile = $.cookie("mobilephone");
                }
            } catch (e) { }
            cookie = this.req.cookie;
        } else {
            if ($.cookie("mobilephone") != null) {
                mobile = $.cookie("mobilephone");
            }
        };
		this.req.mobile = mobile;
		this.req.cookie = cookie;

		this.ajax('checkLogin',{
			mobile: this.req.mobile,
			cookie: this.req.cookie
		}).then(function(xhr){
			if ( xhr.error === 0 ){
				window.openview('storeList.html', 'compiles/list.js');
			}
			else if ( xhr.error === -2 ){
				that.error();
			}
			else{
				that.error('出错啦', xhr.msg);
			}
		})['catch'](function(e){
			that.error('出错啦', '服务君跪了');
		});
	});
	
	main.add('open', function(type, value){
		if ( this.aos ){
			window.redEnvelopeShare.getRecomandTopicsValue('{ "type": "' + type + '", "value": "' + value + '" }');
		}else if ( this.ios ) {
			window.location.href = "message:type:" + type + ",value:" + value;
		}
	});
	
	main.add('dev', function(){
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
	
	main.add('ajax', function(method, data){
		data = data || {};
		return new Promise(function( resolve, reject ){
			data.m = method;
			$.ajax({
				url: 'mealHandler.ashx',
				data: data,
				dataType: 'json',
				success: resolve,
				error: reject
			});
		});
	});
	
	main.add('createServer', function () {
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
	
	main.add('error', function(msg1, msg2){
		return window.openview('chklogin.html', 'compiles/error.js').then(function(){
			if ( msg2 ){
				$('#errortit').html(msg1);
				$('#errormsg').html(msg2);
			}
		});
	});
	
	define([], function(){ return main; });
})();