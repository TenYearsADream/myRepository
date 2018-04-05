using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;

namespace SinoSoft.DataControl
{
    /// <summary>
    /// 数据库一些基本操作
    /// </summary>
    public class DataOperate
    {
        private static Database _DataBase = DataBaseOperate.CreateNetOfficeDataBase();

        /// <summary>
        /// 获取指定sql的数据命令
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DbCommand GetSqlStringCommand(string sql)
        {
            return _DataBase.GetSqlStringCommand(sql);
        }

        /// <summary>
        /// 获取指定sql的DataTable数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(string sql)
        {
            return _DataBase.ExecuteDataSet(CommandType.Text, sql).Tables[0];
        }

        /// <summary>
        /// 获取指定sql的DataTable数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string sql)
        {
            return _DataBase.ExecuteDataSet(CommandType.Text, sql).Tables[0];
        }

        /// <summary>
        /// 获取指定sql的DataView数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataView GetDataView(string sql)
        {
            return _DataBase.ExecuteDataSet(CommandType.Text, sql).Tables[0].DefaultView;
        }

        /// <summary>
        /// 获取指定sql的DataSet数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(string sql)
        {
            return _DataBase.ExecuteDataSet(CommandType.Text, sql);
        }

        /// <summary>
        /// 获取指定sql的DataSet数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(string sql)
        {
            return _DataBase.ExecuteDataSet(CommandType.Text, sql);
        }

        /// <summary>
        /// 获取指定sql的DataSet数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static IDataReader ExecuteReader(string sql)
        {
            return _DataBase.ExecuteReader(CommandType.Text, sql);
        }

        /// <summary>
        /// 获取指定sql第一行第一列值
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static Object ExecuteScalar(string sql)
        {
            return _DataBase.ExecuteScalar(CommandType.Text, sql);
        }

        /// <summary>
        /// 执行sql,返回影响的行数
        /// </summary>
        /// <param name="sql"></param>
        public static int ExecuteNonQuery(string sql)
        {
            return _DataBase.ExecuteNonQuery(CommandType.Text, sql);
        }

        /// <summary>
        /// 执行sql,返回影响的行数
        /// </summary>
        /// <param name="cmd"></param>
        public static int ExecuteNonQuery(DbCommand cmd)
        {
            return _DataBase.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 数据插入
        /// </summary>
        /// <param name="StrSql">SQL</param>
        public static void DataIns(string sql)
        {
            _DataBase.ExecuteNonQuery(CommandType.Text, sql);
        }

        /// <summary>
        /// 数据删除
        /// </summary>
        /// <param name="strSql">SQL</param>
        public static void DataDel(string sql)
        {
            _DataBase.ExecuteNonQuery(CommandType.Text, sql);
        }

        /// <summary>
        /// 数据更新
        /// </summary>
        /// <param name="strSql">SQL</param>
        public static void DataUpdata(string sql)
        {
            _DataBase.ExecuteNonQuery(CommandType.Text, sql);
        }

        
    }
}
