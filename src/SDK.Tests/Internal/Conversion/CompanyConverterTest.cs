using System;
using NUnit.Framework;
using OneSpanSign.Sdk;
using System.Collections.Generic;

namespace SDK.Tests
{
    [TestFixture()]
    public class CompanyConverterTest
    {
        private Company sdkCompany = null;
        private OneSpanSign.API.Company apiCompany = null;
        private CompanyConverter converter = null;

        private static readonly string CO_NAME = "company_name";
        private static readonly string CO_ID = "company_id";

        private static readonly IDictionary<string, object> CO_DATA = new Dictionary<string, object>()
            {{"account_company_data_0_key", "account_company_data_0_value"}};

        private static readonly string CO_ADDR_ADDR1 = "company_address_address1";
        private static readonly string CO_ADDR_ADDR2 = "company_address_address2";
        private static readonly string CO_ADDR_CITY = "company_address_city";
        private static readonly string CO_ADDR_COUNTRY = "company_address_country";
        private static readonly string CO_ADDR_STATE = "company_address_state";
        private static readonly string CO_ADDR_ZIP = "company_address_zipcode";

        [Test()]
        public void ConvertNullAPIToSDK()
        {
            apiCompany = null;
            converter = new CompanyConverter(apiCompany);
            Assert.IsNull(converter.ToSDKCompany());
        }

        [Test()]
        public void ConvertNullSDKToSDK()
        {
            sdkCompany = null;
            converter = new CompanyConverter(sdkCompany);
            Assert.IsNull(converter.ToSDKCompany());
        }

        [Test()]
        public void ConvertSDKToSDK()
        {
            sdkCompany = CreateTypicalSDKCompany();
            converter = new CompanyConverter(sdkCompany);
            Company company = converter.ToSDKCompany();

            Assert.IsNotNull(company);
            Assert.AreEqual(sdkCompany, company);
        }

        [Test()]
        public void ConvertAPIToSDK()
        {
            apiCompany = CreateTypicalAPICompany();
            converter = new CompanyConverter(apiCompany);
            Company company = converter.ToSDKCompany();

            Assert.IsNotNull(company);
            Assert.AreEqual(CO_ID, company.Id);
            Assert.AreEqual(CO_ADDR_ADDR1, company.Address.Address1);
        }

        [Test()]
        public void ConvertNullSDKToAPI()
        {
            sdkCompany = null;
            converter = new CompanyConverter(sdkCompany);

            Assert.IsNull(converter.ToAPICompany());
        }

        [Test()]
        public void ConvertNullAPIToAPI()
        {
            apiCompany = null;
            converter = new CompanyConverter(apiCompany);

            Assert.IsNull(converter.ToAPICompany());
        }

        [Test()]
        public void ConvertAPIToAPI()
        {
            apiCompany = CreateTypicalAPICompany();
            converter = new CompanyConverter(apiCompany);

            OneSpanSign.API.Company company = converter.ToAPICompany();

            Assert.IsNotNull(company);
            Assert.AreEqual(apiCompany, company);
        }

        [Test()]
        public void ConvertSDKToAPI()
        {
            sdkCompany = CreateTypicalSDKCompany();
            converter = new CompanyConverter(sdkCompany);

            OneSpanSign.API.Company company = converter.ToAPICompany();

            Assert.IsNotNull(company);
            Assert.AreEqual(CO_ID, company.Id);
            Assert.AreEqual(CO_ADDR_ADDR1, company.Address.Address1);
        }

        private Company CreateTypicalSDKCompany()
        {
            Company company = new Company();
            company.Id = CO_ID;
            company.Name = CO_NAME;
            company.Data = CO_DATA;
            Address address = new Address();
            address.Address1 = CO_ADDR_ADDR1;
            address.Address2 = CO_ADDR_ADDR2;
            address.City = CO_ADDR_CITY;
            address.Country = CO_ADDR_COUNTRY;
            address.State = CO_ADDR_STATE;
            address.ZipCode = CO_ADDR_ZIP;
            company.Address = address;

            return company;
        }

        private OneSpanSign.API.Company CreateTypicalAPICompany()
        {
            OneSpanSign.API.Company company = new OneSpanSign.API.Company();
            company.Id = CO_ID;
            company.Name = CO_NAME;
            company.Data = CO_DATA;
            OneSpanSign.API.Address address = new OneSpanSign.API.Address();
            address.Address1 = CO_ADDR_ADDR1;
            address.Address2 = CO_ADDR_ADDR2;
            address.City = CO_ADDR_CITY;
            address.Country = CO_ADDR_COUNTRY;
            address.State = CO_ADDR_STATE;
            address.Zipcode = CO_ADDR_ZIP;
            company.Address = address;

            return company;
        }
    }
}