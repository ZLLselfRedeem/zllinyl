<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HotMenu.aspx.cs" Inherits="WeChatPlatManage_HotMenu" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>本期热菜</title>
    <link href="../Css/css.css" rel="Stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:UpdatePanel ID="updatepanel1" runat="server">
    <ContentTemplate>
        <asp:ScriptManager ID="scriptma1" runat="server"></asp:ScriptManager>
    <div class="QueryTerms">
        <table style="width:100%">
            <tr>
                <td align="center" style="width:10%">地区</td>
                <td align="center">热菜top5</td>
            </tr>
            <tr>
                <td colspan="2" style="border-bottom:1px silver dotted;"></td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="label1" Text="杭州" runat="server" CssClass="tabButtonBlueUnClick"></asp:Label>
                </td>
                <td>
                    <asp:GridView ID="GridView_Hangzhou" runat="server" DataKeyNames="DishID,DishName,DishPrice,shopName,shopAddress,saleAmount"
                    AutoGenerateColumns="False" SkinID="gridviewSkin" 
                        ondatabound="GridView_Hangzhou_DataBound" >
                    <Columns>
                        <asp:BoundField DataField="DishID" HeaderText="序号" />
                        <asp:BoundField DataField="DishName" HeaderText="菜名" />
                        <asp:BoundField DataField="DishPrice" HeaderText="价格" />
                        <asp:BoundField DataField="shopName" HeaderText="所在店铺" />
                        <asp:BoundField DataField="shopAddress" HeaderText="店铺地址" />
                        <asp:BoundField DataField="saleAmount" HeaderText="销量" />
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                               <asp:CheckBox ID="chkSet" Text="设为热菜" runat="server" AutoPostBack="True" 
                                    oncheckedchanged="chkSet_CheckedChanged" />
                            </ItemTemplate>
                            <ItemStyle Width="90px" />
                        </asp:TemplateField>
                    </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="border-bottom:1px silver dotted;"></td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="label3" Text="北京" runat="server" CssClass="tabButtonBlueUnClick"></asp:Label>
                </td>
                <td>
                    <asp:GridView ID="GridView_Beijing" runat="server" DataKeyNames="DishID,DishName,DishPrice,shopName,shopAddress,saleAmount"
                    AutoGenerateColumns="False" SkinID="gridviewSkin" 
                        ondatabound="GridView_Beijing_DataBound">
                    <Columns>
                        <asp:BoundField DataField="DishID" HeaderText="序号" />
                        <asp:BoundField DataField="DishName" HeaderText="菜名" />
                        <asp:BoundField DataField="DishPrice" HeaderText="价格" />
                        <asp:BoundField DataField="shopName" HeaderText="所在店铺" />
                        <asp:BoundField DataField="shopAddress" HeaderText="店铺地址" />
                        <asp:BoundField DataField="saleAmount" HeaderText="销量" />
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                               <asp:CheckBox ID="chkSet" Text="设为热菜" runat="server" AutoPostBack="True" 
                                    oncheckedchanged="chkSet_CheckedChanged" />
                            </ItemTemplate>
                            <ItemStyle Width="90px" />
                        </asp:TemplateField>
                    </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="border-bottom:1px silver dotted;"></td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="label4" Text="上海" runat="server" CssClass="tabButtonBlueUnClick"></asp:Label>
                </td>
                <td>
                    <asp:GridView ID="GridView_Shanghai" runat="server" DataKeyNames="DishID,DishName,DishPrice,shopName,shopAddress,saleAmount"
                    AutoGenerateColumns="False" SkinID="gridviewSkin" 
                        ondatabound="GridView_Shanghai_DataBound">
                    <Columns>
                        <asp:BoundField DataField="DishID" HeaderText="序号" />
                        <asp:BoundField DataField="DishName" HeaderText="菜名" />
                        <asp:BoundField DataField="DishPrice" HeaderText="价格" />
                        <asp:BoundField DataField="shopName" HeaderText="所在店铺" />
                        <asp:BoundField DataField="shopAddress" HeaderText="店铺地址" />
                        <asp:BoundField DataField="saleAmount" HeaderText="销量" />
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                               <asp:CheckBox ID="chkSet" Text="设为热菜" runat="server" AutoPostBack="True" 
                                    oncheckedchanged="chkSet_CheckedChanged" />
                            </ItemTemplate>
                            <ItemStyle Width="90px" />
                        </asp:TemplateField>
                    </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="border-bottom:1px silver dotted;"></td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="label5" Text="广州" runat="server" CssClass="tabButtonBlueUnClick"></asp:Label>
                </td>
                <td>
                    <asp:GridView ID="GridView_Guangzhou" runat="server" DataKeyNames="DishID,DishName,DishPrice,shopName,shopAddress,saleAmount"
                    AutoGenerateColumns="False" SkinID="gridviewSkin" 
                        ondatabound="GridView_Guangzhou_DataBound">
                    <Columns>
                        <asp:BoundField DataField="DishID" HeaderText="序号" />
                        <asp:BoundField DataField="DishName" HeaderText="菜名" />
                        <asp:BoundField DataField="DishPrice" HeaderText="价格" />
                        <asp:BoundField DataField="shopName" HeaderText="所在店铺" />
                        <asp:BoundField DataField="shopAddress" HeaderText="店铺地址" />
                        <asp:BoundField DataField="saleAmount" HeaderText="销量" />
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                               <asp:CheckBox ID="chkSet" Text="设为热菜" runat="server" AutoPostBack="True" 
                                    oncheckedchanged="chkSet_CheckedChanged" />
                            </ItemTemplate>
                            <ItemStyle Width="90px" />
                        </asp:TemplateField>
                    </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="border-bottom:1px silver dotted;"></td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="label2" Text="深圳" runat="server" CssClass="tabButtonBlueUnClick"></asp:Label>
                </td>
                <td>
                    <asp:GridView ID="GridView_ShenZhen" runat="server" DataKeyNames="DishID,DishName,DishPrice,shopName,shopAddress,saleAmount"
                    AutoGenerateColumns="False" SkinID="gridviewSkin" 
                        ondatabound="GridView_ShenZhen_DataBound">
                    <Columns>
                        <asp:BoundField DataField="DishID" HeaderText="序号" />
                        <asp:BoundField DataField="DishName" HeaderText="菜名" />
                        <asp:BoundField DataField="DishPrice" HeaderText="价格" />
                        <asp:BoundField DataField="shopName" HeaderText="所在店铺" />
                        <asp:BoundField DataField="shopAddress" HeaderText="店铺地址" />
                        <asp:BoundField DataField="saleAmount" HeaderText="销量" />
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                               <asp:CheckBox ID="chkSet" Text="设为热菜" runat="server" AutoPostBack="True" 
                                    oncheckedchanged="chkSet_CheckedChanged" />
                            </ItemTemplate>
                            <ItemStyle Width="90px" />
                        </asp:TemplateField>
                    </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <asp:Panel ID="Panel_MemberStatisticGridview" runat="server">
            <div class="div_gridview" id="div_othergridview">
                <div class="divDetailTitle" style="overflow-y: hidden">
                    <input id="Hidden1" type="hidden" />
                    <asp:Button ID="Button_Save" runat="server" Text="保存" CssClass="couponButtonSubmit"
                        OnClientClick="return confirm('确定操作?')" onclick="Button_Save_Click" />
                </div>
                <asp:Label ID="Label_errorMessage" runat="server" CssClass="Red" Text=""></asp:Label>
            </div>
        </asp:Panel>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
<script type="text/javascript">
    $(document).ready(function () {
        TabManage();
        GridViewStyle("GridView_Hangzhou", "gv_OverRow");
        GridViewStyle("GridView_Beijing", "gv_OverRow");
        GridViewStyle("GridView_Shanghai", "gv_OverRow");
        GridViewStyle("GridView_Guangzhou", "gv_OverRow");
    });
    var startDate = function (elem) {
        WdatePicker({
            el: elem,
            isShowClear: false,
            maxDate: '#F{$dp.$D(\'TextBox_registerTimeEnd\')||%y-%M-%d}',
            onpicked: function (dp) {
                elem.blur();
            },
            skin: 'whyGreen'
        });
    };
    var endDate = function (elem) {
        WdatePicker({
            el: elem,
            isShowClear: false,
            maxDate: '%y-%M-{%d+1}',
            minDate: '#F{$dp.$D(\'TextBox_registerTimeStr\')}',
            onpicked: function (dp) { elem.blur() },
            skin: 'whyGreen'
        });
    }
    </script>
</html>