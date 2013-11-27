using System;
using System.Collections.Generic;
using Silanis.ESL.SDK.Internal.Conversion;

namespace Silanis.ESL.SDK
{
    public class CustomField
    {
        private List<Translation> translations = new List<Translation> ();

        public CustomField()
        {
        }
        public string Id { 
                get;
                set;
        }

        public string Value {
                get;
                set;
        }

        public bool Required {
                get;
                set;
        }

        public void AddTranslations (IList<Translation> translations)
        {
                this.translations.AddRange (translations);
        }

        internal Silanis.ESL.API.CustomField toAPICustomField() {
            Silanis.ESL.API.CustomField result = new Silanis.ESL.API.CustomField();
    
            result.Id = Id;
            result.Value = Value;
    
            foreach (Translation translation in translations) 
            {
                    result.AddTranslation (translation.toAPITranslation());
            }
    
            return result;
        }

    }
}

