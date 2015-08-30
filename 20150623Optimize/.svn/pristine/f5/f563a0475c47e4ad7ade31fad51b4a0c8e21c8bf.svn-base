<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CouponDetail.aspx.cs" Inherits="Coupon_CouponDetail" %>

<!DOCTYPE html>

<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <style type="text/css">
        .auto-style1 {
            height: 28px;
        }
    </style>
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
                                <th style="width: 15%;">抵价券名称：
                                </th>
                                <td style="width: 35%;">
                                    <asp:Label ID="LabelName" runat="server" Text=""></asp:Label>
                                </td>
                                <th style="width: 15%;">状态：
                                </th>
                                <td style="width: 35%;">
                                    <asp:Label ID="LabelState" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <th>所属门店：</th>
                                <td>
                                    <asp:Label ID="LabelShopName" runat="server" Text=""></asp:Label></td>
                                <th>申请时间：</th>
                                <td>
                                    <asp:Label ID="LabelCreateTime" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <th>预计发放：</th>
                                <td>
                                    <asp:Label ID="LabelTotalCount" runat="server" Text=""></asp:Label>张</td>
                                <th>已发放：</th>
                                <td>
                                    <asp:Label ID="LabelSendCount" runat="server" Text=""></asp:Label>张
                                </td>
                            </tr>
                            <tr>
                                <th class="auto-style1">已使用：</th>
                                <td class="auto-style1">
                                    <asp:Label ID="LabelUsedCount" runat="server" Text=""></asp:Label>张</td>
                                <th class="auto-style1">尚未使用：</th>
                                <td class="auto-style1">
                                    <asp:Label ID="LabelNotUsedCount" runat="server" Text=""></asp:Label>张
                                </td>
                            </tr>
                            <tr>
                                <th>带动消费金额：</th>
                                <td>
                                    <asp:Label ID="LabelAmount" runat="server" Text="">0.00</asp:Label>元</td>
                                <th>审核人：</th>
                                <td>
                                    <asp:Label ID="LabelEmployee" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <th>数据:</th>
                                <td>
                                    <asp:Label ID="LabelCouponType" runat="server" Text=""></asp:Label>
                                </td>
                                <th>&nbsp;</th>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <th>备注：</th>
                                <td colspan="3">

                                    <asp:Label ID="LabelRemark" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>活动内容：</th>
                                <td colspan="3">

                                    <asp:Label ID="LabelDesc" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                             <tr>
                                <th>最多减：</th>
                                <td colspan="3">
                                    <asp:Label ID="LabelMaxAmount" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <!--tr>
                                <th>使用限制：</th>
                                <td colspan="3">
                                    <asp:Label ID="LabelIsGeneralHolidays" runat="server" Text=""></asp:Label>
                                </td>
                            </!--tr>
                             <tr>
                                <th>每日使用时间：</th>
                                <td colspan="3">
                                    <asp:Label ID="LabelTime" runat="server" Text=""></asp:Label>
                                </td>
                            </tr-->
                            <tr>
                                <th>审核记录：</th>
                                <td colspan="3">

                                    <asp:Label ID="LabelRecord" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center">
                                    <asp:Button ID="ButtonEdit" Visible="false" runat="server" CssClass="tabButtonBlueClick" CausesValidation="false" Text="编辑" OnClick="ButtonEdit_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="ButtonCancel" runat="server" CssClass="tabButtonBlueClick" CausesValidation="false" Text="返回" OnClientClick="window.history.back();return false;" />
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
