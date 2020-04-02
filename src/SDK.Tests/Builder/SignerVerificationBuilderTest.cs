using System;
using NUnit.Framework;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;
using OneSpanSign.API;

namespace SDK.Tests
{
	[TestFixture]
    public class SignerVerificationBuilderTest
	{
		[Test]
        public void BuildsSignerVerificationWithBasicInformation()
		{
            OneSpanSign.Sdk.SignerVerification verification = SignerVerificationBuilder.NewSignerVerification("PROVIDER1")
				.WithPayload ("HJKs2H7UvtFDUi73GswE")
				.Build();

            Assert.AreEqual ("PROVIDER1", verification.TypeId);
            Assert.AreEqual ("HJKs2H7UvtFDUi73GswE", verification.Payload);
		}

		[Test]
        public void SignerVerificationTypeIdCannotBeEmpty()
		{
            Assert.Throws<OssException>(() => SignerVerificationBuilder.NewSignerVerification(" ")
                .WithPayload ("HJKs2H7UvtFDUi73GswE")
                .Build());
		}

	}
} 	