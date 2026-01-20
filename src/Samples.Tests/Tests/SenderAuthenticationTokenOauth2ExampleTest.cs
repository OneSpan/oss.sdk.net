using NUnit.Framework;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.OSSProvider;

namespace SDK.Examples
{
    [TestFixture()]
    public class SenderAuthenticationTokenOauth2ExampleTest
    {
        [Test()]
		public void VerifyResult()
        {
			SenderAuthenticationTokenOauth2Example example = new SenderAuthenticationTokenOauth2Example();
			
			OSSAuthClientConfig config = new OSSAuthClientConfig.Builder()
				.WithAuthenticationServer(example.oAuthServerUrl)
				.WithClientId(example.clientId)
				.WithClientSecret(example.clientSecret)
				.WithApiUrl(example.apiUrl)
				.WithSenderId(example.senderId)//optional
				.WithDelegatorId(example.delegatorId)//optional
				.Build();

			OssClient ossClient = OSSAuthClientProvider.Instance.GetOssClient(config);

			example.SetOssClient(ossClient);
			
			example.Run();

			Assert.IsNotNull(example.SenderSessionId);
        }
    }
}

