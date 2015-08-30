﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShopChannelDetail.aspx.cs" Inherits="OrderOptimization_ShopChannelDetail" %>

<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>增值页详情</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <%--<script src="../Scripts/ManageControls.js" type="text/javascript"></script>--%>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("GridView_ShopChannel", "gv_OverRow");
            $("#div_gridview").css({ "height": $(window).height() - 200 });
            $("#div_gridview").css({ "overflow": "auto" });
        });
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 60px;
        }
    </style>
    </head>
<body scroll="no" style="overflow-y: hidden" onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <form id="form1" runat="server">
        <uc2:HeadControl ID="HeadControl1" runat="server" headName="增值页面详情" />
        <div id="box" class="box">
            <div class="tagMenu">
                <ul>
                    <%--<asp:TextBox ID="TextBox_MerchantName" runat="server" Width="1500px" BorderWidth="0" ReadOnly="true" Enabled="false"></asp:TextBox>--%>
                    <li runat="server" id="TextBox_MerchantName" style="width:500px;text-align:left;">增值页面详情</li>
                </ul>
            </div>
            <div class="content">
                <div class="layout">
                    <div class="div_gridview" id="div_gridview">
                        <asp:GridView ID="GridView_ShopChannel" runat="server" DataKeyNames="shopChannelID"
                            AutoGenerateColumns="False" SkinID="gridviewSkin" OnRowCommand="GridView_ShopChannel_RowCommand" OnRowUpdating="GridView_ShopChannel_RowUpdating" Style="margin-right: 0px" OnRowDeleting="GridView_ShopChannel_RowDeleting" OnRowDataBound="GridView_ShopChannel_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="shopChannelID" HeaderText="商户增值页ID" Visible="false"/>
                                <asp:BoundField DataField="channelName" HeaderText="增值页面名称" />
                                <asp:TemplateField HeaderText="授权状态">
                                    <ItemTemplate>
                                        <%#Eval("isAuthorization").ToString().Equals("True") ? "<font color=greed>√</font>":"<font color=red>×</font>" %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="操作">
                                    <ItemTemplate>
                                        <asp:RadioButtonList ID="radListAuthorization" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1">授权&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="0">停止&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <br />
                        <br />
                        <table style="margin-right: 0px; text-align:right">
                            <tr>
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td>
                                <asp:Button ID="btnCancel" runat="server" Text="取   消" CssClass="buttonEnable" OnClick="btnCancel_Click" BorderStyle="None" Width="100px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnUpdate" runat="server" Text="保   存" CssClass="buttonEnable" OnClick="btnSave_Click" Width="100px" />
                            </td>
                        </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
           <%-- <uc1:CheckUser ID="CheckUser1" runat="server" />--%>
    </form>
</body>
</html>
