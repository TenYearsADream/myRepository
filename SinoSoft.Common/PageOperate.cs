using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SinoSoft.Common
{
    /// <summary>
    /// 数据库一些基本操作
    /// </summary>
    public class PageOperate
    {
        /// <summary>
        /// 构建排序、分页Sql。
        /// </summary>
        /// <param name="oldSql">原始Sql语句。</param>
        /// <param name="sortColumns">排序列（具有后跟 "ASC" 或 "DESC" 的逗号分隔字段值的形式）。</param>
        /// <param name="startRowIndex">起始行。</param>
        /// <param name="maximumRows">行数。</param>
        /// <returns>组建好的排序、分页Sql。</returns>
        /// <!--
        /// 创建人  : zhouyinglu
        /// 创建时间: 2011-04-06
        /// -->
        public static string BuildSortPageSql(string oldSql, string sortColumns, int startRowIndex, int maximumRows)
        {
            if (!string.IsNullOrEmpty(oldSql))
            {
                if (startRowIndex < 0)
                {
                    startRowIndex = 0;
                }
                //  StringBuilder sqlBuilder = new StringBuilder("Select distinct * From (Select temp.*,rownum rn From (");
                StringBuilder sqlBuilder = new StringBuilder("Select");
                sqlBuilder.AppendFormat(" top {0}", maximumRows);
                sqlBuilder.AppendFormat(" * from (SELECT ROW_NUMBER() OVER (ORDER BY OId) AS RowNumber,* FROM " + oldSql + ") A where RowNumber < {0}", maximumRows);
                // sqlBuilder.AppendFormat(oldSql);
                //if (!string.IsNullOrEmpty(sortColumns))
                //{
                //    sqlBuilder.AppendFormat(" Order By {0}", sortColumns);//翻页排序
                //}
                //sqlBuilder.Append(") temp ");
                //if (maximumRows > 0)
                //{
                //    sqlBuilder.AppendFormat("Where rownum <= {0}", startRowIndex + maximumRows);
                //}
                //sqlBuilder.AppendFormat(") Where rn > {0} Order By rn", startRowIndex);
                return sqlBuilder.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 构建排序、分页Sql。MSSQL分页
        /// </summary>
        /// <param name="oldSql">原始Sql语句。</param>
        /// <param name="sortColumns">排序列（具有后跟 "ASC" 或 "DESC" 的逗号分隔字段值的形式）。</param>
        /// <param name="startRowIndex">起始行。</param>
        /// <param name="maximumRows">行数。</param>
        /// <param name="maximumRows">总数。</param>
        /// <returns>组建好的排序、分页Sql。</returns>
        /// 
        /// <!--
        /// 创建人  : zhouyinglu
        /// 创建时间: 2011-04-06
        /// -->
        public static string BuildSortPageMSSql(string oldSql, string sortColumns, int startRowIndex, int maximumRows, int TotalCnt)
        {
            if (!string.IsNullOrEmpty(oldSql))
            {
                if (startRowIndex < 0)
                {
                    startRowIndex = 0;
                }
                StringBuilder sqlBuilder = new StringBuilder();

                sqlBuilder.AppendFormat("Select top {0} * From (", maximumRows);

                if (maximumRows > 0)
                {
                    if (TotalCnt - startRowIndex > 0)
                    {
                        sqlBuilder.AppendFormat("Select top {0} * From (", TotalCnt - startRowIndex);
                    }
                    else
                    {
                        sqlBuilder.AppendFormat("Select top {0} * From (", startRowIndex - TotalCnt);
                       
                    }
                }
                sqlBuilder.Append(oldSql);

                sqlBuilder.Append(") temp ");

                if (!string.IsNullOrEmpty(sortColumns) && sortColumns.IndexOf("desc") > 0)
                {
                    string sortColumn = sortColumns.Replace("desc", " asc ");
                    sqlBuilder.AppendFormat(" Order By {0}", sortColumn);//翻页排序
                }
                else
                {
                    string sortColumn = sortColumns.Replace("asc", " desc ");
                    sqlBuilder.AppendFormat(" Order By {0}", sortColumn);//翻页排序
                }

                sqlBuilder.Append(") cc ");

                if (!string.IsNullOrEmpty(sortColumns))
                {

                    sqlBuilder.AppendFormat(" Order By {0} ", sortColumns);//翻页排序
                }

                return sqlBuilder.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

    }
}
