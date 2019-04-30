using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace iS3.Core
{
    public class APIMessageConverter
    {
        //用于解决URL地址中无法传输特殊符号问题，解码--16进制，-分割
        public static string Decode(string rawStr)
        {
            if (rawStr == null) return "";
            string result = "";
            rawStr.Split('-').ToList().ForEach(x => result += Char.ConvertFromUtf32(Convert.ToInt32(x, 16)));
            return result;
        }
        //加密
        public static string EnCode(string rawStr)
        {
            if (rawStr == null) return "";
            List<string> result = new List<string>();
            rawStr.ToList().ForEach(x => result.Add(String.Format("{0:X}", Convert.ToInt32(x))));
            return string.Join("-", result);
        }
    }
}