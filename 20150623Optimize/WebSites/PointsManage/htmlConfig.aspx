<%@ Page Language="C#" AutoEventWireup="true" CodeFile="htmlConfig.aspx.cs" Inherits="PointsManage_htmlConfig" %>

<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HTML页面</title>
    <script type="text/javascript" language="javascript" src="../web/CorpManage/ckeditor/ckeditor.js"></script>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="content">
        <div class="layout">
            <table class="table" cellpadding="0" width="80%" cellspacing="0">
                <tr>
                    <td>
                        城市选择&nbsp;&nbsp;
                        <asp:DropDownList runat="server" ID="ddlCity" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged"
                            AutoPostBack="true" Height="20px" Width="80px">
                            <asp:ListItem Text="全国" Selected="True" Value="0"></asp:ListItem>
                            <asp:ListItem Text="杭州" Value="87"></asp:ListItem>
                            <asp:ListItem Text="上海" Value="9"></asp:ListItem>
                            <asp:ListItem Text="北京" Value="1"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td height="300px">
                        <CKEditor:CKEditorControl ID="CKEditor1" runat="server" Height="300px"></CKEditor:CKEditorControl>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="button" />&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnDelete" runat="server" Text="删除" OnClick="btnDelete_Click" OnClientClick="return confirm('确认删除？')"
                            CssClass="button" />&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnClear" runat="server" Text="清空" OnClick="btnClear_Click" CssClass="button" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
     <uc1:CheckUser ID="CheckUser1" runat="server" />
    </form>
</body>
</html>
