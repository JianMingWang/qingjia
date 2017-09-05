<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="schoolleave_detail_short.aspx.cs" Inherits="qingjia_YiBan.SubPage.WebForm4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0">
    <title>请假系统-离校请假-IMAW</title>
    <link rel="stylesheet" href="css/weui.css" />
    <link rel="stylesheet" href="css/example.css" />
    <link rel="stylesheet" href="css/index.css" />
    <script src="js/example.js"></script>
    <script src="js/router.min.js"></script>
    <script src="js/zepto.min.js"></script>


    <script src="http://code.jquery.com/jquery-1.9.1.min.js"></script>
    <script src="dev/js/mobiscroll.core-2.6.2.js" type="text/javascript"></script>
	<script src="dev/js/mobiscroll.core-2.6.2-hu.js" type="text/javascript"></script>
	<script src="dev/js/mobiscroll.core-2.6.2-de.js" type="text/javascript"></script>
	<script src="dev/js/mobiscroll.core-2.6.2-es.js" type="text/javascript"></script>
	<script src="dev/js/mobiscroll.core-2.6.2-fr.js" type="text/javascript"></script>
	<script src="dev/js/mobiscroll.core-2.6.2-it.js" type="text/javascript"></script>
	<script src="dev/js/mobiscroll.core-2.6.2-no.js" type="text/javascript"></script>
	<script src="dev/js/mobiscroll.core-2.6.2-pt-BR.js" type="text/javascript"></script>
	<script src="dev/js/mobiscroll.core-2.6.2-zh.js" type="text/javascript"></script>
	<script src="dev/js/mobiscroll.core-2.6.2-nl.js" type="text/javascript"></script>
	<script src="dev/js/mobiscroll.core-2.6.2-tr.js" type="text/javascript"></script>
	<script src="dev/js/mobiscroll.core-2.6.2-ja.js" type="text/javascript"></script>
	<link href="dev/css/mobiscroll.core-2.6.2.css" rel="stylesheet" type="text/css" />
	<link href="dev/css/mobiscroll.animation-2.6.2.css" rel="stylesheet" type="text/css" />
	<script src="dev/js/mobiscroll.datetime-2.6.2.js" type="text/javascript"></script>
	<script src="dev/js/mobiscroll.list-2.6.2.js" type="text/javascript"></script>
	<script src="dev/js/mobiscroll.list-2.6.2.js" type="text/javascript"></script>
	<script src="dev/js/mobiscroll.select-2.6.2.js" type="text/javascript"></script>
	<script src="dev/js/mobiscroll.android-ics-2.6.2.js" type="text/javascript"></script>
	<link href="dev/css/mobiscroll.android-ics-2.6.2.css" rel="stylesheet" type="text/css" />
    <script src="dev/js/mobiscroll.wp-2.6.2.js" type="text/javascript"></script>
	<link href="dev/css/mobiscroll.wp-2.6.2.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            var curr = new Date().getFullYear();
            var opt = {

            }

            opt.date = {preset : 'date'};
	opt.datetime = { preset : 'datetime', minDate: new Date(2014,1,1,0,00), maxDate: new Date(2020,12,31,23,59), stepMinute: 5  };
	opt.time = {preset : 'time'};
	opt.tree_list = {preset : 'list', labels: ['Region', 'Country', 'City']};
	opt.image_text = {preset : 'list', labels: ['Cars']};
	opt.select = {preset : 'select'};

            $('select.changes').bind('change', function() {
                var demo = $('#demo').val();
                $(".demos").hide();
                if (!($("#demo_"+demo).length))
                    demo = 'default1';

                $("#demo_" + demo).show();
                $('#test_'+demo).val('').scroller('destroy').scroller($.extend(opt[$('#demo').val()], { theme: $('#theme').val(), mode: $('#mode').val(), display: $('#display').val(), lang: $('#language').val() }));
            });

            $('#demo').trigger('change');
        });
    </script>
    <script type="text/javascript">
        $(function () {
            var curr = new Date().getFullYear();
            var opt = {

            }

            opt.date = {preset : 'date'};
            opt.datetime = { preset : 'datetime', minDate: new Date(2014,1,1,0,00), maxDate: new Date(2020,12,31,23,59), stepMinute: 5  };
            opt.time = {preset : 'time'};
            opt.tree_list = {preset : 'list', labels: ['Region', 'Country', 'City']};
            opt.image_text = {preset : 'list', labels: ['Cars']};
            opt.select = {preset : 'select'};

                    $('select.changes').bind('change', function() {
                        var demo = $('#demo').val();
                        $(".demos").hide();
                        if (!($("#demo_"+demo).length))
                            demo = 'default2';

                        $("#demo_" + demo).show();
                        $('#test_'+demo).val('').scroller('destroy').scroller($.extend(opt[$('#demo').val()], { theme: $('#theme').val(), mode: $('#mode').val(), display: $('#display').val(), lang: $('#language').val() }));
                    });

            $('#demo').trigger('change');
        });
    </script>
