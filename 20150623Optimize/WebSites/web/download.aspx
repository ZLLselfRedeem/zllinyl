<%@ Page Language="C#" AutoEventWireup="true" CodeFile="download.aspx.cs" Inherits="ViewAllocWebSite_download" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>客户端下载</title>
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
        <div class="barSprite">
            <div class="bar download">
                <div class="text">
                    <h3>
                        免费下载</h3>
                    <div class="appSpirte">
                        <div class="app">
                            <a class="store" href="https://itunes.apple.com/cn/app/19dian-mei-shi-xian-mian-cuo/id571857633?ls=1&mt=8">
                                app Store viewalloc</a> <a class="android" href="http://uxian.oss-cn-hangzhou.aliyuncs.com/UploadFiles/uxian.apk">
                                    android viewalloc</a>
                        </div>
                    </div>
                    <div class="qrCode">
                        <img src="img/btn_down3.png" width="88px" height="88px" />
                        扫一扫二维码下载
                    </div>
                </div>
            </div>
        </div>
        <div class="page">
            <div class="appProfile">
                <div class="item pay">
                    <span class="icon">&nbsp;</span>
                    <h4>
                        快速点餐</h4>
                    <p class="txt">
                        极简的点菜流程和体验，选门店、选好菜、支付，随时随地可下单，帮您在吃饭上做减法！</p>
                </div>
                <div class="item shop">
                    <span class="icon">&nbsp;</span>
                    <h4>
                        优质餐厅</h4>
                    <p class="txt">
                        外婆家、炉鱼、老头儿油爆虾、新白鹿、西贝莜面村……知名餐厅持续增加，让您时时有新选择，吃不腻！</p>
                </div>
                <div class="item back">
                    <span class="icon">&nbsp;</span>
                    <h4>
                        尾号验证</h4>
                    <p class="txt">
                        点好菜后，到店只需报出手机4位尾号和昵称，就能轻松就餐，再也没有更便捷的方式了！</p>
                </div>
                <div class="item surprise">
                    <span class="icon">&nbsp;</span>
                    <h4>
                        红包优惠</h4>
                    <p class="txt">
                        每日可领的“天天红包”、每周可领的“周红包”、特殊日子派发的节日红包，新老用户来就有，红包不停歇。不打折门店也能抵扣，吃饭好实惠！</p>
                </div>
            </div>
            <%-- <div>
                更新历史 <a href="updateHistoryList.aspx">更多>></a>
                <hr />
            </div>
            <div>
                <asp:Repeater ID="rpHistory" runat="server">
                    <ItemTemplate>
                        <ul>
                            <li><span>
                                <%# Eval("date")%></span> <a href='updateHistoryDetail.aspx?number=<%# Eval("number") %>'>
                                    <%# Eval("title") %></a></li>
                        </ul>
                    </ItemTemplate>
                </asp:Repeater>
            </div>--%>
            <div class="version">
                <div class="header">
                    更新历史<a class="more" href="updateHistoryList.aspx">查看更多 <code>&gt;&gt;</code></a></div>
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
