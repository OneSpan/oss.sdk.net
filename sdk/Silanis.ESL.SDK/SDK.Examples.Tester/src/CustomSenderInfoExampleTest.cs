using NUnit.Framework;
using System;
using Silanis.ESL.SDK;
using System.Collections.Generic;

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
			Assert.AreEqual(CustomSenderInfoExample.SENDER_FIRST_NAME, package.SenderInfo.FirstName);
			Assert.AreEqual(CustomSenderInfoExample.SENDER_SECOND_NAME, package.SenderInfo.LastName);
			Assert.AreEqual(CustomSenderInfoExample.SENDER_COMPANY, package.SenderInfo.Company);
			Assert.AreEqual(CustomSenderInfoExample.SENDER_TITLE, package.SenderInfo.Title);

            IDictionary<string, Silanis.ESL.SDK.Sender> senders = example.EslClient.AccountService.GetSenders(Direction.ASCENDING, new PageRequest(0, -1));
            Assert.IsTrue(senders.ContainsKey(example.SenderEmail));
            Assert.AreEqual(senders[example.SenderEmail].Language, "fr");
        }
    }
}

