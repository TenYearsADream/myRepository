using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Sino.DAL
{
    public partial class SqlHelper
    {
        /// <summary>
        /// 插入语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t">插入字段实体</param>
        /// <returns></returns>
        public static bool Insertstr<T>(T t)
        {
            string StrSql = string.Empty;
            string FAILEDS = string.Empty;
            string Values = string.Empty;
            Dictionary<string, object> dic = DictionaryName<T>(t);
            string PK = GetPrimaryKey(t.GetType().Name);
            StrSql = SQL_INSERTSTR.Replace("[{TABLE_NAME}]", t.GetType().Name);
            foreach (var item in dic)
            {
                if (item.Key.ToUpper().Equals(PK.ToUpper()))
                {
                    continue;
                }
                else
                {
                    FAILEDS += "[" + item.Key + "],";
                    Values += "'" + item.Value + "',";
                }
            }
            if (FAILEDS.Length > 0)
            {
                FAILEDS = FAILEDS.Substring(0, FAILEDS.Length - 1);
            }
            if (Values.Length > 0)
            {
                Values = Values.Substring(0, Values.Length - 1);
            }
            StrSql = StrSql.Replace("{FAILEDS}", FAILEDS).Replace("{VALUES}", Values);
            return ExecuteNonQuery(StrSql) > 0 ? true : false;

        }


        /// <summary>
        /// 插入语句返回ID值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t">插入字段实体</param>
        /// <returns>ID值</returns>
        public static object InsertstrReturnKey<T>(T t)
        {
            string StrSql = string.Empty;
            string FAILEDS = string.Empty;
            string Values = string.Empty;
            Dictionary<string, object> dic = DictionaryName<T>(t);
            string PK = GetPrimaryKey(t.GetType().Name);
            StrSql = SQL_INSERTSTR.Replace("[{TABLE_NAME}]", t.GetType().Name);
            foreach (var item in dic)
            {
                if (item.Key.ToUpper().Equals(PK.ToUpper()))
                {
                    continue;
                }
                else
                {
                    FAILEDS += "[" + item.Key + "],";
                    Values += "'" + item.Value + "',";
                }
            }
            if (FAILEDS.Length > 0)
            {
                FAILEDS = FAILEDS.Substring(0, FAILEDS.Length - 1);
            }
            if (Values.Length > 0)
            {
                Values = Values.Substring(0, Values.Length - 1);
            }
            StrSql = StrSql.Replace("{FAILEDS}", FAILEDS).Replace("{VALUES}", Values);
            return ExecuteScalar(StrSql);

        }


        /// <summary>
        /// 插入语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t">插入字段实体</param>
        /// <returns></returns>
        public static bool Insertstr<T>(List<T> t)
        {
            string StrSql = string.Empty;
            string FAILEDS = string.Empty;
            string Values = string.Empty;
            using (TransactionScope tra = new TransactionScope())
            {
                bool bo = false;
                foreach (var Model in t)
                {
                    Dictionary<string, object> dic = DictionaryName<T>(Model);
                    string PK = GetPrimaryKey(Model.GetType().Name);
                    StrSql = SQL_INSERTSTR.Replace("[{TABLE_NAME}]", Model.GetType().Name);
                    foreach (var item in dic)
                    {
                        if (item.Key.ToUpper().Equals(PK.ToUpper()))
                        {
                            continue;
                        }
                        else
                        {
                            FAILEDS += "[" + item.Key + "],";
                            Values += "'" + item.Value + "',";
                        }
                    }
                    if (FAILEDS.Length > 0)
                    {
                        FAILEDS = FAILEDS.Substring(0, FAILEDS.Length - 1);
                    }
                    if (Values.Length > 0)
                    {
                        Values = Values.Substring(0, Values.Length - 1);
                    }
                    StrSql = StrSql.Replace("{FAILEDS}", FAILEDS).Replace("{VALUES}", Values);
                    if (ExecuteNonQuery(StrSql) > 1)
                    {
                        bo = true;
                        continue;
                    }
                    else
                    {
                        tra.Dispose();
                        break;
                        
                    }
                }
                if (bo) tra.Complete();
            }

            return true;

        }


        /// <summary>
        /// 删除语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t">删除条件实体</param>
        /// <returns></returns>
        public static bool DELETE<T>(T t)
        {
            string StrSql = string.Empty;
            string WHERE = string.Empty;
            string FAILEDS = string.Empty;

            Dictionary<string, object> dic = DictionaryName<T>(t);
            string PK = GetPrimaryKey(t.GetType().Name);
            StrSql =SQL_DELETE.Replace("[{TABLE_NAME}]", t.GetType().Name);
            foreach (var item in dic)
            {
                WHERE += " and " + item.Key + " ='" + item.Value + "' ";
            }
            StrSql = StrSql.Replace("{VALUES}", WHERE);
            return ExecuteNonQuery(StrSql) > 0 ? true : false;

        }

        /// <summary>
        /// 更新语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t">更新字段实体</param>
        /// <param name="w">更新条件实体</param>
        /// <returns></returns>
        public static bool UPDATE<T>(T t,T w)
        {
            string StrSql = string.Empty;
            string WHERE = string.Empty;
            string Values = string.Empty;
            Dictionary<string, object> dic = DictionaryName<T>(t);
            Dictionary<string, object> wheredic = DictionaryName<T>(w);
            string PK = GetPrimaryKey(t.GetType().Name);
            StrSql = SQL_UPDATE.Replace("[{TABLE_NAME}]", t.GetType().Name);
            foreach (var item in dic)
            {
                if (item.Key.ToUpper().Equals(PK.ToUpper()))
                {
                    continue;
                }
                else
                {
                    Values += item.Key +" = " +"'" + item.Value + "',";
                }
            }
            if (Values.Length > 0)
            {
                Values = Values.Substring(0, Values.Length - 1);
            }
            foreach (var item in wheredic)
            {

                    WHERE += "  and "+ item.Key + " = " + "'" + item.Value + "'";
               
            }
            StrSql = StrSql.Replace("{WHERE}", WHERE).Replace("{VALUES}", Values);
            return ExecuteNonQuery(StrSql) > 0 ? true : false;

        }

    }
}
