namespace OneSpanSign.Sdk.Internal
{
    /// <summary>
    /// Provides constant error and informational messages related to consent localization operations.
    /// This class cannot be instantiated.
    /// </summary>
    public sealed class ConsentLocalizationMessages
    {
        public const string CONSENT_DOCUMENT_LOCALIZED_SUCCESSFULLY = "Consent document localized successfully.";
        public const string FAILED_TO_LOCALIZE_DEFAULT_CONSENT_PREFIX = "Failed to localize default consent: ";
        public const string UPDATED_PACKAGE_NOT_AVAILABLE = "Consent localization could not be determined: updatedPackage is not available.";
        public const string LANGUAGE_NOT_CHANGED = "Consent localization not required because language did not change.";
        public const string DEFAULT_DOCUMENT_CONSENT_ACCEPTED = "Default consent document modification is not allowed: the transaction has at least one signer who has accepted the default consent.";

        private ConsentLocalizationMessages()
        {
            // Prevent instantiation
        }
    }
}
