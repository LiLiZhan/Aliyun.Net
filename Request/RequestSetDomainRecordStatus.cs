using System;
using System.Collections.Generic;

namespace Aliyun
{
    /// <summary>
    /// 设置解析记录状态
    /// </summary>
    public class RequestSetDomainRecordStatus : AliyunRequest
    {
        /// <summary>
        /// 操作接口名，系统规定参数，取值：SetDomainRecordStatus
        /// </summary>
        public override ActionType Action { get { return ActionType.SetDomainRecordStatus; } }
        /// <summary>
        /// 解析记录ID
        /// </summary>
        public string RecordId { get; set; }
        /// <summary>
        /// Enable: 启用解析 Disable: 暂停解析
        /// </summary>
        public string Status { get; set; }

        public override Dictionary<string, string> GeneralParameters()
        {
            Dictionary<string, string> _params = new Dictionary<string, string>();
            _params.Add("Action", Action.ToString());
            _params.Add("RecordId", this.RecordId);
            _params.Add("Status", this.Status);
            return _params;
        }
    }
}
