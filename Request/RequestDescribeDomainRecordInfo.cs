using System;
using System.Collections.Generic;

namespace Aliyun
{
    /// <summary>
    /// 获取解析记录信息
    /// </summary>
    public class RequestDescribeDomainRecordInfo : AliyunRequest
    {
        /// <summary>
        /// 操作接口名，系统规定参数，取值：DescribeDomainRecordInfo
        /// </summary>
        public override ActionType Action { get { return ActionType.DescribeDomainRecordInfo; } }
        /// <summary>
        /// 解析记录ID
        /// </summary>
        public string RecordId { get; set; }

        public override Dictionary<string, string> GeneralParameters()
        {
            Dictionary<string, string> _params = new Dictionary<string, string>();
            _params.Add("Action", Action.ToString());
            _params.Add("RecordId", this.RecordId);
            return _params;
        }
    }
}
