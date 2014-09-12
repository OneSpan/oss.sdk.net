using System;

namespace Silanis.ESL.SDK
{
    public class SignerInformationForEquifaxUSA
    {
        public SignerInformationForEquifaxUSA()
        {
        }

        public SignerInformationForEquifaxUSA(
            string firstName,
            string lastName,
            string streetAddress,
            string city,
            string state,
            string zip,
            string socialSecurityNumber,
            string homePhoneNumber,
            DateTime dateOfBirth)
        {
            FirstName = firstName;
            LastName = lastName;
            StreetAddress = streetAddress;
            City = city;
            State = state;
            Zip = zip;
            SocialSecurityNumber = socialSecurityNumber;
            HomePhoneNumber = homePhoneNumber;
            DateOfBirth = dateOfBirth;
        }

        public SignerInformationForEquifaxUSA(
            string firstName,
            string lastName,
            string streetAddress,
            string city,
            string state,
            string zip,
            string socialInsuranceNumber,
            DateTime dateOfBirth)
        {
            FirstName = firstName;
            LastName = lastName;
            StreetAddress = streetAddress;
            City = city;
            State = state;
            Zip = zip;
            SocialSecurityNumber = socialInsuranceNumber;
            DateOfBirth = dateOfBirth;
        }

        public string City {
            get;
            set;
        }

        public Nullable<DateTime> DateOfBirth {
            get;
            set;
        }

        public string FirstName {
            get;
            set;
        }

        public string HomePhoneNumber {
            get;
            set;
        }

        public string LastName {
            get;
            set;
        }

        public string SocialSecurityNumber {
            get;
            set;
        }

        public string State {
            get;
            set;
        }

        public string StreetAddress {
            get;
            set;
        }

        public string Zip {
            get;
            set;
        }

    }
}