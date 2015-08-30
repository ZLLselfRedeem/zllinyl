<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ManageMenu.ascx.cs" Inherits="ViewAllocWebSite_CorpManage_ManageMenu" %>
<asp:Menu ID="Menu1" runat="server" Orientation="Horizontal"
    StaticSubMenuIndent="10px"  DynamicHorizontalOffset="2" 
    Font-Names="Verdana" Font-Size="1em" ForeColor="#7C6F57">
    <DynamicHoverStyle BackColor="#7C6F57" ForeColor="White" />
    <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
    <DynamicMenuStyle BackColor="#F7F6F3" />
    <DynamicSelectedStyle BackColor="#5D7B9D" />
    <Items>
        <asp:MenuItem NavigateUrl="~/web/CorpManage/recentNewsTypeManage.aspx" Text="最新动态类别管理">
        </asp:MenuItem>
        <asp:MenuItem NavigateUrl="~/web/CorpManage/RecentNewsManage.aspx" Text="最新动态管理"></asp:MenuItem>
        <asp:MenuItem NavigateUrl="~/web/CorpManage/UpdateHistoryManage.aspx" Text="更新历史管理">
        </asp:MenuItem>
        <asp:MenuItem NavigateUrl="~/web/CorpManage/cooperateManage.aspx" Text="合作商户管理"></asp:MenuItem>
        <asp:MenuItem NavigateUrl="~/web/CorpManage/classicCaseManage.aspx" Text="精选案例管理"></asp:MenuItem>
        <asp:MenuItem NavigateUrl="~/web/CorpManage/adminLogin.aspx" Text="退出系统"></asp:MenuItem>
    </Items>
    <StaticHoverStyle BackColor="#7C6F57" ForeColor="White" />
    <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
    <StaticSelectedStyle BackColor="#5D7B9D" />
</asp:Menu>
