using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public  class EncryptAndDecrypt
    {
        #region 加密与解密

        //加密解密用
        private static byte[] _arrBytDESKey = new byte[] { 11, 23, 93, 102, 72, 41, 18, 12 };
        private static byte[] _arrBytDESIV = new byte[] { 75, 158, 46, 97, 78, 57, 17, 36 };

        /// <summary>
        /// 加密处理
        /// </summary>
        /// <param name="p_strEncode">字符</param>
        /// <returns>加密字符</returns>
        public static string Encode(string p_strDecode)
        {
            DESCryptoServiceProvider objDES = new DESCryptoServiceProvider();
            MemoryStream objMemoryStream = new MemoryStream();
            CryptoStream objCryptoStream = new CryptoStream(objMemoryStream, objDES.CreateEncryptor(_arrBytDESKey, _arrBytDESIV), CryptoStreamMode.Write);
            StreamWriter objStreamWriter = new StreamWriter(objCryptoStream);
            objStreamWriter.Write(p_strDecode);
            objStreamWriter.Flush();
            objCryptoStream.FlushFinalBlock();
            objMemoryStream.Flush();
            return Convert.ToBase64String(objMemoryStream.GetBuffer(), 0, (int)objMemoryStream.Length);
        }

        /// <summary>
        /// 解密处理
        /// </summary>
        /// <param name="p_strEncode">加密字符</param>
        /// <returns>解密字符</returns>
        public  static string Decode(string p_strEncode)
        {
            DESCryptoServiceProvider objDES = new DESCryptoServiceProvider();
            byte[] input = Convert.FromBase64String(p_strEncode);
            MemoryStream objMemoryStream = new MemoryStream(input);
            CryptoStream objCryptoStream = new CryptoStream(objMemoryStream, objDES.CreateDecryptor(_arrBytDESKey, _arrBytDESIV), CryptoStreamMode.Read);
            StreamReader objStreamReader = new StreamReader(objCryptoStream);
            return objStreamReader.ReadToEnd();
        }

        #endregion
    }
}
