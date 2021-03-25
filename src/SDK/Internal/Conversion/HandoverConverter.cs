using OneSpanSign.API;
using OneSpanSign.Sdk.Builder;

namespace OneSpanSign.Sdk.Internal.Conversion
{
    public class HandoverConverter
    {
        private OneSpanSign.API.Handover apiHandover;
        private OneSpanSign.Sdk.Handover sdkHandover;

        internal HandoverConverter(OneSpanSign.API.Handover apiHandover)
        {
            this.apiHandover = apiHandover;
        }

        public HandoverConverter(OneSpanSign.Sdk.Handover sdkHandover)
        {
            this.sdkHandover = sdkHandover;
        }

        internal OneSpanSign.API.Handover ToAPIHandover()
        {
            if (sdkHandover == null)
            {
                return apiHandover;
            }

            OneSpanSign.API.Handover result = new OneSpanSign.API.Handover();
            result.Href = sdkHandover.Href;
            result.Text = sdkHandover.Text;
            result.Title = sdkHandover.Title;

            return result;
        }

        public OneSpanSign.Sdk.Handover ToSDKHandover(string language)
        {
            if (apiHandover == null)
            {
                return sdkHandover;
            }

            return HandoverBuilder
                .NewHandover(language)
                .WithHref(apiHandover.Href)
                .WithText(apiHandover.Text)
                .WithTitle(apiHandover.Title)
                .Build();
        }
    }
}