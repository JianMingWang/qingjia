using System;
using System.Web;
using qingjia_YiBan.HomePage.Class;
using qingjia_YiBan.HomePage.Model.API;
using System.Text;

namespace qingjia_YiBan.HomePage
{
    public partial class qingjia_WeChat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["access_token"] != null)
            {
                string access_token = Request.QueryString["access_token"].ToString();
                Session["access_token"] = access_token;
            }

            //测试运行
            //string access_token = "0121403490107_3749f6d6-8a19-4bb3-887a-3d9d2f464d96";
            //Session["access_token"] = access_token;

            //获取学生基本信息
            LoadDB();
            //获取点名时间、请假截止时间
            LoadTimeEnd();
        }

        private void LoadDB()
        {
            string access_token = Session["access_token"].ToString();
            string ST_NUM = access_token.Substring(0, access_token.IndexOf("_"));

            if (access_token == null)
            {
                Response.Redirect("Error.aspx");
            }

            //判断当前回话是否存在Cookie
            if (HttpContext.Current.Request.Cookies["UserInfo"] != null && HttpContext.Current.Request.Cookies["UserInfo"]["UserID"] == ST_NUM)
            {
                HttpCookie _cookie = HttpContext.Current.Request.Cookies["UserInfo"];
                label_teacherName.InnerText = HttpUtility.UrlDecode(_cookie["UserTeacher"]);
                label_Year.InnerText = _cookie["UserYear"].ToString();
            }
            else
            {
                Client<UserInfo> client = new Client<UserInfo>();
                ApiResult<UserInfo> result = client.GetRequest("access_token=" + access_token, "/api/student/me");

                if (result.result == "error" || result.data == null)
                {
                    //出现错误，获取信息失败，跳转到错误界面 尚未完成
                    Response.Redirect("Error.aspx");
                    return;
                }

                UserInfo userInfo = result.data;

                //登录信息正确，将相关信息写入cookies
                if (HttpContext.Current.Request.Cookies["UserInfo"] != null)
                {
                    HttpContext.Current.Response.Cookies.Remove("UserInfo");
                }
                HttpCookie cookie = new HttpCookie("UserInfo");
                cookie.Values.Add("UserID", userInfo.UserID.ToString().Trim());
                cookie.Values.Add("UserName", HttpUtility.UrlEncode(userInfo.UserName.ToString().Trim()));
                cookie.Values.Add("UserClass", HttpUtility.UrlEncode(userInfo.UserClass.ToString().Trim()));
                cookie.Values.Add("UserYear", userInfo.UserYear.ToString().Trim());
                cookie.Values.Add("UserTel", userInfo.UserTel.ToString().Trim());
                cookie.Values.Add("UserTeacher", HttpUtility.UrlEncode(userInfo.UserTeacherName.ToString().Trim()));
                cookie.Values.Add("UserTeacherID", userInfo.UserTeacherID.ToString().Trim());
                cookie.Values.Add("UserContactName", HttpUtility.UrlEncode(userInfo.ContactName.ToString().Trim()));
                cookie.Values.Add("UserContactTel", userInfo.ContactTel.ToString().Trim());
                cookie.Values.Add("IsFreshman", userInfo.IsFreshman.ToString().Trim());
                cookie.Expires = DateTime.Now.AddMinutes(20);
                HttpContext.Current.Response.Cookies.Add(cookie);

                UpdateInfo(userInfo);
                label_teacherName.InnerText = userInfo.UserTeacherName;
                label_Year.InnerText = userInfo.UserYear;
            }
        }

        private void LoadTimeEnd()
        {
            string access_token = Session["access_token"].ToString();
            string ST_NUM = access_token.Substring(0, access_token.IndexOf("_"));

            #region 获得晚点名信息
            if (HttpContext.Current.Request.Cookies["NightInfo"] != null && HttpContext.Current.Request.Cookies["NightInfo"]["UserID"] == ST_NUM)
            {
                HttpCookie _cookie = HttpContext.Current.Request.Cookies["NightInfo"];
                //晚点名请假截止时间
                if (_cookie["DeadLine"] != null)
                {
                    DateTime end_time_night = Convert.ToDateTime(_cookie["DeadLine"].ToString());

                    if (end_time_night < DateTime.Now)//小于当前是见表示尚可请假
                    {
                        label_EndTime.InnerText = "已过请假时间！";
                    }
                    else
                    {
                        label_EndTime.InnerText = end_time_night.ToString("yyyy/MM/dd HH:mm");
                    }
                }
                else
                {
                    label_EndTime.InnerText = "未设置";
                }

                //晚点名时间
                if (_cookie["BatchTime"] != null)
                {
                    DateTime call_time = Convert.ToDateTime(_cookie["BatchTime"].ToString());
                    label_CallTime.InnerText = call_time.ToString("yyyy/MM/dd HH:mm");
                }
                else
                {
                    label_CallTime.InnerText = "未设置";
                }
            }
            else
            {
                Client<NightInfo> client_Night = new Client<NightInfo>();
                ApiResult<NightInfo> result_Night = client_Night.GetRequest("access_token=" + access_token, "/api/student/night");
                if (result_Night.result == "success")
                {
                    NightInfo nightInfo = result_Night.data;

                    #region 存入Cookie
                    if (Request.Cookies["NightInfo"] != null)
                    {
                        Response.Cookies.Remove("NightInfo");
                    }

                    HttpCookie cookie = new HttpCookie("NightInfo");//晚点名信息
                    cookie.Values.Add("UserID", ST_NUM);
                    cookie.Values.Add("TeacherID", nightInfo.TeacherID);//老师ID
                    cookie.Values.Add("TeacherName", HttpUtility.UrlEncode(nightInfo.TeacherName));//老师姓名
                    cookie.Values.Add("BatchTime", nightInfo.BatchTime);//晚点名批次时间
                    cookie.Values.Add("DeadLine", nightInfo.DeadLine);//晚点名请假截止时间
                    cookie.Expires = DateTime.Now.AddMinutes(20);
                    HttpContext.Current.Response.Cookies.Add(cookie);
                    #endregion

                    //晚点名请假截止时间
                    if (nightInfo.DeadLine != null)
                    {
                        DateTime end_time_night = Convert.ToDateTime(nightInfo.DeadLine);

                        if (end_time_night < DateTime.Now)//小于当前是见表示尚可请假
                        {
                            label_EndTime.InnerText = "已过请假时间！";
                        }
                        else
                        {
                            label_EndTime.InnerText = end_time_night.ToString("yyyy/MM/dd HH:mm");
                        }
                    }
                    else
                    {
                        label_EndTime.InnerText = "未设置";
                    }

                    //晚点名时间
                    if (nightInfo.BatchTime != null)
                    {
                        DateTime call_time = Convert.ToDateTime(nightInfo.BatchTime);
                        label_CallTime.InnerText = call_time.ToString("yyyy/MM/dd HH:mm");
                    }
                    else
                    {
                        label_CallTime.InnerText = "未设置";
                    }
                }
                else
                {
                    label_EndTime.InnerText = "获取数据失败！";
                    label_CallTime.InnerText = "获取数据失败！";
                }
            }
            #endregion

            #region 获得节假日信息
            if (HttpContext.Current.Request.Cookies["HolidayInfo"] != null && HttpContext.Current.Request.Cookies["HolidayInfo"]["UserID"] == ST_NUM)
            {
                HttpCookie _cookie = HttpContext.Current.Request.Cookies["HolidayInfo"];
                //节假日请假时间
                if (_cookie["DeadLine"] != null)
                {
                    DateTime end_time_holiday = Convert.ToDateTime(_cookie["DeadLine"].ToString());

                    if (end_time_holiday < DateTime.Now)//小于当前是见表示尚可请假
                    {
                        vacation_end_time.Value = "已过请假时间！";
                    }
                    else
                    {
                        Default_Vacation.Visible = true;
                        vacation_end_time.Value = end_time_holiday.ToString("yyyy/MM/dd HH:mm");
                    }
                }
                else
                {
                    vacation_end_time.Value = "未设置";
                }
            }
            else
            {
                Client<Holiday> client_Holiday = new Client<Holiday>();
                ApiResult<Holiday> result_Holiday = client_Holiday.GetRequest("access_token=" + access_token, "/api/student/holiday");
                if (result_Holiday.result == "success")
                {
                    Holiday holiday = result_Holiday.data;

                    #region 存入Cookie
                    if (HttpContext.Current.Request.Cookies["HolidayInfo"] != null)
                    {
                        HttpContext.Current.Response.Cookies.Remove("HolidayInfo");
                    }
                    HttpCookie cookie = new HttpCookie("HolidayInfo");//节假日离校信息
                    cookie.Values.Add("UserID", ST_NUM);
                    cookie.Values.Add("TeacherID", holiday.TeacherID);//老师ID
                    cookie.Values.Add("DeadLine", holiday.DeadLine);//截止时间
                    cookie.Expires = DateTime.Now.AddMinutes(20);
                    HttpContext.Current.Response.Cookies.Add(cookie);
                    #endregion

                    //节假日请假时间
                    if (holiday.DeadLine != null)
                    {
                        DateTime end_time_holiday = Convert.ToDateTime(holiday.DeadLine);

                        if (end_time_holiday < DateTime.Now)//小于当前是见表示尚可请假
                        {
                            vacation_end_time.Value = "已过请假时间！";
                        }
                        else
                        {
                            Default_Vacation.Visible = true;
                            vacation_end_time.Value = end_time_holiday.ToString("yyyy/MM/dd HH:mm");
                        }
                    }
                    else
                    {
                        vacation_end_time.Value = "未设置";
                    }
                }
                else
                {
                    vacation_end_time.Value = "获取数据失败！";
                }
            }
            #endregion
        }

        //完善个人信息
        private void UpdateInfo(UserInfo userInfo)
        {
            if (userInfo.ContactName == "" || userInfo.ContactTel == "")
            {
                Response.Redirect("./SubPage/info_detail.aspx");
            }
        }
    }
}