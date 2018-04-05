using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SinoSoft.Common.Upload
{
    /// <summary>
    ///UploadTools 的摘要说明
    /// </summary>
    public class UploadTools
    {
        public UploadTools()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        #region 文件上传信息

        #region 文件上传名称
        /// <summary>
        /// 获取文件上传信息中的文件名称
        /// </summary>
        /// <param name="str_Values">上传文件返回值</param>
        /// <returns></returns>
        public static string Upload_FileName(string str_Values)
        {
            // 返回格式：<[文件名称]><[文件长度]><[临时文件名称]>
            if (str_Values != "" && str_Values != null)
            {
                string FileName = str_Values;
                FileName = FileName.Remove(0, 2);
                FileName = FileName.Substring(0, FileName.IndexOf("]><["));

                return FileName;
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region 临时文件名称
        /// <summary>
        /// 临时文件名称
        /// </summary>
        /// <param name="str_Values">上传文件返回值</param>
        /// <returns></returns>
        public static string Upload_TempFileName(string str_Values)
        {
            // 返回格式：<[文件名称]><[文件长度]><[临时文件名称]><[系统日期]>
            if (str_Values != "" && str_Values != null)
            {
                string TempFileName = str_Values;
                TempFileName = TempFileName.Remove(0, TempFileName.IndexOf("]><[") + 4);
                TempFileName = TempFileName.Remove(0, TempFileName.IndexOf("]><[") + 4);

                TempFileName = TempFileName.Substring(0, TempFileName.IndexOf("]><["));

                return TempFileName;
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region 文件长度
        /// <summary>
        /// 文件长度
        /// </summary>
        /// <param name="str_Values">上传文件返回值</param>
        /// <returns></returns>
        public static string Upload_Filelength(string str_Values)
        {
            // 返回格式：<[文件名称]><[文件长度]><[临时文件名称]><[系统日期]>
            if (str_Values != "" && str_Values != null)
            {
                string Filelength = str_Values;
                Filelength = Filelength.Remove(0, Filelength.IndexOf("]><[") + 4);

                Filelength = Filelength.Substring(0, Filelength.IndexOf("]><["));

                return Filelength;
            }
            else
            {
                return "";
            }
        }
        #endregion

        #endregion
    }
}
