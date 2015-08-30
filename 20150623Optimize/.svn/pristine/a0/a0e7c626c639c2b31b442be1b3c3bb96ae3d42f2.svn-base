<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MerchantActivityCount.aspx.cs" Inherits="Award_MerchantActivityCount" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>商户活动统计</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("GridView_Employee", "gv_OverRow");
            var div_Height = $("#QueryTerms").height();
            $("#div_content_left").css({ "height": $(window).height() - 180 - div_Height });
            $("#div_content_right").css({ "height": $(window).height() - 180 - div_Height });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server" onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <uc2:HeadControl ID="HeadControl1" runat="server" headName="商户活动统计" navigationImage="~/images/icon/list.gif"
        navigationText="商户活动统计" navigationUrl="" />
    <div id="box" class="box">
        <div class="tagMenu">
            <ul class="menu">
                <li>商户活动统计</li>
            </ul>
        </div>
        <div class="content">
            <div class="layout">
                <table class="order_main" cellpadding="0" cellspacing="0">
                    <tr>
                        <th class="xian" style="width: 50%; text-align: left; height: 5px;">
                            <div class="QueryTerms">
                                <table>
                                    <tr>
                                        <td>
                                            城市:
                                            <asp:DropDownList ID="DropDownListCity" runat="server" Width="150px">
                                                <asp:ListItem Value="">所有</asp:ListItem>
                                                <asp:ListItem Value="87">杭州</asp:ListItem>
                                                <asp:ListItem Value="73">上海</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            门店名：
                                            <asp:TextBox ID="txtShopName" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td >
                                            活动时间：从
                                            <asp:TextBox ID="beginTime" runat="server" CssClass="Wdate" onfocus="WdatePicker({readOnly:true,dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                            到
                                            <asp:TextBox ID="endTime" runat="server" CssClass="Wdate" onfocus="WdatePicker({readOnly:true,dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp;<asp:Button ID="btnSearch" runat="server" CssClass="button" Text="查 询" OnClick="btnSearch_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </th>
                    </tr>
                    <tr>
                        <td>
                            <div id="div_content_left" class="div_content">
                                <asp:GridView ID="GridView_AwardTotal" runat="server" DataKeyNames="companyName,shopName,orderMoney,orderCount,avoidQueueCount,presentDishCount,redCount,thirdCount,noAwardCount"
                                    AutoGenerateColumns="False" 
                                    SkinID="gridviewSkin" >
                                    <Columns>
                                        <asp:TemplateField HeaderText="行号">
                                            <ItemTemplate>
                                                <%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="companyName" DataFormatString="" HeaderText="公司名" />
                                        <asp:BoundField DataField="shopName" HeaderText="门店名" />
                                        <asp:BoundField DataField="orderMoney" HeaderText="订单金额" />
                                        <asp:BoundField DataField="orderCount" HeaderText="订单量" />
                                        <asp:BoundField DataField="avoidQueueCount" HeaderText="中【免排队】" />
                                        <asp:BoundField DataField="presentDishCount" HeaderText="中【赠菜】" />
                                        <asp:BoundField DataField="redCount" HeaderText="中【返现】" />
                                        <asp:BoundField DataField="thirdCount" HeaderText="中【第三方】" />
                                        <asp:BoundField DataField="noAwardCount" HeaderText="未中奖" />
                                    </Columns>
                                </asp:GridView>
                                <div class="asp_page">
                                    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
                                        NextPageText="下一页" PageSize="10" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                                        TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                                        NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" PageIndexBoxClass="listPageText"
                                        CurrentPageButtonClass="currentButton" ShowPageIndexBox="Always" currentpagebuttonposition="Center"
                                        OnPageChanged="AspNetPager1_PageChanged">
                                    </webdiyer:AspNetPager>
                                </div>
                                <asp:Label runat="server" ID="lblAwardInfo">共{0}家门店，订单总金额{1}，订单总量{2}，中[免排队]{3}，中[赠菜]{4}，中[返现]{5}，中[第三方]{6}，未中奖{7}</asp:Label>
                            </div>
                            
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>


