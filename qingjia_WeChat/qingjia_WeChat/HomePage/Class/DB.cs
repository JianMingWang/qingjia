using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Configuration;
using qingjia_YiBan.HomePage.Model.API;

namespace qingjia_YiBan.HomePage.Class
{
    public class DB
    {
        //private string connString = ConfigurationManager.AppSettings["ConnectionString"];

        ///// <summary>
        ///// 注册验证
        ///// </summary>
        ///// <returns></returns>
        //static public DataSet RegisterCheck(string strWhere)
        //{
        //    string connString = ConfigurationManager.AppSettings["ConnectionString"];

        //    DataSet ds = new DataSet();

        //    using (SqlConnection conn = new SqlConnection(connString))
        //    {
        //        conn.Open();
        //        StringBuilder strSql = new StringBuilder();
        //        strSql.Append("select ID,Wechat ");
        //        strSql.Append(" FROM T_Account ");
        //        if (strWhere.Trim() != "")
        //        {
        //            strSql.Append(" where " + strWhere);
        //        }
        //        using (SqlDataAdapter da = new SqlDataAdapter(strSql.ToString(), conn))
        //        {
        //            da.Fill(ds);
        //        }
        //    }
        //    return ds;
        //}

        //static public bool Register(string ST_NUM, string OpenID)
        //{
        //    string connString = ConfigurationManager.AppSettings["ConnectionString"];

        //    if (ST_NUM != "" && OpenID != "")
        //    {
        //        using (SqlConnection conn = new SqlConnection(connString))
        //        {
        //            string cmdString = "UPDATE T_Account SET Wechat = '" + OpenID + " where ID = '" + ST_NUM + "'";
        //            conn.Open();
        //            using (SqlCommand cmd = new SqlCommand(cmdString, conn))
        //            {
        //                int flag = (int)cmd.ExecuteNonQuery();
        //                if (flag == 1)
        //                {
        //                    return true;
        //                }
        //                else
        //                {
        //                    return false;
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        ///// <summary>
        ///// 获取学生基本信息
        ///// </summary>
        ///// <param name="strwhere"></param>
        ///// <returns></returns>
        //public DataSet GetList(string strwhere)
        //{
        //    DataSet ds = new DataSet();

        //    using (SqlConnection conn = new SqlConnection(connString))
        //    {
        //        conn.Open();
        //        StringBuilder strSql = new StringBuilder();
        //        strSql.Append("select * ");
        //        strSql.Append(" FROM vw_Student ");
        //        if (strwhere.Trim() != "")
        //        {
        //            strSql.Append(" where " + strwhere);
        //        }
        //        using (SqlDataAdapter da = new SqlDataAdapter(strSql.ToString(), conn))
        //        {
        //            da.Fill(ds);
        //        }
        //    }
        //    return ds;
        //}

        ///// <summary>
        ///// 请假截止时间等信息，参数为学生学号
        ///// </summary>
        ///// <param name="ST_NUM"></param>
        ///// <returns></returns>
        //public DataSet GetTimeEnd(string strwhere)
        //{
        //    DataSet ds = new DataSet();

        //    using (SqlConnection conn = new SqlConnection(connString))
        //    {
        //        conn.Open();
        //        StringBuilder strSql = new StringBuilder();
        //        strSql.Append("select * ");
        //        strSql.Append(" FROM T_Deadline ");
        //        if (strwhere.Trim() != "")
        //        {
        //            strSql.Append(" where " + strwhere);
        //        }
        //        using (SqlDataAdapter da = new SqlDataAdapter(strSql.ToString(), conn))
        //        {
        //            da.Fill(ds);
        //        }
        //    }
        //    return ds;
        //}

        ///// <summary>
        ///// 晚点名时间，从视图中查找
        ///// </summary>
        ///// <param name="strwhere">ST_Num 根据学号查找 </param>
        ///// <returns></returns>
        //public DataSet GetCallTime(string strwhere)
        //{
        //    DataSet ds = new DataSet();

        //    using (SqlConnection conn = new SqlConnection(connString))
        //    {
        //        conn.Open();
        //        StringBuilder strSql = new StringBuilder();
        //        strSql.Append("select Datetime ");
        //        strSql.Append(" FROM vw_ClassBatch ");
        //        if (strwhere.Trim() != "")
        //        {
        //            strSql.Append(" where " + strwhere);
        //        }
        //        using (SqlDataAdapter da = new SqlDataAdapter(strSql.ToString(), conn))
        //        {
        //            da.Fill(ds);
        //        }
        //    }
        //    return ds;
        //}

