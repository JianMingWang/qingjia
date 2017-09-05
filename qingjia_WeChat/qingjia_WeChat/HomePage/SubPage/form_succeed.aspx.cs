using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using qingjia_YiBan.HomePage.Class;

namespace qingjia_YiBan.SubPage
{
    public partial class form_succeed : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string LV_NUM = Request.QueryString["LV_NUM"].ToString();
            string access_token = Session["access_token"].ToString();

            Client<string> client = new Client<string>();
            ApiResult<string> result = client.GetRequest("leavelistID=" + LV_NUM + "&access_token=" + access_token, "/api/leavelist/revoke");
            Response.Redirect("form_notaudited.aspx");
        }
    }
}