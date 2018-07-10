using System;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class SignerSSOExample : SDKSample
    {
        public static void Main (string [] args)
        {
            new SignerSSOExample ().Run ();
        }

        override public void Execute ()
        {
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed (PackageName)
                .DescribedAs ("This is a SSO authentication example")
                .WithSigner (SignerBuilder.NewSignerWithEmail (email4)
                             .WithFirstName ("John")
                             .WithLastName ("Smith")
                             .WithSSOAuthentication ())
                .WithDocument (DocumentBuilder.NewDocumentNamed ("First Document")
                    .FromStream (fileStream1, DocumentType.PDF)
                    .WithSignature (SignatureBuilder.SignatureFor (email4)
                        .OnPage (0)
                        .AtPosition (100, 100)))
                .Build ();

            packageId = eslClient.CreatePackage (superDuperPackage);
            retrievedPackage = eslClient.GetPackage (packageId);
        }
    }
}
