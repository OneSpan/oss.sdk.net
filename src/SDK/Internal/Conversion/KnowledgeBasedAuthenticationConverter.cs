using System;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
    internal class KnowledgeBasedAuthenticationConverter
    {
        private OneSpanSign.Sdk.KnowledgeBasedAuthentication sdkKnowledgeBasedAuthentication = null;
        private OneSpanSign.API.KnowledgeBasedAuthentication apiKnowledgeBasedAuthentication = null;

        /// <summary>
        /// Construct with API KnowledgeBasedAuthentication object involved in conversion.
        /// </summary>
        /// <param name="apiKnowledgeBasedAuthentication">API knowledge based authentication.</param>
        public KnowledgeBasedAuthenticationConverter(OneSpanSign.API.KnowledgeBasedAuthentication apiKnowledgeBasedAuthentication)
        {
            this.apiKnowledgeBasedAuthentication = apiKnowledgeBasedAuthentication;
        }

        /// <summary>
        /// Construct with SDK KnowledgeBasedAuthentication object involved in conversion.
        /// </summary>
        /// <param name="sdkKnowledgeBasedAuthentication">SDK knowledge based authentication.</param>
        public KnowledgeBasedAuthenticationConverter(OneSpanSign.Sdk.KnowledgeBasedAuthentication sdkKnowledgeBasedAuthentication)
        {
            this.sdkKnowledgeBasedAuthentication = sdkKnowledgeBasedAuthentication;
        }

        /// <summary>
        /// Convert from SDK KnowledgeBasedAuthentication to API KnowledgeBasedAuthentication.
        /// </summary>
        /// <returns>The API knowledge based authentication.</returns>
        public OneSpanSign.API.KnowledgeBasedAuthentication ToAPIKnowledgeBasedAuthentication()
        {
            if (sdkKnowledgeBasedAuthentication == null)
            {
                return apiKnowledgeBasedAuthentication;
            }

            OneSpanSign.API.KnowledgeBasedAuthentication result = new OneSpanSign.API.KnowledgeBasedAuthentication();
            result.SignerInformationForEquifaxCanada = new SignerInformationForEquifaxCanadaConverter(sdkKnowledgeBasedAuthentication.SignerInformationForEquifaxCanada).ToAPISignerInformationForEquifaxCanada();
            result.SignerInformationForEquifaxUSA = new SignerInformationForEquifaxUSAConverter(sdkKnowledgeBasedAuthentication.SignerInformationForEquifaxUSA).ToAPISignerInformationForEquifaxUSA();
            result.SignerInformationForLexisNexis = new SignerInformationForLexisNexisConverter(sdkKnowledgeBasedAuthentication.SignerInformationForLexisNexis).ToApiSignerInformationForLexisNexis();
            result.KnowledgeBasedAuthenticationStatus = new KnowledgeBasedAuthenticationStatusConverter(sdkKnowledgeBasedAuthentication.KnowledgeBasedAuthenticationStatus).ToAPIKnowledgeBasedAuthenticationStatus();

            return result;
        }

        /// <summary>
        /// Convert from API KnowledgeBasedAuthentication to SDK KnowledgeBasedAuthentication.
        /// </summary>
        /// <returns>The SDK knowledge based authentication.</returns>
        public OneSpanSign.Sdk.KnowledgeBasedAuthentication ToSDKKnowledgeBasedAuthentication()
        {
            if (apiKnowledgeBasedAuthentication == null)
            {
                return sdkKnowledgeBasedAuthentication;
            }

            OneSpanSign.Sdk.KnowledgeBasedAuthentication result = new OneSpanSign.Sdk.KnowledgeBasedAuthentication();
            result.SignerInformationForEquifaxCanada = new SignerInformationForEquifaxCanadaConverter(apiKnowledgeBasedAuthentication.SignerInformationForEquifaxCanada).ToSDKSignerInformationForEquifaxCanada();
            result.SignerInformationForEquifaxUSA = new SignerInformationForEquifaxUSAConverter(apiKnowledgeBasedAuthentication.SignerInformationForEquifaxUSA).ToSDKSignerInformationForEquifaxUSA();
            result.SignerInformationForLexisNexis = new SignerInformationForLexisNexisConverter(apiKnowledgeBasedAuthentication.SignerInformationForLexisNexis).ToSDKSignerInformationForLexisNexis();
            result.KnowledgeBasedAuthenticationStatus = new KnowledgeBasedAuthenticationStatusConverter(apiKnowledgeBasedAuthentication.KnowledgeBasedAuthenticationStatus).ToSDKKnowledgeBasedAuthenticationStatus();
            return result;
        }
    }
}
