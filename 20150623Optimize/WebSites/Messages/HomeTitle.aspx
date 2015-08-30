<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HomeTitle.aspx.cs" Inherits="HomeNew_CityManage" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>标签管理</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
  <script type="text/javascript">
      $(document).ready(function () {
          GridViewStyle("gdList", "gv_OverRow");
          TabManage();
      });
    </script>
    <style type="text/css">
        .auto-style2 {
            width: 75px;
            }
        .auto-style5 {
            width: 478px;
        }
        .auto-style6 {
            width: 73px;
        }
    </style>
</head>
<body scroll="no" style="overflow-y: hidden" onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <form id="form1" runat="server">
    <uc2:HeadControl ID="HeadControl1" runat="server" headName="标签管理" />
    <div id="box" class="box">
        <div class="tagMenu">
            <ul class="menu">
                <li>标签管理</li>
            </ul>
        </div>
        <table class="table" cellpadding="0" cellspacing="0" style="width: 100%; margin-bottom: 0px;">
                    <tr>
                        <th class="auto-style6">
                            &nbsp;&nbsp;
                            城市
                        </th>
                        <td class="auto-style2">
                            <asp:DropDownList ID="ddlCity" runat="server" Height="40px" Width="100px"></asp:DropDownList>
                        </td>
                         <td colspan="4" align="left" class="auto-style5">
                            <asp:Button ID="btnQuery" runat="server" Text="搜索" CssClass="couponButtonSubmit"
                                Width="130px" Height="32px" OnClick="btnQuery_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                             <asp:Button ID="Add" runat="server" Text="新建" CssClass="couponButtonSubmit"
                                Width="130px" Height="33px" OnClick="Add_Click" CommandName="add" />
                        </td>
                    </tr>
                </table>
         

        <div class="content">
           <div style="width: 80%">
                 <table class="table" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td  colspan="3" align="left">
                                    <asp:Label ID="Label_LargePageCount" Visible="false" runat="server" Text=""></asp:Label>
                                    <asp:Button ID="Button_10" runat="server" CssClass="tabButtonBlueUnClick" CommandName="button_10"
                            OnClick="Button_LargePageCount_Click" Width="50px" Text="10"></asp:Button>
                        <asp:Button ID="Button_50" runat="server" CssClass="tabButtonBlueUnClick" CommandName="button_50"
                            OnClick="Button_LargePageCount_Click" Width="50px" Text="50"></asp:Button>
                        <asp:Button ID="Button_100" runat="server" CssClass="tabButtonBlueUnClick" CommandName="button_100"
                            OnClick="Button_LargePageCount_Click" Width="50px" Text="100"></asp:Button>
                                </td>
                            </tr>
                            <tr>
                                <td>
                <div class="div_gridview" id="div_gridview">
                    <asp:GridView ID="GridView_City" runat="server" DataKeyNames="id,cityID,TitleName,TitleIndex,cityName,status,Enable,IsMaster"
                        AutoGenerateColumns="False" SkinID="gridviewSkin" OnRowCommand="GridView_City_RowCommand" OnRowUpdating="GridView_City_RowUpdating" style="margin-right: 0px" OnRowDeleting="GridView_City_RowDeleting" OnRowDataBound="GridView_City_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="标签ID" Visible="false" />
                            <asp:BoundField DataField="cityID" HeaderText="城市ID" Visible="false" />
                            <asp:BoundField DataField="titleName" HeaderText="标签名称" />
                            <asp:BoundField DataField="cityName" HeaderText="城市" />
                            <asp:BoundField DataField="titleIndex" HeaderText="标签顺序" />
                             <asp:TemplateField HeaderText="是否启用">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID ="lbEnable" Text='<%#Eval("Enable").ToString().Equals("True") ? "启用":"停用"%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="是否商户">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID ="lbIsMerchant" Text='<%#Eval("IsMerchant").ToString().Equals("True") ? "是":"否"%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="是否主要标签">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID ="lbIsMaster" Text='<%#Eval("IsMaster").ToString().Equals("True") ? "是":"否"%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="操作">
                                <ItemTemplate>
                                    &nbsp;&nbsp;&nbsp;<img src="../Images/key_edit3.gif" /><asp:LinkButton ID="LinkButton5"
                                        runat="server" CausesValidation="False" foreColor="blue" CommandName="clientUpdate" Text='<%#Eval("Enable").ToString().Equals("True") ? "停用":"启用"%>'></asp:LinkButton>
                                     &nbsp; &nbsp;&nbsp;<img src="../Images/key_edit3.gif" /><asp:LinkButton ID="LinkButton1"
                                        runat="server" CausesValidation="False" foreColor="blue" CommandName="edit" Text="编辑"></asp:LinkButton>
                                    &nbsp;&nbsp;&nbsp;<img src="../Images/delete.gif" />
                                    <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="False" foreColor="blue" CommandName="delete"
                                        Text="删除" OnClientClick="return confirm('你确定删除吗？')"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                    
                </div>
                 <asp:Panel CssClass="gridviewBottom" runat="server" ID="PanelPage">
                    <div class="gridviewBottom_left">
                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
                            NextPageText="下一页" PageSize="10" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                            TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                            NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" CurrentPageButtonClass="currentButton"
                            PageIndexBoxClass="listPageText" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center"
                            MoreButtonType="Image" NavigationButtonType="Image" OnPageChanged="AspNetPager1_PageChanged"></webdiyer:AspNetPager>
                    </div>
                </asp:Panel>
                                          </td>
                            </tr>
                        </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>

