<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChannelManager.aspx.cs" Inherits="OrderOptimization_ChannelManager" %>

<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>增值页面管理</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("GridView_Channel", "gv_OverRow");
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
    <uc2:HeadControl ID="HeadControl1" runat="server" headName="增值页面管理" />
    <div id="box" class="box">
        <div class="tagMenu">
            <ul class="menu">
                <li>增值页列表</li>
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
                    <asp:GridView ID="GridView_Channel" runat="server" DataKeyNames="id,firstID,status"
                        AutoGenerateColumns="False" SkinID="gridviewSkin" OnRowCommand="GridView_Channel_RowCommand" OnRowUpdating="GridView_Channel_RowUpdating" style="margin-right: 0px" OnRowDeleting="GridView_Channel_RowDeleting" OnRowDataBound="GridView_Channel_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="rowID" HeaderText="序号" />
                            <asp:BoundField DataField="id" HeaderText="频道ID" Visible="false" />
<%--                            <asp:BoundField DataField="cityID" HeaderText="城市ID" Visible="false" />--%>
                            <asp:BoundField DataField="channelName" HeaderText="频道名称" />
                            <asp:BoundField DataField="price" HeaderText="价格/¥" />
                            <asp:TemplateField HeaderText="画面类别">
                                <ItemTemplate>
                                    <%#Eval("modelType").ToString().Equals("1") ? "新品":"特价" %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="signStr" HeaderText="标签" />
                             <asp:TemplateField HeaderText="默认页来源">
                                <ItemTemplate>
                                    <%#Eval("firstID").ToString().Equals("0") ? "无":Eval("titleName").ToString() %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="titleName" HeaderText="默认页来源" />--%>
                             <asp:TemplateField HeaderText="是否启用">
                                <ItemTemplate>
                                    <%#Eval("status").ToString().Equals("True") ? "<font color=greed>√</font>":"<font color=red>×</font>" %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="操作">
                                <ItemTemplate>
                                    &nbsp;&nbsp;&nbsp;<img src="../Images/key_edit3.gif" /><asp:LinkButton ID="LinkButton5"
                                        runat="server" CausesValidation="False" foreColor="blue" CommandName="open" Text='<%#Eval("status").ToString().Equals("True") ? "关闭":"启用"%>' 
                                        OnClientClick='<% # GetConfirm(Convert.ToString(Eval("status")), Convert.ToString(Eval("channelName"))) %>'></asp:LinkButton>
                                     &nbsp; &nbsp;&nbsp;<img src="../Images/key_edit3.gif" /><asp:LinkButton ID="LinkButton1"
                                        runat="server" CausesValidation="False" foreColor="blue" CommandName="edit" Text="编辑"></asp:LinkButton>
                                    &nbsp;&nbsp;&nbsp;<img src="../Images/delete.gif" />
                                    <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="False" foreColor="blue" CommandName="delete"
                                        Text="删除" OnClientClick="return confirm('你确定删除吗？')"></asp:LinkButton>
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
