using NUnit.Framework;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.OSSProvider;

namespace SDK.Examples
{
    [TestFixture()]
    public class SignerAuthenticationTokenAndDelegateExampleTest
    {

        [Test()]
		public void VerifyResult()
        {
			SignerAuthenticationTokenAndDelegateExample example = new SignerAuthenticationTokenAndDelegateExample();
			
			OSSAuthClientConfig config = new OSSAuthClientConfig.Builder()
				.WithAuthenticationServer(example.oAuthServerUrl)
				.WithClientId(example.clientId)
				.WithClientSecret(example.clientSecret)
				.WithApiUrl(example.apiUrl)
				.Build();
			
			OssClient ossClient = OSSAuthClientProvider.Instance.GetOssClient(config);
			
			example.SetOssClient(ossClient);
			
			example.Run();

            Assert.IsNotNull(example.SignerSessionIdForMultiUse);
			Assert.IsNotNull(example.SignerSessionIdForSingleUse);
        }
    }
}

