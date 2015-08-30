<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StatisticalStatementManage.aspx.cs"
    Inherits="StatisticalStatement_StatisticalStatementManage" %>

<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>报表统计管理</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        var orderStatisticsUrl = "";
        $(document).ready(function () {
            //comprehensiveStatisticsUrl = "comprehensiveStatistics.aspx";
            comprehensiveStatisticsUrl = "dataStatistics.aspx";
            $("#first li:first-child").addClass("current").siblings().removeClass("current");
            $("#comprehensiveStatistics").attr("src", comprehensiveStatisticsUrl);
            $("#orderStatistics").hide();
            $("#usersAmountStatistics").hide();
            $("#payAmountStatistics").hide();
            $("#shopAmountStatistics").hide();
            $("#dataExportExcel").hide();
            $("#memberAmountStatistice").hide();
            $("#aPIAccessAmountStatistice").hide();
            $("#consumptionWeekStatistics").hide();
            $("#qrCodePageViewStatistics").hide();
            $("#shopOrderListStatistics").hide();
            $("#singleShopDayOrderList").hide();
            $("#orderCustomer").hide();
            $("#employeePoint").hide();
            $("#second_tagMenu ul li").removeClass("current");
        });
        function ClickLi() {
            $("#first li:first-child").addClass("current").siblings().removeClass("current");
            //$("#comprehensiveStatistics").attr("src", "comprehensiveStatistics.aspx").show();
            $("#comprehensiveStatistics").attr("src", "dataStatistics.aspx").show();
            $("#orderStatistics").hide();
            $("#payAmountStatistics").hide();
            $("#shopAmountStatistics").hide();
            $("#aPIAccessAmountStatistice").hide();
            $("#memberAmountStatistice").hide();
            $("#dataExportExcel").hide();
            $("#usersAmountStatistics").hide();
            $("#consumptionWeekStatistics").hide();
            $("#qrCodePageViewStatistics").hide();
            $("#shopOrderListStatistics").hide();
            $("#singleShopDayOrderList").hide();
            $("#orderCustomer").hide();
            $("#employeePoint").hide();
            $("#second_tagMenu ul li").removeClass("current");
        }
        function ClickFirstLi() {
            $("#first li").eq(1).addClass("current").siblings().removeClass("current");
            $("#orderStatistics").attr("src", "OrderStatistics.aspx").show();
            $("#payAmountStatistics").hide();
            $("#shopAmountStatistics").hide();
            $("#aPIAccessAmountStatistice").hide();
            $("#memberAmountStatistice").hide();
            $("#dataExportExcel").hide();
            $("#usersAmountStatistics").hide();
            $("#consumptionWeekStatistics").hide();
            $("#comprehensiveStatistics").hide();
            $("#qrCodePageViewStatistics").hide();
            $("#shopOrderListStatistics").hide();
            $("#singleShopDayOrderList").hide();
            $("#orderCustomer").hide();
            $("#employeePoint").hide();
            $("#second_tagMenu ul li").removeClass("current");
        }
        function ClickSecondLi() {
            $("#first li").eq(2).addClass("current").siblings().removeClass("current");
            $("#payAmountStatistics").attr("src", "PayAmountStatistics.aspx").show();
            $("#orderStatistics").hide();
            $("#usersAmountStatistics").hide();
            $("#shopAmountStatistics").hide();
            $("#aPIAccessAmountStatistice").hide();
            $("#memberAmountStatistice").hide();
            $("#dataExportExcel").hide();
            $("#consumptionWeekStatistics").hide();
            $("#comprehensiveStatistics").hide();
            $("#qrCodePageViewStatistics").hide();
            $("#shopOrderListStatistics").hide();
            $("#singleShopDayOrderList").hide();
            $("#orderCustomer").hide();
            $("#employeePoint").hide();
            $("#second_tagMenu ul li").removeClass("current");
        }
        function ClickTenLi() {
            $("#first li").eq(3).addClass("current").siblings().removeClass("current");
            $("#shopOrderListStatistics").attr("src", "shopOrderListStatistics.aspx").show();
            $("#orderStatistics").hide();
            $("#usersAmountStatistics").hide();
            $("#shopAmountStatistics").hide();
            $("#aPIAccessAmountStatistice").hide();
            $("#memberAmountStatistice").hide();
            $("#dataExportExcel").hide();
            $("#consumptionWeekStatistics").hide();
            $("#comprehensiveStatistics").hide();
            $("#qrCodePageViewStatistics").hide();
            $("#payAmountStatistics").hide();
            $("#singleShopDayOrderList").hide();
            $("#orderCustomer").hide();
            $("#employeePoint").hide();
            $("#second_tagMenu ul li").removeClass("current");
        }
        function ClickElevenLi() {
            $("#first li").eq(4).addClass("current").siblings().removeClass("current");
            $("#singleShopDayOrderList").attr("src", "singleShopDayOrderList.aspx").show();
            $("#orderStatistics").hide();
            $("#usersAmountStatistics").hide();
            $("#shopAmountStatistics").hide();
            $("#aPIAccessAmountStatistice").hide();
            $("#memberAmountStatistice").hide();
            $("#dataExportExcel").hide();
            $("#consumptionWeekStatistics").hide();
            $("#comprehensiveStatistics").hide();
            $("#qrCodePageViewStatistics").hide();
            $("#payAmountStatistics").hide();
            $("#shopOrderListStatistics").hide();
            $("#orderCustomer").hide();
            $("#employeePoint").hide();
            $("#second_tagMenu ul li").removeClass("current");
        }
        function ClickTwelveLi() {
            $("#first li").eq(5).addClass("current").siblings().removeClass("current");
            $("#orderCustomer").attr("src", "orderCustomer.aspx").show();
            $("#orderStatistics").hide();
            $("#usersAmountStatistics").hide();
            $("#shopAmountStatistics").hide();
            $("#aPIAccessAmountStatistice").hide();
            $("#memberAmountStatistice").hide();
            $("#dataExportExcel").hide();
            $("#consumptionWeekStatistics").hide();
            $("#comprehensiveStatistics").hide();
            $("#qrCodePageViewStatistics").hide();
            $("#payAmountStatistics").hide();
            $("#shopOrderListStatistics").hide();
            $("#singleShopDayOrderList").hide();
            $("#employeePoint").hide();
            $("#second_tagMenu ul li").removeClass("current");
        }
        function ClickThirteenLi() {
            $("#first li").eq(6).addClass("current").siblings().removeClass("current");
            $("#employeePoint").attr("src", "employeePoint.aspx").show();
            $("#orderStatistics").hide();
            $("#usersAmountStatistics").hide();
            $("#shopAmountStatistics").hide();
            $("#aPIAccessAmountStatistice").hide();
            $("#memberAmountStatistice").hide();
            $("#dataExportExcel").hide();
            $("#consumptionWeekStatistics").hide();
            $("#comprehensiveStatistics").hide();
            $("#qrCodePageViewStatistics").hide();
            $("#payAmountStatistics").hide();
            $("#shopOrderListStatistics").hide();
            $("#singleShopDayOrderList").hide();
            $("#orderCustomer").hide();
            $("#second_tagMenu ul li").removeClass("current");
        }
        function ClickThirdLi() {
            $("#second li").eq(0).addClass("current").siblings().removeClass("current");
            $("#usersAmountStatistics").attr("src", "UserAmountStatistics.aspx").show();
            $("#orderStatistics").hide();
            $("#payAmountStatistics").hide();
            $("#shopAmountStatistics").hide();
            $("#aPIAccessAmountStatistice").hide();
            $("#memberAmountStatistice").hide();
            $("#dataExportExcel").hide();
            $("#consumptionWeekStatistics").hide();
            $("#comprehensiveStatistics").hide();
            $("#qrCodePageViewStatistics").hide();
            $("#shopOrderListStatistics").hide();
            $("#singleShopDayOrderList").hide();
            $("#orderCustomer").hide();
            $("#employeePoint").hide();
            $("#tagMenu ul li").removeClass("current");
        }
        function ClickFourLi() {
            $("#second li").eq(1).addClass("current").siblings().removeClass("current");
            $("#shopAmountStatistics").attr("src", "ShopAmountStatistics.aspx").show();
            $("#orderStatistics").hide();
            $("#payAmountStatistics").hide();
            $("#usersAmountStatistics").hide();
            $("#aPIAccessAmountStatistice").hide();
            $("#memberAmountStatistice").hide();
            $("#dataExportExcel").hide();
            $("#consumptionWeekStatistics").hide();
            $("#comprehensiveStatistics").hide();
            $("#qrCodePageViewStatistics").hide();
            $("#shopOrderListStatistics").hide();
            $("#singleShopDayOrderList").hide();
            $("#orderCustomer").hide();
            $("#employeePoint").hide();
            $("#tagMenu ul li").removeClass("current");
        }
        function ClickFiveLi() {
            $("#second li").eq(2).addClass("current").siblings().removeClass("current");
            $("#aPIAccessAmountStatistice").attr("src", "APIAccessAmountStatistice.aspx").show();
            $("#orderStatistics").hide();
            $("#payAmountStatistics").hide();
            $("#usersAmountStatistics").hide();
            $("#shopAmountStatistics").hide();
            $("#memberAmountStatistice").hide();
            $("#dataExportExcel").hide();
            $("#consumptionWeekStatistics").hide();
            $("#comprehensiveStatistics").hide();
            $("#qrCodePageViewStatistics").hide();
            $("#shopOrderListStatistics").hide();
            $("#singleShopDayOrderList").hide();
            $("#orderCustomer").hide();
            $("#employeePoint").hide();
            $("#tagMenu ul li").removeClass("current");
        }
        function ClickMemberLi() {
            $("#second li").eq(3).addClass("current").siblings().removeClass("current");
            $("#memberAmountStatistice").attr("src", "MemberStatistics.aspx").show();
            $("#orderStatistics").hide();
            $("#payAmountStatistics").hide();
            $("#usersAmountStatistics").hide();
            $("#shopAmountStatistics").hide();
            $("#aPIAccessAmountStatistice").hide();
            $("#dataExportExcel").hide();
            $("#consumptionWeekStatistics").hide();
            $("#comprehensiveStatistics").hide();
            $("#qrCodePageViewStatistics").hide();
            $("#shopOrderListStatistics").hide();
            $("#singleShopDayOrderList").hide();
            $("#orderCustomer").hide();
            $("#employeePoint").hide();
            $("#tagMenu ul li").removeClass("current");
        }
        function ClickSixLi() {
            $("#second li").eq(4).addClass("current").siblings().removeClass("current");
            $("#dataExportExcel").attr("src", "DataExportExcel.aspx").show();
            $("#orderStatistics").hide();
            $("#payAmountStatistics").hide();
            $("#usersAmountStatistics").hide();
            $("#shopAmountStatistics").hide();
            $("#aPIAccessAmountStatistice").hide();
            $("#memberAmountStatistice").hide();
            $("#comprehensiveStatistics").hide();
            $("#qrCodePageViewStatistics").hide();
            $("#shopOrderListStatistics").hide();
            $("#singleShopDayOrderList").hide();
            $("#orderCustomer").hide();
            $("#employeePoint").hide();
            $("#tagMenu ul li").removeClass("current");
        }
        function ClickSevenLi() {
            $("#second li").eq(5).addClass("current").siblings().removeClass("current");
            $("#consumptionWeekStatistics").attr("src", "ConsumptionWeekStatistics.aspx").show();
            $("#orderStatistics").hide();
            $("#payAmountStatistics").hide();
            $("#usersAmountStatistics").hide();
            $("#shopAmountStatistics").hide();
            $("#aPIAccessAmountStatistice").hide();
            $("#memberAmountStatistice").hide();
            $("#dataExportExcel").hide();
            $("#comprehensiveStatistics").hide();
            $("#qrCodePageViewStatistics").hide();
            $("#shopOrderListStatistics").hide();
            $("#singleShopDayOrderList").hide();
            $("#orderCustomer").hide();
            $("#employeePoint").hide();
            $("#tagMenu ul li").removeClass("current");
        }
        function ClickEightLi() {
            $("#second li").eq(6).addClass("current").siblings().removeClass("current");
            $("#qrCodePageViewStatistics").attr("src", "QRCodePageViewStatistics.aspx").show();
            $("#orderStatistics").hide();
            $("#payAmountStatistics").hide();
            $("#usersAmountStatistics").hide();
            $("#shopAmountStatistics").hide();
            $("#aPIAccessAmountStatistice").hide();
            $("#memberAmountStatistice").hide();
            $("#dataExportExcel").hide();
            $("#comprehensiveStatistics").hide();
            $("#consumptionWeekStatistics").hide();
            $("#shopOrderListStatistics").hide();
            $("#singleShopDayOrderList").hide();
            $("#orderCustomer").hide();
            $("#employeePoint").hide();
            $("#tagMenu ul li").removeClass("current");
        } 
    </script>
