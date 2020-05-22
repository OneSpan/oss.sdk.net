using System;
using OneSpanSign.Sdk.Builder;

namespace OneSpanSign.Sdk
{
    internal class DelegationUserConverter
    {
        private OneSpanSign.API.DelegationUser apiDelegationUser;
        private DelegationUser sdkDelegationUser;

        public DelegationUserConverter( DelegationUser sdkDelegationUser ) {
            this.sdkDelegationUser = sdkDelegationUser;
            this.apiDelegationUser = null;
        }

        public DelegationUserConverter( OneSpanSign.API.DelegationUser apiDelegationUser ) {
            this.apiDelegationUser = apiDelegationUser;
            this.sdkDelegationUser = null;
        }

        public OneSpanSign.API.DelegationUser ToAPIDelegationUser()
        {
            if (sdkDelegationUser == null)
            {
                return apiDelegationUser;
            }
            OneSpanSign.API.DelegationUser result = new OneSpanSign.API.DelegationUser();

            result.Email = sdkDelegationUser.Email;
            result.FirstName = sdkDelegationUser.FirstName;
            result.Id = sdkDelegationUser.Id;
            result.LastName = sdkDelegationUser.LastName;
            result.Name = sdkDelegationUser.Name;
            result.ExpiryDate = sdkDelegationUser.ExpiryDate;

            return result;
        }

        public DelegationUser ToSDKDelegationUser()
        {
            if (apiDelegationUser == null)
            {
                return sdkDelegationUser;
            }

            return DelegationUserBuilder.NewDelegationUser(apiDelegationUser.Email)
                    .WithFirstName(apiDelegationUser.FirstName)
                    .WithId(apiDelegationUser.Id)
                    .WithLastName(apiDelegationUser.LastName)
                    .WithName(apiDelegationUser.Name)
                    .WithExpiryDate(apiDelegationUser.ExpiryDate)
                    .Build();
        }
    }
}

