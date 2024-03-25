using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace OneSpanSign.API
{
    [JsonConverter(typeof(StringEnumConverter))]
    internal enum Connector
    {
        TRUST_VAULT, SFTP, IPAAS
    }
}