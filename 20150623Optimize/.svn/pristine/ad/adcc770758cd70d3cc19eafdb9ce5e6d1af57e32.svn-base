<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FoodDiaryDetails.aspx.cs" Inherits="OtherStatisticalStatement_FoodDiaryDetails" %>

<%@ Import Namespace="VAGastronomistMobileApp.Model" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">

    <title>美食日记统计</title>

    <link rel="stylesheet" href="http://cdn.bootcss.com/bootstrap/3.2.0/css/bootstrap.min.css">
    <link rel="stylesheet" href="http://cdn.bootcss.com/bootstrap/3.2.0/css/bootstrap-theme.min.css">
    <link href="../Css/daterangepicker-bs3.css" rel="stylesheet" />
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <uc2:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
                navigationText="" navigationUrl="" headName="美食日记统计" />

            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="col-md-4">
                        <div id="reportrange" class="pull-left" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc">
                            <i class="glyphicon glyphicon-calendar fa fa-calendar"></i>
                            <span></span><b class="caret"></b>
                        </div>
                    </div>
                    <input type="hidden" id="start_date" runat="server" />
                    <input type="hidden" id="end_date" runat="server" />
                    <div class="col-md-2">
                        <div class="checkbox">
                            <asp:CheckBox runat="server" ID="checkbox_isPaid" Text="已付款" />
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-group">
                            <span class="input-group-addon">手机号码</span>
                            <asp:TextBox runat="server" ID="txtMobilePhoneNumber" CssClass="form-control" placeholder="手机号码"></asp:TextBox>
                            <span class="input-group-btn">
                                <asp:Button runat="server" CssClass="btn btn-default" ID="button1" OnClick="button1_OnClick" Text="查询" />
                            </span>
                        </div>
                    </div>
                    <div class="col-md-1">
                        <!-- Single button -->
                        <div class="btn-group">
                            <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown">
                                导出 <span class="caret"></span>
                            </button>
                            <ul class="dropdown-menu" role="menu">
                                <li>
                                    <a href="#" id="exportExcel">Excel</a>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>

            <asp:GridView ID="fooddiaryGv" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover table-condensed">
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="昵称" />
                    <asp:BoundField DataField="MobilePhoneNumber" HeaderText="手机号码" />
                    <asp:BoundField DataField="PrePaidSum" HeaderText="支付金额" />
                    <asp:BoundField DataField="PrePayTime" HeaderText="支付时间" />
                    <asp:BoundField DataField="ShopName" HeaderText="门店名称" />
                    <asp:TemplateField HeaderText="新浪微博">
                        <ItemTemplate>
                            <%# ((FoodDiaryShared)DataBinder.Eval(Container.DataItem, "Shared")&FoodDiaryShared.新浪微博)!=FoodDiaryShared.没有分享?1:0 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="QQ好友">
                        <ItemTemplate>
                            <%# ((FoodDiaryShared)DataBinder.Eval(Container.DataItem, "Shared")&FoodDiaryShared.QQ好友)!=FoodDiaryShared.没有分享?1:0 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="QQ空间">
                        <ItemTemplate>
                            <%# ((FoodDiaryShared)DataBinder.Eval(Container.DataItem, "Shared")&FoodDiaryShared.QQ空间)!=FoodDiaryShared.没有分享?1:0 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="微信好友">
                        <ItemTemplate>
                            <%# ((FoodDiaryShared)DataBinder.Eval(Container.DataItem, "Shared")&FoodDiaryShared.微信好友)!=FoodDiaryShared.没有分享?1:0 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="微信朋友圈">
                        <ItemTemplate>
                            <%# ((FoodDiaryShared)DataBinder.Eval(Container.DataItem, "Shared")&FoodDiaryShared.微信朋友圈)!=FoodDiaryShared.没有分享?1:0 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="CreateTime" HeaderText="分享时间" />
                    <asp:BoundField DataField="Hit" HeaderText="点击量" />
                    <asp:TemplateField HeaderText="内容">
                        <ItemTemplate>
                            <button type="button" class="btn btn-default" data-toggle="tooltip" data-placement="top" title='<%# DataBinder.Eval(Container.DataItem, "Content") %>'>查看</button>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:BoundField DataField="Content" HeaderText="内容" />--%>
                </Columns>
            </asp:GridView>
            <div class="asp_page">
                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
                    NextPageText="下一页" PageSize="10" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                    TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                    NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" PageIndexBoxClass="listPageText"
                    CurrentPageButtonClass="currentButton" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center"
                    OnPageChanged="AspNetPager1_PageChanged">
                </webdiyer:AspNetPager>
            </div>
        </div>
        <!-- jQuery文件。务必在bootstrap.min.js 之前引入 -->
        <script src="http://cdn.bootcss.com/jquery/1.11.1/jquery.min.js"></script>
        <!-- 最新的 Bootstrap 核心 JavaScript 文件 -->
        <script src="http://cdn.bootcss.com/bootstrap/3.2.0/js/bootstrap.min.js"></script>
        <script src="../Scripts/moment.js"></script>
        <script src="../Scripts/daterangepicker.js"></script>
        <script type="text/javascript">
            $(function () {
                var cb = function (start, end, label) {
                    //console.log(start.toISOString(), end.toISOString(), label);
                    $('#reportrange span').html(start.format('MM/DD/YYYY') + ' - ' + end.format('MM/DD/YYYY'));
                    $("#start_date").val(start.format('MM/DD/YYYY'));
                    $("#end_date").val(end.format('MM/DD/YYYY'));
                    //alert("Callback has fired: [" + start.format('MMMM D, YYYY') + " to " + end.format('MMMM D, YYYY') + ", label = " + label + "]");
                }

                var optionSet1 = {
                    startDate: moment(),
                    endDate: moment(),
                    minDate: '01/01/2014',
                    maxDate: moment(),
                    dateLimit: { days: 60 },
                    showDropdowns: true,
                    showWeekNumbers: true,
                    timePicker: false,
                    timePickerIncrement: 1,
                    timePicker12Hour: true,
                    ranges: {
                        '今天': [moment(), moment()],
                        '昨天': [moment().subtract('days', 1), moment().subtract('days', 1)],
                        '过去7天': [moment().subtract('days', 6), moment()],
                        '过去30天': [moment().subtract('days', 29), moment()],
                        '这个月': [moment().startOf('month'), moment().endOf('month')],
                        '上个月': [moment().subtract('month', 1).startOf('month'), moment().subtract('month', 1).endOf('month')]
                    },
                    opens: 'left',
                    buttonClasses: ['btn btn-default'],
                    applyClass: 'btn-small btn-primary',
                    cancelClass: 'btn-small',
                    format: 'MM/DD/YYYY',
                    separator: ' to ',
                    locale: {
                        applyLabel: '确定',
                        cancelLabel: '取消',
                        fromLabel: '开始',
                        toLabel: '结束',
                        customRangeLabel: '自定义',
                        daysOfWeek: ['日', '一', '二', '三', '四', '五', '六'],
                        monthNames: ['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月'],
                        firstDay: 1
                    }
                };

                $('#reportrange span').html($("#start_date").val() + ' - ' + $("#end_date").val());

                $('#reportrange').daterangepicker(optionSet1, cb);


                $("#exportExcel").click(function () {

                    window.open("FoodDiaryDetailsOutFile.aspx?" + "startDate=" + $("#start_date").val() + "&endDate=" + $("#end_date").val() + "&isPaid=" + $("#checkbox_isPaid").is(':checked') + "&mobilePhoneNumber=" + $("#txtMobilePhoneNumber").val() + "&outType=1");
                    
                });
            })
        </script>
        <uc1:CheckUser runat="server" ID="checkUser1" />
    </form>

</body>
</html>
