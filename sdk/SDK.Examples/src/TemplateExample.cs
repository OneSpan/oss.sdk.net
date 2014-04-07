using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;
using System.Collections.Generic;

namespace SDK.Examples
{
    public class TemplateExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new TemplateExample(Props.GetInstance()).Run();
        }

        private string email1;
        private string email2;
        private Stream fileStream1;
        private Stream fileStream2;

        public TemplateExample(Props props) : this(props.Get("api.url"), props.Get("api.key"), props.Get("1.email"), props.Get("2.email") )
        {
        }

        public TemplateExample(string apiKey, string apiUrl, string email1, string email2) : base( apiKey, apiUrl )
        {
            this.email1 = email1;
            this.email2 = email2;
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
            this.fileStream2 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        override public void Execute()
        {
            DocumentPackage superDuperPackage =
                PackageBuilder.NewPackageNamed("CreateTemplateFromPackageExample: " + DateTime.Now)
                .DescribedAs("This is a package created using the e-SignLive SDK")
                .ExpiresOn(DateTime.Now.AddMonths(1))
                .WithEmailMessage("This message should be delivered to all signers")
                .WithSigner(SignerBuilder.NewSignerPlaceholderWithRoleId(new RoleId("PlaceholderRoleId1")))
                .WithSigner(SignerBuilder.NewSignerPlaceholderWithRoleId(new RoleId("PlaceholderRoleId2")))
//                .WithDocument(DocumentBuilder.NewDocumentNamed("First Document")
//                              .FromStream(fileStream1, DocumentType.PDF)
//                              .WithSignature(SignatureBuilder.SignatureFor(new RoleId("PlaceholderRoleId1"))
//                                             .OnPage(0)
//                                             .WithField(FieldBuilder.CheckBox()
//                                                     .OnPage(0)
//                                                     .AtPosition(400, 200)
//                                                     .WithValue("x")
//                                                       )
//                                             .AtPosition(100, 100)
//                                            )
//                             )
//                .WithDocument(DocumentBuilder.NewDocumentNamed("Second Document")
//                              .FromStream(fileStream2, DocumentType.PDF)
//                              .WithSignature(SignatureBuilder.SignatureFor(new RoleId("PlaceholderRoleId2"))
//                                             .OnPage(0)
//                                             .AtPosition(100, 200)
//                                            )
//                             )
                .Build();

            PackageId originalPackageId = eslClient.CreatePackage(superDuperPackage);
            PackageId templateId = eslClient.CreateTemplateFromPackage(
                                       originalPackageId,
                                       "Template Sample " + DateTime.Now );


            PackageId instantiatedTemplate = eslClient.CreatePackageFromTemplate(templateId,
                                             PackageBuilder.NewPackageNamed("Package From Template")
                                             .WithSigner( SignerBuilder.NewSignerWithEmail( email1 )
                                                                        .WithCustomId("Client1")
                                                                        .WithFirstName("John1")
                                                                        .WithLastName("Smith1")
                                                                        .WithTitle("Managing Director1")
                                                                        .WithCompany("Acme Inc.1")
                                                                        .WithRoleId( new RoleId( "PlaceholderRoleId1" ) ) )
                                             .WithSigner( SignerBuilder.NewSignerWithEmail( email2 )
                                                                        .WithCustomId("Client2")
                                                                        .WithFirstName("John2")
                                                                        .WithLastName("Smith2")
                                                                        .WithTitle("Managing Director2")
                                                                        .WithCompany("Acme Inc.2")
                                                                        .WithRoleId( new RoleId( "PlaceholderRoleId2" ) ) )
                                             .Build() );
                                             
            DocumentPackage originalPackage = eslClient.GetPackage( originalPackageId );
            DocumentPackage instantiatedPackage = eslClient.GetPackage( instantiatedTemplate );

//            eslClient.SendPackage(instantiatedTemplate);
            Console.Out.WriteLine("BLAH");
        }
    }
}

