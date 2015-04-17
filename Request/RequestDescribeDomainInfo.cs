using System;
using System.Collections.Generic;

namespace Aliyun
{
    /// <summary>
    /// 获取域名信息
    /// </summary>
    public class RequestDescribeDomainInfo : AliyunRequest
    {
        /// <summary>
        /// 操作接口名，系统规定参数，取值：DescribeDomainInfo
        /// </summary>
        public override ActionType Action { get { return ActionType.DescribeDomainInfo; } }
        /// <summary>
        /// 域名名称
        /// </summary>
        public string DomainName { get; set; }

        public override Dictionary<string, string> GeneralParameters()
        {
            Dictionary<string, string> _params = new Dictionary<string, string>();
            _params.Add("Action", Action.ToString());
            _params.Add("DomainName", this.DomainName);
            return _params;
        }
    }
}
