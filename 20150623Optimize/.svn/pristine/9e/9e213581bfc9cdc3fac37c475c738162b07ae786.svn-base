<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="PointsManage_index" %>

<!DOCTYPE html>
<html runat="server">
<head>
	<title>积分商城</title>
	<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">
	<link rel="stylesheet" href="css/jquery.mobile-1.4.1.min.css" />
	<link rel="stylesheet" href="css/cssbase.css" />
	<link rel="stylesheet" href="css/common.css" />
	<link rel="stylesheet" href="scripts/jquery/jquery-ui/jquery-ui.css" />
	<script type="text/javascript" src="scripts/jquery/jquery-1.10.2.min.js"></script>
	<script type="text/javascript" src="scripts/jquery/jquery.cookie.js"></script>
	<script type="text/javascript" src="scripts/jquery/jquery.mobile.min.js"></script>
	
	<script type="text/javascript" src="scripts/common.js"></script>
	<script type="text/javascript" src="scripts/jquery/jquery-ui/jquery-ui.js"></script>
	<script type="text/javascript" src="scripts/jquery/jquery-ui/jquery-ui-tips.js"></script>
	
</head>
<body>
    <div class="points" data-role="page">
		<div class="headerSection bgImg">
			<div class="content">
			<ul class="score" id="score">
				<li class="soon">
					<h2>预计明日新增</h2>
					<p class="num txtColor"></p>
				</li>
				<li class="been">
					<h2>当前积分</h2>
					<p class="num"></p>
				</li>
			</ul>
			<div class="handler">
				<h1><a class="btn bgAward" href="#award" data-ajax="false">兑换礼品</a></h1>
				<a href="scorenote.html">我的兑换记录</a>
			</div>
			</div>
		</div>
		<div class="update">
			<div class="content">
				<div class="headItem">
					最近变动<a href="scorelog.html">查看所有</a>
				</div>
				<div class="text" id="scoreUpdate"></div>
			</div>
		</div>
		<div class="award" id="award">
			<div class="content">
				<div class="sprite" id="dataAward"></div>
				<div class="divClear"></div>
			</div>
		</div>
		<div class="score-rule">
			<div class="content" id="rule"></div>
		</div>
		
		<div data-role="popup" id="popupLayout" data-overlay-theme="b" data-theme="b" data-dismissible="false" style="max-width:400px;">
			<div role="main" class="ui-content" id="popupContent">
				<div class="abled">
					<h2 class="title">兑换确认</h2>
					<p>您确认兑换<span class="pro">“<em id="proName">多功能电锯煲</em>”</span>一件</p>
					<p>扣除积分<span class="txtColor" id="scoreNumber">1000</span>分吗？</p>
					<div class="btnSprite">
						<a href="scoreuse.html" class="ok">确认</a>
						<a href="javascript:;" data-rel="back" data-transition="flow">取消</a>
					</div>
				</div>
				<div class="disabled">
					<h2>您的积分数暂不够兑换该商品</h2>
					<div class="btnSprite">
						<a href="javascript:;" data-rel="back" class="close">返回</a>
					</div>
				</div>
			</div>
		</div>
    </div>
	
	<script type="text/javascript" src="scripts/index.js"></script>
</body>
</html>