using System;
namespace OneSpanSign.Sdk
{
    internal class FieldConditionConverter
    {
        private OneSpanSign.Sdk.FieldCondition sdkFieldCondition = null;
        private OneSpanSign.API.FieldCondition apiFieldCondition = null;

        public FieldConditionConverter (OneSpanSign.Sdk.FieldCondition sdkFieldCondition)
        {
            this.sdkFieldCondition = sdkFieldCondition;
        }

        public FieldConditionConverter (OneSpanSign.API.FieldCondition apiFieldCondition)
        {
            this.apiFieldCondition = apiFieldCondition;
        }

        public OneSpanSign.API.FieldCondition ToAPIFieldCondition ()
        {
            if (sdkFieldCondition == null) 
            {
                return apiFieldCondition;
            }

            OneSpanSign.API.FieldCondition result = new OneSpanSign.API.FieldCondition ();

            result.Id = sdkFieldCondition.Id;
            result.Condition = sdkFieldCondition.Condition;
            result.Action = sdkFieldCondition.Action;

            return result;
        }

        public OneSpanSign.Sdk.FieldCondition ToSDKFieldCondition ()
        {
            if (apiFieldCondition == null) 
            {
                return sdkFieldCondition;
            }

            OneSpanSign.Sdk.FieldCondition result = new OneSpanSign.Sdk.FieldCondition ();

            result.Id = apiFieldCondition.Id;
            result.Condition = apiFieldCondition.Condition;
            result.Action = apiFieldCondition.Action;

            return result;
        }
    }
}
