<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShopUpdate.aspx.cs" Inherits="ShopManage_ShopUpdate" %>

<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>门店修改</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="../Scripts/messages_cn.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=xIaOmBpthTUf8zF1WurZyBkU"></script>
    <style type="text/css">
        #allmap, #Gmap, #GDmap {
            width: 300px;
            height: 280px;
            overflow: hidden;
            margin: 0;
        }

        p:hover {
            text-decoration: underline;
            color: #39c;
            cursor: pointer;
        }
    </style>
    <script type="text/javascript">
        $().ready(function () {
            $("#text").val($("#hidden_init").val()); //初始化客户经理数据
            TabManage();
            $("#form1").validate({
                rules: {
                    TextBox_shopName: "required",
                    TextBox_LongitudeBaidu: "number",
                    TextBox_LatitudeBaidu: "number",
                    TextBox_shopDescription: { rangelength: [0, 75] },
                    TextBox_shopRating: { number: true, range: [0, 5], rangelength: [1, 3] },
                    TextBox_accp: { number: true },
                    tb_notPaymentReason: { rangelength: [0, 100] }
                },
                messages: {
                    TextBox_shopName: "请输入门店名",
                    TextBox_LongitudeBaidu: "请输入正确的经度",
                    TextBox_LatitudeBaidu: "请输入正确的纬度",
                    TextBox_shopDescription: "最多输入75个字符",
                    TextBox_shopRating: "请输入一个介于0~5之间的值，最多保留一位小数",
                    TextBox_accp: "请输入正确的金额",
                    tb_notPaymentReason: "最多输入100个字符"
                }
            });
            getBaiduMap();
            ckbCheckAll();

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
        function PartEmployee() {
            var employeeId;
            var employeeName;
            var employeePhone;
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
                        var returnJson = eval("(" + data.d + ")"); //获取后台查询返回的json，反序列化
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
                    alert("获取数据失败");
                }
            });
        }
        function select(p_id) {
            $("#hidden").val(p_id.id);
            $("#text").val($("#" + p_id.id).attr("data-name"));
            $("#init_date").html(''); //清空append内容
        }
        function ckbCheckAll() {
            if (document.getElementById("ckbExistShopTag").childNodes.length > 0) {
                for (var i = 0; i < document.getElementById("ckbExistShopTag").getElementsByTagName("input").length; i++) {
                    document.getElementById("ckbExistShopTag_" + i).checked = true;
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" onkeypress="if(event.keyCode==13||event.which==13){return false;}">
        <uc2:HeadControl ID="HeadControl" runat="server" headName="门店修改" navigationImage="~/images/icon/list.gif"
            navigationText="门店列表" navigationUrl="javascript:history.go(-1)" />
        <div id="box" class="box">
            <div class="tagMenu">
                <ul class="menu">
                    <li>基本信息</li>
                    <li>门店LOGO</li>
                    <li>门店形象宣传照</li>
                    <li>二维码</li>
                </ul>
            </div>
            <div class="content">
                <div class="layout">
                    <table class="table" cellpadding="0" cellspacing="0">
                        <tr>
                            <th>所属公司：
                            </th>
                            <td>
                                <asp:DropDownList ID="DropDownList_Company" runat="server" Enabled="False">
                                </asp:DropDownList>
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
                            <th>门店地址：
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
                                <asp:Button runat="server" ID="txbAddShopTag" Text="添加" CssClass="button" OnClick="txbAddShopTag_Click" />
                                <%--<input id="btnAddShopTag" type="button" value="继续添加" cssclass="button" />--%>
                            </td>
                        </tr>
                        <tr>
                            <th>已有商圈
                            </th>
                            <td>
                                <asp:CheckBoxList ID="ckbExistShopTag" runat="server" RepeatColumns="4" RepeatDirection="Horizontal">
                                </asp:CheckBoxList>
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
                        <tr>
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
                       <%-- <tr>
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
                                <asp:RadioButton ID="RadioButton_Support" GroupName="group" Text="支 持" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;
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
                            <th>银行账户：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddl_account" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>审核情况：
                            </th>
                            <td>
                                <asp:DropDownList ID="DropDownList_IsHandle" runat="server" Enabled="false">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                            <td>
                                <asp:Button ID="Button_ShopUpdate" runat="server" Text="修    改" OnClick="Button_ShopUpdate_Click"
                                    CssClass="button" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="layout" style="display: none">
                    <table class="table" cellpadding="0" cellspacing="0">
                        <tr>
                            <th>门店logo：
                            </th>
                            <td>
                                <table style="border-style: none">
                                    <tr>
                                        <td>（格式png或jpg，比例1:1，最小尺寸300*300，大小不超过<asp:Label runat="server" ID="lbShopLogoSpace"></asp:Label>）
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Image ID="Big_Img" runat="server" ImageUrl="~/Images/bigimage.jpg" Width="136px"
                                                Height="136px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <input id="Text_SelectFile" type="text" readonly="readonly" />
                                                    </td>
                                                    <td>
                                                        <div style="position: relative;">
                                                            <input id="Button_SelectFile" type="button" value="选择文件" class="button" />
                                                            <asp:FileUpload ID="Big_File" runat="server" onchange="Text_SelectFile.value=this.value"
                                                                CssClass="fileUpload" />
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="ButtonBig" runat="server" Text="上传" OnClick="ButtonBig_Click" CssClass="button" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:Label ID="Label_imageName" runat="server" Text="Label" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="layout" style="display: none">
                    <table class="table" cellpadding="0" cellspacing="0">
                        <tr>
                            <th>店铺形象宣传照：
                            </th>
                            <td>
                                <table style="border-style: none">
                                    <tr>
                                        <td>（格式png或jpg，比例160:53，最小尺寸1440x477，不超过<asp:Label runat="server" ID="lbShopFaceSpace"></asp:Label>）
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Image ID="publicityPhoto" runat="server" Width="320px" Height="106px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <input id="Text_PublicityPhoto" type="text" readonly="readonly" />
                                                    </td>
                                                    <td>
                                                        <div style="position: relative;">
                                                            <input id="Button_PublicityPhoto" type="button" value="选择文件" class="button" />
                                                            <asp:FileUpload ID="FileUpload_PublicityPhoto" runat="server" onchange="Text_PublicityPhoto.value=this.value"
                                                                CssClass="fileUpload" />
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="ButtonPublicityPhoto" runat="server" Text="上传" CssClass="button"
                                                            OnClick="ButtonPublicityPhoto_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:Label ID="Label_PublicityPhotoName" runat="server" Text="" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="layout" style="display: none">
                    <table class="table" cellpadding="0" cellspacing="0">
                        <tr>
                            <th>二维码：
                            </th>
                            <td>
                                <table style="border-style: none">
                                    <tr>
                                        <td style="width: 20%">类型
                                        </td>
                                        <td style="width: 40%">
                                            <asp:DropDownList runat="server" ID="DropDownList_QRCodeType">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 40%">
                                            <asp:Button runat="server" ID="btnCreate" Text="生成二维码" OnClick="btnCreate_Click"
                                                Width="110px" />
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td colspan="3">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Image runat="server" ID="imgYLB" Width="150" Height="150" AlternateText="易拉宝二维码" />
                                                    </td>
                                                    <td>
                                                        <asp:Image runat="server" ID="ImgKT" Width="150" Height="150" AlternateText="卡台二维码" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Button runat="server" ID="btnDownloadYLB" Text="下载易拉宝二维码" Width="112px" OnClick="btnDownloadYLB_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:Button runat="server" ID="btnDownloadKT" Text="下载卡台二维码" Width="112px" OnClick="btnDownloadKT_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                            <asp:Label ID="msg" runat="server" ForeColor="Maroon"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <uc1:CheckUser ID="CheckUser1" runat="server" />
        <asp:HiddenField ID="hidden" runat="server" />
        <asp:HiddenField ID="hidden_init" runat="server" />
    </form>
</body>
</html>
