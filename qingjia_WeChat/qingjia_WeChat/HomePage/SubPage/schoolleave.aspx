<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="schoolleave.aspx.cs" Inherits="qingjia_YiBan.SubPage.schoolleave" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0" />
    <title>请假系统-离校请假-IMAW</title>
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
                    <h1 >离校请假说明 </h1>               
                        <section>                           
                            <p>
                                欢迎使用请假系统！
                                离校请假分为短期请假，长期请假和节假日请假。
                            </p>
                            <p>
                                短期请假为小于三天的离校请假。填好请假信息并提交之后，您需要联系辅导员，说明情况，取得同意；
                                </p>
                            <p>
                                长期请假为三天以上的离校请假。填好请假信息并提交之后，您需要联系辅导员，说明情况，取得同意；
                                </p>
                            <p>
                                节假日请假则为国庆节、劳动节等法定节假日的离校请假。填好请假信息并提交之后，如果您的请假时间不超出规定时间，则不需要联系辅导员；如果超出规定时间，您需要联系辅导员，说明情况，取得同意；
                                </p>
                            <p>
                                如果得到辅导员的审批，您会收到我们发给您的邮件，表示您请假成功。
                                </p>
                            <p>
                                当您返校之后，需要尽快联系辅导员，进行销假。
                                </p>
                            <p>
                                如果销假成功，您会收到我们发给您的邮件，表示您销假成功。
                                </p>
                            <p>
                                您可以在请假记录界面看到您的未审批和未销假的请假记录，方便您的管理。

                            </p>                            
                        </section>                    
                </article>
        <div class="page__bd page__bd_spacing">
            <br /><br />
            <a href="schoolleave_detail_short.aspx" class="weui-btn weui-btn_primary">我知道了</a>
            <br />
        </div>
        <div class="page__ft">
            <a href="javascript:home()"><img src="./images/icon_footer_link.png" /></a>
        </div>
    </div>
</body>
</html>
