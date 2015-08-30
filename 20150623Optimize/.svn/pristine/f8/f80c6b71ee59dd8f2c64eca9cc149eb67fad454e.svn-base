<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RecommendRestaurant.aspx.cs" Inherits="WeChatPlatManage_RecommendRestaurant" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>微信后台管理</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            GridViewStyle("GridView_ShopInfo", "gv_OverRow");
            StatusFun();
            TabManage();
            var headControlHeight = $("#headControl").height();
            var tagMenuHeight = $("div.tagMenu").height();
            var layoutHeight = $(window).height() - headControlHeight - tagMenuHeight - 210;
            $("#divIframeContent").css({ "height": layoutHeight + 10 });
            $("#mainFrame").height(layoutHeight);

        });   
    </script>
</head>
<body onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <form id="form1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
        <ContentTemplate>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <!-- 头部菜单 Start -->
            <uc3:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
                navigationText="推荐餐厅" navigationUrl="~/WeChatPlatManage/RecommendRestaurant.aspx"
                headName="微信平台-推荐餐厅" />
            <!-- 头部菜单 end -->
            <div id="box" class="box" style="height:720px;overflow-y:scroll;">
                <div class="tagMenu">
                    <ul class="menu">
                        <li style="border: 1px solid #999;border-bottom: none;background: #fff;height: 25px;line-height: 26px;margin: 0;">推荐餐厅</li>
                    </ul>
                </div>
                <div class="content">
                    <div class="layout">
                        <div class="QueryTerms">
                            <table cellspacing="5">
                                <tr>
                                    <td>
                                        餐厅名称:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox_ResturantName" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label_Error" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Button ID="Button_QueryByResturantName" runat="server" Text="查     询" CssClass="tabButtonBlueClick"
                                            Width="100px" OnClick="Button_QueryByResturantName_Click" />
                                    </td>
                                </tr>
                            </table>
                            <hr size="1" style="border: 1px #cccccc dashed;" />
                            <table>
                                <tr>
                                    <td>
                                        菜单子项：
                                    </td>
                                    <td>
                                        <asp:Button ID="Button_Hangzhou" runat="server" CssClass="tabButtonBlueUnClick" CommandName="Hangzhou"
                                            OnClick="Button_City_Click" Text="杭州"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:Button ID="Button_Beijing" runat="server" CssClass="tabButtonBlueUnClick" CommandName="Beijing"
                                            OnClick="Button_City_Click" Text="北京"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:Button ID="Button_Shanghai" runat="server" CssClass="tabButtonBlueUnClick" CommandName="Shanghai"
                                            OnClick="Button_City_Click" Text="上海"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:Button ID="Button_Guangzhou" runat="server" CssClass="tabButtonBlueUnClick"
                                            CommandName="Guangzhou" OnClick="Button_City_Click" Text="广州"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:Button ID="Button_Shenzhen" runat="server" CssClass="tabButtonBlueUnClick" CommandName="Shenzhen"
                                            OnClick="Button_City_Click" Text="深圳"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:HiddenField ID="cityName" Value="" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:Panel ID="Panel_ShopInfo" runat="server" CssClass="div_gridview">
                            <asp:GridView ID="GridView_ShopInfo" runat="server" DataKeyNames="shopID,shopName,shopAddress,shopTelephone,shopLogo"
                                AutoGenerateColumns="False" SkinID="gridviewSkin" 
                                ondatabound="GridView_ShopInfo_DataBound" 
                                onrowdatabound="GridView_ShopInfo_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="shopID" HeaderText="店铺ID" />
                                    <asp:BoundField DataField="shopName" HeaderText="店铺名称" />
                                    <asp:BoundField DataField="shopAddress" HeaderText="地址" />
                                    <asp:BoundField DataField="shopTelephone" HeaderText="联系电话" />
                                    <asp:BoundField DataField="shopLogo" HeaderText="店铺Logo" />
                                    <asp:TemplateField ShowHeader="False" HeaderText="推荐餐厅设置">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="DropDown_SetRecommand" runat="server" OnSelectedIndexChanged="DropDown_SetRecommand_SelectedIndexChanged"
                                                AutoPostBack="True">
                                                <asp:ListItem Text="未设置" Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="设为主推荐餐厅" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="设为次推荐餐厅" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="设为特色餐厅" Value="2"></asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:Panel CssClass="gridviewBottom" runat="server" ID="PanelPage" Visible="true">
                                <div class="asp_page">
                                    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="liPage" CurrentPageButtonClass="currentButton"
                                        CurrentPageButtonPosition="Center" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                                        FirstPageText="首页" LastPageText="尾页" MoreButtonType="Image" NavigationButtonType="Image"
                                        NextPageText="下一页" NumericButtonType="Text" OnPageChanged="AspNetPager1_PageChanged"
                                        PageIndexBoxClass="listPageText" PageSize="5" PrevPageText="上一页" ShowPageIndexBox="Always"
                                        SubmitButtonClass="listPageBtn" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                                        TextBeforePageIndexBox="转到">
                                    </webdiyer:AspNetPager>
                                </div>
                            </asp:Panel>
                        </asp:Panel>
                        <asp:Panel ID="Panel_Detail" runat="server" CssClass="div_gridview" ScrollBars="Auto">
                            <asp:GridView ID="GridView1" runat="server" DataKeyNames="shopID,shopName,shopAddress,shopTelephone"
                                AutoGenerateColumns="False" SkinID="gridviewSkin" 
                                OnRowDeleting="GridView1_RowDeleting" ondatabound="GridView1_DataBound">
                                <Columns>
                                    <asp:BoundField DataField="field" HeaderText="地区" />
                                    <asp:BoundField DataField="shopID" HeaderText="店铺ID" />
                                    <asp:BoundField DataField="shopName" HeaderText="店铺名称" />
                                    <asp:BoundField DataField="shopAddress" HeaderText="地址" />
                                    <asp:BoundField DataField="shopTelephone" HeaderText="联系电话" />
                                    <asp:BoundField DataField="recommandTypeName" HeaderText="推荐类型" />
                                    <%--<asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <img src="../Images/key_edit3.gif" alt="" />&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1"
                                                runat="server" CausesValidation="False" CommandName="infoSet" Text="图文信息"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <img src="../Images/delete.gif" alt="" />&nbsp;&nbsp;<asp:LinkButton ID="LinkButton2"
                                                runat="server" CausesValidation="False" CommandName="delete" Text="删除" OnClientClick="return confirm('你确定删除吗？')"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <div class="divDetailTitle" style="overflow-y: hidden">
                                <input id="Hidden1" type="hidden" />
                                <asp:Button ID="Button_Save" runat="server" Text="保存设置" CssClass="couponButtonSubmit"
                                    OnClientClick="return confirm('你确定操作吗?')" OnClick="Button_Save_Click" />
                                <asp:Label ID="Label_massage" runat="server" CssClass="Red" Text=""></asp:Label>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
            <uc1:CheckUser ID="CheckUser1" runat="server" />
        </ContentTemplate>
        
        <Triggers></Triggers>
    </asp:UpdatePanel>
    
    <!-- 主编辑区 -->
    </form>
    <script type="text/javascript">
        function StatusFun() {
            var a = document.getElementById("Hidden1");
            if (a != null) {
                //不为空
                $("body").attr("scroll", "no").attr("overflow-y", "hidden");
            }
            else {
                //为空
                $("body").attr("scroll", "auto");
            }
        }
    </script>
</body>
</html>
