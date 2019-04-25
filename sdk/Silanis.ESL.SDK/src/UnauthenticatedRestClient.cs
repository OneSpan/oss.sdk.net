using System;
using Silanis.ESL.SDK.Internal;

namespace Silanis.ESL.SDK
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

