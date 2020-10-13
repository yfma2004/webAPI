using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CommonUtitlity
{
    public class CommonHelper
    {
        /// <summary>
        /// MD5加密字符串（32位大写）
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string GetMD5(string source)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(source);
            string result = BitConverter.ToString(md5.ComputeHash(bytes));
            return result.Replace("-", "");
        }


        /// <summary>
        /// 密码加密
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string GetPwdCryptoStr(string pwd)
        {
            string str = "erpERP" + pwd;
            for (int i = 0; i < 3; i++)
            {
                str= CommonHelper.GetMD5(str);
            }
            return str;
        }



    }
}
