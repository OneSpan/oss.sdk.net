using System;
using NUnit.Framework;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

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
		[ExpectedException(typeof(EslException))]
		public void SignerEmailCannotBeEmpty()
		{
			SignerBuilder.NewSignerWithEmail (" ").WithFirstName ("Billy").WithLastName ("Bob").Build ();
		}

		[Test]
		[ExpectedException(typeof(EslException))]
		public void SignerFirstNameCannotBeEmpty()
		{
			SignerBuilder.NewSignerWithEmail ("billy@bob.com").WithFirstName (" ").WithLastName ("Bob").Build ();
		}

		[Test]
		[ExpectedException(typeof(EslException))]
		public void SignerLastNameCannotBeEmpty()
		{
			SignerBuilder.NewSignerWithEmail ("billy@bob.com").WithFirstName ("Billy").WithLastName (" ").Build ();
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
		[ExpectedException(typeof(EslException))]
		public void CannotProvideQuestionWithoutAnswer()
		{
			SignerBuilder.NewSignerWithEmail ("billy@bob.com")
				.WithFirstName ("Billy")
				.WithLastName ("Bob")
				.ChallengedWithQuestions (ChallengeBuilder.FirstQuestion("What's your favorite sport?"))
				.Build ();
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
			Assert.AreEqual ("1112223333", signer.PhoneNumber);
		}

		[Test]
		[ExpectedException(typeof(EslException))]
		public void EmptyPhoneNumberNotAllowed()
		{
			SignerBuilder.NewSignerWithEmail ("billy@bob.com")
				.WithFirstName ("Billy")
					.WithLastName ("Bob")
					.WithSMSSentTo (" ")
					.Build ();
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
	}
} 	