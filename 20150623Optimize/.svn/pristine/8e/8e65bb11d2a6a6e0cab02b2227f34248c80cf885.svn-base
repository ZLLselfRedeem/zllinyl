<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>悠先点菜 - 新一代吃货必备的app</title>
    <link type="text/css" rel="stylesheet" href="web/css/cssbase.css" />
    <link type="text/css" rel="stylesheet" href="web/css/common.css" />
    <script type="text/javascript" src="web/js/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="web/js/index.js"></script>
</head>
<body>
    <form id="form1" runat="server">
	<div class="pageHeaderContainer">
    <div class="pageHeaderSprite">
        <div class="pageHeader">
            <div class="logo">
                <img src="web/img/viewalloc_logo.png" width="255px" height="131px" alt="悠先"
                    title="悠先手机app应用餐饮高端移动服务" />
            </div>
            <div class="text">
                <p class="topLink">
                    <span class="welcome">您好，欢迎来到悠先网！</span> <a href="CompanyPages/login.aspx">商户登录</a>|<a
                        href="web/register.aspx">商户注册</a>|<a href="Login.aspx">后台管理</a></p>
                <ul class="menu">
                    <li><a href="index.aspx" class="cur">首页</a></li>
                    <li><a href="web/product.htm">产品中心</a></li>
                    <li><a href="web/download.aspx">客户端下载</a></li>
                    <li><a href="web/service.aspx">商户服务</a></li>
                </ul>
            </div>
        </div>
    </div>
	</div>
    <div id="indexContainer">
        <div class="barSprite">
            <div class="bar">
                <div class="app">
					<a class="download" href="web/download.aspx">免费下载</a>
                    <a class="download-uxian-1" href="http://uxian.oss-cn-hangzhou.aliyuncs.com/UploadFiles/poslite.exe">免费下载</a>
                    <p>
                        温馨提醒：支付后可以随时吃，随意退！无后顾之忧！<a href="web/serviceTerms.htm">服务条款</a></p>
                </div>
            </div>
        </div>
        <div class="section">
            <div class="company" id="company">
                <div class="header">
                    <h2>
                        最新合作商户</h2>
                    <p id="com_tab">
                        <a href="javascript:;">杭州</a> | <a href="javascript:;">上海</a> | <a href="javascript:;">
                            北京</a> | <a href="javascript:;">广州</a> | <a href="javascript:;">深圳</a></p>
                </div>
                <ul class="text">
                    <asp:Repeater runat="server" ID="rpHangzhou">
                        <ItemTemplate>
                            <li><a href="#">
                                <img src="<%# Eval("shopLogo") %>" width="110px" height="110px" />
                            </a></li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
                <ul class="text">
                    <asp:Repeater runat="server" ID="rpShanghai">
                        <ItemTemplate>
                            <li><a href="#">
                                <img src="<%# Eval("shopLogo") %>" width="110px" height="110px" />
                            </a></li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
                <ul class="text">
                    <asp:Repeater runat="server" ID="rpBeijing">
                        <ItemTemplate>
                            <li><a href="#">
                                <img src="<%# Eval("shopLogo") %>" width="110px" height="110px" />
                            </a></li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
                <ul class="text">
                    <asp:Repeater runat="server" ID="rpGuangzhou">
                        <ItemTemplate>
                            <li><a href="#">
                                <img src="<%# Eval("shopLogo") %>" width="110px" height="110px" />
                            </a></li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
                <ul class="text">
                    <asp:Repeater runat="server" ID="rpShenzhen">
                        <ItemTemplate>
                            <li><a href="#">
                                <img src="<%# Eval("shopLogo") %>" width="110px" height="110px" />
                            </a></li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
            <div class="news">
                <div class="header">
                    <h2>
                        最新动态</h2>
                    <a href="web/recentNewsList.aspx">更多<code>&gt;&gt;</code></a>
                </div>
                <ul class="text">
                    <asp:Repeater ID="rpNews" runat="server">
                        <ItemTemplate>
                            <ul>
                                <%--   <li><a href='recentNewsDetail/<%# Eval("number") %>.html'>
                                    <%# Eval("title") %></a><span class="date">
                                        <%# Eval("date")%></span> </li>--%>
                                <li><a href='web/recentNewsDetail.aspx?number=<%# Eval("id") %>'>
                                    <%# Eval("title") %></a><span class="date">
                                        <%# Eval("date")%></span> </li>
                            </ul>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
    </div>
    <div class="footerSprite">
        <div class="footer">
            <p class="copyright">
                <em>©</em>2013 杭州友络软件科技有限公司 版权所有 浙ICP备12012727号</p>
            <ul class="bottomLink">
                <li><a href="web/aboutUs.htm">关于我们</a> |</li>
                <li><a href="web/contactUs.htm">联系我们</a> |</li>
                <li><a href="web/serviceTerms.htm">服务条款</a> |</li>
                <li><a href="web/link.htm">友情链接</a></li>
            </ul>
        </div>
    </div>
    </form>
	<script type="text/javascript">
	var _bdhmProtocol = (("https:" == document.location.protocol) ? " https://" : " http://");
	document.write(unescape("%3Cscript src='" + _bdhmProtocol + "hm.baidu.com/h.js%3F2ccaa20e3ab062b2e2ec2909021a030e' type='text/javascript'%3E%3C/script%3E"));
	</script>
</body>
</html>
