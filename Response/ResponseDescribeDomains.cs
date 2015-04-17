namespace Aliyun
{
    /// <summary>
    /// 获取域名列表
    /// </summary>
    public class ResponseDescribeDomains : AliyunResponse
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageNumber { get; set; }
        /// <summary>
        /// 本次获取的域名列表
        /// </summary>
        public Domains Domains { get; set; }
        /// <summary>
        /// 本次查询获取的域名数量
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 域名列表总数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RequestId { get; set; }
    }

}
