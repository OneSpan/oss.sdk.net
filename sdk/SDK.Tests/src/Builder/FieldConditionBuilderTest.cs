using System;
using NUnit.Framework;
using Silanis.ESL.SDK.Builder;
using Silanis.ESL.SDK;

namespace SDK.Tests
{
    public class FielConditionBuilderTest
    {
        private static readonly String ID = "Condition1";
        private static readonly String CONDITION = "document['doc1'].field['field1'].empty == true";
        private static readonly String ACTION = "document['doc1'].field['field2'].disabled = false";

        [Test]
        public void BuildTest()
        {
            FieldCondition fieldCondition = FieldConditionBuilder.NewFieldCondition()
                .WithId(ID)
                .WithCondition(CONDITION)
                .WithAction(ACTION)
                .Build();

            Assert.AreEqual(ID, fieldCondition.Id);
            Assert.AreEqual(CONDITION, fieldCondition.Condition);
            Assert.AreEqual(ACTION, fieldCondition.Action);
        }
    }
}