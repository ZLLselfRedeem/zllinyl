<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HomeTitleAdd.aspx.cs" Inherits="HomeNew_HomeTitleAdd" %>

<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>一级栏目编辑</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="../Scripts/messages_cn.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        $().ready(function () {
            TabManage();
            $("#form1").validate({
                rules: {
                    TextBox_TitleNamedefault: { required: true, rangelength: [1, 20] },
                    TextBox_ownedCompany: { required: true, rangelength: [1, 20] },
                    TextBox_companyTelePhone: { digits: true, rangelength: [6, 12] },
                    TextBox_contactPerson: { maxlength: 5 },
                    TextBox_contactPhone: { digits: true, rangelength: [6, 12] },
                    TextBox_companyAddress: { maxlength: 20 }
                },
                messages: {
                    TextBox_TitleNamedefault: "请输入1到20位栏目名称",
                    TextBox_ownedCompany: "请输入1到20位公司名称",
                    TextBox_companyTelePhone: "请输入合法电话号码",
                    TextBox_contactPerson: "联系人不超过5个字符",
                    TextBox_contactPhone: "请输入合法电话号码",
                    TextBox_companyAddress: "地址不超过20个字符"
                }
            });
        });

    </script>
</head>
<body scroll="no" style="overflow-y:hidden" onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <form id="form1" runat="server">
    <uc2:HeadControl ID="HeadControl1" runat="server" headName="一级栏目编辑" navigationImage="~/images/icon/list.gif" />
    <div id="box" class="box">
        <div class="tagMenu">
            <ul class="menu">
                <li>一级栏目编辑</li>
            </ul>
        </div>
        <div class="content">
            <div class="layout">
                <table class="table" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style=" text-align:right; background-color: #C9E7FF;">
                            一级栏目名称
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox_TitleName" runat="server" Width="155px" ></asp:TextBox>
                            <font color="red">（建议4个以下中文字符）</font>
                        </td>
                    </tr>
                         <tr>
                        <td style=" text-align:right; background-color: #C9E7FF;">
                            城市
                        </td>
                        <td>
                            <asp:DropDownList ID="TextCityName" runat="server" Width="155px"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style=" text-align:right; background-color: #C9E7FF;">
                            栏目顺序
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox_TitleIndex" runat="server" Width="155px" onkeyup="if(this.value.length==1){this.value=this.value.replace(/[^1-9]/g,'')}else{this.value=this.value.replace(/\D/g,'')}" onafterpaste="if(this.value.length==1){this.value=this.value.replace(/[^1-9]/g,'')}else{this.value=this.value.replace(/\D/g,'')}"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style=" text-align:right; background-color: #C9E7FF;">
                            栏目类型
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox_TitleType" runat="server" Width="155px" ReadOnly="True" enable="false" BorderStyle="None"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style=" text-align:right; background-color: #C9E7FF;">
                            栏目状态
                        </td>
                        <td>
                            <asp:RadioButtonList ID="radListStatus" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1">客户端上线</asp:ListItem>
                                <asp:ListItem Value="0">客户端下线</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnCancel" runat="server" Text="取   消"   CssClass="button" OnClick="btnCancel_Click"/>
                            <asp:Button ID="btnUpdate" runat="server" Text="保   存"   CssClass="button" OnClick="btnSave_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <%--<uc1:CheckUser ID="CheckUser1" runat="server" />--%>
    </form>
</body>
</html>
