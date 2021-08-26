using System;
using System.Collections.Generic;
using Configs;
using EnumDefine;
using Extensions;

namespace BaseCommands
{
    public class BaseCommandResponse<T>
    {
        public BaseCommandResponse()
        {
            Messages = new List<string>();
            ServerTime = DateTime.Now.AsUnixTimeStamp().ToString();
        }
        public bool Status { get; set; }
        public ErrorCodeType ErrorCode { get; set; }
        public List<string> Messages;
        public int Version { get; set; }
        public string ServerTime { get; set; }
        public T Data { get; set; }
        public int TotalRow { get; set; }

        public void SetSuccess()
        {
            Status = true;
            ErrorCode = ErrorCodeType.NoErrorCode;
        }

        public void SetSuccess(string message)
        {
            Status = true;
            Messages.Add(message);
            ErrorCode = ErrorCodeType.NoErrorCode;
        }

        public void SetFail(ErrorCodeType code)
        {
            Status = false;
            ErrorCode = code;
            string message = code.GetDisplayName();
            Messages.Add(message);
        }

        public void SetFail(string message, ErrorCodeType code = ErrorCodeType.NoErrorCode)
        {
            Status = false;
            ErrorCode = code;
            Messages.Add(message);
        }

        public void SetFail(Exception ex, ErrorCodeType code = ErrorCodeType.NoErrorCode)
        {
            Status = false;
            ErrorCode = code;
            string message = $"Message: {ex.Message}";
            Messages.Add(message);
        }

        public void SetFail(IEnumerable<string> messages, ErrorCodeType code = ErrorCodeType.NoErrorCode)
        {
            Status = false;
            ErrorCode = code;
            foreach (var message in messages)
            {
                Messages.Add(message);
            }
        }
    }

    public class BaseCommandResponse
    {
        public bool Status { get; set; }
        public ErrorCodeType ErrorCode { get; set; }
        public List<string> Messages { get; set; }
        public int Version { get; set; }
        public string ServerTime { get; set; }
        
        public BaseCommandResponse()
        {
            Messages = new List<string>();
            ServerTime = DateTime.Now.AsUnixTimeStamp().ToString();
        }
        public void SetSuccess()
        {
            Status = true;
            ErrorCode = ErrorCodeType.NoErrorCode;
        }

        public void SetSuccess(string message)
        {
            Status = true;
            Messages.Add(message);
            ErrorCode = ErrorCodeType.NoErrorCode;
        }

        public void SetFail(ErrorCodeType code)
        {
            Status = false;
            ErrorCode = code;
            string message = code.GetDisplayName();
            Messages.Add(message);
        }

        public void SetFail(string message, ErrorCodeType code = ErrorCodeType.NoErrorCode)
        {
            Status = false;
            ErrorCode = code;
            Messages.Add(message);
        }

        public void SetFail(Exception ex, ErrorCodeType code = ErrorCodeType.NoErrorCode)
        {
            Status = false;
            ErrorCode = code;
            string message = $"Message: {ex.Message}";
            Messages.Add(message);
        }

        public void SetFail(IEnumerable<string> messages, ErrorCodeType code = ErrorCodeType.NoErrorCode)
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