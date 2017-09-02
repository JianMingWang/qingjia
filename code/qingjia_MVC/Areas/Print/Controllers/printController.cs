using System;
using System.Linq;
using System.Web.Mvc;
using FineUIMvc;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Configuration;
using System.IO;
using qingjia_MVC.Models;
using qingjia_MVC.Controllers;

namespace qingjia_MVC.Areas.Print.Controllers
{

    public class printController : BaseController
    {
        private imaw_qingjiaEntities db = new imaw_qingjiaEntities();

        // GET: Print/print
        public ActionResult Index()
        {
            ShowNotify("加载成功，右键保存到桌面，打印即可。");
            LoadData();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Window1_Close(string Message)
        {
            string script = String.Format("alert('" + Message + "');");
            PageContext.RegisterStartupScript(ActiveWindow.GetHideReference() + script);
            return UIHelper.Result();
        }

        private void LoadData()
        {
            //获取打印的请假单号，将请假单号用ViewBag传到View中，在View中调用Controller中的方法，获得生成图片的二进制流
            string LV_NUM = Request.QueryString["id"].ToString();
            ViewBag.printNum = LV_NUM;
        }

        public FileResult Print_Form(string LV_NUM)
        {
            //获取图片路径
            //图片在计算机上的存储路径，存储在webconfig中
            string picPath = ConfigurationManager.AppSettings["picPath"].ToString();

            int ToWidth = 1241;//经过缩放后 目标图片的宽度
            int ToHeight = 1755;//经过缩放后 目标图片的高度

            //标准尺寸
            Bitmap b = new Bitmap(ToWidth, ToHeight);
            Graphics g = Graphics.FromImage(b);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            var leavelist = from vw_LeaveList in db.vw_LeaveList where (vw_LeaveList.ID == LV_NUM) select vw_LeaveList;
            vw_LeaveList LL = leavelist.ToList().First();

            if (LL.TypeID == 1 || LL.TypeID == 3)
            {
                var T_LL = from T_LeaveList in db.T_LeaveList where (T_LeaveList.ID == LV_NUM) select T_LeaveList;
                T_LeaveList modelT_LL = T_LL.ToList().First();
                if (modelT_LL.PrintTimes == null)
                {
                    modelT_LL.PrintTimes = 1;
                }
                else
                {
                    modelT_LL.PrintTimes = modelT_LL.PrintTimes + 1;
                }


                string lesson = "";
                if (LL.Lesson == "1")
                {
                    lesson = "第一大节";
                }
                if (LL.Lesson == "2")
                {
                    lesson = "第二大节";
                }
                if (LL.Lesson == "3")
                {
                    lesson = "第三大节";
                }
                if (LL.Lesson == "4")
                {
                    lesson = "第四大节";
                }
                if (LL.Lesson == "5")
                {
                    lesson = "第五大节";
                }

                #region 打印假条
                try
                {
                    #region 请假条打印-原先
                    //上课请假
                    if (LL.LeaveType.ToString().Substring(0, 2) == "上课")
                    {
                        //私假
                        if (LL.LeaveType.ToString().Substring(7, 2) == "事假" || LL.LeaveType.ToString().Substring(7, 2) == "病假")
                        {
                            Bitmap Imgc = (Bitmap)Bitmap.FromFile(picPath + @"\res\images\qingjia\stu_class_record_modle.jpg");
                            Bitmap Bitmapc = new Bitmap(Imgc);
                            Graphics C_teachername = Graphics.FromImage(Bitmapc);
                            Graphics C_class = Graphics.FromImage(Bitmapc);
                            Graphics C_name = Graphics.FromImage(Bitmapc);
                            Graphics C_snum = Graphics.FromImage(Bitmapc);
                            Graphics C_num = Graphics.FromImage(Bitmapc);
                            Graphics C_type = Graphics.FromImage(Bitmapc);
                            Graphics C_reason = Graphics.FromImage(Bitmapc);
                            Graphics C_time_go = Graphics.FromImage(Bitmapc);
                            Graphics C_time_now = Graphics.FromImage(Bitmapc);
                            DateTime C_go;
                            DateTime.TryParse(LL.TimeLeave.ToString(), out C_go);
                            int C_goYear = C_go.Year;
                            int C_goMonth = C_go.Month;
                            int C_goDay = C_go.Day;
                            int C_goHours = C_go.Hour;
                            string C_Gotime = C_goYear.ToString().Trim() + "年" + (C_goMonth < 10 ? "0" + C_goMonth.ToString().Trim() + "月" : C_goMonth.ToString().Trim() + "月") + C_goDay.ToString().Trim() + "日";
                            DateTime C_now = System.DateTime.Now;
                            int C_year = C_now.Year;
                            int C_month = C_now.Month;
                            int C_day = C_now.Day;
                            string C_nowtime = C_year.ToString().Trim() + "年" + (C_month < 10 ? "0" + C_month.ToString().Trim() + "月" : C_month.ToString().Trim() + "月") + C_day.ToString().Trim() + "日";
                            System.Drawing.Font font = new System.Drawing.Font("宋体", 30); //字体与大小
                            System.Drawing.Brush brush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
                            C_teachername.DrawString(LL.Teacher.ToString(), font, brush, 372, 544);
                            C_class.DrawString(LL.ST_Class, font, brush, 500, 683);
                            C_name.DrawString(LL.ST_Name, font, brush, 760, 683);
                            C_snum.DrawString(LL.StudentID, font, brush, 1220, 683);//
                            C_num.DrawString(LL.ID, font, brush, 1150, 800);//请假单号
                            C_time_go.DrawString(C_Gotime + lesson, font, brush, 800, 1071);
                            C_type.DrawString(LL.LeaveType.ToString().Substring(7, 2), font, brush, 800, 1190);
                            C_time_now.DrawString(C_nowtime, font, brush, 1500, 2850);
                            Rectangle C_rec = new Rectangle(767, 1327, 1700, 150);//文字区域，画一个矩形用来控制转行
                            C_reason.DrawString("" + LL.Reason.ToString(), font, brush, C_rec);

                            g.DrawImage(Bitmapc, new Rectangle(0, 0, ToWidth, ToHeight), new Rectangle(0, 0, Bitmapc.Width, Bitmapc.Height), GraphicsUnit.Pixel);
                            g.Dispose();
                            b.Save(Server.MapPath(@"~\res\images\qingjia\stu_class_prove.jpg"));

                            string C_filename = Server.MapPath(@"~\res\images\qingjia\stu_class_prove.jpg");
                            //ViewBag.ImageUrl = "../../res/images/qingjia/stu_class_prove.jpg";
                            //return picReturn(Bitmapc);

                            return picSaveReturn(C_filename);
                        }

                        //公假
                        else
                        {
                            Bitmap Imgs = (Bitmap)Bitmap.FromFile(picPath + @"\res\images\qingjia\stu_class_prove_modle.jpg");
                            Bitmap Bitmaps = new Bitmap(Imgs);
                            Graphics S_teachername = Graphics.FromImage(Bitmaps);
                            Graphics S_class = Graphics.FromImage(Bitmaps);
                            Graphics S_name = Graphics.FromImage(Bitmaps);
                            Graphics S_snum = Graphics.FromImage(Bitmaps);
                            Graphics S_num = Graphics.FromImage(Bitmaps);
                            Graphics S_reason = Graphics.FromImage(Bitmaps);
                            Graphics S_time_go = Graphics.FromImage(Bitmaps);
                            Graphics S_time_now = Graphics.FromImage(Bitmaps);
                            DateTime S_go;
                            DateTime.TryParse(LL.TimeLeave.ToString(), out S_go);
                            int S_goYear = S_go.Year;
                            int S_goMonth = S_go.Month;
                            int S_goDay = S_go.Day;
                            int S_goHours = S_go.Hour;
                            string S_Gotime = S_goYear.ToString().Trim() + "年" + (S_goMonth < 10 ? "0" + S_goMonth.ToString().Trim() + "月" : S_goMonth.ToString().Trim() + "月") + S_goDay.ToString().Trim() + "日";
                            DateTime S_now = System.DateTime.Now;
                            int S_year = S_now.Year;
                            int S_month = S_now.Month;
                            int S_day = S_now.Day;
                            string S_nowtime = S_year.ToString().Trim() + "年" + (S_month < 10 ? "0" + S_month.ToString().Trim() + "月" : S_month.ToString().Trim() + "月") + S_day.ToString().Trim() + "日";
                            System.Drawing.Font font = new System.Drawing.Font("宋体", 30); //字体与大小
                            System.Drawing.Brush brush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
                            S_teachername.DrawString(LL.Teacher.ToString(), font, brush, 300, 540);
                            S_class.DrawString(LL.ST_Class.ToString(), font, brush, 380, 670);
                            S_name.DrawString(LL.ST_Name, font, brush, 683, 670);
                            S_snum.DrawString(LL.StudentID.ToString(), font, brush, 1170, 670);
                            S_num.DrawString(LL.ID.ToString(), font, brush, 1120, 800);
                            S_time_go.DrawString(S_Gotime + lesson, font, brush, 787, 1060);
                            S_num.DrawString(LL.ID.ToString(), font, brush, 1514, 2850);
                            S_time_now.DrawString(S_nowtime, font, brush, 1500, 2950);
                            Rectangle S_rec = new Rectangle(787, 1322, 1700, 150);//文字区域，画一个矩形用来控制转行
                            S_reason.DrawString("" + LL.Reason.ToString(), font, brush, S_rec);

                            g.DrawImage(Bitmaps, new Rectangle(0, 0, ToWidth, ToHeight), new Rectangle(0, 0, Bitmaps.Width, Bitmaps.Height), GraphicsUnit.Pixel);
                            g.Dispose();
                            b.Save(Server.MapPath(@"~\res\images\qingjia\stu_class_prove.jpg"));

                            string S_filename = Server.MapPath(@"~\res\images\qingjia\stu_class_prove.jpg");

                            //直接输出二进制流到页面，输出图片过大
                            //return picReturn(Bitmaps);


                            //先保存到本地，在从本地读取成二进制，输出到页面
                            return picSaveReturn(S_filename);
                        }
                    }

                    //离校请假
                    else
                    {
                        Bitmap img;
                        img = (Bitmap)Bitmap.FromFile(picPath + @"\res\images\qingjia\stu_Leaveform_modle.jpg");
                        //此处使用png格式 会降低图片大小，约为4兆左右，加载时间约为40秒，如果可以做出进度条效果，可以考虑使用，否则用户体验不佳
                        //img = (Bitmap)Bitmap.FromFile(picPath + @"\res\images\qingjia\stu_Leaveform_modle_png.png");
                        if (LL.LeaveType != "长期请假")
                        {
                            if (LL.LeaveType == "节假日请假")//如果是节假日请假
                            {
                                DateTime time_go = Convert.ToDateTime(LL.TimeLeave);
                                DateTime time_back = Convert.ToDateTime(LL.TimeBack);
                                TimeSpan time_days = time_back - time_go;
                                int days = time_days.Days;
                                if (days >= 3)//日期大于三天，需要书记签名的假条
                                {
                                    img = (Bitmap)Bitmap.FromFile(picPath + @"\res\images\qingjia\stu_Leaveform_modle_l.jpg");

                                }
                                else//小于三天则不用
                                {
                                    img = (Bitmap)Bitmap.FromFile(picPath + @"\res\images\qingjia\stu_Leaveform_modle.jpg");
                                }
                            }
                            else
                            {
                                img = (Bitmap)Bitmap.FromFile(picPath + @"\res\images\qingjia\stu_Leaveform_modle.jpg");
                            }
                        }
                        else
                        {

                            if (LL.StateLeave == "1")//书记已经同意了
                            {
                                img = (Bitmap)Bitmap.FromFile(picPath + @"\res\images\qingjia\stu_Leaveform_modle_l.jpg");
                                //img = (Bitmap)Bitmap.FromFile(picPath + @"\res\images\qingjia\stu_Leaveform_modle_png.png");
                            }
                            else
                            {
                                img = (Bitmap)Bitmap.FromFile(picPath + @"\res\images\qingjia\stu_Leaveform_modle.jpg");//如果书记还没审核，是不能生成带有书记的印章的假条的
                            }

                        }
                        Bitmap bitmap = new Bitmap(img);
                        Graphics gno = Graphics.FromImage(bitmap);
                        Graphics gname = Graphics.FromImage(bitmap);
                        Graphics gclass = Graphics.FromImage(bitmap);
                        Graphics gsno = Graphics.FromImage(bitmap);
                        Graphics gdorm = Graphics.FromImage(bitmap);
                        Graphics greason = Graphics.FromImage(bitmap);
                        Graphics gtime_go = Graphics.FromImage(bitmap);
                        Graphics gtime_back = Graphics.FromImage(bitmap);
                        Graphics gtime = Graphics.FromImage(bitmap);
                        Graphics gptel = Graphics.FromImage(bitmap);
                        Graphics gparentname = Graphics.FromImage(bitmap);
                        Graphics gtel = Graphics.FromImage(bitmap);
                        Graphics gnowtime_ask = Graphics.FromImage(bitmap);
                        Graphics gnowtime = Graphics.FromImage(bitmap);
                        Graphics gteachername = Graphics.FromImage(bitmap);
                        Graphics gtimes = Graphics.FromImage(bitmap);


                        DateTime gostr, backstr;
                        DateTime.TryParse(LL.TimeLeave.ToString(), out gostr);
                        DateTime.TryParse(LL.TimeBack.ToString(), out backstr);
                        int goyear = gostr.Year;
                        int gomonth = gostr.Month;
                        int goday = gostr.Day;
                        int gohours = gostr.Hour;
                        string gotime = goyear.ToString().Trim() + "年" + (gomonth < 10 ? "0" + gomonth.ToString().Trim() + "月" : gomonth.ToString().Trim() + "月") + goday.ToString().Trim() + "日" + gohours.ToString().Trim() + "时";
                        int backyear = backstr.Year;
                        int backmonth = backstr.Month;
                        int backday = backstr.Day;
                        int backhours = backstr.Hour;
                        string backtime = backyear.ToString().Trim() + "年" + (backmonth < 10 ? "0" + backmonth.ToString().Trim() + "月" : backmonth.ToString().Trim() + "月") + backday.ToString().Trim() + "日" + backhours.ToString().Trim() + "时";
                        TimeSpan t_span = backstr - gostr;
                        int timeday = t_span.Days;
                        int timehours = t_span.Hours;
                        string time = timeday.ToString().Trim() + "天" + timehours.ToString().Trim();
                        //当前时间
                        DateTime str = System.DateTime.Now;
                        int year = str.Year;
                        int month = str.Month;
                        int day = str.Day;
                        string nowtime = year.ToString().Trim() + "年" + (month < 10 ? "0" + month.ToString().Trim() + "月" : month.ToString().Trim() + "月") + day.ToString().Trim() + "日";
                        //请假时间
                        string nowtime_ask = "20" + LL.ID.ToString().Substring(0, 2) + "年" + LL.ID.ToString().Substring(2, 2) + "月" + LL.ID.ToString().Substring(4, 2) + "日";
                        System.Drawing.Font font = new System.Drawing.Font("宋体", 27); //字体与大小
                        System.Drawing.Brush brush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);

                        //学生留存
                        gno.DrawString(LL.ID.ToString(), font, brush, 395, 250); //写字，最后两个参数表示位置
                        gname.DrawString(LL.ST_Name.ToString(), font, brush, 422, 312);
                        gclass.DrawString(LL.ST_Class.ToString(), font, brush, 976, 312);
                        gsno.DrawString(LL.StudentID.ToString(), font, brush, 1397, 312);
                        gdorm.DrawString(LL.ST_Dor.ToString(), font, brush, 2039, 312);
                        greason.DrawString(LL.Reason.ToString(), font, brush, 685, 440);
                        gtime_go.DrawString(gotime, font, brush, 685, 495);
                        gtime_back.DrawString(backtime, font, brush, 1318, 495);
                        gtime.DrawString(time, font, brush, 1997, 495);
                        gptel.DrawString(LL.OneTel.ToString(), font, brush, 648, 610);
                        gparentname.DrawString(LL.ContactOne.ToString(), font, brush, 1281, 610);
                        gtel.DrawString(LL.ST_Tel.ToString(), font, brush, 647, 670);
                        gnowtime_ask.DrawString(nowtime_ask, font, brush, 1927, 670);
                        gteachername.DrawString(LL.ST_Teacher.ToString(), font, brush, 817, 930);
                        gnowtime.DrawString(nowtime, font, brush, 1927, 930);
                        gtimes.DrawString(modelT_LL.PrintTimes.ToString(), font, brush, 2135, 245);
                        //班级留存
                        gno.DrawString(LL.ID.ToString(), font, brush, 395, 1360); //写字，最后两个参数表示位置
                        gname.DrawString(LL.ST_Name.ToString(), font, brush, 422, 1427);
                        gclass.DrawString(LL.ST_Class.ToString(), font, brush, 976, 1427);
                        gsno.DrawString(LL.StudentID.ToString(), font, brush, 1397, 1427);
                        gdorm.DrawString(LL.ST_Dor.ToString(), font, brush, 2039, 1427);
                        greason.DrawString(LL.Reason.ToString(), font, brush, 685, 1550);
                        gtime_go.DrawString(gotime, font, brush, 685, 1610);
                        gtime_back.DrawString(backtime, font, brush, 1318, 1610);
                        gtime.DrawString(time, font, brush, 1970, 1610);
                        gptel.DrawString(LL.OneTel.ToString(), font, brush, 648, 1722);
                        gparentname.DrawString(LL.ContactOne.ToString(), font, brush, 1281, 1722);
                        gtel.DrawString(LL.ST_Tel.ToString(), font, brush, 647, 1784);
                        gnowtime_ask.DrawString(nowtime_ask, font, brush, 1927, 1784);
                        gteachername.DrawString(LL.ST_Teacher.ToString(), font, brush, 817, 2035);
                        gnowtime.DrawString(nowtime, font, brush, 1927, 2035);
                        gtimes.DrawString(modelT_LL.PrintTimes.ToString(), font, brush, 2135, 1356);
                        //请假联
                        gno.DrawString(LL.ID.ToString(), font, brush, 395, 2530); //写字，最后两个参数表示位置
                        gname.DrawString(LL.ST_Name.ToString(), font, brush, 431, 2594);
                        gclass.DrawString(LL.ST_Class.ToString(), font, brush, 976, 2594);
                        gsno.DrawString(LL.StudentID.ToString(), font, brush, 1397, 2594);
                        gdorm.DrawString(LL.ST_Dor.ToString(), font, brush, 2039, 2594);
                        greason.DrawString(LL.Reason.ToString(), font, brush, 685, 2720);
                        gtime_go.DrawString(gotime, font, brush, 685, 2778);
                        gtime_back.DrawString(backtime, font, brush, 1318, 2778);
                        gtime.DrawString(time, font, brush, 1997, 2780);
                        gnowtime_ask.DrawString(nowtime_ask, font, brush, 1927, 2945);
                        gteachername.DrawString(LL.ST_Teacher.ToString(), font, brush, 817, 3195);
                        gnowtime.DrawString(nowtime, font, brush, 1927, 3195);
                        gtimes.DrawString(modelT_LL.PrintTimes.ToString(), font, brush, 2135, 2528);
                        StringFormat sf = new StringFormat();
                        sf.LineAlignment = StringAlignment.Center;

                        img.Dispose();
                        if (LL.LeaveType != "长期请假")
                        {
                            g.DrawImage(bitmap, new Rectangle(0, 0, ToWidth, ToHeight), new Rectangle(0, 0, bitmap.Width, bitmap.Height), GraphicsUnit.Pixel);
                            g.Dispose();
                            b.Save(Server.MapPath(@"~\res\images\qingjia\stu_Leaveform.jpg"));
                            string filename = Server.MapPath(@"~\res\images\qingjia\stu_Leaveform.jpg");

                            return picSaveReturn(filename);
                        }
                        else
                        {
                            g.DrawImage(bitmap, new Rectangle(0, 0, ToWidth, ToHeight), new Rectangle(0, 0, bitmap.Width, bitmap.Height), GraphicsUnit.Pixel);
                            g.Dispose();
                            b.Save(Server.MapPath(@"~\res\images\qingjia\stu_Leaveform_l.jpg"));
                            string filename = Server.MapPath(@"~\res\images\qingjia\stu_Leaveform_l.jpg");

                            return picSaveReturn(filename);
                        }
                    }
                    #endregion

                }
                catch (Exception ex)
                {
                    //提示  打印请假条错误
                    Window1_Close("出现未知错误，请联系管理员！");
                    return null;
                }
                #endregion
            }
            else
            {
                //无需打印假条
                Window1_Close("无需打印假条！");
                return null;
            }
        }

