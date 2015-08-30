<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShopChannelRequest.aspx.cs" Inherits="OrderOptimization_ShopChannelRequest" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>公司管理</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("GridView_ShopChannelRequest", "gv_OverRow");
            $("#div_gridview").css({ "height": $(window).height() - 200 });
            $("#div_gridview").css({ "overflow": "auto" });
        });
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 82px;
        }
    </style>
</head>
<body scroll="no" style="overflow-y: hidden" onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <form id="form1" runat="server">
        <uc2:HeadControl ID="HeadControl1" runat="server" headName="商户在线申请管理" />
        <div id="box" class="box">
            <div class="tagMenu">
                <ul class="menu">
                    <li>在线申请列表</li>
                </ul>
            </div>
            <div class="content">
                <div class="layout">
                    <div class="QueryTerms">
                        <table>
                            <tr>
                                <th class="auto-style6">&nbsp;&nbsp;
                            城市
                                </th>
                                <td class="auto-style2">
                                    <asp:DropDownList ID="ddlCity" runat="server" Height="40px" Width="100px" AutoPostBack="true" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged"></asp:DropDownList>
                                </td>
                                <%--<td>    商户名称：
                                <asp:TextBox ID="TextBox_MerchantName" runat="server"></asp:TextBox>
                                </td>--%>
                            </tr>
                        </table>
                    </div>
                    <div class="div_gridview" id="div_gridview">
                        <asp:GridView ID="GridView_ShopChannelRequest" runat="server" DataKeyNames="id"
                            AutoGenerateColumns="False" SkinID="gridviewSkin" OnRowCommand="GridView_ShopChannelRequest_RowCommand" OnSelectedIndexChanged="GridView_ShopChannelRequest_SelectedIndexChanged" OnRowDeleting="GridView_ShopChannelRequest_RowDeleting">
                            <Columns>
                                <asp:TemplateField HeaderText="序号">
                                    <ItemTemplate>
                                        <%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="shopID" HeaderText="商户ID" Visible="false" />
                                <asp:BoundField DataField="shopName" HeaderText="商户名称" />
                                <asp:BoundField DataField="address" HeaderText="商户地址" />
                                <asp:BoundField DataField="createTime" HeaderText="时间" />
                                <asp:BoundField DataField="channelName" HeaderText="意愿增值页" />
                                <asp:TemplateField HeaderText="操作">
                                    <ItemTemplate>
                                        &nbsp; &nbsp;&nbsp;<img src="../Images/key_edit3.gif" /><asp:LinkButton ID="LinkButton1"
                                            runat="server" CausesValidation="False" ForeColor="blue" CommandName="delete" Text="处理" OnClientClick="return confirm('你确定操作吗？')"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <label id="lbCount" runat="server"></label>
                        <div class="asp_page">
                            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                                FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PageSize="10" PrevPageText="上一页"
                                SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                                NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" PageIndexBoxClass="listPageText"
                                CurrentPageButtonClass="currentButton" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center">
                            </webdiyer:AspNetPager>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <uc1:CheckUser ID="CheckUser1" runat="server" />
    </form>
</body>
</html>
