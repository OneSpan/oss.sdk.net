using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;
using System.Collections.Generic;

namespace SDK.Examples
{
	public class CreateTemplateExample : SDKSample
    {
        public static void Main(string[] args)
        {
			new CreateTemplateExample(Props.GetInstance()).Run();
        }
		        
        private Stream fileStream1;
        private Stream fileStream2;

		public CreateTemplateExample(Props props) : this(props.Get("api.url"), props.Get("api.key"))
        {
        }

		public CreateTemplateExample(string apiKey, string apiUrl) : base( apiKey, apiUrl )
        {
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
            this.fileStream2 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        override public void Execute()
        {
			DocumentPackage template =
                PackageBuilder.NewPackageNamed("CreateTemplateExample: " + DateTime.Now)
					.DescribedAs("This is a template created using the e-SignLive SDK")                	
                	.WithEmailMessage("This message should be delivered to all signers")
                	.WithSigner(SignerBuilder.NewSignerPlaceholderWithRoleId(new RoleId("PlaceholderRoleId1")))
                	.WithSigner(SignerBuilder.NewSignerPlaceholderWithRoleId(new RoleId("PlaceholderRoleId2")))
                	.WithDocument(DocumentBuilder.NewDocumentNamed("First Document")
                              .FromStream(fileStream1, DocumentType.PDF)
                              .WithSignature(SignatureBuilder.SignatureFor(new RoleId("PlaceholderRoleId1"))
                                             .OnPage(0)
                                             .WithField(FieldBuilder.CheckBox()
                                                     .OnPage(0)
                                                     .AtPosition(400, 200)
                                                     .WithValue("x")
                                                       )
                                             .AtPosition(100, 100)
                                            )
                             )
                	.WithDocument(DocumentBuilder.NewDocumentNamed("Second Document")
                              .FromStream(fileStream2, DocumentType.PDF)
                              .WithSignature(SignatureBuilder.SignatureFor(new RoleId("PlaceholderRoleId2"))
                                             .OnPage(0)
                                             .AtPosition(100, 200)
                                            )
                             )
                .Build();

			PackageId templateId = eslClient.CreateTemplate(template);
            
			Console.WriteLine("templateId = " + templateId);
        }
    }
}

