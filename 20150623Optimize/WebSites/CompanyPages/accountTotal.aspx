﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="accountTotal.aspx.cs" Inherits="CompanyPages_accountTotal" %>

<!DOCTYPE html>
<title>悠先收银宝平台-帐户汇总</title>
<body>
    <div>
        <div class="contentContainer accountTotal" id="page">
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
                    <div class="date" id="date">
                        <em class="title"></em>
                        <input type="text" class="inputText" id="dateStr" autocomplete="off" />
							<span class="division">-</span>
                        <input class="inputText" id="dateEnd" type="text" autocomplete="off" />
                    </div>
                    
                 
                 
                 <style type="text/css">
                 	.couponType{
									  position: absolute;
									  top: 0;
									  left: 510px;
									  width: 250px;
									  height: 29px;
                 	}
                 	#couponType{
                 		border: 1px solid #e7e2c8;
                 		width: 120px;
                 		font-size: 12px;
                 		height: 29px;
               		  color: #70706e;
                 	}
                 	#couponType option{
                 		padding: 8px 0;
                 	}
                 	#searchBtn{
									  position: absolute;
									  left: 720px;
									  _left: 8px;
									  top: 2px;
                 		width: 65px;
									  height: 25px;
									  line-height: 25px;
									  display: block;
									  background: url(images/btn_sprite.png) 0px -175px no-repeat;
									  color: #ffffeb;
									  font-weight: bold;
									  text-align: center;
									  text-decoration: none;
									  cursor: pointer;
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
                
                <div id="search0">
                	<div class="btnSearch" id="searchBtn">查询</div>
                </div>
                
                
					<div class="status" id="statusDate">
						<em class="title">入座时间：</em>
						<ul class="btn">
							<li class="today"><a href="javascript:;" name="4">今天</a></li>
							<li class="yesterday"><a href="javascript:;" name="5">昨天</a></li>
							<li class="week"><a href="javascript:;" name="2">本周</a></li>
						</ul>
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
                    下载账单：<a class="xls" href="javascript:;">Excel格式</a><a class="txt" href="javascript:;">Txt格式</a></div>
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
                    <div class="dataTotal dang sum fLeft" id="dataTotal">
                        <span class="num">共<em>0</em>单</span> <span class="sum">总<em>￥00.00</em></span>
                    </div>
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