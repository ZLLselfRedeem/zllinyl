﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YouxianService.aspx.cs" Inherits="WeChatPlatManage_YouxianService" %>

<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>悠先服务</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        var orderStatisticsUrl = "";
        $(document).ready(function () {
            orderStatisticsUrl = "uxianCooperation.aspx";
            $("ul.menu li:first-child").addClass("current").siblings().removeClass("current");
            $("#frame1").attr("src", orderStatisticsUrl);
            $("#frame2").hide();
            $("#frame3").hide();
            $("#frame4").hide();
        });
        function ClickFirstLi() {
            $("ul.menu li:first-child").addClass("current").siblings().removeClass("current");
            $("#frame1").attr("src", orderStatisticsUrl).show();
            $("#frame2").hide();
            $("#frame3").hide();
            $("#frame4").hide();
        }
        function ClickSecondLi() {
            $("ul.menu li").eq(1).addClass("current").siblings().removeClass("current");
            $("#frame2").attr("src", "uxianQandA.aspx").show();
            $("#frame1").hide();
            $("#frame3").hide();
            $("#frame4").hide();
        }
        function ClickThirdLi() {
            $("ul.menu li").eq(2).addClass("current").siblings().removeClass("current");
            $("#frame3").attr("src", "uxianProposal.aspx").show();
            $("#frame1").hide();
            $("#frame2").hide();
            $("#frame4").hide();
        }
        function ClickFourLi() {
            $("ul.menu li").eq(3).addClass("current").siblings().removeClass("current");
            $("#frame4").attr("src", "uxianComplaintHandling.aspx").show();
            $("#frame1").hide();
            $("#frame2").hide();
            $("#frame3").hide();
        }
    </script>
    <style type="text/css">
        li
        {
            white-space: nowrap;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <!-- 头部菜单 Start -->
    <uc2:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
        navigationText="悠先服务" navigationUrl="" headName="微信平台-悠先服务" />
    <!-- 头部菜单 end -->
    <!-- 主编辑区 -->
    <div id="box" class="box">
        <div class="tagMenu" id="tagMenu">
            <ul class="menu">
                <li onclick="ClickFirstLi()">悠先合作</li>
                <li onclick="ClickSecondLi()">常见问答</li>
                <li onclick="ClickThirdLi()">意见建议</li>
                <li onclick="ClickFourLi()">投诉处理</li>
            </ul>
        </div>
        <div class="content">
            <div class="layout">
                <!--悠先合作-->
                <iframe frameborder="0" name="frame1" width="100%" id="frame1"
                    height="850px" src="" scrolling="auto"></iframe>
                <!--常见问答-->
                <iframe frameborder="0" name="frame2" width="100%" id="frame2"
                    height="850px" src="" scrolling="auto"></iframe>
                <!--意见建议-->
                <iframe frameborder="0" name="frame3" width="100%" id="frame3"
                    height="850px" src="" scrolling="auto"></iframe>
                <!--投诉处理-->
                <iframe frameborder="0" name="frame4" width="100%" id="frame4"
                    height="850px" src="" scrolling="auto"></iframe>
            </div>
        </div>
    </div>
    </form>
</body>
</html>