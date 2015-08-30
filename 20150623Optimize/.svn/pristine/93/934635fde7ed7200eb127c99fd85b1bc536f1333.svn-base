<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompanyAdd.aspx.cs" Inherits="CompanyManage_CompanyAdd" %>

<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>公司添加</title>
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
                    TextBox_companyName: { required: true, rangelength: [1, 20] },
                    TextBox_ownedCompany:{ required: true, rangelength: [1, 20] },
                    TextBox_companyTelePhone: { digits: true, rangelength: [6, 12] },
                    TextBox_contactPerson: { maxlength: 5 },
                    TextBox_contactPhone: { digits: true, rangelength: [6, 12] },
                    TextBox_companyAddress: { maxlength:20 }
                },
                messages: {
                    TextBox_companyName: "请输入1到20位品牌名称",
                    TextBox_ownedCompany: "请输入1到20位公司名称",
                    TextBox_companyTelePhone: "请输入合法电话号码",
                    TextBox_contactPerson: "联系人不超过5个字符",
                    TextBox_contactPhone: "请输入合法电话号码",
                    TextBox_companyAddress: "地址不超过20个字符"
                }
            });
        });

    </script>
</head>
<body scroll="no" style="overflow-y:hidden" onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <form id="form1" runat="server">
    <uc2:HeadControl ID="HeadControl1" runat="server" headName="公司添加" navigationImage="~/images/icon/list.gif" navigationText="公司列表" navigationUrl="~/CompanyManage/CompanyManage.aspx" />
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
                            品牌名：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_companyName" runat="server"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <th>
                            所属公司：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_ownedCompany" runat="server"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <th>
                            公司电话：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_companyTelePhone" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            联系人：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_contactPerson" runat="server"></asp:TextBox>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <th>
                            联系人电话：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_contactPhone" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            公司地址：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_companyAddress" runat="server" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <th>
                            公司描述：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_companyDescription" runat="server" Width="300" TextMode="MultiLine" Height="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="Button_AddCompany" runat="server" Text="添    加" OnClick="Button_AddCompany_Click" CssClass="button" />
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
