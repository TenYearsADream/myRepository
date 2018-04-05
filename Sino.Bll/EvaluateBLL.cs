using Sino.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sino.Bll
{
    public class EvaluateBLL
    {
        public bool Insert(Sino.Model.Evaluate Model)
        {
            return SqlHelper.Insertstr<Sino.Model.Evaluate>(Model);
        }
        public bool InserDataTable(System.Data.DataTable dt, string TableName)
        {
            return SqlHelper.sqlBulk(dt, TableName);
        }
    }
}
