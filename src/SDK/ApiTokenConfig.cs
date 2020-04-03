using System;

namespace OneSpanSign.Sdk
{
    public class ApiTokenConfig
    {
        public string BaseUrl { get; set; }
        public string ClientAppId { get; set; }
        public string ClientAppSecret { get; set; }
        public ApiTokenType TokenType { get; set; }
        public string SenderEmail { get; set; }
    }

    public enum ApiTokenType
    {
        OWNER, SENDER
    }
}