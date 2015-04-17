using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Aliyun
{
    /// <summary>
    /// Html工具
    /// </summary>
    public class Utils
    {
        /// <summary>
        /// 根据Url获取html内容
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="method">Http方法</param>
        /// <returns>html内容</returns>
        public static string GetUrlHtmlContent(string url, string method = "GET")
        {
            return GetUrlHtmlContent(url, (HttpVerb)Enum.Parse(typeof(HttpVerb), method.ToUpper()));
        }
        /// <summary>
        /// 根据Url获取html内容
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="method">Http方法</param>
        /// <returns>html内容</returns>
        public static string GetUrlHtmlContent(string url, HttpVerb method)
        {
            using (HttpWebResponse res = GetUrlResponse(url, method))
            {
                using (StreamReader reader = new StreamReader(res.GetResponseStream(), Encoding.UTF8))
                {
                    string html = reader.ReadToEnd();
                    return html;
                }
            }
        }
        /// <summary>
        /// 获取http响应内容
        /// </summary>
        /// <param name="res">Http响应消息</param>
        /// <returns>html内容</returns>
        public static string GetUrlHtmlContent(HttpWebResponse res)
        {
            using (StreamReader reader = new StreamReader(res.GetResponseStream(), Encoding.UTF8))
            {
                string html = reader.ReadToEnd();
                return html;
            }
        }
        /// <summary>
        /// 根据Url获取http响应
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="method">Http方法</param>
        /// <returns>Http响应消息</returns>
        public static HttpWebResponse GetUrlResponse(string url, string method = "GET")
        {
            return GetUrlResponse(url, (HttpVerb)Enum.Parse(typeof(HttpVerb), method.ToUpper()));
        }
        /// <summary>
        /// 根据Url获取http响应
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="method">Http方法</param>
        /// <returns>Http响应消息</returns>
        public static HttpWebResponse GetUrlResponse(string url, HttpVerb method)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = method.ToString();
            req.ContentType = "application/json;charset=UTF-8";//text/plain; charset=utf-8
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            return res;
        }
        /// <summary>
        /// Socket方式获取url指向的html内容
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="method">Http方法</param>
        /// <returns>html内容</returns>
        public static string GetUrlHtmlContentBySocket(string url, string method = "GET")
        {
            return GetUrlHtmlContentBySocket(url, (HttpVerb)Enum.Parse(typeof(HttpVerb), method.ToUpper()));
        }
        /// <summary>
        /// Socket方式获取url指向的html内容
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="method">Http方法</param>
        /// <returns>html内容</returns>
        public static string GetUrlHtmlContentBySocket(string url, HttpVerb method)
        {
            using (TcpClient client = new TcpClient())
            {
                Uri uri = new Uri(url);
                client.Connect(uri.Host, uri.Port);
                byte[] buff = GetRequestHeaders(uri, method);
                client.Client.Send(buff);
                using (NetworkStream stream = client.GetStream())
                {
                    byte[] bytes = new byte[1048576];
                    int size = stream.Read(bytes, 0, bytes.Length);
                    string html = Encoding.UTF8.GetString(bytes, 0, size);
                    return html;
                }
            }
        }
        /// <summary>
        /// 生成Http请求消息头
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        private static byte[] GetRequestHeaders(Uri uri, HttpVerb method)
        {
            WebHeaderCollection webheaders = new WebHeaderCollection();
            webheaders.Add("Host", uri.Host);
            webheaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            webheaders.Add("Accept-Language", "zh-CN,zh;q=0.8,en-US;q=0.5,en;q=0.3");
            webheaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:37.0) Gecko/20100101 Firefox/37.0");

            StringBuilder header = new StringBuilder();
            header.AppendFormat("{0} {1} HTTP/1.1\r\n", method, uri.PathAndQuery);
            foreach (string key in webheaders)
            {
                header.AppendFormat("{0}:{1}\r\n", key, webheaders[key]);
            }
            header.Append("\r\n");
            webheaders.Clear();
            return Encoding.UTF8.GetBytes(header.ToString());
        }
    }
    /// <summary>
    /// Http方法
    /// </summary>
    public enum HttpVerb
    {
        /// <summary>
        /// Get方法
        /// </summary>
        GET,
        /// <summary>
        /// Post方法
        /// </summary>
        POST
    }
}
