<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmployeeUpdate.aspx.cs" Inherits="AuthorizationManagement_EmployeeUpdate" %>

<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2" runat="server">
    <title>员工添加</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="../Scripts/messages_cn.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        $().ready(function () {
            TabManage();
            $("#form1").validate({
                rules: {
                    TextBox_UserName: "required"
                },
                messages: {
                    TextBox_UserName: "请输入员工名"
                }
            });
            var count = $("#HiddenField_HistoryCount").val();
            var strUrl = "javascript:history.go(-" + count + ")";
            $("#HeadControl1_HyperLink_NavigationUrl").attr("href", strUrl);
        });
    </script>
</head>
<body>
    <form id="form1" runat="server" onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <uc2:HeadControl ID="HeadControl1" runat="server" headName="员工修改" navigationImage="~/images/icon/list.gif"
        navigationText="员工列表" navigationUrl="" />
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
                            <asp:TextBox ID="TextBox_UserName" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox_UserName"
                                ErrorMessage="用户名不能为空" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            密码:
                        </th>
                        <td>
                            <asp:Button ID="Button_Password" runat="server" Text="重置密码" OnClick="Button_Password_Click" />
                        </td>
                        <td>
                            <asp:Label ID="Label_Password" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            姓名：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_EmployeeFirstName" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <th>
                            注册时间：
                        </th>
                        <td>
                            <asp:Label ID="Label_RegisterDate" runat="server" Text="Label"></asp:Label>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <th>
                            性别：
                        </th>
                        <td>
                            <asp:RadioButtonList ID="RadioButtonList_EmployeeSex" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1" Selected="True">男</asp:ListItem>
                                <asp:ListItem Value="0">女</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <th>
                            年龄：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_EmployeeAge" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <th>
                            生日：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_Birthday" runat="server" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"
                                CssClass="Wdate"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            电话：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_EmployeePhone" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <th>
                            职位：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_position" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <th>
                            初始化页面：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_DefaultPage" runat="server"></asp:TextBox>
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
                            <asp:TextBox ID="TextBox_EmployeeSequence" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBox_EmployeeSequence"
                                ErrorMessage="请输入数字" ForeColor="Red" ValidationExpression="^[0-9]{1,20}$" Display="Dynamic"
                                ValidationGroup="ValidationGroup1"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox_EmployeeSequence"
                                ErrorMessage="排序不能为空" ForeColor="Red" ValidationGroup="ValidationGroup1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            友络人员：
                        </th>
                        <td>
                            <asp:RadioButtonList ID="RadioButtonList_isViewAllocWorker" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="true">是</asp:ListItem>
                                <asp:ListItem Value="false">否</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <th>
                            登录后台：
                        </th>
                        <td>
                            <asp:RadioButtonList ID="rbl_isSupportLoginBgSYS" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="true">是</asp:ListItem>
                                <asp:ListItem Value="false">否</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="style1">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <th>
                            备注：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_Remark" runat="server" Height="80px" TextMode="MultiLine"
                                Width="180px"></asp:TextBox>
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
                            <asp:Button ID="Button_Update" runat="server" Text="修    改" OnClick="Button_Update_Click"
                                CssClass="button" ValidationGroup="ValidationGroup1" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="Button_Back" runat="server" Text="返    回" CssClass="button" OnClick="Button_Back_Click" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="HiddenField_HistoryCount" runat="server" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
