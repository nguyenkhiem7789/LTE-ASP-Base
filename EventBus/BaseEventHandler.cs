using Microsoft.Extensions.Logging;

namespace EventBus
{
    public class BaseEventHandler
    {
        protected readonly ILogger<BaseEventHandler> _logger;

        public BaseEventHandler(ILogger<BaseEventHandler> logger)
        {
            _logger = logger;
        }
    }
}