using NUnit.Framework;
using System;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    [TestFixture()]
    public class ClickableAreaExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            ClickableAreaExample example = new ClickableAreaExample();
            example.Run();

            DocumentPackage documentPackage = example.RetrievedPackage;

            foreach (Signature signature in documentPackage.GetDocument(ClickableAreaExample.DOCUMENT_NAME).Signatures)
            {
                foreach (Field field in signature.Fields)
                {
                    // Modified after creation via ModifyField(); reflects the post-modification size.
                    if (field.Id == ClickableAreaExample.CHECKBOX_ID)
                    {
                        Assert.AreEqual(ClickableAreaExample.MODIFIED_CLICKABLE_AREA_WIDTH, field.ClickableArea.Width);
                        Assert.AreEqual(ClickableAreaExample.MODIFIED_CLICKABLE_AREA_HEIGHT, field.ClickableArea.Height);
                        Assert.AreEqual(ClickableAreaAlignment.CENTER, field.ClickableArea.Alignment);
                    }
                    if (field.Id == ClickableAreaExample.ALIGNED_CHECKBOX_ID)
                    {
                        Assert.AreEqual(ClickableAreaExample.ALIGNED_CLICKABLE_AREA_WIDTH, field.ClickableArea.Width);
                        Assert.AreEqual(ClickableAreaExample.ALIGNED_CLICKABLE_AREA_HEIGHT, field.ClickableArea.Height);
                        Assert.AreEqual(ClickableAreaExample.ALIGNED_CLICKABLE_AREA_ALIGNMENT, field.ClickableArea.Alignment);
                    }
                    if (field.Id == ClickableAreaExample.RADIO_ID)
                    {
                        Assert.AreEqual(ClickableAreaExample.RADIO_CLICKABLE_AREA_WIDTH, field.ClickableArea.Width);
                        Assert.AreEqual(ClickableAreaExample.RADIO_CLICKABLE_AREA_HEIGHT, field.ClickableArea.Height);
                        Assert.AreEqual(ClickableAreaAlignment.CENTER, field.ClickableArea.Alignment);
                    }
                    // Added after creation via AddField().
                    if (field.Id == ClickableAreaExample.ADDED_CHECKBOX_ID)
                    {
                        Assert.AreEqual(ClickableAreaExample.ADDED_CLICKABLE_AREA_WIDTH, field.ClickableArea.Width);
                        Assert.AreEqual(ClickableAreaExample.ADDED_CLICKABLE_AREA_HEIGHT, field.ClickableArea.Height);
                        Assert.AreEqual(ClickableAreaAlignment.CENTER, field.ClickableArea.Alignment);
                    }
                }
            }
        }
    }
}
