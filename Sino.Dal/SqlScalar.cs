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
        #region 返回第一行第一列
        /// <summary>
        /// 获取SQL语句执行(不支持事物，不支持储存过程，不支持传参)
        /// </summary>
        /// <param name="StrSql">SQL语句</param>
        /// <returns>返回第一行第一列</returns>
        public static object ExecuteScalar(string StrSql)
        {
            try
            {
                SqlConnection con = GetSqlConnection();
                SQLOpen(con);
                SqlCommand com = new SqlCommand(StrSql, con);
                return com.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetDataTable(string StrSql)
        {
            try
            {
                SqlConnection con = GetSqlConnection();
                SQLOpen(con);
                SqlCommand com = new SqlCommand();
                SqlDataAdapter adp = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                return dt;
            }
            catch
            {
                return null;
            }
        }
        public static bool sqlBulk(DataTable dt, string TableName)
        {
            SqlConnection con = GetSqlConnection();
            SQLOpen(con);
            using (SqlTransaction st = con.BeginTransaction())
            {
                using (SqlBulkCopy copy = new SqlBulkCopy(con, SqlBulkCopyOptions.FireTriggers,st))
                {
                    copy.DestinationTableName = TableName;
                    foreach (DataColumn col in dt.Columns)
                    {
                        copy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                    }
                    try
                    {
                        copy.WriteToServer(dt);
                        st.Commit();
                        return true;
                    }
                    catch
                    {
                        st.Rollback();
                        return false;
                    }
                }
            }
        }
        /// <summary>
        /// 根据SQL语句返回第一行第一列（不支持事物，不支持储存过程，不支持传参）
        /// </summary>
        /// <param name="StrSql">SQL语句</param>
        /// <param name="con">SqlConnection对象</param>
        /// 2012 -4-28 DHAWD
        /// <returns>返回第一行第一列</returns>
        public static object ExecuteScalar(string StrSql, SqlConnection con)
        {
            try
            {
                SQLOpen(con);
                SqlCommand com = GetSqlCommand(StrSql, con);
                return com.ExecuteScalar();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
            finally
            {
                SQLClose(con);
            }
        }

        /// <summary>
        /// 根据SQL语句返回第一行第一列（不支持事物，不支持储存过程，支持传参）
        /// </summary>
        /// <param name="StrSql">SQL语句</param>
        /// <param name="parameter">SqlParameter参数组</param>
        /// 2012-4-28 DHAWD
        /// <returns>返回第一行第一列</returns>
        public static object ExecuteScalar(string StrSql, SqlParameter[] parameter)
        {
            SqlConnection con = GetSqlConnection();
            try
            {
                SQLOpen(con);
                SqlCommand com = GetSqlCommand(StrSql, con, parameter);
                return com.ExecuteScalar();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                SQLClose(con);
            }
        }
        /// <summary>
        /// 根据SQL语句返回第一行第一列（不支持事物，不支持储存过程，支持传参）
        /// </summary>
        /// <param name="StrSql">SQL语句</param>
        /// <param name="con">SqlConnection对象</param>
        /// <param name="parameter">SqlParameter参数组</param>
        /// 2012-4-28 DHAWD
        /// <returns>返回第一行第一列</returns>
        public static object ExecuteScalar(string StrSql, SqlConnection con, SqlParameter[] parameter)
        {
            try
            {
                SQLOpen(con);
                SqlCommand com = GetSqlCommand(StrSql, con, parameter);
                return com.ExecuteScalar();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                SQLClose(con);
            }
        }
        /// <summary>
        /// 根据SQL语句返回第一行第一列（支持事物，不支持储存过程，不支持传参）
        /// </summary>
        /// <param name="StrSql">SQL语句</param>
        /// <param name="con">连接对象</param>
        /// <param name="taransaction">事物句柄</param>
        /// 2012-4-28 DHAWD
        /// <returns>受影响行数</returns>
        public static object ExecuteScalar(string StrSql, SqlConnection con, SqlTransaction taransaction)
        {
            //SqlConnection con = GetSqlConnection();
            try
            {
                SQLOpen(con);
                SqlCommand com = GetSqlCommand(StrSql, con, taransaction);
                return com.ExecuteScalar();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
            finally
            {
                SQLClose(con);
            }
        }

        /// <summary>
        /// 根据SQL语句返回第一行第一列（支持事物，支持储存过程，支持传参）
        /// </summary>
        /// <param name="StrSql">SQL语句或储存过程名称</param>
        /// <param name="paramenter">参数</param>
        /// <param name="con">SqlConnection对象</param>
        /// <param name="ComType">执行SQL语句类型</param>
        /// <param name="taransaction">事物句柄</param>
        /// 2012-4-28 DHAWD
        /// <returns>第一行第一列</returns>
        public static object ExecuteScalar(string StrSql, SqlParameter[] paramenter, SqlConnection con, CommandType ComType, SqlTransaction taransaction)
        {
            try
            {
                SQLOpen(con);
                using (SqlCommand com = GetSqlCommand(StrSql, con, taransaction, paramenter))
                {
                    com.CommandType = ComType;
                    return com.ExecuteScalar();

                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
            finally
            {
                SQLClose(con);
            }
        }

        /// <summary>
        /// 根据SQL语句返回第一行第一列（支持事物，支持储存过程，不支持传参）
        /// </summary>
        /// <param name="StrSql">SQL语句或储存过程名称</param>
        /// <param name="paramenter">参数</param>
        /// <param name="con">SqlConnection对象</param>
        /// <param name="ComType">执行SQL语句类型</param>
        /// <param name="taransaction">事物句柄</param>
        /// 2012-4-28 DHAWD
        /// <returns>第一行第一列</returns>
        public static object ExecuteScalar(string StrSql, SqlConnection con, CommandType ComType, SqlTransaction taransaction)
        {
            try
            {
                SQLOpen(con);
                using (SqlCommand com = GetSqlCommand(StrSql, con, taransaction))
                {
                    com.CommandType = ComType;
                    return com.ExecuteScalar();

                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
            finally
            {
                SQLClose(con);
            }
        }
        /// <summary>
        /// 根据SQL语句返回第一行第一列（支持储存过程，不支持事物，不支持传参）
        /// </summary>
        /// <param name="StrSql">SQL语句或储存过程名称</param>
        /// <param name="paramenter">参数</param>
        /// <param name="con">SqlConnection对象</param>
        /// <param name="ComType">执行SQL语句类型</param>
        /// 2012-4-28 DHAWD
        /// <returns>第一行第一列</returns>
        public static object ExecuteScalar(string StrSql, SqlConnection con, CommandType ComType)
        {
            try
            {
                SQLOpen(con);
                using (SqlCommand com = GetSqlCommand(StrSql, con))
                {
                    com.CommandType = ComType;
                    return com.ExecuteScalar();

                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
            finally
            {
                SQLClose(con);
            }
        }

        /// <summary>
        /// 根据SQL语句返回第一行第一列（支持储存过程，不支持事物,不支持传参）
        /// </summary>
        /// <param name="StrSql">SQL语句</param>
        /// <param name="ComType">执行命令方式</param>
        /// 2012-4-28 DHAWD
        /// <returns>第一行第一列</returns>
        public static object ExecuteScalar(string StrSql, CommandType ComType)
        {
            try
            {
                SqlConnection con = GetSqlConnection();
                try
                {
                    SQLOpen(con);
                    SqlCommand com = GetSqlCommand(StrSql, con);
                    com.CommandType = ComType;
                    return com.ExecuteScalar();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                finally
                {
                    SQLClose(con);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 根据SQL语句返回第一行第一列（支持储存过程，不支持事物，支持传参）
        /// </summary>
        /// <param name="StrSql">SQL语句</param>
        /// <param name="ComType">执行命令方式</param>
        /// <param name="parameter">传参数组</param>
        /// 2012-4-28 DHAWD
        /// <returns>第一行第一列</returns>
        public static object ExecuteScalar(string StrSql, CommandType ComType, SqlParameter[] parameter)
        {
            SqlConnection con = GetSqlConnection();
            try
            {
                SQLOpen(con);
                SqlCommand com = GetSqlCommand(StrSql, con, parameter);
                com.CommandType = ComType;
                return com.ExecuteScalar();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
            finally
            {
                SQLClose(con);
            }
        }

        /// <summary>
        /// 根据SQL语句返回第一行第一列（支持储存过程，不支持事物，支持传参）
        /// </summary>
        /// <param name="StrSql">SQL语句</param>
        /// <param name="ComType">执行命令方式</param>
        /// <param name="parameter">传参数组</param>
        /// 2012-4-28 DHAWD
        /// <returns>第一行第一列</returns>
        public static object ExecuteScalar(string StrSql, SqlConnection con, CommandType ComType, SqlParameter[] parameter)
        {
            try
            {
                SQLOpen(con);
                SqlCommand com = GetSqlCommand(StrSql, con, parameter);
                com.CommandType = ComType;
                return com.ExecuteScalar();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
            finally
            {
                SQLClose(con);
            }
        }

                #endregion
    }
}
