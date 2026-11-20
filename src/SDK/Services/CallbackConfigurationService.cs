using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OneSpanSign.API;
using OneSpanSign.Sdk.Internal;

namespace OneSpanSign.Sdk.Services
{
    /// <summary>
    /// Manages the account's callbacks. Replaces the account-level methods of <see cref="EventNotificationService"/>.
    /// </summary>
    /// <remarks>
    /// <para>An account has at most one account-level callback (<c>Type</c> <c>ACCOUNT</c>) and may have callbacks set
    /// up by connectors (<c>Type</c> <c>CONNECTOR</c>). Connector callbacks can be read, replaced, enabled, disabled
    /// and tested, but not created or deleted.</para>
    /// <para>When the server answers with an HTTP error, methods throw <see cref="OssServerException"/>: its
    /// <see cref="ServerError"/> has the HTTP status as <c>Code</c> (for example 409 or 400) and the server's
    /// explanation as <c>Message</c>. Other failures throw <see cref="OssException"/>.</para>
    /// </remarks>
    public class CallbackConfigurationService
    {
        private readonly IRestClient restClient;
        private readonly string baseUrl;
        private readonly JsonSerializerSettings settings;

        internal CallbackConfigurationService(IRestClient restClient, string baseUrl, JsonSerializerSettings settings)
        {
            this.restClient = restClient;
            this.baseUrl = baseUrl;
            this.settings = settings;
        }

