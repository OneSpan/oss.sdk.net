using NUnit.Framework;
using System;
using OneSpanSign.Sdk;
using OneSpanSign.API;
using OneSpanSign.Sdk.Builder;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Immutable;

namespace SDK.Tests
{
    [TestFixture]
    public class SignatureConverterTest
    {
        private Signature sdkSignature1 = null;
        private Signature sdkSignature2 = null;
        private Approval apiApproval1 = null;
        private Approval apiApproval2 = null;
        private Package apiPackage;
        private SignatureConverter converter = null;
        private readonly string apiRoleId = "APIRoleID";

        [Test()]
        public void ConvertNullSDKToAPI()
        {
            sdkSignature1 = null;
            converter = new SignatureConverter(sdkSignature1);
            Assert.IsNull(converter.ToAPIApproval(), "Converter didn't return a null api object for a null sdk object");
        }

        [Test()]
        public void ConvertNullAPIToSDK()
        {
            apiApproval1 = null;
            converter = new SignatureConverter(apiApproval1, apiPackage);
            Assert.IsNull(converter.ToSDKSignature(), "Converter didn't return a null sdk object for a null api object");
        }

        [Test()]
        public void ConvertNullSDKToSDK()
        {
            sdkSignature1 = null;
            converter = new SignatureConverter(sdkSignature1);
            Assert.IsNull(converter.ToSDKSignature(), "Converter didn't return a null sdk object for a null sdk object");
        }

        [Test()]
        public void ConvertNullAPIToAPI()
        {
            apiApproval1 = null;
            converter = new SignatureConverter(apiApproval1, apiPackage);
            Assert.IsNull(converter.ToAPIApproval(), "Converter didn't return a null api object for a null api object");
        }

        [Test()]
        public void ConvertSDKToSDK()
        {
            sdkSignature1 = CreateTypicalSDKSignature();
            sdkSignature2 = new SignatureConverter(sdkSignature1).ToSDKSignature();
            Assert.IsNotNull(sdkSignature2, "Converter returned a null sdk object for a non null sdk object");
            Assert.AreEqual(sdkSignature2, sdkSignature1, "Converter didn't return the same non-null sdk object it was given");
        }

        [Test()]
        public void ConvertAPIToAPI()
        {
            apiApproval1 = CreateTypicalAPIApproval();
            apiApproval2 = new SignatureConverter(apiApproval1, apiPackage).ToAPIApproval();
            Assert.IsNotNull(apiApproval2,"Converter returned a null api object for a non null api object");
            Assert.AreEqual(apiApproval2, apiApproval1, "Converter didn't return the same non-null api object it was given");
        }


        [Test()]
        public void ConvertAPIToSDK()
        {
            apiApproval1 = CreateTypicalAPIApproval();
            apiPackage = CreateTypicalAPIDocumentPackage();
            sdkSignature1 = new SignatureConverter(apiApproval1, apiPackage).ToSDKSignature();
            Assert.IsNotNull(sdkSignature1, "Converter returned a null sdk object for a non null api object");
            Assert.IsTrue(sdkSignature1.EnforceCaptureSignature, "EnforceCaptureSignature was not correctly set");
            Assert.AreEqual(sdkSignature1.Name, apiApproval1.Fields[0].Name, "Name was not correctly set");
            Assert.AreEqual(sdkSignature1.Height, apiApproval1.Fields[0].Height, "Height was not correctly set");
            Assert.AreEqual(sdkSignature1.Width, apiApproval1.Fields[0].Width, "Width was not correctly set");
            Assert.AreEqual(sdkSignature1.Page, apiApproval1.Fields[0].Page, "Page was not correctly set");
            Assert.AreEqual(sdkSignature1.X, apiApproval1.Fields[0].Left, "Left position was not correctly set");
            Assert.AreEqual(sdkSignature1.Y, apiApproval1.Fields[0].Top, "Top position was not correctly set");
            Assert.AreEqual(sdkSignature1.FontSize, apiApproval1.Fields[0].FontSize, "Font size was not correctly set");
            Assert.AreEqual(sdkSignature1.Tooltip, apiApproval1.Tooltip, "Tooltip was not correctly set");
        }

