using System;

namespace OneSpanSign.Sdk {
    public class ApiToken {
        public string AccessToken { get; set; }
        public long ExpiresAt { get; set; }
    }
}