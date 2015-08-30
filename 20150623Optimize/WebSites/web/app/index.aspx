<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="web_app_index" %>



<!DOCTYPE html>
<html runat="server">
<head>
	<title>悠先点菜 - 新一代吃货必备的app</title>
	<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1,minimum-scale=1, user-scalable=no">
	<link type="text/css" rel="stylesheet" href="css/cssbase.css" />
	<link type="text/css" rel="stylesheet" href="css/common.css" />
	<script type="text/javascript" src="../js/jquery/jquery-1.10.2.min.js"></script>
	<script type="text/javascript" src="../js/mobile.js"></script>
</head>
<body>
    <form id="form1" runat="server">
	<div class="index">
		<div class="header"><img alt="悠先点菜 新一代吃货必备的app" src="img/index_pic_top.png" /></div>
		<div class="container">
			<ul class="text">
				<li class="uxianApp">【<a href="http://u-xian.com/d.aspx">悠先点菜下载</a>】</li>
				<li class="uxianService">【<a href="http://u-xian.com/sd.aspx">悠先服务下载</a>】</li>
			</ul>
			<div class="downloadLink">
				<h2>短信获取下载链接</h2>
				<input type="tel" class="inputText" id="phonenum" placeholder="手机号码" />
				<a href="javascript:;" class="inputBtn" id="receiveLink" >获取链接</a>
			</div>
			
		</div>
		
	</div>
	<div class="footer">
		<a href="/index.aspx">桌面版</a>
		<ul class="txt">
			<li><p>杭州友络软件科技有限公司</p></li>
			<li><p class="en">Copyright&copy;2014 ViewAlloc Inc.All right is reserved</p></li>
		</ul>
	</div>
    </form>
	<script type="text/javascript">
    
	</script>
</body>
</html>

