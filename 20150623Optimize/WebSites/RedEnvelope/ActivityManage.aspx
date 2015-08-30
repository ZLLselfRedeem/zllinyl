﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ActivityManage.aspx.cs" Inherits="RedEnvelope_ActivityManage" ValidateRequest="false" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>红包活动管理</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">

        function Check() {
            var err = "";
            var msg = "请注意以下内容：\r\n"
            if (document.getElementById("txbName").value.length == 0) {
                err += "【活动名称不能为空】\r\n";
            }
            if (document.getElementById("txbName").value.length > 10) {
                err += "【活动名称不能超过10个字符】";
            }
            if (document.getElementById("txbBeginTime").value.length == 0) {
                err += "【开始时间不能为空】\r\n";
            }
            if (document.getElementById("txbEndTime").value.length == 0) {
                err += "【结束时间不能为空】\r\n";
            }
            if (err.length > 0) {
                alert(msg + err);
                return false;
            }
            else {
                return true;
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <uc2:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
                navigationText="" navigationUrl="" headName="红包活动管理" />
            <div class="content" id="divList" runat="server">
                <div class="layout">
                    <div class="QueryTerms">
                        <table style="width: 100%" cellpadding="5" cellspacing="5">
                            <tr>
                                <td style="width: 8%">活动名称
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txbNameQuery"></asp:TextBox>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnQuery" runat="server" Text="搜索" CssClass="button" OnClick="btnQuery_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnAdd" runat="server" Text="新建" CssClass="button" OnClick="btnAdd_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <asp:Panel ID="Panel_ActivityList" runat="server">
                        <div class="div_gridview">
                            <asp:GridView runat="server" ID="gdvActivity" AutoGenerateColumns="False" CssClass="gridview"
                                SkinID="gridviewSkin" DataKeyNames="enabled,expirationTimeRule,ruleValue,activityType,beginTime,endTime,redEnvelopeEffectiveBeginTime,redEnvelopeEffectiveEndTime" OnDataBound="gdvActivity_DataBound">
                                <Columns>
                                    <asp:BoundField DataField="activityId" HeaderText="活动ID" Visible="false" />
                                    <asp:BoundField DataField="name" HeaderText="活动名称" />
                                    <asp:TemplateField HeaderText="活动起止时间">
                                        <ItemTemplate>
                                            <asp:Label ID="lbActivityTime" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="红包有效起止时间">
                                        <ItemTemplate>
                                            <asp:Label ID="lbRedEnvelopeTime" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="activityType" HeaderText="活动类别" />
                                    <asp:BoundField DataField="expirationTimeRule" HeaderText="红包有效期规则" />
                                    <asp:TemplateField HeaderText="状态">
                                        <ItemTemplate>
                                            <asp:Label ID="lbEabled" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <img src="../Images/key_edit3.gif" />
                                            <asp:LinkButton runat="server" ID="llnkbtnEnable" CommandName="enable" OnCommand="lnkbtn_OnCommand"
                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem,"activityId") %>'>启/停</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <img src="../Images/key_edit2.gif" alt="" />
                                            <asp:LinkButton runat="server" ID="lnkbtnModify" CommandName="modify" OnCommand="lnkbtn_OnCommand"
                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem,"activityId") %>'>编辑</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <img src="../Images/icon-viewpic.gif" alt="" />
                                            <asp:LinkButton runat="server" ID="lnkbtnShare" CommandName="share" OnCommand="lnkbtn_OnCommand"
                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem,"activityId") %>'>分享配置</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <img src="../Images/key_delete.gif" alt="" />
                                            <asp:LinkButton runat="server" ID="lnkbtnDel" CommandName="del" OnCommand="lnkbtn_OnCommand"
                                                OnClientClick="return confirm('确认删除？');" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"activityId") %>'>删除</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:Panel ID="Panel1" CssClass="gridviewBottom" runat="server">
                                <div class="asp_page">
                                    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
                                        NextPageText="下一页" PageSize="20" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                                        TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                                        NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" PageIndexBoxClass="listPageText"
                                        CurrentPageButtonClass="currentButton" ShowPageIndexBox="Always" currentpagebuttonposition="Center"
                                        OnPageChanged="AspNetPager1_PageChanged">
                                    </webdiyer:AspNetPager>
                                </div>
                            </asp:Panel>
                        </div>
                    </asp:Panel>
                </div>
            </div>
            <div class="content" id="divDetail" runat="server" style="display: none">
                <div class="layout">
                    <table class="table" cellpadding="0" cellspacing="0">
                        <tr>
                            <th>活动名称
                            </th>
                            <td>
                                <asp:TextBox runat="server" ID="txbName"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>活动类别
                            </th>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlActivityType" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1" Text="大红包"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="天天红包"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="节日免单红包"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="赠送红包"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="抽奖红包"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <%--<tr>
                            <th>活动规则
                            </th>
                            <td>范例：<br />
                                &nbsp;&nbsp;&nbsp;&nbsp;＜li＞ XXXXXX ＜/li＞<br />
                                &nbsp;&nbsp;&nbsp;&nbsp;＜li＞ XXXXXX ＜/li＞<br />
                                &nbsp;&nbsp;&nbsp;&nbsp;＜li＞ XXXXXX ＜/li＞<br />
                                <br />
                                <asp:TextBox runat="server" ID="txbActivityRule" TextMode="MultiLine" Height="100px" Width="800px"></asp:TextBox>
                            </td>
                        </tr>--%>
                        <tr>
                            <th>红包有效期规则</th>
                            <td>
                                <asp:RadioButton ID="rb_1" GroupName="rblExpirationTimeRule" Text="顺延N日" runat="server" />
                                <asp:TextBox runat="server" ID="txbRuleValue"></asp:TextBox><br />
                                -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------<br />
                                <asp:RadioButton ID="rb_2" GroupName="rblExpirationTimeRule" Text="统一时间点" runat="server" />
                                <asp:TextBox ID="txtRedEnvelopeEffectiveBeginTime" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd 00:00:00'})"
                                    Width="150px"></asp:TextBox>-
                                 <asp:TextBox ID="txtRedEnvelopeEffectiveEndTime" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd 23:59:59'})"
                                     Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>开始时间
                            </th>
                            <td>
                                <asp:TextBox ID="txbBeginTime" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd HH:mm:ss'})"
                                    Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>结束时间
                            </th>
                            <td>
                                <asp:TextBox ID="txbEndTime" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd HH:mm:ss'})"
                                    Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button runat="server" ID="btnSave" Text="保存" CssClass="button" OnClick="btnSave_Click"
                                    OnClientClick="return Check();" />
                                <asp:Button runat="server" ID="btnCancle" Text="取消" CssClass="button" OnClick="btnCancle_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <uc1:CheckUser ID="CheckUser1" runat="server" />
        </div>
    </form>
</body>
</html>
