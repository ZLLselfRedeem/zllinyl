<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HomeAdvertDetail.aspx.cs" Inherits="HomeNew_HomeAdvertDetail" %>

<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>商户广告详情</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="../Scripts/messages_cn.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            document.getElementById('text').addEventListener("input", ShopSearch, true);
            $("#ddlSubTitle").change(function () {
                var titleIDS = $(this).val();
                if(titleIDS.indexOf(',1')!=-1)
                {
                    $("#txtIndex").val("");
                    $("#trIndex").show();
                }
                else
                {
                    $("#trIndex").hide();
                }
            });
            $("#ddlTitle").change(function () {
                var titleIDS = $(this).text();
                if (titleIDS.indexOf('全部') != -1) {
                    $("#trImageUpload").hide();
                }
                else {
                    $("#trImageUpload").show();
                }
            });
            
            ShowRow();
        });
        function ShowRow()
        {
            var titleIDS = $("#ddlSubTitle").val();
            if (titleIDS.indexOf(',1') != -1) {
                $("#trIndex").show();
            }
            else {
                $("#trIndex").hide();
            }
        }

        function ShopSearch() {
            var str = $("#text").val();
            var cityID = $("#ddlCity").val();
            if (str == "") {
                return;
            }
            $.ajax({
                type: "Post",
                url: "../Handlers/commonAjaxPage.aspx/GetAdvertShopData",
                data: "{'str':'" + str + "','cityID':'"+cityID+"'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data != null) {
                        if (data.d != "" && data.d != null) {
                            var returnJson = eval("(" + data.d + ")");
                            var strHtml = "<ul>";
                            for (var i = 0; i < returnJson.length; i++) {
                                if (i == 0) {
                                    $("#init_date").html('');
                                }
                                strHtml += "<li onclick='selectShop(this)' id='"
                                    + returnJson[i].shopId + "' data-name='"
                                    + returnJson[i].shopName + "'>&nbsp;&nbsp;"
                                    + returnJson[i].shopName + "&nbsp;&nbsp;</li>"
                            }
                            strHtml += "</ul>";
                            $("#init_date").append(strHtml);
                        }
                    }
                }
            });
        }
        function selectShop(shop) {
            $("#hidShopID").val(shop.id);
            var name = $("#" + shop.id).attr('data-name');
            $("#text").val(name);
            $("#init_date").html('');
        }
    </script>
</head>
<body scroll="no" style="overflow-y: hidden" onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <form id="form1" runat="server">
        <uc2:HeadControl ID="HeadControl1" runat="server" headName="商户广告详情" navigationImage="~/images/icon/list.gif" navigationText="商户广告列表管理" navigationUrl="~/HomeNew/HomeAdvert.aspx" />
        <div id="box" class="box">
            <div class="tagMenu">
                <ul class="menu">
                    <li>基本信息</li>
                </ul>
            </div>
            <div class="content">
                <div class="layout">
                    <table class="table" id="tableAdvert" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style=" text-align:right; background-color: #C9E7FF;">城市
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCity" runat="server" Height="40px" Width="100px" AutoPostBack="true" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td style=" text-align:right; background-color: #C9E7FF;">一级栏目
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlTitle" runat="server" Height="40px" Width="100px" OnSelectedIndexChanged="ddlTitle_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td style=" text-align:right; background-color: #C9E7FF;">二级栏目
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSubTitle" runat="server" Height="40px" Width="100px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trIndex">
                            <td style=" text-align:right; background-color: #C9E7FF;">顺序
                                </td>
                            <td>
                                <asp:TextBox ID="txtIndex" runat="server" onkeyup="if(this.value.length==1){this.value=this.value.replace(/[^1-9]/g,'')}else{this.value=this.value.replace(/\D/g,'')}" onafterpaste="if(this.value.length==1){this.value=this.value.replace(/[^1-9]/g,'')}else{this.value=this.value.replace(/\D/g,'')}"></asp:TextBox>
                                <asp:HiddenField ID="hidIndex" runat="server" />
                                <asp:HiddenField ID="hidRadType" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style=" text-align:right; background-color: #C9E7FF;">商户
                                </td>
                            <td>
                               <%-- <asp:TextBox ID="txtShop" runat="server"></asp:TextBox>--%>
                                <input id="text" runat="server" type="text" onkeyup="ShopSearch()" />
                                <div id="init_date" runat="server" style="position: absolute; clear: both; border: 1px solid #000; background-color: White">
                                </div>
                                <asp:HiddenField ID="hidShopID" runat="server" />
                                <font color="red">（建议10个以下中文字符）</font>
                            </td>
                        </tr>
                        <tr>
                            <td style=" text-align:right; background-color: #C9E7FF;">副标题
                            </td>
                            <td>
                                <asp:TextBox ID="txtSubTitle" TextMode="MultiLine" Height="50" Width="320px" runat="server"></asp:TextBox>
                                <font color="red">（建议20个以下中文字符）</font>
                            </td>
                        </tr>
                        <tr>
                            <td style=" text-align:right; background-color: #C9E7FF;">显示状态
                            </td>
                            <td>
                                <asp:RadioButtonList ID="radListStatus" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">是</asp:ListItem>
                                    <asp:ListItem Value="0" Selected="True">否</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr id="trImageUpload" visible="false">
                            <td style=" text-align:right; background-color: #C9E7FF;">广告图片</td>
                            <td>
                                <asp:FileUpload ID="fileUpload" runat="server" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="Button_Upload" runat="server" CssClass="tabButtonBlueClick" OnClick="Button_Upload_Click"
                                                Text="上传" />&nbsp;
                                                <asp:Button ID="btnShowAdvertPic" runat="server" CssClass="tabButtonBlueClick" Text="预览" OnClick="btnShowAdvertPic_Click" />
                                           <br /><font color="red">（请上传小于1024KB的PNG,JPG,GIF格式图片）</font><br />
                                <asp:HiddenField ID="hidImageUrl" runat="server" />
                                <asp:Image ID="Big_Img" runat="server" style="max-width:400px;max-height:150px;" ImageUrl="~/Images/bigimage.jpg" CssClass="innerTD" Height="150px" Width="400px" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                            <td>
                                <asp:Button ID="btnCancel" runat="server" Text="取   消"   CssClass="button" OnClick="btnCancel_Click"/>
                                <asp:Button ID="btnUpdate" runat="server" Text="保   存"   CssClass="button" OnClick="btnUpdate_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <%--<uc1:CheckUser ID="CheckUser1" runat="server" />--%>
    </form>
</body>
</html>
