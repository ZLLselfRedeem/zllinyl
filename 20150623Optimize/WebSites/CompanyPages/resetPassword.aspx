<%@ Page Language="C#" AutoEventWireup="true" CodeFile="resetPassword.aspx.cs" Inherits="CompanyPages_resetPassword" %>

<!DOCTYPE html>
<title>悠先收银宝平台-密码管理</title>
<body>
    <div>
        <div class="contentContainer" id="page">
            <div class="section">
                <div class="resetPassword" id="resetPassword">
                    <h2 class="headerTxt">
                        修改密码
                    </h2>
                    <p>
                        <span class="title">当前密码：</span>
                        <input class="inputText" type="password" name="current" id="current" value="" maxlength="50" />
                        <label class="tips" for="current">
                        </label>
                    </p>
                    <p>
                        <span class="title">新密码：</span>
                        <input class="inputText" type="password" name="now" id="now" value="" maxlength="50" />
                        <label class="tips" for="now">
                        </label>
                    </p>
                    <p>
                        <span class="title">确认新密码：</span>
                        <input class="inputText" type="password" name="confirm" id="confirm" value="" maxlength="50" />
                        <label class="tips" for="confirm">
                        </label>
                    </p>
                    <p class="btnSprite">
                        <input type="submit" class="btn" id="btnConfirm" value="确认修改" />
                    </p>
                </div>
            </div>
        </div>
    </div>
</body>
</html>