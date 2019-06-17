using System;
using System.Collections.Generic;

namespace Silanis.ESL.SDK
{
    internal class ReferencedFieldConditionsConverter
    {
        public static API.ReferencedFieldConditions ToAPI(ReferencedFieldConditions sdkReferencedFieldConditions)
        {
            if (sdkReferencedFieldConditions == null)
                return null;

            List<API.FieldCondition> apiFieldConditionsInCondition = new List<API.FieldCondition>();
            foreach (FieldCondition sdkFieldCondition in sdkReferencedFieldConditions.ReferencedInCondition) 
            {
                FieldConditionConverter fieldConditionConverter = new FieldConditionConverter(sdkFieldCondition);
                apiFieldConditionsInCondition.Add(fieldConditionConverter.ToAPIFieldCondition());
            }

            List<API.FieldCondition> apiFieldConditionsInAction = new List<API.FieldCondition>();
            foreach (FieldCondition sdkFieldCondition in sdkReferencedFieldConditions.ReferencedInAction) 
            {
                FieldConditionConverter fieldConditionConverter = new FieldConditionConverter(sdkFieldCondition);
                apiFieldConditionsInAction.Add(fieldConditionConverter.ToAPIFieldCondition());
            }

            API.ReferencedFieldConditions apiReferencedFieldConditions = new API.ReferencedFieldConditions();
            apiReferencedFieldConditions.ReferencedInCondition = apiFieldConditionsInCondition;
            apiReferencedFieldConditions.ReferencedInAction = apiFieldConditionsInAction;
            return apiReferencedFieldConditions;
        }

        public static ReferencedFieldConditions ToSDK(API.ReferencedFieldConditions apiReferencedFieldConditions) 
        {
            if (apiReferencedFieldConditions == null)
                return null;

            List<FieldCondition> apiFieldConditionsInCondition = new List<FieldCondition>();
            foreach (API.FieldCondition sdkFieldCondition in apiReferencedFieldConditions.ReferencedInCondition) 
            {
                FieldConditionConverter fieldConditionConverter = new FieldConditionConverter(sdkFieldCondition);
                apiFieldConditionsInCondition.Add(fieldConditionConverter.ToSDKFieldCondition());
            }

            List<FieldCondition> apiFieldConditionsInAction = new List<FieldCondition>();
            foreach (API.FieldCondition sdkFieldCondition in apiReferencedFieldConditions.ReferencedInAction) 
            {
                FieldConditionConverter fieldConditionConverter = new FieldConditionConverter(sdkFieldCondition);
                apiFieldConditionsInAction.Add(fieldConditionConverter.ToSDKFieldCondition());
            }

            ReferencedFieldConditions sdkReferencedFieldConditions = new ReferencedFieldConditions();
            sdkReferencedFieldConditions.ReferencedInCondition = apiFieldConditionsInCondition;
            sdkReferencedFieldConditions.ReferencedInAction = apiFieldConditionsInAction;
            return sdkReferencedFieldConditions;
        }
    }
}
