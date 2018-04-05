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
        #region 返回DataSet
        /// <summary>
        /// 获取SQL语句执行返回DataSet(不支持事物，不支持储存过程，不支持传参)
        /// </summary>
        /// <param name="StrSql">SQL语句</param>
        /// 2012 -4-28 DHAWD
        /// <returns>DataSet</returns>
        public static DataSet GetDataSet(string StrSql)
        {
            SqlConnection con = GetSqlConnection();
            try
            {
                SQLOpen(con);
                SqlCommand com = new SqlCommand(StrSql, con);
                DataSet DatSet = new DataSet();
                using (SqlDataAdapter pter = new SqlDataAdapter(com))
                {
                    pter.Fill(DatSet);
                    return DatSet;
                }
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
        /// 根据SQL语句返回DataSet（不支持事物，不支持储存过程，不支持传参）
        /// </summary>
        /// <param name="StrSql">SQL语句</param>
        /// <param name="con">SqlConnection对象</param>
        /// 2012 -4-28 DHAWD
        /// <returns>DataSet</returns>
        public static DataSet GetDataSet(string StrSql, SqlConnection con)
        {
            try
            {
                SQLOpen(con);
                SqlCommand com = GetSqlCommand(StrSql, con);
                DataSet DatSet = new DataSet();
                using (SqlDataAdapter pter = new SqlDataAdapter(com))
                {
                    pter.Fill(DatSet);
                    return DatSet;
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
        /// 根据SQL语句返回DataSet（不支持事物，不支持储存过程，支持传参）
        /// </summary>
        /// <param name="StrSql">SQL语句</param>
        /// <param name="parameter">SqlParameter参数组</param>
        /// 2012-4-28 DHAWD
        /// <returns>DataSet</returns>
        public static DataSet GetDataSet(string StrSql, SqlParameter[] parameter)
        {
            SqlConnection con = GetSqlConnection();
            try
            {
                SQLOpen(con);
                SqlCommand com = GetSqlCommand(StrSql, con, parameter);
                DataSet DatSet = new DataSet();
                using (SqlDataAdapter pter = new SqlDataAdapter(com))
                {
                    pter.Fill(DatSet);
                    return DatSet;
                }
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
        /// 根据SQL语句返回DataSet（不支持事物，不支持储存过程，支持传参）
        /// </summary>
        /// <param name="StrSql">SQL语句</param>
        /// <param name="con">SqlConnection对象</param>
        /// <param name="parameter">SqlParameter参数组</param>
        /// 2012-4-28 DHAWD
        /// <returns>DataSet</returns>
        public static DataSet GetDataSet(string StrSql, SqlConnection con, SqlParameter[] parameter)
        {
            try
            {
                SQLOpen(con);
                SqlCommand com = GetSqlCommand(StrSql, con, parameter);
                DataSet DatSet = new DataSet();
                using (SqlDataAdapter pter = new SqlDataAdapter(com))
                {
                    pter.Fill(DatSet);
                    return DatSet;
                }
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
        /// 根据SQL语句返回DataSet（支持事物，不支持储存过程，不支持传参）
        /// </summary>
        /// <param name="StrSql">SQL语句</param>
        /// <param name="con">连接对象</param>
        /// <param name="taransaction">事物句柄</param>
        /// 2012-4-28 DHAWD
        /// <returns>DataSet</returns>
        public static DataSet GetDataSet(string StrSql, SqlConnection con, SqlTransaction taransaction)
        {
            //SqlConnection con = GetSqlConnection();
            try
            {
                SQLOpen(con);
                SqlCommand com = GetSqlCommand(StrSql, con, taransaction);
                DataSet DatSet = new DataSet();
                using (SqlDataAdapter pter = new SqlDataAdapter(com))
                {
                    pter.Fill(DatSet);
                    return DatSet;
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
        /// 根据SQL语句返回DataSet（支持事物，支持储存过程，支持传参）
        /// </summary>
        /// <param name="StrSql">SQL语句或储存过程名称</param>
        /// <param name="paramenter">参数</param>
        /// <param name="con">SqlConnection对象</param>
        /// <param name="ComType">执行SQL语句类型</param>
        /// <param name="taransaction">事物句柄</param>
        /// 2012-4-28 DHAWD
        /// <returns>DataSet</returns>
        public static DataSet GetDataSet(string StrSql, SqlParameter[] paramenter, SqlConnection con, CommandType ComType, SqlTransaction taransaction)
        {
            try
            {
                SQLOpen(con);
                using (SqlCommand com = GetSqlCommand(StrSql, con, taransaction, paramenter))
                {
                    com.CommandType = ComType;
                    DataSet DatSet = new DataSet();
                    using (SqlDataAdapter pter = new SqlDataAdapter(com))
                    {
                        pter.Fill(DatSet);
                        return DatSet;
                    }

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
        /// 根据SQL语句返回DataSet（支持事物，支持储存过程，不支持传参）
        /// </summary>
        /// <param name="StrSql">SQL语句或储存过程名称</param>
        /// <param name="paramenter">参数</param>
        /// <param name="con">SqlConnection对象</param>
        /// <param name="ComType">执行SQL语句类型</param>
        /// <param name="taransaction">事物句柄</param>
        /// 2012-4-28 DHAWD
        /// <returns>DataSet</returns>
        public static DataSet GetDataSet(string StrSql, SqlConnection con, CommandType ComType, SqlTransaction taransaction)
        {
            try
            {
                SQLOpen(con);
                using (SqlCommand com = GetSqlCommand(StrSql, con, taransaction))
                {
                    com.CommandType = ComType;
                    DataSet DatSet = new DataSet();
                    using (SqlDataAdapter pter = new SqlDataAdapter(com))
                    {
                        pter.Fill(DatSet);
                        return DatSet;
                    }

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
        /// 根据SQL语句返回DataSet（支持储存过程，不支持事物，不支持传参）
        /// </summary>
        /// <param name="StrSql">SQL语句或储存过程名称</param>
        /// <param name="paramenter">参数</param>
        /// <param name="con">SqlConnection对象</param>
        /// <param name="ComType">执行SQL语句类型</param>
        /// <param name="taransaction">事物句柄</param>
        /// 2012-4-28 DHAWD
        /// <returns>DataSet</returns>
        public static DataSet GetDataSet(string StrSql, SqlConnection con, CommandType ComType)
        {
            try
            {
                SQLOpen(con);
                using (SqlCommand com = GetSqlCommand(StrSql, con))
                {
                    com.CommandType = ComType;
                    DataSet DatSet = new DataSet();
                    using (SqlDataAdapter pter = new SqlDataAdapter(com))
                    {
                        pter.Fill(DatSet);
                        return DatSet;
                    }

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
        /// 根据SQL语句返回DataSet（支持储存过程，不支持事物,不支持传参）
        /// </summary>
        /// <param name="StrSql">SQL语句</param>
        /// <param name="ComType">执行命令方式</param>
        /// 2012-4-28 DHAWD
        /// <returns>DataSet</returns>
        public static DataSet GetDataSet(string StrSql, CommandType ComType)
        {
            try
            {
                SqlConnection con = GetSqlConnection();
                try
                {
                    SQLOpen(con);
                    SqlCommand com = GetSqlCommand(StrSql, con);
                    com.CommandType = ComType;
                    DataSet DatSet = new DataSet();
                    using (SqlDataAdapter pter = new SqlDataAdapter(com))
                    {
                        pter.Fill(DatSet);
                        return DatSet;
                    }
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
        /// 根据SQL语句返回DataSet（支持储存过程，不支持事物，支持传参）
        /// </summary>
        /// <param name="StrSql">SQL语句</param>
        /// <param name="ComType">执行命令方式</param>
        /// <param name="parameter">传参数组</param>
        /// 2012-4-28 DHAWD
        /// <returns>DataSet</returns>
        public static DataSet GetDataSet(string StrSql, CommandType ComType, SqlParameter[] parameter)
        {
            SqlConnection con = GetSqlConnection();
            try
            {
                SQLOpen(con);
                SqlCommand com = GetSqlCommand(StrSql, con, parameter);
                com.CommandType = ComType;
                DataSet DatSet = new DataSet();
                using (SqlDataAdapter pter = new SqlDataAdapter(com))
                {
                    pter.Fill(DatSet);
                    return DatSet;
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
        /// 根据SQL语句返回DataSet（支持储存过程，不支持事物，支持传参）
        /// </summary>
        /// <param name="StrSql">SQL语句</param>
        /// <param name="ComType">执行命令方式</param>
        /// <param name="parameter">传参数组</param>
        /// 2012-4-28 DHAWD
        /// <returns>DataSet</returns>
        public static DataSet GetDataSet(string StrSql, SqlConnection con, CommandType ComType, SqlParameter[] parameter)
        {
            try
            {
                SQLOpen(con);
                SqlCommand com = GetSqlCommand(StrSql, con, parameter);
                com.CommandType = ComType;
                DataSet DatSet = new DataSet();
                using (SqlDataAdapter pter = new SqlDataAdapter(com))
                {
                    pter.Fill(DatSet);
                    return DatSet;
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
        #endregion
    }
}
