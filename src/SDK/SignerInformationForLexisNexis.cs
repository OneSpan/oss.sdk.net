using System;

namespace OneSpanSign.Sdk
{
    public class SignerInformationForLexisNexis
    {
        public SignerInformationForLexisNexis()
        {
        }

        public SignerInformationForLexisNexis(
            string firstName,
            string lastName,
            string flatOrApartmentNumber,
            string houseName,
            string houseNumber,
            string city,
            string state,
            string zip,
            string socialSecurityNumber,
            DateTime dateOfBirth)
        {
            FirstName = firstName;
            LastName = lastName;
            FlatOrApartmentNumber = flatOrApartmentNumber;
            HouseName = houseName;
            HouseNumber = houseNumber;
            City = city;
            State = state;
            Zip = zip;
            SocialSecurityNumber = socialSecurityNumber;
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

        public string FlatOrApartmentNumber {
            get;
            set;
        }
        
        public string HouseName {
            get;
            set;
        }
        
        public string HouseNumber {
            get;
            set;
        }
        public string Zip {
            get;
            set;
        }
    }
}