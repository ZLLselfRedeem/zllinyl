<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dishManage.aspx.cs" Inherits="CompanyPages_dishManage" %>
<!DOCTYPE html>
<title>悠先收银宝平台-菜品管理</title>
<body>
<div>
    <div class="contentContainer" id="page">
    <div class="header header60">
        <div class="headerSprite">
            <div class="text">
		        <h2 class="headerTitle dishManage">菜品添加</h2>
             </div>
        </div>
	</div>
    <div class="section">
        <div class="addContainer" id="addContainer">
            <div class="disCates gray">
				 <table cellpadding="0" cellspacing="0" border="0">
                    <tbody>
                        <tr>
                            <td width="112"><span class="title required"><span class="tip">请选择<i class="txt">分类</i>!<i class="close">x</i></span>分　　类<i class="icon">*</i></span></td>
                            <td width="600">
                                <ul class="cates" id="cates">
                                </ul>
                            </td>
							<td width="218">
								<div class="btnSprite" id="catesBtn">
									<a class="btn add" href="javascript:;">添加分类</a>
									<a class="btn update" href="javascript:;">修改分类</a>
									<a class="btn edit" href="javascript:;">删除分类</a>
								</div>
							</td>
                        </tr>
                    </tbody>
                 </table>
            </div>
            <div class="disName">
                <p class="text" id="disNameSprite"><span class="title required"><label for="disNameTxt" class="tip">请输入<i class="txt">菜名</i>!<i class="close">x</i></label>菜　　名：<i class="icon">*</i></span><input type="text" class="inputText" id="disNameTxt" name="disNameTxt" value="" /></p>
                <p class="text"><span class="title">菜名全拼：</span><input type="text" class="inputText" id="disNameQP" maxlength="50" value="" /></p>
				<p class="text"><span class="title">菜名简拼：</span><input type="text" class="inputText" id="disNameJP" maxlength="50" value="" /></p>
                <p class="last text" id="disNameIndexSprite"><span class="title">排　　序：</span><input type="text" class="inputText" id="disNameIndex" value="1" /></p>
            </div>
            <div class="disComment gray">
                <p class="text"><span class="title">菜品简介：</span><textarea rows="20" cols="20" class="inputTextArea" id="disCommentProfile">还可以输入 153 个字</textarea></p>
                <p class="text last"><span class="title">菜品详情：</span><textarea rows="20" cols="20" class="inputTextArea" id="disCommentDetail">还可以输入 153 个字</textarea></p>
            </div>
			
            <div class="disUpload" id="disUpload">
				<div id="btnUpload" style="height:35px;width:125px;background:url(scripts/jquery.upload/images/btn_upload.png) left top no-repeat;"></div>
				<p class="btnSprite"><!-- <a class="btn" href="javascript:;">提交</a>--><a class="btn cancel" href="javascript:;">取消</a></p>
                <div class="pre-upload-loading"><img width="42" height="42" src="images/loading_2.gif" /></div>
				<ul class="imgs" id="uploadContent">
					<li class="source" id="source"><img alt="菜品图片" title="菜品图片" src="images/nonImg.png" width="320" height="240" /></li>
                    <li class="big" id="preview"><img alt="菜品图片" src="images/nonImg.png" width="240" /></li>
					<!--
                    <li class="small" id="previewSec"><img alt="菜品图片" src="images/nonImg.png" width="108" /></li>
					-->
                </ul>
				<span class="tips">仅支持jpg格式，大小在 <i id="imgCapacity"></i>MB 以内; 拖动鼠标以截取图片</span>
            </div>
            <div class="disConfig">
                <table class="dataContent" id="disConfigContent" cellpadding="0" cellspacing="0" border="0">
                    <thead>
                        <tr class="configHeader">
                            <td width="828px" ><span class="configTitle">规格</span></td>
                            <td width="83px"><div class="btnSprite"><a class="btn addPrice" href="javascript:;">继续添加</a></div></td>
                        </tr>
                    </thead>
                    <tbody id="disConfigPrice">
						<tr class="itemSprite">
							<td colspan="2" class="item">
								<table class="dataPrice" cellpadding="0" cellspacing="0" border="0">
									<tr>
										<td class="required"><span class="title">规　　格：</span><input class="inputText" type="text" value="" name="" /> </td>
										<td><span class="title required"><span class="tip">需要输入<i class="txt">规格[价格]</i>!<i class="close">x</i></span>分　　类<i class="icon">*</i>价　　格：</span><input class="inputText" type="text" value="" name="" /> </td>
										<td><span class="title">掌中宝编号：</span><input class="inputText" type="text" value="" name="" /> </td>
										<td><a class="btn deletePrice" rel="1" href="javascript:;">删除</a></td>
									</tr>
									<tr>
										<td><span class="title">VIP 折扣：</span><span class="discount"><input type="radio" value="" checked="checked" name="vip" />是</span><span class="discount"><input type="radio" value="" name="vip" />否</span></td>
										<td><span class="title">支持返送：</span><span class="discount"><input type="radio" value="" checked="checked" name="back" />是</span><span class="discount"><input type="radio" value="" name="back" /> 否</span></td>
										<td><span class="title">是否停售  ：</span><span class="discount"><input type="radio" value="" checked="checked" name="sale" />是</span><span class="discount"><input type="radio" value="" name="sale" /> 否</span></td>
										<td><a class="more up" href="javascript:;">更多设置（口味、配菜）</a></td>
									</tr>
								</table>
								<table class="dataAttach up" cellpadding="0" cellspacing="0" border="0">
									<tr class="bot">
										<td width="90"><span class="title">选择口味：</span></td>
										<td width="496">
											<ul class="cates catesKouWei">
											</ul>
										</td>
										<td width="218">
											<div class="btnSprite" id="dishTasteBtn">
												<a class="btn add" href="javascript:;">添加口味</a>
												<a class="btn update" href="javascript:;">修改口味</a>
												<a class="btn edit" href="javascript:;">删除口味</a>
											</div>
										</td>
									</tr>
									<tr>
										<td width="90"><span class="title">选择配菜：</span></td>
										<td colspan="2">
											<ul class="cates catesPeiCai">
											</ul>
										</td>
									</tr>
								</table>
							</td>
						</tr>
						
                    </tbody>
                 </table>
                <div class="dishSubmit btnSprite" id="disSubmit">
					<div class="txt-tips-sprite"><i class="txt-tips" id="save-txt-tips">保存成功</i></div>
					<a class="btn back" href="javascript:;">返回</a>
					<a class="btn submit" href="javascript:;">保存</a>
					<a class="btn save" href="javascript:;">保存返回</a>
					<!-- 仅“修改”时存在上一道、下一道 -->
				</div>
            </div>
        </div>

    </div>

	</div>
</div>
</body>
</html>