        /// <summary>
        /// Returns all callbacks of the account: the account-level one first, if any, then the connector ones.
        /// </summary>
        public List<CallbackConfiguration> GetCallbackConfigurations()
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.CALLBACK_CONFIGURATIONS_PATH).Build();
            string message = "Could not get the callback configurations.";
            try
            {
                string response = restClient.Get(path);
                return CallbackConfigurationConverter.ToSDKCallbackConfigurations(
                    JsonConvert.DeserializeObject<List<PublicCallbackConfigurationView>>(response, settings));
            }
            catch (OssServerException e)
            {
                throw ServerException(message, e);
            }
            catch (Exception e)
            {
                throw new OssException(message + " " + e.Message, e);
            }
        }

        public CallbackConfiguration GetCallbackConfiguration(long callbackId)
        {
            string message = "Could not get the callback configuration.";
            try
            {
                return ToCallbackConfiguration(restClient.Get(CallbackPath(callbackId)));
            }
            catch (OssServerException e)
            {
                throw ServerException(message, e);
            }
            catch (Exception e)
            {
                throw new OssException(message + " " + e.Message, e);
            }
        }

        /// <summary>
        /// Creates the account-level callback. It is enabled when created.
        /// </summary>
        /// <remarks>
        /// An account has at most one: if it already exists, this fails with a 409; use
        /// <see cref="ReplaceCallbackConfiguration"/> instead.
        /// </remarks>
        public CallbackConfiguration CreateCallbackConfiguration(CallbackConfigurationRequest request)
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.CALLBACK_CONFIGURATIONS_PATH).Build();
            string message = "Could not create the callback configuration.";
            try
            {
                string response = restClient.Post(path, JsonConvert.SerializeObject(
                    CallbackConfigurationConverter.ToAPICallbackConfigurationForm(request), settings));
                return ToCallbackConfiguration(response);
            }
            catch (OssServerException e)
            {
                throw ServerException(message, e);
            }
            catch (Exception e)
            {
                throw new OssException(message + " " + e.Message, e);
            }
        }

        /// <summary>
        /// Replaces a callback, account-level or connector, with <paramref name="request"/>. Its enabled state does not
        /// change.
        /// </summary>
        /// <remarks>
        /// <para>Any property not set in <paramref name="request"/> is cleared, except secrets: a secret left
        /// <c>null</c> keeps its saved value, so you can read a callback, change it and replace it without sending its
        /// secrets again. Secrets are the key, the OAuth client secret, and the values of custom headers and OAuth
        /// custom parameters; to keep a header or parameter, set its name with a <c>null</c> value. A saved secret is
        /// kept only if:</para>
        /// <list type="bullet">
        /// <item><description><c>Url</c> is unchanged (all secrets);</description></item>
        /// <item><description><c>OAuth.AuthorizationServerUrl</c> is unchanged and <c>OAuth.IgnoreSsl</c> is not
        /// switched on (OAuth client secret and custom parameter values);</description></item>
        /// <item><description><c>OAuth.ClientId</c> is unchanged (OAuth client secret).</description></item>
        /// </list>
        /// <para>Otherwise send the secret again: leaving it out fails with a 400.</para>
        /// </remarks>
        public CallbackConfiguration ReplaceCallbackConfiguration(long callbackId, CallbackConfigurationRequest request)
        {
            string message = "Could not replace the callback configuration.";
            try
            {
                string response = restClient.Put(CallbackPath(callbackId), JsonConvert.SerializeObject(
                    CallbackConfigurationConverter.ToAPICallbackConfigurationForm(request), settings));
                return ToCallbackConfiguration(response);
            }
            catch (OssServerException e)
            {
                throw ServerException(message, e);
            }
            catch (Exception e)
            {
                throw new OssException(message + " " + e.Message, e);
            }
        }

        public CallbackConfiguration EnableCallbackConfiguration(long callbackId)
        {
            return UpdateEnabled(callbackId, true);
        }

        /// <summary>
        /// Disables a callback. It keeps its configuration, but nothing is sent to it until it is enabled again.
        /// </summary>
        public CallbackConfiguration DisableCallbackConfiguration(long callbackId)
        {
            return UpdateEnabled(callbackId, false);
        }

        /// <summary>
        /// Deletes the account-level callback. A connector callback cannot be deleted (409): disable it instead.
        /// </summary>
        public void DeleteCallbackConfiguration(long callbackId)
        {
            string message = "Could not delete the callback configuration.";
            try
            {
                restClient.Delete(CallbackPath(callbackId));
            }
            catch (OssServerException e)
            {
                throw ServerException(message, e);
            }
            catch (Exception e)
            {
                throw new OssException(message + " " + e.Message, e);
            }
        }

        /// <summary>
        /// Sends a test event to the callback URL and returns what happened. Nothing is saved. A failed delivery
        /// (timeout, non-2xx answer, ...) is reported in the result, not thrown.
        /// </summary>
        public CallbackTestResult TestCallbackConfiguration(CallbackTestRequest request)
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.CALLBACK_CONFIGURATION_TEST_PATH).Build();
            string message = "Could not test the callback configuration.";
            try
            {
                string response = restClient.Post(path, JsonConvert.SerializeObject(
                    CallbackConfigurationConverter.ToAPICallbackTestForm(request), settings));
                return CallbackConfigurationConverter.ToSDKCallbackTestResult(
                    JsonConvert.DeserializeObject<CallbackTestResultView>(response, settings));
            }
            catch (OssServerException e)
            {
                throw ServerException(message, e);
            }
            catch (Exception e)
            {
                throw new OssException(message + " " + e.Message, e);
            }
        }

        /// <summary>
        /// Returns the OneSpan intermediate CA certificate your callback endpoint must trust to accept mTLS callbacks.
        /// Fails with a 404 when mTLS callbacks are not enabled.
        /// </summary>
        public ClientCertificateIntermediate GetClientCertificateIntermediate()
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.CLIENT_CERTIFICATE_INTERMEDIATE_PATH).Build();
            string message = "Could not get the client certificate intermediate.";
            try
            {
                string response = restClient.Get(path);
                return CallbackConfigurationConverter.ToSDKClientCertificateIntermediate(
                    JsonConvert.DeserializeObject<PublicIntermediateCertificateView>(response, settings));
            }
            catch (OssServerException e)
            {
                throw ServerException(message, e);
            }
            catch (Exception e)
            {
                throw new OssException(message + " " + e.Message, e);
            }
        }

        private CallbackConfiguration UpdateEnabled(long callbackId, bool enabled)
        {
            string message = "Could not update the callback configuration.";
            try
            {
                string response = restClient.Patch(CallbackPath(callbackId), JsonConvert.SerializeObject(
                    new PublicCallbackUpdateForm { Enabled = enabled }, settings));
                return ToCallbackConfiguration(response);
            }
            catch (OssServerException e)
            {
                throw ServerException(message, e);
            }
            catch (Exception e)
            {
                throw new OssException(message + " " + e.Message, e);
            }
        }

        private string CallbackPath(long callbackId)
        {
            return new UrlTemplate(baseUrl).UrlFor(UrlTemplate.CALLBACK_CONFIGURATION_PATH)
                .Replace("{callbackId}", callbackId.ToString())
                .Build();
        }

        private CallbackConfiguration ToCallbackConfiguration(string response)
        {
            return CallbackConfigurationConverter.ToSDKCallbackConfiguration(
                JsonConvert.DeserializeObject<PublicCallbackConfigurationView>(response, settings));
        }

        private static OssServerException ServerException(string message, OssServerException e)
        {
            ServerError serverError = e.ServerError;
            serverError.Code = e.StatusCode;
            // These endpoints return RFC 9457 problem details, but a proxy in front may still return the classic Error body.
            string problemMessage = ProblemMessage(e.ErrorDetails);
            if (problemMessage != null)
            {
                serverError.Message = problemMessage;
            }
            return new OssServerException(message + " " + e.Message, serverError, e);
        }

        private static string ProblemMessage(string body)
        {
            try
            {
                JObject problem = JToken.Parse(body) as JObject;
                return problem == null ? null : (string)problem["detail"] ?? (string)problem["title"];
            }
            catch (JsonReaderException)
            {
                return null;
            }
        }
    }
}
