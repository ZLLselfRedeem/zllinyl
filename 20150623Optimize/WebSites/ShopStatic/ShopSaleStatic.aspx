<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShopSaleStatic.aspx.cs" Inherits="ShopStatic_ShopStatic" %>

<!DOCTYPE html>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>门店销售报表</title>
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
            navigationText="" navigationUrl="" headName="统计管理 - 门店销售报表" />
        <div id="box" class="box">
            <div class="content">
                <div class="layout">
                    <div class="layout">
                        <table class="table" style="width: 90%;">
                            
                             <tr>
                                <th>所属城市:</th>
                                <td>
                                    <asp:DropDownList ID="DropDownListCity" runat="server">
                                        <asp:ListItem Value="87" Selected="True">杭州</asp:ListItem>
                                        <asp:ListItem Value="73" >上海</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <th> 
                                    统计时段：</th>
                                <td>
                                   
                                    <asp:DropDownList ID="DropDownListStatic" runat="server"></asp:DropDownList>
                                   
                                </td>
                            </tr>
                            <tr>
                                <th>公司：</th>
                                <td>
                                    <asp:TextBox ID="TextBoxCompanyName" runat="server"></asp:TextBox>
                                </td>
                                <th>商户名称：
                                </th>
                                <td>
                                    <asp:TextBox ID="TextBoxShopName" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center">
                                    <asp:Button ID="ButtonQuery" runat="server" Text="查询" CssClass="button" OnClick="ButtonQuery_Click"   /> 
                                    &nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp; 
                                    <asp:Button ID="ButtonExport" runat="server" Text="导出" CssClass="button" OnClick="ButtonExport_Click"  />
                                </td>

                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: left"></td>

                            </tr>
                        </table>
                        <div class="div_gridview" style="width:auto" >
                            <asp:GridView ID="GridViewShopReport" runat="server" AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center" Width="1600px"
                                ViewStateMode="Enabled" SkinID="gridviewSkin" OnRowDataBound="GridViewShopReport_RowDataBound">
                                <Columns>
                                   
                                    <asp:BoundField HeaderText="排名" ItemStyle-HorizontalAlign="Center" DataField="Ranking">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center"  ItemStyle-Width ="500px" ItemStyle-HorizontalAlign="Center" HeaderText="商户名称">
                                        <ItemTemplate>
                                            <asp:Label ID="LabelShopName" runat="server" Text='<%# Bind("ShopName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="500px" />

                                        <ItemStyle HorizontalAlign="Center" Width="500px"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="订单量" ItemStyle-HorizontalAlign="Center" DataField="PreorderCount"  >
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="最高客单价" ItemStyle-HorizontalAlign="Center" DataField="MaxPreorderSum" DataFormatString="{0:N2}">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="平均客单价" ItemStyle-HorizontalAlign="Center" DataField="AveragePreorderSum" DataFormatString="{0:N2}">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="流水" ItemStyle-HorizontalAlign="Center" DataField="TotalPreorderSum" DataFormatString="{0:N2}">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="流水环比<br/>上升">
                                        <ItemTemplate>
                                            <asp:Label ID="LabelCompareRise" runat="server">-</asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />

                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="节省时间(分钟)" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" DataField="SaveTime">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="老客户" ItemStyle-HorizontalAlign="Center" DataField="OldCustomerCount">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="所有客户" ItemStyle-HorizontalAlign="Center" DataField="TotalCustomerCount">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="老客户比例">
                                        <ItemTemplate>
                                            <asp:Label ID="LabelOldCustomerPercent" runat="server">-</asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="总评分个数"  HeaderStyle-Width="20px" ItemStyle-HorizontalAlign="Center" DataField="TotalEvaluationCount">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="好评数" ItemStyle-HorizontalAlign="Center" DataField="GoodEvaluationCount">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="老客户平<br/>均满意度"> 
                                        <ItemTemplate>
                                            <asp:Label ID="LabelOldCustomerGoodEvaluationPercent" runat="server" >-</asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="抵扣券支<br/>付金额"> 
                                        <ItemTemplate>
                                            <asp:Label ID="LabelTotalCoupon" Text='<%# Bind("DeductibleAmountSum") %>'  runat="server" ></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="累计退<br/>款金额"> 
                                        <ItemTemplate>
                                            <asp:Label ID="LabelRefundMoneySum" Text='<%# Bind("RefoundSum") %>'  runat="server" ></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="红包支<br/>付金额"> 
                                        <ItemTemplate>
                                            <asp:Label ID="LabelTotalRedEnvelope" runat="server" Text='<%# Bind("RedEnvelopeSum") %>' ></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="服务经理" ItemStyle-Width ="400px">
                                        <ItemTemplate>
                                            <asp:Label ID="LabelAccountManager" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />

                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="区域经理" ItemStyle-Width ="400px">
                                        <ItemTemplate>
                                            <asp:Label ID="LabelAreaManager" runat="server"></asp:Label>
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
        </div>
    </form>
</body>
</html>
