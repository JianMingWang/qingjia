<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="classleave.aspx.cs" Inherits="qingjia_YiBan.SubPage.classleave" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0" />
    <title>请假系统-上课请假备案-IMAW</title>
    <link rel="stylesheet" href="css/weui.css" />
	<link rel="stylesheet" href="css/example.css" />
    <link rel="stylesheet" href="css/index.css"/>
    <script src="js/example.js"></script>
    <script src="js/router.min.js"></script>
    <script src="js/zepto.min.js"></script>
    
</head>
<body>
 <div id="top" class="index_top" style="background:white;">
        <a href="../qingjia_WeChat.aspx"><img src="images/icon_back_.png" style="height:40%; margin-left:2%; margin-top:4%;" /></a>
        <a href="../qingjia_WeChat.aspx"><img src="images/icon_home.png" style="height:40%; margin-left:2%; margin-top:4%;"/></a>        
    </div>
    <div class="schoolleave_explain">
                <article class="weui-article">
                    <h1 >上课请假备案说明 </h1>               
                        <section>                           
                            <p>
                                欢迎使用请假系统！
                                </p>
                            <p>
                                上课请假备案作为您的上课请假的记录，填好请假信息并提交之后，您需要联系辅导员，说明情况，取得他的同意；
                                </p>
                            <p>
                                如果得到辅导员的审批，您会收到我们发给您的邮件，表示您请假成功。请假成功仅证明辅导员同意您的请假，您还是需要取得任课老师的同意。
                                </p>
                            <p>
                                您可以在请假记录界面看到您的未审批和未销假的请假记录，方便您的管理。
                            </p>                            
                        </section>                    
                </article>
        <div class="page__bd page__bd_spacing">
            <br /><br />
            <a href="classleave_detail.aspx" class="weui-btn weui-btn_primary">我知道了</a>
            <br />
        </div>
        <div class="page__ft">
            <a href="javascript:home()"><img src="./images/icon_footer_link.png" /></a>
        </div>
    </div>
</body>
</html>
