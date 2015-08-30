﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="shopsundrylist.aspx.cs" Inherits="CompanyPages_shopsundrylist" %>
<!DOCTYPE html>
<title>悠先收银宝平台-公司管理</title>
<body>
<div>
    <div class="contentContainer" id="page">
    <div class="header">
        <div class="headerSprite">
            <div class="text">
				<h2 class="headerTitle shopsundrymanage">杂项信息</h2>
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
        <div class="dataContainer shopsundrylist">
            <p class="disBtn companyAccountTopBtn" id="addSprite">
                <a class="btn add" href="companyManage.aspx?type=shopsundrylist&m=add&status=1&syid=0">新增杂项</a>
				<a class="btn back" href="shoplist.aspx">返回</a>
            </p>
            <div class="dataSprite">
				<div class="dataTable" id="dataTable">
					<table class="staticTable" cellpadding="0" cellspacing="0" border="0">
						<thead>
							<tr>
								<th class="">杂项名称</th><th class="">状态</th><th class="yui3-datatable-col-btn">操作</th>
							</tr>
						</thead>
						<tbody>
						</tbody>
					</table>
				</div>
            </div>
        </div>
        <div class="pageController">
            <div class="dataTotal xian fLeft" id="dataTotal">
				<span class="num">共<em>0</em><i class="unit2">项</i></span>
            </div>
            <div class="pageContent fRight"  id="pageContent">
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
