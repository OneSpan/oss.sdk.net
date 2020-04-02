using System;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
    internal class SignerInformationForEquifaxUSAConverter
    {
        private OneSpanSign.Sdk.SignerInformationForEquifaxUSA sdkSignerInformationForEquifaxUSA = null;
        private OneSpanSign.API.SignerInformationForEquifaxUSA apiSignerInformationForEquifaxUSA = null;

        /// <summary>
        /// Construct with API SignerInformationForEquifaxUSA object involved in conversion.
        /// </summary>
        /// <param name="apiSignerInformationForEquifaxUSA">API attachment requirement.</param>
        public SignerInformationForEquifaxUSAConverter(OneSpanSign.API.SignerInformationForEquifaxUSA apiSignerInformationForEquifaxUSA)
        {
            this.apiSignerInformationForEquifaxUSA = apiSignerInformationForEquifaxUSA;
        }

        /// <summary>
        /// Construct with SDK SignerInformationForEquifaxUSA object involved in conversion.
        /// </summary>
        /// <param name="sdkSignerInformationForEquifaxUSA">SDK attachment requirement.</param>
        public SignerInformationForEquifaxUSAConverter(OneSpanSign.Sdk.SignerInformationForEquifaxUSA sdkSignerInformationForEquifaxUSA)
        {
            this.sdkSignerInformationForEquifaxUSA = sdkSignerInformationForEquifaxUSA;
        }

        /// <summary>
        /// Convert from SDK SignerInformationForEquifaxUSA to API SignerInformationForEquifaxUSA.
        /// </summary>
        /// <returns>The API attachment requirement.</returns>
        public OneSpanSign.API.SignerInformationForEquifaxUSA ToAPISignerInformationForEquifaxUSA()
        {
            if (sdkSignerInformationForEquifaxUSA == null)
            {
                return apiSignerInformationForEquifaxUSA;
            }

            OneSpanSign.API.SignerInformationForEquifaxUSA result = new OneSpanSign.API.SignerInformationForEquifaxUSA();

            result.FirstName = sdkSignerInformationForEquifaxUSA.FirstName;
            result.LastName = sdkSignerInformationForEquifaxUSA.LastName;
            result.StreetAddress = sdkSignerInformationForEquifaxUSA.StreetAddress;
            result.City = sdkSignerInformationForEquifaxUSA.City;
            result.State = sdkSignerInformationForEquifaxUSA.State;
            result.Zip = sdkSignerInformationForEquifaxUSA.Zip;
            result.SocialSecurityNumber = sdkSignerInformationForEquifaxUSA.SocialSecurityNumber;
            result.HomePhoneNumber = sdkSignerInformationForEquifaxUSA.HomePhoneNumber;
            result.TimeAtAddress = sdkSignerInformationForEquifaxUSA.TimeAtAddress;
            result.DriversLicenseNumber = sdkSignerInformationForEquifaxUSA.DriversLicenseNumber;
            result.DateOfBirth = sdkSignerInformationForEquifaxUSA.DateOfBirth;
            return result;
        }

        /// <summary>
        /// Convert from API SignerInformationForEquifaxUSA to SDK SignerInformationForEquifaxUSA.
        /// </summary>
        /// <returns>The SDK attachment requirement.</returns>
        public OneSpanSign.Sdk.SignerInformationForEquifaxUSA ToSDKSignerInformationForEquifaxUSA()
        {
            if (apiSignerInformationForEquifaxUSA == null)
            {
                return sdkSignerInformationForEquifaxUSA;
            }

            OneSpanSign.Sdk.SignerInformationForEquifaxUSA result = new OneSpanSign.Sdk.SignerInformationForEquifaxUSA();

            result.FirstName = apiSignerInformationForEquifaxUSA.FirstName;
            result.LastName = apiSignerInformationForEquifaxUSA.LastName;
            result.StreetAddress = apiSignerInformationForEquifaxUSA.StreetAddress;
            result.City = apiSignerInformationForEquifaxUSA.City;
            result.State = apiSignerInformationForEquifaxUSA.State;
            result.Zip = apiSignerInformationForEquifaxUSA.Zip;
            result.SocialSecurityNumber = apiSignerInformationForEquifaxUSA.SocialSecurityNumber;
            result.HomePhoneNumber = apiSignerInformationForEquifaxUSA.HomePhoneNumber;
            result.TimeAtAddress = apiSignerInformationForEquifaxUSA.TimeAtAddress;
            result.DriversLicenseNumber = apiSignerInformationForEquifaxUSA.DriversLicenseNumber;
            result.DateOfBirth = apiSignerInformationForEquifaxUSA.DateOfBirth;

            return result;
        }
    }
}