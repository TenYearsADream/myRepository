using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Sino.DAL
{
    public partial class SqlHelper
    {
        #region 返回SqlDataReader“注意手动关闭IDataReade”

        /// <summary>
        /// 根据SQL语句返回SqlDataReader“注意手动关闭IDataReade”（不支持事物，不支持储存过程，不支持传参）
        /// </summary>
        /// <param name="StrSql">SQL语句</param>
        /// 2012-4-28 DHAWD
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string StrSql)
        {
            SqlConnection con = GetSqlConnection();
            try
            {
                SQLOpen(con);
                SqlCommand com = GetSqlCommand(StrSql, con);
                return com.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
        }


        /// <summary>
        /// 根据SQL语句返回SqlDataReader“注意手动关闭IDataReade”（不支持事物，不支持储存过程，不支持传参）
        /// </summary>
        /// <param name="StrSql">SQL语句</param>
        /// <param name="con">连接对象</param>
        /// 2012-4-28 DHAWD
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string StrSql, SqlConnection con)
        {
            try
            {
                SQLOpen(con);
                SqlCommand com = GetSqlCommand(StrSql, con);
                return com.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 根据SQL语句返回SqlDataReader“注意手动关闭IDataReade”（不支持事物，不支持储存过程，支持传参）
        /// </summary>
        /// <param name="StrSql">SQL语句</param>
        /// <param name="con">连接对象</param>
        /// 2012-4-28 DHAWD
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string StrSql, SqlParameter[] parameter)
        {
            SqlConnection con = GetSqlConnection();
            try
            {
                SQLOpen(con);
                SqlCommand com = GetSqlCommand(StrSql, con, parameter);
                return com.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
        }


        /// <summary>
        /// 根据SQL语句返回SqlDataReader“注意手动关闭IDataReade”（不支持事物，不支持储存过程，支持传参）
        /// </summary>
        /// <param name="StrSql">SQL语句</param>
        /// <param name="con">连接对象</param>
        /// 2012-4-28 DHAWD
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string StrSql, SqlConnection con, SqlParameter[] parameter)
        {
            try
            {
                SQLOpen(con);
                SqlCommand com = GetSqlCommand(StrSql, con, parameter);
                return com.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
        }
        /// <summary>
        /// 根据SQL语句返回SqlDataReader“注意手动关闭IDataReade”（支持事物，不支持储存过程，不支持传参）
        /// </summary>
        /// <param name="StrSql">SQL语句</param>
        /// <param name="con">连接对象</param>
        /// <param name="taransaction">事物句柄</param>
        /// 2012-4-28 DHAWD
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string StrSql, SqlConnection con, SqlTransaction taransaction)
        {
            //SqlConnection con = GetSqlConnection();
            try
            {
                SQLOpen(con);
                SqlCommand com = GetSqlCommand(StrSql, con, taransaction);
                return com.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }

        }
        /// <summary>
        /// 根据SQL语句返回SqlDataReader对象（不支持事物，支持储存过程，不支持传参）
        /// </summary>
        /// <param name="StrSql">SQL语句</param>
        /// <param name="taransaction">事物句柄</param>
        /// 2012-11-4 DHAWD
        /// <returns>返回SqlDataReader对象</returns>
        public static SqlDataReader ExecuteReader(string StrSql, SqlTransaction taransaction)
        {

            try
            {
                SqlConnection con = GetSqlConnection();
                SQLOpen(con);
                SqlCommand com = GetSqlCommand(StrSql, con, taransaction);
                return com.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }

        }
        /// <summary>
        /// 根据SQL语句返回SqlDataReader“注意手动关闭IDataReade”（支持事物，支持储存过程，支持传参）
        /// </summary>
        /// <param name="StrSql">SQL语句或储存过程名称</param>
        /// <param name="paramenter">参数</param>
        /// <param name="con">SqlConnection对象</param>
        /// <param name="ComType">执行SQL语句类型</param>
        /// <param name="taransaction">事物句柄</param>
        /// 2012-4-28 DHAWD
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string StrSql, SqlParameter[] paramenter, SqlConnection con, CommandType ComType, SqlTransaction taransaction)
        {
            try
            {
                SQLOpen(con);
                using (SqlCommand com = GetSqlCommand(StrSql, con, taransaction, paramenter))
                {
                    com.CommandType = ComType;
                    return com.ExecuteReader();

                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }

        }

        /// <summary>
        /// 根据SQL语句返回SqlDataReader“注意手动关闭IDataReade”（支持事物，支持储存过程，不支持传参）
        /// </summary>
        /// <param name="StrSql">SQL语句或储存过程名称</param>
        /// <param name="paramenter">参数</param>
        /// <param name="con">SqlConnection对象</param>
        /// <param name="ComType">执行SQL语句类型</param>
        /// <param name="taransaction">事物句柄</param>
        /// 2012-4-28 DHAWD
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string StrSql, SqlConnection con, CommandType ComType, SqlTransaction taransaction)
        {
            try
            {
                SQLOpen(con);
                using (SqlCommand com = GetSqlCommand(StrSql, con, taransaction))
                {
                    com.CommandType = ComType;
                    return com.ExecuteReader();

                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }

        }
        /// <summary>
        /// 根据SQL语句返回SqlDataReader“注意手动关闭IDataReade”（支持储存过程，不支持事物，不支持传参）
        /// </summary>
        /// <param name="StrSql">SQL语句或储存过程名称</param>
        /// <param name="paramenter">参数</param>
        /// <param name="con">SqlConnection对象</param>
        /// <param name="ComType">执行SQL语句类型</param>
        /// <param name="taransaction">事物句柄</param>
        /// 2012-4-28 DHAWD
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string StrSql, SqlConnection con, CommandType ComType)
        {
            try
            {
                SQLOpen(con);
                using (SqlCommand com = GetSqlCommand(StrSql, con))
                {
                    com.CommandType = ComType;
                    return com.ExecuteReader();

                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 根据SQL语句返回SqlDataReader“注意手动关闭IDataReade”（支持储存过程，不支持事物,不支持传参）
        /// </summary>
        /// <param name="StrSql">SQL语句</param>
        /// <param name="ComType">执行命令方式</param>
        /// 2012-4-28 DHAWD
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string StrSql, CommandType ComType)
        {
            try
            {
                SqlConnection con = GetSqlConnection();
                try
                {
                    SQLOpen(con);
                    SqlCommand com = GetSqlCommand(StrSql, con);
                    com.CommandType = ComType;
                    return com.ExecuteReader();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 根据SQL语句返回SqlDataReader“注意手动关闭IDataReade”（支持储存过程，不支持事物，支持传参）
        /// </summary>
        /// <param name="StrSql">SQL语句</param>
        /// <param name="ComType">执行命令方式</param>
        /// <param name="parameter">传参数组</param>
        /// 2012-4-28 DHAWD
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string StrSql, CommandType ComType, SqlParameter[] parameter)
        {
            SqlConnection con = GetSqlConnection();
            try
            {
                SQLOpen(con);
                SqlCommand com = GetSqlCommand(StrSql, con, parameter);
                com.CommandType = ComType;
                return com.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 根据SQL语句返回SqlDataReader“注意手动关闭IDataReade”（支持储存过程，不支持事物，支持传参）
        /// </summary>
        /// <param name="StrSql">SQL语句</param>
        /// <param name="ComType">执行命令方式</param>
        /// <param name="parameter">传参数组</param>
        /// 2012-4-28 DHAWD
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string StrSql, SqlConnection con, CommandType ComType, SqlParameter[] parameter)
        {
            try
            {
                SQLOpen(con);
                SqlCommand com = GetSqlCommand(StrSql, con, parameter);
                com.CommandType = ComType;
                return com.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
        }
        #endregion        
    }
}
