﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="accountDetails.aspx.cs" Inherits="CompanyPages_accountDetails" %>

<!DOCTYPE html>
<title>悠先收银宝平台-帐户明细</title>
<body>
    <div>
        <div class="contentContainer" id="page">
            <div class="header">
                <div class="headerSprite">
                    <div class="text accountHeader">
                        <h2 class="headerTitle tab" id="tab"></h2>
                        <ul class="account" id="account">
                            <li><em>累计总收入：</em>
                                <p>
                                    <i>0</i>元</p>
                            </li>
                            <li class="able"><em>可提取余额：</em>
                                <p>
                                    <i>0</i>元</p>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="section">
                <div class="accountTitle">
                    <div class="date" id="accountDate">
                        <em class="title">入座时间：</em>
                        <input type="text" class="inputText" id="dateStr" autocomplete="off" />
							<span class="division">-</span>
                        <input class="inputText" id="dateEnd" type="text" autocomplete="off" />
                    </div>
                    <div class="pay" id="pay">
                        <em class="title">金额范围：</em>
                        <input type="text" class="inputText" autocomplete="off" /><span class="division"> -</span>
                        <input class="inputText" type="text" autocomplete="off" />
                    </div>
                    <div class="cates">
						<span id="accountSelect">
							<input type="radio" name="account" value="0" checked="checked" /> 所有
							<input type="radio" name="account" value="5" /> 只看订单
							<input type="radio" name="account" value="4" /> 只看退款
              <input type="radio" name="account" value="6" /> 结账扣款
                        </span>
                    </div>
                    
                 <style type="text/css">
                 	.couponType{
									  position: absolute;
									  top: 0;
									  left: 683px;
									  width: 250px;
									  height: 27px;
                 	}
                 	#couponType{
                 		border: 1px solid #e7e2c8;
                 		width: 142px;
                 		font-size: 12px;
                 		height: 27px;
               		  color: #70706e;
                 	}
                 	#couponType option{
                 		padding: 8px 0;
                 	}
                 	#couponType span{
                 		width: 80px;
                 	}
                 </style>
                <div class="couponType">
                 	<span class="title">抵扣券类型：</span>
                 	<div id="couponWrap">
	                <select id="couponType">
	                	<option value="1">通用券</option>
	                 	<option value="3">会员营销礼券</option>
	                 	<option value="0" selected>所有</option>
	                </select>
                 	</div>
                </div>
					<div class="search searchPhone" id="mobilePhone">
                        <em class="title">手机号码：</em><input type="text" class="inputText" value="" />
                    </div>
                    <div class="search" id="search">
                        <em class="title">流&ensp;水&ensp;号：</em><input type="text" class="inputText" value="" />
                        <a class="btnSearch" href="javascript:;">查询</a>
                    </div>
                    <div class="pageShow" id="pageShow">
                        <em class="title">显示行数：</em>
                        <ul class="btn">
                            <li class="cur"><a href="#">10行</a></li>
                            <li><a href="#">20行</a></li>
                            <li><a href="#">30行</a></li>
                        </ul>
                    </div>
                </div>
                <div class="downLoad" id="downLoad">
                    下载账单：<a class="d xls" href="javascript:;">Excel格式</a><a class="d txt" href="javascript:;">Txt格式</a>
					<a class="back" id="accountDetailBack" href="accountTotal.aspx">返回汇总>></a>
				</div>
                <div class="dataContainer">
                    <div class="dataSprite">
                        <div class="dataTable accountDetail" id="dataTable">
                            <table class="staticTable" cellpadding="0" cellspacing="0" border="0">
                                <thead>
                                    <tr>
                                        <th class="yui3-datatable-col-id">
                                            序号
                                        </th>
                                        <th class="yui3-datatable-col-Btime">
                                            时间
                                        </th>
                                        <th class="yui3-datatable-col-type">
                                            类型
                                        </th>
                                        <th class="yui3-datatable-col-pay">
                                            金额
                                        </th>
                                        <th class="yui3-datatable-col-payd">
                                            余额
                                        </th>
                                        <th class="yui3-datatable-col-btn">
                                            详情
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="pageController">
					<!--
                    <div class="dataTotal dang income  fLeft" id="dataTotal">
                        <span class="num">共<em>0</em>单</span> <span class="sum">总收入<em>￥00.00</em></span>
                    </div>
					-->
                    <div class="pageContent fRight" id="pageContent">
                        <div class="yui3-paginator-multi">
                            <div class="page">
                                <a href="#"><code>&lt;</code></a> <a href="#"><code>&gt;</code></a>
                            </div>
                        </div>
                        <ul class="pageTotal">
                            <li class="txt">共<em>0</em>页 </li>
                           <li class="txt">
								&nbsp;&nbsp;&nbsp;&nbsp;<input type="text" class="inputText" />
							</li>
							<li class="txt">
								<a class="btn" href="#">跳至</a>
							</li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html> 