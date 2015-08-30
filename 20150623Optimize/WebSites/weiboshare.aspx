<%@ Page Language="C#" AutoEventWireup="true" CodeFile="weiboshare.aspx.cs" Inherits="weiboshare" %>

<!DOCTYPE html PUBLIC "-//WAPFORUM//DTD XHTML Mobile 1.0//EN" "http://www.wapforum.org/DTD/xhtml-mobile10.dtd">
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>悠先点菜</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" />
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="AppPages/js/jquery.cookie.js" type="text/javascript"></script>
    <script src="Scripts/CommonScript.js" type="text/javascript"></script>
    <script src="AppPages/js/CommonScript.js" type="text/javascript"></script>
    <style type="text/css">
        table tr th
        {
            border-bottom: 2px solid #000000;
            text-align: left;
        }
        a
        {
            cursor: pointer;
        }
        a:link
        {
            text-decoration: none;
        }
        a:visited
        {
            text-decoration: none;
        }
        a:active
        {
            text-decoration: none;
        }
        a:hover
        {
            text-decoration: none;
        }
        .divImageBig
        {
            width: 600px;
            height: 450px;
            position: absolute;
            left: 50%;
            top: 25%;
            margin-left: -300px;
            border: solid #000000 3px;
            z-index: 50;
            background: url(Images/loadimg.gif) no-repeat center center;
            background-color: #F7F7F7;
        }
        .divImageSmall
        {
            width: 272px;
            height: 204px;
            position: absolute;
            left: 50%;
            top: 25%;
            margin-left: -136px;
            border: 2px solid #000000;
            z-index: 50;
            background: url(Images/loadimg.gif) no-repeat center center;
            background-color: #F7F7F7;
        }
        .closeImg
        {
            position: absolute;
            top: -17px;
            right: -17px;
            z-index: 51;
        }
        .dishImage
        {
            display: none;
        }
        
        .DishNameClass
        {
            text-decoration: underline;
            color: Blue;
            -webkit-tap-highlight-color: rgba(0,0,0,0);
        }
        
        .btn_app
        {
            display: block;
            border: 0px;
            background: url(Images/appdownloadImg/btn_app.png) center top no-repeat;
            width: 172px;
            height: 40px;
            margin: 7px auto 10px auto;
        }
        .tips
        {
            width: 172px;
            text-align: center;
            margin: 0 auto;
            font-family: "微软雅黑" , "宋体";
            padding: 0;
        }
    </style>
    <script type="text/javascript">
        //var isDingcookie = 0;
        var isDingcookie = new Array();
        var preorderId = GetRequestParam("value");
        var imageScale;

        //检测请求时来自手机还是来自pc
        function checkMobilePhone() {
            var isMobilePhone = checkuserAgent();
            if (isMobilePhone) {
                //是手机，包括ipad，进一步判断分辨率
                if (screen.width < 600) {
                    imageScale = 1; //小图
                    $("#divImage").addClass("divImageSmall");
                }
                else {
                    //imageScale = 0; //大图
                    //$("#divImage").addClass("divImageBig");
                    imageScale = 1; //小图
                    $("#divImage").addClass("divImageSmall");
                }
            }
            else {
                //是pc
                //imageScale = 0;
                //$("#divImage").addClass("divImageBig");
                imageScale = 1; //小图
                $("#divImage").addClass("divImageSmall");
            }
        }
        $().ready(function () {

            checkMobilePhone();
            $("#divImage").hide();
            //wangcheng
            // Getdingcount();
            //判断cookie
            if ($.cookie("isDingcookiearray") == undefined || $.cookie("isDingcookiearray") == "undefined") {//没有cookie
                $("#isDingornot").attr("src", "Images/QrcodeImg/yesDing.png");
            }
            else {
                var iscook = $.cookie("isDingcookiearray");
                isDingcookie = eval("(" + iscook + ")"); //获得的是当前定过的所有的id
                for (var i = 0; i < isDingcookie.length; i++) {
                    if (isDingcookie[i].preorderId == preorderId) {
                        $("#isDingornot").attr("src", "Images/QrcodeImg/noDing.png");
                        break; //千万别忘记了，否则循环会继续执行
                    }
                    else {
                        $("#isDingornot").attr("src", "Images/QrcodeImg/yesDing.png");
                    }
                }
            }
        });

        function Ding() {

            var urlyesDing = "Images/QrcodeImg/noDing.png";
            var imgsrc = $("#isDingornot").attr("src");
            if (imgsrc != urlyesDing) {
                var preorderListJson = new PreorderListJson();
                preorderListJson.preorderId = preorderId;
                isDingcookie.push(preorderListJson);
                var isDingcookiearray = $.toJSON(isDingcookie);
                $.cookie("isDingcookiearray", isDingcookiearray, { expires: 36500 });
                $("#isDingornot").attr("src", urlyesDing);
                Getdingcount();
            }
        }

        function ShowImage(dishPriceI18nId, self) {
            var top = ($(window).height() - $("#divImage").outerHeight()) / 2 + $(window).scrollTop();
            $("#imgId").attr("src", "image.aspx?type=118&value=" + dishPriceI18nId + "&scale=" + imageScale);
            $("#imgId").css("display", "block");
            $("#divImage").css({ "display": "block", "top": top, "position": "absolute" });
            $("#divImage").show('slow');

        }

        function HideThis() {
            $("#divImage").hide('slow');
        }

        function Getdingcount() {
            $.ajax({
                type: "Post",
                url: "weiboshare.aspx/GetDingCount",
                data: "{preorderId:'" + preorderId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                },
                success: function (data) {
                    if (data.d != "") {
                        var dingcount = eval("(" + data.d + ")");
                        document.getElementById("yidingcount").innerText = dingcount;
                    }
                },
                error: function (XmlHttpRequest, textStatus, errorThrown) {
                    alert(XmlHttpRequest.responseText);
                }
            });
        }
        function PreorderListJson() {
            var preorderId;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="border: 1px dashed #000000; padding: 5px; width: 96%; margin: 0 auto;">
        <table align="center" style="width: 100%; font-size: 15px;">
            <tr>
                <td>
                    <table>
                        <tr>
                            <td style="font-size: 50px; font-weight: bold;">
                                <a href="http://u-xian.com">
                                    <img id="logo_company" src="" alt="logo" runat="server" /></a> 预点单
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 15px; font-weight: bold;">
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 15px; font-weight: bold;">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <!-- wangcheng -->
            <tr>
                <td>
                    <table border="0" cellspacing="0" cellpadding="0" width="100%" style="text-align: center;
                        vertical-align: middle">
                        <tr style="font-size: 16px;" runat="server" id="weektr">
                            <td>
                                <label id="week">
                                    每周免单中奖率已打败<asp:Label ID="week_label" runat="server" Text=""></asp:Label>的对手</label>
                            </td>
                        </tr>
                        <tr style="font-size: 16px;" runat="server" id="monthtr">
                            <td>
                                <label id="month">
                                    每月iPhone中奖率已打败<asp:Label ID="month_label" runat="server" Text=""></asp:Label>的对手</label>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 20px; vertical-align: middle">
                                <img onclick="Ding()" id="isDingornot" alt="support" />
                                已顶<asp:Label ID="yidingcount" Style="color: red" runat="server" Text=""></asp:Label>次
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #000000;">
                    <asp:Repeater ID="Repeater_Order" runat="server">
                        <HeaderTemplate>
                            <table cellpadding="10" cellspacing="0" style="width: 100%">
                                <tr>
                                    <th>
                                        品名
                                    </th>
                                    <th>
                                        数量
                                    </th>
                                    <th>
                                        单价
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <a href="javascript:;" class="DishNameClass" onclick="ShowImage('<%#DataBinder.Eval(Container.DataItem, "dishPriceI18nId")%>',this);">
                                        <%#DataBinder.Eval(Container.DataItem, "dishName")%>
                                        &nbsp;图</a>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container.DataItem, "quantity")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container.DataItem, "unitPrice")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table></FooterTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="text-align: right; width: 100%">
                        <tr>
                            <td style="font-size: 15px; font-weight: bold; text-align: center;">
                                <asp:Label ID="Label_message" runat="server" Text="该点单号无效！" Font-Bold="true" Font-Size="Larger"
                                    ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 15px; font-weight: bold">
                                <asp:Label ID="Label_quantitySum" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 15px; font-weight: bold">
                                <asp:Label ID="Label_orderSum" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label_orderTime" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                客服热线：400-808-7017
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 15px;">
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <img src="Images/QrcodeImg/wxmp_qrcode.png" alt="QRCode" style="width: 200px; height: 200px" />
                                <a class="btn_app" href="http://viewalloc.com/appdownload.aspx" title="下载悠先"></a>
                            </td>
                        </tr>
                        <tr style="font-size: 14px; text-align: center">
                            <td style="height: 20px;">
                                <p class="tips">
                                    关注<label style="color: red">悠先点菜</label>公众微信获取中奖信息和app下载链接</p>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
    </div>
    </form>
    <div id="divImage" onclick="HideThis();">
        <img src="Images/Close2.png" class="closeImg" alt="close" />
        <img src="" id="imgId" class="dishImage" alt="dish" onerror="javascript:this.style.display='none'" />
    </div>
</body>
</html>
