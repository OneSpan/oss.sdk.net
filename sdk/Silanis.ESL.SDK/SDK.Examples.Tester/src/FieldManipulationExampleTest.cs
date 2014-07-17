using System;
using NUnit.Framework;
using System.Collections.Generic;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
    [TestFixture()]
    public class FieldManipulationExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            FieldManipulationExample example = new FieldManipulationExample(Props.GetInstance());
            example.Run();

            // Test if all signatures are added properly
            Dictionary<string,Field> fieldDictionary = ConvertListToMap(example.addedFields);

            Assert.IsTrue(fieldDictionary.ContainsKey(example.field1.Name));
            Assert.IsTrue(fieldDictionary.ContainsKey(example.field2.Name));
            Assert.IsTrue(fieldDictionary.ContainsKey(example.field3.Name));
        }

        private Dictionary<string, Field> ConvertListToMap(List<Field> fieldList)
        {
            Dictionary<string,Field> fieldDictionary = new Dictionary<string,Field>();
            foreach(Field field in fieldList)
            {
                fieldDictionary.Add(field.Name, field);
            }
            return fieldDictionary;
        }
    }
}

