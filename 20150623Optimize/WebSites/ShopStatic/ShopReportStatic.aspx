<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShopReportStatic.aspx.cs" Inherits="ShopStatic_ShopReportStatic" %>

<!DOCTYPE html>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>店铺举报</title>
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
            navigationText="" navigationUrl="" headName="统计管理 - 门店数据" />
        <div id="box" class="box">
            <div class="content">
                <div class="layout">
                    <div class="layout">
                        <table class="table" style="width: 90%;">

                            <tr>
                                <th>类别：</th>
                                <td>
                                    <asp:DropDownList ID="DropDownReportType" runat="server" Width="150px">
                                        <asp:ListItem Value="">--请选择--</asp:ListItem>
                                        <asp:ListItem Value="1">商户已关</asp:ListItem>
                                        <asp:ListItem Value="2">商户信息错误</asp:ListItem>
                                        <asp:ListItem Value="3">商户电话错误</asp:ListItem>
                                        <asp:ListItem Value="4">地图信息错误</asp:ListItem>
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
                        </table>
                    </div>
                    <div class="div_gridview" style="width: 90%">
                        <asp:GridView ID="GridViewShopReport" runat="server" AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center"
                            ViewStateMode="Enabled" SkinID="gridviewSkin" OnRowDataBound="GridViewShopReport_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="行号" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelNumber" runat="server" Text='<%# Container.DisplayIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="200px" ItemStyle-HorizontalAlign="Center" HeaderText="商户名称">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelShopName" runat="server" Text='<%# Bind("ShopName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />

                                    <ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" HeaderText="城市">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelCity" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />

                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="服务经理">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelManagerName" runat="server"></asp:Label>
                                        (<asp:Label ID="LabelManagerPhoneNumber" runat="server"></asp:Label>)
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />

                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="举报人">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelUserName" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>(<asp:Label ID="LabelPhoneNumber" runat="server" Text='<%# Bind("MobilePhoneNumber") %>'></asp:Label>)
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />

                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="举报时间" ItemStyle-HorizontalAlign="Center" DataField="ReportTime" DataFormatString="{0:yyyy-MM-dd H:mm}">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="举报信息">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelReportInformation" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />

                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
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
