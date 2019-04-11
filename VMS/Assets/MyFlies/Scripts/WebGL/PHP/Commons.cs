using UnityEngine;
using System.Collections;
using System.Security.Cryptography;
using System;

namespace Commons
{
    /// <summary>
    /// 扩展类
    /// </summary>
    public static class Extension
    {
        /// <summary>
        /// 判断字符是否为空
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool isEmpty(this string str)
        {
            if (str.Equals(""))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class Common
    {
        /// <summary>
        /// 对字符进行MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string StrEncrypMd5(string str)
        {
            if (str == null)
            {
                return "加密字符不能为空";
            }

            MD5 md5 = new MD5CryptoServiceProvider();

            byte[] bytRes = System.Text.Encoding.Default.GetBytes(str); ;
            byte[] targetData = md5.ComputeHash(bytRes);
            string byte2String = BitConverter.ToString(targetData).Replace("-", ""); ;

            return byte2String.ToLower();
        }
    }
}
