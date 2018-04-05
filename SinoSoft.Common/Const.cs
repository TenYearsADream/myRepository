using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SinoSoft.Common
{
   public class Const
    {
        /// <summary>
        /// 获取连接字符串
        /// </summary>
       public static string ConnectionString
       {
           get
           {
               if (HttpContext.Current.Application["_dbConnStr"] == null)
               {
                   string dbServerIP = SinoSoft.Utils.XmlCOM.ReadConfig("~/_data/config/conn", "dbServerIP");
                   string dbLoginName = SinoSoft.Utils.XmlCOM.ReadConfig("~/_data/config/conn", "dbLoginName");
                   string dbLoginPass = SinoSoft.Utils.XmlCOM.ReadConfig("~/_data/config/conn", "dbLoginPass");
                   string dbName = SinoSoft.Utils.XmlCOM.ReadConfig("~/_data/config/conn", "dbName");
                   string dbConnStr = "Data Source=" + dbServerIP + ";Initial Catalog=" + dbName + ";User ID=" + dbLoginName + ";Password=" + dbLoginPass + ";Pooling=true";
                   HttpContext.Current.Application.Lock();
                   HttpContext.Current.Application["_dbConnStr"] = dbConnStr;
                   HttpContext.Current.Application.UnLock();
               }
               return HttpContext.Current.Application["_dbConnStr"].ToString();
           }
       }
      
        /// <summary>
        /// 获得用户IP
        /// </summary>
        public static string GetUserIp
        {
            get
            {
                string ip;
                string[] temp;
                bool isErr = false;
                if (HttpContext.Current.Request.ServerVariables["HTTP_X_ForWARDED_For"] == null)
                    ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                else
                    ip = HttpContext.Current.Request.ServerVariables["HTTP_X_ForWARDED_For"].ToString();
                if (ip.Length > 15)
                    isErr = true;
                else
                {
                    temp = ip.Split('.');
                    if (temp.Length == 4)
                    {
                        for (int i = 0; i < temp.Length; i++)
                        {
                            if (temp[i].Length > 3) isErr = true;
                        }
                    }
                    else
                        isErr = true;
                }

                if (isErr)
                    return "1.1.1.1";
                else
                    return ip;
            }
        }
        /// <summary>
        /// 格式化IP
        /// </summary>
        public static string FormatIp(string ipStr)
        {
            string[] temp = ipStr.Split('.');
            string format = "";
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i].Length < 3) temp[i] = Convert.ToString("000" + temp[i]).Substring(Convert.ToString("000" + temp[i]).Length - 3, 3);
                format += temp[i].ToString();
            }
            return format;
        }
        /// <summary>
        /// 来源地址
        /// </summary>
        public static string GetRefererUrl
        {
            get
            {
                if (HttpContext.Current.Request.ServerVariables["Http_Referer"] == null)
                    return "";
                else
                    return HttpContext.Current.Request.ServerVariables["Http_Referer"].ToString();
            }
        }
        /// <summary>
        /// 当前地址
        /// </summary>
        public static string GetCurrentUrl
        {
            get
            {
                string strUrl;
                strUrl = HttpContext.Current.Request.ServerVariables["Url"];
                if (HttpContext.Current.Request.QueryString.Count == 0) //如果无参数
                    return strUrl;
                else
                    return strUrl + "?" + HttpContext.Current.Request.ServerVariables["Query_String"];
            }

        }
        /// <summary>
        /// 判断校验码是否符合要求
        /// </summary>
        /// <param name="code">用户输入的校验码</param>
        /// <returns>返回校验码是否正确</returns>
        public bool CheckValidateCode(string code)
        {
            try
            {
                if (code.ToUpper() != HttpContext.Current.Session["_validate_code"].ToString().ToUpper())
                    return false;
                else
                    return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
