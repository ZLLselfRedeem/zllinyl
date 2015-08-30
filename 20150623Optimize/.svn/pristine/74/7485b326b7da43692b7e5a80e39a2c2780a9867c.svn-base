<%@ Page Language="C#" AutoEventWireup="true" CodeFile="updateHistoryManage.aspx.cs"
    Inherits="ViewAllocWebSite_CorpManage_updateHistoryManage" %>

<%@ Register Src="~/web/CorpManage/ManageMenu.ascx" TagPrefix="Manage" TagName="ManageMenu" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>更新历史管理</title>
    <script type="text/javascript" defer="defer" src="../js/My97DatePicker/WdatePicker.js"></script>
    <link type="text/css" rel="stylesheet" href="../css/manage.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="wrap">
        <Manage:ManageMenu ID="Menu" runat="server" />
        <br />
        <br />
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tbody>
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="5" width="100%" bordercolorlight="#c0c0c0" bordercolordark="#ffffff"
                            border="1">
                            <tr>
                                <td>
                                    更新历史管理
                                    <input value="新增" onclick="return window.location.href='updateHistoryMaintain.aspx'"
                                        id="add" type="button" class="button" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView runat="server" ID="gvUpdateHistory" AutoGenerateColumns="False" CellPadding="4"
                                        Width="100%" EmptyDataText="No Data!" AllowPaging="True" PageSize="20" ForeColor="#333333">
                                        <Columns>
                                            <asp:BoundField HeaderText="序号" DataField="id" Visible="false" />
                                            <asp:BoundField HeaderText="日期" DataField="date" />
                                            <asp:BoundField HeaderText="版本" DataField="remark" />
                                            <asp:BoundField HeaderText="标题" DataField="title" />
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
