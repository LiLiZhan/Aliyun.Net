namespace Aliyun
{
    /// <summary>
    /// 获取域名信息
    /// </summary>
    public class ResponseDescribeDomainInfo : AliyunResponse
    {
        /// <summary>
        /// 域名在解析系统中的DNS列表
        /// </summary>
        public DnsServers DnsServers { get; set; }
        /// <summary>
        /// 域名ID
        /// </summary>
        public string DomainId { get; set; }
        /// <summary>
        /// 域名名称
        /// </summary>
        public string DomainName { get; set; }
        /// <summary>
        /// 非必须的返回值，只针对中文域名返回punycode码
        /// </summary>
        public string PunyCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RequestId { get; set; }
    }

}
