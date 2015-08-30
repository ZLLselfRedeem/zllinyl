<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SystemCouponShareManage.aspx.cs" Inherits="Coupon_SystemCouponShareManage" %>

<!DOCTYPE html>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("GridViewCoupon", "gv_OverRow");
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <uc3:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
            navigationText="" navigationUrl="" headName="" />
        <div id="box" class="box">
            <div class="content">
                <div class="layout">
                    <div class="layout">
                        <table cellspacing="0" cellpadding="0" class="table" style="width: 100%;">

                            <tr>
                                <th>抵价券名称：
                                </th>
                                <td>
                                    <asp:TextBox ID="TextBoxName" Width="150px" runat="server"></asp:TextBox>
                                </td>
                                <th>所属城市：
                                </th>
                                <td>
                                    <asp:DropDownList ID="DropDownListCity" runat="server">
                                        <asp:ListItem Value="">--请选择--</asp:ListItem>
                                        <asp:ListItem Value="87">杭州</asp:ListItem>
                                        <asp:ListItem Value="73">上海</asp:ListItem> 
                                    </asp:DropDownList>
                                </td>
                            </tr>

                            <tr>
                                <th>创建时间：</th>
                                <td>从
                                <asp:TextBox ID="TextBoxCreateTimeFrom" runat="server" CssClass="Wdate" onfocus="WdatePicker({isShowClear:false,readOnly:true,dateFmt:'yyyy-MM-dd'})"></asp:TextBox>到
                                    <asp:TextBox ID="TextBoxCreateTimeTo" CssClass="Wdate" onfocus="WdatePicker({isShowClear:false,readOnly:true,dateFmt:'yyyy-MM-dd'})" runat="server"></asp:TextBox></td>
                            
                                <th></th>
                                <td>
                                   </td>
                            </tr>

                            <tr>

                                <td colspan="4" style="text-align: center">
                                    <asp:Button ID="ButtonSearch" runat="server" CssClass="tabButtonBlueClick" CausesValidation="false" Text="查询" OnClick="ButtonSearch_Click"  />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="ButtonAdd" runat="server" CssClass="tabButtonBlueClick" CausesValidation="false" Text="新增" OnClientClick="window.location.href='CouponAdd.aspx';return false;" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="ButtonExportExcel" runat="server" CssClass="tabButtonBlueClick" CausesValidation="false" Text="导出Excel"  />
                                    
                                </td>
                            </tr>
                            <tr>

                                <td colspan="4" style="text-align: center"></td>
                            </tr>
                        </table>
                    </div>
                    <div class="div_gridview" style="width: auto; height: auto">
                        <asp:GridView ID="GridViewQrCode"  runat="server" ViewStateMode="Enabled"   AutoGenerateColumns="False" SkinID="gridviewSkin" OnRowDataBound="GridViewCoupon_RowDataBound"  >
                            <Columns> 
                                <asp:BoundField DataField="Name" HeaderText="活动名" />
                                <asp:TemplateField HeaderText="所在城市">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelCity" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                                <asp:BoundField DataField="CreateTime" DataFormatString="{0:d}" HeaderText="申请时间" />
                                <asp:BoundField DataField="Pv" HeaderText="PV" />
                                <asp:BoundField DataField="Uv"  HeaderText="UV" /> 
                                <asp:TemplateField HeaderText="领取数">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelGetCount" runat="server"> </asp:Label> 
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="使用数">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelUseCount" runat="server"> </asp:Label> 
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="新注册">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelNewUserCount" runat="server"  ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="带动流水">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelAmount" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="状态">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelState" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="申请人">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelCreatedBy" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                            </Columns>
                        </asp:GridView>
                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
                            NextPageText="下一页" PageSize="10" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                            TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                            NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" CurrentPageButtonClass="currentButton"
                            PageIndexBoxClass="listPageText" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center"  >
                        </webdiyer:AspNetPager>
                    </div>

                </div>
            </div>
        </div>
    </form>
</body>
</html>
