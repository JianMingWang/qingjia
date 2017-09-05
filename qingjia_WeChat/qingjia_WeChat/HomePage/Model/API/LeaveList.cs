using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace qingjia_YiBan.HomePage.Model.API
{
    public class LeaveList
    {
        public string ID { get; set; }
        public string Reasoon { get; set; }
        public string SubmitTime { get; set; }
        public string State { get; set; }
        public string RejectNote { get; set; }
        public string Type { get; set; }
        public DateTime TimeLeave { get; set; }
        public DateTime TimeBack { get; set; }
        public string LeaveWay { get; set; }
        public string BackWay { get; set; }
        public string LeaveAddress { get; set; }
        public string Lesson { get; set; }
        public string TeacherName { get; set; }

    }
}