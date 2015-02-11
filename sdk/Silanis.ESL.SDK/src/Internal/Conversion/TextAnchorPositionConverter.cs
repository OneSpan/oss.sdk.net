using System;
using System.Collections.Generic;
using System.Text;

namespace Silanis.ESL.SDK.src.Internal.Conversion
{
    class TextAnchorPositionConverter
    {
        private TextAnchorPosition sdkPosition = null;
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
                return sdkPosition;
            }
            else
            {
                return TextAnchorPosition.valueOf(apiAnchorPoint);
            }
        }

        public String ToAPIAnchorPoint()
        {
            if (apiAnchorPoint != null)
            {
                return apiAnchorPoint;
            }
            return sdkPosition.getApiValue();
        }
    }
}
