using System;

namespace OneSpanSign.Sdk
{
    internal class AccountProvidersConverter
    {
        private AccountProviders sdkAccountProviders;
        private OneSpanSign.API.AccountProviders apiAccountProviders;

        public AccountProvidersConverter( AccountProviders sdkAccountProviders )
        {
            this.sdkAccountProviders = sdkAccountProviders;
        }

        public AccountProvidersConverter( OneSpanSign.API.AccountProviders apiAccountProviders ) 
        {
            this.apiAccountProviders = apiAccountProviders;
        }

        public AccountProviders ToSDKAccountProviders() {
            if (sdkAccountProviders != null)
            {
                return sdkAccountProviders;
            }
            else if (apiAccountProviders != null)
            {
                AccountProviders accountProviders = new AccountProviders();
                foreach (API.Provider provider in apiAccountProviders.Documents)
                {
                    accountProviders.AddDocument(new ProviderConverter(provider).ToSDKProvider());
                }

                foreach (API.Provider provider in apiAccountProviders.Users)
                {
                    accountProviders.AddUser(new ProviderConverter(provider).ToSDKProvider());
                }

                return accountProviders;
            }
            else
            {
                return null;
            }
        }

        public OneSpanSign.API.AccountProviders ToAPIAccountProviders() {
            if (apiAccountProviders != null)
            {
                return apiAccountProviders;
            }
            else if (sdkAccountProviders != null)
            {
                OneSpanSign.API.AccountProviders result = new OneSpanSign.API.AccountProviders();
                foreach (Provider provider in sdkAccountProviders.Documents)
                {
                    result.AddDocument(new ProviderConverter(provider).ToAPIProvider());
                }

                foreach (Provider provider in sdkAccountProviders.Users)
                {
                    result.AddUser(new ProviderConverter(provider).ToAPIProvider());
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

