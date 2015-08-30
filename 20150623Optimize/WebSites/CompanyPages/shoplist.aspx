<%@ Page Language="C#" AutoEventWireup="true" CodeFile="shoplist.aspx.cs" Inherits="CompanyPages_shoplist" %>
<!DOCTYPE html>
<title>悠先收银宝平台-店铺管理</title>
<body>
<div>
    <div class="contentContainer" id="page">
    <div class="header">
        <div class="headerSprite">
            <div class="text shoplist">
				<h2 class="headerTitle">店铺管理</h2>
                <div class="search" id="search">
                    <p>
                    <input type="text" class="inputText" value="请输入门店名称" />
                    <button type="button" class="btnSearch">查询</button>
                    </p>
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
        <div class="dataContainer shoplist" id="shopManageList">
            <p class="disBtn companyAddBtn" id="addCompany">
                <a class="btn add" href="companyManage.aspx?type=shoplist&m=add&a=0">新增店铺</a>
            </p>
            <div class="dataSprite">
				<div class="dataTable" id="dataTable">
					<table class="staticTable" cellpadding="0" cellspacing="0" border="0">
						<thead>
							<tr>
								<!--<th class="">门店名称</th><th class="">所属公司</th><th class="">环境展示</th><th class="">杂项信息</th><th class="">提款方式</th><th class="">佣金</th><th class="">折扣</th><th class="">银行账户</th><th class="yui3-datatable-col-btn">操作</th>-->
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
