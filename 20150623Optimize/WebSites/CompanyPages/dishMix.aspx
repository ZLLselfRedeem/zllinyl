<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dishMix.aspx.cs" Inherits="CompanyPages_dishMix" %>
<!DOCTYPE html>
<title>悠先收银宝平台-配菜管理</title>
<script src="scripts/controller/dishIngredientsSellOff.js"></script>
<body>
<div>
    <div class="contentContainer" id="page">
    <div class="header">
        <div class="headerSprite">
            <div class="text dishList dishMix">
		        <h2 class="headerTitle tab" id="tab">
				</h2>
                <!--
			    <div class="cates">
                    <span class="txt">选择分类:</span>
					<span id="catesSelect">
						<select class="s" name="s">
							<option value="835">全部</option>
						</select>
					</span>
                </div>
				-->
                <div class="search" id="search" >
                    <p>
                    <input type="text" class="inputText" value="按配菜名搜索" />
                    <button type="button" class="btnSearch">查询</button>
                    </p>
                </div>
                <div class="pageShow" id="pageShow">
                    <em class="title">显示行数：</em>
                    <ul class="btn">
                        <li class="cur"><a href="#">10行</a></li>
                        <li><a href="#">20行</a></li>
                        <li><a href="#">50行</a></li>
                    </ul>
                 </div>
             </div>
        </div>
	</div>
    <div class="section dishMix">
		<div class="headTable" >
			<h3>配菜列表</h3>
			<a class="btn save" href="#">保存数据</a><a class="btn add" href="#">继续添加</a>
		</div>
        <div class="dataContainer">
			
            <div class="dataSprite">
				<div class="dataTable" id="dataTable">
					<table class="staticTable" cellpadding="0" cellspacing="0" border="0">
						<tbody>
						</tbody>
					</table>
				</div>
            </div>
			
        </div>
        <div class="pageController">
            <div class="dataTotal zhong fLeft" id="dataTotal">
				<span class="num">共<em>0</em><i class="unit4">种</i></span>
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