using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Sino.DAL
{
    public partial class SqlHelper
    {
        #region 返回List
       /// <summary>
       /// 获取SQL语句执行返回List(不支持事物，不支持储存过程，不支持传参)
       /// </summary>
       /// <param name="StrSql">SQL语句</param>
       /// <returns>List</returns>
       public static List<T> GetList<T>(string StrSql)where T:new()
       {
           try
           { 
               return GetList<T>(ExecuteReader(StrSql));
           }
           catch (Exception ex)
           {               
               throw new Exception(ex.Message,ex);
           }
        
       }
       /// <summary>
       /// 根据SQL语句返回List（不支持事物，不支持储存过程，不支持传参）
       /// </summary>
       /// <param name="StrSql">SQL语句</param>
       /// <param name="con">SqlConnection对象</param>
       /// 2012 -4-28 DHAWD
       /// <returns>List</returns>
       public static List<T> GetList<T>(string StrSql, SqlConnection con)where T:new()
       {
           try
           {
               return GetList<T>(ExecuteReader(StrSql,con));
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message, ex);
           }
       }

       /// <summary>
       /// 根据SQL语句返回DataTable（不支持事物，不支持储存过程，支持传参）
       /// </summary>
       /// <param name="StrSql">SQL语句</param>
       /// <param name="parameter">SqlParameter参数组</param>
       /// 2012-4-28 DHAWD
       /// <returns>DataTable</returns>
       public static List<T> GetList<T>(string StrSql, SqlParameter[] parameter) where T : new()
       {
           try
           {
               return GetList<T>(ExecuteReader(StrSql, parameter));
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message, ex);
           }
       }
       /// <summary>
       /// 根据SQL语句返回DataTable（不支持事物，不支持储存过程，支持传参）
       /// </summary>
       /// <param name="StrSql">SQL语句</param>
       /// <param name="con">SqlConnection对象</param>
       /// <param name="parameter">SqlParameter参数组</param>
       /// 2012-4-28 DHAWD
       /// <returns>DataTable</returns>
       public static List<T> GetList<T>(string StrSql, SqlConnection con, SqlParameter[] parameter) where T : new()
       {
           try
           {
               return GetList<T>(ExecuteReader(StrSql, parameter));
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message, ex);
           }
       }
       /// <summary>
       /// 根据SQL语句返回DataTable（不支持事物，支持储存过程，不支持传参）
       /// </summary>
       /// <param name="StrSql">SQL语句</param>
       /// <param name="con">事物句柄</param>
       /// 2012-4-28 DHAWD
       /// <returns>DataTable</returns>
       public static List<T> GetList<T>(string StrSql, SqlTransaction transaction) where T : new()
       {
           try
           {
               return GetList<T>(ExecuteReader(StrSql,transaction));
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message, ex);
           }
       }

       /// <summary>
       /// 根据SQL语句返回DataTable（支持事物，不支持储存过程，不支持传参）
       /// </summary>
       /// <param name="StrSql">SQL语句</param>
       /// <param name="con">连接对象</param>
       /// <param name="taransaction">事物句柄</param>
       /// 2012-4-28 DHAWD
       /// <returns>DataTable</returns>
       public static List<T> GetList<T>(string StrSql, SqlConnection con, SqlTransaction taransaction) where T : new()
       {
           try
           {
               return GetList<T>(ExecuteReader(StrSql,con,taransaction));
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message, ex);
           }
       }
       /// <summary>
       /// 根据SQL语句返回DataTable（支持事物，支持储存过程，支持传参）
       /// </summary>
       /// <param name="StrSql">SQL语句或储存过程名称</param>
       /// <param name="paramenter">参数</param>
       /// <param name="con">SqlConnection对象</param>
       /// <param name="ComType">执行SQL语句类型</param>
       /// <param name="taransaction">事物句柄</param>
       /// 2012-4-28 DHAWD
       /// <returns>DataTable</returns>
       public static List<T> GetList<T>(string StrSql, SqlParameter[] paramenter, SqlConnection con, CommandType ComType, SqlTransaction taransaction) where T : new()
       {
           try
           {
               return GetList<T>(ExecuteReader(StrSql,paramenter,con,ComType,taransaction));
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message, ex);
           }
       }

       /// <summary>
       /// 根据SQL语句返回DataTable（支持事物，支持储存过程，不支持传参）
       /// </summary>
       /// <param name="StrSql">SQL语句或储存过程名称</param>
       /// <param name="paramenter">参数</param>
       /// <param name="con">SqlConnection对象</param>
       /// <param name="ComType">执行SQL语句类型</param>
       /// <param name="taransaction">事物句柄</param>
       /// 2012-4-28 DHAWD
       /// <returns>DataTable</returns>
       public static List<T> GetList<T>(string StrSql, SqlConnection con, CommandType ComType, SqlTransaction taransaction) where T : new()
       {
           try
           {
               return GetList<T>(ExecuteReader(StrSql,con,ComType,taransaction));
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message, ex);
           }
       }
       /// <summary>
       /// 根据SQL语句返回DataTable（支持储存过程，不支持事物，不支持传参）
       /// </summary>
       /// <param name="StrSql">SQL语句或储存过程名称</param>
       /// <param name="paramenter">参数</param>
       /// <param name="con">SqlConnection对象</param>
       /// <param name="ComType">执行SQL语句类型</param>
       /// <param name="taransaction">事物句柄</param>
       /// 2012-4-28 DHAWD
       /// <returns>DataTable</returns>
       public static List<T> GetList<T>(string StrSql, SqlConnection con, CommandType ComType) where T : new()
       {
           try
           {
               return GetList<T>(ExecuteReader(StrSql,con,ComType));
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message, ex);
           }
       }

       /// <summary>
       /// 根据SQL语句返回DataTable（支持储存过程，不支持事物,不支持传参）
       /// </summary>
       /// <param name="StrSql">SQL语句</param>
       /// <param name="ComType">执行命令方式</param>
       /// 2012-4-28 DHAWD
       /// <returns>DataTable</returns>
       public static List<T> GetList<T>(string StrSql, CommandType ComType) where T : new()
       {
           try
           {
               return GetList<T>(ExecuteReader(StrSql,ComType));
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message, ex);
           }
       }

       /// <summary>
       /// 根据SQL语句返回DataTable（支持储存过程，不支持事物，支持传参）
       /// </summary>
       /// <param name="StrSql">SQL语句</param>
       /// <param name="ComType">执行命令方式</param>
       /// <param name="parameter">传参数组</param>
       /// 2012-4-28 DHAWD
       /// <returns>DataTable</returns>
       public static List<T> GetList<T>(string StrSql, CommandType ComType, SqlParameter[] parameter) where T : new()
       {
           try
           {
               return GetList<T>(ExecuteReader(StrSql, parameter));
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message, ex);
           }
       }

       /// <summary>
       /// 根据SQL语句返回DataTable（支持储存过程，不支持事物，支持传参）
       /// </summary>
       /// <param name="StrSql">SQL语句</param>
       /// <param name="ComType">执行命令方式</param>
       /// <param name="parameter">传参数组</param>
       /// 2012-4-28 DHAWD
       /// <returns>DataTable</returns>
       public static List<T> GetList<T>(string StrSql, SqlConnection con, CommandType ComType, SqlParameter[] parameter) where T : new()
       {
           try
           {
               return GetList<T>(ExecuteReader(StrSql, parameter));
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message, ex);
           }
       }
       /// <summary>
       /// 根据DataTable获取List集合
       /// </summary>
       /// <typeparam name="T">实体</typeparam>
       /// <param name="Table">数据表格</param>
       /// 2012 -11-4 DHAWD
       /// <returns></returns>
       public static List<T> GetList<T>(DataTable Table) where T :new()
       {
           try
           {
              
               if (Table ==null && Table.Rows.Count ==0)
               {
                   return null;
               }
               List<T> List = new List<T>();
               foreach (DataRow Row in Table.Rows)
               {
                   List.Add(GetModel<T>(Row));                  
               }
               return null;
           }
           catch (Exception ex)
           {
               
               throw new  Exception(ex.Message,ex);
           }           
       
       }

       /// <summary>
       /// 根据DataTable获取List集合
       /// </summary>
       /// <typeparam name="T">实体</typeparam>
       /// <param name="Table">数据表格</param>
       /// 2012 -11-4 DHAWD
       /// <returns></returns>
       public static List<T> GetList<T>(IDataReader DataReader) where T : new()
       {
           try
           {
               List<T> List = new List<T>();
               while (DataReader.Read())
               {
                   List.Add(GetModel<T>(DataReader));
               }               
               ReaderClose(DataReader);            
              
               return List;
           }
           catch (Exception ex)
           {

               throw new Exception(ex.Message, ex);
           }

       }

       #endregion
    }
}
