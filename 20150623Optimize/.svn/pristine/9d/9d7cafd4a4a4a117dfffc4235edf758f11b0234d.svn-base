var shareTitle = "悠先点菜，吃de愉快"
	,	shareImage = ""
	,	shareContent = ""
	,	href = window.location.href
	,	url = href.split('?')[0]
	,	searcher = href.split('?')[1]
	,	thisUrl = url.split('/').slice(0, -1).join('/') + '/v3.html' + (searcher ? '?' + searcher : '');
	
//微信配置config
function weixinShare() {
	$.ajax({
		url: "/AppPages/ajax/wechatGongzhong.ashx",
		type: "post",
		dataType: "json",
		data: { m: 'config', url: thisUrl },
		success: function(data) {
			wx.config({
				debug: false, // 开启调试模式,调用的所有api的返回值会在客户端alert出来，若要查看传入的参数，可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。
				appId: data.appId, // 公众号的唯一标识
				timestamp: data.timestamp, // 生成签名的时间戳
				nonceStr: data.nonceStr, // 生成签名的随机串
				signature: data.signature, // 签名
				jsApiList: ['getNetworkType', 'checkJsApi', 'onMenuShareTimeline', 'onMenuShareAppMessage', 'onMenuShareWeibo', 'onMenuShareQQ'] // 必填，需要使用的JS接口列表，所有JS接口列表见附录2
			});
		},
		error: function() {}
	});
  
	weixinReady();
}

function weixinReady() {
  
	//微信ready
	wx.ready(function() {
		//微信 - 分享朋友
		wx.onMenuShareAppMessage({
			title: shareTitle,
			desc: shareContent,
			link: thisUrl,
			imgUrl: shareImage,
			trigger: function(res) {},
			success: function(res) {},
			cancel: function(res) {},
			fail: function(res) {}
		});
		
		//微信 - 分享朋友圈
		wx.onMenuShareTimeline({
			title: shareContent,
			link: thisUrl,
			imgUrl: shareImage,
			trigger: function(res) {},
			success: function(res) {},
			cancel: function(res) {},
			fail: function(res) {}
		});
	});
}