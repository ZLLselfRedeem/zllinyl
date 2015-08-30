<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoleManage.aspx.cs" Inherits="AuthorizationManagement_RoleManage" %>

<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>角色管理</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("GridView1", "gv_OverRow");
        });       
    </script>
</head>
<body>
    <form id="form1" runat="server" onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <uc2:HeadControl ID="HeadControl1" runat="server" headName="角色管理" navigationImage="~/images/icon/new.gif"
        navigationText="角色添加" navigationUrl="~/AuthorizationManagement/RoleAdd.aspx" />
    <div id="box" class="box">
        <div class="tagMenu">
            <ul class="menu">
                <li>角色列表</li>
            </ul>
        </div>
        <div class="content">
            <div class="layout">
                <div class="div_gridview">
                    <asp:GridView ID="GridView1" runat="server" DataKeyNames="RoleID,RoleName,RoleDescription"
                        AutoGenerateColumns="False" OnRowDeleting="GridView1_RowDeleting" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                        SkinID="gridviewSkin">
                        <Columns>
                            <asp:TemplateField HeaderText="行号">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex + 1%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="RoleName" HeaderText="角色名" />
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <img src="../Images/key_edit3.gif" />&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1"
                                        runat="server" CausesValidation="False" CommandName="Select" Text="修改"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <img src="../Images/delete.gif" />&nbsp;&nbsp;<asp:LinkButton ID="LinkButton2" runat="server"
                                        CausesValidation="False" CommandName="delete" Text="删除" OnClientClick="return confirm('你确定删除吗？')"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <asp:Panel ID="Panel_Role" runat="server" CssClass="panelSyle">
                    <table>
                        <tr>
                            <th colspan="3" class="dialogBox_th">
                                修改角色
                            </th>
                        </tr>
                        <tr>
                            <th>
                                角色名：
                            </th>
                            <td colspan="2">
                                <asp:TextBox ID="TextBox_RoleName" runat="server" Width="230px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox_RoleName"
                                    ErrorMessage="角色名不能为空" ForeColor="Red" ValidationGroup="ValidationGroup1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                角色描述：
                            </th>
                            <td colspan="2">
                                <asp:TextBox ID="TextBox_RoleDescription" runat="server" TextMode="MultiLine" Height="50px"
                                    Width="330px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                特殊权限：
                            </th>
                            <td>
                                <asp:GridView ID="GridView_SpecialAuthority" runat="server" DataKeyNames="specialAuthorityName,specialAuthorityId"
                                    AutoGenerateColumns="False" SkinID="gridviewSkin" Width="330px">
                                    <Columns>
                                        <asp:BoundField DataField="specialAuthorityName" HeaderText="权限名称" />
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox_SpecialAuthority" AutoPostBack="true" runat="server" OnCheckedChanged="Check_SpecialAuthority" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                            <td>
                                <asp:Panel ID="Panel_province_city" runat="server">
                                    <div id="div_content_right" class="div_content">
                                        <asp:TreeView ID="TreeView_province_city" runat="server" ShowLines="True" ShowCheckBoxes="All">
                                        </asp:TreeView>
                                    </div>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="Button_confirm" runat="server" Text="确    定" CssClass="button" ValidationGroup="ValidationGroup1"
                                    OnClick="Button_confirm_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="Button_cancel" runat="server" Text="取    消" CssClass="button" OnClientClick="HiddenConfirmWindow('Panel_Role')" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </div>
    </div>
    <uc1:CheckUser ID="CheckUser1" runat="server" />
    </form>
</body>
</html>
