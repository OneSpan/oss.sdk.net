using System;
using System.IO;
using OneSpanSign.Sdk;
using System.Collections.Generic;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
    public class FieldManipulationExample : SDKSample
    {
        private string documentId = "documentId";
        private SignatureId signatureId = new SignatureId("signatureId");

        public Field field1;
        public Field field2;
        public Field field3;
        public Field updatedField;

        public List<Field> addedFields;
        public List<Field> deletedFields;
        public List<Field> updatedFields;

        public DocumentPackage createdPackage;

        override public void Execute()
        {
            DocumentPackage superDuperPackage =
                PackageBuilder.NewPackageNamed(PackageName)
                    .DescribedAs("This is a package created using the eSignLive SDK")
                    .ExpiresOn(DateTime.Now.AddMonths(100))
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                                .WithCustomId("signatureId1")
                                .WithFirstName("firstName1")
                                .WithLastName("lastName1"))
                    .WithDocument(DocumentBuilder.NewDocumentNamed("FieldManipulationExample")
                                  .WithId("documentId")
                                  .FromStream(fileStream1, DocumentType.PDF)
                                  .WithSignature(SignatureBuilder.SignatureFor(email1)
                                   .OnPage(0)
                                   .WithId(signatureId)
                                   .AtPosition(100, 100))
                                  )
                    .Build();

            packageId = ossClient.CreatePackage(superDuperPackage);

            field1 = FieldBuilder.RadioButton("group1")
                .WithName("field1")
                    .WithId("fieldId1")
                    .AtPosition(400, 100)
                    .OnPage(0)
                    .Build();


            field2 = FieldBuilder.RadioButton("group1")
                .WithName("field2")
                    .WithId("fieldId2")
                    .AtPosition(400, 200)
                    .OnPage(0)
                    .Build();

            field3 = FieldBuilder.RadioButton("group1")
                .WithName("field3")
                    .WithId("fieldId3")
                    .AtPosition(400, 300)
                    .OnPage(0)
                    .Build();

            updatedField = FieldBuilder.RadioButton("group1")
                .WithName("updatedField")
                    .WithId("fieldId3")
                    .AtPosition(400, 300)
                    .OnPage(0)
                    .Build();

            // Adding the fields
            ossClient.ApprovalService.AddField(packageId, documentId, signatureId, field1);
            ossClient.ApprovalService.AddField(packageId, documentId, signatureId, field2);
            ossClient.ApprovalService.AddField(packageId, documentId, signatureId, field3);

            createdPackage = ossClient.GetPackage(packageId);
            addedFields = ossClient.ApprovalService.GetApproval(createdPackage, documentId, signatureId.Id).Fields;

            // Deleting field1
            ossClient.ApprovalService.DeleteField(packageId, documentId, signatureId, field1.Id);

            createdPackage = ossClient.GetPackage(packageId);
            deletedFields = ossClient.ApprovalService.GetApproval(createdPackage, documentId, signatureId.Id).Fields;

            // Updating the information for the third field
            ossClient.ApprovalService.ModifyField(packageId, documentId, signatureId, updatedField);

            createdPackage = ossClient.GetPackage(packageId);
            updatedFields = ossClient.ApprovalService.GetApproval(createdPackage, documentId, signatureId.Id).Fields;
        }
    }
}

