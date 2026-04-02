using System;

namespace OneSpanSign.Sdk.Models
{
    [Serializable]
    public class ConsentLocalizationPayload
    {
        public ConsentLocalizationPayload(string language)
        {
            Language = language ?? throw new ArgumentNullException(nameof(language));
        }

        public string Language { get; set; }
    }
}
