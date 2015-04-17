namespace Aliyun
{
    /// <summary>
    /// 找回域名
    /// </summary>
    public class ResponseRetrievalDomainName : AliyunResponse
    {
        public string RequestId { get; set; }
        /// <summary>
        /// 域名名称
        /// </summary>
        public string DomainName { get; set; }
        /// <summary>
        /// 接收验证码的whois邮箱
        /// </summary>
        public string WhoisEmail { get; set; }
    }
}
