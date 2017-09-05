<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="form_notaudited.aspx.cs" Inherits="qingjia_YiBan.SubPage.form_notaudited" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0">
    <title>请假系统-特殊请假-IMAW</title>
    <link rel="stylesheet" href="css/weui.css" />
    <link rel="stylesheet" href="css/example.css" />
    <link rel="stylesheet" href="css/index.css" />
    <script src="js/example.js"></script>
    <script src="js/router.min.js"></script>
    <script src="js/zepto.min.js"></script>
</head>
<body>
    <form runat="server">
    <div id="top" class="index_top" style="background:white;">
        <a href="../qingjia_WeChat.aspx"><img src="images/icon_back_.png" style="height:40%; margin-left:2%; margin-top:4%;" /></a>
        <a href="../qingjia_WeChat.aspx"><img src="images/icon_home.png" style="height:40%; margin-left:2%; margin-top:4%;"/></a>        
    </div>
    <div class="weui-cells weui-cells_form">
        <div class="change">
            <a href="form_notaudited.aspx"><img src="images/form_notaudited1.png" style="width:25%; margin-left:25%; margin-top:5%;" /></a>
            <a href="form_taudited.aspx"><img src="images/form_taudited2.png" style="width:25%; margin-left:50%;" /></a>
        </div>
        <div runat="server" class="schoolleavedetail_main">
            <div runat="server" id="leave_list_div">
                <input class="weui-input" type="text" style="color:red; margin-left:18px; font-size:15px;" runat="server" disabled="disabled" id="txtError" value="" />
            </div>
            <div class="page__ft">
                <a href="javascript:home()"><img src="./images/icon_footer_link.png" /></a>
            </div>
        </div>
    </div>
        </form>    
</body>
</html>
