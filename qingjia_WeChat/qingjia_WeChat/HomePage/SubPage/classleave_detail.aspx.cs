using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using qingjia_YiBan.HomePage.Class;
using System.Data;
using System.Data.SqlClient;
using qingjia_YiBan.HomePage.Model.API;

namespace qingjia_YiBan.SubPage
{
    public partial class classleave_detail : System.Web.UI.Page
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
            if (HttpContext.Current.Request.Cookies["UserInfo"] != null)
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["UserInfo"];
                Label_Num.InnerText = cookie["UserID"].ToString();
                Label_Name.InnerText = HttpUtility.UrlDecode(cookie["UserName"].ToString());
                Label_Class.InnerText = HttpUtility.UrlDecode(cookie["UserClass"].ToString());
                Label_Tel.InnerText = cookie["UserTel"].ToString();
            }
            else
            {
                string access_token = Session["access_token"].ToString();
                string ST_NUM = access_token.Substring(0, access_token.IndexOf("_"));
                Client<UserInfo> client = new Client<UserInfo>();
                ApiResult<UserInfo> result = client.GetRequest("access_token=" + access_token, "api/student/me");
                UserInfo userInfo = result.data;
                Label_Num.InnerText = userInfo.UserID;
                Label_Name.InnerText = userInfo.UserName;
                Label_Class.InnerText = userInfo.UserClass;
                Label_Tel.InnerText = userInfo.UserTel;
            }
        }

        protected void btnSubmit_ServerClick(object sender, EventArgs e)
        {
            if (Check())
            {
                Insertdata_out();
            }
        }

        //检查输入信息是否合法
        private bool Check()
        {
            if (CheckText(test_default1) && CheckText(txtTeacherName) && LeaveReason.Value.ToString().Trim() != "")
            {
                if (LeaveReason.Value.ToString() != "")
                {
                    if (LeaveReason.Value.ToString().Length <= 60)
                    {
                        return true;
                    }
                    else
                    {
                        txtError.Value = "请假原因不能超过60个字！";
                        return false;
                    }
                }
                else
                {
                    txtError.Value = "请假原因不能为空！";
                    return false;
                }
            }
            else
            {
                txtError.Value = "存在未填写信息！";
                return false;
            }
        }

        private bool CheckText(HtmlInputText txt)
        {
            if (txt.Value == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        protected void Insertdata_out()//上课请假备案
        {
            string access_token = Session["access_token"].ToString();
            string leave_child_type = "";
            string leave_date = "";
            string leave_reason = LeaveReason.Value.ToString().Trim();
            string teacher_name = txtTeacherName.Value.ToString().Trim();
            string lesson = "";

            DateTime gotime_out = Convert.ToDateTime(ChangeTime(test_default1.Value.ToString()));
            leave_date = gotime_out.ToString("yyyy-MM-dd");

            if (x41.Checked == true)
            {
                lesson = "1";
            }
            if (x42.Checked == true)
            {
                lesson = "2";
            }
            if (x43.Checked == true)
            {
                lesson = "3";
            }
            if (x44.Checked == true)
            {
                lesson = "4";
            }
            if (x45.Checked == true)
            {
                lesson = "5";
            }

            if (x31.Checked == true)
            {
                leave_child_type = "公假";
            }
            if (x32.Checked == true)
            {
                leave_child_type = "事假";
            }
            if (x33.Checked == true)
            {
                leave_child_type = "病假";
            }

            string _postString = String.Format("access_token={0}&leave_child_type={1}&leave_date={2}&leave_reason={3}&teacher_name={4}&lesson={5}", access_token, leave_child_type, leave_date, leave_reason, teacher_name, lesson);

            Client<string> client = new Client<string>();
            ApiResult<string> result = new ApiResult<string>();
            result = client.PostRequest(_postString, "/api/leavelist/leavelesson");
            if (result.result == "success")
            {
                Response.Redirect("classleave_succeed.aspx");
            }
            else
            {
                txtError.Value = result.messages;
            }
        }

        private string ChangeTime(string time)
        {
            string time_changed = time.Substring(6, 4) + "-" + time.Substring(0, 2) + "-" + time.Substring(3, 2);
            return time_changed;
        }
    }
}