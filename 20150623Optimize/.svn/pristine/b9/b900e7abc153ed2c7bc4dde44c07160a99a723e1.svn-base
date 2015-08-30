<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PreOrderShopVerified.aspx.cs"
    Inherits="PreOrder19dianManage_PreOrderShopVerified" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>点单对账</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            GridViewStyle("GridView_PreOrderShopVerified", "gv_OverRow");
            StatusFun();
            TabManage();
            var headControlHeight = $("#headControl").height();
            var tagMenuHeight = $("div.tagMenu").height();
            var layoutHeight = $(window).height() - headControlHeight - tagMenuHeight - 210;
            $("#divIframeContent").css({ "height": layoutHeight + 10 });
            $("#mainFrame").height(layoutHeight);
        });  
              
    </script>
</head>
<body onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <form id="form1" runat="server">
    <uc3:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
        navigationText="点单查看" navigationUrl="~/PreOrder19dianManage/PreOrder19dianManage.aspx"
        headName="点单对账" />
    <div id="box" class="box">
        <div class="tagMenu">
            <ul class="menu">
                <li>点单对账</li>
            </ul>
        </div>
        <div class="content">
            <div class="layout">
                <div class="QueryTerms">
                    <table>
                        <tr>
                            <td>
                                公司：<asp:DropDownList ID="DropDownList_Company" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="DropDownList_Company_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                门店：<asp:DropDownList ID="DropDownList_Shop" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_Shop_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <hr size="1" style="border: 1px #cccccc dashed;" />
                    <table cellspacing="5">
                        <tr>
                            <td>
                                <asp:Button ID="Button_UnApproved" runat="server" Text="未对账" CssClass="tabButtonBlueUnClick"
                                    OnClick="Button_Approved_Click" CommandName="n" />
                            </td>
                            <td>
                                <asp:Button ID="Button_Approved" runat="server" Text="已对账" CssClass="tabButtonBlueUnClick"
                                    OnClick="Button_Approved_Click" CommandName="y" />
                            </td>
                            <td>
                                <asp:Button ID="Button_All" runat="server" Text="全部" CssClass="tabButtonBlueUnClick"
                                    OnClick="Button_Approved_Click" CommandName="a" Visible="false" />
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox_preOrder19dianId" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label_Error" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Button ID="Button_preOrder19dianId" runat="server" Text="查     询" CssClass="tabButtonBlueClick"
                                    OnClick="Button_preOrder19dianId_Click" Width="100px" />
                            </td>
                        </tr>
                    </table>
                    <hr size="1" style="border: 1px #cccccc dashed;" />
                    <table>
                        <tr>
                            <td>
                                起止时间：
                            </td>
                            <td>
                                <asp:Button ID="Button_1day" runat="server" OnClick="Button_Time_Click" CssClass="tabButtonBlueUnClick"
                                    CommandName="1" Text="今天"></asp:Button>
                            </td>
                            <td>
                                <asp:Button ID="Button_yesterday" runat="server" OnClick="Button_Time_Click" CssClass="tabButtonBlueUnClick"
                                    CommandName="yesterday" Text="昨天"></asp:Button>
                            </td>
                            <td>
                                <asp:Button ID="Button_7day" runat="server" OnClick="Button_Time_Click" CssClass="tabButtonBlueUnClick"
                                    CommandName="7" Text="最近7天"></asp:Button>
                            </td>
                            <td>
                                <asp:Button ID="Button_30day" runat="server" OnClick="Button_Time_Click" CssClass="tabButtonBlueUnClick"
                                    CommandName="30" Text="1个月"></asp:Button>
                            </td>
                            <td>
                                <script type="text/javascript">
                                    var startDate = function (elem) {
                                        WdatePicker({
                                            el: elem,
                                            isShowClear: false,
                                            maxDate: '#F{$dp.$D(\'TextBox_preOrderTimeEnd\')||%y-%M-%d}',
                                            startDate: '2013-07-26',
                                            onpicked: function (dp) {
                                                elem.blur();
                                            },
                                            skin: 'whyGreen'
                                        });
                                    };
                                    var endDate = function (elem) {
                                        WdatePicker({
                                            el: elem,
                                            isShowClear: false,
                                            maxDate: '%y-%M-{%d+1}',
                                            minDate: '#F{$dp.$D(\'TextBox_preOrderTimeStr\')}',
                                            startDate: '2013-07-28',
                                            onpicked: function (dp) { elem.blur() },
                                            skin: 'whyGreen'
                                        });
                                    }
                                </script>
                                <asp:TextBox ID="TextBox_preOrderTimeStr" runat="server" CssClass="Wdate" onFocus="startDate(this)"
                                    AutoPostBack="true" Width="85px" OnTextChanged="TextBox_preOrderTimeStr_TextChanged"></asp:TextBox>
                                &nbsp;-&nbsp;
                                <asp:TextBox ID="TextBox_preOrderTimeEnd" runat="server" CssClass="Wdate" onFocus="endDate(this)"
                                    AutoPostBack="true" OnTextChanged="TextBox_preOrderTimeStr_TextChanged" Width="85px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <asp:Label ID="Label_Approved" runat="server" Text="Label" Visible="False"></asp:Label>
                </div>
                <asp:Panel ID="Panel_PreOrder19dian" runat="server" CssClass="div_gridview">
                    <asp:GridView ID="GridView_PreOrderShopVerified" runat="server" DataKeyNames="preOrder19dianId"
                        AutoGenerateColumns="False" SkinID="gridviewSkin" OnRowCommand="GridView_PreOrderShopVerified_RowCommand"
                        OnRowDataBound="GridView_PreOrderShopVerified_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="preOrder19dianId" HeaderText="流水号" />
                            <asp:BoundField DataField="UserName" HeaderText="用户昵称" />
                            <asp:BoundField DataField="mobilePhoneNumber" HeaderText="手机号码" />
                            <asp:BoundField DataField="preOrderTime" HeaderText="预点时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
                            <asp:BoundField DataField="prePayTime" HeaderText="支付时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
                            <asp:BoundField DataField="preOrderServerSum" HeaderText="点单金额" DataFormatString="{0:F}" />
                            <asp:BoundField DataField="prePaidSum" HeaderText="支付金额" />
                            <asp:BoundField DataField="invoiceTitle" HeaderText="发票抬头" />
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="Button_Verified" runat="server" CausesValidation="False" CommandName="Verified"
                                        Text="对账" CssClass="linkButtonDetail"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="Button1" runat="server" CausesValidation="False" CommandName="Select"
                                        Text="详情" CssClass="linkButtonDetail"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:Panel CssClass="gridviewBottom" runat="server" ID="PanelPage">
                        <div class="gridviewBottom_left">
                            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                                FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PageSize="10" PrevPageText="上一页"
                                SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                                NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" CurrentPageButtonClass="currentButton"
                                PageIndexBoxClass="listPageText" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center"
                                MoreButtonType="Image" NavigationButtonType="Image">
                            </webdiyer:AspNetPager>
                        </div>
                        <div class="gridviewBottom_right">
                            <table>
                                <tr>
                                    <th>
                                        共
                                    </th>
                                    <td>
                                        <asp:Label ID="Label_OrderCount" runat="server" Text="" ForeColor="#F40404" Font-Bold="True"></asp:Label>
                                    </td>
                                    <th>
                                        单
                                    </th>
                                    <th>
                                    </th>
                                    <th>
                                        总金额
                                    </th>
                                    <td>
                                        ￥<asp:Label ID="Label_preOrderServerSumSum" runat="server" Text="" ForeColor="#F40404"
                                            Font-Bold="True"></asp:Label>
                                    </td>
                                    <th>
                                    </th>
                                    <th>
                                        支付金额
                                    </th>
                                    <td>
                                        ￥<asp:Label ID="Label_prePaidSumSum" runat="server" Text="" ForeColor="#F40404" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                </asp:Panel>
                <asp:Panel ID="Panel_Detail" runat="server" CssClass="div_gridview" Visible="false">
                    <div class="divDetailTitle" style="overflow-y: hidden">
                        <input id="Hidden1" type="hidden" />
                        <label>
                            点单详情信息</label><asp:Button ID="Button_Approve" runat="server" Text="" CssClass="couponButtonSubmit"
                                OnClick="Button_Approve_Click" OnClientClick="return confirm('你确定操作吗?')" />&nbsp;&nbsp;<asp:Button
                                    ID="Button_back" runat="server" CssClass="couponButtonSubmit" OnClick="Button_back_Click"
                                    Visible="false" Text="返     回" />
                    </div>
                    <div id="divIframeContent" style="border-top: none;">
                        <iframe runat="server" frameborder="0" name="mainFrame" width="100%" id="mainFrame"
                            height="" src="" scrolling="auto"></iframe>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
    <uc1:CheckUser ID="CheckUser1" runat="server" />
    </form>
    <script type="text/javascript">
        function StatusFun() {
            var a = document.getElementById("Hidden1");
            if (a != null) {
                $("body").attr("scroll", "no").attr("overflow-y", "hidden");
            }
            else {
                $("body").attr("scroll", "auto");
            }
        }
    </script>
</body>
</html>
