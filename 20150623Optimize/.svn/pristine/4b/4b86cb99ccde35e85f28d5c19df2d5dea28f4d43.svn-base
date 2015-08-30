<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pointsManageFrame.aspx.cs"
    Inherits="PointsManage_pointsManageFrame" %>

<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>积分管理</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        var initUrl = "";
        $(document).ready(function () {
            initUrl = "waiterManage.aspx";
            $("ul.menu li:first-child").addClass("current").siblings().removeClass("current");
            $("#waiterQuery").attr("src", initUrl).show(); //初始化显示服务员查询
            $("#waiterRanking").hide();
            $("#shopRanking").hide();
            $("#exchangeQuery").hide();
            $("#supermarketGoods").hide();
            $("#htmlConfig").hide();
        });
        function ClickLi() {
            $("ul.menu li:first-child").addClass("current").siblings().removeClass("current");
            $("#waiterQuery").attr("src", "waiterManage.aspx").show(); //显示服务员查询
            $("#waiterRanking").hide();
            $("#shopRanking").hide();
            $("#exchangeQuery").hide();
            $("#supermarketGoods").hide();
            $("#htmlConfig").hide();
        }
        function ClickFirstLi() {
            $("ul.menu li").eq(1).addClass("current").siblings().removeClass("current");
            $("#waiterQuery").hide();
            $("#waiterRanking").attr("src", "waiterRanking.aspx").show(); //显示服务员排名
            $("#shopRanking").hide();
            $("#exchangeQuery").hide();
            $("#supermarketGoods").hide();
            $("#htmlConfig").hide();
        }
        function ClickSecondLi() {
            $("ul.menu li").eq(2).addClass("current").siblings().removeClass("current");
            $("#waiterQuery").hide();
            $("#waiterRanking").hide();
            $("#shopRanking").attr("src", "shopRanking.aspx").show(); //显示门店排名
            $("#exchangeQuery").hide();
            $("#supermarketGoods").hide();
            $("#htmlConfig").hide();
        }
        function ClickThirdLi() {
            $("ul.menu li").eq(3).addClass("current").siblings().removeClass("current");
            $("#waiterQuery").hide();
            $("#waiterRanking").hide();
            $("#shopRanking").hide();
            $("#exchangeQuery").attr("src", "exchangeQuery.aspx").show(); //显示兑换查询
            $("#supermarketGoods").hide();
            $("#htmlConfig").hide();
        }
        function ClickFourLi() {
            $("ul.menu li").eq(4).addClass("current").siblings().removeClass("current");
            $("#waiterQuery").hide();
            $("#waiterRanking").hide();
            $("#shopRanking").hide();
            $("#exchangeQuery").hide();
            $("#supermarketGoods").attr("src", "supermarketGoods.aspx").show(); //显示超市商品
            $("#htmlConfig").hide();
        }
        function ClickFiveLi() {
            $("ul.menu li").eq(5).addClass("current").siblings().removeClass("current");
            $("#waiterQuery").hide();
            $("#waiterRanking").hide();
            $("#shopRanking").hide();
            $("#exchangeQuery").hide();
            $("#supermarketGoods").hide();
            $("#htmlConfig").attr("src", "htmlConfig.aspx").show(); //显示HTML页面
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <uc2:HeadControl ID="HeadControl1" runat="server" navigationImage="" navigationText=""
        navigationUrl="" headName="积分管理" />
    <div id="box" class="box">
        <div class="tagMenu" id="tagMenu">
            <ul class="menu" style="width: 1200px">
                <li onclick="ClickLi()">服务员查询</li>
                <li onclick="ClickFirstLi()">服务员排名</li>
                <li onclick="ClickSecondLi()">门店排名</li>
                <li onclick="ClickThirdLi()">兑换查询</li>
                <li onclick="ClickFourLi()">超市商品</li>
                <li onclick="ClickFiveLi()">HTML页面</li>
            </ul>
        </div>
        <div class="content">
            <div class="layout">
                <!--服务员查询-->
                <iframe frameborder="0" name="waiterQuery" width="100%" id="waiterQuery" height="850px"
                    src="" scrolling="auto"></iframe>
                <!--服务员排名-->
                <iframe frameborder="0" name="waiterRanking" width="100%" id="waiterRanking" height="850px"
                    src="" scrolling="auto"></iframe>
                <!--门店排名-->
                <iframe frameborder="0" name="shopRanking" width="100%" id="shopRanking" height="850px"
                    src="" scrolling="auto"></iframe>
                <!--兑换查询-->
                <iframe frameborder="0" name="exchangeQuery" width="100%" id="exchangeQuery" height="850px"
                    src="" scrolling="auto"></iframe>
                <!--超市商品-->
                <iframe frameborder="0" name="supermarketGoods" width="100%" id="supermarketGoods"
                    height="850px" src="" scrolling="auto"></iframe>
                <!--HTML页面-->
                <iframe frameborder="0" name="htmlConfig" width="100%" id="htmlConfig" height="850px"
                    src="" scrolling="auto"></iframe>
            </div>
        </div>
    </div>
     <uc1:CheckUser ID="CheckUser1" runat="server" />
    </form>
</body>
</html>
