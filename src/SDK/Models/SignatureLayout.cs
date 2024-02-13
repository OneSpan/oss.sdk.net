using Newtonsoft.Json;

namespace OneSpanSign.API
{

    internal class SignatureLayout
    {
        //Fields
        
        // Accessors    
        [JsonProperty ("logo")]
        public SignatureLogo SignatureLogo
        {
            get; set;
        }
    }
}