        [Test()]
        public void ConvertSDKToAPI()
        {
            sdkSignature1 = CreateTypicalSDKSignature();
            apiApproval1 = new SignatureConverter(sdkSignature1).ToAPIApproval();

            Assert.IsNotNull(apiApproval1, "Converter returned a null api object for a non null sdk object");
            Assert.IsTrue(apiApproval1.EnforceCaptureSignature, "EnforceCaptureSignature was not correctly set");
            Assert.AreEqual(apiApproval1.Fields[0].Name, sdkSignature1.Name, "Name was not correctly set");
            Assert.AreEqual(apiApproval1.Fields[0].Height, sdkSignature1.Height, "Height was not correctly set");
            Assert.AreEqual(apiApproval1.Fields[0].Width, sdkSignature1.Width, "Width was not correctly set");
            Assert.AreEqual(apiApproval1.Fields[0].Page, sdkSignature1.Page, "Page was not correctly set");
            Assert.AreEqual(apiApproval1.Fields[0].Left, sdkSignature1.X, "Left position was not correctly set");
            Assert.AreEqual(apiApproval1.Fields[0].Top, sdkSignature1.Y, "Top position was not correctly set");
            Assert.AreEqual(apiApproval1.Fields[0].FontSize, sdkSignature1.FontSize, "Font size was not correctly set");
            Assert.AreEqual(apiApproval1.Fields[0].Tooltip, sdkSignature1.Tooltip, "Tooltip was not correctly set");
        }

        private Signature CreateTypicalSDKSignature()
        {
            return SignatureBuilder.AcceptanceFor("abc@test.com")
                    .AtPosition(100, 100)
                    .WithName("signature")
                    .WithSize(100, 100)
                    .WithFontSize(10)
                    .WithTooltip("Signature tooltip message")
                    .EnableEnforceCaptureSignature()
                    .OnPage(0)
                    .Build();
        }

        private Approval CreateTypicalAPIApproval()
        {
            Approval apiApproval = new Approval();
            OneSpanSign.API.Field apiSignature = new OneSpanSign.API.Field
            {
                Name = "apiSignature",
                Top = 100.0,
                Left = 200.0,
                Height = 200.0,
                Width = 100.0,
                Page = 0,
                FontSize = 20
            };
            apiSignature.Tooltip = "Signature tooltip message";
            apiApproval.AddField(apiSignature);
            apiApproval.EnforceCaptureSignature = true;
            apiApproval.Name = "apiSignature";
            apiApproval.Role = apiRoleId;

            return apiApproval;
        }

        /**
         * Create an API DocumentPackage.
         *
         * @return API DocumentPackage.
         */
        private Package CreateTypicalAPIDocumentPackage()
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
                Status = MessageStatus.NEW.getApiValue(),
                From = fromUser
            };
            apiMessage.AddTo(toUser);

            Package apiDocumentPackage = new Package
            {
                Id = "1",
                Language = "en",
                Autocomplete = true,
                Consent = "Consent",
                Completed = new DateTime?(),
                Description = "API document package description",
                Due = new DateTime?(),
                Name = "API package name",
                Status = DocumentPackageStatus.DRAFT.getApiValue(),
                Created = new DateTime?(),
                TimezoneId = "Canada/Mountain",
                Sender = sender
            };
            apiDocumentPackage.AddMessage(apiMessage);

            Role role = CreateTypicalAPIRole();
            apiDocumentPackage.AddRole(role);

            return apiDocumentPackage;
        }

        private OneSpanSign.API.Role CreateTypicalAPIRole()
        {
            OneSpanSign.API.Delivery delivery = new OneSpanSign.API.Delivery
            {
                Download = true,
                Email = true
            };

            OneSpanSign.API.BaseMessage baseMessage = new OneSpanSign.API.BaseMessage
            {
                Content = "Base message content."
            };

            OneSpanSign.API.Signer apiSigner = new OneSpanSign.API.Signer
            {
                Email = "test@abc.com",
                FirstName = "Signer first name",
                LastName = "Signer last name",
                Company = "ABC Inc.",
                SignerType = "THIRD_PARTY_SIGNER",
                Language = "fr",
                Title = "Doctor",
                Delivery = delivery,
                Id = "1"
            };

            OneSpanSign.API.Role apiRole = new OneSpanSign.API.Role
            {
                Id = apiRoleId,
                Name = "Signer name",
                Index = 0,
                Reassign = true,
                EmailMessage = baseMessage,
                Locked = true
            };
            apiRole.AddSigner(apiSigner);

            OneSpanSign.API.AttachmentRequirement attachmentRequirement = new OneSpanSign.API.AttachmentRequirement
            {
                Name = "Driver's license",
                Description = "Please upload your scanned driver's license.",
                Status = OneSpanSign.Sdk.RequirementStatus.INCOMPLETE.getApiValue(),
                Required = true,
                Comment = "Attachment was not uploaded"
            };

            apiRole.AddAttachmentRequirement(attachmentRequirement);

            return apiRole;
        }
    }
}
