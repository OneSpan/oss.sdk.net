using System;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
    internal class SignerInformationForLexisNexisConverter
    {
        private OneSpanSign.Sdk.SignerInformationForLexisNexis sdkSignerInformationForLexisNexis = null;
        private OneSpanSign.API.SignerInformationForLexisNexis apiSignerInformationForLexisNexis = null;

        /// <summary>
        /// Construct with API sdkSignerInformationForLexisNexis object involved in conversion.
        /// </summary>
        /// <param name="apiSignerInformationForLexisNexis">API attachment requirement.</param>
        public SignerInformationForLexisNexisConverter(OneSpanSign.API.SignerInformationForLexisNexis apiSignerInformationForLexisNexis)
        {
            this.apiSignerInformationForLexisNexis = apiSignerInformationForLexisNexis;
        }

        /// <summary>
        /// Construct with SDK sdkSignerInformationForLexisNexis object involved in conversion.
        /// </summary>
        /// <param name="sdkSignerInformationForLexisNexis">SDK attachment requirement.</param>
        public SignerInformationForLexisNexisConverter(OneSpanSign.Sdk.SignerInformationForLexisNexis sdkSignerInformationForLexisNexis)
        {
            this.sdkSignerInformationForLexisNexis = sdkSignerInformationForLexisNexis;
        }

        /// <summary>
        /// Convert from SDK sdkSignerInformationForLexisNexis to API sdkSignerInformationForLexisNexis.
        /// </summary>
        /// <returns>The API attachment requirement.</returns>
        public OneSpanSign.API.SignerInformationForLexisNexis ToApiSignerInformationForLexisNexis()
        {
            if (sdkSignerInformationForLexisNexis == null)
            {
                return apiSignerInformationForLexisNexis;
            }

            OneSpanSign.API.SignerInformationForLexisNexis result = new OneSpanSign.API.SignerInformationForLexisNexis();

            result.FirstName = sdkSignerInformationForLexisNexis.FirstName;
            result.LastName = sdkSignerInformationForLexisNexis.LastName;
            result.FlatOrApartmentNumber = sdkSignerInformationForLexisNexis.FlatOrApartmentNumber;
            result.HouseName = sdkSignerInformationForLexisNexis.HouseName;
            result.HouseNumber = sdkSignerInformationForLexisNexis.HouseNumber;
            result.City = sdkSignerInformationForLexisNexis.City;
            result.State = sdkSignerInformationForLexisNexis.State;
            result.Zip = sdkSignerInformationForLexisNexis.Zip;
            result.SocialSecurityNumber = sdkSignerInformationForLexisNexis.SocialSecurityNumber;
            result.DateOfBirth = sdkSignerInformationForLexisNexis.DateOfBirth;
            return result;
        }

        /// <summary>
        /// Convert from API SignerInformationForLexisNexis to SDK SignerInformationForLexisNexis.
        /// </summary>
        /// <returns>The SDK attachment requirement.</returns>
        public OneSpanSign.Sdk.SignerInformationForLexisNexis ToSDKSignerInformationForLexisNexis()
        {
            if (apiSignerInformationForLexisNexis == null)
            {
                return sdkSignerInformationForLexisNexis;
            }

            OneSpanSign.Sdk.SignerInformationForLexisNexis result = new OneSpanSign.Sdk.SignerInformationForLexisNexis();

            result.FirstName = apiSignerInformationForLexisNexis.FirstName;
            result.LastName = apiSignerInformationForLexisNexis.LastName;
            result.FlatOrApartmentNumber = apiSignerInformationForLexisNexis.FlatOrApartmentNumber;
            result.HouseName = apiSignerInformationForLexisNexis.HouseName;
            result.HouseNumber = apiSignerInformationForLexisNexis.HouseNumber;
            result.City = apiSignerInformationForLexisNexis.City;
            result.State = apiSignerInformationForLexisNexis.State;
            result.Zip = apiSignerInformationForLexisNexis.Zip;
            result.SocialSecurityNumber = apiSignerInformationForLexisNexis.SocialSecurityNumber;
            result.DateOfBirth = apiSignerInformationForLexisNexis.DateOfBirth;

            return result;
        }
    }
}