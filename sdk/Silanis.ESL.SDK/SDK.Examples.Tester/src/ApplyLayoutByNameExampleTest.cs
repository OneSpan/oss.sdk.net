using System;
using NUnit.Framework;
using Silanis.ESL.SDK;
using System.Collections.Generic;

namespace SDK.Examples
{
    public class ApplyLayoutByNameExampleTest
    {
        private readonly double TOLERANCE = 1.25;

        private ApplyLayoutByNameExample example;

        [Test()]
        public void VerifyResult()
        {
            example = new ApplyLayoutByNameExample();
            example.Run();

            // Assert that document layout was applied correctly to document.
            DocumentPackage documentPackage = example.RetrievedPackage;

            Assert.AreNotEqual(documentPackage.Name, example.LAYOUT_PACKAGE_NAME);
            Assert.AreNotEqual(documentPackage.Description, example.LAYOUT_PACKAGE_DESCRIPTION);
            Assert.AreEqual(documentPackage.Signers.Count, 2);
            Assert.AreEqual(documentPackage.Documents.Count, 2);

            Document documentWithLayout = documentPackage.GetDocument(example.APPLY_LAYOUT_DOCUMENT_NAME);
            Assert.AreEqual(documentWithLayout.Description, example.APPLY_LAYOUT_DOCUMENT_DESCRIPTION);
            Assert.AreEqual(documentWithLayout.Id, example.APPLY_LAYOUT_DOCUMENT_ID);
            Assert.AreEqual(documentWithLayout.Signatures.Count, 1);

            // Validate that the signature fields were applied correctly to document.
            ValidateSignatureFields(documentWithLayout.Signatures);
        }

        private void ValidateSignatureFields(IList<Signature> signatures)
        {
            foreach (Signature signature in signatures)
            {
                Assert.AreEqual(signature.SignerEmail, example.email1);
                Assert.AreEqual(signature.Page, 0);
                Assert.Greater(signature.X, 120 - TOLERANCE);
                Assert.Less(signature.X, 120 + TOLERANCE);
                Assert.Greater(signature.Y, 100 - TOLERANCE);
                Assert.Less(signature.Y, 100 + TOLERANCE);
                Assert.AreEqual(signature.Fields.Count, 2);

                foreach (Field field in signature.Fields)
                {
                    if (field.Name.Equals(example.FIELD_1_NAME))
                    {
                        Assert.AreEqual(field.Style, FieldStyle.BOUND_TITLE);
                        Assert.AreEqual(field.Page, 0);
                        Assert.Greater(field.X, 120 - TOLERANCE);
                        Assert.Less(field.X, 120 + TOLERANCE);
                        Assert.Greater(field.Y, 200 - TOLERANCE);
                        Assert.Less(field.Y, 200 + TOLERANCE);
                    }

                    if (field.Name.Equals(example.FIELD_2_NAME))
                    {
                        Assert.AreEqual(field.Style, FieldStyle.BOUND_COMPANY);
                        Assert.AreEqual(field.Page, 0);
                        Assert.Greater(field.X, 120 - TOLERANCE);
                        Assert.Less(field.X, 120 + TOLERANCE);
                        Assert.Greater(field.Y, 300 - TOLERANCE);
                        Assert.Less(field.Y, 300 + TOLERANCE);
                    }
                }
            }
        }
    }
}

