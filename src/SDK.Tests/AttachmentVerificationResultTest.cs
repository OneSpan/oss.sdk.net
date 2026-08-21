using System.Collections.Generic;
using Newtonsoft.Json;
using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Tests
{
    [TestFixture]
    public class AttachmentVerificationResultTest
    {
        [Test]
        public void DeserializeBackendAttachmentVerificationPayload()
        {
            IList<AttachmentVerificationResult> results = DeserializeBackendPayloadWithContent();

            AttachmentVerificationResult result = results[0];
            Assert.AreEqual("attachment-uid", result.AttachmentUuid);
            Assert.AreEqual("passport", result.FileName);
            Assert.AreEqual("42", result.FileId);
            Assert.AreEqual("pdf", result.Extension);
            Assert.IsTrue(result.TypeMatch);
            Assert.AreEqual(ExtractionStatus.COMPLETED, result.ExtractionStatus);
            Assert.IsNull(result.ReasonCode);

            AttachmentClassificationResult classificationResult = result.ClassificationResult;
            Assert.AreEqual("verification-uuid", classificationResult.DocumentUuid);
            Assert.AreEqual("PASSPORT", classificationResult.DocumentType);
            Assert.AreEqual(0.95, classificationResult.ConfidenceScore);
            Assert.AreEqual("HIGH", classificationResult.ConfidenceLevel);
            Assert.AreEqual("bedrock", classificationResult.ProviderName);
            Assert.AreEqual(false, classificationResult.Failed);

            ExtractionResult extractionResult = result.ExtractionResult;
            Assert.AreEqual("verification-uuid", extractionResult.DocumentUuid);
            Assert.AreEqual("bedrock", extractionResult.ProviderName);
            Assert.AreEqual("Jane Doe", extractionResult.ExtractedFields["fullName"]);
            Assert.AreEqual(ExtractionStatus.COMPLETED, extractionResult.ExtractionStatus);
            Assert.IsNull(extractionResult.ReasonCode);
            Assert.IsNotNull(extractionResult.VerificationCheckResults);
            Assert.AreEqual(1, extractionResult.VerificationCheckResults.Count);

            AttachmentVerificationCheckResult checkResult = extractionResult.VerificationCheckResults[0];
            Assert.AreEqual("expiry_check", checkResult.RuleName);
            CollectionAssert.AreEqual(new List<string> { "expiryDate" }, checkResult.Fields);
            Assert.AreEqual(AttachmentVerificationStatus.PASS, checkResult.Status);
            Assert.AreEqual("Document expires on 2099-01-01, still valid", checkResult.Message);
        }

        [Test]
        public void BackendContentIsIgnoredBySdkVerificationResult()
        {
            AttachmentVerificationResult result = DeserializeBackendPayloadWithContent()[0];

            string sdkResultJson = JsonConvert.SerializeObject(result);

            StringAssert.DoesNotContain("\"content\"", sdkResultJson);
            StringAssert.Contains("\"fileId\":\"42\"", sdkResultJson);
            StringAssert.Contains("\"extractionStatus\":\"COMPLETED\"", sdkResultJson);
            StringAssert.Contains("\"verificationCheckResults\"", sdkResultJson);
            StringAssert.Contains("\"status\":\"PASS\"", sdkResultJson);
        }

        [Test]
        public void UnmodelledFieldsAreIgnored()
        {
            string response = "[{" +
                "\"attachmentUuid\":\"attachment-uid\"," +
                "\"extractionStatus\":\"COMPLETED\"," +
                "\"someFutureField\":\"whatever\"," +
                "\"extractionResult\":{" +
                    "\"documentUuid\":\"verification-uuid\"," +
                    "\"extractionStatus\":\"COMPLETED\"," +
                    "\"preflight\":{\"tier\":\"BASIC\",\"checks\":[]}" +
                "}" +
            "}]";

            AttachmentVerificationResult result =
                JsonConvert.DeserializeObject<IList<AttachmentVerificationResult>>(response)[0];

            Assert.AreEqual("attachment-uid", result.AttachmentUuid);
            Assert.AreEqual(ExtractionStatus.COMPLETED, result.ExtractionStatus);
            Assert.AreEqual(ExtractionStatus.COMPLETED, result.ExtractionResult.ExtractionStatus);
        }

        [Test]
#pragma warning disable 618
        public void LegacyExtractionFailureFieldsStillDeserialize()
        {
            string response = "[{" +
                "\"attachmentUuid\":\"attachment-uid\"," +
                "\"typeMatch\":false," +
                "\"extractionFailed\":true," +
                "\"extractionErrorCode\":\"esl.error.attachment_verification.image_low_contrast\"," +
                "\"extractionResult\":{" +
                    "\"documentUuid\":\"verification-uuid\"," +
                    "\"failed\":true," +
                    "\"errorCode\":\"esl.error.attachment_verification.image_low_contrast\"" +
                "}" +
            "}]";

            AttachmentVerificationResult result =
                JsonConvert.DeserializeObject<IList<AttachmentVerificationResult>>(response)[0];

            Assert.IsTrue(result.ExtractionFailed);
            Assert.AreEqual("esl.error.attachment_verification.image_low_contrast", result.ExtractionErrorCode);
            Assert.AreEqual(true, result.ExtractionResult.Failed);
            Assert.AreEqual("esl.error.attachment_verification.image_low_contrast",
                result.ExtractionResult.ErrorCode);
            Assert.IsNull(result.ExtractionStatus, "no structured outcome is present on a legacy payload");
            Assert.IsNull(result.ReasonCode);
        }
#pragma warning restore 618

        private IList<AttachmentVerificationResult> DeserializeBackendPayloadWithContent()
        {
            string response = "[{" +
                "\"attachmentUuid\":\"attachment-uid\"," +
                "\"fileName\":\"passport\"," +
                "\"fileId\":\"42\"," +
                "\"extension\":\"pdf\"," +
                "\"content\":\"ZmlsZSBjb250ZW50\"," +
                "\"typeMatch\":true," +
                "\"extractionStatus\":\"COMPLETED\"," +
                "\"reasonCode\":null," +
                "\"classificationResult\":{" +
                    "\"documentUuid\":\"verification-uuid\"," +
                    "\"documentType\":\"PASSPORT\"," +
                    "\"confidenceScore\":0.95," +
                    "\"confidenceLevel\":\"HIGH\"," +
                    "\"providerName\":\"bedrock\"," +
                    "\"failed\":false," +
                    "\"errorCode\":null," +
                    "\"failureMessage\":null" +
                "}," +
                "\"extractionResult\":{" +
                    "\"documentUuid\":\"verification-uuid\"," +
                    "\"providerName\":\"bedrock\"," +
                    "\"extractedFields\":{\"fullName\":\"Jane Doe\",\"expiryDate\":\"2099-01-01\"}," +
                    "\"verificationCheckResults\":[{" +
                        "\"ruleName\":\"expiry_check\"," +
                        "\"fields\":[\"expiryDate\"]," +
                        "\"status\":\"PASS\"," +
                        "\"message\":\"Document expires on 2099-01-01, still valid\"" +
                    "}]," +
                    "\"extractionStatus\":\"COMPLETED\"," +
                    "\"reasonCode\":null," +
                    "\"failureMessage\":null," +
                    "\"failed\":false" +
                "}" +
            "}]";

            return JsonConvert.DeserializeObject<IList<AttachmentVerificationResult>>(response);
        }
    }
}
