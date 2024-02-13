namespace OneSpanSign.Sdk.Builder
{
    public class SignatureLayoutBuilder
    {
        private SignatureLogo logo;

        public SignatureLayoutBuilder WithLogo (SignatureLogo logo)
        {
            this.logo = logo;
            return this;
        }

     
        public SignatureLayout Build ()
        {
            SignatureLayout result = new SignatureLayout ();
            result.Logo = logo;
            return result;
        }

      
        public static SignatureLayoutBuilder NewSignatureLayoutBuilder ()
        {
            return new SignatureLayoutBuilder ();
        }

    }
}