        ///// <summary>
        ///// 检查是否填写家庭信息
        ///// </summary>
        ///// <param name="ST_NUM"></param>
        ///// <returns></returns>
        //static public bool InfoCheck(UserInfo userInfo)
        //{
        //    if (userInfo.ContactName == "" || userInfo.ContactTel == "")
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}

        ///// <summary>
        ///// 更新个人信息
        ///// </summary>
        ///// <param name="ST_NUM">学号</param>
        ///// <param name="ST_Tel">电话</param>
        ///// <param name="ST_QQ">QQ</param>
        ///// <param name="ST_GuardianName">与家长关系及姓名</param>
        ///// <param name="ST_GuardianNum">联系电话</param>
        //static public void UpdateStuInfo(string ST_NUM, string ST_Tel, string ST_QQ, string ST_GuardianName, string ST_GuardianNum)
        //{
        //    string connString = ConfigurationManager.AppSettings["ConnectionString"];

        //    using (SqlConnection conn = new SqlConnection(connString))
        //    {
        //        conn.Open();
        //        StringBuilder strSql = new StringBuilder();
        //        strSql.Append("UPDATE T_Student ");
        //        strSql.Append(" SET Tel='" + ST_Tel + "',QQ='" + ST_QQ + "',ContactOne='" + ST_GuardianName + "',OneTel='" + ST_GuardianNum + "'");
        //        if (ST_NUM.Trim() != "")
        //        {
        //            strSql.Append(" where ID ='" + ST_NUM + "'");
        //        }
        //        using (SqlCommand cmd = new SqlCommand(strSql.ToString(), conn))
        //        {
        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //}

        ///// <summary>
        ///// 检查密码是否正确
        ///// </summary>
        ///// <param name="strwhere">学号条件</param>
        ///// <param name="Pw">登录密码</param>
        ///// <returns></returns>
        //static public bool CheckPassword(string ST_NUM, string Pw)
        //{
        //    string connString = ConfigurationManager.AppSettings["ConnectionString"];

        //    using (SqlConnection conn = new SqlConnection(connString))
        //    {
        //        conn.Open();
        //        StringBuilder strSql = new StringBuilder();
        //        strSql.Append("select Psd ");
        //        strSql.Append(" FROM T_Account ");
        //        if (ST_NUM.Trim() != "")
        //        {
        //            strSql.Append(" where ID ='" + ST_NUM + "'");
        //        }
        //        using (SqlCommand cmd = new SqlCommand(strSql.ToString(), conn))
        //        {
        //            string UserPw = (string)cmd.ExecuteScalar();
        //            if (UserPw == Pw)
        //            {
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //    }
        //}

        ///// <summary>
        ///// 修改密码
        ///// </summary>
        ///// <param name="ST_NUM">学号</param>
        ///// <param name="Pw">新密码</param>
        //static public void ChangePw(string ST_NUM, string Pw)
        //{
        //    string connString = ConfigurationManager.AppSettings["ConnectionString"];

        //    using (SqlConnection conn = new SqlConnection(connString))
        //    {
        //        conn.Open();
        //        StringBuilder strSql = new StringBuilder();
        //        strSql.Append("UPDATE T_Account ");
        //        strSql.Append(" SET Psd='" + Pw + "'");
        //        if (ST_NUM.Trim() != "")
        //        {
        //            strSql.Append(" where ID ='" + ST_NUM + "'");
        //        }
        //        using (SqlCommand cmd = new SqlCommand(strSql.ToString(), conn))
        //        {
        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //}

        ///// <summary>
        ///// 判断是否需要早晚自习
        ///// </summary>
        ///// <param name="type"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static string getKey(string type, string value)
        //{
        //    string key = "Error";

        //    //早晚自习年级
        //    if (type == "Year")
        //    {
        //        if (value == "2016") key = "1";
        //        if (value != "2016") key = "0";
        //    }

        //    //请假类型名称
        //    if (type == "LeaveType")
        //    {
        //        if (value == "4") key = "短期请假";
        //        if (value == "5") key = "长期请假";
        //        if (value == "6") key = "节假日请假";
        //        if (value == "7") key = "晚点名请假(公假)";
        //        if (value == "8") key = "晚点名请假(事假)";
        //        if (value == "9") key = "晚点名请假(病假)";
        //        if (value == "10") key = "早晚自习请假(公假)";
        //        if (value == "11") key = "早晚自习请假(事假)";
        //        if (value == "12") key = "早晚自习请假(病假)";
        //        if (value == "13") key = "上课请假备案(公假)";
        //        if (value == "14") key = "上课请假备案(事假)";
        //        if (value == "15") key = "上课请假备案(病假)";
        //    }
        //    //返回值
        //    return key;
        //}
    }
}