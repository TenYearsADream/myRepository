using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SinoSoft.Common
{
    public class Tools
    {
        /// <summary>
        /// IE版本
        /// </summary>
        public enum IEVer
        {
            /// <summary>
            /// IE6
            /// </summary>
            IE6,
            /// <summary>
            /// IE7
            /// </summary>
            IE7
        }
        /// <summary>
        /// 根据IE版本和指定的高度来计算出在此IE版本下的高度
        /// </summary>
        /// <param name="value">高度值</param>
        /// <param name="ver">IE版本</param>
        /// <returns></returns>
        public static int dlgHeight(int value, IEVer ver)
        {
            HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;
            if ((browser.Version.IndexOf("6.") > -1) && (ver == IEVer.IE7))
            {
                value += 0x2d;
            }
            if ((browser.Version.IndexOf("7.") > -1) && (ver == IEVer.IE6))
            {
                value -= 0x2d;
            }
            return value;
        }
        /// <summary>
        /// 根据IE版本和指定的宽度来计算出在此IE版本下的宽度
        /// </summary>
        /// <param name="value">宽度值</param>
        /// <param name="ver">IE版本</param>
        /// <returns></returns>
        public static int dlgWidth(int value, IEVer ver)
        {
            HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;
            if ((browser.Version.IndexOf("6.") > -1) && (ver == IEVer.IE7))
            {
                value += 8;
            }
            if ((browser.Version.IndexOf("7.") > -1) && (ver == IEVer.IE6))
            {
                value -= 8;
            }
            return value;
        }



        /// <summary>
        /// 根据文件后缀名获取文件图标
        /// </summary>
        /// <param name="suffix"></param>
        /// <returns></returns>
        public static string GetFileBigIcon(string suffix)
        {
            suffix = suffix.Trim('.').ToLower();
            switch (suffix.ToLower())
            {
                case "doc":
                    return "Images/bigfiles/doc.ico";
                case "docx":
                    return "Images/bigfiles/dot.ico";
                case "xls":
                    return "Images/bigfiles/xls.ico";
                case "xlsx":
                    return "Images/bigfiles/xlt.ico";
                case "ppt":
                    return "Images/bigfiles/ppt.ico";
                case "pptx":
                    return "Images/bigfiles/pot.ico";
                case "pdf":
                    return "Images/bigfiles/pdf.png";
                case "txt":
                    return "Images/bigfiles/txt.ico";
                case "bmp":
                    return "Images/bigfiles/no-books.png";
                case "gif":
                    return "Images/bigfiles/no-books.png";
                case "jpg":
                    return "Images/bigfiles/no-books.png";
                case "jpeg":
                    return "Images/bigfiles/no-books.png";
                case "png":
                    return "Images/bigfiles/no-books.png";
                case "zip":
                    return "Images/bigfiles/no-books.png";
                case "rar":
                    return "Images/bigfiles/no-books.png";
                case "xml":
                    return "Images/bigfiles/no-books.png";
                case "csv":
                    return "Images/bigfiles/csv.ico";
                case "html":
                    return "Images/bigfiles/mhtml.ico";
                case "mdb":
                    return "Images/bigfiles/mdb.ico";
                case "flv":
                    return "Images/bigfiles/flv.ico";
                case "mp4":
                    return "Images/bigfiles/mp4.ico";
                case "rmvb":
                    return "Images/bigfiles/rmvb.ico";
                default:
                    return "Images/bigfiles/no-books.png";
            }
        }
        /// <summary>
        /// 根据文件后缀名获取文件图标
        /// </summary>
        /// <param name="suffix"></param>
        /// <returns></returns>
        public static string GetFileIcon(string suffix)
        {
            suffix = suffix.Trim('.').ToLower();
            switch (suffix.ToLower())
            {
                case "doc":
                    return "Images/files/icdoc.gif";
                case "docx":
                    return "Images/files/icdoc.gif";
                case "xls":
                    return "Images/files/icxls.gif";
                case "xlsx":
                    return "Images/files/icxls.gif";
                case "ppt":
                    return "Images/files/ppt.gif";
                case "pptx":
                    return "Images/files/pptx.gif";
                case "pdf":
                    return "Images/files/pdf.gif";
                case "txt":
                    return "Images/files/txt.gif";
                case "bmp":
                    return "Images/files/bmp.gif";
                case "gif":
                    return "Images/files/gif.gif";
                case "jpg":
                    return "Images/files/jpg.gif";
                case "jpeg":
                    return "Images/files/jpg.gif";
                case "png":
                    return "Images/files/png.gif";
                case "zip":
                    return "Images/files/zip.gif";
                case "rar":
                    return "Images/files/rar.gif";
                case "xml":
                    return "Images/files/xml.gif";
                case "flv":
                    return "Images/files/flv.gif";
                case "mp4":
                    return "Images/files/mp4.gif";
                case "rmvb":
                    return "Images/files/rmvb.gif";
                case "file":
                    return "Images/files/folder.gif";
                default:
                    return "Images/files/page.png";
            }
        }

        /// <summary>
        /// 根据文件后缀名获取文件图标
        /// </summary>
        /// <param name="suffix"></param>
        /// <returns></returns>
        public static string GetFileType(string suffix)
        {
            suffix = suffix.Trim('.').ToLower();
            switch (suffix.ToLower())
            {
                case "doc":
                    return "Word文档";
                case "docx":
                    return "Word文档";
                case "xls":
                    return "电子表格文件";
                case "xlsx":
                    return "电子表格文件";
                case "ppt":
                    return "演示文稿";
                case "pptx":
                    return "演示文稿";
                case "pdf":
                    return "PDF文件";
                case "txt":
                    return "文本文件";
                case "bmp":
                    return "BMP图像";
                case "gif":
                    return "GIF图像";
                case "jpg":
                    return "JPG图像";
                case "jpeg":
                    return "JPEG图像";
                case "png":
                    return "PNG图像";
                case "zip":
                    return "ZIP压缩文件";
                case "rar":
                    return "RAR压缩文件";
                case "xml":
                    return "XML文件";
                case "flv":
                    return "媒体文件(.flv)";
                case "mp4":
                    return "媒体文件(.mp4)";
                case "rmvb":
                    return "媒体文件(.rmvb)";
                case "file":
                    return "文件夹";
                default:
                    return "未知文件";
            }
        }
    }
}
