<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShopImageRevelation .aspx.cs"
    Inherits="ShopManage_ShopImageRevelation_" %>

<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>门店图片展示管理</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
</head>
<body scroll="no" style="overflow-y: hidden" onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <form id="form1" runat="server">
    <uc2:HeadControl ID="HeadControl1" runat="server" headName="门店图片展示管理" navigationImage="~/images/icon/list.gif"
        navigationText="门店列表" navigationUrl="" />
    <div id="box" class="box">
        <div class="tagMenu">
            <ul class="menu">
                <li>门店图片展示管理</li>
            </ul>
        </div>
        <div class="content">
            <div class="layout">
                <table>
                    <tr>
                        <th>
                            门店展示图片（格式png或jpg，比例4:3，最小尺寸652*489，大小不超过<asp:Label runat="server" ID="lbShopEnvironmentSpace"></asp:Label>）：
                        </th>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <input id="Text_ShopImageSelectFile" type="text" readonly="readonly" />
                                    </td>
                                    <td>
                                        <div style="position: relative">
                                            <input id="Button_ShopImageSelectFile" type="button" value="选择文件" class="commonButton" />
                                            <asp:FileUpload ID="FileUpload_ShopImage" runat="server" onchange="Text_ShopImageSelectFile.value=this.value"
                                                CssClass="fileUpload" />
                                        </div>
                                    </td>
                                    <td>
                                        <asp:Button ID="Button_ShopImage" runat="server" Text="上传" Width="70px" CssClass="commonButton"
                                            OnClick="Button_ShopImage_Click" />
                                    </td>
                                    <td>
                                        &nbsp;<asp:Label ID="label_message" CssClass="Red" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Repeater ID="Repeater_ShopImage" runat="server" OnItemCommand="Repeater_ShopImage_ItemCommand">
                                <HeaderTemplate>
                                    <table>
                                        <tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <td style="border: 1px solid gray">
                                        <table>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Image ID="Image_ShopImage" runat="server" Height="135px" ImageUrl="~/Images/smallimage.jpg"
                                                        Width="180px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right">
                                                    <asp:LinkButton ID="Button_ShopImage" runat="server" Text="删除" CssClass="width36Button"
                                                        OnClientClick="return confirm('确定删除吗')" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tr></table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
