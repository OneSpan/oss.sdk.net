using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class BasicPackageCreationExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new BasicPackageCreationExample(Props.GetInstance()).Run();
        }

        public string email1;
        public string email2;
        private Stream fileStream1;
        private Stream fileStream2;

        public BasicPackageCreationExample(Props props) : this(props.Get("api.url"), props.Get("api.key"), props.Get("1.email"), props.Get("2.email"))
        {
        }

        public BasicPackageCreationExample(string apiKey, string apiUrl, string email1, string email2) : base( apiKey, apiUrl )
        {
            this.email1 = email1;
            this.email2 = email2;
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
            this.fileStream2 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        override public void Execute()
        {
            DocumentPackage superDuperPackage =
                PackageBuilder.NewPackageNamed("BasicPackageCreationExample: " + DateTime.Now)
                .DescribedAs("This is a package created using the e-SignLive SDK")
                .ExpiresOn(DateTime.Now.AddMonths(100))
                .WithEmailMessage("This message should be delivered to all signers")
                .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                            .WithCustomId("Client1")
                            .WithFirstName("John")
                            .WithLastName("Smith")
                            .WithTitle("Managing Director")
                            .WithCompany("Acme Inc.")
                           )
                .WithSigner(SignerBuilder.NewSignerWithEmail(email2)
                            .WithFirstName("Patty")
                            .WithLastName("Galant")
                           )
                .WithDocument(DocumentBuilder.NewDocumentNamed("First Document")
                              .FromStream(fileStream1, DocumentType.PDF)
                              .WithSignature(SignatureBuilder.SignatureFor(email1)
                                             .OnPage(0)
                                             .WithField(FieldBuilder.CheckBox()
                                                     .OnPage(0)
                                                     .AtPosition(400, 200)
                                                     .WithValue(FieldBuilder.CHECKBOX_CHECKED)
                                                       )
                                             .AtPosition(100, 100)
                                            )
                             )
                .WithDocument(DocumentBuilder.NewDocumentNamed("Second Document")
                              .FromStream(fileStream2, DocumentType.PDF)
                              .WithSignature(SignatureBuilder.SignatureFor(email2)
                                             .OnPage(0)
                                             .AtPosition(100, 200)
                                             .WithField(FieldBuilder.RadioButton("group")
                                                     .WithValue(false)
                                                     .WithSize(20, 20)
                                                     .OnPage(0)
                                                     .AtPosition(400, 200))
                                             .WithField(FieldBuilder.RadioButton("group")
                                                     .WithValue(true)
                                                     .WithSize(20, 20)
                                                     .OnPage(0)
                                                     .AtPosition(400, 250))
                                             .WithField(FieldBuilder.RadioButton("group")
                                                     .WithValue(false)
                                                     .WithSize(20, 20)
                                                     .OnPage(0)
                                                     .AtPosition(400, 300))
                                            )
                             )
                .Build();

            packageId = eslClient.CreatePackageOneStep(superDuperPackage);
            eslClient.SendPackage(packageId);
            retrievedPackage = eslClient.GetPackage(packageId);
        }
    }
}
