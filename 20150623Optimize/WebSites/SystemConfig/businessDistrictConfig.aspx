<%@ Page Language="C#" AutoEventWireup="true" CodeFile="businessDistrictConfig.aspx.cs" Inherits="SystemConfig_businessDistrictConfig" %>

<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>功能模块管理</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("grFirstBusinessDistrict", "gv_OverRow");
            GridViewStyle("grSecondBusinessDistrict", "gv_OverRow");
        });
    </script>
</head>
<body>
    <form id="form1" runat="server" onkeypress="if(event.keyCode==13||event.which==13){return false;}">
        <uc2:HeadControl ID="HeadControl1" runat="server" headName="商圈配置" navigationImage="~/images/icon/new.gif"
            navigationText="" navigationUrl="" />
        <div id="box" class="box">
            <div class="tagMenu">
                <ul class="menu">
                    <li>商圈配置</li>
                </ul>
            </div>
            <div class="content">
                <div class="layout">
                    <div class="QueryTerms">
                        <table>
                            <tr>
                                <td>商圈城市：<asp:DropDownList ID="ddlCity" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged">
                                    <asp:ListItem Value="87" Selected="True">杭州市</asp:ListItem>
                                    <asp:ListItem Value="1">北京市</asp:ListItem>
                                    <asp:ListItem Value="73">上海市</asp:ListItem>
                                    <asp:ListItem Value="179">广州市</asp:ListItem>
                                    <asp:ListItem Value="199">深圳市</asp:ListItem>
                                </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>一级商圈：<asp:TextBox ID="txtFirstBusinessDistrict" runat="server"></asp:TextBox>
                                    &nbsp;
                                    <asp:Button ID="btnFirstBusinessDistrict" runat="server" CssClass="button" Text="添 加" OnClick="btnFirstBusinessDistrict_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>二级商圈：<asp:DropDownList ID="ddlFirstBusinessDistrict" runat="server"></asp:DropDownList>
                                    &nbsp;
                                    <asp:TextBox ID="txtSecondBusinessDistrict" runat="server"></asp:TextBox>
                                    &nbsp;
                                    <asp:Button ID="btnSecondBusinessDistrict" runat="server" CssClass="button" Text="添 加" OnClick="btnSecondBusinessDistrict_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <table>
                        <tr>
                            <td style="vertical-align: top">
                                <asp:GridView ID="grFirstBusinessDistrict" runat="server" DataKeyNames="TagId,Flag"
                                    AutoGenerateColumns="False"
                                    SkinID="gridviewSkin" OnRowCommand="grFirstBusinessDistrict_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="一级商圈名称">
                                            <ItemTemplate>
                                                <asp:TextBox ID="grtxtFirstBusinessDistrict" runat="server" Text='<%# Bind("Name") %>'
                                                    Width="200"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ShopCount" HeaderText="门店数量" ReadOnly="True" />
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkUpdateFirst" runat="server" CausesValidation="False" CommandName="UpdateFirst"
                                                    Text="保 存" CssClass="linkButtonDetail"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <img src="../Images/delete.gif" />
                                                <asp:LinkButton ID="lnkDeleteFirst" runat="server" CausesValidation="False"
                                                    CommandName="DeleteFirst" Text="删除" OnClientClick="return confirm('你确定删除吗？')"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <img src="../Images/key_detail.gif" />
                                                <asp:LinkButton ID="lnkQuerySecond" runat="server" CausesValidation="False"
                                                    CommandName="QuerySecond" Text="查看二级商圈"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                            <td style="vertical-align: top">
                                <asp:GridView ID="grSecondBusinessDistrict" runat="server" DataKeyNames="TagId"
                                    AutoGenerateColumns="False" SkinID="gridviewSkin" OnRowCommand="grSecondBusinessDistrict_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="二级商圈名称">
                                            <ItemTemplate>
                                                <asp:TextBox ID="grtxtSecondBusinessDistrict" runat="server" Text='<%# Bind("Name") %>'
                                                    Width="200"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ShopCount" HeaderText="门店数量" />
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkUpdateSecond" runat="server" CausesValidation="False" CommandName="UpdateSecond"
                                                    Text="保 存" CssClass="linkButtonDetail"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <img src="../Images/delete.gif" />
                                                <asp:LinkButton ID="lnkDeleteSecond" runat="server" CausesValidation="False"
                                                    CommandName="DeleteSecond" Text="删除" OnClientClick="return confirm('你确定删除吗？')"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
