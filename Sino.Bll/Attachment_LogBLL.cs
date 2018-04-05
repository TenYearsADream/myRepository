using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Sino.DAL;
using Sino.Model;

namespace Sino.Bll
{
    /// <summary>
    /// BT_Attachment_Log
    /// </summary>
    public partial class Attachment_LogBLL
    {
        /// <summary>
        /// 添加一条记录
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public bool Attachment_LogAdd(Attachment_Log Model)
        {
            return SqlHelper.Insertstr<Attachment_Log>(Model);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Sino.Model.Attachment_Log model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into BT_Attachment_Log(");
            strSql.Append("OperateTime,OperateBy,OperateContent,SetType");
            strSql.Append(")");
            strSql.Append(" values (");
            //strSql.Append(""+model.OID+",");
            strSql.Append("'" + model.OperateTime + "',");
            strSql.Append("'" + model.OperateBy + "',");
            strSql.Append("'" + model.OperateContent + "',");
            strSql.Append("'" + model.SetType + "'");
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
    }
}
