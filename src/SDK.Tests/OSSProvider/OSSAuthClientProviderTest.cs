using NUnit.Framework;
using OneSpanSign.Sdk.OSSProvider;

namespace OneSpanSign.Sdk.Test.OSSProvider
{
    [TestFixture]
    [TestOf(typeof(OSSAuthClientProvider))]
    public class OSSAuthClientProviderTest
    
    {
        private string oAuthUrl = "oAuthUrl";
        private string clientId = "clientId";
        private string clientSecret = "clientSecret";
        private string apiUrl = "apiUrl";
        private string senderId = "senderId";
        private string delegatorId = "delegatorId";
        [Test]
        public void GetInstanceShouldReturnSameInstance()
        {
            OSSAuthClientProvider provider1 = OSSAuthClientProvider.Instance;
            OSSAuthClientProvider provider2 = OSSAuthClientProvider.Instance;
            
            Assert.AreSame(provider1, provider2);
        }

        [Test]
        public void WhenGetOssClientWithTheSameClientIdAndSecretThenShouldReturnTheExistingClient()
        {
            OSSAuthClientConfig existingConfig = new OSSAuthClientConfig.Builder()
                .WithAuthenticationServer(oAuthUrl)
                .WithClientId(clientId)
                .WithClientSecret(clientSecret)
                .WithApiUrl(apiUrl)
                .Build();
                
            OssClient existingOssClient = OSSAuthClientProvider.Instance.GetOssClient(existingConfig);
            
            OSSAuthClientConfig newConfig = new OSSAuthClientConfig.Builder()
                .WithAuthenticationServer(oAuthUrl)
                .WithClientId(clientId)
                .WithClientSecret(clientSecret)
                .WithApiUrl(apiUrl)
                .Build();
            
            OssClient updatedOssClient = OSSAuthClientProvider.Instance.GetOssClient(newConfig);
            Assert.True(object.ReferenceEquals(existingOssClient, updatedOssClient));

        }
        
        [Test]
        public void  WhenGetOssClientWithTheSameClientIdButSenderIdDefinedThenShouldReturnANewClient()
        {
            OSSAuthClientConfig existingConfig = new OSSAuthClientConfig.Builder()
                .WithAuthenticationServer(oAuthUrl)
                .WithClientId(clientId)
                .WithClientSecret(clientSecret)
                .WithApiUrl(apiUrl)
                .Build();
                
            OssClient existingOssClient = OSSAuthClientProvider.Instance.GetOssClient(existingConfig);
            
            OSSAuthClientConfig newConfig = new OSSAuthClientConfig.Builder()
                .WithAuthenticationServer(oAuthUrl)
                .WithClientId(clientId)
                .WithClientSecret(clientSecret)
                .WithApiUrl(apiUrl)
                .WithSenderId(senderId)
                .Build();
            
            OssClient newOssClient = OSSAuthClientProvider.Instance.GetOssClient(newConfig);
            Assert.False(object.ReferenceEquals(existingOssClient, newOssClient));
            Assert.AreEqual(newOssClient.oauth2TokenConfig.SenderId, newConfig.SenderId);
            Assert.IsNull(newOssClient.oauth2TokenConfig.DelegatorId);
        }
        
        [Test]
        public void  WhenGetOssClientWithTheSameClientIdButSenderIdIsDifferentThenShouldReturnANewClient()
        {
            OSSAuthClientConfig existingConfig = new OSSAuthClientConfig.Builder()
                .WithAuthenticationServer(oAuthUrl)
                .WithClientId(clientId)
                .WithClientSecret(clientSecret)
                .WithApiUrl(apiUrl)
                .WithSenderId("test")
                .Build();
                
            OssClient existingOssClient = OSSAuthClientProvider.Instance.GetOssClient(existingConfig);
            
            OSSAuthClientConfig newConfig = new OSSAuthClientConfig.Builder()
                .WithAuthenticationServer(oAuthUrl)
                .WithClientId(clientId)
                .WithClientSecret(clientSecret)
                .WithApiUrl(apiUrl)
                .WithSenderId(senderId)
                .Build();
            
            OssClient newOssClient = OSSAuthClientProvider.Instance.GetOssClient(newConfig);
            Assert.False(object.ReferenceEquals(existingOssClient, newOssClient));
            Assert.AreEqual(newOssClient.oauth2TokenConfig.SenderId, newConfig.SenderId);
            Assert.AreNotEqual(newOssClient.oauth2TokenConfig.SenderId, existingOssClient.oauth2TokenConfig.SenderId);
        }
        
        [Test]
        public void WhenGetOssClientWithTheSameClientIdButDefinedSenderIdDelegatorIdThenShouldReturnANewClient()
        {
            OSSAuthClientConfig existingConfig = new OSSAuthClientConfig.Builder()
                .WithAuthenticationServer(oAuthUrl)
                .WithClientId(clientId)
                .WithClientSecret(clientSecret)
                .WithApiUrl(apiUrl)
                .Build();
                
            OssClient existingOssClient = OSSAuthClientProvider.Instance.GetOssClient(existingConfig);
            
            OSSAuthClientConfig newConfig = new OSSAuthClientConfig.Builder()
                .WithAuthenticationServer(oAuthUrl)
                .WithClientId(clientId)
                .WithClientSecret(clientSecret)
                .WithApiUrl(apiUrl)
                .WithSenderId(senderId)
                .WithDelegatorId(delegatorId)
                .Build();
            
            OssClient newOssClient = OSSAuthClientProvider.Instance.GetOssClient(newConfig);
            Assert.False(object.ReferenceEquals(existingOssClient, newOssClient));
            Assert.AreEqual(newOssClient.oauth2TokenConfig.SenderId, newConfig.SenderId);
            Assert.AreEqual(newOssClient.oauth2TokenConfig.DelegatorId, newConfig.DelegatorId);
        }
        
        [Test]
        public void WhenGetOssClientWithTheSameClientIdButDifferentDelegatorIdThenShouldReturnANewClient()
        {
            OSSAuthClientConfig existingConfig = new OSSAuthClientConfig.Builder()
                .WithAuthenticationServer(oAuthUrl)
                .WithClientId(clientId)
                .WithClientSecret(clientSecret)
                .WithApiUrl(apiUrl)
                .WithSenderId(senderId)
                .WithDelegatorId("test")
                .Build();
                
            OssClient existingOssClient = OSSAuthClientProvider.Instance.GetOssClient(existingConfig);
            
            OSSAuthClientConfig newConfig = new OSSAuthClientConfig.Builder()
                .WithAuthenticationServer(oAuthUrl)
                .WithClientId(clientId)
                .WithClientSecret(clientSecret)
                .WithApiUrl(apiUrl)
                .WithSenderId(senderId)
                .WithDelegatorId(delegatorId)
                .Build();
            
            OssClient newOssClient = OSSAuthClientProvider.Instance.GetOssClient(newConfig);
            Assert.False(object.ReferenceEquals(existingOssClient, newOssClient));
            Assert.AreEqual(newOssClient.oauth2TokenConfig.SenderId, newConfig.SenderId);
            Assert.AreEqual(newOssClient.oauth2TokenConfig.DelegatorId, newConfig.DelegatorId);
            Assert.AreNotEqual(newOssClient.oauth2TokenConfig.DelegatorId, existingOssClient.oauth2TokenConfig.DelegatorId);
        }
    }
}