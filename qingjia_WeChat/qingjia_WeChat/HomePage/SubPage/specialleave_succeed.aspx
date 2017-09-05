<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="specialleave_succeed.aspx.cs" Inherits="qingjia_YiBan.SubPage.specialleave_succeed" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0" />
    <title>请假系统-请假成功-IMAW</title>
    <link rel="stylesheet" href="css/weui.css" />
    <link rel="stylesheet" href="css/example.css" />
    <link rel="stylesheet" href="css/index.css" />
    <script src="js/example.js"></script>
    <script src="js/router.min.js"></script>
    <script src="js/zepto.min.js"></script>
</head>
<body>
    <div id="top" class="index_top" style="background:white;">
        <img src="./images/logo.png" style="height:90%; margin-left:2%; margin-top:1%;" />
    </div>  
    <div class="weui-msg" style="background-color:white">
        <br /><br />
        <div class="weui-msg__icon-area"><i class="weui-icon-success weui-icon_msg"></i></div>
        <div class="weui-msg__text-area">
            <h2 class="weui-msg__title">请假成功</h2>
            <p class="weui-msg__desc">您已请假成功，请尽快联系辅导员批假，注意查收邮件！</p>
        </div>
        <div class="weui-msg__opr-area">
            <p class="weui-btn-area">
                <a href="specialleave.aspx" class="weui-btn weui-btn_primary">继续请假</a>
                <a href="../qingjia_WeChat.aspx" class="weui-btn weui-btn_primary2">返回主页</a>
            </p>
        </div>
        <div class="page__ft">
            <br /><br /><br /><br /><br />
            <a href="javascript:home()"><img src="./images/icon_footer_link.png" /></a>
        </div>
    </div>
</body>
</html>
