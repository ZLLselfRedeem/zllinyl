<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ModifyPassword.aspx.cs" Inherits="AuthorizationManagement_ModifyPassword" %>

<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>修改密码</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            var validator = $("#form1").validate({
                rules: {
                    TextBox_AuthorityPasswordNew: {
                        required: true
                    },
                    TextBox_AuthorityPasswordConfirm: {
                        required: true,
                        equalTo: "#TextBox_AuthorityPasswordNew"
                    }
                },
                errorPlacement: function (error, element) {
                    if (element.is(":radio"))
                        error.appendTo(element.parent().next().next());
                    else if (element.is(":checkbox"))
                        error.appendTo(element.next());
                    else
                        error.appendTo(element.parent().next());
                }
            })
        })
    </script>
</head>
<body scroll="no" style="overflow-y: hidden" onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <form id="form1" runat="server">
    <uc2:HeadControl ID="HeadControl1" runat="server" headName="修改密码" navigationImage="~/images/icon/list.gif"
        navigationText="修改密码" navigationUrl="" />
    <div id="box" class="box">
        <div class="tagMenu">
            <ul class="menu">
                <li>修改密码</li>
            </ul>
        </div>
        <div class="content">
            <div class="layout">
                <table class="table" cellpadding="0" cellspacing="0">
                    <tr>
                        <th>
                            原密码：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_AuthorityPassword" runat="server" Width="200"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            新密码:
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_AuthorityPasswordNew" runat="server" Width="200"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            确认新密码：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_AuthorityPasswordConfirm" runat="server" Width="200"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="修    改" OnClick="Button1_Click" CssClass="button" />
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
