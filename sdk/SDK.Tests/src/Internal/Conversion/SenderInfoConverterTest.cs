using NUnit.Framework;
using System;
using Silanis.ESL.API;
using Silanis.ESL.SDK;

namespace SDK.Tests
{
    [TestFixture()]
    public class SenderInfoConverterTest
    {
		private const string EMAIL = "bob@email.com";
		private const string FIRST_NAME = "firstName";
		private const string LAST_NAME = "lastName";
		private const string COMPANY = "company";
		private const string TITLE = "title";

        [Test()]
		public void ToSDKFromAPISigner()
        {
			Silanis.ESL.API.Signer signer = new Silanis.ESL.API.Signer();
			signer.Email = EMAIL;
			signer.FirstName = FIRST_NAME;
			signer.LastName = LAST_NAME;
			signer.Company = COMPANY;
			signer.Title = TITLE;

			SenderInfo senderInfo = new SenderInfoConverter(signer).ToSDKSenderInfo();

			Assert.AreEqual(signer.Email, senderInfo.Email);
			Assert.AreEqual(signer.FirstName, senderInfo.FirstName);
			Assert.AreEqual(signer.LastName, senderInfo.LastName);
			Assert.AreEqual(signer.Company, senderInfo.Company);
			Assert.AreEqual(signer.Title, senderInfo.Title);
        }

		[Test()]
		public void ToSDKFromAPISender()
		{
			Silanis.ESL.API.Signer sender = new Silanis.ESL.API.Signer();
			sender.Email = EMAIL;
			sender.FirstName = FIRST_NAME;
			sender.LastName = LAST_NAME;
			sender.Company = COMPANY;
			sender.Title = TITLE;

			SenderInfo senderInfo = new SenderInfoConverter(sender).ToSDKSenderInfo();

			Assert.AreEqual(sender.Email, senderInfo.Email);
			Assert.AreEqual(sender.FirstName, senderInfo.FirstName);
			Assert.AreEqual(sender.LastName, senderInfo.LastName);
			Assert.AreEqual(sender.Company, senderInfo.Company);
			Assert.AreEqual(sender.Title, senderInfo.Title);
		}
    }
}

