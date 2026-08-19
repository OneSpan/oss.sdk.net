using System.Collections.Generic;
using Newtonsoft.Json;
using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Tests
{
    /// <summary>
    /// Covers every extraction status / reason code combination the server reports, on both the
    /// verification result and the nested extraction result.
    /// </summary>
    [TestFixture]
    public class ExtractionOutcomeTest
    {
        [Test]
        public void CompletedHasNoReasonCode()
        {
            AssertOutcome("COMPLETED", null, ExtractionStatus.COMPLETED, null);
        }

        [Test]
        public void NotPerformedBecauseExtractionIsNotEnabled()
        {
            AssertOutcome("NOT_PERFORMED", "EXTRACTION_NOT_ENABLED",
                ExtractionStatus.NOT_PERFORMED, ExtractionReasonCode.EXTRACTION_NOT_ENABLED);
        }

        [Test]
        public void NotPerformedBecauseClassificationIsUnknown()
        {
            AssertOutcome("NOT_PERFORMED", "CLASSIFICATION_UNKNOWN",
                ExtractionStatus.NOT_PERFORMED, ExtractionReasonCode.CLASSIFICATION_UNKNOWN);
        }

        [Test]
        public void NotPerformedBecauseClassifiedTypeIsUnsupported()
        {
            AssertOutcome("NOT_PERFORMED", "CLASSIFICATION_UNSUPPORTED_TYPE",
                ExtractionStatus.NOT_PERFORMED, ExtractionReasonCode.CLASSIFICATION_UNSUPPORTED_TYPE);
        }

        [Test]
        public void NotPerformedBecauseClassificationQualityWarning()
        {
            AssertOutcome("NOT_PERFORMED", "CLASSIFICATION_QUALITY_WARNING",
                ExtractionStatus.NOT_PERFORMED, ExtractionReasonCode.CLASSIFICATION_QUALITY_WARNING);
        }

        [Test]
        public void FailedBecauseExtractionErrored()
        {
            AssertOutcome("FAILED", "EXTRACTION_ERROR",
                ExtractionStatus.FAILED, ExtractionReasonCode.EXTRACTION_ERROR);
        }

        /// <summary>
        /// A status or reason code introduced by a newer server release reads as null rather than
        /// throwing or falling back to the first declared value, so an older SDK keeps working
        /// against a newer server.
        /// </summary>
        [Test]
        public void UnknownEnumValuesAreReadAsNull()
        {
            AssertOutcome("SOMETHING_NEW", "SOME_NEW_REASON", null, null);
        }

        [Test]
        public void AbsentOutcomeIsNull()
        {
            string response = "[{\"attachmentUuid\":\"attachment-uid\",\"extractionResult\":{}}]";

            AttachmentVerificationResult result = Deserialize(response)[0];

            Assert.IsNull(result.ExtractionStatus);
            Assert.IsNull(result.ReasonCode);
            Assert.IsNull(result.ExtractionResult.ExtractionStatus);
            Assert.IsNull(result.ExtractionResult.ReasonCode);
        }

        [Test]
        public void FailureMessageIsExposedAlongsideTheReasonCode()
        {
            string response = "[{" +
                "\"extractionStatus\":\"FAILED\"," +
                "\"reasonCode\":\"EXTRACTION_ERROR\"," +
                "\"extractionResult\":{" +
                    "\"extractionStatus\":\"FAILED\"," +
                    "\"reasonCode\":\"EXTRACTION_ERROR\"," +
                    "\"failureMessage\":\"Extraction timed out after 30s\"," +
                    "\"failed\":true" +
                "}}]";

            ExtractionResult extractionResult = Deserialize(response)[0].ExtractionResult;

            Assert.AreEqual(ExtractionReasonCode.EXTRACTION_ERROR, extractionResult.ReasonCode);
            Assert.AreEqual("Extraction timed out after 30s", extractionResult.FailureMessage);
        }

        [Test]
        public void OutcomeRoundTripsAsAStringNotAnOrdinal()
        {
            AttachmentVerificationResult result =
                Deserialize(Payload("NOT_PERFORMED", "CLASSIFICATION_UNKNOWN"))[0];

            string json = JsonConvert.SerializeObject(result);

            StringAssert.Contains("\"extractionStatus\":\"NOT_PERFORMED\"", json);
            StringAssert.Contains("\"reasonCode\":\"CLASSIFICATION_UNKNOWN\"", json);
        }

        private void AssertOutcome(string statusJson, string reasonCodeJson,
            ExtractionStatus? expectedStatus, ExtractionReasonCode? expectedReasonCode)
        {
            string label = statusJson + "/" + (reasonCodeJson ?? "null");
            AttachmentVerificationResult result = Deserialize(Payload(statusJson, reasonCodeJson))[0];

            Assert.AreEqual(expectedStatus, result.ExtractionStatus,
                "verification result status for " + label);
            Assert.AreEqual(expectedReasonCode, result.ReasonCode,
                "verification result reason code for " + label);

            ExtractionResult extractionResult = result.ExtractionResult;
            Assert.AreEqual(expectedStatus, extractionResult.ExtractionStatus,
                "extraction result status for " + label);
            Assert.AreEqual(expectedReasonCode, extractionResult.ReasonCode,
                "extraction result reason code for " + label);
        }

        private string Payload(string statusJson, string reasonCodeJson)
        {
            string status = statusJson == null ? "null" : "\"" + statusJson + "\"";
            string reasonCode = reasonCodeJson == null ? "null" : "\"" + reasonCodeJson + "\"";
            return "[{" +
                "\"attachmentUuid\":\"attachment-uid\"," +
                "\"extractionStatus\":" + status + "," +
                "\"reasonCode\":" + reasonCode + "," +
                "\"extractionResult\":{" +
                    "\"documentUuid\":\"verification-uuid\"," +
                    "\"extractionStatus\":" + status + "," +
                    "\"reasonCode\":" + reasonCode +
                "}}]";
        }

        private IList<AttachmentVerificationResult> Deserialize(string response)
        {
            return JsonConvert.DeserializeObject<IList<AttachmentVerificationResult>>(response);
        }
    }
}
