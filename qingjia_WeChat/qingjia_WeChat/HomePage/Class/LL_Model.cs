using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace qingjia_YiBan.HomePage.Class
{
    public class LL_Model
    {
        public LL_Model()
        { }
        #region Model
        private string _ID;
        private string _StudentID;
        private string _Reason;
        private DateTime? _SubmitTime;
        private string _StateLeave;
        private string _StateBack;
        private string _Notes;
        private int? _TypeID;
        private DateTime? _TimeLeave;
        private DateTime? _TimeBack;
        private string _LeaveWay;
        private string _BackWay;
        private string _Address;
        private int? _TypeChildID;
        private string _Lesson;
        private string _Teacher;
        private string _AuditTeacherID;
        private int? _PrintTimes;
        /// <summary>
        /// 
        /// </summary>
        public string ID
        {
            set { _ID = value; }
            get { return _ID; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StudentID
        {
            set { _StudentID = value; }
            get { return _StudentID; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Reason
        {
            set { _Reason = value; }
            get { return _Reason; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? SubmitTime
        {
            set { _SubmitTime = value; }
            get { return _SubmitTime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StateLeave
        {
            set { _StateLeave = value; }
            get { return _StateLeave; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StateBack
        {
            set { _StateBack = value; }
            get { return _StateBack; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Notes
        {
            set { _Notes = value; }
            get { return _Notes; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? TypeID
        {
            set { _TypeID = value; }
            get { return _TypeID; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? TimeLeave
        {
            set { _TimeLeave = value; }
            get { return _TimeLeave; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? TimeBack
        {
            set { _TimeBack = value; }
            get { return _TimeBack; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LeaveWay
        {
            set { _LeaveWay = value; }
            get { return _LeaveWay; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BackWay
        {
            set { _BackWay = value; }
            get { return _BackWay; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Address
        {
            set { _Address = value; }
            get { return _Address; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? TypeChildID
        {
            set { _TypeChildID = value; }
            get { return _TypeChildID; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Lesson
        {
            set { _Lesson = value; }
            get { return _Lesson; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Teacher
        {
            set { _Teacher = value; }
            get { return _Teacher; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AuditTeacherID
        {
            set { _AuditTeacherID = value; }
            get { return _AuditTeacherID; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? PrintTimes
        {
            set { _PrintTimes = value; }
            get { return _PrintTimes; }
        }
        #endregion Model
    }
}