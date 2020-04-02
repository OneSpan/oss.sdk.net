using System;
using System.Collections.Generic;
using OneSpanSign.API;

namespace OneSpanSign.Sdk
{
    internal class AuthenticationConverter
    {
        private OneSpanSign.API.Auth apiAuth = null;
        private OneSpanSign.Sdk.Authentication sdkAuth = null;

        public AuthenticationConverter(OneSpanSign.API.Auth apiAuth)
        {
            this.apiAuth = apiAuth;
        }

        public AuthenticationConverter(OneSpanSign.Sdk.Authentication sdkAuth)
        {
            this.sdkAuth = sdkAuth;
        }

        internal OneSpanSign.API.Auth ToAPIAuthentication()
        {
            if (sdkAuth == null)
            {
                return apiAuth;
            }

            OneSpanSign.API.Auth auth = new OneSpanSign.API.Auth();

            auth.Scheme = new AuthenticationMethodConverter(sdkAuth.Method).ToAPIAuthMethod();

            foreach (Challenge challenge in sdkAuth.Challenges)
            {
                OneSpanSign.API.AuthChallenge authChallenge = new OneSpanSign.API.AuthChallenge();

                authChallenge.Question = challenge.Question;
                authChallenge.Answer = challenge.Answer;
                authChallenge.MaskInput = challenge.MaskOption == Challenge.MaskOptions.MaskInput;
                auth.AddChallenge(authChallenge);
            }

            if (!String.IsNullOrEmpty(sdkAuth.PhoneNumber))
            {
                OneSpanSign.API.AuthChallenge challenge = new OneSpanSign.API.AuthChallenge();

                challenge.Question = sdkAuth.PhoneNumber;
                auth.AddChallenge(challenge);
            }

            return auth;
        }

        internal OneSpanSign.Sdk.Authentication ToSDKAuthentication()
        {
            if (apiAuth == null)
            {
                return sdkAuth;
            }

            string telephoneNumber = null;
            OneSpanSign.Sdk.Authentication sdkAuthentication = null;

            if (apiAuth.Challenges.Count == 0) 
            {
                sdkAuthentication = new Authentication (new AuthenticationMethodConverter(apiAuth.Scheme).ToSDKAuthMethod());
            } 
            else 
            {
                IList<Challenge> sdkChallenges = new List<Challenge>();
                foreach (AuthChallenge apiChallenge in apiAuth.Challenges)
                {
                    if (AuthenticationMethod.CHALLENGE.getApiValue().Equals(apiAuth.Scheme))
                    {
                        sdkChallenges.Add(new ChallengeConverter(apiChallenge).ToSDKChallenge());
                    }
                    else
                    {
                        telephoneNumber = apiChallenge.Question;
                        break;
                    }
                }

                if (AuthenticationMethod.CHALLENGE.getApiValue().Equals(apiAuth.Scheme))
                {
                    sdkAuthentication = new OneSpanSign.Sdk.Authentication(sdkChallenges);
                }
                else
                {
                    sdkAuthentication = new OneSpanSign.Sdk.Authentication(telephoneNumber);
                }
            }

            return sdkAuthentication;
        }
    }
}

