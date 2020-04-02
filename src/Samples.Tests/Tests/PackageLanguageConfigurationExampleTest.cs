using System;
using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    [TestFixture()]
    public class PackageLanguageConfigurationExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            PackageLanguageConfigurationExample example = new PackageLanguageConfigurationExample();
            example.Run();

            DocumentPackage retrievedPackage = example.RetrievedPackage;

            Assert.AreEqual(retrievedPackage.Language.Name, "zh-CN");
        }
    }
}

