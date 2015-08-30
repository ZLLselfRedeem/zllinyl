<%@ Page Language="C#" AutoEventWireup="true" CodeFile="clientError.aspx.cs" Inherits="ServerLog_clientError" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>客户端错误日志</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            GridViewStyle("gr_ClientError", "gv_OverRow");
        });
    </script>
</head>
<body>
    <form id="form1" runat="server" onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <uc3:HeadControl ID="HeadControl1" runat="server" navigationImage="" navigationText=""
        navigationUrl="" headName="客户端错误日志" />
    <div id="box" class="box">
        <div class="content">
            <div class="layout">
                <div class="QueryTerms">
                    <table>
                        <tr>
                            <td>
                                <label>
                                    开始时间：</label><asp:TextBox ID="TextBox_startTime" runat="server" class="Wdate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"
                                        Style="width: 140px"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;&nbsp;
                                <label>
                                    结束时间：<asp:TextBox ID="TextBox_endTime" class="Wdate" runat="server" Style="width: 140px"
                                        onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox>
                                </label>
                            </td>
                            <td>
                                <asp:Button ID="btn_Search" class="button" runat="server" Text="查   询" OnClick="btn_Search_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:Panel ID="Panel_Log" runat="server" CssClass="div_gridview">
                    <asp:GridView ID="gr_ClientError" runat="server" DataKeyNames="id" AutoGenerateColumns="False"
                        SkinID="gridviewSkin" ClientIDMode="Static" OnSelectedIndexChanged="gr_ClientError_SelectedIndexChanged">
                        <Columns>
                            <asp:BoundField DataField="time" HeaderText="时间" />
                            <asp:BoundField DataField="clientBuild" HeaderText="版本号" />
                            <asp:BoundField DataField="appType" HeaderText="客户端类型" />
                            <asp:BoundField DataField="clientType" HeaderText="客户端名称" />
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    消息
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkbtn" CommandName="Select" CssClass="linkButtonDetail" runat="server">查看消息</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:Panel CssClass="gridviewBottom" runat="server" ID="PanelPage">
                        <div class="gridviewBottom_left">
                            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" PageSize="10" FirstPageText="首页"
                                LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" SubmitButtonText="Go"
                                TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                                NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" CurrentPageButtonClass="currentButton"
                                PageIndexBoxClass="listPageText" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center"
                                MoreButtonType="Image" NavigationButtonType="Image" OnPageChanged="AspNetPager1_PageChanged">
                            </webdiyer:AspNetPager>
                        </div>
                    </asp:Panel>
                </asp:Panel>
            </div>
        </div>
    </div>
    <uc1:CheckUser ID="CheckUser1" runat="server" />
    </form>
</body>
</html>
