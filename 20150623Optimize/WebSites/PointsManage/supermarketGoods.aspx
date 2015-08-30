<%@ Page Language="C#" AutoEventWireup="true" CodeFile="supermarketGoods.aspx.cs"
    Inherits="PointsManage_supermarketGoods" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>超市商品</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script type="text/javascript">
        //    //必需是整数
        function isNumber(value) {
            //var rules = "^[1-9][0-9]*$"; //正整数
            var rules = "^[1-9]\d*|0$"; //整数

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
            if (document.getElementById("txbName").value.length == 0) {
                err += "【商品名称不能为空】\r\n";
            }
            if (document.getElementById("txbName").value.length > 16) {
                err += "【商品名称不能超过16个字】\r\n";
             }            
            if (document.getElementById("txbExchangePrice").value.length == 0) {
                err += "【兑换价格不能为空】\r\n";
            }
            if (document.getElementById("txbExchangePrice").value.length > 0) {
                if (!isFloat(document.getElementById("txbExchangePrice").value) && !isNumber(document.getElementById("txbExchangePrice").value)) {
                    err += "【请输入正确的兑换价格】\r\n";
                }
            }
            if (document.getElementById("txbResidueQuantity").value.length == 0) {
                err += "【库存剩余不能为空】\r\n";
            }
            if (document.getElementById("txbResidueQuantity").value.length > 0) {
                if (!isNumber(document.getElementById("txbResidueQuantity").value)) {
                    err += "【请输入正确的库存剩余】\r\n";
                }
            }
            if (document.getElementById("txbHaveExchangeQuantity").value.length > 0) {
                if (!isNumber(document.getElementById("txbHaveExchangeQuantity").value)) {
                    err += "【请输入正确的已兑换数量】\r\n";
                }
            }

                        var str = document.getElementById("fileUpload").value;
                        var pos = str.lastIndexOf(".");
                        var lastname = str.substring(pos, str.length);
                        if (str == "") {//新增
                            if (lastname.length == 0) {
                                err += "【请先点击[浏览]按钮选择要上传的商品图片！】\r\n";
                            }
                            if (lastname.length > 0 && lastname.toLowerCase() != ".png") {
                                err += "【您上传的文件类型为" + lastname + "，图片必须为.png类型】\r\n";
                            }
                        }
                        else {//修改
                            if (lastname.length > 0 && lastname.toLowerCase() != ".png") {
                                err += "【您上传的文件类型为" + lastname + "，图片必须为.png类型】\r\n";
                            }
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
                            商品名称&nbsp;&nbsp;
                            <asp:TextBox runat="server" ID="txbGoodsName"></asp:TextBox>&nbsp;&nbsp;
                            <asp:Button ID="btnQuery" runat="server" Text="搜索" CssClass="button" OnClick="btnQuery_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnAdd" runat="server" Text="新建商品" CssClass="button" OnClick="btnAdd_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <asp:Panel ID="Panel_GoodsList" runat="server">
                <div class="div_gridview">
                    <asp:GridView runat="server" ID="gdvGoodsList" AutoGenerateColumns="False" CssClass="gridview"
                        DataKeyNames="boolVisible,pictureName" SkinID="gridviewSkin" OnDataBound="gdvGoodsList_DataBound">
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="商品编号" />
                            <asp:BoundField DataField="name" HeaderText="商品名" />
                            <asp:TemplateField HeaderText="商品图片">
                                <ItemTemplate>
                                    <asp:Image runat="server" ID="imgGoods" AlternateText="" Height="60" Width="60" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="exchangePrice" HeaderText="兑换价格" />
                            <asp:BoundField DataField="residueQuantity" HeaderText="库存剩余" />
                            <asp:BoundField DataField="haveExchangeQuantity" HeaderText="已兑换数量" />
                            <asp:TemplateField HeaderText="用户可见">
                                <ItemTemplate>
                                    <asp:CheckBox Enabled="false" runat="server" ID="ckbVisible" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <img src="../Images/key_edit2.gif" alt="" />
                                    <asp:LinkButton runat="server" ID="lnkbtnModify" CommandName="modify" OnCommand="lnkbtnEdit_OnCommand"
                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem,"id") %>'>编辑</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <img src="../Images/key_delete.gif" alt="" />
                                    <asp:LinkButton runat="server" ID="lnkbtnDel" CommandName="del" OnCommand="lnkbtnEdit_OnCommand"
                                        OnClientClick="return confirm('确认删除？');" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"id") %>'>删除</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:Panel ID="Panel1" CssClass="gridviewBottom" runat="server">
                        <div class="asp_page">
                            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
                                NextPageText="下一页" PageSize="5" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
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
                        商品编号
                    </th>
                    <td>
                        <asp:Label runat="server" ID="lbGoodsId"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        商品名称
                    </th>
                    <td>
                        <asp:TextBox runat="server" ID="txbName"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        商品图片
                    </th>
                    <td>
                        <asp:Image runat="server" ID="imgGood" AlternateText="暂无图片" Height="200" Style="width: 200" />
                        <br />
                        <asp:FileUpload runat="server" ID="fileUpload" />
                        <br />
                        （图片尺寸:265*265）（类型:png）
                    </td>
                </tr>
                <tr>
                    <th>
                        兑换价格
                    </th>
                    <td>
                        <asp:TextBox runat="server" ID="txbExchangePrice"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        库存剩余
                    </th>
                    <td>
                        <asp:TextBox runat="server" ID="txbResidueQuantity"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        已兑换数量
                    </th>
                    <td>
                        <asp:TextBox runat="server" ID="txbHaveExchangeQuantity"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        用户可见
                    </th>
                    <td>
                        <asp:CheckBox runat="server" ID="ckbVisible" />
                    </td>
                </tr>
                <tr>
                    <th>
                        备注
                    </th>
                    <td>
                        <asp:TextBox runat="server" ID="txbRemark" TextMode="MultiLine" Height="70px" Width="220px"></asp:TextBox>
                    </td>
                </tr>
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
    </form>
</body>
</html>
