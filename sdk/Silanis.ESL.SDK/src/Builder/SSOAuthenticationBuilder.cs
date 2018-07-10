using System;

namespace Silanis.ESL.SDK.Builder
{
    public class SSOAuthenticationBuilder : AuthenticationBuilder
    {
        public SSOAuthenticationBuilder ()
        {
        }

        public override Authentication Build ()
        {
            Authentication result = new Authentication (AuthenticationMethod.SSO);

            return result;
        }
    }
}
