using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace qingjia_YiBan.HomePage.Model.API
{
    public class YB_AccessToken //获取Json数据 解析模型
    {
        public string access_token { get; set; }
        public string userid { get; set; }
        public string expires { get; set; }
    }
}