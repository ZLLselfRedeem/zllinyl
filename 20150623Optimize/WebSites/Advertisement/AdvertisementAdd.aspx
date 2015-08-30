<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdvertisementAdd.aspx.cs"
    Inherits="Advertisement_AdvertisementAdd" %>

<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>广告添加</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <style type="text/css">
        .innerTD
        {
            margin: 0 auto;
        }
        .tblBorder
        {
            border: 1px solid #c0c0c0;
        }
        .tblBorder td
        {
            border: 1px solid #c0c0c0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <uc3:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
        navigationText="返回列表" navigationUrl="~/Advertisement/AdvertisementAdd.aspx" headName="广告添加" />
    <div id="box" class="box">
        <div class="content">
            <div class="layout">
                <asp:Panel ID="Panel_NewAd" runat="server">
                    <table cellspacing="0" cellpadding="0" width="900px" class="table">
                        <tr>
                            <th style="width: 20%; text-align: center">
                                广告名称:
                            </th>
                            <td style="text-align: left; width: 95%">
                                <asp:TextBox ID="TextBox_ADName" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th style="width: 20%; text-align: center">
                                广告分类:
                            </th>
                            <td style="text-align: left; width: 95%">
                                <asp:RadioButtonList ID="rblAdClassify" runat="server" RepeatDirection="Horizontal" >
                                    <asp:ListItem Text="首页广告" Value="1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="美食广场广告" Value="2"></asp:ListItem>
                                </asp:RadioButtonList>
                               
                            </td>
                        </tr>
                        <tr>
                            <th style="width: 20%; text-align: center">
                                广告类型:
                            </th>
                            <td style="text-align: left; width: 95%;">
                                <asp:DropDownList ID="DropDownList_ADType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_ADType_SelectedIndexChanged">
                                    <asp:ListItem Value="1">门店广告</asp:ListItem>
                                    <asp:ListItem Value="3">网页广告</asp:ListItem>
                                    <asp:ListItem Value="4">红包广告</asp:ListItem>
                                    <asp:ListItem Value="5">专题广告</asp:ListItem>
                                    <asp:ListItem Value="6">套餐广告</asp:ListItem>
                                </asp:DropDownList>
                                <%--<asp:DropDownList ID="DropDownList_ADTypeFoodPlaza" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="DropDownList_ADTypeFoodPlaza_SelectedIndexChanged">
                                    <asp:ListItem Value="0">--请选择--</asp:ListItem>
                                    <asp:ListItem Value="1">门店广告</asp:ListItem>
                                    <asp:ListItem Value="3">网页广告</asp:ListItem>
                                    <asp:ListItem Value="5">专题广告</asp:ListItem>
                                </asp:DropDownList>--%>
                            </td>
                        </tr>
                        <tr runat="server" id="trComapny">
                            <th style="width: 20%; text-align: center">
                                所属公司:
                            </th>
                            <td style="text-align: left; width: 95%">
                                <asp:DropDownList ID="DropDownList_Companys" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_Conpamys_SelectedIndexChanged">
                                   
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr  runat="server" id="trStore">
                            <th style="width: 20%; text-align: center">
                                所属门店:
                            </th>
                            <td style="text-align: left; width: 95%;">
                                <asp:RadioButtonList ID="CheckBoxList_ShopId" runat="server" RepeatDirection="Horizontal"
                                    RepeatColumns="4"  >
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr   runat="server" id="trURL">
                            <th style="width: 20%; text-align: center">
                                广告地址:
                            </th>
                            <td style="text-align: left; width: 95%;">
                                <asp:TextBox ID="TextBox_url" runat="server" Width="200px"></asp:TextBox>
                                （若广告类型为网页广告，请填写合适的网页url地址，例如http://www.baidu.com）
                            </td>
                        </tr>
                        <tr   runat="server" id="trActivity">
                            <th style="width: 20%; text-align: center">
                                活动:
                            </th>
                            <td style="text-align: left; width: 95%;">
                                <asp:RadioButtonList runat="server" ID="rblActivity" RepeatColumns="4" RepeatDirection="Horizontal">
                                </asp:RadioButtonList>
                                <br />
                                （若广告类型为红包广告，请选择相应的活动）
                            </td>
                        </tr>
                        <tr>
                            <th style="width: 20%; text-align: center">
                                广告描述:
                            </th>
                            <td style="text-align: left; width: 95%;">
                                <asp:TextBox ID="TextBox_Description" runat="server" Height="80px" TextMode="MultiLine"
                                    Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table cellspacing="0" cellpadding="0" width="800px" runat="Server" id="CouponSelect"
                        style="display: none;" class="table">
                        <tr>
                            <th style="width: 160px; text-align: center">
                                优惠券:
                            </th>
                            <td style="width: 80%; text-align: left">
                                <asp:DropDownList ID="DropDownList_Coupon" runat="server" AutoPostBack="True">
                                    <asp:ListItem Value="0">--请选择--</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <table cellspacing="0" cellpadding="0" width="600px" class="table">
                        <tr>
                            <th style="text-align: center; width: 20%">
                                广告图片：
                            </th>
                            <td style="text-align: left; width: 80%" colspan="2">
                                <table width="100%" class="table">
                                    <tr>
                                        <td align="center">
                                            <asp:FileUpload ID="fileUpload" runat="server" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="Button_Upload" runat="server" CssClass="tabButtonBlueClick" OnClick="Button_Upload_Click"
                                                Text="上传" /><br />
                                            （格式png或jpg，比32:11，最小尺寸1350*495，大小不超过<asp:Label runat="server" ID="lbBannerSpace"></asp:Label>）
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 400px; text-align: center">
                                            <asp:Image ID="Big_Img" runat="server" ImageUrl="~/Images/bigimage.jpg" Width="320px"
                                                Height="110px" CssClass="innerTD" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center" colspan="2">
                            </td>
                            <td style="text-align: center">
                                <asp:Button ID="Button_Add" runat="server" Text="增加广告" class="couponButtonSubmit"
                                    OnClick="Button_Add_Click" />  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  <asp:Button ID="ButtonCancel" runat="server" Text="返回" class="couponButtonSubmit" Visible="false" OnClientClick="window.history.back();" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:HiddenField ID="HiddenField_Image" runat="server" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
