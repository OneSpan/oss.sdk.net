using NUnit.Framework;
using System;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Tests
{
    [TestFixture()]
    public class SignerConverterTest
    {
		private OneSpanSign.Sdk.Signer sdkSigner1 = null;
		private OneSpanSign.API.Signer apiSigner1 = null;
		private OneSpanSign.API.Role apiRole = null;
		private SignerConverter converter = null;

		[Test()]
		public void ConvertAPIToAPI()
		{
			apiRole = CreateTypicalAPIRole();
			apiSigner1 = new SignerConverter(apiRole).ToAPISigner();

			Assert.IsNotNull(apiSigner1);
			Assert.AreEqual(apiSigner1, apiRole.Signers[0]);
		}

		[Test()]
		public void ConvertNullAPIToAPI()
		{
			apiRole = null;
			converter = new SignerConverter(apiRole);

			Assert.IsNull(converter.ToAPISigner());
		}

		[Test()]
		public void ConvertNullSDKToAPI()
		{
			sdkSigner1 = null;
			converter = new SignerConverter(sdkSigner1);

			Assert.IsNull(converter.ToAPISigner());
		}

		[Test()]
		public void ConvertSDKToAPI()
		{
			sdkSigner1 = CreateTypicalSDKSigner();
			apiSigner1 = new SignerConverter(sdkSigner1).ToAPISigner();

			Assert.IsNotNull(apiSigner1);
			Assert.AreEqual(apiSigner1.Email, sdkSigner1.Email);
			Assert.AreEqual(apiSigner1.FirstName, sdkSigner1.FirstName);
			Assert.AreEqual(apiSigner1.LastName, sdkSigner1.LastName);
            Assert.AreEqual(apiSigner1.Company, sdkSigner1.Company);
            Assert.AreEqual(apiSigner1.Language, sdkSigner1.Language);
			Assert.AreEqual(apiSigner1.Title, sdkSigner1.Title);
		}

		[Test()]
		public void ConvertSDKSignerToAPIRole()
		{
			sdkSigner1 = CreateTypicalSDKSigner();
			String roleId = System.Guid.NewGuid().ToString().Replace("-", "");
			apiRole = new SignerConverter(sdkSigner1).ToAPIRole(roleId);

			Assert.IsNotNull(apiRole);
			Assert.AreEqual(apiRole.Signers[0].Email, sdkSigner1.Email);
			Assert.AreEqual(apiRole.Signers[0].FirstName, sdkSigner1.FirstName);
			Assert.AreEqual(apiRole.Signers[0].LastName, sdkSigner1.LastName);
            Assert.AreEqual(apiRole.Signers[0].Company, sdkSigner1.Company);
            Assert.AreEqual(apiRole.Signers[0].Language, sdkSigner1.Language);
			Assert.AreEqual(apiRole.Signers[0].Title, sdkSigner1.Title);
			Assert.AreEqual(apiRole.Id, sdkSigner1.Id);
			Assert.AreEqual(apiRole.Name, sdkSigner1.Id);
			Assert.AreEqual(apiRole.EmailMessage.Content, sdkSigner1.Message);

			string attachmentName = apiRole.AttachmentRequirements[0].Name;
			Assert.AreEqual(apiRole.AttachmentRequirements[0].Name, sdkSigner1.GetAttachmentRequirement(attachmentName).Name);
			Assert.AreEqual(apiRole.AttachmentRequirements[0].Description, sdkSigner1.GetAttachmentRequirement(attachmentName).Description);
			Assert.AreEqual(apiRole.AttachmentRequirements[0].Required, sdkSigner1.GetAttachmentRequirement(attachmentName).Required);
		}

        [Test()]
		public void ConvertSDKSignerWithNullEntriesToAPIRole()
        {
			String roleId = System.Guid.NewGuid().ToString().Replace("-", "");
			sdkSigner1 = SignerBuilder.NewSignerWithEmail("abc@test.com")
				.CanChangeSigner()
				.DeliverSignedDocumentsByEmail()
				.SigningOrder(1)
                .WithCompany("ABC Inc.")
                .WithLanguage("fr")
				.WithFirstName("first name")
				.WithLastName("last name")
				.WithTitle("Miss")
				.Build();

			apiRole = new SignerConverter(sdkSigner1).ToAPIRole(roleId);

			Assert.IsNotNull(apiRole);
			Assert.AreEqual(apiRole.Signers[0].Email, sdkSigner1.Email);
			Assert.AreEqual(apiRole.Signers[0].FirstName, sdkSigner1.FirstName);
			Assert.AreEqual(apiRole.Signers[0].LastName, sdkSigner1.LastName);
            Assert.AreEqual(apiRole.Signers[0].Company, sdkSigner1.Company);
            Assert.AreEqual(apiRole.Signers[0].Language, sdkSigner1.Language);
			Assert.AreEqual(apiRole.Signers[0].Title, sdkSigner1.Title);
			Assert.AreEqual(apiRole.Id, roleId);
			Assert.AreEqual(apiRole.Name, roleId);
			Assert.IsNull(apiRole.EmailMessage);
        }

        [Test()]
        public void ConvertAPIToSDK()
        {
            apiRole = CreateTypicalAPIRole();
            apiSigner1 = apiRole.Signers[0];

            sdkSigner1 = new SignerConverter(apiRole).ToSDKSigner();

            Assert.IsNotNull(sdkSigner1);
            Assert.AreEqual(apiSigner1.Email, sdkSigner1.Email);
            Assert.AreEqual(apiSigner1.FirstName, sdkSigner1.FirstName);
            Assert.AreEqual(apiSigner1.LastName, sdkSigner1.LastName);
            Assert.AreEqual(apiSigner1.Company, sdkSigner1.Company);
            Assert.AreEqual(apiSigner1.SignerType, sdkSigner1.SignerType);
            Assert.AreEqual(apiSigner1.Title, sdkSigner1.Title);
            Assert.AreEqual(apiSigner1.Language, sdkSigner1.Language);
            Assert.AreEqual(apiRole.Id, sdkSigner1.Id);
            Assert.AreEqual(apiRole.Index, sdkSigner1.SigningOrder);
            Assert.AreEqual(apiRole.Reassign, sdkSigner1.CanChangeSigner);
            Assert.AreEqual(apiRole.EmailMessage.Content, sdkSigner1.Message);
            Assert.AreEqual(apiSigner1.Delivery.Email, sdkSigner1.DeliverSignedDocumentsByEmail);

            string attachmentName = apiRole.AttachmentRequirements[0].Name;
            OneSpanSign.API.AttachmentRequirement apiAttachment = apiRole.AttachmentRequirements[0];
            OneSpanSign.Sdk.AttachmentRequirement sdkAttachment = sdkSigner1.GetAttachmentRequirement(attachmentName);
            Assert.AreEqual(attachmentName, sdkSigner1.GetAttachmentRequirement(attachmentName).Name);
            Assert.AreEqual(apiAttachment.Description, sdkAttachment.Description);
            Assert.AreEqual(apiAttachment.Required, sdkAttachment.Required);
            Assert.AreEqual(apiAttachment.Status.ToString(), sdkAttachment.Status.ToString());
            Assert.AreEqual(apiAttachment.Comment, sdkAttachment.SenderComment);
        }

		private OneSpanSign.Sdk.Signer CreateTypicalSDKSigner()
		{
            return SignerBuilder.NewSignerWithEmail("abc@test.com")
				.CanChangeSigner()
				.DeliverSignedDocumentsByEmail()
				.SigningOrder(1)
                .WithCompany("ABC Inc")
                .WithLanguage("fr")
				.WithCustomId("1")
				.WithFirstName("first name")
				.WithLastName("last name")
				.WithEmailMessage("Email message")
				.WithTitle("Miss")
				.WithAttachmentRequirement(AttachmentRequirementBuilder.NewAttachmentRequirementWithName("driver's license")
					.WithDescription("Please upload your scanned driver's license")
					.IsRequiredAttachment()
					.Build())
				.Build();
		}

		private OneSpanSign.API.Role CreateTypicalAPIRole()
		{
			OneSpanSign.API.Role apiRole = new OneSpanSign.API.Role();

            OneSpanSign.API.Signer apiSigner = new OneSpanSign.API.Signer();
            apiSigner.Email = "test@abc.com";
            apiSigner.FirstName = "Signer first name";
            apiSigner.LastName = "Signer last name";
            apiSigner.Company = "ABC Inc.";
            apiSigner.SignerType = "THIRD_PARTY_SIGNER";
            apiSigner.Language = "fr";
            apiSigner.Title = "Doctor";

            OneSpanSign.API.Delivery delivery = new OneSpanSign.API.Delivery();
            delivery.Download = true;
            delivery.Email = true;

            apiSigner.Delivery = delivery;
            apiSigner.Id = "1";

			apiRole.AddSigner(apiSigner);
			apiRole.Id = "3";
			apiRole.Name = "Signer name";
			apiRole.Index = 0;
			apiRole.Reassign = true;
			OneSpanSign.API.BaseMessage baseMessage = new OneSpanSign.API.BaseMessage();
			baseMessage.Content = "Base message content.";
			apiRole.EmailMessage = baseMessage;
			apiRole.Locked = true;

			OneSpanSign.API.AttachmentRequirement attachmentRequirement = new OneSpanSign.API.AttachmentRequirement();
			attachmentRequirement.Name = "Driver's license";
			attachmentRequirement.Description = "Please upload your scanned driver's license.";
            attachmentRequirement.Status = OneSpanSign.Sdk.RequirementStatus.INCOMPLETE.getApiValue();
			attachmentRequirement.Required = true;
			attachmentRequirement.Comment = "Attachment was not uploaded";
			apiRole.AddAttachmentRequirement(attachmentRequirement);

			return apiRole;
		}

	}
}

