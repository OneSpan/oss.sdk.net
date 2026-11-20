using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
    public class CallbackTestResponse
    {
        public int Status { get; set; }

        public Dictionary<string, string> Headers { get; set; }
    }
}
