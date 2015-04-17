using System;
using System.Collections.Generic;

namespace Aliyun
{
    /// <summary>
    /// 获取解析记录列表
    /// </summary>
    public class RequestDescribeDomainRecords : AliyunRequest
    {
        /// <summary>
        /// 操作接口名，系统规定参数，取值：DescribeDomainRecords
        /// </summary>
        public override ActionType Action { get { return ActionType.DescribeDomainRecords; } }
        /// <summary>
        /// 域名名称
        /// </summary>
        public string DomainName { get; set; }
        /// <summary>
        /// 当前页数，起始值为1，默认为1
        /// </summary>
        public string PageNumber { get; set; }
        /// <summary>
        /// 分页查询时设置的每页行数，最大值500，默认为20
        /// </summary>
        public string PageSize { get; set; }
        /// <summary>
        /// 主机记录的关键字，按照”%RRKeyWord%”模式搜索，不区分大小写
        /// </summary>
        public string RRKeyWord { get; set; }
        /// <summary>
        /// 解析类型的关键字，按照全匹配搜索，不区分大小写
        /// </summary>
        public string TypeKeyWord { get; set; }
        /// <summary>
        /// 记录值的关键字，按照”%ValueKeyWord%”模式搜索，不区分大小写
        /// </summary>
        public string ValueKeyWord { get; set; }

        public override Dictionary<string, string> GeneralParameters()
        {
            Dictionary<string, string> _params = new Dictionary<string, string>();
            _params.Add("Action", Action.ToString());
            _params.Add("DomainName", this.DomainName);
            return _params;
        }
    }
}
