using System;
using NUnit.Framework;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;
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
            OneSpanSign.API.ReferencedConditions api = ReferencedConditionsConverter.ToAPI(null);
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
            List<OneSpanSign.API.ReferencedDocument> apiDocuments = CreateApiReferencedDocumentsForTest();

            OneSpanSign.API.ReferencedConditions api = new OneSpanSign.API.ReferencedConditions();
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

            OneSpanSign.API.ReferencedConditions api = ReferencedConditionsConverter.ToAPI(sdk);

            Assert.AreEqual(api.PackageId, PACKAGE_ID);
            Assert.IsTrue(compareReferencedDocuments(api.Documents, sdkDocuments));
        }

        internal List<OneSpanSign.API.ReferencedDocument> CreateApiReferencedDocumentsForTest()
        {
            List<OneSpanSign.API.ReferencedDocument> documents = new List<OneSpanSign.API.ReferencedDocument>();

            OneSpanSign.API.ReferencedDocument referencedDocument = new OneSpanSign.API.ReferencedDocument();
            referencedDocument.DocumentId = DOCUMENT_ID;
            referencedDocument.Fields = createApiReferencedFieldsForTest();

            documents.Add(referencedDocument);
            return documents;
        }

        internal List<OneSpanSign.API.ReferencedField> createApiReferencedFieldsForTest()
        {
            List<OneSpanSign.API.ReferencedField> fields = new List<OneSpanSign.API.ReferencedField>();

            OneSpanSign.API.ReferencedField referencedField = new OneSpanSign.API.ReferencedField();
            referencedField.FieldId = FIELD_1_ID;
            referencedField.Conditions = createApiReferencedFieldConditionsForTest();

            return fields;
        }

        internal OneSpanSign.API.ReferencedFieldConditions createApiReferencedFieldConditionsForTest()
        {
            OneSpanSign.API.ReferencedFieldConditions referencedFieldConditions = new OneSpanSign.API.ReferencedFieldConditions();
            referencedFieldConditions.ReferencedInCondition = createApiFieldConditionsForTest(CONDITION_1_ID, FIELD_1_ID, FIELD_2_ID);
            referencedFieldConditions.ReferencedInAction = createApiFieldConditionsForTest(CONDITION_2_ID, FIELD_3_ID, FIELD_1_ID);
            return referencedFieldConditions;
        }

        internal List<OneSpanSign.API.FieldCondition> createApiFieldConditionsForTest(String conditionId, String fieldInConditionId, String fieldInActionId)
        {
            List<OneSpanSign.API.FieldCondition> fieldConditions = new List<OneSpanSign.API.FieldCondition>();

            OneSpanSign.API.FieldCondition fieldCondition = new OneSpanSign.API.FieldCondition();
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

        internal Boolean compareReferencedDocuments(OneSpanSign.API.ReferencedDocument apiRefDoc, ReferencedDocument sdkRefDoc)
        {
            return apiRefDoc.DocumentId.Equals(sdkRefDoc.DocumentId) &&
                compareReferencedFields(apiRefDoc.Fields, sdkRefDoc.Fields);
        }

        internal Boolean compareReferencedDocuments(List<OneSpanSign.API.ReferencedDocument> apiRefDocs, List<ReferencedDocument> sdkRefDocs)
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

        internal Boolean compareReferencedFields(List<OneSpanSign.API.ReferencedField> apiRefFields, List<ReferencedField> sdkRefFields)
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

        internal Boolean compareReferencedFields(OneSpanSign.API.ReferencedField apiRefField, ReferencedField sdkRefField)
        {
            return apiRefField.FieldId.Equals(sdkRefField.FieldId) &&
                compareReferencedFieldConditions(apiRefField.Conditions, sdkRefField.Conditions);
        }

        internal Boolean compareReferencedFieldConditions(OneSpanSign.API.ReferencedFieldConditions apiReferencedFieldConditions,
            ReferencedFieldConditions sdkReferencedFieldConditions)
        {
            List<OneSpanSign.API.FieldCondition> apiReferencedInCondition = apiReferencedFieldConditions.ReferencedInCondition;
            List<OneSpanSign.API.FieldCondition> apiReferencedInAction = apiReferencedFieldConditions.ReferencedInAction;
            List<FieldCondition> sdkReferencedInCondition = sdkReferencedFieldConditions.ReferencedInCondition;
            List<FieldCondition> sdkReferencedInAction = sdkReferencedFieldConditions.ReferencedInAction;

            return compareFieldConditions(apiReferencedInCondition, sdkReferencedInCondition) &&
                compareFieldConditions(apiReferencedInAction, sdkReferencedInAction);
        }

        internal Boolean compareFieldConditions(OneSpanSign.API.FieldCondition apiFieldCondition, FieldCondition sdkFieldCondition)
        {
            return apiFieldCondition.Id.Equals(sdkFieldCondition.Id) &&
                apiFieldCondition.Condition.Equals(sdkFieldCondition.Condition) &&
                apiFieldCondition.Action.Equals(sdkFieldCondition.Action);
        }

        internal Boolean compareFieldConditions(List<OneSpanSign.API.FieldCondition> apiFieldConditions, List<FieldCondition> sdkFieldConditions)
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

