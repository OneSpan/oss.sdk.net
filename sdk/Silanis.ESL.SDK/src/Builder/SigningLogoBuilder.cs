using System;
using Silanis.ESL.SDK.Internal;

namespace Silanis.ESL.SDK.Builder
{
    public class SigningLogoBuilder
    {
        private string language;
        private string image;

        public SigningLogoBuilder WithLanguage (string language)
        {
            this.language = language;
            return this;
        }

       
        public SigningLogoBuilder WithImage (string image)
        {
            this.image = image;
            return this;
        }

     
        public SigningLogo Build ()
        {
            SigningLogo result = new SigningLogo ();
            result.Language = language;
            result.Image = image;
            return result;
        }

      
        public static SigningLogoBuilder NewSigningLogo ()
        {
            return new SigningLogoBuilder ();
        }

    }
}