<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShopAmountLogQuery.aspx.cs" Inherits="ShopManage_ShopAmountLogQuery" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script src="../Scripts/searchShop.js" type="text/javascript"></script>
    <title>商户余额日志</title>
     <script type="text/javascript">
         $(document).ready(function () {
             GridViewStyle("shopAmoutLogList", "gv_OverRow");
             initData("shopAmountLog");
             TabManage();
         });
     </script>
</head>
<body>
    <form id="form1" runat="server">
            <uc2:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
                navigationText="" navigationUrl="" headName="商户余额日志" />
         <div class="content">
                <div class="layout">
                    <table class="table" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <th>
                                城市
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlCity" runat="server"></asp:DropDownList>
                            </td>
                            <th>
                                查询时间
                            </th>
                            <td>
                                 <asp:TextBox ID="txLogDate" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd'})"
                                    Width="150px"></asp:TextBox>
                            </td>
                            <th>
                                门店名称
                            </th>
                            <td>
                                <input id="text" runat="server" type="text" onkeyup="ShopAmountLogSearch()" />
                                <div id="init_date" runat="server" style="position: absolute; clear: both; border: 1px solid #000;
                                    background-color: White">
                                </div>
                            </td>
                            <td>
                                <asp:Button runat="server" Width="130px" Height="33px" CssClass="couponButtonSubmit" ID="btnQuery" Text="查询" OnClick="btnQuery_Click" />
                                <asp:Button runat="server" Visible="false" Width="130px" Height="33px" CssClass="couponButtonSubmit" ID="Button1" Text="定时生成调试用" OnClick="Button1_Click" />
                            </td>
                        </tr>
                        </table>
                    <hr size="1" style="border: 1px #cccccc dashed;" />
                        <table style="width: 100%">
                        <tr>
                            <td>
                                 <asp:Label ID="Label_LargePageCount" Visible="false" runat="server" Text=""></asp:Label>
                                    <asp:Button ID="Button_10" runat="server" CssClass="tabButtonBlueUnClick" CommandName="button_10"
                            OnClick="Button_LargePageCount_Click" Width="50px" Text="10"></asp:Button>
                        <asp:Button ID="Button_50" runat="server" CssClass="tabButtonBlueUnClick" CommandName="button_50"
                            OnClick="Button_LargePageCount_Click" Width="50px" Text="50"></asp:Button>
                        <asp:Button ID="Button_100" runat="server" CssClass="tabButtonBlueUnClick" CommandName="button_100"
                            OnClick="Button_LargePageCount_Click" Width="50px" Text="100"></asp:Button>
                            </td>
                            <td>
                                <asp:Button runat="server" Width="130px" Height="33px" CssClass="couponButtonSubmit" Text="导出到Excel" ID="btnExcel" OnClick="btnExcel_Click" />
                                
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div class="div_gridview" id="div_gridview">
                                <asp:GridView AutoGenerateColumns="false" Width="100%" runat="server" SkinID="gridviewSkin" ID="shopAmoutLogList">
                                     <Columns>
                                         <asp:BoundField DataField="shopName" HeaderText="门店名" />
                                         <asp:BoundField DataField="cityName" HeaderText="城市" />
                                         <asp:BoundField DataField="amount" DataFormatString="{0:F}" ItemStyle-HorizontalAlign="Right" HeaderText="商户余额" />
                                     </Columns>
                                </asp:GridView>
                                    <asp:Panel CssClass="gridviewBottom" runat="server" ID="PanelPage">
                            <div class="gridviewBottom_left">
                                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
                                    NextPageText="下一页" PageSize="10" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                                    TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                                    NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" CurrentPageButtonClass="currentButton"
                                    PageIndexBoxClass="listPageText" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center"
                                    MoreButtonType="Image" NavigationButtonType="Image" OnPageChanged="AspNetPager1_PageChanged"></webdiyer:AspNetPager>
                            </div>
                        </asp:Panel>
                    </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
    </form>
</body>
</html>
