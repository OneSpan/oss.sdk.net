using System;
using NUnit.Framework;
using Silanis.ESL.SDK;
using NUnit.Framework.SyntaxHelpers;
using System.Collections.Generic;

namespace SDK.Examples
{
    [TestFixture()]
    public class QRCodeExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            QRCodeExample example = new QRCodeExample(Props.GetInstance());
            example.Run();

            // Verify QR codes were added to document
            Assert.AreEqual(example.addedQRCode1.Style, FieldStyle.UNBOUND_QRCODE);
            Assert.That(example.addedQRCode1.Height, Is.EqualTo(77.0).Within(1));
            Assert.That(example.addedQRCode1.Width, Is.EqualTo(77.0).Within(1));
            Assert.That(example.addedQRCode1.X, Is.EqualTo(400.0).Within(1));
            Assert.That(example.addedQRCode1.Y, Is.EqualTo(100.0).Within(1));

            Assert.AreEqual(example.addedQRCode2.Style, FieldStyle.UNBOUND_QRCODE);
            Assert.That(example.addedQRCode2.Height, Is.EqualTo(77.0).Within(1));
            Assert.That(example.addedQRCode2.Width, Is.EqualTo(77.0).Within(1));
            Assert.That(example.addedQRCode2.X, Is.EqualTo(500.0).Within(1));
            Assert.That(example.addedQRCode2.Y, Is.EqualTo(100.0).Within(1));

            // Assert the first QR code was modified correctly
            IList<Field> modifiedQRCodeList = example.modifiedQRCodeList;
            Assert.AreEqual(modifiedQRCodeList.Count, 2);

            foreach (Field field in modifiedQRCodeList)
            {
                if (field.Id.Equals(example.qrCodeId1))
                {
                    Assert.AreEqual(field.Style, FieldStyle.UNBOUND_QRCODE);
                    Assert.That(field.Height, Is.EqualTo(77.0).Within(1));
                    Assert.That(field.Width, Is.EqualTo(77.0).Within(1));
                    Assert.That(field.X, Is.EqualTo(400.0).Within(1));
                    Assert.That(field.Y, Is.EqualTo(500.0).Within(1));    
                }

            }

            // Assert the second QR code was deleted
            IList<Field> deletedQRCodeList = example.deletedQRCodeList;
            Assert.AreEqual(deletedQRCodeList.Count, 1);

            // Assert the QR codes was replaced with the updated ones
            IList<Field> updatedQRCodeList = example.updatedQRCodeList;
            foreach (Field updatedQRCode in updatedQRCodeList)
            {
                if (updatedQRCode.Id.Equals(example.qrCodeId1))
                {
                    Assert.AreEqual(updatedQRCode.Style, FieldStyle.UNBOUND_QRCODE);
                    Assert.That(updatedQRCode.Height, Is.EqualTo(77.0).Within(1));
                    Assert.That(updatedQRCode.Width, Is.EqualTo(77.0).Within(1));
                    Assert.That(updatedQRCode.X, Is.EqualTo(200.0).Within(1));
                    Assert.That(updatedQRCode.Y, Is.EqualTo(600.0).Within(1));
                }
                if (updatedQRCode.Id.Equals(example.qrCodeId2))
                {
                    Assert.AreEqual(updatedQRCode.Style, FieldStyle.UNBOUND_QRCODE);
                    Assert.That(updatedQRCode.Height, Is.EqualTo(77.0).Within(1));
                    Assert.That(updatedQRCode.Width, Is.EqualTo(77.0).Within(1));
                    Assert.That(updatedQRCode.X, Is.EqualTo(300.0).Within(1));
                    Assert.That(updatedQRCode.Y, Is.EqualTo(600.0).Within(1));
                }
            }
        }
    }
}

