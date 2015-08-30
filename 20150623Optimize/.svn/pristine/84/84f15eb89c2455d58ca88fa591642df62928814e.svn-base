<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShopChannelDishDetail.aspx.cs" Inherits="OrderOptimization_ChannelDetail" %>

<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>频道菜品详情</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="../Scripts/messages_cn.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //TabManage();
            //$("#btnUpdate").validate({
            //    rules: {
            //        text: { required: true},
            //        txtIndex: { required: true}
            //    },
            //    messages: {
            //        text: "请选择菜品",
            //        txtIndex: "请输入菜品排序号"
            //    }
            //});
            $("#txtPrice").attr("value", $("#hidPrice").val());
            $("#txtDiscount").attr("value", $("#hidDiscount").val());

        });

        function medalManage() {
            var str = $("#txtDishName").val();
            if (str == "") {
                return;
            }
            $.ajax({
                type: "Post",
                url: '../Award/AwardMsg.aspx/SearchDishMeau',
                data: JSON.stringify({
                    pageIndex: 1,
                    pageSize: 10,
                    key: str,
                    shopID: $("#hidShopID").val()
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d != "" && data.d != null) {
                        var returnJson = eval("(" + data.d + ")");
                        var strHtml = "<ul>";
                        for (var i = 0; i < returnJson.dishInfoDetailList.length; i++) {
                            if (i == 0) {
                                $("#init_date").html('');
                            }
                            strHtml += "<li onclick='selectDish(this)' id='"
                                               + returnJson.dishInfoDetailList[i].dishID + "' dishName='"
                                               + returnJson.dishInfoDetailList[i].dishName + "' dishPriceID='" + returnJson.dishInfoDetailList[i].dishPriceID + "'>&nbsp;&nbsp;"
                                               + returnJson.dishInfoDetailList[i].dishName + "&nbsp;&nbsp;</li>"
                        }
                        strHtml += "</ul>";
                        $("#init_date").append(strHtml);
                    }
                }
            });
        }

        function selectDish(dish) {
            $("#hidDishID").val(dish.id);
            $("#hidDishPriceId").val(dish.attributes["dishpriceid"].value);
            $("#txtDishName").val(dish.attributes["dishName"].value);
            $("#init_date").html('');
            GetDiscountAndPrice();
        }

        function GetDiscountAndPrice()
        {
            $.ajax({
                type: "Post",
                url: '../Handlers/commonAjaxPage.aspx/SearchDishPriceAndDiscount',
                data: JSON.stringify({
                    shopID: $("#hidShopID").val(),
                    dishPriceID: $("#hidDishPriceId").val()
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d != "" && data.d != null) {
                        var responseJson = jQuery.parseJSON(data.d);
                        $("#txtPrice").attr("value",responseJson.price);
                        $("#txtDiscount").attr("value",responseJson.discount);

                        $("#hidPrice").val(responseJson.price);
                        $("#hidDiscount").val(responseJson.discount);
                    }
                }
            });
        }
    </script>
</head>
<body scroll="no" style="overflow-y: hidden" onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <form id="form1" runat="server">
        <uc2:HeadControl ID="HeadControl1" runat="server" headName="频道菜品详情" />
        <div id="box" class="box">
            <div class="tagMenu">
                <%--<ul>
                    <asp:TextBox ID="TextBox_MerchantName" runat="server" Width="1500px" BorderWidth="0" ReadOnly="true" Enabled="false"></asp:TextBox>
                </ul>--%>
                <ul>
                    <li runat="server" id="liShowTitle" style="width:500px;text-align:left;">频道菜品详情</li>
                </ul>
            </div>
            <div class="content">
                <div class="layout">
                    <table class="table" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="text-align: right; background-color: #C9E7FF;" >菜品
                            </td>
                            <td>
                                <input id="txtDishName" runat="server" type="text" onkeyup="medalManage()" value="" />
                                <div id="init_date" runat="server" style="position: absolute; clear: both; border: 1px solid #000; background-color: White">
                                </div>
                                <asp:HiddenField ID="hidDishID" runat="server" />
                                <asp:HiddenField ID="hidDishPriceId" runat="server" />
                                <asp:HiddenField ID="hidPrice" runat="server" />
                                <asp:HiddenField ID="hidDiscount" runat="server" />
                                <asp:HiddenField ID="hidShopID" runat="server" Value="0" />
                                <%--<asp:HiddenField ID="hidOldIndex" runat="server" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; background-color: #C9E7FF;">价格
                            </td>
                            <td>
                                <asp:TextBox ID="txtPrice" runat="server" Width="155px" ReadOnly="true" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td style="text-align: right; background-color: #C9E7FF;">折扣
                            </td>
                            <td>
                                <asp:TextBox ID="txtDiscount" runat="server" Width="155px" ReadOnly="true" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; background-color: #C9E7FF;">排序
                            </td>
                            <td>
                                <asp:TextBox ID="txtIndex" runat="server" Width="155px" onkeyup="if(this.value.length==1){this.value=this.value.replace(/[^1-9]/g,'')}else{this.value=this.value.replace(/\D/g,'')}" onafterpaste="if(this.value.length==1){this.value=this.value.replace(/[^1-9]/g,'')}else{this.value=this.value.replace(/\D/g,'')}"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; background-color: #C9E7FF;">图片
                            </td>
                            <td>
                                <asp:FileUpload ID="fileUpload" runat="server" />
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="Button_Upload" runat="server" CssClass="tabButtonBlueClick" OnClick="Button_Upload_Click"
                                                Text="上传" />
                                <br />
                                <font color="red">（请上传小于1024KB的PNG格式图片,建议比例640*320）</font><br />
                                <asp:HiddenField ID="hidImageUrl" runat="server" />
                                <asp:Image ID="Big_Img" runat="server" Style="max-width: 200px; max-height: 200px;" ImageUrl="~/Images/bigimage.jpg" CssClass="innerTD" Height="200px" Width="200px" />
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; text-align: right; background-color: #C9E7FF;">摘要
                            </td>
                            <td>
                                <asp:TextBox ID="txtDishContent" runat="server" Width="400px" Wrap="true" TextMode="SingleLine" Maxlength="26"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                            <td>
                                <asp:Button ID="btnCancel" runat="server" Text="取   消" CssClass="button" OnClick="btnCancel_Click" />
                                <asp:Button ID="btnUpdate" runat="server" Text="保   存" CssClass="button" OnClick="btnSave_Click" OnClientClick="" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
