using NUnit.Framework;
using System;
using Silanis.ESL.SDK;
using Silanis.ESL.API;
using Silanis.ESL.SDK.Builder;

namespace SDK.Tests
{
    [TestFixture()]
    public class SignerVerificationConverterTest
    {
        private SignerVerification sdkSignerVerification = null;
        private Verification apiSignerVerification = null;
        private SignerVerificationConverter converter;

		[Test()]
		public void ConvertAPIToAPI()
		{
            apiSignerVerification = CreateTypicalAPIVerification();
            Verification verification = new SignerVerificationConverter(apiSignerVerification).ToAPISignerVerification();

            Assert.IsNotNull(verification);
            Assert.AreEqual(verification, apiSignerVerification);
		}

		[Test()]
		public void ConvertNullAPIToAPI()
		{
            apiSignerVerification = null;
            converter = new SignerVerificationConverter(apiSignerVerification);

			Assert.IsNull(converter.ToAPISignerVerification());
		}

		[Test()]
		public void ConvertNullSDKToAPI()
		{
            sdkSignerVerification = null;
            converter = new SignerVerificationConverter(sdkSignerVerification);

            Assert.IsNull(converter.ToAPISignerVerification());
		}

		[Test()]
		public void ConvertSDKToAPI()
		{
            sdkSignerVerification = CreateTypicalSDKSignerVerification();
            apiSignerVerification = new SignerVerificationConverter(sdkSignerVerification).ToAPISignerVerification();

            Assert.IsNotNull(apiSignerVerification);
            Assert.AreEqual(apiSignerVerification.TypeId, sdkSignerVerification.TypeId);
            Assert.AreEqual(apiSignerVerification.Payload, sdkSignerVerification.Payload);
		}

        [Test()]
        public void ConvertSDKToSDK()
        {
            sdkSignerVerification = CreateTypicalSDKSignerVerification();
            SignerVerification signerVerification = new SignerVerificationConverter(sdkSignerVerification).ToSDKSignerVerification();

            Assert.IsNotNull(signerVerification);
            Assert.AreEqual(signerVerification, sdkSignerVerification);
        }

        [Test()]
        public void ConvertAPIToSDK()
        {
            apiSignerVerification = CreateTypicalAPIVerification();
            sdkSignerVerification = new SignerVerificationConverter(apiSignerVerification).ToSDKSignerVerification();

            Assert.IsNotNull(sdkSignerVerification);
            Assert.AreEqual(sdkSignerVerification.TypeId, apiSignerVerification.TypeId);
            Assert.AreEqual(sdkSignerVerification.Payload, apiSignerVerification.Payload);
        }

        private SignerVerification CreateTypicalSDKSignerVerification()
		{
            return SignerVerificationBuilder.NewSignerVerification("PROVIDER1")
                .WithPayload("dNM24duiIN3Mfa3IYs")
				.Build();
		}

        private Verification CreateTypicalAPIVerification()
		{
            Verification verification = new Verification();
            verification.TypeId = "PROVIDER2";
            verification.Payload = "dNM2N3Mfa3IYs4duiI";

            return verification;
		}

	}
}

