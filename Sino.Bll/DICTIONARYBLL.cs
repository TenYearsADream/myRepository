using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sino.Model;
using Sino.DAL;
using System.Data;

namespace Sino.Bll
{
    public class DICTIONARYBLL
    {
        /// <summary>
        /// 获取所有省市
        /// </summary>
        /// <returns></returns>
        public DataSet GetDictionary(string DicName)
        {
            string StrSql = "SELECT [ID],[SYS_CODE],[PRO_NAME],[PRO_CODE],[PRO_CONTENT],[MEMO] FROM ST_DICTIONARY WHERE PRO_NAME ='" + DicName + "' order by PRO_CODE ";
            return SqlHelper.GetDataSet(StrSql);
        }
    }
}
