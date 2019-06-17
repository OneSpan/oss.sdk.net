using NUnit.Framework;
using System.Collections.Generic;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
    [TestFixture()]
    public class PackageReferencedConditionsExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            PackageReferencedConditionsExample example = new PackageReferencedConditionsExample();
            example.Run();

            Assert.IsNotNull(example.PackageLevelRefConditions);
            Assert.AreEqual(example.PackageLevelRefConditions.Documents.Count, 2);

            List<ReferencedDocument> documentLevelRefConditionsDocuments = example.DocumentLevelRefConditions.Documents;
            Assert.AreEqual(documentLevelRefConditionsDocuments.Count, 1);
            Assert.AreEqual(documentLevelRefConditionsDocuments[0].Fields.Count, 3);

            List<ReferencedField> fieldLevelRefConditionsDoc1Fields = example.FieldLevelRefConditions.Documents[0].Fields;
            Assert.AreEqual(fieldLevelRefConditionsDoc1Fields.Count, 1);

            ReferencedFieldConditions field1Conditions = fieldLevelRefConditionsDoc1Fields[0].Conditions;
            Assert.AreEqual(field1Conditions.ReferencedInCondition.Count, 2);
            Assert.AreEqual(field1Conditions.ReferencedInAction.Count, 0);
        }
    }
}

