<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="qingjia_WeChat.aspx.cs" Inherits="qingjia_YiBan.HomePage.qingjia_WeChat" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0" />
    <title>请假系统-首页-IMAW</title>
	<link rel="stylesheet" href="./css/qingjia.index.css" />
    <link rel="stylesheet" href="./css/weui.css"/>
    <link rel="stylesheet" href="./css/example.css"/>
	<script src="./js/zepto.min.js"></script>
    <script src="./js/router.min.js"></script>
    <script src="./js/example.js"></script>
    <script type="text/javascript">
        function showdiv(targetid) {
            close();
            var target = document.getElementById(targetid);
            if (target.style.display == "none") {
                target.style.display = "block";
            }
        }
        function close() {
            var notice = document.getElementById("qingjia_index_notice");
            var leave = document.getElementById("qingjia_index_leave");
            var info = document.getElementById("qingjia_index_info");
            notice.style.display = "none";
            leave.style.display = "none";
            info.style.display = "none";
        }
    </script>
</head>
<body runat="server">
    <div id="top" class="index_top" style="background:white;">
        <img src="./images/logo.png" style="height:90%; margin-left:2%; margin-top:1%;" />
    </div>  
	<div class="container" id="container">
        <div id="qingjia_index_leave" class="qingjia_index_content_leave"">
         <a href="./SubPage/schoolleave.aspx" class="weui-btn weui-btn_primary1" id="schoolleave_button">离校请假</a>
         <a href="./SubPage/specialleave.aspx" class="weui-btn weui-btn_primary2" id="specialleave_button">特殊请假</a>
         <a href="./SubPage/classleave.aspx" class="weui-btn weui-btn_primary3" id="classleave_button">上课请假备案</a>
        </div>
        <div id="qingjia_index_notice" class="qingjia_index_content_notice" >
         <div class="weui-cells__title" style="text-align:center; font-size:28px; font-family:宋体; color:rgb(128, 128, 128);">晚点名安排</div>
         <div class="weui-cells weui-cells_form">
             <div class="weui-cell">
                 <div class="weui-cell__hd"><label class="weui-label1">辅导员</label></div>
                 <div class="weui-cell__bd">
                     <label class="weui-label1" id="label_teacherName" runat="server" style="color:rgb(169,169,169);"></label>
                 </div>
             </div>
             <div class="weui-cell">
                 <div class="weui-cell__hd"><label class="weui-label1">年级</label></div>
                 <div class="weui-cell__bd">
                     <label class="weui-label1" id="label_Year" runat="server" style="color:rgb(169,169,169);"></label>
                 </div>
             </div>

             <div class="weui-cell">
                 <div class="weui-cell__hd"><label class="weui-label1">请假截止时间</label></div>
                 <div class="weui-cell__bd">
                     <label class="weui-label1" id="label_EndTime" runat="server" style="color:rgb(169,169,169);"></label>
                 </div>
             </div>

             <div class="weui-cell">
                 <div class="weui-cell__hd"><label class="weui-label1">点名时间</label></div>
                 <div class="weui-cell__bd">
                     <label class="weui-label1" id="label_CallTime" runat="server" style="color:rgb(169,169,169);"></label>
                 </div>
             </div>
             <br />
         </div>

        <div id="Default_Vacation" runat="server" visible="false">
            <div class="weui-cells__title" style="text-align:center; font-size:28px; font-family:宋体; color:rgb(128, 128, 128);">节假日安排</div>
            <div class="weui-cells weui-cells_form">
            <div class="weui-cell">
                <div class="weui-cell__hd"><label class="weui-label1">请假截止时间</label></div>
                <div class="weui-cell__bd">
                    <input class="weui-input" disabled="disabled" id="vacation_end_time" runat="server" type="text" style="color:rgb(169,169,169);" />
                </div>
            </div>    
            </div>
        </div>

        <div id="Default_Version">
            <div class="weui-cells__title" style="text-align:right; font-size:15px; font-family:宋体; color:rgb(169,169,169);">V.170905</div>
        </div>

        </div>

        <div id="qingjia_index_info" class="qingjia_index_content_info">
         <a href="./SubPage/info_detail.aspx" class="weui-btn weui-btn_primary1" id="personInfo_button">基本信息</a>
         <a href="./SubPage/modify_detail.aspx" class="weui-btn weui-btn_primary2" id="PW_button">密码修改</a>
         <a href="./SubPage/form_notaudited.aspx" class="weui-btn weui-btn_primary3" id="leaveList_button">请假记录</a>
         <br />
     </div>

    </div>
	<script type="text/html" id="tpl_tabbar">
	<div class="weui_tabbar">
        <a href="javascript:void(0);" onclick="showdiv('qingjia_index_leave')" class="weui_tabbar_item">
            <div class="weui_tabbar_icon">
                <img src="./images/icon_nav_article.png" alt="" />
            </div>
            <p class="weui_tabbar_label">请假</p>
        </a>
        <a href="javascript:void(0);" onclick="showdiv('qingjia_index_notice')" class="weui_tabbar_item weui_bar_item_on">
            <div class="weui_tabbar_icon">
                <img src="./images/icon_nav_button.png" alt="" />
            </div>
            <p class="weui_tabbar_label">通知</p>
        </a>
        <a href="javascript:void(0);" onclick="showdiv('qingjia_index_info')" class="weui_tabbar_item">
            <div class="weui_tabbar_icon">
                <img src="./images/icon_nav_cell.png" alt="" />
            </div>
            <p class="weui_tabbar_label">我</p>
        </a>
	</div>
	</script>
</body>
</html>
