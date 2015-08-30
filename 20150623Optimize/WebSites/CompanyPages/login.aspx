﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="CompanyPages_login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>悠先登录</title>
    <link type="text/css" rel="stylesheet" href="css/cssbase.css" />
    <link type="text/css" rel="stylesheet" href="css/common.css" />
    <style type="text/css">
        html{ background-color: #f5f2e9;}
		#error{ color:#ff0000;font-size:12px;font-family:'Microsoft YaHei',arial; }
    </style>
	<script type="text/javascript">
		YUI_config = {
			comboBase: '../ncombo.axd?',
			combine: true,
			root: '3.17.2/build/'
		};
	</script>
	<script type="text/javascript" src="../yui/3.17.2/build/yui/yui-min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="login">
        <div id="header">
            <div class="text">
                <h2>
                    悠先 商户宝收银</h2>
                <span class="tips">悠先点，悠先吃，美食</span>
            </div>
        </div>
        <div class="containerLogin">
            <div class="content">
                <p>
					<input id="userName" class="inputtext" type="text" autofocus="autofocus" placeholder="请输入用户名" />
				</p>
                <p>
					<input id="pw" class="inputtext" type="password" value="" />
				</p>
                <p class="btnSprite">
                    <input id="submit" type="button" class="button" value="" />
                </p>
				<span id="error"></span>
            </div>
        </div>
        <div class="footer">
            <ul>
                <li class="en"><a href="http://www.u-xian.com">www.u-xian.com</a></li><li class="en">
                    All Rights Reserved 2012 </li>
                <li>浙ICP备12012727号</li><li>友络科技旗下网站</li>
            </ul>
        </div>
    </div>
    </form>
	<script type="text/javascript" src="scripts/public/common.js"></script>
</body>
</html>
