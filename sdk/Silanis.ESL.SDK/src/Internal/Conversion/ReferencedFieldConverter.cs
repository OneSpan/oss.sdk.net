using System;
using System.Collections.Generic;

namespace Silanis.ESL.SDK
{
    internal class ReferencedFieldConverter
    {
        public static API.ReferencedField ToAPI(ReferencedField sdkReferencedField) 
        {
            if (sdkReferencedField == null)
                return null;

            ReferencedFieldConditions sdkReferencedFieldConditions = sdkReferencedField.Conditions;
            List<API.FieldCondition> apiReferencedInCondition = new List<API.FieldCondition>();
            foreach (FieldCondition sdkFieldCondition in sdkReferencedFieldConditions.ReferencedInCondition) 
            {
                FieldConditionConverter converter = new FieldConditionConverter(sdkFieldCondition);
                apiReferencedInCondition.Add(converter.ToAPIFieldCondition());
            }

            List<API.FieldCondition> apiReferencedInAction = new List<API.FieldCondition>();
            foreach (FieldCondition sdkFieldCondition in sdkReferencedFieldConditions.ReferencedInAction) 
            {
                FieldConditionConverter converter = new FieldConditionConverter(sdkFieldCondition);
                apiReferencedInAction.Add(converter.ToAPIFieldCondition());
            }

            API.ReferencedFieldConditions apiReferencedFieldConditions = new API.ReferencedFieldConditions();
            apiReferencedFieldConditions.ReferencedInCondition = apiReferencedInCondition;
            apiReferencedFieldConditions.ReferencedInAction = apiReferencedInAction;

            API.ReferencedField apiReferencedField = new API.ReferencedField();
            apiReferencedField.FieldId = sdkReferencedField.FieldId;
            apiReferencedField.Conditions = apiReferencedFieldConditions;
            return apiReferencedField;
        }

        public static ReferencedField ToSDK(API.ReferencedField apiReferencedField) 
        {
            if (apiReferencedField == null)
                return null;

            API.ReferencedFieldConditions apiReferencedFieldConditions = apiReferencedField.Conditions;
            List<FieldCondition> sdkReferencedInCondition = new List<FieldCondition>();
            foreach (API.FieldCondition apiFieldCondition in apiReferencedFieldConditions.ReferencedInCondition) 
            {
                FieldConditionConverter converter = new FieldConditionConverter(apiFieldCondition);
                sdkReferencedInCondition.Add(converter.ToSDKFieldCondition());
            }

            List<FieldCondition> sdkReferencedInAction = new List<FieldCondition>();
            foreach (API.FieldCondition apiFieldCondition in apiReferencedFieldConditions.ReferencedInAction) 
            {
                FieldConditionConverter converter = new FieldConditionConverter(apiFieldCondition);
                sdkReferencedInAction.Add(converter.ToSDKFieldCondition());
            }

            ReferencedFieldConditions sdkReferencedFieldConditions = new ReferencedFieldConditions();
            sdkReferencedFieldConditions.ReferencedInCondition = sdkReferencedInCondition;
            sdkReferencedFieldConditions.ReferencedInAction = sdkReferencedInAction;

            ReferencedField sdkReferencedField = new ReferencedField();
            sdkReferencedField.FieldId = apiReferencedField.FieldId;
            sdkReferencedField.Conditions = sdkReferencedFieldConditions;
            return sdkReferencedField;
        }
    }
}
