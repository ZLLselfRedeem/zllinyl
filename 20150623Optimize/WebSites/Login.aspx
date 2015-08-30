<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>悠先</title>
    <link rel="shortcut icon" href="images/icon@2x.ico"  type="image/x-icon"/>
    <link rel="bookmark" href="images/icon@2x.ico"  type="image/x-icon"/>
    <link href="css/19dian.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <style type="text/css">
        .login_table
        {
            width: 318px;
            height: 191px;
            position: absolute;
            left: 331px;
            top:42px;
            text-align:left;
        }
        .login_textbox
        {}
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
        //点击数字按钮
        function SetNumber(obj) {
            var old_value = currentTextBox.value;
            if (old_value == null) {
                old_value = "";
            }
            var new_value = old_value + obj;
            currentTextBox.value = new_value;
        }
        //清除
        function Clear() {
            var old_value = currentTextBox.value;
            var length = old_value.length;
            var new_value = old_value.substr(0, length - 1);
            currentTextBox.value = new_value;
        }
        //确定
        function Submit() {
            var TextBox_UserName = document.getElementById("TextBox_UserName");
            var TextBox_Password = document.getElementById("TextBox_Password");
            if (currentTextBox == TextBox_UserName && currentTextBox.value != "") {
                TextBox_Password.focus();
                currentTextBox = TextBox_Password;
            }
            if (currentTextBox == TextBox_Password && currentTextBox.value != "") {
                document.getElementById("ImageButton_Login").click();
            }
        }
        document.onkeydown = function (event) {
            e = event ? event : (window.event ? window.event : null);
            if (e.keyCode == 13) {
                document.getElementById("ImageButton_Login").click();
                return false;
            }
        } 

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="wrapper">
        <div class="header">
<%--            <div class="headerTop">
                <div class="headerTopNavigation right">
                    <ul>
                        <li><a>商户登录</a>|</li>
                        <li><a href="Register.aspx">商户注册</a>|</li>
                        <li><a href="ReportLoss.aspx">用户挂失</a></li>
                    </ul>
                </div>
            </div>--%>
            <div class="headerBottom">
                <div class="logo left">
                    <a class="logoa" title="悠先" href="index.aspx"></a>
                </div>
<%--                <div class="nav right">
                    <b class="rtop"><b class="r1"></b><b class="r2"></b><b class="r3"></b><b class="r4"></b></b>
                        <ul>
                            <li><a title="首页" href="index.htm">首页</a></li>
                            <li><a title="下载" href="index.htm#download">下载</a></li>
                            <li><a title="合作商户" href="index.htm#company">合作商户</a></li>
                            <li><a title="关于我们" href="index.htm#about">关于我们</a></li>
                        </ul>
                    <b class="rbottom"><b class="r4"></b><b class="r3"></b><b class="r2"></b><b class="r1"></b></b>
                </div>--%>
<%--                <div class="guanzhu">
                    <ul id="guanzhuList">
                        <li>关注我们</li>
                        <li><a href="http://weixin.qq.com/r/1HWIh33ERkM5h2zknyDw">
                            <img src="images/weixin.png" alt="wechat" style="border: none" /></a></li>
                        <li><a href="http://weibo.com/19dian/">
                            <img src="images/weibo.png" alt="weibo" style="border: none" /></a></li></ul>
                </div>--%>
            </div>
        </div>
        <br/>
        <div style="border-top:3px solid #474747; color:#474747; font-size:xx-large;font:黑体;width:940px;text-align:left">
        <br/>
        后台登录
        </div>
       
        <div class="content">
        <table class="login_table">
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    用户名：</td>
            </tr>
            <tr>
                <td style="width: 65%;">
                    <asp:TextBox ID="TextBox_UserName" runat="server" CssClass="login_textbox" 
                        onclick="GetCurrentTextBox(this)" BorderColor="#54C28B" 
                        BorderStyle="Solid" Height="32px" Width="242px" BorderWidth="1px" 
                        Font-Size="X-Large"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                      密&nbsp;码：</td>
                </tr>
                <tr>
                <td>
                    <asp:TextBox ID="TextBox_Password" runat="server" CssClass="login_textbox" onclick="GetCurrentTextBox(this)"
                        TextMode="Password" BorderColor="#F2645C" BorderStyle="Solid" 
                        Height="32px" Width="244px" BorderWidth="1px" Font-Size="X-Large"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="height:25;font-size:smaller">
                    <asp:Label ID="label_message" runat="server" ForeColor="#F2645C" 
                        Text="用户名或密码错误！" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
            <td>
                <asp:ImageButton ID="ImageButton_Login" runat="server" 
                    onclick="ImageButton_Login_Click" ImageUrl="~/Images/login.png" />
                &nbsp;&nbsp;&nbsp;<asp:ImageButton ID="ImageButton_Cancel" runat="server" 
                    onclick="ImageButton_Cancel_Click" ImageUrl="~/Images/hehe.png" />
            </td>
            </tr>
        </table>
        </div>
        <div class="copyright">
            <p style="margin-top:10px">
                www.u-xian.com&nbsp;&nbsp;&nbsp;&nbsp;All Rights Reserved 2012 &nbsp;&nbsp;&nbsp;&nbsp;
                浙ICP备12012727号&nbsp;&nbsp;&nbsp;&nbsp;友络科技旗下网站</p>
        </div>
         </div>
    </form>
</body>
</html>
