<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DropDownListCompany.ascx.cs" Inherits="WebUserControl_DropDownListCompany" %>
<asp:DropDownList ID="DropDownList_Company" runat="server" 
    style="width: 65px" 
    onselectedindexchanged="DropDownList_Company_SelectedIndexChanged" 
    AutoPostBack="True">
</asp:DropDownList>
