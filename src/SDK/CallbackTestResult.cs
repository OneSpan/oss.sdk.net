using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
    /// <summary>
    /// The result of a test delivery.
    /// </summary>
    /// <remarks>
    /// <c>Outcome</c> is <c>SUCCESS</c> for a 2xx answer, otherwise the reason it failed, for example <c>HTTP_4XX</c>,
    /// <c>TIMEOUT</c> or <c>SSRF_BLOCKED</c>. <c>RequestHeaders</c> has credential values masked. <c>Response</c> is
    /// <c>null</c> when no HTTP response was received.
    /// </remarks>
    public class CallbackTestResult
    {
        public string Outcome { get; set; }

        public string Url { get; set; }

        public string SentPayload { get; set; }

        public Dictionary<string, string> RequestHeaders { get; set; }

        public long DurationMs { get; set; }

        public CallbackTestResponse Response { get; set; }

        public string FailureDetail { get; set; }
    }
}
