using System;
using OneSpanSign.Sdk.Internal;

namespace OneSpanSign.Sdk.Builder
{
    public class SMSAuthenticationBuilder : AuthenticationBuilder
    {
        private readonly string phoneNumber;

        public SMSAuthenticationBuilder(string phoneNumber)
        {
            this.phoneNumber = phoneNumber;
        }

        public override Authentication Build()
        {
            Asserts.NotEmptyOrNull(phoneNumber, "phoneNumber");
            Authentication result = new Authentication(AuthenticationMethod.SMS, phoneNumber);

            return result;
        }
    }
}