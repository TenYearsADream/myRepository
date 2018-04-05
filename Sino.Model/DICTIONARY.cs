using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sino.Model
{
    /// <summary>
    /// ST_DICTIONARY:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class DICTIONARY
    {
        public string ID { get; set; }
        public string SYS_CODE { get; set; }
        public string PRO_NAME { get; set; }
        public int? PRO_CODE { get; set; }
        public string PRO_CONTENT { get; set; }
        public string MEMO { get; set; }
    }
}
