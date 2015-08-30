<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HomeTitleUpdate.aspx.cs" Inherits="HomeNew_HomeTitleUpdate" %>

<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>标签编辑</title>
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
                    TextBox_ownedCompany: { required: true, rangelength: [1, 20] },
                    TextBox_companyTelePhone: { digits: true, rangelength: [6, 12] },
                    TextBox_contactPerson: { maxlength: 5 },
                    TextBox_contactPhone: { digits: true, rangelength: [6, 12] },
                    TextBox_companyAddress: { maxlength: 20 }
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
    <uc2:HeadControl ID="HeadControl1" runat="server" headName="标签编辑" navigationImage="~/images/icon/list.gif" />
    <div id="box" class="box">
        <div class="tagMenu">
            <ul class="menu">
                <li>标签编辑</li>
            </ul>
        </div>
        <div class="content">
            <div class="layout">
                <table class="table" cellpadding="0" cellspacing="0">
                    <tr>
                        <th>
                            标签名称
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_TitleName" runat="server"  Width="155px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            城市
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_cityName" runat="server" ReadOnly="True" Width="155px" enable="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            栏目顺序
                        </th>
                        <td>
                            <asp:TextBox ID="TextOrder" runat="server" Width="155px" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            是否启用
                        </th>
                        <td>
                            <asp:RadioButtonList ID="rblEnable" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1">是</asp:ListItem>
                                <asp:ListItem Value="0">否</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            是否主要标签
                        </th>
                        <td>
                            <asp:RadioButtonList ID="rblIsMaster" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1">是</asp:ListItem>
                                <asp:ListItem Value="0">否</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                     <tr>
                        <th>
                            是否商户
                        </th>
                        <td>
                            <asp:RadioButtonList ID="rblIsMerchant" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1">是</asp:ListItem>
                                <asp:ListItem Value="0">否</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnCancel" runat="server" Text="取    消"   CssClass="button" Onclick ="btnCancel_Click" />
                            <asp:Button ID="btnUpdate" runat="server" Text="修    改"   CssClass="button" OnClick="btnUpdate_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <%--<uc1:CheckUser ID="CheckUser1" runat="server" />--%>
    </form>
</body>
</html>

