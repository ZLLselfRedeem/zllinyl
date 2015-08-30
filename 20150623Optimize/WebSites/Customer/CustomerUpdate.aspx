<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerUpdate.aspx.cs" Inherits="Customer_CustomerUpdate" %>

<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>客户信息</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="../Scripts/messages_cn.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        $().ready(function () {
            iframeParent("#customerBase");
            $("#form1").validate({
                rules: {
                    TextBox_UserName: "required",
                    TextBox_CustomerBirthday: "required"
                },
                messages: {
                    TextBox_UserName: "请输入用户名",
                    TextBox_CustomerBirthday: "生日不能为空"
                }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server" onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <div id="box" class="box">
        <div class="content">
            <div class="layout">
                <table class="table" cellpadding="0" cellspacing="0">
                    <tr>
                        <th>
                            用户名：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_UserName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            性别：
                        </th>
                        <td>
                            <asp:RadioButtonList ID="RadioButtonList_CustomerSex" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1" Selected="True">男</asp:ListItem>
                                <asp:ListItem Value="2">女</asp:ListItem>
                            </asp:RadioButtonList>
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
                            <asp:Label ID="Label_RegisterDate" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            电话：
                        </th>
                        <td>
                            <asp:Label ID="Label_mobilePhoneNumber" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            邮箱：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_customerEmail" runat="server" Width="400"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            住址：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_CustomerAddress" runat="server" Width="400"></asp:TextBox>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="Button_Modify" runat="server" Text="修    改" CssClass="button" OnClick="Button_Modify_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
