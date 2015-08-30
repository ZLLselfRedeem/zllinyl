﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HomeTitle.aspx.cs" Inherits="HomeNew_CityManage" %>

<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>一级栏目管理</title>
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
    <style type="text/css">
        .auto-style2 {
            width: 75px;
            }
        .auto-style5 {
            width: 478px;
        }
        .auto-style6 {
            width: 73px;
        }
    </style>
</head>
<body scroll="no" style="overflow-y: hidden" onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <form id="form1" runat="server">
    <uc2:HeadControl ID="HeadControl1" runat="server" headName="一级栏目管理" />
    <div id="box" class="box">
        <div class="tagMenu">
            <ul class="menu">
                <li>一级栏目管理</li>
            </ul>
        </div>
        <table class="table" cellpadding="0" cellspacing="0" style="width: 100%; margin-bottom: 0px;">
                    <tr>
                        <th class="auto-style6">
                            &nbsp;&nbsp;
                            城市
                        </th>
                        <td class="auto-style2">
                            <asp:DropDownList ID="ddlCity" runat="server" Height="40px" Width="100px"></asp:DropDownList>
                        </td>
                         <td colspan="4" align="left" class="auto-style5">
                            <asp:Button ID="btnQuery" runat="server" Text="搜索" CssClass="couponButtonSubmit"
                                Width="130px" Height="32px" OnClick="btnQuery_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                             <asp:Button ID="Add" runat="server" Text="新建" CssClass="couponButtonSubmit"
                                Width="130px" Height="33px" OnClick="Add_Click" CommandName="add" />
                        </td>
                    </tr>
                </table>
         

        <div class="content">
            <div class="layout">
                <div class="div_gridview" id="div_gridview">
                    <asp:GridView ID="GridView_City" runat="server" DataKeyNames="id,cityID,cityName,status,titleIndex,type"
                        AutoGenerateColumns="False" SkinID="gridviewSkin" OnRowCommand="GridView_City_RowCommand" OnRowUpdating="GridView_City_RowUpdating" style="margin-right: 0px" OnRowDeleting="GridView_City_RowDeleting" OnRowDataBound="GridView_City_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="rowID" HeaderText="序号" />
                            <asp:BoundField DataField="id" HeaderText="栏目ID" Visible="false" />
                            <asp:BoundField DataField="cityID" HeaderText="城市ID" Visible="false" />
                            <asp:BoundField DataField="titleName" HeaderText="栏目名称" />
                            <asp:BoundField DataField="cityName" HeaderText="城市" />
                            <asp:BoundField DataField="titleIndex" HeaderText="栏目顺序" />
                            <asp:BoundField DataField="type" HeaderText="栏目类型" />

                             <asp:TemplateField HeaderText="客户端是否上线">
                                <ItemTemplate>
                                    <%#Eval("status").ToString().Equals("1") ? "<font color=greed>√</font>":"<font color=red>×</font>" %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField  HeaderText="二级栏目">
                                <ItemTemplate>
                                    
                                    <ASP:Label id="lbURL" runat="server"></ASP:Label>
                                </ItemTemplate>
                            </asp:TemplateField>                           

                            <asp:TemplateField HeaderText="操作">
                                <ItemTemplate>
                                    &nbsp;&nbsp;&nbsp;<img src="../Images/key_edit3.gif" /><asp:LinkButton ID="LinkButton5"
                                        runat="server" CausesValidation="False" foreColor="blue" CommandName="clientUpdate" Text='<%#Eval("status").ToString().Equals("1") ? "栏目下线":"栏目上线"%>' OnClientClick='<% # GetUplineConfirm(Convert.ToInt32(Eval("status")),Eval("type").ToString()) %>'></asp:LinkButton>
                                     &nbsp; &nbsp;&nbsp;<img src="../Images/key_edit3.gif" /><asp:LinkButton ID="LinkButton1"
                                        runat="server" CausesValidation="False" foreColor="blue" CommandName="edit" Text="编辑"></asp:LinkButton>
                                    &nbsp;&nbsp;&nbsp;<img src="../Images/delete.gif" />
                                    <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="False" foreColor="blue" CommandName="delete"
                                        Text="删除" OnClientClick= '<% # GetDeleteConfirm(Eval("type").ToString(),Eval("titleName").ToString()) %>'></asp:LinkButton>
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

