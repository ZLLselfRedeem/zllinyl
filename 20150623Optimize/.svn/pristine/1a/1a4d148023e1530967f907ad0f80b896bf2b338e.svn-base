<%@ Page Language="C#" AutoEventWireup="true" CodeFile="customerServiceOperateLogList.aspx.cs"
    Inherits="CustomerServiceProcessing_customerServiceOperateLogList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>客服操作日志</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            GridViewStyle("GridView_customerServiceLog", "gv_OverRow");
        });
    </script>
</head>
<body>
    <form id="form1" runat="server" onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <uc3:HeadControl ID="HeadControl1" runat="server" navigationImage="" navigationText=""
        navigationUrl="" headName="客服操作日志" />
    <div id="box" class="box">
        <div class="content">
            <div class="layout">
                <div class="QueryTerms">
                    <table>
                        <tr>
                            <td>
                                操作类型：<asp:DropDownList ID="DropDownList_OperateType" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="DropDownList_OperateType_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <hr size="1" style="border: 1px #cccccc dashed;" />
                        <tr>
                            <td>
                                <label>
                                    流水号：</label><asp:TextBox ID="TextBox_preOrderId" runat="server" Style="width: 100px"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;&nbsp;
                                <label>
                                    操作者姓名：</label><asp:TextBox ID="TextBox_operateName" runat="server" Style="width: 100px"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;&nbsp;
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
                    <asp:GridView ID="GridView_customerServiceLog" runat="server" DataKeyNames="" AutoGenerateColumns="False"
                        SkinID="gridviewSkin" ClientIDMode="Static">
                        <Columns>
                            <asp:BoundField DataField="employeeName" HeaderText="操作者姓名" />
                            <asp:BoundField DataField="employeeOperate" HeaderText="详细操作内容" />
                            <asp:BoundField DataField="employeeOperateTime" HeaderText="操作时间" />
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
    </form>
</body>
</html>
