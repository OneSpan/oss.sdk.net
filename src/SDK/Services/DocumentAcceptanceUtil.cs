using System.Linq;
using System.Text.RegularExpressions;
using OneSpanSign.API;

namespace OneSpanSign.Sdk.Services
{
    /// <summary>
    /// Utility class for checking document acceptance by signers in a package.
    /// </summary>
    public static class DocumentAcceptanceUtil
    {
        public const string DOC_DATA_ACCEPTED_BY_SIGNERS = "esl_doc_accepted_by_signers";
        public const string DEFAULT_CONSENT_ID = "default-consent";

        private static readonly Regex SIGNER_SPLIT_PATTERN = new Regex(@"\s*,\s*", RegexOptions.Compiled);

        /// <summary>
        /// Checks if the default consent document in the given package has been accepted by at least one signer.
        /// </summary>
        /// <param name="aPackage">The package to check for default consent acceptance (may be null)</param>
        /// <returns>True if at least one signer has accepted the default consent; false otherwise</returns>
        internal static bool HasAcceptedDefaultConsent(Package aPackage)
        {
            var defaultConsentDocument = GetDefaultConsentDocument(aPackage);
            if (defaultConsentDocument == null)
            {
                return false;
            }

            if (defaultConsentDocument.Data == null ||
                !defaultConsentDocument.Data.TryGetValue(DOC_DATA_ACCEPTED_BY_SIGNERS, out var acceptedBySigners))
            {
                return false;
            }

            var acceptedBySignersStr = acceptedBySigners as string;
            if (string.IsNullOrWhiteSpace(acceptedBySignersStr))
            {
                return false;
            }

            return SIGNER_SPLIT_PATTERN
                .Split(acceptedBySignersStr)
                .Any(acceptedSignerUid => !string.IsNullOrWhiteSpace(acceptedSignerUid));
        }

        private static API.Document GetDefaultConsentDocument(Package aPackage)
        {
            if (aPackage == null || aPackage.Documents == null || aPackage.Documents.Count == 0)
            {
                return null;
            }

            return aPackage.Documents
                .FirstOrDefault(document => DEFAULT_CONSENT_ID.Equals(document.Id));
        }
    }
}
