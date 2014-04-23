using System;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;
using System.IO;

namespace SDK.Examples
{
    public class SignerAuthenticationTokenExample : SDKSample
    {
        public static void Main (string[] args)
        {
            new SignerAuthenticationTokenExample(Props.GetInstance()).Run();
        }
        public string SignerAuthenticationToken { get; private set; }

        private Stream fileStream;
        private string signerEmail;

        public SignerAuthenticationTokenExample( Props props ) : this(props.Get("api.url"), props.Get("api.key"), props.Get("1.email"))
        {
        }

        public SignerAuthenticationTokenExample( string apiKey, string apiUrl, string signerEmail) : base( apiKey, apiUrl )
        {
            this.signerEmail = signerEmail;
            this.fileStream = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        override public void Execute()
        {
            string signerId = System.Guid.NewGuid().ToString();
            DocumentPackage package = PackageBuilder.NewPackageNamed ("SignerAuthenticationTokenExample " + DateTime.Now)
                    .DescribedAs ("This is a new package")
                    .WithSigner(SignerBuilder.NewSignerWithEmail(signerEmail)
                                .WithFirstName("John")
                                .WithLastName("Smith")
                                .WithCompany ("Acme Inc")
                                .WithTitle ("Managing Director")
                                .WithCustomId(signerId))
                    .WithDocument(DocumentBuilder.NewDocumentNamed("My Document")
                                  .FromStream(fileStream, DocumentType.PDF)
                                  .WithSignature(SignatureBuilder.SignatureFor(signerEmail)
                                        .OnPage(0)
                                        .AtPosition(500, 100)))
                    .Build ();

            PackageId packageId = eslClient.CreatePackage (package);
            eslClient.SendPackage(packageId);

            SignerAuthenticationToken = eslClient.CreateSignerAuthenticationToken(packageId, signerId);
            Console.WriteLine("Signer Authentication Token = " + SignerAuthenticationToken);
        }

    }
}

