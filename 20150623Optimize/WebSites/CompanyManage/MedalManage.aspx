<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MedalManage.aspx.cs" Inherits="CompanyManage_MedalManage" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>勋章管理</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts/searchShop.js"></script>
    <style type="text/css">
        broColorColorStyle {
            background-color: Red;
        }

        table {
            border: 1px;
            padding: 0 0;
        }

        .textContent {
            margin-left: 1px;
            padding: 0px;
            border: 1px solid gray;
        }

            .textContent .rb {
                border-right: 1px solid gray;
                border-bottom: 1px solid gray;
                padding: 10px;
            }

            .textContent .bot {
                border-bottom: 1px solid gray;
                padding: 10px;
            }

            .textContent .rt {
                border-right: 1px solid gray;
            }

        li:hover {
            text-decoration: underline;
            color: #39c;
            cursor: pointer;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            initData("medalManage");
        });
        function Pannel_Confrim() {
            if ($("#TextBox_Name").val() == "" || $("#TextBox_Name").val().length > 10) {
                alert("请填写字数不超过10个字的勋章名称");
                return false;
            }
            else if ($("#TextBox_Description").val() == "") {
                alert("请填写对应的勋章的描述信息");
                return false;
            }
            else {
                return true;
            }
        }
        function VerifyNumber() {
            if ($("#Text_MedalImageSelectFile").val() == "") {
                $("#label_message").text("请选择上传图片文件");
                return false;
            }
            else if ($("#Text_MedalSelectFile").val() == "") {
                $("#label_medalMessage").text("请选择上传图片文件");
                return false;
            }
            else if ($("#TextBox_medalName").val() == "" || $("#TextBox_medalName").val().length > 10) {
                $("#Label_MeadlName").text("请填写字数不超过10个字的勋章名称");
                return false;
            }
            else if ($("#TextBox_medalDescription").val() == "" || $("#TextBox_medalName").val().length > 100) {
                $("#Label_Description").text("请填写字数不超过100个字的勋章描述");
                return false;
            }
            else {
                return true;
            }
        }
    </script>
