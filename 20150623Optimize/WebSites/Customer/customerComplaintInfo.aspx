﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="customerComplaintInfo.aspx.cs"
    Inherits="Customer_customerComplaintInfo" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>用户投诉</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("GridViewCustomerComplaint", "gv_OverRow");
        });
    </script>
</head>
<body onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <form id="form1" runat="server">
    <uc2:HeadControl ID="HeadControl1" runat="server" headName="用户投诉" navigationImage=""
        navigationText="" navigationUrl="" />
    <div id="box" class="box">
        <div class="tagMenu">
            <ul class="menu">
                <li>用户投诉</li>
            </ul>
        </div>
        <div class="content">
            <div class="layout">
                <div class="div_gridview">
                    <asp:GridView ID="GridViewCustomerComplaint" runat="server" DataKeyNames="" AutoGenerateColumns="False"
                        CssClass="gridview" SkinID="gridviewSkin">
                        <Columns>
                            <asp:BoundField DataField="preOrder19dianId" HeaderText="流水号" />
                            <asp:BoundField DataField="UserName" HeaderText="客户昵称" />
                            <asp:BoundField DataField="mobilePhoneNumber" HeaderText="客户电话" />
                            <asp:BoundField DataField="EmployeeFirstName" HeaderText="服务员姓名" />
                            <asp:BoundField DataField="EmployeePhone" HeaderText="服务员电话" />
                            <asp:BoundField DataField="complaintTime" HeaderText="投诉时间" />
                            <asp:BoundField DataField="complaintMsg" HeaderText="投诉信息" />
                        </Columns>
                    </asp:GridView>
                    <div class="asp_page">
                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
                            NextPageText="下一页" PageSize="10" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                            TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                            NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" PageIndexBoxClass="listPageText"
                            CurrentPageButtonClass="currentButton" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center"
                            CenterCurrentPageButton="True" MoreButtonType="Text" OnPageChanged="AspNetPager1_PageChanged">
                        </webdiyer:AspNetPager>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
