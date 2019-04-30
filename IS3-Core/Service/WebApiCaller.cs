using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace iS3.Core.Service
{
    public static class WebApiCaller
    {
        public static string HttpPost(string url, string body)
        {
            return CommonHttp(url, "POST", body);
        }
        public static string HttpPut(string url, string body)
        {
            return CommonHttp(url, "PUT", body);
        }
        public static string HttpDelete(string url, string body)
        {
            return CommonHttp(url, "DELETE", body);
        }
        public static string CommonHttp(string url, string operation, string body)
        {
            if (ServiceConfig.NeedCache)
            {
                //缓存判断
                if (iS3Cache.checkIfExist(url))
                {
                    if (iS3Cache.CheckIfLastet(url))
                    {
                        return iS3Cache.GetFromCache(url);
                    }
                }
            }
            try
            {
                //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                Encoding encoding = Encoding.UTF8;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = operation;
                //request.Accept = "application/json, text/javascript, */*"; //"text/html, application/xhtml+xml, */*";
                //request.ContentType = "application/json; charset=utf-8";
                request.ContentType = "application/x-www-form-urlencoded";
                //byte[] buffer = encoding.GetBytes(body);
                request.ContentLength = 0;
                //request.GetRequestStream().Write(buffer, 0, buffer.Length);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string result = null;
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), encoding))
                {
                    result= reader.ReadToEnd();
                }
                if (ServiceConfig.NeedCache)
                {
                    //缓存保存
                    iS3Cache.SaveToCache(url, result);
                }
                return result;
            }
            catch (WebException ex)
            {
                var res = (HttpWebResponse)ex.Response;
                StringBuilder sb = new StringBuilder();
                StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
                sb.Append(sr.ReadToEnd());
                //string ssb = sb.ToString();
                throw new Exception(sb.ToString());
            }
        }

        /// <summary>   
        /// GET Method  
        /// </summary>  
        /// <returns></returns>  
        public static string HttpGet(string url)
        {
            if (ServiceConfig.NeedCache)
            {
                //缓存判断
                if (iS3Cache.checkIfExist(url))
                {
                    if (iS3Cache.CheckIfLastet(url))
                    {
                        return iS3Cache.GetFromCache(url);
                    }
                }
            }
            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
                myRequest.Method = "GET";
                myRequest.Timeout = 4000;
                HttpWebResponse myResponse = null;

                myResponse = (HttpWebResponse)myRequest.GetResponse();
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                string content = reader.ReadToEnd();

                if (ServiceConfig.NeedCache)
                {
                    //缓存保存
                    iS3Cache.SaveToCache(url, content);
                }
                return content;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}