<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShopLevelStatic.aspx.cs" Inherits="ShopStatic_ShopLevelStatic" %>

<!DOCTYPE html>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>商户评级</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("GridViewShop", "gv_OverRow");
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <uc3:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
            navigationText="" navigationUrl="" headName="统计管理 - 商户评级" />
        <div id="box" class="box">
            <div class="content">
                <div class="layout">
                    <div class="layout">
                        <table class="table" style="width: 90%;">

                            <tr>
                                <th>城市：
                                </th>
                                <td>
                                    <asp:DropDownList ID="DropDownListCity" runat="server" Width="150px">
                                        <asp:ListItem Value="">--请选择--</asp:ListItem>
                                        <asp:ListItem Value="87">杭州</asp:ListItem>
                                        <asp:ListItem Value="73">上海</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <th>商户名称：
                                </th>
                                <td>
                                    <asp:TextBox ID="TextBoxShopName" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center">
                                    <asp:Button ID="ButtonQuery" runat="server" Text="查询" CssClass="button" OnClick="ButtonQuery_Click" />
                                </td>

                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: left"></td>

                            </tr>
                        </table>

                    </div>
                    <div class="div_gridview" style="width: 90%">
                        <asp:GridView ID="GridViewShop" runat="server" AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center"
                            ViewStateMode="Enabled" OnRowDataBound="GridViewShop_RowDataBound" SkinID="gridviewSkin">
                            <Columns>
                                <asp:TemplateField HeaderText="行号" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelNumber" runat="server" Text='<%# Container.DisplayIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="200px" ItemStyle-HorizontalAlign="Center" HeaderText="商户名称">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelShopName" runat="server" Text='<%# Eval("shopName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" HeaderText="商户级别">
                                    <ItemTemplate>
                                        <asp:PlaceHolder ID="PlaceHolderLevel" runat="server"></asp:PlaceHolder>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="商户积分">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelScore" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="订单完成量" ItemStyle-HorizontalAlign="Center" DataField="prepayOrderCount" />
                                <asp:TemplateField HeaderText="好评数" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelGoodCount" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="中评数" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelNormalCount" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="差评数" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelBadCount" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:GridView>
                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="liPage" CurrentPageButtonClass="currentButton" CurrentPageButtonPosition="Center" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" NumericButtonType="Text" PageIndexBoxClass="listPageText" PageSize="10" PrevPageText="上一页" ShowPageIndexBox="Always" SubmitButtonClass="listPageBtn" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" OnPageChanged="AspNetPager1_PageChanged">
                        </webdiyer:AspNetPager>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
