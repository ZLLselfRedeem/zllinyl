<%@ Page Language="C#" AutoEventWireup="true" CodeFile="complaint.aspx.cs" Inherits="AppPages_manual_complaint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>订单投诉与意见</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">
    <meta charset="utf-8" />
    <!--[if lt IE 9]>
		<script src="../js/html5shiv/html5shiv.js"></script>
	<![endif]-->
    <link rel="stylesheet" type="text/css" href="../css/jquery.mobile.min.css" />
    <link rel="stylesheet" type="text/css" href="../css/common.css" />
    <script src="../js/jquery.js"></script>
    <script src="../js/jquery.mobile.min.js"></script>
    <script src="../js/app/mobile.js"></script>
    <script src="../js/app/mobile.page.js"></script>
    <style type="text/css">
        .ui-page {
            background-color: #f7f7f7;
        }

        #msg {
            color: #ffffff;
            text-align: center;
            font-size: 1.3rem;
        }
    </style>
</head>
<body>
    <!-- Start of first page -->
    <section class="main tousu">
        <article class="content">
            <section class="item" id="itemWaiter">
                <h3 class="clear-p-top">
                    <div class="title">投诉服务员<span id="waiter"></span><span id="tel"></span></div>
                </h3>
                <p>商家提前验证</p>
                <p>对悠先点菜不熟悉</p>
                <p>服务态度差</p>
                <p>菜品质量不好</p>
                <p>对菜品不熟悉</p>
                <p>技能不熟练</p>
            </section>
            <%--<section class="item" id="itemCookbook">
                <h3>投诉菜品质量</h3>
            </section>--%>
            <section class="item" id="itemComment">
                <h3>其他意见</h3>
                <div class="comment">
                    <textarea class="area" placeholder="留下您的宝贵意见，我们将督促服务质量"></textarea>
                </div>

            </section>
        </article>

    </section>
    <footer class="tousu-footer">
        <a class="btn" id="btnSubmit" href="#layout" data-position-to="window" data-transition="pop" data-rel="popup">提交</a>
        <!-- 信息提示框 -->
        <div data-role="popup" id="layout" data-overlay-theme="a" data-theme="b" data-dismissible="false" style="max-width: 400px;">
            <div role="main" class="ui-content" style="padding: 8px;">
                <p id="msg" style="width: 140px; margin: 40px 50px 50px 50px; text-shadow: none; color: #444;"></p>
                <button id="closeMsg">确定</button>
            </div>
        </div>
    </footer>
    <script type="text/javascript">
        app.tousu();
    </script>
</body>
</html>
