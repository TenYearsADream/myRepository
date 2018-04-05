using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Sino.Common
{
    public class StrUtil
    {
        public static string Md5(string original)
        {
            var relStr = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(original))
                {
                    StringBuilder result;
                    ASCIIEncoding ae;
                    byte[] md5Data;
                    using (var md5Csp = new MD5CryptoServiceProvider())
                    {
                        result = new StringBuilder();
                        ae = new ASCIIEncoding();
                        md5Data = md5Csp.ComputeHash(ae.GetBytes(original));
                        for (int i = 0; i < md5Data.Length; i++)
                        {
                            result.AppendFormat("{0:x2}", md5Data[i]);
                        }
                    }
                    relStr = result.ToString();
                }
            }
            catch (Exception e)
            {
                
            }
            return relStr;
        }
    }
}
