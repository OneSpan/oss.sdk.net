using System.Collections.Generic;
using NUnit.Framework;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Tests
{
	[TestFixture]
	public class SignerBuilderTest
	{
		[Test]
		public void BuildsSignerWithBasicInformation()
		{
			Signer signer = SignerBuilder.NewSignerWithEmail("joe@email.com")
				.WithFirstName ("Joe")
				.WithLastName("Smith")
				.SigningOrder (2)
				.Build();

			Assert.AreEqual ("joe@email.com", signer.Email);
			Assert.AreEqual ("Joe", signer.FirstName);
			Assert.AreEqual ("Smith", signer.LastName);
			Assert.AreEqual (2, signer.SigningOrder);
		}

		[Test]
		public void SignerEmailCannotBeEmpty()
		{
            Assert.Throws<OssException>(() => SignerBuilder.NewSignerWithEmail (" ").WithFirstName ("Billy").WithLastName ("Bob").Build ());
		}

		[Test]
		public void SignerFirstNameCannotBeEmpty()
		{
            Assert.Throws<OssException>(() => SignerBuilder.NewSignerWithEmail ("billy@bob.com").WithFirstName (" ").WithLastName ("Bob").Build ());
		}

		[Test]
		public void SignerLastNameCannotBeEmpty()
		{
            Assert.Throws<OssException>(() => SignerBuilder.NewSignerWithEmail ("billy@bob.com").WithFirstName ("Billy").WithLastName (" ").Build ());
		}

		[Test]
		public void CanSpecifyTitleAndCompany()
		{
			Signer signer = SignerBuilder.NewSignerWithEmail ("billy@bob.com")
				.WithFirstName ("Billy")
				.WithLastName ("Bob")
				.WithTitle ("Managing Director")
				.WithCompany ("Acme Inc")
				.Build ();

			Assert.AreEqual ("Managing Director", signer.Title);
			Assert.AreEqual ("Acme Inc", signer.Company);
		}
		
		[Test]
		public void CannotSetSMSNotificationWithoutPhone()
		{
			Assert.Throws<OssException>(() => SignerBuilder.NewSignerWithEmail ("billy@bob.com")
				.WithFirstName ("Billy")
				.WithLastName ("Bob")
				.WithNotificationMethods(NotificationMethodsBuilder.NewNotificationMethods()
					.WithPrimaryMethods(NotificationMethod.EMAIL, NotificationMethod.SMS)
					)
				.Build ());
		}
		
		[Test]
		public void CanSpecifyNotificationMethods()
		{
			Signer signer = SignerBuilder.NewSignerWithEmail ("billy@bob.com")
				.WithFirstName ("Billy")
				.WithLastName ("Bob")
				.WithNotificationMethods(NotificationMethodsBuilder.NewNotificationMethods()
					.WithPhoneNumber("+16356489274")
					.WithPrimaryMethods(NotificationMethod.EMAIL, NotificationMethod.SMS)
				)
				.Build ();
			
			Assert.NotNull(signer.NotificationMethods);
			Assert.IsTrue(signer.NotificationMethods.Primary.Contains(NotificationMethod.EMAIL) && 
			              signer.NotificationMethods.Primary.Contains(NotificationMethod.SMS));
		}

		[Test]
		public void AuthenticationDefaultsToEmail()
		{
			Signer signer = SignerBuilder.NewSignerWithEmail ("billy@bob.com")
				.WithFirstName ("Billy")
				.WithLastName ("Bob")
				.Build ();

			Assert.AreEqual (AuthenticationMethod.EMAIL, signer.AuthenticationMethod);
		}

		[Test]
		public void ProvidingQuestionsAndAnswersSetsAuthenticationMethodToChallenge()
		{
			Signer signer = SignerBuilder.NewSignerWithEmail ("billy@bob.com")
				.WithFirstName ("Billy")
				.WithLastName ("Bob")
				.ChallengedWithQuestions (ChallengeBuilder.FirstQuestion("What's your favorite sport?")
					                          .Answer("golf"))
				.Build ();

			Assert.AreEqual (AuthenticationMethod.CHALLENGE, signer.AuthenticationMethod);
		}

		[Test]
		public void SavesProvidesQuestionsAndAnswers()
		{
			Signer signer = SignerBuilder.NewSignerWithEmail ("billy@bob.com")
				.WithFirstName ("Billy")
					.WithLastName ("Bob")
					.ChallengedWithQuestions (ChallengeBuilder.FirstQuestion("What's your favorite sport?")
					                          .Answer("golf")
					                          .SecondQuestion("Do you have a pet?")
					                          .Answer("yes"))
					.Build ();

			Assert.AreEqual (signer.ChallengeQuestion[0], new Challenge("What's your favorite sport?", "golf", Challenge.MaskOptions.None));
			Assert.AreEqual (signer.ChallengeQuestion[1], new Challenge("Do you have a pet?", "yes", Challenge.MaskOptions.None));
		}

		[Test]
		public void CannotProvideQuestionWithoutAnswer()
		{
            Assert.Throws<OssException>(() => SignerBuilder.NewSignerWithEmail ("billy@bob.com")
				.WithFirstName ("Billy")
				.WithLastName ("Bob")
				.ChallengedWithQuestions (ChallengeBuilder.FirstQuestion("What's your favorite sport?"))
				.Build ());
		}

		[Test]
		public void ProvidingSignerCellPhoneNumberSetsUpSMSAuthentication() 
		{
			Signer signer = SignerBuilder.NewSignerWithEmail ("billy@bob.com")
				.WithFirstName ("Billy")
				.WithLastName ("Bob")
				.WithSMSSentTo ("1112223333")
				.Build ();

			Assert.AreEqual (AuthenticationMethod.SMS, signer.AuthenticationMethod);
			Assert.AreEqual ("1112223333", signer.AuthPhoneNumber);
		}

        [Test]
        public void SetsUpSSOAuthentication ()
        {
            Signer signer = SignerBuilder.NewSignerWithEmail ("billy@bob.com")
                .WithFirstName ("Billy")
                .WithLastName ("Bob")
                .WithSSOAuthentication ()
                .Build ();

            Assert.AreEqual (AuthenticationMethod.SSO, signer.AuthenticationMethod);
        }
        
        [Test]
        public void SetsUpIDVAuthentication ()
        {
	        Signer signer = SignerBuilder.NewSignerWithEmail ("billy@bob.com")
		        .WithFirstName ("Billy")
		        .WithLastName ("Bob")
		        .WithIDVAuthentication(IdvWorkflowBuilder.NewIdvWorkflow("00000000-0000-0001-0000-200000000055")
			        .WithType("DVF")
			        .WithTenant("oss")
			        .Build())
		        .Build ();

	        Assert.AreEqual (AuthenticationMethod.IDV, signer.AuthenticationMethod);
        }

		[Test]
		public void EmptyPhoneNumberNotAllowed()
		{
            Assert.Throws<OssException>(() => SignerBuilder.NewSignerWithEmail ("billy@bob.com")
				.WithFirstName ("Billy")
					.WithLastName ("Bob")
					.WithSMSSentTo (" ")
					.Build ());
		}

		[Test]
		public void CanConfigureSignedDocumentDelivery()
		{
			Signer signer = SignerBuilder.NewSignerWithEmail ("billy@bob.com")
				.WithFirstName ("Billy")
					.WithLastName ("Bob")
					.DeliverSignedDocumentsByEmail()
					.Build ();

			Assert.IsTrue (signer.DeliverSignedDocumentsByEmail);
		}

		[Test]
		public void CanSetAndGetAttachmentRequirements()
		{
			AttachmentRequirement attachmentRequirement = AttachmentRequirementBuilder.NewAttachmentRequirementWithName("Driver's license")
				.WithDescription("Please upload scanned driver's license.")
				.IsRequiredAttachment()
				.Build();

			Signer signer = SignerBuilder.NewSignerWithEmail("billy@bob.com")
				.WithFirstName("Billy")
				.WithLastName("Bob")
				.WithAttachmentRequirement(attachmentRequirement)
				.Build();

			Assert.AreEqual(signer.Attachments.Count, 1);
			Assert.AreEqual(signer.GetAttachmentRequirement("Driver's license").Name, attachmentRequirement.Name);
			Assert.AreEqual(signer.GetAttachmentRequirement("Driver's license").Description, attachmentRequirement.Description);
			Assert.AreEqual(signer.GetAttachmentRequirement("Driver's license").Required, attachmentRequirement.Required);
			Assert.AreEqual(signer.GetAttachmentRequirement("Driver's license").Status, attachmentRequirement.Status);
		}

		[Test]
		public void CanAddTwoAttachmentRequirement()
		{
			AttachmentRequirement attachmentRequirement1 = AttachmentRequirementBuilder.NewAttachmentRequirementWithName("Driver's license")
				.WithDescription("Please upload scanned driver's license.")
				.IsRequiredAttachment()
				.Build();
			AttachmentRequirement attachmentRequirement2 = AttachmentRequirementBuilder.NewAttachmentRequirementWithName("Medicare card")
				.WithDescription("Please upload scanned medicare card.")
				.IsRequiredAttachment()
				.Build();

			Signer signer = SignerBuilder.NewSignerWithEmail("billy@bob.com")
				.WithFirstName("Billy")
				.WithLastName("Bob")
				.WithAttachmentRequirement(attachmentRequirement1)
				.WithAttachmentRequirement(attachmentRequirement2)
				.Build();

			Assert.AreEqual(signer.Attachments.Count, 2);
			Assert.AreEqual(signer.GetAttachmentRequirement("Driver's license").Name, attachmentRequirement1.Name);
			Assert.AreEqual(signer.GetAttachmentRequirement("Driver's license").Description, attachmentRequirement1.Description);
			Assert.AreEqual(signer.GetAttachmentRequirement("Driver's license").Required, attachmentRequirement1.Required);
			Assert.AreEqual(signer.GetAttachmentRequirement("Driver's license").Status, attachmentRequirement1.Status);
			Assert.AreEqual(signer.GetAttachmentRequirement("Medicare card").Name, attachmentRequirement2.Name);
			Assert.AreEqual(signer.GetAttachmentRequirement("Medicare card").Description, attachmentRequirement2.Description);
			Assert.AreEqual(signer.GetAttachmentRequirement("Medicare card").Required, attachmentRequirement2.Required);
			Assert.AreEqual(signer.GetAttachmentRequirement("Medicare card").Status.ToString(), attachmentRequirement2.Status.ToString());
		}
	}
} 	