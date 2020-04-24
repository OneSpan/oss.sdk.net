using System;

namespace OneSpanSign.Sdk
{
    internal class LicenseConverter
    {
        private License sdkLicense;
        private OneSpanSign.API.License apiLicense;

        public LicenseConverter( License sdkLicense )
        {
            this.sdkLicense = sdkLicense;
        }

        public LicenseConverter( OneSpanSign.API.License apiLicense ) 
        {
            this.apiLicense = apiLicense;
        }

        public License ToSDKLicense() {
            if (sdkLicense != null)
            {
                return sdkLicense;
            }
            else if (apiLicense != null)
            {
                LicenseBuilder builder = LicenseBuilder.NewLicense()
                    .CreatedOn(apiLicense.Created)
                    .WithStatus(apiLicense.Status)
                    .WithPaidUntil(apiLicense.PaidUntil)
                    .WithPlan(new PlanConverter(apiLicense.Plan).ToSDKPlan());
                foreach (API.Transaction apiTransaction in apiLicense.Transactions)
                {
                    builder.WithTransaction(new TransactionConverter(apiTransaction).ToSDKTransaction());
                }
                return builder.Build();
            }
            else
            {
                return null;
            }
        }

        public OneSpanSign.API.License ToAPILicense() {
            if (apiLicense != null)
            {
                return apiLicense;
            }
            else if (sdkLicense != null)
            {
                OneSpanSign.API.License result = new OneSpanSign.API.License();
                result.Created = sdkLicense.Created;
                result.Status = sdkLicense.Status;
                result.PaidUntil = sdkLicense.PaidUntil;
                result.Plan = new PlanConverter(sdkLicense.Plan).ToAPIPlan();
                foreach (Transaction transaction in sdkLicense.Transactions)
                {
                    result.AddTransaction(new TransactionConverter(transaction).ToAPITransaction());
                }
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}

