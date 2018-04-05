using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sino.Model
{
    /// <summary>
    /// BT_Attachment_Log
    /// </summary>
    public class Attachment_Log
    {
        public int ID { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime? OperateTime { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string OperateBy { get; set; }
        /// <summary>
        /// 操作内容
        /// </summary>
        public string OperateContent { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string SetType { get; set; }
    }
}
