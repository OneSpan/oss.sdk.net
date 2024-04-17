using System;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;
using System.IO;
using System.Collections.Generic;

namespace SDK.Examples
{
    public class SignerInformationForLexisNexisExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new SignerInformationForLexisNexisExample().Run();
        }

        public readonly string FIRST_NAME = "John";
        public readonly string LAST_NAME = "Smith";
        public readonly string  FLAT_OR_APARTMENT_NUMBER = "1234";
        public readonly string  HOUSE_NAME = "Decarie";
        public readonly string  HOUSE_NUMBER = "5678";
        public readonly string CITY = "CALERA";
        public readonly string STATE = "AL";
        public readonly string ZIP = "35040";
        public readonly string SOCIAL_SECURITY_NUMBER = "666110007";
        public readonly Nullable<DateTime> DATE_OF_BIRTH = new DateTime(1973, 2, 2);

        private string signerId = "signerId";
        private string documentName = "My Document";

        override public void Execute()
        {
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed(PackageName)
                .DescribedAs("This is a package created using OneSpan Sign SDK")
                .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                                .WithFirstName(FIRST_NAME)
                                .WithLastName(LAST_NAME)
                                .WithCustomId(signerId)
                                .ChallengedWithKnowledgeBasedAuthentication(
                                    SignerInformationForLexisNexisBuilder.NewSignerInformationForLexisNexis()
                                        .WithFirstName(FIRST_NAME)
                                        .WithLastName(LAST_NAME)
                                        .WithFlatOrApartmentNumber(FLAT_OR_APARTMENT_NUMBER)
                                        .WithHouseName(HOUSE_NAME)
                                        .WithHouseNumber(HOUSE_NUMBER)
                                        .WithCity(CITY)
                                        .WithState(STATE)
                                        .WithZip(ZIP)
                                        .WithSocialSecurityNumber(SOCIAL_SECURITY_NUMBER)
                                        .WithDateOfBirth(DATE_OF_BIRTH)
                                        .Build()))
                                .WithDocument(DocumentBuilder.NewDocumentNamed(documentName)
                                .FromStream(fileStream1, DocumentType.PDF)
                                .WithSignature(SignatureBuilder.SignatureFor(email1)
                                    .Build())
                                .Build())
                            .Build();

            packageId = ossClient.CreateAndSendPackage(superDuperPackage);

            retrievedPackage = ossClient.GetPackage(packageId);
        }
    }
}
