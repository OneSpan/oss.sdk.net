using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class AuthenticationMethodsExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new AuthenticationMethodsExample(Props.GetInstance()).Run();
        }

        private string email1;
        private string email2;
        private string email3;
        private string sms3;
        private Stream fileStream1;
        private PackageId packageId;

        public static string QUESTION1 = "What's 1+1?";
        public static string ANSWER1 = "2";
        public static string QUESTION2 = "What color's the sky?";
        public static string ANSWER2 = "blue";

        public AuthenticationMethodsExample(Props props)
            : this(props.Get("api.url"), 
                   props.Get("api.key"), 
                   props.Get("1.email"), 
                   props.Get("2.email"), 
                   props.Get("3.email"), 
                   props.Get("3.sms"))
        {
        }

        public AuthenticationMethodsExample(string apiKey, string apiUrl, string email1, string email2, string email3, string sms3) : base(apiKey, apiUrl)
        {
            this.email1 = email1;
            this.email2 = email2;
            this.email3 = email3;
            this.sms3 = sms3;
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        public string Email1
        {
            get
            {
                return email1;
            }
        }

        public string Email2
        {
            get
            {
                return email2;
            }
        }

        public string Email3
        {
            get
            {
                return email3;
            }
        }

        public string Sms3
        {
            get
            {
                return sms3;
            }
        }

        public PackageId PackageId
        {
            get
            {
                return packageId;
            }
        }


        override public void Execute()
        {
            DocumentPackage package = PackageBuilder.NewPackageNamed("C# AuthenticationMethodsExample " + DateTime.Now)
                .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                    .WithFirstName("John1")
                    .WithLastName("Smith1"))
                .WithSigner(SignerBuilder.NewSignerWithEmail(email2)
                    .WithCustomId("signer2")
                    .WithFirstName("John2")
                    .WithLastName("Smith2")
                    .ChallengedWithQuestions(ChallengeBuilder.FirstQuestion(QUESTION1)
                        .Answer(ANSWER1, Challenge.MaskOptions.None)
                        .SecondQuestion(QUESTION2)
                        .Answer(ANSWER2, Challenge.MaskOptions.MaskInput)))
                .WithSigner(SignerBuilder.NewSignerWithEmail(email3)
                    .WithFirstName("John3")
                    .WithLastName("Smith3")
                    .WithSMSSentTo(sms3))
                .WithDocument(DocumentBuilder.NewDocumentNamed("Custom Consent Document")
                    .FromStream(fileStream1, DocumentType.PDF)
                    .WithSignature(SignatureBuilder.SignatureFor(email2)
                        .OnPage(0)
                        .AtPosition(100, 100)))
                .Build();

            packageId = eslClient.CreatePackage(package);
            eslClient.SendPackage(packageId);
        }
    }
}

