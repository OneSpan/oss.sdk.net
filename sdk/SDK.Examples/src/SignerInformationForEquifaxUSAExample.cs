using System;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;
using System.IO;
using System.Collections.Generic;

namespace SDK.Examples
{
    public class SignerInformationForEquifaxUSAExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new SignerInformationForEquifaxUSAExample(Props.GetInstance()).Run();
        }

        public readonly string FIRST_NAME = "First name";
        public readonly string LAST_NAME = "Last name";
        public readonly string STREET_ADDRESS = "1234 Decarie";
        public readonly string CITY = "Montreal";
        public readonly string STATE = "Quebec";
        public readonly string ZIP = "A2A J8J";
        public readonly string SOCIAL_SECURITY_NUMBER = "111-222-333-444";
        public readonly string HOME_PHONE_NUMBER = "1-900-873-8733";
        public readonly Nullable<DateTime> DATE_OF_BIRTH = new DateTime(2002, 2, 2);
        public readonly string SIGNER_EMAIL;

        private string signerId = "signerId";
        private DocumentPackage retrievedPackage;
        private string documentName = "My Document";

        public SignerInformationForEquifaxUSAExample(Props props) : this(props.Get("api.url"), props.Get("api.key"), props.Get("1.email"))
        {
        }

        public SignerInformationForEquifaxUSAExample(string apiKey, string apiUrl, string email1) : base( apiKey, apiUrl )
        {
            this.SIGNER_EMAIL = email1;
        }

        public DocumentPackage RetrievedPackage
        {
            get { return retrievedPackage; }
        }

        override public void Execute()
        {
            Stream file = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);

            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed("SignerInformationForEquifaxUSAExample: " + DateTime.Now)
                .DescribedAs("This is a package created using the e-SignLive SDK")
                .WithSigner(SignerBuilder.NewSignerWithEmail(SIGNER_EMAIL)
                    .WithFirstName("John")
                    .WithLastName("Smith")
                    .WithCustomId(signerId)
                    .ChallengedWithKnowledgeBasedAuthentication(
                            SignerInformationForEquifaxUSABuilder.NewSignerInformationForEquifaxUSA()
                            .WithFirstName(FIRST_NAME)
                            .WithLastName(LAST_NAME)
                            .WithStreetAddress(STREET_ADDRESS)
                            .WithCity(CITY)
                            .WithState(STATE)
                            .WithZip(ZIP)
                            .WithSocialSecurityNumber(SOCIAL_SECURITY_NUMBER)
                            .WithHomePhoneNumber(HOME_PHONE_NUMBER)
                            .WithDateOfBirth(DATE_OF_BIRTH)
                            .Build()))
                    .WithDocument(DocumentBuilder.NewDocumentNamed(documentName)
                    .FromStream(file, DocumentType.PDF)
                    .WithSignature(SignatureBuilder.SignatureFor(SIGNER_EMAIL)
                        .Build())
                    .Build())
                .Build();

            packageId = eslClient.CreateAndSendPackage(superDuperPackage);

            retrievedPackage = eslClient.GetPackage(packageId);
        }
    }
}
