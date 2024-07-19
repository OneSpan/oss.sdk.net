using NUnit.Framework;
using OneSpanSign.Sdk.OSSProvider;

namespace OneSpanSign.Sdk.Test.OSSProvider
{
    [TestFixture]
    [TestOf(typeof(OSSAuthClientProvider))]
    public class OSSAuthClientProviderTest
    {

        [Test]
        public void GetInstanceShouldReturnSameInstance()
        {
            OSSAuthClientProvider provider1 = OSSAuthClientProvider.Instance;
            OSSAuthClientProvider provider2 = OSSAuthClientProvider.Instance;
            
            Assert.AreSame(provider1, provider2);
        }
    }
}