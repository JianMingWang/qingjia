<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="info_detail.aspx.cs" Inherits="qingjia_YiBan.SubPage.info_detail" %>

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
<body runat="server">
    <form runat="server">
    <div id="top" class="index_top" style="background:white;">
        <a href="../qingjia_WeChat.aspx"><img src="images/icon_back_.png" style="height:40%; margin-left:2%; margin-top:4%;" /></a>
        <a href="../qingjia_WeChat.aspx"><img src="images/icon_home.png" style="height:40%; margin-left:2%; margin-top:4%;"/></a>        
    </div>
    <div class="info_detail" >
            <div class="weui-cells weui-cells_form">
                <div class="weui-cells__title" style="text-align:left; font-size:20px; font-family:宋体; color:rgb(128, 128, 128);">个人信息</div>
                <input class="weui-input" type="text" style="color:red; margin-left:18px; font-size:15px;" runat="server" disabled="disabled" id="txtError" value="" />
                <div class="weui-cell" style="color:rgb(169,169,169);">
                    <div class="weui-cell__hd"><label class="weui-label3">学号</label></div>
                    <div class="weui-cell__bd">
                        <label class="weui-label1" id="Label_Num" runat="server" style="color:rgb(169,169,169);"></label>
                    </div>
                </div>
                <div class="weui-cell" style="color:rgb(169,169,169);">
                    <div class="weui-cell__hd"><label class="weui-label3">姓名</label></div>
                    <div class="weui-cell__bd">
                        <label class="weui-label1" id="Label_Name" runat="server" style="color:rgb(169,169,169);"></label>
                    </div>
                </div>
                <div class="weui-cell" style="color:rgb(169,169,169);">
                    <div class="weui-cell__hd"><label class="weui-label3">性别</label></div>
                    <div class="weui-cell__bd">
                        <label class="weui-label1" id="Label_Sex" runat="server" style="color:rgb(169,169,169);"></label>
                    </div>
                </div>
                <div class="weui-cell" style="color:rgb(169,169,169);">
                    <div class="weui-cell__hd"><label class="weui-label3">班级</label></div>
                    <div class="weui-cell__bd">
                        <label class="weui-label1" id="Label_Class" runat="server" style="color:rgb(169,169,169);"></label>
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd"><label class="weui-label3">手机号</label></div>
                    <div class="weui-cell__bd" style="color:rgb(169,169,169);">
                        <input class="weui-input" type="text" runat="server" id="txtTel" />
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd"><label class="weui-label3">QQ号</label></div>
                    <div class="weui-cell__bd" style="color:rgb(169,169,169);">
                        <input class="weui-input" type="text" runat="server" id="txtQQ" />
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd"><label class="weui-label3">与家长关系及姓名</label></div>
                    <div class="weui-cell__bd" style="color:rgb(169,169,169);">
                        <input class="weui-input" type="text" placeholder="例：父亲-某某某" runat="server" id="txtGuardianName" />
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd"><label class="weui-label3">家长联系方式</label></div>
                    <div class="weui-cell__bd" style="color:rgb(169,169,169);">
                        <input class="weui-input" type="text" runat="server" id="txtGuardianNum" />
                    </div>
                </div>               
                <div class="page__bd page__bd_spacing">
                    <br /><br />
                    <input type="submit" class="weui-btn weui-btn_primary" value="提交" id="btnSubmit" runat="server" onserverclick="btnSubmit_ServerClick"  />
                    <br />
                </div>
                <div class="page__ft">
                    <a href="javascript:home()"><img src="./images/icon_footer_link.png" /></a>
                </div>
                </div>       
    </div>
    </form>
</body>
</html>
