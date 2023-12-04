using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    [TestFixture()]
    public class GetFieldValuesExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            GetFieldSummaryExample example = new GetFieldSummaryExample();
            example.Run();

            IList<FieldSummary> fieldSummaries = example.fieldSummaries;

            Assert.AreEqual("X", fieldSummaries[0].FieldValue);
            Assert.AreEqual("FieldId1", fieldSummaries[0].FieldId);
            Assert.AreEqual("CheckBox1", fieldSummaries[0].FieldName);
        }
    }
}