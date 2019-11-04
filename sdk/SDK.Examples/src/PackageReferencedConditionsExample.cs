using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class PackageReferencedConditionsExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new PackageReferencedConditionsExample().Run();
        }

        public static readonly string DOCUMENT_ID_1 = "documentId1";
        public static readonly string DOCUMENT_ID_2 = "documentId2";
        
        public readonly SignatureId signatureId1 = new SignatureId("signatureId1");
        public readonly SignatureId signatureId2 = new SignatureId("signatureId2");
        
        private readonly string fieldId1 = "fieldId1";
        private readonly string fieldId2 = "fieldId2";
        private readonly string fieldId3 = "fieldId3";
        private readonly string fieldId4 = "fieldId4";
        private readonly string fieldId5 = "fieldId5";
        private readonly string fieldId6 = "fieldId6";
        
        public ReferencedConditions PackageLevelRefConditions;
        public ReferencedConditions DocumentLevelRefConditions;
        public ReferencedConditions FieldLevelRefConditions;

        override public void Execute()
        {
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed(packageName)
                .DescribedAs("This is a package created using the eSignLive SDK")
                .WithEmailMessage("This message should be delivered to all signers")
                .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                    .WithCustomId("signer1")
                    .WithFirstName("firstName1")
                    .WithLastName("lastName1"))
                .WithDocument(DocumentBuilder.NewDocumentNamed("PackageReferencedConditionsExampleDocument1")
                    .FromStream(fileStream1, DocumentType.PDF)
                    .WithId(DOCUMENT_ID_1)
                    .WithSignature(SignatureBuilder.SignatureFor(email1)
                        .OnPage(0)
                        .AtPosition(100, 100)
                        .WithId(signatureId1)
                        .WithField(FieldBuilder.TextField()
                            .WithName("field1")
                            .WithId(fieldId1)
                            .AtPosition(400, 100)
                            .OnPage(0)
                            .Build())
                        .WithField(FieldBuilder.TextField()
                            .WithName("field2")
                            .WithId(fieldId2)
                            .AtPosition(400, 200)
                            .OnPage(0)
                            .Build())
                        .WithField(FieldBuilder.TextField()
                            .WithName("field3")
                            .WithId(fieldId3)
                            .AtPosition(400, 300)
                            .OnPage(0)
                            .Build())
                    )
                )
                .WithDocument(DocumentBuilder.NewDocumentNamed("PackageReferencedConditionsExampleDocument2")
                    .FromStream(fileStream2, DocumentType.PDF)
                    .WithId(DOCUMENT_ID_2)
                    .WithSignature(SignatureBuilder.SignatureFor(email1)
                        .OnPage(0)
                        .AtPosition(100, 400)
                        .WithId(signatureId2)
                        .WithField(FieldBuilder.TextField()
                            .WithName("field4")
                            .WithId(fieldId4)
                            .AtPosition(400, 100)
                            .OnPage(0)
                            .Build())
                        .WithField(FieldBuilder.TextField()
                            .WithName("field5")
                            .WithId(fieldId5)
                            .AtPosition(400, 200)
                            .OnPage(0)
                            .Build())
                        .WithField(FieldBuilder.TextField()
                            .WithName("field6")
                            .WithId(fieldId6)
                            .AtPosition(400, 300)
                            .OnPage(0)
                            .Build())
                    )
                )
                .WithCondition(createCondition("condition1", DOCUMENT_ID_1, fieldId1, DOCUMENT_ID_1, fieldId2))
                .WithCondition(createCondition("condition2", DOCUMENT_ID_1, fieldId1, DOCUMENT_ID_1, fieldId3))
                .WithCondition(createCondition("condition3", DOCUMENT_ID_2, fieldId4, DOCUMENT_ID_2, fieldId5))
                .Build();

            packageId = eslClient.CreatePackageOneStep(superDuperPackage);

            PackageLevelRefConditions = eslClient.PackageService.GetReferencedConditions(packageId.Id);
            DocumentLevelRefConditions = eslClient.PackageService.GetReferencedConditions(packageId.Id, DOCUMENT_ID_1);
            FieldLevelRefConditions = eslClient.PackageService.GetReferencedConditions(packageId.Id, DOCUMENT_ID_1, fieldId1);
        }
        
        private FieldCondition createCondition(String id, String conditionDocId, String conditionFieldId, String actionDocId, String actionFieldId)
        {
            return FieldConditionBuilder.NewFieldCondition()
            .WithId(id)
            .WithCondition(String.Format("document['{0}'].field['{1}'].empty == true", conditionDocId, conditionFieldId))
            .WithAction(String.Format("document['{0}'].field['{1}'].disabled = false", actionDocId, actionFieldId))
            .Build();
        }
    }
}

