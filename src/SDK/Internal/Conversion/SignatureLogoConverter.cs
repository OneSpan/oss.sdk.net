using System;
using OneSpanSign.Sdk.Builder;
using OneSpanSign.Sdk.Internal;

namespace OneSpanSign.Sdk
{
    internal class SignatureLogoConverter
    {
        private OneSpanSign.API.SignatureLogo apiSignatureLogo = null;
        private OneSpanSign.Sdk.SignatureLogo sdkSignatureLogo = null;

        public SignatureLogoConverter (OneSpanSign.API.SignatureLogo apiSignatureLogo)
        {
            this.apiSignatureLogo = apiSignatureLogo;
        }

        public SignatureLogoConverter (OneSpanSign.Sdk.SignatureLogo sdkSignatureLogo)
        {
            this.sdkSignatureLogo = sdkSignatureLogo;
        }

        public OneSpanSign.API.SignatureLogo ToAPISignatureLogo ()
        {
            if (sdkSignatureLogo == null)
            {
                return apiSignatureLogo;
            }

            OneSpanSign.API.SignatureLogo result = new OneSpanSign.API.SignatureLogo ();
            result.Image = sdkSignatureLogo.Image;

            return result;
        }

        public OneSpanSign.Sdk.SignatureLogo ToSDKSignatureLogo ()
        {
            if (apiSignatureLogo == null)
            {
                return sdkSignatureLogo;
            }

            OneSpanSign.Sdk.SignatureLogo result = SignatureLogoBuilder.NewSignatureLogoBuilder ()
               .WithImage (apiSignatureLogo.Image)
               .Build ();

            return result;
        }
    }
}
