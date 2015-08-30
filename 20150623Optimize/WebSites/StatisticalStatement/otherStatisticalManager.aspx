<%@ Page Language="C#" AutoEventWireup="true" CodeFile="otherStatisticalManager.aspx.cs"
    Inherits="StatisticalStatement_otherStatisticalManager" %>

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
            orderCustomerUrl = "orderCustomer.aspx";
            $("#first li:first-child").addClass("current").siblings().removeClass("current");
            $("#orderCustomer").attr("src", orderCustomerUrl);
            $("#employeePoint").hide();
        });
        function ClickTwelveLi() {
            $("#first li").eq(0).addClass("current").siblings().removeClass("current");
            $("#orderCustomer").attr("src", "orderCustomer.aspx").show();
            $("#employeePoint").hide();
        }
        function ClickThirteenLi() {
            $("#first li").eq(1).addClass("current").siblings().removeClass("current");
            $("#employeePoint").attr("src", "employeePoint.aspx").show();
            $("#orderCustomer").hide();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <uc2:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
        navigationText="" navigationUrl="" headName="报表统计管理" />
    <div id="box" class="box">
        <div class="tagMenu" id="tagMenu">
            <ul class="menu" id="first" style="width: 1200px">
                <li onclick="ClickTwelveLi()">时间段用户统计</li>
                <li onclick="ClickThirteenLi()">员工积分相关统计</li>
            </ul>
        </div>
        <div class="content">
            <div class="layout">
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
