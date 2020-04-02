using System;

namespace OneSpanSign.Sdk
{
    public class Translation
    {
        public Translation()
        {
        }
        public string Name 
        {
                get;
                set;
        }
        public string Language 
        {
                get;
                set;
        }
        public string Description 
        {
                get;
                set;
        }
        
        internal OneSpanSign.API.Translation toAPITranslation() 
        {
            OneSpanSign.API.Translation result = new OneSpanSign.API.Translation();
			result.Id = "";
            result.Name = Name;
            result.Language = Language;
            result.Description = Description;
            return result;
        }        
    }
}

