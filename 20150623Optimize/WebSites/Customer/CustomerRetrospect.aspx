<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerRetrospect.aspx.cs"
    Inherits="Customer_CustomerRetrospect" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>客户追溯</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui-1.10.4.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <style type="text/css">
        .ui-widget-header {
            display: none;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("gdvCustomerList", "gv_OverRow");
            GridViewStyle("gdvPaidOrder", "gv_OverRow");
        });
    </script>
</head>
<body scroll="no" style="overflow-y: hidden;" onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <form id="form1" runat="server">
        <uc2:HeadControl ID="HeadControl1" runat="server" headName="客户追溯" navigationImage="~/images/icon/list.gif"
            navigationText="" navigationUrl="" />
        <div id="box" class="box">
            <div class="tagMenu">
                <ul class="menu">
                    <li>客户追溯</li>
                </ul>
            </div>
            <div class="content">
                <div class="layout">
                    <div class="QueryTerms">
                        <table style="width: 100%">
                            <tr>
                                <td>用户手机：<asp:TextBox ID="TextBox_mobilePhoneNumber" runat="server" Width="200px" Height="24px"></asp:TextBox>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnQuery" runat="server" Text="查   询" CssClass="button" OnClick="btnQuery_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="div_gridview" id="grid-view">
                        <asp:GridView runat="server" ID="gdvCustomerList" AutoGenerateColumns="False" CssClass="gridview"
                            DataKeyNames="CustomerID" SkinID="gridviewSkin">
                            <Columns>
                                <asp:BoundField DataField="UserName" HeaderText="昵称" />
                                <asp:BoundField DataField="mobilePhoneNumber" HeaderText="电话号码" />
                                <asp:BoundField DataField="RegisterDate" HeaderText="注册时间" />
                                <asp:BoundField DataField="money19dianRemained" HeaderText="账户余额" />
                                <%--          <asp:BoundField DataField="executedRedEnvelopeAmount" HeaderText="已生效红包金额" />
                                <asp:BoundField DataField="notExecutedRedEnvelopeAmount" HeaderText="未生效红包金额" />--%>
                                <asp:TemplateField HeaderText="已生效红包金额">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="executedRedEnvelopeAmount"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="未生效红包金额">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="notExecutedRedEnvelopeAmount"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>           
                                <asp:BoundField DataField="ordersCount" HeaderText="消费订单数" />
                                <asp:BoundField DataField="ordersAmount" HeaderText="消费总金额" />
                              <%-- <asp:TemplateField>
                                <ItemTemplate>
                                    <img src="../Images/key_edit2.gif" alt="" />
                                    <a data-effect="mfp-zoom-in" href="#sum-layout" class="sum">调整余额</a>
                                    <asp:HiddenField runat="server" ID="hiddenCookie" Value='<%# DataBinder.Eval(Container.DataItem,"cookie") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <asp:HiddenField ID="hidCookie" runat="server" />
                    <br />
                    <div class="div_gridview">
                        <asp:GridView runat="server" ID="gdvPaidOrder" AutoGenerateColumns="False" CssClass="gridview"
                            DataKeyNames="preOrder19dianId,prePaidSum,status"
                            SkinID="gridviewSkin">
                            <Columns>
                                <asp:TemplateField HeaderText="行号">
                                    <ItemTemplate>
                                        <%# (this.AspNetPager2.CurrentPageIndex - 1) * this.AspNetPager2.PageSize + Container.DataItemIndex + 1%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="preOrder19dianId" HeaderText="流水号" />
                                <asp:BoundField DataField="shopName" HeaderText="店铺" />
                                <asp:BoundField DataField="prePaidSum" HeaderText="支付总额" />
                                <asp:BoundField DataField="refundMoneySum" HeaderText="已退金额" />
                                <asp:BoundField DataField="prePayTime" HeaderText="支付时间" />
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        支付方式详情
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="payModeDetail" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="isShopConfirmed" HeaderText="是否已入座" />
                                <asp:BoundField DataField="isApproved" HeaderText="是否已对账" />
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        订单状态
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="status" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <div class="asp_page">
                            <webdiyer:AspNetPager ID="AspNetPager2" runat="server" FirstPageText="首页" LastPageText="尾页"
                                NextPageText="下一页" PageSize="5" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                                TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                                NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" PageIndexBoxClass="listPageText"
                                CurrentPageButtonClass="currentButton" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center"
                                OnPageChanged="AspNetPager2_PageChanged">
                            </webdiyer:AspNetPager>
                        </div>
                    </div>
                    <br />
                    <div class="div_gridview">
                        <asp:GridView runat="server" ID="gdvReEnvelopeDetail" AutoGenerateColumns="False"
                            CssClass="gridview" SkinID="gridviewSkin" DataKeyNames="isExecuted,expireTime"
                            OnDataBound="gdvReEnvelopeDetail_DataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="行号">
                                    <ItemTemplate>
                                        <%# (this.AspNetPager3.CurrentPageIndex - 1) * this.AspNetPager3.PageSize + Container.DataItemIndex + 1%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="treasureChestId" HeaderText="宝箱编号" />
                            <asp:BoundField DataField="ownerPhone" HeaderText="宝箱主人" />
                            <asp:BoundField DataField="treasureChestAmout" HeaderText="宝箱金额" />
                            <asp:BoundField DataField="count" HeaderText="宝箱内红包个数" />--%>
                                <asp:BoundField DataField="activityId" HeaderText="活动编号" />
                                <asp:BoundField DataField="redEnvelopeId" HeaderText="红包编号" />
                                <asp:BoundField DataField="redEnvelopeAmount" HeaderText="红包金额" />
                                <asp:BoundField DataField="isExecuted" HeaderText="红包状态" />
                                <asp:BoundField DataField="unusedAmount" HeaderText="未使用金额" />
                                <asp:BoundField DataField="getTime" HeaderText="领取时间" />
                                <asp:BoundField DataField="expireTime" HeaderText="过期时间" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lkbDetail" runat="server" OnCommand="lkbDetail_OnCommand" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"redEnvelopeId") %>'
                                            CommandName="detail">使用详情</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <div class="asp_page">
                            <webdiyer:AspNetPager ID="AspNetPager3" runat="server" FirstPageText="首页" LastPageText="尾页"
                                NextPageText="下一页" PageSize="5" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                                TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                                NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" PageIndexBoxClass="listPageText"
                                CurrentPageButtonClass="currentButton" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center"
                                OnPageChanged="AspNetPager3_PageChanged">
                            </webdiyer:AspNetPager>
                        </div>
                    </div>
                    <br />
                    <div class="div_gridview">
                        <asp:GridView runat="server" ID="gdvRedEnvelopeConn" AutoGenerateColumns="False"
                            CssClass="gridview" SkinID="gridviewSkin">
                            <Columns>
                                <asp:TemplateField HeaderText="行号">
                                    <ItemTemplate>
                                        <%# (this.AspNetPager4.CurrentPageIndex - 1) * this.AspNetPager4.PageSize + Container.DataItemIndex + 1%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="redEnvelopeId" HeaderText="红包编号" />
                                <asp:BoundField DataField="preOrder19dianId" HeaderText="点单流水号" />
                                <asp:BoundField DataField="shopName" HeaderText="店铺名称" />
                                <asp:BoundField DataField="prePaidSum" HeaderText="支付金额" />
                                <asp:BoundField DataField="prePayTime" HeaderText="支付时间" />
                                <asp:BoundField DataField="refundMoneySum" HeaderText="退款金额" />
                                <asp:BoundField DataField="currectUsedAmount" HeaderText="红包支付金额" />
                            </Columns>
                        </asp:GridView>
                        <div class="asp_page">
                            <webdiyer:AspNetPager ID="AspNetPager4" runat="server" FirstPageText="首页" LastPageText="尾页"
                                NextPageText="下一页" PageSize="5" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                                TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                                NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" PageIndexBoxClass="listPageText"
                                CurrentPageButtonClass="currentButton" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center"
                                OnPageChanged="AspNetPager3_PageChanged">
                            </webdiyer:AspNetPager>
                        </div>
                    </div>
                </div>
                <%--  <div id="sum-layout" class="sum-layout">
                <h3 class="headerItem">
                    调整余额</h3>
                <div class="text">
                    <p class="configMoney">
                        <label for="configMoney">
                            金额：<span class="tips">金额不能为0</span></label><input type="text" maxlength="50" class="inputText"
                                name="configMoney"></p>
                    <p class="comment">
                        <span class="txt">注：输入正数增加余额，负数减少余额</span></p>
                    <p class="remark">
                        <label for="remark">
                            原因说明：<span class="tips">原因不能为空</span></label><textarea cols="3" rows="3" class="area"
                                name="remark"></textarea></p>
                    <div class="btnSprite">
                        <a class="btn comfirm" href="javascript:;">确定</a><a class="btn cancel" href="javascript:;">取消</a></div>
                </div>
            </div>--%>
            </div>
        </div>
        <%--<script src="../Scripts/customer.js" type="text/javascript"></script>--%>
        <uc1:CheckUser ID="CheckUser1" runat="server" />
    </form>
</body>
</html>
