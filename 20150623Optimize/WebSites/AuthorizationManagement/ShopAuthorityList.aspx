<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShopAuthorityList.aspx.cs" Inherits="AuthorizationManagement_ShopAuthorityList" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>权限列表</title>
     <link href="../Css/css.css" rel="stylesheet" type="text/css" />
     <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <uc2:HeadControl ID="HeadControl1" runat="server" headName="权限列表" navigationImage="~/images/icon/list.gif"
        navigationText="权限列表" navigationUrl="" />
        <div>
            <asp:GridView ID="GridView1" runat="server" DataKeyNames="ShopAuthorityId" CssClass="gridview" AutoGenerateColumns="False"
                OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowEditing="GridView1_OnRowEditing" OnRowDeleting="GridView1_OnRowDeleting">
                <Columns>
                    <asp:BoundField DataField="AuthorityCode" HeaderText="代码" />
                    <asp:BoundField DataField="ShopAuthorityName" HeaderText="权限名" />
                    <asp:BoundField DataField="ShopAuthorityStatus" HeaderText="状态" />
                    <asp:BoundField DataField="ShopAuthoritySequence" HeaderText="排序" />
                    <asp:BoundField DataField="IsClientShow" HeaderText="是否在客户端显示" />
                    <asp:BoundField DataField="IsSYBShow" HeaderText="是否在收银宝显示" />
                    <asp:BoundField DataField="IsViewAllocWorkerEnable" HeaderText="友络员工是否自动获的权限" />
                    <asp:BoundField DataField="ShopAuthorityType" HeaderText="类型" />
                    <%--<asp:CommandField ShowEditButton="True" />--%>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete" Text="删除" OnClientClick="return confirm('确认要删除吗？');"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </div>
        <uc1:CheckUser ID="CheckUser1" runat="server" />
    </form>
</body>
</html>
