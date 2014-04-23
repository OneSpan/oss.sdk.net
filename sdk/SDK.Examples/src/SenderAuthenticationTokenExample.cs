using System;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;
using System.IO;

namespace SDK.Examples
{
    public class SenderAuthenticationTokenExample : SDKSample
    {
        public static void Main (string[] args)
        {
            new SenderAuthenticationTokenExample(Props.GetInstance()).Run();
        }

        private Stream fileStream;
        public string SenderAuthenticationToken { get; private set; }

        public SenderAuthenticationTokenExample( Props props ) : this(props.Get("api.url"), props.Get("api.key"))
        {
        }

        public SenderAuthenticationTokenExample( string apiKey, string apiUrl) : base( apiKey, apiUrl )
        {
            this.fileStream = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        override public void Execute()
        {
            DocumentPackage superDuperPackage =
                PackageBuilder.NewPackageNamed("SenderAuthenticationTokenExample: " + DateTime.Now)
                .DescribedAs("This is a package created using the e-SignLive SDK")
                .ExpiresOn(DateTime.Now.AddMonths(1))
                .WithEmailMessage("This message should be delivered to all signers")
                .WithDocument(DocumentBuilder.NewDocumentNamed("First Document")
                              .FromStream(fileStream, DocumentType.PDF)
                             )
                .Build();

            PackageId packageId = eslClient.CreatePackage(superDuperPackage);

            SenderAuthenticationToken = eslClient.CreateSenderAuthenticationToken(packageId);

            Console.WriteLine("Sender Authentication Token = " + SenderAuthenticationToken);
        }

    }
}

