using System.Collections.Generic;

namespace Aliyun
{
    /// <summary>
    /// 阿里云响应消息
    /// </summary>
    public interface AliyunResponse
    {
        /// <summary>
        /// 唯一识别码
        /// </summary>
        string RequestId { get; set; }
    }
    /// <summary>
    /// DNS服务器列表
    /// </summary>
    public class DnsServers
    {
        /// <summary>
        /// DNS服务器名称
        /// </summary>
        public List<string> DnsServer { get; set; }
    }
    /// <summary>
    /// 状态列表
    /// </summary>
    public class StatusList
    {
        /// <summary>
        /// Status名称
        /// </summary>
        public List<string> Status { get; set; }
    }
    /// <summary>
    /// 域名
    /// </summary>
    public class DomainItem
    {
        /// <summary>
        /// 名在解析系统中的DNS列表
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
        /// 中文域名的punycode码，英文域名返回为空
        /// </summary>
        public string PunyCode { get; set; }
    }
    /// <summary>
    /// 域名列表
    /// </summary>
    public class Domains
    {
        /// <summary>
        /// 
        /// </summary>
        public List<DomainItem> Domain { get; set; }
    }
    /// <summary>
    /// 解析记录列表
    /// </summary>
    public class DomainRecords
    {
        /// <summary>
        /// 记录
        /// </summary>
        public List<RecordItem> Record { get; set; }
    }
    /// <summary>
    /// Record结构表
    /// </summary>
    public class RecordItem
    {
        /// <summary>
        /// 域名名称
        /// </summary>
        public string DomainName { get; set; }
        /// <summary>
        /// 解析记录ID
        /// </summary>
        public string RecordId { get; set; }
        /// <summary>
        /// 主机记录
        /// </summary>
        public string RR { get; set; }
        /// <summary>
        /// 记录类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 记录值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 生存时间
        /// </summary>
        public long TTL { get; set; }
        /// <summary>
        /// MX记录的优先级
        /// </summary>
        public long Priority { get; set; }
        /// <summary>
        /// 解析线路
        /// </summary>
        public string Line { get; set; }
        /// <summary>
        /// 解析记录状态，Enable/Disable
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 解析记录锁定状态，true/false
        /// </summary>
        public bool Locked { get; set; }
    }
}
