<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CityManage.aspx.cs" Inherits="HomeNew_CityManage" %>

<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>城市管理</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("GridView_City", "gv_OverRow");
            $("#div_gridview").css({ "height": $(window).height() - 200 });
            $("#div_gridview").css({ "overflow": "auto" });
        });
    </script>
</head>
<body scroll="no" style="overflow-y: hidden" onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <form id="form1" runat="server">
        <uc2:HeadControl ID="HeadControl1" runat="server" headName="城市管理" />
        <div id="box" class="box">
            <div class="tagMenu">
                <ul class="menu">
                    <li>城市管理</li>
                </ul>
            </div>
            <div class="content">
                <div class="layout">
                    <div class="QueryTerms">
                        <table>
                            <tr>
                                <td>城市名称：
                                <asp:TextBox ID="TextBox_CityName" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="Button_QueryCity" runat="server" CssClass="button" Text="查 询"
                                        OnClick="Button_QueryCity_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="div_gridview" id="div_gridview">
                        <asp:GridView ID="GridView_City" runat="server" DataKeyNames="cityID,cityName,status,isClientShow"
                            AutoGenerateColumns="False" SkinID="gridviewSkin" OnRowCommand="GridView_City_RowCommand" OnRowUpdating="GridView_City_RowUpdating" Style="margin-right: 0px" OnSelectedIndexChanged="GridView_City_SelectedIndexChanged">
                            <Columns>
                                <asp:BoundField DataField="rowID" HeaderText="序号" />
                                <asp:BoundField DataField="cityID" HeaderText="城市ID" Visible="false" />
                                <asp:BoundField DataField="cityName" HeaderText="城市" />
                                <asp:BoundField DataField="provinceName" HeaderText="省份" />
                                <asp:TemplateField HeaderText="是否入驻">
                                    <ItemTemplate>
                                        <%#Eval("status").ToString().Equals("2") ? "<font color=greed>√</font>":"<font color=red>×</font>" %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="客户端是否上线">
                                    <ItemTemplate>
                                        <%#Eval("isClientShow").ToString().Equals("True") ? "<font color=greed>√</font>":"<font color=red>×</font>" %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="操作">
                                    <ItemTemplate>
                                        <img src="../Images/key_edit3.gif" />&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1"
                                            runat="server" CausesValidation="False" ForeColor="blue" CommandName="cityUpdate"
                                            Text='<%#Eval("status").ToString().Equals("1") ? "城市入驻":"城市退出"%>'
                                            OnClientClick='<% # UpdateConfirmMsg(Convert.ToInt32(Eval("status")), Eval("cityName").ToString())%>'>'></asp:LinkButton>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <img src="../Images/key_edit3.gif" />&nbsp;&nbsp;<asp:LinkButton ID="LinkButton2"
                                        runat="server" CausesValidation="False" CommandName="clientUpdate"
                                        Text='<%#Eval("isClientShow").ToString().Equals("True") ? "客户端下线":"客户端上线"%>'
                                        Enabled='<%#Eval("status").ToString().Equals("2")%>'
                                        ForeColor='<%#Eval("status").ToString().Equals("1")? System.Drawing.Color.Gray:System.Drawing.Color.Blue %>'
                                        OnClientClick='<% # ClientConfirmMsg(Convert.ToInt32(Eval("status")), Eval("isClientShow").ToString(), Eval("cityName").ToString())%>'>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
        <uc1:CheckUser ID="CheckUser1" runat="server" />
    </form>
</body>
</html>

