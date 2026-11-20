using System;

namespace OneSpanSign.Sdk
{
    /// <summary>
    /// The OneSpan intermediate CA that issues the client certificate presented on mTLS callbacks.
    /// </summary>
    /// <remarks>
    /// Add <see cref="ChainPem"/> to the callback endpoint's trust store. Every other property describes the
    /// intermediate itself; <c>Status</c> is <c>VALID</c>, <c>EXPIRED</c> or <c>NOT_YET_VALID</c>.
    /// </remarks>
    public class ClientCertificateIntermediate
    {
        public string ChainPem { get; set; }

        public string Subject { get; set; }

        public string Issuer { get; set; }

        public string SerialNumber { get; set; }

        public string Sha256Fingerprint { get; set; }

        public DateTime? NotBefore { get; set; }

        public DateTime? NotAfter { get; set; }

        public string KeyAlgorithm { get; set; }

        public string SignatureAlgorithm { get; set; }

        public string Status { get; set; }
    }
}
