using System;
using System.Collections.Generic;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using OneSpanSign.API;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Internal;
using OneSpanSign.Sdk.Models;
using OneSpanSign.Sdk.Services;

namespace SDK.Tests
{
    [TestFixture]
    public class PackageServiceTest
    {
        private const string BaseUrl = "http://baseurl";
        private const string PackageUid = "bw1FGkfWT7tf0X4r6bHUfvIblKQ=";

        private Mock<IRestClient> clientMock;
        private PackageService packageService;
        private string apiPath;
        private string postPath;

        [SetUp]
        public void Setup()
        {
            clientMock = new Mock<IRestClient>();
            packageService = new PackageService(clientMock.Object, BaseUrl, new JsonSerializerSettings());
            apiPath = BuildPackagePath(PackageUid);
            postPath = BuildLocalizeConsentPath(PackageUid);
        }

        [Test]
        public void HasLanguageChangedWhenLanguagesDifferConsentIsAttempted()
        {
            var existing = ApiPackage(PackageUid, "en");
            var updated = ApiPackage(PackageUid, "fr");
            var consentData = CreateConsentLocalizationData(PackageUid, "fr");

            clientMock.SetupSequence(c => c.Get(apiPath))
                .Returns(ToJson(existing))
                .Returns(ToJson(updated));
            clientMock.Setup(c => c.Put(apiPath, It.IsAny<string>())).Returns((string)null);
            clientMock.Setup(c => c.Post(postPath, It.IsAny<string>())).Returns(ToJson(consentData));

            var result = packageService.UpdatePackageAndLocalizeConsent(
                new PackageId(PackageUid), ApiPackage(PackageUid, "fr"));

            Assert.AreEqual(PackageUid, result.PackageUid);
            Assert.IsNotNull(result.ConsentInfo);
            Assert.AreEqual(PackageUpdateWorkflowResult.Status.SUCCESS, result.ConsentInfo.Status);
            Assert.AreEqual(ConsentLocalizationMessages.CONSENT_DOCUMENT_LOCALIZED_SUCCESSFULLY, result.ConsentInfo.Message);
            Assert.IsNotNull(result.ConsentInfo.ConsentData);
            Assert.IsNotNull(result.ConsentInfo.ConsentData.ConsentMetadata);
            Assert.AreEqual(PackageUid, result.ConsentInfo.ConsentData.ConsentMetadata.PackageInfo.Uid);
            Assert.AreEqual("fr", result.ConsentInfo.ConsentData.ConsentMetadata.PackageInfo.Language);
            Assert.AreEqual("fr", result.ConsentInfo.ConsentData.ConsentMetadata.Properties.Language);
            Assert.AreEqual("fr", result.ConsentInfo.ConsentData.ConsentMetadata.Document.Language);

            clientMock.Verify(c => c.Post(postPath, It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void HasLanguageChangedWhenLanguagesSameConsentIsSkipped()
        {
            var package = ApiPackage(PackageUid, "fr");

            clientMock.SetupSequence(c => c.Get(apiPath))
                .Returns(ToJson(package))
                .Returns(ToJson(package));
            clientMock.Setup(c => c.Put(apiPath, It.IsAny<string>())).Returns((string)null);

            var result = packageService.UpdatePackageAndLocalizeConsent(
                new PackageId(PackageUid), ApiPackage(PackageUid, "fr"));

            Assert.AreEqual(PackageUpdateWorkflowResult.Status.SKIPPED, result.ConsentInfo.Status);
            Assert.AreEqual(ConsentLocalizationMessages.LANGUAGE_NOT_CHANGED, result.ConsentInfo.Message);
            clientMock.Verify(c => c.Post(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void HasLanguageChangedWhenCurrentLanguageIsNullConsentIsSkipped()
        {
            var existing = ApiPackage(PackageUid, "en");
            var updated = ApiPackage(PackageUid, null);

            clientMock.SetupSequence(c => c.Get(apiPath))
                .Returns(ToJson(existing))
                .Returns(ToJson(updated));
            clientMock.Setup(c => c.Put(apiPath, It.IsAny<string>())).Returns((string)null);

            var result = packageService.UpdatePackageAndLocalizeConsent(
                new PackageId(PackageUid), ApiPackage(PackageUid, null));

            Assert.AreEqual(PackageUpdateWorkflowResult.Status.SKIPPED, result.ConsentInfo.Status);
            Assert.AreEqual(ConsentLocalizationMessages.LANGUAGE_NOT_CHANGED, result.ConsentInfo.Message);
            clientMock.Verify(c => c.Post(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void TryGetPackageWhenPackageExistsConsentProceedsWithCorrectData()
        {
            var existing = ApiPackage(PackageUid, "en");
            var updated = ApiPackage(PackageUid, "fr");
            var consentData = CreateConsentLocalizationData(PackageUid, "fr");

            clientMock.SetupSequence(c => c.Get(apiPath))
                .Returns(ToJson(existing))
                .Returns(ToJson(updated));
            clientMock.Setup(c => c.Put(apiPath, It.IsAny<string>())).Returns((string)null);
            clientMock.Setup(c => c.Post(postPath, It.IsAny<string>())).Returns(ToJson(consentData));

            var result = packageService.UpdatePackageAndLocalizeConsent(
                new PackageId(PackageUid), ApiPackage(PackageUid, "fr"));

            Assert.AreEqual(PackageUid, result.PackageUid);
            Assert.IsNotNull(result.ConsentInfo);
            Assert.AreEqual(PackageUpdateWorkflowResult.Status.SUCCESS, result.ConsentInfo.Status);
            Assert.AreEqual(ConsentLocalizationMessages.CONSENT_DOCUMENT_LOCALIZED_SUCCESSFULLY, result.ConsentInfo.Message);
            Assert.IsNotNull(result.ConsentInfo.ConsentData);
            Assert.IsNotNull(result.ConsentInfo.ConsentData.ConsentMetadata);
            Assert.AreEqual(PackageUid, result.ConsentInfo.ConsentData.ConsentMetadata.PackageInfo.Uid);
            Assert.AreEqual("fr", result.ConsentInfo.ConsentData.ConsentMetadata.PackageInfo.Language);
            Assert.AreEqual("fr", result.ConsentInfo.ConsentData.ConsentMetadata.Properties.Language);
            Assert.AreEqual("fr", result.ConsentInfo.ConsentData.ConsentMetadata.Document.Language);
        }

        [Test]
        public void TryGetPackageWhenExceptionThrownReturnsNullAndConsentProceeds()
        {
            var updated = ApiPackage(PackageUid, "fr");
            var consentData = CreateConsentLocalizationData(PackageUid, "fr");

            clientMock.SetupSequence(c => c.Get(apiPath))
                .Throws(new Exception("failed to get existing package"))
                .Returns(ToJson(updated));
            clientMock.Setup(c => c.Put(apiPath, It.IsAny<string>())).Returns((string)null);
            clientMock.Setup(c => c.Post(postPath, It.IsAny<string>())).Returns(ToJson(consentData));

            var result = packageService.UpdatePackageAndLocalizeConsent(
                new PackageId(PackageUid), ApiPackage(PackageUid, "fr"));

            Assert.AreEqual(PackageUpdateWorkflowResult.Status.SUCCESS, result.ConsentInfo.Status);
            Assert.AreEqual(ConsentLocalizationMessages.CONSENT_DOCUMENT_LOCALIZED_SUCCESSFULLY, result.ConsentInfo.Message);
            Assert.IsNotNull(result.ConsentInfo.ConsentData);
            Assert.IsNotNull(result.ConsentInfo.ConsentData.ConsentMetadata);
            Assert.AreEqual(PackageUid, result.ConsentInfo.ConsentData.ConsentMetadata.PackageInfo.Uid);
            Assert.AreEqual("fr", result.ConsentInfo.ConsentData.ConsentMetadata.PackageInfo.Language);
            Assert.AreEqual("fr", result.ConsentInfo.ConsentData.ConsentMetadata.Properties.Language);
            Assert.AreEqual("fr", result.ConsentInfo.ConsentData.ConsentMetadata.Document.Language);
        }

        [Test]
        public void UpdatePackageAndLocalizeConsentWhenLanguageChangedConsentStatusIsSuccess()
        {
            var existing = ApiPackage(PackageUid, "en");
            var updated = ApiPackage(PackageUid, "fr");
            var consentData = CreateConsentLocalizationData(PackageUid, "fr");

            clientMock.SetupSequence(c => c.Get(apiPath))
                .Returns(ToJson(existing))
                .Returns(ToJson(updated));
            clientMock.Setup(c => c.Put(apiPath, It.IsAny<string>())).Returns((string)null);
            clientMock.Setup(c => c.Post(postPath, It.IsAny<string>())).Returns(ToJson(consentData));

            var result = packageService.UpdatePackageAndLocalizeConsent(
                new PackageId(PackageUid), ApiPackage(PackageUid, "fr"));

            clientMock.Verify(c => c.Get(apiPath), Times.Exactly(2));
            clientMock.Verify(c => c.Put(apiPath, It.IsAny<string>()), Times.Once);
            clientMock.Verify(c => c.Post(postPath, It.IsAny<string>()), Times.Once);

            Assert.AreEqual(PackageUid, result.PackageUid);
            Assert.AreEqual(PackageUpdateWorkflowResult.Status.SUCCESS, result.ConsentInfo.Status);
            Assert.AreEqual(PackageUid, result.ConsentInfo.ConsentData.ConsentMetadata.PackageInfo.Uid);
            Assert.AreEqual("fr", result.ConsentInfo.ConsentData.ConsentMetadata.PackageInfo.Language);
            Assert.AreEqual("fr", result.ConsentInfo.ConsentData.ConsentMetadata.Properties.Language);
            Assert.AreEqual("fr", result.ConsentInfo.ConsentData.ConsentMetadata.Document.Language);
        }

        [Test]
        public void UpdatePackageAndLocalizeConsentWhenOldPackageGetFailsConsentStatusIsSuccess()
        {
            var updated = ApiPackage(PackageUid, "fr");
            var consentData = CreateConsentLocalizationData(PackageUid, "fr");

            clientMock.SetupSequence(c => c.Get(apiPath))
                .Throws(new Exception("failed to get existing package"))
                .Returns(ToJson(updated));
            clientMock.Setup(c => c.Put(apiPath, It.IsAny<string>())).Returns((string)null);
            clientMock.Setup(c => c.Post(postPath, It.IsAny<string>())).Returns(ToJson(consentData));

            var result = packageService.UpdatePackageAndLocalizeConsent(
                new PackageId(PackageUid), ApiPackage(PackageUid, "fr"));

            clientMock.Verify(c => c.Get(apiPath), Times.Exactly(2));
            clientMock.Verify(c => c.Post(postPath, It.IsAny<string>()), Times.Once);

            Assert.AreEqual(PackageUid, result.PackageUid);
            Assert.IsNotNull(result.ConsentInfo);
            Assert.AreEqual(PackageUpdateWorkflowResult.Status.SUCCESS, result.ConsentInfo.Status);
            Assert.AreEqual(ConsentLocalizationMessages.CONSENT_DOCUMENT_LOCALIZED_SUCCESSFULLY, result.ConsentInfo.Message);
            Assert.IsNotNull(result.ConsentInfo.ConsentData);
            Assert.IsNotNull(result.ConsentInfo.ConsentData.ConsentMetadata);
            Assert.AreEqual(PackageUid, result.ConsentInfo.ConsentData.ConsentMetadata.PackageInfo.Uid);
            Assert.AreEqual("fr", result.ConsentInfo.ConsentData.ConsentMetadata.PackageInfo.Language);
            Assert.AreEqual("fr", result.ConsentInfo.ConsentData.ConsentMetadata.Properties.Language);
            Assert.AreEqual("fr", result.ConsentInfo.ConsentData.ConsentMetadata.Document.Language);
        }

        [Test]
        public void UpdatePackageAndLocalizeConsentWhenUpdatedPackageGetFailsConsentIsSkipped()
        {
            var existing = ApiPackage(PackageUid, "en");

            clientMock.SetupSequence(c => c.Get(apiPath))
                .Returns(ToJson(existing))
                .Throws(new Exception("failed to get updated package"));
            clientMock.Setup(c => c.Put(apiPath, It.IsAny<string>())).Returns((string)null);

            var result = packageService.UpdatePackageAndLocalizeConsent(
                new PackageId(PackageUid), ApiPackage(PackageUid, "fr"));

            clientMock.Verify(c => c.Post(It.IsAny<string>(), It.IsAny<string>()), Times.Never);

            Assert.AreEqual(PackageUid, result.PackageUid);
            Assert.IsNotNull(result.ConsentInfo);
            Assert.AreEqual(PackageUpdateWorkflowResult.Status.SKIPPED, result.ConsentInfo.Status);
            Assert.AreEqual(ConsentLocalizationMessages.UPDATED_PACKAGE_NOT_AVAILABLE, result.ConsentInfo.Message);
            Assert.IsNull(result.ConsentInfo.ConsentData);
        }

        [Test]
        public void UpdatePackageAndLocalizeConsentWhenLanguageNotChangedConsentIsSkipped()
        {
            var package = ApiPackage(PackageUid, "fr");

            clientMock.SetupSequence(c => c.Get(apiPath))
                .Returns(ToJson(package))
                .Returns(ToJson(package));
            clientMock.Setup(c => c.Put(apiPath, It.IsAny<string>())).Returns((string)null);

            var result = packageService.UpdatePackageAndLocalizeConsent(
                new PackageId(PackageUid), ApiPackage(PackageUid, "fr"));

            clientMock.Verify(c => c.Post(It.IsAny<string>(), It.IsAny<string>()), Times.Never);

            Assert.AreEqual(PackageUid, result.PackageUid);
            Assert.IsNotNull(result.ConsentInfo);
            Assert.AreEqual(PackageUpdateWorkflowResult.Status.SKIPPED, result.ConsentInfo.Status);
            Assert.AreEqual(ConsentLocalizationMessages.LANGUAGE_NOT_CHANGED, result.ConsentInfo.Message);
            Assert.IsNull(result.ConsentInfo.ConsentData);
        }

        [Test]
        public void UpdatePackageAndLocalizeConsentWhenPackageUpdateFailsThrowsOssException()
        {
            clientMock.Setup(c => c.Get(apiPath)).Returns(ToJson(ApiPackage(PackageUid, "fr")));
            clientMock.Setup(c => c.Put(apiPath, It.IsAny<string>()))
                .Throws(new Exception("failed to update package"));

            Assert.Throws<OssException>(() =>
                packageService.UpdatePackageAndLocalizeConsent(
                    new PackageId(PackageUid), ApiPackage(PackageUid, "fr")));
        }

        [Test]
        public void UpdatePackageAndLocalizeConsentWhenPackageUpdateFailsWithServerErrorThrowsOssServerException()
        {
            var serverError = new ServerError { Code = 400, Message = "validation error" };

            clientMock.Setup(c => c.Get(apiPath)).Returns(ToJson(ApiPackage(PackageUid, "fr")));
            clientMock.Setup(c => c.Put(apiPath, It.IsAny<string>()))
                .Throws(new OssServerException("server error", serverError, null));

            Assert.Throws<OssServerException>(() =>
                packageService.UpdatePackageAndLocalizeConsent(
                    new PackageId(PackageUid), ApiPackage(PackageUid, "fr")));
        }

        [Test]
        public void UpdatePackageAndLocalizeConsentWhenConsentAlreadyAcceptedConsentIsSkipped()
        {
            var existing = ApiPackageWithDefaultConsent(PackageUid, "en", isAccepted: true);
            var updated = ApiPackageWithDefaultConsent(PackageUid, "fr", isAccepted: true);

            clientMock.SetupSequence(c => c.Get(apiPath))
                .Returns(ToJson(existing))
                .Returns(ToJson(updated));
            clientMock.Setup(c => c.Put(apiPath, It.IsAny<string>())).Returns((string)null);

            var result = packageService.UpdatePackageAndLocalizeConsent(
                new PackageId(PackageUid), updated);

            clientMock.Verify(c => c.Post(It.IsAny<string>(), It.IsAny<string>()), Times.Never);

            Assert.AreEqual(PackageUid, result.PackageUid);
            Assert.AreEqual(PackageUpdateWorkflowResult.Status.SKIPPED, result.ConsentInfo.Status);
            Assert.AreEqual(ConsentLocalizationMessages.DEFAULT_DOCUMENT_CONSENT_ACCEPTED, result.ConsentInfo.Message);
            Assert.IsNull(result.ConsentInfo.ConsentData);
        }

        [Test]
        public void UpdatePackageAndLocalizeConsentWhenDefaultConsentNotExistConsentStatusIsFailure()
        {
            var errorMessage = "The default document consent does not exist.";
            var serverError = new ServerError { Code = 422, Message = errorMessage };

            clientMock.SetupSequence(c => c.Get(apiPath))
                .Returns(ToJson(ApiPackage(PackageUid, "en")))
                .Returns(ToJson(ApiPackage(PackageUid, "fr")));
            clientMock.Setup(c => c.Put(apiPath, It.IsAny<string>())).Returns((string)null);
            clientMock.Setup(c => c.Post(postPath, It.IsAny<string>()))
                .Throws(new OssServerException("server error", serverError, null));

            var result = packageService.UpdatePackageAndLocalizeConsent(
                new PackageId(PackageUid), ApiPackage(PackageUid, "fr"));

            Assert.AreEqual(PackageUid, result.PackageUid);
            Assert.AreEqual(PackageUpdateWorkflowResult.Status.FAILURE, result.ConsentInfo.Status);
            Assert.AreEqual(
                ConsentLocalizationMessages.FAILED_TO_LOCALIZE_DEFAULT_CONSENT_PREFIX + errorMessage,
                result.ConsentInfo.Message);
            Assert.IsNull(result.ConsentInfo.ConsentData);
        }

        [Test]
        public void UpdatePackageAndLocalizeConsentWhenConsentConfigNotFoundConsentStatusIsFailure()
        {
            var errorMessage = "Default document consent modification is not allowed: no localized consent template configuration found.";
            var serverError = new ServerError { Code = 422, Message = errorMessage };

            clientMock.SetupSequence(c => c.Get(apiPath))
                .Returns(ToJson(ApiPackage(PackageUid, "en")))
                .Returns(ToJson(ApiPackage(PackageUid, "fr")));
            clientMock.Setup(c => c.Put(apiPath, It.IsAny<string>())).Returns((string)null);
            clientMock.Setup(c => c.Post(postPath, It.IsAny<string>()))
                .Throws(new OssServerException("server error", serverError, null));

            var result = packageService.UpdatePackageAndLocalizeConsent(
                new PackageId(PackageUid), ApiPackage(PackageUid, "fr"));

            Assert.AreEqual(PackageUid, result.PackageUid);
            Assert.AreEqual(PackageUpdateWorkflowResult.Status.FAILURE, result.ConsentInfo.Status);
            Assert.AreEqual(
                ConsentLocalizationMessages.FAILED_TO_LOCALIZE_DEFAULT_CONSENT_PREFIX + errorMessage,
                result.ConsentInfo.Message);
            Assert.IsNull(result.ConsentInfo.ConsentData);
        }

        private static string BuildPackagePath(string uid) =>
            new UrlTemplate(BaseUrl).UrlFor(UrlTemplate.PACKAGE_ID_PATH)
                .Replace("{packageId}", uid).Build();

        private static string BuildLocalizeConsentPath(string uid) =>
            new UrlTemplate(BaseUrl).UrlFor(UrlTemplate.LOCALIZE_CONSENT_PATH)
                .Replace("{packageId}", uid).Build();

        private static Package ApiPackage(string uid, string language) =>
            new Package { Id = uid, Language = language };

        private static Package ApiPackageWithDefaultConsent(string uid, string language, bool isAccepted)
        {
            var package = ApiPackage(uid, language);
            var doc = new OneSpanSign.API.Document { Id = DocumentAcceptanceUtil.DEFAULT_CONSENT_ID };
            if (isAccepted)
            {
                doc.Data = new Dictionary<string, object>
                {
                    { DocumentAcceptanceUtil.DOC_DATA_ACCEPTED_BY_SIGNERS, "56320d1e-4c71-4b6e-86f4-0d5c7d07d069" }
                };
            }
            package.AddDocument(doc);
            return package;
        }

        private static ConsentLocalizationData CreateConsentLocalizationData(string uid, string language) =>
            new ConsentLocalizationData
            {
                ConsentId = "default-consent",
                ConsentMetadata = new ConsentLocalizationData.ConsentMetadataDetails
                {
                    PackageInfo = new ConsentLocalizationData.PackageInfo { Uid = uid, Language = language },
                    Properties = new ConsentLocalizationData.ResourceMetadata { AccountId = "sample-account-id", Language = language },
                    Document = new ConsentLocalizationData.ResourceMetadata { AccountId = "sample-account-id", Language = language }
                }
            };

        private static string ToJson(object obj) => JsonConvert.SerializeObject(obj);
    }
}