        //将图片转换为二进制流，不保存在本地
        private FileResult picReturn(Bitmap bitmap)
        {
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            byte[] byData = ms.GetBuffer();
            ms.Close();
            return File(byData, "image/png");
        }

        public FileResult picSaveReturn(string filepath)
        {
            byte[] photo = new byte[0];
            FileStream file = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            photo = new byte[file.Length];
            file.Read(photo, 0, photo.Length);
            file.Close();
            return File(photo, "image/jpeg");
        }

        //图片下载代码
        //public void download(string path)
        //{
        //    try
        //    {
        //        string FullFileName = Server.MapPath(path); //FileName--要下载的文件名 
        //        System.IO.FileInfo DownloadFile = new System.IO.FileInfo(FullFileName);
        //        if (DownloadFile.Exists)
        //        {
        //            Response.Clear();
        //            Response.ClearHeaders();
        //            Response.Buffer = false;
        //            Response.ContentType = "application/octet-stream";
        //            Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(DownloadFile.Name, System.Text.Encoding.ASCII));
        //            Response.AppendHeader("Content-Length", DownloadFile.Length.ToString());
        //            Response.WriteFile(DownloadFile.FullName);
        //            Response.Flush();
        //            Response.End();
        //        }
        //        else
        //        {
        //            //文件不存在 
        //        }
        //    }
        //    catch
        //    {
        //        //文件不存在
        //    }
        //}
    }
}