using System;
using Silanis.ESL.SDK.Internal;
using System.Collections.Generic;

namespace Silanis.ESL.SDK
{
    public class KnowledgeBasedAuthenticationBuilder
    {
        private SignerInformationForEquifaxCanada signerInformationForEquifaxCanada = null;
        private SignerInformationForEquifaxUSA signerInformationForEquifaxUSA = null;
        private KnowledgeBasedAuthenticationStatus knowledgeBasedAuthenticationStatus;

        private KnowledgeBasedAuthenticationBuilder(SignerInformationForEquifaxCanada signerInformationForEquifaxCanada)
        {
            this.signerInformationForEquifaxCanada = signerInformationForEquifaxCanada;
        }

        private KnowledgeBasedAuthenticationBuilder(SignerInformationForEquifaxUSA signerInformationForEquifaxUSA)
        {
            this.signerInformationForEquifaxUSA = signerInformationForEquifaxUSA;
        }

        public static KnowledgeBasedAuthenticationBuilder WithSignerInformationForEquifaxCanada(SignerInformationForEquifaxCanada signerInformationForEquifaxCanada)
        {
            return new KnowledgeBasedAuthenticationBuilder(signerInformationForEquifaxCanada);
        }

        public static KnowledgeBasedAuthenticationBuilder WithSignerInformationForEquifaxUSA(SignerInformationForEquifaxUSA signerInformationForEquifaxUSA)
        {
            return new KnowledgeBasedAuthenticationBuilder(signerInformationForEquifaxUSA);
        }

        public KnowledgeBasedAuthentication Build()
        {
            KnowledgeBasedAuthentication result = new KnowledgeBasedAuthentication();

            // There can be one and only one KBA type.

            if (signerInformationForEquifaxCanada != null && signerInformationForEquifaxUSA != null)
            {
                throw new EslException ("There can be only one KBA type.",null);
            }

            if (signerInformationForEquifaxCanada == null)
            {
                result.SignerInformationForEquifaxUSA = signerInformationForEquifaxUSA;
            }

            if (signerInformationForEquifaxUSA == null)
            {
                result.SignerInformationForEquifaxCanada = signerInformationForEquifaxCanada;
            }

            knowledgeBasedAuthenticationStatus = KnowledgeBasedAuthenticationStatus.NOT_YET_ATTEMPTED;
            return result;
        }


    }
}

