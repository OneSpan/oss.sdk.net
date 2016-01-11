using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class UpdateSignerExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new UpdateSignerExample(Props.GetInstance()).Run();
        }

        public string email1, email2, email3, sms1;
        private Stream fileStream1;
        public DocumentPackage updatedPackage;

        public const string SIGNER1_CUSTOM_ID = "signerId1";
        public const string SIGNER1_FIRST_NAME = "John1";
        public const string SIGNER1_LAST_NAME = "Smith1";

        public const string SIGNER2_CUSTOM_ID = "signerId2";
        public const string SIGNER2_FIRST_NAME = "Patty";
        public const string SIGNER2_LAST_NAME = "Galant";

        public const string SIGNER3_FIRST_NAME = "John2";
        public const string SIGNER3_LAST_NAME = "Smith2";
        public const string SIGNER3_FIRST_QUESTION = "What's 1+1?";
        public const string SIGNER3_FIRST_ANSWER= "2";
        public const string SIGNER3_SECOND_QUESTION = "What color's the sky?";
        public const string SIGNER3_SECOND_ANSWER= "blue";

        public UpdateSignerExample(Props props) : this(props.Get("api.key"), props.Get("api.url"), props.Get("1.email"), props.Get("2.email"), props.Get("3.email"), props.Get("1.sms"))
        {
        }

        public UpdateSignerExample(string apiKey, string apiUrl, string email1, string email2, string email3, string sms1) : base( apiKey, apiUrl )
        {
            this.email1 = email1;
            this.email2 = email2;
            this.email3 = email3;
            this.sms1 = sms1;
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        override public void Execute()
        {
            Signer signer1 = SignerBuilder.NewSignerWithEmail(email1)
                .WithFirstName(SIGNER1_FIRST_NAME)
                    .WithLastName(SIGNER1_LAST_NAME)
                    .WithCustomId(SIGNER1_CUSTOM_ID)
                    .Build();

            Signer signer2 = SignerBuilder.NewSignerWithEmail(email2)
                .WithFirstName(SIGNER2_FIRST_NAME)
                    .WithLastName(SIGNER2_LAST_NAME)
                    .WithCustomId(SIGNER2_CUSTOM_ID)
                    .Build();

            Signer signer3 = SignerBuilder.NewSignerWithEmail(email3)
                .WithFirstName(SIGNER3_FIRST_NAME)
                    .WithLastName(SIGNER3_LAST_NAME)
                    .ChallengedWithQuestions(ChallengeBuilder.FirstQuestion(SIGNER3_FIRST_QUESTION)
                                             .Answer(SIGNER3_FIRST_ANSWER)
                                             .SecondQuestion(SIGNER3_SECOND_QUESTION)
                                             .Answer(SIGNER3_SECOND_ANSWER))
                    .WithCustomId(SIGNER1_CUSTOM_ID)
                    .Build();

            Signer signer4 = SignerBuilder.NewSignerWithEmail(email2)
                .WithFirstName(SIGNER2_FIRST_NAME)
                    .WithLastName(SIGNER2_LAST_NAME)
                    .WithSMSSentTo(sms1)
                    .WithCustomId(SIGNER2_CUSTOM_ID).Build();

            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed( "UpdateSignerExample " + DateTime.Now )
                .DescribedAs( "This is a package created using the e-SignLive SDK" )
                    .WithSigner(signer1)
                    .WithSigner(signer2)
                    .WithDocument( DocumentBuilder.NewDocumentNamed( "First Document" )
                                  .FromStream( fileStream1, DocumentType.PDF )
                                  .WithSignature( SignatureBuilder.SignatureFor( email1 )
                                   .OnPage( 0 )
                                   .AtPosition( 30, 100 ) )
                                  .WithSignature( SignatureBuilder.SignatureFor( email2 )
                                   .OnPage( 0 )
                                   .AtPosition( 30, 400 ) ))
                    .Build();

            packageId = eslClient.CreatePackage( superDuperPackage );
            eslClient.SendPackage( packageId );
            retrievedPackage = eslClient.GetPackage(packageId);

            eslClient.ChangePackageStatusToDraft(packageId);
            eslClient.PackageService.UpdateSigner(packageId, signer3);
            eslClient.PackageService.UpdateSigner(packageId, signer4);

            eslClient.SendPackage(packageId);
            updatedPackage = eslClient.GetPackage(packageId);
        }
    }
}

