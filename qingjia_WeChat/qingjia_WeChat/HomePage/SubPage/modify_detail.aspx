<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="modify_detail.aspx.cs" Inherits="qingjia_YiBan.SubPage.modify_detail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0" />
    <title>请假系统-个人信息-IMAW</title>
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
    <div class="modify_detail" >        
            <div class="weui-cells weui-cells_form">
                <br /><br /><br />
                <div class="weui-cell">
                    <div class="weui-cell__hd"><label class="weui-label">旧密码</label></div>
                    <div class="weui-cell__bd">
                        <input class="weui-input" runat="server" type="password" id="oldPw"  placeholder="请输入旧密码" />
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd"><label class="weui-label">新密码</label></div>
                    <div class="weui-cell__bd">
                        <input class="weui-input" runat="server" type="password" id="newPw" placeholder="请输入新密码" />
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd"><label class="weui-label">确认密码</label></div>
                    <div class="weui-cell__bd">
                        <input class="weui-input" runat="server" type="password" id="newPwConfirm" placeholder="请确认新密码" />
                    </div>
                </div>
                <input class="weui-input" type="text" style="color:red; margin-left:18px; font-size:15px;" runat="server" disabled="disabled" id="txtError" value="" />
                             
                <div class="page__bd page__bd_spacing">
                    <br /><br />
                    <input type="submit" class="weui-btn weui-btn_primary" id="btnSubmit" runat="server" onserverclick="btnSubmit_ServerClick" value="修改"  />
                    <br />
                </div>
                <div class="page__ft">
                    <br /><br /><br /><br /><br /><br /><br /><br /><br />
                    <a href="javascript:home()"><img src="./images/icon_footer_link.png" /></a>
                </div>
                </div>       
    </div>
        </form>
</body>
</html>
