<%@ Page Language="C#" AutoEventWireup="true" CodeFile="corporationAccounts.aspx.cs"
    Inherits="FinanceManage_corporationAccounts" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>公司账户查询</title>
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
</head>
<body>
    <form id="form1" autocomplete="off"  runat="server">
        <uc3:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
            navigationText="公司账户查询" navigationUrl="" headName="公司账户查询" />
            <div class="content" id="divDetail" runat="server" style="width: 100%; display: '';">
                    <div style="width: 100%">
                        <asp:Label runat="server" ID="lbAccount"></asp:Label>
                        <hr size="1" style="border: 1px #cccccc dashed;" />
                        <table class="table" cellpadding="0" cellspacing="0" style="width: 80%">
                            <tr>
                                <th>交易时间：</th>
                               <td colspan="3">
                                   <asp:TextBox ID="txtOperateBeginTime" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd 00:00:00'})"
                                        Width="130px"></asp:TextBox>至                                
                                    <asp:TextBox ID="txtOperateEndTime" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd 23:59:59'})"
                                        Width="130px"></asp:TextBox>
                               </td>
                                <th>
                                    对方账号：
                                </th>
                                <td>
                                    <asp:TextBox runat="server" ID="txtAccount"></asp:TextBox>
                                </td>
                               <td>

                                   <asp:Button ID="btnSearch" runat="server" CssClass="couponButtonSubmit" Height="33px" OnClick="btnSearch_Click" Text="查询" Width="130px" />
&nbsp;<asp:Button runat="server" Width="130px" Height="33px" CssClass="couponButtonSubmit" Text="导出Excel" Visible="false" ID="btnExcel" OnClick="btnExcel_Click" />

                               </td>
                            </tr>
                        </table>
                        <hr size="1" style="border: 1px #cccccc dashed;" />
                        <table runat="server" id="gdTable" width="100%">
                            <tr>
                                <td align="center">&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="div_gridview" id="div_gridview">
                                        <asp:GridView ID="gdList" runat="server" Width="100%" DataKeyNames="TranFlag,PayeeAcctNo,PayeeName,TxAmount,Balance,Remark,Note,TransDate,TransTime"
                                            AllowSorting="True" AutoGenerateColumns="False" SkinID="gridviewSkin" OnRowDataBound="gdList_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="SeqNo" HeaderText="流水号" />
                                                <asp:TemplateField HeaderText="交易时间">
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="对方账号" DataField="PayeeAcctNo" />
                                                <asp:BoundField DataField="PayeeName" HeaderText="对方户名" />
                                                <asp:BoundField HeaderText="借方金额" DataField="TxAmount" ItemStyle-HorizontalAlign="Right" SortExpression="viewalloc" DataFormatString="{0:F}" />
                                                 <asp:BoundField DataField="TxAmount" HeaderText="贷方金额" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:F}" />
                                                <asp:BoundField DataField="Balance" HeaderText="余额" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:F}" />
                                                <asp:BoundField DataField="Remark" HeaderText="摘要" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="Note" HeaderText="备注" ItemStyle-HorizontalAlign="Right" />
                                            </Columns>
                                        </asp:GridView>
                                        <asp:Label runat="server" ID="lbCount"></asp:Label>
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
