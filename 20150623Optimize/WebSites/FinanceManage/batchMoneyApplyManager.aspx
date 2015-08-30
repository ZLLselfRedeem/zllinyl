<%@ Page Language="C#" AutoEventWireup="true" CodeFile="batchMoneyApplyManager.aspx.cs"
    Inherits="FinanceManage_batchMoneyApplyManager" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>批量打款管理</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script src="../Scripts/searchShop.js" type="text/javascript"></script>
    <style type="text/css">
        body{margin:0px;}
        #bg{width:100%;height:100%;top:0px;left:0px;position:absolute;filter: Alpha(opacity=50);opacity:0.5; background:#000000; display:none;}
        #popbox{position:absolute;width:400px; height:400px; left:50%; top:50%; margin:-200px 0 0 -200px; display:none; background:#666666;}
    </style>
    <script type="text/javascript">
        function gbcount(total,used,remain)
        {
            var max;
            max = total.value;
            if (document.getElementById("txt_serialNumberOrRemark").value.length > max)
            {
                document.getElementById("txt_serialNumberOrRemark").value = document.getElementById("txt_serialNumberOrRemark").value.substring(0, max);
                used.value = max;
                remain.value = 0;
            }
            else
            {
                used.value = document.getElementById("txt_serialNumberOrRemark").value.length;
                remain.value = max - used.value;
            }
        }

        function pupopen() {
            var input = document.getElementsByTagName("input");
            var k = 0;
            for (i = 0; i < input.length; i++) {
                if (input[i].type == "checkbox" && !input[i].disabled) {
                    if (input[i].checked)
                    {
                        k++;
                    }
                }
            }
            if (k == 0)
            {
                alert("请选择打款失败的明细！");
                return;
            }
            if (k > 1)
            {
                alert("确认银行打款失败不允许批量操作！");
                return;
            }

            document.getElementById("bg").style.display = "block";
            document.getElementById("popbox").style.display = "block";
        }
        function pupclose() {
            document.getElementById("bg").style.display = "none";
            document.getElementById("popbox").style.display = "none";
        }

        $(document).ready(function () {
            initData("batchmoneyapplyManager");
            GridViewStyle("GridView1", "gv_OverRow");
            GridViewStyle("gdList", "gv_OverRow");
            TabManage();
        });

        function oncbl(Control) {
            var input = document.getElementsByTagName("input");
            for (i = 0; i < input.length; i++) {
                if (input[i].type == "checkbox" && !input[i].disabled) {
                    input[i].checked = Control.checked;
                }
            }

            sumAmount();
        }
        function checkIsFirst()
        {
            var tb = document.getElementById("gdList");
            var str = 0;
            for (var i = 1; i < tb.rows.length; i++) {
                if (tb.rows[i].cells[8].innerHTML == "首次打款")
                {
                    var name = "gdList_gdCheck_" + (i-1);
                    var checkboxT = document.getElementById(name);
                    if (checkboxT.checked)
                    {
                        str++;
                    }
                }
            }
            var msg="";
            if (str > 0)
            {
                msg = "系统检测到有" + str + "个账号为首次打款，请确认是否继续打款？";
            }
            else
            {
                msg = "打款后将不能对该申请进行操作，确认打款？";
            }
            return confirm(msg);
        }
        function sumAmount()
        {
            var tb = document.getElementById("gdList");
            var sum = 0;
            var select = 0;
            for (var i = 1; i < tb.rows.length; i++) {
                var name = "gdList_gdCheck_" + (i - 1);
                var checkboxT = document.getElementById(name);
                if (checkboxT.checked) {
                    sum += parseFloat(tb.rows[i].cells[11].innerHTML);
                    select++;
                }
            }

            document.getElementById("lbCount").innerHTML = "注：共勾选" + select + "笔记录，申请结账金额为：" + sum.toFixed(2);
        }
    </script>
