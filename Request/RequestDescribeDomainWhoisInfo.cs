using System;
using System.Collections.Generic;

namespace Aliyun
{
    /// <summary>
    /// 获取域名Whois信息
    /// </summary>
    public class RequestDescribeDomainWhoisInfo : AliyunRequest
    {
        /// <summary>
        /// 操作接口名，系统规定参数，取值：DescribeDomainWhoisInfo
        /// </summary>
        public override ActionType Action { get { return ActionType.DescribeDomainWhoisInfo; } }
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
