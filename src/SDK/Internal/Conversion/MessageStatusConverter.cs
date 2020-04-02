using System;

namespace OneSpanSign.Sdk
{
    internal class MessageStatusConverter
    {
        private OneSpanSign.Sdk.MessageStatus sdkMessageStatus;
        private string apiMessageStatus;

        /// <summary>
        /// Construct with API MessageStatus object involved in conversion.
        /// </summary>
        /// <param name="apiMessageStatus">API message status.</param>
        public MessageStatusConverter(string apiMessageStatus)
        {
            this.apiMessageStatus = apiMessageStatus;
        }

        /// <summary>
        /// Construct with SDK MessageStatus object involved in conversion.
        /// </summary>
        /// <param name="sdkMessageStatus">SDK message status.</param>
        public MessageStatusConverter(OneSpanSign.Sdk.MessageStatus sdkMessageStatus)
        {
            this.sdkMessageStatus = sdkMessageStatus;
        }

        /// <summary>
        /// Convert from SDK MessageStatus to API MessageStatus.
        /// </summary>
        /// <returns>The API message status.</returns>
        public string ToAPIMessageStatus()
        {
            return sdkMessageStatus.getApiValue();
        }

        /// <summary>
        /// Convert from API MessageStatus to SDK MessageStatus.
        /// </summary>
        /// <returns>The SDK message status.</returns>
        public OneSpanSign.Sdk.MessageStatus ToSDKMessageStatus()
        {
            return OneSpanSign.Sdk.MessageStatus.valueOf(apiMessageStatus);
        }
    }
}

