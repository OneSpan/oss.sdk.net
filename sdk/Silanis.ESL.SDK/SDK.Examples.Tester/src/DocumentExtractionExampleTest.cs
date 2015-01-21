using NUnit.Framework;
using System;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
    [TestFixture()]
    public class DocumentExtractionExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            DocumentExtractionExample example = new DocumentExtractionExample(Props.GetInstance());
            example.Run();

            DocumentPackage documentPackage = example.RetrievedPackage;

            // Verify if the required information is correctly extracted.
            Document document = documentPackage.Documents[example.DOCUMENT_NAME];

            foreach (Signature signature in document.Signatures)
            {
                Assert.IsTrue((signature.Style == SignatureStyle.HAND_DRAWN) || (signature.Style == SignatureStyle.FULL_NAME));

                if (signature.Style == SignatureStyle.HAND_DRAWN)
                {
                    Assert.AreEqual(signature.Fields.Count, 2);

                    foreach (Field field in signature.Fields)
                    {
                        if (field.Name.StartsWith("CHECKBOX"))
                        {
                            Assert.AreEqual(field.Style, FieldStyle.UNBOUND_CHECK_BOX);
                        }

                        if (field.Name.StartsWith("LABEL"))
                        {
                            Assert.AreEqual(field.Style, FieldStyle.BOUND_DATE);
                        }
                    }
                }
            }
        }
    }
}

