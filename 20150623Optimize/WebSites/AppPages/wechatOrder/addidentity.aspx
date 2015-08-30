<%@ Page Language="C#" AutoEventWireup="true" CodeFile="addidentity.aspx.cs" Inherits="AppPages_wechatOrder_addidentity" %>

<!DOCTYPE html>
<html>
<head>
    <title>手机验证</title>
	<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">
	<meta charset="utf-8" />
	<!--[if lt IE 9]>
		<script src="../js/html5shiv/html5shiv.js"></script>
	<![endif]-->
	<link rel="stylesheet" type="text/css" href="../css/jquery.mobile.min.css" />
	<link rel="stylesheet" type="text/css" href="../css/common.css" />
	<script src="../js/jquery.js"></script>
	<script src="../js/jquery.cookie.js"></script>
	<script src="../js/jquery.mobile.min.js"></script>
	<script src="../js/app/mobile.js"></script>
	<script src="../js/app/mobile.page.js"></script>
	<style type="text/css">
		body{
			background-color:#f4f3ea;
		}
		.ui-page{
			background-color:#f7f7f7;
		}
		#msg,#msg-2{
			color:#ffffff;
			text-align:center;
			font-size:1.3rem;
		}
	</style>
</head>
<body>
    <div class="identity" data-role="page" data-module="identity" data-channel="identity" data-title="手机验证">
		<img src="../img/pic_2.png" width="100%" />
		<article class="content" role="main">
			<p>
				<input type="number" id="phoneNumber" class="inputText" placeholder="请输入您的手机号码" />
			</p>
			<p class="getCode">
				<input type="number" id="validationNumber" class="inputText" placeholder="输入验证码" /><a class="btn" id="getValidate" href="#layout" data-position-to="window" data-transition="pop" data-rel="popup">获取验证码</a>
			</p>
			<a class="btnSubmit" id="btnSubmit" href="#layout-2" data-position-to="window" data-transition="pop" data-rel="popup">验证手机号</a>
		</article>
		<!-- 信息提示框 -->
		<div data-role="popup" id="layout" data-overlay-theme="a" data-theme="b" data-dismissible="false" style="max-width:400px;" >
			<div role="main" class="ui-content" style="padding:8px;">
				<p id="msg">验证码成功!</p>
			</div>
		</div>
		<div data-role="popup" id="layout-2" data-overlay-theme="a" data-theme="b" data-dismissible="false" style="max-width:400px;" >
			<div role="main" class="ui-content" style="padding:8px;">
				<p id="msg-2">验证手机号成功!</p>
			</div>
		</div>
    </div>
	
	<script type="text/javascript">
		app.addIdentity();
	</script>
</body>
</html>
