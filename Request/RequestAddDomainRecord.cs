using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aliyun
{
    /// <summary>
    /// 添加解析记录
    /// </summary>
    public class RequestAddDomainRecord : AliyunRequest
    {
        /// <summary>
        /// 操作接口名，系统规定参数，取值：AddDomainRecord
        /// </summary>
        public override ActionType Action { get { return ActionType.AddDomainRecord; } }
        /// <summary>
        /// 域名名称
        /// </summary>
        public string DomainName { get; set; }
        /// <summary>
        /// 主机记录，如果要解析@.exmaple.com，主机记录要填写” @”，而不是””
        /// </summary>
        public string RR { get; set; }
        /// <summary>
        /// 解析类型，包括(不区分大小写)：A MX CNAME TXT REDIRECT_URL FORWORD_URL NS AAAA SRV
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 记录值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 解析线路，默认为default 解析线路包括(不区分大小写)：default telecom unicom mobile oversea edu google baidu biying
        /// </summary>
        public string Line { get; set; }
        /// <summary>
        /// 生存时间，取值范围（600,1800,3600,43200,86400）单位：秒，默认为600
        /// </summary>
        public long TTL { get; set; }
        /// <summary>
        /// MX记录的优先级，取值范围[1,10]，记录类型为MX记录时，此参数必须
        /// </summary>
        public long Priority { get; set; }

        public override Dictionary<string, string> GeneralParameters()
        {
            Dictionary<string, string> _params = new Dictionary<string, string>();
            _params.Add("Action", this.Action.ToString());
            _params.Add("DomainName", this.DomainName);
            _params.Add("RR", this.RR);
            _params.Add("Type", this.Type);
            _params.Add("Value", this.Value);
            return _params;
        }
    }
}
