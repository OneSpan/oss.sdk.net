using System;
using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Tests
{
    [TestFixture]
    public class SignerInformationForLexisNexisBuilderTest
    {
        private string firstName = "First name";
        private string lastName = "Last name";
        private string flatOrApartmentNumber = "1234";
        private string houseName = "Decarie";
        private string houseNumber = "5678";
        private string city = "Montreal";
        private string state = "Quebec";
        private string zip = "A2A 3B3";
        private string socialSecurityNumber = "111-222-333-444";

        private Nullable<DateTime> dateOfBirth = new DateTime();

        [Test]
        public void BuildWithSpecificValues() {
            SignerInformationForLexisNexis signerInformationForLexisNexis = SignerInformationForLexisNexisBuilder.NewSignerInformationForLexisNexis()
                .WithFirstName(firstName)
                .WithLastName(lastName)
                .WithFlatOrApartmentNumber(flatOrApartmentNumber)
                .WithHouseName(houseName)
                .WithHouseNumber(houseNumber)
                .WithCity(city)
                .WithState(state)
                .WithZip(zip)
                .WithSocialSecurityNumber(socialSecurityNumber)
                .WithDateOfBirth(dateOfBirth)
                .Build();

            Assert.AreEqual(firstName, signerInformationForLexisNexis.FirstName);
            Assert.AreEqual(lastName, signerInformationForLexisNexis.LastName);
            Assert.AreEqual(flatOrApartmentNumber, signerInformationForLexisNexis.FlatOrApartmentNumber);
            Assert.AreEqual(houseName, signerInformationForLexisNexis.HouseName);
            Assert.AreEqual(houseNumber, signerInformationForLexisNexis.HouseNumber);
            Assert.AreEqual(city, signerInformationForLexisNexis.City);
            Assert.AreEqual(state, signerInformationForLexisNexis.State);
            Assert.AreEqual(zip, signerInformationForLexisNexis.Zip);
            Assert.AreEqual(socialSecurityNumber, signerInformationForLexisNexis.SocialSecurityNumber);
            Assert.AreEqual(dateOfBirth, signerInformationForLexisNexis.DateOfBirth);
        }
    }
}

