using System;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;
using System.IO;
using System.Collections.Generic;

namespace SDK.Examples
{
    public class MixedSignerAuthenticationExample: SDKSample
    {
        public static void Main(string[] args)
        {
            new MixedSignerAuthenticationExample(Props.GetInstance()).Run();
        }

        public Signer SIGNER_WITH_AUTHENTICATION_EQUIFAX_CANADA;
        public Signer SIGNER_WITH_AUTHENTICATION_EQUIFAX_USA;

        private string emailForSignerWithAuthenticationEquifaxCanada;
        private string emailForSignerWithAuthenticationEquifaxUsa;

        private DocumentPackage retrievedPackage;
        private string documentName = "My Document";

        public MixedSignerAuthenticationExample(Props props) : this(props.Get("api.url"), props.Get("api.key"), props.Get("1.email"), props.Get("2.email"))
        {
        }

        public MixedSignerAuthenticationExample(string apiKey, string apiUrl, string signer1Email, string signer2Email) : base( apiKey, apiUrl )
        {
            this.emailForSignerWithAuthenticationEquifaxCanada = signer1Email;
            this.emailForSignerWithAuthenticationEquifaxUsa = signer2Email;
        }

        public DocumentPackage RetrievedPackage
        {
            get { return retrievedPackage; }
        }

        override public void Execute()
        {
            Stream file = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);

            SIGNER_WITH_AUTHENTICATION_EQUIFAX_CANADA = 
                SignerBuilder.NewSignerWithEmail(emailForSignerWithAuthenticationEquifaxCanada)
                    .WithFirstName("Signer1")
                    .WithLastName("Canada")
                    .WithCustomId("SingerCanadaID")
                    .ChallengedWithKnowledgeBasedAuthentication(
                            SignerInformationForEquifaxCanadaBuilder.NewSignerInformationForEquifaxCanada()
                                .WithFirstName("Signer1")
                                .WithLastName("lastNameCanada")
                                .WithStreetAddress("1111")
                                .WithCity("Montreal")
                                .WithProvince("QC")
                                .WithPostalCode("A1A1A1")
                                .WithTimeAtAddress(1)
                                .WithDriversLicenseNumber("Driver licence number")
                                .WithSocialInsuranceNumber("111222333")
                                .WithHomePhoneNumber("514-111-2222")
                                .WithDateOfBirth(new DateTime(2000, 1, 1)))
                    .ChallengedWithQuestions(ChallengeBuilder.FirstQuestion("What's your favorite restaurant? (answer: Staffany)")
                        .Answer("Staffany")
                        .SecondQuestion("What sport do you play? (answer: hockey)")
                        .Answer("hockey"))
                    .Build();

            SIGNER_WITH_AUTHENTICATION_EQUIFAX_USA =
                SignerBuilder.NewSignerWithEmail(emailForSignerWithAuthenticationEquifaxUsa)
                    .WithFirstName("Signer2")
                    .WithLastName("USA")
                    .WithCustomId("SignerUSAID")
                    .ChallengedWithKnowledgeBasedAuthentication(
                            SignerInformationForEquifaxUSABuilder.NewSignerInformationForEquifaxUSA()
                                .WithFirstName("Singer2")
                                .WithLastName("lastNameUSA")
                                .WithStreetAddress("2222")
                                .WithCity("New York")
                                .WithState("NY")
                                .WithZip("65212")
                                .WithSocialSecurityNumber("222667098")
                                .WithHomePhoneNumber("870-111-6547")
                                .WithTimeAtAddress(3)
                                .WithDriversLicenseNumber("Driver License Number")
                                .WithDateOfBirth(new DateTime(2002, 2, 2)))
                    .ChallengedWithQuestions(ChallengeBuilder.FirstQuestion("What's your favorite sport? (answer: golf)")
                        .Answer("golf")
                        .SecondQuestion("What music instrument do you play? (answer: drums)")
                        .Answer("drums"))
                    .Build();

            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed("MixedSignerAuthenticationExample: " + DateTime.Now)
                .DescribedAs("This is a package created using the e-SignLive SDK")
                .WithSigner(SIGNER_WITH_AUTHENTICATION_EQUIFAX_CANADA)
                .WithSigner(SIGNER_WITH_AUTHENTICATION_EQUIFAX_USA)
                .WithDocument(DocumentBuilder.NewDocumentNamed(documentName)
                    .FromStream(file, DocumentType.PDF)
                    .WithSignature(SignatureBuilder.SignatureFor(emailForSignerWithAuthenticationEquifaxCanada)
                        .Build())
                    .WithSignature(SignatureBuilder.SignatureFor(emailForSignerWithAuthenticationEquifaxUsa)
                        .Build())
                    .Build())
                .Build();

            packageId = eslClient.CreateAndSendPackage(superDuperPackage);

            retrievedPackage = eslClient.GetPackage(packageId);
        }
    }
}