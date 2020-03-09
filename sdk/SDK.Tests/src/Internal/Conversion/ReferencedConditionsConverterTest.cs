using System;
using NUnit.Framework;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;
using System.Collections.Generic;

namespace SDK.Tests
{
    [TestFixture()]
    public class ReferencedConditionsConverterTest
    {
        protected readonly String PACKAGE_ID = "package1";
        protected readonly String DOCUMENT_ID = "document1";
        protected readonly String FIELD_1_ID = "field1";
        protected readonly String FIELD_2_ID = "field2";
        protected readonly String FIELD_3_ID = "field3";
        protected readonly String CONDITION_1_ID = "Condition1";
        protected readonly String CONDITION_2_ID = "Condition2";
        protected readonly String CONDITION = "document['document1'].field['{0}'].empty == true";
        protected readonly String ACTION = "document['document1'].field['{0}'].disabled = false";

        [Test()]
        public virtual void ConvertNullSDKToAPI()
        {
            Silanis.ESL.API.ReferencedConditions api = ReferencedConditionsConverter.ToAPI(null);
            Assert.IsNull(api);
        }

        [Test()]
        public virtual void ConvertNullAPIToSDK()
        {
            ReferencedConditions sdk = ReferencedConditionsConverter.ToSDK(null);
            Assert.IsNull(sdk);
        }

        [Test()]
        public virtual void ConvertAPIToSDK()
        {
            List<Silanis.ESL.API.ReferencedDocument> apiDocuments = CreateApiReferencedDocumentsForTest();

            Silanis.ESL.API.ReferencedConditions api = new Silanis.ESL.API.ReferencedConditions();
            api.PackageId = PACKAGE_ID;
            api.Documents = apiDocuments;

            ReferencedConditions sdk = ReferencedConditionsConverter.ToSDK(api);

            Assert.AreEqual(sdk.PackageId, PACKAGE_ID);
            Assert.IsTrue(compareReferencedDocuments(apiDocuments, sdk.Documents));
        }

        [Test()]
        public virtual void ConvertSDKToAPI()
        {
            List<ReferencedDocument> sdkDocuments = createSdkReferencedDocumentsForTest();

            ReferencedConditions sdk = new ReferencedConditions();
            sdk.PackageId = PACKAGE_ID;
            sdk.Documents = sdkDocuments;

            Silanis.ESL.API.ReferencedConditions api = ReferencedConditionsConverter.ToAPI(sdk);

            Assert.AreEqual(api.PackageId, PACKAGE_ID);
            Assert.IsTrue(compareReferencedDocuments(api.Documents, sdkDocuments));
        }

        internal List<Silanis.ESL.API.ReferencedDocument> CreateApiReferencedDocumentsForTest()
        {
            List<Silanis.ESL.API.ReferencedDocument> documents = new List<Silanis.ESL.API.ReferencedDocument>();

            Silanis.ESL.API.ReferencedDocument referencedDocument = new Silanis.ESL.API.ReferencedDocument();
            referencedDocument.DocumentId = DOCUMENT_ID;
            referencedDocument.Fields = createApiReferencedFieldsForTest();

            documents.Add(referencedDocument);
            return documents;
        }

        internal List<Silanis.ESL.API.ReferencedField> createApiReferencedFieldsForTest()
        {
            List<Silanis.ESL.API.ReferencedField> fields = new List<Silanis.ESL.API.ReferencedField>();

            Silanis.ESL.API.ReferencedField referencedField = new Silanis.ESL.API.ReferencedField();
            referencedField.FieldId = FIELD_1_ID;
            referencedField.Conditions = createApiReferencedFieldConditionsForTest();

            return fields;
        }

        internal Silanis.ESL.API.ReferencedFieldConditions createApiReferencedFieldConditionsForTest()
        {
            Silanis.ESL.API.ReferencedFieldConditions referencedFieldConditions = new Silanis.ESL.API.ReferencedFieldConditions();
            referencedFieldConditions.ReferencedInCondition = createApiFieldConditionsForTest(CONDITION_1_ID, FIELD_1_ID, FIELD_2_ID);
            referencedFieldConditions.ReferencedInAction = createApiFieldConditionsForTest(CONDITION_2_ID, FIELD_3_ID, FIELD_1_ID);
            return referencedFieldConditions;
        }

        internal List<Silanis.ESL.API.FieldCondition> createApiFieldConditionsForTest(String conditionId, String fieldInConditionId, String fieldInActionId)
        {
            List<Silanis.ESL.API.FieldCondition> fieldConditions = new List<Silanis.ESL.API.FieldCondition>();

            Silanis.ESL.API.FieldCondition fieldCondition = new Silanis.ESL.API.FieldCondition();
            fieldCondition.Id = conditionId;
            fieldCondition.Condition = String.Format(CONDITION, fieldInConditionId);
            fieldCondition.Action = String.Format(ACTION, fieldInActionId);

            fieldConditions.Add(fieldCondition);
            return fieldConditions;
        }

