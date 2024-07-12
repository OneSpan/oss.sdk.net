using System;
using System.IO;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
    /// <summary>
    /// Example of how to configure the Question&Answer authentication method for a signer. The answer is given for testing 
    /// purposes. Never include the answer when creating packages for actual customers.
    /// </summary>
    public class SignerQnASMSChallengeExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new SignerQnASMSChallengeExample().Run();
        }

        public readonly string FIRST_QUESTION = "What's your favorite sport? (answer: golf)";
        public readonly string FIRST_ANSWER = "golf";
        public readonly string SECOND_QUESTION = "What music instrument do you play? (answer: drums)";
        public readonly string SECOND_ANSWER = "drums";
        public static string CHALLENGE_CHALLENGE_TYPE = "CHALLENGE";
        public readonly string PHONE_NUMBER = "+12042345678";
        
        override public void Execute()
        {
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed(PackageName)
                .DescribedAs("This is a Q&A authentication example")
                .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                    .WithFirstName("John")
                    .WithLastName("Smith")
                    .ChallengedWithQASMS(QASMSBuilder.FirstQuestion(CHALLENGE_CHALLENGE_TYPE, FIRST_QUESTION)
                            .Answer(FIRST_ANSWER)
                            .SecondQuestion(CHALLENGE_CHALLENGE_TYPE, SECOND_QUESTION)
                            .AnswerWithMaskInput(SECOND_ANSWER)
                        .smsPhoneNumber(PHONE_NUMBER)))
                .WithDocument(DocumentBuilder.NewDocumentNamed("First Document")
                    .FromStream(fileStream1, DocumentType.PDF)
                    .WithSignature(SignatureBuilder.SignatureFor(email1)
                        .OnPage(0)
                        .AtPosition(199, 100)))
                .Build();

            packageId = ossClient.CreatePackage(superDuperPackage);

            ossClient.SendPackage(packageId);
            retrievedPackage = ossClient.GetPackage(packageId);
        }
    }
}
