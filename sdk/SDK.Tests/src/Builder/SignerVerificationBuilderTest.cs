using System;
using NUnit.Framework;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;
using Silanis.ESL.API;

namespace SDK.Tests
{
	[TestFixture]
    public class SignerVerificationBuilderTest
	{
		[Test]
        public void BuildsSignerVerificationWithBasicInformation()
		{
            Silanis.ESL.SDK.SignerVerification verification = SignerVerificationBuilder.NewSignerVerification("PROVIDER1")
				.WithPayload ("HJKs2H7UvtFDUi73GswE")
				.Build();

            Assert.AreEqual ("PROVIDER1", verification.TypeId);
            Assert.AreEqual ("HJKs2H7UvtFDUi73GswE", verification.Payload);
		}

		[Test]
		[ExpectedException(typeof(EslException))]
        public void SignerVerificationTypeIdCannotBeEmpty()
		{
            SignerVerificationBuilder.NewSignerVerification(" ")
                .WithPayload ("HJKs2H7UvtFDUi73GswE")
                .Build();
		}

	}
} 	