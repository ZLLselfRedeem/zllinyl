<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdMatchBanner.aspx.cs" Inherits="Advertisement_AdMatchBanner" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>广告排期</title>
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
        navigationText="" navigationUrl="" headName="广告排期" />
    <div id="box" class="box">
        <div class="content">
            <div class="layout">
                <div class="layout">
                    <table  class="table" style="width: 70%;">
                        
                        <tr>
                            <th>
                                所属城市：
                            </th>
                            <td>
                                <asp:DropDownList ID="DropDownListCity" runat="server" Width="150px"> 
                                    <asp:ListItem Value="87" Selected="True">杭州市</asp:ListItem>
                                    <asp:ListItem Value="1">北京市</asp:ListItem>
                                    <asp:ListItem Value="73">上海市</asp:ListItem>
                                    <asp:ListItem Value="197">广州市</asp:ListItem>
                                    <asp:ListItem Value="199">深圳市</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <th>
                                 广告分类：
                            </th>
                            <td>
                               <asp:DropDownList ID="DropDownList_adClassify" runat="server"  AutoPostBack="true" Width="150px"
                                    onselectedindexchanged="DropDownList_adClassify_SelectedIndexChanged"  > 
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
                                  <asp:DropDownList ID="DropDownList_ADType" Width="150px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_ADType_SelectedIndexChanged"> 
                                        <asp:ListItem Value="1">公司广告</asp:ListItem>
                                        <asp:ListItem Value="3">宣传广告</asp:ListItem>
                                        <asp:ListItem Value="4">红包广告</asp:ListItem>
                                        <asp:ListItem Value="5">专题广告</asp:ListItem>
                                    <%--    <asp:ListItem Value="6">套餐广告</asp:ListItem>--%>
                                    </asp:DropDownList>
                                   <%-- <asp:DropDownList ID="DropDownList_ADTypeFoodPlaza" runat="server" AutoPostBack="True"  style="display:none"
                                        OnSelectedIndexChanged="DropDownList_ADTypeFoodPlaza_SelectedIndexChanged"> 
                                        <asp:ListItem Value="21">公司广告</asp:ListItem>
                                        <asp:ListItem Value="23">宣传广告</asp:ListItem>
                                        <asp:ListItem Value="25">专题广告</asp:ListItem>
                                    </asp:DropDownList>--%>
                            </td>
                            <th>
                               广告栏位：
                            </th>
                            <td>
                               <asp:DropDownList ID="DropDownList_Banners" runat="server"  Width="150px"
                                    DataTextField="advertisementColumnName" DataValueField="Id"  >
                                       
                                    </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                广告排期:</th>
                            <td>
                                  从<asp:TextBox ID="TextBoxTimeFrom" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"
                                        Width="98px"></asp:TextBox> 至<asp:TextBox ID="TextBoxTimeTo" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"
                                        Width="98px"></asp:TextBox></td>
                            <th>
                               广告时段:</th>
                            <td>
                                 <asp:DropDownList ID="DropDownListInterval" Width="150px" runat="server"> 
                                    <asp:ListItem Value="">--请选择--</asp:ListItem>
                                    <asp:ListItem Value="1">上午</asp:ListItem>
                                    <asp:ListItem Value="2">下午</asp:ListItem> 
                                </asp:DropDownList> </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="center">
                                <asp:Button ID="Button_search" runat="server" Text="搜索" CssClass="button" 
                                    onclick="Button_search_Click" />
                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                      <asp:Button ID="ButtonSelect" runat="server" Text="选择广告" CssClass="button" 
                            onclick="ButtonSelect_Click"  />
                            </td>
                             
                        </tr>
                    </table>
                     
                     
                    <%-- <asp:Button ID="Button_Add" runat="server" Text="排 期" CssClass="button" OnClick="Button_Add_Click" />
                    &nbsp;
                    <asp:Button ID="Button1" runat="server" Text="返 回" CssClass="button" OnClick="Button_Click" /> --%>
                </div>
                <asp:Panel ID="Panel_update" runat="server">
                    <div class="div_gridview" style=" width:70%"> 
                        <asp:GridView ID="GridView_AdList" runat="server"  ViewStateMode="Enabled" DataKeyNames="IntervalTime"
                            AutoGenerateColumns="False" SkinID="gridviewSkin" OnRowDataBound="GridView_AdList_RowDataBound"
                            OnRowCommand="GridView_AdList_RowCommand" HeaderStyle-HorizontalAlign="Center" >
                            <Columns>
                               <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                          <input id="CheckboxAll" type="checkbox"  />全选
                                    </HeaderTemplate>
                                    <ItemTemplate  >
                                        <asp:CheckBox ID="CheckBoxSelect" name="chkList" Visible="false" runat="server" /> 
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>  
                                <asp:TemplateField HeaderText="广告时段" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelIntervalTime" runat="server" Text=""></asp:Label> 
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="广告名称" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelName" runat="server" Text=""></asp:Label>
                                        <asp:HiddenField ID="HiddenFieldAdID" runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="操作"  HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        
                                        <asp:LinkButton ID="LinkButtonStop" runat="server" CausesValidation="False" Visible="false" 
                                            CommandName="stop" Text="停用"></asp:LinkButton> 
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:GridView>
                         
                      
                            
                    </div>
                    <div style=" text-align:center; margin-top:10px; margin-bottom:10px">
                    </div>
                </asp:Panel>
                <asp:Panel ID="Panel_Window" runat="server" CssClass="panelSyle">
                   <table style="width: 550px;">
                        
                        <tr>
                            <td>
                                广告名称：
                            </td>
                            <td>
                                <asp:TextBox ID="TextBoxName" runat="server"></asp:TextBox>
                                  <asp:Button ID="ButtonAdSearch" runat="server" Text="搜索" CssClass="button" 
                                    onclick="ButtonAdSearch_Click"   />
                            </td> 
                        </tr>
                         <tr>
                            <td colspan="2" align="left">
                              <asp:GridView ID="GridViewAd" runat="server" AutoGenerateColumns="False"   SkinID="gridviewSkin" 
                            onrowcommand="GridViewAd_RowCommand" onrowdatabound="GridViewAd_RowDataBound">
                               
                                    <Columns>
                                        <asp:BoundField DataField="name" HeaderText="广告名称" />
                                        <asp:TemplateField HeaderText="广告分类"> 
                                            <ItemTemplate>
                                                <asp:Label ID="LabelClassify" runat="server" Text=""></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="广告类型"> 
                                            <ItemTemplate>
                                                <asp:Label ID="LabelType" runat="server" Text=""></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="操作">
                                            <ItemTemplate>
                                                <asp:LinkButton CommandName="select" CommandArgument='<%# Eval("id")%>' ID="LinkButtonSelect" runat="server">选择</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                               
                                    </asp:GridView>
                                      <webdiyer:AspNetPager ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
                            NextPageText="下一页" PageSize="10" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                            TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                            NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" CurrentPageButtonClass="currentButton"
                            PageIndexBoxClass="listPageText" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center"
                            OnPageChanged="AspNetPager1_PageChanged"></webdiyer:AspNetPager>
                            </td>
                             
                        </tr>
                        <tr>
                           <td colspan="2" align="center">
                              
                           </td>
                       </tr>
                       <tr>
                           <td colspan="2" align="center">
                               <asp:Button ID="ButtonCancel" runat="server" Text="返回" CssClass="button"  />
                           </td>
                       </tr>
                    </table>
                    <asp:HiddenField ID="HiddenField_MedalId" runat="server" />
                </asp:Panel>
                <asp:Panel ID="Panel_Main" runat="server">
                    
                </asp:Panel>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
