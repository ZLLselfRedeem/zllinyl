<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FreeCase.aspx.cs" Inherits="WeChatPlatManage_FreeCase" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>本期免单</title>
    <link href="../Css/css.css" rel="Stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="QueryTerms">
        <table>
            <tr>
                <td style="vertical-align: middle">
                    发布时间：
                </td>
                <td>
                    <asp:Button ID="Button_1day" runat="server" CssClass="tabButtonBlueUnClick" CommandName="1" OnClick="Button_day_Click"
                        Text="今天"></asp:Button>
                </td>
                <td>
                    <asp:Button ID="Button_yesterday" runat="server" CssClass="tabButtonBlueUnClick" OnClick="Button_day_Click"
                        CommandName="yesterday" Text="昨天"></asp:Button>
                </td>
                <td>
                    <asp:Button ID="Button_7day" runat="server" CssClass="tabButtonBlueUnClick" CommandName="7" OnClick="Button_day_Click"
                        Text="最近7天"></asp:Button>
                </td>
                <td>
                    <asp:Button ID="Button_14day" runat="server" CssClass="tabButtonBlueUnClick" CommandName="14" OnClick="Button_day_Click"
                        Text="最近14天"></asp:Button>
                </td>
                <td>
                    <asp:Button ID="Button_30day" runat="server" CssClass="tabButtonBlueUnClick" CommandName="30" OnClick="Button_day_Click"
                        Text="最近30天"></asp:Button>
                </td>
                <td>
                    <asp:Button ID="Button_self" runat="server" CssClass="tabButtonBlueUnClick" CommandName="self" OnClick="Button_day_Click"
                        Text="自定义"></asp:Button>
                </td>
                <td>
                    <asp:TextBox ID="TextBox_registerTimeStr" runat="server" CssClass="Wdate" onFocus="startDate(this)"
                        AutoPostBack="true" Width="85px"></asp:TextBox>
                    &nbsp;-&nbsp;
                    <asp:TextBox ID="TextBox_registerTimeEnd" runat="server" CssClass="Wdate" onFocus="endDate(this)"
                        AutoPostBack="true" Width="85px"></asp:TextBox>
                </td>
                <td><asp:HiddenField ID="Hidden_Day" runat='server' /></td>
            </tr>
        </table>
    </div>
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        <asp:Panel ID="Panel_CustomerDetail" runat="server">
            <div class="div_gridview" id="div_gridview">
                <asp:GridView ID="GridView_FreeCase" runat="server" DataKeyNames="ID,msgContent,pubDateTime,operaterID,status"
                    AutoGenerateColumns="False" SkinID="gridviewSkin" 
                    onrowdeleting="GridView_FreeCase_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="序号" />
                        <%--<asp:BoundField DataField="Title" HeaderText="主题" />--%>
                        <asp:BoundField DataField="msgContent" HeaderText="内容" />
                        <asp:BoundField DataField="pubDateTime" HeaderText="发布时间" />
                        <asp:BoundField DataField="operaterID" HeaderText="操作员" />
                        <asp:BoundField DataField="status" HeaderText="状态" />
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <img src="../Images/key_edit3.gif" alt="" />&nbsp;&nbsp;<asp:LinkButton ID="lbtnUpdate"
                                    runat="server" CausesValidation="False" OnClientClick="btnOptClick(this)" 
                                    CommandName="Select" Text="修改" onclick="lbtnUpdate_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <img src="../Images/delete.gif" alt="" />&nbsp;&nbsp;<asp:LinkButton ID="lbtnDelete"
                                    runat="server" CausesValidation="False" CommandName="delete" Text="删除" OnClientClick="return confirm('你确定删除吗？')"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <div class="asp_page">
                    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
                        NextPageText="下一页" PageSize="10" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                        TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                        NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" PageIndexBoxClass="listPageText"
                        CurrentPageButtonClass="currentButton" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center">
                    </webdiyer:AspNetPager>
                </div>
                <asp:Label ID="Label_message" runat="server" CssClass="Red" Text=""></asp:Label>
                <hr size="1" style="border: 1px #cccccc dashed;" />
            </div>
        </asp:Panel>
        </ContentTemplate>
        </asp:UpdatePanel>

        <asp:Panel ID="Panel_MemberStatisticGridview" runat="server">
            <div class="div_gridview" id="div_othergridview">
                <div>
                    <table>
                        <tr>
                            <td><input type="button" class="tabButtonBlueClick" onclick="btnOptClick(this)" id="btNew" value="新增" /></td>
                            <td><input type="button" class="tabButtonBlueUnClick" id="btUpdate" value="修改" /></td>
                        </tr>
                    </table>
                </div>
                <asp:UpdatePanel ID="updatepanel2" runat="server">
                <ContentTemplate>
                <table id="tbUpdate" style="display:none;border:1px solid silver;" runat="server">
                    <tr>
                        <th>序号:</th>
                        <td><asp:Label ID="lbl_ID" Text="" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <th>内容:</th>
                        <td><asp:TextBox ID="txtContentUpdate" TextMode="MultiLine" runat="server" Height="125px" 
                                Width="450px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th>状态:</th>
                        <td><asp:CheckBox ID="validSet" Text="是否过期" CausesValidation="false" runat="server" /></td>
                    </tr>
                </table>
                <table id="tbNew" style="border:1px solid silver;" runat="server">
                    <tr>
                        <th>内容:</th>
                        <td><asp:TextBox ID="txtContentNew" TextMode="MultiLine" runat="server" Height="125px" 
                                Width="450px"></asp:TextBox></td>
                    </tr>
                </table>
                
                <div class="divDetailTitle" style="overflow-y: hidden">
                     <asp:Button ID="Button_Save" runat="server" Text="新    增" 
                         CssClass="couponButtonSubmit" onclick="Button_Save_Click" />&nbsp;&nbsp;
                    <asp:Button ID="Button_Update" runat="server" Text="修    改" CssClass="couponButtonSubmit"
                        OnClientClick="return confirm('确定操作?')" onclick="Button_Update_Click" />
                    <asp:Label ID="Label1" runat="server" CssClass="Red" Text=""></asp:Label>
                </div>
                
                </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </asp:Panel>
    <asp:HiddenField ID="HiddenField_UD" runat="server" />
    </div>
    </form>
