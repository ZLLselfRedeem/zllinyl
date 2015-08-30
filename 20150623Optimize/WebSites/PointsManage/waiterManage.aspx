<%@ Page Language="C#" AutoEventWireup="true" CodeFile="waiterManage.aspx.cs" Inherits="PointsManage_waiterManage" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>点单查看</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            GridViewStyle("GridView_Employee", "gv_OverRow");
            TabManage();
            var headControlHeight = $("#headControl").height();
            var tagMenuHeight = $("div.tagMenu").height();
            var layoutHeight = $(window).height() - headControlHeight - tagMenuHeight - 210;
            $("#mainFrame").height(layoutHeight);
        }); 
    </script>
</head>
<body onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <form id="form1" runat="server">
    <div id="box" class="box">
        <div class="content">
            <div class="layout">
                <div class="QueryTerms">
                    <table>
                        <tr>
                            <td style="width: 300px">
                                服务员手机号码：
                                <asp:TextBox ID="txt_EmployeePhone" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btn_QueryEmployee" runat="server" CssClass="button" Text="搜 索" OnClick="btn_QueryEmployee_Click" />
                            </td>
                        </tr>
                    </table>
                    <hr size="1" style="border: 1px #cccccc dashed;" />
                </div>
                <asp:Panel ID="Panel_GridView" runat="server" CssClass="gridview">
                    <div class="div_gridview" id="div_gridview">
                        <asp:GridView ID="GridView_Employee" runat="server" DataKeyNames="EmployeeID" AutoGenerateColumns="False"
                            SkinID="gridviewSkin" OnRowCommand="GridView_Employee_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="行号">
                                    <ItemTemplate>
                                        <%# (this.AspNetPager_EmployeeInfo.CurrentPageIndex - 1) * this.AspNetPager_EmployeeInfo.PageSize + Container.DataItemIndex + 1%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="EmployeeName" HeaderText="姓名" />
                                <asp:BoundField DataField="EmployeePhone" HeaderText="手机号码" />
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <img src="../Images/key_edit3.gif" />&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1"
                                            runat="server" CausesValidation="False" CommandName="Detail" Text="详情"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <div class="asp_page">
                            <webdiyer:AspNetPager ID="AspNetPager_EmployeeInfo" runat="server" FirstPageText="首页"
                                LastPageText="尾页" NextPageText="下一页" PageSize="10" PrevPageText="上一页" SubmitButtonText="Go"
                                TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                                NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" PageIndexBoxClass="listPageText"
                                CurrentPageButtonClass="currentButton" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center"
                                OnPageChanged="AspNetPager_EmployeeInfo_PageChanged">
                            </webdiyer:AspNetPager>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="Panel_Detail" runat="server" CssClass="div_gridview" Visible="false">
                    <div class="divDetailTitle">
                        <asp:Button ID="Button_back" runat="server" CssClass="couponButtonSubmit" Text="返     回"
                            OnClick="Button_back_Click" /></div>
                    <div id="divIframeContent" class="divDetailContent">
                        <iframe runat="server" frameborder="0" name="mainFrame" width="100%" id="mainFrame"
                            height="" src="" scrolling="auto"></iframe>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
    <uc1:CheckUser ID="CheckUser1" runat="server" />
    </form>
</body>
</html>
