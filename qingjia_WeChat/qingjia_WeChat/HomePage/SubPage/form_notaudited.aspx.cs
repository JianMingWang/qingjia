using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using qingjia_YiBan.HomePage.Class;
using qingjia_YiBan.HomePage.Model.API;

namespace qingjia_YiBan.SubPage
{
    public partial class form_notaudited : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDB();
            }
        }

        private void LoadDB()
        {
            string access_token = Session["access_token"].ToString();
            Client<List<LeaveList>> client = new Client<List<LeaveList>>();
            ApiResult<List<LeaveList>> result = client.GetRequest("access_token=" + access_token, "/api/leavelist/getlist");
            if (result.result == "success")
            {
                if (result.data != null)
                {
                    List<LeaveList> list = result.data;
                    foreach (LeaveList item in list)
                    {
                        if (item.State == "待审核")
                        {
                            string LV_NUM = item.ID;
                            string LeaveType = item.Type;
                            string go_time = item.TimeBack.ToString("yyyy-MM-dd HH:MM:ss");
                            string back_time = item.SubmitTime;
                            leave_list_div.Controls.Add(CreatLeaveList(LV_NUM, LeaveType, go_time, back_time));
                            leave_list_div.Controls.Add(CreatBr());
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 生成请假记录
        /// </summary>
        /// <param name="LV_NUM"></param>
        /// <param name="Class"></param>
        /// <param name="go_Time"></param>
        /// <param name="back_Time"></param>
        /// <returns></returns>
        private HtmlGenericControl CreatLeaveList(string LV_NUM, string Class, string go_Time, string back_Time)
        {
            //请假记录
            HtmlGenericControl LeaveList = CreatDiv("", "weui-cells weui-cells_form", "");

            //内部构成  创建第一层
            HtmlGenericControl Div_01 = CreatDiv("", "weui-cell", "");
            HtmlGenericControl Div_02 = CreatDiv("", "weui-cell", "");
            HtmlGenericControl Div_03 = CreatDiv("", "weui-cell", "");
            HtmlGenericControl Div_04 = CreatDiv("", "weui-cell", "");
            HtmlGenericControl Div_05 = CreatDiv("", "page__bd page__bd_spacing", "");

            //内部构成  创建第二层
            HtmlGenericControl Div_01_01 = CreatDiv("", "weui-cell__hd", "");
            HtmlGenericControl Label_01 = CreatLabel("", "weui-label2", "请假单号");
            Div_01_01.Controls.Add(Label_01);

            HtmlGenericControl Div_02_01 = CreatDiv("", "weui-cell__hd", "");
            HtmlGenericControl Label_02 = CreatLabel("", "weui-label2", "请假类别");
            Div_02_01.Controls.Add(Label_02);

            HtmlGenericControl Div_03_01 = CreatDiv("", "weui-cell__hd", "");
            HtmlGenericControl Label_03 = CreatLabel("", "weui-label2", "返校时间");
            Div_03_01.Controls.Add(Label_03);

            HtmlGenericControl Div_04_01 = CreatDiv("", "weui-cell__hd", "");
            HtmlGenericControl Label_04 = CreatLabel("", "weui-label2", "提交时间");
            Div_04_01.Controls.Add(Label_04);

            HtmlGenericControl Div_01_02 = CreatDiv("", "weui-cell__hd", "");
            HtmlGenericControl Text_01 = CreatText("", "weui-input", "", LV_NUM);
            Div_01_02.Controls.Add(Text_01);

            HtmlGenericControl Div_02_02 = CreatDiv("", "weui-cell__hd", "");
            HtmlGenericControl Text_02 = CreatText("", "weui-input", "", Class);
            Div_02_02.Controls.Add(Text_02);

            HtmlGenericControl Div_03_02 = CreatDiv("", "weui-cell__hd", "");
            HtmlGenericControl Text_03 = CreatText("", "weui-input", "", go_Time);
            Div_03_02.Controls.Add(Text_03);

            HtmlGenericControl Div_04_02 = CreatDiv("", "weui-cell__hd", "");
            HtmlGenericControl Text_04 = CreatText("", "weui-input", "", back_Time);
            Div_04_02.Controls.Add(Text_04);

            //创建按钮
            HtmlGenericControl Div_05_01 = CreatSubmit("", "weui-btn weui-btn_primary", "", "form_succeed.aspx?LV_NUM=" + LV_NUM);

            //创建换行
            HtmlGenericControl br1 = CreatBr();
            HtmlGenericControl br2 = CreatBr();
            HtmlGenericControl br3 = CreatBr();

            //组装div
            //Div_05.Controls.Add(br1);
            Div_05.Controls.Add(br2);
            Div_05.Controls.Add(Div_05_01);
            Div_05.Controls.Add(br3);

            Div_04.Controls.Add(Div_04_01);
            Div_04.Controls.Add(Div_04_02);

            Div_03.Controls.Add(Div_03_01);
            Div_03.Controls.Add(Div_03_02);

            Div_02.Controls.Add(Div_02_01);
            Div_02.Controls.Add(Div_02_02);

            Div_01.Controls.Add(Div_01_01);
            Div_01.Controls.Add(Div_01_02);

            LeaveList.Controls.Add(Div_01);
            LeaveList.Controls.Add(Div_02);
            LeaveList.Controls.Add(Div_03);
            LeaveList.Controls.Add(Div_04);
            LeaveList.Controls.Add(Div_05);

            return LeaveList;
        }

        /// <summary>
        /// 创建Div
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cssClass"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        private HtmlGenericControl CreatDiv(string id, string cssClass, string style)
        {
            HtmlGenericControl Creat_Div = new HtmlGenericControl("div");
            //ID 属性
            //Creat_Div.Attributes.Add("id", id);
            //Style 属性
            Creat_Div.Attributes.Add("style", style);
            //class 样式类
            Creat_Div.Attributes.Add("class", cssClass);
            //ruanat 属性
            Creat_Div.Attributes.Add("runat", "server");
            //div中的文字
            Creat_Div.InnerText = "";
            return Creat_Div;
        }

        /// <summary>
        /// 创建Input_Text
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cssClass"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        private HtmlGenericControl CreatText(string id, string cssClass, string style, string Value)
        {
            HtmlGenericControl Creat_Text = new HtmlGenericControl("input");
            //Creat_Text.Attributes.Add("id", id);
            Creat_Text.Attributes.Add("style", style);
            Creat_Text.Attributes.Add("class", cssClass);
            Creat_Text.Attributes.Add("runat", "server");
            Creat_Text.Attributes.Add("type", "text");
            Creat_Text.Attributes.Add("disabled", "disabled");
            Creat_Text.Attributes.Add("Value", Value);
            return Creat_Text;
        }

        /// <summary>
        /// 创建Label
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cssClass"></param>
        /// <param name="InnerText"></param>
        /// <returns></returns>
        private HtmlGenericControl CreatLabel(string id, string cssClass, string InnerText)
        {
            HtmlGenericControl Creat_Label = new HtmlGenericControl("label");
            //Creat_Label.Attributes.Add("id", id);
            Creat_Label.Attributes.Add("runat", "server");
            Creat_Label.Attributes.Add("class", cssClass);
            Creat_Label.InnerText = InnerText;
            return Creat_Label;
        }

        /// <summary>
        /// 创建Submit
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cssClass"></param>
        /// <param name="Value"></param>
        /// <param name="clickEvent"></param>
        /// <returns></returns>
        private HtmlGenericControl CreatSubmit(string id, string cssClass, string Value, string href)
        {
            HtmlGenericControl Creat_Submit = new HtmlGenericControl("a");
            //Creat_Submit.Attributes.Add("id", id);
            Creat_Submit.Attributes.Add("class", cssClass);
            //Creat_Submit.Attributes.Add("runat", "server");
            //Creat_Submit.Attributes.Add("type", "submit");
            //Creat_Submit.Attributes.Add("Value", Value);
            //Creat_Submit.Attributes.Add("onclick", "Revoke(" + Value + ");");
            Creat_Submit.Attributes.Add("href", href);
            Creat_Submit.InnerText = "立即撤销";
            return Creat_Submit;
        }

        /// <summary>
        /// 创建换行
        /// </summary>
        /// <returns></returns>
        private HtmlGenericControl CreatBr()
        {
            HtmlGenericControl Creat_Br = new HtmlGenericControl("br");
            return Creat_Br;
        }
    }
}