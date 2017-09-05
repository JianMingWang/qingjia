using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace qingjia_YiBan.HomePage.Class
{
    public class ApiResult<T>
    {
        public ApiResult()
        {

        }

        public ApiResult(string error)
        {
            this.result = "error";
            this.messages = "解析数据时，出现了未知错误！";
        }

        /// <summary>
        /// 返回结果success或failure
        /// </summary>
        public string result { get; set; }
        /// <summary>
        /// 失败原因
        /// </summary>
        public string messages { get; set; }//public List<string> messages { get; set; }
        /// <summary>
        /// 字段格式错误信息
        /// </summary>
        public Dictionary<string, string> fieldErrors { get; set; }
        /// <summary>
        /// 错误码
        /// </summary>
        public string errors { get; set; }//public List<string> errors { get; set; }        
        /// <summary>
        /// 返回结果
        /// </summary>
        public T data { get; set; }
    }
}