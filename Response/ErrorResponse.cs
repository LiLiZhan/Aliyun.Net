using Newtonsoft.Json;
using System.Xml;

namespace Aliyun
{
    /// <summary>
    /// 响应错误详细
    /// </summary>
    public class ErrorResponse : AliyunResponse
    {
        /// <summary>
        /// 唯一识别码
        /// </summary>
        public string RequestId { get; set; }
        /// <summary>
        /// 主机ID
        /// </summary>
        public string HostId { get; set; }
        /// <summary>
        /// 错误码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 错误消息
        /// </summary>
        public string Message { get; set; }

        public ErrorResponse()
        {
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", Code, Message);
        }

        public ErrorResponse(string content, ResponseFormat format)
        {
            ErrorResponse res = null;
            if (format == ResponseFormat.JSON)
                res = JsonConvert.DeserializeObject<ErrorResponse>(content);
            else
            {
                res = new ErrorResponse();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(content);
                XmlNode nodes = XmlHelper.GetNode(doc, "", "/Error");
                foreach (XmlNode node in nodes.ChildNodes)
                {
                    switch (node.Name)
                    {
                        case "RequestId":
                            {
                                string _requestId = null;
                                XmlHelper.GetAttribute((XmlElement)node, ref _requestId);
                                res.RequestId = _requestId;
                            }
                            break;
                        case "Code":
                            {
                                string _code = null;
                                XmlHelper.GetAttribute((XmlElement)node, ref _code);
                                res.Code = _code;
                            }
                            break;
                        case "HostId":
                            {
                                string _hostId = null;
                                XmlHelper.GetAttribute((XmlElement)node, ref _hostId);
                                res.HostId = _hostId;
                            }
                            break;
                        case "Message":
                            {
                                string _message = null;
                                XmlHelper.GetAttribute((XmlElement)node, ref _message);
                                res.Message = _message;
                            }
                            break;
                    }
                }
            }
            this.Code = res.Code;
            this.HostId = res.HostId;
            this.RequestId = res.RequestId;
            this.Message = res.Message;
        }
    }
}
