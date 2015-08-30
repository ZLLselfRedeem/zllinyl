<%@ Page Language="C#" AutoEventWireup="true" CodeFile="redRedEnvelopeListDetail.aspx.cs" Inherits="RedEnvelope_redRedEnvelopeListDetail" %>

<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager, Version=7.0.2.0, Culture=neutral, PublicKeyToken=fb0a0fe055d40fd4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>红包活动列表</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("gdvActivity", "gv_OverRow");
            GridViewStyle("gdvTreasureChest", "gv_OverRow");
            GridViewStyle("gdvRedEnvelope", "gv_OverRow");
            GridViewStyle("gdvOrderDetail", "gv_OverRow");
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <%-- title--%>
        <div>
            <uc2:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
                navigationText="" navigationUrl="" headName="红包活动列表" />
        </div>
        <div id="box" class="box">
            <div class="content">
                <div class="layout">
                    <%-- 头部菜单--%>
                    <div class="QueryTerms">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 50%">
                                    <label>活动名称：</label>
                                    <asp:DropDownList runat="server" ID="ddlActivityName"></asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btn_Search" runat="server" Text="搜 索" CssClass="button" OnClick="btn_Search_Click" />
                                </td>
                                <td style="width: 50%; text-align: right">
                                    <asp:Button ID="btn_Back" runat="server" Text="返 回" CssClass="button" OnClick="btn_Back_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                    <%-- 活动列表--%>
                    <asp:Panel ID="panel_ActivityList" runat="server" CssClass="gridview">
                        <div class="div_gridview">
                            <asp:GridView runat="server" ID="gdvActivity" AutoGenerateColumns="False" CssClass="gridview"
                                SkinID="gridviewSkin" DataKeyNames="activityId" OnSelectedIndexChanged="gdvActivity_SelectedIndexChanged">
                                <Columns>
                                    <asp:TemplateField HeaderText="行号">
                                        <ItemTemplate>
                                            <%# (this.AspNetPager_Activity.CurrentPageIndex - 1) * this.AspNetPager_Activity.PageSize + Container.DataItemIndex + 1%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="activityId" HeaderText="活动ID" Visible="false" />
                                    <asp:BoundField DataField="name" HeaderText="活动名称" />
                                    <asp:BoundField DataField="amount" HeaderText="总金额" />
                                    <asp:BoundField DataField="createTime" HeaderText="创建时间" />
                                    <asp:BoundField DataField="beginTime" HeaderText="开始时间" />
                                    <asp:BoundField DataField="endTime" HeaderText="结束时间" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkbtnSelect" CssClass="linkButtonDetail" Text="查看宝箱" ForeColor="Blue" CommandName="Select"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:Panel ID="panel_AspNetPager_Activity" CssClass="gridviewBottom" runat="server">
                                <div>
                                    <webdiyer:AspNetPager ID="AspNetPager_Activity" runat="server" FirstPageText="首页" LastPageText="尾页"
                                        NextPageText="下一页" PageSize="10" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                                        TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                                        NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" PageIndexBoxClass="listPageText"
                                        CurrentPageButtonClass="currentButton" ShowPageIndexBox="Always" currentpagebuttonposition="Center">
                                    </webdiyer:AspNetPager>
                                </div>
                            </asp:Panel>
                        </div>
                    </asp:Panel>
                    <%-- 宝箱列表--%>
                    <asp:Panel ID="panel_TreasureChestList" runat="server" CssClass="gridview">
                        <div class="div_gridview">
                            <asp:GridView runat="server" ID="gdvTreasureChest" AutoGenerateColumns="False" CssClass="gridview"
                                SkinID="gridviewSkin" DataKeyNames="treasureChestId" OnSelectedIndexChanged="gdvTreasureChest_SelectedIndexChanged">
                                <Columns>
                                    <asp:TemplateField HeaderText="行号">
                                        <ItemTemplate>
                                            <%# (this.AspNetPager_TreasureChest.CurrentPageIndex - 1) * this.AspNetPager_TreasureChest.PageSize + Container.DataItemIndex + 1%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="treasureChestId" HeaderText="宝箱ID" Visible="false" />
                                    <asp:BoundField DataField="mobilePhoneNumber" HeaderText="创建人手机号码" />
                                    <asp:BoundField DataField="createTime" HeaderText="创建时间" />
                                    <asp:BoundField DataField="executedTime" HeaderText="生效时间" />
                                    <asp:BoundField DataField="expireTime" HeaderText="过期时间" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkbtnSelect" CssClass="linkButtonDetail" Text="查看红包" ForeColor="Blue" CommandName="Select"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:Panel ID="panel_AspNetPager_TreasureChest" CssClass="gridviewBottom" runat="server">
                                <div>
                                    <webdiyer:AspNetPager ID="AspNetPager_TreasureChest" runat="server" FirstPageText="首页" LastPageText="尾页"
                                        NextPageText="下一页" PageSize="10" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                                        TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                                        NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" PageIndexBoxClass="listPageText"
                                        CurrentPageButtonClass="currentButton" ShowPageIndexBox="Always" currentpagebuttonposition="Center">
                                    </webdiyer:AspNetPager>
                                </div>
                            </asp:Panel>
                        </div>
                    </asp:Panel>
                    <%-- 红包列表--%>
                    <asp:Panel ID="panel_RedEnvelopeList" runat="server" CssClass=" gridview">
                        <div class="div_gridview">
                            <asp:GridView runat="server" ID="gdvRedEnvelope" AutoGenerateColumns="False" CssClass="gridview"
                                SkinID="gridviewSkin" DataKeyNames="enable" OnSelectedIndexChanged="gdvRedEnvelope_SelectedIndexChanged">
                                <Columns>
                                    <asp:BoundField DataField="UserName" HeaderText="用户姓名" />
                                    <asp:BoundField DataField="mobilePhoneNumber" HeaderText="手机号码" />
                                    <asp:BoundField DataField="stateType" HeaderText="红包状态" />
                                    <asp:BoundField DataField="Amount" HeaderText="红包金额" />
                                    <asp:BoundField DataField="getTime" HeaderText="领取时间" />
                                    <asp:BoundField DataField="isUnlock" HeaderText="是否已解锁" />
                                    <asp:BoundField DataField="redEnvelopeId" HeaderText="" Visible="False" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkbtnSelectOrder" CssClass="linkButtonDetail" Text="查看点单" ForeColor="Blue" CommandName="Select"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </asp:Panel>
                    <%-- 点单详情--%>
                    <asp:Panel ID="panel_OrderDetail" runat="server" CssClass="panelSyle">
                        <asp:GridView ID="gdvOrderDetail" runat="server" DataKeyNames=""
                            AutoGenerateColumns="False" SkinID="gridviewSkin">
                            <Columns>
                                <asp:BoundField DataField="prePayTime" HeaderText="支付时间" />
                                <asp:BoundField DataField="prePaidSum" HeaderText="支付金额" />
                                <asp:BoundField DataField="refundMoneySum" HeaderText="退款金额" />
                            </Columns>
                        </asp:GridView>
                        <div style="width: 100%; text-align: right">
                            <br />
                            <asp:Button ID="Button_cancel" runat="server" Text="退  出" CssClass="button" OnClientClick="HiddenConfirmWindow('Panel_Detail')" />
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
        <%--<uc1:CheckUser ID="CheckUser1" runat="server" />--%>
    </form>
</body>
</html>
