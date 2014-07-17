using System;
using System.IO;
using Silanis.ESL.SDK;
using System.Collections.Generic;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class FieldManipulationExample : SDKSample
    {
        public string email1;
        private Stream fileStream;

        private string documentId = "documentId";
        private SignatureId signatureId = new SignatureId("signatureId");

        public Field field1;
        public Field field2;
        public Field field3;

        public List<Field> addedFields;
        public List<Field> deletedFields;
        public List<Field> updatedFields;

        public DocumentPackage createdPackage;

        public FieldManipulationExample(Props props) : this(props.Get("api.url"), props.Get("api.key"), props.Get("1.email"))
        {
        }

        public FieldManipulationExample(string apiKey, string apiUrl, string email1) : base( apiKey, apiUrl )
        {
            this.email1 = email1;
            this.fileStream = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        override public void Execute()
        {
            DocumentPackage superDuperPackage =
                PackageBuilder.NewPackageNamed("FieldManipulationExample: " + DateTime.Now)
                    .DescribedAs("This is a package created using the e-SignLive SDK")
                    .ExpiresOn(DateTime.Now.AddMonths(100))
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                                .WithCustomId("signatureId1")
                                .WithFirstName("firstName1")
                                .WithLastName("lastName1"))
                    .WithDocument(DocumentBuilder.NewDocumentNamed("FieldManipulationExample")
                                  .WithId("documentId")
                                  .FromStream(fileStream, DocumentType.PDF)
                                  .WithSignature(SignatureBuilder.SignatureFor(email1)
                                   .OnPage(0)
                                   .WithId(signatureId)
                                   .AtPosition(100, 100))
                                  )
                    .Build();

            packageId = eslClient.CreatePackage(superDuperPackage);

            field1 = FieldBuilder.RadioButton("group1")
                .WithName("field1")
                .AtPosition(400, 100)
                    .OnPage(0)
                    .Build();


            field2 = FieldBuilder.RadioButton("group1")
                .WithName("field2")
                    .AtPosition(400, 200)
                    .OnPage(0)
                    .Build();

            field3 = FieldBuilder.RadioButton("group1")
                .WithName("field3")
                    .AtPosition(400, 300)
                    .OnPage(0)
                    .Build();

            // Adding the fields
            eslClient.ApprovalService.AddField(packageId, documentId, signatureId, field1);
            eslClient.ApprovalService.AddField(packageId, documentId, signatureId, field2);
            eslClient.ApprovalService.AddField(packageId, documentId, signatureId, field3);

            createdPackage = eslClient.GetPackage(packageId);
            addedFields = eslClient.ApprovalService.GetApproval(createdPackage, documentId, signatureId.Id).Fields;
        }
    }
}

