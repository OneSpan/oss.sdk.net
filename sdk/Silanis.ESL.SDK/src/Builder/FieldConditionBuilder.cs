using System;

namespace Silanis.ESL.SDK.Builder
{
    public class FieldConditionBuilder
    {
        private string id;
        private string condition;
        private string action;

        public static FieldConditionBuilder NewFieldCondition()
        {
            return new FieldConditionBuilder();
        }

        public FieldConditionBuilder WithId(string id)
        {
            this.id = id;
            return this;
        }

        public FieldConditionBuilder WithCondition(string condition)
        {
            this.condition = condition;
            return this;
        }

        public FieldConditionBuilder WithAction(string action)
        {
            this.action = action;
            return this;
        }

        public FieldCondition Build()
        {
            FieldCondition fieldCondition = new FieldCondition();
            fieldCondition.Id = this.id;
            fieldCondition.Condition = this.condition;
            fieldCondition.Action = this.action;
            return fieldCondition;
        }
    }
}