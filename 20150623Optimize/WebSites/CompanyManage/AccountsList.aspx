<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccountsList.aspx.cs" Inherits="CompanyManage_AccountsList" %>

<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>公司银行账户管理</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("GridView_CompanyAccounts", "gv_OverRow");
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc2:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
            navigationText="" navigationUrl="" headName="银行账户信息管理" />
    </div>
    <div id="box" class="box">
        <div class="content">
            <div class="layout">
                <div class="QueryTerms">
                    <div id="CompanyDiv" runat="server">
                        <table>
                            <tr>
                                <td>
                                    公司：<asp:DropDownList ID="DropDownList_Companys" runat="server" Enabled="false">
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="Button_Add" runat="server" Text="添 加" CssClass="button" OnClick="Button_Add_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <asp:Panel ID="panel_list" runat="server" CssClass=" gridview">
                    <asp:GridView ID="GridView_CompanyAccounts" runat="server" DataKeyNames="identity_Id,companyName,accountNum,bankName,remark,accountName"
                        AutoGenerateColumns="False" SkinID="gridviewSkin" OnRowDeleting="GridView_CompanyAccounts_RowDeleting"
                        OnSelectedIndexChanged="GridView_CompanyAccounts_SelectedIndexChanged">
                        <Columns>
                            <asp:BoundField DataField="accountNum" HeaderText="账户号码"></asp:BoundField>
                            <asp:BoundField DataField="accountName" HeaderText="开户名"></asp:BoundField>
                            <asp:BoundField DataField="bankName" HeaderText="银行名称"></asp:BoundField>
                            <asp:BoundField DataField="remark" HeaderText="备注"></asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <img src="../Images/key_edit3.gif" />
                                    <asp:LinkButton ID="lbt_select" runat="server" CausesValidation="False" CommandName="Select"
                                        Text="修改"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <img src="../Images/delete.gif" /><asp:LinkButton ID="LinkButton2" runat="server"
                                        CausesValidation="False" CommandName="delete" Text="删除" OnClientClick="return confirm('该公司旗下门店可能已关联该银行帐号，此操作将删除该关联关系，您确定删除吗？')"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
                <asp:Panel ID="panel_operate" runat="server">
                    <table style="width: 800px" class="table">
                        <tr>
                            <th style="width: 240px; text-align: right">
                                开户名：
                            </th>
                            <td style="width: 360px;">
                                <asp:TextBox ID="tb_accountName" runat="server" Width="160px"></asp:TextBox>
                                <label style="color: red;">
                                    *</label>
                            </td>
                        </tr>
                        <tr>
                            <th style="width: 240px; text-align: right">
                                账户号码：
                            </th>
                            <td style="width: 360px;">
                                <asp:TextBox ID="TextBox_AccountNum" runat="server" Width="160px"></asp:TextBox>
                                <label style="color: red;">
                                    *</label>
                            </td>
                        </tr>
                        <tr>
                            <th style="width: 240px; text-align: right">
                                账号核对：
                            </th>
                            <td style="width: 360px;">
                                <asp:TextBox ID="TextBox_NumCheck" runat="server" Width="160px"></asp:TextBox>
                                <label style="color: red;">
                                    *</label>
                            </td>
                        </tr>
                        <tr>
                            <th style="width: 240px; text-align: right">
                                银行：
                            </th>
                            <td style="width: 360px;">
                                <%--  <asp:DropDownList ID="DropDownList_BankName" runat="server">
                                </asp:DropDownList>--%>
                                <asp:TextBox ID="TextBox__BankName" runat="server" Width="160px"></asp:TextBox>
                                <label style="color: red;">
                                    *</label>
                            </td>
                        </tr>
                        <tr>
                            <th style="width: 240px; text-align: right">
                                账户备注：
                            </th>
                            <td>
                                <asp:TextBox ID="TextBox_Remark" runat="server" TextMode="MultiLine" Width="500px"
                                    Height="60px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                &nbsp;
                            </th>
                            <td>
                                <asp:Button ID="btn_ok" CssClass="button" runat="server" Text="确  定" OnClick="btn_ok_Click" />
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                <asp:Button ID="btn_back" CssClass="button" runat="server" Text="取  消" OnClick="btn_back_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
