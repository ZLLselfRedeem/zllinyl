<%@ Page Language="C#" AutoEventWireup="true" CodeFile="foodPlazaConfig.aspx.cs" Inherits="SystemConfig_foodPlazaConfig" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>美食广场配置</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script src="../Scripts/searchShop.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            initData("foodPlazaConfigCheckShop");
            TabManage();
            //GridViewStyle("grFoodPlaza", "gv_OverRow");
        });
        function clearOper() {
            if ($("#checkShop").val() == '') {
                $("#flag").val('true');
            }
            else {
                $("#flag").val('false');
            }
        }
    </script>
    <style type="text/css">
        li:hover {
            text-decoration: underline;
            color: #39c;
            cursor: pointer;
        }
    </style>
</head>
<body onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <form id="form1" runat="server">
        <uc2:HeadControl ID="HeadControl1" runat="server" headName="美食广场配置" navigationImage="~/images/icon/new.gif"
            navigationText="" navigationUrl="" />
        <div id="box" class="box">
            <div class="tagMenu">
                <ul class="menu">
                    <li>美食广场配置</li>
                </ul>
            </div>
            <div class="content">
                <div class="layout">
                    <div class="QueryTerms">
                        <table>
                            <tr>
                                <td>城市名称：<asp:DropDownList ID="ddlCity" AutoPostBack="True" runat="server" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged">
                                    <asp:ListItem Value="87" Selected="True">杭州市</asp:ListItem>
                                    <asp:ListItem Value="1">北京市</asp:ListItem>
                                    <asp:ListItem Value="73">上海市</asp:ListItem>
                                    <asp:ListItem Value="179">广州市</asp:ListItem>
                                    <asp:ListItem Value="199">深圳市</asp:ListItem>
                                </asp:DropDownList>&nbsp;
                                    <table>
                                        <tr>
                                            <td>门店：
                                            </td>
                                            <td>
                                                <input id="checkShop" runat="server" type="text" onkeyup="foodPlazaConfigCheckShop()" />
                                                <div id="shopList" runat="server" style="position: absolute; clear: both; border: 1px solid #000; background-color: White">
                                                </div>
                                                (输入搜索选择)</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>时间：<asp:TextBox ID="txtStarTime" runat="server" class="Wdate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"
                                    Style="width: 140px"></asp:TextBox>
                                    &nbsp;-&nbsp;
                                    <asp:TextBox ID="txtEndTime" class="Wdate" runat="server" Style="width: 140px"
                                        onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                </td>
                                <td>金额：<asp:TextBox ID="txtOrderMount" runat="server"></asp:TextBox>&nbsp;
                                    支付状态：<asp:DropDownList ID="ddlIsPay" runat="server">
                                        <asp:ListItem Value="1" Selected="True">已支付</asp:ListItem>
                                        <asp:ListItem Value="0">未支付</asp:ListItem>
                                    </asp:DropDownList>&nbsp;
                                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="查 询" OnClientClick="clearOper()" OnClick="btnSearch_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="div_gridview" id="div_gridview">
                        <asp:GridView ID="grFoodPlaza" runat="server" DataKeyNames="personImgUrl,dishImgs,shopName,preOrder19dianId,preOrderSum,customerId,shopId,customerName,cityId"
                            AutoGenerateColumns="False" SkinID="gridviewSkin" OnSelectedIndexChanged="grFoodPlaza_SelectedIndexChanged">
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
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Image ID="imgDish" runat="server" Height="100px" ImageUrl="~/Images/smallimage.jpg" Width="100px" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: right">
                                                                <asp:CheckBox ID="cbDish" runat="server" />
                                                                <asp:HiddenField ID="hidden_cbDish" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tr></table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False" HeaderStyle-Width="80px">
                                    <ItemTemplate>
                                        <img src="../Images/key_edit3.gif" />&nbsp;&nbsp;<asp:LinkButton ID="lnkPublish"
                                            runat="server" CausesValidation="False" CommandName="Select" Text="发布"></asp:LinkButton>
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
        <asp:HiddenField ID="hiddenConfrim" runat="server" />
        <uc1:CheckUser ID="CheckUser1" runat="server" />
        <asp:HiddenField ID="flag" runat="server" />
    </form>
    <br />
</body>
</html>

