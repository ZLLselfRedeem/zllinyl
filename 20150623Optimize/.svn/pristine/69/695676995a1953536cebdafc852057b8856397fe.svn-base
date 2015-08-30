<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CouponCount.aspx.cs" Inherits="Coupon_CouponCount" %>

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
    </head>
<body>
    <form id="form1" runat="server">
        <uc3:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
            navigationText="" navigationUrl="" headName="订单支付统计" />
        <div id="box" class="box">
            <div class="content">
                <div class="layout">
                    <div class="layout">
                        <table cellspacing="0" cellpadding="0" class="table" style="width: 90%;">

                            <tr>
                                <th style="width: 10%;">城市：
                                </th>
                                <td style="width: 15%;">
                                     <asp:DropDownList ID="ddlCity" runat="server"></asp:DropDownList>
                                </td>
                                <th style="width: 10%;">支付时间： </th>
                                <td style="width: 35%;">
                                     <asp:TextBox ID="txtOperateBeginTime" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd 00:00:00'})"
                                        Width="150px"></asp:TextBox>&nbsp;至                                
                                    <asp:TextBox ID="txtOperateEndTime" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd 23:59:59'})"
                                        Width="150px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center">
                                    <asp:Button ID="btnExcel" Width="130px" Height="33px" runat="server" CssClass="couponButtonSubmit" CausesValidation="false" Text="导出到Excel" OnClick="btnExcel_Click" />
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
