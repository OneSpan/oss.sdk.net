using System;
using System.Collections.Generic;
using System.Reflection;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Services;
using static OneSpanSign.Sdk.Builder.CallbackConfigurationRequestBuilder;
using static OneSpanSign.Sdk.Builder.CallbackOAuthRequestBuilder;

namespace SDK.Tests.Services
{
    [TestFixture]
    public class CallbackConfigurationServiceTest
    {
        private const string BaseUrl = "http://baseurl";
        private const string Callbacks = BaseUrl + "/notifications/v1/callback-configurations";
        private const string AccountCallbackJson = "{\"id\":182,\"type\":\"ACCOUNT\",\"url\":\"https://example.com/cb\","
            + "\"authType\":\"OAUTH2_AUTH\",\"events\":[\"PACKAGE_COMPLETE\",\"PACKAGE_READY_FOR_COMPLETE\"],"
            + "\"customHeaderNames\":[\"X-Customer\"],\"mtlsEnabled\":true,\"enabled\":true,"
            + "\"oauth\":{\"grantType\":\"CLIENT_CREDENTIALS\",\"authMechanism\":\"HEADER\","
            + "\"authorizationServerUrl\":\"https://auth.example.com/token\",\"clientId\":\"cid\",\"scope\":null,"
            + "\"ignoreSsl\":false,\"customParameterNames\":[\"audience\"]}}";
        private const string ConnectorCallbackJson = "{\"id\":1217,\"type\":\"CONNECTOR\",\"url\":\"https://example.com/dyn\","
            + "\"authType\":\"BASIC_AUTH\",\"events\":[\"PACKAGE_CREATE\"],\"customHeaderNames\":[],"
            + "\"mtlsEnabled\":false,\"enabled\":false,\"oauth\":null}";

        private Mock<IRestClient> clientMock;
        private CallbackConfigurationService service;
        private string sentBody;

        [SetUp]
        public void Setup()
        {
            clientMock = new Mock<IRestClient>();
            service = new CallbackConfigurationService(clientMock.Object, BaseUrl, new JsonSerializerSettings());
            sentBody = null;
        }

        [Test]
        public void ListsEveryCallbackWithItsType()
        {
            clientMock.Setup(c => c.Get(Callbacks)).Returns("[" + AccountCallbackJson + "," + ConnectorCallbackJson + "]");

            List<CallbackConfiguration> callbacks = service.GetCallbackConfigurations();

            Assert.AreEqual(2, callbacks.Count);
            Assert.AreEqual("ACCOUNT", callbacks[0].Type);
            Assert.AreEqual("CONNECTOR", callbacks[1].Type);
            Assert.IsNull(callbacks[1].OAuth);
            Assert.IsFalse(callbacks[1].Enabled);
        }

        [Test]
        public void GetsOneCallbackWithEveryPropertyButTheSecrets()
        {
            clientMock.Setup(c => c.Get(Callbacks + "/182")).Returns(AccountCallbackJson);

            CallbackConfiguration callback = service.GetCallbackConfiguration(182);

            Assert.AreEqual(182L, callback.Id);
            Assert.AreEqual("https://example.com/cb", callback.Url);
            Assert.AreEqual("OAUTH2_AUTH", callback.AuthType);
            CollectionAssert.AreEqual(
                new[] { NotificationEvent.PACKAGE_COMPLETE, NotificationEvent.PACKAGE_READY_FOR_COMPLETION },
                callback.Events);
            CollectionAssert.AreEqual(new[] { "X-Customer" }, callback.CustomHeaderNames);
            Assert.IsTrue(callback.MtlsEnabled);
            Assert.IsTrue(callback.Enabled);
            CallbackOAuthSettings oauth = callback.OAuth;
            Assert.AreEqual("CLIENT_CREDENTIALS", oauth.GrantType);
            Assert.AreEqual("HEADER", oauth.AuthMechanism);
            Assert.AreEqual("https://auth.example.com/token", oauth.AuthorizationServerUrl);
            Assert.AreEqual("cid", oauth.ClientId);
            Assert.IsNull(oauth.Scope);
            Assert.IsFalse(oauth.IgnoreSsl);
            CollectionAssert.AreEqual(new[] { "audience" }, oauth.CustomParameterNames);
        }

