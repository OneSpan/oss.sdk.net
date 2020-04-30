using System;
using System.Xml.XPath;

namespace OneSpanSign.Sdk
{
    internal class AccessibleAccountResponseConverter
    {
        private AccessibleAccountResponse sdkAccessibleAccountResponse;
        private OneSpanSign.API.AccessibleAccountResponse apiAccessibleAccountResponse;

        public AccessibleAccountResponseConverter( AccessibleAccountResponse sdkAccessibleAccountResponse )
        {
            this.sdkAccessibleAccountResponse = sdkAccessibleAccountResponse;
        }

        public AccessibleAccountResponseConverter( OneSpanSign.API.AccessibleAccountResponse apiAccessibleAccountResponse ) 
        {
            this.apiAccessibleAccountResponse = apiAccessibleAccountResponse;
        }

        public AccessibleAccountResponse ToSDKAccessibleAccountResponse() {
            if (sdkAccessibleAccountResponse != null)
            {
                return sdkAccessibleAccountResponse;
            }
            else if (apiAccessibleAccountResponse != null)
            {
                AccessibleAccountResponse accessibleAccountResponse = new AccessibleAccountResponse();
                accessibleAccountResponse.AccountName = apiAccessibleAccountResponse.AccountName;
                accessibleAccountResponse.AccountUid = apiAccessibleAccountResponse.AccountUid;
                return accessibleAccountResponse;
            }
            else
            {
                return null;
            }
        }

        public OneSpanSign.API.AccessibleAccountResponse ToAPIAccessibleAccountResponse() {
            if (apiAccessibleAccountResponse != null)
            {
                return apiAccessibleAccountResponse;
            }
            else if (sdkAccessibleAccountResponse != null)
            {
                OneSpanSign.API.AccessibleAccountResponse result = new OneSpanSign.API.AccessibleAccountResponse();
                result.AccountName = sdkAccessibleAccountResponse.AccountName;
                result.AccountUid = sdkAccessibleAccountResponse.AccountUid;
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}

