// JavaScript Document
define(function(require, exports, module){
	var project = new Class();
	
	project.add('initialize', function(){
		this.device = this.getDevices();
		this.onBindOrder();
	});
	
	project.add('getDevices', function(){
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
	
	project.add('onBindOrder', function(){
		var that = this;
		$('body').on('click', '.app-order', function(){
			var value = $(this).attr('app-value'),
				type = $(this).attr('app-type');
	
			if (that.device.Android) {
                    window.redEnvelopeShare.getRecomandTopicsValue(JSON.stringify({ type: type, value: value }));
			}
			else if (that.device.iOS) {
				window.location.href = "message:type:" + type + ",value:" + value;
			}
		})
	});
	
	return project;
});