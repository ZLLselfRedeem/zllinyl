<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShopAdd.aspx.cs" Inherits="ShopManage_ShopAdd" %>

<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>门店添加</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="../Scripts/messages_cn.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=xIaOmBpthTUf8zF1WurZyBkU"></script>
    <script src="../Scripts/searchShop.js" type="text/javascript"></script>
    <style type="text/css">
        #allmap, #Gmap, #GDmap {
            width: 300px;
            height: 280px;
            overflow: hidden;
            margin: 0;
        }

        p:hover, li:hover {
            text-decoration: underline;
            color: #39c;
            cursor: pointer;
        }
    </style>
    <script type="text/javascript">
        $().ready(function () {
            initData("addshop");
            TabManage();
            $("#form1").validate({
                rules: {
                    TextBox_shopName: {
                        required: true,
                        remote: "ShopAdd.aspx/QueryShopName"
                        //                        remote: {
                        //                            type: "Post",
                        //                            url: "ShopAdd.aspx/QueryShopName",
                        //                            data: {
                        //                                shopName: function () {
                        //                                    return $("#TextBox_shopName").val();
                        //                                }
                        //                            }
                        //                        }
                    },
                    TextBox_LongitudeBaidu: { number: true, required: true },
                    TextBox_LatitudeBaidu: { number: true, required: true },

                    TextBox_shopDescription: { rangelength: [0, 75] },
                    TextBox_shopRating: { number: true, range: [0, 5], rangelength: [1, 3] },
                    TextBox_accp: { required: true },
                    TextBox_shopAddress: { required: true },
                    tb_notPaymentReason: { rangelength: [0, 100] }
                },
                messages: {
                    TextBox_shopName: { required: "请输入门店名", remote: "该店铺名称已经存在" },
                    TextBox_LongitudeBaidu: "请输入正确的经度",
                    TextBox_LatitudeBaidu: "请输入正确的纬度",
                    TextBox_shopDescription: "最多输入75个字符",
                    TextBox_shopRating: "请输入一个介于0~5之间的值，最多保留一位小数",
                    TextBox_accp: "人均只能不小于0为整数或小数",
                    TextBox_shopAddress: "请输入门店地址",
                    tb_notPaymentReason: "最多输入100个字符"
                }
            });
            if ((navigator.userAgent.indexOf('MSIE') >= 0) && (navigator.userAgent.indexOf('Opera') < 0)) {
                return;
            }
            else {
                document.getElementById('text').addEventListener("input", keyTest, true);
            }
        });
        function QueryShopCoordinate() {
            var shopDetailAddress = $("#DropDownList_provinceID").find("option:selected").text() + $("#DropDownList_cityID").find("option:selected").text() + $("#DropDownList_countyID").find("option:selected").text() + $("#TextBox_shopAddress").val();
            var cityName = $("#DropDownList_cityID").find("option:selected").text();
            $.ajax({
                type: "Post",
                url: "ShopAdd.aspx/GetShopCoordinate",
                data: "{ 'shopDetailAddress': '" + shopDetailAddress + "','cityName': '" + cityName + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var shopCoordinate = eval("(" + data.d + ")");
                    $("#TextBox_LatitudeBaidu").val(shopCoordinate.latBaidu); //
                    $("#TextBox_LongitudeBaidu").val(shopCoordinate.lngBaidu);
                    getBaiduMap();
                },
                error: function (err) {
                    alert(err);
                }
            });
        }
        function getBaiduMap() {
            var map = new BMap.Map("allmap");            // 创建Map实例
            var point = new BMap.Point($("#TextBox_LongitudeBaidu").val(), $("#TextBox_LatitudeBaidu").val());
            map.centerAndZoom(point, 16);     // 初始化地图,设置中心点坐标和地图级别
            map.addControl(new BMap.NavigationControl());               // 添加平移缩放控件
            map.addControl(new BMap.ScaleControl());                    // 添加比例尺控件
            map.addControl(new BMap.OverviewMapControl());              //添加缩略地图控件
            map.enableScrollWheelZoom();                            //启用滚轮放大缩小
            map.addOverlay(new BMap.Marker(point));
            map.addEventListener("click", function (e) {
                $("#TextBox_LongitudeBaidu").val(e.point.lng);
                $("#TextBox_LatitudeBaidu").val(e.point.lat);
                var nPoint = new BMap.Point(e.point.lng, e.point.lat);
                map.clearOverlays();
                map.addOverlay(new BMap.Marker(nPoint));
            });
        }
        function keyTest() {
            var str = $("#text").val();
            if (str == "") {
                return;
            }
            $.ajax({
                type: "Post",
                url: "ShopAdd.aspx/GetDate",
                data: "{'str':'" + str + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d != "" && data.d != null) {
                        var returnJson = eval("(" + data.d + ")");
                        var dataStr = '';
                        for (var i = 0; i < returnJson.length; i++) {
                            if (i == 0) {
                                $("#init_date").html('');
                            }
                            dataStr += "<p onclick='select(this)' id='"
                                    + returnJson[i].employeeId + "' data-name='"
                                    + returnJson[i].employeeName + "'>姓名："
                                    + returnJson[i].employeeName + "，电话："
                                    + returnJson[i].employeePhone + "</p>";
                        }
                        $("#init_date").append(dataStr);
                    }
                },
                error: function (XmlHttpRequest, textStatus, errorThrown) {
                }
            });
        }
        function select(p_id) {
            $("#hidden").val(p_id.id);
            $("#text").val($("#" + p_id.id).attr("data-name"));
            $("#init_date").html(''); //清空append内容
        }
    </script>