</body>
<script type="text/javascript">
    $(document).ready(function () {
        TabManage();
        GridViewStyle("GridView_FreeCase", "gv_OverRow");
        if ($("#Label_message").text() != "") {
            $("#btUpdate").css("display", "none");
        }
        else {
            $("#btUpdate").css("display", "block");
        }
    });
    var startDate = function (elem) {
        WdatePicker({
            el: elem,
            isShowClear: false,
            maxDate: '#F{$dp.$D(\'TextBox_registerTimeEnd\')||%y-%M-%d}',
            onpicked: function (dp) {
                elem.blur();
            },
            skin: 'whyGreen'
        });
    };
    var endDate = function (elem) {
        WdatePicker({
            el: elem,
            isShowClear: false,
            maxDate: '%y-%M-{%d+1}',
            minDate: '#F{$dp.$D(\'TextBox_registerTimeStr\')}',
            onpicked: function (dp) { elem.blur() },
            skin: 'whyGreen'
        });
    }
    function btnOptClick(obj) {
        $("#btNew").attr("class", "");
        $("#btUpdate").attr("class", "");
        if (obj.value == "新增") {
            $("#btNew").addClass("tabButtonBlueClick");
            $("#btUpdate").addClass("tabButtonBlueUnClick");
            $("#tbNew").css("display", "block");
            $("#tbUpdate").css("display", "none");
            $("#HiddenField_UD").val("N");
        } else {
            $("#btUpdate").addClass("tabButtonBlueClick");
            $("#btNew").addClass("tabButtonBlueUnClick");
            $("#tbUpdate").css("display", "block");
            $("#tbNew").css("display", "none");
            $("#HiddenField_UD").val("U");
        }
    }
    </script>
</html>
