using System;
using NUnit.Framework;

namespace SDK.Examples
{
    [TestFixture ()]
    public class GetPackageFieldsListExampleTest
    {
        
        [Test ()]
        public void verifyResult ()
        {
            GetPackageFieldsListExample example = new GetPackageFieldsListExample ();
            example.Run ();           
            String actualId;
            example.packages.Results [0].TryGetValue ("id", out actualId);
            Assert.AreEqual (actualId, example.PackageId.Id);
        }

    }
}
