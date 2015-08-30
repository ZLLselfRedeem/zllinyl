<%@ Page Language="C#" AutoEventWireup="true" CodeFile="foodPlazaMaintain.aspx.cs" Inherits="SystemConfig_foodPlazaMaintain" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>美食广场维护</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("grFoodPlaza", "gv_OverRow");
        });
    </script>
</head>
<body onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <form id="form1" runat="server">
        <uc2:HeadControl ID="HeadControl1" runat="server" headName="美食广场维护" navigationImage="~/images/icon/new.gif"
            navigationText="" navigationUrl="" />
        <div id="box" class="box">
            <div class="tagMenu">
                <ul class="menu">
                    <li>美食广场维护</li>
                </ul>
            </div>
            <div class="content">
                <div class="layout">
                    <div class="QueryTerms">
                        <table>
                            <tr>
                                <td>城市名称：<asp:DropDownList ID="ddlCity" runat="server">
                                    <asp:ListItem Value="87" Selected="True">杭州市</asp:ListItem>
                                    <asp:ListItem Value="1">北京市</asp:ListItem>
                                    <asp:ListItem Value="73">上海市</asp:ListItem>
                                    <asp:ListItem Value="179">广州市</asp:ListItem>
                                    <asp:ListItem Value="199">深圳市</asp:ListItem>
                                </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>时间：<asp:TextBox ID="txtStarTime" runat="server" class="Wdate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"
                                    Style="width: 140px"></asp:TextBox>
                                    &nbsp;-&nbsp;
                                    <asp:TextBox ID="txtEndTime" class="Wdate" runat="server" Style="width: 140px"
                                        onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                    &nbsp;
                                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="查 询" OnClick="btnSearch_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="div_gridview" id="div_gridview">
                        <asp:GridView ID="grFoodPlaza" runat="server" DataKeyNames="dishImgs,preOrder19dianId,foodPlazaId,personImgUrl"
                            AutoGenerateColumns="False" SkinID="gridviewSkin" OnRowCommand="grFoodPlaza_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="shopName" HeaderText="门店" HeaderStyle-Width="100px" />
                                <asp:TemplateField ShowHeader="False" HeaderStyle-Width="80px">
                                    <HeaderTemplate>
                                        用户头像
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Image ID="imgPerson" runat="server" Height="80px" ImageUrl="~/Images/smallimage.jpg" Width="80px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False">
                                    <HeaderTemplate>
                                        菜品图片
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Repeater ID="rptDishImg" runat="server">
                                            <HeaderTemplate>
                                                <table>
                                                    <tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <td style="border: 1px solid gray;">
                                                    <asp:Image ID="imgDish" runat="server" Height="100px" ImageUrl="~/Images/smallimage.jpg" Width="100px" />
                                                </td>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tr>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <img src="../Images/key_edit3.gif" />&nbsp;<asp:LinkButton ID="lnkListTop"
                                            runat="server" CausesValidation="False" CommandName="ListTop" Text="置顶"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False" HeaderStyle-Width="80px">
                                    <ItemTemplate>
                                        <img src="../Images/key_edit3.gif" />&nbsp;<asp:LinkButton ID="lnkUpdate"
                                            runat="server" CausesValidation="False" CommandName="UpdateData" Text="更新"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="80px">
                                    <ItemTemplate>
                                        <img src="../Images/key_edit3.gif" />&nbsp;<asp:LinkButton ID="lnkDelete"
                                            runat="server" CausesValidation="false" CommandName="DeleteData" Text="删除"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <div class="asp_page">
                            <webdiyer:AspNetPager ID="AspNetPager1" runat="server"
                                FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PageSize="10" PrevPageText="上一页"
                                SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                                NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" PageIndexBoxClass="listPageText"
                                CurrentPageButtonClass="currentButton" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center" OnPageChanged="AspNetPager1_PageChanged">
                            </webdiyer:AspNetPager>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <uc1:CheckUser ID="CheckUser1" runat="server" />
    </form>
    <br />
</body>
</html>