        [Test]
        public void CreatesTheAccountCallbackWithEveryPropertySet()
        {
            clientMock.Setup(c => c.Post(Callbacks, It.IsAny<string>()))
                .Callback<string, string>((path, body) => sentBody = body)
                .Returns(AccountCallbackJson);

            CallbackConfiguration created = service.CreateCallbackConfiguration(NewCallbackConfigurationRequest()
                .WithUrl("https://example.com/cb")
                .WithAuthType("OAUTH2_AUTH")
                .WithEvents("PACKAGE_COMPLETE", "PACKAGE_CREATE")
                .WithCustomHeader("X-Customer", "header-secret")
                .WithMtlsEnabled(true)
                .WithOAuth(NewCallbackOAuthRequest()
                    .WithGrantType("CLIENT_CREDENTIALS")
                    .WithAuthMechanism("HEADER")
                    .WithAuthorizationServerUrl("https://auth.example.com/token")
                    .WithClientId("cid")
                    .WithClientSecret("client-secret")
                    .WithScope("read")
                    .WithIgnoreSsl(true)
                    .WithCustomParameter("audience", "aud"))
                .Build());

            AssertJson("{\"url\":\"https://example.com/cb\","
                + "\"authType\":\"OAUTH2_AUTH\",\"events\":[\"PACKAGE_COMPLETE\",\"PACKAGE_CREATE\"],"
                + "\"customHeaders\":[{\"name\":\"X-Customer\",\"value\":\"header-secret\"}],\"mtlsEnabled\":true,"
                + "\"oauth\":{\"grantType\":\"CLIENT_CREDENTIALS\",\"authMechanism\":\"HEADER\","
                + "\"authorizationServerUrl\":\"https://auth.example.com/token\",\"clientId\":\"cid\","
                + "\"clientSecret\":\"client-secret\",\"scope\":\"read\",\"ignoreSsl\":true,"
                + "\"customParameters\":[{\"name\":\"audience\",\"value\":\"aud\"}]}}", sentBody);
            Assert.AreEqual(182L, created.Id);
        }

        [Test]
        public void ReplacesWithTheSecretsLeftOutSoTheSavedOnesAreKept()
        {
            clientMock.Setup(c => c.Put(Callbacks + "/182", It.IsAny<string>()))
                .Callback<string, string>((path, body) => sentBody = body)
                .Returns(AccountCallbackJson);

            service.ReplaceCallbackConfiguration(182, NewCallbackConfigurationRequest()
                .WithUrl("https://example.com/cb")
                .WithAuthType("OAUTH2_AUTH")
                .WithEvents("PACKAGE_CREATE")
                .WithCustomHeader("X-Customer", null)
                .WithOAuth(NewCallbackOAuthRequest()
                    .WithGrantType("CLIENT_CREDENTIALS")
                    .WithAuthorizationServerUrl("https://auth.example.com/token")
                    .WithClientId("cid")
                    .WithCustomParameter("audience", null))
                .Build());

            AssertJson("{\"url\":\"https://example.com/cb\","
                + "\"authType\":\"OAUTH2_AUTH\",\"events\":[\"PACKAGE_CREATE\"],\"customHeaders\":[{\"name\":\"X-Customer\"}],"
                + "\"oauth\":{\"grantType\":\"CLIENT_CREDENTIALS\",\"authorizationServerUrl\":\"https://auth.example.com/token\","
                + "\"clientId\":\"cid\",\"customParameters\":[{\"name\":\"audience\"}]}}", sentBody);
        }

        [Test]
        public void DisablesWithAPatchAndReturnsTheCallback()
        {
            clientMock.Setup(c => c.Patch(Callbacks + "/1217", It.IsAny<string>()))
                .Callback<string, string>((path, body) => sentBody = body)
                .Returns(ConnectorCallbackJson);

            CallbackConfiguration disabled = service.DisableCallbackConfiguration(1217);

            AssertJson("{\"enabled\":false}", sentBody);
            Assert.IsFalse(disabled.Enabled);
        }

