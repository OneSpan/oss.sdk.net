using System;
using Silanis.ESL.SDK.Internal;

namespace Silanis.ESL.SDK
{
    public class SignerInformationForEquifaxCanadaBuilder
    {
        private string firstName;
        private string lastName;
        private string streetAddress;
        private string city;
        private string state;
        private string zip;
        private string timeAtAddress;
        private string driversLicenseIndicator;
        private string socialInsuranceNumber;
        private string homePhoneNumber;
        private Nullable<DateTime> dateOfBirth;


        public static SignerInformationForEquifaxCanadaBuilder NewSignerInformationForEquifaxCanada() {
                return new SignerInformationForEquifaxCanadaBuilder();
        }
        
        public SignerInformationForEquifaxCanadaBuilder WithFirstName(string firstName)
        {
            this.firstName = firstName;
            return this;
        }

        public SignerInformationForEquifaxCanadaBuilder WithLastName(string lastName)
        {
            this.lastName = lastName;
            return this;
        }

        public SignerInformationForEquifaxCanadaBuilder WithStreetAddress(string streetAddress)
        {
            this.streetAddress = streetAddress;
            return this;
        }

        public SignerInformationForEquifaxCanadaBuilder WithCity(string city)
        {
            this.city = city;
            return this;
        }

        public SignerInformationForEquifaxCanadaBuilder WithProvince(string state)
        {
            this.state = state;
            return this;
        }

        public SignerInformationForEquifaxCanadaBuilder WithPostalCode(string zip)
        {
            this.zip = zip;
            return this;
        }

        public SignerInformationForEquifaxCanadaBuilder WithTimeAtAddress(string timeAtAddress)
        {   
            this.timeAtAddress = timeAtAddress;
            return this;
        }

        public SignerInformationForEquifaxCanadaBuilder WithDriversLicenseIndicator(string driversLicenseIndicator)
        {   
            this.driversLicenseIndicator = driversLicenseIndicator;
            return this;
        }

        public SignerInformationForEquifaxCanadaBuilder WithSocialInsuranceNumber(string socialInsuranceNumber)
        {   
            this.socialInsuranceNumber = socialInsuranceNumber;
            return this;
        }

        public SignerInformationForEquifaxCanadaBuilder WithHomePhoneNumber(string homePhoneNumber)
        {   
            this.homePhoneNumber = homePhoneNumber;
            return this;
        }

        public SignerInformationForEquifaxCanadaBuilder WithDateOfBirth(Nullable<DateTime> dateOfBirth)
        {   
            this.dateOfBirth = dateOfBirth;
            return this;
        }

        public SignerInformationForEquifaxCanada Build()
        {
            SignerInformationForEquifaxCanada result = new SignerInformationForEquifaxCanada();

            result.FirstName = firstName;
            result.LastName = lastName;
            result.StreetAddress = streetAddress;
            result.City = city;
            result.Province = state;
            result.PostalCode = zip;
            result.TimeAtAddress = timeAtAddress;
            result.DriversLicenseIndicator = driversLicenseIndicator;
            result.SocialInsuranceNumber = socialInsuranceNumber;
            result.HomePhoneNumber = homePhoneNumber;
            result.DateOfBirth = dateOfBirth;

            return result;
        }
    }
}



