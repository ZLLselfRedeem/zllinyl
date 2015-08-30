/*!
 * U-xian RedEnvelope Detail Lists.
 * RequireJs and OJS engine module build.
 * By evio shen evio@vip.qq.com 
 * Views: http://webkits.cn
 * Series web application: http://series.webkits.cn
 * 2015/02/10 [ 'jquery', 'cookie', 'ojs' ]
 */
 
define(['jquery', 'cookie', 'ojs'], function($, cookie, OJS){
	// 模板缓存
	OJS.__cache('content');
	OJS.__cache('template');
	
	// 获取请求参数
	var req = window.location.href.split('?');
	if ( req.length === 2 ){ req = _.fromQuery(req[1]); }
	else{ req = {}; };
	
	// 初始化变量
	var 
		size = 10,
		isMore = false,
		ajaxSending = false,
		current = 1,
		clickType = 'present',
		space = 10,
		mobile = req.mobilePhone,
		ojs = new OJS({ cache: true, open: '<#', close: '#>' });
	
	// 页面初始化
	var init = function(){
		ajax('GetCustomerRedEnvelope', { mobilePhoneNumber: mobile }, function(msg){
			$('#content').html(ojs.render('content', JSON.parse(msg.d))).show();
			$('.getLists').on('click', function(){
				if ( ajaxSending ) return;
				$('.selected').removeClass('selected');
				$(this).addClass('selected');
				$('#redEnvelope').empty(); 
				if ( $(this).attr('data-type') === 'history' ){
					$('#remind').css('visibility', 'hidden');
				}else{
					$('#remind').css('visibility', 'visible');
				}
				GetLists($(this).attr('data-type')); 
			});
			$('.getLists.selected').trigger('click');
		});
	};
	
	windowScroll(); init();
	
	function ajax(method, data, callback){
		$.ajax({
			contentType: "application/json",
			url: 'list.aspx/' + method,
			type: 'POST',
			dataType: 'json',
			data: JSON.stringify(data),
			async: true,
			success: callback,
			error: function(){ /*window.location.href = "bang.html";*/ }
		});
	};
	
	function GetLists(type, page){
		if ( !page ){ page = 1; };
		if ( page < 1 ){ page = 1; };
		// send ajax
		$('.tip').html('正在加载...');
		if ( ajaxSending ) return;
		ajaxSending = true;
		ajax(
			'GetWebRedEnvelopeDetail', 
			{ mobilePhoneNumber: mobile, pageIndex: page, pageSize: size, type: type },
			function(msg){
				msg = JSON.parse(msg.d);
				ajaxSending = false; isMore = msg.isHaveMore; current = page; clickType = type;
				$('#redEnvelope').html($('#redEnvelope').html() + ojs.render('template', msg));
				$('.tip').empty();
				if ( !isMore ){ $('.tip').html(''); };
			}
		);
	};
	
	function windowScroll(){
		$(window).on('scroll', function(){
			var scrollTop = $('body').scrollTop(),
				windowHeight = $(window).height(),
				bodyHeight = $('body').outerHeight();

			if ( scrollTop + windowHeight + space >= bodyHeight && isMore && !ajaxSending ){
				GetLists(clickType, current + 1);
			}
		});
	};
});