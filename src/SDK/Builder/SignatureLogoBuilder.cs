namespace OneSpanSign.Sdk.Builder
{
    public class SignatureLogoBuilder
    {
        private string image;

        public SignatureLogoBuilder WithImage (string image)
        {
            this.image = image;
            return this;
        }

     
        public SignatureLogo Build ()
        {
            SignatureLogo result = new SignatureLogo ();
            result.Image = image;
            return result;
        }

      
        public static SignatureLogoBuilder NewSignatureLogoBuilder ()
        {
            return new SignatureLogoBuilder ();
        }

    }
}