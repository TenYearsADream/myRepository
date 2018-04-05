using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sino.Model
{
    /// <summary>
    /// BT_Attachment 实例
    /// </summary>
    public class Attachment
    {
        public string ID { get; set; }
        /// <summary>
        /// 档案类型
        /// </summary>
        public string dType { get; set; }
        /// <summary>
        /// 语种
        /// </summary>
        public string language { get; set; }
        /// <summary>
        /// 领域
        /// </summary>
        public string area { get; set; }
        /// <summary>
        /// 文件名称（不含后缀）
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 所有人，作者
        /// </summary>
        public string OrderBy { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        /// 后缀
        /// </summary>
        public string Suffix { get; set; }
        /// <summary>
        /// 存放路径
        /// </summary>
        public string FtpPath { get; set; }
        /// <summary>
        /// 存放名称
        /// </summary>
        public string FtpName { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public string KeyWords { get; set; }
        /// <summary>
        /// 索引内容
        /// </summary>
        public string Contents { get; set; }
        /// <summary>
        /// 上传路径
        /// </summary>
        public string VirtualPath { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public string BatchNo { get; set; }
        /// <summary>
        /// 阅读量，评价量
        /// </summary>
        public int? CheckNum { get; set; }
        /// <summary>
        /// 文件所在阶层
        /// </summary>
        public int? Sort { get; set; }
        /// <summary>
        /// 标识，0：文件1：视频
        /// </summary>
        public string filetag { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public decimal BateSize { get; set; }
        /// <summary>
        /// 年度
        /// </summary>
        public int? year { get; set; }
    }
}