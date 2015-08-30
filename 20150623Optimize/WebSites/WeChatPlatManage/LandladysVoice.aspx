<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LandladysVoice.aspx.cs" Inherits="WeChatPlatManage_LandladysVoice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>亲聆老板娘</title>
    <link href="../Css/css.css" rel="Stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        function GetAccessToken() {
            var AppId = $("#txtAppid").val();
            var AppSecret = $("#txtAppSecret").val();
            if (AppId == "" || AppSecret == "") {
                alert("请输入AppId和AppSecret.");
                return false;
            }

            $.ajax({
                type: "Post",
                url: "../Wechatashx/GetAccessToken.ashx?AppId=" + AppId + "&AppSecret=" + AppSecret,
                success: function (data) {
                    try {
                        var retObj = eval(data);
                        $("#txtToken").val(retObj.access_token);
                    }
                    catch (e)
                    { alert(e.Message); }
                },
                Error: function () {
                    alert("出错了");

                }
            });
        }
        function UploadVoiceFile() {
            var token = $("#txtToken").val();
            var selectID = $("#selectID").val();
            if (token == "" || selectID == "") {
                alert("请输入Token并选择要上传的语音文件.");
                return false;
            }

            $.ajax({
                type: "Post",
                url: "../Wechatashx/UploadVoiceFile.ashx?token=" + token + "&selectID=" + selectID,
                success: function (data) {
                    try {
                        alert(data);
                        var retObj = eval(data);
                        //alert(retObj.status);
                        $("#divResult").text("操作状态:" + retObj.status);
                    }
                    catch (e)
                    { alert(e.Message); }
                },
                Error: function () {
                    alert("出错了");

                }
            });
        }
    </script>
    <style type="text/css">
        li
        {
            white-space: nowrap;
        }
        .tbButtons input
        {
            width: 90%;
            height: 32px;
            font-size: larger;
        }
        .btnButton
        {
            background-color: rgb(211, 220, 224);
            border: 1px solid rgb(120, 120, 120);
            cursor: pointer;
            font-size: 1.2em;
            font-weight: 300;
            padding: 7px;
            margin-right: 8px;
            width: auto;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <asp:ScriptManager ID="sc1" runat="server">
            </asp:ScriptManager>
            <div>
                <asp:Panel ID="Panel_CustomerDetail" runat="server">
                    <div class="div_gridview" id="div_gridview" style="border-bottom: 2px solid #507CD1;">
                        <asp:GridView ID="GridView_Info" runat="server" DataKeyNames="ID,fileName,remark,pubDateTime,UserName"
                            AutoGenerateColumns="False" SkinID="gridviewSkin">
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderText="序号" />
                                <asp:BoundField DataField="fileName" HeaderText="文件名称" />
                                <asp:BoundField DataField="remark" HeaderText="备注" />
                                <asp:BoundField DataField="pubDateTime" HeaderText="发布时间">
                                    <ItemStyle Width="180px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="UserName" HeaderText="发布者" />
                                <%--<asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <img src="../Images/delete.gif" alt="" />&nbsp;&nbsp;<asp:LinkButton ID="lbtnDelete"
                                    runat="server" CausesValidation="False" CommandName="delete" Text="删除" OnClientClick="return confirm('你确定删除吗？')"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="90px" />
                        </asp:TemplateField>--%>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelec" Text="选择" runat="server" AutoPostBack="True" OnCheckedChanged="chkSelec_CheckedChanged" />
                                    </ItemTemplate>
                                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:Label ID="Label_message" runat="server" CssClass="Red" Text=""></asp:Label>
                    </div>
                </asp:Panel>
            </div>
            <asp:HiddenField ID="selectID" Value="" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="QueryTerms">
        <table style="width: 550px;">
            <tr>
                <th align="right">
                    语音文件地址:
                </th>
                <td style="width: 300px;">
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="260px" />
                </td>
                <td>
                    <asp:Button ID="btn_Save" runat="server" OnClick="btn_Save_Click" Text="上传" Style="border-style: none;
                        border-color: inherit; border-width: medium; width: 70px; background-color: #ffffff;
                        color: #1874CD; text-align: center; line-height: 25px; font-weight: bold; cursor: pointer;
                        height: 21px;" />
                </td>
            </tr>
            <tr>
                <th align="right">
                    备注:
                </th>
                <td colspan="2">
                    <asp:TextBox ID="txtRemark" TextMode="MultiLine" runat="server" Width="100%" Height="109px"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div class="QueryTerms">
        <%--<asp:Panel ID="Panel_MemberStatisticGridview" runat="server">
            <div class="div_gridview" id="div_othergridview">
                <div class="divDetailTitle" style="overflow-y: hidden">
                    <asp:Button ID="Button_Save" runat="server" Text="保存" CssClass="couponButtonSubmit"
                        OnClientClick="return confirm('确定操作?')" onclick="Button_Save_Click" />
                </div>
            </div>
        </asp:Panel>
        <asp:HiddenField ID="HiddenField_up" runat="server" />--%>
        <h3>上传多媒体文件到微信平台</h3>
        <div>
             <table style="line-height:38px;width:600px;">
                   <tr><td style="width:120px;text-align:right;">AppId:</td><td><input type="text" id="txtAppid" style="width:90%;font-size:larger;" /></td></tr>
                   <tr><td style="width:120px;text-align:right;">AppSecret:</td><td><input type="text" id="txtAppSecret" style="width:90%;font-size:larger;" /></td></tr>
             </table>
             <input type="button" id="btnGetAccessToken" value="获取AccessToken" onclick="GetAccessToken()" class="btnButton" />
        </div>
        <table style="line-height:38px;">
              <tr>
                  <td style="width:120px;text-align:right;">当前Token:</td>
                  <td><asp:TextBox ID="txtToken" Width="800px" style="width:800px;font-size:larger;" runat="server"></asp:TextBox></td>
              </tr>
        </table>
         <br />
         <div style="line-height:35px;">
              <input type="button" id="btnUplodVoiceFile" onclick="UploadVoiceFile()" value="上传文件到微信平台" class="btnButton" />
              <br />
              <div id="divResult">操作状态:--</div>
         </div>
    </div>
    
    </form>
</body>
<script type="text/javascript">
    $(document).ready(function () {
        TabManage();
        GridViewStyle("GridView_Info", "gv_OverRow");
    });
</script>
</html>