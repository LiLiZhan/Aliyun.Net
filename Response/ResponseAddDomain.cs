namespace Aliyun
{
    /// <summary>
    /// 添加域名
    /// </summary>
    public class ResponseAddDomain : AliyunResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public string RequestId { get; set; }
        /// <summary>
        /// 域名ID
        /// </summary>
        public string DomainId { get; set; }
        /// <summary>
        /// 域名名称
        /// </summary>
        public string DomainName { get; set; }
        /// <summary>
        /// 只针对中文域名返回punycode码
        /// </summary>
        public string PunyCode { get; set; }
        /// <summary>
        /// 域名在解析系统中的DNS列表
        /// </summary>
        public DnsServers DnsServers { get; set; }
    }
}
