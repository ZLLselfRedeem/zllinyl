<%@ Page Language="C#" AutoEventWireup="true" CodeFile="vipSpeedConfig.aspx.cs" Inherits="ViewAllocVip_shopVIPSpeedConfigManage" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>会员增速配置</title>
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
            var rules = "^[1-9]{1}[0-9]{0,}\.[0-9]{2}$"; //第一位为 1-9 之间的数字，之后为 0-9 不限定位数，一个小数点后两位数字
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
            if (document.getElementById("txbMinSpeed").value.length == 0) {
                err += "【最小增长因子不能为空】\r\n";
            }
            if (document.getElementById("txbMinSpeed").value.length > 0) {
                if (!isNumber(document.getElementById("txbMinSpeed").value)) {
                    err += "【最小增长因子只能为正整数】\r\n";
                }
                if (document.getElementById("txbMinSpeed").value < 1 || document.getElementById("txbMinSpeed").value > 100000) {
                    err += "【最小增长因子取值范围1~100000】\r\n";
                }
            }
            if (document.getElementById("txbMaxSpeed").value.length == 0) {
                err += "【最大增长因子不能为空】\r\n";
            }
            if (document.getElementById("txbMaxSpeed").value.length > 0) {
                if (!isNumber(document.getElementById("txbMaxSpeed").value)) {
                    err += "【最大增长因子只能为正整数】\r\n";
                }
                if (document.getElementById("txbMaxSpeed").value < 1 || document.getElementById("txbMaxSpeed").value > 100000) {
                    err += "【最大增长因子取值范围1~100000】\r\n";
                }
            }
            if (document.getElementById("txbMinSpeed").value - document.getElementById("txbMaxSpeed").value >= 0) {
                err += "【最小增长因子不应超过最大增长因子】\r\n";
            }
            if (err.length > 0) {
                alert(msg + err);
                return false;
            }
            else {
                return true;
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="content" id="divList" runat="server">
        <div class="layout">
            <div class="QueryTerms">
                <table style="width: 100%" cellpadding="5" cellspacing="5">
                    <tr>
                        <td>
                            区域
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlProvinceID" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProvinceID_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlCityID" runat="server" AutoPostBack="True">
                            </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnQuery" runat="server" Text="搜索" CssClass="button" OnClick="btnQuery_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnAdd" runat="server" Text="新建" CssClass="button" OnClick="btnAdd_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <asp:Panel ID="Panel_vipSpeedList" runat="server">
                <div class="div_gridview">
                    <asp:GridView runat="server" ID="gdvVipSpeed" AutoGenerateColumns="False" CssClass="gridview"
                        SkinID="gridviewSkin">
                        <Columns>
                            <asp:BoundField DataField="CityId" HeaderText="城市编号" />
                            <asp:BoundField DataField="CityName" HeaderText="城市" />
                            <asp:BoundField DataField="MinSpeed" HeaderText="最小增长因子" />
                            <asp:BoundField DataField="MaxSpeed" HeaderText="最大增长因子" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <img src="../Images/key_delete.gif" alt="" />
                                    <asp:LinkButton runat="server" ID="lnkbtnDel" CommandName="del" OnCommand="lnkbtnEdit_OnCommand"
                                        OnClientClick="return confirm('确认删除？');" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"CityId") %>'>删除</asp:LinkButton>
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
                                CurrentPageButtonClass="currentButton" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center"
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
            <table class="table" cellpadding="0" cellspacing="0">
                <tr>
                    <th>
                        城市
                    </th>
                    <td>
                        <asp:DropDownList ID="ddlProvinceDetail" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProvinceDetail_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlCityDetail" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>
                        最小增长因子
                    </th>
                    <td>
                        <asp:TextBox runat="server" ID="txbMinSpeed"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        最大增长因子
                    </th>
                    <td>
                        <asp:TextBox runat="server" ID="txbMaxSpeed"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button runat="server" ID="btnSave" Text="保存" CssClass="button" OnClientClick="return Check();"
                            OnClick="btnSave_Click" />
                        <asp:Button runat="server" ID="btnCancle" Text="取消" CssClass="button" OnClick="btnCancle_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <uc1:CheckUser ID="CheckUser1" runat="server" />
    </form>
</body>
</html>
