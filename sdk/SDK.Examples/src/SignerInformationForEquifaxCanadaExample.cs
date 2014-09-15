using System;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;
using System.IO;
using System.Collections.Generic;

namespace SDK.Examples
{
    public class SignerInformationForEquifaxCanadaExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new SignerInformationForEquifaxCanadaExample(Props.GetInstance()).Run();
        }
            
        public readonly string FIRST_NAME = "First name";
        public readonly string LAST_NAME = "Last name";
        public readonly string STREET_ADDRESS = "1234 Decarie";
        public readonly string CITY = "Montreal";
        public readonly string PROVINCE = "Quebec";
        public readonly string POSTAL_CODE = "A2A J8J";
        public readonly string TIME_AT_ADDRESS = "1";
        public readonly string DRIVERS_LICENSE_INDICATOR = "License indicator";
        public readonly string SOCIAL_INSURANCE_NUMBER = "111-222-333-444";
        public readonly string HOME_PHONE_NUMBER = "1-900-873-8733";
        public readonly Nullable<DateTime> DATE_OF_BIRTH = new DateTime(2000, 1, 1);
        public readonly string SIGNER_EMAIL;

        private string signerId = "signerId";
        private DocumentPackage retrievedPackage;
        private string documentName = "My Document";

        public SignerInformationForEquifaxCanadaExample(Props props) : this(props.Get("api.url"), props.Get("api.key"), props.Get("1.email"))
        {
        }

        public SignerInformationForEquifaxCanadaExample(string apiKey, string apiUrl, string signerEmail) : base( apiKey, apiUrl )
        {
            this.SIGNER_EMAIL = signerEmail;
        }

        public DocumentPackage RetrievedPackage
        {
            get { return retrievedPackage; }
        }

        override public void Execute()
        {
            Stream file = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);

            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed("SignerInformationForEquifaxCanadaExample: " + DateTime.Now)
                .DescribedAs("This is a package created using the e-SignLive SDK")
                .WithSigner(SignerBuilder.NewSignerWithEmail(SIGNER_EMAIL)
                    .WithFirstName("John")
                    .WithLastName("Smith")
                    .WithCustomId(signerId)
                    .ChallengedWithKnowledgeBasedAuthentication(
                            SignerInformationForEquifaxCanadaBuilder.NewSignerInformationForEquifaxCanada()
                            .WithFirstName(FIRST_NAME)
                            .WithLastName(LAST_NAME)
                            .WithStreetAddress(STREET_ADDRESS)
                            .WithCity(CITY)
                            .WithProvince(PROVINCE)
                            .WithPostalCode(POSTAL_CODE)
                            .WithTimeAtAddress(TIME_AT_ADDRESS)
                            .WithDriversLicenseIndicator(DRIVERS_LICENSE_INDICATOR)
                            .WithSocialInsuranceNumber(SOCIAL_INSURANCE_NUMBER)
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
