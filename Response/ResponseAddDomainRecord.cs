namespace Aliyun
{
    /// <summary>
    /// 添加解析记录
    /// </summary>
    public class ResponseAddDomainRecord : AliyunResponse
    {
        public string RequestId { get; set; }
        /// <summary>
        /// 解析记录的ID
        /// </summary>
        public string RecordId { get; set; }
    }
}
