<%@ Page Language="C#" AutoEventWireup="true" CodeFile="shopShow.aspx.cs" Inherits="shopShow" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
    <meta name="format-detection" content="telephone=no" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="renderer" content="webkit" />
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="Cache-Control" content="no-cache, must-revalidate" />
    <title><asp:Literal ID="pageTitle" Text="" runat="server"> </asp:Literal></title>
    <script language="javascript" type="text/javascript" src="tron/tron.min.js"></script>
    <script language="javascript" type="text/javascript" src="tron/tron.maps.js"></script>
    <link href="tron/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="tron/bootstrap/css/font-awesome.min.css" rel="stylesheet">
    <link href="tron/bootstrap/css/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="AppPages/css/shopShow.css" rel="stylesheet" />
</head>
<body>
	<div runat="server" id="shareShow">
    </div>

	<div class="storepic">
		<img src="http://image.u-xian.com/UploadFiles/Images/Client/home_not_img.png" class="img-responsive" id="shoplogo" width="100%" />
	</div>

	<div class="storeInfo">
		<div class="container">
			<ul>
				<li class="col-xs-12 storeLevel">
					<table cellpadding="0" cellspacing="0" width="100%">
						<tr>
							<td width="48">门店等级</td>
							<td><span class="drillShow"></span></td>
							<td width="100" class="text-right">好评<span id="prepayOrderCount" style="margin-left:5px;"></span></td>
						</tr>
					</table>
				</li>
				<li class="col-xs-12">
					<table cellpadding="0" cellspacing="0" width="100%">
						<tr>
							<td width="48">门店地址</td>
							<td id="storeAdd"></td>
						</tr>
					</table>
				</li>
				<li class="col-xs-12">
					<table cellpadding="0" cellspacing="0" width="100%">
						<tr>
							<td width="48">联系电话</td>
							<td><span id="tel"></span></td>
						</tr>
					</table>
				</li>
				<li class="col-xs-12">营业时间<span id="time"></span></li>
			</ul>
		</div>
	</div>
	
	<div class="storeEvaluation clearfix">
		<div class="Evaluations"> 
			<div class="evaColumn clearfix pull-right">
				<div class="eav_good" style="width: 75%;">good</div>
				<div class="eav_normall" style="width: 20%;">normall</div>
				<div class="eav_bad" style="width: 5%;">bad</div>
			</div><span style="position:relative; top:5px; left:0;">客户评价</span>
			<div class="evaTxt text-center">
				<span class="good">好评 <span id="eva_good_per"></span></span>&nbsp;&nbsp;
				<span class="normall">中评 <span id="eva_normall_per"></span></span>&nbsp;&nbsp;
				<span class="bad">差评 <span id="eva_bad_per"></span></span>
			</div>
		</div>
		<div class="container">
			<div class="row">
				<ul id="comments"></ul>
			</div>
		</div>
	</div>
	
    <script type="text/javascript">
        require('jquery').then(function(jQuery) {
            jQuery = jQuery[0];
            if (jQuery && !window.jQuery && !window.$) {
                window.$ = window.jQuery = jQuery;
            }
            return require(['tron/bootstrap/js/bootstrap.min.js']);
        }).then(function(){ require('AppPages/js/shopShow.js', function(shopShow){new shopShow();}); });
	</script>
</body>
</html>
