using System;
using System.Collections.Generic;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
    public class GetPackageFieldsListExample : SDKSample
    {
        public Page<Dictionary<String, String>> packages;

        public static void Main (string [] args)
        {
            new GetPackageFieldsListExample ().Run ();
        }

        override public void Execute ()
        {
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed (PackageName)
                .DescribedAs ("This is a package created using OneSpan Sign SDK")
                .ExpiresOn (DateTime.Now.AddMonths (100))
                .WithEmailMessage ("This message should be delivered to all signers")
                .WithSigner (SignerBuilder.NewSignerWithEmail (email1)
                    .WithCustomId ("Client1")
                    .WithFirstName ("John")
                    .WithLastName ("Smith")
                    .WithTitle ("Managing Director")
                    .WithCompany ("Acme Inc."))
                .WithDocument (DocumentBuilder.NewDocumentNamed ("First Document")
                    .FromStream (fileStream1, DocumentType.PDF)
                    .WithSignature (SignatureBuilder.SignatureFor (email1)
                        .OnPage (0)
                        .AtPosition (100, 100)
                        .WithSize (200, 50)))
                .Build ();
            
            packageId = ossClient.CreatePackage (superDuperPackage);
            packages = ossClient.PackageService.GetPackagesFields (DocumentPackageStatus.DRAFT, new PageRequest (1), new HashSet<String>(){"id"});
        }
    }
}
