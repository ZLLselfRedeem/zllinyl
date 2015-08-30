<%@ Page Language="C#" AutoEventWireup="true" CodeFile="originalRoadRefund.aspx.cs"
    Inherits="CustomerServiceProcessing_originalRoadRefund" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>退款原路返回处理</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            GridViewStyle("GridView_List", "gv_OverRow");
        });

    </script>
</head>
<body>
    <form id="form1" runat="server" onkeypress="if(event.keyCode==13||event.which==13){return false;}">
        <uc3:HeadControl ID="HeadControl1" runat="server" navigationImage="" navigationText=""
            navigationUrl="" headName="退款原路返回处理" />
        <div id="box" class="box">
            <div class="tagMenu">
                <ul class="menu">
                    <li>退款原路返回处理</li>
                </ul>
            </div>
            <div class="content">
                <div class="layout">
                    <div class="QueryTerms">
                        <table>
                            <tr>
                                <td colspan="3">
                                    <asp:RadioButtonList runat="server" ID="rblRefundType" RepeatDirection="Horizontal"
                                        AutoPostBack="true" OnSelectedIndexChanged="rblRefundType_SelectedIndexChanged">
                                        <asp:ListItem Text="正常支付" Value="1" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="重复支付" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <%--<asp:RadioButton ID="rb_not" AutoPostBack="true" GroupName="rb" Checked="true" Text="未打款"
                                    runat="server" OnCheckedChanged="rb_CheckedChanged" />--%>
                                    <asp:RadioButton ID="rb_d" AutoPostBack="true" GroupName="rb" Text="打款中" OnCheckedChanged="rb_CheckedChanged"
                                        runat="server" />
                                    <asp:RadioButton ID="rb_yes" AutoPostBack="true" GroupName="rb" Text="已打款" OnCheckedChanged="rb_CheckedChanged"
                                        runat="server" />
                                    <asp:RadioButton ID="rb_fail" AutoPostBack="true" GroupName="rb" Text="打款失败" OnCheckedChanged="rb_CheckedChanged"
                                        runat="server" />
                                    <asp:RadioButton ID="rb_all" AutoPostBack="true" OnCheckedChanged="rb_CheckedChanged"
                                        GroupName="rb" Text="全部" runat="server" />&nbsp;&nbsp;
                                </td>
                                <td>
                                    <label>
                                        流水号：&nbsp;&nbsp;&nbsp;
                                    </label>
                                    <asp:TextBox ID="TextBox_preOrderId" runat="server" Style="width: 100px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        申请开始时间：</label><asp:TextBox ID="TextBox_startTime" runat="server" class="Wdate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"
                                            Style="width: 140px"></asp:TextBox>
                                </td>
                                <td style="width: 30%">
                                    <label>
                                        申请结束时间：<asp:TextBox ID="TextBox_endTime" class="Wdate" runat="server" Style="width: 140px"
                                            onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox>
                                    </label>
                                </td>
                                <td style="width: 30%">
                                    <label>
                                        手机号码：</label><asp:TextBox ID="TextBox_PhoneNum" runat="server" Style="width: 100px"></asp:TextBox>
                                </td>
                                <td style="width: 10%">
                                    <asp:Button ID="btn_Search" class="button" runat="server" Text="查   询" OnClick="btn_Search_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                  <%--  <asp:Panel ID="Panel_List" runat="server" CssClass="div_gridview">--%>
                        <asp:GridView ID="GridView_List" runat="server" DataKeyNames="id,type,connId,refundAmount,applicationTime,remittanceTime,status,remitEmployee,customerMobilephone,note,customerUserName,employeeId,RefundPayType"
                            AutoGenerateColumns="False" SkinID="gridviewSkin" OnRowCommand="GridView1_RowCommand"
                            OnRowDataBound="GridView_List_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="connId" HeaderText="流水号" />
                                <asp:BoundField DataField="customerMobilephone" HeaderText="用户手机" />
                                <asp:BoundField DataField="customerUserName" HeaderText="昵称" />
                                <asp:BoundField DataField="aliBuyerEmail" HeaderText="支付宝账号" />
                                <asp:BoundField DataField="refundAmount" HeaderText="金额" />
                                <asp:BoundField DataField="applicationTime" HeaderText="申请时间" />
                                <asp:TemplateField HeaderText="打款时间">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%#repRemittanceTime(Eval("remittanceTime").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="打款人">
                                    <ItemTemplate>
                                        <asp:Label ID="Label_remitEmployee" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="employeeId" HeaderText="收银宝服务员" />
                                <asp:TemplateField HeaderText="操作">
                                    <ItemTemplate>
                                        <asp:Label ID="Label_status" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <img src="../Images/key_edit3.gif" />&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1"
                                            runat="server" CausesValidation="false" CommandName="finished" Text="完成打款"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                       <%--  <div class="asp_page">--%>
                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" PageSize="10" FirstPageText="首页"
                            LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" SubmitButtonText="Go"
                            TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                            NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" CurrentPageButtonClass="currentButton"
                            PageIndexBoxClass="listPageText" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center"
                            MoreButtonType="Image" NavigationButtonType="Image" OnPageChanged="AspNetPager1_PageChanged">
                        </webdiyer:AspNetPager>
                    <%--</div>--%>
                        <%--  <asp:Panel CssClass="gridviewBottom" runat="server" ID="PanelPage">--%>
                  <%--  </asp:Panel>--%>
                   
                    <%--   </asp:Panel>--%>
                </div>
                <asp:Panel ID="Panel_Operate" runat="server" CssClass="panelSyle">
                    <table>
                        <tr>
                            <th>历史备注：
                            </th>
                            <td style="width: 230px; border: 1px solid black">
                                <asp:Label ID="TextBox_HistoryNote" runat="server" Text=""></asp:Label>
                                <%--<asp:TextBox ID="TextBox_HistoryNote" runat="server" Enabled="false" TextMode="MultiLine"
                                Height="100px" Width="230px"></asp:TextBox>--%>
                            </td>
                        </tr>
                        <tr>
                            <th>新增备注：
                            </th>
                            <td>
                                <asp:TextBox ID="TextBox_Note" runat="server" TextMode="MultiLine" Width="230px"
                                    Height="100px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>&nbsp;
                            </th>
                            <td>
                                <asp:Button ID="Button_confirm" runat="server" Text="确    定" CssClass="button" OnClick="Button_confirm_Click" />&nbsp;&nbsp;
                            <asp:Button ID="Button_cancel" runat="server" Text="取    消" CssClass="button" OnClientClick="HiddenConfirmWindow('Panel_Operate')" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </div>
        <asp:HiddenField ID="HiddenField_Id" runat="server" />
        <asp:HiddenField ID="HiddenField_ConnId" runat="server" />
    </form>
</body>
</html>
