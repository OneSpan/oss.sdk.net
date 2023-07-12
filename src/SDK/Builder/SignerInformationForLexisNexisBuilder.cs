using System;
using OneSpanSign.Sdk.Internal;

namespace OneSpanSign.Sdk
{
    public class SignerInformationForLexisNexisBuilder
    {
        private string firstName;
        private string lastName;
        private string flatOrApartmentNumber;
        private string houseName;
        private string houseNumber;
        private string city;
        private string state;
        private string zip;
        private string socialSecurityNumber;
        private Nullable<DateTime> dateOfBirth;


        public static SignerInformationForLexisNexisBuilder NewSignerInformationForLexisNexis() {
            return new SignerInformationForLexisNexisBuilder();
        }

        public SignerInformationForLexisNexisBuilder WithFirstName(string firstName)
        {
            this.firstName = firstName;
            return this;
        }

        public SignerInformationForLexisNexisBuilder WithLastName(string lastName)
        {
            this.lastName = lastName;
            return this;
        }

        public SignerInformationForLexisNexisBuilder WithFlatOrApartmentNumber(string flatOrApartmentNumber)
        {
            this.flatOrApartmentNumber = flatOrApartmentNumber;
            return this;
        }
        
        public SignerInformationForLexisNexisBuilder WithHouseName(string houseName)
        {
            this.houseName = houseName;
            return this;
        }
        
        public SignerInformationForLexisNexisBuilder WithHouseNumber(string houseNumber)
        {
            this.houseNumber = houseNumber;
            return this;
        }

        public SignerInformationForLexisNexisBuilder WithCity(string city)
        {
            this.city = city;
            return this;
        }

        public SignerInformationForLexisNexisBuilder WithState(string state)
        {
            this.state = state;
            return this;
        }

        public SignerInformationForLexisNexisBuilder WithZip(string zip)
        {
            this.zip = zip;
            return this;
        }

        public SignerInformationForLexisNexisBuilder WithSocialSecurityNumber(string socialSecurityNumber)
        {   
            this.socialSecurityNumber = socialSecurityNumber;
            return this;
        }

        public SignerInformationForLexisNexisBuilder WithDateOfBirth(Nullable<DateTime> dateOfBirth)
        {   
            this.dateOfBirth = dateOfBirth;
            return this;
        }

        public SignerInformationForLexisNexis Build()
        {
            SignerInformationForLexisNexis result = new SignerInformationForLexisNexis();

            result.FirstName = firstName;
            result.LastName = lastName;
            result.FlatOrApartmentNumber = flatOrApartmentNumber;
            result.HouseName = houseName;
            result.HouseNumber = houseNumber;
            result.City = city;
            result.State = state;
            result.Zip = zip;
            result.SocialSecurityNumber = socialSecurityNumber;
            result.DateOfBirth = dateOfBirth;

            return result;
        }
    }
}
