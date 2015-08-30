<%@ Page Language="C#" AutoEventWireup="true" CodeFile="menu.aspx.cs" Inherits="AppPages_wechatOrder_menu" %>

<!DOCTYPE html>
<html>
<head>
	<title>悠先点菜</title>
	<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">
	<meta charset="utf-8" />
	<!--[if lt IE 9]>
		<script src="../js/html5shiv/html5shiv.js"></script>
	<![endif]-->
	<link rel="stylesheet" type="text/css" href="../css/common.css" />
	<link rel="stylesheet" type="text/css" href="../css/jquery.mobile.min.css" />
	<script src="../js/app/uuid.js"></script>
	<script src="../js/app/iscroll.js"></script>
	<script src="../js/jquery.js"></script>
	<script src="../js/jquery.cookie.js"></script>
	<script src="../js/jquery.mobile.min.js"></script>
	<script src="../js/jquery.imgpreloader.js"></script>
	<script src="../js/app/mobile.js"></script>
	<script src="../js/app/mobile.page.js"></script>
	<script src="../js/app/cookbook.js"></script>
	<style type="text/css">
		body{
			background-color:#f4f3ea;
		}
		.ui-page{
			background-color:#f7f7f7;
		}
		#mutilPrices .ui-field-contain>label~[class*=ui-],
		#mutilPrices .ui-field-contain .ui-controlgroup-controls{
			width:100%;
		}
		
		#mutilPrices .ui-radio-off:after{
			background-color: rgba(255, 255, 255, 0.3);
			border:1px solid #000000;
		}
		#mutilPrices .ui-radio-on:after{
			background:url(../img/bg_checkradio.png) center center no-repeat;
			/* background-color: rgba(92, 194, 22, 0.7); 
			border:6px solid #ffffff;*/
			border:1px solid #b6b7a5;
			background-size:100% 100%;
			width:19px;
			height:19px;
		}
		html .ui-body-a{
			background-color:transparent;
		}
	</style>
	
</head>
<body>
<div id="pageLoading" class="pageLoading" data-role="page" data-module="module-loading" data-channel="channel-loading" data-title="">
	<img style="margin-top:25px;" src="../img/loading.gif" />
	<p class="txt">玩命加载中...</p>
	<p class="comment">听说摇一摇手机加载速度会快点!?</p>
</div>
<div id="pageCook" data-role="page" data-module="home" data-channel="cookbook" data-direction="reverse" data-title="" style="overflow-x:hidden;">
	
	<article class="cookBook">
		<header id="headerType" data-role="header" data-position="fixed" data-tap-toggle="false" data-id="list-home">
			<div id="typeScroll">
				<ul></ul>
			</div>
		</header>
		<section id="cookbook" role="main">
			<div id="log" style="display:none;"></div>
			<ul id="cook_100" style="opacity:0;"><div><style type="text/css"></style></div></ul>
		</section>
	</article>
	<aside id="cookPanel" class="cookPanel" data-role="footer">
		<ul id="cookPanelList"></ul>
		<footer class="total">
			<p>原价 <em id="priceTotal">0</em></p>
			<p class="txtColor">立减 <em id="priceBack">0</em></p>
			<a id="btnSubmit" class="btnSubmit" href="#isSundry" data-position-to="window" data-transition="pop" data-rel="popup">确认 <em id="prices">0</em></a>
		</footer>
	</aside>
	<div id="isSellOff" data-role="popup" data-transition="pop" data-overlay-theme="b" data-theme="b" data-dismissible="false">
		<div role="main" class="ui-content" style="padding:8px;">
			该菜品已售完！
		</div>
		<div role="footer" class="ui-footer">
			<a class="cancel" href="javascript:;">确定</a><a class="continue" href="javascript:;">非今日就餐</a>
		</div>
	</div>
	<div id="isSundry" data-role="popup" data-overlay-theme="b" data-theme="b" data-dismissible="false" style="max-width:400px;">
		<div role="header" class="ui-header">
			<h1>其他费用</h1>
			<p class="headerComment">本店用餐会收取其它费用，为避免二次收费，请正确输入用餐人数，到店后按实际人数多退少补收费。</p>
		</div>
		<div role="main" class="ui-content" style="height:200px;overflow-y:scroll;">
			<ul id="sundryInfo"></ul>
			<div class="total">
				<p class="txt"><span class="title">合计:</span><span class="num-price">￥<em id="sundry-price">00.00</em></span></p>
			</div>
		</div>
		<div role="footer" class="ui-footer">
			<a id="isSundryBtn" href="javascript:;" data-rel="back" data-transition="pop">确定</a>
		</div>
	</div>
</div>

<div id="mutilPrices" data-role="page" data-title="">
	<article id="dishPrice" class="item">
		<h1 class="headerTitle">规格 <span class="tip">(带*规格项不支持折扣)</span></h1>
		<div class="ui-field-contain">
			<fieldset data-role="controlgroup">
				<ul></ul>
			</fieldset>
		</div>
	</article>
	<article id="taste" class="item"></article>
	<article id="ingredients" class="item"></article>
	<footer class="footerSprite" data-role="footer" data-position="fixed" data-tap-toggle="false" data-id="list-mutilPrices">
		<div class="repeat">
			<p class="tips">份数</p>
			<div class="num-trigger">
				<div class="slide">
					<span class="num-less">-</span>
					<span class="num" id="repeatNum">1</span>
				</div>
				<span class="num-add">+</span>
			</div>
		</div>
		<a id="submitPrice" class="btnSubmit" href="#pageCook" data-role="back">完成</a>
	</footer>
</div>
</body>
</html>