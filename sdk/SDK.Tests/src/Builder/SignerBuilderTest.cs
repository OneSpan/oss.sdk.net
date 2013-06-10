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
				.Build();

			Assert.AreEqual ("joe@email.com", signer.Email);
			Assert.AreEqual ("Joe", signer.FirstName);
			Assert.AreEqual ("Smith", signer.LastName);
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
	}
}