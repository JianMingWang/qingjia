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

namespace qingjia_YiBan.SubPage
{
    public partial class modify_detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_ServerClick(object sender, EventArgs e)
        {
            if (Check())
            {
                string access_token = Session["access_token"].ToString();

                string PW_Old = oldPw.Value.ToString().Trim();
                string Pw_New = newPw.Value.ToString().Trim();

                string _postString = String.Format("old_psd={0}&new_psd={1}&access_token={2}", PW_Old, Pw_New, access_token);
                Client<string> client = new Client<string>();
                ApiResult<string> result = client.PostRequest(_postString, "/api/student/password");

                if (result != null)
                {
                    if (result.result == "success")
                    {
                        Response.Redirect("modify_succeed.aspx");
                    }
                    else
                    {
                        txtError.Value = result.messages;
                        oldPw.Value = "";
                        newPw.Value = "";
                        newPwConfirm.Value = "";
                    }
                }
                else
                {
                    txtError.Value = "出现未知错误，请联系管理员！";
                    oldPw.Value = "";
                    newPw.Value = "";
                    newPwConfirm.Value = "";
                }
            }
        }

        //检查输入信息是否合法
        private bool Check()
        {
            if (CheckText(oldPw) && CheckText(newPw) && CheckText(newPwConfirm))
            {
                string Pw1 = newPw.Value.ToString().Trim();
                string Pw2 = newPwConfirm.Value.ToString().Trim();
                if (Pw1 == Pw2)
                {
                    return true;
                }
                else
                {
                    txtError.Value = "两次输入新密码不同！";
                    return false;
                }
            }
            else
            {
                txtError.Value = "请输入旧密码！";
                return false;
            }
        }

        private bool CheckText(HtmlInputPassword Input)
        {
            if (Input.Value == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}