using System;

namespace OneSpanSign.Sdk
{
    /// <summary>
    /// Converter for SDK Challenge and API Challenge.
    /// </summary>
    internal class ChallengeConverter
    {
        private OneSpanSign.API.AuthChallenge apiChallenge = null;
        private OneSpanSign.Sdk.Challenge sdkChallenge = null;

        /// <summary>
        /// Construct with API object involved in conversion.
        /// </summary>
        /// <param name="apiChallenge">API challenge.</param>
        public ChallengeConverter(OneSpanSign.API.AuthChallenge apiChallenge)
        {
            this.apiChallenge = apiChallenge;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OneSpanSign.Sdk.ChallengeConverter"/> class.
        /// </summary>
        /// <param name="sdkChallenge">Sdk challenge.</param>
        public ChallengeConverter(OneSpanSign.Sdk.Challenge sdkChallenge)
        {
            this.sdkChallenge = sdkChallenge;
        }

        public OneSpanSign.API.AuthChallenge ToAPIChallenge()
        {
            if (sdkChallenge == null)
            {
                return apiChallenge;
            }

            OneSpanSign.API.AuthChallenge result = new OneSpanSign.API.AuthChallenge();
            result.Question = sdkChallenge.Question;
            result.Answer = sdkChallenge.Answer;

            switch (sdkChallenge.MaskOption)
            {
                case Challenge.MaskOptions.MaskInput:
                    result.MaskInput = true;
                    break;
                case Challenge.MaskOptions.None:
                    result.MaskInput = false;
                    break;
                default:
                    result.MaskInput = true;
                    break;
            }

            return result;
        }

        public OneSpanSign.Sdk.Challenge ToSDKChallenge()
        {
            if (apiChallenge == null)
            {
                return sdkChallenge;
            }

            OneSpanSign.Sdk.Challenge result; 

            if (apiChallenge.MaskInput.Value)
            {
                result = new OneSpanSign.Sdk.Challenge(apiChallenge.Question, apiChallenge.Answer, Challenge.MaskOptions.MaskInput);
            }
            else
            {
                result = new OneSpanSign.Sdk.Challenge(apiChallenge.Question, apiChallenge.Answer);
            }

            return result;
        }
    }
}

