namespace Aliyun
{
    /// <summary>
    /// 申请由管理员找回
    /// </summary>
    public class ResponseApplyForRetrievalDomainName : AliyunResponse
    {
        public string RequestId { get; set; }
        /// <summary>
        /// 域名名称
        /// </summary>
        public string DomainName { get; set; }
    }
}
