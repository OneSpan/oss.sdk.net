using System;
using NUnit.Framework;
using System.Collections.Generic;
using Silanis.ESL.SDK;
using NUnit.Framework.SyntaxHelpers;

namespace SDK.Examples
{
    [TestFixture()]
    public class DocumentLayoutExampleTest
    {
        private DocumentLayoutExample example;

        [Test()]
        public void VerifyResult()
        {
            example = new DocumentLayoutExample(Props.GetInstance());
            example.Run();

            // Assert the layout was created correctly.
            IList<DocumentPackage> layouts = example.layouts;
            Assert.Greater(layouts.Count, 0);

            foreach (DocumentPackage layout in layouts)
            {
                if (layout.Name.Equals(example.LAYOUT_PACKAGE_NAME))
                {
                    Assert.AreEqual(layout.Id.Id, example.layoutId);
                    Assert.AreEqual(layout.Description, example.LAYOUT_PACKAGE_DESCRIPTION);
                    Assert.AreEqual(layout.Documents.Count, 1);
                    Assert.AreEqual(layout.Signers.Count, 2);

                    Document document = layout.Documents[example.LAYOUT_DOCUMENT_NAME];
                    Assert.AreEqual(document.Signatures.Count, 1);

                    // Validate the signature fields of layout were saved correctly.
                    ValidateSignatureFields(document.Signatures);
                }
            }

            // Assert that document layout was applied correctly to document.
            DocumentPackage packageWithLayout = example.packageWithLayout;

            Assert.AreNotEqual(packageWithLayout.Name, example.LAYOUT_PACKAGE_NAME);
            Assert.AreNotEqual(packageWithLayout.Description, example.LAYOUT_PACKAGE_DESCRIPTION);
            Assert.AreEqual(packageWithLayout.Signers.Count, 2);
            Assert.AreEqual(packageWithLayout.Documents.Count, 2);

            Document documentWithLayout = packageWithLayout.Documents[example.APPLY_LAYOUT_DOCUMENT_NAME];
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
                Assert.AreEqual(signature.Fields.Count, 2);

                foreach (Field field in signature.Fields)
                {
                    if (field.Name.Equals(example.FIELD_1_NAME))
                    {
                        Assert.AreEqual(field.Style, FieldStyle.BOUND_TITLE);
                        Assert.AreEqual(field.Page, 0);
                        Assert.Greater(field.X, 99);
                        Assert.Less(field.X, 101);
                        Assert.Greater(field.Y, 199);
                        Assert.Less(field.Y, 201);
                    }

                    if (field.Name.Equals(example.FIELD_2_NAME))
                    {
                        Assert.AreEqual(field.Style, FieldStyle.BOUND_COMPANY);
                        Assert.AreEqual(field.Page, 0);
                        Assert.Greater(field.X, 99);
                        Assert.Less(field.X, 101);
                        Assert.Greater(field.Y, 299);
                        Assert.Less(field.Y, 301);
                    }
                }
            }
        }
    }
}

