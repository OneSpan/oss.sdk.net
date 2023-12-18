using System;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
    internal class SignerInformationForEquifaxCanadaConverter
    {
        private OneSpanSign.Sdk.SignerInformationForEquifaxCanada sdkSignerInformationForEquifaxCanada = null;
        private OneSpanSign.API.SignerInformationForEquifaxCanada apiSignerInformationForEquifaxCanada = null;

        /// <summary>
        /// Construct with API SignerInformationForEquifaxCanada object involved in conversion.
        /// </summary>
        /// <param name="apiSignerInformationForEquifaxCanada">API attachment requirement.</param>
        public SignerInformationForEquifaxCanadaConverter(OneSpanSign.API.SignerInformationForEquifaxCanada apiSignerInformationForEquifaxCanada)
        {
            this.apiSignerInformationForEquifaxCanada = apiSignerInformationForEquifaxCanada;
        }

        /// <summary>
        /// Construct with SDK SignerInformationForEquifaxCanada object involved in conversion.
        /// </summary>
        /// <param name="sdkSignerInformationForEquifaxCanada">SDK attachment requirement.</param>
        public SignerInformationForEquifaxCanadaConverter(OneSpanSign.Sdk.SignerInformationForEquifaxCanada sdkSignerInformationForEquifaxCanada)
        {
            this.sdkSignerInformationForEquifaxCanada = sdkSignerInformationForEquifaxCanada;
        }

        /// <summary>
        /// Convert from SDK SignerInformationForEquifaxCanada to API SignerInformationForEquifaxCanada.
        /// </summary>
        /// <returns>The API attachment requirement.</returns>
        public OneSpanSign.API.SignerInformationForEquifaxCanada ToAPISignerInformationForEquifaxCanada()
        {
            if (sdkSignerInformationForEquifaxCanada == null)
            {
                return apiSignerInformationForEquifaxCanada;
            }

            OneSpanSign.API.SignerInformationForEquifaxCanada result = new OneSpanSign.API.SignerInformationForEquifaxCanada();

            result.FirstName = sdkSignerInformationForEquifaxCanada.FirstName;
            result.LastName = sdkSignerInformationForEquifaxCanada.LastName;
            result.StreetAddress = sdkSignerInformationForEquifaxCanada.StreetAddress;
            result.City = sdkSignerInformationForEquifaxCanada.City;
            result.Province = sdkSignerInformationForEquifaxCanada.Province;
            result.PostalCode = sdkSignerInformationForEquifaxCanada.PostalCode;
            result.TimeAtAddress = sdkSignerInformationForEquifaxCanada.TimeAtAddress;
            result.DriversLicenseNumber = sdkSignerInformationForEquifaxCanada.DriversLicenseNumber;
            result.SocialInsuranceNumber = sdkSignerInformationForEquifaxCanada.SocialInsuranceNumber;
            result.HomePhoneNumber = sdkSignerInformationForEquifaxCanada.HomePhoneNumber;
            result.DateOfBirth = sdkSignerInformationForEquifaxCanada.DateOfBirth;

            return result;
        }

        /// <summary>
        /// Convert from API SignerInformationForEquifaxCanada to SDK SignerInformationForEquifaxCanada.
        /// </summary>
        /// <returns>The SDK attachment requirement.</returns>
        public OneSpanSign.Sdk.SignerInformationForEquifaxCanada ToSDKSignerInformationForEquifaxCanada()
        {
            if (apiSignerInformationForEquifaxCanada == null)
            {
                return sdkSignerInformationForEquifaxCanada;
            }

            OneSpanSign.Sdk.SignerInformationForEquifaxCanada result = new OneSpanSign.Sdk.SignerInformationForEquifaxCanada();

            result.FirstName = apiSignerInformationForEquifaxCanada.FirstName;
            result.LastName = apiSignerInformationForEquifaxCanada.LastName;
            result.StreetAddress = apiSignerInformationForEquifaxCanada.StreetAddress;
            result.City = apiSignerInformationForEquifaxCanada.City;
            result.Province = apiSignerInformationForEquifaxCanada.Province;
            result.PostalCode = apiSignerInformationForEquifaxCanada.PostalCode;
            result.TimeAtAddress = apiSignerInformationForEquifaxCanada.TimeAtAddress;
            result.DriversLicenseNumber = apiSignerInformationForEquifaxCanada.DriversLicenseNumber;
            result.SocialInsuranceNumber = apiSignerInformationForEquifaxCanada.SocialInsuranceNumber;
            result.HomePhoneNumber = apiSignerInformationForEquifaxCanada.HomePhoneNumber;
            result.DateOfBirth = apiSignerInformationForEquifaxCanada.DateOfBirth;

            return result;
        }
    }
}


