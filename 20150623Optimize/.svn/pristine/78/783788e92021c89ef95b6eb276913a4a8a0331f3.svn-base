<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShopVipDicount.aspx.cs" Inherits="ShopManage_ShopVipDicount" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>VIP折扣管理</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script src="../Scripts/CommonScript.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("GridView_ShopVipList", "gv_OverRow");
            $("#form1").validate({
                rules: {
                    TextBox_name: { "required": true },
                    TextBox_discount: { "number": true, "min": 0, "max": 1 }
                },
                messages: {
                    TextBox_name: { "required": "此项不能为空" },
                    TextBox_discount: "请输入0至1的数字"
                },
                success: function (label) {
                    label.html("&nbsp;").addClass("checked");
                }
            });
        });
    </script>
</head>
<body scroll="no" style="overflow-y: hidden" onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <form id="form1" runat="server">
        <uc2:HeadControl ID="HeadControl1" runat="server" headName="店铺VIP折扣管理" navigationImage="~/images/icon/list.gif"
            navigationText="门店列表" navigationUrl="javascript:history.go(-1)" />
        <div id="box" class="box">
            <div class="tagMenu">
                <ul class="menu">
                    <li>店铺VIP折扣管理</li>
                </ul>
            </div>
            <div class="content">
                <div class="layout">
                    <div class="QueryTerms">
                        <asp:Label ID="Label_shop" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button_Add" runat="server" Text="添    加" CssClass="button" OnClick="Button_Add_Click" />
                    </div>
                    <asp:Panel ID="Panel_ShopVipList" runat="server" CssClass="div_gridview" Visible="true">
                        <asp:GridView ID="GridView_ShopVipList" runat="server" DataKeyNames="id,platformVipId,name,discount"
                            AutoGenerateColumns="False" OnRowDeleting="GridView_ShopVipList_RowDeleting"
                            SkinID="gridviewSkin" OnSelectedIndexChanged="GridView_ShopVipList_SelectedIndexChanged">
                            <Columns>
                                <asp:BoundField DataField="id" HeaderText="id" Visible="false" />
                                <asp:BoundField DataField="platformVipId" HeaderText="platformVipId" Visible="false" />
                                <asp:BoundField DataField="name" HeaderText="名称" />
                                <asp:BoundField DataField="discount" HeaderText="折扣" />
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <img src="../Images/key_edit3.gif" />&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1"
                                            runat="server" CausesValidation="False" CommandName="Select" Text="修改"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <img src="../Images/delete.gif" />&nbsp;&nbsp;<asp:LinkButton ID="LinkButton2" runat="server"
                                            CausesValidation="False" CommandName="delete" Text="删除" OnClientClick="return confirm('你确定删除吗？')"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <div class="asp_page">
                            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
                                NextPageText="下一页" PageSize="10" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                                TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                                NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" PageIndexBoxClass="listPageText"
                                CurrentPageButtonClass="currentButton" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center">
                            </webdiyer:AspNetPager>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="Panel_ShopVipAdd" runat="server" CssClass="div_gridview" Visible="true">
                        <table class="table" cellpadding="0" cellspacing="0">
                            <tr>
                                <th class="style9">名称：
                                </th>
                                <td class="style1">
                                    <asp:TextBox ID="TextBox_ShopVipName" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th class="style9">折扣：
                                </th>
                                <td class="style1">
                                    <asp:TextBox ID="TextBox_Discount" runat="server"></asp:TextBox>
                                    %（0%表示免费，100%表示不打折）
                                </td>
                            </tr>
                            <tr>
                                <th class="style9">平台等级：
                                </th>
                                <td class="style1">
                                    <asp:DropDownList ID="DropDownList_VAVip" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="style9">&nbsp;
                                </td>
                                <td class="style1">
                                    <asp:Button ID="Button_Save" runat="server" Text="保  存" CssClass="button" OnClick="Button_Save_Click" />
                                    <asp:Button ID="Button1" runat="server" Text="返  回" CssClass="button" OnClick="Button1_Click" />
                                </td>
                                <asp:HiddenField ID="HiddenField_Id" runat="server" />
                                <asp:HiddenField ID="HiddenField_platformVipId" runat="server" />
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
