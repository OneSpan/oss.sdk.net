using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Tests
{
    [TestFixture]
    public class KnowledgeBasedAuthenticationStatusConverterTest
    {
        private KnowledgeBasedAuthenticationStatus sdkKnowledgeBasedAuthenticationStatus1;
        private string apiKnowledgeBasedAuthenticationStatus1;

       
        [Test]
        public void ConvertApiNotYetAttemptedDToSdkNotYetAttempted()
        {
            apiKnowledgeBasedAuthenticationStatus1 = "NOT_YET_ATTEMPTED";
            sdkKnowledgeBasedAuthenticationStatus1 = new KnowledgeBasedAuthenticationStatusConverter(apiKnowledgeBasedAuthenticationStatus1).ToSDKKnowledgeBasedAuthenticationStatus();

            Assert.AreEqual(apiKnowledgeBasedAuthenticationStatus1, sdkKnowledgeBasedAuthenticationStatus1.getApiValue());
        }

        [Test]
        public void ConvertApiFailedToSdkFailed()
        {
            apiKnowledgeBasedAuthenticationStatus1 = "FAILED";
            sdkKnowledgeBasedAuthenticationStatus1 = new KnowledgeBasedAuthenticationStatusConverter(apiKnowledgeBasedAuthenticationStatus1).ToSDKKnowledgeBasedAuthenticationStatus();

            Assert.AreEqual(apiKnowledgeBasedAuthenticationStatus1, sdkKnowledgeBasedAuthenticationStatus1.getApiValue());
        }

        [Test]
        public void ConvertApiPassedToSdkPassed()
        {
            apiKnowledgeBasedAuthenticationStatus1 = "PASSED";
            sdkKnowledgeBasedAuthenticationStatus1 = new KnowledgeBasedAuthenticationStatusConverter(apiKnowledgeBasedAuthenticationStatus1).ToSDKKnowledgeBasedAuthenticationStatus();

            Assert.AreEqual(sdkKnowledgeBasedAuthenticationStatus1.getApiValue(), apiKnowledgeBasedAuthenticationStatus1);
        }

        [Test]
        public void ConvertApiPassedToSdkInvalidSigner()
        {
            apiKnowledgeBasedAuthenticationStatus1 = "INVALID_SIGNER";
            sdkKnowledgeBasedAuthenticationStatus1 = new KnowledgeBasedAuthenticationStatusConverter(apiKnowledgeBasedAuthenticationStatus1).ToSDKKnowledgeBasedAuthenticationStatus();

            Assert.AreEqual(sdkKnowledgeBasedAuthenticationStatus1.getApiValue(), apiKnowledgeBasedAuthenticationStatus1);
        }

        [Test]
        public void ConvertApiPassedToSdkUpdated()
        {
            apiKnowledgeBasedAuthenticationStatus1 = "UPDATED";
            sdkKnowledgeBasedAuthenticationStatus1 = new KnowledgeBasedAuthenticationStatusConverter(apiKnowledgeBasedAuthenticationStatus1).ToSDKKnowledgeBasedAuthenticationStatus();

            Assert.AreEqual(sdkKnowledgeBasedAuthenticationStatus1.getApiValue(), apiKnowledgeBasedAuthenticationStatus1);
        }

        [Test]
        public void ConvertApiPassedToSdkLocked()
        {
            apiKnowledgeBasedAuthenticationStatus1 = "LOCKED";
            sdkKnowledgeBasedAuthenticationStatus1 = new KnowledgeBasedAuthenticationStatusConverter(apiKnowledgeBasedAuthenticationStatus1).ToSDKKnowledgeBasedAuthenticationStatus();

            Assert.AreEqual(sdkKnowledgeBasedAuthenticationStatus1.getApiValue(), apiKnowledgeBasedAuthenticationStatus1);
        }

        [Test]
        public void ConvertApiUnknownValueToUnrecognizedKnowledgeBasedAuthenticationStatus()
        {
            apiKnowledgeBasedAuthenticationStatus1 = "NEWLY_ADDED_KBA_STATUS";
            sdkKnowledgeBasedAuthenticationStatus1 = new KnowledgeBasedAuthenticationStatusConverter(apiKnowledgeBasedAuthenticationStatus1).ToSDKKnowledgeBasedAuthenticationStatus();

            Assert.AreEqual(sdkKnowledgeBasedAuthenticationStatus1.getApiValue(), apiKnowledgeBasedAuthenticationStatus1);
        }

        [Test]
        public void ConvertSdkNotYetAttemptedToApiNotYetAttempted()
        {
            sdkKnowledgeBasedAuthenticationStatus1 = KnowledgeBasedAuthenticationStatus.NOT_YET_ATTEMPTED;
            apiKnowledgeBasedAuthenticationStatus1 = new KnowledgeBasedAuthenticationStatusConverter(sdkKnowledgeBasedAuthenticationStatus1).ToAPIKnowledgeBasedAuthenticationStatus();

            Assert.AreEqual("NOT_YET_ATTEMPTED", apiKnowledgeBasedAuthenticationStatus1);
        }

        [Test]
        public void ConvertSdkFailedToApiFailed()
        {
            sdkKnowledgeBasedAuthenticationStatus1 = KnowledgeBasedAuthenticationStatus.FAILED;
            apiKnowledgeBasedAuthenticationStatus1 = new KnowledgeBasedAuthenticationStatusConverter(sdkKnowledgeBasedAuthenticationStatus1).ToAPIKnowledgeBasedAuthenticationStatus();

            Assert.AreEqual("FAILED", apiKnowledgeBasedAuthenticationStatus1);
        }

        [Test]
        public void ConvertSdkPassedToApiChallenge()
        {
            sdkKnowledgeBasedAuthenticationStatus1 = KnowledgeBasedAuthenticationStatus.PASSED;
            apiKnowledgeBasedAuthenticationStatus1 = new KnowledgeBasedAuthenticationStatusConverter(sdkKnowledgeBasedAuthenticationStatus1).ToAPIKnowledgeBasedAuthenticationStatus();

            Assert.AreEqual("PASSED", apiKnowledgeBasedAuthenticationStatus1);
        }

        [Test]
        public void ConvertSdkInvalidSignerToApiChallenge()
        {
            sdkKnowledgeBasedAuthenticationStatus1 = KnowledgeBasedAuthenticationStatus.INVALID_SIGNER;
            apiKnowledgeBasedAuthenticationStatus1 = new KnowledgeBasedAuthenticationStatusConverter(sdkKnowledgeBasedAuthenticationStatus1).ToAPIKnowledgeBasedAuthenticationStatus();

            Assert.AreEqual("INVALID_SIGNER", apiKnowledgeBasedAuthenticationStatus1);
        }

        [Test]
        public void ConvertSdkUpdatedToApiChallenge()
        {
            sdkKnowledgeBasedAuthenticationStatus1 = KnowledgeBasedAuthenticationStatus.UPDATED;
            apiKnowledgeBasedAuthenticationStatus1 = new KnowledgeBasedAuthenticationStatusConverter(sdkKnowledgeBasedAuthenticationStatus1).ToAPIKnowledgeBasedAuthenticationStatus();

            Assert.AreEqual("UPDATED", apiKnowledgeBasedAuthenticationStatus1);
        }

        [Test]
        public void ConvertSdkLockedToApiChallenge()
        {
            sdkKnowledgeBasedAuthenticationStatus1 = KnowledgeBasedAuthenticationStatus.LOCKED;
            apiKnowledgeBasedAuthenticationStatus1 = new KnowledgeBasedAuthenticationStatusConverter(sdkKnowledgeBasedAuthenticationStatus1).ToAPIKnowledgeBasedAuthenticationStatus();

            Assert.AreEqual("LOCKED", apiKnowledgeBasedAuthenticationStatus1);
        }

        [Test]
        public void ConvertSdkUnrecognizedKnowledgeBasedAuthenticationStatusToApiUnknownValue()
        {
            apiKnowledgeBasedAuthenticationStatus1 = "NEWLY_ADDED_KBA_STATUS";
            var unrecognizedKnowledgeBasedAuthenticationStatus = KnowledgeBasedAuthenticationStatus.valueOf(apiKnowledgeBasedAuthenticationStatus1);
            var actualApiValue = new KnowledgeBasedAuthenticationStatusConverter(unrecognizedKnowledgeBasedAuthenticationStatus).ToAPIKnowledgeBasedAuthenticationStatus();

            Assert.AreEqual(apiKnowledgeBasedAuthenticationStatus1, actualApiValue);
        }

    }
}

