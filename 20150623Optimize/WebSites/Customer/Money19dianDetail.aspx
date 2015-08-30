<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Money19dianDetail.aspx.cs"
    Inherits="Customer_Money19dianDetail" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>奖励信息</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            iframeParent("#customerMoney19dianDetail");
            GridViewStyle("GridView_Money19dianDetailList", "gv_OverRow");
        });       
    </script>
</head>
<body>
    <form id="form1" runat="server" onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <div id="box" class="box">
        <div class="content">
            <div class="layout">
                <div class="QueryTerms">
                    <table style="width: 700px; height: 39px;">
                        <tr>
                            <td>
                                起始时间：<asp:TextBox ID="TextBox_startTime" runat="server" Width="160px" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"
                                    CssClass="Wdate"></asp:TextBox>
                            </td>
                            <td>
                                结束时间：<asp:TextBox ID="TextBox_endTime" runat="server" Width="160px" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"
                                    CssClass="Wdate"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="Button_Check" runat="server" Text="查   询" CssClass="button" OnClick="Button_Check_Click" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="div_gridview">
                    <asp:GridView ID="GridView_Money19dianDetailList" runat="server" DataKeyNames="customerId,changeReason"
                        AutoGenerateColumns="False" CssClass="gridview" SkinID="gridviewSkin">
                        <Columns>
                            <asp:TemplateField HeaderText="行号">
                                <ItemTemplate>
                                    <%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="用户名">
                                <ItemTemplate>
                                    <asp:Label ID="Label_customerId" runat="server" Text='<%# Bind("customerId") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="手机号">
                                <ItemTemplate>
                                    <asp:Label ID="Label_mobilePhoneNumber" runat="server" Text='<%# Bind("customerId") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email">
                                <ItemTemplate>
                                    <asp:Label ID="Label_customerEmail" runat="server" Text='<%# Bind("customerId") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="cookie">
                                <ItemTemplate>
                                    <asp:Label ID="Label_cooke" runat="server" Text='<%# Bind("customerId") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="变动原因">
                                <ItemTemplate>
                                    <asp:Label ID="Label_changeReason" runat="server" Text='<%# Bind("changeReason") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="changeValue" HeaderText="变动值" />
                            <asp:BoundField DataField="changeTime" HeaderText="变动时间" />
                        </Columns>
                    </asp:GridView>
                    <div class="asp_page">
                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                            FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PageSize="10" PrevPageText="上一页"
                            SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                            NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" CurrentPageButtonClass="currentButton"
                            PageIndexBoxClass="listPageText" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center">
                        </webdiyer:AspNetPager>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
