using System;
using NUnit.Framework;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
    [TestFixture()]
    public class PackageDataContainsSDKVersionTest
    {
        [Test()]
        public void VerifyResult()
        {
            BasicPackageCreationExample example = new BasicPackageCreationExample( Props.GetInstance() );
            example.Run();
            DocumentPackage documentPackage = example.EslClient.GetPackage(example.PackageId);
            
            Assert.IsTrue(documentPackage.Attributes.Contents.ContainsKey( "sdk" ));
            Assert.IsTrue(documentPackage.Attributes.Contents["sdk"].ToString().Contains(".NET"));
        }
    }
}

