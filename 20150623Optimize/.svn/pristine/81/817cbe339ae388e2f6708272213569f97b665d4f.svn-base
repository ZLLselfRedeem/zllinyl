<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoleAdd.aspx.cs" Inherits="AuthorizationManagement_RoleAdd" %>

<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>角色添加</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        $().ready(function () {
            TabManage();
            $("#form1").validate({
                rules: {
                    TextBox_RoleName: { required: true, rangelength: [1, 12] }
                },
                messages: {
                    TextBox_RoleName: "请输入1到12位的角色名称"
                }
            });
        });
    </script>
</head>
<body scroll="no" style="overflow-y: hidden" onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <form id="form1" runat="server">
    <uc2:HeadControl ID="HeadControl1" runat="server" headName="角色添加" navigationImage="~/images/icon/list.gif"
        navigationText="角色列表" navigationUrl="~/AuthorizationManagement/RoleManage.aspx" />
    <div id="box" class="box">
        <div class="tagMenu">
            <ul class="menu">
                <li>角色添加</li>
            </ul>
        </div>
        <div class="content">
            <div class="layout">
                <table cellpadding="0" cellspacing="0" class="table">
                    <tr>
                        <th>
                            角色名：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_RoleName" onkeyup="this.value=this.value.replace(/^\s+|\s+$/g, '')"
                                runat="server" Width="230px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            角色描述：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_RoleDescription" runat="server" TextMode="MultiLine" Height="98px"
                                Width="230px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            &nbsp;
                        </th>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="添    加" OnClick="Button1_Click" CssClass="button" />
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
