using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Transactions;
using System.Configuration;
using System.Reflection;
using System.Data;

namespace Sino.DAL
{
   /// <summary>
   /// SQL访问类
   /// </summary>
   public partial class SqlHelper
   {
       #region 参数设置
      
       /// <summary>
       /// 实体名
       /// </summary>
       public static string ModelName = ConfigurationManager.AppSettings["ModelName"].ToUpper();
       /// <summary>
       /// SQLSERVER连接字符串
       /// </summary>
       public static string SQLSERVER = ConfigurationManager.AppSettings["SQLSERVER"].ToString();
       /// <summary>
       /// 插入语句
       /// </summary>
       public static string SQL_INSERTSTR = "INSERT INTO [{TABLE_NAME}]({FAILEDS}) values({VALUES}) select @@identity";
       /// <summary>
       /// 更新语句
       /// </summary>
       public static string SQL_UPDATE = "UPDATE [{TABLE_NAME}] SET {VALUES} WHERE (1=1) {WHERE}";
       /// <summary>
       /// 查询语句
       /// </summary>
       public static string SQL_SELECT = "SELECT {FAILEDS} FROM [{TABLE_NAME}]  WHERE (1=1) {VALUES}";
       /// <summary>
       /// 删除语句
       /// </summary>
       public static string SQL_DELETE = "DELETE [{TABLE_NAME}] WHERE (1=1) {VALUES}";

       /// <summary>
       /// 连接字符串获取
       /// </summary>
       public static string StrConnection
       {
           get { return SQLSERVER; }
           set { SQLSERVER = value; }
       }
       #endregion

       

       #region 帮助方法


