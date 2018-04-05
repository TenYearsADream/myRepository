using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;

namespace Sino.DAL
{
    public partial class SqlHelper
    {

        #region 返回实体 （私有方法）
        /// <summary>
        /// 根据DataRow获取实体(内部使用)
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="Row">行</param>
        /// 2012 -11-4 DHAWD
        /// <returns>有结果的实体</returns>
        private static T GetModel<T>(DataRow Row) where T : new()
        {
            if (Row == null)
            {
                return default(T);
            }
            T t = new T();
            Type type = t.GetType();
            PropertyInfo[] properties = type.GetProperties();
            foreach (DataColumn Column in Row.Table.Columns)
            {
                var tmpPropQuery = from item in properties
                                   where item.Name.ToUpper() == Column.ColumnName.ToUpper()
                                   select item;
                if (tmpPropQuery.Count() > 0)
                {
                    PropertyInfo tmpProp = tmpPropQuery.FirstOrDefault();
                    if (tmpProp != null && tmpProp.CanWrite)
                    {
                        object o = Row[Column.ColumnName] != DBNull.Value && Row[Column.ColumnName] != null ?
                            Row[Column.ColumnName] : null;

                        object[] paras = { o };
                        tmpProp.ReflectedType.InvokeMember(tmpProp.Name, BindingFlags.SetProperty, null, t, paras);
                    }
                }
            }
            return t;

        }

        /// <summary>
        /// 根据IDataReader获取实体（内部使用）
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="Reader">IDataReader</param>
        /// 2012 -11-4 DHAWD
        /// <returns>有结果的实体</returns>
        private static T GetModel<T>(IDataReader Reader) where T : new()
        {
            if (Reader == null)
            {
                return default(T);
            }
            T t = new T();
            Type type = t.GetType();
            PropertyInfo[] properties = type.GetProperties();
            List<string> ReaderNameList = new List<string>(Reader.FieldCount);
            for (int i = 0; i < Reader.FieldCount; i++)
            {
                ReaderNameList.Add(Reader.GetName(i).ToUpper());
            }
            foreach (PropertyInfo Info in type.GetProperties())
            {
                if (ReaderNameList.Contains(Info.Name.ToUpper()))
                {
                    if (Info.CanWrite)
                    {
                        object o = Reader[Info.Name] != DBNull.Value && Reader[Info.Name] != null ?
                            Reader[Info.Name] : null;

                        object[] paras = { o };
                        Info.ReflectedType.InvokeMember(Info.Name, BindingFlags.SetProperty, null, t, paras);
                    }
                }
            }            
            return t;

        }

        #endregion

        #region 返回实体 （公共方法）

        /// <summary>
        /// 根据DataTable获取实体
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="Table">数据表格</param>
        /// 2012 -11-4 DHAWD
        /// <returns></returns>
        public static T GetModels<T>(IDataReader DataReader) where T : new()
        {
            try
            {
                T Model = new T();
                while (DataReader.Read())
                {
                    Model = GetModel<T>(DataReader);
                }
                ReaderClose(DataReader);

                return Model;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }

        }

        /// <summary>
        /// 根据条件实体获取实体数据
        /// </summary>
        /// <typeparam name="T">要获取的实体</typeparam>
        /// <returns></returns>
        public static T GetModels<T>(T Model) where T : new()
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
                    if (null != Info.GetValue(Model, null))
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
                return GetModels<T>(ExecuteReader(StrSQL));
            }
            catch (Exception EX)
            {

                throw new Exception(EX.Message, EX);
            }
        }

        /// <summary>
        /// 根据条件获取实体数据
        /// </summary>
        /// <typeparam name="T">要获取的实体</typeparam>
        /// <returns></returns>
        public static T GetModels<T>(string WHERE) where T : new()
        {
            string StrSQL = string.Empty;
            string FAILEDS = string.Empty;
            try
            {
                T t = new T();
                Type type = t.GetType();
                StrSQL = SQL_SELECT.Replace("{TABLE_NAME}", type.Name);
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
                //判断条件是否为空
                if (!string.IsNullOrWhiteSpace(WHERE))
                {
                    StrSQL = StrSQL.Replace("{VALUES}", WHERE);
                }
                else 
                {
                    StrSQL = StrSQL.Replace("{VALUES}", " ");
                }
               
                return GetModel<T>(ExecuteReader(StrSQL));
            }
            catch (Exception EX)
            {

                throw new Exception(EX.Message, EX);
            }
        }

        #endregion

    }
}
