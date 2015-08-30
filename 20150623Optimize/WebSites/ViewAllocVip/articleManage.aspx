<%@ Page Language="C#" AutoEventWireup="true" CodeFile="articleManage.aspx.cs" Inherits="ViewAllocVip_articalManage" %>

<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>文章配置页面</title>
    <script type="text/javascript" language="javascript" src="../web/CorpManage/ckeditor/ckeditor.js"></script>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="content">
        <div class="layout">
            <table class="table" cellpadding="0" width="80%" cellspacing="0">
                <tr>
                    <td>
                        城市选择&nbsp;&nbsp;
                        <asp:DropDownList runat="server" ID="ddlCity" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged"
                            AutoPostBack="true" Height="20px" Width="80px">
                            <asp:ListItem Text="全国" Selected="True" Value="0"></asp:ListItem>
                            <asp:ListItem Text="杭州" Value="87"></asp:ListItem>
                            <asp:ListItem Text="上海" Value="9"></asp:ListItem>
                            <asp:ListItem Text="北京" Value="1"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td height="260px">
                        <CKEditor:CKEditorControl ID="CKEditor1" runat="server" Height="260px"></CKEditor:CKEditorControl>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="button" />&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnDelete" runat="server" Text="删除" OnClick="btnDelete_Click" OnClientClick="return confirm('确认删除？')"
                            CssClass="button" />&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnClear" runat="server" Text="清空" OnClick="btnClear_Click" CssClass="button" />
                    </td>
                </tr>
            </table>
            <br />
            <table class="table" cellpadding="0" width="80%" cellspacing="0">
                <tr>
                    <td style="width: 20%">
                        视频（格式：.mp4）
                    </td>
                    <td style="width: 40%">
                        <asp:FileUpload runat="server" ID="FileUploadVideo" />
                    </td>
                    <td>
                        <asp:Button runat="server" ID="btnUpload" Text="上传" OnClick="btnUpload_Click" CssClass="button" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <div class="div_gridview">
                            <asp:GridView runat="server" ID="gdvVideo" AutoGenerateColumns="False" CssClass="gridview"
                                SkinID="gridviewSkin" DataKeyNames="path" OnDataBound="gdvVideo_DataBound">
                                <Columns>
                                    <asp:BoundField DataField="Id" HeaderText="视频编号" Visible="false" />
                                    <asp:BoundField DataField="name" HeaderText="名称" />
                                    <asp:BoundField DataField="path" HeaderText="路径" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <img src="../Images/key_delete.gif" alt="" />
                                            <asp:LinkButton runat="server" ID="lnkbtnDel" CommandName="del" OnCommand="lnkbtnEdit_OnCommand"
                                                OnClientClick="return confirm('确认删除？');" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Id") %>'>删除</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <uc1:CheckUser ID="CheckUser1" runat="server" />
    </form>
</body>
</html>
