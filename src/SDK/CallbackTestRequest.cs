namespace OneSpanSign.Sdk
{
    /// <summary>
    /// A callback configuration to test without saving it.
    /// </summary>
    /// <remarks>
    /// For a saved callback, set only what you want to change: the rest comes from the saved callback, and its secrets
    /// are reused under the conditions on
    /// <see cref="Services.CallbackConfigurationService.ReplaceCallbackConfiguration"/>. For a new configuration, set
    /// everything it needs, as for create.
    /// </remarks>
    public class CallbackTestRequest
    {
        private CallbackTestRequest(long? callbackId, CallbackConfigurationRequest configuration)
        {
            CallbackId = callbackId;
            Configuration = configuration;
        }

        /// <summary>
        /// Tests the saved callback <paramref name="callbackId"/> as it is.
        /// </summary>
        public static CallbackTestRequest ForSavedCallback(long callbackId)
        {
            return new CallbackTestRequest(callbackId, new CallbackConfigurationRequest());
        }

        /// <summary>
        /// Tests the saved callback <paramref name="callbackId"/> with <paramref name="changes"/> applied.
        /// </summary>
        public static CallbackTestRequest ForSavedCallback(long callbackId, CallbackConfigurationRequest changes)
        {
            return new CallbackTestRequest(callbackId, changes);
        }

        /// <summary>
        /// Tests a new configuration that is not saved.
        /// </summary>
        public static CallbackTestRequest ForConfiguration(CallbackConfigurationRequest configuration)
        {
            return new CallbackTestRequest(null, configuration);
        }

        public long? CallbackId { get; }

        public CallbackConfigurationRequest Configuration { get; }
    }
}
