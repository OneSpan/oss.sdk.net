using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
    public class SupportedLanguages
    {
        private string _defaultSignerLanguage;
        private List<string> _signerLanguages;
        
        public string DefaultSignerLanguage
        {
            get
            {
                return _defaultSignerLanguage;
            }
            set
            {
                _defaultSignerLanguage = value;
            }
        }
        
        public List<string> SignerLanguages
        {
            get
            {
                return _signerLanguages;
            }
            set
            {
                _signerLanguages = value;
            }
        }
    }
}