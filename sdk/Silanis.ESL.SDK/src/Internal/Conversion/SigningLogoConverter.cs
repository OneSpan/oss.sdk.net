using System;
using Silanis.ESL.SDK.Builder;
using Silanis.ESL.SDK.Internal;

namespace Silanis.ESL.SDK
{
    internal class SigningLogoConverter
    {
        private Silanis.ESL.API.SigningLogo apiSigningLogo = null;
        private Silanis.ESL.SDK.SigningLogo sdkSigningLogo = null;

        public SigningLogoConverter (Silanis.ESL.API.SigningLogo apiSigningLogo)
        {
            this.apiSigningLogo = apiSigningLogo;
        }

        public SigningLogoConverter (Silanis.ESL.SDK.SigningLogo sdkSigningLogo)
        {
            this.sdkSigningLogo = sdkSigningLogo;
        }

        public Silanis.ESL.API.SigningLogo ToAPISigningLogo ()
        {
            if (sdkSigningLogo == null)
            {
                return apiSigningLogo;
            }

            Silanis.ESL.API.SigningLogo result = new Silanis.ESL.API.SigningLogo ();
            result.Language = sdkSigningLogo.Language;
            result.Image = sdkSigningLogo.Image;

            return result;
        }

        public Silanis.ESL.SDK.SigningLogo ToSDKSigningLogo ()
        {
            if (apiSigningLogo == null)
            {
                return sdkSigningLogo;
            }

            Silanis.ESL.SDK.SigningLogo result = SigningLogoBuilder.NewSigningLogo ()
               .WithLanguage(apiSigningLogo.Language)
               .WithImage (apiSigningLogo.Image)
               .Build ();

            return result;
        }
    }
}
