using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace iS3.Core.Service
{
    public class iS3Cache
    {

        /// <summary>
        /// 缓存是否存在判断
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool checkIfExist(string url)
        {
            string path = Runtime.dataPath + "\\temp\\" + GenerateMD5(url) + ".json";
            if (File.Exists(path))
            { return true; }
            else
            { return false; }
        }
        /// <summary>
        /// 判断是否需要更新
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool CheckIfLastet(string url)
        {
            return true;
        }
        /// <summary>
        /// 读取缓存内容
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetFromCache(string url)
        {
            string path = Runtime.dataPath + "\\temp\\" + GenerateMD5(url) + ".json";
            {
                StreamReader sr = new StreamReader(path, Encoding.Default);
                string line = sr.ReadToEnd();
                return line;
            }
        }

        public static void SaveToCache(string url, string content)
        {
            FileStream fs = new FileStream(Runtime.dataPath + "\\temp\\" + GenerateMD5(url) + ".json", FileMode.Create);
            //获得字节数组
            byte[] data = System.Text.Encoding.Default.GetBytes(content);
            //开始写入
            fs.Write(data, 0, data.Length);
            //清空缓冲区、关闭流
            fs.Flush();
            fs.Close();
        }
        /// <summary>
        /// method to generate a MD5 hash of a string
        /// </summary>
        /// <param name="strToHash">string to hash</param>
        /// <returns>hashed string</returns>
        public static string GenerateMD5(string str)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] byteArray = Encoding.ASCII.GetBytes(str);
            byteArray = md5.ComputeHash(byteArray);
            string hashedValue = "";
            foreach (byte b in byteArray)
            {
                hashedValue += b.ToString("x2");
            }
            return hashedValue;
        }
    }
}