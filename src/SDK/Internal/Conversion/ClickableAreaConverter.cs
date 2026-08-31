using OneSpanSign.API;
using System;

namespace OneSpanSign.Sdk.src.Internal.Conversion
{
    /// <summary>
    /// Converter between SDK ClickableArea and API FieldClickableArea.
    /// </summary>
    class ClickableAreaConverter
    {
        private ClickableArea sdkClickableArea = null;
        private FieldClickableArea apiClickableArea = null;

        public ClickableAreaConverter(ClickableArea sdkClickableArea)
        {
            this.sdkClickableArea = sdkClickableArea;
        }

        public ClickableAreaConverter(FieldClickableArea apiClickableArea)
        {
            this.apiClickableArea = apiClickableArea;
        }

        public FieldClickableArea ToAPIFieldClickableArea()
        {
            if (sdkClickableArea == null)
            {
                return apiClickableArea;
            }

            FieldClickableArea result = new FieldClickableArea();

            result.Width = sdkClickableArea.Width;
            result.Height = sdkClickableArea.Height;
            if (sdkClickableArea.Alignment.HasValue)
            {
                result.Alignment = sdkClickableArea.Alignment.Value.ToString();
            }

            return result;
        }

        public ClickableArea ToSDKClickableArea()
        {
            if (apiClickableArea == null)
            {
                return sdkClickableArea;
            }

            ClickableArea result = new ClickableArea();

            if (apiClickableArea.Width.HasValue)
                result.Width = apiClickableArea.Width;
            if (apiClickableArea.Height.HasValue)
                result.Height = apiClickableArea.Height;
            if (apiClickableArea.Alignment != null)
                result.Alignment = (ClickableAreaAlignment)Enum.Parse(typeof(ClickableAreaAlignment), apiClickableArea.Alignment);

            return result;
        }
    }
}
