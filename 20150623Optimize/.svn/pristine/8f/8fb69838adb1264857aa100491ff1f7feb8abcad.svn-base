<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmployeeAdd.aspx.cs" Inherits="AuthorizationManagement_EmployeeAdd" %>

<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2" runat="server">
    <title>员工添加</title>
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
                    TextBox_UserName: { required: true, rangelength: [3, 15] },
                    TextBox_EmployeeFirstName: { maxlength: 10 },
                    TextBox_EmployeeSequence: { required: true, "number": true, "min": 1 },
                    TextBox_EmployeeAge: { digits: true, range: [0, 150] },
                    TextBox_EmployeePhone: { digits: true, rangelength: [6, 12] },
                    TextBox_position: { rangelength: [1, 8] }
                },
                messages: {
                    TextBox_UserName: "请输入3到15位用户名",
                    TextBox_EmployeeFirstName: "姓名长度不超过10位",
                    TextBox_EmployeeAge: "年龄必须是0到150之间",
                    TextBox_EmployeePhone: "电话必须是6到12位之间的数字",
                    TextBox_EmployeeSequence: "请输入大于1的整数",
                    TextBox_position: "职位名称不能超过8位"
                }
            });
        });

    </script>
</head>
<body scroll="no" style="overflow-y: hidden" onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <form id="form1" runat="server">
    <uc2:HeadControl ID="HeadControl1" runat="server" headName="员工添加" navigationImage="~/images/icon/list.gif"
        navigationText="员工列表" navigationUrl="~/AuthorizationManagement/EmployeeManage.aspx" />
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
                            用户名：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_UserName" runat="server" onkeyup="this.value=this.value.replace(/^\s+|\s+$/g, '')"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            姓名：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_EmployeeFirstName" onkeyup="this.value=this.value.replace(/^\s+|\s+$/g, '')"
                                runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            性别：
                        </th>
                        <td>
                            <asp:RadioButtonList ID="RadioButtonList_EmployeeSex" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1">男</asp:ListItem>
                                <asp:ListItem Value="0" Selected="True">女</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            年龄：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_EmployeeAge" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            电话：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_EmployeePhone" onkeyup="this.value=this.value.replace(/^\s+|\s+$/g, '')"
                                runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            职位：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_position" onkeyup="this.value=this.value.replace(/^\s+|\s+$/g, '')"
                                runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            初始化页面：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_DefaultPage" onkeyup="this.value=this.value.replace(/^\s+|\s+$/g, '')"
                                runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            显示序号：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_EmployeeSequence" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            友络人员：
                        </th>
                        <td>
                            <asp:RadioButtonList ID="RadioButtonList_isViewAllocWorker" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="true">是</asp:ListItem>
                                <asp:ListItem Value="false" Selected="True">否</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            登录后台：
                        </th>
                        <td>
                            <asp:RadioButtonList ID="rbl_isSupportLoginBgSYS" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="true">是</asp:ListItem>
                                <asp:ListItem Value="false" Selected="True">否</asp:ListItem>
                            </asp:RadioButtonList>
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
