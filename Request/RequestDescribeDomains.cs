using System;
using System.Collections.Generic;

namespace Aliyun
{
    /// <summary>
    /// 获取域名列表
    /// </summary>
    public class RequestDescribeDomains : AliyunRequest
    {
        /// <summary>
        /// 操作接口名，系统规定参数，取值：DescribeDomains
        /// </summary>
        public override ActionType Action { get { return ActionType.DescribeDomains; } }
        /// <summary>
        /// 当前页数，起始值为1，默认为1
        /// </summary>
        public long PageNumber { get { return _PageNumber; } set { _PageNumber = value; } }
        private long _PageNumber = 1;

        private long _PageSize = 20;
        /// <summary>
        /// 分页查询时设置的每页行数，最大值100，默认为20
        /// </summary>
        public long PageSize { get { return _PageSize; } set { _PageSize = value; } }
        /// <summary>
        /// 关键字，按照”%KeyWord%”模式搜索，不区分大小写
        /// </summary>
        public string KeyWord { get; set; }

        public override Dictionary<string, string> GeneralParameters()
        {
            Dictionary<string, string> _params = new Dictionary<string, string>();
            _params.Add("Action", Action.ToString());
            return _params;
        }
    }
}
