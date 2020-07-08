
namespace OneSpanSign.Sdk
{
    internal class CompanyConverter
    {
        private Company sdkCompany;
        private OneSpanSign.API.Company apiCompany;

        public CompanyConverter(Company sdkCompany)
        {
            this.sdkCompany = sdkCompany;
        }

        public CompanyConverter(OneSpanSign.API.Company apiCompany)
        {
            this.apiCompany = apiCompany;
        }

        public Company ToSDKCompany()
        {
            if (sdkCompany != null)
            {
                return sdkCompany;
            }
            else if (apiCompany != null)
            {
                CompanyBuilder builder = CompanyBuilder.NewCompany(apiCompany.Name)
                    .WithId(apiCompany.Id)
                    .WithAddress(new AddressConverter(apiCompany.Address).ToSDKAddress())
                    .WithData(apiCompany.Data);
                return builder.Build();
            }
            else
            {
                return null;
            }
        }

        public OneSpanSign.API.Company ToAPICompany()
        {
            if (apiCompany != null)
            {
                return apiCompany;
            }
            else if (sdkCompany != null)
            {
                OneSpanSign.API.Company result = new OneSpanSign.API.Company();
                result.Name = sdkCompany.Name;
                result.Id = sdkCompany.Id;
                result.Data = sdkCompany.Data;
                result.Address = new AddressConverter(sdkCompany.Address).ToAPIAddress();
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}