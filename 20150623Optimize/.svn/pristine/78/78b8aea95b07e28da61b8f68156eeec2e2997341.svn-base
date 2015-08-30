<%@ Page Language="C#" AutoEventWireup="true" CodeFile="reportLoss.aspx.cs" Inherits="ViewAllocWebSite_reportLoss" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>用户挂失</title>
    <link type="text/css" rel="stylesheet" href="css/cssbase.css" />
    <link type="text/css" rel="stylesheet" href="css/common.css" />
    <%--<link rel="shortcut icon" href="images/icon@2x.ico" type="image/x-icon" />
    <link rel="bookmark" href="images/icon@2x.ico" type="image/x-icon" />
    <link href="css/19dian.css" rel="stylesheet" type="text/css" />--%>
    <script src="js/jquery-1.4.1.min.js" type="text/javascript"></script>
    <style type="text/css">
        .reportLoss_table
        {
            width: 318px;
            height: 191px;
            position: absolute;
            left: 331px;
            top: 42px;
            text-align: left;
        }
        .reportLoss_textbox
        {
        }
        .error_css
        {
            visibility: visible;
            font-size: 10pt;
            color: Red;
        }
    </style>
    <script type="text/javascript">
        var currentTextBox;
        $(document).ready(function () {
            currentTextBox = document.getElementById("TextBox_UserName");
        });
        //获取当前光标所在的控件
        function GetCurrentTextBox(textBox) {
            currentTextBox = textBox;
        }
        document.onkeydown = function (event) {
            e = event ? event : (window.event ? window.event : null);
            if (e.keyCode == 13) {
                document.getElementById("ImageButton_reportLoss").click();
                return false;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="pageHeaderContainer">
        <div class="pageHeaderSprite">
            <div class="pageHeader">
                <div class="logo">
                    <img src="img/viewalloc_logo.png" width="255px" height="131px" alt="悠先" title="悠先手机app应用餐饮高端移动服务" />
                </div>
                <div class="text">
                    <p class="topLink">
                        <span class="welcome">您好，欢迎来到悠先网！</span> <a href="../CompanyPages/login.aspx">商户登录</a>|<a
                            href="register.aspx">商户注册</a>|<a href="reportLoss.aspx">会员挂失</a></p>
                    <ul class="menu">
                        <li><a href="../index.aspx">首页</a></li>
                        <li><a href="product.htm">产品中心</a></li>
                        <li><a href="download.aspx">客户端下载</a></li>
                        <li><a href="service.aspx" class="cur">商户服务</a></li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="pageContainer">
        <div class="page user">
            <div class="comsign">
                <h2 class="headerItem">
                    会员挂失</h2>
                <p>
                    <span class="title">手机号码</span>
                    <asp:TextBox ID="TextBox_MobliePhone" runat="server" CssClass="inputText" onclick="GetCurrentTextBox(this)"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ControlToValidate="TextBox_MobliePhone"
                        CssClass="error_css" Text=" *必填"></asp:RequiredFieldValidator>
                </p>
                <p>
                    <span class="title">密&nbsp;&nbsp;码</span>
                    <asp:TextBox ID="TextBox_Password" runat="server" CssClass="inputText" onclick="GetCurrentTextBox(this)"
                        TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="TextBox_Password"
                        CssClass="error_css" Text=" *必填"></asp:RequiredFieldValidator>
                </p>
                <div class="btnSprite">
                    <asp:Button runat="server" ID="Button_register" OnClick="Button_ReportLoss_Click"
                        class="btn" Text="挂失" Height="46px" Width="160px" />
                </div>
                <div>
                    <asp:Label ID="label_message" runat="server" ForeColor="#F2645C" Text="" Visible="False"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <div class="footerSprite">
        <div class="footer">
            <p class="copyright">
                <em>©</em>2013 杭州友络软件科技有限公司 版权所有 浙ICP备12012727号</p>
            <ul class="bottomLink">
                <li><a href="aboutUs.htm">关于我们</a> |</li>
                <li><a href="contactUs.htm">联系我们</a> |</li>
                <li><a href="serviceTerms.htm">服务条款</a> |</li>
                <li><a href="link.htm">友情链接</a></li>
            </ul>
        </div>
    </div>
    </form>
</body>
</html>
