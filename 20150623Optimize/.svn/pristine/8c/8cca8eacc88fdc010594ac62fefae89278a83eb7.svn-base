<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShopAuthorityAdd.aspx.cs" Inherits="AuthorizationManagement_ShopAuthorityAdd" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>权限添加</title>
     <link href="../Css/css.css" rel="stylesheet" type="text/css" />
     <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
</head>
<body>

    <form id="form1" runat="server">
        <uc2:HeadControl ID="HeadControl1" runat="server" headName="权限添加" navigationImage="~/images/icon/list.gif"
        navigationText="权限添加" navigationUrl="" />
        <div>
            <table width="95%">
                <tr>
                    <td width="30%" align="right" >权限名称:</td>
                    <td><asp:TextBox runat="server" ID="textBoxshopAuthorityName" Width="40%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td  align="right" >权限描述</td>
                    <td><asp:TextBox runat="server" ID="textBoxshopAuthorityDescription" TextMode="MultiLine" Width="70%" Rows="5"></asp:TextBox></td>
                </tr>
                <tr>
                    <td  align="right" >类型:</td>
                    <td><asp:DropDownList runat="server" ID="dropDownListCode"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td></td>
                    <td><asp:Button ID="button1" runat="server" OnClick="button1_OnClick" Text="添加" /></td>
                </tr>
            </table>
        </div>
        <uc1:CheckUser ID="CheckUser1" runat="server" />
    </form>
</body>
</html>
