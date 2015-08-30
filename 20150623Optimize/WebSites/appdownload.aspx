<%@ Page Language="C#" AutoEventWireup="true" CodeFile="appdownload.aspx.cs" Inherits="appdownload" %>
<!DOCTYPE html>
<html>
    <head>
	<title>悠先点菜下载</title>
	<meta charset="UTF-8">
	<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, minimum-scale=1.0,user-scalable=no" />
	<!--[if lt IE 9]>
		<script src="AppPages/js/html5shiv/html5shiv.js"></script>
	<![endif]-->
	<link rel="stylesheet" href="AppPages/css/jquery.mobile.min.css" />
	<link rel="stylesheet" href="AppPages/css/base.css" />
	<link rel="stylesheet" href="AppPages/css/common.css" />
	
	<script type="text/javascript" src="AppPages/js/jquery.js"></script>
    <script type="text/javascript" src="AppPages/js/jquery.mobile-1.4.2.js"></script>
    </head>
    <body>
	<div data-role="page" id="getApp">
		
	    <header>
		<h1 class="headerTop">悠先点菜 手机点菜 就用悠先</h1>
	    </header>
	    <p class="popup-layer-sprite">
		<a class="popup-layer" href="javascript:;">
		    <img src="AppPages/img/pagedownload/btn_1.png" title="下载悠先app 在浏览器里打开" alt="下载悠先app 在浏览器里打开" />
		</a>
	    </p>
	    <div class="content-list">
		<div class="item">
		    <img src="AppPages/img/pagedownload/item_1.png" />
		    <h2>知名餐厅任挑</h2>
		    <p class="txt">
			外婆家、炉鱼、老头儿油爆虾……不断上新的餐厅，持续尝鲜的舌尖
		    </p>
		</div>
		<div class="item">
		    <img src="AppPages/img/pagedownload/item_2.png" />
		    <h2>高清图片诱人</h2>
		    <p class="txt">
			不进餐厅，也能看菜单，价格、口味一刷就知，不满意就换，告别尴尬
		    </p>
		</div>
		<div class="item">
		    <img src="AppPages/img/pagedownload/item_3.png" />
		    <h2>快速点餐方便</h2>
		    <p class="txt">
			极简的点菜流程和一键支付，人未到，菜已点，吃饭不要太轻松
		    </p>
		</div>
		<div class="item">
		    <img src="AppPages/img/pagedownload/item_4.png" />
		    <h2>美食日记分享</h2>
		    <p class="txt">
			让它自动拼好美食图文，一键分享吃货历程，逼格就是不需要自己动手
		    </p>
		</div>
		<div class="item last-item">
		    <img src="AppPages/img/pagedownload/item_5.png" />
		    <h2>劲爆活动持续</h2>
		    <p class="txt">
			新用户注册送 10 元、号码有6就送钱、猜中冠军赢免单……每月活动，参与就省，更多惊喜和好玩，波波袭来 
		    </p>
		</div>
	    </div>
	    <p class="popup-layer-sprite">
		<a class="popup-layer" href="javascript:;">
		    <img src="AppPages/img/pagedownload/btn_1.png" title="下载悠先app 在浏览器里打开" alt="下载悠先app 在浏览器里打开" />
		</a>
	    </p>
	    <div class="get-app-footer" data-role="footer">
		Copyright © 2011-2014 ViewAlloc .All Rights Reserverd
	    </div>
	    <div class="panel-show" id="panelShow">
		<img src="AppPages/img/pagedownload/t_android.png" />
	    </div>
	</div>
	<script type="text/javascript" src="AppPages/js/getApp.js?t=2"></script>
    </body>
</html>