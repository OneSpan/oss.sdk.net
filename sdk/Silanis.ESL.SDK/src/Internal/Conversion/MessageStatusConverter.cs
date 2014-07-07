using System;

namespace Silanis.ESL.SDK
{
    internal class MessageStatusConverter
    {
        private Silanis.ESL.SDK.MessageStatus sdkMessageStatus;
        private Silanis.ESL.API.MessageStatus apiMessageStatus;

        /// <summary>
        /// Construct with API MessageStatus object involved in conversion.
        /// </summary>
        /// <param name="apiMessageStatus">API message status.</param>
        public MessageStatusConverter(Silanis.ESL.API.MessageStatus apiMessageStatus)
        {
            this.apiMessageStatus = apiMessageStatus;
        }

        /// <summary>
        /// Construct with SDK MessageStatus object involved in conversion.
        /// </summary>
        /// <param name="sdkMessageStatus">SDK message status.</param>
        public MessageStatusConverter(Silanis.ESL.SDK.MessageStatus sdkMessageStatus)
        {
            this.sdkMessageStatus = sdkMessageStatus;
        }

        /// <summary>
        /// Convert from SDK MessageStatus to API MessageStatus.
        /// </summary>
        /// <returns>The API message status.</returns>
        public Silanis.ESL.API.MessageStatus ToAPIMessageStatus()
        {
            switch (sdkMessageStatus)
            {
                case Silanis.ESL.SDK.MessageStatus.NEW:
                    return Silanis.ESL.API.MessageStatus.NEW;
                case Silanis.ESL.SDK.MessageStatus.READ:
                    return Silanis.ESL.API.MessageStatus.READ;
                case Silanis.ESL.SDK.MessageStatus.TRASHED:
                    return Silanis.ESL.API.MessageStatus.TRASHED;
                default:
                    throw new EslException(String.Format("Unable to decode the message status {0}", sdkMessageStatus), null);
            }
        }

        /// <summary>
        /// Convert from API MessageStatus to SDK MessageStatus.
        /// </summary>
        /// <returns>The SDK message status.</returns>
        public Silanis.ESL.SDK.MessageStatus ToSDKMessageStatus()
        {
            switch (apiMessageStatus)
            {
                case Silanis.ESL.API.MessageStatus.NEW:
                    return Silanis.ESL.SDK.MessageStatus.NEW;
                case Silanis.ESL.API.MessageStatus.READ:
                    return Silanis.ESL.SDK.MessageStatus.READ;
                case Silanis.ESL.API.MessageStatus.TRASHED:
                    return Silanis.ESL.SDK.MessageStatus.TRASHED;
                default:
                    throw new EslException(String.Format("Unable to decode the message status {0}", apiMessageStatus), null);
            }
        }
    }
}

