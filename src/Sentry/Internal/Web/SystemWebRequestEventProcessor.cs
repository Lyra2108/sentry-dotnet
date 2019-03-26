#if SYSTEM_WEB
using System;
using System.Web;
using Sentry.Extensibility;

namespace Sentry.Internal.Web
{
    internal class SystemWebRequestEventProcessor : ISentryEventProcessor
    {
        private readonly IRequestPayloadExtractor _payloadExtractor;

        public SystemWebRequestEventProcessor(IRequestPayloadExtractor payloadExtractor)
            => _payloadExtractor = payloadExtractor ?? throw new ArgumentNullException(nameof(payloadExtractor));

        public SentryEvent Process(SentryEvent @event)
        {
            if (HttpContext.Current?.Request is HttpRequest request)
            {
                @event.Request.Data = _payloadExtractor.ExtractPayload(new SystemWebHttpRequest(request));
            }
            return @event;
        }
    }
}
#endif