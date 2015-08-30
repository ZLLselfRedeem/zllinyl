/*
Copyright (c) 2003-2012, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    // config.uiColor = '#AADC6E';
    config.skin = 'kama'; //kama,v2,office2003,皮肤设置  
    config.language = 'zh-cn'; //设置中文环境
    config.font_names = '宋体;楷体_GB2312;新宋体;黑体;隶书;幼圆;微软雅黑;Arial;Comic Sans MS;Courier New;Tahoma;Times New Roman;Verdana'; //编辑字体设置  

    // 基础工具栏
    // config.toolbar = "Basic";    
    // 全能工具栏
    config.toolbar = "Full";
    // 自定义工具栏
    //    config.toolbar =
    //    [
    //        ['Source', '-', 'Preview'], ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord'],
    //        ['Undo', 'Redo', '-', 'Find', 'Replace', '-', 'SelectAll', 'RemoveFormat'],
    //        ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', 'Blockquote', 'ShowBlocks'], '/',
    //        ['Bold', 'Italic', 'Underline', 'Strike', '-', 'Subscript', 'Superscript'],
    //        ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'], ['Link', 'Unlink', 'Anchor'],
    //        ['Image', 'Flash', 'Table', 'HorizontalRule', 'SpecialChar'], '/',
    //        ['Styles', 'Format', 'Font'], ['Maximize', '-', 'About']
    //    ];
    //    , 'FontSize'   ['TextColor', 'BGColor'],
    //设置引用路径
    //    config.filebrowserBrowseUrl = 'ckfinder/ckfinder.html'; //上传文件时浏览服务文件夹
    //    config.filebrowserImageBrowseUrl = 'ckfinder/ckfinder.html?Type=Images'; //上传图片时浏览服务文件夹
    //    config.filebrowserFlashBrowseUrl = 'ckfinder/ckfinder.html?Type=Flash'; //上传Flash时浏览服务文件夹
    //    config.filebrowserUploadUrl = 'ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files'; //上传文件按钮(标签)
    //    config.filebrowserImageUploadUrl = 'ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images'; //上传图片按钮(标签)
    //    config.filebrowserFlashUploadUrl = 'ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash'; //上传Flash按钮(标签)

    config.filebrowserBrowseUrl = 'ckfinder/ckfinder.html'; //上传文件时浏览服务文件夹
    config.filebrowserImageBrowseUrl = 'ckfinder/ckfinder.htm?Type=Images'; //上传图片时浏览服务文件夹
    config.filebrowserFlashBrowseUrl = 'ckfinder/ckfinder.htm?Type=Flash'; //上传Flash时浏览服务文件夹
    config.filebrowserUploadUrl = 'ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files'; //上传文件按钮(标签)
    config.filebrowserImageUploadUrl = 'ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images'; //上传图片按钮(标签)
    config.filebrowserFlashUploadUrl = 'ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash'; //上传Flash按钮(标签)

    //    config.filebrowserWindowWidth = '800';  //“浏览服务器”弹出框的size设置
    //    config.filebrowserWindowHeight = '500';



    //工具欄是否可以被收縮
    config.toolbarCanCollapse = true;

    //工具欄默認是否展開
    config.toolbarStartupExpanded = true;
    //换行使用<br/>标记
    config.enterMode = CKEDITOR.ENTER_BR;

    //禁止富文本编辑器转义  备注：wangc 20140703
    config.entities = false;

    //是否强制复制来的内容去除格式
//    config.forcePasteAsPlainText = true;

    //使用HTML实体进行输出
//    config.entities = true;

//    config.entities_greek = true;

//    config.entities_additional = '#39';
    //config.protectedSource.push(/(]+>[\s|\S]*?<\/asp:[^\>]+>)|(]+\/>)/gi);
    //config.fullPage = true;

    //config.entities = true;
};

//CKFinder.SetupCKEditor(null, '../ckfinder/'); //注意ckfinder的路径对应实际放置的位置