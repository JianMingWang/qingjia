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
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //验证是否包含早晚自习请假
            if (CheckYear())
            {
                LoadDB();
            }
            else
            {
                end.Style.Add("display", "block");
                index.Style.Add("display", "none");
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
                Label_Tel.InnerText = cookie["UserTel"].ToString(); ;
                Label_ParentTel.InnerText = cookie["UserContactTel"].ToString(); ;
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

        //判断是否需要早晚自习
        private bool CheckYear()
        {
            if (HttpContext.Current.Request.Cookies["UserInfo"] != null)
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["UserInfo"];

                if (cookie["IsFreshman"] == "true")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                string access_token = Session["access_token"].ToString();
                Client<UserInfo> client = new Client<UserInfo>();
                ApiResult<UserInfo> result = client.GetRequest("access_token=" + access_token, "/api/student/me");
                if (result != null)
                {
                    if (result.data.IsFreshman == "true")
                    {
                        return true;
                    }
                    else
                    {
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

        private bool Check()
        {
            if (CheckText(test_default1) && LeaveReason.Value.ToString().Trim() != "")
            {
                return true;
            }
            else
            {
                txtError.Value = "请将信息填充完整！";
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

        protected void Insertdata_out()//晚点名请假
        {
            DateTime gotime_out = Convert.ToDateTime(ChangeTime(test_default1.Value.ToString()));
            Insert_out(gotime_out);
        }

        public void Insert_out(DateTime gotime_out)
        {

            #region 拼装数据
            string access_token = Session["access_token"].ToString();
            string leave_type = "早晚自习请假";
            string leave_child_type = "";
            string leave_date = gotime_out.ToString("yyyy-MM-dd");
            string leave_time = gotime_out.ToString("HH:mm:ss");//HH 代表24小时制 hh代表12小时制
            string back_date = gotime_out.ToString("yyyy-MM-dd");
            string back_time = gotime_out.ToString("HH:mm:ss");//HH 代表24小时制 hh代表12小时制
            string leave_reason = LeaveReason.Value.ToString().Trim();

            if (x11.Checked == true)
            {
                leave_child_type = "公假";
            }
            if (x12.Checked == true)
            {
                leave_child_type = "事假";
            }
            if (x13.Checked == true)
            {
                leave_child_type = "病假";
            }
            #endregion

            #region 发送Post请求
            Client<string> client = new Client<string>();
            string _postString = String.Format("access_token={0}&leave_type={1}&leave_child_type={2}&leave_date={3}&leave_time={4}&back_date={5}&back_time={6}&leave_reason={7}", access_token, leave_type, leave_child_type, leave_date, leave_time, back_date, back_time, leave_reason);//8个参数
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

        private string ChangeTime(string time)
        {
            string txt_time = test_default1.Value.ToString();
            string time_changed = time.Substring(6, 4) + "-" + time.Substring(0, 2) + "-" + time.Substring(3, 2) + " " + "00:00:00.000";
            return time_changed;
        }
    }
}