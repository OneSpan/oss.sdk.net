using System.Collections.Generic;
using OneSpanSign.API;

namespace OneSpanSign.Sdk
{
    internal static class CallbackConfigurationConverter
    {
        internal static CallbackConfiguration ToSDKCallbackConfiguration(PublicCallbackConfigurationView view)
        {
            return new CallbackConfiguration
            {
                Id = view.Id.GetValueOrDefault(),
                Type = view.Type,
                Url = view.Url,
                AuthType = view.AuthType,
                Events = ToSDKEvents(view.Events),
                CustomHeaderNames = view.CustomHeaderNames,
                OAuth = ToSDKOAuthSettings(view.Oauth),
                MtlsEnabled = view.MtlsEnabled == true,
                Enabled = view.Enabled == true
            };
        }

        internal static List<CallbackConfiguration> ToSDKCallbackConfigurations(List<PublicCallbackConfigurationView> views)
        {
            List<CallbackConfiguration> result = new List<CallbackConfiguration>();
            foreach (PublicCallbackConfigurationView view in views)
            {
                result.Add(ToSDKCallbackConfiguration(view));
            }
            return result;
        }

        internal static PublicCallbackConfigurationForm ToAPICallbackConfigurationForm(CallbackConfigurationRequest request)
        {
            PublicCallbackConfigurationForm result = new PublicCallbackConfigurationForm();
            Fill(result, request);
            return result;
        }

        internal static PublicCallbackTestForm ToAPICallbackTestForm(CallbackTestRequest request)
        {
            PublicCallbackTestForm result = new PublicCallbackTestForm();
            result.CallbackId = request.CallbackId;
            Fill(result, request.Configuration);
            return result;
        }

        internal static CallbackTestResult ToSDKCallbackTestResult(CallbackTestResultView view)
        {
            CallbackTestResult result = new CallbackTestResult
            {
                Outcome = view.Outcome,
                Url = view.Url,
                SentPayload = view.SentPayload,
                RequestHeaders = view.RequestHeaders,
                DurationMs = view.DurationMs.GetValueOrDefault(),
                FailureDetail = view.FailureDetail
            };
            if (view.Response != null)
            {
                result.Response = new CallbackTestResponse
                {
                    Status = view.Response.Status.GetValueOrDefault(),
                    Headers = view.Response.Headers
                };
            }
            return result;
        }

        internal static ClientCertificateIntermediate ToSDKClientCertificateIntermediate(PublicIntermediateCertificateView view)
        {
            return new ClientCertificateIntermediate
            {
                ChainPem = view.ChainPem,
                Subject = view.Subject,
                Issuer = view.Issuer,
                SerialNumber = view.SerialNumber,
                Sha256Fingerprint = view.Sha256Fingerprint,
                NotBefore = view.NotBefore,
                NotAfter = view.NotAfter,
                KeyAlgorithm = view.KeyAlgorithm,
                SignatureAlgorithm = view.SignatureAlgorithm,
                Status = view.Status
            };
        }

        private static List<NotificationEvent> ToSDKEvents(List<string> events)
        {
            List<NotificationEvent> result = new List<NotificationEvent>();
            foreach (string notificationEvent in events)
            {
                result.Add(new EventNotificationConverter(notificationEvent).ToSDKNotificationEvent());
            }
            return result;
        }

        private static CallbackOAuthSettings ToSDKOAuthSettings(OauthSettingView view)
        {
            if (view == null)
            {
                return null;
            }
            return new CallbackOAuthSettings
            {
                GrantType = view.GrantType,
                AuthMechanism = view.AuthMechanism,
                AuthorizationServerUrl = view.AuthorizationServerUrl,
                ClientId = view.ClientId,
                Scope = view.Scope,
                IgnoreSsl = view.IgnoreSsl == true,
                CustomParameterNames = view.CustomParameterNames
            };
        }

        private static void Fill(PublicCallbackConfigurationForm form, CallbackConfigurationRequest request)
        {
            form.Url = request.Url;
            form.AuthType = request.AuthType;
            form.Events = request.Events;
            form.Key = request.Key;
            form.Oauth = ToAPIOAuthSettingForm(request.OAuth);
            form.CustomHeaders = ToNamedValues(request.CustomHeaders);
            form.MtlsEnabled = request.MtlsEnabled;
        }

        private static OauthSettingForm ToAPIOAuthSettingForm(CallbackOAuthRequest request)
        {
            if (request == null)
            {
                return null;
            }
            return new OauthSettingForm
            {
                GrantType = request.GrantType,
                AuthMechanism = request.AuthMechanism,
                AuthorizationServerUrl = request.AuthorizationServerUrl,
                ClientId = request.ClientId,
                ClientSecret = request.ClientSecret,
                Scope = request.Scope,
                IgnoreSsl = request.IgnoreSsl,
                CustomParameters = ToNamedValues(request.CustomParameters)
            };
        }

        private static List<NamedValue> ToNamedValues(Dictionary<string, string> values)
        {
            if (values == null)
            {
                return null;
            }
            List<NamedValue> result = new List<NamedValue>();
            foreach (KeyValuePair<string, string> entry in values)
            {
                result.Add(new NamedValue(entry.Key, entry.Value));
            }
            return result;
        }
    }
}
