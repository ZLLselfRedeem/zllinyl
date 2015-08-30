<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AuthorityAdd.aspx.cs" Inherits="AuthorizationManagement_AuthorityAdd" %>

<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>功能模块添加</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="../Scripts/messages_cn.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        $().ready(function () {
            TabManage();
            $("#form1").validate({
                rules: {
                    TextBox_companyName: "required"
                },
                messages: {
                    TextBox_companyName: "请输入公司名称"
                }
            });
        });
    </script>
</head>
<body scroll="no" style="overflow-y: hidden" onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <form id="form1" runat="server">
    <uc2:HeadControl ID="HeadControl1" runat="server" headName="功能模块添加" navigationImage="~/images/icon/list.gif"
        navigationText="功能模块列表" navigationUrl="~/AuthorizationManagement/AuthorityManage.aspx" />
    <div id="box" class="box">
        <div class="tagMenu">
            <ul class="menu">
                <li>基本信息</li>
            </ul>
        </div>
        <div class="content">
            <div class="layout">
                <table class="table" cellpadding="0" cellspacing="0">
                    <tr>
                        <th>
                            权限名：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_AuthorityName" runat="server" Width="200"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox_AuthorityName"
                                ErrorMessage="权限名不能为空" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            权限类型：
                        </th>
                        <td>
                            <asp:DropDownList ID="DropDownList_AuthorityType" runat="server">
                                <asp:ListItem Value="page">页面</asp:ListItem>
                                <asp:ListItem Value="button">按钮</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <th>
                            权限:
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_AuthorityURL" runat="server" Width="200"></asp:TextBox>
                        </td>
                        <td>
                            页面填写页面路径，按钮填写按钮名称
                        </td>
                    </tr>
                    <tr>
                        <th>
                            选择父级：
                        </th>
                        <td>
                            <asp:DropDownList ID="DropDownList_AuthorityRank" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <th>
                            显示序号：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_AuthoritySequence" runat="server" Width="150px"></asp:TextBox>&nbsp;
                        </td>
                        <td>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="请输入数字"
                                ControlToValidate="TextBox_AuthoritySequence" ForeColor="Red" ValidationExpression="^[0-9]{1,20}$"
                                Display="Dynamic"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            权限描述：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_AuthorityDescription" runat="server" TextMode="MultiLine"
                                Height="152px" Width="400px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="添    加" OnClick="Button1_Click" CssClass="button" />
                        </td>
                        <td>
                            &nbsp;
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
