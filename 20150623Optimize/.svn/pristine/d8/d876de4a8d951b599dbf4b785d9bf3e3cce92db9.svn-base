<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ActivityShareConfig.aspx.cs" Inherits="RedEnvelope_ActivityShareConfig"  ValidateRequest="false"%>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <title>活动分享配置</title>
    <style type="text/css">
        .auto-style1 {
            height: 40px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnUpload").click(function () {
                var str = $("#fileUpload").val();
                var pos = str.lastIndexOf(".");
                var lastname = str.substring(pos, str.length);
                if (lastname.length == 0) {
                    alert("请先点击[浏览]按钮选择要上传的图片！");
                    return false;
                }
                if (lastname.length > 0 && lastname.toLowerCase() != ".png" && lastname.toLowerCase() != ".jpg") {
                    alert("您上传的文件类型为" + lastname + "，图片必须为 .png 或 .jpg 类型");
                    return false;
                }
            });

            $("#btnSave").click(function () {
                if ($("#txbShareText").val() == "") {
                    alert("请先填写分享文字");
                    return false;
                }
                if ($("#txbShareText").val().length > 50) {
                    alert("字数太多啦！请不要超过50个");
                    return false;
                }
            })
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <uc2:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
                navigationText="" navigationUrl="" headName="活动分享配置" />
            <div class="content" id="divList" runat="server">
                <div class="layout">
                    <table class="table" cellpadding="0" cellspacing="0" style="width: 90%">
                        <tr>
                            <th style="width: 10%">分享图片  
                            </th>
                            <td>（格式png或jpg，比例1:1，尺寸100*100，大小不超过10K）
                                <br />
                                <br />
                                <asp:FileUpload runat="server" ID="fileUpload" Width="380px" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button runat="server" ID="btnUpload" Text="上传图片" CssClass="button" OnClick="btnUpload_Click" />
                            </td>
                        </tr>
                        <tr>
                            <th style="width: 10%">分享文字  
                            </th>
                            <td>
                                说明：本次领到金额用<span style="color:mediumvioletred">{0}</span>代替，累计金额用<span style="color:mediumvioletred">{1}</span>代替
                                <br />
                                范例：悠先红包天天抢，又领到{0}元，累积领到{1}元啦，明天继续~
                                <br /><br />
                                <asp:TextBox runat="server" ID="txbShareText" TextMode="MultiLine" Height="45px" Width="375px"></asp:TextBox>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button runat="server" ID="btnSave" Text="上传文字" CssClass="button" OnClick="btnSave_Click" />
                            </td>
                        </tr>
                        <tr>
                            <th>活动规则
                            </th>
                            <td>范例：<br />
                                &nbsp;&nbsp;&nbsp;&nbsp;＜li＞ XXXXXX ＜/li＞<br />
                                &nbsp;&nbsp;&nbsp;&nbsp;＜li＞ XXXXXX ＜/li＞<br />
                                &nbsp;&nbsp;&nbsp;&nbsp;＜li＞ XXXXXX ＜/li＞<br />
                                <br />
                                <asp:TextBox runat="server" ID="txbActivityRule" TextMode="MultiLine" Height="100px" Width="800px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button runat="server" ID="btnActivityRule" Text="上传活动规则" Width="100px" 
                                    onclick="btnActivityRule_Click"  />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="auto-style1">------------------------------------列表展示区------------------------------------</td>
                        </tr>
                        <tr>
                            <th style="width: 10%">类别  
                            </th>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlType" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                    <asp:ListItem Value="2" Text="文字"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="图片"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="活动规则"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView runat="server" ID="gdvActivity" AutoGenerateColumns="False" CssClass="gridview"
                                    SkinID="gridviewSkin" OnDataBound="gdvActivity_DataBound" DataKeyNames="type,remark">
                                    <Columns>
                                        <asp:BoundField DataField="id" HeaderText="ID" Visible="false" />
                                        <asp:BoundField DataField="type" HeaderText="类别" />
                                        <asp:BoundField DataField="remark" HeaderText="内容" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Image ID="imgDish" runat="server" Height="50px" Width="50px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <img src="../Images/key_delete.gif" alt="" />
                                                <asp:LinkButton runat="server" ID="lnkbtnDel" CommandName="del" OnCommand="lnkbtn_OnCommand"
                                                    OnClientClick="return confirm('确认删除？');" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"id") %>'>删除</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <div class="asp_page">
                                    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
                                        NextPageText="下一页" PageSize="10" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                                        TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                                        NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" PageIndexBoxClass="listPageText"
                                        CurrentPageButtonClass="currentButton" ShowPageIndexBox="Always" currentpagebuttonposition="Center"
                                        OnPageChanged="AspNetPager1_PageChanged">
                                    </webdiyer:AspNetPager>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:LinkButton runat="server" ID="lkbRedirect" PostBackUrl="~/RedEnvelope/ActivityManage.aspx" Text="返回活动配置列表" Font-Size="Medium"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
