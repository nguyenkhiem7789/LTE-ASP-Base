using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace BaseApplication.Filters
{
    /*public class PermissionAttribute: TypeFilterAttribute
    {
        public PermissionAttribute(string group, string name, bool isRoot, string key = "") : base(typeof(Permiss))
        {
        }
    }*/

    /*public class PermissionFilter : IAsyncActionFilter
    {
        private readonly ILogger<PermissionFilter> _logger;
        public static HashSet<string> KeysExist = new HashSet<string>();
        
        private string Group { get; }
        private string Name { get; }
        private bool IsRoot { get; }
        private string Key { get; }

        public PermissionFilter(string group, string name, bool isRoot, string key, ILogger<PermissionFilter> logger)
        {
            Group = group;
            Name = name;
            IsRoot = isRoot;
            Key = key;
            _logger = logger;
        }
        
        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
            var actionName = descriptor?.ActionName;
            var controllerName = descriptor?.ControllerName;
            string key = (!string.IsNullOrEmpty(Key) ? Key : $"{controllerName}/{actionName}").ToLower();
            if (!KeysExist.Contains(key))
            {
                
            }
        }
    }*/
}