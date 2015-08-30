<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SystemShareCouponDetail.aspx.cs" Inherits="Coupon_SystemShareCouponDetail" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script> 
</head>
<body>
    <form id="form1" runat="server">
   <uc3:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
            navigationText="" navigationUrl="" headName="抵扣券明细" />
        <div id="box" class="box">
            <div class="content">
                <div class="layout">
                    <div class="layout">
                        <table cellspacing="0" cellpadding="0" class="table" style="width: 90%;">

                            <tr>
                                <th style="width: 15%;">名称：
                                </th>
                                <td style="width: 35%;">
                                    <asp:Label ID="LabelName" runat="server" Text=""></asp:Label>
                                </td>
                                <th style="width: 15%;">状态：
                                </th>
                                <td style="width: 35%;">
                                    <asp:Label ID="LabelState" runat="server" Text="">正常</asp:Label>
                                </td>
                            </tr>

                           

                            <tr>
                                <th>领取数：</th>
                                <td>
                                    <asp:Label ID="LabelGetCount" runat="server" Text=""></asp:Label>张</td>
                                <th>使用数：</th>
                                <td>
                                    <asp:Label ID="LabelUsedCount" runat="server" Text=""></asp:Label>张
                                </td>
                            </tr>
                            <tr>
                                <th class="auto-style1">新注册：</th>
                                <td class="auto-style1">
                                    &nbsp;</td>
                                <th class="auto-style1">带动消费金额：</th>
                                <td class="auto-style1">
                                    <asp:Label ID="LabelAmount" runat="server" Text="">0.00</asp:Label>元</td>
                            </tr>
                            <tr>
                                <th>PV：</th>
                                <td>
                                    <asp:Label ID="LabelPV" runat="server" Text="">0</asp:Label></td>
                                <th>UV：</th>
                                <td>
                                    <asp:Label ID="LabelUV" runat="server" Text="">0</asp:Label></td>
                            </tr>
                             <tr>
                                <th>所属城市：</th>
                                <td>
                                    <asp:Label ID="LabelCityName" runat="server" Text=""></asp:Label></td>
                                <th>创建时间：</th>
                                <td>
                                    <asp:Label ID="LabelCreateTime" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>创建人:</th>
                                <td>
                                    <asp:Label ID="LabelCreatedBy" runat="server" Text=""></asp:Label>
                                </td>
                                <th>&nbsp;</th>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr  style=" vertical-align:middle ">
                                <th>二维码:</th>
                                <td colspan="3" style=" vertical-align:middle ">

                                    <asp:Image ID="ImageQrCode" runat="server"  Width="120px"/>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Button ID="ButtonDownload" runat="server" CssClass="tabButtonBlueClick" CausesValidation="false" Text="下载"  OnClick="ButtonDownload_Click" />
                                </td>
                            </tr>
                             
                            <tr>
                                <th>长链接:</th>
                                <td colspan="3">

                                    <asp:HyperLink ID="HyperLinkLongLink" runat="server"></asp:HyperLink> </td>
                            </tr>
                             
                            <tr>
                                <th>短链接:</th>
                                <td colspan="3">

                                     <asp:HyperLink ID="HyperLinkShortLink" runat="server"></asp:HyperLink> </td>
                            </tr>
                             
                            <tr>
                                <th>备注：</th>
                                <td colspan="3">

                                    <asp:Label ID="LabelRemark" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                             
                            <tr>
                                <td colspan="4" style="text-align: center">
                                    &nbsp;<asp:Button ID="ButtonCancel" runat="server" CssClass="tabButtonBlueClick" CausesValidation="false" Text="返回" OnClientClick="window.history.back();return false;" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