</head>
<body>
    <form runat="server">
    <div id="top" class="index_top" style="background:white;">
        <a href="schoolleave.aspx"><img src="images/icon_back_.png" style="height:40%; margin-left:2%; margin-top:4%;" /></a>
        <a href="../qingjia_WeChat.aspx"><img src="images/icon_home.png" style="height:40%; margin-left:2%; margin-top:4%;"/></a>        
    </div>
    <div class="weui-cells weui-cells_form">
        <div class="change">
                <a href="schoolleave_detail_short.aspx"><img src="images/schoolleave_short1.png" style="width:25%; margin-left:12.5%; margin-top:5%;" /></a>
                <a href="schoolleave_detail_long.aspx"><img src="images/schoolleave_long2.png" style="width:25%; margin-left:37.5%;" /></a>
                <a href="schoolleave_detail_holiday.aspx"><img src="images/schoolleave_holiday2 .png" style="width:25%; margin-left:62.5%;" /></a>
         </div>
         <div class="schoolleavedetail_main" style="display:block;" runat="server" id="index">
             <input class="weui-input" type="text" style="color:red; margin-left:18px; font-size:15px;" runat="server" disabled="disabled" id="txtError" value="" />
                <div class="weui-cell" style="color:rgb(169,169,169);">
                    <div class="weui-cell__hd"><label class="weui-label2">学号</label></div>
                    <div class="weui-cell__bd">
                        <label class="weui-label1" id="Label_Num" runat="server" style="color:rgb(169,169,169);"></label>
                    </div>
                </div>
                <div class="weui-cell" style="color:rgb(169,169,169);">
                    <div class="weui-cell__hd"><label class="weui-label2">姓名</label></div>
                    <div class="weui-cell__bd">
                        <label class="weui-label1" id="Label_Name" runat="server" style="color:rgb(169,169,169);"></label>
                    </div>
                </div>
                <div class="weui-cell" style="color:rgb(169,169,169);">
                    <div class="weui-cell__hd"><label class="weui-label2">班级</label></div>
                    <div class="weui-cell__bd">
                        <label class="weui-label1" id="Label_Class" runat="server" style="color:rgb(169,169,169);"></label>
                    </div>
                </div>
                <div class="weui-cell" style="color:rgb(169,169,169);">
                    <div class="weui-cell__hd"><label class="weui-label2">本人联系方式</label></div>
                    <div class="weui-cell__bd">
                        <label class="weui-label1" id="Label_Tel" runat="server" style="color:rgb(169,169,169);"></label>
                    </div>
                </div>
                <div class="weui-cell" style="color:rgb(169,169,169);">
                    <div class="weui-cell__hd"><label class="weui-label2">父母联系方式</label></div>
                    <div class="weui-cell__bd">
                        <label class="weui-label1" id="Label_ParentTel" runat="server" style="color:rgb(169,169,169);"></label>
                    </div>
                </div>
                <div class="weui-cell" style="color:rgb(169,169,169);">
                    <div class="weui-cell__hd"><label class="weui-label2">请假类型</label></div>
                    <div class="weui-cell__bd">
                        <label class="weui-label1" id="Label_LeaveType" runat="server" style="color:rgb(169,169,169);">短期请假</label>
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd"><label for="" class="weui-label2">离校时间</label></div>
                    <div class="weui-cell__bd" style="color:rgb(169,169,169);">
                        <input class="weui-input" type="text" name="test_default" id="test_default1" runat="server" placeholder="请选择离校时间！" />
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd"><label for="" class="weui-label2">返校时间</label></div>
                    <div class="weui-cell__bd" style="color:rgb(169,169,169);">
                        <input class="weui-input" type="text" name="test_default" id="test_default2" runat="server" placeholder="请选择返校时间！" />
                    </div>
                </div>
             <br />
                <div class="weui-cells__title" style="color:black; font-size:17px;">请假原因</div>
                <div class="weui-cells weui-cells_form">
                    <div class="weui-cell">
                        <div class="weui-cell__bd">
                            <textarea class="weui-textarea" runat="server" id="Leave_Reason" placeholder="请输入文本" rows="3"></textarea>
                            <div class="weui-textarea-counter"></div>
                        </div>
                    </div>
                </div>
                <div class="page__bd page__bd_spacing">
                    <br /><br />
                    <input type="submit" class="weui-btn weui-btn_primary" value="立即提交" id="btnSubmit" runat="server" onserverclick="btnSubmit_ServerClick" />
                    <br />
                </div>
                <div class="page__ft">
                    <a href="javascript:home()"><img src="./images/icon_footer_link.png" /></a>
                </div>
           </div>
    </div>





        <div class="content" style="display:none;">
        <div>
            <label for="theme">Theme</label>
            <select name="theme" id="theme" class="changes">
                <option value="wp light">Windows Phone Light</option>
			    <option value="android-ics">Android ICS</option>
                <option value="android">Android</option>
	            <option value="android-ics light">Android ICS Light</option>
	            <option value="ios">iOS</option>
	            <option value="jqm">Jquery Mobile</option>
	            <option value="sense-ui">Sense UI</option>
	            <option value="wp">Windows Phone</option>
	            <!--Themes-->
            </select>
        </div>
        <div>
            <label for="mode">Mode</label>
            <select name="mode" id="mode" class="changes">
                <option value="scroller">Scroller</option>
                <option value="clickpick">Clickpick</option>
                <option value="mixed">Mixed</option>
            </select>
        </div>
        <div>
            <label for="display">Display</label>
            <select name="display" id="display" class="changes">
			<option value="bottom">Bottom</option>
                <option value="modal">Modal</option>
                <option value="inline">Inline</option>
                <option value="bubble">Bubble</option>
                <option value="top">Top</option>
                
            </select>
        </div>
        <div>
            <label for="language">Language</label>
            <select name="language" id="language" class="changes">
                <option value="">English</option>
			    <option value="zh">Chinese</option>
                <option value="hu">Magyar</option>
	            <option value="de">Deutsch</option>
	            <option value="es">Espa駉l</option>
	            <option value="fr">Fran鏰is</option>
	            <option value="it">Italiano</option>
	            <option value="no">Norsk</option>
	            <option value="pt-BR">Pr Brasileiro</option>
	
	            <option value="nl">Nederlands</option>
	            <option value="tr">T黵k鏴</option>
	            <option value="ja">Japanese</option>
	            <!--Lang-->
            </select>
        </div>
        <div>
            <label for="demo">Demo</label>
            <select name="demo" id="demo" class="changes">
			<option value="datetime" selected="selected">Datetime</option>
                <option value="date" >Date</option>
	
	<option value="time" >Time</option>
	<option value="tree_list" >Tree List</option>
	<option value="image_text" >Image & Text</option>
	<option value="select" >Select</option>
	<!--Demos-->
            </select>
        </div>   
</div>
        </form>
</body>
</html>


