<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sd.aspx.cs" Inherits="sd" %>

<!DOCTYPE html PUBLIC "-//WAPFORUM//DTD XHTML Mobile 1.0//EN" "http://www.wapforum.org/DTD/xhtml-mobile10.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>悠先服务下载</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, minimum-scale=1.0,user-scalable=no" />
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="AppPages/js/jquery.js" type="text/javascript"></script>
    <style type="text/css">
        body
        {
            background-image: url("Images/appdownloadImg/bg.png");
            background-repeat: repeat-x;
            overflow: hidden;
        }
    </style>
</head>
<body scroll="no">
    <form id="form1" runat="server">
    <div style="text-align: center;" id="LoadImg">
        <img src="AppPages/uploads/Loading/loading7.gif" style="width: 100px; height: 100px" />
    </div>
    <div id="Div" style="width: 100%; margin-top: 0; display: none">
        <div style="height: 100%">
            <table id="Tab" style="width: 100%; height: 100%; margin-bottom: 0; padding-bottom: 0">
                <tr id="Tr1" style="height: 17%; text-align: center">
                    <td>
                        <img src="Images/appdownloadImg/s_logo.png" width="80px" />
                    </td>
                </tr>
                <tr id="Tr4">
                    <td style="height: 3%">
                    </td>
                </tr>
                <tr style="text-align: center; height: 15%" id="Tr5">
                    <td class="style1">
                        <img src="Images/appdownloadImg/downloadGuide-2.png" width="100%" />
                        <%-- <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td width="50%">
                                    <img src="Images/appdownloadImg/iphone.png" width="100%" onclick="ToiOS()" />
                                </td>
                                <td>
                                    <img src="Images/appdownloadImg/and.png" width="100%" onclick="ToAndroid()" />
                                </td>
                            </tr>
                        </table>--%>
                    </td>
                </tr>
                <tr id="TrHeight">
                    <td>
                    </td>
                </tr>
                <tr style="text-align: center; height: 19%" id="Tr3">
                    <td>
                        <img src="Images/appdownloadImg/wenzi.png" width="100%" />
                    </td>
                </tr>
                <tr id="Tr2" style="height: 8%">
                    <td>
                    </td>
                </tr>
                <tr style="text-align: center; height: 20%">
                    <td>
                        <img src="Images/appdownloadImg/dish.png" width="100%" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
<script type="text/javascript">
    var browser;
    $(function () {
        try {
            var screenHeight = $(document).height() - 16;
            $("#Tab").css("height", screenHeight);
            browser = {
                versions: function () {
                    var u = navigator.userAgent, app = navigator.appVersion;
                    return {         //移动终端浏览器版本信息
                        trident: u.indexOf('Trident') > -1, //IE内核
                        presto: u.indexOf('Presto') > -1, //opera内核
                        webKit: u.indexOf('AppleWebKit') > -1, //苹果、谷歌内核
                        gecko: u.indexOf('Gecko') > -1 && u.indexOf('KHTML') == -1, //火狐内核
                        mobile: !!u.match(/AppleWebKit.*Mobile.*/), //是否为移动终端
                        iOS: !!u.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/), //ios终端
                        Android: u.indexOf('Android') > -1 || u.indexOf('Linux') > -1, //android终端或uc浏览器
                        iPhone: u.indexOf('iPhone') > -1, //是否为iPhone或者QQHD浏览器
                        iPad: u.indexOf('iPad') > -1, //是否iPad
                        webApp: u.indexOf('Safari') == -1 //是否web应该程序，没有头部与底部
                    };
                } (),
                language: (navigator.browserLanguage || navigator.language).toLowerCase()
            }
            if (browser.versions.mobile) {
                //判断是否为移动终端
                CheckBrowser();
            }
            else {
                CheckBrowser();
            }
        } catch (e) {
            ShowDiv();
        }
    });
    function CheckBrowser() {
        if (browser.versions.iOS) {
            //跳转到IOS appstore
            ShowDiv();
            ToiOS();
        }
        else if (browser.versions.Android) {
            //跳转到安卓下载页面
            ShowDiv();
            if (!is_weixin()) {
                ToAndroid();
            }
        }
        else {
            //显示页面，iOS和Android下载
            ShowDiv();
            ToAndroid();
        }
    }
    function is_weixin() {
        var ua = navigator.userAgent.toLowerCase();
        if (ua.match(/MicroMessenger/i) == "micromessenger") {
            return true;
        } else {
            return false;
        }
    }
    function ToAndroid() {
        window.location.href = "http://uxian.oss-cn-hangzhou.aliyuncs.com/UploadFiles/poslite.apk";
    }
    function ToiOS() {
        window.location.href = "https://itunes.apple.com/cn/app/you-xian-fu-wu/id838889215?ls=1&mt=8";
    }
    function ShowDiv() {
        $("#LoadImg").css("display", "none"); //隐藏加载图片
        $("#Div").css("display", "block"); //显示页面  
    }
</script>
