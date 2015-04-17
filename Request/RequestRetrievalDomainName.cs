using System;
using System.Collections.Generic;

namespace Aliyun
{
    /// <summary>
    /// 找回域名
    /// </summary>
    public class RequestRetrievalDomainName : AliyunRequest
    {
        /// <summary>
        /// 操作接口名，系统规定参数，取值：RetrievalDomainName
        /// </summary>
        public override ActionType Action { get { return ActionType.RetrievalDomainName; } }
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
