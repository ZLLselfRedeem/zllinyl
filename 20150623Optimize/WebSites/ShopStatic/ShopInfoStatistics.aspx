<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShopInfoStatistics.aspx.cs" Inherits="ShopStatic_ShopInfoStatistics" %>

<!DOCTYPE html>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>门店统计信息</title>
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
            navigationText="" navigationUrl="" headName="统计管理 - 门店统计信息" />
        <div id="box" class="box">
            <div class="content">
                <div class="layout">
                    <div class="layout">
                        <table class="table" style="width: 90%;">

                            <tr>
                                <th style="width:15%">统计时段：</th> 
                                <td style="width:35%"> 
                                  <asp:DropDownList ID="DropDownListStatic" runat="server"></asp:DropDownList>
                                </td>
                                <th  style="width:15%"> 
                                </th>
                                <td>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center">
                                    <asp:Button ID="ButtonQuery" runat="server" Text="查询" CssClass="button" OnClick="ButtonQuery_Click"   />
                                </td>

                            </tr> 
                        </table>
                    </div>
                    <div class="div_gridview" style="width: 90%">
                        <asp:GridView ID="GridViewShop" runat="server" AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center"
                            ViewStateMode="Enabled" SkinID="gridviewSkin" OnRowDataBound="GridViewShop_RowDataBound" >
                            <Columns>
                                <asp:TemplateField HeaderText="行号" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelNumber" runat="server" Text='<%# Container.DisplayIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" HeaderText="城市">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelCity" runat="server" ></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />

                                    <ItemStyle HorizontalAlign="Center" ></ItemStyle>
                                </asp:TemplateField> 
                                <asp:BoundField HeaderText="累计门店" ItemStyle-HorizontalAlign="Center" DataField="TotalCount"  >
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="目前在线门店" ItemStyle-HorizontalAlign="Center" DataField="OnlineCount"  >
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField> 
                                <asp:BoundField HeaderText="新增门店" ItemStyle-HorizontalAlign="Center" DataField="AddedCount"  >
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="暂停支付门店" ItemStyle-HorizontalAlign="Center" DataField="StopPaymentCount"  >
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField> 
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:GridView>
                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="liPage" CurrentPageButtonClass="currentButton" CurrentPageButtonPosition="Center" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" NumericButtonType="Text" PageIndexBoxClass="listPageText" PageSize="10" PrevPageText="上一页" ShowPageIndexBox="Always" SubmitButtonClass="listPageBtn" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" OnPageChanged="AspNetPager1_PageChanged"  >
                        </webdiyer:AspNetPager>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
