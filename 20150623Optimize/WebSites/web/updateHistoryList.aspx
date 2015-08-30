﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="updateHistoryList.aspx.cs"
    Inherits="ViewAllocWebSite_updateHistoryList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>更新历史</title>
    <link type="text/css" rel="stylesheet" href="css/cssbase.css" />
    <link type="text/css" rel="stylesheet" href="css/common.css" />
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
                        <li><a href="download.aspx" class="cur">客户端下载</a></li>
                        <li><a href="service.aspx">商户服务</a></li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="pageContainer">
        <div class="page version">
            <div class="header">
                更新历史</div>
            <div class="text">
                <asp:Repeater ID="rpHistory" runat="server">
                    <ItemTemplate>
                        <div class="item">
                            <dl>
                                <dt>
                                    <%# Eval("date")%><em><%# Eval("remark")%></em></dt>
                                <dd class="txt">
                                    <h3>
                                        <%# Eval("title") %></h3>
                                    <%# Eval("content") %>
                                </dd>
                            </dl>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <p class="pageTxt">
                <code>&lt;&lt;</code><asp:HyperLink ID="lnkUp" runat="server">上一页</asp:HyperLink>
                <asp:HyperLink ID="lnkDown" runat="server">下一页</asp:HyperLink><code>&gt;&gt;</code>
                <asp:Label ID="lbl_info" runat="server" Text="当前第x页,共x页"></asp:Label>
            </p>
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
