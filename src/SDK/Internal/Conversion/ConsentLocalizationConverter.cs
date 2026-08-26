using System;
using OneSpanSign.API.Models;
using OneSpanSign.Sdk.Models;

namespace OneSpanSign.Sdk.Internal.Conversion
{
    public static class ConsentLocalizationConverter
    {
        public static ConsentLocalizationRequest ToApi(ConsentLocalizationPayload payload)
        {
            if (payload == null) throw new ArgumentNullException(nameof(payload));

            if (string.IsNullOrWhiteSpace(payload.Language))
            {
                throw new ArgumentException("Language cannot be null or empty.", nameof(payload));
            }

            return new ConsentLocalizationRequest(payload.Language);
        }
    }
}
