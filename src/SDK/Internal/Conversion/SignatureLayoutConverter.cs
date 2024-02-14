using OneSpanSign.Sdk.Builder;

namespace OneSpanSign.Sdk
{
    internal class SignatureLayoutConverter
    {
        private OneSpanSign.API.SignatureLayout apiSignatureLayout = null;
        private OneSpanSign.Sdk.SignatureLayout sdkSignatureLayout = null;

        public SignatureLayoutConverter (OneSpanSign.API.SignatureLayout apiSignatureLayout)
        {
            this.apiSignatureLayout = apiSignatureLayout;
        }

        public SignatureLayoutConverter (OneSpanSign.Sdk.SignatureLayout sdkSignatureLayout)
        {
            this.sdkSignatureLayout = sdkSignatureLayout;
        }

        public OneSpanSign.API.SignatureLayout ToAPISignatureLayout ()
        {
            if (sdkSignatureLayout == null)
            {
                return apiSignatureLayout;
            }

            OneSpanSign.API.SignatureLayout result = new OneSpanSign.API.SignatureLayout ();
            
            result.SignatureLogo = new SignatureLogoConverter(sdkSignatureLayout.Logo).ToAPISignatureLogo();

            
            return result;
        }

        public OneSpanSign.Sdk.SignatureLayout ToSDKSignatureLayout ()
        {
            if (apiSignatureLayout == null)
            {
                return sdkSignatureLayout;
            }

            OneSpanSign.Sdk.SignatureLayout result = SignatureLayoutBuilder.NewSignatureLayoutBuilder ()
               .Build ();
            
            result.Logo = new SignatureLogoConverter(apiSignatureLayout.SignatureLogo).ToSDKSignatureLogo();
            
            return result;
        }
    }
}