        [Test]
        public void EnablesWithAPatch()
        {
            clientMock.Setup(c => c.Patch(Callbacks + "/1217", It.IsAny<string>()))
                .Callback<string, string>((path, body) => sentBody = body)
                .Returns(ConnectorCallbackJson);

            service.EnableCallbackConfiguration(1217);

            AssertJson("{\"enabled\":true}", sentBody);
        }

        [Test]
        public void DeletesTheCallback()
        {
            service.DeleteCallbackConfiguration(182);

            clientMock.Verify(c => c.Delete(Callbacks + "/182"), Times.Once);
        }

        [Test]
        public void TestsASavedCallbackSendingOnlyWhatChanges()
        {
            clientMock.Setup(c => c.Post(Callbacks + "/test", It.IsAny<string>()))
                .Callback<string, string>((path, body) => sentBody = body)
                .Returns("{\"outcome\":\"HTTP_4XX\",\"url\":\"https://example.com/new\",\"sentPayload\":\"{}\","
                    + "\"requestHeaders\":{\"Authorization\":\"Basic ***\"},\"durationMs\":42,"
                    + "\"response\":{\"status\":401,\"headers\":{\"www-authenticate\":\"Basic\"}},"
                    + "\"failureDetail\":\"401 Unauthorized\"}");

            CallbackTestResult result = service.TestCallbackConfiguration(CallbackTestRequest.ForSavedCallback(182,
                NewCallbackConfigurationRequest().WithUrl("https://example.com/new").Build()));

            AssertJson("{\"callbackId\":182,\"url\":\"https://example.com/new\"}", sentBody);
            Assert.AreEqual("HTTP_4XX", result.Outcome);
            Assert.AreEqual("https://example.com/new", result.Url);
            Assert.AreEqual("{}", result.SentPayload);
            Assert.AreEqual("Basic ***", result.RequestHeaders["Authorization"]);
            Assert.AreEqual(42L, result.DurationMs);
            Assert.AreEqual(401, result.Response.Status);
            Assert.AreEqual("Basic", result.Response.Headers["www-authenticate"]);
            Assert.AreEqual("401 Unauthorized", result.FailureDetail);
        }

        [Test]
        public void TestsAnUnsavedConfigurationWithoutACallbackId()
        {
            clientMock.Setup(c => c.Post(Callbacks + "/test", It.IsAny<string>()))
                .Callback<string, string>((path, body) => sentBody = body)
                .Returns("{\"outcome\":\"CONNECT_REFUSED\",\"url\":\"https://example.com/cb\",\"sentPayload\":\"{}\","
                    + "\"requestHeaders\":{},\"durationMs\":3}");

            CallbackTestResult result = service.TestCallbackConfiguration(CallbackTestRequest.ForConfiguration(
                NewCallbackConfigurationRequest().WithUrl("https://example.com/cb").WithAuthType("NO_AUTH").Build()));

            AssertJson("{\"url\":\"https://example.com/cb\",\"authType\":\"NO_AUTH\"}", sentBody);
            Assert.AreEqual("CONNECT_REFUSED", result.Outcome);
            Assert.IsNull(result.Response);
        }

        [Test]
        public void TestsASavedCallbackAsItIs()
        {
            clientMock.Setup(c => c.Post(Callbacks + "/test", It.IsAny<string>()))
                .Callback<string, string>((path, body) => sentBody = body)
                .Returns("{\"outcome\":\"SUCCESS\",\"url\":\"https://example.com/cb\",\"sentPayload\":\"{}\","
                    + "\"requestHeaders\":{},\"durationMs\":5}");

            service.TestCallbackConfiguration(CallbackTestRequest.ForSavedCallback(182));

            AssertJson("{\"callbackId\":182}", sentBody);
        }

