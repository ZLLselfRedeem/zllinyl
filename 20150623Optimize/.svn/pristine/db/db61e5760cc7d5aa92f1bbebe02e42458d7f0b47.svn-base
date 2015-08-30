<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ActivityMessageDetail.aspx.cs"
    Inherits="Messages_ActivityMessageDetail" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>发布纯文本消息</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <style type="text/css">
        .auto-style1 {
            width: 198px;
        }
    </style>
    <script type="text/javascript">
        function gbcount(total, used, remain) {
            var max;
            max = total.value;
            if (document.getElementById("tbActivityExplain").value.length > max) {
                document.getElementById("tbActivityExplain").value = document.getElementById("tbActivityExplain").value.substring(0, max);
                used.value = max;
                remain.value = 0;
            }
            else {
                used.value = document.getElementById("tbActivityExplain").value.length;
                remain.value = max - used.value;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <uc3:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
             navigationText="发布纯文本消息" navigationUrl="" headName="发布纯文本消息" />
      <div class="content" id="divDetail" runat="server" style="width: 100%; display: '';">
            <div style="width: 80%">
                <table class="table" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <th class="auto-style1">活动名称:</th>
                                <td>
                                     <asp:TextBox ID="tbName" runat="server" Width="413px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th class="auto-style1">
                                    适用城市:
                                </th>
                                <td>
                                    <asp:DropDownList ID="ddlCity" runat="server" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <th class="auto-style1">
                                    活动标签:</th>
                                <td>
                                     <asp:DropDownList ID="ddlMessageFirstTitle" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <th class="auto-style1">
                                    消息类型:</th>
                                <td>
                                   <asp:RadioButton ID="rbPureText" Text="纯文本消息" GroupName="b" runat="server" OnCheckedChanged="rbPureText_CheckedChanged" AutoPostBack="True" /><asp:RadioButton ID="rbSpecialAdvertisement" Text="专题广告" GroupName="b" runat="server" OnCheckedChanged="rbSpecialAdvertisement_CheckedChanged" AutoPostBack="True" /><asp:RadioButton ID="rbRedEnvelopeAdvertisement" Text="红包广告" GroupName="b" runat="server" OnCheckedChanged="rbRedEnvelopeAdvertisement_CheckedChanged" AutoPostBack="True" /><asp:RadioButton ID="rbCommercialTenantPackage" Text="商户礼券" GroupName="b" runat="server" OnCheckedChanged="rbCommercialTenantPackage_CheckedChanged" AutoPostBack="True" />
                                </td>
                            </tr>
                            <tr runat="server" id="AdvertisementURL">
                                <th class="auto-style1">
                                    广告地址:
                                </th>
                                <td colspan="7">
                                    <asp:TextBox ID="tbAdvertisementURL" runat="server" Width="300px"></asp:TextBox>
                                </td>
                            </tr>
                             <tr runat="server" id="ShopName">
                                 <th class="auto-style1">
                                     店铺名称：
                                 </th>
                                 <td>
                                      <asp:TextBox ID="tbShopName" runat="server" Width="200px"></asp:TextBox><asp:Button ID="ButtonSearch" runat="server" CssClass="tabButtonBlueClick" CausesValidation="false" Text="查询" OnClick="ButtonSearch_Click" />
                                 </td>
                             </tr>
                             <tr id="Activity" runat="server">
                                 <th class="auto-style1">
                                     选择红包：
                                 </th>
                                 <td>
                                     <asp:RadioButtonList ID="rblActivity" runat="server" RepeatColumns="3">
                                     </asp:RadioButtonList>
                                 </td>
                             </tr>
                             <tr id="Coupon" runat="server">
                                 <th class="auto-style1">
                                     选择礼券：
                                 </th>
                                 <td>
                                     <asp:RadioButtonList ID="rblCoupon" runat="server" RepeatColumns="3">
                                     </asp:RadioButtonList>
                                 </td>
                             </tr>
                             <tr>
                                 <th class="auto-style1">
                                     活动Logo：
                                 </th>
                                 <td>
                                     <table width="100%" class="table">
                                    <tr>
                                        <td align="center">
                                            <asp:FileUpload ID="fileUploadLog" runat="server" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="Button_Upload_Log" runat="server" CssClass="tabButtonBlueClick" OnClick="Button_Upload_Log_Click"
                                                Text="上传" /><br />
                                            （注：支持图片格式JPG、PNG，图片大小不超过100K。图片尺寸不小于68*68，请按1:1比例制作图片）
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 400px; text-align: center">
                                            <asp:Image ID="Big_Img_Log" runat="server" ImageUrl="~/Images/bigimage.jpg" Width="68px"
                                                Height="68px" CssClass="innerTD" />
                                        </td>
                                    </tr>
                                </table>
                                 </td>
                             </tr>
                             <tr id="AdvertisementAddress" runat="server">
                                 <th class="auto-style1">
                                     活动广告图：
                                 </th>
                                 <td>
                                     <table width="100%" class="table">
                                    <tr>
                                        <td align="center">
                                            <asp:FileUpload ID="fileUpload" runat="server" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="Button_Upload" runat="server" CssClass="tabButtonBlueClick" OnClick="Button_Upload_Click"
                                                Text="上传" /><br />
                                            （注：支持图片格式JPG、PNG，图片大小不超过1M。图片尺寸不小于640*384，请按5:3比例制作图片）
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 400px; text-align: center">
                                            <asp:Image ID="Big_Img" runat="server" ImageUrl="~/Images/bigimage.jpg" Width="640px"
                                                Height="384px" CssClass="innerTD" />
                                        </td>
                                    </tr>
                                </table>
                                 </td>
                             </tr>
                            <tr id="ActivityExplain" runat="server">
                                <th>
                                    活动说明：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbActivityExplain" onKeyDown="gbcount(this.form.total,this.form.used,this.form.remain);" onKeyUp="gbcount(this.form.total,this.form.used,this.form.remain);" runat="server" Width="600" Height="150" Rows="5" TextMode="MultiLine"></asp:TextBox><p>最多字数：
                        <input maxlength="4" readonly="readonly" name="total" size="3" value="500" />
                        已用字数：
                        <input  maxlength="4" readonly="readonly" name="used" size="3" value="0" />
                        剩余字数：
                        <input maxlength="4" readonly="readonly" name="remain" size="3" value="500" />
                        </p>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Button Text="发布活动" Width="130px" Height="33px" CssClass="couponButtonSubmit" runat="server" ID="btnUpdate" OnClick="btnUpdate_Click" />
                               
                                </td>
                            </tr>
                        </table>
                <asp:HiddenField ID="HiddenField_Image" runat="server" />
                <asp:HiddenField ID="HiddenField_Image_Log" runat="server" />
                    </div>
                </div>
    </form>
</body>
</html>
