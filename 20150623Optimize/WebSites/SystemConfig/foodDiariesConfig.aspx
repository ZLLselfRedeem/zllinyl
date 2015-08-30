<%@ Page Language="C#" AutoEventWireup="true" CodeFile="foodDiariesConfig.aspx.cs"
    Inherits="SystemConfig_foodDiariesConfig" %>

<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>美食日记分享配置</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../web/CorpManage/ckeditor/ckeditor.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("GridView_List", "gv_OverRow");
        });
    </script>
    <style type="text/css">
        .td
        {
            width: 700px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <uc2:HeadControl ID="HeadControl1" runat="server" headName="" navigationImage="~/images/icon/list.gif"
        navigationText="" navigationUrl="" />
    <div id="box" class="box">
        <div class="tagMenu">
            <ul class="menu">
                <li>美食日记分享配置</li>
            </ul>
        </div>
        <div class="content">
            <div class="layout">
                <asp:Panel ID="Panel_List" runat="server" CssClass="div_gridview">
                    <div>
                        <asp:Button ID="btn_nva" runat="server" CssClass="button" Text="添 加" OnClick="btn_nva_Click" />
                    </div>
                    <hr />
                    <div class="asp_page">
                        <asp:GridView ID="GridView_List" runat="server" DataKeyNames="id,foodDiariesShareInfo,type,status,typaName"
                            AutoGenerateColumns="False" SkinID="gridviewSkin" OnSelectedIndexChanged="GridView_List_SelectedIndexChanged"
                            OnRowDeleting="GridView_List_RowDeleting">
                            <Columns>
                                <asp:BoundField DataField="typaName" HeaderText="信息类别" />
                                <asp:BoundField DataField="foodDiariesShareInfo" HeaderText="描述信息" ItemStyle-Width="60%" />
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <img src="../Images/key_edit3.gif" />&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1"
                                            runat="server" CausesValidation="False" CommandName="Select" Text="修改"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <img src="../Images/key_edit3.gif" />&nbsp;&nbsp;<asp:LinkButton ID="LinkButton2"
                                            runat="server" CausesValidation="False" CommandName="Delete" Text="删除" OnClientClick="return confirm('确定删除？');"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </asp:Panel>
                <asp:Panel ID="Panel_Add" runat="server" Visible="false" CssClass="div_gridview">
                    <table class="table" cellpadding="0" cellspacing="0">
                        <tr>
                            <th>
                                分类：
                            </th>
                            <td class="td">
                                <asp:DropDownList ID="ddl_type" runat="server">
                                    <asp:ListItem Text="美食日记分享页面顶部描述" Value="1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="美食日记分享页面底部描述(app)" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="美食日记分享页面底部描述(pc)" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                描述信息：
                            </th>
                            <td class="td">
                                <CKEditor:CKEditorControl ID="txt_CKEditor" runat="server" Height="300px"></CKEditor:CKEditorControl>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_CKEditor"
                                    ErrorMessage="描述信息不能为空"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="Button_Save" runat="server" CssClass="button" Text="保  存" OnClick="Button_Save_Click" />&nbsp;&nbsp;
                                <asp:Button ID="Button_Back" runat="server" OnClientClick="return confirm('是否放弃当前编辑?')"
                                    CssClass="button" Text="返  回" OnClick="Button_Back_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
