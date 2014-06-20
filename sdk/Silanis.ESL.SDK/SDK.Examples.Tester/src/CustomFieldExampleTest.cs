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

			Assert.IsTrue(example.EslClient.GetCustomFieldService().DoesCustomFieldExist(example.CustomFieldId1));
			Assert.IsTrue(example.EslClient.GetCustomFieldService().DoesCustomFieldValueExist(example.CustomFieldId1));
			Assert.IsFalse(example.EslClient.GetCustomFieldService().DoesCustomFieldExist(example.CustomFieldId2));

			Assert.AreEqual(documentPackage.Documents["First Document"].Signatures.Count, 1);
			Assert.AreEqual(documentPackage.Documents["First Document"].Signatures[0].SignerEmail, example.Email1);
			Assert.IsNotNull(documentPackage.Documents["First Document"].Signatures[0].Fields[0]);
		}
    }
}

