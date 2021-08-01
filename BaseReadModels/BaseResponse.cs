using System;
using System.Collections.Generic;
using System.Globalization;
using EnumDefine;

namespace BaseReadModels
{
    public class BaseResponse
    {
        public bool Status { get; set; }
        public ErrorCodeEnum ErrorCode { get; set; }
        public List<string> Messages { get; set; }
        public string ServerTime { get; set; }

        public BaseResponse()
        {
            Messages = new List<string>();
            ServerTime = DateTime.Now.ToString(CultureInfo.InvariantCulture);
        }

        public void SetSuccess()
        {
            Status = true;
            ErrorCode = ErrorCodeEnum.NoErrorCode;
        }

        public void SetSuccess(string message)
        {
            Status = true;
            Messages.Add(message);
            ErrorCode = ErrorCodeEnum.NoErrorCode;
        }

        public void SetPermissionDeny()
        {
            Status = false;
            ErrorCode = ErrorCodeEnum.PermissionDeny;
            string message = ErrorCode.ToString();
            Messages.Add(message);
        }

        public void SetFail(ErrorCodeEnum code)
        {
            Status = false;
            ErrorCode = code;
            string message = code.ToString();
            Messages.Add(message);
        }

        public void SetFail(string message, ErrorCodeEnum code = ErrorCodeEnum.NoErrorCode)
        {
            Status = false;
            ErrorCode = code;
            Messages.Add(message);
        }

        public void SetFail(Exception ex, ErrorCodeEnum code = ErrorCodeEnum.NoErrorCode)
        {
            Status = false;
            ErrorCode = code;
            string message = $"Message: {ex.Message}";
            Messages.Add(message);
        }

        public void SetFail(IEnumerable<string> messages, ErrorCodeEnum code = ErrorCodeEnum.NoErrorCode)
        {
            Status = false;
            ErrorCode = code;
            foreach (var message in messages)
            {
                Messages.Add(message);
            }
        }
    }

    public class BaseResponse<T>
    {
        public bool Status { get; set; }
        public ErrorCodeEnum ErrorCode { get; set; }
        public List<string> Messages { get; set; }
        public int Version { get; set; }
        public string ServerTime { get; set; }
        
        public T Data { get; set; }

        public BaseResponse()
        {
            Messages = new List<string>();
            ServerTime = DateTime.Now.ToString(CultureInfo.InvariantCulture);
        }

        public void SetSuccess()
        {
            Status = true;
            ErrorCode = ErrorCodeEnum.NoErrorCode;
        }

        public void SetSuccess(string message)
        {
            Status = true;
            Messages.Add(message);
            ErrorCode = ErrorCodeEnum.NoErrorCode;
        }

        public void SetPermissionDeny()
        {
            Status = false;
            ErrorCode = ErrorCodeEnum.PermissionDeny;
            string message = ErrorCode.ToString();
            Messages.Add(message);
        }

        public void SetFail(ErrorCodeEnum code)
        {
            Status = false;
            ErrorCode = code;
            string message = code.ToString();
            Messages.Add(message);
        }

        public void SetFail(string message, ErrorCodeEnum code = ErrorCodeEnum.NoErrorCode)
        {
            Status = false;
            ErrorCode = code;
            Messages.Add(message);
        }

        public void SetFail(Exception ex, ErrorCodeEnum code = ErrorCodeEnum.NoErrorCode)
        {
            Status = false;
            ErrorCode = code;
            string message = $"Message: {ex.Message}";
            Messages.Add(message);
        }

        public void SetFail(IEnumerable<string> messages, ErrorCodeEnum code = ErrorCodeEnum.NoErrorCode)
        {
            Status = false;
            ErrorCode = code;
            foreach (var message in messages)
            {
                Messages.Add(message);
            }
        }
    }
}