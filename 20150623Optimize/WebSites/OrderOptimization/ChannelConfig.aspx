﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChannelConfig.aspx.cs" Inherits="OrderOptimization_ChannelConfig" %>

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

        $(window).bind('beforeunload', function () {
            return '请确认当前页面是否有未发布的数据，离开此页面未发布的数据将会丢失！';
        });

            function unbindunbeforunload() {
                window.onbeforeunload = undefined;
            }

            function oncbl(Control) {
                var input = document.getElementsByTagName("input");
                for (i = 0; i < input.length; i++) {
                    if (input[i].type == "checkbox") {
                        input[i].checked = Control.checked;
                    }
                }
            }
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
                <ul>
                    <%--<asp:TextBox ID="TextBox_MerchantName" runat="server" Width="1500px" BorderWidth="0" ReadOnly="true" Enabled="false"></asp:TextBox>--%>
                     <li runat="server" id="TextBox_MerchantName" style="width:500px;text-align:left;">商户增值页面配置</li>
                </ul>
            </div>
            <div class="content" id="divDetail" runat="server" style="width: 100%; display: '';">
                <div style="width: 80%">
                    <div>
                        <hr style="height: 1px; border: none; border-top: 2px solid blue;" />
                    </div>
                    <table>
                        <tr>
                            <td>全选：<input type="checkbox" id="ckbCheckAll" onclick="oncbl(this)" /></td>
                            <td>
                                <asp:Button ID="btnAdd" runat="server" Height="30px" Text="新增" CssClass="couponButtonSubmit" OnClientClick="unbindunbeforunload();" OnClick="btnAdd_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnDelete" runat="server" Height="30px" Text="删除" OnClientClick="unbindunbeforunload();return confirm('你确定删除吗？')" CssClass="couponButtonSubmit" OnClick="btnDelete_Click" /></td>
                        </tr>
                        <br />
                        <th>增值页排序</th>
                        <td>
                            <asp:TextBox ID="TextBox_Index" runat="server" Width="106px"></asp:TextBox></td>
                    </table>
                </div>
                <div class="content">
                    <div class="layout">
                        <div class="div_gridview" id="div_gridview">
                            <asp:GridView ID="GridView_ChannelConfig" runat="server" DataKeyNames="id,dishIndex"
                                AutoGenerateColumns="False" SkinID="gridviewSkin" OnRowCommand="GridView_ChannelConfig_RowCommand" OnRowUpdating="GridView_ChannelConfig_RowUpdating" Style="margin-right: 0px" OnRowDeleting="GridView_ChannelConfig_RowDeleting" OnRowDataBound="GridView_ChannelConfig_RowDataBound">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <input type="checkbox" id="ckbSelect" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="rowID" HeaderText="序号" />
                                    <asp:BoundField DataField="id" HeaderText="菜品ID" Visible="false" />
                                    <asp:BoundField DataField="dishName" HeaderText="菜品" />
                                    <asp:BoundField DataField="dishPrice" HeaderText="价格/¥" />
                                    <asp:BoundField DataField="dishIndex" HeaderText="排序" />
                                    <asp:TemplateField HeaderText="操作">
                                        <ItemTemplate>
                                            &nbsp; &nbsp;&nbsp;<img src="../Images/key_edit3.gif" /><asp:LinkButton ID="LinkButton1"
                                                runat="server" CausesValidation="False" ForeColor="blue" CommandName="edit" Text="编辑" OnClientClick="unbindunbeforunload();"></asp:LinkButton>
                                            &nbsp;&nbsp;&nbsp;<img src="../Images/delete.gif" />
                                            <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="False" ForeColor="blue" CommandName="delete"
                                                Text="删除" OnClientClick="unbindunbeforunload();return confirm('你确定删除吗？')"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                            <br />
                            <table style="margin-right: 0px; padding: 0px 0px 0px 20px; text-align: right">
                                <tr>
                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:Button ID="btnCancel" runat="server" Text="取   消" CssClass="buttonEnable" OnClick="btnCancel_Click" BorderStyle="None" Width="100px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnUpdate" runat="server" Text="发   布" CssClass="buttonEnable" OnClientClick="unbindunbeforunload();" OnClick="btnSave_Click" Width="100px" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <%--        <uc1:CheckUser ID="CheckUser1" runat="server" />--%>
    </form>
</body>
</html>
