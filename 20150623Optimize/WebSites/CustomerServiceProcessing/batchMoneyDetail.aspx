<%@ Page Language="C#" AutoEventWireup="true" CodeFile="batchMoneyDetail.aspx.cs"
    Inherits="CustomerServiceProcessing_batchMoneyDetail" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>批量打款申请明细</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            GridViewStyle("GridView_batchMoneyDetail", "gv_OverRow");
            TabManage();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <uc3:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
            navigationText="批量打款申请明细" navigationUrl="" headName="批量打款申请明细" />
        <div id="box" class="box">
            <div class="tagMenu">
                <ul class="menu">
                    <li>批量打款申请明细</li>
                </ul>
            </div>
            <div class="content">
                <div class="layout">
                    <div class="QueryTerms">
                        <table>
                            <tr>
                                <td>批量打款ID
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_batchMoneyApplyId" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btn_queryBatch" CssClass="button" runat="server" Text="查询批量" OnClick="btn_queryBatch_Click" />
                                </td>
                                <td rowspan="2">
                                    <asp:Button ID="btn_exportExcel" runat="server" Height="50px" Text="导出excel" OnClick="btn_exportExcel_Click" />
                                </td>
                                <td rowspan="2">
                                    <asp:Button ID="btn_batchCancle" runat="server" Height="50px" Text="批量撤销" OnClientClick="return confirm('批量撤销后列表将不再显示该申请，确认撤销？');" OnClick="btn_batchCancle_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>流水号搜索
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_number" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btn_querySingle" CssClass="button" runat="server" Text="查询单笔" OnClick="btn_querySingle_Click" />
                                </td>
                            </tr>
                        </table>
                        <hr size="1" style="border: 1px #cccccc dashed;" />
                        总计打款<asp:Literal ID="lit_totleCount" runat="server"></asp:Literal>笔，<asp:Literal
                            ID="lit_totleAmount" runat="server"></asp:Literal>元，已汇款<asp:Literal ID="lit_remitCount"
                                runat="server"></asp:Literal>笔，<asp:Literal ID="lit_remitAmount" runat="server"></asp:Literal>元
                    ，余额调整<asp:Literal ID="lit_adjustCount" runat="server"></asp:Literal>笔，<asp:Literal
                        ID="lit_adjustAmount" runat="server"></asp:Literal>元
                    <hr size="1" style="border: 1px #cccccc dashed;" />
                    </div>
                    <asp:Panel ID="Panel_batchMoneyManageDetail" runat="server" CssClass="div_gridview">
                        <asp:GridView ID="GridView_batchMoneyDetail" runat="server" DataKeyNames="batchMoneyApplyDetailId,batchMoneyApplyId,companyId,shopId,applyAmount,serialNumberOrRemark,shopName,accountId"
                            AutoGenerateColumns="False" SkinID="gridviewSkin" OnRowCommand="GridView_batchMoneyDetail_RowCommand"
                            Style="margin-top: 5px">
                            <Columns>
                                <asp:BoundField DataField="batchMoneyApplyDetailId" HeaderText="ID" />
                                <asp:BoundField DataField="shopName" HeaderText="门店名" />
                                <asp:BoundField DataField="companyName" HeaderText="公司名" />
                                <asp:BoundField DataField="bankName" HeaderText="开户银行" />
                                <asp:BoundField DataField="accountName" HeaderText="开户名" />
                                <asp:BoundField DataField="accountNum" HeaderText="帐号" />
                                <asp:BoundField DataField="applyAmount" HeaderText="申请打款金额" />
                                <asp:TemplateField ShowHeader="True">
                                    <HeaderTemplate>
                                        流水号或备注
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_serialNumberOrRemark" runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="True">
                                    <HeaderTemplate>
                                        保存操作
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btn_save" runat="server" CausesValidation="False" CommandName="Save"
                                            Text="保存" CssClass="linkButtonDetail"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="haveAdjustAmount" HeaderText="余额调整" />
                                <asp:TemplateField ShowHeader="True">
                                    <HeaderTemplate>
                                        撤销操作
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbSelect" runat="server" />&nbsp;&nbsp;
                                        <asp:LinkButton ID="btn_cancle" runat="server" CausesValidation="False" CommandName="Cancle"
                                            Text="撤销" CssClass="linkButtonDetail"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
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
                    </asp:Panel>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hidden" Value="1" runat="server" />
    </form>
</body>
</html>
