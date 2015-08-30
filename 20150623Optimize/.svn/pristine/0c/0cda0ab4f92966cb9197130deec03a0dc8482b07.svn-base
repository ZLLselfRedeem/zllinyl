<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompanyUpdate.aspx.cs" Inherits="CompanyManage_CompanyUpdate" %>

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
                    TextBox_companyName: "required"
                },
                messages: {
                    TextBox_companyName: "请输入公司名"
                }
            });
        });
    </script>
</head>
<body scroll="no" style="overflow-y: hidden" onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <form id="form1" runat="server">
    <uc2:HeadControl ID="HeadControl1" runat="server" headName="公司修改" navigationImage="~/images/icon/list.gif"
        navigationText="公司列表" navigationUrl="javascript:history.go(-1)" />
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
                            新浪微博：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_sinaWeibo" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            腾讯微博：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_qqWeibo" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            微信公共帐号：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_wechatPublicName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            人均消费：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_accp" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            公司地址：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_companyAddress" runat="server" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            公司描述：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_companyDescription" runat="server" Width="300" TextMode="MultiLine"
                                Height="50"></asp:TextBox>
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
    </form>
</body>
</html>
