<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EncourageConfig.aspx.cs"
    Inherits="VAEncourageConfig_EncourageConfig" %>

<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>奖励配置</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("GridView_VAEncourageConfig", "gv_OverRow");
            $("#div_content").css({ "height": $(window).height() - 100 });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <uc2:HeadControl ID="HeadControl1" runat="server" headName="奖励配置" navigationImage="~/images/icon/new.gif"
        navigationText="奖励配置" navigationUrl="" />
    <div id="box" class="box">
        <div class="tagMenu">
            <ul class="menu">
                <li>奖励配置</li>
            </ul>
        </div>
        <div class="content">
            <div class="layout">
                <div class="QueryTerms">
                    <table>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="Button_financialTypeAdd" runat="server" Text="添加奖励配置" OnClick="Button_VAEncourageConfigAdd_Click"
                                    Visible="false" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="div_gridview">
                    <asp:GridView ID="GridView_VAEncourageConfig" runat="server" DataKeyNames="id,configName,configDescription,configContent"
                        SkinID="gridviewSkin" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView_VAEncourageConfig_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderText="行号">
                                <ItemTemplate>
                                    <%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="configName" HeaderText="配置名称" />
                            <asp:BoundField DataField="configContent" HeaderText="配置值" />
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <img src="../Images/key_edit3.gif" />&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1"
                                        runat="server" CausesValidation="False" CommandName="Select" Text="修改"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <div class="asp_page">
                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
                            NextPageText="下一页" PageSize="10" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                            TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                            NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" PageIndexBoxClass="listPageText"
                            CurrentPageButtonClass="currentButton" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center">
                        </webdiyer:AspNetPager>
                    </div>
                </div>
                <asp:Panel ID="Panel_VAEncourageConfig" runat="server" CssClass="panelSyle">
                    <table>
                        <tr>
                            <th class="dialogBox_th" colspan="3">
                                奖励配置
                            </th>
                        </tr>
                        <tr>
                            <th>
                                配置名称：
                            </th>
                            <td>
                                <asp:TextBox ID="TextBox_ConfigName" runat="server" Width="215px" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                配置描述：
                            </th>
                            <td>
                                <asp:TextBox ID="TextBox_ConfigDescription" runat="server" TextMode="MultiLine" Height="70px"
                                    Width="214px" ReadOnly="True" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                消息设置：
                            </th>
                            <td>
                                <asp:TextBox ID="TextBox_configMessage" runat="server" Height="70px" TextMode="MultiLine"
                                    Width="214px" ReadOnly="True" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                配置值：
                            </th>
                            <td>
                                <asp:TextBox ID="TextBox_ConfigContent" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="Button_AddVAEncourageConfig" runat="server" CssClass="button" Text="确    定"
                                    OnClick="Button_AddVAEncourageConfig_Click" />
                                <asp:Button ID="Button_CancelAddfinancialType" runat="server" CssClass="button" Text="取    消"
                                    OnClick="Button_CancelAddfinancialType_Click" />
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
