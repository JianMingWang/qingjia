using System.Collections.Generic;
using System.Web;
using qingjia_YiBan.HomePage.Class;
using qingjia_YiBan.HomePage.Model.API;

namespace qingjia_WeChat.HomePage
{
    /// <summary>
    /// UserAuth 的摘要说明
    /// </summary>
    public class UserProcess : IHttpHandler
    {
        //微信端口调用  获取微信OpenID
        public void ProcessRequest(HttpContext context)
        {
            //2017 0905  目前使用的微信公众平台是学工广场
            var appid = "wxd66dc84bc3f5e793";//学工广场
            var secret = "9e20112513fdff5b69251ee70c690d5f";//学工广场

            //var appid = "wx7c4da8ce4965fe78";//管理学院
            //var secret = "a137fd495599209338701b65fb895a40";//管理学院

            // context.Session["pageCate"] = pageCate;

            var code = context.Request.QueryString["code"];
            var state = context.Request.QueryString["state"];

            if (string.IsNullOrEmpty(code))
            {
                var url = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri=http%3a%2f%2fwww.zhanglidaoyan.com&response_type=code&scope=snsapi_base&state=STATE#wechat_redirect", appid);
                context.Response.Redirect(url);
            }

            else
            {
                var client = new System.Net.WebClient();
                client.Encoding = System.Text.Encoding.UTF8;

                var url = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code", appid, secret, code);
                var data = client.DownloadString(url);

                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                //反序列化  将json转换成键值对
                var obj = serializer.Deserialize<Dictionary<string, string>>(data);

                //通过关键字来获取值
                string accessToken;
                if (!obj.TryGetValue("access_token", out accessToken))
                    return;

                var openid = obj["openid"];

                if (openid != null && openid != "")
                {
                    //获取到用户ID  AccessToken
                    Client<qingjia_AccessToken> clientModel = new Client<qingjia_AccessToken>();
                    ApiResult<qingjia_AccessToken> result = clientModel.GetRequest("OpenID=" + openid, "/api/oauth/access_token_wechat");

                    if (result.result == "success" || result.data != null)
                    {
                        //获取授权
                        context.Response.Redirect("qingjia_WeChat.aspx?access_token=" + result.data.access_token);
                    }
                    else
                    {
                        //qingjia 授权失败 跳转到绑定页面
                        context.Response.Redirect("./SubPage/bind.aspx?OpenID=" + openid);
                    }
                }
                else
                {
                    //未获取到易班授权授权 跳转到错误页面
                    context.Response.Redirect("Error.aspx");
                }

                //if (opentid != null && opentid != "")
                //{
                //    context.Response.Redirect("./SubPage/bind.aspx?openId=" + opentid);
                //}
                //if (state.Equals("1"))
                //{
                //    context.Response.Redirect("qingjia_WeChat.aspx?openId=" + opentid);
                //}
                //else if (state.Equals("0"))
                //{
                //    context.Response.Redirect("qingjia_WeChat.aspx?openId=" + opentid);
                //}
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}