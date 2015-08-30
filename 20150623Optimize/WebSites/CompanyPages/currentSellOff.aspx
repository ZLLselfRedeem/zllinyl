<%@ Page Language="C#" AutoEventWireup="true" CodeFile="currentSellOff.aspx.cs" Inherits="CompanyPages_currentSellOff" %>
<!DOCTYPE html>
<title>悠先收银宝平台-沽清管理</title>
<body>
<div>
    <div class="contentContainer" id="page">
    <div class="header">
        <div class="headerSprite">
            <div class="text sellOffHeader">
		        <h2 class="headerTitle sellOff">沽清管理</h2>
				<div class="cates">
                    <span class="txt">选择分类:</span>
					<span id="catesSelect">
						<select id="yui_3_12_0_1_1381630434230_633" class="s" name="s">
							<option value="835">全部</option>
						</select>
					</span>
                </div>
                <div class="search" id="search">
                    <p>
						<input type="text" class="inputText" value="按菜品名 简拼 全拼搜索" />
						<button type="button" class="btnSearch">查询</button>
                    </p>
                 </div>
                 <div class="pageShow" id="pageShow">
                    <em class="title">显示行数：</em>
                    <ul class="btn">
                        <li class="cur"><a href="javascript:;">10行</a></li>
                        <li><a href="javascript:;">20行</a></li>
                        <li><a href="javascript:;">30行</a></li>
                    </ul>
                 </div>
             </div>
        </div>
	</div>
    <span class="arrow_selloff">沽清</span>
    <div class="section">
        
        <div class="selloffEditor fLeft">
        <h3 class="title">当前选中分类菜品</h3>
        <div class="dataContainer">
            <ul class="headerTxt">
                <li>  </li>
            </ul>
            <div class="dataSprite">
				<div class="dataTable" id="dataTable">
					<table class="staticTable" cellpadding="0" cellspacing="0" border="0">
						<thead>
							<tr>
								<th class="yui3-datatable-col-DishName">名称</th><th class="yui3-datatable-col-ScaleName">规格</th><th class="yui3-datatable-col-DishPrice">价格</th><th class="yui3-datatable-col-btn">操作</th>
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
						<input type="text" class="inputText" />
					</li>
					<li class="txt">
						<a class="btn" href="#">跳至</a>
					</li>
				</ul>
            </div>
        </div>
        </div>
        <div class="selloffEditor fRight" id="sellOffBeen">
        <h3 class="title">已沽清菜品</h3>
        <div class="dataContainer">
			<div id="cbAll">
				<ul class="checkAll">
					<li class="inputSprite"><input class="inputCheck" type="checkbox" value="" /> 全选</li>
					<li class="btnSprite"><a class="btn" href="#">批量取消</a><a class="btn" href="#">全部取消</a></li>
				</ul>
			</div>
            <div class="dataSprite">
				<div class="dataTable">
					<table class="staticTable" cellpadding="0" cellspacing="0" border="0">
						<thead>
							<tr>
								<th class="yui3-datatable-col-DishName">名称</th><th class="yui3-datatable-col-ScaleName">规格</th><th class="yui3-datatable-col-DishPrice">价格</th><th class="yui3-datatable-col-btn">操作</th>
							</tr>
						</thead>
						<tbody>
						</tbody>
					</table>
				</div>
            </div>
        </div>
        <div class="pageController">
            <div class="pageContent fRight" id="pageSellOffBeen">
				<div class="yui3-paginator-multi">
					<div class="page">
						<a href="#"><code>&lt;</code></a>
						<a href="#"><code>&gt;</code></a>
					</div>
				</div>
                <ul class="pageTotal">
					<li>
						共<em>0</em>页
					</li>
					<li><input type="text" class="inputText" /></li>
					<li>
						<a class="btn" href="#">跳至</a>
					</li>
                </ul>
			</div>
        </div>
        </div>
        <div class="divClear"></div>
    </div>

	
	
	</div>
</div>
</body>
</html>