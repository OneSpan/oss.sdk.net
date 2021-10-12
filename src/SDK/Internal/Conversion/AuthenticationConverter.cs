using System;
using System.Collections.Generic;
using OneSpanSign.API;
using OneSpanSign.Sdk.Internal.Conversion;

namespace OneSpanSign.Sdk
{
    internal class AuthenticationConverter
    {
        private Auth apiAuth = null;
        private Authentication sdkAuth = null;

        public AuthenticationConverter(Auth apiAuth)
        {
            this.apiAuth = apiAuth;
        }

        public AuthenticationConverter(Authentication sdkAuth)
        {
            this.sdkAuth = sdkAuth;
        }

        internal Auth ToAPIAuthentication()
        {
            if (sdkAuth == null)
            {
                return apiAuth;
            }

            Auth auth = new Auth();

            auth.Scheme = new AuthenticationMethodConverter(sdkAuth.Method).ToAPIAuthMethod();

            foreach (Challenge challenge in sdkAuth.Challenges)
            {
                AuthChallenge authChallenge = new AuthChallenge();

                authChallenge.Question = challenge.Question;
                authChallenge.Answer = challenge.Answer;
                authChallenge.MaskInput = challenge.MaskOption == Challenge.MaskOptions.MaskInput;
                auth.AddChallenge(authChallenge);
            }
            
            if (sdkAuth.IdvWorkflow != null) 
            {
                auth.IdvWorkflow = new IdvWorkflowConverter(sdkAuth.IdvWorkflow).ToAPIIdvWorkflow();
            }

            if (!String.IsNullOrEmpty(sdkAuth.PhoneNumber))
            {
                AuthChallenge challenge = new AuthChallenge();

                challenge.Question = sdkAuth.PhoneNumber;
                auth.AddChallenge(challenge);
            }

            return auth;
        }

        internal Authentication ToSDKAuthentication()
        {
            if (apiAuth == null)
            {
                return sdkAuth;
            }

            string telephoneNumber = null;
            Authentication sdkAuthentication = null;

            if (apiAuth.Challenges.Count == 0)
            {
                sdkAuthentication =
                    new Authentication(new AuthenticationMethodConverter(apiAuth.Scheme).ToSDKAuthMethod());
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
                    sdkAuthentication = new Authentication(sdkChallenges);
                }
                else if(AuthenticationMethod.SMS.getApiValue().Equals(apiAuth.Scheme))
                {
                    sdkAuthentication = new Authentication(AuthenticationMethod.SMS, telephoneNumber);
                }
                else if (AuthenticationMethod.IDV.getApiValue().Equals(apiAuth.Scheme))
                {
                    sdkAuthentication = new Authentication(AuthenticationMethod.IDV, telephoneNumber,  
                        new IdvWorkflowConverter(apiAuth.IdvWorkflow).ToSDKIdvWorkflow());
                }
            }

            return sdkAuthentication;
        }
    }
}