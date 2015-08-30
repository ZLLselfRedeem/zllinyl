<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditMerchantActivity.aspx.cs" Inherits="Award_EditMerchantActivity" %>

<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>编辑商户详情</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="../Scripts/messages_cn.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // 初始化
            if ($("#radRed input[type='radio']:checked").val() == "1") {
                $("#spanSetRedCount").show();
            }
            else {
                $("#spanSetRedCount").hide();
            }

            if ($("#radAvoidQueue input[type='radio']:checked").val() == "1") {
                $("#spanSetAvoidQueueCount").show();
            }
            else {
                $("#spanSetAvoidQueueCount").hide();
            }

            if ($("#radDish input[type='radio']:checked").val() == "1") {
                $("#divContentMenu").show();
            }
            else {
                $("#divContentMenu").hide();
            }

            // 事件
            $("#radAvoidQueue input[type='radio']").click(function () {
                if ($(this).val() == "1") {
                    $("#spanSetAvoidQueueCount").show();
                }
                else {
                    $("#spanSetAvoidQueueCount").hide();
                }
            });
            $("#radRed input[type='radio']").click(function () {
                if ($(this).val() == "1") {
                    $("#spanSetRedCount").show();
                }
                else {
                    $("#spanSetRedCount").hide();
                }
            });

            $("#radDish input[type='radio']").click(function () {
                if ($(this).val() == "1") {
                    $("#divContentMenu").show();
                }
                else {
                    $("#divContentMenu").hide();
                }
            });
        });
    </script>
</head>
<body scroll="no" style="overflow-y: hidden" onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <form id="form1" runat="server">
        <uc2:HeadControl ID="HeadControl1" runat="server" headName="编辑商户详情" navigationImage="~/images/icon/list.gif"
            navigationText="商户活动查询" navigationUrl="javascript:history.go(-1)" />
        <div id="box" class="box">
            <div class="tagMenu">
                <ul class="menu">
                    <li>基本信息</li>
                </ul>
            </div>
            <div class="content">
                <div class="layout">
                    <table class="table" cellpadding="0" cellspacing="0">
                        <tr>
                            <th>公司名：
                            </th>
                            <td>
                                <asp:TextBox ID="txtCompanyName" runat="server" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>门店名：
                            </th>
                            <td>
                                <asp:TextBox ID="txtShopName" runat="server" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>抽奖功能：
                            </th>
                            <td>

                                <asp:RadioButtonList ID="radAward" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">打开</asp:ListItem>
                                    <asp:ListItem Value="0">关闭</asp:ListItem>
                                </asp:RadioButtonList>

                            </td>
                        </tr>
                        <tr>
                            <th>免排队：
                            </th>
                            <td>
                                <asp:RadioButtonList ID="radAvoidQueue" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">打开</asp:ListItem>
                                    <asp:ListItem Value="0">关闭</asp:ListItem>
                                </asp:RadioButtonList>
                                <span id="spanSetAvoidQueueCount">设置名额：<asp:TextBox ID="txtSetAvoidQueueCount" runat="server"></asp:TextBox></span>
                            </td>
                        </tr>
                        <tr>
                            <th>赠送菜品：
                            </th>
                            <td>
                                <asp:RadioButtonList ID="radDish" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">打开</asp:ListItem>
                                    <asp:ListItem Value="0">关闭</asp:ListItem>
                                </asp:RadioButtonList>
                                <div class="div_gridview" id="divContentMenu">
                                    <asp:GridView ID="GridViewDish" runat="server" DataKeyNames="Id,ShopId,Name,Count"
                                        AutoGenerateColumns="False" SkinID="gridviewSkin" OnRowCommand="GridViewDish_RowCommand" OnRowDeleting="GridViewDish_RowDeleting">
                                        <Columns>
                                            <asp:BoundField DataField="Name" HeaderText="菜品名称" />

                                            <asp:BoundField DataField="Count" HeaderText="赠菜份数" />

                                            <asp:TemplateField ShowHeader="False">
                                                <ItemTemplate>
                                                    <img src="../Images/key_edit3.gif" />&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1"
                                                        runat="server" CausesValidation="False" CommandName="edit" Text="修改"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ShowHeader="False">
                                                <ItemTemplate>
                                                    <img src="../Images/delete.gif" />&nbsp;&nbsp;
                                                <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="False" CommandName="delete"
                                                    Text="删除" OnClientClick="return confirm('你确定删除吗？')"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Button ID="btnAddDish" Text="添加赠菜" runat="server" OnClick="btnAddDish_Click" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <th>返现限量：
                            </th>
                            <td>
                                <asp:RadioButtonList ID="radRed" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">打开</asp:ListItem>
                                    <asp:ListItem Value="0">关闭</asp:ListItem>
                                </asp:RadioButtonList>
                                <span id="spanSetRedCount">设置数量上限：<asp:TextBox ID="txtSetRedCount" runat="server"></asp:TextBox></span>
                                <span id="spanSetRedAmount">设置金额上限：<asp:TextBox ID="txtSetRedAmount" runat="server"></asp:TextBox></span>
                            </td>
                        </tr>

                        <tr>
                            <td>&nbsp;
                            </td>
                            <td>
                                <asp:Button ID="btnSave" runat="server" Text="保    存" CssClass="button" OnClick="btnSave_Click" />
                                <asp:Button ID="btnCancle" runat="server" Text="取    消" CssClass="button" OnClick="btnCancle_Click" />
                                <asp:HiddenField ID="hidShowDish" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

