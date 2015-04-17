namespace Aliyun
{
    /// <summary>
    /// 设置解析记录状态
    /// </summary>
    public class ResponseSetDomainRecordStatus : AliyunResponse
    {
        public string RequestId { get; set; }
        /// <summary>
        /// 解析记录的ID
        /// </summary>
        public string RecordId { get; set; }
        /// <summary>
        /// 当前解析记录状态
        /// </summary>
        public string Status { get; set; }
    }
}
