using System.Collections.Generic;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Services;
using static OneSpanSign.Sdk.Builder.CallbackConfigurationRequestBuilder;

namespace SDK.Examples
{
    /// <summary>
    /// Manages the account callback with <see cref="CallbackConfigurationService"/>: creates or replaces it, then
    /// lists, tests, changes, disables and deletes it.
    /// </summary>
    /// <remarks>
    /// Running this replaces, then deletes, the account's existing callback.
    /// </remarks>
    public class CallbackConfigurationExample : SDKSample
    {
        public const string URL = "https://example.com/callback";
        public const string KEY = "abc";

        public CallbackConfiguration saved, replaced, disabled;
        public List<CallbackConfiguration> configurations;
        public CallbackTestResult testResult;

        public static void Main(string[] args)
        {
            new CallbackConfigurationExample().Run();
        }

        override public void Execute()
        {
            CallbackConfigurationService service = ossClient.CallbackConfigurationService;

            CallbackConfigurationRequest request = NewCallbackConfigurationRequest()
                .WithUrl(URL)
                .WithAuthType("BASIC_AUTH")
                .WithKey(KEY)
                .WithEvents("PACKAGE_CREATE", "PACKAGE_COMPLETE")
                .Build();
            // The account may already have its callback, e.g. from EventNotificationService; create would then fail with 409.
            CallbackConfiguration account = FindAccountCallback(service.GetCallbackConfigurations());
            saved = account == null
                ? service.CreateCallbackConfiguration(request)
                : service.ReplaceCallbackConfiguration(account.Id, request);

            configurations = service.GetCallbackConfigurations();

            testResult = service.TestCallbackConfiguration(CallbackTestRequest.ForSavedCallback(saved.Id));

            // No key: the saved one is kept because the url does not change.
            replaced = service.ReplaceCallbackConfiguration(saved.Id, NewCallbackConfigurationRequest()
                .WithUrl(URL)
                .WithAuthType("BASIC_AUTH")
                .WithEvents("PACKAGE_CREATE", "PACKAGE_COMPLETE", "PACKAGE_DECLINE")
                .Build());

            disabled = service.DisableCallbackConfiguration(saved.Id);

            service.DeleteCallbackConfiguration(saved.Id);
        }

        private static CallbackConfiguration FindAccountCallback(List<CallbackConfiguration> configurations)
        {
            foreach (CallbackConfiguration configuration in configurations)
            {
                if (configuration.Type == "ACCOUNT")
                {
                    return configuration;
                }
            }
            return null;
        }
    }
}
