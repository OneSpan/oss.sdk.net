using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;
using System.Collections.Generic;

namespace SDK.Examples
{
    public class CreateSenderTemplateExample : SDKSample
    {
        public String TEMPLATE_SENDER_VISIBILITY = "SENDER";

        private Stream fileStream1;
        private String email1;         
        public PackageId templateId;     

        public static void Main(string[] args)
        {
            new CreateSenderTemplateExample(Props.GetInstance()).Run();
        }

        public CreateSenderTemplateExample(Props props) : this(props.Get("api.key"), props.Get("api.url"), props.Get("1.email"))
        {
        }

        public CreateSenderTemplateExample(string apiKey, string apiUrl, string email1) : base( apiKey, apiUrl )
        {
            this.email1 = email1;
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        override public void Execute()
        {
            DocumentPackage template =
                PackageBuilder.NewPackageNamed("CreateSenderTemplateExample: " + DateTime.Now)
                    .DescribedAs("This is a Template created using the e-SignLive SDK")      
                    .WithPrivateVisibility()
                    .WithEmailMessage("This message should be delivered to all signers")
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                                                    .WithFirstName("Patty")    
                                                    .WithLastName("Galant"))   
                    .WithDocument(DocumentBuilder.NewDocumentNamed("First Document")
                                  .WithId("documentId")  
                                  .FromStream(fileStream1, DocumentType.PDF)
                                  .WithSignature(SignatureBuilder.SignatureFor(email1)
                                  .OnPage(0)
                                  .AtPosition(400, 200)
                               ))
                    .Build();

            templateId = eslClient.CreateTemplate(template);

            Console.WriteLine("templateId = " + templateId);
        }

    }
}

