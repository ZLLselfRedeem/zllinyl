<%@ Page Language="C#" AutoEventWireup="true" CodeFile="financialReconciliation.aspx.cs"
    Inherits="FinanceManage_financialReconciliation" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>财务对账查询</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script src="../Scripts/searchShop.js" type="text/javascript"></script>
    <style type="text/css">
        body{margin:0px;}
        #bg{width:100%;height:100%;top:0px;left:0px;position:absolute;filter: Alpha(opacity=50);opacity:0.5; background:#000000; display:none;}
        #popbox{position:absolute;width:400px; height:400px; left:50%; top:50%; margin:-200px 0 0 -200px; display:none; background:#666666;}
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            initData("financialReconciliation");
            GridViewStyle("gdList", "gv_OverRow");
            TabManage();
        });
    </script>
</head>
<body>
    <form id="form1" autocomplete="off"  runat="server">
        <uc3:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
            navigationText="财务对账查询" navigationUrl="" headName="财务对账查询" />
            <div class="content" id="divDetail" runat="server" style="width: 100%; display: '';">
                    <div style="width: 100%">
                        <table class="table" cellpadding="0" cellspacing="0" style="width: 80%">
                            <tr>
                                <th>对账时间：</th>
                               <td>
                                   <asp:TextBox ID="txtOperateBeginTime" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd 00:00:00'})"
                                        Width="130px"></asp:TextBox>至                                
                                    <asp:TextBox ID="txtOperateEndTime" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd 23:59:59'})"
                                        Width="130px"></asp:TextBox>
                               </td>
                                <th>
                                    城市：
                                </th>
                                <td>
                                     <asp:DropDownList ID="ddlCity" runat="server"></asp:DropDownList>
                                </td>
                               <td colspan="4">

                                   <asp:Button ID="btnSearch" runat="server" CssClass="couponButtonSubmit" Height="33px" OnClick="btnSearch_Click" Text="搜索" Width="130px" />
&nbsp;<asp:Button runat="server" Width="130px" Height="33px" CssClass="couponButtonSubmit" Text="导出Excel" ID="btnExcel" OnClick="btnExcel_Click" />

                               </td>
                            </tr>
                            <tr>
                                <th>门店名：</th>
                                <td>
                                   <input id="text" runat="server" type="text" onkeyup="FinancialReconciliationShopSearch()" />
                                    <div id="init_date" runat="server" style="position: absolute; clear: both; border: 1px solid #000; background-color: White">
                                    </div>
                                </td>
                                <th>佣金情况：</th>
                                <td><asp:DropDownList ID="ddlStatus" runat="server">
                                        <asp:ListItem Value="2">全部</asp:ListItem>
                                        <asp:ListItem Value="1">有产生佣金</asp:ListItem>
                                        <asp:ListItem Value="0">未发生佣金</asp:ListItem>
                                    </asp:DropDownList></td>
                               <td colspan="4"></td>
                            </tr>
                        </table>
                        <hr size="1" style="border: 1px #cccccc dashed;" />
                        <table runat="server" id="gdTable" width="100%">
                            <tr>
                                <td align="center">&nbsp;</td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="Label_LargePageCount" Visible="false" runat="server" Text=""></asp:Label>
                                    <asp:Button ID="Button_10" runat="server" CssClass="tabButtonBlueUnClick" CommandName="button_10"
                                        OnClick="Button_LargePageCount_Click" Width="50px" Text="10"></asp:Button>
                                    <asp:Button ID="Button_50" runat="server" CssClass="tabButtonBlueUnClick" CommandName="button_50"
                                        OnClick="Button_LargePageCount_Click" Width="50px" Text="50"></asp:Button>
                                    <asp:Button ID="Button_100" runat="server" CssClass="tabButtonBlueUnClick" CommandName="button_100"
                                        OnClick="Button_LargePageCount_Click" Width="50px" Text="100"></asp:Button>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="div_gridview" id="div_gridview">
                                        <asp:GridView ID="gdList" runat="server" Width="100%" DataKeyNames="shopName,companyName,prePaidSum,viewallocCommission,
                                            viewallocCommissionValue,viewalloc,redenvelope,balance,alipay,wechat,ExtendPay"
                                            AllowSorting="True" AutoGenerateColumns="False" SkinID="gridviewSkin" OnSorting="gdList_Sorting">
                                            <Columns>
                                                <asp:BoundField DataField="shopName" HeaderText="门店名" SortExpression="shopName" />
                                                <asp:BoundField DataField="companyName" HeaderText="公司名" SortExpression="companyName" />
                                                <asp:BoundField HeaderText="交易量" DataField="prePaidSum" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:F}" SortExpression="preOrderSum" />
                                                <asp:BoundField DataField="viewallocCommissionValue" HeaderText="当前佣金比例" ItemStyle-HorizontalAlign="Right" SortExpression="viewallocCommissionValue" DataFormatString="{0:F}" />
                                                <asp:BoundField HeaderText="实际佣金比例" DataField="viewalloc" ItemStyle-HorizontalAlign="Right" SortExpression="viewalloc" DataFormatString="{0:F}" />
                                                 <asp:BoundField DataField="viewallocCommission" HeaderText="佣金金额" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:F}" SortExpression="viewallocCommission" />
                                                <asp:BoundField DataField="redenvelope" HeaderText="红包支付" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:F}" SortExpression="redenvelope" />
                                                <asp:BoundField DataField="balance" HeaderText="粮票支付" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:F}" SortExpression="balance" />
                                                <asp:BoundField DataField="alipay" HeaderText="支付宝支付" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:F}" SortExpression="alipay" />
                                                <asp:BoundField DataField="wechat" HeaderText="微信支付" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:F}" SortExpression="wechat" />
                                                <asp:BoundField DataField="ExtendPay" HeaderText="纯红包贴补" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:F}" SortExpression="ExtendPay" />
                                            </Columns>
                                        </asp:GridView>

                                        <asp:Panel CssClass="gridviewBottom" runat="server" ID="PanelPage">
                                            <div class="gridviewBottom_left">
                                                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
                                                    NextPageText="下一页" PageSize="10" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                                                    TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                                                    NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" CurrentPageButtonClass="currentButton"
                                                    PageIndexBoxClass="listPageText" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center"
                                                    MoreButtonType="Image" NavigationButtonType="Image" OnPageChanged="AspNetPager1_PageChanged">
                                                </webdiyer:AspNetPager>
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
