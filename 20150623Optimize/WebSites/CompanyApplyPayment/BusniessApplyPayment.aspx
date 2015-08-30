<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BusniessApplyPayment.aspx.cs"
    Inherits="CompanyApplyPayment_BusniessApplyPayment" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" width="80px">
    <title>打款列表</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/searchShop.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            initData("shopapplypayment");
            GridViewStyle("GridView_CheckedNeedToPay", "gv_OverRow");
        });  
    </script>
    <style type="text/css">
        li:hover
        {
            text-decoration: underline;
            color: #39c;
            cursor: pointer;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <uc3:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
        navigationText="" navigationUrl="" headName="打款列表" />
    <div id="box" class="box">
        <div class="content">
            <div class="layout">
                <div class="QueryTerms">
                    <table>
                        <tr>
                            <td>
                                门店搜索：
                            </td>
                            <td>
                                <input id="text" runat="server" type="text" onkeyup="shopSearch()" />
                                <div id="init_date" runat="server" style="position: absolute; clear: both; border: 1px solid #000;
                                    background-color: White">
                                </div>
                            </td>
                            <td>
                                公司名称：
                            </td>
                            <td>
                                <asp:Label ID="Label_companyName" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="结账扣款金额:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox_pay" runat="server"></asp:TextBox>(最大可结款金额：<asp:Label ID="lb_maxAmount"
                                    runat="server" Text="0"></asp:Label>元)&nbsp;
                            </td>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="备注:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox_remark" runat="server" TextMode="MultiLine" Height="60px"
                                    Width="200px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="Button1" runat="server" Text="确定结账扣款" CssClass="couponButtonSubmit"
                                    Width="130px" Height="33px" OnClick="Button1_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:Panel ID="Panel_CheckedNeedToPay" runat="server" CssClass="div_gridview">
                    <asp:GridView ID="GridView_CheckedNeedToPay" runat="server" DataKeyNames="accountId,operTime,accountMoney,remark"
                        AutoGenerateColumns="False" SkinID="gridviewSkin" OnSelectedIndexChanged="GridView_CheckedNeedToPay_SelectedIndexChanged">
                        <Columns>
                            <asp:BoundField DataField="accountId" HeaderText="单号" />
                            <asp:BoundField DataField="operTime" HeaderText="打款时间" />
                            <asp:BoundField DataField="accountMoney" HeaderText="金额" />
                            <asp:BoundField DataField="remark" HeaderText="备注" />
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <img src="../Images/key_edit3.gif" />&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1"
                                        runat="server" CausesValidation="False" CommandName="Select" Text="修改备注"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:Panel CssClass="gridviewBottom" runat="server" ID="PanelPage">
                        <div class="gridviewBottom_left">
                            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" PageSize="10" OnPageChanged="AspNetPager1_PageChanged"
                                FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" SubmitButtonText="Go"
                                TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                                NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" CurrentPageButtonClass="currentButton"
                                PageIndexBoxClass="listPageText" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center"
                                MoreButtonType="Image" NavigationButtonType="Image">
                            </webdiyer:AspNetPager>
                        </div>
                    </asp:Panel>
                </asp:Panel>
                <asp:Panel ID="Panel_Role" runat="server" CssClass="panelSyle">
                    <table>
                        <tr>
                            <th colspan="2" class="dialogBox_th">
                                修改备注
                            </th>
                        </tr>
                        <tr>
                            <th>
                                备注：
                            </th>
                            <td>
                                <asp:TextBox ID="txt_remark" runat="server" TextMode="MultiLine" Width="250px" Height="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_remark"
                                    ErrorMessage="备注不能为空" ForeColor="Red" ValidationGroup="ValidationGroup1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="Button_confirm" runat="server" Text="确    定" CssClass="button" ValidationGroup="ValidationGroup1"
                                    OnClick="Button_confirm_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="Button_cancel" runat="server" Text="取    消" CssClass="button" OnClientClick="HiddenConfirmWindow('Panel_Role')" />
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
