<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cookbook.aspx.cs" Inherits="AppPages_manual_cookbook" %>

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
	<script src="../js/app/iscroll.js"></script>
	<script src="../js/jquery.js"></script>
	<script src="../js/jquery.mobile.min.js"></script>
	<script src="../js/app/mobile.js"></script>
	<script src="../js/app/cookbook.js"></script>
	<script src="../js/app/mobile.page.js"></script>
	
	<style type="text/css">
		body{
			background-color:#f4f3ea;
		}
		.ui-page{
			background-color:#f7f7f7;
		}
	</style>
	
</head>
<body>
<div id="pageCook" data-role="page" data-module="home" data-channel="cookbook" data-title="" style="overflow-x:hidden;">
	<article class="cookBook">
		<header id="headerType" data-role="header" data-position="fixed" data-tap-toggle="false" data-id="list-home">
			<div id="typeScroll">
				<ul></ul>
			</div>
		</header>
		<section id="cookbook" role="main">
			<div id="log" style="display:none;"></div>
			<ul id="cook_100" style="opacity:0;"></ul>
		</section>
		
	</article>
	<aside id="cookPanel" class="cookPanel" data-role="footer">
		<ul id="cookPanelList"></ul>
		<footer class="total">
			<p>原价 <em id="priceTotal">0</em></p>
			<p class="txtColor">立减 <em id="priceBack">0</em></p>
			<a href="#popTips" class="btnSubmit" id="btnSubmit">确认 <em id="prices">0</em></a>
		</footer>
	</aside>
	<div id="popTips" style="display:none;">
	</div>
	<script type="text/javascript">
		$(function(){
			$( "#pageCook" ).pagecontainer({ defaults: true });
			
		});
		
	</script>
</div>
</body>
</html>