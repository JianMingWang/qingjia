using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using qingjia_MVC.Models;
using qingjia_MVC.Common;
using qingjia_MVC.Models.API;
using System.IO;
using System.Configuration;
using System.Web.Http.Controllers;

namespace qingjia_MVC.Controllers
{
    #region 数据模型

    public class User_Login
    {
        public string UserID { get; set; }
        public string UserPsd { get; set; }
        public string YiBanID { get; set; }
    }

    public class User_Login_WeChat
    {
        public string UserID { get; set; }
        public string UserPsd { get; set; }
        public string OpenID { get; set; }
    }

    #endregion

    [RoutePrefix("api/oauth")]
    public class OauthController : ApiController
    {
        //实例化数据库
        private imaw_qingjiaEntities db = new imaw_qingjiaEntities();

        /// <summary>
        /// POST
        /// 
        /// 获取授权 用户ID 用户Psd 易班用户ID  返回给用户一个GUID当做AccessToken
        /// </summary>
        /// <param name="user_login_info"></param>
        /// <returns></returns>
        [HttpPost, Route("authorize")]
        public ApiBaseResult Authorize([FromBody]User_Login user_login_info)
        {
            ApiBaseResult result = new ApiBaseResult();

            string UserID = "";
            string UserPSd = "";
            string YiBanID = "";

            #region 检查参数是否正确

            if (user_login_info == null)
            {
                //参数错误
                result.result = "error";
                result.messages = "未接收到合法参数！";
                return result;
            }
            else
            {
                //存在合法参数正确
                try
                {
                    UserID = user_login_info.UserID;
                    UserPSd = user_login_info.UserPsd;
                    YiBanID = user_login_info.YiBanID;
                    if (UserID == null || UserPSd == null || YiBanID == null)
                    {
                        result.result = "error";
                        result.messages = "参数格式错误或缺少参数！";
                        return result;
                    }
                }
                catch
                {
                    result.result = "error";
                    result.messages = "参数格式错误或缺少参数！";
                    return result;
                }
            }

            #endregion

            #region 账号绑定

            var accountList = from T_Account in db.T_Account where (T_Account.YiBanID == YiBanID) select T_Account;
            if (accountList.Any())
            {
                //此账号已绑定
                result.result = "error";
                result.messages = "此账号已绑定！";
            }
            else
            {
                //此账号尚未绑定
                T_Account account = db.T_Account.Find(UserID);
                if (account != null)
                {
                    if (account.Psd == UserPSd)
                    {
                        //验证通过
                        string GuidString = Guid.NewGuid().ToString();
                        string access_token = UserID + "_" + GuidString;
                        account.YiBanID = YiBanID;
                        account.YB_AccessToken = GuidString;
                        db.SaveChanges();

                        AccessToken authorizeModel = new AccessToken();
                        authorizeModel.access_token = access_token;
                        result.result = "success";
                        result.data = authorizeModel;
                    }
                    else
                    {
                        //用户密码错误
                        result.result = "error";
                        result.messages = "账号密码错误！";
                    }
                }
                else
                {
                    //此用户ID不存在
                    result.result = "error";
                    result.messages = "此用户ID不存在！";
                }
            }

            #endregion

            return result;
        }

        [HttpPost, Route("authorize_wechat")]
        public ApiBaseResult Authorize_WeChat([FromBody]User_Login_WeChat user_login_info)
        {
            ApiBaseResult result = new ApiBaseResult();

            string UserID = "";
            string UserPSd = "";
            string OpenID = "";

            #region 检查参数是否正确

            if (user_login_info == null)
            {
                //参数错误
                result.result = "error";
                result.messages = "未接收到合法参数！";
                return result;
            }
            else
            {
                //存在合法参数正确
                try
                {
                    UserID = user_login_info.UserID;
                    UserPSd = user_login_info.UserPsd;
                    OpenID = user_login_info.OpenID;
                    if (UserID == null || UserPSd == null || OpenID == null)
                    {
                        result.result = "error";
                        result.messages = "参数格式错误或缺少参数！";
                        return result;
                    }
                }
                catch
                {
                    result.result = "error";
                    result.messages = "参数格式错误或缺少参数！";
                    return result;
                }
            }

            #endregion

            #region 账号绑定

            var accountList = from T_Account in db.T_Account where (T_Account.Wechat == OpenID) select T_Account;
            if (accountList.Any())
            {
                //此账号已绑定
                result.result = "error";
                result.messages = "此账号已绑定！";
            }
            else
            {
                //此账号尚未绑定
                T_Account account = db.T_Account.Find(UserID);
                if (account != null)
                {
                    if (account.Psd == UserPSd)
                    {
                        //验证通过
                        string GuidString = Guid.NewGuid().ToString();
                        string access_token = UserID + "_" + GuidString;
                        account.Wechat = OpenID;
                        account.YB_AccessToken = GuidString;
                        db.SaveChanges();

                        AccessToken authorizeModel = new AccessToken();
                        authorizeModel.access_token = access_token;
                        result.result = "success";
                        result.data = authorizeModel;
                    }
                    else
                    {
                        //用户密码错误
                        result.result = "error";
                        result.messages = "账号密码错误！";
                    }
                }
                else
                {
                    //此用户ID不存在
                    result.result = "error";
                    result.messages = "此用户ID不存在！";
                }
            }

            #endregion

            return result;
        }

