<%@ Page Language="C#" AutoEventWireup="true" CodeFile="waiterQuery.aspx.cs" Inherits="PointsManage_waiterQuery" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>服务员详情</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("GridView_Employee", "gv_OverRow");
        });
        var startDate = function (elem) {
            WdatePicker({
                el: elem,
                isShowClear: false,
                startDate: '2013-01-01',
                onpicked: function (dp) {
                    elem.blur();
                }
            });
        };
        var endDate = function (elem) {
            WdatePicker({
                el: elem,
                isShowClear: false,
                onpicked: function (dp) { elem.blur() }
            });
        }
    </script>
    <style type="text/css">
        fieldset
        {
            border: 2px black solid;
            width: 50%;
        }
        #tb tr td
        {
            border: 1px solid black;
        }
    </style>
</head>
<body onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <form id="form1" runat="server">
    <div id="box" class="box">
        <div class="content">
            <div class="layout">
                <asp:Panel ID="Panel_Detail" runat="server">
                    <br />
                    <div id="employee_Info" style="text-align: center">
                        <fieldset>
                            <legend>基本资料</legend>
                            <table style="width: 80%; text-align: center; border: 2px black solid; border-collapse: collapse;"
                                id="tb">
                                <tr>
                                    <td>
                                        <asp:Label Text="姓名" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tb_Name" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        性别
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_Sex" runat="server">
                                            <asp:ListItem Value="0">女</asp:ListItem>
                                            <asp:ListItem Value="1">男</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="2">未知</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        电话
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tb_Phone" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        注册时间
                                    </td>
                                    <td>
                                        <asp:Label ID="lb_RegisterTime" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        生日
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tb_Birthday" runat="server" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"
                                            CssClass="Wdate"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        当前积分
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tb_current_Point" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        兑换记录
                                    </td>
                                    <td>
                                        <asp:Label ID="lb_ExchengeCount" runat="server" Text=""></asp:Label>&nbsp;条
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lk_PointExchang" runat="server" OnClick="lk_PointExchang_Click">查看</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        备注
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tb_Remark" runat="server" Height="70px" TextMode="MultiLine" Width="160px"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Button ID="btn_Save" runat="server" CssClass="button" Text="保  存" OnClick="btn_Save_Click" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                    <br />
                    <div id="Div1" style="text-align: center">
                        <fieldset>
                            <legend>服务记录</legend>
                            <div style="text-align: left">
                                查询时间<asp:TextBox ID="TextBox_TimeStr" runat="server" CssClass="Wdate" onFocus="startDate(this)"
                                    AutoPostBack="true" Width="100px" OnTextChanged="TextBox_Time_WaiterWorkExperienceQuery_Changed"></asp:TextBox>
                                &nbsp;-&nbsp;
                                <asp:TextBox ID="TextBox_TimeEnd" runat="server" CssClass="Wdate" onFocus="endDate(this)"
                                    AutoPostBack="true" OnTextChanged="TextBox_Time_WaiterWorkExperienceQuery_Changed"
                                    Width="100px"></asp:TextBox>
                            </div>
                            <div class="div_gridview" id="div2">
                                <asp:GridView ID="GridView_WaiterWorkExperience" runat="server" DataKeyNames="employeeID,status,serviceEndTime"
                                    AutoGenerateColumns="False" SkinID="gridviewSkin">
                                    <Columns>
                                        <asp:BoundField DataField="serviceStartTime" HeaderText="服务开始" />
                                        <asp:TemplateField HeaderText="服务结束">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_serviceEndTime" runat="server" Text=""></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="shopName" HeaderText="门店" />
                                        <asp:TemplateField HeaderText="可收银">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cb_isSupportSyb" Enabled="false" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="可开通">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cb_isOpen" Enabled="false" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </fieldset>
                    </div>
                    <br />
                    <div id="Div3" style="text-align: center">
                        <fieldset>
                            <legend>积分记录</legend>
                            <div style="text-align: left">
                                查询时间<asp:TextBox ID="TextBox_LogStarTime" runat="server" CssClass="Wdate" onFocus="startDate(this)"
                                    AutoPostBack="true" Width="100px" OnTextChanged="TextBox_LogTime_TextChanged"></asp:TextBox>
                                &nbsp;-&nbsp;
                                <asp:TextBox ID="TextBox_LogEndTime" runat="server" CssClass="Wdate" onFocus="endDate(this)"
                                    AutoPostBack="true" OnTextChanged="TextBox_LogTime_TextChanged" Width="100px"></asp:TextBox>
                                <asp:CheckBox ID="cb_Add" Text="显示增加" runat="server" AutoPostBack="true" OnCheckedChanged="cb_Add_CheckedChanged" /><asp:CheckBox
                                    ID="cb_Reduce" Text="显示减少" AutoPostBack="true" runat="server" OnCheckedChanged="cb_Reduce_CheckedChanged" />
                            </div>
                            <div class="div_gridview" id="div4">
                                <asp:GridView ID="GridView_WaiterPointLog" runat="server" DataKeyNames="" AutoGenerateColumns="False"
                                    SkinID="gridviewSkin">
                                    <Columns>
                                        <asp:TemplateField HeaderText="行号">
                                            <ItemTemplate>
                                                <%# (this.AspNetPager_Point.CurrentPageIndex - 1) * this.AspNetPager_Point.PageSize + Container.DataItemIndex + 1%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="operateTime" HeaderText="日期" />
                                        <asp:BoundField DataField="UserName" HeaderText="客户姓名" />
                                        <asp:BoundField DataField="mobilePhoneNumber" HeaderText="客户手机" />
                                        <asp:BoundField DataField="shopName" HeaderText="门店" />
                                        <asp:BoundField DataField="monetary" HeaderText="消费金额" />
                                        <asp:BoundField DataField="pointVariation" HeaderText="获得积分" />
                                        <asp:BoundField DataField="remark" HeaderText="备注" />
                                        <asp:BoundField DataField="EmployeeFirstName" HeaderText="操作员" />
                                    </Columns>
                                </asp:GridView>
                                <div class="asp_page">
                                    <webdiyer:AspNetPager ID="AspNetPager_Point" runat="server" FirstPageText="首页" LastPageText="尾页"
                                        NextPageText="下一页" PageSize="10" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                                        TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                                        NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" PageIndexBoxClass="listPageText"
                                        CurrentPageButtonClass="currentButton" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center"
                                        OnPageChanged="AspNetPager_Point_PageChanged">
                                    </webdiyer:AspNetPager>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
    <uc1:CheckUser ID="CheckUser1" runat="server" />
    </form>
</body>
</html>
