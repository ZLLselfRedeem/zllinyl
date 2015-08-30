<%@ Page Language="C#" AutoEventWireup="true" CodeFile="uxianQandA.aspx.cs" Inherits="WeChatPlatManage_uxianQandA" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>常见问答</title>
    <link href="../Css/css.css" rel="Stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:UpdatePanel ID="update1" runat="server">
            <ContentTemplate>
                <asp:ScriptManager ID="scmanager1" runat="server"></asp:ScriptManager>
                <asp:Panel ID="Panel_CustomerDetail" runat="server">
                    <div class="div_gridview" id="div_gridview" style="border-bottom: 2px solid #507CD1;">
                        <asp:GridView ID="GridView_QandA" runat="server" DataKeyNames="ID,question,answer,pubDateTime,UserName"
                            AutoGenerateColumns="False" SkinID="gridviewSkin" OnRowDeleting="GridView_QandA_RowDeleting">
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderText="序号" />
                                <asp:BoundField DataField="question" HeaderText="问题" />
                                <asp:BoundField DataField="answer" HeaderText="回答" />
                                <asp:BoundField DataField="pubDateTime" HeaderText="发布时间">
                                    <ItemStyle Width="180px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="UserName" HeaderText="发布者" />
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <img src="../Images/key_edit3.gif" alt="" />&nbsp;&nbsp;<asp:LinkButton ID="lbtnUpdate"
                                            runat="server" CausesValidation="False" OnClientClick="btnOptClick(this)" CommandName="Select"
                                            Text="修改" OnClick="lbtnUpdate_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Width="90px" />
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <img src="../Images/delete.gif" alt="" />&nbsp;&nbsp;<asp:LinkButton ID="lbtnDelete"
                                            runat="server" CausesValidation="False" CommandName="delete" Text="删除" OnClientClick="return confirm('你确定删除吗？')"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Width="90px" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                       
                        <asp:Label ID="Label_message" runat="server" CssClass="Red" Text=""></asp:Label>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="asp_page">
            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
                NextPageText="下一页" PageSize="5" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" PageIndexBoxClass="listPageText"
                CurrentPageButtonClass="currentButton" ShowPageIndexBox="Always" 
                CurrentPageButtonPosition="Center" onpagechanged="AspNetPager1_PageChanged">
            </webdiyer:AspNetPager>
        </div>
        <asp:Panel ID="Panel_MemberStatisticGridview" runat="server">
            <div class="div_gridview" id="div1">
                <div>
                    <table>
                        <tr>
                            <td><input type="button" class="tabButtonBlueClick" onclick="btnOptClick(this)" id="btNew" value="新增" /></td>
                            <td><input type="button" class="tabButtonBlueUnClick" id="btUpdate" value="修改" /></td>
                        </tr>
                    </table>
                </div>
                <asp:UpdatePanel ID="update2" runat="server">
                    <ContentTemplate>
                        <table id="tbUpdate" style="display: none; border: 1px solid silver;" runat="server">
                            <tr>
                                <th>
                                    序号:
                                </th>
                                <td>
                                    <asp:Label ID="lbl_ID" Text="" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    问题:
                                </th>
                                <td>
                                    <asp:TextBox ID="txtQuestionUpdate" TextMode="MultiLine" runat="server" Height="84px"
                                        Width="450px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    回答:
                                </th>
                                <td>
                                    <asp:TextBox ID="txtAnswerUpdate" TextMode="MultiLine" runat="server" Height="125px"
                                        Width="450px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                
                <table id="tbNew" style="border: 1px solid silver;" runat="server">
                    <tr>
                        <th>
                            问题:
                        </th>
                        <td>
                            <asp:TextBox ID="txtQuestionNew" TextMode="MultiLine" runat="server" Height="84px"
                                Width="450px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>回答:</th>
                        <td>
                            <asp:TextBox ID="txtAnswerNew" TextMode="MultiLine" runat="server" Height="125px"
                                Width="450px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <div class="divDetailTitle" style="overflow-y: hidden">
                    <asp:Button ID="Button_Save" runat="server" Text="新    增" 
                         CssClass="couponButtonSubmit" onclick="Button_Save_Click" />&nbsp;&nbsp;
                    <asp:Button ID="Button_Update" runat="server" Text="修    改" CssClass="couponButtonSubmit"
                        OnClientClick="return confirm('确定操作?')" onclick="Button_Update_Click" />
                </div>
            </div>
        </asp:Panel>
        
    <%--<asp:HiddenField ID="HiddenField_DinnerStarTime" runat="server" />
    <asp:HiddenField ID="HiddenField_DinnerEndTime" runat="server" />--%>
    </div>
    </form>
</body>
<script type="text/javascript">
    $(document).ready(function () {
        TabManage();
        GridViewStyle("GridView_QandA", "gv_OverRow");
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
        } else {
            $("#btUpdate").addClass("tabButtonBlueClick");
            $("#btNew").addClass("tabButtonBlueUnClick");
            $("#tbUpdate").css("display", "block");
            $("#tbNew").css("display", "none");
        }
    }
    </script>
</html>