        [Test]
        public void GetsTheClientCertificateIntermediate()
        {
            clientMock.Setup(c => c.Get(BaseUrl + "/notifications/v1/client-certificate/intermediate")).Returns(
                "{\"chainPem\":\"-----BEGIN CERTIFICATE-----\",\"subject\":\"CN=OneSpan\",\"issuer\":\"CN=Root\","
                + "\"serialNumber\":\"3A:7B\",\"sha256Fingerprint\":\"AB:CD\",\"notBefore\":\"2026-01-01T00:00:00Z\","
                + "\"notAfter\":\"2027-01-01T00:00:00Z\",\"keyAlgorithm\":\"EC P-256\","
                + "\"signatureAlgorithm\":\"SHA256withECDSA\",\"status\":\"VALID\"}");

            ClientCertificateIntermediate certificate = service.GetClientCertificateIntermediate();

            Assert.AreEqual("-----BEGIN CERTIFICATE-----", certificate.ChainPem);
            Assert.AreEqual("CN=OneSpan", certificate.Subject);
            Assert.AreEqual("CN=Root", certificate.Issuer);
            Assert.AreEqual("3A:7B", certificate.SerialNumber);
            Assert.AreEqual("AB:CD", certificate.Sha256Fingerprint);
            Assert.AreEqual(new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), certificate.NotBefore.Value.ToUniversalTime());
            Assert.AreEqual(new DateTime(2027, 1, 1, 0, 0, 0, DateTimeKind.Utc), certificate.NotAfter.Value.ToUniversalTime());
            Assert.AreEqual("EC P-256", certificate.KeyAlgorithm);
            Assert.AreEqual("SHA256withECDSA", certificate.SignatureAlgorithm);
            Assert.AreEqual("VALID", certificate.Status);
        }

        [Test]
        public void TheReadModelHasNoSecretProperty()
        {
            foreach (Type type in new[] { typeof(CallbackConfiguration), typeof(CallbackOAuthSettings) })
            {
                foreach (PropertyInfo property in type.GetProperties())
                {
                    CollectionAssert.DoesNotContain(new[] { "Key", "ClientSecret", "CustomHeaders", "CustomParameters" },
                        property.Name, type.Name + "." + property.Name);
                }
            }
        }

        [Test]
        public void UnknownPropertiesAndValuesDoNotBreakReading()
        {
            clientMock.Setup(c => c.Get(Callbacks + "/9")).Returns("{\"id\":9,\"type\":\"SOMETHING_NEW\","
                + "\"connector\":\"salesforceOauth2\",\"addedLater\":{\"x\":1},\"url\":\"https://example.com/cb\","
                + "\"authType\":\"NEW_AUTH\",\"events\":[\"NEW_EVENT\"],\"mtlsEnabled\":false,\"enabled\":true,"
                + "\"oauth\":{\"grantType\":\"NEW_GRANT\",\"authMechanism\":\"NEW_MECHANISM\",\"addedLater\":true}}");
            clientMock.Setup(c => c.Post(Callbacks + "/test", It.IsAny<string>())).Returns(
                "{\"outcome\":\"NEW_OUTCOME\",\"addedLater\":1,\"url\":\"https://example.com/cb\","
                + "\"sentPayload\":\"{}\",\"requestHeaders\":{},\"durationMs\":1}");

            CallbackConfiguration callback = service.GetCallbackConfiguration(9);
            CallbackTestResult result = service.TestCallbackConfiguration(CallbackTestRequest.ForSavedCallback(9));

            Assert.AreEqual("SOMETHING_NEW", callback.Type);
            Assert.AreEqual("NEW_AUTH", callback.AuthType);
            Assert.AreEqual("UNRECOGNIZED", callback.Events[0].GetName());
            Assert.AreEqual("NEW_EVENT", callback.Events[0].getApiValue());
            Assert.AreEqual("NEW_GRANT", callback.OAuth.GrantType);
            Assert.AreEqual("NEW_MECHANISM", callback.OAuth.AuthMechanism);
            Assert.AreEqual("NEW_OUTCOME", result.Outcome);
        }

        [Test]
        public void ABadRequestCarriesItsStatusAndDetail()
        {
            clientMock.Setup(c => c.Post(Callbacks, It.IsAny<string>()))
                .Throws(HttpError(400, "Bad Request", Problem(400, "Bad Request", "Illegal character in path")));

            ServerError error = ServerErrorOf(() =>
                service.CreateCallbackConfiguration(NewCallbackConfigurationRequest().WithUrl("not a url").Build()));

            Assert.AreEqual(400, error.Code);
            Assert.AreEqual("Illegal character in path", error.Message);
        }