</head>
<body scroll="no">
    <form id="form1" runat="server">
    <uc2:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
        navigationText="" navigationUrl="" headName="报表统计管理" />
    <div id="box" class="box">
        <div class="tagMenu" id="tagMenu">
            <ul class="menu" id="first" style="width: 1000px">
                <li onclick="ClickLi()">综合统计</li>
                <li onclick="ClickFirstLi()">订单量统计</li>
                <li onclick="ClickSecondLi()">支付量统计</li>
                <li onclick="ClickTenLi()">门店订单统计</li>
                <li onclick="ClickElevenLi()">单店单日订单统计</li>
                <li onclick="ClickTwelveLi()">时间段用户统计</li>
                <li onclick="ClickThirteenLi()">员工积分相关统计</li>
            </ul>
        </div>
        <div class="tagMenu" id="second_tagMenu">
            <ul class="menu" id="second" style="width: 1000px">
                <li onclick="ClickThirdLi()">用户数统计</li>
                <li onclick="ClickFourLi()">门店数统计</li>
                <li onclick="ClickFiveLi()">API访问次数统计</li>
                <li onclick="ClickMemberLi()">会员数量统计</li>
                <li onclick="ClickSixLi()">点单数据导出excel</li>
                <li onclick="ClickSevenLi()">消费周统计</li>
                <li onclick="ClickEightLi()">下载页访问统计</li>
            </ul>
        </div>
        <div class="content">
            <div class="layout">
                <!--综合统计-->
                <iframe frameborder="0" name="comprehensiveStatistics" width="100%" id="comprehensiveStatistics"
                    height="850px" src="" scrolling="auto"></iframe>
                <!--订单量统计-->
                <iframe frameborder="0" name="orderStatistics" width="100%" id="orderStatistics"
                    height="850px" src="" scrolling="auto"></iframe>
                <!--支付量统计-->
                <iframe frameborder="0" name="payAmountStatistics" width="100%" id="payAmountStatistics"
                    height="850px" src="" scrolling="auto"></iframe>
                <!--用户数统计-->
                <iframe frameborder="0" name="usersAmountStatistics" width="100%" id="usersAmountStatistics"
                    height="850px" src="" scrolling="auto"></iframe>
                <!--门店数统计-->
                <iframe frameborder="0" name="shopAmountStatistics" width="100%" id="shopAmountStatistics"
                    height="850px" src="" scrolling="auto"></iframe>
                <!--API访问次数统计-->
                <iframe frameborder="0" name="aPIAccessAmountStatistice" width="100%" id="aPIAccessAmountStatistice"
                    height="850px" src="" scrolling="auto"></iframe>
                <!--会员数统计-->
                <iframe frameborder="0" name="memberAmountStatistice" width="100%" id="memberAmountStatistice"
                    height="850px" src="" scrolling="auto"></iframe>
                <!--点单数据导出excel表格-->
                <iframe frameborder="0" name="dataExportExcel" width="100%" id="dataExportExcel"
                    height="850px" src="" scrolling="auto"></iframe>
                <!--消费周统计-->
                <iframe frameborder="0" name="consumptionWeekStatistics" width="100%" id="consumptionWeekStatistics"
                    height="850px" src="" scrolling="auto"></iframe>
                <!--下载页访问统计-->
                <iframe frameborder="0" name="qrCodePageViewStatistics" width="100%" id="qrCodePageViewStatistics"
                    height="850px" src="" scrolling="auto"></iframe>
                <!--门店订单统计-->
                <iframe frameborder="0" name="shopOrderListStatistics" width="100%" id="shopOrderListStatistics"
                    height="850px" src="" scrolling="auto"></iframe>
                <!--单店单日订单统计-->
                <iframe frameborder="0" name="singleShopDayOrderList" width="100%" id="singleShopDayOrderList"
                    height="850px" src="" scrolling="auto"></iframe>
                <!--时间段用户统计-->
                <iframe frameborder="0" name="orderCustomer" width="100%" id="orderCustomer" height="850px"
                    src="" scrolling="auto"></iframe>
                <!--员工积分相关统计-->
                <iframe frameborder="0" name="employeePoint" width="100%" id="employeePoint" height="850px"
                    src="" scrolling="auto"></iframe>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
