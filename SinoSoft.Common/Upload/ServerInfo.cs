namespace SinoSoft.Common.Upload
{
    using System;
    using System.Collections;
    using System.Data;
    using System.IO;
    using System.Management;
    using System.Web;
    using System.Web.SessionState;

    /// <summary>
    /// 服务器变量信息
    /// </summary>
    public class ServerInfo
    {
        public static bool CheckObj(string str)
        {
            try
            {
                object obj = HttpContext.Current.Server.CreateObject(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 返回指定的Application对象
        /// </summary>
        /// <param name="InfoName">对象名称</param>
        /// <returns>返回指定的Application对象</returns>
        public static object GetApplicationInfo(object InfoName)
        {
            HttpApplicationState application = HttpContext.Current.Application;
            if ((application != null) && (application["_AppInfo"] != null))
            {
                if (InfoName == null)
                {
                    return null;
                }
                Hashtable hashtable = application["_AppInfo"] as Hashtable;
                if ((hashtable != null) && hashtable.ContainsKey(InfoName))
                {
                    return hashtable[InfoName];
                }
            }
            return null;
        }

        /// <summary>
        /// 返回指定的Session对象
        /// </summary>
        /// <param name="InfoName">Session对象名称</param>
        /// <returns></returns>
        public static object GetSessionInfo(object InfoName)
        {
            return GetSessionInfo(InfoName, false);
        }
        /// <summary>
        /// 返回指定的Session对象
        /// </summary>
        /// <param name="InfoName">Session对象名称</param>
        /// <param name="isShow">是否显示吊线信息</param>
        /// <returns></returns>
        public static object GetSessionInfo(object InfoName, bool isShow)
        {
            HttpSessionState session = HttpContext.Current.Session;
            if (((session == null) || (session["_SessionInfo"] == null)) && isShow)
            {
                HttpContext.Current.Response.Write("<script language=javascript>alert('会话丢失，请重新操作！');</script>");
                HttpContext.Current.Response.End();
                return "";
            }
            if (((session != null) && (InfoName != null)) && (session["_SessionInfo"] != null))
            {
                Hashtable hashtable = session["_SessionInfo"] as Hashtable;
                if ((hashtable != null) && hashtable.ContainsKey(InfoName))
                {
                    return hashtable[InfoName];
                }
            }
            return "";
        }

        /// <summary>
        /// 返回指定的Session对象
        /// </summary>
        /// <param name="SE">服务器会话信息</param>
        /// <param name="InfoName">Session对象名称</param>
        /// <returns></returns>
        public static object GetSessionInfo(HttpSessionState SE, object InfoName)
        {
            if ((SE == null) || (SE["_SessionInfo"] == null))
            {
                HttpContext.Current.Response.Write("<script language=javascript>alert('会话丢失，请重新操作！');</script>");
                HttpContext.Current.Response.End();
                return null;
            }
            if (((SE != null) && (InfoName != null)) && (SE["_SessionInfo"] != null))
            {
                Hashtable hashtable = SE["_SessionInfo"] as Hashtable;
                if ((hashtable != null) && hashtable.ContainsKey(InfoName))
                {
                    return hashtable[InfoName];
                }
            }
            return null;
        }

        /// <summary>
        /// 设置制定Application对象信息
        /// </summary>
        /// <param name="InfoName">Application对象名称</param>
        /// <param name="InfoValue">Application对象的值</param>
        /// <returns></returns>
        public static bool SetApplicationInfo(object InfoName, object InfoValue)
        {
            HttpApplicationState application = HttpContext.Current.Application;
            if ((application == null) || (InfoName == null))
            {
                return false;
            }
            if (application["_AppInfo"] == null)
            {
                application["_AppInfo"] = new Hashtable();
            }
            Hashtable hashtable = application["_AppInfo"] as Hashtable;
            if (hashtable == null)
            {
                application.Remove("_AppInfo");
                application.Add("_AppInfo", new Hashtable());
            }
            if (hashtable.ContainsKey(InfoName))
            {
                hashtable[InfoName] = InfoValue;
            }
            else
            {
                hashtable.Add(InfoName, InfoValue);
            }
            return true;
        }

        /// <summary>
        /// 设置指定Session对象信息
        /// </summary>
        /// <param name="InfoName">Session对象名称</param>
        /// <param name="InfoValue">Session对象的值</param>
        /// <returns></returns>
        public static bool SetSessionInfo(object InfoName, object InfoValue)
        {
            return SetSessionInfo(HttpContext.Current.Session, InfoName, InfoValue);
        }
        /// <summary>
        /// 设置指定Session对象信息
        /// </summary>
        /// <param name="SE">服务器会话信息</param>
        /// <param name="InfoName">Session对象名称</param>
        /// <param name="InfoValue">Session对象的值</param>
        /// <returns></returns>
        public static bool SetSessionInfo(HttpSessionState SE, object InfoName, object InfoValue)
        {
            if ((SE == null) || (InfoName == null))
            {
                return false;
            }
            if (SE["_SessionInfo"] == null)
            {
                SE["_SessionInfo"] = new Hashtable();
            }
            Hashtable hashtable = SE["_SessionInfo"] as Hashtable;
            if (hashtable == null)
            {
                SE.Remove("_SessionInfo");
                SE.Add("_SessionInfo", new Hashtable());
            }
            if (hashtable.ContainsKey(InfoName))
            {
                hashtable[InfoName] = InfoValue;
            }
            else
            {
                hashtable.Add(InfoName, InfoValue);
            }
            return true;
        }

        /// <summary>
        /// 删除指定的临时文件
        /// </summary>
        /// <param name="FileName"></param>
        public static void Temp_DelFile(string FileName)
        {
            if ((FileName != "") && (FileName != null))
            {
                string fileName = Temp_Path() + FileName;
                try
                {
                    new FileInfo(fileName).Delete();
                }
                catch
                {
                }
            }
        }
        /// <summary>
        /// 获取临时文件夹的目录
        /// </summary>
        /// <returns></returns>
        public static string Temp_Path()
        {
            string applicationPath = HttpContext.Current.Request.ApplicationPath;
            if (applicationPath == "")
            {
                applicationPath = "/";
            }
            if (applicationPath.EndsWith("/") && (applicationPath.Length > 1))
            {
                applicationPath = applicationPath.Remove(applicationPath.Length - 1, 1);
            }
            string path = (HttpContext.Current.Server.MapPath(applicationPath) + @"\Temp\").Replace(@"\\", @"\");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                return path;
            }
            DirectoryInfo info = new DirectoryInfo(path);
            foreach (FileInfo file in info.GetFiles())
            {
                if (Convert.ToDateTime(file.LastWriteTime).ToLocalTime().Day != DateTime.Now.Day)
                {
                    try
                    {
                        file.Delete();
                    }
                    catch
                    {
                    }
                }
            }
            return path;
        }
        /// <summary>
        /// 获取临时文件夹的http目录
        /// </summary>
        /// <returns></returns>
        public static string Temp_WebPath()
        {
            return (VirtualPath() + "/Temp/").Replace("//", "/");
        }
        /// <summary>
        /// 获取虚拟路径
        /// </summary>
        /// <returns></returns>
        public static string VirtualPath()
        {
            return VirtualPath(false);
        }
        /// <summary>
        /// 获取虚拟路径
        /// </summary>
        /// <param name="Http">是否添加Http</param>
        /// <returns></returns>
        public static string VirtualPath(bool Http)
        {
            string applicationPath = HttpContext.Current.Request.ApplicationPath;
            if (Http)
            {
                switch (applicationPath)
                {
                    case "":
                    case "/":
                        applicationPath = "http://";
                        applicationPath = (applicationPath + HttpContext.Current.Request.ServerVariables["SERVER_NAME"].Trim()) + ":" + HttpContext.Current.Request.ServerVariables["SERVER_PORT"].Trim() + "/";
                        break;
                }
                return applicationPath;
            }
            if ((applicationPath == "") || (applicationPath == null))
            {
                applicationPath = "/";
            }
            return applicationPath;
        }

    }
}