        [Test]
        public void AnUnknownIdCarriesItsStatusAndDetail()
        {
            clientMock.Setup(c => c.Get(Callbacks + "/999"))
                .Throws(HttpError(404, "Not Found", Problem(404, "Not Found", "No callback configuration with id 999")));

            ServerError error = ServerErrorOf(() => service.GetCallbackConfiguration(999));

            Assert.AreEqual(404, error.Code);
            Assert.AreEqual("No callback configuration with id 999", error.Message);
        }

        [Test]
        public void AConflictCarriesItsStatusAndDetail()
        {
            clientMock.Setup(c => c.Post(Callbacks, It.IsAny<string>())).Throws(HttpError(409, "Conflict",
                Problem(409, "Conflict", "The account already has an account-level callback configuration")));

            ServerError error = ServerErrorOf(() =>
                service.CreateCallbackConfiguration(NewCallbackConfigurationRequest().Build()));

            Assert.AreEqual(409, error.Code);
            Assert.AreEqual("The account already has an account-level callback configuration", error.Message);
        }

        [Test]
        public void AnEmptyErrorBodyStillCarriesTheStatus()
        {
            clientMock.Setup(c => c.Get(Callbacks)).Throws(HttpError(401, "Unauthorized", ""));

            ServerError error = ServerErrorOf(() => service.GetCallbackConfigurations());

            Assert.AreEqual(401, error.Code);
            Assert.AreEqual("Unauthorized", error.Message);
        }

        [Test]
        public void AProblemWithoutDetailCarriesItsStatus()
        {
            clientMock.Setup(c => c.Get(Callbacks)).Throws(HttpError(403, "Forbidden",
                "{\"status\":403,\"title\":\"Forbidden\",\"instance\":\"/api/notifications/v1/callback-configurations\"}"));

            ServerError error = ServerErrorOf(() => service.GetCallbackConfigurations());

            Assert.AreEqual(403, error.Code);
            Assert.AreEqual("Forbidden", error.Message);
        }

        [Test]
        public void AClassicErrorBodyKeepsItsMessage()
        {
            clientMock.Setup(c => c.Get(Callbacks)).Throws(HttpError(401, "Unauthorized",
                "{\"code\":401,\"message\":\"Invalid api key\",\"messageKey\":\"error.unauthorised.noSession\"}"));

            ServerError error = ServerErrorOf(() => service.GetCallbackConfigurations());

            Assert.AreEqual(401, error.Code);
            Assert.AreEqual("Invalid api key", error.Message);
            Assert.AreEqual("error.unauthorised.noSession", error.MessageKey);
        }

        [Test]
        public void ANonJsonErrorBodyStillCarriesTheStatus()
        {
            clientMock.Setup(c => c.Get(Callbacks))
                .Throws(HttpError(502, "Bad Gateway", "<html><body>Bad gateway</body></html>"));

            ServerError error = ServerErrorOf(() => service.GetCallbackConfigurations());

            Assert.AreEqual(502, error.Code);
            Assert.AreEqual("Bad Gateway", error.Message);
        }

        private static OssServerException HttpError(int status, string reasonPhrase, string body)
        {
            return new OssServerException(status + " " + reasonPhrase, body, status, reasonPhrase, null);
        }

        private static string Problem(int status, string title, string detail)
        {
            return "{\"status\":" + status + ",\"title\":\"" + title + "\",\"detail\":\"" + detail
                + "\",\"instance\":\"/api/notifications/v1/callback-configurations\"}";
        }

        private static ServerError ServerErrorOf(Action call)
        {
            OssServerException e = Assert.Throws<OssServerException>(() => call());
            return e.ServerError;
        }

        private static void AssertJson(string expected, string actual)
        {
            Assert.IsTrue(JToken.DeepEquals(JToken.Parse(expected), JToken.Parse(actual)),
                "Expected " + expected + " but was " + actual);
        }
    }
}
