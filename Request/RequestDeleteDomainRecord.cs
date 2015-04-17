using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aliyun
{
    /// <summary>
    /// 删除解析记录
    /// </summary>
    public class RequestDeleteDomainRecord : AliyunRequest
    {
        /// <summary>
        /// 操作接口名，系统规定参数，取值：DeleteDomainRecord
        /// </summary>
        public override ActionType Action { get { return ActionType.DeleteDomainRecord; } }
        /// <summary>
        /// 解析记录的ID
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
