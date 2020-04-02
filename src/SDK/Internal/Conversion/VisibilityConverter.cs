using System;

namespace OneSpanSign.Sdk
{
    internal class VisibilityConverter
    {
        private OneSpanSign.Sdk.Visibility sdkVisibility;
        private string apiVisibility;

        public VisibilityConverter(string apiVisibility)
        {
            this.apiVisibility = apiVisibility;
        }

        public VisibilityConverter(OneSpanSign.Sdk.Visibility sdkVisibility)
        {
            this.sdkVisibility = sdkVisibility;
        }

        public string ToAPIVisibility()
        {
            return sdkVisibility.getApiValue();
        }

        public OneSpanSign.Sdk.Visibility ToSDKVisibility()
        {
            return OneSpanSign.Sdk.Visibility.valueOf(apiVisibility);
        }
    }
}

