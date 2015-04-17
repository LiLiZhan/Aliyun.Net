using System.Collections.Generic;

namespace Aliyun
{
    /// <summary>
    /// 添加域名
    /// </summary>
    public class RequestAddDomain : AliyunRequest
    {
        /// <summary>
        /// 操作接口名，系统规定参数，取值：AddDomain
        /// </summary>
        public override ActionType Action { get { return ActionType.AddDomain; } }
        /// <summary>
        /// 域名名称
        /// </summary>
        public string DomainName { get; set; }

        public override Dictionary<string, string> GeneralParameters()
        {
            Dictionary<string, string> _params = new Dictionary<string, string>();
            _params.Add("Action", this.Action.ToString());
            _params.Add("DomainName", this.DomainName);
            return _params;
        }
    }
}
