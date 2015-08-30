<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoleAuthorityManage.aspx.cs"
    Inherits="AuthorizationManagement_RoleAuthorityManage" %>

<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>角色分配权限</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("GridView_Role", "gv_OverRow");
            $("#div_content_left").css({ "height": $(window).height() - 180 });
            $("#div_content_right").css({ "height": $(window).height() - 180 });
        });       
    </script>
</head>
<body>
    <form id="form1" runat="server" onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <uc2:HeadControl ID="HeadControl1" runat="server" headName="角色分配权限" navigationImage="~/images/icon/list.gif"
        navigationText="角色分配权限" navigationUrl="" />
    <div id="box" class="box">
        <div class="tagMenu">
            <ul class="menu">
                <li>角色分配权限</li>
            </ul>
        </div>
        <div class="content">
            <div class="layout">
                <table class="order_main" cellpadding="0" cellspacing="0">
                    <tr>
                        <th style="width: 50%; text-align: left; font-size: 12px;">
                            用户角色：
                        </th>
                        <th style="width: 50%; text-align: left; font-size: 12px;">
                            权限：<input id="Button_All" type="button" value="全选" onclick="CheckAll(this.form)"
                                class="button" />&nbsp;&nbsp;&nbsp;
                            <input id="Button_NotAll" type="button" value="全不选" onclick="UnCheckAll(this.form)"
                                class="button" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="Button_Confirm" runat="server" Text="确定" OnClick="Button_Confirm_Click"
                                class="button" />
                        </th>
                    </tr>
                    <tr>
                        <th class="xian" style="width: 50%; text-align: left; height: 5px;">
                        </th>
                        <th class="xian" style="width: 50%; text-align: left; height: 5px;">
                        </th>
                    </tr>
                    <tr>
                        <td>
                            <div id="div_content_left" class="div_content">
                                <asp:GridView ID="GridView_Role" runat="server" DataKeyNames="RoleID,RoleName" AutoGenerateColumns="False"
                                    OnSelectedIndexChanged="GridView_Role_SelectedIndexChanged" SkinID="gridviewSkin">
                                    <Columns>
                                        <asp:TemplateField HeaderText="行号">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex + 1%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="RoleName" HeaderText="角色名称"></asp:BoundField>
                                        <asp:CommandField ShowSelectButton="True"></asp:CommandField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                        <td>
                            <div id="div_content_right" class="div_content">
                                <asp:TreeView ID="TreeView1" runat="server" ShowLines="True" ShowCheckBoxes="All">
                                </asp:TreeView>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <uc1:CheckUser ID="CheckUser1" runat="server" />
    </form>
</body>
</html>
