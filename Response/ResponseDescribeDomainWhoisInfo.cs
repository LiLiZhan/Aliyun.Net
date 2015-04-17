namespace Aliyun
{
    /// <summary>
    /// 获取域名Whois信息
    /// </summary>
    public class ResponseDescribeDomainWhoisInfo : AliyunResponse
    {
        /// <summary>
        /// 域名当前使用的DNS列表
        /// </summary>
        public DnsServers DnsServers { get; set; }
        /// <summary>
        /// 域名状态列表
        /// </summary>
        public StatusList StatusList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RequestId { get; set; }
    }

}
