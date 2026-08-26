using NUnit.Framework;
using System;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    [TestFixture()]
    public class ClickableAreaCloneExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            ClickableAreaCloneExample example = new ClickableAreaCloneExample();
            example.Run();

            DocumentPackage documentPackage = example.RetrievedPackage;

            foreach (Signature signature in documentPackage.GetDocument(ClickableAreaCloneExample.DOCUMENT_NAME).Signatures)
            {
                foreach (Field field in signature.Fields)
                {
                    if (field.Id == ClickableAreaCloneExample.CHECKBOX_ID)
                    {
                        Assert.AreEqual(ClickableAreaCloneExample.CLICKABLE_AREA_WIDTH, field.ClickableArea.Width);
                        Assert.AreEqual(ClickableAreaCloneExample.CLICKABLE_AREA_HEIGHT, field.ClickableArea.Height);
                        Assert.AreEqual(ClickableAreaAlignment.CENTER, field.ClickableArea.Alignment);
                    }
                }
            }
        }
    }
}
