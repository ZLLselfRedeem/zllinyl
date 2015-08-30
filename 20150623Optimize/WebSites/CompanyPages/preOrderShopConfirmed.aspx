﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="preOrderShopConfirmed.aspx.cs" Inherits="CompanyPages_preOrderShopConfirmed" %>
<!DOCTYPE html>
<title>悠先收银宝平台-收银审核</title>
<body>
<div>
    <div class="contentContainer pageComfirmed" id="page">
		<div class="header">
			<div class="headerSprite">
				<div class="text">
					<h2 class="headerTitle confirmed">收银审核</h2>
					<div class="search" id="search">
						<p>
						<input type="text" class="inputText" value="请输入手机号码或尾号" />
						<button type="button" class="btnSearch">查询</button>
						</p>
					 </div>
					 <div class="date" id="date">
						<em class="title">起止时间：</em>
						<input class="inputText" id="dateStr" name="checkIn" autocomplete="off" type="text" /><span class="division"> -</span> <input class="inputText" id="dateEnd" type="text" />
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
			<div class="dataContainer comfirmed">
				<div class="dataSprite">
					<div class="dataTable" id="dataTable">
						<table class="staticTable" cellpadding="0" cellspacing="0" border="0">
							<thead>
								<tr class="h">
									<th class="yui3-datatable-col-preOrder19dianId">流水号</th><th class="yui3-datatable-col-UserName">用户昵称</th><th class="yui3-datatable-col-mobilePhoneNumber">手机号码</th><th class="yui3-datatable-col-prePayTime">支付时间</th><th class="yui3-datatable-col-prePaidSum">支付金额</th><th class="yui3-datatable-col-isStatus">状态</th><th class="yui3-datatable-col-btn">操作</th>
								</tr>
							</thead>
							<tbody>
							</tbody>
						</table>
					</div>
				</div>
			</div>
			<div class="pageController">
				<div class="dataTotal dang pay fLeft" id="dataTotal">
					<span class="num">共<em>0</em>单</span>
					<span class="sum">总金额<em>￥00.00</em></span>
					<span class="pay">支付金额<em>￥00.00</em></span>
				</div>
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
			<div style="position:relative;left:0;"><div id="aa" style="position:absolute;left:0;right:0;"></div></div>
		</div>

		
	</div>
</div>
</body>
</html>
