<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DishManageControl.ascx.cs"
    Inherits="WebUserControl_DishManageControl" %>
<%--上面的导航--%>
<div class="DishManageNavigation">
    当前美食：<asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
    <asp:LinkButton ID="LinkButton_base" runat="server" OnClick="LinkButton_base_Click">基本信息</asp:LinkButton>
    &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="LinkButton_video" runat="server" OnClick="LinkButton_video_Click">视频图片</asp:LinkButton>
    &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="LinkButton_price" runat="server" OnClick="LinkButton_price_Click">规格价格</asp:LinkButton>
    &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="LinkButton_back" runat="server"  
        CssClass="DishInfoButtonOff" onclick="LinkButton_back_Click">返回列表</asp:LinkButton>
</div>
<%--上面的导航--%>