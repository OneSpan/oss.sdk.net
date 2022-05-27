using NUnit.Framework;
using System;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;
using System.Globalization;
using OneSpanSign.API;

namespace SDK.Tests
{
    [TestFixture()]
    public class DocumentPackageConverterTest
    {
		private OneSpanSign.Sdk.DocumentPackage sdkPackage1 = null;
		private OneSpanSign.API.Package apiPackage1 = null;
		private OneSpanSign.API.Package apiPackage2 = null;
		private DocumentPackageConverter converter = null;

		[Test()]
		public void ConvertNullSDKToAPI()
		{
			sdkPackage1 = null;
			converter = new DocumentPackageConverter(sdkPackage1);
			Assert.IsNull(converter.ToAPIPackage());
		}

		[Test()]
		public void ConvertNullAPIToAPI()
		{
			apiPackage1 = null;
			converter = new DocumentPackageConverter(apiPackage1);
			Assert.IsNull(converter.ToAPIPackage());
		}

		[Test()]
		public void ConvertAPIToAPI()
		{
			apiPackage1 = CreateTypicalAPIPackage();
			converter = new DocumentPackageConverter(apiPackage1);
			apiPackage2 = converter.ToAPIPackage();

			Assert.IsNotNull(apiPackage2);
			Assert.AreEqual(apiPackage2, apiPackage1);
		}

        [Test()]
        public void ConvertAPIToSDK()
        {
            apiPackage1 = CreateTypicalAPIPackage();
            sdkPackage1 = new DocumentPackageConverter(apiPackage1).ToSDKPackage();

            Assert.IsNotNull(sdkPackage1);
            Assert.AreEqual(apiPackage1.Id, sdkPackage1.Id.Id);
            Assert.AreEqual(apiPackage1.Autocomplete, sdkPackage1.Autocomplete);
            Assert.AreEqual(apiPackage1.Description, sdkPackage1.Description);
            Assert.AreEqual(apiPackage1.Due, sdkPackage1.ExpiryDate);
            Assert.AreEqual(apiPackage1.Status.ToString(), sdkPackage1.Status.ToString());
            Assert.AreEqual(apiPackage1.Name, sdkPackage1.Name);
            Assert.AreEqual(apiPackage1.Messages[0].Content, sdkPackage1.Messages[0].Content);
            Assert.AreEqual(apiPackage1.Messages[0].Created, sdkPackage1.Messages[0].Created);
            Assert.AreEqual(apiPackage1.Messages[0].Status.ToString(), sdkPackage1.Messages[0].Status.ToString());
            Assert.AreEqual(apiPackage1.Messages[0].From.FirstName, sdkPackage1.Messages[0].From.FirstName);
            Assert.AreEqual(apiPackage1.Messages[0].From.LastName, sdkPackage1.Messages[0].From.LastName);
            Assert.AreEqual(apiPackage1.Messages[0].From.Email, sdkPackage1.Messages[0].From.Email);
            Assert.AreEqual(apiPackage1.Messages[0].To[0].FirstName, sdkPackage1.Messages[0].To["email2@email.com"].FirstName);
            Assert.AreEqual(apiPackage1.Messages[0].To[0].LastName, sdkPackage1.Messages[0].To["email2@email.com"].LastName);
            Assert.AreEqual(apiPackage1.Messages[0].To[0].Email, sdkPackage1.Messages[0].To["email2@email.com"].Email);
            Assert.AreEqual(apiPackage1.Sender.Email, sdkPackage1.SenderInfo.Email);
            Assert.AreEqual(apiPackage1.Created, sdkPackage1.CreatedDate);

        }

		[Test()]
		public void ConvertSDKToAPI()
		{
			sdkPackage1 = CreateTypicalSDKDocumentPackage();
			apiPackage1 = new DocumentPackageConverter(sdkPackage1).ToAPIPackage();

			Assert.IsNotNull(apiPackage1);
            Assert.AreEqual(apiPackage1.Id, sdkPackage1.Id.ToString());
			Assert.AreEqual(apiPackage1.Name, sdkPackage1.Name);
			Assert.AreEqual(apiPackage1.Description, sdkPackage1.Description);
			Assert.AreEqual(apiPackage1.EmailMessage, sdkPackage1.EmailMessage);
			Assert.AreEqual(apiPackage1.Language, sdkPackage1.Language.ToString());
			Assert.AreEqual(apiPackage1.Due, sdkPackage1.ExpiryDate);
			Assert.AreEqual(apiPackage1.Status, sdkPackage1.Status.getApiValue());
		}

		private OneSpanSign.Sdk.DocumentPackage CreateTypicalSDKDocumentPackage()
		{
			OneSpanSign.Sdk.DocumentPackage sdkDocumentPackage = PackageBuilder.NewPackageNamed("SDK Package Name")
                .WithID(new PackageId("packageId"))
                .WithStatus(DocumentPackageStatus.DRAFT)
				.DescribedAs("typical description")
				.WithEmailMessage("typical email message")
				.WithLanguage(CultureInfo.GetCultureInfo("zh-CN"))
				.Build();

			return sdkDocumentPackage;
		}

		private OneSpanSign.API.Package CreateTypicalAPIPackage()
		{
            User fromUser = new User
            {
                FirstName = "John",
                LastName = "Smith",
                Email = "email@email.com"
            };

            User toUser = new User
            {
                FirstName = "Patty",
                LastName = "Galant",
                Email = "email2@email.com"
            };

            OneSpanSign.API.Sender sender = new OneSpanSign.API.Sender
            {
                Email = "sender@email.com"
            };

            OneSpanSign.API.Message apiMessage = new OneSpanSign.API.Message
            {
                Content = "opt-out reason",
                Status = OneSpanSign.Sdk.MessageStatus.NEW.getApiValue(),
                Created = DateTime.Now,
                From = fromUser
            };
            apiMessage.AddTo(toUser);

            OneSpanSign.API.Package apiPackage = new OneSpanSign.API.Package
            {
                Id = "1",
                Language = "zh-CN",
                Autocomplete = true,
                Consent = "Consent",
                Completed = new DateTime?(),
                Description = "API document package description",
                Due = new DateTime?(),
                Name = "API package name",
                Status = DocumentPackageStatus.DRAFT.getApiValue(),
                Created = DateTime.Now,
                Sender = sender
            };
            apiPackage.AddMessage(apiMessage);

			return apiPackage;
		}
    }
}

