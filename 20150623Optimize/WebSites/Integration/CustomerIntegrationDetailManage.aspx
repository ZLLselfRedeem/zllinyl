<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerIntegrationDetailManage.aspx.cs"
    Inherits="Integration_CustomerIntegrationDetailManage" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>用户积分维护</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script src="../Scripts/searchShop.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
     <style type="text/css">
        body{margin:0px;}
        #bg{width:100%;height:100%;top:0px;left:0px;position:absolute;filter: Alpha(opacity=50);opacity:0.5; background:#000000; display:none;}
        #popbox{position:absolute;width:360px; height:250px; left:50%; top:50%; margin:-200px 0 0 -200px; display:none; background:#666666;}
    </style>
    <script type="text/javascript">
      $(document).ready(function () {
            GridViewStyle("gdList", "gv_OverRow");
            TabManage();
      });



      function pupopen() {
          if (document.getElementById("lbUserName").textContent == "")
          {
              alert("没有相应的用户信息，请先查询");
              return;
          }
          document.getElementById("bg").style.display = "block";
          document.getElementById("popbox").style.display = "block";
      }
      function pupclose() {
          document.getElementById("bg").style.display = "none";
          document.getElementById("popbox").style.display = "none";
      }
    </script>
</head>
<body>
    <form id="form1" autocomplete="off" runat="server">
        <uc3:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
            navigationText="用户积分维护" navigationUrl="" headName="用户积分维护" />
            <div class="content">
                    <div style="width: 80%">
                        <table class="table" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <th>适用城市：</th>
                                <td>
                                     <asp:DropDownList ID="ddlCity" runat="server" AutoPostBack="True"></asp:DropDownList>
                                </td>
                                <th>
                                    发布时间：
                                </th>
                                <td>
                                     <asp:TextBox ID="tbBeginTime" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd 00:00:00'})"
                                        Width="130px"></asp:TextBox>至                                
                                    <asp:TextBox ID="tbEndTime" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd 23:59:59'})"
                                        Width="130px"></asp:TextBox>
                                </td>
                                <th>
                                    手机号：
                                </th>
                                <td>
                                    <asp:TextBox runat="server" ID="tbmobilePhoneNumber"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button Width="130px" Height="33px" CssClass="couponButtonSubmit" ID="btnSearch" runat="server" Text="搜索"
                                     OnClick="btnSearch_Click" />
                                </td>
                            </tr>
                        </table>
                        <hr size="1" style="border: 1px #cccccc dashed;" />
                       <table class="table" cellpadding="0" cellspacing="0" style="width: 100%">
                           <tr>
                               <td>
                                   当前积分：<asp:Label runat="server" ID="lbIntegration"></asp:Label>
                                  
                                       &nbsp;&nbsp;&nbsp;&nbsp;
                                  
                                      <button runat="server" id="btnFail" type="button" onclick="pupopen()" style="width: 130px; height: 33px" class="couponButtonSubmit">调整积分</button>
                               </td>
                           </tr>
                           <tr>
                               <td>
                                   用户：<label runat="server" id="lbUserName"></label><label runat="server" visible="false" id="lbCustomerID"></label>
                               </td>
                           </tr>
                           <tr>
                               <td>
                                   积分记录：
                               </td>
                           </tr>
                            <tr>
                                <td align="left">
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
                                <td colspan="3">
                                     <div class="div_gridview" id="div_gridview">
                                          <asp:GridView ID="gdList" runat="server" Width="100%" DataKeyNames="CreateDate,Description,RuleID,Integration,CurrentIntegration,CreateUser"
                AllowSorting="True" AutoGenerateColumns="False" SkinID="gridviewSkin">
                <Columns>
                    <asp:BoundField DataField="CreateDate" HeaderText="时间" />
                    <asp:BoundField DataField="Description" HeaderText="业务事件"/>
                    <asp:BoundField DataField="Integration" HeaderText="变更积分"/>
                    <asp:BoundField DataField="CurrentIntegration" HeaderText="积分数"/>
                    <asp:BoundField DataField="CreateUser" HeaderText="操作人"/>
                </Columns>
            </asp:GridView>
                                          <label id="lbCount" runat="server" ></label>
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
                    </div>
                                </td>
                            </tr>
                        </table>
                </div>
       <div style="display:none" id="bg"></div>
        <div style="display:none" id="popbox">
            <table width="100%" style="background-color:white">
                <tr>
                    <td align="center">
                        调整用户积分
                    </td>
                </tr>
                <tr>
                   <td>
                       用户：<asp:label runat="server" ID="lbUserName1"></asp:label>
                   </td>
                </tr>
                <tr>
                    <td>
                        调整积分：<asp:DropDownList runat="server" ID="ddlChange">
                            <asp:ListItem Text="增加" Value="1"></asp:ListItem>
                            <asp:ListItem Text="减少" Value="-1"></asp:ListItem>
                             </asp:DropDownList><asp:TextBox runat="server" ID="tbin"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        请输入事件说明：
                        <p>
                            <asp:TextBox runat="server" ID="tbRemark" TextMode="MultiLine" Width="350px" Height="100px"></asp:TextBox>
                        </p>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button runat="server" Width="130px" Height="33px" CssClass="couponButtonSubmit" ID="btnSubmit" OnClick="btnSubmit_Click" Text="确认" />&nbsp;&nbsp;&nbsp;&nbsp;<button style="width:130px;height:33px" class="couponButtonSubmit" onclick="pupclose()">取消</button>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
