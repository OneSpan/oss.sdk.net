using System;
namespace OneSpanSign.Sdk
{
    internal class SenderImageSignatureConverter
    {
        private SenderImageSignature sdkSenderImageSignature;
        private OneSpanSign.API.SenderImageSignature apiSenderImageSignature;

        public SenderImageSignatureConverter(OneSpanSign.API.SenderImageSignature apiSenderImageSignature)
        {
            this.apiSenderImageSignature = apiSenderImageSignature;
        }

        public SenderImageSignatureConverter(SenderImageSignature sdkSenderImageSignature)
        {
            this.sdkSenderImageSignature = sdkSenderImageSignature;
        }

        internal OneSpanSign.API.SenderImageSignature ToAPISenderImageSignature()
        {
            if (sdkSenderImageSignature == null)
            {
                return apiSenderImageSignature;
            }
            apiSenderImageSignature = new OneSpanSign.API.SenderImageSignature();
            apiSenderImageSignature.Content = sdkSenderImageSignature.Content;
            apiSenderImageSignature.FileName = sdkSenderImageSignature.FileName;
            apiSenderImageSignature.MediaType = sdkSenderImageSignature.MediaType;

            return apiSenderImageSignature;
        }

        internal OneSpanSign.Sdk.SenderImageSignature ToSDKSenderImageSignature()
        {
            if (apiSenderImageSignature == null)
            {
                return sdkSenderImageSignature;
            }
            sdkSenderImageSignature = new SenderImageSignature();
            sdkSenderImageSignature.Content = apiSenderImageSignature.Content;
            sdkSenderImageSignature.FileName = apiSenderImageSignature.FileName;
            sdkSenderImageSignature.MediaType = apiSenderImageSignature.MediaType;

            return sdkSenderImageSignature;
        }
    }
}
