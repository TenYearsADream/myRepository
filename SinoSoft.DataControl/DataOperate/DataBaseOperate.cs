using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace SinoSoft.DataControl
{
    /// <summary>
    /// 数据库一些基本操作
    /// </summary>
    public class DataBaseOperate
    {

        #region 创建Database

        /// <summary>
        /// 根据配置文件中连接串名称获取DataBase
        /// </summary>
        /// <param name="connectionSetting">配置文件中连接串</param>
        /// <returns>所需Datase</returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2010年9月7日
        /// -->
        private static Database GetDatabase(ConnectionStringSettings connectionSetting)
        {
            return new SqlDatabase(connectionSetting.ConnectionString);//SQL2005

        }

        /// <summary>
        /// 根据配置文件中连接串名称获取DataBase
        /// </summary>
        /// <param name="name">配置文件中连接串名称</param>
        /// <returns>所需Datase</returns>
        /// <!--
        /// 创建人  : LiKun
        /// 创建时间: 2006-09-06
        /// -->
        public static Database GetDatabase(string name)
        {
            return GetDatabase(ConfigurationManager.ConnectionStrings[name]);
        }

        private static Database _NetOfficeDataBase;

        /// <summary>
        /// 创建NetOffice数据库
        /// </summary>
        /// <returns>NetOffice数据库对象</returns>
        /// <!--
        /// 创建人  : LiKun
        /// 创建时间: 2006-09-05
        /// -->
        public static Database CreateNetOfficeDataBase()
        {
            if (null == _NetOfficeDataBase)
            {
                _NetOfficeDataBase = GetDatabase(Config.NetOfficeConnection);
            }
            return _NetOfficeDataBase;
        }

        #endregion
    }
}
