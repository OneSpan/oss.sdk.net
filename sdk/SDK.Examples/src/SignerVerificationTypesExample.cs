using System;
using Silanis.ESL.API;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;
using System.Collections.Generic;

namespace SDK.Examples
{
    public class SignerVerificationTypesExample : SDKSample
    {
        public IList<VerificationType> verificationTypes;

        public static void Main(string[] args)
        {
            new SignerVerificationExample().Run();
        }

        override public void Execute()
        {
            this.verificationTypes = eslClient.AccountService.getVerificationTypes();
        }
    }
}

