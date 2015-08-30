﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="preOrderShopVerified.aspx.cs" Inherits="CompanyPages_preOrderShopVerified" %>
<!DOCTYPE html>
<title>悠先收银宝平台-财务对账</title>
<body>
<div>
    <div class="contentContainer pageVerified" id="page">
		<div class="header">
		
        <div class="headerSprite">
            <div class="text">
		        <h2 class="headerTitle verified">财务对账</h2>
                <div class="search" id="search000">
                    <p>手机号码或尾号：
                    <input type="text" class="inputText" value="请输入手机号码或尾号" />
                    <!--<button type="button" class="btnSearch">查询</button>-->
                    </p>
                 </div>
                 <div class="date" id="date">
					<div id="calendarStr"></div>
					<div id="calendarEnd"></div>
                    <em class="title">起止时间：</em>
                    <input type="text" class="inputText" id="dateStr" /><span class="division"> -</span> <input class="inputText" id="dateEnd" type="text" />
                 </div>
                 
                 
                 <style type="text/css">
                 	.couponType{
									  position: absolute;
									  top: 46px;
									  left: 325px;
									  width: 250px;
									  height: 27px;
                 	}
                 	#couponType{
                 		border: 1px solid #e7e2c8;
                 		width: 120px;
                 		font-size: 12px;
                 		height: 27px;
               		  color: #70706e;
                 	}
                 	#couponType option{
                 		padding: 8px 0;
                 	}
                 	#searchBtn{
									  position: absolute;
									  right: 15px;
									  top: 47px;
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
                 	#pageShow{
                 		top: 0;
                 		height: 29px;
                 	}
                 	#search000{
                 		top: 45px;
                 		right: 105px;
                 		width: auto;
                 		border: none;
									  font-size: 12px;
									  color: #b6ab91;
									  height: 29px;
									  line-height: 29px;
                 	}
                 	#search000 input{
                 		width: 155px;
                 		float: right;
                 		border: 1px solid #e7e2c8;
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
                 
                 <div class="pageShow" id="pageShow">
                    <em class="title">显示行数：</em>
                    <ul class="btn">
                        <li class="cur"><a href="#">10行</a></li>
                        <li><a href="#">20行</a></li>
                        <li><a href="#">30行</a></li>
                    </ul>
                 </div>
             </div>
        </div>
	</div>
    <div class="section">
        <div class="pageController">
          <div class="dataTotal dang pay fLeft" id="dataTotal">
            <span class="num">共<em>0</em>单</span>
            <span class="sum">总金额<em>￥00.00</em></span>
            <span class="pay">支付金额<em>￥00.00</em></span>
            <span class="useCount">使用抵扣券<em>0</em>张</span>
            <span class="realTotal">实际抵扣金额共计<em>￥00.00</em></span>
          </div>
        </div>
        <div class="dataContainer verifiedContainer">
            <div class="dataSprite">
				<div class="dataTable" id="dataTable">
					<table class="staticTable" cellpadding="0" cellspacing="0" border="0">
						<thead>
							<tr class="h">
								<th class="yui3-datatable-col-preOrder19dianId">流水号</th><th class="yui3-datatable-col-UserName">用户昵称</th><th class="yui3-datatable-col-mobilePhoneNumber">手机号码</th><th class="yui3-datatable-col-prePayTime">支付时间</th><th class="yui3-datatable-col-prePaidSum">支付金额（元）</th><th class="yui3-datatable-col-isStatus">状态</th><th class="yui3-datatable-col-btn">操作</th>
							</tr>
						</thead>
						<tbody>
						</tbody>
					</table>
				</div>
            </div>
        </div>
        <div class="pageController">
            <div class="pageContent fRight" id="pageContent">
				<div class="yui3-paginator-multi">
					<div class="page">
						<a href="#"><code>&lt;</code></a>
						<a href="#"><code>&gt;</code></a>
					</div>
				</div>
                <ul class="pageTotal">
					<li class="txt">
						共<em>0</em>页
					</li>
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
