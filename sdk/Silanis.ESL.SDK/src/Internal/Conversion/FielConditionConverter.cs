using System;
namespace Silanis.ESL.SDK
{
    internal class FieldConditionConverter
    {
        private Silanis.ESL.SDK.FieldCondition sdkFieldCondition = null;
        private Silanis.ESL.API.FieldCondition apiFieldCondition = null;

        public FieldConditionConverter (Silanis.ESL.SDK.FieldCondition sdkFieldCondition)
        {
            this.sdkFieldCondition = sdkFieldCondition;
        }

        public FieldConditionConverter (Silanis.ESL.API.FieldCondition apiFieldCondition)
        {
            this.apiFieldCondition = apiFieldCondition;
        }

        public Silanis.ESL.API.FieldCondition ToAPIFieldCondition ()
        {
            if (sdkFieldCondition == null) 
            {
                return apiFieldCondition;
            }

            Silanis.ESL.API.FieldCondition result = new Silanis.ESL.API.FieldCondition ();

            result.Id = sdkFieldCondition.Id;
            result.Condition = sdkFieldCondition.Condition;
            result.Action = sdkFieldCondition.Action;

            return result;
        }

        public Silanis.ESL.SDK.FieldCondition ToSDKFieldCondition ()
        {
            if (apiFieldCondition == null) 
            {
                return sdkFieldCondition;
            }

            Silanis.ESL.SDK.FieldCondition result = new Silanis.ESL.SDK.FieldCondition ();

            result.Id = apiFieldCondition.Id;
            result.Condition = apiFieldCondition.Condition;
            result.Action = apiFieldCondition.Action;

            return result;
        }
    }
}
