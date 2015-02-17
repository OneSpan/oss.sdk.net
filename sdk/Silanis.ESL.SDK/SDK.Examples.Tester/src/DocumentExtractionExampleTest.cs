using NUnit.Framework;
using System;
using Silanis.ESL.SDK;
using System.Collections.Generic;

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

            Assert.AreEqual(6, document.Signatures.Count);

            Signature signature0 = document.Signatures[2];
            AssertSignature(signature0, 3);
            List<Field> fields0 = signature0.Fields;

            AssertField(fields0, FieldStyle.BOUND_NAME, "{signer.name}", 225, 303, 195, 28, 0);
            AssertField(fields0, FieldStyle.UNBOUND_CHECK_BOX, null, 283, 94, 85, 28, 0);
            AssertField(fields0, FieldStyle.BOUND_NAME, "{signer.name}", 222, 537, 195, 28, 0);
            AssertSignature(signature0, SignatureStyle.HAND_DRAWN, 224, 90, 195, 28, 0);

            //Assert signature #1
            Signature signature1 = document.Signatures[3];
            AssertSignature(signature1, 2);
            List<Field> fields1 = signature1.Fields;
            AssertField(fields1, FieldStyle.UNBOUND_TEXT_FIELD, null, 343, 315, 195, 28, 0);
            AssertField(fields1, FieldStyle.BOUND_TITLE, "{signer.title}", 342, 527, 195, 28, 0);
            AssertSignature(signature1, SignatureStyle.HAND_DRAWN, 345, 93, 195, 28, 0);

            //Assert signature #2
            Signature signature2 = document.Signatures[1];
            AssertSignature(signature2, 0);
            AssertSignature(signature2, SignatureStyle.INITIALS, 81, 89, 195, 28, 0);

            //Assert signature #3
            Signature signature3 = document.Signatures[0];
            AssertSignature(signature3, 1);
            List<Field> fields3 = signature3.Fields;
            AssertField(fields3, FieldStyle.BOUND_COMPANY, "{signer.company}", 170, 542, 195, 28, 0);
            AssertSignature(signature3, SignatureStyle.FULL_NAME, 131, 541, 195, 28, 0);

            //Assert signature #4
            Signature signature4 = document.Signatures[5];
            AssertSignature(signature4, 2);
            List<Field> fields4 = signature4.Fields;
            AssertField(fields4, FieldStyle.BOUND_NAME, "{signer.name}", 724, 299, 195, 28, 0);
            AssertField(fields4, FieldStyle.BOUND_DATE, "{approval.signed}", 724, 509, 195, 28, 0);
            AssertSignature(signature4, SignatureStyle.HAND_DRAWN, 726, 91, 195, 28, 0);

            //Assert signature #5
            Signature signature5 = document.Signatures[4];
            AssertSignature(signature5, 2);
            List<Field> fields5 = signature5.Fields;
            AssertField(fields5, FieldStyle.BOUND_NAME, "{signer.name}", 42, 262, 195, 28, 1);
            AssertField(fields5, FieldStyle.BOUND_DATE, "{approval.signed}", 41, 471, 195, 28, 1);
            AssertSignature(signature5, SignatureStyle.HAND_DRAWN, 43, 54, 195, 28, 1);
        }

        private void AssertField(List<Field> fields, FieldStyle subtype, String binding,
                                 int top, int left, int width, int height, int pageIndex) 
        {
            bool matches = false;
            foreach (Field field in fields) 
            {
                if (equals(field.Binding, binding) &&
                    equals(field.Style, subtype) &&
                    equals(field.Y, top) &&
                    equals(field.X, left) &&
                    equals(field.Width, width) &&
                    equals(field.Height, height) &&
                    equals(field.Page, pageIndex)) 
                {
                    matches = true;
                    break;
                }
            }

            Assert.IsTrue(matches);
        }

        private void AssertSignature(Signature signature, SignatureStyle style,
                                     int top, int left, int width, int height, int pageIndex) 
        {

            bool matches = false;
            if (equals(signature.Style, style) &&
                equals(signature.Y, top) &&
                equals(signature.X, left) &&
                equals(signature.Width, width) &&
                equals(signature.Height, height) &&
                equals(signature.Page, pageIndex)) 
            {
                matches = true;
            }

            Assert.IsTrue(matches);
        }

        private void AssertSignature(Signature signature, int numbOfFields) 
        {
            Assert.AreEqual(numbOfFields, signature.Fields.Count);
        }

        private bool equals(Object object1, Object object2) 
        {
            if (object1 == object2) 
            {
                return true;
            }
            if (object1 == null || object2 == null) 
            {
                return false;
            }
            return (object1 == object2);
        }

        private bool equals(double number1, double number2) 
        {
            if (number1 == number2) 
            {
                return true;
            }
            double interval = number1 - number2;
            if (interval < 1 && interval > -1 ) 
            {
                return true;
            }
            return false;
        }
    }
}

