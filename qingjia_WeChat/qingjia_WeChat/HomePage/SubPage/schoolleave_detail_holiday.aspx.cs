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
    public partial class WebForm6 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (CheckDate())
                {
                    LoadDB();
                }
                else
                {
                    end.Style.Add("display", "block");
                    index.Style.Add("display", "none");
                }
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
                Label_ParentTel.InnerText = cookie["UserContactTel"].ToString();
            }
            else
            {
                //从API接口获取数据
                string access_token = Session["access_token"].ToString();
                string ST_NUM = access_token.Substring(0, access_token.IndexOf("_"));
                Client<UserInfo> client = new Client<UserInfo>();
                ApiResult<UserInfo> result = client.GetRequest("access_token=" + access_token, "/api/student/me");

                if (result.result == "error" || result.data == null)
                {
                    //出现错误，获取信息失败，跳转到错误界面 尚未完成
                    Response.Redirect("../Error.aspx");
                    return;
                }
                UserInfo userInfo = result.data;

                Label_Num.InnerText = userInfo.UserID;
                Label_Name.InnerText = userInfo.UserName;
                Label_Class.InnerText = userInfo.UserClass;
                Label_Tel.InnerText = userInfo.UserTel;
                Label_ParentTel.InnerText = userInfo.ContactTel;
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
            if (CheckText(test_default1) && CheckText(test_default2) && CheckText(txt_leave_way) && CheckText(txt_back_way))
            {
                if (Convert.ToDateTime(ChangeTime(test_default1.Value.ToString())) < Convert.ToDateTime(ChangeTime(test_default2.Value.ToString())))
                {
                    if (Leave_Reason.Value.ToString() != "")
                    {
                        return true;
                    }
                    else
                    {
                        txtError.Value = "离校去向不能为空！";
                        return false;
                    }
                }
                else
                {
                    txtError.Value = "返校时间不能小于离校时间！";
                    return false;
                }
            }
            else
            {
                txtError.Value = "请将信息填充完整！";
                return false;
            }
        }

        //检查文本框
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

        protected void Insertdata_out()//离校请假用的
        {
            DateTime gotime_out = Convert.ToDateTime(ChangeTime(test_default1.Value.ToString()));
            DateTime backtime_out = Convert.ToDateTime(ChangeTime(test_default2.Value.ToString()));

            //请假记录插入操作
            Insert_out(gotime_out, backtime_out);

        }

        //请假记录插入操作
        public void Insert_out(DateTime gotime_out, DateTime backtime_out)
        {
            #region 拼装数据
            string access_token = Session["access_token"].ToString();
            string leave_type = "节假日请假";
            string leave_date = gotime_out.ToString("yyyy-MM-dd");
            string leave_time = gotime_out.ToString("HH:mm:ss");//HH 代表24小时制 hh代表12小时制
            string back_date = backtime_out.ToString("yyyy-MM-dd");
            string back_time = backtime_out.ToString("HH:mm:ss");//HH 代表24小时制 hh代表12小时制
            string leave_way = txt_leave_way.Value.ToString().Trim();
            string back_way = txt_back_way.Value.ToString().Trim();
            string address = Leave_Reason.Value.ToString().Trim();

            string leave_reason = "";
            if (x31.Checked == true)
            {
                leave_reason = "回家";
            }
            if (x32.Checked == true)
            {
                leave_reason = "旅游";
            }
            if (x33.Checked == true)
            {
                leave_reason = "因公外出";
            }
            if (x34.Checked == true)
            {
                leave_reason = "其他";
            }
            #endregion

            #region 发送Post请求
            Client<string> client = new Client<string>();
            string _postString = String.Format("access_token={0}&leave_type={1}&leave_date={2}&leave_time={3}&back_date={4}&back_time={5}&leave_way={6}&back_way={7}&address={8}&leave_reason={9}", access_token, leave_type, leave_date, leave_time, back_date, back_time, leave_way, back_way, address, leave_reason);//9个参数
            ApiResult<string> result = client.PostRequest(_postString, "/api/leavelist/leaveschool");
            if (result != null)
            {
                if (result.result == "success")
                {
                    Response.Redirect("schoolleave_succeed.aspx");
                }
                else
                {
                    txtError.Value = result.messages;
                }
            }
            else
            {
                //出现错误   此处报错说明API接口或网络存在问题
                txtError.Value = "出现未知错误，请联系管理员！";
            }
            #endregion
        }

        //转换日期格式
        private string ChangeTime(string time)
        {
            string txt_time = test_default1.Value.ToString();
            string time_changed = time.Substring(6, 4) + "-" + time.Substring(0, 2) + "-" + time.Substring(3, 2) + " ";
            if (time.IndexOf("PM") != -1)//说明含有PM
            {
                if (time.Substring(11, 2) != "12")//PM   且不为12
                {
                    int hour = int.Parse(time.Substring(11, 2)) + 12;
                    time_changed += hour + time.Substring(13, 3) + ":00.000";
                }
                else
                {
                    time_changed += time.Substring(11, 5) + ":00.000";
                }
            }
            else//说明含有AM
            {
                if (time.Substring(11, 2) != "12")//PM   且不为12
                {
                    time_changed += time.Substring(11, 5) + ":00.000";
                }
                else
                {
                    time_changed += "00" + time.Substring(13, 3) + ":00.000";
                }
            }
            return time_changed;
        }

        //检查日期时间格式
        private bool CheckDate()
        {
            string access_token = Session["access_token"].ToString();
            string ST_NUM = access_token.Substring(0, access_token.IndexOf("_"));

            //判断Cookie是否存在
            if (HttpContext.Current.Request.Cookies["HolidayInfo"] != null && HttpContext.Current.Request.Cookies["HolidayInfo"]["UserID"] == ST_NUM)
            {
                HttpCookie _cookie = Request.Cookies["HolidayInfo"];
                string time = _cookie["DeadLine"];
                DateTime _time;
                try
                {
                    _time = Convert.ToDateTime(time);

                    if (_time <= DateTime.Now)//小于当前时间表示不可请假
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                catch
                {
                    Response.Redirect("../Error.aspx");
                    return false;
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
                    string time = holiday.DeadLine;
                    DateTime _time;
                    try
                    {
                        _time = Convert.ToDateTime(time);

                        if (_time <= DateTime.Now)//小于当前时间表示不可请假
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    catch
                    {
                        Response.Redirect("../Error.aspx");
                        return false;
                    }
                }
                else
                {
                    Response.Redirect("../Error.aspx");
                    return false;
                }
            }
        }

    }
}