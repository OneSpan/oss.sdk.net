using System;

namespace OneSpanSign.Sdk
{
    public class KnowledgeBasedAuthentication
    {

        public KnowledgeBasedAuthentication() 
        {
        }

        public SignerInformationForEquifaxCanada SignerInformationForEquifaxCanada
        {
            get; set;
        }

        public SignerInformationForEquifaxUSA SignerInformationForEquifaxUSA
        {
            get; set;
        }

        public KnowledgeBasedAuthenticationStatus KnowledgeBasedAuthenticationStatus
        {
            get; set;
        }
    }
}

