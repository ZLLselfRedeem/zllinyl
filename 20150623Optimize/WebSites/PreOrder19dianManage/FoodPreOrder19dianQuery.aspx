<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FoodPreOrder19dianQuery.aspx.cs" Inherits="PreOrder19dianManage_FoodPreOrder19dianQuery" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <title>红包财务查询</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <uc2:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
                navigationText="" navigationUrl="" headName="财务粮票统计" />
            <div class="content" id="divList" runat="server">
                <div class="layout">
                    <table class="table" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <th style="width: 12%">查询周期  
                            </th>
                            <td colspan="7">
                                <asp:TextBox ID="txbBeginTime" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd 00:00:00'})"
                                    Width="150px"></asp:TextBox>&nbsp; ~&nbsp;
                                <asp:TextBox ID="txbEndTime" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd 23:59:59'})"
                                    Width="150px"></asp:TextBox> （备注：查询周期请控制在一个月内）
                            </td>
                        </tr>
                        <tr>
                            <th>
                                城市
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlCity" runat="server"></asp:DropDownList>
                            </td>
                            <td colspan="4">
                                <asp:Button runat="server" ID="btnQuery" Text="查询" CssClass="button" OnClick="btnQuery_Click" />
                            </td>
                        </tr>

                        <tr>
                            <th>粮票支付金额  
                            </th>
                            <td colspan="7">
                                <asp:Label runat="server" ID="lbAmount" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th>所有粮票退款金额  
                            </th>
                            <td colspan="7">
                                <asp:Label runat="server" ID="lbRefundAmount" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
