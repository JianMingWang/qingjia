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
    public partial class info_detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDB();
            }
        }

        //加载个人信息
        private void LoadDB()
        {
            string access_token = Session["access_token"].ToString();
            string ST_NUM = access_token.Substring(0, access_token.IndexOf("_"));
            Client<UserInfo> client = new Client<UserInfo>();
            ApiResult<UserInfo> result = client.GetRequest("access_token=" + access_token, "/api/student/me");
            if (result.result == "error" || result.data == null)
            {
                Response.Redirect("../Error.aspx?errorMessage=" + result.messages);
                return;
            }

            UserInfo userInfo = result.data;

            Label_Num.InnerText = userInfo.UserID;
            Label_Name.InnerText = userInfo.UserName;
            Label_Sex.InnerText = userInfo.UserSex;
            Label_Class.InnerText = userInfo.UserClass;
            txtTel.Value = userInfo.UserTel;
            txtQQ.Value = userInfo.UserQQ;
            if (userInfo.ContactName != "" && userInfo.ContactTel != "" && userInfo.UserTel != "" && userInfo.UserQQ != "")
            {
                txtGuardianName.Value = userInfo.ContactName;
                txtGuardianNum.Value = userInfo.ContactTel;
            }
            else
            {
                txtError.Value = "首次登陆需请填写个人信息！";
                txtGuardianName.Value = "";
                txtGuardianNum.Value = "";
            }
        }

        //更新个人信息
        protected void btnSubmit_ServerClick(object sender, EventArgs e)
        {
            string access_token = Session["access_token"].ToString();

            if (Check())
            {
                string ST_Tel = txtTel.Value;
                string ST_QQ = txtQQ.Value;
                string Guardian = txtGuardianName.Value.ToString().Trim();
                string ST_Guardian = Guardian.Substring(0, 2);
                string ST_GuardianName = Guardian.Substring(3, Guardian.Length - 3);
                string ST_GuardianTel = txtGuardianNum.Value;

                Client<string> client = new Client<string>();
                string _postString = String.Format("access_token={0}&ST_Tel={1}&ST_QQ={2}&ST_Guardian={3}&ST_GuardianName={4}&ST_GuardianTel={5}", access_token, ST_Tel, ST_QQ, ST_Guardian, ST_GuardianName, ST_GuardianTel);
                ApiResult<string> result = client.PostRequest(_postString, "/api/student/changeinfo");

                if (result.result == "success")
                {
                    Response.Redirect("info_succeed.aspx");
                }
                else
                {
                    txtError.Value = result.messages;
                }
            }
        }

        //检查输入信息是否合法
        private bool Check()
        {
            if (CheckText(txtTel))
            {
                if (CheckText(txtQQ))
                {
                    if (CheckText(txtGuardianNum))
                    {
                        if (CheckText(txtGuardianName))
                        {
                            string st_guardian_name = txtGuardianName.Value.ToString().Trim();
                            if (st_guardian_name.Length > 3)
                            {
                                if (!((st_guardian_name.Substring(0, 2) == "父亲" || st_guardian_name.Substring(0, 2) == "母亲" || st_guardian_name.Substring(0, 2) == "其他") && st_guardian_name.Substring(2, 1) == "-"))
                                {
                                    txtError.Value = "填写方式错误！关系为父亲、母亲、其他";
                                    return false;
                                }
                                else
                                {
                                    return true;
                                }
                            }
                            else
                            {
                                txtError.Value = "请注意填写方式！";
                                return false;
                            }
                        }
                        else
                        {
                            txtError.Value = "未填写与家长关系及姓名！";
                            return false;
                        }
                    }
                    else
                    {
                        txtError.Value = "未填写家长联系方式！";
                        return false;
                    }
                }
                else
                {
                    txtError.Value = "未填写QQ号信息！";
                    return false;
                }
            }
            else
            {
                txtError.Value = "未填写个人联系方式！";
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
    }
}