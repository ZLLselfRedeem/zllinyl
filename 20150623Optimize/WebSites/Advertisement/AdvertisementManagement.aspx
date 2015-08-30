<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdvertisementManagement.aspx.cs" Inherits="Advertisement_AdvertisementManagement" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>广告列表</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("GridView_AdList", "gv_OverRow");

            $("#CheckboxAll").click(function () {
                $("input[type='checkbox']").attr("checked", $(this).attr("checked"));
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <uc3:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
        navigationText="" navigationUrl="" headName="广告列表" />
    <div id="box" class="box">
        <div class="content">
            <div class="layout">
                <div class="layout">
                    <table  class="table" style="width: 70%;">
                        
                        <tr>
                            <th>
                                广告名称：
                            </th>
                            <td>
                                <asp:TextBox ID="TextBoxName"  Width="150px"  runat="server"></asp:TextBox>
                            </td>
                            <th>
                                 广告分类：
                            </th>
                            <td>
                               <asp:DropDownList ID="DropDownList_adClassify" runat="server"   Width="150px"  > 
                                    <asp:ListItem Value="">--请选择--</asp:ListItem>
                                    <asp:ListItem Value="1">首页广告</asp:ListItem>
                                    <asp:ListItem Value="2">美食广场广告</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                广告类型：
                            </th>
                            <td>
                                  <asp:DropDownList ID="DropDownList_ADType" Width="150px" runat="server"   > 
                                        <asp:ListItem Value="">--请选择--</asp:ListItem>
                                        <asp:ListItem Value="1">公司广告</asp:ListItem>
                                        <asp:ListItem Value="3">宣传广告</asp:ListItem>
                                        <asp:ListItem Value="4">红包广告</asp:ListItem>
                                        <asp:ListItem Value="5">专题广告</asp:ListItem>
                                        <asp:ListItem Value="6">套餐广告</asp:ListItem>
                                    </asp:DropDownList> 
                            </td>
                            <th>
                              
                            </th>
                            <td>
                              
                            </td>
                        </tr> 
                        <tr>
                            <td colspan="4" align="center">
                                <asp:Button ID="Button_search" runat="server" Text="搜索" CssClass="button" 
                                    onclick="Button_search_Click"   /> 
                            </td>
                             
                        </tr>
                    </table>
                     
                     
                    <%-- <asp:Button ID="Button_Add" runat="server" Text="排 期" CssClass="button" OnClick="Button_Add_Click" />
                    &nbsp;
                    <asp:Button ID="Button1" runat="server" Text="返 回" CssClass="button" OnClick="Button_Click" /> --%>
                </div>
                <asp:Panel ID="Panel_update" runat="server">
                    <div class="div_gridview" style=" width:70%"> 
                        <asp:GridView ID="GridViewAd" runat="server"  ViewStateMode="Enabled"   AutoGenerateColumns="False" SkinID="gridviewSkin"   
                            HeaderStyle-HorizontalAlign="Center" 
                            onrowdatabound="GridViewAd_RowDataBound" >
                            <Columns>
                              
                                
                                 <asp:BoundField DataField="name" HeaderText="广告名称" /> 
                                  <asp:TemplateField HeaderText="广告分类" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelClassify" runat="server" Text=""></asp:Label> 
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField> 
                                  <asp:TemplateField HeaderText="广告类型" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelType" runat="server" Text=""></asp:Label> 
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="操作"  HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        
                                        <asp:LinkButton ID="LinkButtonEdit" runat="server"   Text="编辑"></asp:LinkButton> 
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:GridView>
                          <webdiyer:AspNetPager ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
                            NextPageText="下一页" PageSize="10" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                            TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                            NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" CurrentPageButtonClass="currentButton"
                            PageIndexBoxClass="listPageText" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center"
                            OnPageChanged="AspNetPager1_PageChanged"></webdiyer:AspNetPager>
                      
                            
                    </div>
                    <div style=" text-align:center; margin-top:10px; margin-bottom:10px">
                    </div>
                </asp:Panel> 
                </asp:Panel>
            </div>
        </div>
    </div>
    </form>
</body>
</html>