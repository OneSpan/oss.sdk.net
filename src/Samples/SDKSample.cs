using System;
using OneSpanSign.Sdk;
using System.IO;
using OneSpanSign.Sdk.Internal;
using System.Collections.Generic;

namespace SDK.Examples
{
    public abstract class SDKSample : BaseSDKSample
    {

        public SDKSample()
        {
            ossClient = OssClientBuilder.NewOssClientBuilder()
                .WithAdditionalHeaders(new Dictionary<String, String>())
                .WithProxyConfiguration(null)
                .WithProperties(props)
                .WithAuthenticationType(props.Get("api.auth.type"))
                .Build();

            SetProperties();
        }

        public SDKSample(string apiKey, string apiUrl)
        {
            ossClient = new OssClient(apiKey, apiUrl);
            SetProperties();
        }
    }
}