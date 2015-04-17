namespace Aliyun
{
    /// <summary>
    /// 删除解析记录
    /// </summary>
    public class ResponseDeleteDomainRecord : AliyunResponse
    {
        public string RequestId { get; set; }
        /// <summary>
        /// 解析记录的ID
        /// </summary>
        public string RecordId { get; set; }
    }
}