</head>
<body onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <form id="form1" runat="server">
        <uc2:HeadControl ID="HeadControl1" runat="server" headName="勋章管理" navigationImage="~/images/icon/list.gif"
            navigationText="" navigationUrl="" />
        <div id="box" class="box">
            <div class="tagMenu">
                <ul class="menu">
                    <li>勋章管理</li>
                </ul>
            </div>
            <div class="content">
                <div class="layout">
                    <asp:Panel ID="Panel_Table" runat="server">
                        <table class="textContent" cellpadding="0" cellspacing="0">
                            <tr>
                                <th class="rb">选择门店：
                                </th>
                                <td class="bot">
                                    <input id="text" runat="server" type="text" onkeyup="medalManage()" />(输入搜索选择)
                                    <div id="init_date" runat="server" style="position: absolute; clear: both; border: 1px solid #000; background-color: White">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <th class="rt">勋章图片：
                                <br />
                                    （最小120*120,png,jpg）
                                </th>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <input id="Text_MedalImageSelectFile" type="text" readonly="readonly" />
                                            </td>
                                            <td>
                                                <div style="position: relative">
                                                    <input id="Button_MedalImageSelectFile" type="button" value="选择文件" class="commonButton" />
                                                    <asp:FileUpload ID="FileUpload_MedalImage" runat="server" onchange="Text_MedalImageSelectFile.value=this.value"
                                                        CssClass="fileUpload" />
                                                    <asp:Button ID="Button_ShopImage" runat="server" Text="上传" Width="70px" CssClass="commonButton" OnClick="Button_ShopImage_Click" />
                                                </div>
                                            </td>
                                            <td>&nbsp;<asp:Label ID="label_message" CssClass="Red" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="rb">&nbsp;
                                </td>
                                <td class="bot">
                                    <asp:Repeater ID="Repeater_MedalImage" runat="server" OnItemCommand="Repeater_MedalImage_ItemCommand">
                                        <HeaderTemplate>
                                            <table>
                                                <tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <td style="border: 1px solid gray;">
                                                <table>
                                                    <tr>
                                                        <td colspan="3">
                                                            <asp:Image ID="Image_MedalImage" runat="server" Height="120px" ImageUrl="~/Images/smallimage.jpg"
                                                                Width="120px" />
                                                        </td>
                                                    </tr>
                                                    <%--<tr>
                                                        <td colspan="3">
                                                            <asp:Label ID="Label_MedalImage" CssClass="Red" runat="server" Text=" "></asp:Label>
                                                    </tr>--%>
                                            </td>
                                            <tr>
                                                <td style="text-align: left">
                                                    <asp:LinkButton ID="Button_MedalImage" runat="server" Text="删除" CssClass="width36Button"
                                                        CommandArgument="delete" OnClientClick="return confirm('此操作将删除对应勋章信息，确定删除吗？')" />
                                                </td>
                                                <td></td>
                                                <%--<td style="text-align: right">
                                                    <asp:LinkButton ID="LinkButton_edit" runat="server" Text="编辑" CommandArgument="edit"
                                                        CssClass="width36Button" />
                                                </td>--%>
                                            </tr>
                                            </table> </td>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tr></table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                            </tr>
                            <%--  <tr>
                            <th class="rt">
                                勋章图片：
                                <br />
                                （128*128,png）
                            </th>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <input id="Text_MedalSelectFile" type="text" readonly="readonly" />
                                        </td>
                                        <td>
                                            <div style="position: relative">
                                                <input id="Button_MedalSelectFile" type="button" value="选择文件" class="commonButton" />
                                                <asp:FileUpload ID="FileUpload_Medal" runat="server" onchange="Text_MedalSelectFile.value=this.value"
                                                    CssClass="fileUpload" />
                                            </div>
                                        </td>
                                        <td>
                                            &nbsp;<asp:Label ID="label_medalMessage" CssClass="Red" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>--%>
                            <%--  <tr>
                            <td class="rb">
                                &nbsp;
                            </td>
                            <td class="bot">
                                <asp:Repeater ID="Repeater_Medal" runat="server" OnItemCommand="Repeater_Medal_ItemCommand">
                                    <HeaderTemplate>
                                        <table>
                                            <tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <td style="border: 1px solid gray;">
                                            <table>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Image ID="Image_Medal" runat="server" Height="128px" ImageUrl="~/Images/smallimage.jpg"
                                                            Width="128px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left" colspan="3">
                                                        <asp:Label ID="Label_Medal" CssClass="Red" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right" colspan="3">
                                                        <asp:LinkButton ID="Button_Medal" runat="server" Text="删除" CssClass="width36Button"
                                                            OnClientClick="return confirm('此操作将删除对应勋章信息，确定删除吗？')" />
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
                        </tr>--%>
                            <%--     <tr>
                            <th class="rb">
                                勋章名称：
                            </th>
                            <td class="bot">
                                <asp:TextBox ID="TextBox_medalName" runat="server" Width="150px"></asp:TextBox>
                                <asp:Label CssClass="Red" ID="Label_MeadlName" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>--%>
                            <%--   <tr>
                            <th class="rb">
                                勋章描述：
                            </th>
                            <td class="bot">
                                <asp:TextBox ID="TextBox_medalDescription" runat="server" Width="300px" Height="100px"
                                    TextMode="MultiLine"></asp:TextBox>
                                <asp:Label ID="Label_Description" CssClass="Red" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>--%>
                            <%-- <tr>
                                <td></td>
                                <td align="center" style="padding: 10px 0;">
                                    <asp:Button ID="Button_Add" runat="server" OnClientClick="return VerifyNumber();"
                                        Text="添 加" CssClass="couponButtonSubmit" OnClick="Button_Add_Click" />
                                </td>
                            </tr>--%>
                        </table>
                    </asp:Panel>
                    <%--<asp:Panel ID="Panel_Role" runat="server" CssClass="panelSyle">
                        <table>
                            <tr>
                                <th colspan="3" class="dialogBox_th">
                                    <asp:Label ID="Label_Title" runat="server" Text="Label"></asp:Label>
                                </th>
                            </tr>
                            <tr>
                                <th>勋章名称：
                                </th>
                                <td>
                                    <asp:TextBox ID="TextBox_Name" runat="server" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>勋章描述：
                                </th>
                                <td>
                                    <asp:TextBox ID="TextBox_Description" runat="server" TextMode="MultiLine" Height="150px"
                                        Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                </td>
                                <td>
                                    <asp:Button ID="Button_confirm" runat="server" Text="确 定" CssClass="button" OnClientClick="return Pannel_Confrim();"
                                        OnClick="Button_confirm_Click" />
                                    &nbsp; &nbsp; &nbsp; &nbsp;
                                <asp:Button ID="Button_cancel" runat="server" Text="取 消" OnClick="Button_cancel_Click"
                                    CssClass="button" OnClientClick="HiddenConfirmWindow('Panel_Role')" />
                                </td>
                                <td>&nbsp;
                                </td>
                            </tr>
                        </table>
                        <asp:HiddenField ID="HiddenField_MedalId" runat="server" />
                    </asp:Panel>--%>
                </div>
            </div>
        </div>
        <%-- <asp:HiddenField ID="HiddenField_SmallUrl" runat="server" />
        <asp:HiddenField ID="HiddenField_BigUrl" runat="server" />--%>
        <%-- <asp:HiddenField ID="HiddenField_ShopSmallUrl" runat="server" />--%>
        <%-- <asp:HiddenField ID="HiddenField_ShopBigUrl" runat="server" />--%>
    </form>
</body>
</html>
