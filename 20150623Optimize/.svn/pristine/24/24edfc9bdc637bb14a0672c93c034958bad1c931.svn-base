<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MenuAdd.aspx.cs" Inherits="MenuManage_MenuAdd" %>

<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>菜谱添加</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script src="../Scripts/searchShop.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            initData("addmenu");
            TabManage();
            $("#form1").validate({
                rules: {
                    TextBox_MenuSequence: { "number": true, "min": 1 },
                    TextBox_MenuName: { required: true }
                },
                messages: {
                    TextBox_MenuSequence: "请填写大于0的整数",
                    TextBox_MenuName: "菜谱名不能为空"
                }
            });
        });
    </script>
    <style type="text/css">
        li:hover
        {
            text-decoration: underline;
            color: #39c;
            cursor: pointer;
        }
    </style>
</head>
<body scroll="auto" onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <form id="form1" runat="server">
    <uc2:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
        navigationText="菜谱列表" navigationUrl="javascript:history.go(-1)" headName="菜谱添加" />
    <div id="box" class="box">
        <div class="tagMenu">
            <ul class="menu">
                <li>菜谱添加</li>
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
                    </tr>
                    <tr>
                        <th>
                            菜谱名称：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_MenuName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <th class="style1">
                            显示序号：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_MenuSequence" runat="server">1</asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            公司名称搜索：
                        </th>
                        <td>
                            <input id="text" runat="server" type="text" onkeyup="menuAddSearchCompany()" />
                            <div id="init_date" runat="server" style="position: absolute; clear: both; border: 1px solid #000;
                                background-color: White">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            所在门店：
                        </th>
                        <td>
                            <asp:GridView ID="GridView_Shop" runat="server" DataKeyNames="shopName,shopID,shopImagePath"
                                AutoGenerateColumns="False" SkinID="gridviewSkin" Width="300px">
                                <Columns>
                                    <asp:BoundField DataField="shopName" HeaderText="门店名称" />
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox_Shop" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            是否默认菜谱
                        </th>
                        <td>
                            <asp:RadioButtonList ID="RadioButtonList_defaultMenu" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="true">是</asp:ListItem>
                                <asp:ListItem Value="false" Selected="True">否</asp:ListItem>
                            </asp:RadioButtonList>
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
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="Button_Add" runat="server" Text="添    加" OnClientClick="return confirm('请确定当前门店没有添加过菜谱，否则此操作将删除添加过菜谱，该菜谱关联的菜也将不存在，确定此操作？');"
                                CssClass="button" OnClick="Button_Add_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
