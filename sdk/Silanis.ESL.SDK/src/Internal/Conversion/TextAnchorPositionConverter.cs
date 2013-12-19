using System;
using System.Collections.Generic;
using System.Text;

namespace Silanis.ESL.SDK.src.Internal.Conversion
{
    class TextAnchorPositionConverter
    {
        private Nullable<TextAnchorPosition> sdkPosition = null;
        private String apiAnchorPoint = null;

        public TextAnchorPositionConverter(TextAnchorPosition sdkPosition)
        {
            this.sdkPosition = sdkPosition;
        }

        public TextAnchorPositionConverter(String apiAnchorPoint)
        {
            this.apiAnchorPoint = apiAnchorPoint;
        }

        public TextAnchorPosition ToSDKTextAnchorPosition()
        {
            if (sdkPosition != null)
            {
                return sdkPosition.Value;
            }
            else
            {
                return (TextAnchorPosition)Enum.Parse(typeof(TextAnchorPosition), apiAnchorPoint);
            }
        }

        public String ToAPIAnchorPoint()
        {
            if (apiAnchorPoint != null)
            {
                return apiAnchorPoint;
            }
            else
            {
                return sdkPosition.ToString();
            }
        }
    }
}
