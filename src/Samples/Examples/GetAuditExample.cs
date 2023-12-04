using System;
using System.IO;
using System.Collections.Generic;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
    /// <summary>
    /// Retrieves the values found in the fields of each of the documents in a package.
    /// </summary>
    public class GetAuditExample : SDKSample
    {
        public readonly string DOCUMENT1_NAME = "First Document";
        public List<Audit> audits;

        public static void Main(string[] args)
        {
            new GetAuditExample().Run();
        }

        override public void Execute()
        {
            DocumentPackage superDuperPackage =
                PackageBuilder.NewPackageNamed(PackageName)
                    .DescribedAs("This is a package created using the OneSpan SDK")
                    .WithEmailMessage("This message should be delivered to all signers")
                    .WithSigner(
                        SignerBuilder.NewSignerWithEmail(email1) 
                            .WithCustomId("signer1")
                            .WithFirstName("John")
                            .WithLastName("Smith")
                            .WithTitle("Managing Director")
                            .WithCompany("Acme Inc.")
                            .Build())
                    .WithDocument(
                        DocumentBuilder.NewDocumentNamed(DOCUMENT1_NAME)
                            .FromStream(fileStream1, DocumentType.PDF)
                            .WithSignature(
                                SignatureBuilder.SignatureFor(email1)
                                    .OnPage(0)
                                    .Build())
                            .Build())
                    .Build();

            PackageId packageId = ossClient.CreatePackageOneStep(superDuperPackage);
            ossClient.SendPackage(packageId);
            ossClient.PackageService.Trash(packageId);
            audits = ossClient.AuditService.GetAudit(packageId);

        }
    }
}

 