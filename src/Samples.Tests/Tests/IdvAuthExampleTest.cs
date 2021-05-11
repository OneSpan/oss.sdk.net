using System.Collections.Generic;
using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    [TestFixture()]
    public class IdvAuthExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            IdvAuthExample example = new IdvAuthExample();
            example.Run();

            DocumentPackage retrievedPackage = example.RetrievedPackage;

            AssertIdvWorkflowConfigs(new List<IdvWorkflowConfig>(), example.idvWorkflowConfigsBeforeCreating);
            AssertIdvWorkflowConfigs(example.idvWorkflowConfigsToBeCreated, example.idvWorkflowConfigsAfterCreating);
            AssertIdvWorkflowConfigs(example.idvWorkflowConfigsToBeUpdated, example.idvWorkflowConfigsAfterUpdating);
            AssertIdvWorkflowConfigs(new List<IdvWorkflowConfig>(), example.idvWorkflowConfigsAfterDeleting);

            Signer signer = retrievedPackage.GetSigner(example.email1);
            Assert.AreEqual(AuthenticationMethod.IDV, signer.AuthenticationMethod);
            Assert.AreEqual(0, signer.ChallengeQuestion.Count);
            Assert.AreEqual(IdvAuthExample.PHONE_NUMBER, signer.PhoneNumber);

            IdvWorkflow idvWorkflow = signer.Authentication.IdvWorkflow;
            Assert.AreEqual(IdvAuthExample.IDV_WORKFLOW_ID1, idvWorkflow.Id);
            Assert.AreEqual(IdvAuthExample.TENANT, idvWorkflow.Tenant);
        }

        private void AssertIdvWorkflowConfigs(IList<IdvWorkflowConfig> expected, IList<IdvWorkflowConfig> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count);

            if (actual.Count == 0)
            {
                return;
            }

            foreach (IdvWorkflowConfig config in expected)
            {
                IdvWorkflowConfig idvWorkflowConfig = FindById(config.Id, actual);
                if (idvWorkflowConfig == null)
                {
                    Assert.Fail("The idvWorkflowConfig is not set correctly");
                }

                AssertIdvWorkflowConfig(config, idvWorkflowConfig);
            }
        }

        private IdvWorkflowConfig FindById(string id, IList<IdvWorkflowConfig> idvWorkflowConfigs)
        {
            foreach (IdvWorkflowConfig config in idvWorkflowConfigs)
            {
                if (id == config.Id)
                {
                    return config;
                }
            }

            return null;
        }

        private void AssertIdvWorkflowConfig(IdvWorkflowConfig expected, IdvWorkflowConfig actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Type, actual.Type);
            Assert.AreEqual(expected.Desc, actual.Desc);
            Assert.AreEqual(expected.SkipWhenAccessingSignedDocuments, actual.SkipWhenAccessingSignedDocuments);
        }
    }
}