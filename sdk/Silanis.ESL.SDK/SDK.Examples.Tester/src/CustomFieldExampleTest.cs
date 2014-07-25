using System;
using NUnit.Framework;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
    public class CustomFieldExampleTest
    {
		[Test]
		public void verify() {
			CustomFieldExample example = new CustomFieldExample(Props.GetInstance());
			example.Run();

			DocumentPackage documentPackage = example.EslClient.GetPackage(example.PackageId);

            Assert.IsTrue(example.EslClient.GetCustomFieldService().DoesCustomFieldExist(example.customFieldId1));
            Assert.IsTrue(example.EslClient.GetCustomFieldService().DoesCustomFieldValueExist(example.customFieldId1));
            Assert.IsFalse(example.EslClient.GetCustomFieldService().DoesCustomFieldExist(example.customFieldId2));

			Assert.AreEqual(documentPackage.Documents["First Document"].Signatures.Count, 1);
            Assert.AreEqual(documentPackage.Documents["First Document"].Signatures[0].SignerEmail, example.email1);
			Assert.IsNotNull(documentPackage.Documents["First Document"].Signatures[0].Fields[0]);

            // Get first custom field
            CustomField retrievedCustomField = example.retrievedCustomField;
            Assert.AreEqual(retrievedCustomField.Id, example.customFieldId1);
            Assert.AreEqual(retrievedCustomField.Value, example.DEFAULT_VALUE);
            Assert.AreEqual(retrievedCustomField.Translations[0].Name, example.ENGLISH_NAME);
            Assert.AreEqual(retrievedCustomField.Translations[0].Language, example.ENGLISH_LANGUAGE);
            Assert.AreEqual(retrievedCustomField.Translations[0].Description, example.ENGLISH_DESCRIPTION);
            Assert.AreEqual(retrievedCustomField.Translations[1].Name, example.FRENCH_NAME);
            Assert.AreEqual(retrievedCustomField.Translations[1].Language, example.FRENCH_LANGUAGE);
            Assert.AreEqual(retrievedCustomField.Translations[1].Description, example.FRENCH_DESCRIPTION);

            // Get entire list of custom fields
            Assert.Greater(example.retrievedCustomFieldList1.Count, 0);

            // Get first page of custom fields
            Assert.Greater(example.retrievedCustomFieldList2.Count, 0);
		}
    }
}

