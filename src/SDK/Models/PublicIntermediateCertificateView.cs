using System;
using Newtonsoft.Json;

namespace OneSpanSign.API
{
    internal class PublicIntermediateCertificateView
    {
        [JsonProperty("chainPem")]
        public string ChainPem { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("issuer")]
        public string Issuer { get; set; }

        [JsonProperty("serialNumber")]
        public string SerialNumber { get; set; }

        [JsonProperty("sha256Fingerprint")]
        public string Sha256Fingerprint { get; set; }

        [JsonProperty("notBefore")]
        public DateTime? NotBefore { get; set; }

        [JsonProperty("notAfter")]
        public DateTime? NotAfter { get; set; }

        [JsonProperty("keyAlgorithm")]
        public string KeyAlgorithm { get; set; }

        [JsonProperty("signatureAlgorithm")]
        public string SignatureAlgorithm { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
