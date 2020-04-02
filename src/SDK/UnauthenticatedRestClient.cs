using System;
using OneSpanSign.Sdk.Internal;

namespace OneSpanSign.Sdk
{
    public class UnauthenticatedRestClient
    {
        private ProxyConfiguration proxyConfiguration;

        public UnauthenticatedRestClient()
        {
        }

        public UnauthenticatedRestClient (ProxyConfiguration proxyConfiguration)
        {
            this.proxyConfiguration = proxyConfiguration;
        }

        public string GetUnauthenticated(string path)
        {
            if (proxyConfiguration != null)
                HttpMethods.proxyConfiguration = proxyConfiguration;
            
            byte[] responseBytes = HttpMethods.GetHttp(path);
            return Converter.ToString(responseBytes);
        }
    }
}

