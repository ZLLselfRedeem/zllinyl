<%@ Page Language="C#" AutoEventWireup="true" CodeFile="service.aspx.cs" Inherits="ViewAllocWebSite_service" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>商户服务</title>
    <link type="text/css" rel="stylesheet" href="css/cssbase.css" />
    <link type="text/css" rel="stylesheet" href="css/common.css" />
    <style type="text/css">
        img
        {
            width: 100;
            height: 100;
        }
    </style>
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
        <div class="barSprite">
            <div class="bar">
                <img src="upload/banner_1.png" width="960px" height="240px" />
            </div>
        </div>
        <div class="page">
            <div class="appProfile">
                <div class="barComment">
                    <div class="title">
                        <h2>
                            商户服务</h2>
                    </div>
                </div>
                <div class="item free">
                    <span class="icon">&nbsp;</span>
                    <h4>
                        <span class="index">1.</span>红包补贴</h4>
                    <p class="txt">
                       悠先平台给消费者长期补贴红包，有效培养用户使用习惯，提升用户粘度。</p>
                </div>
                <div class="item quan">
                    <span class="icon">&nbsp;</span>
                    <h4>
                        <span class="index">2.</span>设置优惠券</h4>
                    <p class="txt">
                        商户可以在悠先平台上给达到一定消费条件的顾客发放优惠券、抵价券，拉动顾客复购率。</p>
                </div>
                <div class="item marketing">
                    <span class="icon">&nbsp;</span>
                    <h4>
                        <span class="index">3.</span>精准营销</h4>
                    <p class="txt">
                        利用悠先首页app广告位、微博、微信公众号、推送等免费资源精准化营销，提高客单价、唤醒沉睡老客户等均有成功案例。</p>
                </div>
                <div class="item account">
                    <span class="icon">&nbsp;</span>
                    <h4>
                        <span class="index">4.</span>商户结算</h4>
                    <p class="txt">
                        消费者先付款后吃饭，商户不用担心客户跑单。悠先与商户合作产生的流水，可即时结算，方便灵活。</p>
                </div>
            </div>
            <div class="case">
                <div class="header">
                    精选成功案例</div>
                <ul class="text">
                    <asp:Repeater runat="server" ID="rpCase">
                        <ItemTemplate>
                            <li>
                                <img src='<%# Eval("content") %>' alt="" width="267" height="200" />
                                <p class="name">
                                    <a href="#">
                                        <%# Eval("title") %></a></p>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        <%--    <div>
                <%=strHtml%>
            </div>--%>
            <div class="caseCompany">
                <div class="header">
                    部分合作商户</div>
                <ul class="text">
                    <asp:Repeater runat="server" ID="rpCooperate">
                        <ItemTemplate>
                            <li><a href="javascript:;">
                                <img src='<%# Eval("content") %>' alt="" width="110" height="110" />
                            </a></li>
                        </ItemTemplate>
                    </asp:Repeater>
                    <%--<li><a href="#">
                        <img src="img/pic_com.png" width="110px" height="110px" /></a></li>                    
                    <li><a href="#">
                        <img src="img/pic_com.png" width="110px" height="110px" /></a></li>--%>
                </ul>
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
