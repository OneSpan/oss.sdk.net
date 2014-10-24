using System;

namespace Silanis.ESL.SDK
{
    internal class KnowledgeBasedAuthenticationStatusConverter
    {
        private Silanis.ESL.SDK.KnowledgeBasedAuthenticationStatus sdkKnowledgeBasedAuthenticationStatus;
        private Silanis.ESL.API.KnowledgeBasedAuthenticationStatus apiKnowledgeBasedAuthenticationStatus;

        /// <summary>
        /// Construct with API KnowledgeBasedAuthenticationStatus object involved in conversion.
        /// </summary>
        /// <param name="apiKnowledgeBasedAuthenticationStatus">API sender type.</param>
        public KnowledgeBasedAuthenticationStatusConverter(Silanis.ESL.API.KnowledgeBasedAuthenticationStatus apiKnowledgeBasedAuthenticationStatus)
        {
            this.apiKnowledgeBasedAuthenticationStatus = apiKnowledgeBasedAuthenticationStatus;
        }

        /// <summary>
        /// Construct with SDK KnowledgeBasedAuthenticationStatus object involved in conversion.
        /// </summary>
        /// <param name="sdkKnowledgeBasedAuthenticationStatus">SDK sender type.</param>
        public KnowledgeBasedAuthenticationStatusConverter(Silanis.ESL.SDK.KnowledgeBasedAuthenticationStatus sdkKnowledgeBasedAuthenticationStatus)
        {
            this.sdkKnowledgeBasedAuthenticationStatus = sdkKnowledgeBasedAuthenticationStatus;
        }

        /// <summary>
        /// Convert from SDK KnowledgeBasedAuthenticationStatus to API KnowledgeBasedAuthenticationStatus.
        /// </summary>
        /// <returns>The API sender type.</returns>
        public Silanis.ESL.API.KnowledgeBasedAuthenticationStatus ToAPIKnowledgeBasedAuthenticationStatus()
        {
            switch (sdkKnowledgeBasedAuthenticationStatus)
            {
                case KnowledgeBasedAuthenticationStatus.NOT_YET_ATTEMPTED:
                    return Silanis.ESL.API.KnowledgeBasedAuthenticationStatus.NOT_YET_ATTEMPTED;
                case KnowledgeBasedAuthenticationStatus.FAILED:
                    return Silanis.ESL.API.KnowledgeBasedAuthenticationStatus.FAILED;
                case KnowledgeBasedAuthenticationStatus.PASSED:
                    return Silanis.ESL.API.KnowledgeBasedAuthenticationStatus.PASSED;
                default:
                    throw new EslException(String.Format("Unable to decode the knowledge based authentication status {0}", sdkKnowledgeBasedAuthenticationStatus), null);
            }
        }

        /// <summary>
        /// Convert from API KnowledgeBasedAuthenticationStatus to SDK KnowledgeBasedAuthenticationStatus.
        /// </summary>
        /// <returns>The SDK sender type.</returns>
        public Silanis.ESL.SDK.KnowledgeBasedAuthenticationStatus ToSDKKnowledgeBasedAuthenticationStatus()
        {
            switch (apiKnowledgeBasedAuthenticationStatus)
            {
                case Silanis.ESL.API.KnowledgeBasedAuthenticationStatus.NOT_YET_ATTEMPTED:
                    return KnowledgeBasedAuthenticationStatus.NOT_YET_ATTEMPTED;
                case Silanis.ESL.API.KnowledgeBasedAuthenticationStatus.FAILED:
                    return KnowledgeBasedAuthenticationStatus.FAILED;
                case Silanis.ESL.API.KnowledgeBasedAuthenticationStatus.PASSED:
                    return KnowledgeBasedAuthenticationStatus.PASSED;
                default:
                    throw new EslException(String.Format("Unable to decode the knowledge based authentication status {0}", apiKnowledgeBasedAuthenticationStatus), null);
            }
        }
    }
}
