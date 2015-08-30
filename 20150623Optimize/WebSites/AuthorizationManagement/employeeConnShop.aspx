﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="employeeConnShop.aspx.cs"
    Inherits="AuthorizationManagement_employeeConnShop" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>用户门店权限分配</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("GridView_Employee", "gv_OverRow");
            var div_Height = $("#QueryTerms").height();
            $("#div_content_left").css({ "height": $(window).height() - 180 - div_Height });
            $("#div_content_right").css({ "height": $(window).height() - 180 - div_Height });
        });       
    </script>
</head>
<body>
    <form id="form1" runat="server" onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <uc2:HeadControl ID="HeadControl1" runat="server" headName="用户店铺管理" navigationImage="~/images/icon/list.gif"
        navigationText="用户店铺管理" navigationUrl="" />
    <div id="box" class="box">
        <div class="tagMenu">
            <ul class="menu">
                <li>用户店铺管理</li>
            </ul>
        </div>
        <div class="content">
            <div class="layout">
                <table class="order_main" cellpadding="0" cellspacing="0">
                    <tr>
                        <th class="xian" style="width: 50%; text-align: left; height: 5px;">
                            <div class="QueryTerms">
                                <table>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            姓名或用户名：
                                            <asp:TextBox ID="TextBox_Name" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp;<asp:Button ID="Button_QueryEmplooer" runat="server" CssClass="button" Text="查 询"
                                                OnClick="Button_QueryEmplooer_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </th>
                    </tr>
                    <tr>
                        <td>
                            <div id="div_content_left" class="div_content">
                                <asp:GridView ID="GridView_Employee" runat="server" DataKeyNames="employeeID,employeeFirstName"
                                    AutoGenerateColumns="False" OnSelectedIndexChanged="GridView_Employee_SelectedIndexChanged"
                                    SkinID="gridviewSkin">
                                    <Columns>
                                        <asp:TemplateField HeaderText="行号">
                                            <ItemTemplate>
                                                <%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="UserName" HeaderText="用户名" />
                                        <asp:BoundField DataField="employeeFirstName" HeaderText="姓名" />
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <img src="../Images/key_edit3.gif" />&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1"
                                                    runat="server" CausesValidation="False" CommandName="Select" Text="选择"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <div class="asp_page">
                                    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
                                        NextPageText="下一页" PageSize="10" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                                        TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                                        NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" PageIndexBoxClass="listPageText"
                                        CurrentPageButtonClass="currentButton" ShowPageIndexBox="Always" currentpagebuttonposition="Center"
                                        OnPageChanged="AspNetPager1_PageChanged">
                                    </webdiyer:AspNetPager>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
