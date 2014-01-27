using NUnit.Framework;
using System;
using Silanis.ESL.API;
using Silanis.ESL.SDK;

namespace SDK.Tests
{
    [TestFixture()]
    public class SenderConverterTest
    {
		private const string EMAIL = "bob@email.com";
		private const string FIRST_NAME = "firstName";
		private const string LAST_NAME = "lastName";
		private const string COMPANY = "company";
		private const string TITLE = "title";

		[Test()]
		public void ToSDKFromAPISender()
		{

			Silanis.ESL.API.Sender sender = new Silanis.ESL.API.Sender();
			sender.Email = EMAIL;
			sender.FirstName = FIRST_NAME;
			sender.LastName = LAST_NAME;
			sender.Company = COMPANY;
			sender.Title = TITLE;

			SenderInfo senderInfo = new SenderConverter(sender).ToSDKSenderInfo();

			Assert.IsNotNull(senderInfo);
			Assert.AreEqual(sender.Email, senderInfo.Email);
			Assert.AreEqual(sender.FirstName, senderInfo.FirstName);
			Assert.AreEqual(sender.LastName, senderInfo.LastName);
			Assert.AreEqual(sender.Company, senderInfo.Company);
			Assert.AreEqual(sender.Title, senderInfo.Title);
		}

		[Test()]
		public void ToAPIFromSDKSenderInfo()
		{
			Silanis.ESL.SDK.SenderInfo senderInfo = new Silanis.ESL.SDK.SenderInfo();
			senderInfo.Email = EMAIL;
			senderInfo.FirstName = FIRST_NAME;
			senderInfo.LastName = LAST_NAME;
			senderInfo.Company = COMPANY;
			senderInfo.Title = TITLE;

			Silanis.ESL.API.Sender sender = new SenderConverter(senderInfo).ToAPISender();

			Assert.IsNotNull(sender);
			Assert.AreEqual(senderInfo.Email, sender.Email);
			Assert.AreEqual(senderInfo.FirstName, sender.FirstName);
			Assert.AreEqual(senderInfo.LastName, sender.LastName);
			Assert.AreEqual(senderInfo.Company, sender.Company);
			Assert.AreEqual(senderInfo.Title, sender.Title);
		}

		[Test()]
		[ExpectedException( typeof( ArgumentNullException) )]
		public void FromSDKNull()
		{
			Silanis.ESL.SDK.SenderInfo senderInfo = null;
			SenderConverter converter = new SenderConverter(senderInfo);
		}

		[Test()]
		[ExpectedException( typeof( ArgumentNullException) )]
		public void FromAPINull()
		{
			Silanis.ESL.API.Sender sender = null;
			SenderConverter converter = new SenderConverter(sender);
		}
    }
}

