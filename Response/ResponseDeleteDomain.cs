namespace Aliyun
{
    /// <summary>
    /// 删除域名
    /// </summary>
    public class ResponseDeleteDomain : AliyunResponse
    {
        public string RequestId { get; set; }
        /// <summary>
        /// 域名名称
        /// </summary>
        public string DomainName { get; set; }
    }
}
