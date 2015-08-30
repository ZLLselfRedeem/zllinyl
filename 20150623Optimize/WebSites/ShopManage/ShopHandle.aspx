<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShopHandle.aspx.cs" Inherits="ShopManage_ShopHandle" %>

<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>门店审核</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script src="../Scripts/searchShop.js" type="text/javascript"></script>
    <style type="text/css">
        #allmap, #Gmap, #GDmap
        {
            width: 300px;
            height: 280px;
            overflow: hidden;
            margin: 0;
        }
        li:hover
        {
            text-decoration: underline;
            color: #39c;
            cursor: pointer;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            initData("shophandle");
        });
    </script>
</head>
<body>
    <form id="form1" runat="server" onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <uc2:HeadControl ID="HeadControl1" runat="server" headName="门店审核" navigationImage="~/images/icon/list.gif"
        navigationText="门店列表" navigationUrl="~/ShopManage/ShopManage.aspx" />
    <div id="box" class="box">
        <div class="tagMenu">
            <ul class="menu">
                <li>门店审核</li>
            </ul>
        </div>
        <div class="content">
            <div class="layout">
                <table class="table" cellpadding="0" cellspacing="0">
                    <tr>
                        <th>
                            门店搜索：
                        </th>
                        <td>
                            <input id="text" runat="server" type="text" onkeyup="keyTest()" />
                            <div id="init_date" runat="server" style="position: absolute; clear: both; border: 1px solid #000;
                                background-color: White">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            公司名称：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_companyName" Enabled="false" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            门店电话：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_shopTelePhone" Enabled="false" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            联系人：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_contactPerson" Enabled="false" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            联系人电话：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_contactPhone" Enabled="false" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            客户经理：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_manager" Enabled="false" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            新浪微博：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_sinaWeibo" Enabled="false" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            腾讯微博：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_qqWeibo" Enabled="false" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            微信公共帐号：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_wechatPublicName" Enabled="false" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            营业执照：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_shopBusinessLicense" Enabled="false" runat="server" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            营业时间：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_openTime" runat="server" Enabled="false" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            卫生许可证：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_shopHygieneLicense" Enabled="false" runat="server" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            门店地区：
                        </th>
                        <td>
                            <asp:DropDownList ID="DropDownList_provinceID" Enabled="false" runat="server" AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:DropDownList ID="DropDownList_cityID" Enabled="false" runat="server" AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:DropDownList ID="DropDownList_countyID" Enabled="false" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            详细地址：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_shopAddress" Enabled="false" runat="server" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            百度经度：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_LongitudeBaidu" Enabled="false" runat="server"></asp:TextBox>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <th>
                            百度纬度：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_LatitudeBaidu" Enabled="false" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <th>
                            门店状态：
                        </th>
                        <td>
                            <asp:DropDownList ID="DropDownList4" Enabled="false" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            店铺描述：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_shopDescription" Enabled="false" runat="server" Width="300"
                                TextMode="MultiLine" Height="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            店铺评分：
                        </th>
                        <td>
                            <asp:TextBox runat="server" ID="TextBox_shopRating" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            人均消费：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_accp" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            支持支付：
                        </th>
                        <td>
                            <asp:RadioButton ID="RadioButton_Support" GroupName="group" Text="支 持" Checked="true"
                                Enabled="false" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:RadioButton ID="RadioButton_NotSupport" GroupName="group" Text="暂不支持" runat="server"
                                Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            不支持支付原因：
                        </th>
                        <td>
                            <asp:TextBox ID="tb_notPaymentReason" runat="server" Width="300" Enabled="false"
                                TextMode="MultiLine" Height="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            点菜描述：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_orderDishDes" runat="server" Width="300" Enabled="false"
                                TextMode="MultiLine" Height="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            门店LOGO：
                        </th>
                        <td>
                            <asp:Image ID="img_ShopLogo" runat="server" Height="100px" Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            门店背景图片：
                        </th>
                        <td>
                            <asp:Image ID="img_ShopBgImg" runat="server" Height="100px" Width="200px" />
                        </td>
                    </tr>
                    <tr style="display: none">
                        <th>
                            门店状态：
                        </th>
                        <td>
                            <asp:DropDownList ID="DropDownList_shopStatus" runat="server" Enabled="false">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            审核情况：
                        </th>
                        <td>
                            <asp:DropDownList ID="DropDownList_IsHandle" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="审    核" OnClick="Button_ShopHandle_Click"
                                CssClass="button" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <uc1:CheckUser ID="CheckUser1" runat="server" />
    </form>
</body>
</html>
