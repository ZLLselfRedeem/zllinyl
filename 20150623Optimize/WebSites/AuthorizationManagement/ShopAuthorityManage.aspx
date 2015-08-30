<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShopAuthorityManage.aspx.cs" Inherits="AuthorizationManagement_ShopAuthorityManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="GridView1" runat="server" DataKeyNames="roleId" CssClass="gridview" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="roleName" HeaderText="权限名" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="checkBoxIsHave" runat="server" Checked='<%#Bind("isHave")%>' Enabled="False" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <img src="../Images/key_edit3.gif" />
                            <asp:LinkButton ID="LinkButton1"
                                            runat="server" CausesValidation="False" CommandName="Select" Text='<%# DataBinder.Eval(Container.DataItem, "isHave").Equals(true)?"取消":"添加"%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <%--<asp:Repeater ID="repeater1" runat="server">
        <ItemTemplate>
            <asp:Label ID="Label_Name" runat="server" Text='<%#Bind("roleName")%>'></asp:Label>
            <asp:CheckBox ID="checkBoxIsHave" runat="server" Checked='<%#Bind("isHave")%>'  />
        </ItemTemplate>
    </asp:Repeater>--%>
        </div>
    </form>
</body>
</html>
