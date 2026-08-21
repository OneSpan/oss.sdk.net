using System;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using OneSpanSign.API;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Internal;

namespace SDK.Tests.Services
{
    [TestFixture]
    public class AttachmentRequirementServiceTest
    {
        private const string BaseUrl = "http://baseurl";
        private const string PackageUidValue = "package-id";

        private Mock<IRestClient> clientMock;
        private AttachmentRequirementService service;
        private string verificationResultsPath;

        [SetUp]
        public void Setup()
        {
            clientMock = new Mock<IRestClient>();
            service = new AttachmentRequirementService(clientMock.Object, BaseUrl, new JsonSerializerSettings());
            verificationResultsPath = new UrlTemplate(BaseUrl)
                .UrlFor(UrlTemplate.ATTACHMENT_VERIFICATION_RESULTS_PATH)
                .Replace("{packageId}", PackageUidValue)
                .Build();
        }

        [Test]
        public void GetAttachmentVerificationResultsUsesExpectedPathAndReturnsResults()
        {
            clientMock.Setup(c => c.Get(verificationResultsPath))
                .Returns("[{\"attachmentUuid\":\"attachment-uid\",\"fileName\":\"passport\"}]");

            var results = service.GetAttachmentVerificationResults(new PackageId(PackageUidValue));

            clientMock.Verify(c => c.Get(verificationResultsPath), Times.Once);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("attachment-uid", results[0].AttachmentUuid);
            Assert.AreEqual("passport", results[0].FileName);
        }

        [Test]
        public void GetAttachmentVerificationResultsParsesStructuredExtractionOutcome()
        {
            JsonSerializerSettings productionSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc
            };
            productionSettings.Converters.Add(new CultureInfoJsonCreationConverter());
            AttachmentRequirementService serviceWithProductionSettings =
                new AttachmentRequirementService(clientMock.Object, BaseUrl, productionSettings);

            clientMock.Setup(c => c.Get(verificationResultsPath)).Returns("[{" +
                "\"attachmentUuid\":\"attachment-uid\"," +
                "\"extractionStatus\":\"NOT_PERFORMED\"," +
                "\"reasonCode\":\"CLASSIFICATION_UNKNOWN\"," +
                "\"extractionResult\":{" +
                    "\"extractionStatus\":\"NOT_PERFORMED\"," +
                    "\"reasonCode\":\"CLASSIFICATION_UNKNOWN\"" +
                "}}]");

            var results = serviceWithProductionSettings
                .GetAttachmentVerificationResults(new PackageId(PackageUidValue));

            Assert.AreEqual(ExtractionStatus.NOT_PERFORMED, results[0].ExtractionStatus);
            Assert.AreEqual(ExtractionReasonCode.CLASSIFICATION_UNKNOWN, results[0].ReasonCode);
            Assert.AreEqual(ExtractionStatus.NOT_PERFORMED, results[0].ExtractionResult.ExtractionStatus);
            Assert.AreEqual(ExtractionReasonCode.CLASSIFICATION_UNKNOWN, results[0].ExtractionResult.ReasonCode);
        }

        [Test]
        public void GetAttachmentVerificationResultsReturnsEmptyListWhenResponseIsNull()
        {
            clientMock.Setup(c => c.Get(verificationResultsPath)).Returns((string)null);

            var results = service.GetAttachmentVerificationResults(new PackageId(PackageUidValue));

            clientMock.Verify(c => c.Get(verificationResultsPath), Times.Once);
            Assert.IsNotNull(results);
            Assert.AreEqual(0, results.Count);
        }

        [Test]
        public void GetAttachmentVerificationResultsReturnsEmptyListWhenResponseIsEmpty()
        {
            clientMock.Setup(c => c.Get(verificationResultsPath)).Returns("");

            var results = service.GetAttachmentVerificationResults(new PackageId(PackageUidValue));

            clientMock.Verify(c => c.Get(verificationResultsPath), Times.Once);
            Assert.IsNotNull(results);
            Assert.AreEqual(0, results.Count);
        }

        [Test]
        public void GetAttachmentVerificationResultsPreservesServerErrorWhenServerExceptionIsThrown()
        {
            ServerError serverError = new ServerError { MessageKey = "error.key", Message = "Something failed" };
            clientMock.Setup(c => c.Get(verificationResultsPath))
                .Throws(new OssServerException("upstream failure", serverError, null));

            var ex = Assert.Throws<OssServerException>(
                () => service.GetAttachmentVerificationResults(new PackageId(PackageUidValue)));
            Assert.IsNotNull(ex.ServerError);
            Assert.AreEqual("error.key", ex.ServerError.MessageKey);
        }

        [Test]
        public void GetAttachmentVerificationResultsWrapsMalformedResponseAsOssException()
        {
            clientMock.Setup(c => c.Get(verificationResultsPath)).Returns("{not-json");

            Assert.Throws<OssException>(
                () => service.GetAttachmentVerificationResults(new PackageId(PackageUidValue)));
        }
    }
}