        /// <summary>
        /// GET YiBanID 验证
        /// 
        /// 获取AccessToken
        /// </summary>
        /// <param name="YiBanID"></param>
        /// <returns></returns>
        [HttpGet, Route("access_token")]
        public ApiBaseResult Access_Token(string YiBanID)
        {
            //存在问题：Access_Token会被访问两次，未找到原因

            ApiBaseResult result = new ApiBaseResult();

            var userList = from T_Account in db.T_Account where (T_Account.YiBanID == YiBanID) select T_Account;
            if (userList.Any())
            {
                //3分钟内已获得access_token
                if (userList.ToList().First().LoginTime != null)
                {
                    TimeSpan ts = DateTime.Now - (DateTime)userList.ToList().First().LoginTime;
                    if (ts.Minutes < 1)
                    {
                        AccessToken authorizeModel = new AccessToken();
                        string access_token = userList.ToList().First().ID + "_" + userList.ToList().First().YB_AccessToken;
                        authorizeModel.access_token = access_token;
                        result.result = "success";
                        result.data = authorizeModel;

                        WriteLog("Old access_token", access_token);
                    }
                    else
                    {
                        //验证通过
                        string GuidString = Guid.NewGuid().ToString();
                        string access_token = "";
                        string UserID = userList.ToList().First().ID;
                        T_Account account = db.T_Account.Find(UserID);
                        account.LoginTime = DateTime.Now;
                        access_token = account.ID + "_" + GuidString;
                        account.YiBanID = YiBanID;
                        account.YB_AccessToken = GuidString;
                        db.SaveChanges();

                        AccessToken authorizeModel = new AccessToken();
                        authorizeModel.access_token = access_token;
                        result.result = "success";
                        result.data = authorizeModel;

                        WriteLog("New access_token", access_token);
                    }
                }
                else
                {
                    //验证通过
                    string GuidString = Guid.NewGuid().ToString();
                    string access_token = "";
                    string UserID = userList.ToList().First().ID;
                    T_Account account = db.T_Account.Find(UserID);
                    account.LoginTime = DateTime.Now;
                    access_token = account.ID + "_" + GuidString;
                    account.YiBanID = YiBanID;
                    account.YB_AccessToken = GuidString;
                    db.SaveChanges();

                    AccessToken authorizeModel = new AccessToken();
                    authorizeModel.access_token = access_token;
                    result.result = "success";
                    result.data = authorizeModel;

                    WriteLog("New access_token", access_token);
                }
            }
            else
            {
                //尚未绑定YiBanID
                result.result = "error";
                result.messages = "尚未绑定账号的易班ID，通过Authorize接口实现易班账号绑定。";
            }
            return result;
        }

        /// <summary>
        /// GET OpenID 验证
        /// 
        /// </summary>
        /// <param name="OpenID"></param>
        /// <returns></returns>
        [HttpGet, Route("access_token_wechat")]
        public ApiBaseResult Access_Token_WeChat(string OpenID)
        {
            //存在问题：Access_Token会被访问两次，未找到原因

            ApiBaseResult result = new ApiBaseResult();

            var userList = from T_Account in db.T_Account where (T_Account.Wechat == OpenID) select T_Account;
            if (userList.Any())
            {
                if (userList.ToList().First().LoginTime != null)
                {
                    //判断1分钟内是否访问过该接口
                    TimeSpan ts = DateTime.Now - (DateTime)userList.ToList().First().LoginTime;
                    //1分钟内已获得access_token
                    if (ts.Minutes < 1)
                    {
                        AccessToken authorizeModel = new AccessToken();
                        string access_token = userList.ToList().First().ID + "_" + userList.ToList().First().YB_AccessToken;
                        authorizeModel.access_token = access_token;
                        result.result = "success";
                        result.data = authorizeModel;

                        WriteLog("Old access_token", access_token);
                    }
                    else
                    {
                        //验证通过
                        string GuidString = Guid.NewGuid().ToString();
                        string access_token = "";
                        string UserID = userList.ToList().First().ID;
                        T_Account account = db.T_Account.Find(UserID);
                        account.LoginTime = DateTime.Now;
                        access_token = account.ID + "_" + GuidString;
                        account.Wechat = OpenID;
                        account.YB_AccessToken = GuidString;
                        db.SaveChanges();

                        AccessToken authorizeModel = new AccessToken();
                        authorizeModel.access_token = access_token;
                        result.result = "success";
                        result.data = authorizeModel;

                        WriteLog("New access_token", access_token);
                    }
                }
                else
                {
                    //验证通过
                    string GuidString = Guid.NewGuid().ToString();
                    string access_token = "";
                    string UserID = userList.ToList().First().ID;
                    T_Account account = db.T_Account.Find(UserID);
                    account.LoginTime = DateTime.Now;
                    access_token = account.ID + "_" + GuidString;
                    account.Wechat = OpenID;
                    account.YB_AccessToken = GuidString;
                    db.SaveChanges();

                    AccessToken authorizeModel = new AccessToken();
                    authorizeModel.access_token = access_token;
                    result.result = "success";
                    result.data = authorizeModel;

                    WriteLog("New access_token", access_token);
                }
            }
            else
            {
                //尚未绑定YiBanID
                result.result = "error";
                result.messages = "尚未绑定账号的易班ID，通过Authorize接口实现易班账号绑定。";
            }
            return result;
        }

        private void WriteLog(string name, string value)
        {
            try
            {
                string logPath = ConfigurationManager.AppSettings["logPath"].ToString();
                string path = logPath + @"\log.txt";

                FileStream fs = new FileStream(path, FileMode.Append);
                string time = DateTime.Now.ToString();
                string content = name + ":" + time + "   " + "value:" + value + "Host:" + RequestContext.Url.Request.Headers.Host.ToString() + "\n\n" + ">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\n\n";
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(content);
                sw.Close();
                fs.Close();
                RequestContext.Url.Request.Headers.Host.ToString();
            }
            catch
            {

            }
        }
    }
}
