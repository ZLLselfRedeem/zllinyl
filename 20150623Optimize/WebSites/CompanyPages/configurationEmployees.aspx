<%@ Page Language="C#" AutoEventWireup="true" CodeFile="configurationEmployees.aspx.cs" Inherits="CompanyPages_configurationEmployees" %>

<!DOCTYPE html>
<title>悠先收银宝平台-店员管理</title>
<style>
/* 设置滚动条的样式 */
::-webkit-scrollbar {
    width: 12px;
}
/* 滚动槽 */
::-webkit-scrollbar-track {
    -webkit-box-shadow: inset 0 0 6px rgba(0,0,0,0.3);
    border-radius: 10px;
}
/* 滚动条滑块 */
::-webkit-scrollbar-thumb {
    border-radius: 10px;
    background: rgba(0,0,0,0.1);
    -webkit-box-shadow: inset 0 0 6px rgba(0,0,0,0.5);
}
::-webkit-scrollbar-thumb:window-inactive {
    background: rgba(255,0,0,0.4);
}
    </style>
<body>
    <div>
        <div class="contentContainer" id="page">
            <div class="header">
                <div class="headerSprite">
                    <div class="text configuration dishMix configurationEmployees">
                        <h2 class="headerTitle tab" id="tab"></h2>

                        <div class="search" id="addPhone">
                            <p>
                                <input type="text" class="inputText" value="输入手机号码" />
                                <button type="button" class="btnSearch">添加</button>
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
            <div class="section configurationEmployees">
                <div class="dataContainer">

                    <div class="dataSprite">
                        <form id="dataFm">
                            <div class="dataTable" id="dataTable"></div>
                        </form>
                    </div>

                </div>
                <div class="pageController">
                    <div class="dataTotal member fLeft" id="dataTotal">
                        <span class="num">共<em>0</em><i class="unit5">位</i></span>
                    </div>
                    <div class="pageContent fRight" id="pageContent">
                        <div class="yui3-paginator-multi">
                            <div class="page">
                                <a href="#"><code>&lt;</code></a>
                                <a href="#"><code>&gt;</code></a>
                            </div>
                        </div>
                        <ul class="pageTotal">
                            <li class="txt">共<em>0</em>页
                            </li>
                            <li class="txt">&nbsp;&nbsp;&nbsp;&nbsp;<input type="text" class="inputText" />
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