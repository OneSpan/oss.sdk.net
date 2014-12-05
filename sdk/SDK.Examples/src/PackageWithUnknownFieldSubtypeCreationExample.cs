using System;
using Silanis.ESL.SDK;
using System.Collections.Generic;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class PackageWithUnknownFieldSubtypeCreationExample : SDKSample
    {
        public string email1;

        public const string documentId = "af050f74414b96ab";
        private SignatureId signatureId = new SignatureId("12885246-4e00-44f8-8e8c-5052c7706135");

        public Field field1;
        public Field field2;
        public Field field3;
        public Field updatedField;

        public List<Field> addedFields;
        public List<Field> deletedFields;
        public List<Field> updatedFields;

        public DocumentPackage createdPackage;

        public static void Main (string[] args)
        {
            new PackageWithUnknownFieldSubtypeCreationExample(Props.GetInstance()).Run();
        }

        public PackageWithUnknownFieldSubtypeCreationExample( Props props ) : this(props.Get("api.url"), props.Get("api.key"), props.Get("1.email")) 
        {
        }

        public PackageWithUnknownFieldSubtypeCreationExample( string apiKey, string apiUrl, string email1 ) : base( apiKey, apiUrl ) 
        {        
        }

        override public void Execute()
        {
            packageId = new PackageId("0caad322-06fd-41b1-bc0e-7641d22d0e0d");


            field1 = FieldBuilder.RadioButton("group1")
                .WithName("field1")
                    .WithId("fieldId1")
                    .AtPosition(400, 100)
                    .WithValue(true)
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
            eslClient.ApprovalService.AddField(packageId, documentId, signatureId, field1);
            eslClient.ApprovalService.AddField(packageId, documentId, signatureId, field2);
            eslClient.ApprovalService.AddField(packageId, documentId, signatureId, field3);

            createdPackage = eslClient.GetPackage(packageId);
            addedFields = eslClient.ApprovalService.GetApproval(createdPackage, documentId, signatureId.Id).Fields;

            // Deleting field1
            eslClient.ApprovalService.DeleteField(packageId, documentId, signatureId, field1.Id);

            createdPackage = eslClient.GetPackage(packageId);
            deletedFields = eslClient.ApprovalService.GetApproval(createdPackage, documentId, signatureId.Id).Fields;

            // Updating the information for the third field
            eslClient.ApprovalService.ModifyField(packageId, documentId, signatureId, updatedField);

            createdPackage = eslClient.GetPackage(packageId);
            updatedFields = eslClient.ApprovalService.GetApproval(createdPackage, documentId, signatureId.Id).Fields;

            eslClient.ApprovalService.DeleteField(packageId, documentId, signatureId, field2.Id);
            eslClient.ApprovalService.DeleteField(packageId, documentId, signatureId, field3.Id);

        }
    }
}

