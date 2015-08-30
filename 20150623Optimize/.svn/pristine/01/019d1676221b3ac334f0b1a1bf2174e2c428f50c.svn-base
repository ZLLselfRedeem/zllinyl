<%@ Page Language="C#" AutoEventWireup="true" CodeFile="platformVipConfig.aspx.cs"
    Inherits="platformVipConfig" %>

<%@ Register Src="WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>友络平台VIP信息配置</title>
    <link href="Css/css.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="Scripts/ManageControls.js" type="text/javascript"></script>
    <script src="Scripts/CommonScript.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("GridView_VipList", "gv_OverRow");
        });
    </script>
    <style type="text/css">
        .style2
        {
            height: 210px;
        }
        .style4
        {
            height: 210px;
            width: 355px;
        }
        .style6
        {
            height: 36px;
            width: 355px;
        }
        .style8
        {
            height: 35px;
            width: 355px;
        }
        .style9
        {
            height: 35px;
        }
    </style>
</head>
<body scroll="no" style="overflow-y: hidden" onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <form id="form1" runat="server">
    <uc2:HeadControl ID="HeadControl1" runat="server" headName="平台VIP折扣管理" navigationImage="~/images/icon/list.gif"
        navigationText="" navigationUrl="" />
    <div id="box" class="box">
        <div class="tagMenu">
            <ul class="menu">
                <li>平台VIP折扣管理</li>
            </ul>
        </div>
        <div class="content">
            <div class="layout">
                <div class="QueryTerms">
                    <asp:Button ID="Button_Add" runat="server" Text="添    加" CssClass="button" OnClick="Button_Add_Click" />
                </div>
                <asp:Panel ID="Panel_VipList" runat="server" CssClass="div_gridview">
                    <div class="asp_page">
                        <asp:GridView ID="GridView_VipList" runat="server" DataKeyNames="id,name ,isMonetary,consumptionLevel,status,vipImg"
                            AutoGenerateColumns="False" SkinID="gridviewSkin" OnRowDeleting="GridView_VipList_RowDeleting"
                            OnSelectedIndexChanged="GridView_VipList_SelectedIndexChanged">
                            <Columns>
                                <asp:BoundField DataField="id" HeaderText="id" Visible="false" />
                                <asp:BoundField DataField="name" HeaderText="等级名称" />
                                <asp:BoundField DataField="consumptionLevel" HeaderText="消费金额（次数）" />
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <img src="Images/key_edit3.gif" />&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1" runat="server"
                                            CausesValidation="False" CommandName="Select" Text="修改"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <img src="Images/delete.gif" />&nbsp;&nbsp;<asp:LinkButton ID="LinkButton2" runat="server"
                                            CausesValidation="False" CommandName="delete" Text="删除" OnClientClick="return confirm('你确定删除吗？')"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </asp:Panel>
                <asp:Panel ID="Panel_VipAdd" runat="server" CssClass="div_gridview">
                    <table class="table" cellpadding="0" cellspacing="0">
                        <tr>
                            <th class="style9">
                                等级名称：
                            </th>
                            <td class="style8">
                                <asp:TextBox ID="TextBox_Name" runat="server" Width="180px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th class="style9">
                                消费金额（次数）：
                            </th>
                            <td class="style8">
                                <asp:TextBox ID="TextBox_Count" runat="server" Width="180px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th class="style2">
                                等级图片：
                            </th>
                            <td class="style4">
                                <table>
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td colspan="3">
                                                        <asp:Image ID="img" runat="server" ImageUrl="~/Images/bigimage.jpg" Width="320px"
                                                            Height="110px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <input id="Text_SelectFile" type="text" readonly="readonly" />
                                                    </td>
                                                    <td>
                                                        <div style="position: relative;">
                                                            <input id="Button_SelectFile" type="button" value="选择文件" class="button" />
                                                            <asp:FileUpload ID="Big_File" runat="server" onchange="Text_SelectFile.value=this.value"
                                                                CssClass="fileUpload" />
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="ButtonBig" runat="server" Text="上传" OnClick="ButtonBig_Click" CssClass="button" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="style9" colspan="2" align="center">
                                <asp:Button ID="Button_Save" runat="server" CssClass="button" Text="保  存" OnClick="Button_Save_Click" />&nbsp;&nbsp;
                                <asp:Button ID="Button1" runat="server" CssClass="button" Text="返  回" OnClick="Button1_Click" />
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
