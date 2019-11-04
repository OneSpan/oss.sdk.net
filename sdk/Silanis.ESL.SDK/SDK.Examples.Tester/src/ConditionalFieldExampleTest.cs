using System;
using NUnit.Framework;

namespace SDK.Examples
{
    [TestFixture()]
    public class ConditionalFieldExampleTest
    {
        [Test()]
        public void VerifyResult ()
        {
            ConditionalFieldExample example = new ConditionalFieldExample ();
            example.Run ();

            Assert.AreEqual (example.RetrievedPackage.Conditions.Count, 1);

            Assert.AreEqual (example.RetrievedPackageWithUpdatedConditions.Conditions.Count, 1);
            Assert.AreEqual (example.RetrievedPackageWithUpdatedConditions.Conditions[0].Action, "document['DocumentId'].field['fieldId1'].disabled = true");

            Assert.AreEqual (example.RetrievedPackageWithoutConditions.Conditions.Count, 0);
        }
    }
}
