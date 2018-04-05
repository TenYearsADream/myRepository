using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SinoSoft.DataControl
{
    public class Config
    {
        #region 数据库连接相关配置

        /// <summary>
        /// NetOffice数据库连接名。
        /// </summary>
        public const string NetOfficeConnectionName = "NetOffice";

        
        /// <summary>
        /// NetOffice数据库连接。
        /// </summary>
        public static ConnectionStringSettings NetOfficeConnection
        {
            get
            {
                return ConfigurationManager.ConnectionStrings[NetOfficeConnectionName];
            }
        }

        /// <summary>
        /// NetOffice数据库
        /// </summary>
        public static string NetOfficeLink
        {
            get
            {
                return ConfigurationManager.ConnectionStrings[NetOfficeConnectionName].ToString();
            }
        }

        #endregion
    }
}
