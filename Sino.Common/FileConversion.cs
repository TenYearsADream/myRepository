using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Office.Core;
using System.Web;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;

namespace SinoSoft.Common
{
     /// <summary>
    /// 文件转换类
    /// 1.office/图片 to swf
    /// 2.其他转换
    /// </summary>
    public class FileConversion
    {
        FileConversion() { }
        /// <summary>  
        /// 把Word文件转换成为PDF格式文件  
        /// </summary>  
        /// <param name="sourcePath">源文件路径</param>  
        /// <param name="targetPath">目标文件路径</param>   
        /// <returns>true=转换成功</returns>  
        public static bool WordToPDF(string sourcePath, string targetPath)
        {
            targetPath = Regex.Replace(targetPath, " ", "", RegexOptions.IgnoreCase);//去除字符串中间的空格
            //bool result = false;
            Microsoft.Office.Interop.Word.WdExportFormat exportFormat = Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF;
            Microsoft.Office.Interop.Word.ApplicationClass application = null;

            Microsoft.Office.Interop.Word.Document document = null;
            try
            {
                bool TimeOver = false;//转换时间到了
                System.Timers.Timer timer = new System.Timers.Timer(30000);
                timer.AutoReset = false;//只循环一次
                timer.Elapsed += delegate(object sender, System.Timers.ElapsedEventArgs e)
                {
                    TimeOver = true;
                };
                timer.Start();
                Thread th = new Thread(new ThreadStart(delegate()
                {
                    try
                    {
                        //document = application.Documents.Open(sourcePath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                        application = new Microsoft.Office.Interop.Word.ApplicationClass();
                        application.Visible = false;
                        document = application.Documents.Open(sourcePath);
                        document.SaveAs();
                        document.ExportAsFixedFormat(targetPath, exportFormat);
                    }
                    catch
                    {

                    }
                }));
                th.IsBackground = true;//后台线程
                th.Start();
                //如果转换没有成功就一直循环
                while (th.ThreadState != System.Threading.ThreadState.Stopped)
                {
                    //如果一直没有转换成功，但是最大的转换时间到了也直接跳出While循环
                    if (TimeOver)
                    {
                        break;
                    }
                }
                if (th.ThreadState != System.Threading.ThreadState.Stopped)
                {
                    th.Abort();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);           
                //result = false;
            }
            finally
            {
                if (document != null)
                {
                    document.Close();
                    document = null;
                }
                if (application != null)
                {
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();                
            }
            //判断文件是否转换成功
            if (File.Exists(targetPath))
                return true;
            else return false;
        }

        /// <summary>  
        /// 把Microsoft.Office.Interop.Excel文件转换成PDF格式文件  
        /// </summary>  
        /// <param name="sourcePath">源文件路径</param>  
        /// <param name="targetPath">目标文件路径</param>   
        /// <returns>true=转换成功</returns>  
        public static bool ExcelToPDF(string sourcePath, string targetPath)
        {
            targetPath = Regex.Replace(targetPath, " ", "", RegexOptions.IgnoreCase);//去除字符串中间的空格
            bool result = false;
            Microsoft.Office.Interop.Excel.XlFixedFormatType targetType = Microsoft.Office.Interop.Excel.XlFixedFormatType.xlTypePDF;
            object missing = Type.Missing;
            Microsoft.Office.Interop.Excel.ApplicationClass application = null;
            Microsoft.Office.Interop.Excel.Workbook workBook = null;
            try
            {
                bool TimeOver = false;//转换时间到了
                System.Timers.Timer timer = new System.Timers.Timer(30000);
                timer.AutoReset = false;//只循环一次
                timer.Elapsed += delegate(object sender, System.Timers.ElapsedEventArgs e)
                {
                    TimeOver = true;
                };
                timer.Start();
                Thread th = new Thread(new ThreadStart(delegate()
                {
                    try
                    {
                        application = new Microsoft.Office.Interop.Excel.ApplicationClass();
                        application.Visible = false;
                        workBook = application.Workbooks.Open(sourcePath);
                        workBook.Application.DisplayAlerts = false;
                        workBook.SaveAs();
                        workBook.ExportAsFixedFormat(targetType, targetPath);
                    }
                    catch
                    {

                    }
                }));
                th.IsBackground = true;//后台线程
                th.Start();
                //如果转换没有成功就一直循环
                while (th.ThreadState != System.Threading.ThreadState.Stopped)
                {
                    //如果一直没有转换成功，但是最大的转换时间到了也直接跳出While循环
                    if (TimeOver)
                    {
                        break;
                    }
                }
                if (th.ThreadState != System.Threading.ThreadState.Stopped)
                {
                    th.Abort();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                result = false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close(true, missing, missing);
                    workBook = null;
                }
                if (application != null)
                {
                    application.Quit();
                    application = null;
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            //判断文件是否转换成功
            if (File.Exists(targetPath))
                return true;
            else return false;
        }

        /// <summary>  
        /// 把PowerPoint文件转换成PDF格式文件  
        /// </summary>  
        /// <param name="sourcePath">源文件路径</param>  
        /// <param name="targetPath">目标文件路径</param>   
        /// <returns>true=转换成功</returns>  
        public static bool PowerPointToPDF(string sourcePath, string targetPath)
        {
            targetPath = Regex.Replace(targetPath, " ", "", RegexOptions.IgnoreCase);//去除字符串中间的空格
            bool result;
            Microsoft.Office.Interop.PowerPoint.PpSaveAsFileType targetFileType = Microsoft.Office.Interop.PowerPoint.PpSaveAsFileType.ppSaveAsPDF;
            object missing = Type.Missing;
            Microsoft.Office.Interop.PowerPoint.ApplicationClass application = null;
            Microsoft.Office.Interop.PowerPoint.Presentation persentation = null;
            try
            {
                bool TimeOver = false;//转换时间到了
                System.Timers.Timer timer = new System.Timers.Timer(30000);
                timer.AutoReset = false;//只循环一次
                timer.Elapsed += delegate(object sender, System.Timers.ElapsedEventArgs e)
                {
                    TimeOver = true;
                };
                timer.Start();
                Thread th = new Thread(new ThreadStart(delegate()
                {
                    try
                    {
                        application = new Microsoft.Office.Interop.PowerPoint.ApplicationClass();
                        //application.Visible = MsoTriState.msoFalse;  
                        persentation = application.Presentations.Open(sourcePath, MsoTriState.msoTrue, MsoTriState.msoFalse, MsoTriState.msoFalse);
                        persentation.SaveAs(targetPath, targetFileType, Microsoft.Office.Core.MsoTriState.msoTrue);
                    }
                    catch
                    {

                    }
                }));
                th.IsBackground = true;//后台线程
                th.Start();
                //如果转换没有成功就一直循环
                while (th.ThreadState != System.Threading.ThreadState.Stopped)
                {
                    //如果一直没有转换成功，但是最大的转换时间到了也直接跳出While循环
                    if (TimeOver)
                    {
                        break;
                    }
                }
                if (th.ThreadState != System.Threading.ThreadState.Stopped)
                {
                    th.Abort();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                result = false;
            }
            finally
            {
                if (persentation != null)
                {
                    persentation.Close();
                    persentation = null;
                }
                if (application != null)
                {
                    application.Quit();
                    application = null;
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            //判断文件是否转换成功
            if (File.Exists(targetPath))
                return true;
            else return false;
        }

        /*/// <summary>
        /// 把PDF文件转化为SWF文件
        /// </summary>
        /// <param name="toolPah">pdf2swf工具路径</param>
        /// <param name="sourcePath">源文件路径</param>
        /// <param name="targetPath">目标文件路径</param>
        /// <returns>true=转化成功</returns>
        public static bool PDFToSWF(string toolPah, string sourcePath, string targetPath)
        {
            Process pc = new Process();
            bool returnValue = true;

            string cmd = toolPah;
            string args = " -t " + sourcePath + " -s flashversion=9 -o " + targetPath;
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo(cmd, args);
                psi.WindowStyle = ProcessWindowStyle.Hidden;
                pc.StartInfo = psi;
                pc.Start();
                pc.WaitForExit();
            }
            catch (Exception ex)
            {
                returnValue = false;
                throw new Exception(ex.Message);
            }
            finally
            {
                pc.Close();
                pc.Dispose();
            }
            return returnValue;
        }*/

        /*/// <summary>
        /// png、jpg和jpeg文件的转化
        /// </summary>
        /// <param name="toolPah"></param>
        /// <param name="sourcePath"></param>
        /// <param name="targetPath"></param>
        /// <returns></returns>
        public static bool PicturesToSwf(string toolPah, string sourcePath, string targetPath)
        {
            Process pc = new Process();
            bool returnValue = true;

            string cmd = toolPah;
            string args = " " + sourcePath + " -o " + targetPath + " -T 9";
            //如果是多个图片转化为swf 格式为 ..jpeg2swf.exe C:\1.jpg C:\2.jpg -o C:\swf1.swf
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo(cmd, args);
                psi.WindowStyle = ProcessWindowStyle.Hidden;
                pc.StartInfo = psi;
                pc.Start();
                pc.WaitForExit();
            }
            catch (Exception ex)
            {
                returnValue = false;
                throw new Exception(ex.Message);
            }
            finally
            {
                pc.Close();
                pc.Dispose();
            }
            return returnValue;
        }

        /// <summary>
        /// Gif文件转化为swf
        /// </summary>
        /// <param name="toolPah"></param>
        /// <param name="sourcePath"></param>
        /// <param name="targetPath"></param>
        /// <returns></returns>
        public static bool GifPicturesToSwf(string toolPah, string sourcePath, string targetPath)
        {
            Process pc = new Process();
            bool returnValue = true;

            string cmd = toolPah;
            string args = " " + sourcePath + " -o " + targetPath;
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo(cmd, args);
                psi.WindowStyle = ProcessWindowStyle.Hidden;
                pc.StartInfo = psi;
                pc.Start();
                pc.WaitForExit();
            }
            catch (Exception ex)
            {
                returnValue = false;
                throw new Exception(ex.Message);
            }
            finally
            {
                pc.Close();
                pc.Dispose();
            }
            return returnValue;
        }*/

        #region 获取SWF转换工具路径
        public static string pdf2swfToolPath = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"swftool\pdf2swf.exe";
            //HttpContext.Current.Server.MapPath("~/swftool/pdf2swf.exe");
        public static string png2swfToolPath = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"swftool\png2swf.exe";
            //HttpContext.Current.Server.MapPath("~/swftool/png2swf.exe");
        public static string jpeg2swfToolPath = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"swftool\jpeg2swf.exe";
            //HttpContext.Current.Server.MapPath("~/swftool/jpeg2swf.exe");
        public static string gif2swfToolPath = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"swftool\gif2swf.exe";
            //HttpContext.Current.Server.MapPath("~/swftool/gif2swf.exe");
        #endregion

        //#region 获取文件存储路径
        //public static string PictureFilePath = HttpContext.Current.Server.MapPath("~/originalfile/pic/");
        //public static string OfficeFilePath = HttpContext.Current.Server.MapPath("~/originalfile/office/");
        //public static string PdfFilePath = HttpContext.Current.Server.MapPath("~/originalfile/pdf/");
        //public static string SWFFilePath = HttpContext.Current.Server.MapPath("~/originalfile/swf/");
        //#endregion
    }
}
