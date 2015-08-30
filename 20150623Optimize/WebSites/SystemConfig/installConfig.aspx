<%@ Page Language="C#" AutoEventWireup="true" CodeFile="installConfig.aspx.cs" Inherits="SystemConfig_installConfig" %>

<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>产品配置</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("GridView_List", "gv_OverRow");
        });
    </script>
</head>
<body scroll="no" style="overflow-y: hidden" onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <form id="form1" runat="server">
    <uc2:HeadControl ID="HeadControl1" runat="server" headName="" navigationImage="~/images/icon/list.gif"
        navigationText="" navigationUrl="" />
    <div id="box" class="box">
        <div class="tagMenu">
            <ul class="menu">
                <li>产品配置</li>
            </ul>
        </div>
        <div class="content">
            <div class="layout">
                <div class="QueryTerms">
                    产品类型：
                    <asp:RadioButton ID="rbt_App" Text="悠先点菜App" GroupName="rbt" Checked="true" runat="server"
                        AutoPostBack="true" OnCheckedChanged="rbt_CheckedChanged" /><asp:RadioButton ID="rbt_Service"
                            Text="悠先服务" AutoPostBack="true" GroupName="rbt" OnCheckedChanged="rbt_CheckedChanged"
                            runat="server" />
                </div>
                <asp:Panel ID="Panel_List" runat="server" CssClass="div_gridview">
                    <div class="asp_page">
                        <asp:GridView ID="GridView_List" runat="server" DataKeyNames="id,latestBuild,latestUpdateDescription,latestUpdateUrl,type,oldBuildSupport,updateTime"
                            AutoGenerateColumns="False" SkinID="gridviewSkin" OnSelectedIndexChanged="GridView_List_SelectedIndexChanged">
                            <Columns>
                                <asp:BoundField DataField="latestBuild" HeaderText="当前版本" />
                                <asp:BoundField DataField="latestUpdateDescription" HeaderText="版本描述" />
                                <asp:BoundField DataField="latestUpdateUrl" HeaderText="下载地址" />
                                <asp:TemplateField HeaderText="分类">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_type" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="oldBuildSupport" HeaderText="支持最低版本" />
                                <asp:BoundField DataField="updateTime" HeaderText="更新时间" />
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <img src="../Images/key_edit3.gif" />&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1"
                                            runat="server" CausesValidation="False" CommandName="Select" Text="修改"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </asp:Panel>
                <asp:Panel ID="Panel_Add" runat="server" Visible="false" CssClass="div_gridview">
                    <table class="table" cellpadding="0" cellspacing="0">
                        <tr>
                            <th>
                                当前版本：
                            </th>
                            <td>
                                <asp:TextBox ID="txt_latestBuild" runat="server"></asp:TextBox>(请输入版本号)
                            </td>
                        </tr>
                        <tr>
                            <th>
                                版本描述：
                            </th>
                            <td>
                                <asp:TextBox ID="txt_latestUpdateDescription" TextMode="MultiLine" Width="300px"
                                    Height="100px" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                下载地址：
                            </th>
                            <td>
                                <asp:TextBox ID="txt_latestUpdateUrl" runat="server" Width="300px" TextMode="MultiLine"
                                    Height="80px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                分类：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddl_type" runat="server">
                                </asp:DropDownList>
                                (请选择客户端类型)
                            </td>
                        </tr>
                        <tr>
                            <th>
                                支持最低版本：
                            </th>
                            <td>
                                <asp:TextBox ID="txt_oldBuildSupport" runat="server"></asp:TextBox>(请输入版本号)
                            </td>
                        </tr>
                        <tr>
                            <th>
                                程序包：
                            </th>
                            <td>
                                <table runat="server" id="table">
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
                                            <asp:Button ID="ButtonBig" runat="server" Text="上传" CssClass="button" OnClick="ButtonBig_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="Button_Save" runat="server" CssClass="button" Text="保  存" OnClick="Button_Save_Click" />&nbsp;&nbsp;
                                <asp:Button ID="Button_Back" runat="server" CssClass="button" Text="返  回" OnClick="Button_Back_Click" />
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
