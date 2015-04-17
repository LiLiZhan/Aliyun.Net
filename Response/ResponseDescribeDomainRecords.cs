namespace Aliyun
{
    /// <summary>
    /// 获取解析记录列表
    /// </summary>
    public class ResponseDescribeDomainRecords : AliyunResponse
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageNumber { get; set; }
        /// <summary>
        /// 解析记录列表
        /// </summary>
        public DomainRecords DomainRecords { get; set; }
        /// <summary>
        /// 本次查询获取的解析数量
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 解析记录总数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RequestId { get; set; }
    }
}
