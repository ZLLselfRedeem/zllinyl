<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RecommendShopInfo.aspx.cs" Inherits="AppPages_RecommendShopInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>悠先</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" />
    <script src="js/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/app/mobile.js"></script>
</head>
<body style="background-color: White">
    <div data-role="page">
        <div style="height:42px; padding-left:15px; margin-top:0px; background-color:#f8f8ff;">
            <h2 style="margin:8px auto;"><asp:Label ID="rType" ForeColor="#ff6101" runat="server"></asp:Label></h2>
        </div>
        <div data-role="content" style="margin-top: 2px; background-color:#eeeeee; padding:15px;">
            <div id="div_company">
                <asp:Image ID="img1" Width="120px" Height="90px" runat="server" />
                <asp:Image ID="img2" Width="120px" Height="90px" runat="server" />
                <table style="line-height:30px;font-size:12pt;">
                    <tr><td><asp:Label ID="lblName" runat="server" ></asp:Label></td></tr>
                    <tr><td><asp:Label ID="lblAddr" runat="server" ></asp:Label></td></tr>
                    <tr><td><asp:Label ID="lblTel" runat="server" ></asp:Label></td></tr>
                </table>
            </div>
        </div>
        <script type="text/javascript">
            $(document).ready(function () {
                var obj = VA.mobile.widgets.getPageSize();
                var imgW = (parseInt(obj["winW"]) - 50) / 2;
                $("#img1").css("width", imgW);
                $("#img1").css("height", imgW);
                $("#img2").css("width", imgW);
                $("#img2").css("height", imgW);
            });
        </script>
    </div>
</body>
</html>