       /// <summary>
       /// 获取一个连接对象
       /// </summary> 
       /// <returns>SqlConnection对象</returns>
       public static SqlConnection GetSqlConnection() 
       {
           try
           {
               return new SqlConnection(SQLSERVER);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }
       /// <summary>
       /// 根据连接字符串获取一个连接对象
       /// </summary>
       /// <param name="SERVERSTR">连接字符串</param>
       /// <returns>SqlConnection对象</returns>
       public static SqlConnection GetSqlConnection(string SERVERSTR) 
       {
           try
           {
               return new SqlConnection(SERVERSTR);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }
       /// <summary>
       /// 获取一个SqlCommand对象（支持事物）
       /// </summary>
       /// <param name="CommTex">SQL语句</param>
       /// <param name="Conn">连接对象</param>
       /// <param name="taransaction">事物对象</param>
       /// <returns>SqlCommand对象</returns>
       public static SqlCommand GetSqlCommand(string CommTexT, SqlConnection Conn,SqlTransaction taransaction) 
       {
           try
           {
               if (taransaction != null)
               {
                   return new SqlCommand(CommTexT, Conn, taransaction);
               }
               else
                   return new SqlCommand(CommTexT, Conn);
           }
           catch (Exception ex)
           {
               
               throw new Exception(ex.Message, ex);
           }
       }
       /// <summary>
       /// 获取一个SqlCommand对象（支持事物，支持传参）
       /// </summary>
       /// <param name="CommText">SQL语句</param>
       /// <param name="Conn">连接对象</param>
       /// <param name="transaction">事物对象</param>
       /// <param name="parameter">参数数组</param>
       /// <returns>SqlCommand对象</returns>
       public static SqlCommand GetSqlCommand(string CommText, SqlConnection Conn, SqlTransaction transaction, SqlParameter[] parameter)
       {
           SqlCommand com = new SqlCommand();
           com.CommandText = CommText;
           com.Connection = Conn;
           if (transaction != null)
           {
               com.Transaction = transaction;
           }
           //判断是否有传参
           if (parameter != null && parameter.Length > 0)
           {

               for (int i = 0; i < parameter.Length; i++)
               {
                   SqlParameter par = new SqlParameter();
                   par.ParameterName = parameter[i].ParameterName.StartsWith("@") ? parameter[i].ParameterName : "@" + parameter[i].ParameterName;
                   
                       par.SqlDbType = parameter[i].SqlDbType;//设置参数类型
                   
                       par.Size = parameter[i].Size;//设置列中的数据大小
                   
                       par.Direction = parameter[i].Direction;//设置参数流向
                   par.Value = parameter[i].Value;
                   com.Parameters.Add(par);
               }
           }
               return com;
       }

       /// <summary>
       /// 获取一个SqlCommand对象（不支持事物）
       /// </summary>
       /// <param name="CommTex">SQL语句</param>
       /// <param name="Conn">连接对象</param>
       /// <returns>SqlCommand对象</returns>
       public static SqlCommand GetSqlCommand(string CommTexT, SqlConnection Conn)
       {
           try
           {
                   return new SqlCommand(CommTexT, Conn);
           }
           catch (Exception ex)
           {

               throw new Exception(ex.Message, ex);
           }
       }

       /// <summary>
       /// 获取一个SqlCommand对象（不支持事物,支持传参）
       /// </summary>
       /// <param name="CommTex">SQL语句</param>
       /// <param name="Conn">连接对象</param>
       /// <returns>SqlCommand对象</returns>
       public static SqlCommand GetSqlCommand(string CommTexT, SqlConnection Conn, SqlParameter[] parameter)
       {
           try
           {
               using (SqlCommand com = new SqlCommand(CommTexT, Conn))
               {
                   //判断是否有传参
                   if (parameter != null && parameter.Length > 0)
                   {

                       for (int i = 0; i < parameter.Length; i++)
                       {
                           SqlParameter par = new SqlParameter();
                           par.ParameterName = parameter[i].ParameterName.StartsWith("@") ? parameter[i].ParameterName : "@" + parameter[i].ParameterName;
                           
                               par.SqlDbType = parameter[i].SqlDbType;//设置参数类型
                           
                               par.Size = parameter[i].Size;//设置列中的数据大小
                           
                               par.Direction = parameter[i].Direction;//设置参数流向
                           par.Value = parameter[i].Value;
                           com.Parameters.Add(par);
                       }
                   }
                   return  com;
               }
           }
           catch (Exception ex)
           {

               throw new Exception(ex.Message, ex);
           }
       }

       /// <summary>
       /// 打开连接
       /// </summary>
       /// <param name="con"></param>
       public static void SQLOpen(SqlConnection con)
       {
           if (con.State != ConnectionState.Open)
               con.Open();
       }
       /// <summary>
       /// 关闭数据库
       /// </summary>
       /// <param name="con"></param>
       public static void SQLClose(SqlConnection con)
       {
           if (con.State != ConnectionState.Closed)
               con.Close();
       }

       /// <summary>
       /// 关闭IDataReader对象
       /// </summary>
       /// <param name="con"></param>
       public static void ReaderClose(IDataReader DataReader)
       {
           if (DataReader.IsClosed)
               DataReader.Close();
       }

       #endregion

       #region 说明性代码（已经注释）
       /// <summary>
       /// 获取实体值（便于理解写法）
       /// </summary>
       /// <typeparam name="T">实体</typeparam>
       /// <param name="Row">数据行</param>
       /// <returns></returns>
       //public static T GetModel<T>(DataRow Row) where T : new()
       //{
       //    if (Row == null)
       //    {
       //        return default(T);
       //    }
       //    T t = new T();
       //    Type type = t.GetType();
       //    foreach (PropertyInfo Info in type.GetProperties())
       //    {
       //        foreach (DataColumn Column in Row.Table.Columns)
       //        {
       //            if (Info.Name.ToUpper() == Column.ColumnName.ToUpper())
       //            {
       //                if (Info.CanWrite)
       //                {
       //                    object o = Row[Column.ColumnName] != DBNull.Value && Row[Column.ColumnName] != null ?
       //                    Row[Column.ColumnName] : null;
       //                    object[] paras = { o };
       //                    Info.ReflectedType.InvokeMember(Info.Name, BindingFlags.SetProperty, null, t, paras);
       //                }
       //            }
       //        }
       //    }
       //    return t;
       //}

       #endregion

       

       






       /// <summary>
       /// 通过系统的存储过程获取主键
       /// </summary>
       /// <param name="TableName">表名</param>
       /// <returns></returns>
       public static string GetPrimaryKey(string TableName)
       {
           string PK = string.Empty;
           string sql = "EXEC sys.sp_pkeys @table_name = '" + TableName + "'";
           DataTable dt = GetTable(sql);
           if (null != dt && dt.Rows.Count > 0)
               PK = Convert.ToString(dt.Rows[0]["COLUMN_NAME"]);
           return PK;
       }

       /// <summary>
       /// 根据实体获取所以数据
       /// </summary>
       /// <typeparam name="T">要获取的实体</typeparam>
       /// <returns></returns>
       public static List<T> GetList<T>() where T:new()
       {
           string StrSQL = string.Empty;
           string FAILEDS = string.Empty;
           try
           {
               T t = new T();
               Type type = t.GetType();
               StrSQL = SQL_SELECT.Replace("{TABLE_NAME}", type.Name);
               StrSQL = StrSQL.Replace("{VALUES}", " "); //无条件去除空
               foreach (PropertyInfo Info in type.GetProperties())
               {
                   FAILEDS += Info.Name + ",";
               }

               if (FAILEDS.Length > 0)
               {
                   FAILEDS = FAILEDS.Remove(FAILEDS.Length - 1);
               }
               else
               {
                   FAILEDS = "*";
               }
               StrSQL = StrSQL.Replace("{FAILEDS}", FAILEDS);

               return GetList<T>(StrSQL);
           }
           catch (Exception EX)
           {

               throw new Exception(EX.Message,EX);
           }
           
       }

       /// <summary>
       /// 根据实体获取所以数据
       /// </summary>
       /// <typeparam name="T">要获取的实体</typeparam>
       /// <returns></returns>
       public static List<T> GetList<T>(T Model) where T : new()
       {
           string StrSQL = string.Empty;
           string FAILEDS = string.Empty;
           string WHERE = string.Empty;
           try
           {
               //T t = new T();
               Type type = Model.GetType();              
               StrSQL = SQL_SELECT.Replace("{TABLE_NAME}", type.Name);
               foreach (PropertyInfo Info in type.GetProperties())
               {
                   FAILEDS += Info.Name + ",";

                   //获取条件
                   if (null != Info.GetValue(Model,null))
                   {
                       WHERE += " AND " + Info.Name + " ='" + Info.GetValue(Model, null) + "' ";
                   }
                   
               }

               if (FAILEDS.Length > 0)
               {
                   FAILEDS = FAILEDS.Remove(FAILEDS.Length - 1);
               }
               else
               {
                   FAILEDS = "*";
               }

               StrSQL = StrSQL.Replace("{FAILEDS}", FAILEDS);
               StrSQL = StrSQL.Replace("{VALUES}", WHERE);
               return GetList<T>(StrSQL);
           }
           catch (Exception EX)
           {

               throw new Exception(EX.Message, EX);
           }
       }


       /// <summary>
       /// 根据实体获取对应的字典（不排除主建 ，去除为NULL）
       /// </summary>
       /// <typeparam name="T">对应的实体</typeparam>
       /// <param name="t">实体</param>
       /// <returns>返回字典</returns>
       public static Dictionary<string, object> DictionaryName<T>(T t)
       {
           Dictionary<string, object> dic = new Dictionary<string, object>();
           try
           {
               //判断对应的Model是否存在
                if (t.GetType().Module.Name.ToUpper().Equals(ModelName))
               {
                   foreach (PropertyInfo property in t.GetType().GetProperties())
                   {
                       if (null != property.GetValue(t, null))
                       {
                           dic.Add(property.Name, property.GetValue(t, null));
                       }
                   }
                   if (dic.Count > 0)
                   {
                       return dic;
                   }
                   else 
                   {
                       throw new Exception("所有的参数都为NULL");
                   }
                   
               }
               else 
               {
                   throw new Exception("对应的Model不存在这个实体");
               }
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }
    }
}
