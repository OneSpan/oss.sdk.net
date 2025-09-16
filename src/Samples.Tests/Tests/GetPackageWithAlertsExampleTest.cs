using NUnit.Framework;

namespace SDK.Examples
{
    [TestFixture ()]
    public class GetPackageWithAlertsExampleTest
    {
        
        [Test ()]
        public void verifyResult ()
        {
            GetPackageWithAlertsExample example = new GetPackageWithAlertsExample ();
            example.Run (); 
            
            Assert.IsNotNull(example.RetrievedPackage.Alerts);
        }

    }
}
