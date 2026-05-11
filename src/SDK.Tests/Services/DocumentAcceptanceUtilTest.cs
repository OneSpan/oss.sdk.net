using NUnit.Framework;
using System.Collections.Generic;
using OneSpanSign.API;
using OneSpanSign.Sdk.Services;

namespace SDK.Tests
{
    [TestFixture]
    public class DocumentAcceptanceUtilTest
    {
        private const string DefaultConsentId = DocumentAcceptanceUtil.DEFAULT_CONSENT_ID;
        private const string AcceptedBySignersKey = DocumentAcceptanceUtil.DOC_DATA_ACCEPTED_BY_SIGNERS;

        [Test]
        public void NullPackageReturnsFalse()
        {
            Assert.IsFalse(DocumentAcceptanceUtil.HasAcceptedDefaultConsent(null));
        }

        [Test]
        public void EmptyDocumentsReturnsFalse()
        {
            Assert.IsFalse(DocumentAcceptanceUtil.HasAcceptedDefaultConsent(new Package()));
        }

        [Test]
        public void NoDefaultConsentDocumentReturnsFalse()
        {
            var package = PackageWithDocument(DocumentWithId("other-document"));
            Assert.IsFalse(DocumentAcceptanceUtil.HasAcceptedDefaultConsent(package));
        }

        [Test]
        public void DefaultConsentDocumentNullDataReturnsFalse()
        {
            var package = PackageWithDocument(DefaultConsentDocument(data: null));
            Assert.IsFalse(DocumentAcceptanceUtil.HasAcceptedDefaultConsent(package));
        }

        [Test]
        public void DefaultConsentDocumentEmptyDataReturnsFalse()
        {
            var package = PackageWithDocument(DefaultConsentDocument(new Dictionary<string, object>()));
            Assert.IsFalse(DocumentAcceptanceUtil.HasAcceptedDefaultConsent(package));
        }

        [Test]
        public void DefaultConsentDocumentNullValueReturnsFalse()
        {
            var package = PackageWithDocument(DefaultConsentDocument(AcceptedBySignersKey, null));
            Assert.IsFalse(DocumentAcceptanceUtil.HasAcceptedDefaultConsent(package));
        }

        [Test]
        public void DefaultConsentDocumentEmptyStringValueReturnsFalse()
        {
            var package = PackageWithDocument(DefaultConsentDocument(AcceptedBySignersKey, ""));
            Assert.IsFalse(DocumentAcceptanceUtil.HasAcceptedDefaultConsent(package));
        }

        [Test]
        public void DefaultConsentDocumentWhitespaceOnlyValueReturnsFalse()
        {
            var package = PackageWithDocument(DefaultConsentDocument(AcceptedBySignersKey, "   "));
            Assert.IsFalse(DocumentAcceptanceUtil.HasAcceptedDefaultConsent(package));
        }

        [Test]
        public void DefaultConsentDocumentNonStringValueReturnsFalse()
        {
            var package = PackageWithDocument(DefaultConsentDocument(AcceptedBySignersKey, 42));
            Assert.IsFalse(DocumentAcceptanceUtil.HasAcceptedDefaultConsent(package));
        }

        [Test]
        public void DefaultConsentDocumentOnlyEmptySegmentsAfterSplitReturnsFalse()
        {
            var package = PackageWithDocument(DefaultConsentDocument(AcceptedBySignersKey, ",,"));
            Assert.IsFalse(DocumentAcceptanceUtil.HasAcceptedDefaultConsent(package));
        }

        [Test]
        public void SingleAcceptedSignerReturnsTrue()
        {
            var package = PackageWithDocument(DefaultConsentDocument(AcceptedBySignersKey, "signer1"));
            Assert.IsTrue(DocumentAcceptanceUtil.HasAcceptedDefaultConsent(package));
        }

        [Test]
        public void MultipleAcceptedSignersReturnsTrue()
        {
            var package = PackageWithDocument(DefaultConsentDocument(AcceptedBySignersKey, "signer1,signer2,signer3"));
            Assert.IsTrue(DocumentAcceptanceUtil.HasAcceptedDefaultConsent(package));
        }

        [Test]
        public void AcceptedSignersSpacesAroundCommasReturnsTrue()
        {
            var package = PackageWithDocument(DefaultConsentDocument(AcceptedBySignersKey, "signer1 , signer2"));
            Assert.IsTrue(DocumentAcceptanceUtil.HasAcceptedDefaultConsent(package));
        }

        [Test]
        public void AcceptedSignersLeadingTrailingSpacesReturnsTrue()
        {
            var package = PackageWithDocument(DefaultConsentDocument(AcceptedBySignersKey, " signer1 "));
            Assert.IsTrue(DocumentAcceptanceUtil.HasAcceptedDefaultConsent(package));
        }

        [Test]
        public void DefaultConsentDocumentNotFirstReturnsTrue()
        {
            var package = new Package();
            package.AddDocument(DocumentWithId("other-document"));
            package.AddDocument(DefaultConsentDocument(AcceptedBySignersKey, "signer1"));
            Assert.IsTrue(DocumentAcceptanceUtil.HasAcceptedDefaultConsent(package));
        }

        private static Package PackageWithDocument(Document document)
        {
            var package = new Package();
            package.AddDocument(document);
            return package;
        }

        private static Document DocumentWithId(string id)
        {
            return new Document { Id = id };
        }

        private static Document DefaultConsentDocument(IDictionary<string, object> data)
        {
            return new Document { Id = DefaultConsentId, Data = data };
        }

        private static Document DefaultConsentDocument(string key, object value)
        {
            return DefaultConsentDocument(new Dictionary<string, object> { { key, value } });
        }
    }
}
