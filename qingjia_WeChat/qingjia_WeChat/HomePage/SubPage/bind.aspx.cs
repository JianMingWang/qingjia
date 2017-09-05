using System;
using qingjia_YiBan.HomePage.Class;
using qingjia_YiBan.HomePage.Model.API;

namespace qingjia_YiBan.SubPage
{
    public partial class bind : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_ServerClick(object sender, EventArgs e)
        {
            if (Check())
            {
                string UserID = User_Num.Value.ToString().Trim();
                string UserPsd = User_Pw.Value.ToString().Trim();
                string YiBanID = Request.QueryString["OpenID"].ToString();

                Client<AccessToken> client = new Client<AccessToken>();
                string _postString = String.Format("UserID={0}&UserPsd={1}&OpenID={2}", UserID, UserPsd, YiBanID);
                ApiResult<AccessToken> result = client.PostRequest(_postString, "/api/oauth/authorize_wechat");
                if (result.result == "error")
                {
                    txtError.Value = result.messages;
                }
                else
                {
                    AccessToken qingjiaAccessToken = new AccessToken();
                    qingjiaAccessToken = result.data;
                    Response.Redirect("../qingjia_WeChat.aspx?access_token=" + qingjiaAccessToken.access_token);
                }
            }
        }

        private bool Check()
        {
            if (User_Num.Value.ToString().Trim() != "")
            {
                if (User_Pw.Value.ToString().Trim() != "")
                {
                    return true;
                }
                else
                {
                    txtError.Value = "请输入密码！";
                    return false;
                }
            }
            else
            {
                txtError.Value = "请输入账号！";
                return false;
            }
        }
    }
}