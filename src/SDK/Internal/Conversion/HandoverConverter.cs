using OneSpanSign.API;
using OneSpanSign.Sdk.Builder;

namespace OneSpanSign.Sdk.Internal.Conversion
{
    public class HandoverConverter
    {
        private Link apiLink;
        private Handover sdkHandover;

        internal HandoverConverter(Link apiLink)
        {
            this.apiLink = apiLink;
        }

        public HandoverConverter(Handover sdkHandover)
        {
            this.sdkHandover = sdkHandover;
        }

        internal Link ToAPILink()
        {
            if (sdkHandover == null)
            {
                return apiLink;
            }

            Link result = new Link();
            result.Href = sdkHandover.Href;
            result.Text = sdkHandover.Text;
            result.Title = sdkHandover.Title;

            return result;
        }

        public Handover ToSDKHandover(string language)
        {
            if (apiLink == null)
            {
                return sdkHandover;
            }

            return HandoverBuilder
                .NewHandover(language)
                .WithHref(apiLink.Href)
                .WithText(apiLink.Text)
                .WithTitle(apiLink.Title)
                .Build();
        }
    }
}