</head>
<body>
    <form id="form1" autocomplete="off"  runat="server">
        <uc3:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
            navigationText="批量打款管理" navigationUrl="" headName="批量打款管理" />
            <div class="content" id="divDetail" runat="server" style="width: 100%; display: '';">
                    <div style="width: 80%">
                        <table class="table" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <th>门店名：</th>
                                <td>
                                    <input id="text" runat="server" type="text" onkeyup="BatchmoneyapplyManagerShopSearch()" />
                                    <div id="init_date" runat="server" style="position: absolute; clear: both; border: 1px solid #000; background-color: White">
                                    </div>
                                </td>
                                <th>公司名称：
                                </th>
                                <td>
                                    <asp:Label runat="server" Width="130px" ID="lb_companyName"></asp:Label>
                                </td>
                                <th>打款单号：
                                </th>
                                <td>
                                    <asp:TextBox runat="server" ID="txt_batchMoneyApplyDetailCode"></asp:TextBox>
                                </td>
                                <th>
                                    城市：
                                </th>
                                <td>
                                    <asp:DropDownList ID="ddlCity" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <th>状态：</th>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlStatus">
                                        <asp:ListItem Value="0">全部</asp:ListItem>
                                        <asp:ListItem Value="5">申请未提交</asp:ListItem>
                                        <asp:ListItem Value="6">申请被撤回</asp:ListItem>
                                        <asp:ListItem Value="7">申请提交至出纳</asp:ListItem>
                                        <asp:ListItem Value="8">出纳已确认帐目</asp:ListItem>
                                        <asp:ListItem Value="9">主管提交至银行</asp:ListItem>
                                        <asp:ListItem Value="10">银行已受理</asp:ListItem>
                                        <asp:ListItem Value="12">银行打款成功</asp:ListItem>
                                        <asp:ListItem Value="13">银行打款失败</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <th>打款标识：</th>
                                <td><asp:DropDownList ID="ddlIsFirst" runat="server">
                                        <asp:ListItem Value="2">全部</asp:ListItem>
                                        <asp:ListItem Value="1">首次打款</asp:ListItem>
                                        <asp:ListItem Value="0">非首次打款</asp:ListItem>
                                    </asp:DropDownList></td>
                                <th>申请时间：
                                </th>
                                <td colspan="2">
                                    <asp:TextBox ID="txtOperateBeginTime" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd 00:00:00'})"
                                        Width="130px"></asp:TextBox>至                                
                                    <asp:TextBox ID="txtOperateEndTime" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd 23:59:59'})"
                                        Width="130px"></asp:TextBox>

                                </td>
                                <td>
                                    <asp:Button runat="server" Width="130px" Height="33px" CssClass="couponButtonSubmit" ID="btnSearch" Text="搜索" OnClick="btnSearch_Click" /></td>
                            </tr>
                        </table>
                        <hr size="1" style="border: 1px #cccccc dashed;" />
                        <table>
                            <tr>
                                <td>
                                    <asp:Button runat="server" Width="130px" Height="33px" CssClass="couponButtonSubmit" Text="批量打款" ID="btnAllPay" OnClick="btnAllPay_Click" />
                                </td>
                                <td>
                                    <asp:Button runat="server" Width="130px" Height="33px" CssClass="couponButtonSubmit" Text="批量确认" ID="btnSubmit" OnClick="btnSubmit_Click" />
                                </td>
                                <td>
                                    <asp:Button runat="server" Width="130px" Height="33px" CssClass="couponButtonSubmit" Text="批量撤回" ID="btnCancel" OnClick="btnCancel_Click" />
                                </td>
                                <td>
                                    <asp:Button runat="server" Width="130px" Height="33px" CssClass="couponButtonSubmit" Text="导出Excel" ID="btnExcel" OnClick="btnExcel_Click" />
                                </td>
                                <td style="display:none">
                                    <button runat="server" id="btnFail" type="button" onclick="pupopen()" style="width: 150px; height: 33px" class="couponButtonSubmitLong">确认银行打款已失败</button>
                                    <asp:Button runat="server" Width="150px" Height="33px" CssClass="couponButtonSubmitLong" Text="确认银行打款已成功" ID="btnPaySuccess" OnClick="btnPaySuccess_Click" Visible="false" />
                                </td>
                            </tr>
                        </table>
                        <table runat="server" id="gdTable">
                            <tr>
                                <td align="center">&nbsp;</td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <input id="allCheck" runat="server" type="checkbox" onclick="oncbl(this)" />全选
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
                                        <asp:GridView ID="gdList" runat="server" Width="1700px" DataKeyNames="shopID,batchMoneyApplyDetailId,
                                              batchMoneyApplyDetailCode,batchMoneyApplyId,accountNum,accountName,applyAmount,bankName,
                                              companyId,financePlayMoneyTime,status,PayeeBankName,
                                              newbankName,newaccountName,newaccountNum,newPayeeBankName,viewallocCommissionValue"
                                            AllowSorting="True" AutoGenerateColumns="False" SkinID="gridviewSkin" OnRowCommand="GridView_OrderStatistics_RowCommand" OnSorting="gdList_Sorting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="选择">
                                                    <ItemTemplate>
                                                        <input type="checkbox" id="gdCheck" onclick="sumAmount(this)" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="batchMoneyApplyDetailCode" HeaderText="单号" SortExpression="batchMoneyApplyDetailCode" />
                                                <asp:BoundField DataField="batchMoneyApplyDetailId" Visible="false" HeaderText="单号" SortExpression="batchMoneyApplyDetailId" />
                                                <asp:BoundField DataField="shopName" HeaderText="门店名" SortExpression="shopName" />
                                                <asp:BoundField DataField="companyName" HeaderText="公司名" SortExpression="companyName" />
                                                <asp:BoundField DataField="bankName" HeaderText="开户银行" SortExpression="bankName" />
                                                <asp:BoundField DataField="PayeeBankName" HeaderText="支行名称" SortExpression="PayeeBankName" />
                                                <asp:BoundField DataField="accountName" HeaderStyle-Width="5%" HeaderText="开户名" SortExpression="accountName" />
                                                <asp:BoundField DataField="accountNum" HeaderText="账号" SortExpression="accountNum" />
                                                <asp:BoundField DataField="isFirst" HeaderStyle-Width="6%" HeaderText="打款标识" SortExpression="isFirst" />
                                                <asp:BoundField DataField="createdTime" HeaderText="申请打款时间" SortExpression="createdTime" />
                                                <asp:BoundField DataField="financePlayMoneyTime" HeaderText="财务打款时间" SortExpression="financePlayMoneyTime" />
                                                <asp:BoundField DataField="applyAmount" HeaderText="申请结款金额" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:F}" SortExpression="applyAmount" />
                                                <asp:BoundField DataField="viewallocCommissionValue" HeaderText="当前佣金比例" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:F}" SortExpression="viewallocCommissionValue" />
                                                <asp:BoundField DataField="serialNumberOrRemark" HeaderText="流水号或备注" SortExpression="serialNumberOrRemark" />
                                                <asp:BoundField DataField="status" HeaderText="状态" HeaderStyle-Width="7%" SortExpression="status" />
                                                <asp:TemplateField HeaderText="操作" HeaderStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" CommandName="confirm" ID="lbtnConfirm">确认</asp:LinkButton>&nbsp;<asp:LinkButton runat="server" CommandName="Pay" ID="lbtnPay">打款</asp:LinkButton>&nbsp;<asp:LinkButton runat="server" CommandName="back" ID="lbtnCancel">撤回</asp:LinkButton>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <label id="lbCount" runat="server" ></label>
                                        <asp:Panel CssClass="gridviewBottom" runat="server" ID="PanelPage">
                                            <div class="gridviewBottom_left">
                                                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
                                                    NextPageText="下一页" PageSize="10" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                                                    TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                                                    NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" CurrentPageButtonClass="currentButton"
                                                    PageIndexBoxClass="listPageText" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center"
                                                    MoreButtonType="Image" NavigationButtonType="Image" OnPageChanged="AspNetPager1_PageChanged">
                                                </webdiyer:AspNetPager>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
        <div style="display:none" id="bg"></div>
        <div style="display:none" id="popbox">
            <table width="100%" style="background-color:white">
                <tr>
                    <td align="center">
                        确认银行打款已失败
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:TextBox ID="txt_serialNumberOrRemark" Text="请填写备注信息" onKeyDown="gbcount(this.form.total,this.form.used,this.form.remain);" onKeyUp="gbcount(this.form.total,this.form.used,this.form.remain);" TextMode="MultiLine" Width="355px" Height="330px" runat="server"></asp:TextBox><p>最多字数：
                        <input maxlength="4" readonly="readonly" name="total" size="3" value="500" />
                        已用字数：
                        <input  maxlength="4" readonly="readonly" name="used" size="3" value="0" />
                        剩余字数：
                        <input maxlength="4" readonly="readonly" name="remain" size="3" value="493" />
                        </p>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button runat="server" Width="130px" Height="33px" CssClass="couponButtonSubmit" ID="btnFailSubmit" Text="确认" OnClick="btnFailSubmit_Click" />&nbsp;&nbsp;&nbsp;&nbsp;<button style="width:130px;height:33px" class="couponButtonSubmit" onclick="pupclose()">取消</button>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
