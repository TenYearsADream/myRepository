using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SinoSoft.Common
{
    using System;
    using System.Data;
    using System.Data.OleDb;
    /// <summary>
    /// Excel数据操作
    /// </summary>
    public class ExcelPro
    {
        /// <summary>
        /// 读取指定Excel文件中的数据到DataTable
        /// </summary>
        /// <param name="str_File">指定Excel文件</param>
        /// <returns></returns>
        public static DataTable GetExcelDataTable(string str_File)
        {
            string conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + str_File + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1;\"";
            string format = "select * from [{0}]";
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = conStr;
            DataTable dataTable = new DataTable();
            connection.Open();
            string str_tableName = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0][2].ToString();
            if ((str_tableName != string.Empty) && (str_tableName.Trim() != ""))
            {
                OleDbCommand selectCommand = new OleDbCommand(string.Format(format, str_tableName), connection);
                OleDbDataAdapter adapter = new OleDbDataAdapter(selectCommand);
                adapter.Fill(dataTable);
                dataTable.AcceptChanges();
                adapter.InsertCommand = new OleDbCommand();
                adapter.UpdateCommand = new OleDbCommand();
                adapter.Update(dataTable);
            }
            connection.Close();
            return dataTable;
        }
    }
}
