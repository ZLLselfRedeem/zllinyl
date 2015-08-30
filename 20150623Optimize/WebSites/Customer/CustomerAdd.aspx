<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerAdd.aspx.cs" Inherits="Customer_CustomerAdd" %>

<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>客户添加</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="../Scripts/messages_cn.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        $().ready(function () {
            TabManage();
            $("#form1").validate({
                rules: {
                    TextBox_UserName: "required",
                    TextBox_Password: { required: true },
                    TextBox_ConfirmPassword: { required: true, equalTo: "#TextBox_Password" },
                    TextBox_CustomerFirstName: "required",
                    TextBox_CustomerLastName: "required"
                },
                messages: {
                    TextBox_UserName: "请输入用户名",
                    TextBox_Password: "请输入密码",
                    TextBox_ConfirmPassword: { required: "请输入确认密码", equalTo: "两次输入密码不一致不一致" },
                    TextBox_CustomerFirstName: "客户姓氏不能为空",
                    TextBox_CustomerLastName: "客户名字不能为空"
                }
            });
        });

    </script>
</head>
<body>
    <form id="form1" runat="server" onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <!-- 头部菜单 Start -->
    <uc2:HeadControl ID="HeadControl1" runat="server" headName="客户添加" navigationImage="~/images/icon/list.gif"
        navigationText="客户列表" navigationUrl="~/CompanyManage/CompanyManage.aspx" />
    <!-- 头部菜单 end -->
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
                            <asp:TextBox ID="TextBox_UserName"  onkeyup="this.value=this.value.replace(/^\s+|\s+$/g, '')" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            密码：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_Password" runat="server"  onkeyup="this.value=this.value.replace(/^\s+|\s+$/g, '')" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            确认密码：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_ConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            姓氏：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_CustomerFirstName"  onkeyup="this.value=this.value.replace(/^\s+|\s+$/g, '')" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            名字：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_CustomerLastName"  onkeyup="this.value=this.value.replace(/^\s+|\s+$/g, '')" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            性别：
                        </th>
                        <td>
                            <asp:RadioButtonList ID="RadioButtonList_CustomerSex" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1" Selected="True">男</asp:ListItem>
                                <asp:ListItem Value="0">女</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            电话：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_CustomerPhone" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            生日：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_CustomerBirthday" runat="server" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"
                                CssClass="Wdate"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            注册时间：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_RegisterDate" runat="server" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"
                                CssClass="Wdate"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            用户等级：
                        </th>
                        <td>
                            <asp:DropDownList ID="DropDownList_CustomerRankID" runat="server" Width="125">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            住址：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_CustomerAddress" runat="server" Width="300"></asp:TextBox>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <th>
                            备注：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_CustomerNote" runat="server" TextMode="MultiLine" Width="300"
                                Height="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
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
