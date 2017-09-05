<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bind.aspx.cs" Inherits="qingjia_YiBan.SubPage.bind" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0" />
    <link rel="stylesheet" href="css/weui.css" />
    <link rel="stylesheet" href="css/example.css" />
    <link rel="stylesheet" href="css/index.css" />
    <script src="js/example.js"></script>
    <script src="js/router.min.js"></script>
    <script src="js/zepto.min.js"></script>
    <title>请假系统-绑定账号-IMAW</title>
</head>
<body style="margin:0px; background-color:white;">
    <form runat="server">
    <div id="top" class="index_top" style="background:white; margin:0px;">
        <img src="./images/logo.png" style="height:90%; margin-left:2%; margin-top:1%;" />
    </div>
    <div class="modify_detail">
        <div class="weui-cells weui-cells_form" style="margin-top:0px; border-bottom:none; margin:0px; height:100%;">
            <br /><br /><br />
            <article class="weui-article">
                <h1 style="text-align:center;">管理学院</h1>
                <h1 style="text-align:center;">学生请假系统-易班端</h1>                    
            </article>
            <input class="weui-input" type="text" style="color:red; margin-left:18px; font-size:15px;" runat="server" disabled="disabled" id="txtError" value="" />
            <div class="weui-cell">
                <div class="weui-cell__hd"><label class="weui-label">账号</label></div>
                <div class="weui-cell__bd">
                    <input class="weui-input" type="text" runat="server" id="User_Num" placeholder="请输入您的账号" />
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd"><label class="weui-label">密码</label></div>
                <div class="weui-cell__bd">
                    <input class="weui-input" type="password" runat="server" id="User_Pw" placeholder="请输入您的密码" />
                </div>
            </div>

            <div class="page__bd page__bd_spacing">
                <br /><br />
                <input type="submit" class="weui-btn weui-btn_primary" value="绑定账号" id="btnSubmit" runat="server" onserverclick="btnSubmit_ServerClick" />
                <br />
            </div>

            <div class="page__ft" style="margin-bottom:0px; border-bottom:none;">
                <br /><br />
                <a href="javascript:home()"><img src="./images/icon_footer_link.png" /></a>
                <br /><br />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
