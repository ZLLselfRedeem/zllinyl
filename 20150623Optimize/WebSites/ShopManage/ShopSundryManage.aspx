<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShopSundryManage.aspx.cs"
    Inherits="ShopManage_ShopSundryManage" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>杂项管理</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("GridView_ShopSundryManage", "gv_OverRow");
            $("#form1").validate({
                rules: {
                    TextBox_SundryName: { required: true },
                    TextBox_sundryStandary: { required: true },
                    TextBox_Price: { required: true, number: true }
                },
                messages: {
                    TextBox_SundryName: "请输入杂项名称",
                    TextBox_sundryStandary: "请输入杂项规格",
                    TextBox_Price: "请输入单价"
                }
            });
        });
    </script>
</head>
<body scroll="no" style="overflow-y: hidden" onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <form id="form1" runat="server">
    <uc2:HeadControl ID="HeadControl1" runat="server" headName="杂项管理" navigationImage="~/images/icon/list.gif"
        navigationText="门店列表" navigationUrl="~/ShopManage/ShopManage.aspx" />
    <div id="box" class="box">
        <div class="tagMenu">
            <ul class="menu">
                <li>杂项管理</li>
            </ul>
        </div>
        <div class="content">
            <div class="layout">
                <div class="QueryTerms">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label_shop" runat="server"></asp:Label>
                            </td>
                            <td>
                                &nbsp;&nbsp;
                            </td>
                            <td>
                                <asp:Button ID="Button_ReadySundry" runat="server" CssClass="button" Text="启   用"
                                    OnClick="Button_ReadySundry_Click" />
                                <asp:Button ID="Button_Add" runat="server" Text="添    加" CssClass="button" OnClick="Button_Add_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:Panel ID="Panel_TablewareAndRiceList" runat="server" CssClass="div_gridview"
                    Visible="true">
                    <asp:GridView ID="GridView_ShopSundryManage" runat="server" AutoGenerateColumns="False"
                        DataKeyNames="sundryId,shopId,sundryName,sundryStandard,sundryChargeMode,supportChangeQuantity,price,status,description"
                        SkinID="gridviewSkin" OnRowCommand="GridView_ShopSundryManage_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="行号">
                                <ItemTemplate>
                                    <%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="sundryName" HeaderText="杂项名称" />
                            <asp:TemplateField HeaderText="开启状态">
                                <ItemTemplate>
                                    <img src="../Images/key_edit3.gif" />&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1"
                                        runat="server" CausesValidation="False" CommandName="SetIsValid" Text=""></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <img src="../Images/key_edit3.gif" />&nbsp;&nbsp;<asp:LinkButton ID="LinkButton2"
                                        runat="server" CausesValidation="False" CommandName="Modification" Text="修改"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <div class="asp_page">
                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                            FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PageSize="10" PrevPageText="上一页"
                            SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                            NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" CurrentPageButtonClass="currentButton"
                            PageIndexBoxClass="listPageText" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center">
                        </webdiyer:AspNetPager>
                    </div>
                </asp:Panel>
                <asp:Panel ID="Panel_TablewareAndRiceAdd" runat="server" CssClass="div_gridview"
                    Visible="false">
                    <table class="table" cellpadding="0" cellspacing="0">
                        <tr>
                            <th class="style9">
                                门店名称：
                            </th>
                            <td>
                                <asp:DropDownList ID="DropDownList_shop" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                杂项名称：
                            </th>
                            <td class="style1">
                                <asp:TextBox ID="TextBox_SundryName" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                杂项规格：
                            </th>
                            <td>
                                <asp:TextBox ID="TextBox_sundryStandary" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                收费模式：
                            </th>
                            <td>
                                <asp:DropDownList ID="DropDownList_sundryStandard" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="DropDownList_sundryStandard_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Value="0">请选择</asp:ListItem>
                                    <asp:ListItem Value="1">固定金额</asp:ListItem>
                                    <%-- <asp:ListItem Value="2">按比例</asp:ListItem>--%>
                                    <asp:ListItem Value="3">按人次</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                单价/比例：
                            </th>
                            <td>
                                <asp:TextBox ID="TextBox_Price" runat="server" Width="80px"></asp:TextBox>
                                <asp:Literal ID="message" runat="server" Text="（请输入大于0的数字）"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                是否必选：
                            </th>
                            <td>
                                <asp:RadioButton ID="RadioButton_required_true" GroupName="required" runat="server"
                                    Checked="true" Text="是" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="RadioButton_required_false" GroupName="required" runat="server"
                                    Text="否" />
                            </td>
                        </tr>
                        <tr>
                            <th>
                                享受折扣：
                            </th>
                            <td>
                                <asp:RadioButton ID="RadioButton_vipDiscountable_true" GroupName="vipDiscountable"
                                    runat="server" Text="是" Enabled="false" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="RadioButton_vipDiscountable_false" GroupName="vipDiscountable"
                                    Checked="true" runat="server" Text="否" Enabled="false" />
                            </td>
                        </tr>
                        <tr>
                            <th>
                                支持返送：
                            </th>
                            <td>
                                <asp:RadioButton ID="RadioButton_backDiscountable_true" GroupName="backDiscountable"
                                    runat="server" Text="是" Enabled="false" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="RadioButton_backDiscountable_false" GroupName="backDiscountable"
                                    Checked="true" runat="server" Text="否" Enabled="false" />
                            </td>
                        </tr>
                        <tr>
                            <th>
                                信息描述：
                            </th>
                            <td>
                                <asp:TextBox ID="TextBox_MessageDescription" runat="server" Width="220px" TextMode="MultiLine"
                                    Height="102px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Button ID="Button_Comfirm" runat="server" Text="确    定" CssClass="button" OnClick="Button_Comfirm_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button
                                    ID="Button_Back" runat="server" Text="返    回" CssClass="button cancel" OnClick="Button_Back_Click" />
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
