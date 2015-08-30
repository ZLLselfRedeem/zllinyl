<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConsumptionWeekStatistics.aspx.cs"
    Inherits="StatisticalStatement_ConsumptionWeekStatistics" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>API访问次数统计</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="QueryTerms">
                <asp:Panel ID="Panel__ConsumptionWeekStatistics" runat="server">
                    <table>
                        <tr>
                            <td>城市：<asp:DropDownList ID="DropDownList_City" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_City_SelectedIndexChanged">
                            </asp:DropDownList>
                            </td>
                            <td>公司：<asp:DropDownList ID="DropDownList_Company" runat="server" AutoPostBack="True"
                                OnSelectedIndexChanged="DropDownList_Company_SelectedIndexChanged">
                            </asp:DropDownList>
                            </td>
                            <td>门店：<asp:DropDownList ID="DropDownList_Shop" runat="server" AutoPostBack="True"
                                OnSelectedIndexChanged="DropDownList_Shop_SelectedIndexChanged">
                            </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: middle" colspan="2">统计日期：
                            <asp:TextBox ID="TextBox_preOrderTime" runat="server" AutoPostBack="true" CssClass="Wdate"
                                onFocus="endDate(this)" Width="85px"
                                OnTextChanged="TextBox_preOrderTime_TextChanged"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label_Message" runat="server" CssClass="Red" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <asp:Panel ID="Panel_ConsumptionWeekStatistics" runat="server">
                <div class="div_gridview" id="div_gridview">
                    <asp:GridView ID="GridView_ConsumptionWeekStatistics" runat="server" DataKeyNames="week,newPreOrderCount,newPreOrderAmount,newPreOrderCount_Shop,newPreOrderAmount_Shop,
                newPreOrderCount_Shop_Proportion,newPreOrderCount_isPaid,newPreOrderAmount_isPaid,newPreOrderAmount_isPaid_Proportion"
                        AutoGenerateColumns="False" SkinID="gridviewSkin">
                        <Columns>
                            <asp:BoundField DataField="week" HeaderText="日期" />
                            <asp:BoundField DataField="newPreOrderCount" HeaderText="新增单数" />
                            <asp:BoundField DataField="newPreOrderAmount" HeaderText="新增金额" />
                            <asp:BoundField DataField="newPreOrderCount_Shop" HeaderText="新增到店" />
                            <asp:BoundField DataField="newPreOrderAmount_Shop" HeaderText="新增到店金额" />
                            <asp:BoundField DataField="newPreOrderCount_Shop_Proportion" HeaderText="到店比例" />
                            <asp:BoundField DataField="newPreOrderCount_isPaid" HeaderText="新增手机支付（笔）" />
                            <asp:BoundField DataField="newPreOrderAmount_isPaid" HeaderText="新增手机支付金额" />
                            <asp:BoundField DataField="newPreOrderAmount_isPaid_Proportion" HeaderText="支付比例" />
                        </Columns>
                    </asp:GridView>
                </div>
                <asp:Label ID="Label_massage" runat="server" CssClass="Red" Text=""></asp:Label>
            </asp:Panel>
            <br />
            <div class="QueryTerms">
            </div>
            <asp:Panel ID="Panel1" runat="server">

                <div class="div_gridview" id="div1">
                    <h2>当前公司当前门店合计：</h2>
                    <asp:GridView ID="GridView_All" runat="server" DataKeyNames="newPreOrderCount,newPreOrderAmount,newPreOrderCount_Shop,newPreOrderAmount_Shop,
                newPreOrderCount_Shop_Proportion,newPreOrderCount_isPaid,newPreOrderAmount_isPaid,newPreOrderAmount_isPaid_Proportion"
                        AutoGenerateColumns="False" SkinID="gridviewSkin">
                        <Columns>
                            <asp:BoundField DataField="newPreOrderCount" HeaderText="新增单数" />
                            <asp:BoundField DataField="newPreOrderAmount" HeaderText="新增金额" />
                            <asp:BoundField DataField="newPreOrderCount_Shop" HeaderText="新增到店" />
                            <asp:BoundField DataField="newPreOrderAmount_Shop" HeaderText="新增到店金额" />
                            <asp:BoundField DataField="newPreOrderCount_Shop_Proportion" HeaderText="到店比例" />
                            <asp:BoundField DataField="newPreOrderCount_isPaid" HeaderText="新增手机支付（笔）" />
                            <asp:BoundField DataField="newPreOrderAmount_isPaid" HeaderText="新增手机支付金额" />
                            <asp:BoundField DataField="newPreOrderAmount_isPaid_Proportion" HeaderText="支付比例" />
                        </Columns>
                    </asp:GridView>
                </div>
                <asp:Label ID="Label1" runat="server" CssClass="Red" Text=""></asp:Label>
            </asp:Panel>
        </div>
    </form>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("GridView_ConsumptionWeekStatistics", "gv_OverRow");
            GridViewStyle("GridView_All", "gv_OverRow");
        });
        var endDate = function (elem) {
            WdatePicker({
                el: elem,
                isShowClear: false,
                maxDate: '%y-%M-{%d+1}',
                minDate: '#F{$dp.$D(\'TextBox_preOrderTimeStr\')}',
                onpicked: function (dp) { elem.blur() },
                skin: 'whyGreen'
            });
        }
    </script>
</body>
</html>
