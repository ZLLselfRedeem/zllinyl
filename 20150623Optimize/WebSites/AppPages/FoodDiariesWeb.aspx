<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FoodDiariesWeb.aspx.cs" Inherits="AppPages_FoodDiariesWeb" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>美食日记</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">
    <meta charset="utf-8" />
    <!--[if lt IE 9]>
		<script src="js/html5shiv/html5shiv.js"></script>
	<![endif]-->
    <link rel="stylesheet" type="text/css" href="css/base.css" />
    <link rel="stylesheet" type="text/css" href="css/share.css" />
    <script src="js/app/mobile.js"></script>
    <script src="js/jquery.js"></script>
    <script src="js/jquery.imgpreloader.js"></script>
    <style type="text/css">
        .ui-widget-header .ui-state-default {
            background: #e6e6e6 url(images/ui-bg_glass_75_e6e6e6_1x400.png) 50% 50% repeat-x;
        }
    </style>
</head>
<body>
    <%--<div id="pageLoading" class="pageLoading" data-role="page" data-module="module-loading"
        data-channel="channel-loading" data-title="">
        <img src="img/loading-circle.gif" />

		<p class="txt">玩命加载中...</p>
		<p class="comment">听说摇一摇手机加载速度会快点!?</p>
		
    </div>--%>
    <div class="food-diaries-show" id="diariesWebShow" >
        <div class="web-container">
            <header class="header-top">
                <h2>
                    <div class="txt">
                        <span class="date"><i id="shareDate"></i></span>@<i id="shareShop">悠先点菜</i>
                    </div>
                </h2>
            </header>
            <article class="diaries-show-content">
                <section class="comment">
                    <span id="diariesText"></span>
                </section>
                <section class="pics-list">
                    <ul class="text" id="picsList">
                    </ul>
                    <p class="tips">
                        <span id="username"></span>的美食日记!
                    </p>
                </section>
                <footer class="diariesComment">
                </footer>
            </article>
        </div>
    </div>
    <script src="js/share.js"></script>
    <script type="text/javascript">
        $(function () {
            var pageType = "pc";
            setFooderHtml(pageType,"");
        });
        Diaries.getDiaries();
    </script>
</body>
</html>
