using NUnit.Framework;
using System;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
    [TestFixture()]
    public class SignerOrderingExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            SignerOrderingExample example = new SignerOrderingExample( Props.GetInstance() );
            example.Run();
            
            DocumentPackage package = example.EslClient.GetPackage( example.PackageId );
            Assert.IsNotNull(package);
        }
    }
}

