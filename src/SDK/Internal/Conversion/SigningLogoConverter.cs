using System;
using OneSpanSign.Sdk.Builder;
using OneSpanSign.Sdk.Internal;

namespace OneSpanSign.Sdk
{
    internal class SigningLogoConverter
    {
        private OneSpanSign.API.SigningLogo apiSigningLogo = null;
        private OneSpanSign.Sdk.SigningLogo sdkSigningLogo = null;

        public SigningLogoConverter (OneSpanSign.API.SigningLogo apiSigningLogo)
        {
            this.apiSigningLogo = apiSigningLogo;
        }

        public SigningLogoConverter (OneSpanSign.Sdk.SigningLogo sdkSigningLogo)
        {
            this.sdkSigningLogo = sdkSigningLogo;
        }

        public OneSpanSign.API.SigningLogo ToAPISigningLogo ()
        {
            if (sdkSigningLogo == null)
            {
                return apiSigningLogo;
            }

            OneSpanSign.API.SigningLogo result = new OneSpanSign.API.SigningLogo ();
            result.Language = sdkSigningLogo.Language;
            result.Image = sdkSigningLogo.Image;

            return result;
        }

        public OneSpanSign.Sdk.SigningLogo ToSDKSigningLogo ()
        {
            if (apiSigningLogo == null)
            {
                return sdkSigningLogo;
            }

            OneSpanSign.Sdk.SigningLogo result = SigningLogoBuilder.NewSigningLogo ()
               .WithLanguage(apiSigningLogo.Language)
               .WithImage (apiSigningLogo.Image)
               .Build ();

            return result;
        }
    }
}