        internal List<ReferencedDocument> createSdkReferencedDocumentsForTest()
        {
            List<ReferencedDocument> documents = new List<ReferencedDocument>();

            ReferencedDocument referencedDocument = new ReferencedDocument();
            referencedDocument.DocumentId = DOCUMENT_ID;
            referencedDocument.Fields = createSdkReferencedFieldsForTest();

            documents.Add(referencedDocument);
            return documents;
        }

        internal List<ReferencedField> createSdkReferencedFieldsForTest()
        {
            List<ReferencedField> fields = new List<ReferencedField>();

            ReferencedField referencedField = new ReferencedField();
            referencedField.FieldId = FIELD_1_ID;
            referencedField.Conditions = createSdkReferencedFieldConditionsForTest();

            fields.Add(referencedField);
            return fields;
        }

        internal ReferencedFieldConditions createSdkReferencedFieldConditionsForTest()
        {
            ReferencedFieldConditions referencedFieldConditions = new ReferencedFieldConditions();
            referencedFieldConditions.ReferencedInCondition = createSdkFieldConditionsForTest(CONDITION_1_ID, FIELD_1_ID, FIELD_2_ID);
            referencedFieldConditions.ReferencedInAction = createSdkFieldConditionsForTest(CONDITION_2_ID, FIELD_3_ID, FIELD_1_ID);
            return referencedFieldConditions;
        }

        internal List<FieldCondition> createSdkFieldConditionsForTest(String conditionId, String fieldInConditionId, String fieldInActionId)
        {
            List<FieldCondition> fieldConditions = new List<FieldCondition>();

            fieldConditions.Add(FieldConditionBuilder.NewFieldCondition()
                .WithId(conditionId)
                .WithCondition(String.Format(CONDITION, fieldInConditionId))
                .WithAction(String.Format(ACTION, fieldInActionId))
                .Build());

            return fieldConditions;
        }

        internal Boolean compareReferencedDocuments(Silanis.ESL.API.ReferencedDocument apiRefDoc, ReferencedDocument sdkRefDoc)
        {
            return apiRefDoc.DocumentId.Equals(sdkRefDoc.DocumentId) &&
                compareReferencedFields(apiRefDoc.Fields, sdkRefDoc.Fields);
        }

        internal Boolean compareReferencedDocuments(List<Silanis.ESL.API.ReferencedDocument> apiRefDocs, List<ReferencedDocument> sdkRefDocs)
        {
            if (apiRefDocs.Count != sdkRefDocs.Count)
                return false;

            for (int i = 0; i < apiRefDocs.Count; i++)
            {
                if (!compareReferencedDocuments(apiRefDocs[i], sdkRefDocs[i]))
                    return false;
            }
            return true;
        }

        internal Boolean compareReferencedFields(List<Silanis.ESL.API.ReferencedField> apiRefFields, List<ReferencedField> sdkRefFields)
        {
            if (apiRefFields.Count != sdkRefFields.Count)
                return false;

            for (int i = 0; i < apiRefFields.Count; i++)
            {
                if (!compareReferencedFields(apiRefFields[i], sdkRefFields[i]))
                    return false;
            }
            return true;
        }

        internal Boolean compareReferencedFields(Silanis.ESL.API.ReferencedField apiRefField, ReferencedField sdkRefField)
        {
            return apiRefField.FieldId.Equals(sdkRefField.FieldId) &&
                compareReferencedFieldConditions(apiRefField.Conditions, sdkRefField.Conditions);
        }

        internal Boolean compareReferencedFieldConditions(Silanis.ESL.API.ReferencedFieldConditions apiReferencedFieldConditions,
            ReferencedFieldConditions sdkReferencedFieldConditions)
        {
            List<Silanis.ESL.API.FieldCondition> apiReferencedInCondition = apiReferencedFieldConditions.ReferencedInCondition;
            List<Silanis.ESL.API.FieldCondition> apiReferencedInAction = apiReferencedFieldConditions.ReferencedInAction;
            List<FieldCondition> sdkReferencedInCondition = sdkReferencedFieldConditions.ReferencedInCondition;
            List<FieldCondition> sdkReferencedInAction = sdkReferencedFieldConditions.ReferencedInAction;

            return compareFieldConditions(apiReferencedInCondition, sdkReferencedInCondition) &&
                compareFieldConditions(apiReferencedInAction, sdkReferencedInAction);
        }

        internal Boolean compareFieldConditions(Silanis.ESL.API.FieldCondition apiFieldCondition, FieldCondition sdkFieldCondition)
        {
            return apiFieldCondition.Id.Equals(sdkFieldCondition.Id) &&
                apiFieldCondition.Condition.Equals(sdkFieldCondition.Condition) &&
                apiFieldCondition.Action.Equals(sdkFieldCondition.Action);
        }

        internal Boolean compareFieldConditions(List<Silanis.ESL.API.FieldCondition> apiFieldConditions, List<FieldCondition> sdkFieldConditions)
        {
            if (apiFieldConditions.Count != sdkFieldConditions.Count)
                return false;

            for (int i = 0; i < apiFieldConditions.Count; i++)
            {
                if (!compareFieldConditions(apiFieldConditions[i], sdkFieldConditions[i]))
                    return false;
            }
            return true;
        }
    }
}

