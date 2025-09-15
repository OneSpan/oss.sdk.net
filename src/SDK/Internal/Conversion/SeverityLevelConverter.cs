using System;

namespace OneSpanSign.Sdk
{
    internal class SeverityLevelConverter
    {
        private SeverityLevel sdkSeverityLevel;
        private OneSpanSign.API.SeverityLevel apiSeverityLevel;

        /// <summary>
        /// Construct with API SeverityLevel object involved in conversion.
        /// </summary>
        /// <param name="apiSeverityLevel">API sender type.</param>
        public SeverityLevelConverter(OneSpanSign.API.SeverityLevel apiSeverityLevel)
        {
            this.apiSeverityLevel = apiSeverityLevel;
        }

        /// <summary>
        /// Construct with SDK SeverityLevel object involved in conversion.
        /// </summary>
        /// <param name="sdkSeverityLevel">SDK sender type.</param>
        public SeverityLevelConverter(SeverityLevel sdkSeverityLevel)
        {
            this.sdkSeverityLevel = sdkSeverityLevel;
        }

        /// <summary>
        /// Convert from SDK SeverityLevel to API SeverityLevel.
        /// </summary>
        /// <returns>The API sender type.</returns>
        public OneSpanSign.API.SeverityLevel ToAPISeverityLevel()
        {
            if (null == sdkSeverityLevel)
            {
                return apiSeverityLevel;
            }

            return (OneSpanSign.API.SeverityLevel)Enum.Parse(
                typeof(OneSpanSign.API.SeverityLevel),
                sdkSeverityLevel.ToString(),
                true);
        }

        /// <summary>
        /// Convert from API SeverityLevel to SDK SeverityLevel.
        /// </summary>
        /// <returns>The SDK sender type.</returns>
        public SeverityLevel ToSDKSeverityLevel()
        {
            return SeverityLevel.valueOf(apiSeverityLevel.ToString());
        }
    }
}

