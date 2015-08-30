<%@ Page Language="C#" AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="ViewAllocWebSite_register" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>商户注册</title>
    <link type="text/css" rel="stylesheet" href="css/cssbase.css" />
    <link type="text/css" rel="stylesheet" href="css/common.css" />
    <script src="js/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="js/jquery.validate.js" type="text/javascript"></script>
    <style type="text/css">
        label.error
        {
            color: Red;
            font-size: 10pt;
            padding-left: 10px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $.validator.addMethod("textBox_UserName_Rule", function (value, element) {
                var length = value.length;
                var your_tel = /[\W]/g; ;
                return this.optional(element) || (length == 11) && !your_tel.test(value);
            }, "手机号码有误");
            $("#form1").validate({
                rules: {
                    TextBox_UserName: {
                        required: true,
                        textBox_UserName_Rule: true,
                        remote: { url: "Register.aspx/QueryEmployeeByName"}//表示检测当前手机号码是否已注册
                    },
                    TextBox_CompanyName: {
                        required: true,
                        remote: { url: "Register.aspx/CompanyNameValid" }
                    },
                    TextBox_VerificationCode: { required: true },
                    TextBox_OwndCompany: { required: true },
                    TextBox_CompanyTelePhone: { required: true },
                    TextBox_ContactPerson: { required: true },
                    TextBox_ContactPhone: { required: true },
                    TextBox_CompanyAddress: { required: true }
                },
                messages: {
                    TextBox_UserName: { required: "手机号码不能为空", remote: "该手机号码已注册", textBox_UserName_Rule: "请填写正确的手机号码" },
                    TextBox_VerificationCode: { required: "验证码不能为空" },
                    TextBox_CompanyName: { required: "品牌名称不能为空", remote: "该品牌名称已经存在" },
                    TextBox_OwndCompany: { required: "所属公司不能为空" },
                    TextBox_CompanyTelePhone: { required: "电话不能为空" },
                    TextBox_ContactPerson: { required: "联系人不能为空" },
                    TextBox_ContactPhone: { required: "联系电话不能为空" },
                    TextBox_CompanyAddress: { required: "公司地址不能为空" }
                },
                errorPlacement: function (error, element) {
                    if (element.is(":radio"))
                        error.appendTo(element.parent().next().next());
                    else if (element.is(":checkbox"))
                        error.appendTo(element.next());
                    else
                        error.appendTo(element.parent());
                },
                success: function (label) {
                    label.html("&nbsp;").addClass("checked");
                }
            });
        });
        function RegiserComplete() {
            if (confirm('恭喜你注册成功！')) {
                window.location.href = '../CompanyPages/page.aspx';

            } else {
                window.location.href = 'Login.aspx';
            }
        }
        function btn_getVerificationCode_Click() {
            var phoneNum = $("#TextBox_UserName").val();
            if (phoneNum.length != 11) {
                alert("请输入11有效手机号码");
                return;
            }
            else {
                $.ajax({
                    type: "Post",
                    url: "register.aspx/GetVerificationCode",
                    data: "{'phoneNum':'" + phoneNum + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data.d != "" && data.d != null && data.d == "ok") {
                            alert("发送成功");
                        }
                        else {
                            alert("发送失败，请重试");
                        }
                    },
                    error: function (XmlHttpRequest, textStatus, errorThrown) {
                        alert("发送失败，请重试");
                    }
                });
            }
        };
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
                            href="register.aspx">商户注册</a>|<a href="../Login.aspx">后台管理</a></p>
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
                    商户注册</h2>
                <p>
                    <span class="title">手机号码</span>
                    <asp:TextBox ID="TextBox_UserName" runat="server" CssClass="inputText" onkeyup="this.value=this.value.replace(, '')"></asp:TextBox>
                    <input id="btn_getVerificationCode" type="button" style="width: 90px" onclick="btn_getVerificationCode_Click();" value="获取验证码" />
                </p>
                <p>
                    <span class="title">验证码</span>
                    <asp:TextBox ID="TextBox_VerificationCode" runat="server" CssClass="inputText"></asp:TextBox></p>
                <p>
                    <span class="title">品牌名称</span>
                    <asp:TextBox ID="TextBox_CompanyName" runat="server" CssClass="inputText"></asp:TextBox></p>
                <p>
                    <span class="title">所属公司</span><asp:TextBox ID="TextBox_OwndCompany" runat="server"
                        CssClass="inputText"></asp:TextBox></p>
                <p>
                    <span class="title">电 话</span><asp:TextBox ID="TextBox_CompanyTelePhone" runat="server"
                        CssClass="inputText"></asp:TextBox></p>
                <p>
                    <span class="title">联 系 人</span>
                    <asp:TextBox ID="TextBox_ContactPerson" runat="server" CssClass="inputText"></asp:TextBox></p>
                <p>
                    <span class="title">联系电话</span>
                    <asp:TextBox ID="TextBox_ContactPhone" runat="server" CssClass="inputText"></asp:TextBox></p>
                <p>
                    <span class="title">公司地址</span>
                    <asp:TextBox ID="TextBox_CompanyAddress" runat="server" CssClass="inputText"></asp:TextBox></p>
                <p>
                    <asp:Label ID="label_message" runat="server" ForeColor="#CC0000"></asp:Label></p>
                <div class="btnSprite">
                    <asp:Button runat="server" ID="Button_register" OnClick="Button_register_Click" class="btn"
                        Text="注册" Height="42px" Width="160px" />
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
