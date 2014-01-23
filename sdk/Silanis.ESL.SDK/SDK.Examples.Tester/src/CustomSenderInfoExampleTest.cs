using NUnit.Framework;
using System;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
    [TestFixture()]
    public class CustomSenderInfoExampleTest
    {
        [Test()]
		public void VerifyResult()
        {
			CustomSenderInfoExample example = new CustomSenderInfoExample(Props.GetInstance());
			example.Run();

			DocumentPackage package = example.EslClient.GetPackage(example.PackageId);

			Assert.IsNotNull(package.SenderInfo);
			Assert.AreEqual(example.Package.SenderInfo.FirstName, package.SenderInfo.FirstName);
			Assert.AreEqual(example.Package.SenderInfo.LastName, package.SenderInfo.LastName);
			Assert.AreEqual(example.Package.SenderInfo.Company, package.SenderInfo.Company);
			Assert.AreEqual(example.Package.SenderInfo.Title, package.SenderInfo.Title);
        }
    }
}

