using NUnit.Framework;
using System;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
    [TestFixture()]
    public class FieldInjectionExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            FieldInjectionExample example = new FieldInjectionExample( Props.GetInstance() );
            example.Run();
            
            DocumentPackage package = example.EslClient.GetPackage( example.PackageId );
            Assert.IsNotNull(package);
        }
    }
}

