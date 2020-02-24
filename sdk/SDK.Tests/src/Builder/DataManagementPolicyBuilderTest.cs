using System;
using NUnit.Framework;
using Silanis.ESL.SDK;

namespace SDK.Tests
{
    [TestFixture]
    public class DataManagementPolicyBuilderTest
    {
        [Test]
        public void BuildTest ()
        {
            TransactionRetention retention = TransactionRetentionBuilder.NewTransactionRetention ().Build ();
            DataManagementPolicyBuilder builder = DataManagementPolicyBuilder.NewDataManagementPolicy ()
                    .WithTransactionRetention (retention);

            DataManagementPolicy result = builder.Build ();

            Assert.IsNotNull (result);
            Assert.IsNotNull (result.TransactionRetention);
        }
    }
}
