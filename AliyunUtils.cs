using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace Aliyun
{
    /// <summary>
    /// 响应格式
    /// </summary>
    public enum ResponseFormat
    {
        XML,
        JSON
    }

    /// <summary>
    /// 阿里云工具
    /// </summary>
    public class AliyunUtils
    {
        private static AliyunRequest AliyunRequest = new AliyunRequest();
        /// <summary>
        /// 初始化请求
        /// </summary>
        /// <param name="AliyunRequest">请求</param>
        public static void Init(AliyunRequest AliyunRequest)
        {
            AliyunUtils.AliyunRequest = AliyunRequest;
        }
        /// <summary>
        /// 生成阿里云URL
        /// </summary>
        /// <param name="request">请求参数</param>
        /// <returns>生成的URL</returns>
        public static string GeneralURL(AliyunRequest request)
        {
            //第一步、创建请求参数
            SortedDictionary<string, string> _parameters = new SortedDictionary<string, string>(StringComparer.Ordinal); //区分大小写排序，这个问题开始卡我半天
            _parameters.Add("Timestamp", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"));
            _parameters.Add("SignatureNonce", _makeNonceAliyun());

            Dictionary<string, string> _params = request.GeneralParameters();
            if (_params != null)
            {
                foreach (KeyValuePair<string, string> param in _params)
                {
                    _parameters.Add(param.Key, param.Value);
                }
            }

            string q = _httpBuildQuery(_parameters);
            string Signature = _makeSignAliyun(request, _parameters);
            string url = q + "&Signature=" + Signature;

            //第二步、生成请求URL
            url = AliyunRequest.Base_Url + "?" + url;
            return url;
        }
        /// <summary>
        /// 生成阿里云URL
        /// </summary>
        /// <param name="parameters">请求参数</param>
        /// <returns>生成的URL</returns>
        public static string GeneralURL(Dictionary<string, string> parameters)
        {
            //第一步、创建请求参数
            SortedDictionary<string, string> _parameters = new SortedDictionary<string, string>(StringComparer.Ordinal); //区分大小写排序，这个问题开始卡我半天
            _parameters.Add("Format", AliyunRequest.Format.ToString());
            _parameters.Add("Version", AliyunRequest.Version);
            _parameters.Add("AccessKeyId", AliyunRequest.AccessKeyId);
            _parameters.Add("SignatureMethod", AliyunRequest.SignatureMethod);
            _parameters.Add("Timestamp", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"));
            _parameters.Add("SignatureVersion", AliyunRequest.SignatureVersion);
            _parameters.Add("SignatureNonce", _makeNonceAliyun());
            if (parameters != null)
            {
                foreach (KeyValuePair<string, string> param in parameters)
                {
                    _parameters.Add(param.Key, param.Value);
                }
            }
            string q = _httpBuildQuery(_parameters);
            string Signature = _makeSignAliyun(_parameters);
            string url = q + "&Signature=" + Signature;

            //第二步、生成请求URL
            url = AliyunRequest.Base_Url + "?" + url;
            return url;
        }
        /// <summary>
        /// 解析阿里云响应的html内容
        /// </summary>
        /// <param name="html">响应的html内容</param>
        /// <param name="format">html响应主体内容格式</param>
        /// <returns>阿里云html响应内容</returns>
        public static AliyunHtmlResponse GetHtmlResponse(string html, ResponseFormat format)
        {
            AliyunHtmlResponse res = new AliyunHtmlResponse(html, format);
            return res;
        }
        /// <summary>
        /// 解析阿里云响应的html内容
        /// </summary>
        /// <param name="html">响应的html内容</param>
        /// <returns>阿里云html响应内容</returns>
        public static AliyunHtmlResponse GetHtmlResponse(string html)
        {
            AliyunHtmlResponse res = new AliyunHtmlResponse(html, AliyunRequest.Format);
            return res;
        }
        /// <summary>
        /// 获取阿里云响应
        /// </summary>
        /// <param name="request">请求参数</param>
        /// <returns>阿里云响应</returns>
        public static AliyunResponse GetResponse(AliyunRequest request)
        {
            string url = GeneralURL(request);
            string html = Utils.GetUrlHtmlContentBySocket(url, request.Http_Method);
            AliyunHtmlResponse resp = GetHtmlResponse(html, request.Format);
            if (resp.StatusCode == System.Net.HttpStatusCode.OK)
            {
                AliyunResponse iresp = _analyzeResponse(request.Action, request.Format, resp.Content);
                return iresp;
            }
            else
                return resp.ErrorMessage;
        }
        /// <summary>
        /// 获取阿里云响应
        /// </summary>
        /// <param name="type">Action类型</param>
        /// <param name="parameters">请求参数</param>
        /// <returns>阿里云响应</returns>
        public static AliyunResponse GetResponse(ActionType type, Dictionary<string, string> parameters)
        {
            parameters.Add("Action", type.ToString());
            string url = GeneralURL(parameters);
            string html = Utils.GetUrlHtmlContentBySocket(url, AliyunRequest.Http_Method);
            AliyunHtmlResponse resp = GetHtmlResponse(html, AliyunRequest.Format);
            if (resp.StatusCode == HttpStatusCode.OK)
            {
                AliyunResponse iresp = _analyzeResponse(type, AliyunRequest.Format, resp.Content);
                return iresp;
            }
            else
                return resp.ErrorMessage;
        }
        /// <summary>
        /// 解析html正文内容
        /// </summary>
        /// <param name="type">Action类型</param>
        /// <param name="format">html响应主体内容格式</param>
        /// <param name="content">正文内容</param>
        /// <returns>阿里云响应</returns>
        private static AliyunResponse _analyzeResponse(ActionType type, ResponseFormat format, string content)
        {
            string info = null;
            if (format == ResponseFormat.JSON)
                info = content;
            else
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(content);
                string _info = JsonConvert.SerializeXmlNode(doc.DocumentElement);
                string start = ("{\"" + string.Format("{0}Response", type) + "\":");
                info = _info.Substring(start.Length, _info.Length - start.Length - 1);
            }
            return _analyzeJson(type, info);
        }
        /// <summary>
        /// 分析JSON响应结果
        /// </summary>
        /// <param name="action">Action类型</param>
        /// <param name="content">内容</param>
        /// <returns>阿里云响应</returns>
        private static AliyunResponse _analyzeJson(ActionType action, string content)
        {
            Type responseType;
            Type requestType;
            if (Utilities.GetType(action, out responseType, out requestType))
            {
                AliyunResponse iresp = JsonConvert.DeserializeObject(content, responseType) as AliyunResponse;
                return iresp;
            }
            return null;
        }
        /// <summary>
        /// 获取错误消息
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public static ErrorMessage GetErrorMessage(ErrorResponse error)
        {
            return ErrorMessage.GetMessage(error);
        }

        /// <summary>
        /// 请求唯一随机数，用于防止网络重放攻击。建议使用13位时间毫秒数+4位随机数。
        /// </summary>
        /// <returns></returns>
        private static string _makeNonceAliyun()
        {
            long stamp = _getUnixTimeStamp();
            Random ra = new Random();
            int rand = ra.Next(1000, 9999);
            StringBuilder sb = new StringBuilder();
            sb.Append(stamp);
            sb.Append(rand);
            return sb.ToString();
        }

        /// <summary>
        /// 获取13位unix timestamp
        /// </summary>
        /// <returns></returns>
        private static long _getUnixTimeStamp()
        {
            TimeSpan span = DateTime.UtcNow - (new DateTime(1970, 1, 1));
            return Convert.ToInt64(span.TotalMilliseconds);
        }

        private static string _makeSignAliyun(SortedDictionary<String, String> parameters)
        {
            //第一步、使用请求参数构造规范化的请求字符串
            string q = _httpBuildQuery(parameters);

            //第二步：使用上一步构造的规范化字符串按照下面的规则构造用于计算签名的字符串。
            string StringToSign = AliyunRequest.Http_Method.ToString() + "&%2F&" + _urlEncodeX(q);

            //第三步、按照RFC2104的定义，使用上面的用于签名的字符串计算签名HMAC值。
            string Signature = _hmacSha1Sign(StringToSign, AliyunRequest.Access_Key_Secret + "&");

            Signature = _urlEncodeX(Signature);
            return Signature;
        }

        private static string _makeSignAliyun(AliyunRequest request, SortedDictionary<String, String> parameters)
        {
            //第一步、使用请求参数构造规范化的请求字符串
            string q = _httpBuildQuery(parameters);

            //第二步：使用上一步构造的规范化字符串按照下面的规则构造用于计算签名的字符串。
            string StringToSign = request.Http_Method.ToString() + "&%2F&" + _urlEncodeX(q);

            //第三步、按照RFC2104的定义，使用上面的用于签名的字符串计算签名HMAC值。
            string Signature = _hmacSha1Sign(StringToSign, request.Access_Key_Secret + "&");

            Signature = _urlEncodeX(Signature);
            return Signature;
        }

        /// <summary>
        /// HmacSha1算法
        /// </summary>
        /// <param name="text"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string _hmacSha1Sign(string text, string key)
        {
            Encoding encode = Encoding.UTF8;
            byte[] byteData = encode.GetBytes(text);
            byte[] byteKey = encode.GetBytes(key);
            using (HMACSHA1 hmac = new HMACSHA1(byteKey))
            {
                using (CryptoStream cs = new CryptoStream(Stream.Null, hmac, CryptoStreamMode.Write))
                {
                    cs.Write(byteData, 0, byteData.Length);
                    cs.Close();
                    return Convert.ToBase64String(hmac.Hash);
                }
            }
        }

        private static string _httpBuildQuery(SortedDictionary<String, String> parameters)
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<String, String> kvp in parameters) //系统参数
            {
                sb.Append("&");
                sb.Append(_urlEncodeX(kvp.Key));
                sb.Append("=");
                sb.Append(_urlEncodeX(kvp.Value));
            }
            return sb.ToString().Substring(1);
        }

        /// <summary>
        /// 符合阿里云规定的URL编码（可以将%3A、%2F这样的大写，空格也可以直接转码成%20，好嗨皮）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string _urlEncodeX(string str)
        {
            string rt = Microsoft.JScript.GlobalObject.escape(str);
            rt = rt.Replace("+", "%2B");    //js不编码“+”号
            rt = rt.Replace("*", "%2A");    //js不编码“*”号
            rt = rt.Replace("%7E", "~");    //js会编码“~”，但是阿里云要求“~”原滋原味不要被编码
            rt = rt.Replace("%7e", "~");
            return rt;
        }
    }
    /// <summary>
    /// Action类型
    /// </summary>
    public enum ActionType
    {
        /// <summary>
        /// 无
        /// </summary>
        None,
        /// <summary>
        /// 添加域名
        /// </summary>
        AddDomain,
        /// <summary>
        /// 删除域名
        /// </summary>
        DeleteDomain,
        /// <summary>
        /// 获取域名列表
        /// </summary>
        DescribeDomains,
        /// <summary>
        /// 获取域名信息
        /// </summary>
        DescribeDomainInfo,
        /// <summary>
        /// 获取域名Whois信息
        /// </summary>
        DescribeDomainWhoisInfo,
        /// <summary>
        /// 找回域名
        /// </summary>
        RetrievalDomainName,
        /// <summary>
        /// 申请由管理员找回
        /// </summary>
        ApplyForRetrievalDomainName,
        /// <summary>
        /// 添加解析记录
        /// </summary>
        AddDomainRecord,
        /// <summary>
        /// 删除解析记录
        /// </summary>
        DeleteDomainRecord,
        /// <summary>
        /// 修改解析记录
        /// </summary>
        UpdateDomainRecord,
        /// <summary>
        /// 设置解析记录状态
        /// </summary>
        SetDomainRecordStatus,
        /// <summary>
        /// 获取解析记录列表
        /// </summary>
        DescribeDomainRecords,
        /// <summary>
        /// 获取解析记录信息
        /// </summary>
        DescribeDomainRecordInfo
    }
    /// <summary>
    /// 工具
    /// </summary>
    public class Utilities
    {
        private static List<TypeRelation> relations;
        static Utilities()
        {
            relations = new List<TypeRelation>();
            relations.Add(new TypeRelation() { Action = ActionType.AddDomain, RequestType = typeof(RequestAddDomain), ResponseType = typeof(ResponseAddDomain) });
            relations.Add(new TypeRelation() { Action = ActionType.AddDomainRecord, RequestType = typeof(RequestAddDomainRecord), ResponseType = typeof(ResponseAddDomainRecord) });
            relations.Add(new TypeRelation() { Action = ActionType.ApplyForRetrievalDomainName, RequestType = typeof(RequestApplyForRetrievalDomainName), ResponseType = typeof(ResponseApplyForRetrievalDomainName) });
            relations.Add(new TypeRelation() { Action = ActionType.DeleteDomain, RequestType = typeof(RequestDeleteDomain), ResponseType = typeof(ResponseDeleteDomain) });
            relations.Add(new TypeRelation() { Action = ActionType.DeleteDomainRecord, RequestType = typeof(RequestDeleteDomainRecord), ResponseType = typeof(ResponseDeleteDomainRecord) });
            relations.Add(new TypeRelation() { Action = ActionType.DescribeDomainInfo, RequestType = typeof(RequestDescribeDomainInfo), ResponseType = typeof(ResponseDescribeDomainInfo) });
            relations.Add(new TypeRelation() { Action = ActionType.DescribeDomainRecordInfo, RequestType = typeof(RequestDescribeDomainRecordInfo), ResponseType = typeof(ResponseDescribeDomainRecordInfo) });
            relations.Add(new TypeRelation() { Action = ActionType.DescribeDomainRecords, RequestType = typeof(RequestDescribeDomainRecords), ResponseType = typeof(ResponseDescribeDomainRecords) });
            relations.Add(new TypeRelation() { Action = ActionType.DescribeDomains, RequestType = typeof(RequestDescribeDomains), ResponseType = typeof(ResponseDescribeDomains) });
            relations.Add(new TypeRelation() { Action = ActionType.DescribeDomainWhoisInfo, RequestType = typeof(RequestDescribeDomainWhoisInfo), ResponseType = typeof(ResponseDescribeDomainWhoisInfo) });
            relations.Add(new TypeRelation() { Action = ActionType.RetrievalDomainName, RequestType = typeof(RequestRetrievalDomainName), ResponseType = typeof(ResponseRetrievalDomainName) });
            relations.Add(new TypeRelation() { Action = ActionType.SetDomainRecordStatus, RequestType = typeof(RequestSetDomainRecordStatus), ResponseType = typeof(ResponseSetDomainRecordStatus) });
            relations.Add(new TypeRelation() { Action = ActionType.UpdateDomainRecord, RequestType = typeof(RequestUpdateDomainRecord), ResponseType = typeof(ResponseUpdateDomainRecord) });
        }
        /// <summary>
        /// 获取类型
        /// </summary>
        /// <param name="requestType">请求参数类型</param>
        /// <param name="responseType">响应参数类型</param>
        /// <param name="actionType">Action类型</param>
        /// <returns></returns>
        public static bool GetType(Type requestType, out Type responseType, out ActionType actionType)
        {
            responseType = null;
            actionType = ActionType.None;
            TypeRelation relation = relations.Find((dat) => { return dat.RequestType == requestType; });
            if (relation != null)
            {
                responseType = relation.ResponseType;
                actionType = relation.Action;
                return true;
            }
            return false;
        }
        /// <summary>
        /// 获取类型
        /// </summary>
        /// <param name="actionType">Action类型</param>
        /// <param name="responseType">请求参数类型</param>
        /// <param name="requestType">响应参数类型</param>
        /// <returns></returns>
        public static bool GetType(ActionType actionType, out Type responseType, out Type requestType)
        {
            responseType = null;
            requestType = null;
            TypeRelation relation = relations.Find((dat) => { return dat.Action == actionType; });
            if (relation != null)
            {
                responseType = relation.ResponseType;
                requestType = relation.RequestType;
                return true;
            }
            return false;
        }
    }
    /// <summary>
    /// 类型关系
    /// </summary>
    public class TypeRelation
    {
        /// <summary>
        /// 请求参数类型
        /// </summary>
        public Type RequestType { get; set; }
        /// <summary>
        /// 响应参数类型
        /// </summary>
        public Type ResponseType { get; set; }
        /// <summary>
        /// Action类型
        /// </summary>
        public ActionType Action { get; set; }
    }
    /// <summary>
    /// 阿里云html响应消息
    /// </summary>
    public class AliyunHtmlResponse
    {
        /// <summary>
        /// 协议版本
        /// </summary>
        public Version ProtocolVersion { get; private set; }
        /// <summary>
        /// Http状态码
        /// </summary>
        public HttpStatusCode StatusCode { get; private set; }
        /// <summary>
        /// 状态描述
        /// </summary>
        public string StatusDescription { get { return this.StatusCode.ToString(); } }
        /// <summary>
        /// ContentType
        /// </summary>
        public string ContentType { get; private set; }
        /// <summary>
        /// Server
        /// </summary>
        public string Server { get; private set; }
        /// <summary>
        /// 正文内容
        /// </summary>
        public string Content { get; private set; }
        /// <summary>
        /// 响应正文内容格式
        /// </summary>
        public ResponseFormat Format { get; private set; }
        /// <summary>
        /// 错误消息
        /// </summary>
        public ErrorResponse ErrorMessage
        {
            get
            {
                if (this.StatusCode == HttpStatusCode.OK)
                    return null;
                return new ErrorResponse(this.Content, this.Format);
            }
        }

        private string htmlContent = null;
        /// <summary>
        /// 初始化阿里云html响应消息
        /// </summary>
        /// <param name="html"></param>
        /// <param name="format"></param>
        public AliyunHtmlResponse(string html, ResponseFormat format)
        {
            this.htmlContent = html;
            this.Format = format;
            this.Analyze();
        }

        private void Analyze()
        {
            if (!string.IsNullOrEmpty(this.htmlContent))
            {
                string[] total = this.htmlContent.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                MatchCollection regex = Regex.Matches(total[0].Substring(5), @"[a-zA-Z0-9.]+", RegexOptions.None);
                this.ProtocolVersion = new Version(regex[0].Groups[0].Value);
                this.StatusCode = (HttpStatusCode)Convert.ToInt32(regex[1].Groups[0].Value);
                this.Server = total[1].Substring(8);
                this.ContentType = total[3].Substring(14);
                if (this.Format == ResponseFormat.JSON)
                {
                    if (this.StatusCode == HttpStatusCode.OK)
                        this.Content = total[9];
                    else
                        this.Content = total[8];
                }
                else
                    this.Content = total[8];
            }
        }
    }
}