</head>
<body onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <form id="form1" runat="server">
        <uc2:HeadControl ID="HeadControl1" runat="server" headName="门店添加" navigationImage="~/images/icon/list.gif"
            navigationText="门店列表" navigationUrl="~/ShopManage/ShopManage.aspx" />
        <div id="box" class="box">
            <div class="tagMenu">
                <ul class="menu">
                    <li>门店添加</li>
                </ul>
            </div>
            <div class="content">
                <div class="layout">
                    <table class="table" cellpadding="0" cellspacing="0">
                        <tr>
                            <th>所属公司：
                            </th>
                            <td>
                                <input id="companyText" runat="server" type="text" onkeyup="shopAddSearchCompany()" />
                                <div id="company_init_data" runat="server" style="position: absolute; clear: both; border: 1px solid #000; background-color: White">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <th>门店名：
                            </th>
                            <td>
                                <asp:TextBox ID="TextBox_shopName" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>门店电话：
                            </th>
                            <td>
                                <asp:TextBox ID="TextBox_shopTelePhone" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>联系人：
                            </th>
                            <td>
                                <asp:TextBox ID="TextBox_contactPerson" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>联系人电话：
                            </th>
                            <td>
                                <asp:TextBox ID="TextBox_contactPhone" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>客户经理：
                            </th>
                            <td>
                                <input id="text" onkeyup="keyTest()" type="text" />（搜索后点请击选择）
                            <div id="init_date">
                            </div>
                            </td>
                        </tr>
                        <tr>
                            <th>新浪微博：
                            </th>
                            <td>
                                <asp:TextBox ID="TextBox_sinaWeibo" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>腾讯微博：
                            </th>
                            <td>
                                <asp:TextBox ID="TextBox_qqWeibo" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>微信公共帐号：
                            </th>
                            <td>
                                <asp:TextBox ID="TextBox_wechatPublicName" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>营业执照：
                            </th>
                            <td>
                                <asp:TextBox ID="TextBox_shopBusinessLicense" runat="server" Width="300"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>营业时间：
                            </th>
                            <td>
                                <asp:TextBox ID="TextBox_openTime" runat="server" Width="300"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>卫生许可证：
                            </th>
                            <td>
                                <asp:TextBox ID="TextBox_shopHygieneLicense" runat="server" Width="300"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>门店地区：
                            </th>
                            <td>
                                <asp:DropDownList ID="DropDownList_provinceID" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="DropDownList_provinceID_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:DropDownList ID="DropDownList_cityID" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_cityID_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:DropDownList ID="DropDownList_countyID" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>详细地址：
                            </th>
                            <td>
                                <asp:TextBox ID="TextBox_shopAddress" runat="server" Width="300"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>商圈配置：
                            </th>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlLevel1" AutoPostBack="true" OnSelectedIndexChanged="ddlLevel1_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:DropDownList runat="server" ID="ddlLevel2">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>百度地图:
                            </th>
                            <td>
                                <div id="allmap">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <th>百度经度：
                            </th>
                            <td>
                                <asp:TextBox ID="TextBox_LongitudeBaidu" runat="server"></asp:TextBox>&nbsp;
                            <input id="Button_Longitude" type="button" value="点击获取" onclick="QueryShopCoordinate();" />
                            </td>
                        </tr>
                        <tr>
                            <th>百度纬度：
                            </th>
                            <td>
                                <asp:TextBox ID="TextBox_LatitudeBaidu" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <th>门店状态：
                            </th>
                            <td>
                                <asp:DropDownList ID="DropDownList_shopStatus" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>店铺描述：
                            </th>
                            <td>
                                <asp:TextBox ID="TextBox_shopDescription" runat="server" Width="300" TextMode="MultiLine"
                                    Height="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>店铺评分：
                            </th>
                            <td>
                                <asp:TextBox runat="server" ID="TextBox_shopRating"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>人均消费：
                            </th>
                            <td>
                                <asp:TextBox ID="TextBox_accp" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                      <%--  <tr>
                            <th>支持四舍五入：
                            </th>
                            <td>
                                <asp:RadioButton ID="rb_isSupportAccountsRound" GroupName="isSupportAccountsRound" Text="支 持"
                                    Checked="true" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:RadioButton ID="rb_notSupportAccountsRound" GroupName="isSupportAccountsRound"
                                Text="暂不支持" runat="server" />
                            </td>
                        </tr>--%>
                        <tr>
                            <th>支持红包支付：
                            </th>
                            <td>
                                <asp:RadioButton ID="rb_SupportRedEnvelopePayment" GroupName="groupRedEnvelope" Text="支 持"
                                    Checked="true" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:RadioButton ID="rb_notSupportRedEnvelopePayment" GroupName="groupRedEnvelope"
                                Text="暂不支持" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <th>支持支付：
                            </th>
                            <td>
                                <asp:RadioButton ID="RadioButton_Support" GroupName="group" Text="支 持" Checked="true"
                                    runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:RadioButton ID="RadioButton_NotSupport" GroupName="group" Text="暂不支持" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <th>不支持支付原因：
                            </th>
                            <td>
                                <asp:TextBox ID="tb_notPaymentReason" runat="server" Width="300" TextMode="MultiLine"
                                    Height="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>点菜描述：
                            </th>
                            <td>
                                <asp:TextBox ID="TextBox_orderDishDes" runat="server" Width="300" TextMode="MultiLine"
                                    Height="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                            <td>
                                <asp:Button ID="Button_ShopAdd" Text="添    加" runat="server" CssClass="button" OnClick="Button_ShopAdd_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hidden" runat="server" />
        <uc1:CheckUser ID="CheckUser1" runat="server" />
        <asp:HiddenField ID="hidden_companyId" runat="server" />
    </form>
</body>
</html>
