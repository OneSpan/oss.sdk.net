using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
    /// <summary>
    /// Example class demonstrating a transaction with five PLACEHOLDER role signers, each configured
    /// with a different authentication method: SMS, SSO, KBA, Q&amp;A and QASMS.
    ///
    /// OneSpan Sign accepts a placeholder's SMS, KBA, Q&amp;A and QASMS authentication without the
    /// detailed information (phone number, questions/answers, KBA identity information) being filled
    /// in, since a placeholder does not have a known identity yet; that information is only required
    /// once the placeholder is replaced by a named recipient.
    /// </summary>
    public class PlaceholderAuthenticationExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new PlaceholderAuthenticationExample().Run();
        }

        public readonly string DOCUMENT_NAME = "Placeholder Authentication Document";
        public readonly string DOCUMENT_ID = "doc1";
        public readonly string PACKAGE_DESCRIPTION = "This package demonstrates placeholder authentication created using the OneSpan Sign SDK";

        public readonly string SMS_PLACEHOLDER_ID = "sms-placeholder-id";
        public readonly string SSO_PLACEHOLDER_ID = "sso-placeholder-id";
        public readonly string KBA_PLACEHOLDER_ID = "kba-placeholder-id";
        public readonly string QNA_PLACEHOLDER_ID = "qna-placeholder-id";
        public readonly string QASMS_PLACEHOLDER_ID = "qasms-placeholder-id";

        override public void Execute()
        {
            PlaceholderSigner smsPlaceholder = new PlaceholderSigner(SMS_PLACEHOLDER_ID);
            PlaceholderSigner ssoPlaceholder = new PlaceholderSigner(SSO_PLACEHOLDER_ID);
            PlaceholderSigner kbaPlaceholder = new PlaceholderSigner(KBA_PLACEHOLDER_ID);
            PlaceholderSigner qnaPlaceholder = new PlaceholderSigner(QNA_PLACEHOLDER_ID);
            PlaceholderSigner qasmsPlaceholder = new PlaceholderSigner(QASMS_PLACEHOLDER_ID);

            DocumentPackage package = PackageBuilder.NewPackageNamed(PackageName)
                .DescribedAs(PACKAGE_DESCRIPTION)
                // SMS authentication without a phone number.
                .WithSigner(SignerBuilder.NewPlaceholderSigner(smsPlaceholder)
                    .WithAuthentication(new Authentication(AuthenticationMethod.SMS)))
                .WithSigner(SignerBuilder.NewPlaceholderSigner(ssoPlaceholder)
                    .WithSSOAuthentication())
                // KBA authentication without the detailed signer information.
                .WithSigner(SignerBuilder.NewPlaceholderSigner(kbaPlaceholder)
                    .ChallengedWithKnowledgeBasedAuthentication(SignerInformationForLexisNexisBuilder.NewSignerInformationForLexisNexis()))
                // Q&A authentication without any questions or answers.
                .WithSigner(SignerBuilder.NewPlaceholderSigner(qnaPlaceholder)
                    .ChallengedWithQuestions(ChallengeBuilder.FirstQuestion(null)))
                // QASMS authentication without a phone number, questions or answers.
                .WithSigner(SignerBuilder.NewPlaceholderSigner(qasmsPlaceholder)
                    .ChallengedWithQASMS(QASMSBuilder.FirstQuestion(null, null)))
                .WithDocument(DocumentBuilder.NewDocumentNamed(DOCUMENT_NAME)
                    .WithId(DOCUMENT_ID)
                    .FromStream(fileStream1, DocumentType.PDF)
                    .WithSignature(SignatureBuilder.SignatureFor(smsPlaceholder)
                        .OnPage(0)
                        .AtPosition(100, 100))
                    .WithSignature(SignatureBuilder.SignatureFor(ssoPlaceholder)
                        .OnPage(0)
                        .AtPosition(100, 200))
                    .WithSignature(SignatureBuilder.SignatureFor(kbaPlaceholder)
                        .OnPage(0)
                        .AtPosition(100, 300))
                    .WithSignature(SignatureBuilder.SignatureFor(qnaPlaceholder)
                        .OnPage(0)
                        .AtPosition(100, 400))
                    .WithSignature(SignatureBuilder.SignatureFor(qasmsPlaceholder)
                        .OnPage(0)
                        .AtPosition(100, 500)))
                .Build();

            packageId = ossClient.CreatePackageOneStep(package);
            retrievedPackage = ossClient.GetPackage(packageId);
        }
    }
}
