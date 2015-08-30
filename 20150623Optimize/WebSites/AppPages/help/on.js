(function(){
	// 判断环境
    
	
	function getDevices() {
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
	
	var device = getDevices();
	
	var inWx = navigator.userAgent.match(/MicroMessenger/i); // wei xin
    var inAp = device.iOS; // ios
    var inAn = device.Android; // android
	
	$('.btn-zan').on('click', function(){
		var type = $(this).attr('data-type'),
			value = $(this).attr('data-value');
		
		// 暂时不做
		if ( inAp ){
			//window.location = "message:type:0,value:showShareLayout";
		}
		else if ( inAn ){
			window.redEnvelopeShare.getRecomandTopicsValue('{ "pageindex": "' + type + '", "type": "' + value + '" }');
		}
	});
	$('.benotice').on('click', function(){
		if ( inAp ){
			window.location = "message:type:99,value:clickAttention";
		}
		else if ( inAn ){
			window.redEnvelopeShare.getRecomandTopicsValue('{ "pageindex": "wx", "type": "notice" }');
		}
	});
	
	$('.becopy').on('click', function(){
		if ( inAp ){
			window.location = "message:type:98,value:clickCopy";
		}
		else if ( inAn ){
			window.redEnvelopeShare.getRecomandTopicsValue('{ "pageindex": "wx", "type": "copy" }');
		}
	});
})();