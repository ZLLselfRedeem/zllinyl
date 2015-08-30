<%@ Page Language="C#" AutoEventWireup="true" CodeFile="accountPreOrderDetail.aspx.cs" Inherits="CompanyPages_accountPreOrderDetail" %>
<!DOCTYPE html>
<title>悠先收银宝平台-收银审核</title>
<body>
<div>
    <div class="contentContainer" id="page">
	
	
    <div class="section nyPage">
        <div class="showList" id="showList">
            <div class="item" id="commonInfo">
                <h3 class="title">基本信息</h3>
                <table class="dataTable" cellpadding="0" cellspacing="0" border="0">
                    <thead>
                        <tr>
                           <th class="w100">流水号</th><th class="w100">用户昵称</th><th class="w100">手机号码</th><th class="w160">发票抬头</th><th class="w100">支付金额</th><th class="w260">支付时间</th>
                        </tr>
                    </thead>
                    <tbody> 
                    </tbody>
                </table>
            </div>
            <div class="item" id="orderInfo">
                <h3 class="title">菜品信息</h3>
                <table class="dataTable" cellpadding="0" cellspacing="0" border="0">
                    <thead>
                        <tr>
                            <th class="w176">行号</th><th class="w436" style="text-align:left;">菜名</th><th class="w176">价格</th><th class="w176">数量</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
			<!--
            <div class="item"id="couponInfo">
                <h3 class="title">优惠券信息</h3>
                <table class="dataTable" cellpadding="0" cellspacing="0" border="0">
                    <thead>
                        <tr>
                            <th class="w260">优惠券名称 </th><th class="w176">内容</th><th class="w176">使用数量</th><th class="w176">使用时间</th><th class="w176">状态</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
			-->
            <div class="slidePanel" id="slidePanel">
                <div class="item" id="payInfo">
                    <h3 class="title">支付信息</h3>
                    <table class="dataTable" cellpadding="0" cellspacing="0" border="0">
                        <thead>
                            <tr>
                                <th class="w130">实际支付金额(￥)</th><th class="w130">原价</th><th class="w130">补差价金额</th><th class="w230">第三方支付金额</th><th class="w230">红包金额</th><th class="w230">粮票金额</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
				<div class="item" id="discountInfo">
                    <h3 class="title">优惠信息</h3>
                    <table class="dataTable" cellpadding="0" cellspacing="0" border="0">
                        <thead>
                            <tr>
                                <th class="w176">抵扣总金额</th><th class="w230">折扣金额</th><th class="w230">抵价券金额</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
				<div class="item" id="refundInfo">
                    <h3 class="title">退款信息</h3>
                    <table class="dataTable" cellpadding="0" cellspacing="0" border="0">
                        <thead>
                            <tr>
                                <th class="w176">退款状态</th><th class="w230">退款金额(￥)</th><th class="w230">退款时间</th><th class="w230">退款原因</th><th class="w176">退款人</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
				<!--
                <div class="item" id="payBack">
                    <h3 class="title">支付奖励信息</h3>
                    <table class="dataTable" cellpadding="0" cellspacing="0" border="0">
                        <thead>
                            <tr>
                                <th class="w260">支付奖励</th><th class="w230">优惠描述</th><th class="w230">奖励提前时间(小时)</th><th class="w230">是否已奖励</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
				
                <div class="item" id="payResult">
                    <h3 class="title">结算信息</h3>
                    <table class="dataTable" cellpadding="0" cellspacing="0" border="0">
                        <thead>
                            <tr>
                                <th class="w230">第三方支付手续费(￥)</th><th class="w176">结算金额(￥)</th><th class="w176">已结算金额(￥)</th><th class="w176">友络佣金</th><th class="w176">是否结算</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
				-->
				<div class="item" id="confirmedInfo">
                    <h3 class="title">入座信息</h3>
                    <table class="dataTable" cellpadding="0" cellspacing="0" border="0">
                        <thead>
                            <tr>
                                <th class="w176">行号</th><th class="w176">操作者</th><th class="w176">操作者职位</th><th class="w230">操作时间</th><th class="w176">操作状态</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
				<div class="item" id="verifiedInfo">
                    <h3 class="title">对账信息</h3>
                    <table class="dataTable" cellpadding="0" cellspacing="0" border="0">
                        <thead>
                            <tr>
                                <th class="w176">行号</th><th class="w176">操作者</th><th class="w176">操作者职位</th><th class="w230">操作时间</th><th class="w176">操作状态</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
               
            </div>
			<a class="bar" href="javascript:;"> 
                更多信息(收起)<span class="arrow">&nbsp;</span>
            </a>
        </div>
        <div class="comment" id="comment">
            <dl class="txtSprite confirmed">
                <dt>入座管理信息</dt>
                <dd class="txt"><i>&nbsp;</i>该点单暂无入座信息</dd>
            </dl>
            <dl class="txtSprite verified">
                <dt>财务对账信息</dt>
                <dd class="txt"><i>&nbsp;</i>该点单暂无对账信息</dd>
            </dl>
            <div class="btnSprite">
                <a class="btn cancel operate" style="display:none;" href="javascript:;"></a>
				<a class="btn back" href="accountDetails.aspx">返回</a>
            </div>
        </div>
    </div>
	
	
	</div>
</div>
</body>
</html>