<%@ Page Language="C#" AutoEventWireup="true" CodeFile="shopWeChatConfig.aspx.cs"
    Inherits="ShopManage_shopWeChatConfig" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" width="80px">
    <title>打款列表</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            GridViewStyle("GridView_ShopWechatConfig", "gv_OverRow");
        });  
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <uc3:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
        navigationText="返回" navigationUrl="" headName="门店微信点菜Url配置" />
    <div id="box" class="box">
        <div class="content">
            <div class="layout">
                <div class="QueryTerms">
                    <table>
                        <tr>
                            <td>
                                公司：<asp:DropDownList ID="DropDownList_Company" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="DropDownList_Company_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                门店：<asp:DropDownList ID="DropDownList_Shop" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_Shop_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                <asp:Button ID="btn_Query" runat="server" Text="生成链接" Width="100px" OnClick="btn_Query_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:Panel ID="Panel_ShopWechatConfig" runat="server" CssClass="div_gridview">
                    <asp:GridView ID="GridView_ShopWechatConfig" runat="server" DataKeyNames="id,status"
                        AutoGenerateColumns="False" SkinID="gridviewSkin" OnRowCommand="GridView_ShopWechatConfig_RowCommand"
                        Style="margin-right: 0px">
                        <Columns>
                            <asp:BoundField DataField="shopName" HeaderText="门店名称" />
                            <asp:BoundField DataField="cookie" HeaderText="门店cookie" />
                            <asp:BoundField DataField="createdTime" HeaderText="创建时间" />
                            <asp:BoundField DataField="wechatOrderUrl" HeaderText="微信公共帐号点菜Url" />
                            <asp:TemplateField HeaderText="状态">
                                <ItemTemplate>
                                    <img src="../Images/key_edit3.gif" />&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1"
                                        runat="server" CausesValidation="False" CommandName="SetIsValid" Text=""></asp:LinkButton>
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
            </div>
        </div>
    </div>
    </form>
</body>
</html>
