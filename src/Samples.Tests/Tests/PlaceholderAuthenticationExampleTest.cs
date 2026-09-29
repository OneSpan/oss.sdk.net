using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    /// <summary>
    /// Verifies that OneSpan Sign accepts a transaction whose placeholders are configured with SMS,
    /// KBA, Q&amp;A and QASMS authentication even when the detailed information (phone number,
    /// questions/answers, KBA identity information) is left unfilled, since a placeholder has no
    /// known identity yet.
    /// </summary>
    [TestFixture()]
    public class PlaceholderAuthenticationExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            PlaceholderAuthenticationExample example = new PlaceholderAuthenticationExample();
            example.Run();

            Signer smsPlaceholder = example.RetrievedPackage.GetPlaceholder(example.SMS_PLACEHOLDER_ID);
            Signer ssoPlaceholder = example.RetrievedPackage.GetPlaceholder(example.SSO_PLACEHOLDER_ID);
            Signer kbaPlaceholder = example.RetrievedPackage.GetPlaceholder(example.KBA_PLACEHOLDER_ID);
            Signer qnaPlaceholder = example.RetrievedPackage.GetPlaceholder(example.QNA_PLACEHOLDER_ID);
            Signer qasmsPlaceholder = example.RetrievedPackage.GetPlaceholder(example.QASMS_PLACEHOLDER_ID);

            Assert.IsNotNull(smsPlaceholder);
            Assert.IsNotNull(ssoPlaceholder);
            Assert.IsNotNull(kbaPlaceholder);
            Assert.IsNotNull(qnaPlaceholder);
            Assert.IsNotNull(qasmsPlaceholder);

            Assert.IsTrue(smsPlaceholder.IsNewPlaceholderSigner());
            Assert.IsTrue(ssoPlaceholder.IsNewPlaceholderSigner());
            Assert.IsTrue(kbaPlaceholder.IsNewPlaceholderSigner());
            Assert.IsTrue(qnaPlaceholder.IsNewPlaceholderSigner());
            Assert.IsTrue(qasmsPlaceholder.IsNewPlaceholderSigner());

            // SMS authentication is accepted without a phone number.
            Assert.AreEqual(AuthenticationMethod.SMS, smsPlaceholder.Authentication.Method);
            Assert.IsTrue(string.IsNullOrEmpty(smsPlaceholder.Authentication.PhoneNumber));

            Assert.AreEqual(AuthenticationMethod.SSO, ssoPlaceholder.Authentication.Method);

            // KBA authentication is accepted without the detailed signer information.
            Assert.IsNotNull(kbaPlaceholder.KnowledgeBasedAuthentication);
            Assert.IsNotNull(kbaPlaceholder.KnowledgeBasedAuthentication.SignerInformationForLexisNexis);

            // Q&A authentication is accepted without any questions or answers.
            Assert.AreEqual(AuthenticationMethod.CHALLENGE, qnaPlaceholder.Authentication.Method);
            Assert.AreEqual(0, qnaPlaceholder.Authentication.Challenges.Count);

            // QASMS authentication is accepted without a phone number, questions or answers.
            Assert.AreEqual(AuthenticationMethod.QASMS, qasmsPlaceholder.Authentication.Method);
            Assert.AreEqual(0, qasmsPlaceholder.Authentication.Challenges.Count);
        }
    }
}
