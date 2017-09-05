using System.Web;
using System.Text;
using qingjia_YiBan.HomePage.Class;
using qingjia_YiBan.HomePage.Model.API;


namespace qingjia_YiBan.HomePage
{
    /// <summary>
    /// UserAuth 的摘要说明
    /// </summary>
    public class UserAuth : IHttpHandler
    {
        //易班端口调用  易班轻应用
        public void ProcessRequest(HttpContext context)
        {
            //2017 0905   目前使用的易班账户是 15307179930
            var appid = "28a9bb3bd738ffdb";
            var secret = "415e7189ec176d377fffc3678b732079";

            var code = context.Request.QueryString["code"];//code授权码
            var state = context.Request.QueryString["state"];//state防止拦截攻击

            if (string.IsNullOrEmpty(code))
            {
                var url = string.Format("https://openapi.yiban.cn/oauth/authorize?client_id={0}&redirect_uri=http%3a%2f%2fzhanglidaoyan.com&state=STATE", appid);
                context.Response.Redirect(url);
                return;
            }
            else
            {
                //WebClient 发送 Post请求
                string postString = "client_id=" + appid + "&client_secret=" + secret + "&code=" + code + "&redirect_uri=" + "http://zhanglidaoyan.com";
                byte[] postData = Encoding.UTF8.GetBytes(postString);//将字符串转换为UTF-8编码
                string url = "https://openapi.yiban.cn/oauth/access_token";
                System.Net.WebClient webclient = new System.Net.WebClient();
                webclient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");//POST 请求在头部必须添加
                byte[] responseData = webclient.UploadData(url, "POST", postData);//发起POST请求、返回byte字节
                string srcString = Encoding.UTF8.GetString(responseData);//将byte字节转换为字符串

                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();//解析JSON数据
                JsonModel obj = serializer.Deserialize<JsonModel>(srcString);

                if (obj != null)
                {
                    //获取到用户ID  AccessToken
                    Client<qingjia_AccessToken> client = new Client<qingjia_AccessToken>();
                    ApiResult<qingjia_AccessToken> result = client.GetRequest("YiBanID=" + obj.userid, "/api/oauth/access_token");

                    if (result.result == "success" || result.data != null)
                    {
                        //获取授权
                        context.Response.Redirect("qingjia_WeChat.aspx?access_token=" + result.data.access_token);
                    }
                    else
                    {
                        //qingjia 授权失败 跳转到绑定页面
                        context.Response.Redirect("./SubPage/bind.aspx?YiBanID=" + obj.userid);
                    }
                }
                else
                {
                    //未获取到易班授权授权 跳转到错误页面
                    context.Response.Redirect("Error.aspx");
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public class JsonModel //获取Json数据 解析模型
        {
            public string access_token { get; set; }
            public string userid { get; set; }
            public string expires { get; set; }
        }
    }
}