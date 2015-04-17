using System.Collections.Generic;

namespace Aliyun
{
    /// <summary>
    /// 阿里云公共请求消息
    /// </summary>
    public class AliyunRequest
    {
        /// <summary>
        /// Action类型
        /// </summary>
        public virtual ActionType Action { get { return ActionType.None; } }
        private ResponseFormat API_FORMAT = ResponseFormat.JSON;
        private string API_VERSION = "2015-01-09";
        private string SIGNATURE_METHOD = "HMAC-SHA1";
        private string SIGNATURE_VERSION = "1.0";
        private HttpVerb HTTP_METHOD = HttpVerb.GET;
        private string SEARCH_BASE_URL = "http://dns.aliyuncs.com/";
        private string ACCESS_KEY_ID = "";
        private string ACCESS_KEY_SECRET = "";
        /// <summary>
        /// 返回值的类型，支持JSON与XML。默认为JSON
        /// </summary>
        public ResponseFormat Format { get { return this.API_FORMAT; } set { this.API_FORMAT = value; } }
        /// <summary>
        /// API版本号，为日期形式：YYYY-MM-DD
        /// </summary>
        public string Version { get { return this.API_VERSION; } set { this.API_VERSION = value; } }
        /// <summary>
        /// 签名方式，目前支持HMAC-SHA1
        /// </summary>
        public string SignatureMethod { get { return this.SIGNATURE_METHOD; } set { this.SIGNATURE_METHOD = value; } }
        /// <summary>
        /// 签名算法版本，目前版本是1.0
        /// </summary>
        public string SignatureVersion { get { return this.SIGNATURE_VERSION; } set { this.SIGNATURE_VERSION = value; } }
        /// <summary>
        /// Http请求方法
        /// </summary>
        public HttpVerb Http_Method { get { return this.HTTP_METHOD; } set { this.HTTP_METHOD = value; } }
        /// <summary>
        /// API的服务接入地址
        /// </summary>
        public string Base_Url { get { return this.SEARCH_BASE_URL; } set { this.SEARCH_BASE_URL = value; } }
        /// <summary>
        /// 阿里云颁发给用户的访问服务所用的密钥ID
        /// </summary>
        public string AccessKeyId { get { return this.ACCESS_KEY_ID; } set { this.ACCESS_KEY_ID = value; } }
        /// <summary>
        /// Access Key Secret
        /// </summary>
        public string Access_Key_Secret { get { return this.ACCESS_KEY_SECRET; } set { this.ACCESS_KEY_SECRET = value; } }

        /// <summary>
        /// 生成参数
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<string, string> GeneralParameters()
        {
            return null;
        }
    }
}
