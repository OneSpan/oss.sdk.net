using NUnit.Framework;
using System;
using Silanis.ESL.SDK;
using System.Collections.Generic;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    [TestFixture()]
    public class BasicPackageCreationExampleTest
    {
        private readonly string EXTERNAL_SIGNER = "EXTERNAL_SIGNER";

        [Test()]
        public void VerifyResult()
        {
            BasicPackageCreationExample example = new BasicPackageCreationExample();
            example.Run();

            DocumentPackage documentPackage = example.RetrievedPackage;

            // Verify if the package is created correctly
            Assert.AreEqual("This is a package created using the eSignLive SDK", documentPackage.Description);
            Assert.AreEqual("This message should be delivered to all signers", documentPackage.EmailMessage);
            Assert.AreEqual("Canada/Mountain", documentPackage.TimezoneId);

            Assert.AreEqual(false, documentPackage.Settings.EnableInPerson);

            // Verify if the sdk version is set correctly
            Assert.IsTrue(documentPackage.Attributes.Contents.ContainsKey( "sdk" ));
            Assert.IsTrue(documentPackage.Attributes.Contents["sdk"].ToString().Contains(".NET"));

            // Verify if the origin is set correctly
            Assert.IsTrue(documentPackage.Attributes.Contents.ContainsKey("origin"));
            Assert.IsTrue(documentPackage.Attributes.Contents["origin"].ToString().Contains("api"));

            // Signer 1
            Signer signer = documentPackage.GetSigner(example.email1);
            Assert.AreEqual("Client1", signer.Id);
            Assert.AreEqual("John", signer.FirstName);
            Assert.AreEqual("Smith", signer.LastName);
            Assert.AreEqual("Managing Director", signer.Title);
            Assert.AreEqual("Acme Inc.", signer.Company);
            Assert.AreEqual(EXTERNAL_SIGNER, signer.SignerType);

            // Signer 2
            signer = documentPackage.GetSigner(example.email2);
            Assert.AreEqual("Patty", signer.FirstName);
            Assert.AreEqual("Galant", signer.LastName);
            Assert.AreEqual(EXTERNAL_SIGNER, signer.SignerType);

            // Document 1
            Document document = documentPackage.GetDocument(example.DOCUMENT1_NAME);
            Assert.AreEqual(1, document.NumberOfPages);

            Signature signature = document.Signatures [0];
            Assert.AreEqual (example.email1, signature.SignerEmail);
            Assert.AreEqual (example.SIGNATURE_FONT_SIZE, signature.FontSize);

            List<Field> fields = signature.Fields;
            Field field1 = fields[0];
            Assert.AreEqual (FieldStyle.BOUND_NAME, field1.Style);
            Assert.AreEqual (0, field1.Page);
            Assert.AreEqual (example.AUTO_FIELD_FONT_SIZE, field1.FontSize);

            Field field2 = fields [1];
            Assert.AreEqual (FieldStyle.UNBOUND_CHECK_BOX, field2.Style);
            Assert.AreEqual (0, field2.Page);
            Assert.AreEqual (FieldBuilder.CHECKBOX_CHECKED, field2.Value);

            // Document 2
            document = documentPackage.GetDocument(example.DOCUMENT2_NAME);
            Assert.AreEqual(1, document.NumberOfPages);

            fields = document.Signatures[0].Fields;

            Field field = findFieldByName("firstField", fields);
            Assert.AreEqual(FieldStyle.UNBOUND_RADIO_BUTTON, field.Style);
            Assert.AreEqual(0, field.Page);
            Assert.AreEqual("", field.Value);
            Assert.AreEqual("group", field.Validator.Options[0]);

            field = findFieldByName("secondField", fields);
            Assert.AreEqual(FieldStyle.UNBOUND_RADIO_BUTTON, field.Style);
            Assert.AreEqual(0, field.Page);
            Assert.AreEqual(FieldBuilder.RADIO_SELECTED, field.Value);
            Assert.AreEqual("group", field.Validator.Options[0]);

            field = findFieldByName("thirdField", fields);
            Assert.AreEqual(FieldStyle.UNBOUND_RADIO_BUTTON, field.Style);
            Assert.AreEqual(0, field.Page);
            Assert.AreEqual("", field.Value);
            Assert.AreEqual("group", field.Validator.Options[0]);

        }

        private Field findFieldByName(string fieldName, List<Field> fields)
        {
            foreach (Field field in fields) 
            {
                if (field.Name != null && field.Name.Equals(fieldName)) 
                {
                    return field;
                }
            }
            
            return null;
        }
    }
}

