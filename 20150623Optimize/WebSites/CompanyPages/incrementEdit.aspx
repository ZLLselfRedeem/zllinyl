<!DOCTYPE html>
<title>悠先收银宝平台-增值管理</title>
<body>
<div>
    <div class="contentContainer page-increment" id="page">
    <div class="header header60">
        <div class="headerSprite">
            <div class="text">
		        <h2 class="headerTitle increment">增值管理</h2>
             </div>
        </div>
	</div>
    <div class="section">
        <div class="increment-editor-container" id="incrementEditContainer">
            <div class="increment-editor-content">
                <p class="ad-pic" id="adPic"><img src="images/increment-ad-pic.jpg" width="330" height="563" /></p>
				<div class="increment">
					<div class="hd">
						<p class="tips" id="tipsTotal">共计: 0 个菜品</p>
						<p class="btn-sprite"><a class="btn save-sort" id="saveSort" href="javascript:;" style="display:none;">保存为默认排序</a><a class="btn add" id="addIncrementDish" href="incrementManage.aspx?type=incrementEdit&m=add">新增菜品</a></p>
					</div>
					<div class="dataContainer">
						<div id="cbAll">
							<ul class="checkAll">
								<li class="inputSprite"><input class="inputCheck" type="checkbox" value="" /> 全选</li>
								<li class="btnSprite"><a class="btn" href="#">批量删除</a><a class="btn" href="#">全部删除</a></li>
							</ul>
						</div>
						<div class="dataSprite">
							<div class="dataTable" id="dataTable">
								<table class="staticTable" cellpadding="0" cellspacing="0" border="0">
									<thead>
										<tr>
											<th class="yui3-datatable-col-DishName">名称</th><th class="yui3-datatable-col-DishPrice">现价</th><th class="yui3-datatable-col-btn">操作</th>
										</tr>
									</thead>
									<tbody>
									</tbody>
								</table>
							</div>
						</div>
					</div>
					<!--
					<div class="pageController">
						<div class="pageContent fRight" id="">
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
					-->
				</div>
            </div>

            <div class="dish-submit-sprite btn-save-back-submmit">
                <div class="dishSubmit btnSprite" id="disSubmit">
					<!--
					<div class="txt-tips-sprite"><i class="txt-tips" id="save-txt-tips">保存成功</i></div>
					-->
					<a class="btn back" href="increment.aspx">返回</a>
					<a class="btn submit" href="javascript:;">保存并发布</a>
					<!--
					<a class="btn save" href="javascript:;">保存返回</a>
					-->
					<!-- 仅“修改”时存在上一道、下一道 -->
				</div>
            </div>
        </div>

    </div>

	</div>
</div>
</body>
</html>