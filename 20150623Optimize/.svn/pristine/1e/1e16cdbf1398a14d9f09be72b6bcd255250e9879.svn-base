<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShopChannelConfig.aspx.cs" Inherits="OrderOptimization_ShopChannelConfig" %>

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
            GridViewStyle("GridView_ChannelConfig", "gv_OverRow");
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
        <uc2:HeadControl ID="HeadControl1" runat="server" headName="商户增值页面配置" />
        <div id="box" class="box">
            <div class="tagMenu">
                <ul >
                    <%--<asp:TextBox ID="TextBox_MerchantName" runat="server" Width="1500px" BorderWidth="0" ReadOnly="true" Enabled="false"></asp:TextBox>--%>
                    <li runat="server" id="TextBox_MerchantName" style="width:500px;text-align:left;">商户增值页面配置</li>
                </ul>
            </div>
            <div class="content">
                <div class="layout">
                    <div class="div_gridview" id="div_gridview">
                        <asp:GridView ID="GridView_ChannelConfig" runat="server" DataKeyNames="id,status,channelIndex,name"
                            AutoGenerateColumns="False" SkinID="gridviewSkin" OnRowCommand="GridView_ChannelConfig_RowCommand" OnRowUpdating="GridView_ChannelConfig_RowUpdating" Style="margin-right: 0px" OnRowDeleting="GridView_ChannelConfig_RowDeleting" OnRowDataBound="GridView_ChannelConfig_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="rowID" HeaderText="序号" />
                                <asp:BoundField DataField="id" HeaderText="增值页ID" Visible="false" />
                                <asp:BoundField DataField="name" HeaderText="增值页名称" />
                                <asp:BoundField DataField="sign" HeaderText="标签" />
                                <asp:BoundField DataField="channelIndex" HeaderText="排序" />
                                <asp:TemplateField HeaderText="是否开启">
                                    <ItemTemplate>
                                        <%#Eval("status").ToString().Equals("True") ? "<font color=greed>√</font>":"<font color=red>×</font>" %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="操作">
                                    <ItemTemplate>
                                        &nbsp;&nbsp;&nbsp;<img src="../Images/key_edit3.gif" /><asp:LinkButton ID="LinkButton5"
                                            runat="server" CausesValidation="False" ForeColor="blue" CommandName="open" Text='<%#Eval("status").ToString().Equals("True") ? "关闭":"启用"%>'
                                            OnClientClick='<% # GetConfirm(Convert.ToString(Eval("status")), Convert.ToString(Eval("name")))%>'></asp:LinkButton>
                                        &nbsp; &nbsp;&nbsp;<img src="../Images/key_edit3.gif" /><asp:LinkButton ID="LinkButton1"
                                            runat="server" CausesValidation="False" CommandName="config" Text="广告设置" Enabled='<%#Eval("status").ToString().Equals("True")%>' ForeColor='<%#Eval("status").ToString().Equals("False")? System.Drawing.Color.Gray:System.Drawing.Color.Blue %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
<%--        <uc1:CheckUser ID="CheckUser1" runat="server" />--%>
    </form>
</body>
</html>
