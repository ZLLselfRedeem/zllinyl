<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MenuUpdate.aspx.cs" MaintainScrollPositionOnPostback="true"
    Inherits="MenuManage_MenuUpdate" %>

<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>菜谱修改</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
        });

    </script>
    <style type="text/css">
        .table
        {
            border: solid 1px black;
        }
        .table th
        {
            border-bottom: solid 1px black;
        }
        .table td
        {
            border-bottom: solid 1px black;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" align="center">
        <tr>
            <td class="menubar_title">
                <img border="0" src="../images/icon/user.gif" style="text-align: center" hspace="3"
                    vspace="3" />&nbsp;菜谱修改
            </td>
            <td class="menubar_readme_text" valign="bottom">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td height="27px" class="menubar_function_text">
                目前操作功能：菜谱修改
            </td>
            <td class="menubar_menu_td" align="right">
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="menubar_button" id="button_0">
                            <img border="0" align="top" src="../images/icon/list.gif" />&nbsp;<a href="MenuList.aspx">菜谱列表</a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="5px" colspan="2">
                &nbsp;
            </td>
        </tr>
    </table>
    <div id="box" class="box">
        <div class="tagMenu">
            <ul class="menu">
                <li>菜谱修改</li>
            </ul>
        </div>
        <div class="content">
            <div class="layout">
                <table class="table" cellpadding="0" cellspacing="0" width="90%">
                    <tr>
                        <th>
                            语言：
                        </th>
                        <td>
                            <asp:DropDownList ID="DropDownList_LangID" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <th>
                            菜谱名称：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_MenuName" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox_MenuName"
                                ErrorMessage="此项不能为空" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <th>
                            显示序号：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_MenuSequence" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox_MenuSequence"
                                ErrorMessage="此项不能为空" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                    ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBox_MenuSequence"
                                    ErrorMessage="请输入数字" ForeColor="Red" ValidationExpression="^[0-9]{1,20}$" Display="Dynamic"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            所属公司：
                        </th>
                        <td>
                            <asp:DropDownList ID="DropDownList_Company" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <th>
                            所属门店：
                        </th>
                        <td>
                            <asp:GridView ID="GridView_Shop" CssClass="table" runat="server" DataKeyNames="shopName,shopId"
                                AutoGenerateColumns="False" SkinID="gridviewSkin" Width="300px" ShowHeader="False">
                                <Columns>
                                    <asp:BoundField DataField="shopName" ShowHeader="False" ItemStyle-BorderStyle="Solid"
                                        ItemStyle-BorderColor="ActiveBorder" ItemStyle-BorderWidth="1px" />
                                    <asp:TemplateField ShowHeader="False" ItemStyle-BorderStyle="Solid" ItemStyle-BorderColor="ActiveBorder"
                                        ItemStyle-BorderWidth="1px" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox_Shop" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <th>
                            菜谱描述：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_MenuDesc" runat="server" TextMode="MultiLine" Height="120px"
                                Width="300px"></asp:TextBox>
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
                            <asp:Button ID="Button_Update" runat="server" Text="修    改" CssClass="button" OnClick="Button_Update_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="Button_Cancel" runat="server" Text="返    回" CssClass="button" OnClick="Button_Cancel_Click" />
                            <asp:HiddenField ID="HiddenField_MenuI18nID" runat="server" />
                            <asp:HiddenField ID="HiddenField_MenuID" runat="server" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
