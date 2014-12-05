using NUnit.Framework;
using System;
using System.Collections.Generic;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
    [TestFixture()]
    public class PackageWithUnknownFieldSubtypeCreationExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            PackageWithUnknownFieldSubtypeCreationExample example = new PackageWithUnknownFieldSubtypeCreationExample(Props.GetInstance());
            example.Run();

            DocumentPackage retrievedPackage = example.EslClient.GetPackage(example.PackageId);
            Document document = retrievedPackage.Documents["Default Document Name"];

            foreach(Signature signature in document.Signatures) {
                foreach(Field field in signature.Fields) {
                    FieldStyle style = field.Style;
                    //assertThat("Document name is incorrectly returned.", field.getStyle(), is(isNull()));
                }
            }

            // Test if all signatures are added properly
            Dictionary<string,Field> fieldDictionary = ConvertListToMap(example.addedFields);

            Assert.IsTrue(fieldDictionary.ContainsKey(example.field1.Id));
            Assert.IsTrue(fieldDictionary.ContainsKey(example.field2.Id));
            Assert.IsTrue(fieldDictionary.ContainsKey(example.field3.Id));

            // Test if field1 is deleted properly
            fieldDictionary = ConvertListToMap(example.deletedFields);

            Assert.IsFalse(fieldDictionary.ContainsKey(example.field1.Id));
            Assert.IsTrue(fieldDictionary.ContainsKey(example.field2.Id));
            Assert.IsTrue(fieldDictionary.ContainsKey(example.field3.Id));

            // Test if field3 is updated properly
            fieldDictionary = ConvertListToMap(example.updatedFields);

            Assert.IsFalse(fieldDictionary.ContainsKey(example.field1.Id));
            Assert.IsTrue(fieldDictionary.ContainsKey(example.field2.Id));
            Assert.IsTrue(fieldDictionary.ContainsKey(example.updatedField.Id));
        }

        private Dictionary<string, Field> ConvertListToMap(List<Field> fieldList)
        {
            Dictionary<string,Field> fieldDictionary = new Dictionary<string,Field>();
            foreach(Field field in fieldList)
            {
                fieldDictionary.Add(field.Id, field);
            }
            return fieldDictionary;
        }
    }
}

