<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerManage.aspx.cs" Inherits="Customer_CustomerManage" %>

<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>客户信息</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="../Scripts/messages_cn.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            TabManage();
            var customerId = queryStringByName("customerId");
            var cookie = queryStringByName("cookie");
            var customerBaseSrc = "CustomerUpdate.aspx?customerId=" + customerId;
            $("#customerBase").attr("src", customerBaseSrc);
        });
        function queryStringByName(queryName) {
            var str = location.href; //取得整个地址栏
            if (str.indexOf("?") > -1) {
                var queryParam = str.substring(str.indexOf("?") + 1);
                //如果有多个参数
                //if (queryParam.indexOf("&") > -1)
                var param = queryParam.split("&");
                for (var a = 0; a < param.length; a++) {
                    var query = param[a].split("=");
                    if (query[0] == queryName) {
                        return query[1];
                    }
                }
            }
            return "";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <uc2:HeadControl ID="HeadControl1" runat="server" headName="客户信息" navigationImage="~/images/icon/list.gif"
        navigationText="客户列表" navigationUrl="~/Customer/CustomerList.aspx" />
    <div id="box" class="box">
        <div class="tagMenu">
            <ul class="menu">
                <li>基本信息</li>
            </ul>
        </div>
        <div class="content">
            <div class="content">
                <div class="layout">
                    <iframe frameborder="0" name="customerBase" width="100%" id="customerBase" height="99%"
                        src="" scrolling="no"></iframe>
                </div>
                <div class="layout">
                    <iframe frameborder="0" name="customerCoupon" width="100%" id="customerCoupon" height="99%"
                        src="" scrolling="no"></iframe>
                </div>
                <div class="layout">
                    <iframe frameborder="0" name="customerMoney19dianDetail" width="100%" id="customerMoney19dianDetail"
                        height="99%" src="" scrolling="auto"></iframe>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
