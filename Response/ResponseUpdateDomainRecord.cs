namespace Aliyun
{
    /// <summary>
    /// 修改解析记录
    /// </summary>
    public class ResponseUpdateDomainRecord : AliyunResponse
    {
        public string RequestId { get; set; }
        /// <summary>
        /// 解析记录的ID
        /// </summary>
        public string RecordId { get; set; }
    }
}
