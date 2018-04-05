using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace SinoSoft.Common
{
    /// <summary>
    /// 加密
    /// </summary>
    /// <!--
    /// 创建人  : ZhouYingl
    /// 创建时间: 2011-09-08
    /// -->
    public sealed class Encode
    {
        /// <summary>
        /// 私有构造函数，防止用new实例化类
        /// </summary>
        /// <!--
        /// 创建人  : ZhouYingl
        /// 创建时间: 2011-09-08
        /// -->
        private Encode()
        {
        }

        /// <summary>
        /// 使用Md5算法加密
        /// </summary>
        /// <param name="original">原文</param>
        /// <returns>Md5加密后密文</returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2009-02-28
        /// -->
        public static string Md5(string original)
        {
            if (!string.IsNullOrEmpty(original))
            {
                StringBuilder result;
                ASCIIEncoding ae;
                byte[] md5data;

                using (MD5CryptoServiceProvider md5CSP = new MD5CryptoServiceProvider())
                {
                    result = new StringBuilder();
                    ae = new ASCIIEncoding();
                    md5data = md5CSP.ComputeHash(ae.GetBytes(original));

                    for (int i = 0; i < md5data.Length; i++)
                    {
                        result.AppendFormat("{0:x2}", md5data[i]);
                    }
                }

                return result.ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// 解密函数
        /// </summary>
        /// <param name="deCode ">待加密字符串</param>
        /// <param name="key">加密关键字</param>
        /// <returns>加密后的字符串</returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2009-02-28
        /// -->
        public static string Deciphering(string deCode, string key)
        {
            //DESCryptoServiceProvider DESe = new DESCryptoServiceProvider();
            //int sL = 0;
            //double deStrLong = 0;
            //if (!string.IsNullOrEmpty(deCode))
            //{
            //    if (deCode.IndexOf("-") < 0)
            //    {
            //        return String.Empty;
            //    }
            //    deStrLong = Microsoft.VisualBasic.Conversion.Val(deCode.Substring(deCode.IndexOf("-") + 1, deCode.Length - deCode.IndexOf("-") - 1));
            //    deCode = deCode.Substring(0, deCode.IndexOf("-"));
            //    sL = deCode.Length;
            //    Byte[] inputBuffer = new Byte[sL];
            //    MemoryStream S = new MemoryStream(inputBuffer, true);
            //    Byte[] TheKey = { };
            //    Byte[] Vector = { 0x12, 0x44, 0x16, 0xEE, 0x88, 0x15, 0xDD, 0x41 };
            //    PasswordDeriveBytes Pa = new PasswordDeriveBytes(SHA1HashKey(key), TheKey);
            //    TheKey = Pa.CryptDeriveKey("DES", "SHA1", 0, Vector);
            //    CryptoStream deStream = new CryptoStream(S, DESe.CreateDecryptor(TheKey, Vector), CryptoStreamMode.Write);

            //    Byte[] Storage = new Byte[sL];
            //    for (int i = 0; i < deCode.Length / 2; i++)
            //    {
            //        string test = deCode.Substring(i * 2, 2).ToString();
            //        Storage[i] = Convert.ToByte(Microsoft.VisualBasic.Conversion.Val("&H" + test));
            //    }
            //    deStream.Write(Storage, 0, sL);
            //    StringBuilder deString = new StringBuilder();

            //    for (int j = 0; j < inputBuffer.Length; j++)
            //    {
            //        deString.Append(Microsoft.VisualBasic.Strings.Chr(inputBuffer[j]));
            //    }
            //    return deString.ToString().Substring(0, Convert.ToInt32(deStrLong));
            //}
            //else
            //{
            //    return string.Empty;
            //}
            return string.Empty;
        }

        /// <summary>
        /// 哈希值
        /// </summary>
        /// <param name="key">加密关键字</param>
        /// <returns>哈希值</returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2009-02-28
        /// -->
        public static string SHA1HashKey(string key)
        {
            //using (SHA1CryptoServiceProvider SHA1 = new SHA1CryptoServiceProvider())
            //{
            //    ASCIIEncoding ascEncod = new ASCIIEncoding();
            //    Byte[] inputBuffer = { };
            //    Byte[] outBuffer = { };
            //    inputBuffer = ascEncod.GetBytes(key);
            //    outBuffer = SHA1.ComputeHash(inputBuffer);
            //    StringBuilder keyString = new StringBuilder();
            //    for (int i = 0; i < 20; i++)
            //    {
            //        keyString.Append(Microsoft.VisualBasic.Conversion.Hex(outBuffer[i]).ToString().PadLeft(2, '0'));
            //    }
            //    return keyString.ToString();
            //}
            return string.Empty;
        }

        /// <summary>
        /// DES加密方法
        /// </summary>
        /// <param name="valueToEncrypt">需要加密的值。</param>
        /// <param name="key">密钥。</param>
        /// <returns>密文。</returns>
        public static string DesEncrypt(string valueToEncrypt, string key)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            //把字符串放到byte数组中 ，原来使用的UTF8编码，我改成Unicode编码了，不行  

            byte[] inputByteArray = Encoding.Default.GetBytes(valueToEncrypt);

            //建立加密对象的密钥和偏移量 ，原文使用ASCIIEncoding.ASCII方法的GetBytes方法 ，使得输入密码必须输入英文文本  
            //而且sKey最好为8位或者偶数位
            des.Key = ASCIIEncoding.ASCII.GetBytes(key);

            des.IV = ASCIIEncoding.ASCII.GetBytes(key);

            MemoryStream ms = new MemoryStream();

            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);

            cs.Write(inputByteArray, 0, inputByteArray.Length);

            cs.FlushFinalBlock();

            StringBuilder ret = new StringBuilder();

            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }

            ret.ToString();

            return ret.ToString();
        }

        /// <summary>
        /// DES解密方法
        /// </summary>
        /// <param name="valueToDecrypt">需要解密的值</param>
        /// <param name="key">密钥。</param>
        /// <returns>明文。</returns>
        public static string DesDecrypt(string valueToDecrypt, string key)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            //Put  the  input  string  into  the  byte  array  
            byte[] inputByteArray = new byte[valueToDecrypt.Length / 2];

            for (int x = 0; x < valueToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(valueToDecrypt.Substring(x * 2, 2), 16));

                inputByteArray[x] = (byte)i;
            }

            //建立加密对象的密钥和偏移量，此值重要，不能修改  

            des.Key = ASCIIEncoding.ASCII.GetBytes(key);

            des.IV = ASCIIEncoding.ASCII.GetBytes(key);

            MemoryStream ms = new MemoryStream();

            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);

            //Flush  the  data  through  the  crypto  stream  into  the  memory  stream  

            cs.Write(inputByteArray, 0, inputByteArray.Length);

            cs.FlushFinalBlock();

            //Get  the  decrypted  data  back  from  the  memory  stream  

            //建立StringBuild对象，CreateDecrypt使用的是流对象，必须把解密后的文本变成流对象  

            //StringBuilder ret = new StringBuilder();

            return System.Text.Encoding.Default.GetString(ms.ToArray());
        }
    }
}
