<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MealAdd.aspx.cs" Inherits="Meal_MealAdd"  ValidateRequest="false"  %>


<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <title>广告添加</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <style type="text/css">
        .innerTD
        {
            margin: 0 auto;
        }
        .tblBorder
        {
            border: 1px solid #c0c0c0;
        }
        .tblBorder td
        {
            border: 1px solid #c0c0c0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
     <uc3:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
        navigationText="返回列表" navigationUrl="~/Meal/MealManager.aspx" headName="套餐添加" />
    <div id="box" class="box">
        <div class="content">
            <div class="layout"> 
                    <table cellspacing="0" cellpadding="0" width="900px" class="table">
                        <tr>
                            <th style="width: 20%; text-align: center">
                                套餐名称:
                            </th>
                            <td style="text-align: left; width: 95%">
                                <asp:TextBox ID="TextBoxMealName" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="TextBoxMealName" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr> 
                        <tr  >
                            <th style="width: 20%; text-align: center">
                                所属公司:
                            </th>
                            <td style="text-align: left; width: 95%">
                                <asp:DropDownList ID="DropDownListCompanys" runat="server" AutoPostBack="True" DataTextField="companyName" DataValueField="companyID" OnSelectedIndexChanged="DropDownListCompanys_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:Label ID="LabelCompany" Visible="false" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr  >
                            <th style="width: 20%; text-align: center">
                                所属门店:
                            </th>
                            <td style="text-align: left; width: 95%;">
                                <asp:RadioButtonList ID="RadioButtonListShop" runat="server" RepeatDirection="Horizontal" DataTextField="shopName" DataValueField="shopID"
                                    RepeatColumns="4">
                                </asp:RadioButtonList>
                                 <asp:Label ID="LabelShop" Visible="false" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>  
                        <tr>
                            <th style="width: 20%; text-align: center">套餐价格：</th>
                            <td style="text-align: left; width: 95%;"> 
                                <asp:TextBox ID="TextBoxPrice" runat="server"  Width="200px" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="TextBoxPrice" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="TextBoxPrice" ErrorMessage="*" ForeColor="Red" Type="Double"></asp:RangeValidator>
                            </td>
                        </tr>
                        <tr>
                            <th style="width: 20%; text-align: center">原价：</th>
                            <td style="text-align: left; width: 95%;">
                                <asp:TextBox ID="TextBoxOriginalPrice" runat="server"  Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="TextBoxOriginalPrice" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="TextBoxOriginalPrice" ErrorMessage="*" ForeColor="Red" Type="Double"></asp:RangeValidator>
                            </td>
                        </tr>
                        <tr>
                            <th style="width: 20%; text-align: center">建议人数：</th>
                            <td style="text-align: left; width: 95%;"> 
                                <asp:TextBox ID="TextBoxSuggestion" runat="server"  Width="200px" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="TextBoxSuggestion" ForeColor="Red"></asp:RequiredFieldValidator>
                                范例：建议6-8人使用</td>
                        </tr>
                        <tr>
                            <th style="width: 20%; text-align: center">套餐序号：</th>
                            <td style="text-align: left; width: 95%;"> 
                                <asp:TextBox ID="TextBoxOrderNumber" runat="server"  Width="200px" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" ControlToValidate="TextBoxOrderNumber" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="TextBoxOrderNumber" ErrorMessage="*" ForeColor="Red" Type="Integer" Display="Dynamic" MaximumValue="1000" MinimumValue="0"></asp:RangeValidator>
                                建议使用：10，20，30……</td>
                        </tr>
                        <tr>
                            <th style="width: 20%; text-align: center">菜单：</th>
                            <td style="text-align: left; width: 95%;">
                                <asp:TextBox ID="TextBoxMenu" runat="server" Width="525px" Height="152px" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ControlToValidate="TextBoxMenu" ForeColor="Red"></asp:RequiredFieldValidator>
                                <br />
                                范例：<br /> &nbsp;&nbsp;&nbsp;&nbsp;&lt;li&gt;&lt;span&gt; 凉菜： &lt;/span&gt;XXXXXX &lt;/li&gt;<br /> &nbsp;&nbsp;&nbsp;&nbsp;&lt;li&gt;&lt;span&gt; 热菜： &lt;/span&gt;XXXXXX &lt;/li&gt;<br /> 
                                <br />
                            </td>
                        </tr>
                        <tr runat="server" id="trIsActive" visible="false">
                            <th style="width: 20%; text-align: center">是否启用：</th>
                            <td style="text-align: left; width: 95%;">
                                <asp:RadioButtonList ID="RadioButtonListIsActive" runat="server"  RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">上架</asp:ListItem>
                                    <asp:ListItem Selected="True"  Value="0">下架</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr runat="server" id="trImage" visible="false">
                            <th style="width: 20%; text-align: center">套餐图片：</th>
                            <td style="text-align: left; width: 95%;">
                                <table class="table" width="100%">
                                    <tr>
                                        <td >
                                            <asp:FileUpload ID="fileUpload" runat="server" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="Button_Upload" runat="server" CssClass="tabButtonBlueClick" OnClick="Button_Upload_Click" Text="上传" />
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td  >
                                            <asp:Image ID="Big_Img" runat="server" CssClass="innerTD" Height="256px" ImageUrl="~/Images/bigimage.jpg" Width="320px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            
                            <td colspan="2" style=" text-align: center">
                                <asp:Button ID="ButtonAdd" runat="server" class="couponButtonSubmit" OnClick="ButtonAdd_Click" Text="增加套餐" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="ButtonCancel" runat="server" class="couponButtonSubmit" OnClientClick="window.history.back();return false;" Text="返回"   />
                            </td>
                        </tr>
                    </table> 
                <asp:HiddenField ID="HiddenField_Image" runat="server" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
