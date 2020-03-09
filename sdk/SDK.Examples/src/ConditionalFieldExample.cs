using System;
using System.Collections.Generic;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class ConditionalFieldExample : SDKSample
    {
        private readonly String documentId = "DocumentId";
        private readonly SignatureId signatureId = new SignatureId ("signatureId");
        private readonly String fieldId1 = "fieldId1";
        private readonly String fieldId2 = "fieldId2";

        public DocumentPackage RetrievedPackageWithoutConditions;
        public DocumentPackage RetrievedPackageWithUpdatedConditions;

        public static void Main (string [] args)
        {
            new ConditionalFieldExample ().Run ();
        }

        public override void Execute ()
        {
            FieldCondition condition = new FieldCondition ();
            condition.Id = "ConditionId";
            condition.Condition = "document['DocumentId'].field['fieldId2'].value == 'X'";
            condition.Action = "document['DocumentId'].field['fieldId1'].disabled = false";

            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed (packageName)
                    .DescribedAs ("Description")
                    .WithSigner (SignerBuilder.NewSignerWithEmail (email1)
                            .WithFirstName ("Patty")
                            .WithLastName ("Galant"))
                                   .WithDocument (DocumentBuilder.NewDocumentNamed ("Document")
                                   .WithId (documentId)
                                   .FromStream (fileStream1, DocumentType.PDF)
                            .WithSignature (SignatureBuilder.SignatureFor (email1)
                                    .WithId(signatureId)
                                    .OnPage (0)
                                    .AtPosition (400, 100)
                                            .WithField (FieldBuilder.TextField ()
                                            .WithId (fieldId1)
                                            .OnPage (0)
                                            .AtPosition (0, 0))
                                    .WithField (FieldBuilder.CheckBox ()
                                            .WithId (fieldId2)
                                            .OnPage (0)
                                            .AtPosition (0, 0))))
                    .WithCondition (condition)
                    .Build ();

            packageId = eslClient.CreatePackageOneStep (superDuperPackage);
            retrievedPackage = eslClient.GetPackage (packageId);

            FieldCondition newCondition = new FieldCondition ();
            newCondition.Id = "ConditionId";
            newCondition.Condition = "document['DocumentId'].field['fieldId2'].value == 'X'";
            newCondition.Action = "document['DocumentId'].field['fieldId1'].disabled = true";
            List<FieldCondition> conditions = new List<FieldCondition> ();
            conditions.Add (newCondition);

            ConditionalField conditionalField = new ConditionalField ();
            conditionalField.Id = fieldId2;
            conditionalField.Conditions = conditions;
            conditionalField.Style = FieldStyle.UNBOUND_CHECK_BOX;

            eslClient.ApprovalService.ModifyConditionalField (packageId, documentId, signatureId, conditionalField);
            RetrievedPackageWithUpdatedConditions = eslClient.GetPackage (packageId);

            conditions.Clear ();
            conditionalField.Conditions = conditions;
            eslClient.ApprovalService.ModifyConditionalField (packageId, documentId, signatureId, conditionalField);
            RetrievedPackageWithoutConditions = eslClient.GetPackage (packageId);

        }
    }
}
