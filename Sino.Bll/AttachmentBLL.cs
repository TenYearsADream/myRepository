using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sino.Dal;
using Sino.Model;
using System.Data;
using Sino.DAL;

namespace Sino.Bll
{
    public class AttachmentBLL
    {
        /// <summary>
        /// 添加一条记录
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public bool AttachmentAdd(Attachment Model)
        {
            return SqlHelper.Insertstr<Attachment>(Model);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Attachment model)
        {            
            //[ID],[dType],[language],[FileName],[OrderBy] ,[CreateDate],[CreateBy],[Suffix],[FtpPath],[FtpName],[KeyWords],[Contents]
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into BT_Attachment(");
            strSql.Append("[dType],[language],[FileName],[OrderBy] ,[CreateDate],[CreateBy],[Suffix],[FtpPath],[FtpName],[KeyWords],[Contents],[VirtualPath],[BatchNo],[Sort],[CheckNum],[Area],[BateSize],[filetag],[year]");
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append("'" + model.dType + "',");
            strSql.Append("'" + model.language + "',");
            strSql.Append("'" + model.FileName + "',");
            strSql.Append("'" + model.OrderBy + "',");

            strSql.Append("GETDATE(),");
            strSql.Append("'" + model.CreateBy + "',");
            strSql.Append("'" + model.Suffix + "',");
            strSql.Append("'" + model.FtpPath + "',");
            strSql.Append("'" + model.FtpName + "',");
            strSql.Append("'" + model.KeyWords + "',");
            strSql.Append("'" + model.Contents + "',");
            strSql.Append("'" + model.VirtualPath + "',");
            strSql.Append("'" + model.BatchNo + "',");
            strSql.Append("'" + model.Sort + "',");
            strSql.Append("'0',");
            strSql.Append("'" + model.area + "',");
            strSql.Append("'" + model.BateSize + "',");
            strSql.Append("'" + model.filetag + "',");
            strSql.Append("'" + model.year + "'");
            strSql.Append(")");
            if (SqlHelper.ExecuteNonQuery(strSql.ToString()) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ExistsByFileName(string name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(*) FROM BT_Attachment WHERE FileName='" + name + "'");
            if (int.Parse(SqlHelper.ExecuteScalar(strSql.ToString()).ToString()) >0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public DataTable GetListID(DateTime StartDate, string CreateBy)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ID FROM BT_Attachment where CreateDate > '" + StartDate + "' and CreateBy='" + CreateBy + "'");
            return SqlHelper.GetDataTable(strSql.ToString());
        }
    }
}
