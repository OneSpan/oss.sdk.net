using System;

namespace OneSpanSign.Sdk
{
    internal class ProviderConverter
    {
        private Provider sdkProvider;
        private OneSpanSign.API.Provider apiProvider;

        public ProviderConverter( Provider sdkProvider )
        {
            this.sdkProvider = sdkProvider;
        }

        public ProviderConverter( OneSpanSign.API.Provider apiProviders ) 
        {
            this.apiProvider = apiProviders;
        }

        public Provider ToSDKProvider() {
            if (sdkProvider != null)
            {
                return sdkProvider;
            }
            else if (apiProvider != null)
            {
                ProviderBuilder builder = ProviderBuilder.NewProvider(apiProvider.Name)
                    .WithId(apiProvider.Id)
                    .WithProvides(apiProvider.Provides)
                    .WithData(apiProvider.Data);
                return builder.Build();
            }
            else
            {
                return null;
            }
        }

        public OneSpanSign.API.Provider ToAPIProvider() {
            if (apiProvider != null)
            {
                return apiProvider;
            }
            else if (sdkProvider != null)
            {
                OneSpanSign.API.Provider result = new OneSpanSign.API.Provider();
                result.Name = sdkProvider.Name;
                result.Id = sdkProvider.Id;
                result.Data = sdkProvider.Data;
                result.Provides = sdkProvider.Provides;
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}

