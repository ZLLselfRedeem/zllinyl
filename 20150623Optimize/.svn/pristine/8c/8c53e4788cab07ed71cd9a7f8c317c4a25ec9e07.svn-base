<%@ Page Language="C#" AutoEventWireup="true" CodeFile="recentNewsTypeManage.aspx.cs"
    Inherits="ViewAllocWebSite_CorpManage_recentNewsTypeManage" %>

<%@ Register Src="~/web/CorpManage/ManageMenu.ascx" TagPrefix="Manage" TagName="ManageMenu" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>最新动态类别管理</title>
    <script type="text/javascript" language="javascript">

        function Cancel() {
            this.document.getElementById("txtType").value = "";
            this.document.getElementById("ddlOrder").value = "";
        }

    </script>
    <link type="text/css" rel="stylesheet" href="../css/manage.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="wrap">
        <Manage:managemenu id="Menu" runat="server" />
        <br />
        <br />
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tbody>
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="5" width="100%" bordercolorlight="#c0c0c0" bordercolordark="#ffffff"
                            border="1">
                            <tr>
                                <td colspan="2">
                                    最新动态类别管理
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    类别名称
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtType"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvType" ControlToValidate="txtType"
                                        Text="必填" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    类别顺序
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlOrder" Height="22px" Width="154px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvOrder" ControlToValidate="ddlOrder"
                                        Text="必填" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Button runat="server" ID="btnSave" Text="保存" OnClick="btnSave_Click" CssClass="button" />
                                    <input id="btnCancleType" type="button" value="清除" causesvalidation="false" onclick="return Cancel();"
                                        class="button" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:GridView runat="server" ID="gvType" AutoGenerateColumns="False" CellPadding="4"
                                        Width="100%" EmptyDataText="No Data!" AllowPaging="True" PageSize="20" ForeColor="#333333">
                                        <Columns>
                                            <asp:BoundField DataField="id" HeaderText="序号" Visible="false" />
                                            <asp:BoundField DataField="title" HeaderText="类别名称" />
                                            <asp:BoundField DataField="sequence" HeaderText="类别顺序(升序)" />
                                            <asp:TemplateField HeaderText="操作">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkbtnModify" CommandName="modify" OnCommand="lnkbtnEdit_OnCommand"
                                                        CausesValidation="false" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id") %>'>编辑</asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="lnkbtnDel" CommandName="del" OnCommand="lnkbtnEdit_OnCommand"
                                                        CausesValidation="false" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id") %>'
                                                        OnClientClick="return window.confirm('您確定刪除？')">删除</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#999999" />
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <FooterStyle BackColor="#E6E6E6" Font-Bold="True" ForeColor="Black" />
                                        <HeaderStyle BackColor="#E6E6E6" Font-Bold="True" ForeColor="Black" />
                                        <PagerStyle BackColor="#E6E6E6" HorizontalAlign="Right" />
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
