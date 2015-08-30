<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TreasureChestConfig.aspx.cs"
    Inherits="RedEnvelope_TreasureChestConfig" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>宝箱配置</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script type="text/javascript">

        function isNumber(value) {
            var rules = "^[1-9][0-9]*$"; //正整数
            //var rules = "^[1-9]\d*|0$"; //整数

            if (value.match(rules) == null) {
                return false;
            }
            else {
                return true;
            }
        }
        function isFloat(value) {
            //var rules = "^[1-9]{1}[0-9]{0,}\.[0-9]{2}$"; //第一位为 1-9 之间的数字，之后为 0-9 不限定位数，一个小数点后两位数字
            var rules = "^[0-9]*(\.[0-9]{1,2})?$";
            if (value.match(rules) == null) {
                return false;
            }
            else {
                return true;
            }
        }

        function Check() {
            var err = "";
            var msg = "请注意以下内容：\r\n"
            if (document.getElementById("ddlActivityName").value == "NA") {
                err += "【请先选择活动名称】\r\n";
            }
            if (document.getElementById("txbAmount").value.length == 0) {
                err += "【宝箱金额不能为空】\r\n";
            }
            if (document.getElementById("txbAmount").value.length > 0) {
                if (!isFloat(document.getElementById("txbAmount").value) && !isNumber(document.getElementById("txbAmount").value)) {
                    err += "【请输入正确的宝箱金额】\r\n";
                }
                if (document.getElementById("txbAmount").value < 1 || document.getElementById("txbAmount").value > 1000000) {
                    err += "【宝箱金额取值范围1~1000000】\r\n";
                }
            }
            //            if (document.getElementById("txbCount").value.length == 0) {
            //                err += "【完全解锁人数不能为空】\r\n";
            //            }
            //            if (document.getElementById("txbCount").value.length > 0) {
            //                if (!isNumber(document.getElementById("txbCount").value)) {
            //                    err += "【完全解锁人数只能为正整数】\r\n";
            //                }
            //                if (document.getElementById("txbCount").value < 1 || document.getElementById("txbCount").value > 50000) {
            //                    err += "【完全解锁人数取值范围1~50000】\r\n";
            //                }
            //            }
            var maxRedEnvelope = document.getElementById("hiddenMaxRedEnvelope").value;

            if (document.getElementById("txbMin").value.length > 0) {
                if (!isFloat(document.getElementById("txbMin").value) && !isNumber(document.getElementById("txbMin").value)) {
                    err += "【请输入正确的红包最小值】\r\n";
                }
            }
            if (document.getElementById("txbMax").value.length > 0) {
                if (!isFloat(document.getElementById("txbMax").value) && !isNumber(document.getElementById("txbMax").value)) {
                    err += "【请输入正确的红包最大值】\r\n";
                }
            }
            //            if (document.getElementById("txbQuantity").value.length > 0) {
            //                if (!isFloat(document.getElementById("txbQuantity").value) && !isNumber(document.getElementById("txbQuantity").value)) {
            //                    err += "【请输入正确的宝箱数量】\r\n";
            //                }
            //                if (document.getElementById("txbAmount").txbQuantity < 1 || document.getElementById("txbQuantity").value > 1000000) {
            //                    err += "【宝箱数量取值范围1~1000000】\r\n";
            //                }
            //            }
            if (err.length > 0) {
                alert(msg + err);
                return false;
            }
            else {
                return true;
            }
        }

        function displayControl() {
            var v = document.getElementById("rblAmout").value;
            alert(v);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc2:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
            navigationText="" navigationUrl="" headName="红包活动管理" />
        <div class="content" id="divList" runat="server">
            <div class="layout">
                <div class="QueryTerms">
                    <table style="width: 100%" cellpadding="5" cellspacing="5">
                        <tr>
                            <td style="width: 8%">
                                活动名称
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlActivityNameQuery">
                                </asp:DropDownList>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnQuery" runat="server" Text="搜索" CssClass="button" OnClick="btnQuery_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnAdd" runat="server" Text="新建" CssClass="button" OnClick="btnAdd_Click" />
                                <asp:HiddenField runat="server" ID="hiddenMaxRedEnvelope" />
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:Panel ID="Panel_TreasureChestList" runat="server">
                    <div class="div_gridview">
                        <asp:GridView runat="server" ID="gdvTreasureChest" AutoGenerateColumns="False" CssClass="gridview"
                            SkinID="gridviewSkin" OnDataBound="gdvTreasureChest_DataBound">
                            <Columns>
                                <asp:BoundField DataField="treasureChestId" HeaderText="宝箱ID" Visible="false" HeaderStyle-Width="2%" />
                                <asp:BoundField DataField="activityName" HeaderText="活动名称" HeaderStyle-Width="8%" />
                                <asp:BoundField DataField="amount" HeaderText="宝箱金额" HeaderStyle-Width="6%" />
                                <asp:BoundField DataField="amountRule" HeaderText="红包取值规则" HeaderStyle-Width="10%" />
                                <%--<asp:BoundField DataField="count" HeaderText="完全解锁人数" />--%>
                                <asp:BoundField DataField="min" HeaderText="红包最小值" HeaderStyle-Width="6%" />
                                <asp:BoundField DataField="max" HeaderText="红包最大值" HeaderStyle-Width="6%" />
                                <asp:BoundField DataField="defaultAmountRange" HeaderText="金额区间(默认用户)" HeaderStyle-Width="10%" />
                                <asp:BoundField DataField="defaultRateRange" HeaderText="概率区间(默认用户)" HeaderStyle-Width="10%" />
                                <asp:BoundField DataField="newAmountRange" HeaderText="金额区间(新用户)" HeaderStyle-Width="10%" />
                                <asp:BoundField DataField="newRateRange" HeaderText="概率区间(新用户)" HeaderStyle-Width="10%" />
                                <asp:BoundField DataField="isPreventCheat" HeaderText="是否开启防作弊" HeaderStyle-Width="6%" />
                                <%-- <asp:BoundField DataField="quantity" HeaderText="宝箱数量" />--%>
                                <asp:TemplateField HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <img src="../Images/key_edit2.gif" alt="" />
                                        <asp:LinkButton runat="server" ID="lnkbtnModify" CommandName="modify" OnCommand="lnkbtn_OnCommand"
                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem,"treasureChestConfigId") %>'>编辑</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="16%">
                                    <ItemTemplate>
                                        <img src="../Images/key_delete.gif" alt="" />
                                        <asp:LinkButton runat="server" ID="lnkbtnDel" CommandName="del" OnCommand="lnkbtn_OnCommand"
                                            OnClientClick="return confirm('确认删除？');" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"treasureChestConfigId") %>'>删除</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:Panel ID="Panel1" CssClass="gridviewBottom" runat="server">
                            <div class="asp_page">
                                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
                                    NextPageText="下一页" PageSize="10" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                                    TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                                    NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" PageIndexBoxClass="listPageText"
                                    CurrentPageButtonClass="currentButton" ShowPageIndexBox="Always" currentpagebuttonposition="Center"
                                    OnPageChanged="AspNetPager1_PageChanged">
                                </webdiyer:AspNetPager>
                            </div>
                        </asp:Panel>
                    </div>
                </asp:Panel>
            </div>
        </div>
        <div class="content" id="divDetail" runat="server" style="display: none">
            <div class="layout">
                <table class="table" cellpadding="0" cellspacing="0" style="width: 80%">
                    <tr>
                        <th style="width: 20%">
                            活动名称
                        </th>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlActivityName">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            宝箱金额
                        </th>
                        <td>
                            <asp:TextBox runat="server" ID="txbAmount"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            是否开启防作弊
                        </th>
                        <td>
                            <asp:CheckBox runat="server" ID="ckbIsPreventCheat" Checked="true" />
                        </td>
                    </tr>
                    <%--<tr>
                        <th>
                            完全解锁人数
                        </th>
                        <td>
                            <asp:TextBox runat="server" ID="txbCount"></asp:TextBox>
                        </td>
                    </tr>--%>
                    <tr>
                        <th>
                            红包取值规则
                        </th>
                        <td>
                            <asp:RadioButtonList runat="server" ID="rblAmout" RepeatDirection="Horizontal" AutoPostBack="true"
                                OnSelectedIndexChanged="rblAmout_SelectedIndexChanged">
                                <asp:ListItem Value="1" Text="最小值/最大值" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="2" Text="概率取值"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
                <div runat="server" id="divMinMax">
                    <table class="table" cellpadding="0" cellspacing="0" style="width: 80%">
                        <tr>
                            <th style="width: 20%">
                                红包最小值
                            </th>
                            <td>
                                <asp:TextBox runat="server" ID="txbMin"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                红包最大值
                            </th>
                            <td>
                                <asp:TextBox runat="server" ID="txbMax"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div runat="server" id="divRange">
                    <table class="table" cellpadding="0" cellspacing="0" style="width: 80%">
                        <tr>
                            <th style="width: 20%">
                                金额区间（默认用户）
                            </th>
                            <td>
                                <asp:TextBox runat="server" ID="txbDefaultAmountRange"></asp:TextBox>【范例：50,60,70,80,90,100】
                            </td>
                        </tr>
                        <tr>
                            <th>
                                概率区间（默认用户）
                            </th>
                            <td>
                                <asp:TextBox runat="server" ID="txbDefaultRateRange"></asp:TextBox>【范例：0,50,70,80,90,100】
                            </td>
                        </tr>
                        <tr>
                            <th style="width: 20%">
                                金额区间（新用户）
                            </th>
                            <td>
                                <asp:TextBox runat="server" ID="txbNewAmountRange"></asp:TextBox>【范例：20,30,40,50】选填
                            </td>
                        </tr>
                        <tr>
                            <th>
                                概率区间（新用户）
                            </th>
                            <td>
                                <asp:TextBox runat="server" ID="txbNewRateRange"></asp:TextBox>【范例：0,50,85,100】选填
                            </td>
                        </tr>
                    </table>
                </div>
                <table class="table" cellpadding="0" cellspacing="0" style="width: 80%">
                    <%-- <tr>
                        <th style="width: 20%">
                            宝箱数量
                        </th>
                        <td>
                            <asp:TextBox runat="server" ID="txbQuantity"></asp:TextBox>
                        </td>
                    </tr>--%>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Button runat="server" ID="btnSave" Text="保存" CssClass="button" OnClick="btnSave_Click"
                                OnClientClick="return Check();" />
                            <asp:Button runat="server" ID="btnCancle" Text="取消" CssClass="button" OnClick="btnCancle_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <uc1:CheckUser ID="CheckUser1" runat="server" />
    </div>
    </form>
</body>
</html>
