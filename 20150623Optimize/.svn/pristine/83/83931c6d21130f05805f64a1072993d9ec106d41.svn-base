<%@ Page Language="C#" AutoEventWireup="true" CodeFile="uxianRechargeCheck.aspx.cs" Inherits="CustomerServiceProcessing_uxianRechargeCheck" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>账户充值审批</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            GridViewStyle("grRechargeApply", "gv_OverRow");
            GridViewStyle("grRechargeList", "gv_OverRow");
        });
    </script>
</head>
<body>
    <form id="form1" runat="server" onkeypress="if(event.keyCode==13||event.which==13){return false;}">
        <uc3:HeadControl ID="HeadControl1" runat="server" navigationImage="" navigationText=""
            navigationUrl="" headName="账户充值审批" />
        <div id="box" class="box">
            <div class="content">
                <div class="layout">
                    操作类型
                <asp:RadioButton ID="rbOperate" GroupName="rb" Text="审批"
                    Checked="true" AutoPostBack="true" runat="server" OnCheckedChanged="rbOperate_CheckedChanged" />
                    <asp:RadioButton ID="rbQuery" AutoPostBack="true" OnCheckedChanged="rbOperate_CheckedChanged"
                        GroupName="rb" Text="历史记录" runat="server" />
                    <hr size="1" style="border: 1px #cccccc dashed;" />
                    <asp:Panel ID="panelOperate" runat="server">
                        <table>
                            <tr>
                                <td>&nbsp;
                                    <label>
                                        开始时间：</label><asp:TextBox ID="txtStartTime" runat="server" class="Wdate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"
                                            Style="width: 140px"></asp:TextBox>
                                </td>
                                <td>&nbsp;
                                    <label>
                                        结束时间：<asp:TextBox ID="txtEndTime" class="Wdate" runat="server" Style="width: 140px"
                                            onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                    </label>
                                </td>
                                <td>
                                    <asp:Button ID="btn_Search" class="button" runat="server" Text="查   询" OnClick="btn_Search_Click" />
                                </td>
                                <td>&nbsp;<asp:Button ID="btnAgree" CssClass="button" runat="server" Text="通过审批" OnClientClick="return confirm('确定执行通过审批操作？');" OnClick="btnAgree_Click" />&nbsp;
                            <asp:Button ID="btnRefuse" CssClass="button" runat="server" Text="拒绝审批" OnClientClick="return confirm('确定执行拒绝审批操作？');" OnClick="btnRefuse_Click" /></td>
                            </tr>
                        </table>
                        <hr size="1" style="border: 1px #cccccc dashed;" />
                        <div runat="server" id="divRemark">(注：共选择了<asp:Label runat="server" ID="lbCount" Text="0"></asp:Label>条记录，总计金额<asp:Label runat="server" ID="lbAmount" Text="0"></asp:Label>元）</div>
                        <asp:GridView ID="grRechargeApply" runat="server" DataKeyNames="id,amount" AutoGenerateColumns="False"
                            SkinID="gridviewSkin">
                            <Columns>
                                <asp:BoundField DataField="operateTime" HeaderText="申请时间" />
                                <asp:BoundField DataField="EmployeeFirstName" HeaderText="申请人" />
                                <asp:BoundField DataField="amount" HeaderText="金额" />
                                <asp:BoundField DataField="remark" HeaderText="备注" />
                                <asp:BoundField DataField="customerPhone" HeaderText="手机号码" />
                                <asp:BoundField DataField="status" HeaderText="操作状态" />
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        操作
                                        <asp:CheckBox ID="chbCheckAll" runat="server" AutoPostBack="True"
                                            OnCheckedChanged="chbCheckAll_CheckedChanged" Text="(全选)" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbSelect" runat="server" AutoPostBack="True"
                                            OnCheckedChanged="cbSelect_CheckedChanged" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>                        
                    </asp:Panel>
                    <asp:Panel ID="panelList" runat="server">
                        <table>
                            <tr>
                                <td>状态：
                                    <asp:DropDownList ID="ddlStatus" runat="server">
                                    </asp:DropDownList></td>
                                <td>&nbsp;
                                    <label>
                                        开始时间：</label><asp:TextBox ID="txtStrTime1" runat="server" class="Wdate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"
                                            Style="width: 140px"></asp:TextBox>
                                </td>
                                <td>&nbsp;
                                    <label>
                                        结束时间：<asp:TextBox ID="txtEndTime1" class="Wdate" runat="server" Style="width: 140px"
                                            onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                    </label>
                                </td>
                                <td>
                                    <asp:Button ID="Button1" class="button" runat="server" Text="查   询" OnClick="Button1_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="Button2" class="button" runat="server" Text="导出excel" OnClick="Button2_Click" />
                                </td>
                            </tr>
                        </table>
                        <hr size="1" style="border: 1px #cccccc dashed;" />
                        <asp:GridView ID="grRechargeList" runat="server" DataKeyNames="id" AutoGenerateColumns="False"
                            SkinID="gridviewSkin">
                            <Columns>
                                <asp:BoundField DataField="operateTime" HeaderText="申请时间" />
                                <asp:BoundField DataField="EmployeeFirstName" HeaderText="申请人" />
                                <asp:BoundField DataField="amount" HeaderText="申请金额" />
                                <asp:BoundField DataField="remark" HeaderText="申请备注" />
                                <asp:BoundField DataField="customerPhone" HeaderText="手机号码" />
                                <asp:BoundField DataField="status" HeaderText="操作状态" />
                                <asp:BoundField DataField="approvalEmployee" HeaderText="审核人" />
                                <asp:BoundField DataField="approvalTime" HeaderText="审核时间" />
                            </Columns>
                        </asp:GridView>
                        <div class="asp_page">
                            <webdiyer:AspNetPager ID="AspNetPager1" runat="server"
                                FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PageSize="10" PrevPageText="上一页"
                                SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                                NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" PageIndexBoxClass="listPageText"
                                CurrentPageButtonClass="currentButton" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center">
                            </webdiyer:AspNetPager>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
