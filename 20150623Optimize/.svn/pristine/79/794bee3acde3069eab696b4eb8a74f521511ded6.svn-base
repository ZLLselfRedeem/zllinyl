<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dishList.aspx.cs" Inherits="CompanyPages_dishList" %>
<!DOCTYPE html>
<title>悠先收银宝平台-菜品管理</title>
<body>
<div>
    <div class="contentContainer" id="page">
    <div class="header">
        <div class="headerSprite">
            <div class="text dishList">
				<h2 class="headerTitle tab" id="tab">
				</h2>
                
                <div class="cates">
                    <span class="txt">选择分类:</span>
					<span id="catesSelect">
						<select class="s" name="s">
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
                        <li class="cur"><a href="#">10行</a></li>
                        <li><a href="#">20行</a></li>
                        <li><a href="#">30行</a></li>
                    </ul>
                 </div>
             </div>
        </div>
	</div>
    <div class="section dishList">
        <div class="dataContainer">
            <p class="disBtn" id="disBtn">
                <a class="btn add" href="dishManage.aspx?type=add">添加菜品</a>
				<a class="btn add" href="dishManageMutil.aspx?type=mutil">批量添加</a>
                <a class="btn update" href="javascript:;">更新菜单</a>
            </p>
            <div class="dataSprite">
				<div class="dataTable" id="dataTable">
					<table class="staticTable" cellpadding="0" cellspacing="0" border="0">
						<thead>
							<tr>
								<th class="yui3-datatable-col-imageurl">图片</th><th class="yui3-datatable-col-dishTypeList">菜品名称</th><th class="yui3-datatable-col-DishName">规格</th><th class="yui3-datatable-col-dishPriceList">价格</th><th class="yui3-datatable-col-btn">操作</th>
							</tr>
						</thead>
						<tbody>
						</tbody>
					</table>
				</div>
            </div>
        </div>
        <div class="pageController">
            <div class="dataTotal dao fLeft" id="dataTotal">
				<span class="num">共<em>0</em><i class="unit3">道</i></span>
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
