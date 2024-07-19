using NUnit.Framework;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.OSSProvider;

namespace SDK.Examples
{
    [TestFixture()]
    public class PackageInformationOAuth2ExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            PackageInformationOAuth2Example example = new PackageInformationOAuth2Example();

            OSSAuthClientConfig config = new OSSAuthClientConfig.Builder()
                .WithAuthenticationServer(example.oAuthServerUrl)
                .WithClientId(example.clientId)
                .WithClientSecret(example.clientSecret)
                .WithApiUrl(example.apiUrl)
                .Build();

            OssClient ossClient = OSSAuthClientProvider.Instance.GetOssClient(config);

            example.SetOssClient(ossClient);
            
            example.Run();

            Assert.IsNotNull(example.supportConfiguration);

            Assert.IsNotNull(example.supportConfiguration.Email);
            Assert.IsNotEmpty(example.supportConfiguration.Email);

            Assert.IsNotNull(example.supportConfiguration.Phone);
            Assert.IsNotEmpty(example.supportConfiguration.Phone);
        }
    }
}

