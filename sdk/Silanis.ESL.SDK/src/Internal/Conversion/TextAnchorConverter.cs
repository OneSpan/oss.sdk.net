using Silanis.ESL.API;
using System;
using System.Collections.Generic;
using System.Text;

namespace Silanis.ESL.SDK.src.Internal.Conversion
{
    class TextAnchorConverter
    {
        private TextAnchor sdkTextAnchor = null;
        private ExtractAnchor apiExtractAnchor = null;

        public TextAnchorConverter(TextAnchor sdkTextAnchor)
        {
            this.sdkTextAnchor = sdkTextAnchor;
        }

        public TextAnchorConverter(ExtractAnchor apiExtractAnchor)
        {
            this.apiExtractAnchor = apiExtractAnchor;
        }

        public TextAnchor ToSDKTextAnchor()
        {
            if (apiExtractAnchor == null)
            {
                return sdkTextAnchor;
            }
            else
            {
                TextAnchor result = new TextAnchor();

                result.Position = new TextAnchorPositionConverter(apiExtractAnchor.AnchorPoint).ToSDKTextAnchorPosition();
                result.Occurrence = apiExtractAnchor.Index;
                result.AnchorText = apiExtractAnchor.Text;
                result.Character = apiExtractAnchor.CharacterIndex;
                result.XOffset = apiExtractAnchor.LeftOffset;
                result.YOffset = apiExtractAnchor.TopOffset;
                result.Width = apiExtractAnchor.Width;
                result.Height = apiExtractAnchor.Height;

                return result;
            }
        }

        public ExtractAnchor ToAPIExtractAnchor()
        {
            if (sdkTextAnchor == null)
            {
                return apiExtractAnchor;
            }
            else
            {
                ExtractAnchor result = new ExtractAnchor();

                result.LeftOffset = sdkTextAnchor.XOffset;
                result.TopOffset = sdkTextAnchor.YOffset;
                result.Text = sdkTextAnchor.AnchorText;
                result.Index = sdkTextAnchor.Occurrence;
                result.CharacterIndex = sdkTextAnchor.Character;
                result.AnchorPoint = new TextAnchorPositionConverter(sdkTextAnchor.Position).ToAPIAnchorPoint();
                result.Width = sdkTextAnchor.Width;
                result.Height = sdkTextAnchor.Height;

                return result;
            }
        }
    }
}
