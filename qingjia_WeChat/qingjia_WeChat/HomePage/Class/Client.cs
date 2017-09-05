using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Text;
using System.Configuration;

namespace qingjia_YiBan.HomePage.Class
{
    public class Client<T>
    {
        private WebClient client = new WebClient();
        private string url = ConfigurationManager.AppSettings["qingjiaApiUrl"].ToString();
        private ApiResult<T> error_result = new ApiResult<T>("error");
        
        /// <summary>
        /// POST请求
        /// </summary>
        /// <param name="_postString">参数字符串</param>
        /// <param name="_url">API地址路径</param>
        /// <returns></returns>
        public ApiResult<T> PostRequest(string _postString, string _url)
        {
            //POST 请求在头部必须添加//POST 请求在头部必须添加
            this.client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            //将字符串转换为UTF-8编码
            byte[] postData = Encoding.UTF8.GetBytes(_postString);
            url += _url;

            //发起请求，获得字节流返回数据数据
            byte[] responseData = client.UploadData(url, "POST", postData);

            //将byte字节转换为字符串
            string response = Encoding.UTF8.GetString(responseData);

            try
            {
                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();//解析JSON数据
                ApiResult<T> result = serializer.Deserialize<ApiResult<T>>(response);
                return result;
            }
            catch
            {
                return error_result;
            }
        }

        /// <summary>
        /// GET请求
        /// </summary>
        /// <param name="_postString">参数字符串</param>
        /// <param name="_url">API地址路径</param>
        /// <returns></returns>
        public ApiResult<T> GetRequest(string _postString, string _url)
        {
            //将字符串转换为UTF-8编码
            byte[] postData = Encoding.UTF8.GetBytes(_postString);

            //利用URL传值
            url = url + _url + "?" + _postString;

            //string response = client.DownloadString(url);

            byte[] responseData = client.DownloadData(url);
            //将byte字节转换为字符串
            string response = Encoding.UTF8.GetString(responseData);

            try
            {
                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();//解析JSON数据
                ApiResult<T> result = serializer.Deserialize<ApiResult<T>>(response);
                return result;
            }
            catch
            {
                return error_result;
            }
        }
    }
}