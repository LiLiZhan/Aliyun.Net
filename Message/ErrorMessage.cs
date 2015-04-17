﻿using System.Collections.Generic;

namespace Aliyun
{
    /// <summary>
    /// 错误消息
    /// </summary>
    public class ErrorMessage
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public string Code { get { return _code ?? Description; } set { _code = value; } }
        private string _code;
        /// <summary>
        /// 错误描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 错误
        /// </summary>
        public string Error { get; set; }
        /// <summary>
        /// 状态码
        /// </summary>
        public int HttpStatus { get; set; }

        public override string ToString()
        {
            return this.Error;
        }

        static ErrorMessage()
        {
            init();
        }
        public static Dictionary<string, ErrorMessage> errorMessages;
        private static void init()
        {
            errorMessages = new Dictionary<string, ErrorMessage>();
            List<ErrorMessage> messages = new List<ErrorMessage>();
            /*通用性错误*/
            messages.Add(new ErrorMessage() { Error = "缺少参数", Code = "MissingParameter", Description = "The input parameter \"<parameter name>\" that is mandatory for processing this request is not supplied", HttpStatus = 400 });
            messages.Add(new ErrorMessage() { Error = "参数取值无效", Code = "InvalidParameter", Description = "The specified value of parameter \"<parameter name>\" is not valid.", HttpStatus = 400 });
            messages.Add(new ErrorMessage() { Error = "无效的接口", Code = "UnsupportedOperation", Description = "The specified action is not supported.", HttpStatus = 400 });
            messages.Add(new ErrorMessage() { Error = "无效的版本", Code = "NoSuchVersion", Description = "The specified version does not exist.", HttpStatus = 400 });
            messages.Add(new ErrorMessage() { Error = "操作被流量控制系统拒绝", Code = "Throttling", Description = "Request was denied due to request throttling.", HttpStatus = 400 });
            messages.Add(new ErrorMessage() { Error = "无效的Access Key", Code = "InvalidAccessKeyId.NotFound", Description = "The Access Key ID provided does not exist in our records.", HttpStatus = 400 });
            messages.Add(new ErrorMessage() { Error = "操作被禁止", Code = "Forbidden", Description = "User not authorized to operate on the specified resource.", HttpStatus = 403 });
            messages.Add(new ErrorMessage() { Error = "操作被风险控制系统禁止", Code = "Forbidden.RiskControl", Description = "This operation is forbidden by Aliyun Risk Control system.", HttpStatus = 403 });
            messages.Add(new ErrorMessage() { Error = "无效的签名", Code = "SignatureDoesNotMatch", Description = "The signature we calculated does not match the one you provided. Please refer to the API reference about authentication for details.", HttpStatus = 403 });
            messages.Add(new ErrorMessage() { Error = "无实名验证", Code = "Forbidden.UserVerification", Description = "Your user account is not verified by Aliyun.", HttpStatus = 403 });
            messages.Add(new ErrorMessage() { Error = "服务器无法完成对请求的处理", Code = "InternalError", Description = "The request processing has failed due to some unknown error, exception or failure.", HttpStatus = 500 });
            messages.Add(new ErrorMessage() { Error = "服务器当前无法处理请求", Code = "ServiceUnavailable", Description = "The request has failed due to a temporary failure of the server.", HttpStatus = 503 });

            messages.Add(new ErrorMessage() { Error = "无效的状态", Code = "InvalidStatus", Description = "The domain record status is invalid.", HttpStatus = 400 });
            messages.Add(new ErrorMessage() { Error = "不支持的域名后缀", Code = "InvalidDomainName.Suffix", Description = "The domain suffix isnot been supported.", HttpStatus = 400 });
            messages.Add(new ErrorMessage() { Error = "未注册的域名", Code = "InvalidDomainName.Unregistered", Description = "The domain name is not registered.", HttpStatus = 400 });
            messages.Add(new ErrorMessage() { Error = "未知错误", Code = "UnKnownError", Description = "There is an unknown error in system.", HttpStatus = 400 });
            messages.Add(new ErrorMessage() { Error = "本用户下已存在的域名", Code = "InvalidDomainName.Duplicate", Description = "The domain name is duplicated in this user’s domain list.", HttpStatus = 400 });
            messages.Add(new ErrorMessage() { Error = "其他用户下已存在此域名", Code = "DomainAddedByOthers", Description = "The domain name has been added by other users.", HttpStatus = 400 });
            messages.Add(new ErrorMessage() { Error = "中文域名与注册局返回信息不匹配", Code = "CHNDomainInfoNotMatch", Description = "The information of chinese domain does not match with CNNIC.", HttpStatus = 400 });
            messages.Add(new ErrorMessage() { Error = "获取不到Whois信息", Code = "UnabletoGetWoisInfo", Description = "The domain is unable to get information from whois system.", HttpStatus = 400 });
            messages.Add(new ErrorMessage() { Error = "域名在本账户下不存在", Code = "IncorrectDomainUser", Description = "The domain name does not belong to this user.", HttpStatus = 400 });
            messages.Add(new ErrorMessage() { Error = "域名为万网域名禁止删除", Code = "Forbidden.DomainType", Description = "The domain is forbidden to operate because it is a hichina domain.", HttpStatus = 400 });
            messages.Add(new ErrorMessage() { Error = "域名含有锁定解析", Code = "Fobidden.RecordLocked", Description = "The domain is forbidden to delete because there are some records have been locked.", HttpStatus = 400 });
            messages.Add(new ErrorMessage() { Error = "域名在云解析系统中不存在", Code = "InvalidDomainName.NoExist", Description = "The domain is not exist in aliyun DNS system.", HttpStatus = 400 });
            messages.Add(new ErrorMessage() { Error = "域名格式错误", Code = "InvalidDomainName.Format", Description = "The format of domain is error.", HttpStatus = 400 });
            messages.Add(new ErrorMessage() { Error = "Whois邮箱未获取到", Code = "UnableToGetWhoisEmail", Description = "The domain is unable to get whois email.", HttpStatus = 400 });
            messages.Add(new ErrorMessage() { Error = "万网域名不允许找回", Code = "Forbidden.DomainType", Description = "The domain is forbidden to operate because it is a hichina domain.", HttpStatus = 400 });
            messages.Add(new ErrorMessage() { Error = "90秒内不允许重复找回", Code = "Fobidden.TooOfen", Description = "The operation is too ofen in 90 seconds.", HttpStatus = 400 });
            messages.Add(new ErrorMessage() { Error = "超出解析记录类型最大值90", Code = "QuotaExceeded.Record", Description = "You Can’t add this domain record because the {“Type RR Line”} record has been out of MAX count (90).", HttpStatus = 400 });
            messages.Add(new ErrorMessage() { Error = "禁止解析操作的域名", Code = "DomainForbidden", Description = "The action could not be completed because the domain has been forbidden.", HttpStatus = 400 });
            messages.Add(new ErrorMessage() { Error = "URL转发不支持泛解析记录", Code = "URLForwardError.PanRecord", Description = "The Pan-record is not supported in url forward record.", HttpStatus = 400 });
            messages.Add(new ErrorMessage() { Error = "URL转发不是默认线路", Code = "URLForwardError.NotDefaultLine", Description = "The url forward record only support default line.", HttpStatus = 400 });
            messages.Add(new ErrorMessage() { Error = "URL转发目标域名为中文域名", Code = "URLForwardError. ChineseChar", Description = "The Chinese char is not supported in url forward record.", HttpStatus = 400 });
            messages.Add(new ErrorMessage() { Error = "URL转发域名未备案", Code = "URLForwardError.NotVerifyDomain", Description = "The domain name must be verified in Hichina in url forward record.", HttpStatus = 400 });
            messages.Add(new ErrorMessage() { Error = "解析记录在本账户下不存在", Code = "DomainRecordNotBelongToUser", Description = "The domain record does not belong to this user.", HttpStatus = 400 });
            messages.Add(new ErrorMessage() { Error = "解析记录被锁定", Code = "DomainRecordLocked", Description = "The domain record has been locked.", HttpStatus = 400 });
            messages.Add(new ErrorMessage() { Error = "解析记录已存在", Code = "DomainRecordDuplicate", Description = "The domain record is duplicated.", HttpStatus = 400 });
            messages.Add(new ErrorMessage() { Error = "解析记录冲突", Code = "DomainRecordConflict", Description = "The domain record is conflict with other records.", HttpStatus = 400 });

            foreach (ErrorMessage msg in messages)
            {
                if (!errorMessages.ContainsKey(msg.Code))
                    errorMessages.Add(msg.Code, msg);
            }
        }
        /// <summary>
        /// 获取错误消息
        /// </summary>
        /// <param name="res">错误响应</param>
        /// <returns>错误消息</returns>
        public static ErrorMessage GetMessage(ErrorResponse res)
        {
            ErrorMessage msg;
            if (!errorMessages.TryGetValue(res.Code, out msg))
                msg = new ErrorMessage() { Code = res.Code, Description = res.Message, HttpStatus = -1 };
            return msg;
        }

        /// <summary>
        /// 获取错误消息
        /// </summary>
        /// <param name="code">错误码</param>
        /// <returns>错误消息</returns>
        public static ErrorMessage GetMessage(string code)
        {
            ErrorMessage msg;
            if (errorMessages.TryGetValue(code, out msg))
            {
                return msg;
            }
            return null;
        }
    }
}
